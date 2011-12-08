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
    public partial class Form_Notice : Form
    {
        private bool yes;

        public bool Yes
        {
            get { return yes; }
            set { yes = value; }
        }

        public Form_Notice()
        {
            InitializeComponent();
        }

        public Form_Notice(string sNotice2, bool bOk_Cancel)
        {
            InitializeComponent();

            LoadPic();
            SubFunction.SetError(lbNotice2, lbNotice2.Location.Y, pnNotice.Size, sNotice2);
            lbNotice1.Text = "";

            if (!bOk_Cancel)
            {
                pbHuy.Visible = false;
            }

            this.ShowDialog();
        }

        public Form_Notice(string sNotice1, string sNotice2, bool bOk_Cancel)
        {
            InitializeComponent();

            LoadPic();
            SubFunction.SetError(lbNotice1, lbNotice1.Location.Y, pnNotice.Size, sNotice1);
            SubFunction.SetError(lbNotice2, lbNotice2.Location.Y, pnNotice.Size, sNotice2);

            if (!bOk_Cancel)
            {
                pbHuy.Visible = false;
            }

            this.ShowDialog();
        }

        private void LoadPic()
        {
            try
            {
                this.BackgroundImage = Image.FromFile(@"Resources\noticepopup.jpg");

                pbHuy.Image = Image.FromFile(@"Resources\ChucNang\icon_cancel.png");
                pbHoanTat.Image = Image.FromFile(@"Resources\ChucNang\icon_ok.png");
            }
            catch
            {
                this.Dispose();
            }
        }

        private void Form_Notice_Load(object sender, EventArgs e)
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
