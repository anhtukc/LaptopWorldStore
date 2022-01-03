namespace LaptopWorldStore.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ssd")]
    public partial class ssd
    {
        [StringLength(50)]
        [Display(Name = "Mã sản phẩm")]
        public string product_id { get; set; }

        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Display(Name = "Tốc độ ghi")]
        public int write_speed { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Display(Name = "Tốc độ đọc")]
        public int read_speed { get; set; }

        [Key]
        [Column(Order = 2)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Display(Name = "Dung lượng")]
        public int size { get; set; }

        [Key]
        [Column(Order = 3)]
        [StringLength(100)]
        [Display(Name = "Phương thức kết nối")]
        public string connector { get; set; }

        public virtual product product { get; set; }
    }
}
