using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DTO;
using DAO;

namespace BUS
{
    public class NguonCungCapBus
    {
        public static int GetCount(string text)
        {
            return NguonCungCapDao.GetCount(text);
        }

        public static List<NguonCungCap> GetList(string text,
            string sortColumn, string sortOrder, int skip, int take)
        {
            return NguonCungCapDao.GetList(text, sortColumn, sortOrder, skip, take);
        }

        public static NguonCungCap GetById(int id)
        {
            return NguonCungCapDao.GetById(id);
        }

        public static bool Insert(NguonCungCap data)
        {
            return NguonCungCapDao.Insert(data);
        }

        public static bool Delete(NguonCungCap data)
        {
            return NguonCungCapDao.Delete(data);
        }

        public static bool DeleteList(string ids)
        {
            return NguonCungCapDao.DeleteList(ids);
        }

        public static bool Update(NguonCungCap data)
        {
            return NguonCungCapDao.Update(data);
        }
    }
}
