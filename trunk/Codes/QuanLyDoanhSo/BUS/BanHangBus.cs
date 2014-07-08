using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DTO;
using DAO;

namespace BUS
{
    public class BanHangBus
    {
        public static int GetCount(string text)
        {
            return BanHangDao.GetCount(text);
        }

        public static List<BanHang> GetList(string text,
            string sortColumn, string sortOrder, int skip, int take)
        {
            return BanHangDao.GetList(text, sortColumn, sortOrder, skip, take);
        }

        public static BanHang GetById(int id)
        {
            return BanHangDao.GetById(id);
        }

        public static BanHang GetByIdUserAndDate(int idUser, DateTime date)
        {
            return BanHangDao.GetByIdUserAndDate(idUser, date);
        }

        public static bool Insert(BanHang data)
        {
            return BanHangDao.Insert(data);
        }

        public static bool Delete(BanHang data)
        {
            return BanHangDao.Delete(data);
        }

        public static bool DeleteList(string ids)
        {
            return BanHangDao.DeleteList(ids);
        }

        public static bool Update(BanHang data)
        {
            return BanHangDao.Update(data);
        }
    }
}
