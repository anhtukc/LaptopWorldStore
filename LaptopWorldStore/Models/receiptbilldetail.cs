namespace LaptopWorldStore.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("receiptbilldetail")]
    public partial class receiptbilldetail
    {
        [Key]
        public Guid receiptbilldetail_id { get; set; }

        public Guid receiptbill_id { get; set; }

        [Required]
        [StringLength(50)]
        public string product_id { get; set; }

        [Required]
        [StringLength(200)]
        public string product_name { get; set; }

        public decimal price { get; set; }

        public int quantity { get; set; }

        public decimal grand_paid { get; set; }

        public bool flag { get; set; }

        public virtual receiptbill receiptbill { get; set; }
    }
}
