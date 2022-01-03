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
        [Display(Name = "Mã hóa đơn nhập")]
        public Guid receiptbill_id { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Nhân viên nhận hàng")]
        public string employee_id { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Nhà cung cấp")]
        public string provider_id { get; set; }
        [Display(Name = "Ngày tạo")]
        public DateTime date_created { get; set; }
        [Display(Name = "Tổng tiền")]
        public decimal total_paid { get; set; }

        public bool flag { get; set; }

        public virtual employee employee { get; set; }

        public virtual provider provider { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<receiptbilldetail> receiptbilldetails { get; set; }
    }
}
