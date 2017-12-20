using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace K9CleanSweep.Models
{
    public class CleanSweepContext : DbContext
    {
        public CleanSweepContext(DbContextOptions<CleanSweepContext> options) : base(options)
        { }

        public DbSet<Client> clients { get; set; }
        public DbSet<Employee> employees { get; set; }
        public DbSet<Review> reviews { get; set; }
    }
}
