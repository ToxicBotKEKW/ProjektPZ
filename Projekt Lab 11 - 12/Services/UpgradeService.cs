using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Projekt_Lab_11___12.Data;
using Projekt_Lab_11___12.Models.Entities;
using Projekt_Lab_11___12.Models.ViewModels;
using Projekt_Lab_11___12.Services.Interfaces;

namespace Projekt_Lab_11___12.Services
{
    public class UpgradeService : IUpgradeService
    {
        private readonly Projekt_Lab_11___12Context _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<User> _userManager;

        public UpgradeService(Projekt_Lab_11___12Context context, IHttpContextAccessor httpContextAccessor, UserManager<User> userManager)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
        }

        public async Task<UpgradeViewModel> UpgradeViewModel()
        {
            var user = await _userManager.GetUserAsync(_httpContextAccessor.HttpContext.User);
            if (user == null) return null;

            user = await _context.Users
                .Include(u => u.IronMine)
                .Include(u => u.GoldMine)
                .Include(u => u.DiamondMine)
                .FirstOrDefaultAsync(u => u.Id == user.Id);

            PointForClick ironMinePointForClick = await _context.PointForClicks
                .FirstOrDefaultAsync(x => x.Level == user.IronMine.Level && x.ResourceType == user.IronMine.MineResourceType);

            PointForClick goldMinePointForClick = await _context.PointForClicks
                .FirstOrDefaultAsync(x => x.Level == user.GoldMine.Level && x.ResourceType == user.GoldMine.MineResourceType);

            PointForClick diamondMinePointForClick = await _context.PointForClicks
                .FirstOrDefaultAsync(x => x.Level == user.DiamondMine.Level && x.ResourceType == user.DiamondMine.MineResourceType);

            List<LevelRequirement> ironMineLevelRequirements = await _context.LevelRequirements
                .Where(x => x.Level == (user.IronMine.Level + 1) && x.MineResourceType == user.IronMine.MineResourceType)
                .ToListAsync();

            List<LevelRequirement> goldMineLevelRequirements = await _context.LevelRequirements
                .Where(x => x.Level == (user.GoldMine.Level + 1) && x.MineResourceType == user.GoldMine.MineResourceType)
                .ToListAsync();

            List<LevelRequirement> diamondMineLevelRequirements = await _context.LevelRequirements
                .Where(x => x.Level == (user.DiamondMine.Level + 1) && x.MineResourceType == user.DiamondMine.MineResourceType)
                .ToListAsync();


            UpgradeViewModel viewModel = new UpgradeViewModel()
            {
                Iron = user.Iron,
                Gold = user.Gold,
                Diamond = user.Diamond,
                IronMine = user.IronMine,
                GoldMine = user.GoldMine,
                DiamondMine = user.DiamondMine,
                IronMinePointForClick = ironMinePointForClick,
                GoldMinePointForClick = goldMinePointForClick,
                DiamondMinePointForClick = diamondMinePointForClick,
                IronMineLevelRequirments = ironMineLevelRequirements,
                GoldMineLevelRequirments = goldMineLevelRequirements,
                DiamondMineLevelRequirments = diamondMineLevelRequirements
            };

            return viewModel;
        }

        public async Task<(bool Succes, string Message)> Upgrade(int upgradeMineId)
        {
            var user = await _userManager.GetUserAsync(_httpContextAccessor.HttpContext.User);
            if (user == null)
            {
                return (false, "Użytkownik nie jest zalogowany.");
            }

            user = await _context.Users
                .Include(u => u.IronMine)
                .Include(u => u.GoldMine)
                .Include(u => u.DiamondMine)
                .FirstOrDefaultAsync(u => u.Id == user.Id);

            Mine mine = await _context.Mines.FirstOrDefaultAsync(x => x.Id == upgradeMineId);

            bool mineBelongsToUser =
                (user.IronMine != null && user.IronMine.Id == mine.Id) ||
                (user.GoldMine != null && user.GoldMine.Id == mine.Id) ||
                (user.DiamondMine != null && user.DiamondMine.Id == mine.Id);

            if (!mineBelongsToUser)
            {
                return (false, "Ta kopalnie nie należy do ciebie.");
            }

            List<LevelRequirement> diamondMineLevelRequirements = await _context.LevelRequirements
               .Where(x => x.Level == (mine.Level + 1) && x.MineResourceType == mine.MineResourceType)
               .ToListAsync();

            if (diamondMineLevelRequirements == null)
            {
                return (false, "Masz maksymalny poziom kopalni.");
            }

            double iron = 0;
            double gold = 0;
            double diamond = 0;

            foreach(var item in diamondMineLevelRequirements)
            {
                if (item.ResourceType == ResourceType.Iron)
                {
                    iron += item.Amount;
                }
                else if (item.ResourceType == ResourceType.Gold)
                {
                    gold += item.Amount;
                }
                else if (item.ResourceType == ResourceType.Diamond)
                {
                    diamond += item.Amount;
                }
            }

            if (
                user.Iron >= iron && 
                user.Gold >= gold && 
                user.Diamond >= diamond
                )
            {
                user.Iron -= iron;
                user.Gold -= gold;
                user.Diamond -= diamond;
                mine.Level += 1;
            }
            else
            {
                Console.WriteLine("Nope");
                return (false, "Nie stać cię na ulepszenie kopalni.");
            }

            await _context.SaveChangesAsync();

            return (true, "Poprawnie udało się ulepszyć kopalnie.");
        }
    }
}
