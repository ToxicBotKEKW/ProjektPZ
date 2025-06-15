using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Projekt_Lab_11___12.Data;
using Projekt_Lab_11___12.Models.DTO;
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
        private readonly IWebHostEnvironment _webHostEnvironment;

        public AdminService(Projekt_Lab_11___12Context context, IHttpContextAccessor httpContextAccessor, UserManager<User> userManager, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
            _webHostEnvironment = webHostEnvironment;
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
                Emerald = user.Emerald,
                PickaxesInSystem = pickaxesInSystem,
                PickaxesInShop = pickaxeInShops,
                NewPickaxe = new NewPickaxeDTO()
            };

            return model;
        }


        public async Task AddToShop(int pickaxeShopId, double ironCost, double goldCost, double diamondCost)
        {
            if(await _context.PickaxeShops.FirstOrDefaultAsync(x => x.Id == pickaxeShopId) != null) { 
                return; 
            }

            var pickaxe = await _context.Pickaxes
                .FirstOrDefaultAsync(x => x.Id == pickaxeShopId);

            if (pickaxe == null) { 
                return ;
            }

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

        public async Task CreatePickaxe(AdminViewModel adminViewModel)
        {
            string uniqueFileName = null;
            if (adminViewModel.NewPickaxe.ImageFile != null && adminViewModel.NewPickaxe.ImageFile.Length > 0)
            {
                var uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "img", "pickaxe");
                Directory.CreateDirectory(uploadsFolder);

                var fileExtension = Path.GetExtension(adminViewModel.NewPickaxe.ImageFile.FileName);
                string fileName;

                do
                {
                    fileName = Guid.NewGuid().ToString() + fileExtension;
                    uniqueFileName = fileName;
                }
                while (System.IO.File.Exists(Path.Combine(uploadsFolder, fileName)));

                var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await adminViewModel.NewPickaxe.ImageFile.CopyToAsync(stream);
                }
            }

            var pickaxe = new Pickaxe
            {
                Name = adminViewModel.NewPickaxe.Name,
                ImageName = uniqueFileName,
                RequirmentLevel = adminViewModel.NewPickaxe.RequirmentLevel,
                PickaxeResourceMultipliers = new List<PickaxeResourceMultiplier>
                {
                    new PickaxeResourceMultiplier { ResourceType = ResourceType.Iron, Value = adminViewModel.NewPickaxe.IronMultipler },
                    new PickaxeResourceMultiplier { ResourceType = ResourceType.Gold, Value = adminViewModel.NewPickaxe.GoldMultipler },
                    new PickaxeResourceMultiplier { ResourceType = ResourceType.Diamond, Value = adminViewModel.NewPickaxe.DiamondMultipler }
                }
            };

            _context.Pickaxes.Add(pickaxe);
            await _context.SaveChangesAsync();
        }
    }
}
