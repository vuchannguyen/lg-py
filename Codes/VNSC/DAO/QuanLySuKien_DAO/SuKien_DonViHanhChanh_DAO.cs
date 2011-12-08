using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAO
{
    public class SuKien_DonViHanhChanh_DAO
    {
        public static List<SuKien_DonViHanhChanh> LayDSSuKien_DonViHanhChanh()
        {
            try
            {
                VNSCDataContext VNSC = SQL_Connection.CreateSQlConnection();
                List<SuKien_DonViHanhChanh> list = new List<SuKien_DonViHanhChanh>();

                var q = from p in VNSC.SuKien_DonViHanhChanhs
                        select p;

                foreach (SuKien_DonViHanhChanh k in q)
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

        public static List<SuKien_DonViHanhChanh> TraCuuDSSuKien_DonViHanhChanhTheoMaSuKien(int iMa)
        {
            try
            {
                VNSCDataContext VNSC = SQL_Connection.CreateSQlConnection();
                List<SuKien_DonViHanhChanh> list = new List<SuKien_DonViHanhChanh>();

                var q = from p in VNSC.SuKien_DonViHanhChanhs
                        where p.MaSuKien == iMa
                        select p;

                foreach (SuKien_DonViHanhChanh k in q)
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

        public static bool Insert(SuKien_DonViHanhChanh dto)
        {
            try
            {
                VNSCDataContext VNSC = SQL_Connection.CreateSQlConnection();

                VNSC.SuKien_DonViHanhChanhs.InsertOnSubmit(dto);
                VNSC.SubmitChanges();
                return true;
            }
            catch (System.Exception ex)
            {
                return false;
            }
        }

        public static bool Delete(int iMaSuKien, int iMaDonViHanhChanh)
        {
            try
            {
                VNSCDataContext VNSC = SQL_Connection.CreateSQlConnection();
                SuKien_DonViHanhChanh dto = VNSC.SuKien_DonViHanhChanhs.Single(P => P.MaSuKien == iMaSuKien && P.MaDonViHanhChanh == iMaDonViHanhChanh);

                VNSC.SuKien_DonViHanhChanhs.DeleteOnSubmit(dto);
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

                var q = from p in VNSC.SuKien_DonViHanhChanhs
                        where p.MaSuKien == iMaSuKien
                        select p;

                foreach (SuKien_DonViHanhChanh k in q)
                {
                    VNSC.SuKien_DonViHanhChanhs.DeleteOnSubmit(k);
                }

                VNSC.SubmitChanges();
                return true;
            }
            catch (System.Exception ex)
            {
                return false;
            }
        }

        public static bool UpdateSuKien_DonViHanhChanhInfo(SuKien_DonViHanhChanh dto)
        {
            //try
            //{
            //    VNSCDataContext VNSC = SQL_Connection.CreateSQlConnection();
            //    SuKien_DonViHanhChanh sk = VNSC.SuKien_DonViHanhChanhs.Single(P => P.Ma == dto.Ma);
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
