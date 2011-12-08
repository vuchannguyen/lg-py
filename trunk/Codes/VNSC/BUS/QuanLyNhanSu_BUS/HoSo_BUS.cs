using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAO;

namespace BUS
{
    public class HoSo_BUS
    {
        #region Tra cứu danh sách hồ sơ, với đầu vào là 1 danh sách có sẳn
        public static List<HoSo> TraCuuDSHoSoTheoTen(string sTen, List<HoSo> ListHoSoDauVao)
        {
            return HoSo_DAO.TraCuuDSHoSoTheoTen(sTen,ListHoSoDauVao);
        }

        public static List<HoSo> TraCuuDSHoSoTheoTenNhomTrachVu(String sTen, List<HoSo> ListHoSoDauVao)
        {
            return HoSo_DAO.TraCuuDSHoSoTheoTenNhomTrachVu(sTen, ListHoSoDauVao);
        }

        public static List<HoSo> TraCuuDSHoSoTheoTenTrachVu(String sTen, List<HoSo> ListHoSoDauVao)
        {
            return HoSo_DAO.TraCuuDSHoSoTheoTenTrachVu(sTen,ListHoSoDauVao);
        }

        public static List<HoSo> TraCuuDSHoSoTheoTenDonVi(String sTen, List<HoSo> ListHoSoDauVao)
        {
            return HoSo_DAO.TraCuuDSHoSoTheoTenDonVi(sTen,ListHoSoDauVao);
        }

        public static List<HoSo> TraCuuDSHoSoTheoTuoi(int Tuoi, List<HoSo> ListHoSoDauVao)
        {
            return HoSo_DAO.TraCuuDSHoSoTheoTuoi(Tuoi,ListHoSoDauVao);
        }

        public static List<HoSo> TraCuuDSHoSoTheoSoTruong(string sTen, List<HoSo> ListHoSoDauVao)
        {
            return HoSo_DAO.TraCuuDSHoSoTheoSoTruong(sTen,ListHoSoDauVao);
        }

        public static List<HoSo> TraCuuDSHoSoTheoNgheNghiep(string sTen, List<HoSo> ListHoSoDauVao)
        {
            return HoSo_DAO.TraCuuDSHoSoTheoNgheNghiep(sTen,ListHoSoDauVao);
        }

        public static List<HoSo> TraCuuDSHoSoTheoGhiChu(string sGhiChu, List<HoSo> ListHoSoDauVao)
        {
            return HoSo_DAO.TraCuuDSHoSoTheoGhiChu(sGhiChu,ListHoSoDauVao);
        }

        public static List<HoSo> TraCuuDSHoSoTheoDao(string sDao, List<HoSo> ListHoSoDauVao)
        {
            return HoSo_DAO.TraCuuDSHoSoTheoDao(sDao,ListHoSoDauVao);
        }

        public static List<HoSo> TraCuuDSHoSoTheoChau(string sChau, List<HoSo> ListHoSoDauVao)
        {
            return HoSo_DAO.TraCuuDSHoSoTheoChau(sChau,ListHoSoDauVao);
        }

        public static List<HoSo> TraCuuDSHoSoTheoTonGiao(string sTonGiao, List<HoSo> ListHoSoDauVao)
        {
            return HoSo_DAO.TraCuuDSHoSoTheoTonGiao(sTonGiao,ListHoSoDauVao);
        }

        public static List<HoSo> TraCuuDSHoSoTheoTrinhDoHocVan(string sTrinhDo, List<HoSo> ListHoSoDauVao)
        {
            return HoSo_DAO.TraCuuDSHoSoTheoTrinhDoHocVan(sTrinhDo,ListHoSoDauVao);
        }

        public static List<HoSo> TraCuuDSHoSoTheoQueQuan(string sQueQuan, List<HoSo> ListHoSoDauVao)
        {
            return HoSo_DAO.TraCuuDSHoSoTheoQueQuan(sQueQuan,ListHoSoDauVao);
        }

