namespace LaptopWorldStore.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("laptop")]
    public partial class laptop
    {
        [StringLength(50)]
        public string product_id { get; set; }

        [Key]
        [Column(Order = 0)]
        [StringLength(200)]
        public string monitor { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(200)]
        public string cpu { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(200)]
        public string gpu { get; set; }

        [Key]
        [Column(Order = 3)]
        [StringLength(500)]
        public string ssd { get; set; }

        [Key]
        [Column(Order = 4)]
        [StringLength(500)]
        public string ram { get; set; }

        [Key]
        [Column(Order = 5)]
        [StringLength(200)]
        public string os { get; set; }

        [Key]
        [Column(Order = 6)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int weight { get; set; }

        [Key]
        [Column(Order = 7)]
        public double battery { get; set; }

        public virtual product product { get; set; }
    }
}
