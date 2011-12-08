using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Function;
using DAO;
using BUS;
using System.IO;


namespace VNSC
{
    public partial class Form_Detail : Form
    {
        private List<int> list_IDV;
        private List<int> list_MaLuuTru;
        private string sMaHoSo;
        private int dMaLuuTru;

        private List<string> list_FolderAvatar;

        public Form_Detail()
        {
            InitializeComponent();
        }

        public Form_Detail(string sMa)
        {
            InitializeComponent();

            LoadPic();
            this.Size = new System.Drawing.Size(800, 600);

            pnLyLich.Size = new System.Drawing.Size(550, 550);
            pnLyLich.Location = new Point(240, 35);

            gbLyLichCaNhan.Size = new System.Drawing.Size(550, 550);
            gbLyLichCaNhan.Location = SubFunction.SetWidthCenter(pnLyLich.Size, gbLyLichCaNhan.Size, 0);

            gbLyLichHuongDao.Size = new System.Drawing.Size(550, 550);
            gbLyLichHuongDao.Location = SubFunction.SetWidthCenter(pnLyLich.Size, gbLyLichHuongDao.Size, 0);

            gbNgheNghiep_KiNang.Size = new System.Drawing.Size(550, 550);
            gbNgheNghiep_KiNang.Location = SubFunction.SetWidthCenter(pnLyLich.Size, gbNgheNghiep_KiNang.Size, 0);

            gbHuanLuyen.Size = new System.Drawing.Size(550, 550);
            gbHuanLuyen.Location = SubFunction.SetWidthCenter(pnLyLich.Size, gbHuanLuyen.Size, 0);

            pn_gbHuanLuyen.Size = new System.Drawing.Size(540, 430);
            pn_gbHuanLuyen.Location = SubFunction.SetWidthCenter(gbHuanLuyen.Size, pn_gbHuanLuyen.Size, 50);

            pbIcon_Search.Location = new Point(20, 190);

            list_IDV = new List<int>();
            list_MaLuuTru = new List<int>();

            list_FolderAvatar = new List<string>();
            list_FolderAvatar.Add("DB");
            list_FolderAvatar.Add("Avatar");

            sMaHoSo = sMa;

            LayDSIDV_ComboBox(cbIDV_LLCN);
            LayDSNhomTrachVu_ComboBox(cbNhomTrachVu_LLCN);

            setLuuTruTheoMaHoSo(sMaHoSo);
            setHoSoTheoMaHoSo(sMaHoSo);
            this.ShowDialog();
        }

        private void setLuuTruTheoMaHoSo(string sMa)
        {
            List<HoSo_LuuTru> list_HoSo_LuuTru = HoSo_LuuTru_BUS.TraCuuDSLuuTruTheoMaHoSo(sMa);
            lvLuuTru.Items.Clear();
            list_MaLuuTru.Clear();
            foreach (HoSo_LuuTru dto_Temp in list_HoSo_LuuTru)
            {
                list_MaLuuTru.Add(dto_Temp.MaLuuTru);
                lvLuuTru.Items.Add(LuuTru_BUS.TraCuuLuuTruTheoMa(dto_Temp.MaLuuTru).NgayCapNhat.Substring(0, 18) + LuuTru_BUS.TraCuuLuuTruTheoMa(dto_Temp.MaLuuTru).NgayCapNhat.Substring(21));
            }

            if (list_MaLuuTru.Count == 0)
            {
                pbHistory.Visible = false;
                pbIcon_Search.Visible = true;
                lvLuuTru.Visible = false;
                pbDelete.Visible = false;
                lbHistory.Visible = false;

                setHoSoTheoMaHoSo(sMaHoSo);
            }

            tbHoTen_LLCN.Select();
        }