        public static List<HoSo> TraCuuDSHoSoTheoGioiTinh(string sGioiTinh, List<HoSo> ListHoSoDauVao)
        {
            return HoSo_DAO.TraCuuDSHoSoTheoGioiTinh(sGioiTinh,ListHoSoDauVao);
        }

        public static List<HoSo> TraCuuDSHoSoTheoDienThoaiLienLac(string sDienThoai, List<HoSo> ListHoSoDauVao)
        {
            return HoSo_DAO.TraCuuDSHoSoTheoDienThoaiLienLac(sDienThoai,ListHoSoDauVao);
        }

        public static List<HoSo> TraCuuDSHoSoTheoEmail(string sEmail, List<HoSo> ListHoSoDauVao)
        {
            return HoSo_DAO.TraCuuDSHoSoTheoEmail(sEmail,ListHoSoDauVao);
        }

        public static List<HoSo> TraCuuDSHoSoTheoDiaChi(string sDiaChi, List<HoSo> ListHoSoDauVao)
        {
            return HoSo_DAO.TraCuuDSHoSoTheoDiaChi(sDiaChi,ListHoSoDauVao);
        }

        public static List<HoSo> TraCuuDSHoSoTheoNganh(string sNganh, List<HoSo> ListHoSoDauVao)
        {
            return HoSo_DAO.TraCuuDSHoSoTheoNganh(sNganh,ListHoSoDauVao);
        }

        public static List<HoSo> TraCuuDSHoSoTheoLienDoan(string sLienDoan, List<HoSo> ListHoSoDauVao)
        {
            return HoSo_DAO.TraCuuDSHoSoTheoLienDoan(sLienDoan,ListHoSoDauVao);
        }

        public static List<HoSo> TraCuuDSHoSoTheoNgayTuyenHua(DateTime NgayTuyenHua, List<HoSo> ListHoSoDauVao)
        {
            return HoSo_DAO.TraCuuDSHoSoTheoNgayTuyenHua(NgayTuyenHua,ListHoSoDauVao);
        }

        public static List<HoSo> TraCuuDSHoSoTheoTrachVuNgoaiDonVi(string sTrachVu, List<HoSo> ListHoSoDauVao)
        {
            return HoSo_DAO.TraCuuDSHoSoTheoTrachVuNgoaiDonVi(sTrachVu,ListHoSoDauVao);
        }

        public static List<HoSo> TraCuuDSHoSoTheoTrachVuTaiDonVi(string sTrachVu, List<HoSo> ListHoSoDauVao)
        {
            return HoSo_DAO.TraCuuDSHoSoTheoTrachVuTaiDonVi(sTrachVu, ListHoSoDauVao);
        }
        #endregion

        public static List<HoSo> LayDSHoSo()
        {
            return HoSo_DAO.LayDSHoSo();
        }

        public static List<HoSo> TraCuuDSHoSoTheoTen(string sTen)
        {
            return HoSo_DAO.TraCuuDSHoSoTheoTen(sTen);
        }

        public static List<HoSo> TraCuuDSHoSoTheoTenNhomTrachVu(String sTen)
        {
            return HoSo_DAO.TraCuuDSHoSoTheoTenNhomTrachVu(sTen);
        }

        public static List<HoSo> TraCuuDSHoSoTheoTenTrachVu(String sTen)
        {
            return HoSo_DAO.TraCuuDSHoSoTheoTenTrachVu(sTen);
        }

        public static List<HoSo> TraCuuDSHoSoTheoTenDonVi(String sTen)
        {
            return HoSo_DAO.TraCuuDSHoSoTheoTenDonVi(sTen);
        }


        public static List<HoSo> TraCuuDSHoSoTheoTuoi(int Tuoi)
        {
            return HoSo_DAO.TraCuuDSHoSoTheoTuoi(Tuoi);
        }

        public static List<HoSo> TraCuuDSHoSoTheoSoTruong(string sTen)
        {
            return HoSo_DAO.TraCuuDSHoSoTheoSoTruong(sTen);
        }

