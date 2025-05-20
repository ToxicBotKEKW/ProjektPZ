using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Projekt_Lab_11___12.Data;
using Projekt_Lab_11___12.Models.Entities;
using Projekt_Lab_11___12.Models.ViewModels;
using Projekt_Lab_11___12.Services.Interfaces;

namespace Projekt_Lab_11___12.Services
{
    public class StoreService : IStoreService
    {
        private readonly Projekt_Lab_11___12Context _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<User> _userManager;

        public StoreService(
            Projekt_Lab_11___12Context context, 
            IHttpContextAccessor httpContextAccessor, 
            UserManager<User> userManager)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
        }

        public async Task<StoreViewModel> StoreViewModel(int page = 1, int pageSize = 1, string searchQuery = "", string sortOrder = "")
        {
            var user = await _userManager.GetUserAsync(_httpContextAccessor.HttpContext.User);
            if (user == null) return null;

            user = await _context.Users
                .Include(u => u.PickaxesEq)
                .FirstOrDefaultAsync(u => u.Id == user.Id);

            List<int> userPickaxeIds = user.PickaxesEq.Select(p => p.Id).ToList();

            var query = _context.PickaxeShops
                .Include(x => x.Pickaxe)
                    .ThenInclude(x => x.PickaxeResourceMultipliers)
                .Include(x => x.PickaxeShopResourceCosts)
                .Where(x => !userPickaxeIds.Contains(x.PickaxeId));

            if (!string.IsNullOrWhiteSpace(searchQuery))
            {
                query = query.Where(x => x.Pickaxe.Name.Contains(searchQuery));
            }

            switch (sortOrder)
            {
                case "level":
                    query = query.OrderBy(x => x.Pickaxe.RequirmentLevel);
                    break;
                case "level_desc":
                    query = query.OrderByDescending(x => x.Pickaxe.RequirmentLevel);
                    break;
                case "price":
                    query = query.OrderBy(x => x.PickaxeShopResourceCosts.Sum(c => c.Value));
                    break;
                case "price_desc":
                    query = query.OrderByDescending(x => x.PickaxeShopResourceCosts.Sum(c => c.Value));
                    break;
                default:
                    query = query.OrderBy(x => x.Pickaxe.Name);
                    break;
            }

            var allItems = await query.ToListAsync();

            var pagedItems = allItems
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            var viewModel = new StoreViewModel
            {
                Iron = user.Iron,
                Gold = user.Gold,
                Diamond = user.Diamond,
                CurrentPage = page,
                TotalPages = (int)Math.Ceiling((double)allItems.Count / pageSize),
                SearchQuery = searchQuery,
                SortOrder = sortOrder
            };

            pagedItems.ForEach(x =>
            {
                viewModel.PickaxeShopViewModels.Add(new PickaxeShopViewModel
                {
                    Id = x.Id,
                    PickaxeId = x.PickaxeId,
                    Pickaxe = x.Pickaxe,
                    PickaxeShopResourceCosts = x.PickaxeShopResourceCosts
                });
            });

            return viewModel;
        }



        public async Task<(bool Succes, String Message)> Buy(int pickaxeShopId)
        {
            var user = await _userManager.GetUserAsync(_httpContextAccessor.HttpContext.User);
            if (user == null)
            {
                return (false, "Użytkownik nie jest zalogowany.");
            }

            user = await _context.Users
                .Include(u => u.PickaxesEq)
                .FirstOrDefaultAsync(u => u.Id == user.Id);

            var pickaxeShop = await _context.PickaxeShops
                .Include(x => x.Pickaxe)
                    .ThenInclude(x => x.PickaxeResourceMultipliers)
                .Include(x => x.PickaxeShopResourceCosts)
                .FirstOrDefaultAsync(x => x.Id == pickaxeShopId);

            if (pickaxeShop == null)
                return (false, "Kilof o takim id nie istnieje.");

            if (user.PickaxesEq.Any(p => p.Id == pickaxeShop.PickaxeId))
                return (false, "Masz już ten kilof.");

            foreach(var cost in pickaxeShop.PickaxeShopResourceCosts)
            {
                switch (cost.ResourceType)
                {
                    case ResourceType.Iron:
                        if (user.Iron < cost.Value)
                            return (false, "Nie masz wystarczająco żelaza.");
                        break;
                    case ResourceType.Gold:
                        if (user.Gold < cost.Value)
                            return (false, "Nie masz wystarczająco złota.");
                        break;
                    case ResourceType.Diamond:
                        if (user.Diamond < cost.Value)
                            return (false, "Nie masz wystarczająco diamentów.");
                        break;

                }
            }

            foreach (var cost in pickaxeShop.PickaxeShopResourceCosts)
            {
                switch (cost.ResourceType)
                {
                    case ResourceType.Iron:
                        user.Iron -= cost.Value;
                        break;
                    case ResourceType.Gold:
                        user.Gold -= cost.Value;
                        break;
                    case ResourceType.Diamond:
                        user.Diamond -= cost.Value;
                        break;

                }
            }

            user.PickaxesEq.Add(pickaxeShop.Pickaxe);


            await _context.SaveChangesAsync();

            return (true, "Zakup powiódł się!");
        }
    }
}
