using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BillingService.Models {
    public class sale {
        [Required]
        public Guid billId { get; set; }

        [Key]
        public Guid saleId { get; set; }

        [ForeignKey ("product")]
        public Guid productRefId { get; set; }

        [Required]
        public DateTime saleDate { get; set; }

        [Required]
        public uint saleAmount { get; set; }

        [Required]
        public double saleUnitPrice { get; set; }

        [Required]
        public double saleTotalPrice { get; set; }

        public sale () { }

        public sale (Guid _billId, Guid _productRefId, DateTime _saleDate, uint _saleAmount, double _saleUnitPrice, double _saleTotalPrice) {
            billId = _billId;
            saleId = Guid.NewGuid ();
            productRefId = _productRefId;
            saleDate = _saleDate;
            saleAmount = _saleAmount;
            saleUnitPrice = _saleUnitPrice;
            saleTotalPrice = _saleAmount * _saleUnitPrice;
        }
    }
}
