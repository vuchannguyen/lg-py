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
    public class NguyenLieuDao : SQLConnection
    {
        public static IQueryable<NguyenLieu> GetQuery(string text, bool? isActive)
        {
            var sql = from data in dbContext.NguyenLieus
                      select data;

            if (!string.IsNullOrEmpty(text))
            {
                text = CommonDao.GetFilterText(text);
                sql = sql.Where(p => SqlMethods.Like(p.MaNguyenLieu, text) ||
                    SqlMethods.Like(p.Ten, text) ||
                    SqlMethods.Like(p.MoTa, text)
                    );
            }

            if (isActive != null)
            {
                sql = sql.Where(p => p.IsActive == isActive);
            }

            sql = sql.Where(p => p.DeleteFlag == false);

            return sql;
        }

        public static int GetCount(string text, bool? isActive)
        {
            return GetQuery(text, isActive).Count();
        }

        public static List<NguyenLieu> GetList(string text, bool? isActive,
            string sortColumn, string sortOrder, int skip, int take)
        {
            string sortSQL = string.Empty;

            switch (sortColumn)
            {
                case "Mã NL":
                    sortSQL += "MaNguyenLieu " + sortOrder;
                    break;

                case "Tên":
                    sortSQL += "Ten " + sortOrder;
                    break;

                case "ĐVT":
                    sortSQL += "DonViTinh " + sortOrder;
                    break;

                case "Mô tả":
                    sortSQL += "MoTa " + sortOrder;
                    break;

                default:
                    sortSQL += "Ten " + sortOrder;
                    break;
            }

            IQueryable<NguyenLieu> sql = sql = GetQuery(text, isActive).OrderBy(sortSQL); ;

            if ((skip <= 0 && take <= 0) || (skip < 0 && take > 0) || (skip > 0 && take < 0))
            {
                return sql.ToList();
            }

            return sql.Skip(skip).Take(take).ToList();
        }

        public static NguyenLieu GetLastData()
        {
            return dbContext.NguyenLieus.OrderByDescending(p => p.Id).FirstOrDefault();
        }

        public static NguyenLieu GetLastDataByMa()
        {
            return dbContext.NguyenLieus.OrderByDescending(p => p.MaNguyenLieu).FirstOrDefault();
        }

        public static NguyenLieu GetById(int id)
        {
            return dbContext.NguyenLieus.Where(p => p.Id == id).FirstOrDefault<NguyenLieu>();
        }

        public static bool Insert(NguyenLieu data, User user)
        {
            try
            {
                dbContext.NguyenLieus.InsertOnSubmit(data);
                dbContext.SubmitChanges();

                return true;
            }
            catch
            {
                return false;
            }
        }

        public static bool Delete(NguyenLieu data, User user)
        {
            try
            {
                if (data != null)
                {
                    NguyenLieu objDb = GetById(data.Id);

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
                            NguyenLieu data = GetById(result);

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

        public static bool Update(NguyenLieu data, User user)
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
