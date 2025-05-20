using System.ComponentModel.DataAnnotations;

namespace Projekt_Lab_11___12.Models.Entities
{
    public class Pickaxe
    {
        [Key]
        public int Id { get; set; }
        public String Name { get; set; }
        public String ImageName { get; set; }
        public List<PickaxeResourceMultiplier> PickaxeResourceMultipliers { get; set; } = new();
        public int RequirmentLevel { get; set; }
        public List<User> Users { get; set; } = new();

    }
}
