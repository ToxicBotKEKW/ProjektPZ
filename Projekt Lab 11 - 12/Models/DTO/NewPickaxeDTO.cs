using Projekt_Lab_11___12.Models.Entities;
using System.ComponentModel.DataAnnotations;

namespace Projekt_Lab_11___12.Models.DTO
{
    public class NewPickaxeDTO
    {
        public String Name { get; set; }
        public IFormFile ImageFile { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Minimalna wartość to 1")]
        public int IronMultipler { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Minimalna wartość to 1")]
        public int GoldMultipler { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Minimalna wartość to 1")]
        public int DiamondMultipler { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Minimalna wartość to 1")]
        public int RequirmentLevel { get; set; }
    }
}
