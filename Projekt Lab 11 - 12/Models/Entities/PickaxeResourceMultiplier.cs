using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Projekt_Lab_11___12.Models.Entities
{
    public class PickaxeResourceMultiplier
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Pickaxe")]
        public int PickaxeId { get; set; }

        public Pickaxe Pickaxe { get; set; }

        [ForeignKey("ResourceType")]
        public int ResourceTypeId { get; set; }
        public ResourceType ResourceType { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Minimalna wartość to 1")]
        public int Value { get; set; }
    }
}
