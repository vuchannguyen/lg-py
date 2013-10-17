using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DTO;
using DAO;

namespace BUS
{
    public class KhachHangGroupBus
    {
        public static int GetCount(string text)
        {
            return KhachHangGroupDao.GetCount(text);
        }

        public static List<KhachHangGroup> GetList(string text,
            string sortColumn, string sortOrder, int skip, int take)
        {
            return KhachHangGroupDao.GetList(text, sortColumn, sortOrder, skip, take);
        }

        public static KhachHangGroup GetById(int id)
        {
            return KhachHangGroupDao.GetById(id);
        }

        public static bool Insert(KhachHangGroup data, User user)
        {
            return KhachHangGroupDao.Insert(data, user);
        }

        public static bool Delete(KhachHangGroup data, User user)
        {
            return KhachHangGroupDao.Delete(data, user);
        }

        public static bool DeleteList(string ids, User user)
        {
            return KhachHangGroupDao.DeleteList(ids, user);
        }

        public static bool Update(KhachHangGroup data, User user)
        {
            return KhachHangGroupDao.Update(data, user);
        }
    }
}
