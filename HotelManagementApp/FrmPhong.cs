using HotelManagementApp.Models;
using System;
using System.Linq;
using System.Windows.Forms;

namespace HotelManagementApp
{
    public partial class FrmPhong : Form
    {
        private Model1 db = new Model1();
        private int? selectedMaPhong = null;
        private string quyen; // nhận quyền từ FrmMain để kiểm soát hành động

        public FrmPhong(string quyen)
        {
            InitializeComponent();
            this.quyen = quyen ?? "nhanvien"; 
        }

        private void FrmPhong_Load(object sender, EventArgs e)
        {
            string role = (quyen ?? "").Trim().ToLower();

            // Chỉ admin mới được thêm, xóa
            btnThem.Enabled = (role == "admin");
            btnXoa.Enabled = (role == "admin");
            btnMoLoaiPhong.Enabled = (role == "admin");

            // Nhân viên được sửa, nhưng chỉ được sửa tình trạng
            btnSua.Enabled = true;

            LoadLoaiPhongToCombo();
            LoadPhongToGrid();

            if (role == "nhanvien")
            {
                // ✅ Khóa các ô: Mã, Tên, Loại phòng
                txtMaPhong.Enabled = false;
                txtTenPhong.Enabled = false;
                cboLoaiPhong.Enabled = false;
                // Chỉ cho sửa tình trạng
                cboTinhTrang.Enabled = true;
            }
            else if (role == "admin")
            {
                // Admin có toàn quyền
                txtMaPhong.Enabled = true;
                txtTenPhong.Enabled = true;
                cboLoaiPhong.Enabled = true;
                cboTinhTrang.Enabled = true;
            }
        }


        private void LoadLoaiPhongToCombo()
        {
            var ds = db.LoaiPhong
                       .Select(lp => new { lp.MaLoaiPhong, lp.TenLoai })
                       .ToList();

            cboLoaiPhong.DataSource = ds;
            cboLoaiPhong.DisplayMember = "TenLoai";
            cboLoaiPhong.ValueMember = "MaLoaiPhong";
        }

        private void LoadPhongToGrid()
        {
            var list = db.Phong
                         .Select(p => new
                         {
                             p.MaPhong,
                             p.TenPhong,
                             MaLoai = p.MaLoaiPhong,
                             Loai = p.LoaiPhong != null ? p.LoaiPhong.TenLoai : "",
                             p.TrangThai
                         })
                         .ToList();

            dgvPhong.DataSource = list;
            if (dgvPhong.Columns["MaPhong"] != null) dgvPhong.Columns["MaPhong"].HeaderText = "Mã phòng";
            if (dgvPhong.Columns["TenPhong"] != null) dgvPhong.Columns["TenPhong"].HeaderText = "Tên phòng";
            if (dgvPhong.Columns["Loai"] != null) dgvPhong.Columns["Loai"].HeaderText = "Loại phòng";
            if (dgvPhong.Columns["TrangThai"] != null) dgvPhong.Columns["TrangThai"].HeaderText = "Tình trạng";

            ClearInputs();
        }

        private void ClearInputs()
        {
            txtMaPhong.Text = "";
            txtTenPhong.Text = "";
            if (cboLoaiPhong.Items.Count > 0) cboLoaiPhong.SelectedIndex = 0;
            cboTinhTrang.SelectedIndex = 0;
            selectedMaPhong = null;

            // ✅ bật nhập và gợi ý số kế tiếp
            txtMaPhong.Enabled = true;
            txtMaPhong.Text = GetNextMaPhong().ToString();
        }

        private int GetNextMaPhong()
        {
            var used = db.Phong.Select(p => p.MaPhong).ToList();
            if (used.Count == 0) return 1;

            used.Sort();
            int candidate = 1;
            foreach (var id in used)
            {
                if (id == candidate) candidate++;
                else if (id > candidate) break;   // gặp lỗ thì dừng
            }
            return candidate;
        }

        private void dgvPhong_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                var row = dgvPhong.Rows[e.RowIndex];
                selectedMaPhong = Convert.ToInt32(row.Cells["MaPhong"].Value);
                txtMaPhong.Text = row.Cells["MaPhong"].Value.ToString();
                txtTenPhong.Text = row.Cells["TenPhong"].Value?.ToString();

