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

namespace Weedon
{
    public partial class FormMain : Form
    {
        #region Variables
        public static DTO.User user;
        public static bool isPrintUsing;
        public static bool isEditing;
        public static bool isRestored;

        private bool isMainMenuClickUser;
        private bool isMainMenuClickKhachHang;
        private bool isMainMenuClickSP;
        private bool isMainMenuClickNL;
        private bool isMainMenuClickNKNL;
        private bool isMainMenuClickDoanhThu;
        private bool isMainMenuClickSetting;
        private bool isMainMenuClickTool;

        UserControl uc;

        private float oldWidth = 1014f;
        private float oldHeght = 764f;

        public static bool isConnected;
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

                pbHeader.Image = Image.FromFile(ConstantResource.MAIN_LOGO);
                pbHorizonline.Image = Image.FromFile(ConstantResource.MAIN_HORIZONLINE);

                pbUser.Image = Image.FromFile(ConstantResource.USER_ICON_USER);
                pbKhachHang.Image = Image.FromFile(ConstantResource.KHACHHANG_ICON_KHACHHANG);
                pbSanPham.Image = Image.FromFile(ConstantResource.SANPHAM_ICON_SANPHAM);
                pbNguyenLieu.Image = Image.FromFile(ConstantResource.NGUYENLIEU_ICON_NGUYENLIEU);
                pbNhatKyNguyenLieu.Image = Image.FromFile(ConstantResource.NKNL_ICON_NKNL);
                pbDoanhThu.Image = Image.FromFile(ConstantResource.DOANHTHU_ICON_DOANHTHU);
                pbSetting.Image = Image.FromFile(ConstantResource.SETTING_ICON_SETTING);
                pbTool.Image = Image.FromFile(ConstantResource.TOOL_ICON_TOOL);

                pnBottom.BackgroundImage = Image.FromFile(ConstantResource.MAIN_BOTTOM_HORIZONLINE);

                pbStartup.BackgroundImage = Image.FromFile(ConstantResource.MAIN_STARTUP);
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
            pnGeneralInfo.Location = CommonFunc.SetWidthCenter(pnMain.Size, pnGeneralInfo.Size, pnGeneralInfo.Top);

            pnBody.Width = pnMain.Width;
            pnBody.Height = pnMain.Height - 100 - Constant.DEFAULT_BOT_HEIGHT;

            pnBody.Location = CommonFunc.SetWidthCenter(pnMain.Size, pnBody.Size, Constant.DEFAULT_TOP_HEIGHT);

            pbStartup.Size = new System.Drawing.Size(1000, 413);
            pbStartup.Location = CommonFunc.SetCenterLocation(pnBody.Size, pbStartup.Size);

            FormConnection frmConnection = new FormConnection();
            frmConnection.FormClosed += new FormClosedEventHandler(FormConnection_FormClosed);
            frmConnection.ShowDialog();

            if (isConnected)
            {
                FormLogin frm = new FormLogin();
                frm.FormClosed += new FormClosedEventHandler(FormLogin_Closed);
                frm.ShowDialog();
            }
        }

