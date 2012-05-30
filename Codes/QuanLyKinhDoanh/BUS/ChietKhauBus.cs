using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DTO;
using DAO;

namespace BUS
{
    public class ChietKhauBus
    {
        public static int GetCount(string text)
        {
            return ChietKhauDao.GetCount(text);
        }

        public static List<ChietKhau> GetList(string text,
            string sortColumn, string sortOrder, int skip, int take)
        {
            return ChietKhauDao.GetList(text, sortColumn, sortOrder, skip, take);
        }

        public static ChietKhau GetById(int id)
        {
            return ChietKhauDao.GetById(id);
        }

        public static ChietKhau GetByIdSP(int id)
        {
            return ChietKhauDao.GetByIdSP(id);
        }

        public static bool Insert(ChietKhau data, User user)
        {
            return ChietKhauDao.Insert(data, user);
        }

        public static bool Delete(ChietKhau data, User user)
        {
            return ChietKhauDao.Delete(data, user);
        }

        public static bool DeleteList(string ids, User user)
        {
            return ChietKhauDao.DeleteList(ids, user);
        }

        public static bool Update(ChietKhau data, User user)
        {
            return ChietKhauDao.Update(data, user);
        }
    }
}
