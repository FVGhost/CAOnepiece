using System;
using System.Collections.Generic;

namespace AnimeInfoApp.Models
{
    public class Anime
    {
        public int MalId { get; set; }
        public string Title { get; set; }
        public string Synopsis { get; set; }
        public string ImageUrl { get; set; }
        public DateTime? AiredFrom { get; set; }
        public DateTime? AiredTo { get; set; }
        public string Type { get; set; }
        public int Episodes { get; set; }
        public string Status { get; set; }
        public double Score { get; set; }
        public int Members { get; set; }
        public string Url { get; set; }
    }
}
