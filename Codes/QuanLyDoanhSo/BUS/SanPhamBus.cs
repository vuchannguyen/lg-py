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
        public static int GetCount(string text)
        {
            return SanPhamDao.GetCount(text);
        }

        public static List<SanPham> GetList(string text,
            string sortColumn, string sortOrder, int skip, int take)
        {
            return SanPhamDao.GetList(text, sortColumn, sortOrder, skip, take);
        }

        public static SanPham GetLastData()
        {
            return SanPhamDao.GetLastData();
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
