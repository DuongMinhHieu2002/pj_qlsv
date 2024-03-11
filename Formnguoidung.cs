using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace qlsv_pj
{
    public partial class Formnguoidung : Form
    {
        List<string> listactype = new List<string>() {"Quản trị viên","Người dùng"};
        int index = -1;
        public Formnguoidung()
        {
            InitializeComponent();
        }

        void LoadListUser() {
            dtgvtk.DataSource = null;
            dtgvtk.DataSource = listuser.Instance.Listacuser;
            dtgvtk.Refresh();
        }
        private void btnthoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dtgvtk_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            index = Convert.ToInt16(dtgvtk.Rows[e.RowIndex].Cells[0].Value.ToString());
            LoadListUser();
        }

        private void cbbloaitk_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void Formnguoidung_Load(object sender, EventArgs e)
        {
            listuser.Instance.Listacuser.Add(new user("user1", "h123", false));
            LoadListUser();
        }

        private void dtgvtk_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            index = e.RowIndex;
            txttentk.Text = dtgvtk.Rows[index].Cells[0].Value.ToString();
            txtmk.Text = dtgvtk.Rows[index].Cells[1].Value.ToString();

         

        }
        private bool IsPasswordValid(string password)
        {
            // Kiểm tra xem mật khẩu có ít nhất một chữ cái và một số không
            bool hasLetter = false;
            bool hasDigit = false;

            foreach (char c in password)
            {
                if (char.IsLetter(c))
                {
                    hasLetter = true;
                }
                else if (char.IsDigit(c))
                {
                    hasDigit = true;
                }

                if (hasLetter && hasDigit)
                {
                    return true; // Mật khẩu hợp lệ
                }
            }

            return false; // Mật khẩu không hợp lệ
        }


        private void btnthem_Click(object sender, EventArgs e)
        {
            string un = txttentk.Text;
            string pw = txtmk.Text;
            string repw = txb_repass.Text;

            bool userExists = false;

            foreach (user existingUser in listuser.Instance.Listacuser)
            {
                if (existingUser.Username == un)
                {
                    userExists = true;
                    break;
                }
            }

            if (userExists)
            {
                MessageBox.Show("Tên người dùng đã tồn tại trong danh sách", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (pw != repw)
            {
                MessageBox.Show("Nhập lại sai Password", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (!IsPasswordValid(pw))
            {
                MessageBox.Show("Mật khẩu phải chứa ít nhất một chữ cái và một số", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                bool at = false;
                listuser.Instance.Listacuser.Add(new user(un, pw, at));
                LoadListUser();
            }



        }

        private void btnsua_Click(object sender, EventArgs e)
        {
            string un = txttentk.Text;
            string pw = txtmk.Text;
            string repw = txb_repass.Text;

            bool userExists = false;
             // Biến này sẽ lưu trữ chỉ mục của sinh viên trong danh sách (nếu tồn tại)

            // Tìm kiếm sinh viên trong danh sách và kiểm tra xem tên sinh viên đã tồn tại hay chưa
             if (pw != repw)
            {
                MessageBox.Show("Nhập lại sai Password", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (!IsPasswordValid(pw))
            {
                MessageBox.Show("Mật khẩu phải chứa ít nhất một chữ cái và một số", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                // Cập nhật thông tin của sinh viên tại chỉ mục index (nếu có)
               
                {
                    listuser.Instance.Listacuser[index].Username = un;
                    listuser.Instance.Listacuser[index].Password = pw;
                    MessageBox.Show("Sửa thông tin sinh viên thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadListUser();
                }
            }
        }


        private void btnxoa_Click(object sender, EventArgs e)
        {
            listuser.Instance.Listacuser.RemoveAt(index);
            LoadListUser();
        }
    }
}
