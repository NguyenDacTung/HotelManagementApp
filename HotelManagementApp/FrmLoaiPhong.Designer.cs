namespace HotelManagementApp
{
    partial class FrmLoaiPhong
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
            this.lblMaLoaiPhong = new System.Windows.Forms.Label();
            this.lblTenLoaiPhong = new System.Windows.Forms.Label();
            this.lblMoTa = new System.Windows.Forms.Label();
            this.lblGiaCoBan = new System.Windows.Forms.Label();

            this.txtMaLoaiPhong = new System.Windows.Forms.TextBox();
            this.txtTenLoaiPhong = new System.Windows.Forms.TextBox();
            this.txtMoTa = new System.Windows.Forms.TextBox();
            this.txtGiaCoBan = new System.Windows.Forms.TextBox();

            this.btnThem = new System.Windows.Forms.Button();
            this.btnSua = new System.Windows.Forms.Button();
            this.btnXoa = new System.Windows.Forms.Button();
            this.btnLamMoi = new System.Windows.Forms.Button();

            this.dgvLoaiPhong = new System.Windows.Forms.DataGridView();

            ((System.ComponentModel.ISupportInitialize)(this.dgvLoaiPhong)).BeginInit();
            this.SuspendLayout();

            // 
            // lblMaLoaiPhong
            // 
            this.lblMaLoaiPhong.AutoSize = true;
            this.lblMaLoaiPhong.Location = new System.Drawing.Point(30, 25);
            this.lblMaLoaiPhong.Name = "lblMaLoaiPhong";
            this.lblMaLoaiPhong.Size = new System.Drawing.Size(110, 20);
            this.lblMaLoaiPhong.Text = "Mã loại phòng:";

            // 
            // lblTenLoaiPhong
            // 
            this.lblTenLoaiPhong.AutoSize = true;
            this.lblTenLoaiPhong.Location = new System.Drawing.Point(400, 25);
            this.lblTenLoaiPhong.Name = "lblTenLoaiPhong";
            this.lblTenLoaiPhong.Size = new System.Drawing.Size(115, 20);
            this.lblTenLoaiPhong.Text = "Tên loại phòng:";

            // 
            // lblMoTa
            // 
            this.lblMoTa.AutoSize = true;
            this.lblMoTa.Location = new System.Drawing.Point(30, 70);
            this.lblMoTa.Name = "lblMoTa";
            this.lblMoTa.Size = new System.Drawing.Size(52, 20);
            this.lblMoTa.Text = "Mô tả:";

            // 
            // lblGiaCoBan
            // 
            this.lblGiaCoBan.AutoSize = true;
            this.lblGiaCoBan.Location = new System.Drawing.Point(400, 70);
            this.lblGiaCoBan.Name = "lblGiaCoBan";
            this.lblGiaCoBan.Size = new System.Drawing.Size(84, 20);
            this.lblGiaCoBan.Text = "Giá cơ bản:";

            // 
            // txtMaLoaiPhong
            // 
            this.txtMaLoaiPhong.Location = new System.Drawing.Point(150, 22);
            this.txtMaLoaiPhong.Name = "txtMaLoaiPhong";
            this.txtMaLoaiPhong.Size = new System.Drawing.Size(200, 27);

            // 
            // txtTenLoaiPhong
            // 
            this.txtTenLoaiPhong.Location = new System.Drawing.Point(520, 22);
            this.txtTenLoaiPhong.Name = "txtTenLoaiPhong";
            this.txtTenLoaiPhong.Size = new System.Drawing.Size(250, 27);

            // 
            // txtMoTa
            // 
            this.txtMoTa.Location = new System.Drawing.Point(150, 67);
            this.txtMoTa.Multiline = true;
            this.txtMoTa.Name = "txtMoTa";
            this.txtMoTa.Size = new System.Drawing.Size(200, 60);

            // 
            // txtGiaCoBan
            // 
            this.txtGiaCoBan.Location = new System.Drawing.Point(520, 67);
            this.txtGiaCoBan.Name = "txtGiaCoBan";
            this.txtGiaCoBan.Size = new System.Drawing.Size(250, 27);

            // 
            // btnThem
            // 
            this.btnThem.Location = new System.Drawing.Point(30, 145);
            this.btnThem.Name = "btnThem";
            this.btnThem.Size = new System.Drawing.Size(120, 40);
            this.btnThem.Text = "Thêm";
            this.btnThem.UseVisualStyleBackColor = true;
            this.btnThem.Click += new System.EventHandler(this.btnThem_Click);

            // 
            // btnSua
            // 
            this.btnSua.Location = new System.Drawing.Point(170, 145);
            this.btnSua.Name = "btnSua";
            this.btnSua.Size = new System.Drawing.Size(120, 40);
            this.btnSua.Text = "Sửa";
            this.btnSua.UseVisualStyleBackColor = true;
            this.btnSua.Click += new System.EventHandler(this.btnSua_Click);

            // 
            // btnXoa
            // 
            this.btnXoa.Location = new System.Drawing.Point(310, 145);
            this.btnXoa.Name = "btnXoa";
            this.btnXoa.Size = new System.Drawing.Size(120, 40);
            this.btnXoa.Text = "Xóa";
            this.btnXoa.UseVisualStyleBackColor = true;
            this.btnXoa.Click += new System.EventHandler(this.btnXoa_Click);

            // 
            // btnLamMoi
            // 
            this.btnLamMoi.Location = new System.Drawing.Point(450, 145);
            this.btnLamMoi.Name = "btnLamMoi";
            this.btnLamMoi.Size = new System.Drawing.Size(120, 40);
            this.btnLamMoi.Text = "Làm mới";
            this.btnLamMoi.UseVisualStyleBackColor = true;
            this.btnLamMoi.Click += new System.EventHandler(this.btnLamMoi_Click);

            // 
            // dgvLoaiPhong
            // 
            this.dgvLoaiPhong.AllowUserToAddRows = false;
            this.dgvLoaiPhong.AllowUserToDeleteRows = false;
            this.dgvLoaiPhong.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvLoaiPhong.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvLoaiPhong.Location = new System.Drawing.Point(30, 210);
            this.dgvLoaiPhong.MultiSelect = false;
            this.dgvLoaiPhong.Name = "dgvLoaiPhong";
            this.dgvLoaiPhong.ReadOnly = true;
            this.dgvLoaiPhong.RowHeadersVisible = false;
            this.dgvLoaiPhong.RowTemplate.Height = 29;
            this.dgvLoaiPhong.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvLoaiPhong.Size = new System.Drawing.Size(740, 280);
            this.dgvLoaiPhong.TabIndex = 10;
            this.dgvLoaiPhong.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvLoaiPhong_CellClick);

            // 
            // FrmLoaiPhong
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 520);
            this.Controls.Add(this.dgvLoaiPhong);
            this.Controls.Add(this.btnLamMoi);
            this.Controls.Add(this.btnXoa);
            this.Controls.Add(this.btnSua);
            this.Controls.Add(this.btnThem);
            this.Controls.Add(this.txtGiaCoBan);
            this.Controls.Add(this.txtMoTa);
            this.Controls.Add(this.txtTenLoaiPhong);
            this.Controls.Add(this.txtMaLoaiPhong);
            this.Controls.Add(this.lblGiaCoBan);
            this.Controls.Add(this.lblMoTa);
            this.Controls.Add(this.lblTenLoaiPhong);
            this.Controls.Add(this.lblMaLoaiPhong);
            this.Name = "FrmLoaiPhong";
            this.Text = "Quản lý loại phòng";
            this.Load += new System.EventHandler(this.FrmLoaiPhong_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvLoaiPhong)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label lblMaLoaiPhong;
        private System.Windows.Forms.Label lblTenLoaiPhong;
        private System.Windows.Forms.Label lblMoTa;
        private System.Windows.Forms.Label lblGiaCoBan;

        private System.Windows.Forms.TextBox txtMaLoaiPhong;
        private System.Windows.Forms.TextBox txtTenLoaiPhong;
        private System.Windows.Forms.TextBox txtMoTa;
        private System.Windows.Forms.TextBox txtGiaCoBan;

        private System.Windows.Forms.Button btnThem;
        private System.Windows.Forms.Button btnSua;
        private System.Windows.Forms.Button btnXoa;
        private System.Windows.Forms.Button btnLamMoi;

        private System.Windows.Forms.DataGridView dgvLoaiPhong;
    }
}
