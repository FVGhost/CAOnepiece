using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using CAOnepiece.Models;

namespace CAOnepiece.Data
{
    public class CAOnepieceContext : DbContext
    {
        public CAOnepieceContext (DbContextOptions<CAOnepieceContext> options)
            : base(options)
        {
        }

        public DbSet<CAOnepiece.Models.Fruit> Fruit { get; set; } = default!;
    }
}
