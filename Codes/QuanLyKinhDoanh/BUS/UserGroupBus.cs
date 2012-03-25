using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DTO;
using DAO;

namespace BUS
{
    public class UserGroupBus
    {
        public static int GetCount(string text)
        {
            return UserGroupDao.GetCount(text);
        }

        public static List<UserGroup> GetList(string text,
            string sortColumn, string sortOrder, int skip, int take)
        {
            return UserGroupDao.GetList(text, sortColumn, sortOrder, skip, take);
        }

        public static UserGroup GetById(int id)
        {
            return UserGroupDao.GetById(id);
        }

        public static bool Insert(UserGroup data)
        {
            return UserGroupDao.Insert(data);
        }

        public static bool Delete(UserGroup data)
        {
            return UserGroupDao.Delete(data);
        }

        public static bool DeleteList(string ids)
        {
            return UserGroupDao.DeleteList(ids);
        }

        public static bool Update(UserGroup data)
        {
            return UserGroupDao.Update(data);
        }
    }
}
