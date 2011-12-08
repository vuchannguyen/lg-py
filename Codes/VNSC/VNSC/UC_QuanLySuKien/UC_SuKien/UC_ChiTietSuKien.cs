using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using CryptoFunction;
using Function;
using BUS;
using DAO;
using System.IO;

namespace VNSC
{
    public partial class UC_ChiTietSuKien : UserControl
    {
        private int iMaSuKien;

        private UC_DonViHanhChanh uc_DonViHanhChanh;
        private UC_TrachVuSuKien uc_TrachVuSuKien;

        private int iZoom;
        private Image imgZoom;
        private Image imgEvatar;
        //private bool bNewAvatar;
        private List<string> list_FolderEvatar;

        private Point point_Pic;
        private Point point_PicBound;
        private Size size_Pic;
        private Size size_PicRec;

        private int iTime;
        private SuKien dto_SuKien;
        private string sNgayCapNhat;
        private string sEvatarPath;

        public UC_ChiTietSuKien()
        {
            InitializeComponent();
        }

        public UC_ChiTietSuKien(int iMa, string sTen)
        {
            InitializeComponent();

            iMaSuKien = iMa;

            lbTitle.Left = lbSelect.Left;
            lbSelect.Text = "SỰ KIỆN";
            lbTitle.Text = "SỰ KIỆN " + sTen;

            //Tong quan
        }

