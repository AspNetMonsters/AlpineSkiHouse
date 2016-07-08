using AlpineSkiHouse.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlpineSkiHouse.Data
{
    public class PassContext : DbContext
    {
        public PassContext(DbContextOptions<PassContext> options) 
            :base(options)
        {
        }

        public DbSet<Pass> Passes { get; set; }

        public DbSet<PassActivation> PassActivations { get; set; }

        public DbSet<Scan> Scans { get; set; }
    }
}
