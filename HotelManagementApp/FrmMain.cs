using System;
using System.Windows.Forms;

namespace HotelManagementApp
{
    public partial class FrmMain : Form
    {
        // Thêm 3 property để FrmLogin có thể truyền dữ liệu sang
        public string TenDangNhap { get; set; }
        public string Quyen { get; set; }
        public int MaNhanVien { get; set; }

        public FrmMain()
        {
            InitializeComponent();
        }

        public FrmMain(string tenDangNhap, string quyen)
        {
            InitializeComponent();
            this.TenDangNhap = tenDangNhap;
            this.Quyen = quyen;
        }


        private void FrmMain_Load(object sender, EventArgs e)
        {
            lblNhanVien.Text = $"Nhân viên: {TenDangNhap} ({Quyen})";

            if (Quyen != null && Quyen.ToLower() == "nhanvien")
            {
                btnNhanVien.Enabled = false;
                btnBaoCao.Enabled = false;
            }
        }
        

        private void btnPhong_Click(object sender, EventArgs e)
        {
            FrmPhong frm = new FrmPhong(Quyen);
            frm.ShowDialog();
        }

        private void btnKhachHang_Click(object sender, EventArgs e)
        {
            FrmKhachHang f = new FrmKhachHang();
            f.ShowDialog();
        }

        private void btnDatPhong_Click(object sender, EventArgs e)
        {
            FrmDatPhong frm = new FrmDatPhong();
            frm.ShowDialog();
        }

        private void btnHoaDon_Click(object sender, EventArgs e)
        {
            FrmHoaDon f = new FrmHoaDon();
            f.ShowDialog();
        }

        private void btnNhanVien_Click(object sender, EventArgs e)
        {
            FrmNhanVien f = new FrmNhanVien();
            f.ShowDialog();
        }

        private void btnBaoCao_Click(object sender, EventArgs e)
        {
            FrmBaoCaoDoanhThu f = new FrmBaoCaoDoanhThu();
            f.ShowDialog();
        }

        private void btnDoiMatKhau_Click(object sender, EventArgs e)
        {
            FrmDoiMatKhau frm = new FrmDoiMatKhau(MaNhanVien);
            frm.ShowDialog();
        }

        private void btnDangXuat_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn đăng xuất?",
                                                  "Xác nhận",
                                                  MessageBoxButtons.YesNo,
                                                  MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                this.Hide();
                FrmLogin frmLogin = new FrmLogin();
                frmLogin.Show();
            }
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn thoát chương trình?",
                                                  "Xác nhận",
                                                  MessageBoxButtons.YesNo,
                                                  MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

    }
}
