using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAO
{
    public class TrachVuSuKien_DAO
    {
        public static List<TrachVuSuKien> LayDSTrachVuSuKien()
        {
            try
            {
                VNSCDataContext VNSC = SQL_Connection.CreateSQlConnection();
                List<TrachVuSuKien> list = new List<TrachVuSuKien>();

                var q = from p in VNSC.TrachVuSuKiens
                        select p;

                foreach (TrachVuSuKien k in q)
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

        public static List<TrachVuSuKien> TraCuuDSTrachVuSuKienTheoMaSuKien(int iMa)
        {
            try
            {
                VNSCDataContext VNSC = SQL_Connection.CreateSQlConnection();
                List<TrachVuSuKien> list = new List<TrachVuSuKien>();

                var q = from p in VNSC.SuKien_TrachVuSuKiens
                        where p.MaSuKien == iMa
                        select p;

                foreach (SuKien_TrachVuSuKien k in q)
                {
                    list.Add(VNSC.TrachVuSuKiens.Single(P => P.Ma == k.MaTrachVuSuKien));
                }

                return list;
            }
            catch (System.Exception ex)
            {
                return null;
            }
        }

        public static List<TrachVuSuKien> TraCuuDSTrachVuSuKienTheoMaDonViHanhChanh(int iMa)
        {
            try
            {
                VNSCDataContext VNSC = SQL_Connection.CreateSQlConnection();
                List<TrachVuSuKien> list = new List<TrachVuSuKien>();

                var q = from p in VNSC.TrachVuSuKiens
                        where p.MaDonViHanhChanh == iMa
                        select p;

                foreach (TrachVuSuKien k in q)
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

        public static List<TrachVuSuKien> TraCuuDSTrachVuSuKienTheoTen(string sTen)
        {
            try
            {
                VNSCDataContext VNSC = SQL_Connection.CreateSQlConnection();
                List<TrachVuSuKien> list = new List<TrachVuSuKien>();

                var q = from p in VNSC.TrachVuSuKiens
                        where p.Ten.Contains(sTen)
                        select p;

                foreach (TrachVuSuKien k in q)
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

        public static TrachVuSuKien TraCuuTrachVuSuKienTheoMa(int iMa)
        {
            try
            {
                VNSCDataContext VNSC = SQL_Connection.CreateSQlConnection();
                TrachVuSuKien sk = VNSC.TrachVuSuKiens.Single(P => P.Ma == iMa);
                return sk;
            }
            catch (System.Exception ex)
            {
                return null;
            }
        }

        public static TrachVuSuKien TraCuuTrachVuSuKienTheoTen(string sTen)
        {
            try
            {
                VNSCDataContext VNSC = SQL_Connection.CreateSQlConnection();
                TrachVuSuKien sk = VNSC.TrachVuSuKiens.Single(P => P.Ten == sTen);
                return sk;
            }
            catch (System.Exception ex)
            {
                return null;
            }
        }

        public static bool Insert(TrachVuSuKien dto)
        {
            try
            {
                VNSCDataContext VNSC = SQL_Connection.CreateSQlConnection();

                VNSC.TrachVuSuKiens.InsertOnSubmit(dto);
                VNSC.SubmitChanges();
                return true;
            }
            catch (System.Exception ex)
            {
                return false;
            }
        }

        public static bool Delete(int iMa)
        {
            try
            {
                VNSCDataContext VNSC = SQL_Connection.CreateSQlConnection();
                TrachVuSuKien dto = VNSC.TrachVuSuKiens.Single(P => P.Ma == iMa);

                VNSC.TrachVuSuKiens.DeleteOnSubmit(dto);
                VNSC.SubmitChanges();
                return true;
            }
            catch (System.Exception ex)
            {
                return false;
            }
        }

        public static bool UpdateTrachVuSuKienInfo(TrachVuSuKien dto)
        {
            try
            {
                VNSCDataContext VNSC = SQL_Connection.CreateSQlConnection();
                TrachVuSuKien sk = VNSC.TrachVuSuKiens.Single(P => P.Ma == dto.Ma);

                sk.Ten = dto.Ten;
                sk.MaDonViHanhChanh = dto.MaDonViHanhChanh;
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
