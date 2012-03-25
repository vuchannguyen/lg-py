using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DTO;
using DAO;

namespace BUS
{
    public class UserBus
    {
        public static int GetCount(string text)
        {
            return UserDao.GetCount(text);
        }

        public static List<User> GetList(string text,
            string sortColumn, string sortOrder, int skip, int take)
        {
            return UserDao.GetList(text, sortColumn, sortOrder, skip, take);
        }

        public static User GetById(int id)
        {
            return UserDao.GetById(id);
        }

        public static bool Insert(User data)
        {
            return UserDao.Insert(data);
        }

        public static bool Delete(User data)
        {
            return UserDao.Delete(data);
        }

        public static bool DeleteList(string ids)
        {
            return UserDao.DeleteList(ids);
        }

        public static bool Update(User data)
        {
            return UserDao.Update(data);
        }
    }
}
