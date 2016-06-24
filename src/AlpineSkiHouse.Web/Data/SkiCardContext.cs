using AlpineSkiHouse.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlpineSkiHouse.Data
{
    public class SkiCardContext : DbContext
    {
        public SkiCardContext(DbContextOptions<SkiCardContext> options) : base(options)
        {

        }

        public DbSet<SkiCard> SkiCards { get; set; }
    }
}
