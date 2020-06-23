using Microsoft.EntityFrameworkCore;
using ProductService.Models;

namespace ProductService.Contexts {
    public class ProductDatabaseContext : DbContext {

        protected override void OnConfiguring (DbContextOptionsBuilder options) {
            options.UseNpgsql ("Host=localhost;Database=product_db;Username=postgres");
        }

        protected override void OnModelCreating (ModelBuilder builder) {
            builder.Entity<product> ()
                .HasIndex (u => u.productName)
                .IsUnique ();
        }

        public virtual DbSet<product> product { get; set; }
    }
}
