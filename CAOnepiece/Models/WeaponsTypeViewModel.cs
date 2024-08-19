using Microsoft.AspNetCore.Mvc.Rendering;

namespace CAOnepiece.Models
{
    public class WeaponsTypeViewModel
    {
        public List<Weapon>? weapons { get; set; }
        public SelectList? Type { get; set; }
        public string? WeaponType { get; set; }
        public string? SearchString { get; set; }
    }


}
