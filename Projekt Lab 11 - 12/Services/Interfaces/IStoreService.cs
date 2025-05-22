using Projekt_Lab_11___12.Models.ViewModels;

namespace Projekt_Lab_11___12.Services.Interfaces
{
    public interface IStoreService
    {
        public Task<StoreViewModel> StoreViewModel(int page = 1, int pageSize = 3, string searchQuery = "", string sortOrder = "");
        public Task<(bool Succes, String Message)> Buy(int pickaxeShopId);
    }
}
