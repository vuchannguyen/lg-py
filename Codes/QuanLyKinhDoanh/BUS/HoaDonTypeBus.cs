using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DTO;
using DAO;

namespace BUS
{
    public class HoaDonTypeBus
    {
        public static int GetCount(string text)
        {
            return HoaDonTypeDao.GetCount(text);
        }

        public static List<HoaDonType> GetList(string text,
            string sortColumn, string sortOrder, int skip, int take)
        {
            return HoaDonTypeDao.GetList(text, sortColumn, sortOrder, skip, take);
        }

        public static HoaDonType GetById(int id)
        {
            return HoaDonTypeDao.GetById(id);
        }

        public static bool Insert(HoaDonType data)
        {
            return HoaDonTypeDao.Insert(data);
        }

        public static bool Delete(HoaDonType data)
        {
            return HoaDonTypeDao.Delete(data);
        }

        public static bool DeleteList(string ids)
        {
            return HoaDonTypeDao.DeleteList(ids);
        }

        public static bool Update(HoaDonType data)
        {
            return HoaDonTypeDao.Update(data);
        }
    }
}
