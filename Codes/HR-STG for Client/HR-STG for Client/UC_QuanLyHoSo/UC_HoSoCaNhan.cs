using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Xml;
using CryptoFunction;
using Function;
using DTO;
using DAO;
using BUS;

namespace HR_STG_for_Client
{
    public partial class UC_HoSoCaNhan : UserControl
    {
        private List<HoSo_DTO> list_dto;

        private List<UC_HuanLuyen> list_UC_HuanLuyen;
        private List<UC_HuanLuyen> list_UC_HuanLuyen_Insert;
        private List<int> list_UC_HuanLuyen_Delete;
        private List<UC_HuanLuyen> list_UC_HuanLuyen_Update;

        private List<int> list_ID;
        private List<string> list_NhomTrachVu;
        private List<string> list_TrachVu;

        private HoSo_DTO dto_HoSo;
        private HuanLuyen_DTO dto_HuanLuyen;
        //private LuuTru dto_LuuTru;
        private HoSo_HuanLuyen_DTO dto_HoSo_HuanLuyen;
        //private HoSo_LuuTru dto_HoSo_LuuTru;

        private string sNgayCapNhat;
        private string sNgayCapNhatTruoc;
        private int iMaHoSo;
        private string sAvatarPath;

        private Point point_Pic;
        private Point point_PicBound;
        private Size size_Pic;
        private Size size_PicRec;

        private int iZoom;
        private Image imgZoom;
        private Image imgAvatar;
        private bool bNewAvatar;
        private List<string> list_FolderAvatar;

        private Form_Notice frm_Notice;
        private Form_Confirm frm_Confirm;

        public UC_HoSoCaNhan()
        {
            InitializeComponent();
        }

        private void LoadPic()
        {
            try
            {
                pbImport.Image = Image.FromFile(@"Resources\NhanSu\import.png");

                pbThem.Image = Image.FromFile(@"Resources\ChucNang\icon_them.png");
                pbXoa.Image = Image.FromFile(@"Resources\ChucNang\icon_xoa_disable.png");
                pbSua.Image = Image.FromFile(@"Resources\ChucNang\icon_sua_disable.png");

                pbTitle.Image = Image.FromFile(@"Resources\NhanSu\icon_qlhoso_hscn_title.png");

                pbTiepTuc_LLCN.Image = Image.FromFile(@"Resources\ChucNang\forward_disable.png");
                pbHuy_LLCN.Image = Image.FromFile(@"Resources\ChucNang\icon_cancel.png");

                pbTiepTuc_LLHD.Image = Image.FromFile(@"Resources\ChucNang\forward.png");
                pbTroVe_LLHD.Image = Image.FromFile(@"Resources\ChucNang\back.png");
                pbHuy_LLHD.Image = Image.FromFile(@"Resources\ChucNang\icon_cancel.png");

                pbTiepTuc_NNKN.Image = Image.FromFile(@"Resources\ChucNang\forward.png");
                pbTroVe_NNKN.Image = Image.FromFile(@"Resources\ChucNang\back.png");
                pbHuy_NNKN.Image = Image.FromFile(@"Resources\ChucNang\icon_cancel.png");

                pbTroVe_HL.Image = Image.FromFile(@"Resources\ChucNang\back.png");
                pbHuy_HL.Image = Image.FromFile(@"Resources\ChucNang\icon_cancel.png");
                pbHoanTat_HL.Image = Image.FromFile(@"Resources\ChucNang\icon_ok.png");

                pbAdd.Image = Image.FromFile(@"Resources\ChucNang\add.png");

                pbBrowse.Image = Image.FromFile(@"Resources\ChucNang\icon_browse.png");
                //pbCrop.Image = Image.FromFile(@"Resources\ChucNang\icon_crop.png");
            }
            catch
            {
                this.Dispose();
                frm_Notice = new Form_Notice("Kiểm tra thư mục Resource!", false);
            }
        }

        private void UC_HoSoCaNhan_Load(object sender, EventArgs e)
        {
            list_NhomTrachVu = new List<string>();
            list_TrachVu = new List<string>();

            if (!LayDSNhomTrachVu_ComboBox(cbNhomTrachVu_LLCN))
            {
                this.Visible = false;
                Form_Notice frm = new Form_Notice("Chưa khởi tạo Nhóm trách vụ!", false);
                return;
            }
            else
            {
                this.Visible = true;
            }

            if (!LayDSTrachVu_ComboBox(cbTrachVu_LLCN))
            {
                this.Visible = false;
                Form_Notice frm = new Form_Notice("Chưa khởi tạo Trách vụ!", false);
                return;
            }
            else
            {
                this.Visible = true;
            }

            LoadPic();
            this.Size = new System.Drawing.Size(800, 600);

            pnQuanLy.Size = new System.Drawing.Size(770, 485);
            pnQuanLy.Location = SubFunction.SetWidthCenter(this.Size, pnQuanLy.Size, 100);

            lbTitle.Left = lbSelect.Left;
            lbSelect.Text = "";

            pnLyLich.Size = new System.Drawing.Size(550, 545);
            pnLyLich.Location = SubFunction.SetWidthCenter(this.Size, pnLyLich.Size, 50);

            gbLyLichCaNhan.Size = new System.Drawing.Size(550, 545);
            gbLyLichCaNhan.Location = SubFunction.SetWidthCenter(pnLyLich.Size, gbLyLichCaNhan.Size, 0);

            gbLyLichHuongDao.Size = new System.Drawing.Size(550, 545);
            gbLyLichHuongDao.Location = SubFunction.SetWidthCenter(pnLyLich.Size, gbLyLichHuongDao.Size, 0);

            gbNgheNghiep_KiNang.Size = new System.Drawing.Size(550, 545);
            gbNgheNghiep_KiNang.Location = SubFunction.SetWidthCenter(pnLyLich.Size, gbNgheNghiep_KiNang.Size, 0);

            gbHuanLuyen.Size = new System.Drawing.Size(550, 545);
            gbHuanLuyen.Location = SubFunction.SetWidthCenter(pnLyLich.Size, gbHuanLuyen.Size, 0);

            pn_gbHuanLuyen.Size = new System.Drawing.Size(540, 430);
            pn_gbHuanLuyen.Location = SubFunction.SetWidthCenter(gbHuanLuyen.Size, pn_gbHuanLuyen.Size, 50);

            size_PicRec.Width = 90;
            size_PicRec.Height = 120;
            pnAvatar.Left = pnLyLich.Right + 5;
            pnAvatar.Top = pnLyLich.Top + 10;

            pbAvatar.MouseWheel += new System.Windows.Forms.MouseEventHandler(this.pbAvatar_MouseWheel);

            //list_dto = new List<HoSo>();

            list_UC_HuanLuyen = new List<UC_HuanLuyen>();
            list_UC_HuanLuyen_Insert = new List<UC_HuanLuyen>();
            list_UC_HuanLuyen_Delete = new List<int>();
            list_UC_HuanLuyen_Update = new List<UC_HuanLuyen>();

            //dto_HoSo = new HoSo();
            //dto_HuanLuyen = new HuanLuyen();
            //dto_LuuTru = new LuuTru();
            //dto_HoSo_HuanLuyen = new HoSo_HuanLuyen();
            //dto_HoSo_LuuTru = new HoSo_LuuTru();

            list_FolderAvatar = new List<string>();
            list_FolderAvatar.Add("DB");
            list_FolderAvatar.Add("Avatar");

            refreshListView();
        }



