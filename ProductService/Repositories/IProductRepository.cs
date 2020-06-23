using System;
using System.Collections.Generic;
using ProductService.Models;

namespace ProductService.Repositories {
    public interface IProductRepository {
        void addProduct (product newProduct);
        product getProduct (Guid productId);
        product updateProduct (product newProduct);
        product deleteProduct (Guid productId);
        IEnumerable<sale> buyProduct (IEnumerable<Guid> productIds);
        // ? control functions
        bool productExistsById (Guid productId);
        bool productExistsByName (string productName);
        // ? for testing/debuging
        IEnumerable<product> getAllProducts ();
    }
}
