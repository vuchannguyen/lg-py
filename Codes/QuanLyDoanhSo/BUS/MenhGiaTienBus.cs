using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DTO;
using DAO;

namespace BUS
{
    public class MenhGiaTienBus
    {
        public static int GetCount(string text)
        {
            return MenhGiaTienDao.GetCount(text);
        }

        public static List<MenhGiaTien> GetList(string text,
            string sortColumn, string sortOrder, int skip, int take)
        {
            return MenhGiaTienDao.GetList(text, sortColumn, sortOrder, skip, take);
        }

        public static MenhGiaTien GetById(int id)
        {
            return MenhGiaTienDao.GetById(id);
        }

        public static bool Insert(MenhGiaTien data)
        {
            return MenhGiaTienDao.Insert(data);
        }

        public static bool Delete(MenhGiaTien data)
        {
            return MenhGiaTienDao.Delete(data);
        }

        public static bool DeleteList(string ids)
        {
            return MenhGiaTienDao.DeleteList(ids);
        }

        public static bool Update(MenhGiaTien data)
        {
            return MenhGiaTienDao.Update(data);
        }
    }
}
