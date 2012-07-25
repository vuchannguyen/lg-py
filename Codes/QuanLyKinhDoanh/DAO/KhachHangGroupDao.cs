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
    public class KhachHangGroupDao : SQLConnection
    {
        public static IQueryable<KhachHangGroup> GetQuery(string text)
        {
            var sql = from data in dbContext.KhachHangGroups
                      select data;

            if (!string.IsNullOrEmpty(text))
            {
                text = CommonDao.GetFilterText(text);
                sql = sql.Where(p => SqlMethods.Like(p.Ten, text) ||
                    SqlMethods.Like(p.MoTa, text)
                    );
            }

            return sql;
        }

        public static int GetCount(string text)
        {
            return GetQuery(text).Count();
        }

        public static List<KhachHangGroup> GetList(string text,
            string sortColumn, string sortOrder, int skip, int take)
        {
            string sortSQL = string.Empty;

            switch (sortColumn)
            {
                case "Tên":
                    sortSQL += "Ten " + sortOrder;
                    break;

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

        public static KhachHangGroup GetById(int id)
        {
            return dbContext.KhachHangGroups.Where(p => p.Id == id).FirstOrDefault<KhachHangGroup>();
        }

        public static KhachHangGroup GetByMa(string text)
        {
            return dbContext.KhachHangGroups.Where(p => p.Ma == text).FirstOrDefault<KhachHangGroup>();
        }

        public static bool Insert(KhachHangGroup data, User user)
        {
            try
            {
                if (GetByMa(data.Ma) != null)
                {
                    return false;
                }

                data.CreateBy = data.UpdateBy = user.UserName;
                data.CreateDate = data.UpdateDate = DateTime.Now;

                dbContext.KhachHangGroups.InsertOnSubmit(data);
                dbContext.SubmitChanges();

                return true;
            }
            catch
            {
                return false;
            }
        }

        public static bool Delete(KhachHangGroup data, User user)
        {
            try
            {
                if (data != null)
                {
                    data.UpdateBy = user.UserName;
                    data.UpdateDate = DateTime.Now;

                    dbContext.KhachHangGroups.DeleteOnSubmit(data);
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
                            KhachHangGroup data = GetById(result);

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

        public static bool Update(KhachHangGroup data, User user)
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
