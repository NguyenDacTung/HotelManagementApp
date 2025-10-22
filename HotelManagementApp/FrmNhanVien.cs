using HotelManagementApp.Models;
using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace HotelManagementApp
{
    public partial class FrmNhanVien : Form
    {
        private Model1 db = new Model1();
        private int? selectedMaNV = null; 

        public FrmNhanVien()
        {
            InitializeComponent();
        }

        private void FrmNhanVien_Load(object sender, EventArgs e)
        {
            LoadData();
            cboGioiTinh.SelectedIndex = 0; 
        }

        private void LoadData()
        {
            var list = db.NhanVien
                         .Select(nv => new
                         {
                             nv.MaNV,
                             nv.TenNV,
                             nv.GioiTinh,
                             nv.NgaySinh,
                             nv.DiaChi
                         })
                         .ToList();

            dgvNhanVien.DataSource = list;
            dgvNhanVien.Columns["MaNV"].HeaderText = "Mã NV";
            dgvNhanVien.Columns["TenNV"].HeaderText = "Tên nhân viên";
            dgvNhanVien.Columns["GioiTinh"].HeaderText = "Giới tính";
            dgvNhanVien.Columns["NgaySinh"].HeaderText = "Ngày sinh";
            dgvNhanVien.Columns["DiaChi"].HeaderText = "Địa chỉ";

            ClearInputs();
        }

        private void ClearInputs()
        {
            txtTenNV.Clear();
            txtDiaChi.Clear();
            dtpNgaySinh.Value = DateTime.Now;
            cboGioiTinh.SelectedIndex = 0;
            selectedMaNV = null;
        }

        private void dgvNhanVien_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvNhanVien.Rows[e.RowIndex];
                selectedMaNV = Convert.ToInt32(row.Cells["MaNV"].Value);
                txtTenNV.Text = row.Cells["TenNV"].Value?.ToString();
                cboGioiTinh.Text = row.Cells["GioiTinh"].Value?.ToString();
                dtpNgaySinh.Value = Convert.ToDateTime(row.Cells["NgaySinh"].Value);
                txtDiaChi.Text = row.Cells["DiaChi"].Value?.ToString();
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtTenNV.Text))
                {
                    MessageBox.Show("Vui lòng nhập tên nhân viên!");
                    return;
                }

                var nv = new NhanVien
                {
                    TenNV = txtTenNV.Text,
                    GioiTinh = cboGioiTinh.Text,
                    NgaySinh = dtpNgaySinh.Value,
                    DiaChi = txtDiaChi.Text
                };

                db.NhanVien.Add(nv);
                db.SaveChanges();
                MessageBox.Show("Thêm nhân viên thành công!");
                LoadData();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi thêm: " + ex.Message);
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (selectedMaNV == null)
            {
                MessageBox.Show("Vui lòng chọn nhân viên cần sửa!");
                return;
            }

            try
            {
                var nv = db.NhanVien.FirstOrDefault(x => x.MaNV == selectedMaNV);
                if (nv != null)
                {
                    nv.TenNV = txtTenNV.Text;
                    nv.GioiTinh = cboGioiTinh.Text;
                    nv.NgaySinh = dtpNgaySinh.Value;
                    nv.DiaChi = txtDiaChi.Text;

                    db.SaveChanges();
                    MessageBox.Show("Cập nhật thành công!");
                    LoadData();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi sửa: " + ex.Message);
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (selectedMaNV == null)
            {
                MessageBox.Show("Vui lòng chọn nhân viên cần xóa!");
                return;
            }

            if (MessageBox.Show("Bạn có chắc muốn xóa nhân viên này?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    var nv = db.NhanVien.FirstOrDefault(x => x.MaNV == selectedMaNV);
                    if (nv != null)
                    {
                        db.NhanVien.Remove(nv);
                        db.SaveChanges();
                        MessageBox.Show("Xóa thành công!");
                        LoadData();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi xóa: " + ex.Message);
                }
            }
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            LoadData();
        }
    }
}
