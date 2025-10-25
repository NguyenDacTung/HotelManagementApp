using HotelManagementApp.Models;
using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace HotelManagementApp
{
    public partial class FrmKhachHang : Form
    {
        private Model1 db = new Model1();

        public FrmKhachHang()
        {
            InitializeComponent();
        }

        // Khi load form
        private void FrmKhachHang_Load(object sender, EventArgs e)
        {
            // Nạp giới tính
            cboGioiTinh.Items.Clear();
            cboGioiTinh.Items.Add("Nam");
            cboGioiTinh.Items.Add("Nữ");
            cboGioiTinh.SelectedIndex = 0;

            LoadData();
            ClearFields();
        }

        // Nạp dữ liệu khách hàng
        private void LoadData()
        {
            var list = db.KhachHang
                         .Select(kh => new
                         {
                             kh.MaKH,
                             kh.TenKH,
                             kh.GioiTinh,
                             kh.SDT,
                             kh.CMND
                         })
                         .ToList();

            dgvKhachHang.DataSource = list;
        }

        // Làm trống các ô nhập
        private void ClearFields()
        {
            txtMaKH.Clear();
            txtTenKH.Clear();
            txtSDT.Clear();
            txtCMND.Clear();
            cboGioiTinh.SelectedIndex = 0;
            txtTimKiemMaKH.Clear();

            txtMaKH.Enabled = true;
            txtMaKH.Text = GetNextMaKh().ToString(); // ⬅️ gợi ý mã kế tiếp (có thể sửa tay)
        }

        private int GetNextMaKh()
        {
            var used = db.KhachHang.Select(k => k.MaKH).ToList();
            if (used.Count == 0) return 1;

            used.Sort();
            int candidate = 1;
            foreach (var id in used)
            {
                if (id == candidate) candidate++;
                else if (id > candidate) break; // gặp lỗ thì dừng và dùng candidate
            }
            return candidate;
        }


        // Thêm khách hàng mới
        private void btnThem_Click(object sender, EventArgs e)
        {
            try
            {
                // Xác định mã KH: dùng nhập tay nếu có, ngược lại dùng gợi ý
                int maKH;
                if (string.IsNullOrWhiteSpace(txtMaKH.Text))
                {
                    maKH = GetNextMaKh();
                }
                else if (!int.TryParse(txtMaKH.Text, out maKH))
                {
                    MessageBox.Show("Vui lòng nhập Mã KH là số nguyên!");
                    txtMaKH.Focus();
                    return;
                }
                else if (db.KhachHang.Any(x => x.MaKH == maKH))
                {
                    MessageBox.Show("Mã KH đã tồn tại, vui lòng nhập mã khác!");
                    txtMaKH.Focus();
                    return;
                }

                if (string.IsNullOrWhiteSpace(txtTenKH.Text))
                {
                    MessageBox.Show("Vui lòng nhập tên khách hàng!");
                    return;
                }

                var kh = new KhachHang
                {
                    MaKH = maKH, // ⬅️ GÁN RÕ RÀNG
                    TenKH = txtTenKH.Text.Trim(),
                    GioiTinh = cboGioiTinh.Text,
                    SDT = txtSDT.Text.Trim(),
                    CMND = txtCMND.Text.Trim()
                };

                db.KhachHang.Add(kh);
                db.SaveChanges();

                MessageBox.Show("Thêm khách hàng thành công!");
                LoadData();
                ClearFields(); // sẽ gợi ý mã mới dựa trên dữ liệu hiện tại
            }
            catch (Exception ex)
            {
                // Phòng trường hợp hiếm khi race condition trùng mã
                MessageBox.Show("Lỗi khi thêm khách hàng: " + ex.Message);
            }
        }


        // Sửa khách hàng
        private void btnSua_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtMaKH.Text))
                {
                    MessageBox.Show("Vui lòng chọn khách hàng cần sửa!");
                    return;
                }

                int maKH = int.Parse(txtMaKH.Text);
                var kh = db.KhachHang.FirstOrDefault(x => x.MaKH == maKH);

                if (kh != null)
                {
                    kh.TenKH = txtTenKH.Text.Trim();
                    kh.GioiTinh = cboGioiTinh.Text;
                    kh.SDT = txtSDT.Text.Trim();
                    kh.CMND = txtCMND.Text.Trim();

                    db.SaveChanges();
                    MessageBox.Show("Cập nhật thông tin khách hàng thành công!");
                    LoadData();
                    ClearFields();
                }
                else
                {
                    MessageBox.Show("Không tìm thấy khách hàng cần sửa!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi sửa khách hàng: " + ex.Message);
            }
        }

        // Xóa khách hàng
        private void btnXoa_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtMaKH.Text))
                {
                    MessageBox.Show("Vui lòng chọn khách hàng cần xóa!");
                    return;
                }

                if (!int.TryParse(txtMaKH.Text, out int maKH))
                {
                    MessageBox.Show("Mã KH không hợp lệ!");
                    return;
                }

                // ✅ KIỂM TRA RÀNG BUỘC: có phiếu đặt phòng nào tham chiếu KH này không?
                bool dangDuocThamChieu = db.DatPhong.Any(dp => dp.MaKH == maKH);
                if (dangDuocThamChieu)
                {
                    MessageBox.Show(
                        "Không thể xóa vì khách hàng đang được sử dụng trong bảng đặt phòng.\n" +
                        "Hãy xóa/cập nhật các phiếu đặt liên quan trước.",
                        "Không thể xóa", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var kh = db.KhachHang.FirstOrDefault(x => x.MaKH == maKH);
                if (kh == null)
                {
                    MessageBox.Show("Không tìm thấy khách hàng để xóa!");
                    return;
                }

                if (MessageBox.Show("Bạn có chắc muốn xóa khách hàng này?",
                                    "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                    return;

                db.KhachHang.Remove(kh);
                db.SaveChanges();
                MessageBox.Show("Xóa khách hàng thành công!");
                LoadData();
                ClearFields();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi xóa khách hàng: " + ex.GetBaseException().Message);
            }
        }


        // Làm mới danh sách
        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            ClearFields();
            LoadData();
        }

        // Khi click 1 dòng trong DataGridView
        private void dgvKhachHang_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvKhachHang.Rows[e.RowIndex];
                txtMaKH.Text = row.Cells["MaKH"].Value.ToString();
                txtTenKH.Text = row.Cells["TenKH"].Value.ToString();
                cboGioiTinh.Text = row.Cells["GioiTinh"].Value.ToString();
                txtSDT.Text = row.Cells["SDT"].Value.ToString();
                txtCMND.Text = row.Cells["CMND"].Value.ToString();
            }
        }

        // Nút thoát
        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        // Nút tìm kiếm theo mã KH
        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtTimKiemMaKH.Text))
                {
                    MessageBox.Show("Vui lòng nhập mã khách hàng cần tìm!");
                    return;
                }

                int maKH = int.Parse(txtTimKiemMaKH.Text);
                var kh = db.KhachHang
                           .Where(k => k.MaKH == maKH)
                           .Select(k => new
                           {
                               k.MaKH,
                               k.TenKH,
                               k.GioiTinh,
                               k.SDT,
                               k.CMND
                           })
                           .ToList();

                if (kh.Count == 0)
                {
                    MessageBox.Show("Không tìm thấy khách hàng có mã " + maKH);
                }
                else
                {
                    dgvKhachHang.DataSource = kh;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tìm kiếm: " + ex.Message);
            }
        }
    }
}
