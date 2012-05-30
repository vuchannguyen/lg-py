using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DTO;
using DAO;

namespace BUS
{
    public class SanPhamGroupBus
    {
        public static int GetCount(string text)
        {
            return SanPhamGroupDao.GetCount(text);
        }

        public static List<SanPhamGroup> GetList(string text,
            string sortColumn, string sortOrder, int skip, int take)
        {
            return SanPhamGroupDao.GetList(text, sortColumn, sortOrder, skip, take);
        }

        public static SanPhamGroup GetById(int id)
        {
            return SanPhamGroupDao.GetById(id);
        }

        public static bool Insert(SanPhamGroup data, User user)
        {
            return SanPhamGroupDao.Insert(data, user);
        }

        public static bool Delete(SanPhamGroup data, User user)
        {
            return SanPhamGroupDao.Delete(data, user);
        }

        public static bool DeleteList(string ids, User user)
        {
            return SanPhamGroupDao.DeleteList(ids, user);
        }

        public static bool Update(SanPhamGroup data, User user)
        {
            return SanPhamGroupDao.Update(data, user);
        }
    }
}
