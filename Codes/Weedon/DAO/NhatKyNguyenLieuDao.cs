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
    public class NhatKyNguyenLieuDao : SQLConnection
    {
        public static IQueryable<NhatKyNguyenLieu> GetQuery(string text, bool? isActive, DateTime date)
        {
            var sql = from data in dbContext.NhatKyNguyenLieus
                      select data;

            if (!string.IsNullOrEmpty(text))
            {
                text = CommonDao.GetFilterText(text);
                sql = sql.Where(p => SqlMethods.Like(p.NguyenLieu.MaNguyenLieu, text) ||
                    SqlMethods.Like(p.NguyenLieu.Ten, text) ||
                    SqlMethods.Like(p.NguyenLieu.MoTa, text)
                    );
            }

            if (isActive != null)
            {
                sql = sql.Where(p => p.NguyenLieu.IsActive == isActive);
            }

            if (date != null)
            {
                sql = sql.Where(p => p.Date.Day == date.Day && p.Date.Month == date.Month && p.Date.Year == date.Year);
            }

            sql = sql.Where(p => p.DeleteFlag == false);

            return sql;
        }

        public static int GetCount(string text, bool? isActive, DateTime date)
        {
            return GetQuery(text, isActive, date).Count();
        }

        public static List<NhatKyNguyenLieu> GetList(string text, bool? isActive, DateTime date, 
            string sortColumn, string sortOrder, int skip, int take)
        {
            string sortSQL = string.Empty;

            switch (sortColumn)
            {
                default:
                    sortSQL += "NguyenLieu.Ten " + sortOrder;
                    break;
            }

            IQueryable<NhatKyNguyenLieu> sql = sql = GetQuery(text, isActive, date).OrderBy(sortSQL); ;

            if ((skip <= 0 && take <= 0) || (skip < 0 && take > 0) || (skip > 0 && take < 0))
            {
                return sql.ToList();
            }

            return sql.Skip(skip).Take(take).ToList();
        }

        public static NhatKyNguyenLieu GetLastData()
        {
            return dbContext.NhatKyNguyenLieus.OrderByDescending(p => p.Id).FirstOrDefault();
        }

        public static NhatKyNguyenLieu GetById(int id)
        {
            return dbContext.NhatKyNguyenLieus.Where(p => p.Id == id).FirstOrDefault<NhatKyNguyenLieu>();
        }

        public static bool Insert(NhatKyNguyenLieu data, User user)
        {
            try
            {
                dbContext.NhatKyNguyenLieus.InsertOnSubmit(data);
                dbContext.SubmitChanges();

                return true;
            }
            catch
            {
                return false;
            }
        }

        public static bool Delete(NhatKyNguyenLieu data, User user)
        {
            try
            {
                if (data != null)
                {
                    NhatKyNguyenLieu objDb = GetById(data.Id);

                    if (objDb != null)
                    {
                        objDb.DeleteFlag = true;
                        dbContext.SubmitChanges();

                        return true;
                    }
                }
            }
            catch
            {

            }

            CreateSQlConnection();

            return false;
        }

        public static bool DeleteList(string ids, User user)
        {
            DbTransaction trans = null;
            bool isDone = true;

            try
            {
                if (dbContext.Connection.State != System.Data.ConnectionState.Open)
                {
                    dbContext.Connection.Open();
                }

                if (dbContext.Transaction == null || dbContext.Transaction.Connection == null)
                {
                    trans = dbContext.Connection.BeginTransaction();
                    dbContext.Transaction = trans;
                }
                else
                {
                    trans = dbContext.Transaction;
                    dbContext.Transaction = trans;
                }

                if (!string.IsNullOrEmpty(ids))
                {
                    string[] idArr = ids.Split(new string[] { CommonDao.SEPERATE_STRING }, StringSplitOptions.RemoveEmptyEntries);
                    int result = 0;

                    foreach (string id in idArr)
                    {
                        if (int.TryParse(id, out result))
                        {
                            NhatKyNguyenLieu data = GetById(result);

                            if (!Delete(data, user))
                            {
                                isDone = false;

                                break;
                            }
                        }
                        else
                        {
                            isDone = false;

                            break;
                        }
                    }
                }
                else
                {
                    isDone = false;
                }
            }
            catch
            {
                isDone = false;
            }

            if (trans != null)
            {
                if (isDone)
                {
                    trans.Commit();
                }
                else
                {
                    trans.Rollback();
                }

                trans.Dispose();
            }

            dbContext.Connection.Close();

            return isDone;
        }

        public static bool Update(NhatKyNguyenLieu data, User user)
        {
            try
            {
                if (data != null)
                {
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
