using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DTO;
using DAO;

namespace BUS
{
    public class NhatKyNguyenLieuBus
    {
        public static int GetCount(string text, bool? isActive, DateTime date)
        {
            return NhatKyNguyenLieuDao.GetCount(text, isActive, date);
        }

        public static List<NhatKyNguyenLieu> GetList(string text, bool? isActive, DateTime date,
            string sortColumn, string sortOrder, int skip, int take)
        {
            return NhatKyNguyenLieuDao.GetList(text, isActive, date, sortColumn, sortOrder, skip, take);
        }

        public static NhatKyNguyenLieu GetLastData()
        {
            return NhatKyNguyenLieuDao.GetLastData();
        }

        public static NhatKyNguyenLieu GetById(int id)
        {
            return NhatKyNguyenLieuDao.GetById(id);
        }

        public static bool Insert(NhatKyNguyenLieu data, User user)
        {
            return NhatKyNguyenLieuDao.Insert(data, user);
        }

        public static bool Delete(NhatKyNguyenLieu data, User user)
        {
            return NhatKyNguyenLieuDao.Delete(data, user);
        }

        public static bool DeleteList(string ids, User user)
        {
            return NhatKyNguyenLieuDao.DeleteList(ids, user);
        }

        public static bool Update(NhatKyNguyenLieu data, User user)
        {
            return NhatKyNguyenLieuDao.Update(data, user);
        }
    }
}
