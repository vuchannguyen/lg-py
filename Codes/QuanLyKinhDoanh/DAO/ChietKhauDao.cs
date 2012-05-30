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
            return dbContext.ChietKhaus.Where(p => p.Id == id).SingleOrDefault<ChietKhau>();
        }

        public static ChietKhau GetByIdSP(int id)
        {
            return dbContext.ChietKhaus.Where(p => p.IdSanPham == id && p.SanPham.DeleteFlag == false).SingleOrDefault<ChietKhau>();
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

            return false;
        }

        public static bool DeleteList(string ids, User user)
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
                            ChietKhau data = GetById(result);

                            if (!Delete(data, user))
                            {
                                CreateSQlConnection();

                                if (trans != null) trans.Rollback();

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

                dbContext.Connection.Close();

                return false;
            }
            catch
            {
                if (trans != null) trans.Rollback();
                return false;
            }
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
