namespace LaptopWorldStore.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("new")]
    public partial class _new
    {
        [Key]
        [StringLength(50)]
        [Display(Name = "Mã bản tin")]
        public string New_id { get; set; }

        [StringLength(50)]
        public string category_id { get; set; }

        [StringLength(300)]
        [Display(Name = "Tiêu đề")]
        public string new_name { get; set; }

        [StringLength(100)]
        [Display(Name = "Tác giả")]
        public string author_name { get; set; }

        [StringLength(100)]
        [Display(Name = "Ảnh")]
        public string picture { get; set; }
        [Display(Name = "Nội dung")]
        public string Content { get; set; }

        [Column(TypeName = "date")]
        [Display(Name = "Ngày tạo")]
        public DateTime? Date_created { get; set; }

        public bool? flag { get; set; }

        public virtual category category { get; set; }
    }
}
