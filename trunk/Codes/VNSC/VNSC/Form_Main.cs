using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Drawing.Imaging;
using Function;
using DTO;
using DAO;
using PluginInterface;

namespace VNSC
{
    public partial class Form_Main : Form
    {
        private UC_QuanLyNhanSu uc_QuanLyNhanSu;
        private UC_QuanLySuKien uc_QuanLySuKien;
        private UC_TraCuu uc_TraCuu;
        private UC_HeThong uc_HeThong;
        private Form_CongCuHoTro frm_CongCuHoTro;

        private Startup A;

        private Form_DB_Definition frm_DB_Def;
        public static bool bDB_Def;

        public static Form_Notice frm_Notice;

        private static PictureBox pb_Fade;
        private static Color colToFadeTo;
        private static Bitmap bitmap;

        public static List<IPlugin> list_Plugin = new List<IPlugin>();
        public static List<HoSo> list_Export;
        public static string CurrentControlName;
        public static bool bExport;

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
                pnTopBar.BackgroundImage = Image.FromFile(@"Resources\topbar.png");
                pbMinimize.Image = Image.FromFile(@"Resources\ChucNang\button_minimize.png");
                pbExit.Image = Image.FromFile(@"Resources\ChucNang\button_exit.png");

                pbHeader.Image = Image.FromFile(@"Resources\brand.png");
                pbHorizonline.Image = Image.FromFile(@"Resources\horizonline.png");

                pbTraCuuChiTiet.Image = Image.FromFile(@"Resources\icon_tracuu.png");
                pbQuanLyHoSo.Image = Image.FromFile(@"Resources\icon_qlhoso.png");
                pbQuanLySuKien.Image = Image.FromFile(@"Resources\icon_qlsukien.png");
                pbHeThong.Image = Image.FromFile(@"Resources\top_system.png");
                pbCongCuHoTro.Image = Image.FromFile(@"Resources\button_plugins.png");

                pbThongKeTongQuat.Image = Image.FromFile(@"Resources\icon_statistic.png");

                pnAbout.BackgroundImage = Image.FromFile(@"Resources\bottom_horizonline.png");
            }
            catch
            {
                MessageBox.Show("ERROR!", "Kiểm tra thư mục Resource!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Dispose();
            }
        }

        private void Form_Main_Load(object sender, EventArgs e)
        {
            //LoadPic();

            A.Go();

            //this.WindowState = FormWindowState.Maximized;
            //pnMain.Dock = DockStyle.Fill;
            //this.WindowState = FormWindowState.Normal;
            //this.Size = new Size(400, 197);

            this.StartPosition = FormStartPosition.CenterScreen;
            pbStartup.Image = Image.FromFile(@"Resources\startup.jpg");
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

            Form_Main.CurrentControlName = "Form_Main";
            Form_Main.bExport = false;
        }



        #region StartUp
        private void LoadForm()
        {
            LoadPic();
            pnMain.Visible = true;
            pnMain.Dock = DockStyle.Fill;

            pnHeaderAndMainMenu.Location = SubFunction.SetWidthCenter(pnMain.Size, pnHeaderAndMainMenu.Size, 30);
            pnTotal.Location = new Point(pnHeaderAndMainMenu.Left, pnHeaderAndMainMenu.Bottom + 50);

            pnHello.Left = pnHeaderAndMainMenu.Left;
            lbAbout.Location = SubFunction.SetWidthCenter(pnMain.Size, lbAbout.Size, lbAbout.Top);
        }

        public static bool LoadPlugin()
        {
            try
            {
                Plugin_Function.LoadPlugin(Form_Main.list_Plugin);
            }
            catch
            {
                return false;
            }

            return true;
        }

        private void LoadCompleted()
        {
            pbStartup.Visible = false;
            this.BackColor = Color.White;

            this.Size = new Size(0, 0);
            this.Location = new Point(0, 0);
            this.Size = Screen.PrimaryScreen.Bounds.Size;
            this.WindowState = FormWindowState.Maximized;

            string sPath = @"SQL.con";
            if (File.Exists(sPath))
            {
                StreamReader srReader = new StreamReader(sPath);
                if (srReader.ReadLine() == "WA")
                {
                    SQL_Connection.bWindowsAuthentication = true;
                    SQL_Connection.sServerName = srReader.ReadLine();
                }
                else
                {
                    SQL_Connection.bWindowsAuthentication = false;
                    SQL_Connection.sServerName = srReader.ReadLine();
                    SQL_Connection.sUserName = srReader.ReadLine();
                    SQL_Connection.sPassword = srReader.ReadLine();
                }
                srReader.Close();

                if (SQL_Connection.CreateSQlConnection() == null)
                {
                    frm_DB_Def = new Form_DB_Definition();
                    frm_DB_Def.FormClosed += new FormClosedEventHandler(frm_DB_Def_FormClosed);

                    this.IsMdiContainer = true;
                    frm_DB_Def.MdiParent = this;
                    frm_DB_Def.Show();
                }
                else
                {
                    this.IsMdiContainer = false;
                    LoadForm();
                    LoadPlugin();

                    pnTotal.Visible = false;
                    bitmap = new Bitmap(this.Width, this.Height);
                    this.DrawToBitmap(bitmap, this.ClientRectangle);
                    pnTotal.Visible = true;
                }
            }
            else
            {
                frm_DB_Def = new Form_DB_Definition();
                frm_DB_Def.FormClosed += new FormClosedEventHandler(frm_DB_Def_FormClosed);

                this.IsMdiContainer = true;
                frm_DB_Def.MdiParent = this;
                frm_DB_Def.Show();
            }
        }

