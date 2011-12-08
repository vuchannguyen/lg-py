using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAO
{
    public class SuKien_HuanLuyen_DAO
    {
        public static List<SuKien_HuanLuyen> LayDSSuKien_HuanLuyen()
        {
            try
            {
                VNSCDataContext VNSC = SQL_Connection.CreateSQlConnection();
                List<SuKien_HuanLuyen> list = new List<SuKien_HuanLuyen>();

                var q = from p in VNSC.SuKien_HuanLuyens
                        select p;

                foreach (SuKien_HuanLuyen k in q)
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

        public static SuKien_HuanLuyen TraCuuSuKien_HuanLuyenTheoMa(int iMa)
        {
            try
            {
                VNSCDataContext VNSC = SQL_Connection.CreateSQlConnection();
                SuKien_HuanLuyen sk = VNSC.SuKien_HuanLuyens.Single(P => P.Ma == iMa);
                return sk;
            }
            catch (System.Exception ex)
            {
                return null;
            }
        }

        public static SuKien_HuanLuyen TraCuuSuKien_HuanLuyenTheoTen(string sTen)
        {
            try
            {
                VNSCDataContext VNSC = SQL_Connection.CreateSQlConnection();
                SuKien_HuanLuyen sk = VNSC.SuKien_HuanLuyens.Single(P => P.TenKhoa == sTen);
                return sk;
            }
            catch (System.Exception ex)
            {
                return null;
            }
        }

        public static bool Insert(SuKien_HuanLuyen dto)
        {
            try
            {
                VNSCDataContext VNSC = SQL_Connection.CreateSQlConnection();

                VNSC.SuKien_HuanLuyens.InsertOnSubmit(dto);
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
                SuKien_HuanLuyen dto = VNSC.SuKien_HuanLuyens.Single(P => P.Ma == iMa);

                VNSC.SuKien_HuanLuyens.DeleteOnSubmit(dto);
                VNSC.SubmitChanges();
                return true;
            }
            catch (System.Exception ex)
            {
                return false;
            }
        }

        public static bool UpdateSuKien_HuanLuyenInfo(SuKien_HuanLuyen dto)
        {
            try
            {
                VNSCDataContext VNSC = SQL_Connection.CreateSQlConnection();
                SuKien_HuanLuyen sk = VNSC.SuKien_HuanLuyens.Single(P => P.Ma == dto.Ma);

                sk.Nganh = dto.Nganh;
                sk.Khoa = dto.Khoa;
                sk.TenKhoa = dto.TenKhoa;
                sk.KhoaTruong = dto.KhoaTruong;
                sk.Nam = dto.Nam;
                sk.MHL = dto.MHL;
                sk.TinhTrang = dto.TinhTrang;

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
