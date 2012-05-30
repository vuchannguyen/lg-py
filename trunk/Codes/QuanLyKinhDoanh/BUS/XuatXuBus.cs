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

        public static bool Insert(XuatXu data, User user)
        {
            return XuatXuDao.Insert(data, user);
        }

        public static bool Delete(XuatXu data, User user)
        {
            return XuatXuDao.Delete(data, user);
        }

        public static bool DeleteList(string ids, User user)
        {
            return XuatXuDao.DeleteList(ids, user);
        }

        public static bool Update(XuatXu data, User user)
        {
            return XuatXuDao.Update(data, user);
        }
    }
}
