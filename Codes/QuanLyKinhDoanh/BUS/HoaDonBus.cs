using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DTO;
using DAO;

namespace BUS
{
    public class HoaDonBus
    {
        public static int GetCount(string text)
        {
            return HoaDonDao.GetCount(text);
        }

        public static List<HoaDon> GetList(string text,
            string sortColumn, string sortOrder, int skip, int take)
        {
            return HoaDonDao.GetList(text, sortColumn, sortOrder, skip, take);
        }

        public static HoaDon GetById(int id)
        {
            return HoaDonDao.GetById(id);
        }

        public static bool Insert(HoaDon data)
        {
            return HoaDonDao.Insert(data);
        }

        public static bool Delete(HoaDon data)
        {
            return HoaDonDao.Delete(data);
        }

        public static bool DeleteList(string ids)
        {
            return HoaDonDao.DeleteList(ids);
        }

        public static bool Update(HoaDon data)
        {
            return HoaDonDao.Update(data);
        }
    }
}