        private void LoadPic()
        {
            try
            {
                pbTiepTuc_LLCN.Image = Image.FromFile(@"Resources\ChucNang\forward.png");

                pbTiepTuc_LLHD.Image = Image.FromFile(@"Resources\ChucNang\forward.png");
                pbTroVe_LLHD.Image = Image.FromFile(@"Resources\ChucNang\back.png");

                pbTroVe_HL.Image = Image.FromFile(@"Resources\ChucNang\back.png");

                pbTiepTuc_NNKN.Image = Image.FromFile(@"Resources\ChucNang\forward.png");
                pbTroVe_NNKN.Image = Image.FromFile(@"Resources\ChucNang\back.png");

                pbAvatar.Image = Image.FromFile(@"Resources\NhanSu\avatar.png");
                pbBackHome.Image = Image.FromFile(@"Resources\ChucNang\icon_home.png");
                pbIcon_Search.Image = Image.FromFile(@"Resources\NhanSu\icon_mag_chitiethoso.png");

                pbDelete.Image = Image.FromFile(@"Resources\ChucNang\delete.png");
                pbHistory.Image = Image.FromFile(@"Resources\NhanSu\icon_history.png");
            }
            catch
            {
                this.Dispose();
                Form_Notice frm_Notice = new Form_Notice("Kiểm tra thư mục Resource!", false);
            }
        }

        private void Form_Detail_Load(object sender, EventArgs e)
        {
            //
        }



        #region Lay thong tin ho so
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
            List<NhomTrachVu> listTemp = NhomTrachVu_BUS.LayDSNhomTrachVu();
            if (listTemp.Count > 0)
            {
                cb.Items.Clear();
                for (int i = 0; i < listTemp.Count; i++)
                {
                    cb.Items.Add(listTemp[i].Ten);
                }

                return true;
            }
            else
            {
                return false;
            }
        }

        private bool LayDSTrachVuTheoTenNhomTrachVu_ComboBox(ComboBox cb, string sTenNhomTrachVu)
        {
            List<TrachVu> listTemp = TrachVu_BUS.TraCuuDSTrachVuTheoTenNhomTrachVu(sTenNhomTrachVu);
            if (listTemp.Count > 0)
            {
                cb.Items.Clear();
                for (int i = 0; i < listTemp.Count; i++)
                {
                    cb.Items.Add(listTemp[i].Ten);
                }

                return true;
            }
            else
            {
                Form_Notice frm = new Form_Notice("Kiểm tra Nhóm trách vụ bị trùng!", false);
                return false;
            }
        }

        private string getDateAvatar(string sDate)
        {
            string sTemp = "";

            if (sDate.Length > 23) //22/02/2222 - 22:22:22 Chieu
            {
                if (sDate.EndsWith("Chiều"))
                {
                    sTemp = sDate.Substring(0, 2) + sDate.Substring(3, 2) + sDate.Substring(6, 4) + "_" + sDate.Substring(13, 2) + sDate.Substring(16, 2) + sDate.Substring(19, 2) + "CH";
                }
                else
                {
                    sTemp = sDate.Substring(0, 2) + sDate.Substring(3, 2) + sDate.Substring(6, 4) + "_" + sDate.Substring(13, 2) + sDate.Substring(16, 2) + sDate.Substring(19, 2) + "SA";
                }
            }

            return sTemp;
        }

