using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DTO;
using DAO;

namespace BUS
{
    public class SettingBus
    {
        public static int GetCount(string text)
        {
            return SettingDao.GetCount(text);
        }

        public static List<Setting> GetList(string text,
            string sortColumn, string sortOrder, int skip, int take)
        {
            return SettingDao.GetList(text, sortColumn, sortOrder, skip, take);
        }

        public static Setting GetById(int id)
        {
            return SettingDao.GetById(id);
        }

        public static bool Insert(Setting data, User user)
        {
            return SettingDao.Insert(data, user);
        }

        public static bool Delete(Setting data, User user)
        {
            return SettingDao.Delete(data, user);
        }

        public static bool DeleteList(string ids, User user)
        {
            return SettingDao.DeleteList(ids, user);
        }

        public static bool Update(Setting data, User user)
        {
            return SettingDao.Update(data, user);
        }
    }
}
