using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DAO;
using BUS;
using System.IO;
using Function;

namespace VNSC
{
    public partial class UC_SuKien_HoSoThamDu : UserControl
    {
        private int iMaSuKien;
        private string sSelect;

        private List<SuKien_HoSo> list_dto;

        private List<UC_HuanLuyen> list_UC_HuanLuyen;
        private List<UC_HuanLuyen> list_UC_HuanLuyen_Insert;
        private List<int> list_UC_HuanLuyen_Delete;
        private List<UC_HuanLuyen> list_UC_HuanLuyen_Update;

        private List<int> list_IDV;
        private List<string> list_NhomTrachVu;
        private List<string> list_TrachVu;

        private HoSo dto_HoSo;

        private SuKien_HoSo dto_SuKien_HoSo;
        private SuKien_HuanLuyen dto_SuKien_HuanLuyen;
        private SuKien_HoSo_HuanLuyen dto_SuKien_HoSo_HuanLuyen;

        private string sMaHoSo;

        private string sNgayCapNhat;
        private string sNgayCapNhatTruoc;
        private string sMaSuKien_HoSo;
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
        //private Form_Confirm frm_Confirm;

        private bool bCopyHoSoCaNhanToHoSoThamDu;
        private bool bImportHoSoClientToHoSoThamDu;

        private SuKien_HoSo dto_SuKien_HoSo_Import;
        private List<SuKien_HuanLuyen> list_SuKien_HuanLuyen;
        //private List<SuKien_HoSo_HuanLuyen> list_SK_HS_HL;

        #region Form_Load
        public UC_SuKien_HoSoThamDu()
        {
            InitializeComponent();
        }

        public UC_SuKien_HoSoThamDu(int iMa)
        {
            InitializeComponent();

            iMaSuKien = iMa;
            sSelect = "THÊM";

            list_dto = SuKien_HoSo_BUS.LayDSSuKien_HoSo();
            if (list_dto.Count > 0)
            {
                sMaSuKien_HoSo = list_dto[list_dto.Count - 1].Ma;
            }

            NewMa();
            tbMa_LLCN.Text = sMaSuKien_HoSo;

            bCopyHoSoCaNhanToHoSoThamDu = false;
            bImportHoSoClientToHoSoThamDu = false;
        }

        public UC_SuKien_HoSoThamDu(int iMa, SuKien_HoSo dto_SK_HS, List<SuKien_HuanLuyen> list_SK_HL)
        {
            InitializeComponent();

            iMaSuKien = iMa;
            sSelect = "THÊM";

            list_dto = SuKien_HoSo_BUS.LayDSSuKien_HoSo();
            if (list_dto.Count > 0)
            {
                sMaSuKien_HoSo = list_dto[list_dto.Count - 1].Ma;
            }

            NewMa();
            tbMa_LLCN.Text = sMaSuKien_HoSo;

            bCopyHoSoCaNhanToHoSoThamDu = false;
            bImportHoSoClientToHoSoThamDu = true;

            dto_SuKien_HoSo_Import = dto_SK_HS;
            list_SuKien_HuanLuyen = list_SK_HL;
        }

        public UC_SuKien_HoSoThamDu(int iMa, string sMaSuKien_HoSo)
        {
            InitializeComponent();

            iMaSuKien = iMa;
            this.sMaSuKien_HoSo = sMaSuKien_HoSo;

            sSelect = "SỬA";

            bCopyHoSoCaNhanToHoSoThamDu = false;
        }

        public UC_SuKien_HoSoThamDu(int iMa, string sMaHoSo, bool bCopy)
        {
            InitializeComponent();

            iMaSuKien = iMa;
            sSelect = "THÊM";

            list_dto = SuKien_HoSo_BUS.LayDSSuKien_HoSo();
            if (list_dto.Count > 0)
            {
                sMaSuKien_HoSo = list_dto[list_dto.Count - 1].Ma;
            }

            NewMa();
            tbMa_LLCN.Text = sMaSuKien_HoSo;

            this.sMaHoSo = sMaHoSo;
            bCopyHoSoCaNhanToHoSoThamDu = bCopy;
        }

        private void LoadPic()
        {
            try
            {
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
                pbAvatar.Image = Image.FromFile(@"Resources\NhanSu\avatar.png");
            }
            catch
            {
                this.Dispose();
                frm_Notice = new Form_Notice("Kiểm tra thư mục Resource!", false);
            }
        }

