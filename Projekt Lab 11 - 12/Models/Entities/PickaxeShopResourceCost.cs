using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Projekt_Lab_11___12.Models.Entities
{
    public class PickaxeShopResourceCost
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("PickaxeShop")]
        public int PickaxeShopId { get; set; }

        public PickaxeShop PickaxeShop { get; set; }

        [ForeignKey("ResourceType")]
        public int ResourceTypeId { get; set; }
        public ResourceType ResourceType { get; set; }

        [Range(0.01, double.MaxValue, ErrorMessage = "Minimalne wartość to 0,01")]
        public double Value { get; set; }
    }
}
