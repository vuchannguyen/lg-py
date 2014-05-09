using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DTO;
using DAO;

namespace BUS
{
    public class KhuyenMaiBus
    {
        public static int GetCount(string text)
        {
            return KhuyenMaiDao.GetCount(text);
        }

        public static List<KhuyenMai> GetList(string text,
            string sortColumn, string sortOrder, int skip, int take)
        {
            return KhuyenMaiDao.GetList(text, sortColumn, sortOrder, skip, take);
        }

        public static KhuyenMai GetById(int id)
        {
            return KhuyenMaiDao.GetById(id);
        }

        public static bool Insert(KhuyenMai data)
        {
            return KhuyenMaiDao.Insert(data);
        }

        public static bool Delete(KhuyenMai data)
        {
            return KhuyenMaiDao.Delete(data);
        }

        public static bool DeleteList(string ids)
        {
            return KhuyenMaiDao.DeleteList(ids);
        }

        public static bool Update(KhuyenMai data)
        {
            return KhuyenMaiDao.Update(data);
        }
    }
}
