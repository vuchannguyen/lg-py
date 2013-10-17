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
        /// <summary>
        /// Count with HoaDonDetail variables
        /// </summary>
        public static int GetCount(int type, string text, string timeType, DateTime date)
        {
            return HoaDonDetailDao.GetCount(type, text, timeType, date);
        }

        /// <summary>
        /// GetList with HoaDonDetail variables
        /// </summary>
        public static List<HoaDonDetail> GetList(int type, string text, string timeType, DateTime date,
            string sortColumn, string sortOrder, int skip, int take)
        {
            return HoaDonDetailDao.GetList(type, text, timeType, date, sortColumn, sortOrder, skip, take);
        }

        /// <summary>
        /// Count with SanPham variables
        /// </summary>
        public static int GetCount(int type, string text, int idGroup)
        {
            return HoaDonDetailDao.GetCount(type, text, idGroup);
        }

        /// <summary>
        /// GetList with SanPham variables
        /// </summary>
        public static List<HoaDonDetail> GetList(int type, string text, int idGroup,
            string sortColumn, string sortOrder, int skip, int take)
        {
            return HoaDonDetailDao.GetList(type, text, idGroup,
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

        public static bool Insert(HoaDonDetail data, User user)
        {
            return HoaDonDetailDao.Insert(data, user);
        }

        public static bool Delete(HoaDonDetail data, User user)
        {
            return HoaDonDetailDao.Delete(data, user);
        }

        public static bool DeleteList(string ids, User user)
        {
            return HoaDonDetailDao.DeleteList(ids, user);
        }

        public static bool Update(HoaDonDetail data, User user)
        {
            return HoaDonDetailDao.Update(data, user);
        }
    }
}
