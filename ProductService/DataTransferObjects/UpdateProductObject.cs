using System;

namespace ProductService.DataTransferObjects {
    public class UpdateProductObject {
        public Guid productId { get; set; }
        public string productName { get; set; }
        public uint productStock { get; set; }
        public double productPrice { get; set; }
        public string productDescription { get; set; }
    }
}
