using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyPhongTap
{
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            tabMain_SelectedIndexChanged(sender, e);
        }

        private void tabMain_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (tabMain.SelectedIndex)
            { 
                case 0:
                    tabPhongTap.Controls.Add(new UcPhongTap());
                    break;
                default:
                    break;
            }
        }
    }
}
