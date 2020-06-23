using Microsoft.EntityFrameworkCore;
using ProductService.Models;

namespace ProductService.Contexts {
    public class ProductDatabaseContext : DbContext {

        public ProductDatabaseContext (DbContextOptions<ProductDatabaseContext> options) : base (options) {

        }

        protected override void OnModelCreating (ModelBuilder builder) {
            builder.Entity<product> ()
                .HasIndex (u => u.productName)
                .IsUnique ();
        }

        public virtual DbSet<product> product { get; set; }
    }
}
