using Projekt_Lab_11___12.Models.DTO;
using Projekt_Lab_11___12.Models.Entities;

namespace Projekt_Lab_11___12.Models.ViewModels
{
    public class AdminViewModel
    {
        public double Iron { get; set; }
        public double Gold { get; set; }
        public double Diamond { get; set; }
        public double Emerald { get; set; }
        public List<Pickaxe> PickaxesInSystem { get; set; }
        public List<PickaxeShop> PickaxesInShop { get; set; }
        public NewPickaxeDTO NewPickaxe { get; set; }
    }
}
