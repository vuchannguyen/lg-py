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
        UcUser ucQuanLyUser;
        UserControl uc;
        #endregion

        public FormMain()
        {
            InitializeComponent();

            this.SetStyle(ControlStyles.DoubleBuffer | ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint, true);
            this.UpdateStyles();
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
                pbMuaBan.Image = Image.FromFile(ConstantResource.GIAODICH_ICON_GIAODICH);
                pbKhoHang.Image = Image.FromFile(ConstantResource.KHOHANG_ICON_KHOHANG);
                pbThuChi.Image = Image.FromFile(ConstantResource.THUCHI_ICON_THUCHI);
                pbThanhToan.Image = Image.FromFile(ConstantResource.GIAODICH_ICON_THANHTOAN);

                pnAbout.BackgroundImage = Image.FromFile(ConstantResource.MAIN_BOTTOM_HORIZONLINE);
            }
            catch
            {
                MessageBox.Show("ERROR!", "Kiểm tra thư mục Resource!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Dispose();
            }
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

            pbThanhToan_Click(sender, e);
        }

        private void Refresh()
        {
            lbUser.ForeColor = Constant.COLOR_NORMAL;
            lbKhachHang.ForeColor = Constant.COLOR_NORMAL;
            lbSanPham.ForeColor = Constant.COLOR_NORMAL;
            lbMuaBan.ForeColor = Constant.COLOR_NORMAL;
            lbThanhToan.ForeColor = Constant.COLOR_NORMAL;
            lbKhoHang.ForeColor = Constant.COLOR_NORMAL;
            lbThuChi.ForeColor = Constant.COLOR_NORMAL;

            pbUser.Image = Image.FromFile(ConstantResource.USER_ICON_USER);
            pbKhachHang.Image = Image.FromFile(ConstantResource.KHACHHANG_ICON_KHACHHANG);
            pbSanPham.Image = Image.FromFile(ConstantResource.SANPHAM_ICON_SANPHAM);
            pbMuaBan.Image = Image.FromFile(ConstantResource.GIAODICH_ICON_GIAODICH);
            pbThanhToan.Image = Image.FromFile(ConstantResource.GIAODICH_ICON_THANHTOAN);
            pbKhoHang.Image = Image.FromFile(ConstantResource.KHOHANG_ICON_KHOHANG);
            pbThuChi.Image = Image.FromFile(ConstantResource.THUCHI_ICON_THUCHI);
        }

        #region Main Button
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

        private void pbUser_Click(object sender, EventArgs e)
        {
            Refresh();

            CommonFunc.NewControl(pnBody.Controls, ref uc, new UcUser());
        }

        private void pbUser_MouseEnter(object sender, EventArgs e)
        {
            pbUser.Image = Image.FromFile(ConstantResource.USER_ICON_USER_MOUSEOVER);
            lbUser.ForeColor = Constant.COLOR_MOUSEOVER;

            ttDetail.SetToolTip(pbUser, Constant.TOOLTIP_USER);
        }

        private void pbUser_MouseLeave(object sender, EventArgs e)
        {
            pbUser.Image = Image.FromFile(ConstantResource.USER_ICON_USER);
            lbUser.ForeColor = Constant.COLOR_NORMAL;
        }

        private void pbKhachHang_Click(object sender, EventArgs e)
        {
            Refresh();
            
            CommonFunc.NewControl(pnBody.Controls, ref uc, new UcKhachHang());
        }

        private void pbKhachHang_MouseEnter(object sender, EventArgs e)
        {
            pbKhachHang.Image = Image.FromFile(ConstantResource.KHACHHANG_ICON_KHACHHANG_MOUSEOVER);
            lbKhachHang.ForeColor = Constant.COLOR_MOUSEOVER;

            ttDetail.SetToolTip(pbKhachHang, Constant.TOOLTIP_KHACHHANG);
        }

        private void pbKhachHang_MouseLeave(object sender, EventArgs e)
        {
            pbKhachHang.Image = Image.FromFile(ConstantResource.KHACHHANG_ICON_KHACHHANG);
            lbKhachHang.ForeColor = Constant.COLOR_NORMAL;
        }

        private void pbSanPham_Click(object sender, EventArgs e)
        {
            Refresh();

            CommonFunc.NewControl(pnBody.Controls, ref uc, new UcSanPhamIndex());
        }

        private void pbSanPham_MouseEnter(object sender, EventArgs e)
        {
            pbSanPham.Image = Image.FromFile(ConstantResource.SANPHAM_ICON_SANPHAM_MOUSEOVER);
            lbSanPham.ForeColor = Constant.COLOR_MOUSEOVER;

            ttDetail.SetToolTip(pbSanPham, Constant.TOOLTIP_SANPHAM);
        }

        private void pbSanPham_MouseLeave(object sender, EventArgs e)
        {
            pbSanPham.Image = Image.FromFile(ConstantResource.SANPHAM_ICON_SANPHAM);
            lbSanPham.ForeColor = Constant.COLOR_NORMAL;
        }

        private void pbMuaBan_Click(object sender, EventArgs e)
        {
            Refresh();

            CommonFunc.NewControl(pnBody.Controls, ref uc, new UcMuaBanIndex());
        }

        private void pbMuaBan_MouseEnter(object sender, EventArgs e)
        {
            pbMuaBan.Image = Image.FromFile(ConstantResource.GIAODICH_ICON_GIAODICH_MOUSEOVER);
            lbMuaBan.ForeColor = Constant.COLOR_MOUSEOVER;

            ttDetail.SetToolTip(pbMuaBan, Constant.TOOLTIP_MUABAN);
        }

        private void pbMuaBan_MouseLeave(object sender, EventArgs e)
        {
            pbMuaBan.Image = Image.FromFile(ConstantResource.GIAODICH_ICON_GIAODICH);
            lbMuaBan.ForeColor = Constant.COLOR_NORMAL;
        }

        private void pbKhoHang_Click(object sender, EventArgs e)
        {
            Refresh();

            CommonFunc.NewControl(pnBody.Controls, ref uc, new UcKhoHang());
        }

        private void pbKhoHang_MouseEnter(object sender, EventArgs e)
        {
            pbKhoHang.Image = Image.FromFile(ConstantResource.KHOHANG_ICON_KHOHANG_MOUSEOVER);
            lbKhoHang.ForeColor = Constant.COLOR_MOUSEOVER;

            ttDetail.SetToolTip(pbKhoHang, Constant.TOOLTIP_KHOHANG);
        }

        private void pbKhoHang_MouseLeave(object sender, EventArgs e)
        {
            pbKhoHang.Image = Image.FromFile(ConstantResource.KHOHANG_ICON_KHOHANG);
            lbKhoHang.ForeColor = Constant.COLOR_NORMAL;
        }

        private void pbThuChi_Click(object sender, EventArgs e)
        {
            Refresh();

            CommonFunc.NewControl(pnBody.Controls, ref uc, new UcThuChiIndex());
        }

        private void pbThuChi_MouseEnter(object sender, EventArgs e)
        {
            pbThuChi.Image = Image.FromFile(ConstantResource.THUCHI_ICON_THUCHI_MOUSEOVER);
            lbThuChi.ForeColor = Constant.COLOR_MOUSEOVER;

            ttDetail.SetToolTip(pbThuChi, Constant.TOOLTIP_THUCHI);
        }

        private void pbThuChi_MouseLeave(object sender, EventArgs e)
        {
            pbThuChi.Image = Image.FromFile(ConstantResource.THUCHI_ICON_THUCHI);
            lbThuChi.ForeColor = Constant.COLOR_NORMAL;
        }

        private void pbThanhToan_Click(object sender, EventArgs e)
        {
            Refresh();

            CommonFunc.NewControl(pnBody.Controls, ref uc, new QuanLyKinhDoanh.GiaoDich.UcThanhToan());
        }

        private void pbThanhToan_MouseEnter(object sender, EventArgs e)
        {
            pbThanhToan.Image = Image.FromFile(ConstantResource.GIAODICH_ICON_THANHTOAN_MOUSEOVER);
            lbThanhToan.ForeColor = Constant.COLOR_MOUSEOVER;

            ttDetail.SetToolTip(pbThanhToan, Constant.TOOLTIP_THANHTOAN);
        }

        private void pbThanhToan_MouseLeave(object sender, EventArgs e)
        {
            pbThanhToan.Image = Image.FromFile(ConstantResource.GIAODICH_ICON_THANHTOAN);
            lbThanhToan.ForeColor = Constant.COLOR_NORMAL;
        }
        #endregion

        private float oldWidth = 1024f;
        private float oldHeght = 768f;

        private void FormMain_SizeChanged(object sender, EventArgs e)
        {
            //float ratioWidth = this.Width / oldWidth;
            //float ratioHeight = this.Height / oldHeght;

            ////oldWidth = (float)this.Width;
            ////oldHeght = (float)this.Height;

            ////if (this.Width > 1024)
            ////SizeF size = new SizeF(ratioWidth, ratioHeight);

            //for (int i = 0; i < this.Controls.Count; i++)
            //{
            //    this.Controls[i].Scale((float)ratioWidth, (float)ratioHeight);
            //}
        }

    }
}
