namespace HotelManagementApp
{
    partial class FrmDatPhong
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblMaDP = new System.Windows.Forms.Label();
            this.txtMaDatPhong = new System.Windows.Forms.TextBox();
            this.lblMaKH = new System.Windows.Forms.Label();
            this.cboMaKH = new System.Windows.Forms.ComboBox();
            this.lblMaNV = new System.Windows.Forms.Label();
            this.cboMaNV = new System.Windows.Forms.ComboBox();
            this.lblMaPhong = new System.Windows.Forms.Label();
            this.cboMaPhong = new System.Windows.Forms.ComboBox();
            this.lblDonGia = new System.Windows.Forms.Label();
            this.txtDonGia = new System.Windows.Forms.TextBox();
            this.lblNgayDat = new System.Windows.Forms.Label();
            this.dtpNgayDat = new System.Windows.Forms.DateTimePicker();
            this.lblNgayDen = new System.Windows.Forms.Label();
            this.dtpNgayDen = new System.Windows.Forms.DateTimePicker();
            this.lblNgayDi = new System.Windows.Forms.Label();
            this.dtpNgayDi = new System.Windows.Forms.DateTimePicker();
            this.lblTongTien = new System.Windows.Forms.Label();
            this.dgvDatPhong = new System.Windows.Forms.DataGridView();

            this.btnThem = new System.Windows.Forms.Button();
            this.btnXoa = new System.Windows.Forms.Button();
            this.btnLamMoi = new System.Windows.Forms.Button();
            this.btnTimKiem = new System.Windows.Forms.Button();
            this.btnLapHoaDon = new System.Windows.Forms.Button();

