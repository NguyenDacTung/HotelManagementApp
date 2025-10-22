using HotelManagementApp.Models;
using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace HotelManagementApp
{
    public partial class FrmLogin : Form
    {
        private Model1 db = new Model1();

        public FrmLogin()
        {
            InitializeComponent();
        }

        private void FrmLogin_Load(object sender, EventArgs e)
        {
            txtUser.Focus();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string tenDangNhap = txtUser.Text.Trim();
            string matKhau = txtPass.Text.Trim();

            if (tenDangNhap == "" || matKhau == "")
            {
                MessageBox.Show("Vui lòng nhập đầy đủ tên đăng nhập và mật khẩu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                var taiKhoan = db.TaiKhoan
                    .FirstOrDefault(t => t.TenDangNhap == tenDangNhap && t.MatKhau == matKhau);

                if (taiKhoan != null)
                {
                    MessageBox.Show("Đăng nhập thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // ✅ truyền quyền và tên đăng nhập sang FrmMain
                    FrmMain frmMain = new FrmMain(taiKhoan.TenDangNhap, taiKhoan.Quyen);
                    frmMain.MaNhanVien = (int)taiKhoan.MaNV;
                    frmMain.Quyen = taiKhoan.Quyen;
                    frmMain.TenDangNhap = taiKhoan.TenDangNhap;

                    this.Hide();
                    frmMain.ShowDialog();
                    this.Show();
                }
                else
                {
                    MessageBox.Show("Sai tên đăng nhập hoặc mật khẩu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi kết nối cơ sở dữ liệu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