        #region Funtion
        private void refreshListView()
        {
            list_dto = HoSo_BUS.LayDSHoSo();

            SubFunction.ClearlvItem(lvThongTin);

            for (int i = 0; i < list_dto.Count; i++)
            {
                ListViewItem lvi = new ListViewItem();
                lvi.Text = list_dto[i].Ma.ToString();
                lvi.SubItems.Add(SubFunction.setSTT(i + 1));
                lvi.SubItems.Add(list_dto[i].HoTen);
                lvi.SubItems.Add(NhomTrachVu_BUS.TraCuuNhomTrachVuTheoMa(list_dto[i].MaNhomTrachVu).Ten);
                lvi.SubItems.Add(TrachVu_BUS.TraCuuTrachVuTheoMa(list_dto[i].MaTrachVu).Ten);
                lvi.SubItems.Add(list_dto[i].Nganh);
                lvi.SubItems.Add(list_dto[i].DonVi);

                lvThongTin.Items.Add(lvi);
            }

            pbXoa.Enabled = false;
            pbXoa.Image = Image.FromFile(@"Resources\ChucNang\icon_xoa_disable.png");
            pbSua.Enabled = false;
            pbSua.Image = Image.FromFile(@"Resources\ChucNang\icon_sua_disable.png");
        }

        private void NewLyLichCaNhan()
        {
            pbAvatar.Image = Image.FromFile(@"Resources\NhanSu\avatar.png");
            pbAvatar.Enabled = false;
            pnAvatar.Visible = true;
            bNewAvatar = false;

            cbNhomTrachVu_LLCN.Enabled = true;
            cbNhomTrachVu_LLCN.SelectedIndex = -1;
            cbTrachVu_LLCN.Enabled = false;
            cbTrachVu_LLCN.SelectedIndex = -1;

            if (String.Format("{0:tt}", DateTime.Now) == "AM")
            {
                sNgayCapNhat = String.Format("{0:dd/MM/yyyy}", DateTime.Now) + " - " + String.Format("{0:hh:mm:ss}", DateTime.Now) + " Sáng";
                lbNgayCapNhat.Text = sNgayCapNhat.Substring(0, 18) + sNgayCapNhat.Substring(21);
            }
            else
            {
                sNgayCapNhat = String.Format("{0:dd/MM/yyyy}", DateTime.Now) + " - " + String.Format("{0:hh:mm:ss}", DateTime.Now) + " Chiều";
                lbNgayCapNhat.Text = sNgayCapNhat.Substring(0, 18) + sNgayCapNhat.Substring(21);
            }

            tbHoTen_LLCN.Text = "";
            tbQueQuan_LLCN.Text = "";
            tbTrinhDoHocVan_LLCN.Text = "";
            tbTonGiao_LLCN.Text = "";
            tbDiaChi_LLCN.Text = "";
            tbDienThoaiLienLac_LLCN.Text = "";
            tbEmail_LLCN.Text = "";

            dtpNgaySinh_LLCN.Value = new DateTime(2000, 1, 1);
        }

        private void NewLyLichHuongDao()
        {
            rbThieu.Checked = true;

            tbDonVi_LLHD.Text = "";
            tbLienDoan_LLHD.Text = "";
            tbDao_LLHD.Text = "";
            tbChau_LLHD.Text = "";
            tbTruongNhanLoiHua_LLHD.Text = "";
            tbTrachVuTaiDonVi_LLHD.Text = "";
            tbTrachVuNgoaiDonVi_LLHD.Text = "";
            tbGhiChu_LLHD.Text = "";

            dtpNgayTuyenHua_LLHD.Value = new DateTime(2000, 1, 1);
        }

        private void NewNgheNghiep_KiNang()
        {
            chbNutDay_NNKN.Checked = false;
            chbPhuongHuong_NNKN.Checked = false;
            chbCuuThuong_NNKN.Checked = false;
            chbTruyenTin_NNKN.Checked = false;
            chbTroChoi_NNKN.Checked = false;
            chbLuaTrai_NNKN.Checked = false;

            tbNgheNghiep_NNKN.Text = "";
            tbSoTruong_NNKN.Text = "";
        }

        private void NewHuanLuyen()
        {
            if (list_UC_HuanLuyen.Count > 0)
            {
                list_UC_HuanLuyen.Clear();
            }

            if (pn_gbHuanLuyen.Controls.Count > 0)
            {
                pn_gbHuanLuyen.Controls.Clear();
            }
        }

        private void NewAll()
        {
            NewLyLichCaNhan();
            NewLyLichHuongDao();
            NewNgheNghiep_KiNang();
            NewHuanLuyen();

            gbLyLichCaNhan.Visible = true;
            gbLyLichHuongDao.Visible = false;
            gbNgheNghiep_KiNang.Visible = false;
            gbHuanLuyen.Visible = false;

            list_UC_HuanLuyen_Insert.Clear();
            list_UC_HuanLuyen_Delete.Clear();
            list_UC_HuanLuyen_Update.Clear();
        }

        //private string NewMa()
        //{
        //    if (list_dto.Count > 0)
        //    {
        //        sMaHoSo = SubFunction.ThemMa4So(int.Parse(sMaHoSo));
        //    }
        //    else
        //    {
        //        sMaHoSo = "0001";
        //    }

        //    return sMaHoSo;
        //}

        private void Cancel()
        {
            pnQuanLy.Visible = true;
            pnSelect.Visible = true;
            pbImport.Visible = true;

            pnLyLich.Visible = false;

            lbTitle.Text = "HỒ SƠ CÁ NHÂN";
            lbSelect.Text = "";
            pnNgayCapNhat.Visible = false;

            pnAvatar.Visible = false;

            refreshListView();
        }

        private bool LayDSNhomTrachVu_ComboBox(ComboBox cb)
        {
            List<NhomTrachVu_DTO> list_Temp = NhomTrachVu_BUS.LayDSNhomTrachVu();

            if (list_Temp.Count > 0)
            {
                list_NhomTrachVu.Clear();
                cb.Items.Clear();
                for (int i = 0; i < list_Temp.Count; i++)
                {
                    list_NhomTrachVu.Add(list_Temp[i].Ma);
                    cb.Items.Add(list_Temp[i].Ten);
                }
            }
            else
            {
                return false;
            }

            return true;
        }

        private bool LayDSTrachVu_ComboBox(ComboBox cb)
        {
            List<TrachVu_DTO> list_Temp = TrachVu_BUS.LayDSTrachVu();

            if (list_Temp.Count > 0)
            {
                list_TrachVu.Clear();
                cb.Items.Clear();
                for (int i = 0; i < list_Temp.Count; i++)
                {
                    list_TrachVu.Add(list_Temp[i].Ma);
                    cb.Items.Add(list_Temp[i].Ten);
                }
            }
            else
            {
                return false;
            }

            return true;
        }

        private bool LayDSTrachVuTheoMaNhomTrachVu_ComboBox(ComboBox cb, string sMaNhomTrachVu)
        {
            XmlNodeList list_Temp = XMLConnection.CreateXmlDocNTV_TV("TrachVu");

            if (list_Temp != null)
            {
                list_TrachVu.Clear();
                cb.Items.Clear();
                for (int i = 0; i < list_Temp.Count; i++)
                {
                    if (list_Temp[i]["MaNhomTrachVu"].InnerText == sMaNhomTrachVu)
                    {
                        list_TrachVu.Add(list_Temp[i].Attributes["Ma"].InnerText);
                        cb.Items.Add(list_Temp[i]["Ten"].InnerText);
                    }
                }
            }
            else
            {
                Form_Notice frm = new Form_Notice("Kiểm tra Nhóm trách vụ bị trùng!", false);
                return false;
            }

            return true;
        }

        private bool TestAvatarExist(string sFileName)
        {

            return false;
        }

        private string setAvatarPath(string sMa, string sDate)
        {
            if (sDate.Length > 23) //22/02/2222 - 22:22:22 Chieu
            {
                if (sDate.EndsWith("Chiều"))
                {
                    sAvatarPath = sMa + "_" + sDate.Substring(0, 2) + sDate.Substring(3, 2) + sDate.Substring(6, 4) + "_" + sDate.Substring(13, 2) + sDate.Substring(16, 2) + sDate.Substring(19, 2) + "CH.jpg";
                }
                else
                {
                    sAvatarPath = sMa + "_" + sDate.Substring(0, 2) + sDate.Substring(3, 2) + sDate.Substring(6, 4) + "_" + sDate.Substring(13, 2) + sDate.Substring(16, 2) + sDate.Substring(19, 2) + "SA.jpg";
                }
            }

            return sAvatarPath;
        }
        #endregion



