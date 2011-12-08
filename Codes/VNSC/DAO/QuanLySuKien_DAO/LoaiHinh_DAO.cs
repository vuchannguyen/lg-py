using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAO
{
    public class LoaiHinh_DAO
    {
        public static List<LoaiHinh> LayDSLoaiHinh()
        {
            try
            {
                VNSCDataContext VNSC = SQL_Connection.CreateSQlConnection();
                List<LoaiHinh> list = new List<LoaiHinh>();

                var q = from p in VNSC.LoaiHinhs
                        select p;

                foreach (LoaiHinh k in q)
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

        public static List<LoaiHinh> TraCuuDSLoaiHinhTheoTen(string sTen)
        {
            try
            {
                VNSCDataContext VNSC = SQL_Connection.CreateSQlConnection();
                List<LoaiHinh> list = new List<LoaiHinh>();

                var q = from p in VNSC.LoaiHinhs
                        where p.Ten.Contains(sTen)
                        select p;
                foreach (LoaiHinh k in q)
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

        public static List<LoaiHinh> TraCuuDSLoaiHinhTheoMaNhomLoaiHinh(String sMa)
        {
            try
            {
                VNSCDataContext VNSC = SQL_Connection.CreateSQlConnection();
                List<LoaiHinh> list = new List<LoaiHinh>();

                var q = from p in VNSC.LoaiHinhs
                        where p.MaNhomLoaiHinh == sMa
                        select p;
                foreach (LoaiHinh k in q)
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

        public static List<LoaiHinh> TraCuuDSLoaiHinhTheoTenNhomLoaiHinh(String sTen)
        {
            try
            {
                VNSCDataContext VNSC = SQL_Connection.CreateSQlConnection();
                NhomLoaiHinh NhomLoaiHinh = NhomLoaiHinh_DAO.TraCuuNhomLoaiHinhTheoTen(sTen);
                List<LoaiHinh> list = new List<LoaiHinh>();

                var q = from p in VNSC.LoaiHinhs
                        where p.MaNhomLoaiHinh == NhomLoaiHinh.Ma
                        select p;
                foreach (LoaiHinh k in q)
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

        public static LoaiHinh TraCuuLoaiHinhTheoMa(string sMa)
        {
            try
            {
                VNSCDataContext VNSC = SQL_Connection.CreateSQlConnection();
                LoaiHinh sk = VNSC.LoaiHinhs.Single(P => P.Ma == sMa);
                return sk;
            }
            catch (System.Exception ex)
            {
                return null;
            }
        }

        public static LoaiHinh TraCuuLoaiHinhTheoTen(string sTen)
        {
            try
            {
                VNSCDataContext VNSC = SQL_Connection.CreateSQlConnection();
                LoaiHinh sk = VNSC.LoaiHinhs.Single(P => P.Ten == sTen);
                return sk;
            }
            catch (System.Exception ex)
            {
                return null;
            }
        }

        public static bool Insert(LoaiHinh dto)
        {
            try
            {
                VNSCDataContext VNSC = SQL_Connection.CreateSQlConnection();

                VNSC.LoaiHinhs.InsertOnSubmit(dto);
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
                LoaiHinh dto = VNSC.LoaiHinhs.Single(P => P.Ma == sMa);

                VNSC.LoaiHinhs.DeleteOnSubmit(dto);
                VNSC.SubmitChanges();
                return true;
            }
            catch (System.Exception ex)
            {
                return false;
            }
        }

        public static bool UpdateLoaiHinhInfo(LoaiHinh dto)
        {
            try
            {
                VNSCDataContext VNSC = SQL_Connection.CreateSQlConnection();
                LoaiHinh sk = VNSC.LoaiHinhs.Single(P => P.Ma == dto.Ma);
                sk.MaNhomLoaiHinh = dto.MaNhomLoaiHinh;
                sk.Ten = dto.Ten;
                sk.Nganh = dto.Nganh;
                sk.MoTa = dto.MoTa;

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
