namespace LaptopWorldStore.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("employee")]
    public partial class employee
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public employee()
        {
            accounts = new HashSet<account>();
            receiptbills = new HashSet<receiptbill>();
        }

        [Key]
        [StringLength(50)]
        public string employee_id { get; set; }

        [Required]
        [StringLength(100)]
        public string employee_name { get; set; }

        [Required]
        [StringLength(15)]
        public string phonenumber { get; set; }

        [Column(TypeName = "date")]
        public DateTime birthday { get; set; }

        [Required]
        [StringLength(200)]
        public string address { get; set; }

        [Required]
        [StringLength(100)]
        public string email { get; set; }

        public bool flag { get; set; }

        [StringLength(20)]
        public string cmnd { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<account> accounts { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<receiptbill> receiptbills { get; set; }
    }
}
