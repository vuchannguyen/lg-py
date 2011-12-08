using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Function;
using System.Xml;
using CryptoFunction;
using DAO;
using System.IO;

namespace HR_STG_for_Client
{
    public partial class Form_Main : Form
    {
        private UC_HoSoCaNhan UC_HoSoCaNhan;
        private UC_TongQuanSuKien uc_TongQuanSuKien;
        private Startup A;

        //public static Form_Notice frm_Notice;

        private static PictureBox pb_Fade;
        private static Color colToFadeTo;
        private static Bitmap bitmap;

        public static void form_Fade()
        {
            pb_Fade.Visible = true;

            pb_Fade.Image = Fade.Lighter(Image_Function.cloneBitMap(bitmap), 80, colToFadeTo.R, colToFadeTo.G, colToFadeTo.B);
            pb_Fade.BringToFront();
        }

        public static void form_Fade(Bitmap bmp)
        {
            pb_Fade.Visible = true;

            pb_Fade.Image = Fade.Lighter(Image_Function.cloneBitMap(bmp), 80, colToFadeTo.R, colToFadeTo.G, colToFadeTo.B);
            pb_Fade.BringToFront();
        }

        public static void form_Normal()
        {
            pb_Fade.Visible = false;
        }

        public Form_Main()
        {
            InitializeComponent();

            this.SetStyle(ControlStyles.DoubleBuffer | ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint, true);
            this.UpdateStyles();

            A = new Startup(this);
            A.WaitStart += new Startup.TestEventHandler(A_WaitStart);
        }