        private void frm_DB_Def_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (!bDB_Def)
            {
                this.Dispose();
            }

            this.IsMdiContainer = false;
            LoadForm();

            pnTotal.Visible = false;
            bitmap = new Bitmap(this.Width, this.Height);
            this.DrawToBitmap(bitmap, this.ClientRectangle);
            pnTotal.Visible = true;
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



        private void RemoveOldControl()
        {
            try
            {
                pnMain.Controls.Remove(uc_TraCuu);
                pnMain.Controls.Remove(uc_QuanLyNhanSu);
                pnMain.Controls.Remove(uc_QuanLySuKien);
                pnMain.Controls.Remove(uc_HeThong);
                //pnMain.Controls.Remove(uc_CongCuHoTro);
            }
            catch
            {
                //Khong lam gi het
            }
        }

        private void SetNewUC(UserControl uc)
        {
            uc.Location = SubFunction.SetWidthCenter(this.Size, uc.Size, pnHeaderAndMainMenu.Top + 120);
            pnMain.Controls.Add(uc);

            uc.Visible = true;
        }

        private void NewControls(int i)
        {
            if (i == 0)
            {
                RemoveOldControl();

                uc_TraCuu = new UC_TraCuu();
                SetNewUC(uc_TraCuu);
            }

            if (i == 1)
            {
                RemoveOldControl();

                uc_QuanLyNhanSu = new UC_QuanLyNhanSu();
                SetNewUC(uc_QuanLyNhanSu);
            }

            if (i == 2)
            {
                RemoveOldControl();

                uc_QuanLySuKien = new UC_QuanLySuKien();
                SetNewUC(uc_QuanLySuKien);
            }

            if (i == 3)
            {
                RemoveOldControl();

                uc_HeThong = new UC_HeThong();
                SetNewUC(uc_HeThong);
            }

            if (i == 4)
            {
                Form_Main.form_Fade(Image_Function.PrintScreen());
                frm_CongCuHoTro = new Form_CongCuHoTro(Form_Main.list_Plugin, Form_Main.list_Export);
                frm_CongCuHoTro.FormClosed += new FormClosedEventHandler(frm_CongCuHoTro_FormClosed);
                frm_CongCuHoTro.ShowDialog();
                Form_Main.form_Normal();
            }
        }



