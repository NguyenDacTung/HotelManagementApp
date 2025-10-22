using HotelManagementApp.Models;
using System;
using System.Linq;
using System.Windows.Forms;

namespace HotelManagementApp
{
    public partial class FrmLoaiPhong : Form
    {
        private Model1 db = new Model1();
        private int? selectedMaLoai = null;

        public FrmLoaiPhong()
        {
            InitializeComponent();
        }

        private void FrmLoaiPhong_Load(object sender, EventArgs e)
        {
            LoadLoaiPhong();
        }

        private void LoadLoaiPhong()
        {
            var list = db.LoaiPhong
                         .Select(lp => new
                         {
                             lp.MaLoaiPhong,
                             lp.TenLoai,
                             lp.GiaPhong,
                             lp.MoTa
                         })
                         .ToList();

            dgvLoaiPhong.DataSource = list;

            if (dgvLoaiPhong.Columns["MaLoaiPhong"] != null) dgvLoaiPhong.Columns["MaLoaiPhong"].HeaderText = "Mã loại";
            if (dgvLoaiPhong.Columns["TenLoai"] != null) dgvLoaiPhong.Columns["TenLoai"].HeaderText = "Tên loại";
            if (dgvLoaiPhong.Columns["GiaPhong"] != null) dgvLoaiPhong.Columns["GiaPhong"].HeaderText = "Giá";
            if (dgvLoaiPhong.Columns["MoTa"] != null) dgvLoaiPhong.Columns["MoTa"].HeaderText = "Mô tả";

            ClearInputs();
        }

        private void ClearInputs()
        {
            txtMaLoaiPhong.Text = "";
            txtTenLoaiPhong.Text = "";
            txtGiaCoBan.Text = "";
            txtMoTa.Clear();
            selectedMaLoai = null;
        }

        private void dgvLoaiPhong_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                var row = dgvLoaiPhong.Rows[e.RowIndex];
                selectedMaLoai = Convert.ToInt32(row.Cells["MaLoaiPhong"].Value);
                txtMaLoaiPhong.Text = row.Cells["MaLoaiPhong"].Value.ToString();
                txtTenLoaiPhong.Text = row.Cells["TenLoai"].Value?.ToString();
                txtGiaCoBan.Text = row.Cells["GiaPhong"].Value?.ToString();
                txtMoTa.Text = row.Cells["MoTa"].Value?.ToString();
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtTenLoaiPhong.Text))
            {
                MessageBox.Show("Vui lòng nhập tên loại phòng.");
                return;
            }

            if (!decimal.TryParse(txtGiaCoBan.Text, out decimal gia))
            {
                MessageBox.Show("Giá không hợp lệ.");
                return;
            }

            var lp = new LoaiPhong
            {
                TenLoai = txtTenLoaiPhong.Text.Trim(),
                GiaPhong = gia,
                MoTa = txtMoTa.Text.Trim()
            };

            db.LoaiPhong.Add(lp);
            db.SaveChanges();
            MessageBox.Show("Thêm loại phòng thành công.");
            LoadLoaiPhong();
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (selectedMaLoai == null)
            {
                MessageBox.Show("Vui lòng chọn loại phòng cần sửa.");
                return;
            }

            var lp = db.LoaiPhong.Find(selectedMaLoai.Value);
            if (lp != null)
            {
                if (!decimal.TryParse(txtGiaCoBan.Text, out decimal gia))
                {
                    MessageBox.Show("Giá không hợp lệ.");
                    return;
                }

                lp.TenLoai = txtTenLoaiPhong.Text.Trim();
                lp.GiaPhong = gia;
                lp.MoTa = txtMoTa.Text.Trim();

                db.SaveChanges();
                MessageBox.Show("Cập nhật thành công.");
                LoadLoaiPhong();
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (selectedMaLoai == null)
            {
                MessageBox.Show("Vui lòng chọn loại phòng cần xóa.");
                return;
            }

            if (MessageBox.Show("Bạn có chắc muốn xóa loại phòng này? (Các phòng thuộc loại này sẽ cần xử lý)", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;

            var lp = db.LoaiPhong.Find(selectedMaLoai.Value);
            if (lp != null)
            {
                try
                {
                    db.LoaiPhong.Remove(lp);
                    db.SaveChanges();
                    MessageBox.Show("Xóa thành công.");
                    LoadLoaiPhong();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi xóa: " + ex.Message);
                }
            }
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            LoadLoaiPhong();
        }
    }
}
