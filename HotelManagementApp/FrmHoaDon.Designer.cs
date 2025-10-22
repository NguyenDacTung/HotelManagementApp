namespace HotelManagementApp
{
    partial class FrmHoaDon
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.lblMaHD = new System.Windows.Forms.Label();
            this.lblMaDatPhong = new System.Windows.Forms.Label();
            this.lblNgayLap = new System.Windows.Forms.Label();
            this.lblTongTien = new System.Windows.Forms.Label();

            this.txtMaHD = new System.Windows.Forms.TextBox();
            this.txtMaDatPhong = new System.Windows.Forms.TextBox();
            this.dtpNgayLap = new System.Windows.Forms.DateTimePicker();
            this.txtTongTien = new System.Windows.Forms.TextBox();

            this.dgvHoaDon = new System.Windows.Forms.DataGridView();

            this.btnSua = new System.Windows.Forms.Button();
            this.btnXoa = new System.Windows.Forms.Button();
            this.txtTimKiem = new System.Windows.Forms.TextBox();
            this.btnTimKiem = new System.Windows.Forms.Button();

            ((System.ComponentModel.ISupportInitialize)(this.dgvHoaDon)).BeginInit();
            this.SuspendLayout();

            // 
            // Labels
            // 
            this.lblMaHD.AutoSize = true;
            this.lblMaHD.Location = new System.Drawing.Point(30, 30);
            this.lblMaHD.Name = "lblMaHD";
            this.lblMaHD.Size = new System.Drawing.Size(79, 20);
            this.lblMaHD.Text = "Mã hóa đơn";

            this.lblMaDatPhong.AutoSize = true;
            this.lblMaDatPhong.Location = new System.Drawing.Point(30, 70);
            this.lblMaDatPhong.Name = "lblMaDatPhong";
            this.lblMaDatPhong.Size = new System.Drawing.Size(100, 20);
            this.lblMaDatPhong.Text = "Mã đặt phòng";

            this.lblNgayLap.AutoSize = true;
            this.lblNgayLap.Location = new System.Drawing.Point(30, 110);
            this.lblNgayLap.Name = "lblNgayLap";
            this.lblNgayLap.Size = new System.Drawing.Size(68, 20);
            this.lblNgayLap.Text = "Ngày lập";

            this.lblTongTien.AutoSize = true;
            this.lblTongTien.Location = new System.Drawing.Point(30, 150);
            this.lblTongTien.Name = "lblTongTien";
            this.lblTongTien.Size = new System.Drawing.Size(71, 20);
            this.lblTongTien.Text = "Tổng tiền";

            // 
            // TextBoxes & DateTimePicker
            // 
            this.txtMaHD.Location = new System.Drawing.Point(140, 27);
            this.txtMaHD.Name = "txtMaHD";
            this.txtMaHD.Size = new System.Drawing.Size(150, 27);

            this.txtMaDatPhong.Location = new System.Drawing.Point(140, 67);
            this.txtMaDatPhong.Name = "txtMaDatPhong";
            this.txtMaDatPhong.Size = new System.Drawing.Size(150, 27);

            this.dtpNgayLap.Location = new System.Drawing.Point(140, 107);
            this.dtpNgayLap.Name = "dtpNgayLap";
            this.dtpNgayLap.Size = new System.Drawing.Size(150, 27);

            this.txtTongTien.Location = new System.Drawing.Point(140, 147);
            this.txtTongTien.Name = "txtTongTien";
            this.txtTongTien.Size = new System.Drawing.Size(150, 27);

            // 
            // DataGridView
            // 
            this.dgvHoaDon.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvHoaDon.Location = new System.Drawing.Point(30, 230);
            this.dgvHoaDon.Name = "dgvHoaDon";
            this.dgvHoaDon.RowHeadersWidth = 51;
            this.dgvHoaDon.RowTemplate.Height = 29;
            this.dgvHoaDon.Size = new System.Drawing.Size(640, 230);
            this.dgvHoaDon.TabIndex = 10;
            this.dgvHoaDon.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvHoaDon_CellClick);

            // 
            // Buttons
            // 
            this.btnSua.Location = new System.Drawing.Point(330, 27);
            this.btnSua.Name = "btnSua";
            this.btnSua.Size = new System.Drawing.Size(94, 29);
            this.btnSua.Text = "Sửa";
            this.btnSua.UseVisualStyleBackColor = true;
            this.btnSua.Click += new System.EventHandler(this.btnSua_Click);

            this.btnXoa.Location = new System.Drawing.Point(330, 67);
            this.btnXoa.Name = "btnXoa";
            this.btnXoa.Size = new System.Drawing.Size(94, 29);
            this.btnXoa.Text = "Xóa";
            this.btnXoa.UseVisualStyleBackColor = true;
            this.btnXoa.Click += new System.EventHandler(this.btnXoa_Click);

            this.txtTimKiem.Location = new System.Drawing.Point(330, 110);
            this.txtTimKiem.Name = "txtTimKiem";
            this.txtTimKiem.Size = new System.Drawing.Size(180, 27);

            this.btnTimKiem.Location = new System.Drawing.Point(520, 110);
            this.btnTimKiem.Name = "btnTimKiem";
            this.btnTimKiem.Size = new System.Drawing.Size(94, 29);
            this.btnTimKiem.Text = "Tìm kiếm";
            this.btnTimKiem.UseVisualStyleBackColor = true;
            this.btnTimKiem.Click += new System.EventHandler(this.btnTimKiem_Click);

            // 
            // FrmHoaDon
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(700, 500);
            this.Controls.Add(this.lblMaHD);
            this.Controls.Add(this.lblMaDatPhong);
            this.Controls.Add(this.lblNgayLap);
            this.Controls.Add(this.lblTongTien);
            this.Controls.Add(this.txtMaHD);
            this.Controls.Add(this.txtMaDatPhong);
            this.Controls.Add(this.dtpNgayLap);
            this.Controls.Add(this.txtTongTien);
            this.Controls.Add(this.btnSua);
            this.Controls.Add(this.btnXoa);
            this.Controls.Add(this.txtTimKiem);
            this.Controls.Add(this.btnTimKiem);
            this.Controls.Add(this.dgvHoaDon);
            this.Name = "FrmHoaDon";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Quản lý hóa đơn";
            this.Load += new System.EventHandler(this.FrmHoaDon_Load);

            ((System.ComponentModel.ISupportInitialize)(this.dgvHoaDon)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label lblMaHD;
        private System.Windows.Forms.Label lblMaDatPhong;
        private System.Windows.Forms.Label lblNgayLap;
        private System.Windows.Forms.Label lblTongTien;

        private System.Windows.Forms.TextBox txtMaHD;
        private System.Windows.Forms.TextBox txtMaDatPhong;
        private System.Windows.Forms.DateTimePicker dtpNgayLap;
        private System.Windows.Forms.TextBox txtTongTien;

        private System.Windows.Forms.DataGridView dgvHoaDon;
        private System.Windows.Forms.Button btnSua;
        private System.Windows.Forms.Button btnXoa;
        private System.Windows.Forms.Button btnTimKiem;
        private System.Windows.Forms.TextBox txtTimKiem;
    }
}
