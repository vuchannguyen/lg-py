using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAO
{
    public class LuuTru_DAO
    {
        public static List<LuuTru> LayDSLuuTru()
        {
            try
            {
                VNSCDataContext VNSC = SQL_Connection.CreateSQlConnection();
                List<LuuTru> list = new List<LuuTru>();

                var q = from p in VNSC.LuuTrus
                        select p;

                foreach (LuuTru k in q)
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

        public static LuuTru TraCuuLuuTruTheoMa(int iMa)
        {
            try
            {
                VNSCDataContext VNSC = SQL_Connection.CreateSQlConnection();
                LuuTru sk = VNSC.LuuTrus.Single(P => P.Ma == iMa);
                return sk;
            }
            catch (System.Exception ex)
            {
                return null;
            }
        }

        public static LuuTru TraCuuLuuTruTheoNgay(string sNgayCapNhat)
        {
            try
            {
                VNSCDataContext VNSC = SQL_Connection.CreateSQlConnection();
                LuuTru sk = VNSC.LuuTrus.Single(P => P.NgayCapNhat == sNgayCapNhat);

                return sk;
            }
            catch (System.Exception ex)
            {
                return null;
            }
        }

        public static bool Insert(LuuTru dto)
        {
            try
            {
                VNSCDataContext VNSC = SQL_Connection.CreateSQlConnection();

                VNSC.LuuTrus.InsertOnSubmit(dto);
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
                LuuTru dto = VNSC.LuuTrus.Single(P => P.Ma == iMa);

                VNSC.LuuTrus.DeleteOnSubmit(dto);
                VNSC.SubmitChanges();
                return true;
            }
            catch (System.Exception ex)
            {
                return false;
            }
        }

        public static bool UpdateLuuTruInfo(LuuTru dto)
        {
            //try
            //{
            //    VNSCDataContext VNSC = SQL_Connection.CreateSQlConnection();
            //    LuuTru sk = VNSC.LuuTrus.Single(P => P.Ma == dto.Ma);
            //    sk.MaNhomTrachVu = dto.MaNhomTrachVu;
            //    sk.Ten = dto.Ten;
            //    sk.MoTa = dto.MoTa;

            //    VNSC.SubmitChanges();
            //    return true;
            //}
            //catch (System.Exception ex)
            //{
            //    return false;
            //}

            return false; //Luu tru khong co update
        }
    }
}