                if (row.Cells["MaLoai"] != null && row.Cells["MaLoai"].Value != null)
                {
                    int maLoai = Convert.ToInt32(row.Cells["MaLoai"].Value);
                    cboLoaiPhong.SelectedValue = maLoai;
                }

                cboTinhTrang.Text = row.Cells["TrangThai"].Value?.ToString();
            }
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            LoadLoaiPhongToCombo();
            LoadPhongToGrid();
        }

        private void btnMoLoaiPhong_Click(object sender, EventArgs e)
        {
            if (!quyen.Equals("admin", StringComparison.OrdinalIgnoreCase))
            {
                MessageBox.Show("Chỉ admin mới có quyền quản lý loại phòng.", "Không có quyền", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            FrmLoaiPhong frm = new FrmLoaiPhong();
            frm.ShowDialog();

            LoadLoaiPhongToCombo();
            LoadPhongToGrid();
        }

        // Thêm phòng
        private void btnThem_Click(object sender, EventArgs e)
        {
            if (!quyen.Equals("admin", StringComparison.OrdinalIgnoreCase))
            {
                MessageBox.Show("Chỉ admin mới có thể thêm phòng.", "Không có quyền",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string ten = txtTenPhong.Text.Trim();
            if (string.IsNullOrEmpty(ten))
            {
                MessageBox.Show("Vui lòng nhập tên phòng.", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (cboLoaiPhong.SelectedValue == null)
            {
                MessageBox.Show("Vui lòng chọn loại phòng.", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int maLoai = Convert.ToInt32(cboLoaiPhong.SelectedValue);
            string trangThai = cboTinhTrang.Text;

            // ✅ xác định mã phòng
            int maPhong;
            bool userTypedId = !string.IsNullOrWhiteSpace(txtMaPhong.Text);

            if (userTypedId)
            {
                if (!int.TryParse(txtMaPhong.Text, out maPhong))
                {
                    MessageBox.Show("Vui lòng nhập Mã phòng là số nguyên!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtMaPhong.Focus(); return;
                }
                if (db.Phong.Any(x => x.MaPhong == maPhong))
                {
                    MessageBox.Show("Mã phòng đã tồn tại, vui lòng nhập mã khác!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtMaPhong.Focus(); return;
                }
            }
            else
            {
                maPhong = GetNextMaPhong(); // gợi ý nếu để trống
            }

            var p = new Phong
            {
                MaPhong = maPhong,            // ⬅️ gán rõ ràng
                TenPhong = ten,
                MaLoaiPhong = maLoai,
                TrangThai = trangThai
            };

            try
            {
                db.Phong.Add(p);
                db.SaveChanges();
                MessageBox.Show("Thêm phòng thành công.", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadPhongToGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi thêm phòng: " + ex.GetBaseException().Message,
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        // Sửa phòng
        private void btnSua_Click(object sender, EventArgs e)
        {
            if (selectedMaPhong == null)
            {
                MessageBox.Show("Vui lòng chọn phòng để sửa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var p = db.Phong.Find(selectedMaPhong.Value);
            if (p == null)
            {
                MessageBox.Show("Không tìm thấy phòng.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            p.TenPhong = txtTenPhong.Text.Trim();
            p.MaLoaiPhong = Convert.ToInt32(cboLoaiPhong.SelectedValue);
            p.TrangThai = cboTinhTrang.Text;

            db.SaveChanges();
            MessageBox.Show("Cập nhật phòng thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            LoadPhongToGrid();
        }

        // Xóa phòng
        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (!quyen.Equals("admin", StringComparison.OrdinalIgnoreCase))
            {
                MessageBox.Show("Chỉ admin mới có thể xóa phòng.", "Không có quyền", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (selectedMaPhong == null)
            {
                MessageBox.Show("Vui lòng chọn phòng để xóa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (MessageBox.Show("Bạn có chắc muốn xóa phòng này?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;

            var p = db.Phong.Find(selectedMaPhong.Value);
            if (p == null)
            {
                MessageBox.Show("Không tìm thấy phòng.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                db.Phong.Remove(p);
                db.SaveChanges();
                MessageBox.Show("Xóa phòng thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadPhongToGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi xóa: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
