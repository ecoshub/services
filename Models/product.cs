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

        [DefaultValue ("initial product desciription")]
        public string productDesciption { get; set; }

        [Required]
        public DateTime productRegisterDate { get; set; }
    }
}
