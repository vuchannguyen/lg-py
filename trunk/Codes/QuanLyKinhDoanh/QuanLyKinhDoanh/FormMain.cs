using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Library;
using DTO;
using BUS;

namespace QuanLyKinhDoanh
{
    public partial class FormMain : Form
    {
        #region Variables
        public static DTO.User user;
        public static bool isPrintUsing;
        public static bool isEditing;

        private bool isMainMenuClickUser;
        private bool isMainMenuClickKhachHang;
        private bool isMainMenuClickSP;
        private bool isMainMenuClickKho;
        private bool isMainMenuClickThuChi;
        private bool isMainMenuClickThanhToan;

        UserControl uc;

        private float oldWidth = 1014f;
        private float oldHeght = 764f;
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

                pbHeader.Image = Image.FromFile(ConstantResource.MAIN_LOGOND);
                pbHorizonline.Image = Image.FromFile(ConstantResource.MAIN_HORIZONLINE);

                pbUser.Image = Image.FromFile(ConstantResource.USER_ICON_USER);
                pbKhachHang.Image = Image.FromFile(ConstantResource.KHACHHANG_ICON_KHACHHANG);
                pbSanPham.Image = Image.FromFile(ConstantResource.SANPHAM_ICON_SANPHAM);               
                pbKhoHang.Image = Image.FromFile(ConstantResource.KHOHANG_ICON_KHOHANG);
                pbThuChi.Image = Image.FromFile(ConstantResource.THUCHI_ICON_THUCHI);
                pbThanhToan.Image = Image.FromFile(ConstantResource.GIAODICH_ICON_THANHTOAN);

