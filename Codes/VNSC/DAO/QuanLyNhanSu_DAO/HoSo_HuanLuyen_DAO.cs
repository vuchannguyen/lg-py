using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAO
{
    public class HoSo_HuanLuyen_DAO
    {
        public static List<HoSo_HuanLuyen> LayDSHoSo_HuanLuyen()
        {
            try
            {
                VNSCDataContext VNSC = SQL_Connection.CreateSQlConnection();
                List<HoSo_HuanLuyen> list = new List<HoSo_HuanLuyen>();

                var q = from p in VNSC.HoSo_HuanLuyens
                        select p;

                foreach (HoSo_HuanLuyen k in q)
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

        public static List<HoSo_HuanLuyen> TraCuuDSHuanLuyenTheoMaHoSo(string sMa)
        {
            try
            {
                VNSCDataContext VNSC = SQL_Connection.CreateSQlConnection();
                List<HoSo_HuanLuyen> list = new List<HoSo_HuanLuyen>();

                var q = from p in VNSC.HoSo_HuanLuyens
                        where p.MaHoSo == sMa
                        select p;

                foreach (HoSo_HuanLuyen k in q)
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

        public static bool Insert(HoSo_HuanLuyen dto)
        {
            try
            {
                VNSCDataContext VNSC = SQL_Connection.CreateSQlConnection();

                VNSC.HoSo_HuanLuyens.InsertOnSubmit(dto);
                VNSC.SubmitChanges();
                return true;
            }
            catch (System.Exception ex)
            {
                return false;
            }
        }

        public static bool Delete(string sMaHoSo, int iMaHuanLuyen)
        {
            try
            {
                VNSCDataContext VNSC = SQL_Connection.CreateSQlConnection();
                HoSo_HuanLuyen dto = VNSC.HoSo_HuanLuyens.Single(P => P.MaHoSo == sMaHoSo && P.MaHuanLuyen == iMaHuanLuyen);

                VNSC.HoSo_HuanLuyens.DeleteOnSubmit(dto);
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
                var q = from p in VNSC.HoSo_HuanLuyens
                        where p.MaHoSo == sMaHoSo
                        select p;
                foreach (HoSo_HuanLuyen k in q)
                {
                    VNSC.HoSo_HuanLuyens.DeleteOnSubmit(k);
                }

                VNSC.SubmitChanges();
                return true;
            }
            catch (System.Exception ex)
            {
                return false;
            }
        }

        public static bool UpdateHoSo_HuanLuyenInfo(HoSo_HuanLuyen dto)
        {
            //try
            //{
            //    VNSCDataContext VNSC = SQL_Connection.CreateSQlConnection();
            //    HoSo_HuanLuyen sk = VNSC.HoSo_HuanLuyens.Single(P => P.Ma == dto.Ma);
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