        private void UC_SuKien_HoSoThamDu_Load(object sender, EventArgs e)
        {
            LoadPic();

            list_IDV = new List<int>();
            list_NhomTrachVu = new List<string>();
            list_TrachVu = new List<string>();

            cbIDV_LLCN.Items.Add(" ");
            LayDSIDV_ComboBox(cbIDV_LLCN);

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

            list_dto = new List<SuKien_HoSo>();

            list_UC_HuanLuyen = new List<UC_HuanLuyen>();
            list_UC_HuanLuyen_Insert = new List<UC_HuanLuyen>();
            list_UC_HuanLuyen_Delete = new List<int>();
            list_UC_HuanLuyen_Update = new List<UC_HuanLuyen>();

            dto_HoSo = new HoSo();

            dto_SuKien_HoSo = new SuKien_HoSo();
            dto_SuKien_HuanLuyen = new SuKien_HuanLuyen();
            dto_SuKien_HoSo_HuanLuyen = new SuKien_HoSo_HuanLuyen();

            list_FolderAvatar = new List<string>();
            list_FolderAvatar.Add("DB");
            list_FolderAvatar.Add("Avatar");

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

            if (sSelect == "SỬA")
            {
                setSuKien_HoSoTheoMaSuKien_HoSo(sMaSuKien_HoSo);
            }

            if (bCopyHoSoCaNhanToHoSoThamDu)
            {
                setSuKien_HoSoTheoMaHoSo(sMaHoSo);
                if (!KiemTraHoSoCaNhanBiTrungHoSoThamDu(dto_HoSo))
                {
                    pbHoanTat_HL_Click(sender, e);
                }
                else
                {
                    this.Dispose();
                }
            }

            if (bImportHoSoClientToHoSoThamDu)
            {
                setSuKien_HoSoImport(dto_SuKien_HoSo_Import, list_SuKien_HuanLuyen);
                if (!KiemTraHoSoCaNhanBiTrungHoSoThamDu(dto_HoSo))
                {
                    pbHoanTat_HL_Click(sender, e);
                }
                else
                {
                    this.Dispose();
                }
            }

            this.Size = new System.Drawing.Size(1024, 600);

            pnLyLich.Size = new System.Drawing.Size(550, 545);
            pnLyLich.Location = SubFunction.SetCenterLocation(this.Size, pnLyLich.Size);

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

            pnNgayCapNhat.Left = pnLyLich.Left;
            pnNgayCapNhat.Top = pnLyLich.Top - 22;

            size_PicRec.Width = 90;
            size_PicRec.Height = 120;
            pnAvatar.Left = pnLyLich.Right + 5;
            pnAvatar.Top = pnLyLich.Top + 10;
        }
        #endregion



        #region Funtion
        private string NewMa()
        {
            if (list_dto.Count > 0)
            {
                sMaSuKien_HoSo = SubFunction.ThemMa4So(int.Parse(sMaSuKien_HoSo.Substring(0, 4))) + "SK" + iMaSuKien.ToString();

                return sMaSuKien_HoSo;
            }
            else
            {
                sMaSuKien_HoSo = "0001SK" + iMaSuKien.ToString();

                return sMaSuKien_HoSo;
            }
        }

        private void Cancel()
        {
            this.Dispose();
        }

        private bool LayDSIDV_ComboBox(ComboBox cb)
        {
            List<IDV> list_Temp = IDV_BUS.LayDSIDV();
            if (list_Temp.Count > 0)
            {
                list_IDV.Clear();
                cb.Items.Clear();
                cb.Items.Add(" ");
                for (int i = 0; i < list_Temp.Count; i++)
                {
                    list_IDV.Add(list_Temp[i].Ma);
                    cb.Items.Add(list_Temp[i].DienGiai);
                }

                return true;
            }
            else
            {
                return false;
            }
        }

        private bool LayDSNhomTrachVu_ComboBox(ComboBox cb)
        {
            List<NhomTrachVu> list_Temp = NhomTrachVu_BUS.LayDSNhomTrachVu();
            if (list_Temp.Count > 0)
            {
                list_NhomTrachVu.Clear();
                cb.Items.Clear();
                for (int i = 0; i < list_Temp.Count; i++)
                {
                    list_NhomTrachVu.Add(list_Temp[i].Ma);
                    cb.Items.Add(list_Temp[i].Ten);
                }

                return true;
            }
            else
            {
                return false;
            }
        }

        private bool LayDSTrachVu_ComboBox(ComboBox cb)
        {
            List<TrachVu> list_Temp = TrachVu_BUS.LayDSTrachVu();
            if (list_Temp.Count > 0)
            {
                list_TrachVu.Clear();
                cb.Items.Clear();
                for (int i = 0; i < list_Temp.Count; i++)
                {
                    list_TrachVu.Add(list_Temp[i].Ma);
                    cb.Items.Add(list_Temp[i].Ten);
                }

                return true;
            }
            else
            {
                return false;
            }
        }

        private bool LayDSTrachVuTheoMaNhomTrachVu_ComboBox(ComboBox cb, string sMaNhomTrachVu)
        {
            List<TrachVu> list_Temp = TrachVu_BUS.TraCuuDSTrachVuTheoMaNhomTrachVu(sMaNhomTrachVu);
            if (list_Temp != null)
            {
                list_TrachVu.Clear();
                cb.Items.Clear();
                for (int i = 0; i < list_Temp.Count; i++)
                {
                    list_TrachVu.Add(list_Temp[i].Ma);
                    cb.Items.Add(list_Temp[i].Ten);
                }

                return true;
            }
            else
            {
                Form_Notice frm = new Form_Notice("Kiểm tra Nhóm trách vụ bị trùng!", false);
                return false;
            }
        }

