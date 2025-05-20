using Projekt_Lab_11___12.Models.Entities;

namespace Projekt_Lab_11___12.Models.ViewModels
{
    public class PickaxeShopViewModel
    {
        public int Id { get; set; }
        public int PickaxeId { get; set; }
        public Pickaxe Pickaxe { get; set; }
        public List<PickaxeShopResourceCost> PickaxeShopResourceCosts { get; set; }
    }
}
