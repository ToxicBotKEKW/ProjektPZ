using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Projekt_Lab_11___12.Data;
using Projekt_Lab_11___12.Models.Entities;
using Projekt_Lab_11___12.Models.ViewModels;
using Projekt_Lab_11___12.Services.Interfaces;

namespace Projekt_Lab_11___12.Services
{
    public class AdminService : IAdminService
    {
        private readonly Projekt_Lab_11___12Context _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<User> _userManager;

        public AdminService(Projekt_Lab_11___12Context context, IHttpContextAccessor httpContextAccessor, UserManager<User> userManager)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
        }

        public async Task<AdminViewModel> AdminViewModel()
        {
            var user = await _userManager.GetUserAsync(_httpContextAccessor.HttpContext.User);
            if (user == null)
            {
                return null;
            }

            user = await _context.Users
                .FirstOrDefaultAsync(x => x.Id == user.Id);

            List<Pickaxe> pickaxesInSystem = await _context.Pickaxes
                .Include(x => x.PickaxeResourceMultipliers)
                .ToListAsync();
            List<PickaxeShop> pickaxeInShops = await _context.PickaxeShops
                .Include(x => x.PickaxeShopResourceCosts)
                .ToListAsync();

            AdminViewModel model = new AdminViewModel()
            {
                Iron = user.Iron,
                Gold = user.Gold,
                Diamond = user.Diamond,
                PickaxesInSystem = pickaxesInSystem,
                PickaxesInShop = pickaxeInShops
            };

            return model;
        }


        public async Task AddToShop(int pickaxeShopId, double ironCost, double goldCost, double diamondCost)
        {
            Console.WriteLine("1");

            if(await _context.PickaxeShops.FirstOrDefaultAsync(x => x.Id == pickaxeShopId) != null) { 
                return; 
            }

            Console.WriteLine("2");

            var pickaxe = await _context.Pickaxes
                .FirstOrDefaultAsync(x => x.Id == pickaxeShopId);

            if (pickaxe == null) { 
                return ;
            }
            Console.WriteLine("3");

            PickaxeShop pickaxeShop = new PickaxeShop()
            {
                Pickaxe = pickaxe,
                PickaxeShopResourceCosts = new List<PickaxeShopResourceCost>()
                {
                    new PickaxeShopResourceCost() {
                        ResourceType = ResourceType.Iron,
                        Value = ironCost,
                    },
                    new PickaxeShopResourceCost() {
                        ResourceType = ResourceType.Gold,
                        Value = goldCost,
                    },
                    new PickaxeShopResourceCost() {
                        ResourceType = ResourceType.Diamond,
                        Value = diamondCost,
                    }
                }
            };

            Console.WriteLine("4");

            _context.PickaxeShops.Add(pickaxeShop);

            await _context.SaveChangesAsync();

        }


        public async Task RemoveFromShop(int pickaxeShopId)
        {
            var pickaxeShop = await _context.PickaxeShops.FirstOrDefaultAsync(x => x.Id == pickaxeShopId);

            if (pickaxeShop == null)
            {
                return;
            }

            _context.PickaxeShops.Remove(pickaxeShop);

            await _context.SaveChangesAsync();
        }
    }
}
