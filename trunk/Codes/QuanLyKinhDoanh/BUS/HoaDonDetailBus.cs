using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DTO;
using DAO;

namespace BUS
{
    public class HoaDonDetailBus
    {
        public static int GetCount(string text, int type, string timeType, DateTime date)
        {
            return HoaDonDetailDao.GetCount(text, type, timeType, date);
        }

        public static List<HoaDonDetail> GetList(string text, int type, string timeType, DateTime date,
            string sortColumn, string sortOrder, int skip, int take)
        {
            return HoaDonDetailDao.GetList(text, type, timeType, date, sortColumn, sortOrder, skip, take);
        }

        public static int GetCount(int type, string text, int idGroup, bool isHavePrice, string status,
            bool isExpired, int warningDays)
        {
            return HoaDonDetailDao.GetCount(type, text, idGroup, isHavePrice, status, isExpired, warningDays);
        }

        public static List<HoaDonDetail> GetList(int type, string text, int idGroup, bool isHavePrice, string status,
            bool isExpired, int warningDays,
            string sortColumn, string sortOrder, int skip, int take)
        {
            return HoaDonDetailDao.GetList(type, text, idGroup, isHavePrice, status, isExpired, warningDays,
                sortColumn, sortOrder, skip, take);
        }

        public static List<HoaDonDetail> GetListByIdHoaDon(int idHoaDon)
        {
            return HoaDonDetailDao.GetListByIdHoaDon(idHoaDon);
        }

        public static HoaDonDetail GetLastData()
        {
            return HoaDonDetailDao.GetLastData();
        }

        public static HoaDonDetail GetById(int id)
        {
            return HoaDonDetailDao.GetById(id);
        }

        public static bool CheckIfSold(int idSP)
        {
            return HoaDonDetailDao.CheckIfSold(idSP);
        }

        public static bool Insert(HoaDonDetail data)
        {
            return HoaDonDetailDao.Insert(data);
        }

        public static bool Delete(HoaDonDetail data)
        {
            return HoaDonDetailDao.Delete(data);
        }

        public static bool DeleteList(string ids)
        {
            return HoaDonDetailDao.DeleteList(ids);
        }

        public static bool Update(HoaDonDetail data)
        {
            return HoaDonDetailDao.Update(data);
        }
    }
}
