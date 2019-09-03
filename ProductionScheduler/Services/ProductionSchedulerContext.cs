using ProductionScheduler.Models;
using ProductionScheduler.Models.UserLogin;
using System.Data.Entity;

namespace ProductionScheduler.Services {
    internal class ProductionSchedulerContext : DbContext {

        public ProductionSchedulerContext()
            : base("name=DefaultConnection") {

        }

        public DbSet<User> Users { get; set; }
        public DbSet<Job> Jobs { get; set; }
        public DbSet<Part> Parts { get; set; }
        public DbSet<Press> Presses { get; set; }
        public DbSet<Mold> Molds { get; set; }
        public DbSet<ProductFamily> ProductFamilies { get; set; }

    }
}
