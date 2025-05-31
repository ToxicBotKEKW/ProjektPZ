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

        protected Mine() {
        }
    }
}
