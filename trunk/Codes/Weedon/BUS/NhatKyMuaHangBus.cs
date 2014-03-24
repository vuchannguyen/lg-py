using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DTO;
using DAO;

namespace BUS
{
    public class NhatKyMuaHangBus
    {
        public static int GetCount(string text, string timeType, DateTime date)
        {
            return NhatKyMuaHangDao.GetCount(text, timeType, date);
        }

        public static List<NhatKyMuaHang> GetList(string text, string timeType, DateTime date,
            string sortColumn, string sortOrder, int skip, int take)
        {
            return NhatKyMuaHangDao.GetList(text, timeType, date, sortColumn, sortOrder, skip, take);
        }

        public static NhatKyMuaHang GetLastData()
        {
            return NhatKyMuaHangDao.GetLastData();
        }

        public static NhatKyMuaHang GetById(int id)
        {
            return NhatKyMuaHangDao.GetById(id);
        }

        public static NhatKyMuaHang GetByDate(DateTime date)
        {
            return NhatKyMuaHangDao.GetByDate(date);
        }

        public static bool Insert(NhatKyMuaHang data, User user)
        {
            return NhatKyMuaHangDao.Insert(data, user);
        }

        public static bool Delete(NhatKyMuaHang data, User user)
        {
            return NhatKyMuaHangDao.Delete(data, user);
        }

        public static bool DeleteList(string ids, User user)
        {
            return NhatKyMuaHangDao.DeleteList(ids, user);
        }

        public static bool Update(NhatKyMuaHang data, User user)
        {
            return NhatKyMuaHangDao.Update(data, user);
        }
    }
}
