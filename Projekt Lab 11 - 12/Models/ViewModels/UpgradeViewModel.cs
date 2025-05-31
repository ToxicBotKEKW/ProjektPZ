using Projekt_Lab_11___12.Models.Entities;

namespace Projekt_Lab_11___12.Models.ViewModels
{
    public class UpgradeViewModel
    {
        public double Iron { get; set; }
        public double Gold { get; set; }
        public double Diamond { get; set; }
        public IronMine IronMine { get; set; }
        public GoldMine GoldMine { get; set; }
        public DiamondMine DiamondMine { get; set; }
        public PointForClick IronMinePointForClick { get; set; }
        public PointForClick GoldMinePointForClick { get; set; }
        public PointForClick DiamondMinePointForClick { get; set; }
        public List<LevelRequirement> IronMineLevelRequirments { get; set; }
        public List<LevelRequirement> GoldMineLevelRequirments { get; set; }
        public List<LevelRequirement> DiamondMineLevelRequirments { get; set; }
        public Dictionary<int, string> PurchaseErrors { get; set; } = new();
    }
}
