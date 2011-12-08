using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAO
{
    public class HoSo_LuuTru_DAO
    {
        public static List<HoSo_LuuTru> LayDSHoSo_LuuTru()
        {
            try
            {
                VNSCDataContext VNSC = SQL_Connection.CreateSQlConnection();
                List<HoSo_LuuTru> list = new List<HoSo_LuuTru>();

                var q = from p in VNSC.HoSo_LuuTrus
                        select p;

                foreach (HoSo_LuuTru k in q)
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

        public static List<HoSo_LuuTru> TraCuuDSLuuTruTheoMaHoSo(string sMa)
        {
            try
            {
                VNSCDataContext VNSC = SQL_Connection.CreateSQlConnection();
                List<HoSo_LuuTru> list = new List<HoSo_LuuTru>();

                var q = from p in VNSC.HoSo_LuuTrus
                        where p.MaHoSo == sMa
                        select p;
                foreach (HoSo_LuuTru k in q)
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

        public static bool Insert(HoSo_LuuTru dto)
        {
            try
            {
                VNSCDataContext VNSC = SQL_Connection.CreateSQlConnection();

                VNSC.HoSo_LuuTrus.InsertOnSubmit(dto);
                VNSC.SubmitChanges();
                return true;
            }
            catch (System.Exception ex)
            {
                return false;
            }
        }

        public static bool Delete(string sMaHoSo, int iMaLuuTru)
        {
            try
            {
                VNSCDataContext VNSC = SQL_Connection.CreateSQlConnection();
                HoSo_LuuTru dto = VNSC.HoSo_LuuTrus.Single(P => P.MaHoSo == sMaHoSo && P.MaLuuTru == iMaLuuTru);

                VNSC.HoSo_LuuTrus.DeleteOnSubmit(dto);
                VNSC.SubmitChanges();
                return true;
            }
            catch (System.Exception ex)
            {
                return false;
            }
        }

        public static bool DeleteAll(string sMaHoSo)
        {
            try
            {
                VNSCDataContext VNSC = SQL_Connection.CreateSQlConnection();
                var q = from p in VNSC.HoSo_LuuTrus
                        where p.MaHoSo == sMaHoSo
                        select p;
                foreach (HoSo_LuuTru k in q)
                {
                    VNSC.HoSo_LuuTrus.DeleteOnSubmit(k);
                }

                VNSC.SubmitChanges();
                return true;
            }
            catch (System.Exception ex)
            {
                return false;
            }
        }

        public static bool UpdateHoSo_LuuTruInfo(HoSo_LuuTru dto)
        {
            //try
            //{
            //    VNSCDataContext VNSC = SQL_Connection.CreateSQlConnection();
            //    HoSo_LuuTru sk = VNSC.HoSo_LuuTrus.Single(P => P.Ma == dto.Ma);
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
