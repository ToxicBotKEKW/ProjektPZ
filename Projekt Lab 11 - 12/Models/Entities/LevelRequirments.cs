using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Projekt_Lab_11___12.Models.Entities
{
    public class LevelRequirement
    {
        [Key]
        public int Id { get; set; }
        public int Level { get; set; }

        [ForeignKey("ResourceType")]
        public int ResourceTypeId { get; set; }
        [Range(0.01, double.MaxValue, ErrorMessage = "Minimalne wartość to 0,01")]
        public double Amount { get; set; }

        public ResourceType ResourceType { get; set; }
        public ResourceType MineResourceType { get; set; }
    }

}
