using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Projekt_Lab_11___12.Models.Entities
{
    public abstract class Mine
    {
        [Key]
        public int Id { get; set; }
        public int Level { get; set; } = 1;
        public string Name { get; set; }
        public ResourceType MineResourceType { get; set; }

        [Range(0.01, double.MaxValue, ErrorMessage = "Minimalne wartość to 0,01")]
        public double PermAdditionalValue { get; set; } = 0;

        public BonusClicks BonusClicks { get; set; } = new();

        protected Mine() {
        }
    }
}
