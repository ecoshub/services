using Microsoft.EntityFrameworkCore;
using ProductService.Models;

namespace ProductService.Contexts {
    public class ProductDatabaseContext : DbContext {

        protected override void OnConfiguring (DbContextOptionsBuilder options) {
            options.UseNpgsql ("Host=localhost;Database=product_db;Username=postgres");
        }

        public virtual DbSet<product> product { get; set; }
    }
}