        #region Select
        private void pbThem_Click(object sender, EventArgs e)
        {
            if (lvThongTin.Items.Count >= 50)
            {
                frm_Notice = new Form_Notice("Đã sử dụng hết 50 Hồ sơ.", false);
            }
            else
            {
                pnQuanLy.Visible = false;
                pnSelect.Visible = false;
                pbImport.Visible = false;

                pnLyLich.Visible = true;

                lbTitle.Text = "THÊM HỒ SƠ CÁ NHÂN";
                lbSelect.Text = "THÊM";

                NewAll();
                //NewMa();
                pnNgayCapNhat.Visible = true;

                if (list_dto.Count == 0)
                {
                    iMaHoSo = 1;
                }
                else
                {
                    iMaHoSo = list_dto[list_dto.Count - 1].Ma + 1;
                }

                tbHoTen_LLCN.Focus();
            }
        }

        private void pbThem_MouseEnter(object sender, EventArgs e)
        {
            pbThem.Image = Image.FromFile(@"Resources\ChucNang\icon_them_selected.png");
        }

        private void pbThem_MouseLeave(object sender, EventArgs e)
        {
            pbThem.Image = Image.FromFile(@"Resources\ChucNang\icon_them.png");
        }

        private void pbXoa_Click(object sender, EventArgs e)
        {
            frm_Confirm = new Form_Confirm("Đồng ý xóa " + lvThongTin.SelectedItems.Count + " dữ liệu?");
            if (frm_Confirm.Yes)
            {
                for (int i = 0; i < lvThongTin.SelectedItems.Count; i++)
                {
                    if (!DeleteHoSo(int.Parse(lvThongTin.SelectedItems[i].SubItems[0].Text)))
                    {
                        frm_Notice = new Form_Notice("Không thể xóa Hồ Sơ " + lvThongTin.SelectedItems[i].SubItems[0].Text + "!", "Vẫn còn ...!", false);
                        break;
                    }
                }

                refreshListView();
            }
        }

        private void pbXoa_MouseEnter(object sender, EventArgs e)
        {
            pbXoa.Image = Image.FromFile(@"Resources\ChucNang\icon_xoa_selected.png");
        }

        private void pbXoa_MouseLeave(object sender, EventArgs e)
        {
            pbXoa.Image = Image.FromFile(@"Resources\ChucNang\icon_xoa.png");
        }



        #region Lay thong tin ho so
        private void setHoSoTheoMaHoSo(int iMa)
        {
            if (File.Exists("HoSo.hrd"))
            {
                dto_HoSo = HoSo_BUS.TraCuuHoSoTheoMa(iMa);
                
                cbNhomTrachVu_LLCN.Text = NhomTrachVu_BUS.TraCuuNhomTrachVuTheoMa(dto_HoSo.MaNhomTrachVu).Ten;
                cbTrachVu_LLCN.Text = TrachVu_BUS.TraCuuTrachVuTheoMa(dto_HoSo.MaTrachVu).Ten;

                //sAvatarPath = Path.Combine(File_Function.getFinalFolder(list_FolderAvatar), setAvatarPath(iMa.ToString(), dto_HoSo.NgayCapNhat));
                //if (File.Exists(sAvatarPath))
                //{
                //    pbAvatar.Image = Image.FromFile(sAvatarPath);
                //    bNewAvatar = true;
                //}

                if (dto_HoSo.Avatar != "")
                {
                    pbAvatar.Image = Convert_Function.ConvertByteArrayToImage(Convert_Function.ConvertStringToByteArray(dto_HoSo.Avatar));
                }

                tbHoTen_LLCN.Text = dto_HoSo.HoTen;
                dtpNgaySinh_LLCN.Value = (DateTime)dto_HoSo.NgaySinh;

                if (dto_HoSo.GioiTinh == "Nam")
                {
                    rbNam.Checked = true;
                }

                if (dto_HoSo.GioiTinh == "Nữ")
                {
                    rbNu.Checked = true;
                }

                tbQueQuan_LLCN.Text = dto_HoSo.QueQuan;
                tbTrinhDoHocVan_LLCN.Text = dto_HoSo.TrinhDoHocVan;
                tbTonGiao_LLCN.Text = dto_HoSo.TonGiao;
                tbDiaChi_LLCN.Text = dto_HoSo.DiaChi;
                tbDienThoaiLienLac_LLCN.Text = dto_HoSo.DienThoaiLienLac;
                tbEmail_LLCN.Text = dto_HoSo.Email;

                if (dto_HoSo.Nganh == "Ấu")
                {
                    rbAu.Checked = true;
                }

                if (dto_HoSo.Nganh == "Thiếu")
                {
                    rbThieu.Checked = true;
                }

                if (dto_HoSo.Nganh == "Kha")
                {
                    rbKha.Checked = true;
                }

                if (dto_HoSo.Nganh == "Tráng")
                {
                    rbTrang.Checked = true;
                }

                if (dto_HoSo.Nganh == "Khác")
                {
                    rbKhac.Checked = true;
                }

                tbDonVi_LLHD.Text = dto_HoSo.DonVi;
                tbLienDoan_LLHD.Text = dto_HoSo.LienDoan;
                tbDao_LLHD.Text = dto_HoSo.Dao;
                tbChau_LLHD.Text = dto_HoSo.Chau;
                dtpNgayTuyenHua_LLHD.Value = (DateTime)dto_HoSo.NgayTuyenHua;
                tbTruongNhanLoiHua_LLHD.Text = dto_HoSo.TruongNhanLoiHua;
                tbTrachVuTaiDonVi_LLHD.Text = dto_HoSo.TrachVuTaiDonVi;
                tbTrachVuNgoaiDonVi_LLHD.Text = dto_HoSo.TrachVuNgoaiDonVi;
                tbTenRung_LLHD.Text = dto_HoSo.TenRung;
                tbGhiChu_LLHD.Text = dto_HoSo.GhiChu;

                tbNgheNghiep_NNKN.Text = dto_HoSo.NgheNghiep;

                if (dto_HoSo.NutDay == 1) //1
                {
                    chbNutDay_NNKN.Checked = true;
                }

                if (dto_HoSo.PhuongHuong == 1) //2
                {
                    chbPhuongHuong_NNKN.Checked = true;
                }

                if (dto_HoSo.CuuThuong == 1) //3
                {
                    chbCuuThuong_NNKN.Checked = true;
                }

                if (dto_HoSo.TruyenTin == 1) //4
                {
                    chbTruyenTin_NNKN.Checked = true;
                }

                if (dto_HoSo.TroChoi == 1) //5
                {                  
                    chbTroChoi_NNKN.Checked = true;
                }

                if (dto_HoSo.LuaTrai == 1) //6
                {
                    chbLuaTrai_NNKN.Checked = true;
                }

                tbSoTruong_NNKN.Text = dto_HoSo.SoTruong;

                List<HoSo_HuanLuyen_DTO> list_HoSo_HuanLuyen = HoSo_HuanLuyen_BUS.TraCuuDSHuanLuyenTheoMaHoSo(iMa);
                foreach (HoSo_HuanLuyen_DTO dto_Temp in list_HoSo_HuanLuyen)
                {
                    HuanLuyen_DTO dto_HuanLuyen_Temp = HuanLuyen_BUS.TraCuuHuanLuyenTheoMa(dto_Temp.MaHuanLuyen);
                    UC_HuanLuyen uc_HuanLuyen = new UC_HuanLuyen(dto_HuanLuyen_Temp.Ma, dto_HuanLuyen_Temp.Nganh, dto_HuanLuyen_Temp.Khoa, dto_HuanLuyen_Temp.TenKhoa, dto_HuanLuyen_Temp.KhoaTruong, (DateTime)dto_HuanLuyen_Temp.Nam, dto_HuanLuyen_Temp.MHL, dto_HuanLuyen_Temp.TinhTrang, true);

                    int iNewLocation = list_UC_HuanLuyen.Count * 180 + pn_gbHuanLuyen.AutoScrollPosition.Y;
                    uc_HuanLuyen.Location = new Point(8, iNewLocation);

                    list_UC_HuanLuyen.Add(uc_HuanLuyen);
                    pn_gbHuanLuyen.Controls.Add(list_UC_HuanLuyen[list_UC_HuanLuyen.Count - 1]);
                    list_UC_HuanLuyen[list_UC_HuanLuyen.Count - 1].VisibleChanged += new EventHandler(AfterDeleteHL);

                    list_UC_HuanLuyen_Update.Add(uc_HuanLuyen);
                }
            }
        }

