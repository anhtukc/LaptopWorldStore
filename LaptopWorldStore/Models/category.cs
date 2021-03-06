namespace LaptopWorldStore.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class category
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public category()
        {
            news = new HashSet<_new>();
            products = new HashSet<product>();
        }

        [Key]
        [StringLength(50)]
        [Display(Name = "Mã mục")]
        public string category_id { get; set; }

        [Required]
        [StringLength(200)]
        [Display(Name = "Tên mục")]
        public string category_name { get; set; }

        [StringLength(4000)]
        [Display(Name = "chú thích")]
        public string description { get; set; }

        public bool flag { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<_new> news { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<product> products { get; set; }
    }
}
