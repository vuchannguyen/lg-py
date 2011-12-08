using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAO
{
    public class SuKien_DieuPhoi_DAO
    {
        public static List<SuKien_DieuPhoi> LayDSSuKien_DieuPhoi()
        {
            try
            {
                VNSCDataContext VNSC = SQL_Connection.CreateSQlConnection();
                List<SuKien_DieuPhoi> list = new List<SuKien_DieuPhoi>();

                var q = from p in VNSC.SuKien_DieuPhois
                        select p;

                foreach (SuKien_DieuPhoi k in q)
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

        public static List<SuKien_DieuPhoi> TraCuuDSSuKien_DieuPhoiTheoMaSuKien(int iMa)
        {
            try
            {
                VNSCDataContext VNSC = SQL_Connection.CreateSQlConnection();
                List<SuKien_DieuPhoi> list = new List<SuKien_DieuPhoi>();

                var q = from p in VNSC.SuKien_DieuPhois
                        where p.MaSuKien == iMa
                        select p;

                foreach (SuKien_DieuPhoi k in q)
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

        public static bool Insert(SuKien_DieuPhoi dto)
        {
            try
            {
                VNSCDataContext VNSC = SQL_Connection.CreateSQlConnection();

                VNSC.SuKien_DieuPhois.InsertOnSubmit(dto);
                VNSC.SubmitChanges();
                return true;
            }
            catch (System.Exception ex)
            {
                return false;
            }
        }

        public static bool Delete(int iMaSuKien, int iMaDieuPhoi)
        {
            try
            {
                VNSCDataContext VNSC = SQL_Connection.CreateSQlConnection();
                SuKien_DieuPhoi dto = VNSC.SuKien_DieuPhois.Single(P => P.MaSuKien == iMaSuKien && P.MaDieuPhoi == iMaDieuPhoi);

                VNSC.SuKien_DieuPhois.DeleteOnSubmit(dto);
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

                var q = from p in VNSC.SuKien_DieuPhois
                        where p.MaSuKien == iMaSuKien
                        select p;

                foreach (SuKien_DieuPhoi k in q)
                {
                    VNSC.SuKien_DieuPhois.DeleteOnSubmit(k);
                }

                VNSC.SubmitChanges();
                return true;
            }
            catch (System.Exception ex)
            {
                return false;
            }
        }

        public static bool UpdateSuKien_DieuPhoiInfo(SuKien_DieuPhoi dto)
        {
            //try
            //{
            //    VNSCDataContext VNSC = SQL_Connection.CreateSQlConnection();
            //    SuKien_DieuPhoi sk = VNSC.SuKien_DieuPhois.Single(P => P.Ma == dto.Ma);
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
