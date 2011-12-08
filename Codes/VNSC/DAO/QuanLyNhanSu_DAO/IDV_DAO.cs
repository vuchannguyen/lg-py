using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAO
{
    public class IDV_DAO
    {
        public static List<IDV> LayDSIDV()
        {
            try
            {
                VNSCDataContext VNSC = SQL_Connection.CreateSQlConnection();
                List<IDV> list = new List<IDV>();

                var q = from p in VNSC.IDVs
                        select p;

                foreach (IDV k in q)
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

        public static List<IDV> TraCuuDSIDVTheoDienGiai(string sDienGiai)
        {
            try
            {
                VNSCDataContext VNSC = SQL_Connection.CreateSQlConnection();
                List<IDV> list = new List<IDV>();

                var q = from p in VNSC.IDVs
                        where p.DienGiai.Contains(sDienGiai)
                        select p;
                foreach (IDV k in q)
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

        public static IDV TraCuuIDVTheoMa(int iMa)
        {
            try
            {
                VNSCDataContext VNSC = SQL_Connection.CreateSQlConnection();
                IDV sk = VNSC.IDVs.Single(P => P.Ma == iMa);
                return sk;
            }
            catch (System.Exception ex)
            {
                return null;
            }
        }

        public static IDV TraCuuIDVTheoDienGiai(string sDienGiai)
        {
            try
            {
                VNSCDataContext VNSC = SQL_Connection.CreateSQlConnection();
                IDV sk = VNSC.IDVs.Single(P => P.DienGiai == sDienGiai);
                return sk;
            }
            catch (System.Exception ex)
            {
                return null;
            }
        }

        private static bool KiemTraIDVInsertBiTrung(IDV dto)
        {
            List<IDV> list_IDV = LayDSIDV();
            foreach (IDV Temp in list_IDV)
            {
                if (Temp.IDV1 == dto.IDV1)
                {
                    return false;
                }
            }

            return true;
        }

        public static bool Insert(IDV dto)
        {
            try
            {
                if (!KiemTraIDVInsertBiTrung(dto))
                {
                    return false;
                }

                VNSCDataContext VNSC = SQL_Connection.CreateSQlConnection();

                VNSC.IDVs.InsertOnSubmit(dto);
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
                IDV dto = VNSC.IDVs.Single(P => P.Ma == iMa);

                VNSC.IDVs.DeleteOnSubmit(dto);
                VNSC.SubmitChanges();
                return true;
            }
            catch (System.Exception ex)
            {
                return false;
            }
        }

        private static bool KiemTraIDVUpdateBiTrung(IDV dto)
        {
            List<IDV> list_IDV = LayDSIDV();
            foreach (IDV Temp in list_IDV)
            {
                if (Temp.IDV1 == dto.IDV1)
                {
                    if (Temp.Ma != dto.Ma)
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        public static bool UpdateIDVInfo(IDV dto)
        {
            try
            {
                if (!KiemTraIDVUpdateBiTrung(dto))
                {
                    return false;
                }

                VNSCDataContext VNSC = SQL_Connection.CreateSQlConnection();
                IDV sk = VNSC.IDVs.Single(P => P.Ma == dto.Ma);

                sk.IDV1 = dto.IDV1;
                sk.DienGiai = dto.DienGiai;
                sk.MatKhau = dto.MatKhau;
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
