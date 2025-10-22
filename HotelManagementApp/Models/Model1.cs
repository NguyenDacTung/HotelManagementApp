using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace HotelManagementApp.Models
{
    public partial class Model1 : DbContext
    {
        public Model1()
            : base("name=Model11")
        {
        }

        public virtual DbSet<ChiTietDatPhong> ChiTietDatPhong { get; set; }
        public virtual DbSet<DatPhong> DatPhong { get; set; }
        public virtual DbSet<HoaDon> HoaDon { get; set; }
        public virtual DbSet<KhachHang> KhachHang { get; set; }
        public virtual DbSet<LoaiPhong> LoaiPhong { get; set; }
        public virtual DbSet<NhanVien> NhanVien { get; set; }
        public virtual DbSet<Phong> Phong { get; set; }
        public virtual DbSet<TaiKhoan> TaiKhoan { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DatPhong>()
                .HasMany(e => e.ChiTietDatPhong)
                .WithRequired(e => e.DatPhong)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Phong>()
                .HasMany(e => e.ChiTietDatPhong)
                .WithRequired(e => e.Phong)
                .WillCascadeOnDelete(false);
        }
    }
}
