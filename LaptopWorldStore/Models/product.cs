namespace LaptopWorldStore.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("product")]
    public partial class product
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public product()
        {
            laptops = new HashSet<laptop>();
            rams = new HashSet<ram>();
            receiptbilldetails = new HashSet<receiptbilldetail>();
            sellbilldetails = new HashSet<sellbilldetail>();
            ssds = new HashSet<ssd>();
        }

        [Key]
        [StringLength(50)]
        [Display(Name = "Mã sản phẩm")]
        public string product_id { get; set; }

        [StringLength(50)]
        [Display(Name = "Mã loại")]
        public string category_id { get; set; }

        [StringLength(50)]
        [Display(Name = "Mã nhà cung cấp")]
        public string provider_id { get; set; }

        [Required]
        [StringLength(200)]
        [Display(Name = "Tên sản phẩm")]
        public string product_name { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "Ảnh")]
        public string picture { get; set; }
        [Display(Name = "Giá bán")]
        public decimal price { get; set; }
        [Display(Name = "Số lượng")]
        public int quantity { get; set; }
        [Display(Name = "Miêu tả")]
        public string decription { get; set; }

        public bool flag { get; set; }

        public virtual category category { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<laptop> laptops { get; set; }

        public virtual provider provider { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ram> rams { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<receiptbilldetail> receiptbilldetails { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<sellbilldetail> sellbilldetails { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ssd> ssds { get; set; }
    }
}
