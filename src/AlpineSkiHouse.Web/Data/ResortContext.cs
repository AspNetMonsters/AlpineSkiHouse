using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AlpineSkiHouse.Models;

namespace AlpineSkiHouse.Data
{
    public class ResortContext : DbContext
    {
        public ResortContext(DbContextOptions<ResortContext> options)
            :base(options)
        {
        }

        public DbSet<Resort> Resorts { get; set; }

        public DbSet<Location> Locations { get; set; }
    }
}
