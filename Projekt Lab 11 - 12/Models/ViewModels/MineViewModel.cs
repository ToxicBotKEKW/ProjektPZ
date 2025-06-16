using Projekt_Lab_11___12.Models.Entities;

namespace Projekt_Lab_11___12.Models.ViewModels
{
    public class MineViewModel
    {
        public double Iron { get; set; }
        public double Gold { get; set; }
        public double Diamond { get; set; }
        public double Emerald { get; set; }
        public IronMine IronMine { get; set; }
        public GoldMine GoldMine { get; set; }
        public DiamondMine DiamondMine { get; set; }
    }
}
