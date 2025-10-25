using HotelManagementApp.Models;
using System;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;

namespace HotelManagementApp
{
    public partial class FrmHoaDon : Form
    {
        private Model1 db = new Model1();
        private bool isAdmin;

        public FrmHoaDon(bool admin = false)
        {
            InitializeComponent();
            isAdmin = admin;
            btnSua.Enabled = isAdmin;
            btnXoa.Enabled = isAdmin;
        }

        private void FrmHoaDon_Load(object sender, EventArgs e)
        {
            LoadData();
            btnSua.Enabled = isAdmin;
            btnXoa.Enabled = isAdmin;

            if (this.Tag is string s && !string.IsNullOrWhiteSpace(s))
                txtMaDatPhong.Text = s;
        }



        private void LoadData()
        {
            dgvHoaDon.DataSource = db.HoaDon
                .Select(h => new { h.MaHD, h.MaDatPhong, h.NgayLap, h.TongTien })
                .ToList();

            if (dgvHoaDon.Columns["MaHD"] != null) dgvHoaDon.Columns["MaHD"].HeaderText = "Mã hóa đơn";
            if (dgvHoaDon.Columns["MaDatPhong"] != null) dgvHoaDon.Columns["MaDatPhong"].HeaderText = "Mã đặt phòng";

            txtMaHD.Clear();
            txtMaDatPhong.Clear();
            txtTongTien.Clear();
            dtpNgayLap.Value = DateTime.Now;
        }


        private void dgvHoaDon_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                var row = dgvHoaDon.Rows[e.RowIndex];

                txtMaHD.Text = row.Cells["MaHD"].Value?.ToString() ?? "";
                txtMaDatPhong.Text = row.Cells["MaDatPhong"].Value?.ToString() ?? "";

                if (row.Cells["NgayLap"].Value != null)
                    dtpNgayLap.Value = Convert.ToDateTime(row.Cells["NgayLap"].Value);

                txtTongTien.Text = row.Cells["TongTien"].Value?.ToString() ?? "";
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (!isAdmin)
            {
                MessageBox.Show("Chỉ admin mới được sửa hóa đơn!");
                return;
            }

            if (!int.TryParse(txtMaHD.Text, out int maHD))
            {
                MessageBox.Show("Mã hóa đơn không hợp lệ.");
                return;
            }

            var hd = db.HoaDon.FirstOrDefault(h => h.MaHD == maHD);
            if (hd == null)
            {
                MessageBox.Show("Không tìm thấy hóa đơn.");
                return;
            }

            // MaDatPhong có thể NULL trong DB → cho phép bỏ trống
            int? maDatPhong = null;
            if (!string.IsNullOrWhiteSpace(txtMaDatPhong.Text))
            {
                if (!int.TryParse(txtMaDatPhong.Text, out int tmp))
                {
                    MessageBox.Show("Mã đặt phòng phải là số nguyên.");
                    return;
                }
                maDatPhong = tmp;
            }

            // Hỗ trợ số có dấu . , theo vi-VN hoặc Invariant
            decimal tongTien;
            var vi = CultureInfo.GetCultureInfo("vi-VN");
            if (!decimal.TryParse(txtTongTien.Text, NumberStyles.Number, vi, out tongTien) &&
                !decimal.TryParse(txtTongTien.Text, NumberStyles.Number, CultureInfo.InvariantCulture, out tongTien))
            {
                MessageBox.Show("Tổng tiền không hợp lệ.");
                return;
            }

            hd.MaDatPhong = maDatPhong;
            hd.NgayLap = dtpNgayLap.Value;
            hd.TongTien = tongTien;

            db.SaveChanges();
            LoadData();
            MessageBox.Show("Cập nhật hóa đơn thành công!");
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (!isAdmin)
            {
                MessageBox.Show("Chỉ admin mới được xóa hóa đơn!");
                return;
            }

            if (!int.TryParse(txtMaHD.Text, out int maHD))
            {
                MessageBox.Show("Vui lòng chọn hóa đơn hợp lệ để xóa.");
                return;
            }

            var hd = db.HoaDon.FirstOrDefault(h => h.MaHD == maHD);
            if (hd == null)
            {
                MessageBox.Show("Không tìm thấy hóa đơn.");
                return;
            }

            db.HoaDon.Remove(hd);
            db.SaveChanges();
            LoadData();
            MessageBox.Show("Xóa hóa đơn thành công!");
        }
        

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            string keyword = txtTimKiem.Text.Trim();
            dgvHoaDon.DataSource = db.HoaDon
                .Where(h => h.MaHD.ToString().Contains(keyword)
                         || h.MaDatPhong.ToString().Contains(keyword))
                .Select(h => new
                {
                    h.MaHD,
                    h.MaDatPhong,
                    h.NgayLap,
                    h.TongTien
                }).ToList();
        }
    }
}
