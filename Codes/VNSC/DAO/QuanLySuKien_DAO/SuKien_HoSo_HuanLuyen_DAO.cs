using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAO
{
    public class SuKien_HoSo_HuanLuyen_DAO
    {
        public static List<SuKien_HoSo_HuanLuyen> LayDSSuKien_HoSo_HuanLuyen()
        {
            try
            {
                VNSCDataContext VNSC = SQL_Connection.CreateSQlConnection();
                List<SuKien_HoSo_HuanLuyen> list = new List<SuKien_HoSo_HuanLuyen>();

                var q = from p in VNSC.SuKien_HoSo_HuanLuyens
                        select p;

                foreach (SuKien_HoSo_HuanLuyen k in q)
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

        public static List<SuKien_HoSo_HuanLuyen> TraCuuDSSuKien_HuanLuyenTheoMaSuKien_HoSo(string sMa)
        {
            try
            {
                VNSCDataContext VNSC = SQL_Connection.CreateSQlConnection();
                List<SuKien_HoSo_HuanLuyen> list = new List<SuKien_HoSo_HuanLuyen>();

                var q = from p in VNSC.SuKien_HoSo_HuanLuyens
                        where p.MaSuKien_HoSo == sMa
                        select p;

                foreach (SuKien_HoSo_HuanLuyen k in q)
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

        public static bool Insert(SuKien_HoSo_HuanLuyen dto)
        {
            try
            {
                VNSCDataContext VNSC = SQL_Connection.CreateSQlConnection();

                VNSC.SuKien_HoSo_HuanLuyens.InsertOnSubmit(dto);
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
                SuKien_HoSo_HuanLuyen dto = VNSC.SuKien_HoSo_HuanLuyens.Single(P => P.MaSuKien_HoSo == sMaHoSo && P.MaSuKien_HuanLuyen == iMaHuanLuyen);

                VNSC.SuKien_HoSo_HuanLuyens.DeleteOnSubmit(dto);
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
                var q = from p in VNSC.SuKien_HoSo_HuanLuyens
                        where p.MaSuKien_HoSo == sMaHoSo
                        select p;
                foreach (SuKien_HoSo_HuanLuyen k in q)
                {
                    VNSC.SuKien_HoSo_HuanLuyens.DeleteOnSubmit(k);
                }

                VNSC.SubmitChanges();
                return true;
            }
            catch (System.Exception ex)
            {
                return false;
            }
        }

        public static bool UpdateSuKien_HoSo_HuanLuyenInfo(SuKien_HoSo_HuanLuyen dto)
        {
            //try
            //{
            //    VNSCDataContext VNSC = SQL_Connection.CreateSQlConnection();
            //    SuKien_HoSo_HuanLuyen sk = VNSC.SuKien_HoSo_HuanLuyens.Single(P => P.Ma == dto.Ma);
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
