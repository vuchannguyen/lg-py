using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DTO;
using DAO;

namespace BUS
{
    public class HoaDonBus
    {
        public static int GetCount(string text, int type, int status, int idKH, string timeType, DateTime date)
        {
            return HoaDonDao.GetCount(text, type, status, idKH, timeType, date);
        }

        public static List<HoaDon> GetList(string text, int type, int status, int idKH, string timeType, DateTime date,
            string sortColumn, string sortOrder, int skip, int take)
        {
            return HoaDonDao.GetList(text, type, status, idKH, timeType, date, sortColumn, sortOrder, skip, take);
        }

        public static HoaDon GetLastData()
        {
            return HoaDonDao.GetLastData();
        }

        public static HoaDon GetLastData(int idType)
        {
            return HoaDonDao.GetLastData(idType);
        }

        public static HoaDon GetById(int id)
        {
            return HoaDonDao.GetById(id);
        }

        public static bool Insert(HoaDon data, User user)
        {
            return HoaDonDao.Insert(data, user);
        }

        public static bool Delete(HoaDon data, User user)
        {
            return HoaDonDao.Delete(data, user);
        }

        public static bool DeleteList(string ids, User user)
        {
            return HoaDonDao.DeleteList(ids, user);
        }

        public static bool Update(HoaDon data, User user)
        {
            return HoaDonDao.Update(data, user);
        }
    }
}