        private void LoadPic()
        {
            try
            {
                pbHorizonline.Image = Image.FromFile(@"Resources\horizonline.png");
                pbThongKeTongQuat.Image = Image.FromFile(@"Resources\icon_statistic.png");

                pbTitle.Image = Image.FromFile(@"Resources\SuKien\icon_sukien_sukien_title.png");

                pbCacQuyDinh.Image = Image.FromFile(@"Resources\SuKien\button_quydinh.png");
                pbDonViHanhChanh.Image = Image.FromFile(@"Resources\SuKien\button_donvihanhchanh.png");
                pbTrachVuSuKien.Image = Image.FromFile(@"Resources\SuKien\button_tvsukien.png");
                pbHoSoThamDu.Image = Image.FromFile(@"Resources\SuKien\button_hsthamdu.png");
                pbDieuPhoi.Image = Image.FromFile(@"Resources\SuKien\button_dieuphoi.png");
                pbChucNangKhac.Image = Image.FromFile(@"Resources\SuKien\button_xuatdulieu.png");

                pbBrowse.Image = Image.FromFile(@"Resources\ChucNang\icon_browse.png");
                pbApply.Image = Image.FromFile(@"Resources\ChucNang\icon_apply_dis.png");
                pbEvatar.Image = Image.FromFile(@"Resources\SuKien\photodock.png");
            }
            catch
            {
                MessageBox.Show("ERROR!", "Kiểm tra thư mục Resource!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Dispose();
            }
        }

        private void UC_ChiTietSuKien_Load(object sender, EventArgs e)
        {
            LoadPic();

            pnHeaderAndMainMenu.Location = SubFunction.SetWidthCenter(this.Size, pnHeaderAndMainMenu.Size, 4);
            pnTotal.Location = new Point(pnHeaderAndMainMenu.Left, pnHeaderAndMainMenu.Bottom + 50);

            pnImageEvent.Location = new Point(pnHeaderAndMainMenu.Right - pbEvatar.Width - 30, pnTotal.Top);

            size_PicRec.Width = 250;
            size_PicRec.Height = 200;

            list_FolderEvatar = new List<string>();
            list_FolderEvatar.Add("DB");
            list_FolderEvatar.Add("Evatar");

            pbEvatar.MouseWheel += new System.Windows.Forms.MouseEventHandler(this.pbEvatar_MouseWheel);

            dto_SuKien = SuKien_BUS.TraCuuSuKienTheoMa(iMaSuKien);
            if (dto_SuKien.NgayCapNhatEvatar != null)
            {
                sEvatarPath = Path.Combine(File_Function.getFinalFolder(list_FolderEvatar), setEvatarPath(dto_SuKien.IDS, dto_SuKien.NgayCapNhatEvatar));
                if (File.Exists(sEvatarPath))
                {
                    string sImage = Convert_Function.ConvertByteArrayToString(Convert_Function.ConvertImageToByteArray(Image.FromFile(sEvatarPath)));
                    pbEvatar.Image = Convert_Function.ConvertByteArrayToImage(Convert_Function.ConvertStringToByteArray(sImage));
                }
            }
        }



        private void NewControls(int i)
        {
            if (i == 2)
            {
                try
                {
                    this.Controls.Remove(uc_DonViHanhChanh);
                    this.Controls.Remove(uc_TrachVuSuKien);
                }
                catch
                {
                    //Khong lam gi het
                }

                uc_DonViHanhChanh = new UC_DonViHanhChanh(iMaSuKien, lbTitle.Text);
                this.Controls.Add(uc_DonViHanhChanh);
                uc_DonViHanhChanh.BringToFront();

                uc_DonViHanhChanh.Visible = true;
            }

            if (i == 3)
            {
                try
                {
                    this.Controls.Remove(uc_DonViHanhChanh);
                    this.Controls.Remove(uc_TrachVuSuKien);
                }
                catch
                {
                    //Khong lam gi het
                }

                uc_TrachVuSuKien = new UC_TrachVuSuKien(iMaSuKien, lbTitle.Text);
                this.Controls.Add(uc_TrachVuSuKien);
                uc_TrachVuSuKien.BringToFront();

                uc_TrachVuSuKien.Visible = true;
            }

            if (i == 4)
            {
                try
                {
                    this.Controls.Remove(uc_DonViHanhChanh);
                    this.Controls.Remove(uc_TrachVuSuKien);
                }
                catch
                {
                    //Khong lam gi het
                }

                Form_Main.form_Fade(Image_Function.PrintScreen());
                Form_HoSoThamDu frm = new Form_HoSoThamDu(iMaSuKien);
                Form_Main.form_Normal();
            }

            if (i == 5)
            {
                try
                {
                    this.Controls.Remove(uc_DonViHanhChanh);
                    this.Controls.Remove(uc_TrachVuSuKien);
                }
                catch
                {
                    //Khong lam gi het
                }

                Form_Main.form_Fade();
                Form_DieuPhoi frm = new Form_DieuPhoi(iMaSuKien);
                Form_Main.form_Normal();
            }
        }



        private void pbCacQuyDinh_Click(object sender, EventArgs e)
        {

        }

        private void pbCacQuyDinh_MouseEnter(object sender, EventArgs e)
        {
            pbCacQuyDinh.Image = Image.FromFile(@"Resources\SuKien\button_quydinh_selected.png");
            lbCacQuyDinh.ForeColor = Color.Orange;
        }

        private void pbCacQuyDinh_MouseLeave(object sender, EventArgs e)
        {
            pbCacQuyDinh.Image = Image.FromFile(@"Resources\SuKien\button_quydinh.png");
            lbCacQuyDinh.ForeColor = Color.Gray;
        }



        private void pbDonViHanhChanh_Click(object sender, EventArgs e)
        {
            NewControls(2);
        }

        private void pbDonViHanhChanh_MouseEnter(object sender, EventArgs e)
        {
            pbDonViHanhChanh.Image = Image.FromFile(@"Resources\SuKien\button_donvihanhchanh_selected.png");
            lbDonViHanhChanh.ForeColor = Color.Orange;
        }

        private void pbDonViHanhChanh_MouseLeave(object sender, EventArgs e)
        {
            pbDonViHanhChanh.Image = Image.FromFile(@"Resources\SuKien\button_donvihanhchanh.png");
            lbDonViHanhChanh.ForeColor = Color.Gray;
        }



        private void pbTrachVuSuKien_Click(object sender, EventArgs e)
        {
            NewControls(3);
        }

        private void pbTrachVuSuKien_MouseEnter(object sender, EventArgs e)
        {
            pbTrachVuSuKien.Image = Image.FromFile(@"Resources\SuKien\button_tvsukien_selected.png");
            lbTrachVuSuKien.ForeColor = Color.Orange;
        }

        private void pbTrachVuSuKien_MouseLeave(object sender, EventArgs e)
        {
            pbTrachVuSuKien.Image = Image.FromFile(@"Resources\SuKien\button_tvsukien.png");
            lbTrachVuSuKien.ForeColor = Color.Gray;
        }



        private void pbHoSoThamDu_Click(object sender, EventArgs e)
        {
            NewControls(4);
        }

        private void pbHoSoThamDu_MouseEnter(object sender, EventArgs e)
        {
            pbHoSoThamDu.Image = Image.FromFile(@"Resources\SuKien\button_hsthamdu_selected.png");
            lbHoSoThamDu.ForeColor = Color.Orange;
        }

        private void pbHoSoThamDu_MouseLeave(object sender, EventArgs e)
        {
            pbHoSoThamDu.Image = Image.FromFile(@"Resources\SuKien\button_hsthamdu.png");
            lbHoSoThamDu.ForeColor = Color.Gray;
        }



        private void pbDieuPhoi_Click(object sender, EventArgs e)
        {
            NewControls(5);
        }

        private void pbDieuPhoi_MouseEnter(object sender, EventArgs e)
        {
            pbDieuPhoi.Image = Image.FromFile(@"Resources\SuKien\button_dieuphoi_selected.png");
            lbDieuPhoi.ForeColor = Color.Orange;
        }

        private void pbDieuPhoi_MouseLeave(object sender, EventArgs e)
        {
            pbDieuPhoi.Image = Image.FromFile(@"Resources\SuKien\button_dieuphoi.png");
            lbDieuPhoi.ForeColor = Color.Gray;
        }



        private string InsertSuKien2String()
        {
            try
            {
                SuKien dto_SuKien = SuKien_BUS.TraCuuSuKienTheoMa(iMaSuKien);

                XmlDocument XmlDoc = new XmlDocument();
                List<XmlElement> list_dto = new List<XmlElement>();

                XmlElement Content = XmlDoc.CreateElement("SuKien");
                Content.SetAttribute("Ma", dto_SuKien.Ma.ToString());



                #region Add content
                list_dto.Add(XmlDoc.CreateElement("Avatar"));
                list_dto[list_dto.Count - 1].InnerText = Convert_Function.ConvertByteArrayToString(Convert_Function.ConvertImageToByteArray(pbEvatar.Image));

                list_dto.Add(XmlDoc.CreateElement("IDS"));
                list_dto[list_dto.Count - 1].InnerText = dto_SuKien.IDS;

                list_dto.Add(XmlDoc.CreateElement("Ten"));
                list_dto[list_dto.Count - 1].InnerText = dto_SuKien.Ten;

                list_dto.Add(XmlDoc.CreateElement("DiaDiem"));
                list_dto[list_dto.Count - 1].InnerText = dto_SuKien.DiaDiem;

                list_dto.Add(XmlDoc.CreateElement("DonViToChuc"));
                list_dto[list_dto.Count - 1].InnerText = dto_SuKien.DonViToChuc;

                list_dto.Add(XmlDoc.CreateElement("NhomLoaiHinh"));
                list_dto[list_dto.Count - 1].InnerText = NhomLoaiHinh_BUS.TraCuuNhomLoaiHinhTheoMa(dto_SuKien.MaNhomLoaiHinh).Ten;

                list_dto.Add(XmlDoc.CreateElement("LoaiHinh"));
                list_dto[list_dto.Count - 1].InnerText = LoaiHinh_BUS.TraCuuLoaiHinhTheoMa(dto_SuKien.MaLoaiHinh).Ten;

                list_dto.Add(XmlDoc.CreateElement("Nganh"));
                list_dto[list_dto.Count - 1].InnerText = LoaiHinh_BUS.TraCuuLoaiHinhTheoMa(dto_SuKien.MaLoaiHinh).Nganh;

                list_dto.Add(XmlDoc.CreateElement("KhaiMac"));
                list_dto[list_dto.Count - 1].InnerText = dto_SuKien.KhaiMac.ToString();

                list_dto.Add(XmlDoc.CreateElement("BeMac"));
                list_dto[list_dto.Count - 1].InnerText = dto_SuKien.BeMac.ToString();

                list_dto.Add(XmlDoc.CreateElement("MoTa"));
                list_dto[list_dto.Count - 1].InnerText = dto_SuKien.MoTa;
                #endregion



                for (int i = 0; i < list_dto.Count; i++)
                {
                    Content.AppendChild(list_dto[i]);
                }

                return Content.OuterXml;
            }
            catch
            {
                return null;
            }
        }

        private string InsertIDV2String()
        {
            try
            {
                List<IDV> list_IDV = IDV_BUS.LayDSIDV();
                XmlDocument XmlDoc = new XmlDocument();

                XmlElement Content = XmlDoc.CreateElement("IDDonVi");



                #region Add content
                foreach (IDV dto_Temp in list_IDV)
                {
                    List<XmlElement> list_dto = new List<XmlElement>();

                    XmlElement element_Temp = XmlDoc.CreateElement("IDV");
                    element_Temp.SetAttribute("Ma", dto_Temp.Ma.ToString());

                    list_dto.Add(XmlDoc.CreateElement("IDV1"));
                    list_dto[list_dto.Count - 1].InnerText = dto_Temp.IDV1;

                    list_dto.Add(XmlDoc.CreateElement("DienGiai"));
                    list_dto[list_dto.Count - 1].InnerText = dto_Temp.DienGiai;

                    list_dto.Add(XmlDoc.CreateElement("MatKhau"));
                    list_dto[list_dto.Count - 1].InnerText = dto_Temp.MatKhau;

                    for (int i = 0; i < list_dto.Count; i++)
                    {
                        element_Temp.AppendChild(list_dto[i]);
                    }

                    Content.AppendChild(element_Temp);
                }
                #endregion



                return Content.OuterXml;
            }
            catch
            {
                return null;
            }
        }

        private string InsertNhomTrachVu2String()
        {
            try
            {
                List<NhomTrachVu> list_NhomTrachVu = NhomTrachVu_BUS.LayDSNhomTrachVu();
                XmlDocument XmlDoc = new XmlDocument();

                XmlElement Content = XmlDoc.CreateElement("NTV");



                #region Add content
                foreach (NhomTrachVu dto_Temp in list_NhomTrachVu)
                {
                    List<XmlElement> list_dto = new List<XmlElement>();

                    XmlElement element_Temp = XmlDoc.CreateElement("NhomTrachVu");
                    element_Temp.SetAttribute("Ma", dto_Temp.Ma.ToString());

                    list_dto.Add(XmlDoc.CreateElement("Ten"));
                    list_dto[list_dto.Count - 1].InnerText = dto_Temp.Ten;

                    for (int i = 0; i < list_dto.Count; i++)
                    {
                        element_Temp.AppendChild(list_dto[i]);
                    }

                    Content.AppendChild(element_Temp);
                }
                #endregion



                return Content.OuterXml;
            }
            catch
            {
                return null;
            }
        }

        private string InsertTrachVu2String()
        {
            try
            {
                List<TrachVu> list_TrachVu = TrachVu_BUS.LayDSTrachVu();
                XmlDocument XmlDoc = new XmlDocument();

                XmlElement Content = XmlDoc.CreateElement("TV");



                #region Add content
                foreach (TrachVu dto_Temp in list_TrachVu)
                {
                    List<XmlElement> list_dto = new List<XmlElement>();

                    XmlElement element_Temp = XmlDoc.CreateElement("TrachVu");
                    element_Temp.SetAttribute("Ma", dto_Temp.Ma.ToString());

                    list_dto.Add(XmlDoc.CreateElement("Ten"));
                    list_dto[list_dto.Count - 1].InnerText = dto_Temp.Ten;

                    list_dto.Add(XmlDoc.CreateElement("MaNhomTrachVu"));
                    list_dto[list_dto.Count - 1].InnerText = dto_Temp.MaNhomTrachVu;

                    for (int i = 0; i < list_dto.Count; i++)
                    {
                        element_Temp.AppendChild(list_dto[i]);
                    }

                    Content.AppendChild(element_Temp);
                }
                #endregion



                return Content.OuterXml;
            }
            catch
            {
                return null;
            }
        }

        private void CreateNTV_TVConfig()
        {
            string sContent = "<HR-STG Version=\"" + System.Reflection.Assembly.GetExecutingAssembly().GetName().Version + "\">";

            string sConfig = InsertNhomTrachVu2String();
            if (sConfig == null)
            {
                //Form_Notice frm = new Form_Notice("Không thể tạo Config!", "Vui lòng kiểm tra lại.", false);
                return;
            }
            sContent += sConfig;

            sConfig = InsertTrachVu2String();
            if (sConfig == null)
            {
                //Form_Notice frm = new Form_Notice("Không thể tạo Config!", "Vui lòng kiểm tra lại.", false);
                return;
            }
            sContent += sConfig;

            sContent += "</HR-STG>";

            if (Crypto.EncryptData(sContent, "NTV_TV.cfg"))
            {
                //Form_Notice frm = new Form_Notice("Tạo file thành công.", false);
            }
            else
            {
                //Form_Notice frm = new Form_Notice("Tạo file thất bại!", "Vui lòng thử lại.", false);
            }
        }

        private void pbChucNangKhac_Click(object sender, EventArgs e)
        {
            CreateNTV_TVConfig();

            string sPath = File_Function.SaveDialog("Config file", "cfg");

            if (sPath != null)
            {
                string sContent = "<HR-STG Version=\"" + System.Reflection.Assembly.GetExecutingAssembly().GetName().Version + "\">";

                string sConfig = InsertSuKien2String();
                if (sConfig == null)
                {
                    Form_Notice frm = new Form_Notice("Không thể tạo Config!", "Vui lòng kiểm tra lại.", false);
                    return;
                }
                sContent += sConfig;

                sConfig = InsertIDV2String();
                if (sConfig == null)
                {
                    Form_Notice frm = new Form_Notice("Không thể tạo Config!", "Vui lòng kiểm tra lại.", false);
                    return;
                }
                sContent += sConfig;

                sContent += "</HR-STG>";

                if (Crypto.EncryptData(sContent, sPath))
                {
                    //Form_Notice frm = new Form_Notice("Tạo file thành công.", false);
                }
                else
                {
                    Form_Notice frm = new Form_Notice("Tạo file thất bại!", "Vui lòng thử lại.", false);
                }
            }
        }

        private void pbChucNangKhac_MouseEnter(object sender, EventArgs e)
        {
            pbChucNangKhac.Image = Image.FromFile(@"Resources\SuKien\button_xuatdulieu_selected.png");
            lbChucNangKhac.ForeColor = Color.Orange;
        }

        private void pbChucNangKhac_MouseLeave(object sender, EventArgs e)
        {
            pbChucNangKhac.Image = Image.FromFile(@"Resources\SuKien\button_xuatdulieu.png");
            lbChucNangKhac.ForeColor = Color.Gray;
        }



        private void pbBrowse_Click(object sender, EventArgs e)
        {
            string sPath = File_Function.OpenDialog("JPG file", "jpg");

            if (sPath != null)
            {
                try
                {
                    imgEvatar = Image.FromFile(sPath);
                }
                catch
                {
                    Form_Notice frm_Notice = new Form_Notice("Không thể mở hình!", "Vui lòng kiểm tra lại.", false);

                    return;
                }

                if (imgEvatar.Width > 1000 || imgEvatar.Height > 1000)
                {
                    imgZoom = Image_Function.resizeImage(imgEvatar, new Size(1000, 1000));
                }
                else
                {
                    imgZoom = imgEvatar;
                }

                if (imgEvatar.Width >= 250 && imgEvatar.Height >= 200)
                {
                    pbEvatar.Cursor = Cursors.SizeAll;
                    pbEvatar.Enabled = true;

                    if (imgZoom.Width > imgZoom.Height)
                    {
                        iZoom = imgZoom.Width;
                    }
                    else
                    {
                        iZoom = imgZoom.Height;
                    }

                    point_Pic = new Point(imgZoom.Width / 2 - size_PicRec.Width / 2, imgZoom.Height / 2 - size_PicRec.Height / 2);
                    point_PicBound = point_Pic;
                    size_Pic.Width = imgZoom.Width;
                    size_Pic.Height = imgZoom.Height;

                    pbEvatar.Image = Image_Function.CropImage(imgZoom, new Rectangle(point_PicBound, size_PicRec), pbEvatar.ClientRectangle);

                    //lbTest.Visible = false;
                    pbApply.Enabled = true;
                }
                else
                {
                    Form_Notice frm = new Form_Notice("Kích thước ảnh quá nhỏ!", false);
                }
            }
        }

        private void pbBrowse_MouseEnter(object sender, EventArgs e)
        {
            pbBrowse.Image = Image.FromFile(@"Resources\ChucNang\icon_browse_selected.png");
        }

        private void pbBrowse_MouseLeave(object sender, EventArgs e)
        {
            pbBrowse.Image = Image.FromFile(@"Resources\ChucNang\icon_browse.png");
        }



        #region Evatar
        private void pbEvatar_MouseDown(object sender, MouseEventArgs e)
        {
            point_Pic.X += e.X;
            point_Pic.Y += e.Y;
        }

        private void pbEvatar_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                point_PicBound.X = point_Pic.X - e.X;
                point_PicBound.Y = point_Pic.Y - e.Y;

                point_PicBound = Image_Function.setPicBound(point_PicBound, size_Pic, size_PicRec);
                pbEvatar.Image = Image_Function.CropImage(imgZoom, new Rectangle(point_PicBound, size_PicRec), pbEvatar.ClientRectangle);
            }
        }