        private void setHoSoTheoMaHoSo(string sMa)
        {
            HoSo dto_HoSo = HoSo_BUS.TraCuuHoSoTheoMa(sMa);

            tbMa_LLCN.Text = sMa;

            if (dto_HoSo.MaIDV != null)
            {
                cbIDV_LLCN.Text = IDV_BUS.TraCuuIDVTheoMa((int)dto_HoSo.MaIDV).DienGiai;
            }

            cbNhomTrachVu_LLCN.Text = NhomTrachVu_BUS.TraCuuNhomTrachVuTheoMa(dto_HoSo.MaNhomTrachVu).Ten;
            LayDSTrachVuTheoTenNhomTrachVu_ComboBox(cbTrachVu_LLCN, cbNhomTrachVu_LLCN.Text);
            cbTrachVu_LLCN.Text = TrachVu_BUS.TraCuuTrachVuTheoMa(dto_HoSo.MaTrachVu).Ten;

            string sAvatarPath = Path.Combine(File_Function.getFinalFolder(list_FolderAvatar), tbMa_LLCN.Text.Substring(0, 4) + "_" + getDateAvatar(dto_HoSo.NgayCapNhat) + ".jpg");
            if (File.Exists(sAvatarPath))
            {
                pbAvatar.Image = Image.FromFile(sAvatarPath);
            }

            lbNgayCapNhat.Text = dto_HoSo.NgayCapNhat.Substring(0, 18) + dto_HoSo.NgayCapNhat.Substring(21);

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
            tbTracVuTaiDonVi_LLHD.Text = dto_HoSo.TrachVuTaiDonVi;
            tbTracVuNgoaiDonVi_LLHD.Text = dto_HoSo.TrachVuNgoaiDonVi;
            tbTenRung_LLHD.Text = dto_HoSo.TenRung;
            tbGhiChu_LLHD.Text = dto_HoSo.GhiChu;

            tbNgheNghiep_NNKN.Text = dto_HoSo.NgheNghiep;

            if (dto_HoSo.NutDay == 1) //1
            {
                chbNutDay_NNKN.Checked = true;
            }
            else
            {
                chbNutDay_NNKN.Checked = false;
            }

            if (dto_HoSo.PhuongHuong == 1) //2
            {
                chbPhuongHuong_NNKN.Checked = true;
            }
            else
            {
                chbPhuongHuong_NNKN.Checked = false;
            }

            if (dto_HoSo.CuuThuong == 1) //3
            {
                chbCuuThuong_NNKN.Checked = true;
            }
            else
            {
                chbCuuThuong_NNKN.Checked = false;
            }

            if (dto_HoSo.TruyenTin == 1) //4
            {
                chbTruyenTin_NNKN.Checked = true;
            }
            else
            {
                chbTruyenTin_NNKN.Checked = false;
            }

            if (dto_HoSo.TroChoi == 1) //5
            {
                chbTroChoi_NNKN.Checked = true;
            }
            else
            {
                chbTroChoi_NNKN.Checked = false;
            }

            if (dto_HoSo.LuaTrai == 1) //6
            {
                chbLuaTrai_NNKN.Checked = true;
            }
            else
            {
                chbLuaTrai_NNKN.Checked = false;
            }

            tbSoTruong_NNKN.Text = dto_HoSo.SoTruong;

            List<HoSo_HuanLuyen> list_HoSo_HuanLuyen = HoSo_HuanLuyen_BUS.TraCuuDSHuanLuyenTheoMaHoSo(tbMa_LLCN.Text);
            List<UC_HuanLuyen> list_UC_HuanLuyen = new List<UC_HuanLuyen>();
            foreach (HoSo_HuanLuyen dto_Temp in list_HoSo_HuanLuyen)
            {
                HuanLuyen dto_HuanLuyen_Temp = HuanLuyen_BUS.TraCuuHuanLuyenTheoMa(dto_Temp.MaHuanLuyen);
                UC_HuanLuyen uc_HuanLuyen = new UC_HuanLuyen(dto_HuanLuyen_Temp.Ma, dto_HuanLuyen_Temp.Nganh, dto_HuanLuyen_Temp.Khoa, dto_HuanLuyen_Temp.TenKhoa, dto_HuanLuyen_Temp.KhoaTruong, (DateTime)dto_HuanLuyen_Temp.Nam, dto_HuanLuyen_Temp.MHL, dto_HuanLuyen_Temp.TinhTrang, false);

                int iNewLocation = list_UC_HuanLuyen.Count * 180 + pn_gbHuanLuyen.AutoScrollPosition.Y;
                uc_HuanLuyen.Location = new Point(8, iNewLocation);

                list_UC_HuanLuyen.Add(uc_HuanLuyen);
                pn_gbHuanLuyen.Controls.Add(list_UC_HuanLuyen[list_UC_HuanLuyen.Count - 1]);
            }

            tbHoTen_LLCN.Select();
        }
        #endregion



