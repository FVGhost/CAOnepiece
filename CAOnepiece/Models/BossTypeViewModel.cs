using Microsoft.AspNetCore.Mvc.Rendering;

namespace CAOnepiece.Models
{
    public class BossTypeViewModel
    {
        public List<Boss>? Boss { get; set; }
        public SelectList? Type { get; set; }
        public string? BossType { get; set; }
        public string? SearchString { get; set; }
    }


}
