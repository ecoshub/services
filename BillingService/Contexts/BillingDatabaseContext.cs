using BillingService.Models;
using Microsoft.EntityFrameworkCore;

namespace BillingService.Contexts {
    public class BillingDatabaseContext : DbContext {
        public BillingDatabaseContext (DbContextOptions<BillingDatabaseContext> options) : base (options) {

        }

        public virtual DbSet<sale> sale { get; set; }
    }
}
