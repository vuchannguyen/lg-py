using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Function;

namespace VNSC
{
    public partial class Form_Confirm : Form
    {
        private bool yes;

        public bool Yes
        {
            get { return yes; }
            set { yes = value; }
        }

        public Form_Confirm()
        {
            InitializeComponent();
        }

        public Form_Confirm(string sConfirm2)
        {
            InitializeComponent();

            LoadPic();
            SubFunction.SetError(lbNotice2, lbNotice2.Location.Y, pnNotice.Size, sConfirm2);
            lbNotice1.Text = "";

            this.ShowDialog();
        }

        public Form_Confirm(string sConfirm1, string sConfirm2)
        {
            InitializeComponent();

            LoadPic();
            SubFunction.SetError(lbNotice1, lbNotice1.Location.Y, pnNotice.Size, sConfirm1);
            SubFunction.SetError(lbNotice2, lbNotice2.Location.Y, pnNotice.Size, sConfirm2);

            this.ShowDialog();
        }

        private void LoadPic()
        {
            try
            {
                this.BackgroundImage = Image.FromFile(@"Resources\form_confirm.jpg");

                pbHuy.Image = Image.FromFile(@"Resources\ChucNang\icon_cancel.png");
                pbHoanTat.Image = Image.FromFile(@"Resources\ChucNang\icon_ok.png");
            }
            catch
            {
                this.Dispose();
                Form_Notice frm_Notice = new Form_Notice("Kiểm tra thư mục Resource!", false);
            }
        }

        private void Form_Confirm_Load(object sender, EventArgs e)
        {
            //
        }

        private void pbHuy_Click(object sender, EventArgs e)
        {
            yes = false;

            this.Close();
        }

        private void pbHuy_MouseEnter(object sender, EventArgs e)
        {
            pbHuy.Image = Image.FromFile(@"Resources\ChucNang\icon_cancel_selected.png");
        }

        private void pbHuy_MouseLeave(object sender, EventArgs e)
        {
            pbHuy.Image = Image.FromFile(@"Resources\ChucNang\icon_cancel.png");
        }

        private void pbHoanTat_Click(object sender, EventArgs e)
        {
            yes = true;

            this.Close();
        }

        private void pbHoanTat_MouseEnter(object sender, EventArgs e)
        {
            pbHoanTat.Image = Image.FromFile(@"Resources\ChucNang\icon_ok_selected.png");
        }

        private void pbHoanTat_MouseLeave(object sender, EventArgs e)
        {
            pbHoanTat.Image = Image.FromFile(@"Resources\ChucNang\icon_ok.png");
        }
    }
}
