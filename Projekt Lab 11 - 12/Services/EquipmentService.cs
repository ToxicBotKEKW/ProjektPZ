using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Projekt_Lab_11___12.Data;
using Projekt_Lab_11___12.Models.Entities;
using Projekt_Lab_11___12.Models.ViewModels;
using Projekt_Lab_11___12.Services.Interfaces;

namespace Projekt_Lab_11___12.Services
{
    public class EquipmentService : IEquipmentService
    {
        private readonly Projekt_Lab_11___12Context _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<User> _userManager;

        public EquipmentService(
            Projekt_Lab_11___12Context context,
            IHttpContextAccessor httpContextAccessor,
            UserManager<User> userManager)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
        }

        public async Task<EquipmentViewModel> EquipmentViewModel()
        {
            var user = await _userManager.GetUserAsync(_httpContextAccessor.HttpContext.User);
            if (user == null)
            {
                return null;
            }

            user = await _context.Users
                .Include(x => x.PickaxesEq)
                    .ThenInclude(x => x.PickaxeResourceMultipliers)
                .FirstOrDefaultAsync(x => x.Id == user.Id);

            EquipmentViewModel viewModel = new()
            {
                Iron = user.Iron,
                Gold = user.Gold,
                Diamond = user.Diamond,
                Emerald = user.Emerald,
                PickaxesEq = user.PickaxesEq,
                UsedPickaxeId = user.UsedPickaxeId
            };

            return viewModel;
        }

        public async Task<(bool Succes, String Message)> Equip(int pickaxeId)
        {
            var user = await _userManager.GetUserAsync(_httpContextAccessor.HttpContext.User);
            if(user == null)
            {
                return (false, "Użytkownik nie jest zalogowany.");
            }

            user = await _context.Users
                .Include (x => x.PickaxesEq)
                    .ThenInclude(x => x.PickaxeResourceMultipliers)
                .FirstOrDefaultAsync(x => x.Id == user.Id);

            Pickaxe pickaxe = user.PickaxesEq.FirstOrDefault(x => x.Id == pickaxeId);

            if (pickaxe == null) 
            {
                return (false, "Nie posiadasz tego kilofa!");
            }

            user.UsedPickaxe = pickaxe;

            await _context.SaveChangesAsync();

            return (true, "Poprawnie wyposażono kilof!");
        }
    }
}