        private bool setHoSoTheoThongTinXML(XmlNodeList list_HoSo, XmlNodeList list_HuanLuyen)
        {
            try
            {
                if (File.Exists("HoSo.hrd"))
                {
                    cbNhomTrachVu_LLCN.Text = NhomTrachVu_BUS.TraCuuNhomTrachVuTheoMa(list_HoSo[0]["MaNhomTrachVu"].InnerText).Ten;
                    cbTrachVu_LLCN.Text = TrachVu_BUS.TraCuuTrachVuTheoMa(list_HoSo[0]["MaTrachVu"].InnerText).Ten;

                    //sAvatarPath = Path.Combine(File_Function.getFinalFolder(list_FolderAvatar), setAvatarPath(iMa.ToString(), dto_HoSo.NgayCapNhat));
                    //if (File.Exists(sAvatarPath))
                    //{
                    //    pbAvatar.Image = Image.FromFile(sAvatarPath);
                    //    bNewAvatar = true;
                    //}

                    if (list_HoSo[0]["Avatar"].InnerText != "")
                    {
                        pbAvatar.Image = Convert_Function.ConvertByteArrayToImage(Convert_Function.ConvertStringToByteArray(list_HoSo[0]["Avatar"].InnerText));
                    }
                    else
                    {
                        pbAvatar.Image = Image.FromFile(@"Resources\NhanSu\avatar.png");
                    }

                    tbHoTen_LLCN.Text = list_HoSo[0]["HoTen"].InnerText;
                    dtpNgaySinh_LLCN.Value = DateTime.Parse(list_HoSo[0]["NgaySinh"].InnerText);

                    if (list_HoSo[0]["GioiTinh"].InnerText == "Nam")
                    {
                        rbNam.Checked = true;
                    }

                    if (list_HoSo[0]["GioiTinh"].InnerText == "Nữ")
                    {
                        rbNu.Checked = true;
                    }

                    tbQueQuan_LLCN.Text = list_HoSo[0]["QueQuan"].InnerText;
                    tbTrinhDoHocVan_LLCN.Text = list_HoSo[0]["TrinhDoHocVan"].InnerText;
                    tbTonGiao_LLCN.Text = list_HoSo[0]["TonGiao"].InnerText;
                    tbDiaChi_LLCN.Text = list_HoSo[0]["DiaChi"].InnerText;
                    tbDienThoaiLienLac_LLCN.Text = list_HoSo[0]["DienThoaiLienLac"].InnerText;
                    tbEmail_LLCN.Text = list_HoSo[0]["Email"].InnerText;

                    if (list_HoSo[0]["Nganh"].InnerText == "Ấu")
                    {
                        rbAu.Checked = true;
                    }

                    if (list_HoSo[0]["Nganh"].InnerText == "Thiếu")
                    {
                        rbThieu.Checked = true;
                    }

                    if (list_HoSo[0]["Nganh"].InnerText == "Kha")
                    {
                        rbKha.Checked = true;
                    }

                    if (list_HoSo[0]["Nganh"].InnerText == "Tráng")
                    {
                        rbTrang.Checked = true;
                    }

                    if (list_HoSo[0]["Nganh"].InnerText == "Khác")
                    {
                        rbKhac.Checked = true;
                    }

                    tbDonVi_LLHD.Text = list_HoSo[0]["DonVi"].InnerText;
                    tbLienDoan_LLHD.Text = list_HoSo[0]["LienDoan"].InnerText;
                    tbDao_LLHD.Text = list_HoSo[0]["Dao"].InnerText;
                    tbChau_LLHD.Text = list_HoSo[0]["Chau"].InnerText;
                    dtpNgayTuyenHua_LLHD.Value = DateTime.Parse(list_HoSo[0]["NgayTuyenHua"].InnerText);
                    tbTruongNhanLoiHua_LLHD.Text = list_HoSo[0]["TruongNhanLoiHua"].InnerText;
                    tbTrachVuTaiDonVi_LLHD.Text = list_HoSo[0]["TrachVuTaiDonVi"].InnerText;
                    tbTrachVuNgoaiDonVi_LLHD.Text = list_HoSo[0]["TrachVuNgoaiDonVi"].InnerText;
                    tbTenRung_LLHD.Text = list_HoSo[0]["TenRung"].InnerText;
                    tbGhiChu_LLHD.Text = list_HoSo[0]["GhiChu"].InnerText;

                    tbNgheNghiep_NNKN.Text = list_HoSo[0]["NgheNghiep"].InnerText;

                    if ("1" == list_HoSo[0]["NutDay"].InnerText) //1
                    {
                        chbNutDay_NNKN.Checked = true;
                    }

                    if ("1" == list_HoSo[0]["PhuongHuong"].InnerText) //2
                    {
                        chbPhuongHuong_NNKN.Checked = true;
                    }

                    if ("1" == list_HoSo[0]["CuuThuong"].InnerText) //3
                    {
                        chbCuuThuong_NNKN.Checked = true;
                    }

                    if ("1" == list_HoSo[0]["TruyenTin"].InnerText) //4
                    {
                        chbTruyenTin_NNKN.Checked = true;
                    }

                    if ("1" == list_HoSo[0]["TroChoi"].InnerText) //5
                    {
                        chbTroChoi_NNKN.Checked = true;
                    }

                    if ("1" == list_HoSo[0]["LuaTrai"].InnerText) //6
                    {
                        chbLuaTrai_NNKN.Checked = true;
                    }

                    tbSoTruong_NNKN.Text = list_HoSo[0]["SoTruong"].InnerText;

                    for (int i = 0; i < list_HuanLuyen.Count; i++)
                    //foreach (XmlNodeList dto_Temp in list_HuanLuyen)
                    {
                        UC_HuanLuyen uc_HuanLuyen = new UC_HuanLuyen(int.Parse(list_HuanLuyen[i].Attributes["Ma"].InnerText), list_HuanLuyen[i]["Nganh"].InnerText, list_HuanLuyen[i]["Khoa"].InnerText, list_HuanLuyen[i]["TenKhoa"].InnerText, list_HuanLuyen[i]["KhoaTruong"].InnerText, DateTime.Parse(list_HuanLuyen[i]["Nam"].InnerText), list_HuanLuyen[i]["MHL"].InnerText, list_HuanLuyen[i]["TinhTrang"].InnerText, true);

                        int iNewLocation = list_UC_HuanLuyen.Count * 180 + pn_gbHuanLuyen.AutoScrollPosition.Y;
                        uc_HuanLuyen.Location = new Point(8, iNewLocation);

                        list_UC_HuanLuyen.Add(uc_HuanLuyen);
                        pn_gbHuanLuyen.Controls.Add(list_UC_HuanLuyen[list_UC_HuanLuyen.Count - 1]);
                        list_UC_HuanLuyen[list_UC_HuanLuyen.Count - 1].VisibleChanged += new EventHandler(AfterDeleteHL);

                        list_UC_HuanLuyen_Update.Add(uc_HuanLuyen);
                    }
                }
            }
            catch
            {
                return false;
            }

            return true;
        }
        #endregion



