namespace HotelManagementApp.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("HoaDon")]
    public partial class HoaDon
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int MaHD { get; set; }

        public int? MaDatPhong { get; set; }

        [Column(TypeName = "date")]
        public DateTime? NgayLap { get; set; }

        public decimal? TongTien { get; set; }

        public virtual DatPhong DatPhong { get; set; }
    }
}
