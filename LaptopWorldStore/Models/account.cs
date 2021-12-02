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
        public string employee_id { get; set; }

        [Key]
        [StringLength(100)]
        public string username { get; set; }

        [Required]
        [StringLength(100)]
        public string password { get; set; }

        public virtual employee employee { get; set; }
    }
}
