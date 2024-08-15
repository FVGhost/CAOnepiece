namespace CAOnepiece.Models
{
    public class Weapon
    {
        public int Id { get; set; }
        public string? WeaponName { get; set; }
        public string? Description { get; set; }

        public int BossId { get; set; }

        public Boss? Boss { get; set; }
    }
}