        #region Luu tru
        private void setHoSoTheoMaLuuTru(int dMaLuuTru)
        {
            LuuTru dto_HoSo = LuuTru_BUS.TraCuuLuuTruTheoMa(dMaLuuTru);

            if (dto_HoSo.MaIDV != null)
            {
                cbIDV_LLCN.Text = IDV_BUS.TraCuuIDVTheoMa((int)dto_HoSo.MaIDV).DienGiai;
            }

            cbNhomTrachVu_LLCN.Text = NhomTrachVu_BUS.TraCuuNhomTrachVuTheoMa(dto_HoSo.MaNhomTrachVu).Ten;
            LayDSTrachVuTheoTenNhomTrachVu_ComboBox(cbTrachVu_LLCN, cbNhomTrachVu_LLCN.Text);
            cbTrachVu_LLCN.Text = TrachVu_BUS.TraCuuTrachVuTheoMa(dto_HoSo.MaTrachVu).Ten;

            lbNgayCapNhat.Text = dto_HoSo.NgayCapNhat.Substring(0, 18) + dto_HoSo.NgayCapNhat.Substring(21);

            string sAvatarPath = Path.Combine(File_Function.getFinalFolder(list_FolderAvatar), tbMa_LLCN.Text.Substring(0, 4) + "_" + getDateAvatar(dto_HoSo.NgayCapNhat) + ".jpg");
            if (File.Exists(sAvatarPath))
            {
                pbAvatar.Image = Image.FromFile(sAvatarPath);
            }
            else
            {
                pbAvatar.Image = Image.FromFile(@"Resources\NhanSu\avatar.png");
            }

            tbHoTen_LLCN.Text = dto_HoSo.HoTen;
            dtpNgaySinh_LLCN.Value = (DateTime)dto_HoSo.NgaySinh;
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
            tbTracVuTaiDonVi_LLHD.Text = dto_HoSo.TrachVuTaiDonVi;
            tbTracVuNgoaiDonVi_LLHD.Text = dto_HoSo.TrachVuNgoaiDonVi;
            tbGhiChu_LLHD.Text = dto_HoSo.GhiChu;

            tbNgheNghiep_NNKN.Text = dto_HoSo.NgheNghiep;

            if (dto_HoSo.NutDay == 1) //1
            {
                chbNutDay_NNKN.Checked = true;
            }
            else
            {
                chbNutDay_NNKN.Checked = false;
            }

            if (dto_HoSo.PhuongHuong == 1) //2
            {
                chbPhuongHuong_NNKN.Checked = true;
            }
            else
            {
                chbPhuongHuong_NNKN.Checked = false;
            }

            if (dto_HoSo.CuuThuong == 1) //3
            {
                chbCuuThuong_NNKN.Checked = true;
            }
            else
            {
                chbCuuThuong_NNKN.Checked = false;
            }

            if (dto_HoSo.TruyenTin == 1) //4
            {
                chbTruyenTin_NNKN.Checked = true;
            }
            else
            {
                chbTruyenTin_NNKN.Checked = false;
            }

            if (dto_HoSo.TroChoi == 1) //5
            {
                chbTroChoi_NNKN.Checked = true;
            }
            else
            {
                chbTroChoi_NNKN.Checked = false;
            }

            if (dto_HoSo.LuaTrai == 1) //6
            {
                chbLuaTrai_NNKN.Checked = true;
            }
            else
            {
                chbLuaTrai_NNKN.Checked = false;
            }

            tbSoTruong_NNKN.Text = dto_HoSo.SoTruong;

            List<HoSo_HuanLuyen> list_HoSo_HuanLuyen = HoSo_HuanLuyen_BUS.TraCuuDSHuanLuyenTheoMaHoSo(tbMa_LLCN.Text);
            List<UC_HuanLuyen> list_UC_HuanLuyen = new List<UC_HuanLuyen>();
            foreach (HoSo_HuanLuyen dto_Temp in list_HoSo_HuanLuyen)
            {
                HuanLuyen dto_HuanLuyen_Temp = HuanLuyen_BUS.TraCuuHuanLuyenTheoMa(dto_Temp.MaHuanLuyen);
                UC_HuanLuyen uc_HuanLuyen = new UC_HuanLuyen(dto_HuanLuyen_Temp.Ma, dto_HuanLuyen_Temp.Nganh, dto_HuanLuyen_Temp.Khoa, dto_HuanLuyen_Temp.TenKhoa, dto_HuanLuyen_Temp.KhoaTruong, (DateTime)dto_HuanLuyen_Temp.Nam, dto_HuanLuyen_Temp.MHL, dto_HuanLuyen_Temp.TinhTrang, false);

                int iNewLocation = list_UC_HuanLuyen.Count * 180 + pn_gbHuanLuyen.AutoScrollPosition.Y;
                uc_HuanLuyen.Location = new Point(8, iNewLocation);

                list_UC_HuanLuyen.Add(uc_HuanLuyen);
                pn_gbHuanLuyen.Controls.Add(list_UC_HuanLuyen[list_UC_HuanLuyen.Count - 1]);
            }
        }
        #endregion



