using System;
using System.Collections.Generic;
using System.Text;

namespace DTO
{
    public class HoSo_DTO
    {
        #region Attributes & Properties
        private int ma;

        public int Ma
        {
            get { return ma; }
            set { ma = value; }
        }

        private int maIDV;

        public int MaIDV
        {
            get { return maIDV; }
            set { maIDV = value; }
        }

        private string maNhomTrachVu;

        public string MaNhomTrachVu
        {
            get { return maNhomTrachVu; }
            set { maNhomTrachVu = value; }
        }

        private string maTrachVu;

        public string MaTrachVu
        {
            get { return maTrachVu; }
            set { maTrachVu = value; }
        }

        private string avatar;

        public string Avatar
        {
            get { return avatar; }
            set { avatar = value; }
        }

        private string ngayCapNhat;

        public string NgayCapNhat
        {
            get { return ngayCapNhat; }
            set { ngayCapNhat = value; }
        }

        private string hoTen;

        public string HoTen
        {
            get { return hoTen; }
            set { hoTen = value; }
        }

        private DateTime ngaySinh;

        public DateTime NgaySinh
        {
            get { return ngaySinh; }
            set { ngaySinh = value; }
        }

        private string gioiTinh;

        public string GioiTinh
        {
            get { return gioiTinh; }
            set { gioiTinh = value; }
        }

        private string queQuan;

        public string QueQuan
        {
            get { return queQuan; }
            set { queQuan = value; }
        }

        private string trinhDoHocVan;

        public string TrinhDoHocVan
        {
            get { return trinhDoHocVan; }
            set { trinhDoHocVan = value; }
        }

        private string tonGiao;

        public string TonGiao
        {
            get { return tonGiao; }
            set { tonGiao = value; }
        }

        private string diaChi;

        public string DiaChi
        {
            get { return diaChi; }
            set { diaChi = value; }
        }

        private string dienThoaiLienLac;

        public string DienThoaiLienLac
        {
            get { return dienThoaiLienLac; }
            set { dienThoaiLienLac = value; }
        }

        private string email;

        public string Email
        {
            get { return email; }
            set { email = value; }
        }

        private string nganh;

        public string Nganh
        {
            get { return nganh; }
            set { nganh = value; }
        }

        private string donVi;

        public string DonVi
        {
            get { return donVi; }
            set { donVi = value; }
        }

        private string lienDoan;

        public string LienDoan
        {
            get { return lienDoan; }
            set { lienDoan = value; }
        }

        private string dao;

        public string Dao
        {
            get { return dao; }
            set { dao = value; }
        }

        private string chau;

        public string Chau
        {
            get { return chau; }
            set { chau = value; }
        }

        private DateTime ngayTuyenHua;

        public DateTime NgayTuyenHua
        {
            get { return ngayTuyenHua; }
            set { ngayTuyenHua = value; }
        }

        private string truongNhanLoiHua;

        public string TruongNhanLoiHua
        {
            get { return truongNhanLoiHua; }
            set { truongNhanLoiHua = value; }
        }

        private string trachVuTaiDonVi;

        public string TrachVuTaiDonVi
        {
            get { return trachVuTaiDonVi; }
            set { trachVuTaiDonVi = value; }
        }

        private string trachVuNgoaiDonVi;

        public string TrachVuNgoaiDonVi
        {
            get { return trachVuNgoaiDonVi; }
            set { trachVuNgoaiDonVi = value; }
        }

        private string tenRung;

        public string TenRung
        {
            get { return tenRung; }
            set { tenRung = value; }
        }

        private string ghiChu;

        public string GhiChu
        {
            get { return ghiChu; }
            set { ghiChu = value; }
        }

        private string ngheNghiep;

        public string NgheNghiep
        {
            get { return ngheNghiep; }
            set { ngheNghiep = value; }
        }

        private int nutDay;

        public int NutDay
        {
            get { return nutDay; }
            set { nutDay = value; }
        }

        private int phuongHuong;

        public int PhuongHuong
        {
            get { return phuongHuong; }
            set { phuongHuong = value; }
        }

        private int cuuThuong;

        public int CuuThuong
        {
            get { return cuuThuong; }
            set { cuuThuong = value; }
        }

        private int truyenTin;

        public int TruyenTin
        {
            get { return truyenTin; }
            set { truyenTin = value; }
        }

        private int troChoi;

        public int TroChoi
        {
            get { return troChoi; }
            set { troChoi = value; }
        }

        private int luaTrai;

        public int LuaTrai
        {
            get { return luaTrai; }
            set { luaTrai = value; }
        }

        private string soTruong;

        public string SoTruong
        {
            get { return soTruong; }
            set { soTruong = value; }
        }
        #endregion



        #region Default Contructor
        public HoSo_DTO()
        {
            //ma = new int();
            //maIDV = new int();
            maNhomTrachVu = string.Empty;
            maTrachVu = string.Empty;
            avatar = string.Empty;
            ngayCapNhat = string.Empty;
            hoTen = string.Empty;
            ngaySinh = new DateTime(2000, 1, 1);
            gioiTinh = string.Empty;
            queQuan = string.Empty;
            trinhDoHocVan = string.Empty;
            tonGiao = string.Empty;
            diaChi = string.Empty;
            dienThoaiLienLac = string.Empty;
            email = string.Empty;

            nganh = string.Empty;
            donVi = string.Empty;
            lienDoan = string.Empty;
            dao = string.Empty;
            chau = string.Empty;
            ngayTuyenHua = new DateTime(2000, 1, 1);
            truongNhanLoiHua = string.Empty;
            trachVuTaiDonVi = string.Empty;
            trachVuNgoaiDonVi = string.Empty;
            tenRung = string.Empty;
            ghiChu = string.Empty;

            ngheNghiep = string.Empty;
            //nutDay = new int();
            //phuongHuong = new int();
            //cuuThuong = new int();
            //truyenTin = new int();
            //troChoi = new int();
            //luaTrai = new int();
            soTruong = string.Empty;
        }
        #endregion
    }
}
