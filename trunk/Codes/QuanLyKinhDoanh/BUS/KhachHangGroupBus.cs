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

        public static bool Insert(KhachHangGroup data)
        {
            return KhachHangGroupDao.Insert(data);
        }

        public static bool Delete(KhachHangGroup data)
        {
            return KhachHangGroupDao.Delete(data);
        }

        public static bool DeleteList(string ids)
        {
            return KhachHangGroupDao.DeleteList(ids);
        }

        public static bool Update(KhachHangGroup data)
        {
            return KhachHangGroupDao.Update(data);
        }
    }
}
