namespace LaptopWorldStore.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("sellbill")]
    public partial class sellbill
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public sellbill()
        {
            sellbilldetails = new HashSet<sellbilldetail>();
            sellbilldetails1 = new HashSet<sellbilldetail>();
        }

        [Key]
        [Display(Name = "Mã hóa đơn bán")]
        public Guid sellbill_id { get; set; }

        [Column(TypeName = "date")]
        [Display(Name = "Ngày tạo")]
        public DateTime? billDate { get; set; }

        [StringLength(50)]
        [Display(Name = "Phương thức vận chuyển")]
        public string shippingtype { get; set; }

        [Required]
        [Display(Name = "Khách hàng")]
        public Guid customer_id { get; set; }

        [Display(Name = "Tổng tiền")]
        public decimal total_paid { get; set; }

        public bool flag { get; set; }

        public virtual customer customer { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<sellbilldetail> sellbilldetails { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<sellbilldetail> sellbilldetails1 { get; set; }
    }
}
