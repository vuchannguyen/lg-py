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
    public class HoaDonTypeDao : SQLConnection
    {
        public static IQueryable<HoaDonType> GetQuery(string text)
        {
            var sql = from data in dbContext.HoaDonTypes
                      select data;

            if (!string.IsNullOrEmpty(text))
            {
                text = CommonDao.GetFilterText(text);
                sql = sql.Where(p => SqlMethods.Like(p.Ten, text)
                    );
            }

            return sql;
        }

        public static int GetCount(string text)
        {
            return GetQuery(text).Count();
        }

        public static List<HoaDonType> GetList(string text,
            string sortColumn, string sortOrder, int skip, int take)
        {
            string sortSQL = string.Empty;

            switch (sortColumn)
            {
                case "chTen":
                    sortSQL += "Ten " + sortOrder;
                    break;

                default:
                    sortSQL += "Ten " + CommonDao.SORT_ASCENDING;
                    break;
            }

            var sql = GetQuery(text).OrderBy(sortSQL);

            if (skip == 0 && take == 0)
            {
                return sql.ToList();
            }

            return sql.Skip(skip).Take(take).ToList();
        }

        public static HoaDonType GetById(int id)
        {
            return dbContext.HoaDonTypes.Where(p => p.Id == id).SingleOrDefault<HoaDonType>();
        }

        public static bool Insert(HoaDonType data)
        {
            try
            {
                dbContext.HoaDonTypes.InsertOnSubmit(data);
                dbContext.SubmitChanges();

                return true;
            }
            catch
            {
                return false;
            }
        }

        public static bool Delete(HoaDonType data)
        {
            if (data != null)
            {
                HoaDonType objDb = GetById(data.Id);
                if (objDb != null)
                {
                    dbContext.HoaDonTypes.DeleteOnSubmit(objDb);

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
                dbContext.Connection.Open();
                trans = dbContext.Connection.BeginTransaction();
                dbContext.Transaction = trans;

                if (!string.IsNullOrEmpty(ids))
                {
                    ids = ids.TrimEnd(',');
                    string[] idArr = ids.Split(',');
                    int result = 0;

                    foreach (string id in idArr)
                    {
                        if (!int.TryParse(id, out result))
                        {
                            HoaDonType data = GetById(result);

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

        public static bool Update(HoaDonType data)
        {
            try
            {
                if (data != null)
                {
                    HoaDonType objDb = GetById(data.Id);

                    objDb.Ten = data.Ten;

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
