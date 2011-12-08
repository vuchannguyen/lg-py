using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAO
{
    public class SuKien_HoSoThamDu_DAO
    {
        public static List<SuKien_HoSoThamDu> LayDSSuKien_HoSoThamDu()
        {
            try
            {
                VNSCDataContext VNSC = SQL_Connection.CreateSQlConnection();
                List<SuKien_HoSoThamDu> list = new List<SuKien_HoSoThamDu>();

                var q = from p in VNSC.SuKien_HoSoThamDus
                        select p;

                foreach (SuKien_HoSoThamDu k in q)
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

        public static List<SuKien_HoSoThamDu> TraCuuDSHoSoThamDuTheoMaSuKien(int iMa)
        {
            try
            {
                VNSCDataContext VNSC = SQL_Connection.CreateSQlConnection();
                List<SuKien_HoSoThamDu> list = new List<SuKien_HoSoThamDu>();

                var q = from p in VNSC.SuKien_HoSoThamDus
                        where p.MaSuKien == iMa
                        select p;

                foreach (SuKien_HoSoThamDu k in q)
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

        public static SuKien_HoSoThamDu TraCuuSuKien_HoSoThamDuTheoMa(int iMaSuKien, string sMaHoSo)
        {
            try
            {
                VNSCDataContext VNSC = SQL_Connection.CreateSQlConnection();
                SuKien_HoSoThamDu sk = VNSC.SuKien_HoSoThamDus.Single(P => P.MaSuKien == iMaSuKien && P.MaSuKien_HoSo == sMaHoSo);
                return sk;
            }
            catch (System.Exception ex)
            {
                return null;
            }
        }

        public static bool Insert(SuKien_HoSoThamDu dto)
        {
            try
            {
                VNSCDataContext VNSC = SQL_Connection.CreateSQlConnection();

                VNSC.SuKien_HoSoThamDus.InsertOnSubmit(dto);
                VNSC.SubmitChanges();
                return true;
            }
            catch (System.Exception ex)
            {
                return false;
            }
        }

        public static bool Delete(int iMaSuKien, string sMaHoSo)
        {
            try
            {
                VNSCDataContext VNSC = SQL_Connection.CreateSQlConnection();
                SuKien_HoSoThamDu dto = VNSC.SuKien_HoSoThamDus.Single(P => P.MaSuKien == iMaSuKien && P.MaSuKien_HoSo == sMaHoSo);

                VNSC.SuKien_HoSoThamDus.DeleteOnSubmit(dto);
                VNSC.SubmitChanges();
                return true;
            }
            catch (System.Exception ex)
            {
                return false;
            }
        }

        public static bool UpdateSuKien_HoSoThamDuInfo(SuKien_HoSoThamDu dto)
        {
            //Khong co update

            return true;
        }
    }
}