        public static List<HoSo> TraCuuDSHoSoTheoNgheNghiep(string sTen)
        {
            return HoSo_DAO.TraCuuDSHoSoTheoNgheNghiep(sTen);
        }

        public static List<HoSo> TraCuuDSHoSoTheoGhiChu(string sGhiChu)
        {
            return HoSo_DAO.TraCuuDSHoSoTheoGhiChu(sGhiChu);
        }

        public static List<HoSo> TraCuuDSHoSoTheoDao(string sDao)
        {
            return HoSo_DAO.TraCuuDSHoSoTheoDao(sDao);
        }

        public static List<HoSo> TraCuuDSHoSoTheoChau(string sChau)
        {
            return HoSo_DAO.TraCuuDSHoSoTheoChau(sChau);
        }

        public static List<HoSo> TraCuuDSHoSoTheoTonGiao(string sTonGiao)
        {
            return HoSo_DAO.TraCuuDSHoSoTheoTonGiao(sTonGiao);
        }

        public static List<HoSo> TraCuuDSHoSoTheoTrinhDoHocVan(string sTrinhDo)
        {
            return HoSo_DAO.TraCuuDSHoSoTheoTrinhDoHocVan(sTrinhDo);
        }

        public static List<HoSo> TraCuuDSHoSoTheoQueQuan(string sQueQuan)
        {
            return HoSo_DAO.TraCuuDSHoSoTheoQueQuan(sQueQuan);
        }

        public static List<HoSo> TraCuuDSHoSoTheoGioiTinh(string sGioiTinh)
        {
            return HoSo_DAO.TraCuuDSHoSoTheoGioiTinh(sGioiTinh);
        }

        public static List<HoSo> TraCuuDSHoSoTheoDienThoaiLienLac(string sDienThoai)
        {
            return HoSo_DAO.TraCuuDSHoSoTheoDienThoaiLienLac(sDienThoai);
        }

        public static List<HoSo> TraCuuDSHoSoTheoEmail(string sEmail)
        {
            return HoSo_DAO.TraCuuDSHoSoTheoEmail(sEmail);
        }

        public static List<HoSo> TraCuuDSHoSoTheoDiaChi(string sDiaChi)
        {
            return HoSo_DAO.TraCuuDSHoSoTheoDiaChi(sDiaChi);
        }

        public static List<HoSo> TraCuuDSHoSoTheoNganh(string sNganh)
        {
            return HoSo_DAO.TraCuuDSHoSoTheoNganh(sNganh);
        }

        public static List<HoSo> TraCuuDSHoSoTheoLienDoan(string sLienDoan)
        {
            return HoSo_DAO.TraCuuDSHoSoTheoLienDoan(sLienDoan);
        }

        public static List<HoSo> TraCuuDSHoSoTheoNgayTuyenHua(DateTime NgayTuyenHua)
        {
            return HoSo_DAO.TraCuuDSHoSoTheoNgayTuyenHua(NgayTuyenHua);
        }

        public static List<HoSo> TraCuuDSHoSoTheoTrachVuNgoaiDonVi(string sTrachVu)
        {
            return HoSo_DAO.TraCuuDSHoSoTheoTrachVuNgoaiDonVi(sTrachVu);
        }

        public static List<HoSo> TraCuuDSHoSoTheoTrachVuTaiDonVi(string sTrachVu)
        {
            return HoSo_DAO.TraCuuDSHoSoTheoTrachVuTaiDonVi(sTrachVu);
        }


        public static HoSo TraCuuHoSoTheoMa(string sMa)
        {
            return HoSo_DAO.TraCuuHoSoTheoMa(sMa);
        }

        public static bool Insert(HoSo dto)
        {
            return HoSo_DAO.Insert(dto);
        }

        public static bool Delete(string sMa)
        {
            return HoSo_DAO.Delete(sMa);
        }

        public static bool UpdateHoSoInfo(HoSo dto)
        {
            return HoSo_DAO.UpdateHoSoInfo(dto);
        }
    }
}
