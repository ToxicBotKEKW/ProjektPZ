using Projekt_Lab_11___12.Models.ViewModels;

namespace Projekt_Lab_11___12.Services.Interfaces
{
    public interface IUpgradeService
    {
        public Task<UpgradeViewModel> UpgradeViewModel();
        public Task<(bool Succes, String Message)> Upgrade(int upgradeMineId);
        public Task ResetAccount();
    }
}