        private void pbStartup_Click(object sender, EventArgs e)
        {
            //LoadCompleted();
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



        private void pbTraCuuChiTiet_Click(object sender, EventArgs e)
        {
            //Hien tra cuu
            pnTotal.Visible = false;

            NewControls(0);
        }

        private void pbTraCuuChiTiet_MouseEnter(object sender, EventArgs e)
        {
            pbTraCuuChiTiet.Image = Image.FromFile(@"Resources\icon_tracuu_selected.png");
        }

        private void pbTraCuuChiTiet_MouseLeave(object sender, EventArgs e)
        {
            pbTraCuuChiTiet.Image = Image.FromFile(@"Resources\icon_tracuu.png");
        }

        private void pbQuanLyHoSo_Click(object sender, EventArgs e)
        {
            //Hien quan ly ho so
            pnTotal.Visible = false;

            NewControls(1);
        }

        private void pbQuanLyHoSo_MouseEnter(object sender, EventArgs e)
        {
            pbQuanLyHoSo.Image = Image.FromFile(@"Resources\icon_qlhoso_selected.png");
        }

        private void pbQuanLyHoSo_MouseLeave(object sender, EventArgs e)
        {
            pbQuanLyHoSo.Image = Image.FromFile(@"Resources\icon_qlhoso.png");
        }

        private void pbQuanLySuKien_Click(object sender, EventArgs e)
        {
            //Hien quan ly su kien
            pnTotal.Visible = false;

            NewControls(2);
        }

        private void pbQuanLySuKien_MouseEnter(object sender, EventArgs e)
        {
            pbQuanLySuKien.Image = Image.FromFile(@"Resources\icon_qlsukien_selected.png");
        }

        private void pbQuanLySuKien_MouseLeave(object sender, EventArgs e)
        {
            pbQuanLySuKien.Image = Image.FromFile(@"Resources\icon_qlsukien.png");
        }

        private void pbHeThong_Click(object sender, EventArgs e)
        {
            //Hien he thong
            pnTotal.Visible = false;

            NewControls(3);
        }

        private void pbHeThong_MouseEnter(object sender, EventArgs e)
        {
            pbHeThong.Image = Image.FromFile(@"Resources\top_system_mouseover.png");
        }

        private void pbHeThong_MouseLeave(object sender, EventArgs e)
        {
            pbHeThong.Image = Image.FromFile(@"Resources\top_system.png");
        }

        private void pbCongCuHoTro_Click(object sender, EventArgs e)
        {
            //Hien cong cu ho tro
            pnTotal.Visible = false;

            NewControls(4);
        }

        private void pbCongCuHoTro_MouseEnter(object sender, EventArgs e)
        {
            pbCongCuHoTro.Image = Image.FromFile(@"Resources\button_plugins_mouseover.png");
        }

        private void pbCongCuHoTro_MouseLeave(object sender, EventArgs e)
        {
            pbCongCuHoTro.Image = Image.FromFile(@"Resources\button_plugins.png");
        }



        private HoSo_DTO ConvertHoSo2HoSo_DTO(HoSo dto)
        {
            HoSo_DTO dto_Temp = new HoSo_DTO();

            //dto_Temp.Ma = int.Parse(dto.Ma.Substring(0, 4));
            dto_Temp.NgayCapNhat = dto.NgayCapNhat;

            //dto_Temp.MaIDV = (int)dto.MaIDV;
            dto_Temp.MaNhomTrachVu = dto.NhomTrachVu.Ten;
            dto_Temp.MaTrachVu = dto.TrachVu.Ten;
            dto_Temp.HoTen = dto.HoTen;
            dto_Temp.NgaySinh = (DateTime)dto.NgaySinh;
            dto_Temp.GioiTinh = dto.GioiTinh;
            dto_Temp.QueQuan = dto.QueQuan;
            dto_Temp.TrinhDoHocVan = dto.TrinhDoHocVan;
            dto_Temp.TonGiao = dto.TonGiao;
            dto_Temp.DiaChi = dto.DiaChi;
            dto_Temp.DienThoaiLienLac = dto.DienThoaiLienLac;
            dto_Temp.Email = dto.Email;

            dto_Temp.Nganh = dto.Nganh;
            dto_Temp.DonVi = dto.DonVi;
            dto_Temp.LienDoan = dto.LienDoan;
            dto_Temp.Dao = dto.Dao;
            dto_Temp.Chau = dto.Chau;
            dto_Temp.NgayTuyenHua = (DateTime)dto.NgayTuyenHua;
            dto_Temp.TruongNhanLoiHua = dto.TruongNhanLoiHua;
            dto_Temp.TrachVuTaiDonVi = dto.TrachVuTaiDonVi;
            dto_Temp.TrachVuNgoaiDonVi = dto.TrachVuNgoaiDonVi;
            dto_Temp.TenRung = dto.TenRung;
            dto_Temp.GhiChu = dto.GhiChu;

            dto_Temp.NgheNghiep = dto.NgheNghiep;
            //dto_Temp.NutDay = (int)dto.NutDay;
            //dto_Temp.PhuongHuong = (int)dto.PhuongHuong;
            //dto_Temp.CuuThuong = (int)dto.CuuThuong;
            //dto_Temp.TruyenTin = (int)dto.TruyenTin;
            //dto_Temp.TroChoi = (int)dto.TroChoi;
            //dto_Temp.LuaTrai = (int)dto.LuaTrai;
            dto_Temp.SoTruong = dto.SoTruong;

            return dto_Temp;
        }

        private void LoadUCExportExcel()
        {
            string sContent = "<HR-STG Version=\"" + System.Reflection.Assembly.GetExecutingAssembly().GetName().Version + "\">";

            foreach (HoSo dto in list_Export)
            {
                HoSo_DTO dto_Temp = ConvertHoSo2HoSo_DTO(dto);

                sContent += HoSo_DAO.Insert2String(dto_Temp);
            }

            sContent += "</HR-STG>";

            UserControl uc_ExportExcel = Plugin_Function.GetPluginUC(Form_Main.list_Plugin[0], sContent);
            uc_ExportExcel.Location = SubFunction.SetWidthCenter(this.Size, uc_ExportExcel.Size, pnHeaderAndMainMenu.Top + 120);
            pnMain.Controls.Add(uc_ExportExcel);
            uc_ExportExcel.BringToFront();
        }

        private void frm_CongCuHoTro_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (bExport)
            {
                LoadUCExportExcel();
            }
        }
    }
}
