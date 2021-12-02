namespace LaptopWorldStore.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ram")]
    public partial class ram
    {
        [StringLength(50)]
        public string product_id { get; set; }

        [Key]
        [Column(Order = 0)]
        [StringLength(50)]
        public string form_factor { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int speed { get; set; }

        [Key]
        [Column(Order = 2)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int size { get; set; }

        [Key]
        [Column(Order = 3)]
        [StringLength(50)]
        public string memory_technology { get; set; }

        public virtual product product { get; set; }
    }
}