        //#region Luu tru
        //private void setHoSoTheoNgayCapNhat(string sNgayCapNhat)
        //{
        //    LuuTru dto_HoSo = LuuTru_BUS.TraCuuLuuTruTheoNgay(sNgayCapNhat);

        //    LayDSNhomTrachVu_ComboBox(cbNhomTrachVu_LLCN);
        //    cbNhomTrachVu_LLCN.Text = NhomTrachVu_BUS.TraCuuNhomTrachVuTheoMa(dto_HoSo.MaNhomTrachVu).Ten;
        //    LayDSTrachVuTheoTenNhomTrachVu_ComboBox(cbTrachVu_LLCN, cbNhomTrachVu_LLCN.Text);
        //    cbTrachVu_LLCN.Text = TrachVu_BUS.TraCuuTrachVuTheoMa(dto_HoSo.MaTrachVu).Ten;

        //    lbNgayCapNhat.Text = dto_HoSo.NgayCapNhat.Substring(0, 17) + dto_HoSo.NgayCapNhat.Substring(20);

        //    //dto_HoSo.Avatar = pbAvatar.Image;

        //    tbHoTen_LLCN.Text = dto_HoSo.HoTen;
        //    dtpNgaySinh_LLCN.Value = (DateTime)dto_HoSo.NgaySinh;
        //    tbQueQuan_LLCN.Text = dto_HoSo.QueQuan;
        //    tbTrinhDoHocVan_LLCN.Text = dto_HoSo.TrinhDoHocVan;
        //    tbTonGiao_LLCN.Text = dto_HoSo.TonGiao;
        //    tbDiaChi_LLCN.Text = dto_HoSo.DiaChi;
        //    tbDienThoaiLienLac_LLCN.Text = dto_HoSo.DienThoaiLienLac;
        //    tbEmail_LLCN.Text = dto_HoSo.Email;

        //    if (dto_HoSo.Nganh == "Ấu")
        //    {
        //        rbAu.Checked = true;
        //    }

        //    if (dto_HoSo.Nganh == "Thiếu")
        //    {
        //        rbThieu.Checked = true;
        //    }

        //    if (dto_HoSo.Nganh == "Kha")
        //    {
        //        rbKha.Checked = true;
        //    }

        //    if (dto_HoSo.Nganh == "Tráng")
        //    {
        //        rbTrang.Checked = true;
        //    }

        //    if (dto_HoSo.Nganh == "Khác")
        //    {
        //        rbKhac.Checked = true;
        //    }

        //    tbDonVi_LLHD.Text = dto_HoSo.DonVi;
        //    tbLienDoan_LLHD.Text = dto_HoSo.LienDoan;
        //    tbDao_LLHD.Text = dto_HoSo.Dao;
        //    tbChau_LLHD.Text = dto_HoSo.Chau;
        //    dtpNgayTuyenHua_LLHD.Value = (DateTime)dto_HoSo.NgayTuyenHua;
        //    tbTruongNhanLoiHua_LLHD.Text = dto_HoSo.TruongNhanLoiHua;
        //    tbTracVuTaiDonVi_LLHD.Text = dto_HoSo.TrachVuTaiDonVi;
        //    tbTracVuNgoaiDonVi_LLHD.Text = dto_HoSo.TrachVuNgoaiDonVi;
        //    tbGhiChu_LLHD.Text = dto_HoSo.GhiChu;

        //    tbNgheNghiep_NNKN.Text = dto_HoSo.NgheNghiep;

        //    if (dto_HoSo.NutDay == 1) //1
        //    {
        //        chbNutDay_NNKN.Checked = true;
        //    }
        //    else
        //    {
        //        chbNutDay_NNKN.Checked = false;
        //    }

        //    if (dto_HoSo.PhuongHuong == 1) //2
        //    {
        //        chbPhuongHuong_NNKN.Checked = true;
        //    }
        //    else
        //    {
        //        chbPhuongHuong_NNKN.Checked = false;
        //    }

        //    if (dto_HoSo.CuuThuong == 1) //3
        //    {
        //        chbCuuThuong_NNKN.Checked = true;
        //    }
        //    else
        //    {
        //        chbCuuThuong_NNKN.Checked = false;
        //    }

