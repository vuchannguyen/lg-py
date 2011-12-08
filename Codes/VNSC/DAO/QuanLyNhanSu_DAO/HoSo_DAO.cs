using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAO
{
    public class HoSo_DAO
    {
        public static List<HoSo> LayDSHoSo()
        {
            try
            {
                VNSCDataContext VNSC = SQL_Connection.CreateSQlConnection();
                List<HoSo> list = new List<HoSo>();

                var q = from p in VNSC.HoSos
                        select p;

                list = q.ToList();

                //foreach (HoSo k in q)
                //{
                //    list.Add(k);
                //}

                return list;
            }
            catch (System.Exception ex)
            {
                return null;
            }
        }



        #region Phần Tra Cứu danh sách hồ sơ
        public static List<HoSo> TraCuuDSHoSoTheoTen(string sTen)
        {
            try
            {
                VNSCDataContext VNSC = SQL_Connection.CreateSQlConnection();
                List<HoSo> list = new List<HoSo>();

                var q = from p in VNSC.HoSos
                        where p.HoTen.Contains(sTen)
                        select p;
                foreach (HoSo k in q)
                {
                    list.Add(k);
                }
                return list;
            }
            catch (System.Exception ex)
            {
                return null;
            }
        }

        public static List<HoSo> TraCuuDSHoSoTheoTenNhomTrachVu(String sTen)
        {
            try
            {
                VNSCDataContext VNSC = SQL_Connection.CreateSQlConnection();
                NhomTrachVu NhomTrachVu = NhomTrachVu_DAO.TraCuuNhomTrachVuTheoTen(sTen);
                List<HoSo> list = new List<HoSo>();

                var q = from p in VNSC.HoSos
                        where p.MaNhomTrachVu == NhomTrachVu.Ma
                        select p;
                foreach (HoSo k in q)
                {
                    list.Add(k);
                }
                return list;
            }
            catch (System.Exception ex)
            {
                return null;
            }
        }

        public static List<HoSo> TraCuuDSHoSoTheoTenTrachVu(String sTen)
        {
            try
            {
                VNSCDataContext VNSC = SQL_Connection.CreateSQlConnection();
                TrachVu trachvu = TrachVu_DAO.TraCuuTrachVuTheoTen(sTen);
                List<HoSo> list = new List<HoSo>();

                var q = from p in VNSC.HoSos
                        where p.MaNhomTrachVu == trachvu.Ma
                        select p;
                foreach (HoSo k in q)
                {
                    list.Add(k);
                }
                return list;
            }
            catch (System.Exception ex)
            {
                return null;
            }
        }

        public static List<HoSo> TraCuuDSHoSoTheoTenDonVi(String sTen)
        {
            try
            {
                VNSCDataContext VNSC = SQL_Connection.CreateSQlConnection();
                List<HoSo> list = new List<HoSo>();

                var q = from p in VNSC.HoSos
                        where p.DonVi.Contains(sTen)
                        select p;
                foreach (HoSo k in q)
                {
                    list.Add(k);
                }
                return list;
            }
            catch (System.Exception ex)
            {
                return null;
            }
        }

        public static List<HoSo> TraCuuDSHoSoTheoTuoi(int Tuoi)
        {
            try
            {
                VNSCDataContext VNSC = SQL_Connection.CreateSQlConnection();
                List<HoSo> list = new List<HoSo>();

                var q = from p in VNSC.HoSos
                        where (DateTime.Now.Year - DateTime.Parse(p.NgaySinh.ToString()).Year) == Tuoi
                        select p;
                foreach (HoSo k in q)
                {
                    list.Add(k);
                }
                return list;

            }
            catch (System.Exception ex)
            {
                return null;
            }
        }

        public static List<HoSo> TraCuuDSHoSoTheoSoTruong(string sTen)
        {
            try
            {
                VNSCDataContext VNSC = SQL_Connection.CreateSQlConnection();
                List<HoSo> list = new List<HoSo>();

                var q = from p in VNSC.HoSos
                        where p.SoTruong.Contains(sTen)
                        select p;
                foreach (HoSo k in q)
                {
                    list.Add(k);
                }
                return list;

            }
            catch (System.Exception ex)
            {
                return null;
            }
        }

        public static List<HoSo> TraCuuDSHoSoTheoNgheNghiep(string sTen)
        {
            try
            {
                VNSCDataContext VNSC = SQL_Connection.CreateSQlConnection();
                List<HoSo> list = new List<HoSo>();

                var q = from p in VNSC.HoSos
                        where p.NgheNghiep.Contains(sTen)
                        select p;
                foreach (HoSo k in q)
                {
                    list.Add(k);
                }
                return list;

            }
            catch (System.Exception ex)
            {
                return null;
            }
        }

        public static List<HoSo> TraCuuDSHoSoTheoGhiChu(string sGhiChu)
        {
            try
            {
                VNSCDataContext VNSC = SQL_Connection.CreateSQlConnection();
                List<HoSo> list = new List<HoSo>();

                var q = from p in VNSC.HoSos
                        where p.GhiChu.Contains(sGhiChu)
                        select p;
                foreach (HoSo k in q)
                {
                    list.Add(k);
                }
                return list;

            }
            catch (System.Exception ex)
            {
                return null;
            }
        }

        public static List<HoSo> TraCuuDSHoSoTheoDao(string sDao)
        {
            try
            {
                VNSCDataContext VNSC = SQL_Connection.CreateSQlConnection();
                List<HoSo> list = new List<HoSo>();

                var q = from p in VNSC.HoSos
                        where p.Dao.Contains(sDao)
                        select p;
                foreach (HoSo k in q)
                {
                    list.Add(k);
                }
                return list;

            }
            catch (System.Exception ex)
            {
                return null;
            }
        }

        public static List<HoSo> TraCuuDSHoSoTheoChau(string sChau)
        {
            try
            {
                VNSCDataContext VNSC = SQL_Connection.CreateSQlConnection();
                List<HoSo> list = new List<HoSo>();

                var q = from p in VNSC.HoSos
                        where p.Chau.Contains(sChau)
                        select p;
                foreach (HoSo k in q)
                {
                    list.Add(k);
                }
                return list;

            }
            catch (System.Exception ex)
            {
                return null;
            }
        }

        public static List<HoSo> TraCuuDSHoSoTheoTonGiao(string sTonGiao)
        {
            try
            {
                VNSCDataContext VNSC = SQL_Connection.CreateSQlConnection();
                List<HoSo> list = new List<HoSo>();

                var q = from p in VNSC.HoSos
                        where p.TonGiao.Contains(sTonGiao)
                        select p;
                foreach (HoSo k in q)
                {
                    list.Add(k);
                }
                return list;

            }
            catch (System.Exception ex)
            {
                return null;
            }
        }

        public static List<HoSo> TraCuuDSHoSoTheoTrinhDoHocVan(string sTrinhDo)
        {
            try
            {
                VNSCDataContext VNSC = SQL_Connection.CreateSQlConnection();
                List<HoSo> list = new List<HoSo>();

                var q = from p in VNSC.HoSos
                        where p.TrinhDoHocVan.Contains(sTrinhDo)
                        select p;
                foreach (HoSo k in q)
                {
                    list.Add(k);
                }
                return list;

            }
            catch (System.Exception ex)
            {
                return null;
            }
        }

        public static List<HoSo> TraCuuDSHoSoTheoQueQuan(string sQueQuan)
        {
            try
            {
                VNSCDataContext VNSC = SQL_Connection.CreateSQlConnection();
                List<HoSo> list = new List<HoSo>();

                var q = from p in VNSC.HoSos
                        where p.QueQuan.Contains(sQueQuan)
                        select p;
                foreach (HoSo k in q)
                {
                    list.Add(k);
                }
                return list;

            }
            catch (System.Exception ex)
            {
                return null;
            }
        }

        public static List<HoSo> TraCuuDSHoSoTheoGioiTinh(string sGioiTinh)
        {
            try
            {
                VNSCDataContext VNSC = SQL_Connection.CreateSQlConnection();
                List<HoSo> list = new List<HoSo>();

                var q = from p in VNSC.HoSos
                        where p.GioiTinh.Contains(sGioiTinh)
                        select p;
                foreach (HoSo k in q)
                {
                    list.Add(k);
                }
                return list;

            }
            catch (System.Exception ex)
            {
                return null;
            }
        }

        public static List<HoSo> TraCuuDSHoSoTheoDienThoaiLienLac(string sDienThoai)
        {
            try
            {
                VNSCDataContext VNSC = SQL_Connection.CreateSQlConnection();
                List<HoSo> list = new List<HoSo>();

                var q = from p in VNSC.HoSos
                        where p.DienThoaiLienLac.Contains(sDienThoai)
                        select p;
                foreach (HoSo k in q)
                {
                    list.Add(k);
                }
                return list;
            }
            catch (System.Exception ex)
            {
                return null;
            }
        }

        public static List<HoSo> TraCuuDSHoSoTheoEmail(string sEmail)
        {
            try
            {
                VNSCDataContext VNSC = SQL_Connection.CreateSQlConnection();
                List<HoSo> list = new List<HoSo>();

                var q = from p in VNSC.HoSos
                        where p.Email.Contains(sEmail)
                        select p;
                foreach (HoSo k in q)
                {
                    list.Add(k);
                }
                return list;

            }
            catch (System.Exception ex)
            {
                return null;
            }
        }

        public static List<HoSo> TraCuuDSHoSoTheoDiaChi(string sDiaChi)
        {
            try
            {
                VNSCDataContext VNSC = SQL_Connection.CreateSQlConnection();
                List<HoSo> list = new List<HoSo>();

                var q = from p in VNSC.HoSos
                        where p.DiaChi.Contains(sDiaChi)
                        select p;
                foreach (HoSo k in q)
                {
                    list.Add(k);
                }
                return list;

            }
            catch (System.Exception ex)
            {
                return null;
            }
        }

        public static List<HoSo> TraCuuDSHoSoTheoNganh(string sNganh)
        {
            try
            {
                VNSCDataContext VNSC = SQL_Connection.CreateSQlConnection();
                List<HoSo> list = new List<HoSo>();

                var q = from p in VNSC.HoSos
                        where p.Nganh.Contains(sNganh)
                        select p;
                foreach (HoSo k in q)
                {
                    list.Add(k);
                }
                return list;

            }
            catch (System.Exception ex)
            {
                return null;
            }
        }

        public static List<HoSo> TraCuuDSHoSoTheoLienDoan(string sLienDoan)
        {
            try
            {
                VNSCDataContext VNSC = SQL_Connection.CreateSQlConnection();
                List<HoSo> list = new List<HoSo>();

                var q = from p in VNSC.HoSos
                        where p.LienDoan.Contains(sLienDoan)
                        select p;
                foreach (HoSo k in q)
                {
                    list.Add(k);
                }
                return list;

            }
            catch (System.Exception ex)
            {
                return null;
            }
        }

        public static List<HoSo> TraCuuDSHoSoTheoNgayTuyenHua(DateTime NgayTuyenHua)
        {
            try
            {
                VNSCDataContext VNSC = SQL_Connection.CreateSQlConnection();
                List<HoSo> list = new List<HoSo>();

                var q = from p in VNSC.HoSos
                        where p.NgayTuyenHua == NgayTuyenHua
                        select p;
                foreach (HoSo k in q)
                {
                    list.Add(k);
                }
                return list;

            }
            catch (System.Exception ex)
            {
                return null;
            }
        }

        public static List<HoSo> TraCuuDSHoSoTheoTrachVuNgoaiDonVi(string sTrachVu)
        {
            try
            {
                VNSCDataContext VNSC = SQL_Connection.CreateSQlConnection();
                List<HoSo> list = new List<HoSo>();

                var q = from p in VNSC.HoSos
                        where p.TrachVuNgoaiDonVi.Contains(sTrachVu) 
                        select p;
                foreach (HoSo k in q)
                {
                    list.Add(k);
                }
                return list;

            }
            catch (System.Exception ex)
            {
                return null;
            }
        }

        public static List<HoSo> TraCuuDSHoSoTheoTrachVuTaiDonVi(string sTrachVu)
        {
            try
            {
                VNSCDataContext VNSC = SQL_Connection.CreateSQlConnection();
                List<HoSo> list = new List<HoSo>();

                var q = from p in VNSC.HoSos
                        where p.TrachVuTaiDonVi.Contains(sTrachVu)
                        select p;
                foreach (HoSo k in q)
                {
                    list.Add(k);
                }
                return list;

            }
            catch (System.Exception ex)
            {
                return null;
            }
        }

        #endregion



        #region Tra cứu danh sách hồ sơ, với đầu vào là 1 danh sách có sẳn
        public static List<HoSo> TraCuuDSHoSoTheoTen(string sTen,List<HoSo> ListHoSoDauVao)
        {
            try
            {
                List<HoSo> list = new List<HoSo>();

                var q = from p in ListHoSoDauVao
                        where p.HoTen.Contains(sTen)
                        select p;
                foreach (HoSo k in q)
                {
                    list.Add(k);
                }
                return list;
            }
            catch (System.Exception ex)
            {
                return null;
            }
        }

        public static List<HoSo> TraCuuDSHoSoTheoTenNhomTrachVu(String sTen, List<HoSo> ListHoSoDauVao)
        {
            try
            {
                NhomTrachVu NhomTrachVu = NhomTrachVu_DAO.TraCuuNhomTrachVuTheoTen(sTen);
                List<HoSo> list = new List<HoSo>();

                var q = from p in ListHoSoDauVao
                        where p.MaNhomTrachVu == NhomTrachVu.Ma
                        select p;
                foreach (HoSo k in q)
                {
                    list.Add(k);
                }
                return list;
            }
            catch (System.Exception ex)
            {
                return null;
            }
        }

        public static List<HoSo> TraCuuDSHoSoTheoTenTrachVu(String sTen, List<HoSo> ListHoSoDauVao)
        {
            try
            {
                TrachVu trachvu = TrachVu_DAO.TraCuuTrachVuTheoTen(sTen);
                List<HoSo> list = new List<HoSo>();

                var q = from p in ListHoSoDauVao
                        where p.MaNhomTrachVu == trachvu.Ma
                        select p;
                foreach (HoSo k in q)
                {
                    list.Add(k);
                }
                return list;
            }
            catch (System.Exception ex)
            {
                return null;
            }
        }

        public static List<HoSo> TraCuuDSHoSoTheoTenDonVi(String sTen, List<HoSo> ListHoSoDauVao)
        {
            try
            {
                List<HoSo> list = new List<HoSo>();

                var q = from p in ListHoSoDauVao
                        where p.DonVi.Contains(sTen)
                        select p;
                foreach (HoSo k in q)
                {
                    list.Add(k);
                }
                return list;
            }
            catch (System.Exception ex)
            {
                return null;
            }
        }

        public static List<HoSo> TraCuuDSHoSoTheoTuoi(int Tuoi, List<HoSo> ListHoSoDauVao)
        {
            try
            {

                List<HoSo> list = new List<HoSo>();

                var q = from p in ListHoSoDauVao
                        where (DateTime.Now.Year - DateTime.Parse(p.NgaySinh.ToString()).Year) == Tuoi
                        select p;
                foreach (HoSo k in q)
                {
                    list.Add(k);
                }
                return list;

            }
            catch (System.Exception ex)
            {
                return null;
            }
        }

        public static List<HoSo> TraCuuDSHoSoTheoSoTruong(string sTen, List<HoSo> ListHoSoDauVao)
        {
            try
            {

                List<HoSo> list = new List<HoSo>();

                var q = from p in ListHoSoDauVao
                        where p.SoTruong.Contains(sTen)
                        select p;
                foreach (HoSo k in q)
                {
                    list.Add(k);
                }
                return list;

            }
            catch (System.Exception ex)
            {
                return null;
            }
        }

        public static List<HoSo> TraCuuDSHoSoTheoNgheNghiep(string sTen, List<HoSo> ListHoSoDauVao)
        {
            try
            {

                List<HoSo> list = new List<HoSo>();

                var q = from p in ListHoSoDauVao
                        where p.NgheNghiep.Contains(sTen)
                        select p;
                foreach (HoSo k in q)
                {
                    list.Add(k);
                }
                return list;

            }
            catch (System.Exception ex)
            {
                return null;
            }
        }

        public static List<HoSo> TraCuuDSHoSoTheoGhiChu(string sGhiChu, List<HoSo> ListHoSoDauVao)
        {
            try
            {

                List<HoSo> list = new List<HoSo>();

                var q = from p in ListHoSoDauVao
                        where p.GhiChu.Contains(sGhiChu)
                        select p;
                foreach (HoSo k in q)
                {
                    list.Add(k);
                }
                return list;

            }
            catch (System.Exception ex)
            {
                return null;
            }
        }

        public static List<HoSo> TraCuuDSHoSoTheoDao(string sDao, List<HoSo> ListHoSoDauVao)
        {
            try
            {

                List<HoSo> list = new List<HoSo>();

                var q = from p in ListHoSoDauVao
                        where p.Dao.Contains(sDao)
                        select p;
                foreach (HoSo k in q)
                {
                    list.Add(k);
                }
                return list;

            }
            catch (System.Exception ex)
            {
                return null;
            }
        }

        public static List<HoSo> TraCuuDSHoSoTheoChau(string sChau, List<HoSo> ListHoSoDauVao)
        {
            try
            {

                List<HoSo> list = new List<HoSo>();

                var q = from p in ListHoSoDauVao
                        where p.Chau.Contains(sChau)
                        select p;
                foreach (HoSo k in q)
                {
                    list.Add(k);
                }
                return list;

            }
            catch (System.Exception ex)
            {
                return null;
            }
        }

        public static List<HoSo> TraCuuDSHoSoTheoTonGiao(string sTonGiao, List<HoSo> ListHoSoDauVao)
        {
            try
            {

                List<HoSo> list = new List<HoSo>();

                var q = from p in ListHoSoDauVao
                        where p.TonGiao.Contains(sTonGiao)
                        select p;
                foreach (HoSo k in q)
                {
                    list.Add(k);
                }
                return list;

            }
            catch (System.Exception ex)
            {
                return null;
            }
        }

        public static List<HoSo> TraCuuDSHoSoTheoTrinhDoHocVan(string sTrinhDo, List<HoSo> ListHoSoDauVao)
        {
            try
            {

                List<HoSo> list = new List<HoSo>();

                var q = from p in ListHoSoDauVao
                        where p.TrinhDoHocVan.Contains(sTrinhDo)
                        select p;
                foreach (HoSo k in q)
                {
                    list.Add(k);
                }
                return list;

            }
            catch (System.Exception ex)
            {
                return null;
            }
        }

        public static List<HoSo> TraCuuDSHoSoTheoQueQuan(string sQueQuan, List<HoSo> ListHoSoDauVao)
        {
            try
            {

                List<HoSo> list = new List<HoSo>();

                var q = from p in ListHoSoDauVao
                        where p.QueQuan.Contains(sQueQuan)
                        select p;
                foreach (HoSo k in q)
                {
                    list.Add(k);
                }
                return list;

            }
            catch (System.Exception ex)
            {
                return null;
            }
        }

        public static List<HoSo> TraCuuDSHoSoTheoGioiTinh(string sGioiTinh, List<HoSo> ListHoSoDauVao)
        {
            try
            {

                List<HoSo> list = new List<HoSo>();

                var q = from p in ListHoSoDauVao
                        where p.GioiTinh.Contains(sGioiTinh)
                        select p;
                foreach (HoSo k in q)
                {
                    list.Add(k);
                }
                return list;

            }
            catch (System.Exception ex)
            {
                return null;
            }
        }

        public static List<HoSo> TraCuuDSHoSoTheoDienThoaiLienLac(string sDienThoai, List<HoSo> ListHoSoDauVao)
        {
            try
            {

                List<HoSo> list = new List<HoSo>();

                var q = from p in ListHoSoDauVao
                        where p.DienThoaiLienLac.Contains(sDienThoai)
                        select p;
                foreach (HoSo k in q)
                {
                    list.Add(k);
                }
                return list;

            }
            catch (System.Exception ex)
            {
                return null;
            }
        }

        public static List<HoSo> TraCuuDSHoSoTheoEmail(string sEmail, List<HoSo> ListHoSoDauVao)
        {
            try
            {

                List<HoSo> list = new List<HoSo>();

                var q = from p in ListHoSoDauVao
                        where p.Email.Contains(sEmail)
                        select p;
                foreach (HoSo k in q)
                {
                    list.Add(k);
                }
                return list;

            }
            catch (System.Exception ex)
            {
                return null;
            }
        }

        public static List<HoSo> TraCuuDSHoSoTheoDiaChi(string sDiaChi, List<HoSo> ListHoSoDauVao)
        {
            try
            {

                List<HoSo> list = new List<HoSo>();

                var q = from p in ListHoSoDauVao
                        where p.DiaChi.Contains(sDiaChi)
                        select p;
                foreach (HoSo k in q)
                {
                    list.Add(k);
                }
                return list;

            }
            catch (System.Exception ex)
            {
                return null;
            }
        }

        public static List<HoSo> TraCuuDSHoSoTheoNganh(string sNganh, List<HoSo> ListHoSoDauVao)
        {
            try
            {

                List<HoSo> list = new List<HoSo>();

                var q = from p in ListHoSoDauVao
                        where p.Nganh.Contains(sNganh)
                        select p;
                foreach (HoSo k in q)
                {
                    list.Add(k);
                }
                return list;

            }
            catch (System.Exception ex)
            {
                return null;
            }
        }

        public static List<HoSo> TraCuuDSHoSoTheoLienDoan(string sLienDoan, List<HoSo> ListHoSoDauVao)
        {
            try
            {

                List<HoSo> list = new List<HoSo>();

                var q = from p in ListHoSoDauVao
                        where p.LienDoan.Contains(sLienDoan)
                        select p;
                foreach (HoSo k in q)
                {
                    list.Add(k);
                }
                return list;

            }
            catch (System.Exception ex)
            {
                return null;
            }
        }

        public static List<HoSo> TraCuuDSHoSoTheoNgayTuyenHua(DateTime NgayTuyenHua, List<HoSo> ListHoSoDauVao)
        {
            try
            {

                List<HoSo> list = new List<HoSo>();

                var q = from p in ListHoSoDauVao
                        where p.NgayTuyenHua == NgayTuyenHua
                        select p;
                foreach (HoSo k in q)
                {
                    list.Add(k);
                }
                return list;

            }
            catch (System.Exception ex)
            {
                return null;
            }
        }

        public static List<HoSo> TraCuuDSHoSoTheoTrachVuNgoaiDonVi(string sTrachVu, List<HoSo> ListHoSoDauVao)
        {
            try
            {

                List<HoSo> list = new List<HoSo>();

                var q = from p in ListHoSoDauVao
                        where p.TrachVuNgoaiDonVi.Contains(sTrachVu)
                        select p;
                foreach (HoSo k in q)
                {
                    list.Add(k);
                }
                return list;

            }
            catch (System.Exception ex)
            {
                return null;
            }
        }

        public static List<HoSo> TraCuuDSHoSoTheoTrachVuTaiDonVi(string sTrachVu, List<HoSo> ListHoSoDauVao)
        {
            try
            {

                List<HoSo> list = new List<HoSo>();

                var q = from p in ListHoSoDauVao
                        where p.TrachVuTaiDonVi.Contains(sTrachVu)
                        select p;
                foreach (HoSo k in q)
                {
                    list.Add(k);
                }
                return list;

            }
            catch (System.Exception ex)
            {
                return null;
            }
        }

        #endregion



        public static HoSo TraCuuHoSoTheoMa(string sMa)
        {
            try
            {
                VNSCDataContext VNSC = SQL_Connection.CreateSQlConnection();
                HoSo sk = VNSC.HoSos.Single(P => P.Ma == sMa);
                return sk;
            }
            catch (System.Exception ex)
            {
                return null;
            }
        }

        public static bool Insert(HoSo dto)
        {
            try
            {
                VNSCDataContext VNSC = SQL_Connection.CreateSQlConnection();

                VNSC.HoSos.InsertOnSubmit(dto);
                VNSC.SubmitChanges();
                return true;
            }
            catch (System.Exception ex)
            {
                return false;
            }
        }

        public static bool Delete(string sMa)
        {
            try
            {
                VNSCDataContext VNSC = SQL_Connection.CreateSQlConnection();
                HoSo dto = VNSC.HoSos.Single(P => P.Ma == sMa);

                VNSC.HoSos.DeleteOnSubmit(dto);
                VNSC.SubmitChanges();
                return true;
            }
            catch (System.Exception ex)
            {
                return false;
            }
        }

        public static bool UpdateHoSoInfo(HoSo dto)
        {
            try
            {
                VNSCDataContext VNSC = SQL_Connection.CreateSQlConnection();
                HoSo sk = VNSC.HoSos.Single(P => P.Ma == dto.Ma);
                sk.NgayCapNhat = dto.NgayCapNhat;

                sk.MaIDV = dto.MaIDV;
                sk.MaNhomTrachVu = dto.MaNhomTrachVu;
                sk.MaTrachVu = dto.MaTrachVu;
                sk.HoTen = dto.HoTen;
                sk.NgaySinh = dto.NgaySinh;
                sk.GioiTinh = dto.GioiTinh;
                sk.QueQuan = dto.QueQuan;
                sk.TrinhDoHocVan = dto.TrinhDoHocVan;
                sk.TonGiao = dto.TonGiao;
                sk.DiaChi = dto.DiaChi;
                sk.DienThoaiLienLac = dto.DienThoaiLienLac;
                sk.Email = dto.Email;

                sk.Nganh = dto.Nganh;
                sk.DonVi = dto.DonVi;
                sk.LienDoan = dto.LienDoan;
                sk.Dao = dto.Dao;
                sk.Chau = dto.Chau;
                sk.NgayTuyenHua = dto.NgayTuyenHua;
                sk.TruongNhanLoiHua = dto.TruongNhanLoiHua;
                sk.TrachVuTaiDonVi = dto.TrachVuTaiDonVi;
                sk.TrachVuNgoaiDonVi = dto.TrachVuNgoaiDonVi;
                sk.TenRung = dto.TenRung;
                sk.GhiChu = dto.GhiChu;

                sk.NgheNghiep = dto.NgheNghiep;
                sk.NutDay = dto.NutDay;
                sk.PhuongHuong = dto.PhuongHuong;
                sk.CuuThuong = dto.CuuThuong;
                sk.TruyenTin = dto.TruyenTin;
                sk.TroChoi = dto.TroChoi;
                sk.LuaTrai = dto.LuaTrai;
                sk.SoTruong = dto.SoTruong;

                VNSC.SubmitChanges();
                return true;
            }
            catch (System.Exception ex)
            {
                return false;
            }
        }
    }
}
