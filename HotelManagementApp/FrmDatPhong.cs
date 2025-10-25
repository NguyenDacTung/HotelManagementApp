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
                       join p in db.Phong on dp.MaPhong equals p.MaPhong
                       select new
                       {
                           dp.MaDatPhong,
                           kh.TenKH,
                           nv.TenNV,
                           TenPhong = p.TenPhong,
                           dp.NgayDat,
                           dp.NgayDen,
                           dp.NgayDi
                       };

            dgvDatPhong.DataSource = data.ToList();
        }

        private int GetNextMaDatPhong()
        {
            var used = db.DatPhong.Select(d => d.MaDatPhong).ToList();
            if (used.Count == 0) return 1;

            used.Sort();
            int candidate = 1;
            foreach (var id in used)
            {
                if (id == candidate) candidate++;
                else if (id > candidate) break; 
            }
            return candidate;
        }

        // ✅ Tự động sinh mã đặt phòng tiếp theo
        private void AutoGenerateMaDatPhong()
        {
            txtMaDatPhong.Text = GetNextMaDatPhong().ToString();
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

        private void btnThem_Click(object sender, EventArgs e)
        {
            try
            {
                if (cboMaKH.SelectedValue == null || cboMaNV.SelectedValue == null || cboMaPhong.SelectedValue == null)
                {
                    MessageBox.Show("Vui lòng chọn đầy đủ khách hàng, nhân viên và phòng!");
                    return;
                }

                int maPhong = Convert.ToInt32(cboMaPhong.SelectedValue);

                // ✅ Kiểm tra lại trạng thái phòng mới nhất trong DB
                var phongNow = db.Phong.AsNoTracking().FirstOrDefault(p => p.MaPhong == maPhong);
                if (phongNow == null)
                {
                    MessageBox.Show("Không tìm thấy phòng đã chọn!");
                    LoadComboBoxData();
                    return;
                }
                if (!string.Equals(phongNow.TrangThai, "Trống", StringComparison.OrdinalIgnoreCase))
                {
                    MessageBox.Show("Phòng này hiện không còn trống. Vui lòng chọn phòng khác.");
                    LoadComboBoxData(); // nạp lại danh sách phòng trống
                    return;
                }

                // --- Thêm phiếu đặt phòng ---
                var dp = new DatPhong
                {
                    MaKH = (int)cboMaKH.SelectedValue,
                    MaNV = (int)cboMaNV.SelectedValue,
                    MaPhong = maPhong,
                    NgayDat = dtpNgayDat.Value,
                    NgayDen = dtpNgayDen.Value,
                    NgayDi = dtpNgayDi.Value
                };
                db.DatPhong.Add(dp);
                db.SaveChanges(); // lưu để có MaDatPhong

                // --- Ghi chi tiết + đổi trạng thái phòng ---
                var loai = db.LoaiPhong.Find(phongNow.MaLoaiPhong);
                var donGia = loai?.GiaPhong ?? 0m;

                db.ChiTietDatPhong.Add(new ChiTietDatPhong
                {
                    MaDatPhong = dp.MaDatPhong,
                    MaPhong = maPhong,
                    DonGia = donGia
                });

                var phong = db.Phong.Find(maPhong);
                if (phong != null) phong.TrangThai = "Đang ở";

                // --- Tạo hóa đơn (mã HD tự sinh theo code của bạn) ---
                int nextMaHD = db.HoaDon.Any() ? db.HoaDon.Max(h => h.MaHD) + 1 : 1;
                int soNgay = Math.Max(1, (dtpNgayDi.Value.Date - dtpNgayDen.Value.Date).Days);
                decimal tongTien = donGia * soNgay;

                db.HoaDon.Add(new HoaDon
                {
                    MaHD = nextMaHD,
                    MaDatPhong = dp.MaDatPhong,
                    NgayLap = DateTime.Now,
                    TongTien = tongTien
                });

                db.SaveChanges();

                MessageBox.Show("Đặt phòng và tạo hóa đơn thành công!");

                // ✅ NẠP LẠI COMBO PHÒNG TRỐNG để phòng vừa đặt biến mất khỏi danh sách
                LoadComboBoxData();
                LoadData();
                AutoGenerateMaDatPhong();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi đặt phòng: " + ex.GetBaseException().Message);
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

            if (dp == null)
            {
                MessageBox.Show("Không tìm thấy phiếu đặt phòng để trả!");
                return;
            }

            if (MessageBox.Show("Xác nhận trả phòng này? (Hóa đơn vẫn được giữ lại)",
                                "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;

            try
            {
                var hoaDon = db.HoaDon.FirstOrDefault(h => h.MaDatPhong == maDP);
                if (hoaDon != null)
                {
                    db.Entry(hoaDon).Reference(h => h.DatPhong).CurrentValue = null;
                }

                var chiTiet = db.ChiTietDatPhong.Where(c => c.MaDatPhong == maDP).ToList();
                foreach (var ct in chiTiet)
                {
                    var phong = db.Phong.Find(ct.MaPhong);
                    if (phong != null)
                        phong.TrangThai = "Trống";
                }

                if (chiTiet.Any())
                    db.ChiTietDatPhong.RemoveRange(chiTiet);

                db.DatPhong.Remove(dp);
                db.SaveChanges();

                MessageBox.Show("Trả phòng thành công! Hóa đơn vẫn giữ nguyên mã đặt phòng.");
                LoadData();
                AutoGenerateMaDatPhong();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi trả phòng: " + ex.GetBaseException().Message);
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
