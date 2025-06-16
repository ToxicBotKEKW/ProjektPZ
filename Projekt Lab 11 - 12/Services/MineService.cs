using Projekt_Lab_11___12.Data;
using Projekt_Lab_11___12.Services.Interfaces;
using Projekt_Lab_11___12.Models.Entities;
using Projekt_Lab_11___12.Models.ViewModels;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Projekt_Lab_11___12.Services
{
    public class MineService : IMineService
    {
        private readonly Projekt_Lab_11___12Context _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<User> _userManager;

        public MineService(
            Projekt_Lab_11___12Context context,
            IHttpContextAccessor httpContextAccessor,
            UserManager<User> userManager)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
        }

        public async Task<MineViewModel> MineViewModel()
        {
            var user = await _userManager.GetUserAsync(_httpContextAccessor.HttpContext.User);
            if (user == null)
            {
                return null;
            }

            user = await _context.Users
                .Include(u => u.UsedPickaxe)
                    .ThenInclude(p => p.PickaxeResourceMultipliers)
                .Include(u => u.IronMine)
                    .ThenInclude(u => u.BonusClicks)
                .Include(u => u.GoldMine)
                    .ThenInclude(u => u.BonusClicks)
                .Include(u => u.DiamondMine)
                    .ThenInclude(u => u.BonusClicks)
                .FirstOrDefaultAsync(u => u.Id == user.Id);

            var model = new MineViewModel
            {
                Iron = user.Iron,
                Gold = user.Gold,
                Diamond = user.Diamond,
                Emerald = user.Emerald,
                IronMine = user.IronMine,
                GoldMine = user.GoldMine,
                DiamondMine = user.DiamondMine,
            };

            return model;
        }

        public async Task<string> PointsForClick(ResourceType resourceType)
        {
            var user = await _userManager.GetUserAsync(_httpContextAccessor.HttpContext.User);
            if (user == null) return "0";

            user = await _context.Users
                .Include(u => u.UsedPickaxe)
                    .ThenInclude(p => p.PickaxeResourceMultipliers)
                .Include(u => u.IronMine)
                    .ThenInclude(u => u.BonusClicks)
                .Include(u => u.GoldMine)
                    .ThenInclude(u => u.BonusClicks)
                .Include(u => u.DiamondMine)
                    .ThenInclude(u => u.BonusClicks)
                .FirstOrDefaultAsync(u => u.Id == user.Id);

            int mineLevel = resourceType switch
            {
                ResourceType.Iron => user.IronMine?.Level ?? 0,
                ResourceType.Gold => user.GoldMine?.Level ?? 0,
                ResourceType.Diamond => user.DiamondMine?.Level ?? 0,
                _ => 0
            };

            double multiplier = 0;

            if(user.UsedPickaxe != null)
            {
                if (mineLevel < user.UsedPickaxe.RequirmentLevel)
                {
                    return "Masz za niski poziom kopalni aby używać tego kilofa.";
                }

                multiplier = user.UsedPickaxe.PickaxeResourceMultipliers
                    .Where(x => x.ResourceType == resourceType)
                    .Sum(x => x.Value);
            }
            else
            {
                multiplier = 0;
            }


            multiplier *= 0.01;

            double pointsForClick = _context.PointForClicks
                .Where(x => x.Level == mineLevel && x.ResourceType == resourceType)
                .Sum(x => x.Amount);

            pointsForClick = pointsForClick == null ? 0 : pointsForClick;

            double points = pointsForClick + (pointsForClick * multiplier);

            switch (resourceType)
            {
                case ResourceType.Iron:
                    points += user.IronMine.PermAdditionalValue;
                    user.Iron += points;
                    break;
                case ResourceType.Gold:
                    points += user.GoldMine.PermAdditionalValue;
                    user.Gold += points;
                    break;
                case ResourceType.Diamond:
                    points += user.DiamondMine.PermAdditionalValue;
                    user.Diamond += points;
                    break;
            }

            await _userManager.UpdateAsync(user);
            return $"{points}";
        }


        public async Task<string> BonusPointsForClick(ResourceType resourceType)
        {
            var user = await _userManager.GetUserAsync(_httpContextAccessor.HttpContext.User);
            if (user == null) return "0";

            user = await _context.Users
                .Include(u => u.UsedPickaxe)
                    .ThenInclude(p => p.PickaxeResourceMultipliers)
                .Include(u => u.IronMine)
                    .ThenInclude(u => u.BonusClicks)
                .Include(u => u.GoldMine)
                    .ThenInclude(u => u.BonusClicks)
                .Include(u => u.DiamondMine)
                    .ThenInclude(u => u.BonusClicks)
                .FirstOrDefaultAsync(u => u.Id == user.Id);

            int mineLevel = resourceType switch
            {
                ResourceType.Iron => user.IronMine?.Level ?? 0,
                ResourceType.Gold => user.GoldMine?.Level ?? 0,
                ResourceType.Diamond => user.DiamondMine?.Level ?? 0,
                _ => 0
            };

            double multiplier = 0;

            if (user.UsedPickaxe != null)
            {
                if (mineLevel < user.UsedPickaxe.RequirmentLevel)
                {
                    return "Masz za niski poziom kopalni aby używać tego kilofa.";
                }

                multiplier = user.UsedPickaxe.PickaxeResourceMultipliers
                    .Where(x => x.ResourceType == resourceType)
                    .Sum(x => x.Value);
            }
            else
            {
                multiplier = 0;
            }


            multiplier *= 0.01;

            double pointsForClick = _context.PointForClicks
                .Where(x => x.Level == mineLevel && x.ResourceType == resourceType)
                .Sum(x => x.Amount);

            pointsForClick = pointsForClick == null ? 0 : pointsForClick;

            double points = pointsForClick + (pointsForClick * multiplier);

            switch (resourceType)
            {
                case ResourceType.Iron:
                    if (user.IronMine.BonusClicks.CurrentClicks > 0)
                    {
                        points += user.IronMine.PermAdditionalValue;
                        points *= 2;
                        user.Iron += points;
                        user.IronMine.BonusClicks.CurrentClicks -= 1;
                    }
                    else
                    {
                        return "Nie masz już dostępnych bonusowych kliknięć.";
                    }
                    break;
                case ResourceType.Gold:
                    if (user.GoldMine.BonusClicks.CurrentClicks > 0)
                    {
                        points += user.GoldMine.PermAdditionalValue;
                        points *= 2;
                        user.Gold += points;
                        user.GoldMine.BonusClicks.CurrentClicks -= 1;
                    }
                    else
                    {
                        return "Nie masz już dostępnych bonusowych kliknięć.";
                    }
                    break;
                case ResourceType.Diamond:
                    if (user.DiamondMine.BonusClicks.CurrentClicks > 0)
                    {
                        points += user.DiamondMine.PermAdditionalValue;
                        points *= 2;
                        user.Diamond += points;
                        user.DiamondMine.BonusClicks.CurrentClicks -= 1;
                    }
                    else
                    {
                        return "Nie masz już dostępnych bonusowych kliknięć.";
                    }
                    break;
            }

            await _userManager.UpdateAsync(user);
            return $"{points}";
        }
    }
}
