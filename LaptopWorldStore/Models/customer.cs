namespace LaptopWorldStore.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("customer")]
    public partial class customer
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public customer()
        {
            sellbills = new HashSet<sellbill>();
        }
        [Key]
        [Display(Name = "Mã nhân viên")]
        public Guid customer_id { get; set; }

        [StringLength(15)]
        [Display(Name = "Số điện thoại")]
        public string phonenumber { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "Tên nhân viên")]
        public string customer_name { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "Địa chỉ")]
        public string address { get; set; }

        public bool flag { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<sellbill> sellbills { get; set; }
    }
}
