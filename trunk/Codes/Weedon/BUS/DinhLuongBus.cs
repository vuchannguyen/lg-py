using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DTO;
using DAO;

namespace BUS
{
    public class DinhLuongBus
    {
        public static int GetCount(string text)
        {
            return DinhLuongDao.GetCount(text);
        }

        public static List<DinhLuong> GetList(string text,
            string sortColumn, string sortOrder, int skip, int take)
        {
            return DinhLuongDao.GetList(text, sortColumn, sortOrder, skip, take);
        }

        public static List<DinhLuong> GetListByIdSP(int id)
        {
            return DinhLuongDao.GetListByIdSP(id);
        }

        public static List<DinhLuong> GetListByIdNL(int id)
        {
            return DinhLuongDao.GetListByIdNL(id);
        }

        public static DinhLuong GetById(int id)
        {
            return DinhLuongDao.GetById(id);
        }

        public static int CountByIdSP(int id)
        {
            return DinhLuongDao.CountByIdSP(id);
        }

        public static bool Insert(DinhLuong data, User user)
        {
            return DinhLuongDao.Insert(data, user);
        }

        public static bool Delete(DinhLuong data, User user)
        {
            return DinhLuongDao.Delete(data, user);
        }

        public static bool DeleteList(string ids, User user)
        {
            return DinhLuongDao.DeleteList(ids, user);
        }

        public static bool Update(DinhLuong data, User user)
        {
            return DinhLuongDao.Update(data, user);
        }
    }
}
