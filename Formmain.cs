using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace qlsv_pj
{
    public partial class Formmain : Form
    {
        public bool isEx = true;
        public string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=DB_QLSV;Integrated Security=True;";
        public int id = 0;
        public event EventHandler logout;
        public Formmain()
        {
            InitializeComponent();
        }

        private void textBox8_TextChanged(object sender, EventArgs e)
        {

        }

        private void Formmain_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (isEx == true)
            Application.Exit();
        }
        void phanquyen()
        {
            if (@const.accounttype == false)
            {
                tsmiuser.Enabled = tsmisv.Enabled = tsmigv.Enabled = false;
            }
        }
        private void Formmain_Load(object sender, EventArgs e)
        {
            LoadDataIntoDataGridView();
            tsthem.Enabled = tstsua.Enabled = tstxoa.Enabled = true;
            phanquyen();
        }

        private void Formmain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (isEx)
            {
                if (MessageBox.Show("Bạn muốn thoát chương trình", "Cảnh báo", MessageBoxButtons.YesNo) != DialogResult.Yes)
                    e.Cancel = true;
            }
           
        }

        private void đăngXuấtToolStripMenuItem_Click(object sender, EventArgs e)
        {
            logout(this, new EventArgs());
        }

        private void tsmisv_Click(object sender, EventArgs e)
        {
            tsthem.Enabled = tstsua.Enabled = tstxoa.Enabled = true;
        }

        private void tsmiuser_Click(object sender, EventArgs e)
        {
            Formnguoidung f = new Formnguoidung();
            f.Show();
        }
        private void LoadDataIntoDataGridView()
        {
            SqlConnection conn = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=DB_QLSV;Integrated Security=True;");
            string insertQuery = "SELECT * FROM sinhvien";
            using (SqlCommand command = new SqlCommand(insertQuery, conn))
            {
                SqlDataAdapter adapter = new SqlDataAdapter(insertQuery, conn);
                DataTable dataTable = new DataTable();
                conn.Open();
                adapter.Fill(dataTable);

                // Gán dữ liệu từ DataTable vào DataGridView
                dtbvsv.DataSource = dataTable;

                // Ẩn cột đầu tiên (cột 0)
                dtbvsv.Columns[0].Visible = false;
            }
        }


        private void tsmigv_Click(object sender, EventArgs e)
        {
            Formqlgv f = new Formqlgv();
            f.Show();
        }

        private void tsthem_Click(object sender, EventArgs e)
        {
            ThemSinhVien(textBox8.Text, txtgioitinh.Text, (txtngaysinh.Text), txtmasv.Text, txtdiachi.Text, txtemail.Text);
            LoadDataIntoDataGridView();
        }
        public void CapNhatSinhVien(int id, string hoten, string gioitinh, string ngaySinhstr, string masv, string diachi, string email)
        {
            if (!IsValidEmail(email))
            {
                MessageBox.Show("Địa chỉ email không hợp lệ", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            if (!DateTime.TryParseExact(ngaySinhstr.Trim(), "dd/MM/yyyy", null, DateTimeStyles.None, out DateTime ngaySinh))
            {
                MessageBox.Show(ngaySinhstr + "??", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("UPDATE sinhvien SET hoten = @hoten, gioitinh = @gioitinh, ngaysinh = @ngaysinh, masv = @masv, diachi = @diachi, email = @email WHERE Id = @id", connection))
                {
                    command.Parameters.Add("@id", SqlDbType.Int).Value = id;
                    command.Parameters.Add("@hoten", SqlDbType.NVarChar).Value = hoten;
                    command.Parameters.Add("@gioitinh", SqlDbType.NVarChar).Value = gioitinh;
                    command.Parameters.Add("@ngaysinh", SqlDbType.Date).Value = ngaySinh.Date;
                    command.Parameters.Add("@masv", SqlDbType.NVarChar).Value = masv;
                    command.Parameters.Add("@diachi", SqlDbType.NVarChar).Value = diachi;
                    command.Parameters.Add("@email", SqlDbType.NVarChar).Value = email;

                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Cập nhật thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Không tìm thấy sinh viên có ID = " + id, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }
        private bool IsValidEmail(string email)
        {
            // Sử dụng biểu thức chính quy để kiểm tra địa chỉ email
            // ^: Bắt đầu chuỗi
            // \w+: Chấp nhận một hoặc nhiều ký tự chữ cái, số, hoặc dấu gạch dưới
            // @: Ký tự '@'
            // \w+: Chấp nhận một hoặc nhiều ký tự chữ cái, số, hoặc dấu gạch dưới (domain)
            // \.: Ký tự '.'
            // \w{2,}: Chấp nhận ít nhất 2 ký tự chữ cái hoặc số (top-level domain)
            // $: Kết thúc chuỗi
            string pattern = @"^\w+@\w+\.\w{2,}$";
            return System.Text.RegularExpressions.Regex.IsMatch(email, pattern);
        }
        public void XoaSinhVien(int id)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("DELETE FROM sinhvien WHERE Id = @id", connection))
                {
                    command.Parameters.Add("@id", SqlDbType.Int).Value = id;

                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Xóa thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Không tìm thấy sinh viên có ID = " + id, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }
        public bool ThemSinhVien(string hoTen, string gioiTinh, string  ngaySinhstr, string maSv, string diaChi, string email)
        {
            try
            {
                if (!DateTime.TryParseExact(ngaySinhstr.Trim(), "dd/MM/yyyy", null, DateTimeStyles.None, out DateTime ngaySinh)|| !IsValidEmail(email))
                {
                    if (!IsValidEmail(email))
                    {
                        MessageBox.Show("Địa chỉ email không hợp lệ", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                   else
                    {
                        MessageBox.Show(ngaySinhstr + "??", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
            
                }
                else 
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    string insertQuery = "INSERT INTO sinhvien (hoten, gioitinh, ngaysinh, masv, diachi, email) " +
                                         "VALUES (@HoTen, @GioiTinh, @NgaySinh, @MaSv, @DiaChi, @Email)";

                    using (SqlCommand command = new SqlCommand(insertQuery, conn))
                    {
                        command.Parameters.AddWithValue("@HoTen", hoTen);
                        command.Parameters.AddWithValue("@GioiTinh", gioiTinh);
                        command.Parameters.AddWithValue("@NgaySinh", ngaySinh);
                        command.Parameters.AddWithValue("@MaSv", maSv);
                        command.Parameters.AddWithValue("@DiaChi", diaChi);
                        command.Parameters.AddWithValue("@Email", email);

                        int rowsAffected = command.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Thêm sinh viên thành công!");
                            textBox8.Clear();
                            txtgioitinh.Clear();
                            txtngaysinh.Clear();
                            txtmasv.Clear();
                            txtdiachi.Clear();
                            txtemail.Clear();
                     
                        }
                        else
                        {
                            MessageBox.Show("Thêm sinh viên thất bại!");
                        }
                        return rowsAffected > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                // Xử lý lỗi nếu có
                MessageBox.Show("Lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        private void tstluu_Click(object sender, EventArgs e)
        {
            textBox8.Clear();
            txtgioitinh.Clear();
            txtngaysinh.Clear();
            txtmasv.Clear();
            txtdiachi.Clear();
            txtemail.Clear();
            LoadDataIntoDataGridView();

        }

        private void tstsua_Click(object sender, EventArgs e)
        {
            CapNhatSinhVien(id,textBox8.Text, txtgioitinh.Text, (txtngaysinh.Text), txtmasv.Text, txtdiachi.Text, txtemail.Text);
            LoadDataIntoDataGridView();
        }

        private void dtbvsv_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                // Gán dữ liệu từ ô được bấm vào các TextBox
                id = Convert.ToUInt16(dtbvsv.Rows[e.RowIndex].Cells[0].Value.ToString());
                textBox8.Text = dtbvsv.Rows[e.RowIndex].Cells[1].Value.ToString();
                txtgioitinh.Text = dtbvsv.Rows[e.RowIndex].Cells[2].Value.ToString();
                DateTime ngayGio = Convert.ToDateTime(dtbvsv.Rows[e.RowIndex].Cells[3].Value);

                // Chuyển đổi thành dạng ngày và hiển thị
                txtngaysinh.Text = ngayGio.ToString("dd/MM/yyyy");
                txtmasv.Text = dtbvsv.Rows[e.RowIndex].Cells[4].Value.ToString();
                txtdiachi.Text = dtbvsv.Rows[e.RowIndex].Cells[5].Value.ToString();
                txtemail.Text = dtbvsv.Rows[e.RowIndex].Cells[6].Value.ToString();

                // Ẩn cột đầu tiên (cột ID)
                dtbvsv.Columns[0].Visible = false;
            }
        }



        private void tstxoa_Click(object sender, EventArgs e)
        {
            XoaSinhVien(id);
            LoadDataIntoDataGridView();
            textBox8.Clear();
            txtgioitinh.Clear();
            txtngaysinh.Clear();
            txtmasv.Clear();
            txtdiachi.Clear();
            txtemail.Clear();
        }

        private void trợGiúpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Formhelp f = new Formhelp();
            f.ShowDialog(); 
        }

        private void quảnLýToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
    }
}
