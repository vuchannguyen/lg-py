using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Library;
using QuanLyKinhDoanh.Main.QuanLyUser;

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

            this.Size = new System.Drawing.Size(1014, 758);

            pnMain.BackColor = Color.White;
            pnMain.Visible = true;
            pnMain.Dock = DockStyle.Fill;

            pnHeaderAndMainMenu.Location = CommonFunc.SetWidthCenter(pnMain.Size, pnHeaderAndMainMenu.Size, 30);

            pnHello.Left = pnHeaderAndMainMenu.Left;
            lbAbout.Location = CommonFunc.SetWidthCenter(pnMain.Size, lbAbout.Size, lbAbout.Top);

            pnBody.Width = pnMain.Width;
            pnBody.Height = pnMain.Height - Constant.TOP_HEIGHT_DEFAULT - Constant.BOT_HEIGHT_DEFAULT;

            pnBody.Location = CommonFunc.SetWidthCenter(pnMain.Size, pnBody.Size, Constant.TOP_HEIGHT_DEFAULT);
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

        private void NewVal()
        {
            uc = new UcQuanLyUser();
        }

        private void RemoveOldControl()
        {
            try
            {
                pnBody.Controls.Remove(uc);
            }
            catch
            {
                //Khong lam gi het
            }
        }

        private void NewControls(int i)
        {
            //if (i == 0)
            //{
            //    RemoveOldControl();

            //    ucQuanLyUser = new UcQuanLyUser();
            //    SetNewUC(ucQuanLyUser);
            //}

            //if (i == 1)
            //{
            //    RemoveOldControl();

            //    uc_QuanLyNhanSu = new UC_QuanLyNhanSu();
            //    SetNewUC(uc_QuanLyNhanSu);
            //}

            //if (i == 2)
            //{
            //    RemoveOldControl();

            //    uc_QuanLySuKien = new UC_QuanLySuKien();
            //    SetNewUC(uc_QuanLySuKien);
            //}

            //if (i == 3)
            //{
            //    RemoveOldControl();

            //    uc_HeThong = new UC_HeThong();
            //    SetNewUC(uc_HeThong);
            //}

            //if (i == 4)
            //{
            //    Form_Main.form_Fade(Image_Function.PrintScreen());
            //    frm_CongCuHoTro = new Form_CongCuHoTro(Form_Main.list_Plugin, Form_Main.list_Export);
            //    frm_CongCuHoTro.FormClosed += new FormClosedEventHandler(frm_CongCuHoTro_FormClosed);
            //    frm_CongCuHoTro.ShowDialog();
            //    Form_Main.form_Normal();
            //}
        }

        #region Image Button
        private void pbQuanLyUser_Click(object sender, EventArgs e)
        {
            pnBody.Controls.Remove(uc);
            uc = new UcQuanLyUser();
            uc.Location = CommonFunc.SetWidthCenter(this.Size, uc.Size, 0);
            pnBody.Controls.Add(uc);
            //CommonFunc.SetNewUc(pnBody.Controls, uc, this.Size);

            //SetNewUc(new UcQuanLyUser(), true);
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

        private void pbMinimize_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void pbMinimize_MouseEnter(object sender, EventArgs e)
        {
            pbMinimize.Image = Image.FromFile(@"Resources\ChucNang\button_minimize_mouseover.png");
        }

        private void pbMinimize_MouseLeave(object sender, EventArgs e)
        {
            pbMinimize.Image = Image.FromFile(@"Resources\ChucNang\button_minimize.png");
        }

        private void pbExit_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void pbExit_MouseEnter(object sender, EventArgs e)
        {
            pbExit.Image = Image.FromFile(@"Resources\ChucNang\button_exit_mouseover.png");
        }

        private void pbExit_MouseLeave(object sender, EventArgs e)
        {
            pbExit.Image = Image.FromFile(@"Resources\ChucNang\button_exit.png");
        }
    }
}