                pnAbout.BackgroundImage = Image.FromFile(ConstantResource.MAIN_BOTTOM_HORIZONLINE);
            }
            catch
            {
                MessageBox.Show(Constant.MESSAGE_ERROR_MISSING_RESOURCE, Constant.CAPTION_ERROR, MessageBoxButtons.OK, MessageBoxIcon.Error);

                this.Dispose();
            }
        }

        private void Form_Main_Load(object sender, EventArgs e)
        {
            this.Visible = false;

            LoadResource();

            pnMain.BackColor = Color.White;
            pnMain.Visible = true;
            pnMain.Dock = DockStyle.Fill;

            pnHeaderAndMainMenu.Location = CommonFunc.SetWidthCenter(pnMain.Size, pnHeaderAndMainMenu.Size, 30);

            //pnHello.Left = pnHeaderAndMainMenu.Left;
            lbAbout.Location = CommonFunc.SetWidthCenter(pnMain.Size, lbAbout.Size, lbAbout.Top);

            pnBody.Width = pnMain.Width;
            pnBody.Height = pnMain.Height - 100 - Constant.DEFAULT_BOT_HEIGHT;

            pnBody.Location = CommonFunc.SetWidthCenter(pnMain.Size, pnBody.Size, Constant.DEFAULT_TOP_HEIGHT);

            FormLogin frm = new FormLogin();
            frm.FormClosed += new FormClosedEventHandler(FormLogin_Closed);
            frm.ShowDialog();
        }

        private void FormLogin_Closed(object sender, FormClosedEventArgs e)
        {
            if (user != null)
            {
                this.Visible = true;

                isPrintUsing = false;
                isEditing = false;

                lbNgay.Text = DateTime.Now.ToString(Constant.DEFAULT_DATE_FORMAT);
                lbAccount.Text = user.UserName;

                pbThanhToan_Click(sender, e);
            }
            else
            {
                this.Dispose();
            }
        }

        private void Init()
        {
            lbUser.ForeColor = Constant.COLOR_NORMAL;
            lbKhachHang.ForeColor = Constant.COLOR_NORMAL;
            lbSanPham.ForeColor = Constant.COLOR_NORMAL;
            lbThanhToan.ForeColor = Constant.COLOR_NORMAL;
            lbKhoHang.ForeColor = Constant.COLOR_NORMAL;
            lbThuChi.ForeColor = Constant.COLOR_NORMAL;

            pbUser.Image = Image.FromFile(ConstantResource.USER_ICON_USER);
            pbKhachHang.Image = Image.FromFile(ConstantResource.KHACHHANG_ICON_KHACHHANG);
            pbSanPham.Image = Image.FromFile(ConstantResource.SANPHAM_ICON_SANPHAM);
            pbThanhToan.Image = Image.FromFile(ConstantResource.GIAODICH_ICON_THANHTOAN);
            pbKhoHang.Image = Image.FromFile(ConstantResource.KHOHANG_ICON_KHOHANG);
            pbThuChi.Image = Image.FromFile(ConstantResource.THUCHI_ICON_THUCHI);

            pbUser.Enabled = true;
            pbKhachHang.Enabled = true;
            pbSanPham.Enabled = true;
            pbThanhToan.Enabled = true;
            pbKhoHang.Enabled = true;
            pbThuChi.Enabled = true;

            isMainMenuClickUser = false;
            isMainMenuClickKhachHang = false;
            isMainMenuClickSP = false;
            isMainMenuClickKho = false;
            isMainMenuClickThuChi = false;
            isMainMenuClickThanhToan = false;
        }

        private bool WarningEditingDialog()
        {
            if (isEditing)
            {
                return MessageBox.Show(Constant.MESSAGE_EXIT, Constant.CAPTION_WARNING, MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == System.Windows.Forms.DialogResult.Yes;
            }

            return true;
        }



        #region Main Button
        private void pbMinimize_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void pbUser_Click(object sender, EventArgs e)
        {
            if (WarningEditingDialog())
            {
                ScaleNormalControls();

                CommonFunc.NewControl(pnBody.Controls, ref uc, new UcUser());

                Init();
                isMainMenuClickUser = true;

                pbUser.Image = Image.FromFile(ConstantResource.USER_ICON_USER_MOUSEOVER);
                lbUser.ForeColor = Constant.COLOR_IN_USE;

                ScaleMaximizedControls();
            }
        }

        private void pbUser_MouseEnter(object sender, EventArgs e)
        {
            pbUser.Image = Image.FromFile(ConstantResource.USER_ICON_USER_MOUSEOVER);
            lbUser.ForeColor = Constant.COLOR_MOUSEOVER;

            ttDetail.SetToolTip(pbUser, Constant.TOOLTIP_USER);
        }

        private void pbUser_MouseLeave(object sender, EventArgs e)
        {
            if (!isMainMenuClickUser)
            {
                pbUser.Image = Image.FromFile(ConstantResource.USER_ICON_USER);
                lbUser.ForeColor = Constant.COLOR_NORMAL;
            }
            else
            {
                pbUser.Image = Image.FromFile(ConstantResource.USER_ICON_USER_MOUSEOVER);
                lbUser.ForeColor = Constant.COLOR_IN_USE;
            }
        }

        private void pbKhachHang_Click(object sender, EventArgs e)
        {
            if (WarningEditingDialog())
            {
                ScaleNormalControls();

                CommonFunc.NewControl(pnBody.Controls, ref uc, new UcKhachHang());

                Init();
                isMainMenuClickKhachHang = true;

                pbKhachHang.Image = Image.FromFile(ConstantResource.KHACHHANG_ICON_KHACHHANG_MOUSEOVER);
                lbKhachHang.ForeColor = Constant.COLOR_IN_USE;

                ScaleMaximizedControls();
            }
        }

        private void pbKhachHang_MouseEnter(object sender, EventArgs e)
        {
            pbKhachHang.Image = Image.FromFile(ConstantResource.KHACHHANG_ICON_KHACHHANG_MOUSEOVER);
            lbKhachHang.ForeColor = Constant.COLOR_MOUSEOVER;

            ttDetail.SetToolTip(pbKhachHang, Constant.TOOLTIP_KHACHHANG);
        }

        private void pbKhachHang_MouseLeave(object sender, EventArgs e)
        {
            if (!isMainMenuClickKhachHang)
            {
                pbKhachHang.Image = Image.FromFile(ConstantResource.KHACHHANG_ICON_KHACHHANG);
                lbKhachHang.ForeColor = Constant.COLOR_NORMAL;
            }
            else
            {
                pbKhachHang.Image = Image.FromFile(ConstantResource.KHACHHANG_ICON_KHACHHANG_MOUSEOVER);
                lbKhachHang.ForeColor = Constant.COLOR_IN_USE;
            }
        }

        private void pbSanPham_Click(object sender, EventArgs e)
        {
            if (WarningEditingDialog())
            {
                ScaleNormalControls();

                CommonFunc.NewControl(pnBody.Controls, ref uc, new UcSanPhamIndex());

                Init();
                isMainMenuClickSP = true;

                pbSanPham.Image = Image.FromFile(ConstantResource.SANPHAM_ICON_SANPHAM_MOUSEOVER);
                lbSanPham.ForeColor = Constant.COLOR_IN_USE;

                ScaleMaximizedControls();
            }
        }

        private void pbSanPham_MouseEnter(object sender, EventArgs e)
        {
            pbSanPham.Image = Image.FromFile(ConstantResource.SANPHAM_ICON_SANPHAM_MOUSEOVER);
            lbSanPham.ForeColor = Constant.COLOR_MOUSEOVER;

            ttDetail.SetToolTip(pbSanPham, Constant.TOOLTIP_SANPHAM);
        }

        private void pbSanPham_MouseLeave(object sender, EventArgs e)
        {
            if (!isMainMenuClickSP)
            {
                pbSanPham.Image = Image.FromFile(ConstantResource.SANPHAM_ICON_SANPHAM);
                lbSanPham.ForeColor = Constant.COLOR_NORMAL;
            }
            else
            {
                pbSanPham.Image = Image.FromFile(ConstantResource.SANPHAM_ICON_SANPHAM_MOUSEOVER);
                lbSanPham.ForeColor = Constant.COLOR_IN_USE;
            }
        }

        private void pbKhoHang_Click(object sender, EventArgs e)
        {
            if (WarningEditingDialog())
            {
                ScaleNormalControls();

                CommonFunc.NewControl(pnBody.Controls, ref uc, new UcKhoHangIndex());

                Init();
                isMainMenuClickKho = true;

                pbKhoHang.Image = Image.FromFile(ConstantResource.KHOHANG_ICON_KHOHANG_MOUSEOVER);
                lbKhoHang.ForeColor = Constant.COLOR_IN_USE;

                ScaleMaximizedControls();
            }
        }

        private void pbKhoHang_MouseEnter(object sender, EventArgs e)
        {
            pbKhoHang.Image = Image.FromFile(ConstantResource.KHOHANG_ICON_KHOHANG_MOUSEOVER);
            lbKhoHang.ForeColor = Constant.COLOR_MOUSEOVER;

            ttDetail.SetToolTip(pbKhoHang, Constant.TOOLTIP_KHOHANG);
        }

        private void pbKhoHang_MouseLeave(object sender, EventArgs e)
        {
            if (!isMainMenuClickKho)
            {
                pbKhoHang.Image = Image.FromFile(ConstantResource.KHOHANG_ICON_KHOHANG);
                lbKhoHang.ForeColor = Constant.COLOR_NORMAL;
            }
            else
            {
                pbKhoHang.Image = Image.FromFile(ConstantResource.KHOHANG_ICON_KHOHANG_MOUSEOVER);
                lbKhoHang.ForeColor = Constant.COLOR_IN_USE;
            }
        }

        private void pbThuChi_Click(object sender, EventArgs e)
        {
            if (WarningEditingDialog())
            {
                ScaleNormalControls();

                CommonFunc.NewControl(pnBody.Controls, ref uc, new UcThuChiIndex());

                Init();
                isMainMenuClickThuChi = true;

                pbThuChi.Image = Image.FromFile(ConstantResource.THUCHI_ICON_THUCHI_MOUSEOVER);
                lbThuChi.ForeColor = Constant.COLOR_IN_USE;

                ScaleMaximizedControls();
            }
        }

        private void pbThuChi_MouseEnter(object sender, EventArgs e)
        {
            pbThuChi.Image = Image.FromFile(ConstantResource.THUCHI_ICON_THUCHI_MOUSEOVER);
            lbThuChi.ForeColor = Constant.COLOR_MOUSEOVER;

            ttDetail.SetToolTip(pbThuChi, Constant.TOOLTIP_THUCHI);
        }

        private void pbThuChi_MouseLeave(object sender, EventArgs e)
        {
            if (!isMainMenuClickThuChi)
            {
                pbThuChi.Image = Image.FromFile(ConstantResource.THUCHI_ICON_THUCHI);
                lbThuChi.ForeColor = Constant.COLOR_NORMAL;
            }
            else
            {
                pbThuChi.Image = Image.FromFile(ConstantResource.THUCHI_ICON_THUCHI_MOUSEOVER);
                lbThuChi.ForeColor = Constant.COLOR_IN_USE;
            }
        }

        private void pbThanhToan_Click(object sender, EventArgs e)
        {
            if (WarningEditingDialog())
            {
                ScaleNormalControls();

                Init();
                isMainMenuClickThanhToan = true;

                pbThanhToan.Image = Image.FromFile(ConstantResource.GIAODICH_ICON_THANHTOAN_MOUSEOVER);
                lbThanhToan.ForeColor = Constant.COLOR_IN_USE;

                CommonFunc.NewControl(pnBody.Controls, ref uc, new QuanLyKinhDoanh.GiaoDich.UcThanhToan());

                ScaleMaximizedControls();
            }
        }

        private void pbThanhToan_MouseEnter(object sender, EventArgs e)
        {
            pbThanhToan.Image = Image.FromFile(ConstantResource.GIAODICH_ICON_THANHTOAN_MOUSEOVER);
            lbThanhToan.ForeColor = Constant.COLOR_MOUSEOVER;

            ttDetail.SetToolTip(pbThanhToan, Constant.TOOLTIP_THANHTOAN);
        }

        private void pbThanhToan_MouseLeave(object sender, EventArgs e)
        {
            if (!isMainMenuClickThanhToan)
            {
                pbThanhToan.Image = Image.FromFile(ConstantResource.GIAODICH_ICON_THANHTOAN);
                lbThanhToan.ForeColor = Constant.COLOR_NORMAL;
            }
            else
            {
                pbThanhToan.Image = Image.FromFile(ConstantResource.GIAODICH_ICON_THANHTOAN_MOUSEOVER);
                lbThanhToan.ForeColor = Constant.COLOR_IN_USE;
            }
        }
        #endregion



        private void FormMain_SizeChanged(object sender, EventArgs e)
        {
            if (this.Width >= 1014 || this.Height >= 764)
            {
                float ratioWidth = this.Width / oldWidth;
                float ratioHeight = this.Height / oldHeght;

                oldWidth = this.Width;
                oldHeght = this.Height;

                for (int i = 0; i < this.Controls.Count; i++)
                {
                    this.Controls[i].Scale(new SizeF(ratioWidth, ratioHeight));
                }
            }
        }

        private void ScaleMaximizedControls()
        {
            if (this.WindowState == FormWindowState.Maximized)
            {
                float ratioWidth = this.Width / 1014f;
                float ratioHeight = this.Height / 764f;

                for (int i = 0; i < this.Controls.Count; i++)
                {
                    this.Controls[i].Scale(new SizeF(ratioWidth, ratioHeight));
                }
            }
        }

        private void ScaleNormalControls()
        {
            if (this.WindowState == FormWindowState.Maximized)
            {
                if (this.Width >= 1014 || this.Height >= 764)
                {
                    float ratioWidth = 1014f / this.Width;
                    float ratioHeight = 764f / this.Height;

                    for (int i = 0; i < this.Controls.Count; i++)
                    {
                        this.Controls[i].Scale(new SizeF(ratioWidth, ratioHeight));
                    }
                }
            }
        }

        private void Exit()
        {
            this.Visible = false;
            user = null;

            FormLogin frm = new FormLogin();
            frm.FormClosed += new FormClosedEventHandler(FormLogin_Closed);
            frm.ShowDialog();
        }

        private void lbExit_MouseEnter(object sender, EventArgs e)
        {
            lbExit.ForeColor = Color.Red;
        }

        private void lbExit_MouseLeave(object sender, EventArgs e)
        {
            lbExit.ForeColor = Color.White;
        }

        private void lbExit_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(Constant.MESSAGE_EXIT, Constant.CAPTION_WARNING, MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                Exit();
            }
        }

        private void lbHuyTaiKhoan_MouseEnter(object sender, EventArgs e)
        {
            lbHuyTaiKhoan.ForeColor = Color.Red;
        }

        private void lbHuyTaiKhoan_MouseLeave(object sender, EventArgs e)
        {
            lbHuyTaiKhoan.ForeColor = Color.White;
        }

        private void lbHuyTaiKhoan_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(Constant.MESSAGE_CONFIRM_SELF_DESTRUCTION, Constant.CAPTION_WARNING, MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                if (!UserBus.Delete(user, user))
                {
                    MessageBox.Show(Constant.MESSAGE_ERROR, Constant.CAPTION_ERROR,
                        MessageBoxButtons.OK, MessageBoxIcon.Error);

                    return;
                }

                Exit();
            }
        }
    }
}
