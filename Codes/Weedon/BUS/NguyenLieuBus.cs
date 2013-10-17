using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DTO;
using DAO;

namespace BUS
{
    public class NguyenLieuBus
    {
        public static int GetCount(string text, bool? isActive)
        {
            return NguyenLieuDao.GetCount(text, isActive);
        }

        public static List<NguyenLieu> GetList(string text, bool? isActive,
            string sortColumn, string sortOrder, int skip, int take)
        {
            return NguyenLieuDao.GetList(text, isActive, sortColumn, sortOrder, skip, take);
        }

        public static NguyenLieu GetLastData()
        {
            return NguyenLieuDao.GetLastData();
        }

        public static NguyenLieu GetLastDataByMa()
        {
            return NguyenLieuDao.GetLastDataByMa();
        }

        public static NguyenLieu GetById(int id)
        {
            return NguyenLieuDao.GetById(id);
        }

        public static bool Insert(NguyenLieu data, User user)
        {
            return NguyenLieuDao.Insert(data, user);
        }

        public static bool Delete(NguyenLieu data, User user)
        {
            return NguyenLieuDao.Delete(data, user);
        }

        public static bool DeleteList(string ids, User user)
        {
            return NguyenLieuDao.DeleteList(ids, user);
        }

        public static bool Update(NguyenLieu data, User user)
        {
            return NguyenLieuDao.Update(data, user);
        }
    }
}
