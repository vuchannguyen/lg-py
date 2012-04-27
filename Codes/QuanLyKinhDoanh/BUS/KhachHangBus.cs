using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DTO;
using DAO;

namespace BUS
{
    public class KhachHangBus
    {
        public static int GetCount(string text)
        {
            return KhachHangDao.GetCount(text);
        }

        public static List<KhachHang> GetList(string text,
            string sortColumn, string sortOrder, int skip, int take)
        {
            return KhachHangDao.GetList(text, sortColumn, sortOrder, skip, take);
        }

        public static KhachHang GetById(int id)
        {
            return KhachHangDao.GetById(id);
        }

        public static KhachHang GetLastData()
        {
            return KhachHangDao.GetLastData();
        }

        public static KhachHang GetLastData(int idGroup)
        {
            return KhachHangDao.GetLastData(idGroup);
        }

        public static bool Insert(KhachHang data)
        {
            return KhachHangDao.Insert(data);
        }

        public static bool Delete(KhachHang data)
        {
            return KhachHangDao.Delete(data);
        }

        public static bool DeleteList(string ids)
        {
            return KhachHangDao.DeleteList(ids);
        }

        public static bool Update(KhachHang data)
        {
            return KhachHangDao.Update(data);
        }
    }
}