        private void pbEvatar_MouseUp(object sender, MouseEventArgs e)
        {
            point_Pic.X = point_PicBound.X;
            point_Pic.Y = point_PicBound.Y;
        }

        private void pbEvatar_MouseWheel(object sender, MouseEventArgs e)
        {
            if (e.Delta > 0)
            {
                if (iZoom + 100 < 2000)
                {
                    iZoom += 100;
                    imgZoom = Image_Function.resizeImage(imgEvatar, new Size(iZoom, iZoom));

                    size_Pic.Width = imgZoom.Width;
                    size_Pic.Height = imgZoom.Height;
                    point_PicBound = Image_Function.setPicBound(point_PicBound, size_Pic, size_PicRec);

                    pbEvatar.Image = Image_Function.CropImage(imgZoom, new Rectangle(point_PicBound, size_PicRec), pbEvatar.ClientRectangle);
                    pbEvatar_MouseUp(sender, e);
                }
            }

            if (e.Delta < 0)
            {
                if (iZoom - 100 > 100)
                {
                    iZoom -= 100;
                    imgZoom = Image_Function.resizeImage(imgEvatar, new Size(iZoom, iZoom));

                    if (imgZoom.Width >= size_PicRec.Width && imgZoom.Height >= size_PicRec.Height)
                    {
                        size_Pic.Width = imgZoom.Width;
                        size_Pic.Height = imgZoom.Height;
                        point_PicBound = Image_Function.setPicBound(point_PicBound, size_Pic, size_PicRec);
                    }
                    else
                    {
                        iZoom += 100;
                        imgZoom = Image_Function.resizeImage(imgEvatar, new Size(iZoom, iZoom));

                        size_Pic.Width = imgZoom.Width;
                        size_Pic.Height = imgZoom.Height;
                        point_PicBound = Image_Function.setPicBound(point_PicBound, size_Pic, size_PicRec);
                    }

                    pbEvatar.Image = Image_Function.CropImage(imgZoom, new Rectangle(point_PicBound, size_PicRec), pbEvatar.ClientRectangle);
                    pbEvatar_MouseUp(sender, e);
                }
            }
        }

