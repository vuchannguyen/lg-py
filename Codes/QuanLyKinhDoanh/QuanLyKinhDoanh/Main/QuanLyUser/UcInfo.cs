using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Library;

namespace QuanLyKinhDoanh.Main.QuanLyUser
{
    public partial class UcInfo : UserControl
    {
        public UcInfo()
        {
            InitializeComponent();
        }

        private void LoadResource()
        {
            try
            {
                pbHuy.Image = Image.FromFile(@"Resources\ChucNang\icon_cancel.png");
                pbHoanTat.Image = Image.FromFile(@"Resources\ChucNang\icon_ok.png");
            }
            catch
            {
                this.Dispose();
                //Form_Notice frm_Notice = new Form_Notice("Kiểm tra thư mục Resource!", false);
            }
        }

        private void UcInfo_Load(object sender, EventArgs e)
        {
            LoadResource();

            //this.Dock = DockStyle.Fill;
            pnInfo.Size = new System.Drawing.Size(580, 440);
            //pnInfo.Location = CommonFunc.SetCenterLocation(this.Size, pnInfo.Size);
            this.BringToFront();
        }

        private void pbHuy_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void pbHuy_MouseEnter(object sender, EventArgs e)
        {

        }

        private void pbHuy_MouseLeave(object sender, EventArgs e)
        {

        }

        private void pbHoanTat_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void pbHoanTat_MouseEnter(object sender, EventArgs e)
        {

        }

        private void pbHoanTat_MouseLeave(object sender, EventArgs e)
        {

        }
    }
}
