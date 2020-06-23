using ProductService.Contexts;
using ProductService.Models;

namespace ProductService.DatabaseSeed {

    public static class Seeding {

        public static void Seed (ProductDatabaseContext _context) {
            product temp = new product ("Asus N550JK", 20, 7799.90, "Asus N550JK laptop");
            _context.product.Add (temp);
            temp = new product ("IPhone S12", 56, 10999.90, "IPhone S12 Smartphone");
            _context.product.Add (temp);
            temp = new product ("Logitech K360", 48, 299.90, "Logitech wireless keyboard");
            _context.product.Add (temp);
            temp = new product ("Logitech A67", 20, 7799.90, "Logitech 3+1 Sound System");
            _context.product.Add (temp);
            temp = new product ("Samsung A50", 85, 7799.90, "Samsung A50 Smartphone");
            _context.product.Add (temp);
        }

    }
}
