using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DTO;
using DAO;

namespace BUS
{
    public class BanHangChiTietBus
    {
        public static int GetCount(string text)
        {
            return BanHangChiTietDao.GetCount(text);
        }

        public static List<BanHangChiTiet> GetList(string text,
            string sortColumn, string sortOrder, int skip, int take)
        {
            return BanHangChiTietDao.GetList(text, sortColumn, sortOrder, skip, take);
        }

        public static List<BanHangChiTiet> GetListByIdBanHang(int idBanHang)
        {
            return BanHangChiTietDao.GetListByIdBanHang(idBanHang);
        }

        public static BanHangChiTiet GetById(int id)
        {
            return BanHangChiTietDao.GetById(id);
        }

        public static BanHangChiTiet GetLastData()
        {
            return BanHangChiTietDao.GetLastData();
        }

        public static bool Insert(BanHangChiTiet data)
        {
            return BanHangChiTietDao.Insert(data);
        }

        public static bool Delete(BanHangChiTiet data)
        {
            return BanHangChiTietDao.Delete(data);
        }

        public static bool DeleteList(string ids)
        {
            return BanHangChiTietDao.DeleteList(ids);
        }

        public static bool Update(BanHangChiTiet data)
        {
            return BanHangChiTietDao.Update(data);
        }
    }
}
