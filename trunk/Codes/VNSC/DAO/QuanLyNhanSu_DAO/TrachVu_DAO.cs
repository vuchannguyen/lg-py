using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAO
{
    public class TrachVu_DAO
    {
        public static List<TrachVu> LayDSTrachVu()
        {
            try
            {
                VNSCDataContext VNSC = SQL_Connection.CreateSQlConnection();
                List<TrachVu> list = new List<TrachVu>();

                var q = from p in VNSC.TrachVus
                        select p;

                foreach (TrachVu k in q)
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

        public static List<TrachVu> TraCuuDSTrachVuTheoTen(string sTen)
        {
            try
            {
                VNSCDataContext VNSC = SQL_Connection.CreateSQlConnection();
                List<TrachVu> list = new List<TrachVu>();

                var q = from p in VNSC.TrachVus
                        where p.Ten.Contains(sTen)
                        select p;

                foreach (TrachVu k in q)
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

        public static List<TrachVu> TraCuuDSTrachVuTheoMaNhomTrachVu(String sMa)
        {
            try
            {
                VNSCDataContext VNSC = SQL_Connection.CreateSQlConnection();
                List<TrachVu> list = new List<TrachVu>();

                var q = from p in VNSC.TrachVus
                        where p.MaNhomTrachVu == sMa
                        select p;

                foreach (TrachVu k in q)
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

        public static List<TrachVu> TraCuuDSTrachVuTheoTenNhomTrachVu(String sTen)
        {
            try
            {
                VNSCDataContext VNSC = SQL_Connection.CreateSQlConnection();
                NhomTrachVu NhomTrachVu = NhomTrachVu_DAO.TraCuuNhomTrachVuTheoTen(sTen);
                List<TrachVu> list = new List<TrachVu>();

                var q = from p in VNSC.TrachVus
                        where p.MaNhomTrachVu == NhomTrachVu.Ma
                        select p;
                foreach (TrachVu k in q)
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

        public static TrachVu TraCuuTrachVuTheoMa(string sMa)
        {
            try
            {
                VNSCDataContext VNSC = SQL_Connection.CreateSQlConnection();
                TrachVu sk = VNSC.TrachVus.Single(P => P.Ma == sMa);
                return sk;
            }
            catch (System.Exception ex)
            {
                return null;
            }
        }

        public static TrachVu TraCuuTrachVuTheoTen(string sTen)
        {
            try
            {
                VNSCDataContext VNSC = SQL_Connection.CreateSQlConnection();
                TrachVu sk = VNSC.TrachVus.Single(P => P.Ten == sTen);
                return sk;
            }
            catch (System.Exception ex)
            {
                return null;
            }
        }

        public static bool Insert(TrachVu dto)
        {
            try
            {
                VNSCDataContext VNSC = SQL_Connection.CreateSQlConnection();

                VNSC.TrachVus.InsertOnSubmit(dto);
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
                TrachVu dto = VNSC.TrachVus.Single(P => P.Ma == sMa);

                VNSC.TrachVus.DeleteOnSubmit(dto);
                VNSC.SubmitChanges();
                return true;
            }
            catch (System.Exception ex)
            {
                return false;
            }
        }

        public static bool UpdateTrachVuInfo(TrachVu dto)
        {
            try
            {
                VNSCDataContext VNSC = SQL_Connection.CreateSQlConnection();
                TrachVu sk = VNSC.TrachVus.Single(P => P.Ma == dto.Ma);

                sk.MaNhomTrachVu = dto.MaNhomTrachVu;
                sk.Ten = dto.Ten;
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
