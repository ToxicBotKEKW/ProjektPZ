using Projekt_Lab_11___12.Models.Entities;

namespace Projekt_Lab_11___12.Models.ViewModels
{
    public class EquipmentViewModel
    {
        public double Iron { get; set; }
        public double Gold { get; set; }
        public double Diamond { get; set; }
        public int? UsedPickaxeId { get; set; }
        public List<Pickaxe> PickaxesEq { get; set; }
    }
}