        private bool DeleteSuKien_HoSo(string sMa)
        {
            List<SuKien_HoSo_HuanLuyen> list_SuKien_HoSo_HuanLuyen = SuKien_HoSo_HuanLuyen_BUS.TraCuuDSSuKien_HuanLuyenTheoMaSuKien_HoSo(sMa);
            foreach (SuKien_HoSo_HuanLuyen dto_Temp in list_SuKien_HoSo_HuanLuyen)
            {
                int iTemp = dto_Temp.MaSuKien_HuanLuyen;
                if (!SuKien_HoSo_HuanLuyen_BUS.Delete(dto_Temp.MaSuKien_HoSo, dto_Temp.MaSuKien_HuanLuyen))
                {
                    return false;
                }

                if (!HuanLuyen_BUS.Delete(iTemp))
                {
                    return false;
                }
            }

            string sPath = "";
            sPath = Path.Combine(File_Function.getFinalFolder(list_FolderAvatar), setAvatarPath(sMa, SuKien_HoSo_BUS.TraCuuSuKien_HoSoTheoMa(sMa).NgayCapNhat));
            if (File.Exists(sPath))
            {
                File.Delete(sPath);
            }

            if (!SuKien_HoSo_BUS.Delete(sMa))
            {
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



        private bool KiemTraHoSoCaNhanBiTrungHoSoThamDu(HoSo dto_Temp)
        {
            List<SuKien_HoSo> list_Temp = SuKien_HoSo_BUS.TraCuuDSSuKien_HoSoTheoMaSuKien(iMaSuKien);
            for (int i = 0; i < list_Temp.Count; i++)
            {
                if (list_Temp[i].HoTen == dto_Temp.HoTen && list_Temp[i].NgaySinh == dto_Temp.NgaySinh && list_Temp[i].GioiTinh == dto_Temp.GioiTinh)
                {
                    string[] sTen = dto_Temp.HoTen.Split();
                    Form_Notice frm = new Form_Notice("Kiểm tra Hồ sơ trùng lắp:", sTen[sTen.Length - 1], false);

                    return true;
                }
            }

            return false;
        }



        #region Lay thong tin ho so tham du
        private void setSuKien_HoSoTheoMaSuKien_HoSo(string sMa)
        {
            dto_SuKien_HoSo = SuKien_HoSo_BUS.TraCuuSuKien_HoSoTheoMa(sMa);

            tbMa_LLCN.Text = sMa;

            if (dto_SuKien_HoSo.MaIDV == null)
            {
                cbIDV_LLCN.SelectedIndex = 0;
            }
            else
            {
                cbIDV_LLCN.Text = IDV_BUS.TraCuuIDVTheoMa((int)dto_SuKien_HoSo.MaIDV).DienGiai;
            }
            cbNhomTrachVu_LLCN.Text = NhomTrachVu_BUS.TraCuuNhomTrachVuTheoMa(dto_SuKien_HoSo.MaNhomTrachVu).Ten;
            cbTrachVu_LLCN.Text = TrachVu_BUS.TraCuuTrachVuTheoMa(dto_SuKien_HoSo.MaTrachVu).Ten;

            sAvatarPath = Path.Combine(File_Function.getFinalFolder(list_FolderAvatar), setAvatarPath(sMa, dto_SuKien_HoSo.NgayCapNhat));
            if (File.Exists(sAvatarPath))
            {
                string sImage = Convert_Function.ConvertByteArrayToString(Convert_Function.ConvertImageToByteArray(Image.FromFile(sAvatarPath)));
                pbAvatar.Image = Convert_Function.ConvertByteArrayToImage(Convert_Function.ConvertStringToByteArray(sImage));

                //pbAvatar.Image = Image.FromFile(sAvatarPath);
                bNewAvatar = true;
            }

            tbHoTen_LLCN.Text = dto_SuKien_HoSo.HoTen;
            dtpNgaySinh_LLCN.Value = (DateTime)dto_SuKien_HoSo.NgaySinh;

            if (dto_SuKien_HoSo.GioiTinh == "Nam")
            {
                rbNam.Checked = true;
            }

            if (dto_SuKien_HoSo.GioiTinh == "Nữ")
            {
                rbNu.Checked = true;
            }

            tbQueQuan_LLCN.Text = dto_SuKien_HoSo.QueQuan;
            tbTrinhDoHocVan_LLCN.Text = dto_SuKien_HoSo.TrinhDoHocVan;
            tbTonGiao_LLCN.Text = dto_SuKien_HoSo.TonGiao;
            tbDiaChi_LLCN.Text = dto_SuKien_HoSo.DiaChi;
            tbDienThoaiLienLac_LLCN.Text = dto_SuKien_HoSo.DienThoaiLienLac;
            tbEmail_LLCN.Text = dto_SuKien_HoSo.Email;

            if (dto_SuKien_HoSo.Nganh == "Ấu")
            {
                rbAu.Checked = true;
            }

            if (dto_SuKien_HoSo.Nganh == "Thiếu")
            {
                rbThieu.Checked = true;
            }

            if (dto_SuKien_HoSo.Nganh == "Kha")
            {
                rbKha.Checked = true;
            }

            if (dto_SuKien_HoSo.Nganh == "Tráng")
            {
                rbTrang.Checked = true;
            }

            if (dto_SuKien_HoSo.Nganh == "Khác")
            {
                rbKhac.Checked = true;
            }

            tbDonVi_LLHD.Text = dto_SuKien_HoSo.DonVi;
            tbLienDoan_LLHD.Text = dto_SuKien_HoSo.LienDoan;
            tbDao_LLHD.Text = dto_SuKien_HoSo.Dao;
            tbChau_LLHD.Text = dto_SuKien_HoSo.Chau;
            dtpNgayTuyenHua_LLHD.Value = (DateTime)dto_SuKien_HoSo.NgayTuyenHua;
            tbTruongNhanLoiHua_LLHD.Text = dto_SuKien_HoSo.TruongNhanLoiHua;
            tbTrachVuTaiDonVi_LLHD.Text = dto_SuKien_HoSo.TrachVuTaiDonVi;
            tbTrachVuNgoaiDonVi_LLHD.Text = dto_SuKien_HoSo.TrachVuNgoaiDonVi;
            tbTenRung_LLHD.Text = dto_SuKien_HoSo.TenRung;
            tbGhiChu_LLHD.Text = dto_SuKien_HoSo.GhiChu;

            tbNgheNghiep_NNKN.Text = dto_SuKien_HoSo.NgheNghiep;

            if (dto_SuKien_HoSo.NutDay == 1) //1
            {
                chbNutDay_NNKN.Checked = true;
            }

            if (dto_SuKien_HoSo.PhuongHuong == 1) //2
            {
                chbPhuongHuong_NNKN.Checked = true;
            }

            if (dto_SuKien_HoSo.CuuThuong == 1) //3
            {
                chbCuuThuong_NNKN.Checked = true;
            }

            if (dto_SuKien_HoSo.TruyenTin == 1) //4
            {
                chbTruyenTin_NNKN.Checked = true;
            }

            if (dto_SuKien_HoSo.TroChoi == 1) //5
            {
                chbTroChoi_NNKN.Checked = true;
            }

            if (dto_SuKien_HoSo.LuaTrai == 1) //6
            {
                chbLuaTrai_NNKN.Checked = true;
            }

            tbSoTruong_NNKN.Text = dto_SuKien_HoSo.SoTruong;

            List<SuKien_HoSo_HuanLuyen> list_SuKien_HoSo_HuanLuyen = SuKien_HoSo_HuanLuyen_BUS.TraCuuDSSuKien_HuanLuyenTheoMaSuKien_HoSo(sMa);
            foreach (SuKien_HoSo_HuanLuyen dto_Temp in list_SuKien_HoSo_HuanLuyen)
            {
                SuKien_HuanLuyen dto_SuKien_HuanLuyen_Temp = SuKien_HuanLuyen_BUS.TraCuuSuKien_HuanLuyenTheoMa(dto_Temp.MaSuKien_HuanLuyen);
                UC_HuanLuyen uc_HuanLuyen = new UC_HuanLuyen(dto_SuKien_HuanLuyen_Temp.Ma, dto_SuKien_HuanLuyen_Temp.Nganh, dto_SuKien_HuanLuyen_Temp.Khoa, dto_SuKien_HuanLuyen_Temp.TenKhoa, dto_SuKien_HuanLuyen_Temp.KhoaTruong, (DateTime)dto_SuKien_HuanLuyen_Temp.Nam, dto_SuKien_HuanLuyen_Temp.MHL, dto_SuKien_HuanLuyen_Temp.TinhTrang, true);

                int iNewLocation = list_UC_HuanLuyen.Count * 180 + pn_gbHuanLuyen.AutoScrollPosition.Y;
                uc_HuanLuyen.Location = new Point(8, iNewLocation);

                list_UC_HuanLuyen.Add(uc_HuanLuyen);
                pn_gbHuanLuyen.Controls.Add(list_UC_HuanLuyen[list_UC_HuanLuyen.Count - 1]);
                list_UC_HuanLuyen[list_UC_HuanLuyen.Count - 1].VisibleChanged += new EventHandler(AfterDeleteHL);

                list_UC_HuanLuyen_Update.Add(uc_HuanLuyen);
            }
        }
        #endregion



        #region Lay thong tin ho so import
        private void setSuKien_HoSoImport(SuKien_HoSo dto_SK_HS, List<SuKien_HuanLuyen> list_SK_HL)
        {
            if (dto_SK_HS.MaIDV == 0)
            {
                cbIDV_LLCN.SelectedIndex = 0;
            }
            else
            {
                cbIDV_LLCN.Text = IDV_BUS.TraCuuIDVTheoMa((int)dto_SK_HS.MaIDV).DienGiai;
            }
            cbNhomTrachVu_LLCN.Text = NhomTrachVu_BUS.TraCuuNhomTrachVuTheoMa(dto_SK_HS.MaNhomTrachVu).Ten;
            cbTrachVu_LLCN.Text = TrachVu_BUS.TraCuuTrachVuTheoMa(dto_SK_HS.MaTrachVu).Ten;

            sAvatarPath = Path.Combine(File_Function.getFinalFolder(list_FolderAvatar), setAvatarPath(sMaHoSo, dto_SK_HS.NgayCapNhat));
            if (File.Exists(sAvatarPath))
            {
                pbAvatar.Image = Image.FromFile(sAvatarPath);
                bNewAvatar = true;
            }

            tbHoTen_LLCN.Text = dto_SK_HS.HoTen;
            dtpNgaySinh_LLCN.Value = (DateTime)dto_SK_HS.NgaySinh;

            if (dto_SK_HS.GioiTinh == "Nam")
            {
                rbNam.Checked = true;
            }

            if (dto_SK_HS.GioiTinh == "Nữ")
            {
                rbNu.Checked = true;
            }

            tbQueQuan_LLCN.Text = dto_SK_HS.QueQuan;
            tbTrinhDoHocVan_LLCN.Text = dto_SK_HS.TrinhDoHocVan;
            tbTonGiao_LLCN.Text = dto_SK_HS.TonGiao;
            tbDiaChi_LLCN.Text = dto_SK_HS.DiaChi;
            tbDienThoaiLienLac_LLCN.Text = dto_SK_HS.DienThoaiLienLac;
            tbEmail_LLCN.Text = dto_SK_HS.Email;

            if (dto_SK_HS.Nganh == "Ấu")
            {
                rbAu.Checked = true;
            }

            if (dto_SK_HS.Nganh == "Thiếu")
            {
                rbThieu.Checked = true;
            }

            if (dto_SK_HS.Nganh == "Kha")
            {
                rbKha.Checked = true;
            }

            if (dto_SK_HS.Nganh == "Tráng")
            {
                rbTrang.Checked = true;
            }

            if (dto_SK_HS.Nganh == "Khác")
            {
                rbKhac.Checked = true;
            }

            tbDonVi_LLHD.Text = dto_SK_HS.DonVi;
            tbLienDoan_LLHD.Text = dto_SK_HS.LienDoan;
            tbDao_LLHD.Text = dto_SK_HS.Dao;
            tbChau_LLHD.Text = dto_SK_HS.Chau;
            dtpNgayTuyenHua_LLHD.Value = (DateTime)dto_SK_HS.NgayTuyenHua;
            tbTruongNhanLoiHua_LLHD.Text = dto_SK_HS.TruongNhanLoiHua;
            tbTrachVuTaiDonVi_LLHD.Text = dto_SK_HS.TrachVuTaiDonVi;
            tbTrachVuNgoaiDonVi_LLHD.Text = dto_SK_HS.TrachVuNgoaiDonVi;
            tbTenRung_LLHD.Text = dto_SK_HS.TenRung;
            tbGhiChu_LLHD.Text = dto_SK_HS.GhiChu;

            tbNgheNghiep_NNKN.Text = dto_SK_HS.NgheNghiep;

            if (dto_SK_HS.NutDay == 1) //1
            {
                chbNutDay_NNKN.Checked = true;
            }

            if (dto_SK_HS.PhuongHuong == 1) //2
            {
                chbPhuongHuong_NNKN.Checked = true;
            }

            if (dto_SK_HS.CuuThuong == 1) //3
            {
                chbCuuThuong_NNKN.Checked = true;
            }

            if (dto_SK_HS.TruyenTin == 1) //4
            {
                chbTruyenTin_NNKN.Checked = true;
            }

            if (dto_SK_HS.TroChoi == 1) //5
            {
                chbTroChoi_NNKN.Checked = true;
            }

            if (dto_SK_HS.LuaTrai == 1) //6
            {
                chbLuaTrai_NNKN.Checked = true;
            }

            tbSoTruong_NNKN.Text = dto_SK_HS.SoTruong;

            //List<SuKien_HoSo_HuanLuyen> list_SuKien_HoSo_HuanLuyen = SuKien_HoSo_HuanLuyen_BUS.TraCuuDSSuKien_HuanLuyenTheoMaSuKien_HoSo(sMa);
            foreach (SuKien_HuanLuyen dto_SuKien_HuanLuyen_Temp in list_SK_HL)
            {
                //SuKien_HuanLuyen dto_SuKien_HuanLuyen_Temp = SuKien_HuanLuyen_BUS.TraCuuSuKien_HuanLuyenTheoMa(dto_Temp.MaSuKien_HuanLuyen);
                UC_HuanLuyen uc_HuanLuyen = new UC_HuanLuyen(dto_SuKien_HuanLuyen_Temp.Ma, dto_SuKien_HuanLuyen_Temp.Nganh, dto_SuKien_HuanLuyen_Temp.Khoa, dto_SuKien_HuanLuyen_Temp.TenKhoa, dto_SuKien_HuanLuyen_Temp.KhoaTruong, (DateTime)dto_SuKien_HuanLuyen_Temp.Nam, dto_SuKien_HuanLuyen_Temp.MHL, dto_SuKien_HuanLuyen_Temp.TinhTrang, true);

                int iNewLocation = list_UC_HuanLuyen.Count * 180 + pn_gbHuanLuyen.AutoScrollPosition.Y;
                uc_HuanLuyen.Location = new Point(8, iNewLocation);

                list_UC_HuanLuyen.Add(uc_HuanLuyen);
                pn_gbHuanLuyen.Controls.Add(list_UC_HuanLuyen[list_UC_HuanLuyen.Count - 1]);
                list_UC_HuanLuyen[list_UC_HuanLuyen.Count - 1].VisibleChanged += new EventHandler(AfterDeleteHL);

                list_UC_HuanLuyen_Update.Add(uc_HuanLuyen);
            }
        }
        #endregion



        #region Lay thong tin tu ho so ca nhan de copy
        private void setSuKien_HoSoTheoMaHoSo(string sMa)
        {
            dto_HoSo = HoSo_BUS.TraCuuHoSoTheoMa(sMa);

            //tbMa_LLCN.Text = sMa;

            if (dto_HoSo.MaIDV == null)
            {
                cbIDV_LLCN.SelectedIndex = 0;
            }
            else
            {
                cbIDV_LLCN.Text = IDV_BUS.TraCuuIDVTheoMa((int)dto_HoSo.MaIDV).DienGiai;
            }
            cbNhomTrachVu_LLCN.Text = NhomTrachVu_BUS.TraCuuNhomTrachVuTheoMa(dto_HoSo.MaNhomTrachVu).Ten;
            cbTrachVu_LLCN.Text = TrachVu_BUS.TraCuuTrachVuTheoMa(dto_HoSo.MaTrachVu).Ten;

            sAvatarPath = Path.Combine(File_Function.getFinalFolder(list_FolderAvatar), setAvatarPath(sMa, dto_HoSo.NgayCapNhat));
            if (File.Exists(sAvatarPath))
            {
                pbAvatar.Image = Image.FromFile(sAvatarPath);
                bNewAvatar = true;
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

            List<HoSo_HuanLuyen> list_HoSo_HuanLuyen = HoSo_HuanLuyen_BUS.TraCuuDSHuanLuyenTheoMaHoSo(sMa);
            foreach (HoSo_HuanLuyen dto_Temp in list_HoSo_HuanLuyen)
            {
                HuanLuyen dto_HuanLuyen_Temp = HuanLuyen_BUS.TraCuuHuanLuyenTheoMa(dto_Temp.MaHuanLuyen);
                UC_HuanLuyen uc_HuanLuyen = new UC_HuanLuyen(dto_HuanLuyen_Temp.Ma, dto_HuanLuyen_Temp.Nganh, dto_HuanLuyen_Temp.Khoa, dto_HuanLuyen_Temp.TenKhoa, dto_HuanLuyen_Temp.KhoaTruong, (DateTime)dto_HuanLuyen_Temp.Nam, dto_HuanLuyen_Temp.MHL, dto_HuanLuyen_Temp.TinhTrang, true);

                int iNewLocation = list_UC_HuanLuyen.Count * 180 + pn_gbHuanLuyen.AutoScrollPosition.Y;
                uc_HuanLuyen.Location = new Point(8, iNewLocation);

                list_UC_HuanLuyen.Add(uc_HuanLuyen);
                pn_gbHuanLuyen.Controls.Add(list_UC_HuanLuyen[list_UC_HuanLuyen.Count - 1]);
                list_UC_HuanLuyen[list_UC_HuanLuyen.Count - 1].VisibleChanged += new EventHandler(AfterDeleteHL);

                list_UC_HuanLuyen_Update.Add(uc_HuanLuyen);
            }
        }
        #endregion
        #endregion



        #region LLCN
        private void cbIDV_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tbMa_LLCN.Text.Length > 0)
            {
                if (cbIDV_LLCN.SelectedIndex > 0)
                {
                    tbMa_LLCN.Text = sMaSuKien_HoSo + IDV_BUS.TraCuuIDVTheoMa(list_IDV[cbIDV_LLCN.SelectedIndex - 1]).IDV1;
                }
                else
                {
                    tbMa_LLCN.Text = sMaSuKien_HoSo;
                }
            }
        }

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
                if (sSelect == "SỬA")
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
        private void NewSuKien_HoSo()
        {
            dto_SuKien_HoSo = new SuKien_HoSo();

            dto_SuKien_HoSo.Ma = sMaSuKien_HoSo;
            dto_SuKien_HoSo.NgayCapNhat = sNgayCapNhat;

            if (cbIDV_LLCN.SelectedIndex > 0)
            {
                dto_SuKien_HoSo.MaIDV = list_IDV[cbIDV_LLCN.SelectedIndex - 1];
            }

            dto_SuKien_HoSo.MaNhomTrachVu = list_NhomTrachVu[cbNhomTrachVu_LLCN.SelectedIndex];
            dto_SuKien_HoSo.MaTrachVu = list_TrachVu[cbTrachVu_LLCN.SelectedIndex];

            dto_SuKien_HoSo.HoTen = tbHoTen_LLCN.Text;
            dto_SuKien_HoSo.NgaySinh = dtpNgaySinh_LLCN.Value;

            if (rbNam.Checked)
            {
                dto_SuKien_HoSo.GioiTinh = rbNam.Text;
            }

            if (rbNu.Checked)
            {
                dto_SuKien_HoSo.GioiTinh = rbNu.Text;
            }

            dto_SuKien_HoSo.QueQuan = tbQueQuan_LLCN.Text;
            dto_SuKien_HoSo.TrinhDoHocVan = tbTrinhDoHocVan_LLCN.Text;
            dto_SuKien_HoSo.TonGiao = tbTonGiao_LLCN.Text;
            dto_SuKien_HoSo.DiaChi = tbDiaChi_LLCN.Text;
            dto_SuKien_HoSo.DienThoaiLienLac = tbDienThoaiLienLac_LLCN.Text;
            dto_SuKien_HoSo.Email = tbEmail_LLCN.Text;

            if (rbAu.Checked)
            {
                dto_SuKien_HoSo.Nganh = rbAu.Text;
            }

            if (rbThieu.Checked)
            {
                dto_SuKien_HoSo.Nganh = rbThieu.Text;
            }

            if (rbKha.Checked)
            {
                dto_SuKien_HoSo.Nganh = rbKha.Text;
            }

            if (rbTrang.Checked)
            {
                dto_SuKien_HoSo.Nganh = rbTrang.Text;
            }

            if (rbKhac.Checked)
            {
                dto_SuKien_HoSo.Nganh = rbKhac.Text;
            }

            dto_SuKien_HoSo.DonVi = tbDonVi_LLHD.Text;
            dto_SuKien_HoSo.LienDoan = tbLienDoan_LLHD.Text;
            dto_SuKien_HoSo.Dao = tbDao_LLHD.Text;
            dto_SuKien_HoSo.Chau = tbChau_LLHD.Text;
            dto_SuKien_HoSo.NgayTuyenHua = dtpNgayTuyenHua_LLHD.Value;
            dto_SuKien_HoSo.TruongNhanLoiHua = tbTruongNhanLoiHua_LLHD.Text;
            dto_SuKien_HoSo.TrachVuTaiDonVi = tbTrachVuTaiDonVi_LLHD.Text;
            dto_SuKien_HoSo.TrachVuNgoaiDonVi = tbTrachVuNgoaiDonVi_LLHD.Text;
            dto_SuKien_HoSo.TenRung = tbTenRung_LLHD.Text;
            dto_SuKien_HoSo.GhiChu = tbGhiChu_LLHD.Text;

            dto_SuKien_HoSo.NgheNghiep = tbNgheNghiep_NNKN.Text;
            if (chbNutDay_NNKN.Checked) //1
            {
                dto_SuKien_HoSo.NutDay = 1;
            }
            else
            {
                dto_SuKien_HoSo.NutDay = 0;
            }

            if (chbPhuongHuong_NNKN.Checked) //2
            {
                dto_SuKien_HoSo.PhuongHuong = 1;
            }
            else
            {
                dto_SuKien_HoSo.PhuongHuong = 0;
            }

            if (chbCuuThuong_NNKN.Checked) //3
            {
                dto_SuKien_HoSo.CuuThuong = 1;
            }
            else
            {
                dto_SuKien_HoSo.CuuThuong = 0;
            }

            if (chbTruyenTin_NNKN.Checked) //4
            {
                dto_SuKien_HoSo.TruyenTin = 1;
            }
            else
            {
                dto_SuKien_HoSo.TruyenTin = 0;
            }

            if (chbTroChoi_NNKN.Checked) //5
            {
                dto_SuKien_HoSo.TroChoi = 1;
            }
            else
            {
                dto_SuKien_HoSo.TroChoi = 0;
            }

            if (chbLuaTrai_NNKN.Checked) //6
            {
                dto_SuKien_HoSo.LuaTrai = 1;
            }
            else
            {
                dto_SuKien_HoSo.LuaTrai = 0;
            }

            dto_SuKien_HoSo.SoTruong = tbSoTruong_NNKN.Text;
        }

        private bool InsertSuKien_HuanLuyen(List<UC_HuanLuyen> list_UC_Temp)
        {
            for (int i = 0; i < list_UC_Temp.Count; i++)
            {
                dto_SuKien_HuanLuyen = new SuKien_HuanLuyen();

                dto_SuKien_HuanLuyen.Nganh = list_UC_Temp[i].Nganh;
                dto_SuKien_HuanLuyen.Khoa = list_UC_Temp[i].Khoa;
                dto_SuKien_HuanLuyen.TenKhoa = list_UC_Temp[i].TenKhoa;
                dto_SuKien_HuanLuyen.KhoaTruong = list_UC_Temp[i].KhoaTruong;
                dto_SuKien_HuanLuyen.Nam = list_UC_Temp[i].Nam;
                dto_SuKien_HuanLuyen.MHL = list_UC_Temp[i].MHL;
                dto_SuKien_HuanLuyen.TinhTrang = list_UC_Temp[i].TinhTrang;

                if (SuKien_HuanLuyen_BUS.Insert(dto_SuKien_HuanLuyen))
                {
                    dto_SuKien_HoSo_HuanLuyen = new SuKien_HoSo_HuanLuyen();

                    dto_SuKien_HoSo_HuanLuyen.MaSuKien_HoSo = sMaSuKien_HoSo;
                    List<SuKien_HuanLuyen> list_Temp = SuKien_HuanLuyen_BUS.LayDSSuKien_HuanLuyen();
                    dto_SuKien_HoSo_HuanLuyen.MaSuKien_HuanLuyen = list_Temp[list_Temp.Count - 1].Ma;

                    if (!SuKien_HoSo_HuanLuyen_BUS.Insert(dto_SuKien_HoSo_HuanLuyen))
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
        


        #region Update dto
        private bool UpdateSuKien_HuanLuyen()
        {
            if (!InsertSuKien_HuanLuyen(list_UC_HuanLuyen_Insert))
            {
                return false;
            }

            for (int i = 0; i < list_UC_HuanLuyen_Delete.Count; i++)
            {
                if (!SuKien_HoSo_HuanLuyen_BUS.Delete(sMaSuKien_HoSo, list_UC_HuanLuyen_Delete[i]))
                {
                    return false;
                }

                if (!SuKien_HuanLuyen_BUS.Delete(list_UC_HuanLuyen_Delete[i]))
                {
                    return false;
                }
            }

            for (int i = 0; i < list_UC_HuanLuyen_Update.Count; i++)
            {
                dto_SuKien_HuanLuyen = new SuKien_HuanLuyen();

                dto_SuKien_HuanLuyen.Ma = list_UC_HuanLuyen_Update[i].Ma;
                dto_SuKien_HuanLuyen.Nganh = list_UC_HuanLuyen_Update[i].Nganh;
                dto_SuKien_HuanLuyen.Khoa = list_UC_HuanLuyen_Update[i].Khoa;
                dto_SuKien_HuanLuyen.TenKhoa = list_UC_HuanLuyen_Update[i].TenKhoa;
                dto_SuKien_HuanLuyen.KhoaTruong = list_UC_HuanLuyen_Update[i].KhoaTruong;
                dto_SuKien_HuanLuyen.Nam = list_UC_HuanLuyen_Update[i].Nam;
                dto_SuKien_HuanLuyen.MHL = list_UC_HuanLuyen_Update[i].MHL;
                dto_SuKien_HuanLuyen.TinhTrang = list_UC_HuanLuyen_Update[i].TinhTrang;

                if (!SuKien_HuanLuyen_BUS.UpdateSuKien_HuanLuyenInfo(dto_SuKien_HuanLuyen))
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
            if (sSelect == "THÊM")
            {
                NewSuKien_HoSo();

                if (SuKien_HoSo_BUS.Insert(dto_SuKien_HoSo))
                {
                    if (!gbHuanLuyen.Visible)
                    {
                        gbHuanLuyen.Visible = true;
                    }

                    if (InsertSuKien_HuanLuyen(list_UC_HuanLuyen))
                    {
                        SuKien_HoSoThamDu Temp = new SuKien_HoSoThamDu();
                        Temp.MaSuKien = iMaSuKien;
                        Temp.MaSuKien_HoSo = sMaSuKien_HoSo;

                        if (SuKien_HoSoThamDu_BUS.Insert(Temp))
                        {
                            if (bNewAvatar)
                            {
                                if (!File_Function.savePic(list_FolderAvatar, setAvatarPath(sMaSuKien_HoSo, sNgayCapNhat), (Bitmap)pbAvatar.Image))
                                {
                                    frm_Notice = new Form_Notice("Kiểm tra Avatar của hồ sơ đang mở!", false);
                                }
                            }

                            Cancel();
                        }
                        else
                        {
                            Form_Notice frm = new Form_Notice("Không thể tạo Hồ Sơ!", false);
                        }
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

            if (sSelect == "SỬA")
            {
                sNgayCapNhatTruoc = dto_SuKien_HoSo.NgayCapNhat;
                NewSuKien_HoSo();

                if (SuKien_HoSo_BUS.UpdateSuKien_HoSoInfo(dto_SuKien_HoSo))
                {
                    if (UpdateSuKien_HuanLuyen())
                    {
                        if (bNewAvatar)
                        {
                            if (!File_Function.savePic(list_FolderAvatar, setAvatarPath(sMaSuKien_HoSo, sNgayCapNhat), (Bitmap)pbAvatar.Image))
                            {
                                frm_Notice = new Form_Notice("Kiểm tra Avatar của hồ sơ đang mở!", false);
                            }
                        }

                        setAvatarPath(sMaSuKien_HoSo, sNgayCapNhatTruoc);
                        sAvatarPath = Path.Combine(File_Function.getFinalFolder(list_FolderAvatar), sAvatarPath);

                        pbAvatar.Visible = false;
                        if (File.Exists(sAvatarPath))
                        {
                            //try
                            //{
                            //    imgAvatar.Dispose();
                            //}
                            //catch
                            //{ 
                            //    //khong lam gi het
                            //}

                            //try
                            //{
                            //    imgZoom.Dispose();
                            //}
                            //catch
                            //{
                            //    //khong lam gi het
                            //}

                            //try
                            //{
                            //    pbAvatar.Image.Dispose();
                            //}
                            //catch
                            //{
                            //    //khong lam gi het
                            //}

                            try
                            {
                                File.Delete(sAvatarPath);
                            }
                            catch
                            {
                                MessageBox.Show("Error");
                                //khong lam gi het
                            }
                        }

                        Cancel();
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
    }
}
