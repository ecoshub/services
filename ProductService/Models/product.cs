using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ProductService.Models {
    public class product {
        [Key]
        public Guid productId { get; set; }

        [Required]
        public string productName { get; set; }

        [DefaultValue ("0")]
        public uint productStock { get; set; }

        [Required]
        public double productPrice { get; set; }

        [DefaultValue ("initial product description")]
        public string productDescription { get; set; }

        [Required]
        public DateTime productRegisterDate { get; set; }

        public product () { }

        public product (
            string product_name,
            uint product_stock,
            double product_price,
            string product_description) {
            productId = Guid.NewGuid ();
            productName = product_name;
            productStock = product_stock;
            productPrice = product_price;
            productDescription = product_description;
            productRegisterDate = DateTime.Now;
        }
    }

}
