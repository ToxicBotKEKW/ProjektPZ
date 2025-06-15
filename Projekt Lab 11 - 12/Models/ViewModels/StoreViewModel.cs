using Projekt_Lab_11___12.Models.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Projekt_Lab_11___12.Models.ViewModels
{
    public class StoreViewModel
    {
        public double Iron { get; set; }
        public double Gold { get; set; }
        public double Diamond { get; set; }
        public double Emerald { get; set; }
        public List<PickaxeShopViewModel> PickaxeShopViewModels { get; set; } = new List<PickaxeShopViewModel>();
        public Dictionary<int, string> PurchaseErrors { get; set; } = new();
        public string SearchQuery { get; set; }
        public string SortOrder { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public IronMine IronMine { get; set; }
        public GoldMine GoldMine { get; set; }
        public DiamondMine DiamondMine { get; set; }
    }
}