        private void pbEvatar_MouseEnter(object sender, EventArgs e)
        {
            pbEvatar.Focus();
        }

        private void pbEvatar_MouseLeave(object sender, EventArgs e)
        {
            pbBrowse.Focus();
        }
        #endregion



        private string setEvatarPath(string sMa, string sDate)
        {
            if (sDate.Length > 23) //22/02/2222 - 22:22:22 Chieu
            {
                if (sDate.EndsWith("Chiều"))
                {
                    sEvatarPath = sMa + "_" + sDate.Substring(0, 2) + sDate.Substring(3, 2) + sDate.Substring(6, 4) + "_" + sDate.Substring(13, 2) + sDate.Substring(16, 2) + sDate.Substring(19, 2) + "CH.jpg";
                }
                else
                {
                    sEvatarPath = sMa + "_" + sDate.Substring(0, 2) + sDate.Substring(3, 2) + sDate.Substring(6, 4) + "_" + sDate.Substring(13, 2) + sDate.Substring(16, 2) + sDate.Substring(19, 2) + "SA.jpg";
                }
            }

            return sEvatarPath;
        }

        private void timer_Evatar_Tick(object sender, EventArgs e)
        {
            iTime++;
            if (iTime >= 10)
            {
                sEvatarPath = Path.Combine(File_Function.getFinalFolder(list_FolderEvatar), setEvatarPath(dto_SuKien.IDS, dto_SuKien.NgayCapNhatEvatar));
                if (File.Exists(sEvatarPath))
                {
                    string sImage = Convert_Function.ConvertByteArrayToString(Convert_Function.ConvertImageToByteArray(Image.FromFile(sEvatarPath)));
                    pbEvatar.Image = Convert_Function.ConvertByteArrayToImage(Convert_Function.ConvertStringToByteArray(sImage));
                }

                pbEvatar.Enabled = false;
                pbEvatar.Visible = true;

                lbTest.Visible = false;
                pbApply.Enabled = false;
                timer_Evatar.Stop();
            }

            if (lbTest.Text == "Vui lòng đợi Apply...")
            {
                lbTest.Text = "Vui lòng đợi Apply";
            }

            if (lbTest.Text == "Vui lòng đợi Apply..")
            {
                lbTest.Text = "Vui lòng đợi Apply...";
            }

            if (lbTest.Text == "Vui lòng đợi Apply.")
            {
                lbTest.Text = "Vui lòng đợi Apply..";
            }

            if (lbTest.Text == "Vui lòng đợi Apply")
            {
                lbTest.Text = "Vui lòng đợi Apply.";
            }

            try
            {
                sEvatarPath = Path.Combine(File_Function.getFinalFolder(list_FolderEvatar), setEvatarPath(dto_SuKien.IDS, dto_SuKien.NgayCapNhatEvatar));

                if (File.Exists(sEvatarPath))
                {
                    File.Delete(sEvatarPath);
                }

                dto_SuKien.NgayCapNhatEvatar = sNgayCapNhat;
                SuKien_BUS.UpdateSuKienInfo(dto_SuKien);

                sEvatarPath = Path.Combine(File_Function.getFinalFolder(list_FolderEvatar), setEvatarPath(dto_SuKien.IDS, dto_SuKien.NgayCapNhatEvatar));
                if (File.Exists(sEvatarPath))
                {
                    string sImage = Convert_Function.ConvertByteArrayToString(Convert_Function.ConvertImageToByteArray(Image.FromFile(sEvatarPath)));
                    pbEvatar.Image = Convert_Function.ConvertByteArrayToImage(Convert_Function.ConvertStringToByteArray(sImage));
                }

                pbEvatar.Enabled = false;
                pbEvatar.Visible = true;

                lbTest.Visible = false;
                pbApply.Enabled = false;
                timer_Evatar.Stop();
            }
            catch
            {
                //khong lam gi het
            }
        }

