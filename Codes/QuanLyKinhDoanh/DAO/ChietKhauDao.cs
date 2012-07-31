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
    public class ChietKhauDao : SQLConnection
    {
        public static IQueryable<ChietKhau> GetQuery(string text)
        {
            var sql = from data in dbContext.ChietKhaus
                      select data;

            if (!string.IsNullOrEmpty(text))
            {
                text = CommonDao.GetFilterText(text);
                sql = sql.Where(p => SqlMethods.Like(p.SanPham.Ten, text) ||
                    SqlMethods.Like(p.Value.ToString(), text)
                    );
            }

            return sql;
        }

        public static int GetCount(string text)
        {
            return GetQuery(text).Count();
        }

        public static List<ChietKhau> GetList(string text,
            string sortColumn, string sortOrder, int skip, int take)
        {
            string sortSQL = string.Empty;

            switch (sortColumn)
            {
                default:
                    sortSQL += "Id " + sortOrder;
                    break;
            }

            var sql = GetQuery(text).OrderBy(sortSQL);

            if ((skip <= 0 && take <= 0) || (skip < 0 && take > 0) || (skip > 0 && take < 0))
            {
                return sql.ToList();
            }

            return sql.Skip(skip).Take(take).ToList();
        }

        public static ChietKhau GetById(int id)
        {
            return dbContext.ChietKhaus.Where(p => p.Id == id).FirstOrDefault<ChietKhau>();
        }

        public static ChietKhau GetByIdSP(int id)
        {
            return dbContext.ChietKhaus.Where(p => p.IdSanPham == id && p.SanPham.DeleteFlag == false).FirstOrDefault<ChietKhau>();
        }

        public static bool Insert(ChietKhau data, User user)
        {
            try
            {
                if (GetByIdSP(data.IdSanPham) != null)
                {
                    return false;
                }

                data.CreateBy = data.UpdateBy = user.UserName;
                data.CreateDate = data.UpdateDate = DateTime.Now;

                dbContext.ChietKhaus.InsertOnSubmit(data);
                dbContext.SubmitChanges();

                return true;
            }
            catch
            {
                return false;
            }
        }

        public static bool Delete(ChietKhau data, User user)
        {
            try
            {
                if (data != null)
                {
                    ChietKhau objDb = GetById(data.Id);

                    if (objDb != null)
                    {
                        data.UpdateBy = user.UserName;
                        data.UpdateDate = DateTime.Now;

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
                            ChietKhau data = GetById(result);

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

        public static bool Update(ChietKhau data, User user)
        {
            try
            {
                if (data != null)
                {
                    data.UpdateBy = user.UserName;
                    data.UpdateDate = DateTime.Now;

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
