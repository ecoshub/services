using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using ProductService.Contexts;
using ProductService.Models;

namespace ProductService.Repositories {

    public class ProductRepository : IProductRepository {

        private readonly ProductDatabaseContext _context;

        public ProductRepository (ProductDatabaseContext context) {
            _context = context;
        }

        public void addProduct (product newProduct) {
            newProduct.productRegisterDate = DateTime.Now;
            _context.product.Add (newProduct);
            _context.SaveChanges ();
        }

        public product getProduct (Guid productId) {
            return _context.product.AsNoTracking ().Where (c => c.productId == productId).FirstOrDefault ();
        }

        public product updateProduct (product newProduct) {
            _context.Entry (newProduct).State = EntityState.Modified;
            _context.SaveChanges ();
            return newProduct;
        }

        public product deleteProduct (Guid productId) {
            product selected = getProduct (productId);
            _context.product.Remove (selected);
            _context.SaveChanges ();
            return selected;
        }

        public IEnumerable<product> getAllProducts () {
            return _context.product.OrderBy (c => c.productName).ToList ();
        }

        public bool productExistsById (Guid productId) {
            return _context.product.Any (n => n.productId == productId);
        }

        public bool productExistsByName (string productName) {
            return _context.product.Any (n => n.productName == productName);
        }

        public IEnumerable<sale> buyProduct (IEnumerable<Guid> productIds) {
            throw new NotImplementedException ();
        }
    }
}
