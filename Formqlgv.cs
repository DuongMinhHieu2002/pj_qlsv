using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
namespace qlsv_pj
{
    public partial class Formqlgv : Form
    {
        public int id;
        public Formqlgv()
        {
            InitializeComponent();
        }

        private void Formqlgv_Load(object sender, EventArgs e)
        {
            LoadDataIntoDataGridView();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void btnthoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnthem_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=DB_QLSV;Integrated Security=True;");
            conn.Open();
            string insertQuery = "INSERT INTO giaovien (hoten,sdt,diachi,monhoc) VALUES (@HoTen, @SDT, @DiaChi, @MonHoc)";
            if (txthoten.Text == "" || txtsdt.Text == "" || txtdiachi.Text == "" || txtmonhoc.Text == "")
            {
                MessageBox.Show("Đữ liệu chưa đầy đủ","Lỗi",MessageBoxButtons.OK);
            }
            else if (!IsValidPhoneNumber(txtsdt.Text))
            {
                MessageBox.Show("Số điện thoại không hợp lệ. Vui lòng nhập số điện thoại gồm 10 chữ số.", "Lỗi", MessageBoxButtons.OK);
            }
            else
            {
                using (SqlCommand command = new SqlCommand(insertQuery, conn))
                {
                    command.Parameters.AddWithValue("@HoTen", txthoten.Text);
                    command.Parameters.AddWithValue("@SDT", txtsdt.Text);
                    command.Parameters.AddWithValue("@DiaChi", txtdiachi.Text);
                    command.Parameters.AddWithValue("@MonHoc", txtmonhoc.Text);

                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Thêm giáo viên thành công!");
                        txtmonhoc.Clear();
                        txtdiachi.Clear();
                        txthoten.Clear();
                        txtsdt.Clear();
                        LoadDataIntoDataGridView();
                    }
                    else
                    {
                        MessageBox.Show("Thêm giáo viên thất bại!");
                    }
                }
            }
            conn.Close();

        }
        private void LoadDataIntoDataGridView()
        {
            
            SqlConnection conn = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=DB_QLSV;Integrated Security=True;");
            string insertQuery = "SELECT * FROM giaovien";
            using (SqlCommand command = new SqlCommand(insertQuery, conn))
            {
                
                SqlDataAdapter adapter = new SqlDataAdapter(insertQuery, conn);
                DataTable dataTable = new DataTable();
                conn.Open();
                adapter.Fill(dataTable);
                // Gán dữ liệu từ DataTable vào DataGridView
                dtgvbv.DataSource = dataTable;
                dtgvbv.Columns[0].Visible = false;
            }
        }
        private void XoaGiaoVien(int teacherId)
        {
            SqlConnection conn = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=DB_QLSV;Integrated Security=True;");
            conn.Open();
            string deleteQuery = "DELETE FROM giaovien WHERE Id = @TeacherId";

            using (SqlCommand command = new SqlCommand(deleteQuery, conn))
            {
                command.Parameters.AddWithValue("@TeacherId", teacherId);

                int rowsAffected = command.ExecuteNonQuery();
                if (rowsAffected > 0)
                {
                    MessageBox.Show("Xóa giáo viên thành công!");
                    LoadDataIntoDataGridView(); // Cập nhật DataGridView sau khi xóa
                }
                else
                {
                    MessageBox.Show("Xóa giáo viên thất bại!");
                }
            }

            conn.Close();
        }
       

        private void dtgvbv_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            id = Convert.ToUInt16 (dtgvbv.Rows[e.RowIndex].Cells[0].Value.ToString());
            txthoten.Text = dtgvbv.Rows[e.RowIndex].Cells[1].Value.ToString();
            txtsdt.Text = dtgvbv.Rows[e.RowIndex].Cells[2].Value.ToString();
            txtdiachi.Text = dtgvbv.Rows[e.RowIndex].Cells[3].Value.ToString();
            txtmonhoc.Text = dtgvbv.Rows[e.RowIndex].Cells[4].Value.ToString();
        }

        private void btnrf_Click(object sender, EventArgs e)
        {
            LoadDataIntoDataGridView();
        }

        private void btnxoa_Click(object sender, EventArgs e)
        {
            XoaGiaoVien(id);
        }

        private void btnsua_Click(object sender, EventArgs e)
        {
            if (txthoten.Text == "" || txtdiachi.Text == "" || txtmonhoc.Text == "")//1
            {
                MessageBox.Show("Dữ liệu chưa đầy đủ", "Lỗi", MessageBoxButtons.OK);//2
            }
            else if (!IsValidPhoneNumber(txtsdt.Text))//3
            {
                MessageBox.Show("Số điện thoại không hợp lệ. Vui lòng nhập số điện thoại gồm 10 chữ số.", "Lỗi", MessageBoxButtons.OK);//4
            }
            else
            {
                CapNhatGiaoVien(id, txthoten.Text, txtsdt.Text, txtdiachi.Text, txtmonhoc.Text);
                LoadDataIntoDataGridView();
            }
        }
        private void CapNhatGiaoVien(int teacherId, string hoTen, string sdt, string diaChi, string monHoc)
        {
            SqlConnection conn = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=DB_QLSV;Integrated Security=True;");//5
            conn.Open(); //5
            string updateQuery = "UPDATE giaovien SET hoten = @HoTen, sdt = @SDT, diachi = @DiaChi, monhoc = @MonHoc WHERE Id = @TeacherId";//5

            using (SqlCommand command = new SqlCommand(updateQuery, conn))//6
            {
                command.Parameters.AddWithValue("@HoTen", hoTen);//7
                command.Parameters.AddWithValue("@SDT", sdt);//7
                command.Parameters.AddWithValue("@DiaChi", diaChi);//7
                command.Parameters.AddWithValue("@MonHoc", monHoc);//7
                command.Parameters.AddWithValue("@TeacherId", teacherId);//7

                int rowsAffected = command.ExecuteNonQuery();//7
                if (rowsAffected > 0)//8
                {
                    MessageBox.Show("Cập nhật thông tin giáo viên thành công!");//9
                    LoadDataIntoDataGridView(); // Cập nhật DataGridView sau khi cập nhật dữ liệu// 9
                }
                else
                {
                    MessageBox.Show("Cập nhật thông tin giáo viên thất bại!");//10
                }
            }

            conn.Close();//11
        }

        // Hàm kiểm tra số điện thoại có đúng định dạng 10 chữ số hay không
        private bool IsValidPhoneNumber(string phoneNumber)
        {
            // Sử dụng biểu thức chính quy để kiểm tra
            // Định dạng số điện thoại gồm 10 chữ số
            // ^: Bắt đầu chuỗi
            // \d{10}: Chấp nhận chính xác 10 ký tự số (\d)
            // $: Kết thúc chuỗi
            string pattern = @"^\d{10}$";
            return System.Text.RegularExpressions.Regex.IsMatch(phoneNumber, pattern);
        }

    }
}
