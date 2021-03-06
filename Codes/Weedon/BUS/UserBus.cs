﻿using System;
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

        public static User GetByUserName(string text)
        {
            return UserDao.GetByUserName(text);
        }

        public static bool Insert(User data, User user)
        {
            return UserDao.Insert(data, user);
        }

        public static bool Delete(User data, User user)
        {
            return UserDao.Delete(data, user);
        }

        public static bool DeleteList(string ids, User user)
        {
            return UserDao.DeleteList(ids, user);
        }

        public static bool Update(User data, User user)
        {
            return UserDao.Update(data, user);
        }
    }
}
