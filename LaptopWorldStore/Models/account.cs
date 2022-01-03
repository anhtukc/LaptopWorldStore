namespace LaptopWorldStore.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("account")]
    public partial class account
    {
        [StringLength(50)]
        [Display(Name =" Mã nhân viên")]
        public string employee_id { get; set; }

        [Key]
        [StringLength(100)]
        [Display(Name = "Tài Khoản")]
        public string username { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "Mật khẩu")]
        public string password { get; set; }

        public virtual employee employee { get; set; }
    }
}
