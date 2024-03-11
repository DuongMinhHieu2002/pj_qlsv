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
    public partial class Formdangnhap : Form
    {
        public Formdangnhap()
        {
            InitializeComponent();
        }
        bool checklogin(string un,string pw)
        {
            for(int i = 0; i < listuser.Instance.Listacuser.Count; i++)
            {
                if(un == listuser.Instance.Listacuser[i].Username && pw == listuser.Instance.Listacuser[i].Password)
                {
                    @const.accounttype = listuser.Instance.Listacuser[i].Actype;
                    return true;
                }
            }
            return false;
        }
        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void txtmatkhau_TextChanged(object sender, EventArgs e)
        {

        }

        private void Formdangnhap_Load(object sender, EventArgs e)
        {
            txtmatkhau.UseSystemPasswordChar = true;
        }

        private void btndangnhap_Click(object sender, EventArgs e)
        {
            string un = txttaikhoan.Text;
            string pw = txtmatkhau.Text;
            if (checklogin(un,pw))
            {
                Formmain f = new Formmain();
                f.Show();
                this.Hide();
                f.logout += F_logout;
            }
            else
            {
                MessageBox.Show("Sai Tên tài khoản hoạc mật khẩu","Lỗi",MessageBoxButtons.OK);
                txttaikhoan.Focus();
                return;
            }
           
        }

        private void F_logout(object sender, EventArgs e)
        {
            (sender as Formmain).isEx = false;
            (sender as Formmain).Close();
            this.Show();
        }

        private void btnthoat_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Formdangnhap_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
        private void cbhienthi_CheckedChanged(object sender, EventArgs e)
        {
            if(cbhienthi.Checked) {
                txtmatkhau.UseSystemPasswordChar = false;

            }
            if (!cbhienthi.Checked)
            {
                txtmatkhau.UseSystemPasswordChar = true;

            }
        }
    }
}
