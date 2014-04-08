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
    public class GiaChinhThucDao : SQLConnection
    {
        public static IQueryable<GiaChinhThuc> GetQuery(string text)
        {
            var sql = from data in dbContext.GiaChinhThucs
                      select data;

            if (!string.IsNullOrEmpty(text))
            {
                text = CommonDao.GetFilterText(text);
                sql = sql.Where(p => SqlMethods.Like(p.SanPham.MaSanPham, text) ||
                    SqlMethods.Like(p.SanPham.Ten, text) ||
                    SqlMethods.Like(p.SanPham.MoTa, text)
                    );
            }

            return sql;
        }

        public static int GetCount(string text)
        {
            return GetQuery(text).Count();
        }

        public static List<GiaChinhThuc> GetList(string text,
            string sortColumn, string sortOrder, int skip, int take)
        {
            string sortSQL = string.Empty;

            switch (sortColumn)
            {
                case "Mã SP":
                    sortSQL += "SanPham.MaSanPham " + sortOrder;
                    break;

                case "Tên":
                    sortSQL += "SanPham.Ten " + sortOrder;
                    break;

                case "Mô tả":
                    sortSQL += "SanPham.MoTa " + sortOrder;
                    break;

                default:
                    sortSQL += "SanPham.Ten " + sortOrder;
                    break;
            }

            var sql = GetQuery(text).OrderBy(sortSQL);

            if ((skip <= 0 && take <= 0) || (skip < 0 && take > 0) || (skip > 0 && take < 0))
            {
                return sql.ToList();
            }

            return sql.Skip(skip).Take(take).ToList();
        }

        public static GiaChinhThuc GetById(int id)
        {
            return dbContext.GiaChinhThucs.Where(p => p.Id == id).FirstOrDefault<GiaChinhThuc>();
        }

        public static GiaChinhThuc GetByIdSanPham(int id)
        {
            return dbContext.GiaChinhThucs.Where(p => p.SanPham.Id == id).FirstOrDefault<GiaChinhThuc>();
        }

        public static bool Insert(GiaChinhThuc data, User user)
        {
            try
            {
                if (GetByIdSanPham(data.IdSanPham) != null)
                {
                    return false;
                }

                dbContext.GiaChinhThucs.InsertOnSubmit(data);
                dbContext.SubmitChanges();

                return true;
            }
            catch
            {
                return false;
            }
        }

        public static bool Delete(GiaChinhThuc data, User user)
        {
            try
            {
                if (data != null)
                {
                    dbContext.GiaChinhThucs.DeleteOnSubmit(data);
                    dbContext.SubmitChanges();

                    return true;
                }
            }
            catch
            {
                //return false;
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
                            GiaChinhThuc data = GetById(result);

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

        public static bool Update(GiaChinhThuc data, User user)
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
