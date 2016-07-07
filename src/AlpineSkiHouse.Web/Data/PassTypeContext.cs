using AlpineSkiHouse.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlpineSkiHouse.Data
{
    public class PassTypeContext : DbContext
    {
        public PassTypeContext(DbContextOptions<PassTypeContext> options)
            :base(options)
        {
        }

        public DbSet<PassType> PassTypes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PassTypeResort>()
                .HasKey(p => new { p.PassTypeId, p.ResortId });

            base.OnModelCreating(modelBuilder);
        }
    }
}
