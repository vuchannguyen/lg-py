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
        public static int GetCount(string text, int type, string timeType, DateTime date)
        {
            return HoaDonDao.GetCount(text, type, timeType, date);
        }

        public static List<HoaDon> GetList(string text, int type, string timeType, DateTime date,
            string sortColumn, string sortOrder, int skip, int take)
        {
            return HoaDonDao.GetList(text, type, timeType, date, sortColumn, sortOrder, skip, take);
        }

        public static int GetCountThu(string text)
        {
            return HoaDonDao.GetCountThu(text);
        }

        public static List<HoaDon> GetListThu(string text,
            string sortColumn, string sortOrder, int skip, int take)
        {
            return HoaDonDao.GetListThu(text, sortColumn, sortOrder, skip, take);
        }

        public static int GetCountChi(string text)
        {
            return HoaDonDao.GetCountChi(text);
        }

        public static List<HoaDon> GetListChi(string text,
            string sortColumn, string sortOrder, int skip, int take)
        {
            return HoaDonDao.GetListChi(text, sortColumn, sortOrder, skip, take);
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

        public static bool Insert(HoaDon data)
        {
            return HoaDonDao.Insert(data);
        }

        public static bool Delete(HoaDon data)
        {
            return HoaDonDao.Delete(data);
        }

        public static bool DeleteList(string ids)
        {
            return HoaDonDao.DeleteList(ids);
        }

        public static bool Update(HoaDon data)
        {
            return HoaDonDao.Update(data);
        }
    }
}
