using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DTO;
using DAO;

namespace BUS
{
    public class NhapHangBus
    {
        public static int GetCount(string text, int idUser, int idKH, DateTime date)
        {
            return NhapHangDao.GetCount(text, idUser, idKH, date);
        }

        public static List<NhapHang> GetList(string text, int idUser, int idKH, DateTime date,
            string sortColumn, string sortOrder, int skip, int take)
        {
            return NhapHangDao.GetList(text, idUser, idKH, date, sortColumn, sortOrder, skip, take);
        }

        public static NhapHang GetLastData()
        {
            return NhapHangDao.GetLastData();
        }

        public static NhapHang GetById(int id)
        {
            return NhapHangDao.GetById(id);
        }

        public static NhapHang GetByDate(DateTime date)
        {
            return NhapHangDao.GetByDate(date);
        }

        public static bool Insert(NhapHang data)
        {
            return NhapHangDao.Insert(data);
        }

        public static bool Delete(NhapHang data)
        {
            return NhapHangDao.Delete(data);
        }

        public static bool DeleteList(string ids)
        {
            return NhapHangDao.DeleteList(ids);
        }

        public static bool Update(NhapHang data)
        {
            return NhapHangDao.Update(data);
        }
    }
}