        private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.Visible)
            {
                if (isRestored)
                {
                    e.Cancel = true;

                    Exit();
                }
                else
                {
                    if (MessageBox.Show(Constant.MESSAGE_EXIT_APP, Constant.CAPTION_WARNING, MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == System.Windows.Forms.DialogResult.No)
                    {
                        e.Cancel = true;
                    }
                }
            }
        }

        private void FormConnection_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (!isConnected)
            {
                this.Dispose();
            }
        }

        private void FormLogin_Closed(object sender, FormClosedEventArgs e)
        {
            if (user == null)
            {
                this.Dispose();

                return;
            }

            this.Visible = true;

            isPrintUsing = false;
            isEditing = false;

            lbNgay.Text = string.Format(Constant.DEFAULT_WELCOME_DATE_TIME_FORMAT,
                GetDayOfWeek(), DateTime.Now.ToString(Constant.DEFAULT_DATE_FORMAT));
            lbAccount.Text = user.UserName;
        }

        private void Init()
        {
            lbUser.ForeColor = Constant.COLOR_NORMAL;
            lbKhachHang.ForeColor = Constant.COLOR_NORMAL;
            lbSanPham.ForeColor = Constant.COLOR_NORMAL;
            lbNguyenLieu.ForeColor = Constant.COLOR_NORMAL;
            lbNhatKyNguyenLieu.ForeColor = Constant.COLOR_NORMAL;
            lbDoanhThu.ForeColor = Constant.COLOR_NORMAL;
            lbSetting.ForeColor = Constant.COLOR_NORMAL;
            lbTool.ForeColor = Constant.COLOR_NORMAL;

            pbUser.Image = Image.FromFile(ConstantResource.USER_ICON_USER);
            pbKhachHang.Image = Image.FromFile(ConstantResource.KHACHHANG_ICON_KHACHHANG);
            pbSanPham.Image = Image.FromFile(ConstantResource.SANPHAM_ICON_SANPHAM);
            pbNguyenLieu.Image = Image.FromFile(ConstantResource.NGUYENLIEU_ICON_NGUYENLIEU);
            pbNhatKyNguyenLieu.Image = Image.FromFile(ConstantResource.NKNL_ICON_NKNL);
            pbDoanhThu.Image = Image.FromFile(ConstantResource.DOANHTHU_ICON_DOANHTHU);
            pbSetting.Image = Image.FromFile(ConstantResource.SETTING_ICON_SETTING);
            pbTool.Image = Image.FromFile(ConstantResource.TOOL_ICON_TOOL);

            pbUser.Enabled = true;
            pbKhachHang.Enabled = true;
            pbSanPham.Enabled = true;
            pbNguyenLieu.Enabled = true;
            pbNhatKyNguyenLieu.Enabled = true;
            pbDoanhThu.Enabled = true;
            pbSetting.Enabled = true;
            pbTool.Enabled = true;

            isMainMenuClickUser = false;
            isMainMenuClickKhachHang = false;
            isMainMenuClickSP = false;
            isMainMenuClickNL = false;
            isMainMenuClickNKNL = false;
            isMainMenuClickDoanhThu = false;
            isMainMenuClickSetting = false;
            isMainMenuClickTool = false;

            InitPermission();
        }

        private void InitPermission()
        {
            if (FormMain.user.IdGroup != Constant.ID_GROUP_ADMIN)
            {
                pbTool.Enabled = false;
            }
        }

        private string GetDayOfWeek()
        {
            string day = string.Empty;

            switch (DateTime.Now.DayOfWeek)
            {
                case DayOfWeek.Monday:
                    day = "thứ Hai";
                    break;

                case DayOfWeek.Tuesday:
                    day = "thứ Ba";
                    break;

                case DayOfWeek.Wednesday:
                    day = "thứ Tư";
                    break;

                case DayOfWeek.Thursday:
                    day = "thứ Năm";
                    break;

                case DayOfWeek.Friday:
                    day = "thứ Sáu";
                    break;

                case DayOfWeek.Saturday:
                    day = "thứ Bảy";
                    break;

                case DayOfWeek.Sunday:
                    day = "Chủ Nhật";
                    break;
            }

            return day;
        }

        private bool WarningEditingDialog()
        {
            if (isEditing)
            {
                return MessageBox.Show(Constant.MESSAGE_EXIT, Constant.CAPTION_WARNING, MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == System.Windows.Forms.DialogResult.Yes;
            }

            return true;
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

            pnBody.Controls.Remove(uc);
            Init();
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

                CommonFunc.NewControl(pnBody.Controls, ref uc, new UcKhachHangIndex());

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

        private void pbNguyenLieu_Click(object sender, EventArgs e)
        {
            if (WarningEditingDialog())
            {
                ScaleNormalControls();

                CommonFunc.NewControl(pnBody.Controls, ref uc, new UcNguyenLieuIndex());

                Init();
                isMainMenuClickNL = true;

                pbNguyenLieu.Image = Image.FromFile(ConstantResource.NGUYENLIEU_ICON_NGUYENLIEU_MOUSEOVER);
                lbNguyenLieu.ForeColor = Constant.COLOR_IN_USE;

                ScaleMaximizedControls();
            }
        }

        private void pbNguyenLieu_MouseEnter(object sender, EventArgs e)
        {
            pbNguyenLieu.Image = Image.FromFile(ConstantResource.NGUYENLIEU_ICON_NGUYENLIEU_MOUSEOVER);
            lbNguyenLieu.ForeColor = Constant.COLOR_MOUSEOVER;

            ttDetail.SetToolTip(pbNguyenLieu, Constant.TOOLTIP_NGUYENLIEU);
        }

        private void pbNguyenLieu_MouseLeave(object sender, EventArgs e)
        {
            if (!isMainMenuClickNL)
            {
                pbNguyenLieu.Image = Image.FromFile(ConstantResource.NGUYENLIEU_ICON_NGUYENLIEU);
                lbNguyenLieu.ForeColor = Constant.COLOR_NORMAL;
            }
            else
            {
                pbNguyenLieu.Image = Image.FromFile(ConstantResource.NGUYENLIEU_ICON_NGUYENLIEU_MOUSEOVER);
                lbNguyenLieu.ForeColor = Constant.COLOR_IN_USE;
            }
        }

        private void pbNhatKyNguyenLieu_Click(object sender, EventArgs e)
        {
            if (WarningEditingDialog())
            {
                ScaleNormalControls();

                CommonFunc.NewControl(pnBody.Controls, ref uc, new UcNhatKyNguyenLieuIndex());

                Init();
                isMainMenuClickNKNL = true;

                pbNhatKyNguyenLieu.Image = Image.FromFile(ConstantResource.NKNL_ICON_NKNL_MOUSEOVER);
                lbNhatKyNguyenLieu.ForeColor = Constant.COLOR_IN_USE;

                ScaleMaximizedControls();
            }
        }

        private void pbNhatKyNguyenLieu_MouseEnter(object sender, EventArgs e)
        {
            pbNhatKyNguyenLieu.Image = Image.FromFile(ConstantResource.NKNL_ICON_NKNL_MOUSEOVER);
            lbNhatKyNguyenLieu.ForeColor = Constant.COLOR_MOUSEOVER;

            ttDetail.SetToolTip(pbNhatKyNguyenLieu, Constant.TOOLTIP_NKNL);
        }

        private void pbNhatKyNguyenLieu_MouseLeave(object sender, EventArgs e)
        {
            if (!isMainMenuClickNKNL)
            {
                pbNhatKyNguyenLieu.Image = Image.FromFile(ConstantResource.NKNL_ICON_NKNL);
                lbNhatKyNguyenLieu.ForeColor = Constant.COLOR_NORMAL;
            }
            else
            {
                pbNhatKyNguyenLieu.Image = Image.FromFile(ConstantResource.NKNL_ICON_NKNL_MOUSEOVER);
                lbNhatKyNguyenLieu.ForeColor = Constant.COLOR_IN_USE;
            }
        }

        private void pbTool_Click(object sender, EventArgs e)
        {
            if (WarningEditingDialog())
            {
                ScaleNormalControls();

                Init();
                isMainMenuClickTool = true;

                pbTool.Image = Image.FromFile(ConstantResource.TOOL_ICON_TOOL_MOUSEOVER);
                lbTool.ForeColor = Constant.COLOR_IN_USE;

                CommonFunc.NewControl(pnBody.Controls, ref uc, new UcToolIndex());

                ScaleMaximizedControls();
            }
        }

        private void pbTool_MouseEnter(object sender, EventArgs e)
        {
            pbTool.Image = Image.FromFile(ConstantResource.TOOL_ICON_TOOL_MOUSEOVER);
            lbTool.ForeColor = Constant.COLOR_MOUSEOVER;

            ttDetail.SetToolTip(pbTool, Constant.TOOLTIP_TOOL);
        }

        private void pbTool_MouseLeave(object sender, EventArgs e)
        {
            if (!isMainMenuClickTool)
            {
                pbTool.Image = Image.FromFile(ConstantResource.TOOL_ICON_TOOL);
                lbTool.ForeColor = Constant.COLOR_NORMAL;
            }
            else
            {
                pbTool.Image = Image.FromFile(ConstantResource.TOOL_ICON_TOOL_MOUSEOVER);
                lbTool.ForeColor = Constant.COLOR_IN_USE;
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

        private void lbEdit_Click(object sender, EventArgs e)
        {
            if (WarningEditingDialog())
            {
                ScaleNormalControls();

                CommonFunc.NewControl(pnBody.Controls, ref uc, new User.UcInfo(user));

                Init();

                ScaleMaximizedControls();
            }
        }

        private void lbEdit_MouseEnter(object sender, EventArgs e)
        {
            lbEdit.ForeColor = Color.Red;
        }

        private void lbEdit_MouseLeave(object sender, EventArgs e)
        {
            lbEdit.ForeColor = Color.White;
        }

        private void lbAboutSoftware_Click(object sender, EventArgs e)
        {
            new AboutSoftware().ShowDialog();
        }

        private void lbAboutCD_Click(object sender, EventArgs e)
        {
            new AboutCD().ShowDialog();
        }

        private void lbAboutSoftware_MouseEnter(object sender, EventArgs e)
        {
            lbAboutSoftware.ForeColor = Color.Orange;
        }

        private void lbAboutSoftware_MouseLeave(object sender, EventArgs e)
        {
            lbAboutSoftware.ForeColor = Color.Gray;
        }

        private void lbAboutCD_MouseEnter(object sender, EventArgs e)
        {
            lbAboutCD.ForeColor = Color.Orange;
        }

        private void lbAboutCD_MouseLeave(object sender, EventArgs e)
        {
            lbAboutCD.ForeColor = Color.Gray;
        }

        private void pbBanHang_Click(object sender, EventArgs e)
        {
            if (WarningEditingDialog())
            {
                ScaleNormalControls();

                Init();
                isMainMenuClickDoanhThu = true;

                pbDoanhThu.Image = Image.FromFile(ConstantResource.DOANHTHU_ICON_DOANHTHU_MOUSEOVER);
                lbDoanhThu.ForeColor = Constant.COLOR_IN_USE;

                CommonFunc.NewControl(pnBody.Controls, ref uc, new Weedon.UcDoanhThuIndex());

                ScaleMaximizedControls();
            }
        }

        private void pbBanHang_MouseEnter(object sender, EventArgs e)
        {
            pbDoanhThu.Image = Image.FromFile(ConstantResource.DOANHTHU_ICON_DOANHTHU_MOUSEOVER);
            lbDoanhThu.ForeColor = Constant.COLOR_MOUSEOVER;

            ttDetail.SetToolTip(pbDoanhThu, Constant.TOOLTIP_DOANHTHU);
        }

        private void pbBanHang_MouseLeave(object sender, EventArgs e)
        {
            if (!isMainMenuClickDoanhThu)
            {
                pbDoanhThu.Image = Image.FromFile(ConstantResource.DOANHTHU_ICON_DOANHTHU);
                lbDoanhThu.ForeColor = Constant.COLOR_NORMAL;
            }
            else
            {
                pbDoanhThu.Image = Image.FromFile(ConstantResource.DOANHTHU_ICON_DOANHTHU_MOUSEOVER);
                lbDoanhThu.ForeColor = Constant.COLOR_IN_USE;
            }
        }

        private void pbSetting_Click(object sender, EventArgs e)
        {
            if (WarningEditingDialog())
            {
                ScaleNormalControls();

                Init();
                isMainMenuClickSetting = true;

                pbSetting.Image = Image.FromFile(ConstantResource.SETTING_ICON_SETTING_MOUSEOVER);
                lbSetting.ForeColor = Constant.COLOR_IN_USE;

                CommonFunc.NewControl(pnBody.Controls, ref uc, new Weedon.UcSetting());

                ScaleMaximizedControls();
            }
        }

        private void pbSetting_MouseEnter(object sender, EventArgs e)
        {
            pbSetting.Image = Image.FromFile(ConstantResource.SETTING_ICON_SETTING_MOUSEOVER);
            lbSetting.ForeColor = Constant.COLOR_MOUSEOVER;

            ttDetail.SetToolTip(pbSetting, Constant.TOOLTIP_SETTING);
        }

        private void pbSetting_MouseLeave(object sender, EventArgs e)
        {
            if (!isMainMenuClickSetting)
            {
                pbSetting.Image = Image.FromFile(ConstantResource.SETTING_ICON_SETTING);
                lbSetting.ForeColor = Constant.COLOR_NORMAL;
            }
            else
            {
                pbSetting.Image = Image.FromFile(ConstantResource.SETTING_ICON_SETTING_MOUSEOVER);
                lbSetting.ForeColor = Constant.COLOR_IN_USE;
            }
        }
    }
}
