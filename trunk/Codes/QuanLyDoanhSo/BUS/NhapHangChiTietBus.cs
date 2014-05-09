using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DTO;
using DAO;

namespace BUS
{
    public class NhapHangChiTietBus
    {
        /// <summary>
        /// Count with NhapHangChiTiet variables
        /// </summary>
        public static int GetCount(string text)
        {
            return NhapHangChiTietDao.GetCount(text);
        }

        /// <summary>
        /// GetList with NhapHangChiTiet variables
        /// </summary>
        public static List<NhapHangChiTiet> GetList(string text,
            string sortColumn, string sortOrder, int skip, int take)
        {
            return NhapHangChiTietDao.GetList(text, sortColumn, sortOrder, skip, take);
        }

        public static List<NhapHangChiTiet> GetListByIdNhapHang(int idNhapHang)
        {
            return NhapHangChiTietDao.GetListByIdNhapHang(idNhapHang);
        }

        public static NhapHangChiTiet GetLastData()
        {
            return NhapHangChiTietDao.GetLastData();
        }

        public static NhapHangChiTiet GetById(int id)
        {
            return NhapHangChiTietDao.GetById(id);
        }

        public static bool Insert(NhapHangChiTiet data)
        {
            return NhapHangChiTietDao.Insert(data);
        }

        public static bool Delete(NhapHangChiTiet data)
        {
            return NhapHangChiTietDao.Delete(data);
        }

        public static bool DeleteList(string ids)
        {
            return NhapHangChiTietDao.DeleteList(ids);
        }

        public static bool Update(NhapHangChiTiet data)
        {
            return NhapHangChiTietDao.Update(data);
        }
    }
}
