using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BUS;
using DAO;

namespace Function
{
    class Search_Function
    {
        public static List<HoSo> SearchWithOneCriteria(string sCriteria, List<HoSo> InputList)
        {
            /*Tam thoi tim theo cac kieu sau:
             * 1. Ho Ten
             * 2. Doi Tuong
             * 3. Trach Vu
             * 4. Nganh
             * 5. Don Vi
             */
            List<HoSo> List_HoTen = new List<HoSo>();
            List<HoSo> List_NhomTrachVu =  new List<HoSo>();
            List<HoSo> List_TrachVu =  new List<HoSo>();
            List<HoSo> List_DonVi = new List<HoSo>();

            if (InputList==null)
            {
                List_HoTen = BUS.HoSo_BUS.TraCuuDSHoSoTheoTen(sCriteria);
                List_NhomTrachVu = BUS.HoSo_BUS.TraCuuDSHoSoTheoTenNhomTrachVu(sCriteria);
                List_TrachVu = BUS.HoSo_BUS.TraCuuDSHoSoTheoTenTrachVu(sCriteria);
                List_DonVi = BUS.HoSo_BUS.TraCuuDSHoSoTheoTenDonVi(sCriteria);
            }
            else
            {
                List_HoTen = BUS.HoSo_BUS.TraCuuDSHoSoTheoTen(sCriteria,InputList);
                List_NhomTrachVu = BUS.HoSo_BUS.TraCuuDSHoSoTheoTenNhomTrachVu(sCriteria, InputList);
                List_TrachVu = BUS.HoSo_BUS.TraCuuDSHoSoTheoTenTrachVu(sCriteria, InputList);
                List_DonVi = BUS.HoSo_BUS.TraCuuDSHoSoTheoTenDonVi(sCriteria, InputList);
            }

            List<HoSo> pointerList = null;
            int iSubListCount = 4; //tim  theo 4 criteria

            List<HoSo> List_Final = new List<HoSo>();
            for (int i = 1; i <= iSubListCount; i++ )
            {
                switch (i)
                {
                    case 1:
                        {
                            pointerList = List_HoTen;
                            break;
                        }
                    case 2:
                        {
                            pointerList = List_NhomTrachVu;
                            break;
                        }
                    case 3:
                        {
                            pointerList = List_TrachVu;
                            break;
                        }
                    case 4:
                        {
                            pointerList = List_DonVi;
                            break;
                        }
                    default:
                        {
                            pointerList = List_HoTen;
                            break;
                        }
                }

                for (int j = 0; j < pointerList.Count;j++ )
                {
                    if (List_Final.Count > 0)
                    {
                        bool isItemExist = false;
                        for (int k = 0; k < List_Final.Count;k++ )
                        {
                            if (List_Final[k].Ma == pointerList[j].Ma)
                            {
                                isItemExist = true;
                            }
                        }
                        if (!isItemExist)
                        {
                            List_Final.Add(pointerList[j]);
                        }
                    }
                    else
                    {
                        List_Final.Add(pointerList[j]);
                    }
                }
            }

            return List_Final;
        }
    }
}
