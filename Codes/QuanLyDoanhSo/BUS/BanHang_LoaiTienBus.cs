using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DTO;
using DAO;

namespace BUS
{
    public class BanHang_LoaiTienBus
    {
        public static int GetCount(string text)
        {
            return BanHang_LoaiTienDao.GetCount(text);
        }

        public static List<BanHang_LoaiTien> GetList(string text,
            string sortColumn, string sortOrder, int skip, int take)
        {
            return BanHang_LoaiTienDao.GetList(text, sortColumn, sortOrder, skip, take);
        }

        public static List<BanHang_LoaiTien> GetListByIdBanHang(int idBanHang)
        {
            return BanHang_LoaiTienDao.GetListByIdBanHang(idBanHang);
        }

        public static BanHang_LoaiTien GetById(int id)
        {
            return BanHang_LoaiTienDao.GetById(id);
        }

        public static bool Insert(BanHang_LoaiTien data)
        {
            return BanHang_LoaiTienDao.Insert(data);
        }

        public static bool Delete(BanHang_LoaiTien data)
        {
            return BanHang_LoaiTienDao.Delete(data);
        }

        public static bool DeleteList(string ids)
        {
            return BanHang_LoaiTienDao.DeleteList(ids);
        }

        public static bool Update(BanHang_LoaiTien data)
        {
            return BanHang_LoaiTienDao.Update(data);
        }
    }
}
