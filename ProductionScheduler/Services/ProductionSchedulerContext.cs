using ProductionScheduler.Models;
using ProductionScheduler.Models.UserLogin;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductionScheduler.Services
{
    class ProductionSchedulerContext : DbContext
    {

        public ProductionSchedulerContext()
            :base("name=DefaultConnection")
        {

        }

        public DbSet<User> Users { get; set; }
        public DbSet<Job> Jobs { get; set; }
        public DbSet<Part> Parts { get; set; }
        public DbSet<Press> Presses { get; set; }
        public DbSet<Mold> Molds { get; set; }
        public DbSet<ProductFamily> ProductFamilies { get; set; }

    }
}
