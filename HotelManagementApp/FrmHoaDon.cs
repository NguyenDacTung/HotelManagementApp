using HotelManagementApp.Models;
using System;
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
        }

        private void FrmHoaDon_Load(object sender, EventArgs e)
        {
            LoadData();
            if (!isAdmin)
                btnXoa.Enabled = false;
        }

        private void LoadData()
        {
            dgvHoaDon.DataSource = db.HoaDon
                .Select(h => new
                {
                    h.MaHD,
                    h.MaDatPhong,
                    h.NgayLap,
                    h.TongTien
                }).ToList();
        }

        private void dgvHoaDon_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                txtMaHD.Text = dgvHoaDon.Rows[e.RowIndex].Cells["MaHD"].Value.ToString();
                txtMaDatPhong.Text = dgvHoaDon.Rows[e.RowIndex].Cells["MaDatPhong"].Value.ToString();
                dtpNgayLap.Value = Convert.ToDateTime(dgvHoaDon.Rows[e.RowIndex].Cells["NgayLap"].Value);
                txtTongTien.Text = dgvHoaDon.Rows[e.RowIndex].Cells["TongTien"].Value.ToString();
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtMaHD.Text)) return;
            int maHD = int.Parse(txtMaHD.Text);
            var hd = db.HoaDon.FirstOrDefault(h => h.MaHD == maHD);
            if (hd != null)
            {
                hd.MaDatPhong = int.Parse(txtMaDatPhong.Text);
                hd.NgayLap = dtpNgayLap.Value;
                hd.TongTien = decimal.Parse(txtTongTien.Text);
                db.SaveChanges();
                LoadData();
                MessageBox.Show("Cập nhật hóa đơn thành công!");
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (!isAdmin)
            {
                MessageBox.Show("Chỉ admin mới được xóa hóa đơn!");
                return;
            }

            if (string.IsNullOrEmpty(txtMaHD.Text)) return;
            int maHD = int.Parse(txtMaHD.Text);
            var hd = db.HoaDon.FirstOrDefault(h => h.MaHD == maHD);
            if (hd != null)
            {
                db.HoaDon.Remove(hd);
                db.SaveChanges();
                LoadData();
                MessageBox.Show("Xóa hóa đơn thành công!");
            }
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
