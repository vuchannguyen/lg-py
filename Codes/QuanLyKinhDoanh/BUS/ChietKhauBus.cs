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

        public static bool Insert(ChietKhau data)
        {
            return ChietKhauDao.Insert(data);
        }

        public static bool Delete(ChietKhau data)
        {
            return ChietKhauDao.Delete(data);
        }

        public static bool DeleteList(string ids)
        {
            return ChietKhauDao.DeleteList(ids);
        }

        public static bool Update(ChietKhau data)
        {
            return ChietKhauDao.Update(data);
        }
    }
}
