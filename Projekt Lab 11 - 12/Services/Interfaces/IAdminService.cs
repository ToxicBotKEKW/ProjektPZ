using Projekt_Lab_11___12.Models.ViewModels;

namespace Projekt_Lab_11___12.Services.Interfaces
{
    public interface IAdminService
    {
        public Task<AdminViewModel> AdminViewModel();
        public Task RemoveFromShop(int pickaxeShopId);
        public Task AddToShop(
            int pickaxeShopId,
            double ironCost,
            double goldCost,
            double diamondCost
            );

        public Task CreatePickaxe(AdminViewModel adminViewModel);
    }
}
