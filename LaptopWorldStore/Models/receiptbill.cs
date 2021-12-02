namespace LaptopWorldStore.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("receiptbill")]
    public partial class receiptbill
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public receiptbill()
        {
            receiptbilldetails = new HashSet<receiptbilldetail>();
        }

        [Key]
        public Guid receiptbill_id { get; set; }

        [Required]
        [StringLength(50)]
        public string employee_id { get; set; }

        [Required]
        [StringLength(50)]
        public string provider_id { get; set; }

        public decimal total_paid { get; set; }

        public bool flag { get; set; }

        public virtual employee employee { get; set; }

        public virtual provider provider { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<receiptbilldetail> receiptbilldetails { get; set; }
    }
}
