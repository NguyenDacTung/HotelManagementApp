namespace HotelManagementApp.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ChiTietDatPhong")]
    public partial class ChiTietDatPhong
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int MaDatPhong { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int MaPhong { get; set; }

        public decimal? DonGia { get; set; }

        public virtual DatPhong DatPhong { get; set; }

        public virtual Phong Phong { get; set; }
    }
}
