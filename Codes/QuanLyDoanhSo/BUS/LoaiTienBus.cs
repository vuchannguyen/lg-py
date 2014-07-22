using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DTO;
using DAO;

namespace BUS
{
    public class LoaiTienBus
    {
        public static int GetCount(string text)
        {
            return LoaiTienDao.GetCount(text);
        }

        public static List<LoaiTien> GetList(string text,
            string sortColumn, string sortOrder, int skip, int take)
        {
            return LoaiTienDao.GetList(text, sortColumn, sortOrder, skip, take);
        }

        public static LoaiTien GetById(int id)
        {
            return LoaiTienDao.GetById(id);
        }

        public static bool Insert(LoaiTien data)
        {
            return LoaiTienDao.Insert(data);
        }

        public static bool Delete(LoaiTien data)
        {
            return LoaiTienDao.Delete(data);
        }

        public static bool DeleteList(string ids)
        {
            return LoaiTienDao.DeleteList(ids);
        }

        public static bool Update(LoaiTien data)
        {
            return LoaiTienDao.Update(data);
        }
    }
}
