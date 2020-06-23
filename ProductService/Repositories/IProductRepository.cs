using System;
using System.Collections.Generic;
using ProductService.DataTransferObjects;
using ProductService.Models;

namespace ProductService.Repositories {
    public interface IProductRepository {
        void addProduct (AddProductObject newProduct);
        OutProductObject getProduct ();
        OutProductObject updateProduct (UpdateProductObject newProduct);
        OutProductObject deleteProduct ();
        GetBillObject buyProduct (IEnumerable<Guid> productIds);
        // ? control functions
        bool productExistsById (Guid productId);
        bool productExistsByName (string productName);
        // ? for testing/debuging
        IEnumerable<OutProductObject> getAllProducts ();
    }
}
