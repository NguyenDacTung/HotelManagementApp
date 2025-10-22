using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotelManagementApp.Models
{
    [Table("TaiKhoan")]
    public partial class TaiKhoan
    {
        [Key]
        [StringLength(50)]
        public string TenDangNhap { get; set; }

        [StringLength(50)]
        public string MatKhau { get; set; }

        public int? MaNV { get; set; }

        [StringLength(20)]
        public string Quyen { get; set; }

        [ForeignKey("MaNV")]
        public virtual NhanVien NhanVien { get; set; }
    }
}
