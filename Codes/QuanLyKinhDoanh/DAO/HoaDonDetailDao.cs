using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DTO;
using Library;
using System.Linq.Dynamic;
using System.Data.Linq.SqlClient;
using System.Data.Common;

namespace DAO
{
    public class HoaDonDetailDao : SQLConnection
    {
        public static IQueryable<HoaDonDetail> GetQuery(string text, int type)
        {
            var sql = from data in dbContext.HoaDonDetails
                      select data;

            if (!string.IsNullOrEmpty(text))
            {
                text = CommonDao.GetFilterText(text);
                sql = sql.Where(p => SqlMethods.Like(p.SanPham.Ten, text)
                    );
            }

            if (type != 0)
            {
                sql = sql.Where(p => p.HoaDon.IdType == type);
            }

            sql = sql.Where(p => p.HoaDon.DeleteFlag == false);

            return sql;
        }

        public static int GetCount(string text, int type)
        {
            return GetQuery(text, type).Count();
        }

        public static List<HoaDonDetail> GetList(string text, int type,
            string sortColumn, string sortOrder, int skip, int take)
        {
            string sortSQL = string.Empty;

            switch (sortColumn)
            {

                default:
                    sortSQL += "Id " + CommonDao.SORT_ASCENDING;
                    break;
            }

            var sql = GetQuery(text, type).OrderBy(sortSQL);

            if ((skip <= 0 && take <= 0) || (skip < 0 && take > 0) || (skip > 0 && take < 0))
            {
                return sql.ToList();
            }

            return sql.Skip(skip).Take(take).ToList();
        }

        public static HoaDonDetail GetLastData()
        {
            var sql = dbContext.HoaDonDetails.OrderBy("Id " + CommonDao.SORT_DESCENDING);

            return sql.Skip(0).Take(1).ToList().FirstOrDefault();
        }

        public static HoaDonDetail GetById(int id)
        {
            return dbContext.HoaDonDetails.Where(p => p.Id == id).SingleOrDefault<HoaDonDetail>();
        }

        public static bool Insert(HoaDonDetail data)
        {
            try
            {
                dbContext.HoaDonDetails.InsertOnSubmit(data);
                dbContext.SubmitChanges();

                return true;
            }
            catch
            {
                return false;
            }
        }

        public static bool Delete(HoaDonDetail data)
        {
            if (data != null)
            {
                HoaDonDetail objDb = GetById(data.Id);

                if (objDb != null)
                {
                    dbContext.HoaDonDetails.DeleteOnSubmit(objDb);

                    return true;
                }
            }

            return false;
        }

        public static bool DeleteList(string ids)
        {
            DbTransaction trans = null;
            try
            {
                if (dbContext.Connection.State != System.Data.ConnectionState.Open)
                {
                    dbContext.Connection.Open();
                }

                trans = dbContext.Connection.BeginTransaction();
                dbContext.Transaction = trans;

                if (!string.IsNullOrEmpty(ids))
                {
                    string[] idArr = ids.Split(new string[] { CommonDao.SEPERATE_STRING }, StringSplitOptions.RemoveEmptyEntries);
                    int result = 0;

                    foreach (string id in idArr)
                    {
                        if (int.TryParse(id, out result))
                        {
                            HoaDonDetail data = GetById(result);

                            if (!Delete(data))
                            {
                                return false;
                            }
                        }
                        else
                        {
                            return false;
                        }
                    }

                    trans.Commit();
                    dbContext.Connection.Close();

                    return true;
                }

                return false;
            }
            catch
            {
                if (trans != null) trans.Rollback();
                return false;
            }
        }

        public static bool Update(HoaDonDetail data)
        {
            try
            {
                if (data != null)
                {
                    HoaDonDetail objDb = GetById(data.Id);

                    objDb.IdHoaDon = data.IdHoaDon;
                    objDb.IdSanPham = data.IdSanPham;
                    objDb.SoLuong = data.SoLuong;
                    objDb.ThanhTien = data.ThanhTien;

                    dbContext.SubmitChanges();
                    return true;
                }

                return false;
            }
            catch
            {
                return false;
            }
        }
    }
}
