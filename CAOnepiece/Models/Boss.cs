using System.ComponentModel.DataAnnotations;

namespace CAOnepiece.Models
{
    public class Boss
    {
        [Key]
        public int Id { get; set; }
        public string? BossName { get; set; }
        public string? Description { get; set; }

        public ICollection<Weapon>? Weapons { get; set; }
    }
}
