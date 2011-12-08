using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAO
{
    public class DieuPhoi_DAO
    {
        public static List<DieuPhoi> LayDSDieuPhoi()
        {
            try
            {
                VNSCDataContext VNSC = SQL_Connection.CreateSQlConnection();
                List<DieuPhoi> list = new List<DieuPhoi>();

                var q = from p in VNSC.DieuPhois
                        select p;

                foreach (DieuPhoi k in q)
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

        public static List<DieuPhoi> TraCuuDSDieuPhoiTheoMaSuKien(int iMa)
        {
            try
            {
                VNSCDataContext VNSC = SQL_Connection.CreateSQlConnection();
                List<DieuPhoi> list = new List<DieuPhoi>();

                var q = from p in VNSC.SuKien_DieuPhois
                        where p.MaSuKien == iMa
                        select p;

                foreach (SuKien_DieuPhoi k in q)
                {
                    list.Add(VNSC.DieuPhois.Single(P => P.Ma == k.MaDieuPhoi));
                }

                return list;
            }
            catch (System.Exception ex)
            {
                return null;
            }
        }

        public static List<DieuPhoi> TraCuuDSDieuPhoiTheoMaSuKien_HoSo(int iMaSuKien, string sMaSuKien_HoSo)
        {
            try
            {
                VNSCDataContext VNSC = SQL_Connection.CreateSQlConnection();
                List<DieuPhoi> list = new List<DieuPhoi>();

                var q = from p in VNSC.SuKien_DieuPhois
                        where p.MaSuKien == iMaSuKien
                        select p;

                DieuPhoi dto_Temp = new DieuPhoi();
                foreach (SuKien_DieuPhoi k in q)
                {
                    dto_Temp = VNSC.DieuPhois.Single(P => P.Ma == k.MaDieuPhoi);
                    if (dto_Temp.MaSuKien_HoSo == sMaSuKien_HoSo)
                    {
                        list.Add(dto_Temp);
                    }
                }

                return list;
            }
            catch (System.Exception ex)
            {
                return null;
            }
        }

        public static List<DieuPhoi> TraCuuDSDieuPhoiTheoMaSuKien_DonViHanhChanh_TrachVuSuKien(int iMa, int iMaDonViHanhChanh, int iMaTrachVuSuKien)
        {
            try
            {
                VNSCDataContext VNSC = SQL_Connection.CreateSQlConnection();
                List<DieuPhoi> list = new List<DieuPhoi>();
                List<DieuPhoi> list_Temp = new List<DieuPhoi>();

                var q = from p in VNSC.SuKien_DieuPhois
                        where p.MaSuKien == iMa
                        select p;

                foreach (SuKien_DieuPhoi k in q)
                {
                    list_Temp.Add(VNSC.DieuPhois.Single(P => P.Ma == k.MaDieuPhoi));
                }

                foreach (DieuPhoi k in list_Temp)
                {
                    if (k.MaDonViHanhChanh == iMaDonViHanhChanh && k.MaTrachVuSuKien == iMaTrachVuSuKien)
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

        public static List<DieuPhoi> TraCuuDSDieuPhoiTheoTen(string sTen)
        {
            try
            {
                VNSCDataContext VNSC = SQL_Connection.CreateSQlConnection();
                List<DieuPhoi> list = new List<DieuPhoi>();

                var q = from p in VNSC.DieuPhois
                        where SuKien_HoSo_DAO.TraCuuSuKien_HoSoTheoMa(p.MaSuKien_HoSo).HoTen.Contains(sTen)
                        select p;

                foreach (DieuPhoi k in q)
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

        public static DieuPhoi TraCuuDieuPhoiTheoMa(int iMa)
        {
            try
            {
                VNSCDataContext VNSC = SQL_Connection.CreateSQlConnection();
                DieuPhoi sk = VNSC.DieuPhois.Single(P => P.Ma == iMa);
                return sk;
            }
            catch (System.Exception ex)
            {
                return null;
            }
        }

        //public static DieuPhoi TraCuuDieuPhoiTheoTen(string sTen)
        //{
        //    try
        //    {
        //        VNSCDataContext VNSC = SQL_Connection.CreateSQlConnection();
        //        DieuPhoi sk = VNSC.DieuPhois.Single(P => P.Ten == sTen);
        //        return sk;
        //    }
        //    catch (System.Exception ex)
        //    {
        //        return null;
        //    }
        //}

        public static bool Insert(DieuPhoi dto)
        {
            try
            {
                VNSCDataContext VNSC = SQL_Connection.CreateSQlConnection();

                VNSC.DieuPhois.InsertOnSubmit(dto);
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
                DieuPhoi dto = VNSC.DieuPhois.Single(P => P.Ma == iMa);

                VNSC.DieuPhois.DeleteOnSubmit(dto);
                VNSC.SubmitChanges();
                return true;
            }
            catch (System.Exception ex)
            {
                return false;
            }
        }

        public static bool UpdateDieuPhoiInfo(DieuPhoi dto)
        {
            //try
            //{
            //    VNSCDataContext VNSC = SQL_Connection.CreateSQlConnection();
            //    DieuPhoi sk = VNSC.DieuPhois.Single(P => P.Ma == dto.Ma);

            //    sk.Ten = dto.Ten;
            //    sk.MaDonViHanhChanh = dto.MaDonViHanhChanh;
            //    sk.MoTa = dto.MoTa;

            //    VNSC.SubmitChanges();
            //    return true;
            //}
            //catch (System.Exception ex)
            //{
            //    return false;
            //}

            return true; //Khong co update
        }
    }
}
