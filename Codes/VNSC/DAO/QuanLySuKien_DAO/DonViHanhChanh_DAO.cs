using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAO
{
    public class DonViHanhChanh_DAO
    {
        public static List<DonViHanhChanh> LayDSDonViHanhChanh()
        {
            try
            {
                VNSCDataContext VNSC = SQL_Connection.CreateSQlConnection();
                List<DonViHanhChanh> list = new List<DonViHanhChanh>();

                var q = from p in VNSC.DonViHanhChanhs
                        select p;

                foreach (DonViHanhChanh k in q)
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

        public static List<DonViHanhChanh> TraCuuDSDonViHanhChanhTheoMaSuKien(int iMa)
        {
            try
            {
                VNSCDataContext VNSC = SQL_Connection.CreateSQlConnection();
                List<DonViHanhChanh> list = new List<DonViHanhChanh>();

                var q = from p in VNSC.SuKien_DonViHanhChanhs
                        where p.MaSuKien == iMa
                        select p;

                foreach (SuKien_DonViHanhChanh k in q)
                {
                    list.Add(VNSC.DonViHanhChanhs.Single(P => P.Ma == k.MaDonViHanhChanh));
                }

                return list;
            }
            catch (System.Exception ex)
            {
                return null;
            }
        }

        public static List<DonViHanhChanh> TraCuuDSDonViHanhChanhTheoCapQuanTriNhoHon(int iMaSuKien, int iCap)
        {
            try
            {
                List<DonViHanhChanh> list = new List<DonViHanhChanh>();
                List<DonViHanhChanh> q = TraCuuDSDonViHanhChanhTheoMaSuKien(iMaSuKien);

                foreach (DonViHanhChanh k in q)
                {
                    if (k.PhanCap > iCap)
                    {
                        list.Add(k);
                    }
                }
                return list;
            }
            catch (System.Exception ex)
            {
                return null;
            }
        }

        public static List<DonViHanhChanh> TraCuuDSDonViHanhChanhTheoCapQuanTriLonHon(int iMaSuKien, int iCap)
        {
            try
            {
                List<DonViHanhChanh> list = new List<DonViHanhChanh>();
                List<DonViHanhChanh> q = TraCuuDSDonViHanhChanhTheoMaSuKien(iMaSuKien);

                foreach (DonViHanhChanh k in q)
                {
                    if (k.PhanCap < iCap)
                    {
                        list.Add(k);
                    }
                }
                return list;
            }
            catch (System.Exception ex)
            {
                return null;
            }
        }

        public static List<DonViHanhChanh> TraCuuDSDonViHanhChanhTheoTen(string sTen)
        {
            try
            {
                VNSCDataContext VNSC = SQL_Connection.CreateSQlConnection();
                List<DonViHanhChanh> list = new List<DonViHanhChanh>();

                var q = from p in VNSC.DonViHanhChanhs
                        where p.Ten.Contains(sTen)
                        select p;

                foreach (DonViHanhChanh k in q)
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

        public static DonViHanhChanh TraCuuDonViHanhChanhTheoMa(int iMa)
        {
            try
            {
                VNSCDataContext VNSC = SQL_Connection.CreateSQlConnection();
                DonViHanhChanh sk = VNSC.DonViHanhChanhs.Single(P => P.Ma == iMa);
                return sk;
            }
            catch (System.Exception ex)
            {
                return null;
            }
        }

        public static DonViHanhChanh TraCuuDonViHanhChanhTheoTen(string sTen)
        {
            try
            {
                VNSCDataContext VNSC = SQL_Connection.CreateSQlConnection();
                DonViHanhChanh sk = VNSC.DonViHanhChanhs.Single(P => P.Ten == sTen);
                return sk;
            }
            catch (System.Exception ex)
            {
                return null;
            }
        }

        public static bool Insert(DonViHanhChanh dto)
        {
            try
            {
                VNSCDataContext VNSC = SQL_Connection.CreateSQlConnection();

                VNSC.DonViHanhChanhs.InsertOnSubmit(dto);
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
                DonViHanhChanh dto = VNSC.DonViHanhChanhs.Single(P => P.Ma == iMa);

                VNSC.DonViHanhChanhs.DeleteOnSubmit(dto);
                VNSC.SubmitChanges();
                return true;
            }
            catch (System.Exception ex)
            {
                return false;
            }
        }

        public static bool UpdateDonViHanhChanhInfo(DonViHanhChanh dto)
        {
            try
            {
                VNSCDataContext VNSC = SQL_Connection.CreateSQlConnection();
                DonViHanhChanh sk = VNSC.DonViHanhChanhs.Single(P => P.Ma == dto.Ma);

                sk.Ten = dto.Ten;
                sk.PhanCap = dto.PhanCap;
                sk.CapQuanTri = dto.CapQuanTri;
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
