using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Projekt_Lab_11___12.Models.Entities
{
    public class PickaxeShop
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Pickaxe")]
        public int PickaxeId { get; set; }

        public Pickaxe Pickaxe { get; set; }
        public List<PickaxeShopResourceCost> PickaxeShopResourceCosts { get; set; }

        public PickaxeShop()
        {
            PickaxeShopResourceCosts = new List<PickaxeShopResourceCost>();
        }
    }
}