            this.txtTimKiem = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDatPhong)).BeginInit();
            this.SuspendLayout();

            // === FORM TITLE ===
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblTitle.Location = new System.Drawing.Point(320, 20);
            this.lblTitle.Text = "QUẢN LÝ ĐẶT PHÒNG";

            // === MÃ ĐẶT PHÒNG ===
            this.lblMaDP.AutoSize = true;
            this.lblMaDP.Location = new System.Drawing.Point(50, 80);
            this.lblMaDP.Text = "Mã đặt phòng:";
            this.txtMaDatPhong.Location = new System.Drawing.Point(180, 77);
            this.txtMaDatPhong.Width = 150;
            this.txtMaDatPhong.ReadOnly = true;

            // === KHÁCH HÀNG ===
            this.lblMaKH.AutoSize = true;
            this.lblMaKH.Location = new System.Drawing.Point(50, 115);
            this.lblMaKH.Text = "Khách hàng:";
            this.cboMaKH.Location = new System.Drawing.Point(180, 112);
            this.cboMaKH.Width = 200;

            // === NHÂN VIÊN ===
            this.lblMaNV.AutoSize = true;
            this.lblMaNV.Location = new System.Drawing.Point(50, 150);
            this.lblMaNV.Text = "Nhân viên:";
            this.cboMaNV.Location = new System.Drawing.Point(180, 147);
            this.cboMaNV.Width = 200;

            // === PHÒNG ===
            this.lblMaPhong.AutoSize = true;
            this.lblMaPhong.Location = new System.Drawing.Point(50, 185);
            this.lblMaPhong.Text = "Phòng:";
            this.cboMaPhong.Location = new System.Drawing.Point(180, 182);
            this.cboMaPhong.Width = 200;
            this.cboMaPhong.SelectedIndexChanged += new System.EventHandler(this.cboMaPhong_SelectedIndexChanged);

            // === ĐƠN GIÁ ===
            this.lblDonGia.AutoSize = true;
            this.lblDonGia.Location = new System.Drawing.Point(50, 220);
            this.lblDonGia.Text = "Đơn giá:";
            this.txtDonGia.Location = new System.Drawing.Point(180, 217);
            this.txtDonGia.Width = 200;
            this.txtDonGia.ReadOnly = true;

            // === NGÀY ĐẶT ===
            this.lblNgayDat.AutoSize = true;
            this.lblNgayDat.Location = new System.Drawing.Point(420, 80);
            this.lblNgayDat.Text = "Ngày đặt:";
            this.dtpNgayDat.Location = new System.Drawing.Point(520, 77);
            this.dtpNgayDat.Width = 200;

            // === NGÀY ĐẾN ===
            this.lblNgayDen.AutoSize = true;
            this.lblNgayDen.Location = new System.Drawing.Point(420, 115);
            this.lblNgayDen.Text = "Ngày đến:";
            this.dtpNgayDen.Location = new System.Drawing.Point(520, 112);
            this.dtpNgayDen.Width = 200;
            this.dtpNgayDen.ValueChanged += new System.EventHandler(this.dtpNgayDen_ValueChanged);

            // === NGÀY ĐI ===
            this.lblNgayDi.AutoSize = true;
            this.lblNgayDi.Location = new System.Drawing.Point(420, 150);
            this.lblNgayDi.Text = "Ngày đi:";
            this.dtpNgayDi.Location = new System.Drawing.Point(520, 147);
            this.dtpNgayDi.Width = 200;
            this.dtpNgayDi.ValueChanged += new System.EventHandler(this.dtpNgayDi_ValueChanged);

            // === TỔNG TIỀN ===
            this.lblTongTien.AutoSize = true;
            this.lblTongTien.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblTongTien.ForeColor = System.Drawing.Color.DarkBlue;
            this.lblTongTien.Location = new System.Drawing.Point(420, 190);
            this.lblTongTien.Text = "Tổng tiền: 0 VND";

            // === NÚT CHỨC NĂNG ===
            this.btnThem.Text = "Đặt phòng";
            this.btnThem.Location = new System.Drawing.Point(50, 270);
            this.btnThem.Click += new System.EventHandler(this.btnThem_Click);

            this.btnXoa.Text = "Xóa";
            this.btnXoa.Location = new System.Drawing.Point(160, 270);
            this.btnXoa.Click += new System.EventHandler(this.btnXoa_Click);

            this.btnLamMoi.Text = "Làm mới";
            this.btnLamMoi.Location = new System.Drawing.Point(270, 270);
            this.btnLamMoi.Click += new System.EventHandler(this.btnLamMoi_Click);

            this.btnTimKiem.Text = "Tìm kiếm";
            this.btnTimKiem.Location = new System.Drawing.Point(600, 270);
            this.btnTimKiem.Click += new System.EventHandler(this.btnTimKiem_Click);

            this.btnLapHoaDon.Text = "Lập hóa đơn";
            this.btnLapHoaDon.Location = new System.Drawing.Point(710, 270);
            this.btnLapHoaDon.Click += new System.EventHandler(this.btnLapHoaDon_Click);

            // === Ô TÌM KIẾM ===
            this.txtTimKiem.Location = new System.Drawing.Point(420, 272);
            this.txtTimKiem.Width = 170;

            // === DATAGRIDVIEW ===
            this.dgvDatPhong.Location = new System.Drawing.Point(50, 320);
            this.dgvDatPhong.Width = 760;
            this.dgvDatPhong.Height = 250;
            this.dgvDatPhong.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;

            // === FORM SETUP ===
            this.ClientSize = new System.Drawing.Size(860, 600);
            this.Controls.AddRange(new System.Windows.Forms.Control[]
            {
                this.lblTitle, this.lblMaDP, this.txtMaDatPhong,
                this.lblMaKH, this.cboMaKH, this.lblMaNV, this.cboMaNV,
                this.lblMaPhong, this.cboMaPhong, this.lblDonGia, this.txtDonGia,
                this.lblNgayDat, this.dtpNgayDat, this.lblNgayDen, this.dtpNgayDen,
                this.lblNgayDi, this.dtpNgayDi, this.lblTongTien,
                this.btnThem, this.btnXoa, this.btnLamMoi,
                this.txtTimKiem, this.btnTimKiem, this.btnLapHoaDon,
                this.dgvDatPhong
            });
            this.Text = "Quản lý đặt phòng";
            this.Load += new System.EventHandler(this.FrmDatPhong_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDatPhong)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblMaDP;
        private System.Windows.Forms.TextBox txtMaDatPhong;
        private System.Windows.Forms.Label lblMaKH;
        private System.Windows.Forms.ComboBox cboMaKH;
        private System.Windows.Forms.Label lblMaNV;
        private System.Windows.Forms.ComboBox cboMaNV;
        private System.Windows.Forms.Label lblMaPhong;
        private System.Windows.Forms.ComboBox cboMaPhong;
        private System.Windows.Forms.Label lblDonGia;
        private System.Windows.Forms.TextBox txtDonGia;
        private System.Windows.Forms.Label lblNgayDat;
        private System.Windows.Forms.DateTimePicker dtpNgayDat;
        private System.Windows.Forms.Label lblNgayDen;
        private System.Windows.Forms.DateTimePicker dtpNgayDen;
        private System.Windows.Forms.Label lblNgayDi;
        private System.Windows.Forms.DateTimePicker dtpNgayDi;
        private System.Windows.Forms.Label lblTongTien;

        private System.Windows.Forms.Button btnThem;
        private System.Windows.Forms.Button btnXoa;
        private System.Windows.Forms.Button btnLamMoi;
        private System.Windows.Forms.Button btnTimKiem;
        private System.Windows.Forms.Button btnLapHoaDon;
        private System.Windows.Forms.TextBox txtTimKiem;
        private System.Windows.Forms.DataGridView dgvDatPhong;
    }
}
