using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Library;

namespace QuanLyKinhDoanh
{
    public partial class FormMain : Form
    {
        #region Variables
        UcQuanLyUser ucQuanLyUser;
        UserControl uc;
        #endregion

        public FormMain()
        {
            InitializeComponent();

            this.SetStyle(ControlStyles.DoubleBuffer | ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint, true);
            this.UpdateStyles();
        }

        private void Form_Main_Load(object sender, EventArgs e)
        {
            LoadResource();

            pnMain.BackColor = Color.White;
            pnMain.Visible = true;
            pnMain.Dock = DockStyle.Fill;

            pnHeaderAndMainMenu.Location = CommonFunc.SetWidthCenter(pnMain.Size, pnHeaderAndMainMenu.Size, 30);

            pnHello.Left = pnHeaderAndMainMenu.Left;
            lbAbout.Location = CommonFunc.SetWidthCenter(pnMain.Size, lbAbout.Size, lbAbout.Top);

            pnBody.Width = pnMain.Width;
            pnBody.Height = pnMain.Height - 100 - Constant.BOT_HEIGHT_DEFAULT;

            pnBody.Location = CommonFunc.SetWidthCenter(pnMain.Size, pnBody.Size, Constant.TOP_HEIGHT_DEFAULT);
        }

        private void LoadResource()
        {
            try
            {
                //this.BackgroundImage = Image.FromFile(@"Resources\background.jpg");
                pnTopBar.BackgroundImage = Image.FromFile(ConstantResource.MAIN_TOP_BAR);
                pbMinimize.Image = Image.FromFile(ConstantResource.CHUC_NANG_BUTTON_MINIMIZE);
                pbExit.Image = Image.FromFile(ConstantResource.CHUC_NANG_BUTTON_EXIT);

                //pbHeader.Image = Image.FromFile(@"Resources\Main\brand.png");
                pbHorizonline.Image = Image.FromFile(ConstantResource.MAIN_HORIZONLINE);

                pbUser.Image = Image.FromFile(ConstantResource.USER_ICON_USER);
                pbKhachHang.Image = Image.FromFile(ConstantResource.KHACHHANG_ICON_KHACHHANG);
                pbSanPham.Image = Image.FromFile(ConstantResource.SANPHAM_ICON_SANPHAM);
                pbBan.Image = Image.FromFile(ConstantResource.BAN_ICON_BAN);
                pbMua.Image = Image.FromFile(ConstantResource.MUA_ICON_MUA);
                pbKhoHang.Image = Image.FromFile(ConstantResource.KHOHANG_ICON_KHOHANG);
                pbThuChi.Image = Image.FromFile(ConstantResource.THUCHI_ICON_THUCHI);

                pnAbout.BackgroundImage = Image.FromFile(ConstantResource.MAIN_BOTTOM_HORIZONLINE);
            }
            catch
            {
                MessageBox.Show("ERROR!", "Kiểm tra thư mục Resource!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Dispose();
            }
        }

        #region Image Button
        private void pbUser_Click(object sender, EventArgs e)
        {
            CommonFunc.NewControl(pnBody.Controls, ref uc, new UcQuanLyUser());
        }

        private void pbUser_MouseEnter(object sender, EventArgs e)
        {
            pbUser.Image = Image.FromFile(ConstantResource.USER_ICON_USER_MOUSEOVER);
        }

        private void pbUser_MouseLeave(object sender, EventArgs e)
        {
            pbUser.Image = Image.FromFile(ConstantResource.USER_ICON_USER);
        }
        #endregion

        private void pbMinimize_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void pbMinimize_MouseEnter(object sender, EventArgs e)
        {
            pbMinimize.Image = Image.FromFile(ConstantResource.CHUC_NANG_BUTTON_MINIMIZE_MOUSEOVER);
        }

        private void pbMinimize_MouseLeave(object sender, EventArgs e)
        {
            pbMinimize.Image = Image.FromFile(ConstantResource.CHUC_NANG_BUTTON_MINIMIZE);
        }

        private void pbExit_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void pbExit_MouseEnter(object sender, EventArgs e)
        {
            pbExit.Image = Image.FromFile(ConstantResource.CHUC_NANG_BUTTON_EXIT_MOUSEOVER);
        }

        private void pbExit_MouseLeave(object sender, EventArgs e)
        {
            pbExit.Image = Image.FromFile(ConstantResource.CHUC_NANG_BUTTON_EXIT);
        }

        private void pbKhachHang_Click(object sender, EventArgs e)
        {

        }

        private void pbKhachHang_MouseEnter(object sender, EventArgs e)
        {
            pbKhachHang.Image = Image.FromFile(ConstantResource.KHACHHANG_ICON_KHACHHANG_MOUSEOVER);
        }

        private void pbKhachHang_MouseLeave(object sender, EventArgs e)
        {
            pbKhachHang.Image = Image.FromFile(ConstantResource.KHACHHANG_ICON_KHACHHANG);
        }

        private void pbSanPham_Click(object sender, EventArgs e)
        {

        }

        private void pbSanPham_MouseEnter(object sender, EventArgs e)
        {
            pbSanPham.Image = Image.FromFile(ConstantResource.SANPHAM_ICON_SANPHAM_MOUSEOVER);
        }

        private void pbSanPham_MouseLeave(object sender, EventArgs e)
        {
            pbSanPham.Image = Image.FromFile(ConstantResource.SANPHAM_ICON_SANPHAM);
        }

        private void pbMua_Click(object sender, EventArgs e)
        {
            CommonFunc.NewControl(pnBody.Controls, ref uc, new UcMua());
        }

        private void pbMua_MouseEnter(object sender, EventArgs e)
        {
            pbMua.Image = Image.FromFile(ConstantResource.MUA_ICON_MUA_MOUSEOVER);
        }

        private void pbMua_MouseLeave(object sender, EventArgs e)
        {
            pbMua.Image = Image.FromFile(ConstantResource.MUA_ICON_MUA);
        }

        private void pbBan_Click(object sender, EventArgs e)
        {
            CommonFunc.NewControl(pnBody.Controls, ref uc, new UcBan());
        }

        private void pbBan_MouseEnter(object sender, EventArgs e)
        {
            pbBan.Image = Image.FromFile(ConstantResource.BAN_ICON_BAN_MOUSEOVER);
        }

        private void pbBan_MouseLeave(object sender, EventArgs e)
        {
            pbBan.Image = Image.FromFile(ConstantResource.BAN_ICON_BAN);
        }

        private void pbKhoHang_Click(object sender, EventArgs e)
        {

        }

        private void pbKhoHang_MouseEnter(object sender, EventArgs e)
        {
            pbKhoHang.Image = Image.FromFile(ConstantResource.KHOHANG_ICON_KHOHANG_MOUSEOVER);
        }

        private void pbKhoHang_MouseLeave(object sender, EventArgs e)
        {
            pbKhoHang.Image = Image.FromFile(ConstantResource.KHOHANG_ICON_KHOHANG);
        }

        private void pbThuChi_Click(object sender, EventArgs e)
        {

        }

        private void pbThuChi_MouseEnter(object sender, EventArgs e)
        {
            pbThuChi.Image = Image.FromFile(ConstantResource.THUCHI_ICON_THUCHI_MOUSEOVER);
        }

        private void pbThuChi_MouseLeave(object sender, EventArgs e)
        {
            pbThuChi.Image = Image.FromFile(ConstantResource.THUCHI_ICON_THUCHI);
        }
    }
}
