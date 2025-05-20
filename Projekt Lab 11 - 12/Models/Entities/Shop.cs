using System.ComponentModel.DataAnnotations;

namespace Projekt_Lab_11___12.Models.Entities
{
    public class Shop
    {
        [Key]
        public int Id { get; set; }
        public List<PickaxeShop> PickaxeShops { get; set; }

        public Shop()
        {
            PickaxeShops = new List<PickaxeShop>();
        }
    }
}
