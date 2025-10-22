using HotelManagementApp.Models;
using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace HotelManagementApp
{
    public partial class FrmDatPhong : Form
    {
        private Model1 db = new Model1();

        public FrmDatPhong()
        {
            InitializeComponent();
        }
        private decimal currentDonGia = 0m;

        private void FrmDatPhong_Load(object sender, EventArgs e)
        {
            LoadComboBoxData();
            LoadData();
            AutoGenerateMaDatPhong();
        }

        private void LoadComboBoxData()
        {
            cboMaKH.DataSource = db.KhachHang.ToList();
            cboMaKH.DisplayMember = "TenKH";
            cboMaKH.ValueMember = "MaKH";

            cboMaNV.DataSource = db.NhanVien.ToList();
            cboMaNV.DisplayMember = "TenNV";
            cboMaNV.ValueMember = "MaNV";

            cboMaPhong.DataSource = db.Phong
                .Where(p => p.TrangThai == "Trống")
                .ToList();
            cboMaPhong.DisplayMember = "TenPhong";
            cboMaPhong.ValueMember = "MaPhong";
        }

        private void LoadData()
        {
            var data = from dp in db.DatPhong
                       join kh in db.KhachHang on dp.MaKH equals kh.MaKH
                       join nv in db.NhanVien on dp.MaNV equals nv.MaNV
                       select new
                       {
                           dp.MaDatPhong,
                           kh.TenKH,
                           nv.TenNV,
                           dp.NgayDat,
                           dp.NgayDen,
                           dp.NgayDi
                       };

            dgvDatPhong.DataSource = data.ToList();
        }

        // ✅ Tự động sinh mã đặt phòng tiếp theo
        private void AutoGenerateMaDatPhong()
        {
            int nextId = db.DatPhong.Any() ? db.DatPhong.Max(d => d.MaDatPhong) + 1 : 1;
            txtMaDatPhong.Text = nextId.ToString();
        }

        // ✅ Khi chọn phòng → tự hiện đơn giá
        private void cboMaPhong_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboMaPhong.SelectedValue == null) { currentDonGia = 0m; txtDonGia.Text = "0"; return; }
            if (!int.TryParse(cboMaPhong.SelectedValue.ToString(), out int maPhong)) return;

            var phong = db.Phong.Find(maPhong);
            if (phong == null) { currentDonGia = 0m; txtDonGia.Text = "0"; return; }

            var loai = db.LoaiPhong.Find(phong.MaLoaiPhong);
            currentDonGia = loai?.GiaPhong ?? 0m;
            txtDonGia.Text = currentDonGia.ToString("N0");

            TinhTongTien();
        }


        // ✅ Khi thay đổi ngày → tính tổng tiền tạm
        private void dtpNgayDen_ValueChanged(object sender, EventArgs e)
        {
            TinhTongTien();
        }

        private void dtpNgayDi_ValueChanged(object sender, EventArgs e)
        {
            TinhTongTien();
        }

        private void TinhTongTien()
        {
            int days = (dtpNgayDi.Value.Date - dtpNgayDen.Value.Date).Days;
            if (days < 1) days = 1;
            decimal tong = currentDonGia * days;
            lblTongTien.Text = $"Tổng tiền: {tong:N0} VND";
        }


        // ✅ Đặt phòng (thay cho nút "Thêm")
        private void btnThem_Click(object sender, EventArgs e)
        {
            try
            {
                var dp = new DatPhong
                {
                    MaKH = (int)cboMaKH.SelectedValue,
                    MaNV = (int)cboMaNV.SelectedValue,
                    NgayDat = dtpNgayDat.Value,
                    NgayDen = dtpNgayDen.Value,
                    NgayDi = dtpNgayDi.Value
                };

                db.DatPhong.Add(dp);
                db.SaveChanges(); // lưu trước để có mã đặt phòng

                int maPhong = (int)cboMaPhong.SelectedValue;
                var chiTiet = new ChiTietDatPhong
                {
                    MaDatPhong = dp.MaDatPhong,
                    MaPhong = maPhong,
                    DonGia = decimal.Parse(txtDonGia.Text.Replace(",", ""))
                };
                db.ChiTietDatPhong.Add(chiTiet);

                // cập nhật trạng thái phòng
                var phong = db.Phong.Find(maPhong);
                if (phong != null)
                    phong.TrangThai = "Đang ở";

                db.SaveChanges();
                MessageBox.Show("Đặt phòng thành công!");
                LoadData();
                AutoGenerateMaDatPhong();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi đặt phòng: " + ex.Message);
            }
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            txtMaDatPhong.Clear();
            txtDonGia.Clear();
            cboMaKH.SelectedIndex = 0;
            cboMaNV.SelectedIndex = 0;
            cboMaPhong.SelectedIndex = 0;
            lblTongTien.Text = "Tổng tiền: 0 VND";
            dtpNgayDat.Value = DateTime.Now;
            dtpNgayDen.Value = DateTime.Now;
            dtpNgayDi.Value = DateTime.Now.AddDays(1);
            AutoGenerateMaDatPhong();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (dgvDatPhong.CurrentRow == null) return;

            int maDP = Convert.ToInt32(dgvDatPhong.CurrentRow.Cells["MaDatPhong"].Value);
            var dp = db.DatPhong.Find(maDP);
            if (dp != null)
            {
                // xóa chi tiết trước
                var ctdp = db.ChiTietDatPhong.FirstOrDefault(c => c.MaDatPhong == maDP);
                if (ctdp != null) db.ChiTietDatPhong.Remove(ctdp);

                db.DatPhong.Remove(dp);
                db.SaveChanges();
                MessageBox.Show("Xóa đặt phòng thành công!");
                LoadData();
                AutoGenerateMaDatPhong();
            }
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            string keyword = txtTimKiem.Text.ToLower();
            var data = db.DatPhong
                .Where(dp => dp.MaDatPhong.ToString().Contains(keyword))
                .Select(dp => new
                {
                    dp.MaDatPhong,
                    dp.NgayDat,
                    dp.NgayDen,
                    dp.NgayDi
                }).ToList();
            dgvDatPhong.DataSource = data;
        }

        private void btnLapHoaDon_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtMaDatPhong.Text))
            {
                MessageBox.Show("Vui lòng chọn phiếu đặt phòng!");
                return;
            }

            FrmHoaDon frmHD = new FrmHoaDon();
            frmHD.Tag = txtMaDatPhong.Text;
            frmHD.ShowDialog();
        }
    }
}