        private void pbApply_Click(object sender, EventArgs e)
        {
            dto_SuKien = SuKien_BUS.TraCuuSuKienTheoMa(iMaSuKien);

            if (String.Format("{0:tt}", DateTime.Now) == "AM")
            {
                sNgayCapNhat = String.Format("{0:dd/MM/yyyy}", DateTime.Now) + " - " + String.Format("{0:hh:mm:ss}", DateTime.Now) + " Sáng";
            }
            else
            {
                sNgayCapNhat = String.Format("{0:dd/MM/yyyy}", DateTime.Now) + " - " + String.Format("{0:hh:mm:ss}", DateTime.Now) + " Chiều";
            }

            if (!File_Function.savePic(list_FolderEvatar, setEvatarPath(dto_SuKien.IDS, sNgayCapNhat), (Bitmap)pbEvatar.Image))
            {
                Form_Notice frm_Notice = new Form_Notice("Kiểm tra Evatar của hồ sơ đang mở!", false);
            }

            pbEvatar.Visible = false;
            if (dto_SuKien.NgayCapNhatEvatar != null)
            {
                iTime = 1;
                lbTest.Text = "Vui lòng đợi Apply";
                lbTest.Visible = true;
                timer_Evatar.Start();
            }
            else
            {
                dto_SuKien.NgayCapNhatEvatar = sNgayCapNhat;
                SuKien_BUS.UpdateSuKienInfo(dto_SuKien);

                sEvatarPath = Path.Combine(File_Function.getFinalFolder(list_FolderEvatar), setEvatarPath(dto_SuKien.IDS, dto_SuKien.NgayCapNhatEvatar));
                if (File.Exists(sEvatarPath))
                {
                    string sImage = Convert_Function.ConvertByteArrayToString(Convert_Function.ConvertImageToByteArray(Image.FromFile(sEvatarPath)));
                    pbEvatar.Image = Convert_Function.ConvertByteArrayToImage(Convert_Function.ConvertStringToByteArray(sImage));
                }

                pbEvatar.Enabled = false;
                pbEvatar.Visible = true;

                lbTest.Visible = false;
                pbApply.Enabled = false;
                timer_Evatar.Stop();
            }
        }

        private void pbApply_MouseEnter(object sender, EventArgs e)
        {
            pbApply.Image = Image.FromFile(@"Resources\ChucNang\icon_apply_selected.png");
        }

        private void pbApply_MouseLeave(object sender, EventArgs e)
        {
            pbApply.Image = Image.FromFile(@"Resources\ChucNang\icon_apply.png");
        }
    }
}
