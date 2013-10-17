using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DTO;
using DAO;

namespace BUS
{
    public class GiaChinhThucBus
    {
        public static int GetCount(string text)
        {
            return GiaChinhThucDao.GetCount(text);
        }

        public static List<GiaChinhThuc> GetList(string text,
            string sortColumn, string sortOrder, int skip, int take)
        {
            return GiaChinhThucDao.GetList(text, sortColumn, sortOrder, skip, take);
        }

        public static GiaChinhThuc GetById(int id)
        {
            return GiaChinhThucDao.GetById(id);
        }

        public static GiaChinhThuc GetByIdSanPham(int id)
        {
            return GiaChinhThucDao.GetByIdSanPham(id);
        }

        public static bool Insert(GiaChinhThuc data, User user)
        {
            return GiaChinhThucDao.Insert(data, user);
        }

        public static bool Delete(GiaChinhThuc data, User user)
        {
            return GiaChinhThucDao.Delete(data, user);
        }

        public static bool DeleteList(string ids, User user)
        {
            return GiaChinhThucDao.DeleteList(ids, user);
        }

        public static bool Update(GiaChinhThuc data, User user)
        {
            return GiaChinhThucDao.Update(data, user);
        }
    }
}
