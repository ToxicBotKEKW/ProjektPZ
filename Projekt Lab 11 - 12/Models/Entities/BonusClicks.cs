using System.ComponentModel.DataAnnotations;

namespace Projekt_Lab_11___12.Models.Entities
{
    public class BonusClicks
    {
        [Key]
        public int Id { get; set; }
        public int MaxClicks { get; set; } = 100;
        public int CurrentClicks { get; set; } = 100;
    }
}