        //    if (dto_HoSo.TruyenTin == 1) //4
        //    {
        //        chbTruyenTin_NNKN.Checked = true;
        //    }
        //    else
        //    {
        //        chbTruyenTin_NNKN.Checked = false;
        //    }

        //    if (dto_HoSo.TroChoi == 1) //5
        //    {
        //        chbTroChoi_NNKN.Checked = true;
        //    }
        //    else
        //    {
        //        chbTroChoi_NNKN.Checked = false;
        //    }

        //    if (dto_HoSo.LuaTrai == 1) //6
        //    {
        //        chbLuaTrai_NNKN.Checked = true;
        //    }
        //    else
        //    {
        //        chbLuaTrai_NNKN.Checked = false;
        //    }

        //    tbSoTruong_NNKN.Text = dto_HoSo.SoTruong;

        //    List<HoSo_HuanLuyen> list_HoSo_HuanLuyen = HoSo_HuanLuyen_BUS.TraCuuDSHuanLuyenTheoMaHoSo(tbMa_LLCN.Text);
        //    List<UC_HuanLuyen> list_UC_HuanLuyen = new List<UC_HuanLuyen>();
        //    foreach (HoSo_HuanLuyen dto_Temp in list_HoSo_HuanLuyen)
        //    {
        //        HuanLuyen dto_HuanLuyen_Temp = HuanLuyen_BUS.TraCuuHuanLuyenTheoMa(dto_Temp.MaHuanLuyen);
        //        UC_HuanLuyen uc_HuanLuyen = new UC_HuanLuyen(dto_HuanLuyen_Temp.Ma, dto_HuanLuyen_Temp.Nganh, dto_HuanLuyen_Temp.Khoa, dto_HuanLuyen_Temp.TenKhoa, dto_HuanLuyen_Temp.KhoaTruong, (DateTime)dto_HuanLuyen_Temp.Nam, dto_HuanLuyen_Temp.MHL, dto_HuanLuyen_Temp.TinhTrang, false);

        //        int iNewLocation = list_UC_HuanLuyen.Count * 180 + pn_gbHuanLuyen.AutoScrollPosition.Y;
        //        uc_HuanLuyen.Location = new Point(8, iNewLocation);

        //        list_UC_HuanLuyen.Add(uc_HuanLuyen);
        //        pn_gbHuanLuyen.Controls.Add(list_UC_HuanLuyen[list_UC_HuanLuyen.Count - 1]);
        //    }

        //    tbHoTen_LLCN.Select();
        //}
        //#endregion



        private void pbBackHome_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void pbBackHome_MouseEnter(object sender, EventArgs e)
        {
            pbBackHome.Image = Image.FromFile(@"Resources\ChucNang\icon_home_selected.png");
        }

        private void pbBackHome_MouseLeave(object sender, EventArgs e)
        {
            pbBackHome.Image = Image.FromFile(@"Resources\ChucNang\icon_home.png");
        }



        private void pbTiepTuc_LLCN_Click(object sender, EventArgs e)
        {
            gbLyLichCaNhan.Visible = false;
            gbLyLichHuongDao.Visible = true;
        }

        private void pbTiepTuc_LLCN_MouseEnter(object sender, EventArgs e)
        {
            pbTiepTuc_LLCN.Image = Image.FromFile(@"Resources\ChucNang\forward_selected.png");
        }

        private void pbTiepTuc_LLCN_MouseLeave(object sender, EventArgs e)
        {
            pbTiepTuc_LLCN.Image = Image.FromFile(@"Resources\ChucNang\forward.png");
        }



        private void pbTroVe_LLHD_Click(object sender, EventArgs e)
        {
            gbLyLichCaNhan.Visible = true;
            gbLyLichHuongDao.Visible = false;
        }

        private void pbTroVe_LLHD_MouseEnter(object sender, EventArgs e)
        {
            pbTroVe_LLHD.Image = Image.FromFile(@"Resources\ChucNang\back_selected.png");
        }

        private void pbTroVe_LLHD_MouseLeave(object sender, EventArgs e)
        {
            pbTroVe_LLHD.Image = Image.FromFile(@"Resources\ChucNang\back.png");
        }

        private void pbTiepTuc_LLHD_Click(object sender, EventArgs e)
        {
            gbLyLichHuongDao.Visible = false;
            gbNgheNghiep_KiNang.Visible = true;
        }

