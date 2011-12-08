using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAO
{
    public class HuanLuyen_DAO
    {
        public static List<HuanLuyen> LayDSHuanLuyen()
        {
            try
            {
                VNSCDataContext VNSC = SQL_Connection.CreateSQlConnection();
                List<HuanLuyen> list = new List<HuanLuyen>();

                var q = from p in VNSC.HuanLuyens
                        select p;

                foreach (HuanLuyen k in q)
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

        public static HuanLuyen TraCuuHuanLuyenTheoMa(int iMa)
        {
            try
            {
                VNSCDataContext VNSC = SQL_Connection.CreateSQlConnection();
                HuanLuyen sk = VNSC.HuanLuyens.Single(P => P.Ma == iMa);
                return sk;
            }
            catch (System.Exception ex)
            {
                return null;
            }
        }

        public static HuanLuyen TraCuuHuanLuyenTheoTen(string sTen)
        {
            try
            {
                VNSCDataContext VNSC = SQL_Connection.CreateSQlConnection();
                HuanLuyen sk = VNSC.HuanLuyens.Single(P => P.TenKhoa == sTen);
                return sk;
            }
            catch (System.Exception ex)
            {
                return null;
            }
        }

        public static bool Insert(HuanLuyen dto)
        {
            try
            {
                VNSCDataContext VNSC = SQL_Connection.CreateSQlConnection();

                VNSC.HuanLuyens.InsertOnSubmit(dto);
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
                HuanLuyen dto = VNSC.HuanLuyens.Single(P => P.Ma == iMa);

                VNSC.HuanLuyens.DeleteOnSubmit(dto);
                VNSC.SubmitChanges();
                return true;
            }
            catch (System.Exception ex)
            {
                return false;
            }
        }

        public static bool UpdateHuanLuyenInfo(HuanLuyen dto)
        {
            try
            {
                VNSCDataContext VNSC = SQL_Connection.CreateSQlConnection();
                HuanLuyen sk = VNSC.HuanLuyens.Single(P => P.Ma == dto.Ma);

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
