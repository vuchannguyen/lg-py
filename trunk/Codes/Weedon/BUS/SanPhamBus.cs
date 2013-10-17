using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DTO;
using DAO;

namespace BUS
{
    public class SanPhamBus
    {
        public static int GetCount(string text, int idGroup)
        {
            return SanPhamDao.GetCount(text, idGroup);
        }

        public static List<SanPham> GetList(string text, int idGroup,
            string sortColumn, string sortOrder, int skip, int take)
        {
            return SanPhamDao.GetList(text, idGroup, sortColumn, sortOrder, skip, take);
        }

        public static List<SanPham> GetListByIdGroup(int idGroup)
        {
            return SanPhamDao.GetListByIdGroup(idGroup);
        }

        public static SanPham GetLastData()
        {
            return SanPhamDao.GetLastData();
        }

        public static SanPham GetLastData(int idGroup)
        {
            return SanPhamDao.GetLastData(idGroup);
        }

        public static SanPham GetById(int id)
        {
            return SanPhamDao.GetById(id);
        }

        public static bool Insert(SanPham data, User user)
        {
            return SanPhamDao.Insert(data, user);
        }

        public static bool Delete(SanPham data, User user)
        {
            return SanPhamDao.Delete(data, user);
        }

        public static bool DeleteList(string ids, User user)
        {
            return SanPhamDao.DeleteList(ids, user);
        }

        public static bool Update(SanPham data, User user)
        {
            return SanPhamDao.Update(data, user);
        }
    }
}
