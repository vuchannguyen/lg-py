using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DTO;
using DAO;

namespace BUS
{
    public class XuatXuBus
    {
        public static int GetCount(string text)
        {
            return XuatXuDao.GetCount(text);
        }

        public static List<XuatXu> GetList(string text,
            string sortColumn, string sortOrder, int skip, int take)
        {
            return XuatXuDao.GetList(text, sortColumn, sortOrder, skip, take);
        }

        public static XuatXu GetById(int id)
        {
            return XuatXuDao.GetById(id);
        }

        public static bool Insert(XuatXu data)
        {
            return XuatXuDao.Insert(data);
        }

        public static bool Delete(XuatXu data)
        {
            return XuatXuDao.Delete(data);
        }

        public static bool DeleteList(string ids)
        {
            return XuatXuDao.DeleteList(ids);
        }

        public static bool Update(XuatXu data)
        {
            return XuatXuDao.Update(data);
        }
    }
}