        private void LoadPic()
        {
            try
            {
                //this.BackgroundImage = Image.FromFile(@"Resources\background.jpg");
                pnTopBar.BackgroundImage = Image.FromFile(@"Resources\General\topbar.png");
                pbMinimize.Image = Image.FromFile(@"Resources\ChucNang\button_minimize.png");
                pbExit.Image = Image.FromFile(@"Resources\ChucNang\button_exit.png");

                pbHeader.Image = Image.FromFile(@"Resources\General\brand.png");
                pbHorizonline.Image = Image.FromFile(@"Resources\General\horizonline.png");

                pbQuanLyHoSo.Image = Image.FromFile(@"Resources\General\icon_qlhoso.png");
                pbQuanLySuKien.Image = Image.FromFile(@"Resources\General\icon_qlsukien.png");

                pnAbout.BackgroundImage = Image.FromFile(@"Resources\General\bottom_horizonline.png");
            }
            catch
            {
                MessageBox.Show("ERROR!", "Kiểm tra thư mục Resource!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Dispose();
            }
        }

        private void Form_Main_Load(object sender, EventArgs e)
        {
            A.Go();

            this.StartPosition = FormStartPosition.CenterScreen;
            pbStartup.Image = Image.FromFile(@"Resources\General\startup.jpg");
            pbStartup.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            pbStartup.Location = SubFunction.SetCenterLocation(this.Size, pbStartup.Size);
            pbStartup.Dock = DockStyle.Fill;

            pnMain.Size = new Size(0, 0);
            this.Size = new Size(400, 197);
            Size size_Screen = new Size(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
            this.Location = SubFunction.SetCenterLocation(size_Screen, this.Size);

            colToFadeTo = Color.Black;

            pb_Fade = new PictureBox();
            pb_Fade.Dock = DockStyle.Fill;
            pb_Fade.Visible = false;
            this.Controls.Add(pb_Fade);
        }

        private void NewControls(int i)
        {
            if (i == 2)
            {
                try
                {
                    pnMain.Controls.Remove(UC_HoSoCaNhan);
                    pnMain.Controls.Remove(uc_TongQuanSuKien);
                }
                catch
                {
                    //Khong lam gi het
                }

                UC_HoSoCaNhan = new UC_HoSoCaNhan();
                UC_HoSoCaNhan.Location = SubFunction.SetWidthCenter(this.Size, new Size(800, 600), pnHeaderAndMainMenu.Top + 120);
                pnMain.Controls.Add(UC_HoSoCaNhan);

                UC_HoSoCaNhan.Visible = true;
            }

            if (i == 3)
            {
                try
                {
                    pnMain.Controls.Remove(UC_HoSoCaNhan);
                    pnMain.Controls.Remove(uc_TongQuanSuKien);
                }
                catch
                {
                    //Khong lam gi het
                }

                uc_TongQuanSuKien = new UC_TongQuanSuKien();
                uc_TongQuanSuKien.Location = SubFunction.SetWidthCenter(this.Size, uc_TongQuanSuKien.Size, pnHeaderAndMainMenu.Top + 120);
                pnMain.Controls.Add(uc_TongQuanSuKien);

                uc_TongQuanSuKien.Visible = true;
            }
        }



        #region StartUp
        private void LoadForm()
        {
            LoadPic();
            pnMain.Visible = true;
            pnMain.Dock = DockStyle.Fill;

            pnHeaderAndMainMenu.Location = SubFunction.SetWidthCenter(pnMain.Size, pnHeaderAndMainMenu.Size, 30);

            pnHello.Left = pnHeaderAndMainMenu.Left;
            lbAbout.Location = SubFunction.SetWidthCenter(pnMain.Size, lbAbout.Size, lbAbout.Top);
        }

        private void LoadCompleted()
        {
            pbStartup.Visible = false;
            this.BackColor = Color.White;

            this.Size = new Size(0, 0);
            this.Location = new Point(0, 0);
            this.Size = Screen.PrimaryScreen.Bounds.Size;
            this.WindowState = FormWindowState.Maximized;

            LoadForm();

            pbQuanLyHoSo.Image = Image.FromFile(@"Resources\General\icon_qlhoso.png");
            pbQuanLySuKien.Image = Image.FromFile(@"Resources\General\icon_qlsukien_selected.png");

            bitmap = new Bitmap(this.Width, this.Height);
            this.DrawToBitmap(bitmap, this.ClientRectangle);

            pbQuanLyHoSo.Image = Image.FromFile(@"Resources\General\icon_qlhoso_selected.png");
            pbQuanLySuKien.Image = Image.FromFile(@"Resources\General\icon_qlsukien.png");

            if (!File.Exists("HoSo.hrd"))
            {
                CreateHoSoData();
            }

            DAO.XMLConnection.sPathNTV_TV = "NTV_TV.cfg";
            DAO.XMLConnection.sPathHoSo = "HoSo.hrd";

            NewControls(2);
        }

        private void CreateHoSoData()
        {
            XmlDocument XmlDoc = new XmlDocument();
            List<XmlElement> list_elem = new List<XmlElement>();

            XmlElement Content = XmlDoc.CreateElement("HR-STG");
            //Content.SetAttribute("Version", "");

            list_elem.Add(XmlDoc.CreateElement("HS"));
            list_elem.Add(XmlDoc.CreateElement("HL"));
            list_elem.Add(XmlDoc.CreateElement("HS_HL"));

            for (int i = 0; i < list_elem.Count; i++)
            {
                Content.AppendChild(list_elem[i]);
            }

            if (Crypto.EncryptData(Content.OuterXml, "HoSo.hrd"))
            {
                //Form_Notice frm = new Form_Notice("Tạo file thành công.", false);

                //this.Dispose();
            }
            else
            {
                Form_Notice frm = new Form_Notice("Tạo HoSo.hrd thất bại!", "Liên hệ với Ban tổ chức.", false);

                this.Close();
            }
        }

        private void Form_Main_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (pbStartup.Visible)
            {
                pbStartup_Click(sender, e);
            }
        }

        void A_WaitStart()
        {
            LoadCompleted();
        }
        #endregion



        private void pbStartup_Click(object sender, EventArgs e)
        {
            // LoadCompleted();
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

        private void pbMinimize_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void pbMinimize_MouseEnter(object sender, EventArgs e)
        {
            pbMinimize.Image = Image.FromFile(@"Resources\ChucNang\button_minimize_selected.png");
        }

        private void pbMinimize_MouseLeave(object sender, EventArgs e)
        {
            pbMinimize.Image = Image.FromFile(@"Resources\ChucNang\button_minimize.png");
        }



        private void pbQuanLyHoSo_Click(object sender, EventArgs e)
        {
            //Hien quan ly ho so
            NewControls(2);

            pbQuanLyHoSo.Enabled = false;
            pbQuanLyHoSo.Image = Image.FromFile(@"Resources\General\icon_qlhoso_selected.png");

            pbQuanLySuKien.Enabled = true;
            pbQuanLySuKien.Image = Image.FromFile(@"Resources\General\icon_qlsukien.png");
        }

        private void pbQuanLyHoSo_MouseEnter(object sender, EventArgs e)
        {
            pbQuanLyHoSo.Image = Image.FromFile(@"Resources\General\icon_qlhoso_selected.png");
        }

        private void pbQuanLyHoSo_MouseLeave(object sender, EventArgs e)
        {
            if (pbQuanLyHoSo.Enabled)
            {
                pbQuanLyHoSo.Image = Image.FromFile(@"Resources\General\icon_qlhoso.png");
            }
        }



        private void pbQuanLySuKien_Click(object sender, EventArgs e)
        {
            //Hien quan ly su kien
            Form_Main.form_Fade(Image_Function.PrintScreen());

            Form_DangNhap frm = new Form_DangNhap();

            if (frm.Yes)
            {
                NewControls(3);

                pbQuanLyHoSo.Enabled = true;
                pbQuanLyHoSo.Image = Image.FromFile(@"Resources\General\icon_qlhoso.png");

                pbQuanLySuKien.Enabled = false;
                pbQuanLySuKien.Image = Image.FromFile(@"Resources\General\icon_qlsukien_selected.png");

            }

            form_Normal();
        }

        private void pbQuanLySuKien_MouseEnter(object sender, EventArgs e)
        {
            pbQuanLySuKien.Image = Image.FromFile(@"Resources\General\icon_qlsukien_selected.png");
        }

        private void pbQuanLySuKien_MouseLeave(object sender, EventArgs e)
        {
            if (pbQuanLySuKien.Enabled)
            {
                pbQuanLySuKien.Image = Image.FromFile(@"Resources\General\icon_qlsukien.png");
            }
        }
    }
}
