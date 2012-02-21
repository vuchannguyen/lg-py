using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CommonFunction;

namespace QuanLyKinhDoanh
{
    public partial class Form_Main : Form
    {
        public Form_Main()
        {
            InitializeComponent();

            this.SetStyle(ControlStyles.DoubleBuffer | ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint, true);
            this.UpdateStyles();
        }

        private void Form_Main_Load(object sender, EventArgs e)
        {
            LoadResource();

            pnMain.Visible = true;
            pnMain.Dock = DockStyle.Fill;

            pnHeaderAndMainMenu.Location = SubFunction.SetWidthCenter(pnMain.Size, pnHeaderAndMainMenu.Size, 30);

            pnHello.Left = pnHeaderAndMainMenu.Left;
            lbAbout.Location = SubFunction.SetWidthCenter(pnMain.Size, lbAbout.Size, lbAbout.Top);
        }

        private void LoadResource()
        {
            try
            {
                //this.BackgroundImage = Image.FromFile(@"Resources\background.jpg");
                pnTopBar.BackgroundImage = Image.FromFile(@"Resources\Main\topbar.png");
                pbMinimize.Image = Image.FromFile(@"Resources\ChucNang\button_minimize.png");
                pbExit.Image = Image.FromFile(@"Resources\ChucNang\button_exit.png");

                //pbHeader.Image = Image.FromFile(@"Resources\Main\brand.png");
                pbHorizonline.Image = Image.FromFile(@"Resources\Main\horizonline.png");

                pbQuanLyUser.Image = Image.FromFile(@"Resources\User\icon_user.png");
                //pbBanHang.Image = Image.FromFile(@"Resources\icon_qlhoso.png");
                //pbMuaHang.Image = Image.FromFile(@"Resources\icon_qlsukien.png");
                //pbThuChi.Image = Image.FromFile(@"Resources\top_system.png");
                //pbTimKiem.Image = Image.FromFile(@"Resources\button_plugins.png");

                pnAbout.BackgroundImage = Image.FromFile(@"Resources\Main\bottom_horizonline.png");
            }
            catch
            {
                MessageBox.Show("ERROR!", "Kiểm tra thư mục Resource!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Dispose();
            }
        }



        #region Image Button
        private void pbQuanLyUser_Click(object sender, EventArgs e)
        {
            this.Visible = false;
        }

        private void pbQuanLyUser_MouseEnter(object sender, EventArgs e)
        {
            pbQuanLyUser.Image = Image.FromFile(@"Resources\User\icon_user_mouseover.png");
        }

        private void pbQuanLyUser_MouseLeave(object sender, EventArgs e)
        {
            pbQuanLyUser.Image = Image.FromFile(@"Resources\User\icon_user.png");
        }
        #endregion
    }
}