        private void pbTiepTuc_LLHD_MouseEnter(object sender, EventArgs e)
        {
            pbTiepTuc_LLHD.Image = Image.FromFile(@"Resources\ChucNang\forward_selected.png");
        }

        private void pbTiepTuc_LLHD_MouseLeave(object sender, EventArgs e)
        {
            pbTiepTuc_LLHD.Image = Image.FromFile(@"Resources\ChucNang\forward.png");
        }



        private void pbTroVe_NNKN_Click(object sender, EventArgs e)
        {
            gbLyLichHuongDao.Visible = true;
            gbNgheNghiep_KiNang.Visible = false;
        }

        private void pbTroVe_NNKN_MouseEnter(object sender, EventArgs e)
        {
            pbTroVe_NNKN.Image = Image.FromFile(@"Resources\ChucNang\back_selected.png");
        }

        private void pbTroVe_NNKN_MouseLeave(object sender, EventArgs e)
        {
            pbTroVe_NNKN.Image = Image.FromFile(@"Resources\ChucNang\back.png");
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



        private void pbTroVe_HL_Click(object sender, EventArgs e)
        {
            gbNgheNghiep_KiNang.Visible = true;
            gbHuanLuyen.Visible = false;
        }

        private void pbTroVe_HL_MouseEnter(object sender, EventArgs e)
        {
            pbTroVe_HL.Image = Image.FromFile(@"Resources\ChucNang\back_selected.png");
        }

        private void pbTroVe_HL_MouseLeave(object sender, EventArgs e)
        {
            pbTroVe_HL.Image = Image.FromFile(@"Resources\ChucNang\back.png");
        }

        private void lvLuuTru_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lvLuuTru.SelectedItems.Count > 0)
            {
                dMaLuuTru = list_MaLuuTru[lvLuuTru.SelectedIndices[0]];
                setHoSoTheoMaLuuTru(dMaLuuTru);
            }
        }

        private void pbHistory_Click(object sender, EventArgs e)
        {
            if (lbHistory.Visible)
            {
                lbHistory.Visible = false;
                pbIcon_Search.Visible = true;
                lvLuuTru.Visible = false;
                pbDelete.Visible = false;

                setHoSoTheoMaHoSo(sMaHoSo);
            }
            else
            {
                lbHistory.Visible = true;
                pbIcon_Search.Visible = false;
                lvLuuTru.Visible = true;
                pbDelete.Visible = true;

                lvLuuTru.SelectedItems.Clear();
                lvLuuTru.Items[lvLuuTru.Items.Count - 1].Selected = true;
                lvLuuTru.Focus();
            }
        }

        private bool deleteLuuTru()
        {
            if (!HoSo_LuuTru_BUS.Delete(sMaHoSo, dMaLuuTru))
            {
                return false;
            }

            if (!LuuTru_BUS.Delete(dMaLuuTru))
            {
                return false;
            }

            return true;
        }

        private void pbDelete_Click(object sender, EventArgs e)
        {
            Form_Confirm frm_Confirm = new Form_Confirm("Đồng ý xóa Lưu trữ này?");
            if (frm_Confirm.Yes)
            {
                if (!deleteLuuTru())
                {
                    Form_Notice frm_Notice = new Form_Notice("Không thể xóa Lưu trữ này!", false);
                }
                else
                {
                    setLuuTruTheoMaHoSo(sMaHoSo);

                    if (lvLuuTru.Items.Count > 0)
                    {
                        lvLuuTru.SelectedItems.Clear();
                        lvLuuTru.Items[lvLuuTru.Items.Count - 1].Selected = true;
                    }
                }
            }
        }

        private void cbIDV_LLCN_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tbMa_LLCN.Text.Length > 0)
            {
                if (cbIDV_LLCN.SelectedIndex > 0)
                {
                    tbMa_LLCN.Text = tbMa_LLCN.Text.Substring(0, 4) + IDV_BUS.TraCuuIDVTheoMa(list_IDV[cbIDV_LLCN.SelectedIndex - 1]).IDV1;
                }
                else
                {
                    tbMa_LLCN.Text = tbMa_LLCN.Text.Substring(0, 4);
                }
            }
        }
    }
}
