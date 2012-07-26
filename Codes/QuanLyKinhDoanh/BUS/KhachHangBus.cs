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
        public static int GetCount(string text, bool isBirthDay)
        {
            return KhachHangDao.GetCount(text, isBirthDay);
        }

        public static List<KhachHang> GetList(string text, bool isBirthDay,
            string sortColumn, string sortOrder, int skip, int take)
        {
            return KhachHangDao.GetList(text, isBirthDay, sortColumn, sortOrder, skip, take);
        }

        public static List<KhachHang> GetListByIdGroup(int idGroup)
        {
            return KhachHangDao.GetListByIdGroup(idGroup);
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

        public static bool Insert(KhachHang data, User user)
        {
            return KhachHangDao.Insert(data, user);
        }

        public static bool Delete(KhachHang data, User user)
        {
            return KhachHangDao.Delete(data, user);
        }

        public static bool DeleteList(string ids, User user)
        {
            return KhachHangDao.DeleteList(ids, user);
        }

        public static bool Update(KhachHang data, User user)
        {
            return KhachHangDao.Update(data, user);
        }
    }
}
