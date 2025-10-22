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
            txtMaKH.Enabled = false;
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
        }

        // Thêm khách hàng mới
        private void btnThem_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtTenKH.Text))
                {
                    MessageBox.Show("Vui lòng nhập tên khách hàng!");
                    return;
                }

                KhachHang kh = new KhachHang
                {
                    TenKH = txtTenKH.Text.Trim(),
                    GioiTinh = cboGioiTinh.Text,
                    SDT = txtSDT.Text.Trim(),
                    CMND = txtCMND.Text.Trim()
                };

                db.KhachHang.Add(kh);
                db.SaveChanges();

                MessageBox.Show("Thêm khách hàng thành công!");
                LoadData();
                ClearFields();
            }
            catch (Exception ex)
            {
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

                int maKH = int.Parse(txtMaKH.Text);
                var kh = db.KhachHang.FirstOrDefault(x => x.MaKH == maKH);

                if (kh != null)
                {
                    if (MessageBox.Show("Bạn có chắc muốn xóa khách hàng này?",
                                        "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        db.KhachHang.Remove(kh);
                        db.SaveChanges();
                        MessageBox.Show("Xóa khách hàng thành công!");
                        LoadData();
                        ClearFields();
                    }
                }
                else
                {
                    MessageBox.Show("Không tìm thấy khách hàng để xóa!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi xóa khách hàng: " + ex.Message);
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

                txtMaKH.Enabled = false;
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
