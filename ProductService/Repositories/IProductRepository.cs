using System;
using System.Collections.Generic;
using ProductService.DataTransferObjects;
using ProductService.Models;

namespace ProductService.Repositories {
    public interface IProductRepository {
        void addProduct (product newProduct);
        product getProduct ();
        product updateProduct (product newProduct);
        product deleteProduct ();
        GetBillObject buyProduct (IEnumerable<Guid> productIds);
        // ? control functions
        bool productExistsById (Guid productId);
        bool productExistsByName (string productName);
        // ? for testing/debuging
        IEnumerable<product> getAllProducts ();
    }
}
