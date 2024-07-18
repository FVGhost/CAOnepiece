using Microsoft.AspNetCore.Mvc.Rendering;

namespace CAOnepiece.Models
{
    public class FruitTypeViewModel
    {
        public List<Fruit>? Fruits { get; set; }
        public SelectList? Type { get; set; }
        public string? FruitType { get; set; }
        public string? SearchString { get; set; }
    }
}
