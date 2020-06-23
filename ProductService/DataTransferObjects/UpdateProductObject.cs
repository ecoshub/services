namespace ProductService.DataTransferObjects {
    public class UpdateProductObject {
        public string productName { get; set; }
        public uint productStock { get; set; }
        public double productPrice { get; set; }
        public string productDescription { get; set; }
    }
}
