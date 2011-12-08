using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAO
{
    public class SuKien_TrachVuSuKien_DAO
    {
        public static List<SuKien_TrachVuSuKien> LayDSSuKien_TrachVuSuKien()
        {
            try
            {
                VNSCDataContext VNSC = SQL_Connection.CreateSQlConnection();
                List<SuKien_TrachVuSuKien> list = new List<SuKien_TrachVuSuKien>();

                var q = from p in VNSC.SuKien_TrachVuSuKiens
                        select p;

                foreach (SuKien_TrachVuSuKien k in q)
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

        public static List<SuKien_TrachVuSuKien> TraCuuDSSuKien_TrachVuSuKienTheoMaSuKien(int iMa)
        {
            try
            {
                VNSCDataContext VNSC = SQL_Connection.CreateSQlConnection();
                List<SuKien_TrachVuSuKien> list = new List<SuKien_TrachVuSuKien>();

                var q = from p in VNSC.SuKien_TrachVuSuKiens
                        where p.MaSuKien == iMa
                        select p;

                foreach (SuKien_TrachVuSuKien k in q)
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

        public static bool Insert(SuKien_TrachVuSuKien dto)
        {
            try
            {
                VNSCDataContext VNSC = SQL_Connection.CreateSQlConnection();

                VNSC.SuKien_TrachVuSuKiens.InsertOnSubmit(dto);
                VNSC.SubmitChanges();
                return true;
            }
            catch (System.Exception ex)
            {
                return false;
            }
        }

        public static bool Delete(int iMaSuKien, int iMaTrachVuSuKien)
        {
            try
            {
                VNSCDataContext VNSC = SQL_Connection.CreateSQlConnection();
                SuKien_TrachVuSuKien dto = VNSC.SuKien_TrachVuSuKiens.Single(P => P.MaSuKien == iMaSuKien && P.MaTrachVuSuKien == iMaTrachVuSuKien);

                VNSC.SuKien_TrachVuSuKiens.DeleteOnSubmit(dto);
                VNSC.SubmitChanges();
                return true;
            }
            catch (System.Exception ex)
            {
                return false;
            }
        }

        public static bool DeleteAll(int iMaSuKien)
        {
            try
            {
                VNSCDataContext VNSC = SQL_Connection.CreateSQlConnection();

                var q = from p in VNSC.SuKien_TrachVuSuKiens
                        where p.MaSuKien == iMaSuKien
                        select p;

                foreach (SuKien_TrachVuSuKien k in q)
                {
                    VNSC.SuKien_TrachVuSuKiens.DeleteOnSubmit(k);
                }

                VNSC.SubmitChanges();
                return true;
            }
            catch (System.Exception ex)
            {
                return false;
            }
        }

        public static bool UpdateSuKien_TrachVuSuKienInfo(SuKien_TrachVuSuKien dto)
        {
            //try
            //{
            //    VNSCDataContext VNSC = SQL_Connection.CreateSQlConnection();
            //    SuKien_TrachVuSuKien sk = VNSC.SuKien_TrachVuSuKiens.Single(P => P.Ma == dto.Ma);
            //    sk.MaNhomTrachVu = dto.MaNhomTrachVu;
            //    //sk.Ten = dto.Ten;
            //    //sk.MoTa = dto.MoTa;

            //    VNSC.SubmitChanges();
            //    return true;
            //}
            //catch (System.Exception ex)
            //{
            //    return false;
            //}

            return false; //Khong co update
        }
    }
}
