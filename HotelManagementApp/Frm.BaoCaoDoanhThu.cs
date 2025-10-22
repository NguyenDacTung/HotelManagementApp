using System;
using System.Drawing;
using System.Windows.Forms;
using System.Data;
using System.Linq;
using System.Collections.Generic;
using System.Reflection;

// Thư viện cần thiết cho EF/LINQ
using System.Data.Entity;
using HotelManagementApp.Models; // Namespace chứa Model1 và các Entity

namespace HotelManagementApp
{
    public partial class FrmBaoCaoDoanhThu : Form
    {
        // Khai báo db là field theo yêu cầu của bạn
        private Model1 db = new Model1();

        public FrmBaoCaoDoanhThu()
        {
            InitializeComponent();
        }

        private void FrmBaoCaoDoanhThu_Load(object sender, EventArgs e)
        {
            InitializeComboBoxes();
            btnThongKe_Click(sender, e);
        }

        private void InitializeComboBoxes()
        {
            for (int i = 1; i <= 12; i++)
            {
                cboThang.Items.Add(i.ToString());
            }
            cboThang.SelectedIndex = DateTime.Now.Month - 1;

            int currentYear = DateTime.Now.Year;
            for (int i = currentYear - 5; i <= currentYear; i++)
            {
                cboNam.Items.Add(i.ToString());
            }
            cboNam.SelectedItem = currentYear.ToString();
        }

        private void btnThongKe_Click(object sender, EventArgs e)
        {
            if (cboThang.SelectedItem == null || cboNam.SelectedItem == null)
            {
                MessageBox.Show("Vui lòng chọn đầy đủ Tháng và Năm.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int month = int.Parse(cboThang.SelectedItem.ToString());
            int year = int.Parse(cboNam.SelectedItem.ToString());

            try
            {
                DataTable dtDoanhThu = GetRevenueData(month, year);

                dgvDoanhThu.DataSource = dtDoanhThu;

                CalculateAndDisplayTotal(dtDoanhThu);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi trong quá trình thống kê. Lỗi: " + ex.Message, "Lỗi Database/Entity", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ✅ LOGIC CHỈ TRUY VẤN BẢNG HoaDon
        private DataTable GetRevenueData(int month, int year)
        {
            DateTime startDate = new DateTime(year, month, 1);
            DateTime endDate = startDate.AddMonths(1).AddDays(-1);

            // Chỉ truy vấn bảng HoaDon và lọc theo tháng/năm
            var revenueList = db.HoaDon
                .Where(hd => DbFunctions.TruncateTime(hd.NgayLap) >= startDate.Date &&
                             DbFunctions.TruncateTime(hd.NgayLap) <= endDate.Date)
                .Select(hd => new
                {
                    // Tên thuộc tính phải khớp với DataPropertyName trong Designer
                    MaHD = hd.MaHD,
                    MaDatPhong = hd.MaDatPhong, // Lấy MaDatPhong thay vì MaPhong
                    NgayLap = hd.NgayLap,
                    TongTien = hd.TongTien
                })
                .OrderBy(x => x.NgayLap)
                .ToList();

            return ConvertListToDataTable(revenueList);
        }

        // Hàm hỗ trợ để chuyển List<T> sang DataTable 
        private DataTable ConvertListToDataTable<T>(List<T> items)
        {
            DataTable dataTable = new DataTable(typeof(T).Name);
            PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (PropertyInfo prop in Props)
            {
                dataTable.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
            }

            foreach (T item in items)
            {
                var values = new object[Props.Length];
                for (int i = 0; i < Props.Length; i++)
                {
                    values[i] = Props[i].GetValue(item, null);
                }
                dataTable.Rows.Add(values);
            }
            return dataTable;
        }

        // Hàm tính toán và hiển thị tổng doanh thu
        private void CalculateAndDisplayTotal(DataTable dt)
        {
            decimal totalRevenue = 0;

            if (dt != null && dt.Rows.Count > 0)
            {
                // Tính tổng từ cột "TongTien"
                totalRevenue = dt.AsEnumerable()
                                 .Sum(row => row.Field<decimal>("TongTien"));
            }

            // Định dạng tiền tệ và hiển thị
            txtTongDoanhThu.Text = totalRevenue.ToString("N0") + " VNĐ";
        }
    }
}