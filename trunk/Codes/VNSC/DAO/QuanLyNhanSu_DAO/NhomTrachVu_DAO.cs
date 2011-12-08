using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAO
{
    public class NhomTrachVu_DAO
    {
        public static List<NhomTrachVu> LayDSNhomTrachVu()
        {
            try
            {
                VNSCDataContext VNSC = SQL_Connection.CreateSQlConnection();
                List<NhomTrachVu> list = new List<NhomTrachVu>();

                var q = from p in VNSC.NhomTrachVus
                        select p;

                foreach (NhomTrachVu k in q)
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

        public static List<NhomTrachVu> TraCuuDSNhomTrachVuTheoTen(string sTen)
        {
            try
            {
                VNSCDataContext VNSC = SQL_Connection.CreateSQlConnection();
                List<NhomTrachVu> list = new List<NhomTrachVu>();

                var q = from p in VNSC.NhomTrachVus
                        where p.Ten.Contains(sTen)
                        select p;
                foreach (NhomTrachVu k in q)
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

        public static NhomTrachVu TraCuuNhomTrachVuTheoMa(string sMa)
        {
            try
            {
                VNSCDataContext VNSC = SQL_Connection.CreateSQlConnection();
                NhomTrachVu sk = VNSC.NhomTrachVus.Single(P => P.Ma == sMa);
                return sk;
            }
            catch (System.Exception ex)
            {
                return null;
            }
        }

        public static NhomTrachVu TraCuuNhomTrachVuTheoTen(string sTen)
        {
            try
            {
                VNSCDataContext VNSC = SQL_Connection.CreateSQlConnection();
                NhomTrachVu sk = VNSC.NhomTrachVus.Single(P => P.Ten == sTen);
                return sk;
            }
            catch (System.Exception ex)
            {
                return null;
            }
        }

        public static bool Insert(NhomTrachVu dto)
        {
            try
            {
                VNSCDataContext VNSC = SQL_Connection.CreateSQlConnection();

                VNSC.NhomTrachVus.InsertOnSubmit(dto);
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
                NhomTrachVu dto = VNSC.NhomTrachVus.Single(P => P.Ma == sMa);

                VNSC.NhomTrachVus.DeleteOnSubmit(dto);
                VNSC.SubmitChanges();
                return true;
            }
            catch (System.Exception ex)
            {
                return false;
            }
        }

        public static bool UpdateNhomTrachVuInfo(NhomTrachVu dto)
        {
            try
            {
                VNSCDataContext VNSC = SQL_Connection.CreateSQlConnection();
                NhomTrachVu sk = VNSC.NhomTrachVus.Single(P => P.Ma == dto.Ma);

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
