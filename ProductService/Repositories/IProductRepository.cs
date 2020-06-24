using System;
using System.Collections.Generic;
using ProductService.Models;
using ProductService.Tools;

namespace ProductService.Repositories {
    public interface IProductRepository {
        void addProduct (product newProduct);
        product getProduct (Guid productId);
        product updateProduct (product newProduct);
        product deleteProduct (Guid productId);
        (List<sale>, CustomError) buyProduct (IEnumerable<Guid> productIds);
        List<sale> getBill (Guid bill_id);
        // ? control functions
        bool productExistsById (Guid productId);
        bool productExistsByName (string productName);
        // ? for testing/debuging
        IEnumerable<product> getAllProducts ();
    }
}
