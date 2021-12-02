namespace LaptopWorldStore.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("sellbilldetail")]
    public partial class sellbilldetail
    {
        [Key]
        public Guid sellbilldetail_id { get; set; }

        public Guid sellbill_id { get; set; }

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

        public virtual product product { get; set; }

        public virtual sellbill sellbill { get; set; }

        public virtual sellbill sellbill1 { get; set; }
    }
}
