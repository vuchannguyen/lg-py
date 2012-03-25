using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DTO;
using DAO;

namespace BUS
{
    public class HoaDonStatusBus
    {
        public static int GetCount(string text)
        {
            return HoaDonStatusDao.GetCount(text);
        }

        public static List<HoaDonStatus> GetList(string text,
            string sortColumn, string sortOrder, int skip, int take)
        {
            return HoaDonStatusDao.GetList(text, sortColumn, sortOrder, skip, take);
        }

        public static HoaDonStatus GetById(int id)
        {
            return HoaDonStatusDao.GetById(id);
        }

        public static bool Insert(HoaDonStatus data)
        {
            return HoaDonStatusDao.Insert(data);
        }

        public static bool Delete(HoaDonStatus data)
        {
            return HoaDonStatusDao.Delete(data);
        }

        public static bool DeleteList(string ids)
        {
            return HoaDonStatusDao.DeleteList(ids);
        }

        public static bool Update(HoaDonStatus data)
        {
            return HoaDonStatusDao.Update(data);
        }
    }
}
