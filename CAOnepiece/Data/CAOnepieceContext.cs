using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using CAOnepiece.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace CAOnepiece.Data
{
    public class CAOnepieceContext : IdentityDbContext<CAOnepieceUser>
    {
        public CAOnepieceContext (DbContextOptions<CAOnepieceContext> options)
            : base(options)
        {
        }

        public DbSet<CAOnepiece.Models.Fruit> Fruit { get; set; } = default!;
        public DbSet<CAOnepiece.Models.Weapon> Weapons { get; set; } = default!;
        public DbSet<CAOnepiece.Models.Boss> Boss { get; set; } = default!;


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            
            modelBuilder.Entity<Weapon>()
                .HasOne(w => w.Boss)
                .WithMany(b => b.Weapons)
               .HasForeignKey(w => w.BossId);


        }
    }
}
    

