using HotelManagementApp.Models;
using System;
using System.Linq;
using System.Windows.Forms;

namespace HotelManagementApp
{
    public partial class FrmDoiMatKhau : Form
    {
        private Model1 db = new Model1();
        private int maNhanVien; // Mã nhân viên đăng nhập hiện tại

        public FrmDoiMatKhau(int maNV)
        {
            InitializeComponent();
            this.maNhanVien = maNV;
        }

        private void FrmDoiMatKhau_Load(object sender, EventArgs e)
        {
            txtOldPass.Focus();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string matKhauCu = txtOldPass.Text.Trim();
            string matKhauMoi = txtNewPass.Text.Trim();
            string xacNhan = txtConfirm.Text.Trim();

            if (matKhauCu == "" || matKhauMoi == "" || xacNhan == "")
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (matKhauMoi != xacNhan)
            {
                MessageBox.Show("Mật khẩu mới và xác nhận không khớp!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                // Tìm tài khoản theo mã nhân viên
                var taiKhoan = db.TaiKhoan.FirstOrDefault(t => t.MaNV == maNhanVien);

                if (taiKhoan == null)
                {
                    MessageBox.Show("Không tìm thấy tài khoản!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (taiKhoan.MatKhau != matKhauCu)
                {
                    MessageBox.Show("Mật khẩu cũ không đúng!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Cập nhật mật khẩu
                taiKhoan.MatKhau = matKhauMoi;
                db.SaveChanges();

                MessageBox.Show("Đổi mật khẩu thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Lỗi hệ thống", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
