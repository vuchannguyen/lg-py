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
        public static int GetCount(string text, int idGroup, bool isHavePrice, string status)
        {
            return SanPhamDao.GetCount(text, idGroup, isHavePrice, status);
        }

        public static List<SanPham> GetList(string text, int idGroup, bool isHavePrice, string status,
            string sortColumn, string sortOrder, int skip, int take)
        {
            return SanPhamDao.GetList(text, idGroup, isHavePrice, status, sortColumn, sortOrder, skip, take);
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

        public static bool Insert(SanPham data)
        {
            return SanPhamDao.Insert(data);
        }

        public static bool Delete(SanPham data)
        {
            return SanPhamDao.Delete(data);
        }

        public static bool DeleteList(string ids)
        {
            return SanPhamDao.DeleteList(ids);
        }

        public static bool Update(SanPham data)
        {
            return SanPhamDao.Update(data);
        }
    }
}
