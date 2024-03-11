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
    public partial class Formhelp : Form
    {
        public Formhelp()
        {
            InitializeComponent();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void Formhelp_Load(object sender, EventArgs e)
        {
            pictureBox1.Image = Image.FromFile("C:\\Users\\Duong Minh Hieu\\source\\repos\\qlsv_pj\\Resources\\qr.png");
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
        }
    }
}
