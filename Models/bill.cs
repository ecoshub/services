namespace ProductService.Models {
    public class bill {
        [Required]
        public Guid billId { get; set; }

        [Key]
        public Guid saleId { get; set; }

        [ForeignKey ("product")]
        public Guid productRefId { get; set; }

        [Required]
        public DateTime billDate { get; set; }

        [Required]
        public uint saleAmount { get; set; }

        [Required]
        public double unitPrice { get; set; }

        [Required]
        public double totalPrice { get; set; }
    }
}
