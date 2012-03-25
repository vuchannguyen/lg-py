using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DTO;
using DAO;

namespace BUS
{
    public class HoaDonDetailBus
    {
        public static int GetCount(string text)
        {
            return HoaDonDetailDao.GetCount(text);
        }

        public static List<HoaDonDetail> GetList(string text,
            string sortColumn, string sortOrder, int skip, int take)
        {
            return HoaDonDetailDao.GetList(text, sortColumn, sortOrder, skip, take);
        }

        public static HoaDonDetail GetById(int id)
        {
            return HoaDonDetailDao.GetById(id);
        }

        public static bool Insert(HoaDonDetail data)
        {
            return HoaDonDetailDao.Insert(data);
        }

        public static bool Delete(HoaDonDetail data)
        {
            return HoaDonDetailDao.Delete(data);
        }

        public static bool DeleteList(string ids)
        {
            return HoaDonDetailDao.DeleteList(ids);
        }

        public static bool Update(HoaDonDetail data)
        {
            return HoaDonDetailDao.Update(data);
        }
    }
}
