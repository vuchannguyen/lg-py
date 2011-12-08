using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAO
{
    public class NhomLoaiHinh_DAO
    {

        public static List<NhomLoaiHinh> LayDSNhomLoaiHinh()
        {
            try
            {
                VNSCDataContext VNSC = SQL_Connection.CreateSQlConnection();
                List<NhomLoaiHinh> list = new List<NhomLoaiHinh>();

                var q = from p in VNSC.NhomLoaiHinhs
                        select p;

                foreach (NhomLoaiHinh k in q)
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

        public static List<NhomLoaiHinh> TraCuuDSNhomLoaiHinhTheoTen(string sTen)
        {
            try
            {
                VNSCDataContext VNSC = SQL_Connection.CreateSQlConnection();
                List<NhomLoaiHinh> list = new List<NhomLoaiHinh>();

                var q = from p in VNSC.NhomLoaiHinhs
                        where p.Ten.Contains(sTen)
                        select p;
                foreach (NhomLoaiHinh k in q)
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

        public static NhomLoaiHinh TraCuuNhomLoaiHinhTheoMa(string sMa)
        {
            try
            {
                VNSCDataContext VNSC = SQL_Connection.CreateSQlConnection();
                NhomLoaiHinh sk = VNSC.NhomLoaiHinhs.Single(P => P.Ma == sMa);
                return sk;
            }
            catch (System.Exception ex)
            {
                return null;
            }
        }

        public static NhomLoaiHinh TraCuuNhomLoaiHinhTheoTen(string sTen)
        {
            try
            {
                VNSCDataContext VNSC = SQL_Connection.CreateSQlConnection();
                NhomLoaiHinh sk = VNSC.NhomLoaiHinhs.Single(P => P.Ten == sTen);
                return sk;
            }
            catch (System.Exception ex)
            {
                return null;
            }
        }

        public static bool Insert(NhomLoaiHinh dto)
        {
            try
            {
                VNSCDataContext VNSC = SQL_Connection.CreateSQlConnection();

                VNSC.NhomLoaiHinhs.InsertOnSubmit(dto);
                VNSC.SubmitChanges();
                return true;
            }
            catch (System.Exception ex)
            {
                return false;
            }
        }

        public static bool Delete(string sMa)
        {
            try
            {
                VNSCDataContext VNSC = SQL_Connection.CreateSQlConnection();
                NhomLoaiHinh dto = VNSC.NhomLoaiHinhs.Single(P => P.Ma == sMa);

                VNSC.NhomLoaiHinhs.DeleteOnSubmit(dto);
                VNSC.SubmitChanges();
                return true;
            }
            catch (System.Exception ex)
            {
                return false;
            }
        }

        public static bool UpdateNhomLoaiHinhInfo(NhomLoaiHinh dto)
        {
            try
            {
                VNSCDataContext VNSC = SQL_Connection.CreateSQlConnection();
                NhomLoaiHinh sk = VNSC.NhomLoaiHinhs.Single(P => P.Ma == dto.Ma);

                sk.Ten = dto.Ten;
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
