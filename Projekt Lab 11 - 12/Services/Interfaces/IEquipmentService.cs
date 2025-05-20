using Projekt_Lab_11___12.Models.ViewModels;

namespace Projekt_Lab_11___12.Services.Interfaces
{
    public interface IEquipmentService
    {
        public Task<EquipmentViewModel> EquipmentViewModel();
        public Task<(bool Succes, String Message)> Equip(int pickaxeId);
    }
}