        private void pbSua_Click(object sender, EventArgs e)
        {
            pnQuanLy.Visible = false;
            pnSelect.Visible = false;
            pbImport.Visible = false;

            pnLyLich.Visible = true;

            lbTitle.Text = "SỬA HỒ SƠ CÁ NHÂN";
            lbSelect.Text = "SỬA";

            NewAll();
            pnNgayCapNhat.Visible = true;

            iMaHoSo = int.Parse(lvThongTin.SelectedItems[0].Text);
            setHoSoTheoMaHoSo(iMaHoSo);

            //NewLuuTru();
        }

        private void pbSua_MouseEnter(object sender, EventArgs e)
        {
            pbSua.Image = Image.FromFile(@"Resources\ChucNang\icon_sua_selected.png");
        }

        private void pbSua_MouseLeave(object sender, EventArgs e)
        {
            pbSua.Image = Image.FromFile(@"Resources\ChucNang\icon_sua.png");
        }
        #endregion



        private void lvThongTin_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lvThongTin.SelectedItems.Count > 0)
            {
                if (lvThongTin.SelectedItems.Count == 1)
                {
                    pbSua.Enabled = true;
                    pbSua.Image = Image.FromFile(@"Resources\ChucNang\icon_sua.png");
                }
                else
                {
                    pbSua.Enabled = false;
                    pbSua.Image = Image.FromFile(@"Resources\ChucNang\icon_sua_disable.png");
                }

                pbXoa.Enabled = true;
                pbXoa.Image = Image.FromFile(@"Resources\ChucNang\icon_xoa.png");
            }
            else
            {
                pbXoa.Enabled = false;
                pbXoa.Image = Image.FromFile(@"Resources\ChucNang\icon_xoa_disable.png");
                pbSua.Enabled = false;
                pbSua.Image = Image.FromFile(@"Resources\ChucNang\icon_sua_disable.png");
            }
        }

        private void lvThongTin_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            //Form_Main.form_Fade();
            //Form_Detail frm_Detail = new Form_Detail(lvThongTin.SelectedItems[0].Text);
            //Form_Main.form_Normal();
        }

        private void lvThongTin_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            ListViewColumnSort.SortColumn(lvThongTin, e);
        }



        #region LLCN
        private void cbNhomTrachVu_LLCN_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbNhomTrachVu_LLCN.Text.Length > 0)
            {
                cbTrachVu_LLCN.Items.Clear();
                if (!LayDSTrachVuTheoMaNhomTrachVu_ComboBox(cbTrachVu_LLCN, list_NhomTrachVu[cbNhomTrachVu_LLCN.SelectedIndex]))
                {
                    frm_Notice = new Form_Notice("Chưa có Trách Vụ trong Nhóm trách vụ này!", false);
                }
                else
                {
                    cbTrachVu_LLCN.Enabled = true;
                }
            }

            if (cbTrachVu_LLCN.Text.Length > 0 && tbHoTen_LLCN.Text.Length > 0)
            {
                pbTiepTuc_LLCN.Enabled = true;
                pbTiepTuc_LLCN.Image = Image.FromFile(@"Resources\ChucNang\forward.png");
            }
            else
            {
                pbTiepTuc_LLCN.Enabled = false;
                pbTiepTuc_LLCN.Image = Image.FromFile(@"Resources\ChucNang\forward_disable.png");
            }
        }

        private void cbTrachVu_LLCN_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbTrachVu_LLCN.Text.Length > 0 && tbHoTen_LLCN.Text.Length > 0)
            {
                //tbMa_LLCN.Text = NewMa();
                pbTiepTuc_LLCN.Enabled = true;
                pbTiepTuc_LLCN.Image = Image.FromFile(@"Resources\ChucNang\forward.png");
            }
            else
            {
                pbTiepTuc_LLCN.Enabled = false;
                pbTiepTuc_LLCN.Image = Image.FromFile(@"Resources\ChucNang\forward_disable.png");
            }
        }
        private void tbHoTen_LLCN_TextChanged(object sender, EventArgs e)
        {
            if (cbTrachVu_LLCN.Text.Length > 0 && tbHoTen_LLCN.Text.Length > 0)
            {
                pbTiepTuc_LLCN.Enabled = true;
                pbTiepTuc_LLCN.Image = Image.FromFile(@"Resources\ChucNang\forward.png");
            }
            else
            {
                pbTiepTuc_LLCN.Enabled = false;
                pbTiepTuc_LLCN.Image = Image.FromFile(@"Resources\ChucNang\forward_disable.png");
            }
        }

        private void pbHuy_LLCN_Click(object sender, EventArgs e)
        {
            frm_Notice = new Form_Notice("Dữ liệu mới sẽ không được lưu lại!", "Bạn có muốn thoát?", true);
            if (frm_Notice.Yes)
            {
                Cancel();
            }
        }

        private void pbHuy_LLCN_MouseEnter(object sender, EventArgs e)
        {
            pbHuy_LLCN.Image = Image.FromFile(@"Resources\ChucNang\icon_cancel_selected.png");
        }

        private void pbHuy_LLCN_MouseLeave(object sender, EventArgs e)
        {
            pbHuy_LLCN.Image = Image.FromFile(@"Resources\ChucNang\icon_cancel.png");
        }

        private void pbTiepTuc_LLCN_Click(object sender, EventArgs e)
        {
            gbLyLichCaNhan.Visible = false;
            gbLyLichHuongDao.Visible = true;

            tbDonVi_LLHD.Focus();
        }

        private void pbTiepTuc_LLCN_MouseEnter(object sender, EventArgs e)
        {
            pbTiepTuc_LLCN.Image = Image.FromFile(@"Resources\ChucNang\forward_selected.png");
        }

        private void pbTiepTuc_LLCN_MouseLeave(object sender, EventArgs e)
        {
            pbTiepTuc_LLCN.Image = Image.FromFile(@"Resources\ChucNang\forward.png");
        }
        #endregion



        #region LLHD
        private void pbTroVe_LLHD_Click(object sender, EventArgs e)
        {
            gbLyLichCaNhan.Visible = true;
            gbLyLichHuongDao.Visible = false;

            tbHoTen_LLCN.Focus();
        }

        private void pbTroVe_LLHD_MouseEnter(object sender, EventArgs e)
        {
            pbTroVe_LLHD.Image = Image.FromFile(@"Resources\ChucNang\back_selected.png");
        }

        private void pbTroVe_LLHD_MouseLeave(object sender, EventArgs e)
        {
            pbTroVe_LLHD.Image = Image.FromFile(@"Resources\ChucNang\back.png");
        }

        private void pbHuy_LLHD_Click(object sender, EventArgs e)
        {
            frm_Notice = new Form_Notice("Dữ liệu mới sẽ không được lưu lại!", "Bạn có muốn thoát?", true);
            if (frm_Notice.Yes)
            {
                Cancel();
            }
        }

        private void pbHuy_LLHD_MouseEnter(object sender, EventArgs e)
        {
            pbHuy_LLHD.Image = Image.FromFile(@"Resources\ChucNang\icon_cancel_selected.png");
        }

        private void pbHuy_LLHD_MouseLeave(object sender, EventArgs e)
        {
            pbHuy_LLHD.Image = Image.FromFile(@"Resources\ChucNang\icon_cancel.png");
        }

        private void pbTiepTuc_LLHD_Click(object sender, EventArgs e)
        {
            gbLyLichHuongDao.Visible = false;
            gbNgheNghiep_KiNang.Visible = true;

            tbDonVi_LLHD.Focus();
        }

        private void pbTiepTuc_LLHD_MouseEnter(object sender, EventArgs e)
        {
            pbTiepTuc_LLHD.Image = Image.FromFile(@"Resources\ChucNang\forward_selected.png");
        }

        private void pbTiepTuc_LLHD_MouseLeave(object sender, EventArgs e)
        {
            pbTiepTuc_LLHD.Image = Image.FromFile(@"Resources\ChucNang\forward.png");
        }
        #endregion



        #region NNKN
        private void pbTroVe_NNKN_Click(object sender, EventArgs e)
        {
            gbLyLichHuongDao.Visible = true;
            gbNgheNghiep_KiNang.Visible = false;

            tbDonVi_LLHD.Focus();
        }

        private void pbTroVe_NNKN_MouseEnter(object sender, EventArgs e)
        {
            pbTroVe_NNKN.Image = Image.FromFile(@"Resources\ChucNang\back_selected.png");
        }

        private void pbTroVe_NNKN_MouseLeave(object sender, EventArgs e)
        {
            pbTroVe_NNKN.Image = Image.FromFile(@"Resources\ChucNang\back.png");
        }

        private void pbHuy_NNKN_Click(object sender, EventArgs e)
        {
            frm_Notice = new Form_Notice("Dữ liệu mới sẽ không được lưu lại!", "Bạn có muốn thoát?", true);
            if (frm_Notice.Yes)
            {
                Cancel();
            }
        }

        private void pbHuy_NNKN_MouseEnter(object sender, EventArgs e)
        {
            pbHuy_NNKN.Image = Image.FromFile(@"Resources\ChucNang\icon_cancel_selected.png");
        }

        private void pbHuy_NNKN_MouseLeave(object sender, EventArgs e)
        {
            pbHuy_NNKN.Image = Image.FromFile(@"Resources\ChucNang\icon_cancel.png");
        }

        private void pbTiepTuc_NNKN_Click(object sender, EventArgs e)
        {
            gbNgheNghiep_KiNang.Visible = false;
            gbHuanLuyen.Visible = true;
        }

        private void pbTiepTuc_NNKN_MouseEnter(object sender, EventArgs e)
        {
            pbTiepTuc_NNKN.Image = Image.FromFile(@"Resources\ChucNang\forward_selected.png");
        }

        private void pbTiepTuc_NNKN_MouseLeave(object sender, EventArgs e)
        {
            pbTiepTuc_NNKN.Image = Image.FromFile(@"Resources\ChucNang\forward.png");
        }
        #endregion



        #region HL
        private void AfterDeleteHL(object sender, EventArgs e)
        {
            UC_HuanLuyen uc_Temp = (UC_HuanLuyen)sender;
            if (!uc_Temp.Visible)
            {
                if (lbSelect.Text == "SỬA")
                {
                    if (list_UC_HuanLuyen_Update.Remove(uc_Temp))
                    {
                        list_UC_HuanLuyen_Delete.Add(uc_Temp.Ma);
                    }
                }

                list_UC_HuanLuyen.Remove(uc_Temp);
                list_UC_HuanLuyen_Insert.Remove(uc_Temp);

                for (int i = 0; i < list_UC_HuanLuyen.Count; i++)
                {
                    int iNewLocation = i * 180 + pn_gbHuanLuyen.AutoScrollPosition.Y;
                    list_UC_HuanLuyen[i].Location = new Point(8, iNewLocation);
                }
            }
        }

        private void pbAdd_Click(object sender, EventArgs e)
        {
            //pnThem_HL.Top = pnThem_HL.Location.Y + 180;
            int iNewLocation = list_UC_HuanLuyen.Count * 180 + pn_gbHuanLuyen.AutoScrollPosition.Y;

            UC_HuanLuyen uc_HuanLuyen = new UC_HuanLuyen();
            uc_HuanLuyen.Location = new Point(8, iNewLocation);
            uc_HuanLuyen.VisibleChanged += new EventHandler(AfterDeleteHL);

            list_UC_HuanLuyen.Add(uc_HuanLuyen); //dung de hien thi
            pn_gbHuanLuyen.Controls.Add(list_UC_HuanLuyen[list_UC_HuanLuyen.Count - 1]);

            list_UC_HuanLuyen_Insert.Add(uc_HuanLuyen); //dung de insert
        }

        private void pbTroVe_HL_Click(object sender, EventArgs e)
        {
            gbNgheNghiep_KiNang.Visible = true;
            gbHuanLuyen.Visible = false;

            tbNgheNghiep_NNKN.Focus();
        }

        private void pbTroVe_HL_MouseEnter(object sender, EventArgs e)
        {
            pbTroVe_HL.Image = Image.FromFile(@"Resources\ChucNang\back_selected.png");
        }

        private void pbTroVe_HL_MouseLeave(object sender, EventArgs e)
        {
            pbTroVe_HL.Image = Image.FromFile(@"Resources\ChucNang\back.png");
        }

        private void pbHuy_HL_Click(object sender, EventArgs e)
        {
            frm_Notice = new Form_Notice("Dữ liệu mới sẽ không được lưu lại!", "Bạn có muốn thoát?", true);
            if (frm_Notice.Yes)
            {
                Cancel();
            }
        }

        private void pbHuy_HL_MouseEnter(object sender, EventArgs e)
        {
            pbHuy_HL.Image = Image.FromFile(@"Resources\ChucNang\icon_cancel_selected.png");
        }

        private void pbHuy_HL_MouseLeave(object sender, EventArgs e)
        {
            pbHuy_HL.Image = Image.FromFile(@"Resources\ChucNang\icon_cancel.png");
        }



        #region Insert dto
        private void NewHoSo()
        {
            dto_HoSo = new HoSo_DTO();

            dto_HoSo.Ma = iMaHoSo;
            dto_HoSo.NgayCapNhat = sNgayCapNhat;

            dto_HoSo.Avatar = Convert_Function.ConvertByteArrayToString(Convert_Function.ConvertImageToByteArray(pbAvatar.Image));

            dto_HoSo.MaNhomTrachVu = list_NhomTrachVu[cbNhomTrachVu_LLCN.SelectedIndex];
            dto_HoSo.MaTrachVu = list_TrachVu[cbTrachVu_LLCN.SelectedIndex];

            dto_HoSo.HoTen = tbHoTen_LLCN.Text;
            dto_HoSo.NgaySinh = dtpNgaySinh_LLCN.Value;

            if (rbNam.Checked)
            {
                dto_HoSo.GioiTinh = rbNam.Text;
            }

            if (rbNu.Checked)
            {
                dto_HoSo.GioiTinh = rbNu.Text;
            }

            dto_HoSo.QueQuan = tbQueQuan_LLCN.Text;
            dto_HoSo.TrinhDoHocVan = tbTrinhDoHocVan_LLCN.Text;
            dto_HoSo.TonGiao = tbTonGiao_LLCN.Text;
            dto_HoSo.DiaChi = tbDiaChi_LLCN.Text;
            dto_HoSo.DienThoaiLienLac = tbDienThoaiLienLac_LLCN.Text;
            dto_HoSo.Email = tbEmail_LLCN.Text;

            if (rbAu.Checked)
            {
                dto_HoSo.Nganh = rbAu.Text;
            }

            if (rbThieu.Checked)
            {
                dto_HoSo.Nganh = rbThieu.Text;
            }

            if (rbKha.Checked)
            {
                dto_HoSo.Nganh = rbKha.Text;
            }

            if (rbTrang.Checked)
            {
                dto_HoSo.Nganh = rbTrang.Text;
            }

            if (rbKhac.Checked)
            {
                dto_HoSo.Nganh = rbKhac.Text;
            }

            dto_HoSo.DonVi = tbDonVi_LLHD.Text;
            dto_HoSo.LienDoan = tbLienDoan_LLHD.Text;
            dto_HoSo.Dao = tbDao_LLHD.Text;
            dto_HoSo.Chau = tbChau_LLHD.Text;
            dto_HoSo.NgayTuyenHua = dtpNgayTuyenHua_LLHD.Value;
            dto_HoSo.TruongNhanLoiHua = tbTruongNhanLoiHua_LLHD.Text;
            dto_HoSo.TrachVuTaiDonVi = tbTrachVuTaiDonVi_LLHD.Text;
            dto_HoSo.TrachVuNgoaiDonVi = tbTrachVuNgoaiDonVi_LLHD.Text;
            dto_HoSo.TenRung = tbTenRung_LLHD.Text;
            dto_HoSo.GhiChu = tbGhiChu_LLHD.Text;

            dto_HoSo.NgheNghiep = tbNgheNghiep_NNKN.Text;
            if (chbNutDay_NNKN.Checked) //1
            {
                dto_HoSo.NutDay = 1;
            }
            else
            {
                dto_HoSo.NutDay = 0;
            }

            if (chbPhuongHuong_NNKN.Checked) //2
            {
                dto_HoSo.PhuongHuong = 1;
            }
            else
            {
                dto_HoSo.PhuongHuong = 0;
            }

            if (chbCuuThuong_NNKN.Checked) //3
            {
                dto_HoSo.CuuThuong = 1;
            }
            else
            {
                dto_HoSo.CuuThuong = 0;
            }

            if (chbTruyenTin_NNKN.Checked) //4
            {
                dto_HoSo.TruyenTin = 1;
            }
            else
            {
                dto_HoSo.TruyenTin = 0;
            }

            if (chbTroChoi_NNKN.Checked) //5
            {
                dto_HoSo.TroChoi = 1;
            }
            else
            {
                dto_HoSo.TroChoi = 0;
            }

            if (chbLuaTrai_NNKN.Checked) //6
            {
                dto_HoSo.LuaTrai = 1;
            }
            else
            {
                dto_HoSo.LuaTrai = 0;
            }

            dto_HoSo.SoTruong = tbSoTruong_NNKN.Text;
        }

        private bool EncryptContent(string sContent)
        {
            if (sContent == null)
            {
                return false;
            }

            if (!Crypto.EncryptData(sContent, "HoSo.hrd"))
            {
                return false;
            }

            return true;
        }

        private bool InsertHoSo(HoSo_DTO dto)
        {
            string sContent = HoSo_BUS.Insert(dto);

            return EncryptContent(sContent);
        }

        private bool InsertHuanLuyen(List<UC_HuanLuyen> list_UC_Temp)
        {
            for (int i = 0; i < list_UC_Temp.Count; i++)
            {
                dto_HuanLuyen = new HuanLuyen_DTO();

                //Create ma cho client HL
                List<HuanLuyen_DTO> list_HL = HuanLuyen_BUS.LayDSHuanLuyen();
                if (list_HL.Count == 0)
                {
                    dto_HuanLuyen.Ma = 1;
                }
                else
                {
                    dto_HuanLuyen.Ma = list_HL[list_HL.Count - 1].Ma + 1;
                }
                //

                dto_HuanLuyen.Nganh = list_UC_Temp[i].Nganh;
                dto_HuanLuyen.Khoa = list_UC_Temp[i].Khoa;
                dto_HuanLuyen.TenKhoa = list_UC_Temp[i].TenKhoa;
                dto_HuanLuyen.KhoaTruong = list_UC_Temp[i].KhoaTruong;
                dto_HuanLuyen.Nam = list_UC_Temp[i].Nam;
                dto_HuanLuyen.MHL = list_UC_Temp[i].MHL;
                dto_HuanLuyen.TinhTrang = list_UC_Temp[i].TinhTrang;

                string sContent = HuanLuyen_BUS.Insert(dto_HuanLuyen);
                if (EncryptContent(sContent))
                {
                    dto_HoSo_HuanLuyen = new HoSo_HuanLuyen_DTO();

                    dto_HoSo_HuanLuyen.MaHoSo = iMaHoSo;
                    dto_HoSo_HuanLuyen.MaHuanLuyen = dto_HuanLuyen.Ma;

                    sContent = HoSo_HuanLuyen_BUS.Insert(dto_HoSo_HuanLuyen);
                    if (!EncryptContent(sContent))
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }

            return true;
        }
        #endregion



        #region Delete dto
        private bool DeleteHoSo(int iMa)
        {
            string sContent = "";

            List<HoSo_HuanLuyen_DTO> list_HoSo_HuanLuyen = HoSo_HuanLuyen_BUS.TraCuuDSHuanLuyenTheoMaHoSo(iMa);
            foreach (HoSo_HuanLuyen_DTO dto_Temp in list_HoSo_HuanLuyen)
            {
                int iTemp = dto_Temp.MaHuanLuyen;

                sContent = HoSo_HuanLuyen_BUS.Delete(dto_Temp.MaHoSo, dto_Temp.MaHuanLuyen);
                if (!EncryptContent(sContent))
                {
                    return false;
                }

                sContent = HuanLuyen_BUS.Delete(iTemp);
                if (!EncryptContent(sContent))
                {
                    return false;
                }
            }

            //string sPath = "";

            //sPath = Path.Combine(File_Function.getFinalFolder(list_FolderAvatar), setAvatarPath(iMa.ToString(), HoSo_BUS.TraCuuHoSoTheoMa(iMa).NgayCapNhat));
            //if (File.Exists(sPath))
            //{
            //    File.Delete(sPath);
            //}

            sContent = HoSo_BUS.Delete(iMa);
            if (!EncryptContent(sContent))
            {
                return false;
            }

            return true;
        }
        #endregion



        #region Update dto
        private bool UpdateHoSo(HoSo_DTO dto)
        {
            string sContent = HoSo_BUS.UpdateHoSoInfo(dto);
            if (!EncryptContent(sContent))
            {
                return false;
            }

            return true;
        }
        private bool UpdateHuanLuyen()
        {
            string sContent = "";

            if (!InsertHuanLuyen(list_UC_HuanLuyen_Insert))
            {
                return false;
            }

            for (int i = 0; i < list_UC_HuanLuyen_Delete.Count; i++)
            {
                sContent = HoSo_HuanLuyen_BUS.Delete(iMaHoSo, list_UC_HuanLuyen_Delete[i]);
                if (!EncryptContent(sContent))
                {
                    return false;
                }

                sContent = HuanLuyen_BUS.Delete(list_UC_HuanLuyen_Delete[i]);
                if (!EncryptContent(sContent))
                {
                    return false;
                }
            }

            for (int i = 0; i < list_UC_HuanLuyen_Update.Count; i++)
            {
                dto_HuanLuyen = new HuanLuyen_DTO();

                dto_HuanLuyen.Ma = list_UC_HuanLuyen_Update[i].Ma;
                dto_HuanLuyen.Nganh = list_UC_HuanLuyen_Update[i].Nganh;
                dto_HuanLuyen.Khoa = list_UC_HuanLuyen_Update[i].Khoa;
                dto_HuanLuyen.TenKhoa = list_UC_HuanLuyen_Update[i].TenKhoa;
                dto_HuanLuyen.KhoaTruong = list_UC_HuanLuyen_Update[i].KhoaTruong;
                dto_HuanLuyen.Nam = list_UC_HuanLuyen_Update[i].Nam;
                dto_HuanLuyen.MHL = list_UC_HuanLuyen_Update[i].MHL;
                dto_HuanLuyen.TinhTrang = list_UC_HuanLuyen_Update[i].TinhTrang;

                sContent = HuanLuyen_BUS.UpdateHuanLuyenInfo(dto_HuanLuyen);
                if (!EncryptContent(sContent))
                {
                    return false;
                }
            }

            return true;
        }
        #endregion



        private void pbHoanTat_HL_Click(object sender, EventArgs e)
        {
            //Nhap vao CSDL
            if (lbSelect.Text == "THÊM")
            {
                NewHoSo();

                if (InsertHoSo(dto_HoSo))
                {
                    if (InsertHuanLuyen(list_UC_HuanLuyen))
                    {
                        Cancel();

                        refreshListView();
                    }
                    else
                    {
                        Form_Notice frm = new Form_Notice("Không thể tạo Khóa Huấn Luyện!", false);
                    }
                }
                else
                {
                    Form_Notice frm = new Form_Notice("Không thể tạo Hồ Sơ!", false);
                }
            }

            if (lbSelect.Text == "SỬA")
            {
                sNgayCapNhatTruoc = dto_HoSo.NgayCapNhat;
                NewHoSo();

                if (UpdateHoSo(dto_HoSo))
                {
                    if (UpdateHuanLuyen())
                    {
                        Cancel();

                        refreshListView();
                    }
                    else
                    {
                        Form_Notice frm = new Form_Notice("Không thể cập nhật Hồ Sơ!", false);
                    }
                }
                else
                {
                    Form_Notice frm = new Form_Notice("Không thể cập nhật Hồ Sơ!", false);
                }
            }
        }

        private void pbHoanTat_HL_MouseEnter(object sender, EventArgs e)
        {
            pbHoanTat_HL.Image = Image.FromFile(@"Resources\ChucNang\icon_ok_selected.png");
        }

        private void pbHoanTat_HL_MouseLeave(object sender, EventArgs e)
        {
            pbHoanTat_HL.Image = Image.FromFile(@"Resources\ChucNang\icon_ok.png");
        }
        #endregion



        #region Avatar
        private void pbBrowse_Click(object sender, EventArgs e)
        {
            string sPath = File_Function.OpenDialog("JPG file", "jpg");
            if (sPath != null)
            {
                try
                {
                    imgAvatar = Image.FromFile(sPath);
                }
                catch
                {
                    frm_Notice = new Form_Notice("Không thể mở hình!", "Vui lòng kiểm tra lại.", false);

                    return;
                }

                if (imgAvatar.Width > 500 || imgAvatar.Height > 500)
                {
                    imgZoom = Image_Function.resizeImage(imgAvatar, new Size(500, 500));
                }
                else
                {
                    imgZoom = imgAvatar;
                }

                if (imgAvatar.Width >= 90 && imgAvatar.Height >= 120)
                {
                    pbAvatar.Cursor = Cursors.SizeAll;
                    pbAvatar.Enabled = true;

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

                    //pbAvatar.Image.Dispose();
                    pbAvatar.Image = Image_Function.CropImage(imgZoom, new Rectangle(point_PicBound, size_PicRec), pbAvatar.ClientRectangle);
                    bNewAvatar = true;
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

        private void pbAvatar_MouseDown(object sender, MouseEventArgs e)
        {
            point_Pic.X += e.X;
            point_Pic.Y += e.Y;
        }

        private void pbAvatar_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                point_PicBound.X = point_Pic.X - e.X;
                point_PicBound.Y = point_Pic.Y - e.Y;

                //pbAvatar.Image = Image_Function.resizeImage(imgAvatar, new Size(iZoom, iZoom));
                point_PicBound = Image_Function.setPicBound(point_PicBound, size_Pic, size_PicRec);
                //point_PicBound = Image_Function.setPicBound(point_PicBound, size_Pic, pbAvatar.Image.Size);
                pbAvatar.Image = Image_Function.CropImage(imgZoom, new Rectangle(point_PicBound, size_PicRec), pbAvatar.ClientRectangle);
            }
        }

        private void pbAvatar_MouseUp(object sender, MouseEventArgs e)
        {
            point_Pic.X = point_PicBound.X;
            point_Pic.Y = point_PicBound.Y;
        }

        private void pbAvatar_MouseWheel(object sender, MouseEventArgs e)
        {
            if (e.Delta > 0)
            {
                if (iZoom + 100 < 1000)
                {
                    iZoom += 100;
                    imgZoom = Image_Function.resizeImage(imgAvatar, new Size(iZoom, iZoom));

                    size_Pic.Width = imgZoom.Width;
                    size_Pic.Height = imgZoom.Height;
                    point_PicBound = Image_Function.setPicBound(point_PicBound, size_Pic, size_PicRec);

                    pbAvatar.Image = Image_Function.CropImage(imgZoom, new Rectangle(point_PicBound, size_PicRec), pbAvatar.ClientRectangle);
                    pbAvatar_MouseUp(sender, e);
                }
            }

            if (e.Delta < 0)
            {
                if (iZoom - 100 > 100)
                {
                    iZoom -= 100;
                    imgZoom = Image_Function.resizeImage(imgAvatar, new Size(iZoom, iZoom));

                    if (imgZoom.Width >= size_PicRec.Width && imgZoom.Height >= size_PicRec.Height)
                    {
                        size_Pic.Width = imgZoom.Width;
                        size_Pic.Height = imgZoom.Height;
                        point_PicBound = Image_Function.setPicBound(point_PicBound, size_Pic, size_PicRec);
                    }
                    else
                    {
                        iZoom += 100;
                        imgZoom = Image_Function.resizeImage(imgAvatar, new Size(iZoom, iZoom));

                        size_Pic.Width = imgZoom.Width;
                        size_Pic.Height = imgZoom.Height;
                        point_PicBound = Image_Function.setPicBound(point_PicBound, size_Pic, size_PicRec);
                    }

                    pbAvatar.Image = Image_Function.CropImage(imgZoom, new Rectangle(point_PicBound, size_PicRec), pbAvatar.ClientRectangle);
                    pbAvatar_MouseUp(sender, e);
                }
            }
        }

        private void pbAvatar_MouseEnter(object sender, EventArgs e)
        {
            pbAvatar.Focus();
        }

        private void pbAvatar_MouseLeave(object sender, EventArgs e)
        {
            pbBrowse.Focus();
        }
        #endregion



        private void pbImport_Click(object sender, EventArgs e)
        {
            string[] sPaths = File_Function.OpenDialogMultiSelect("HR data", "hrd");

            if (null != sPaths)
            {
                for (int i = 0; i < sPaths.Length; i++)
                {
                    if (lvThongTin.Items.Count >= 50)
                    {
                        frm_Notice = new Form_Notice("Đã sử dụng hết 50 Hồ sơ.", false);

                        return;
                    }
                    else
                    {
                        XmlDocument XmlDoc = new XmlDocument();

                        string sContent = Crypto.DecryptData(sPaths[i]);
                        if (sContent == null)
                        {
                            frm_Notice = new Form_Notice("Không thể mở file!", "Vui lòng kiểm tra lại.", false);

                            return;
                        }
                        else
                        {
                            XmlDoc.LoadXml(sContent);
                        }

                        XmlNodeList list_HoSo = XmlDoc.GetElementsByTagName("HoSo");
                        XmlNodeList list_HuanLuyen = XmlDoc.GetElementsByTagName("HuanLuyen");
                        XmlNodeList list_HoSo_HuanLuyen = XmlDoc.GetElementsByTagName("HoSo_HuanLuyen");

                        XmlDocument XmlDocHoSo = XMLConnection.SelectXmlDocHoSo();

                        if (setHoSoTheoThongTinXML(list_HoSo, list_HuanLuyen))
                        {
                            lbSelect.Text = "THÊM";
                            pbHoanTat_HL_Click(sender, e);
                            lbSelect.Text = "";
                        }
                    }
                }
            }
        }

        private void pbImport_MouseEnter(object sender, EventArgs e)
        {
            pbImport.Image = Image.FromFile(@"Resources\NhanSu\import_selected.png");
        }

        private void pbImport_MouseLeave(object sender, EventArgs e)
        {
            pbImport.Image = Image.FromFile(@"Resources\NhanSu\import.png");
        }
    }
}
