using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DTO;
using DAO;

namespace BUS
{
    public class BanHang_MenhGiaTienBus
    {
        public static int GetCount(string text)
        {
            return BanHang_MenhGiaTienDao.GetCount(text);
        }

        public static List<BanHang_MenhGiaTien> GetList(string text,
            string sortColumn, string sortOrder, int skip, int take)
        {
            return BanHang_MenhGiaTienDao.GetList(text, sortColumn, sortOrder, skip, take);
        }

        public static BanHang_MenhGiaTien GetById(int id)
        {
            return BanHang_MenhGiaTienDao.GetById(id);
        }

        public static bool Insert(BanHang_MenhGiaTien data)
        {
            return BanHang_MenhGiaTienDao.Insert(data);
        }

        public static bool Delete(BanHang_MenhGiaTien data)
        {
            return BanHang_MenhGiaTienDao.Delete(data);
        }

        public static bool DeleteList(string ids)
        {
            return BanHang_MenhGiaTienDao.DeleteList(ids);
        }

        public static bool Update(BanHang_MenhGiaTien data)
        {
            return BanHang_MenhGiaTienDao.Update(data);
        }
    }
}
