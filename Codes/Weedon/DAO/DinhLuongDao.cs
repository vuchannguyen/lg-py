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
    public class DinhLuongDao : SQLConnection
    {
        public static IQueryable<DinhLuong> GetQuery(string text)
        {
            var sql = from data in dbContext.DinhLuongs
                      select data;

            if (!string.IsNullOrEmpty(text))
            {
                text = CommonDao.GetFilterText(text);
                sql = sql.Where(p => SqlMethods.Like(p.SanPham.Ten, text) ||
                    SqlMethods.Like(p.NguyenLieu.Ten, text)
                    );
            }

            return sql;
        }

        public static int GetCount(string text)
        {
            return GetQuery(text).Count();
        }

        public static List<DinhLuong> GetList(string text,
            string sortColumn, string sortOrder, int skip, int take)
        {
            string sortSQL = string.Empty;

            switch (sortColumn)
            {
                case "Mã SP":
                    sortSQL += "SanPham.Ma " + sortOrder;
                    break;

                case "Nhóm SP":
                    sortSQL += "SanPham.SanPhamGroup.Ten " + sortOrder;
                    break;

                case "Tên SP":
                    sortSQL += "SanPham.Ten " + sortOrder;
                    break;

                case "SL-NL":
                    sortSQL += "SanPham.DinhLuongs.Count " + sortOrder;
                    break;

                case "Giá gốc":
                    sortSQL += "SanPham.GiaGoc " + sortOrder;
                    break;

                case "Giá bán":
                    sortSQL += "SanPham.GiaBan " + sortOrder;
                    break;

                default:
                    sortSQL += "SanPham.Ten " + sortOrder;
                    break;
            }

            IQueryable<DinhLuong> sql = null;

            switch (sortColumn)
            {
                case "SL-NL":
                    if (sortOrder == CommonDao.SORT_ASCENDING)
                    {
                        sql = GetQuery(text).OrderBy(p => p.SanPham.DinhLuongs.Count);
                    }
                    else
                    {
                        sql = GetQuery(text).OrderBy(p => p.SanPham.DinhLuongs.Count);
                    }
                    break;

                default:
                    sql = GetQuery(text).OrderBy(sortSQL);
                    break;
            }

            if ((skip <= 0 && take <= 0) || (skip < 0 && take > 0) || (skip > 0 && take < 0))
            {
                return sql.ToList();
            }

            return sql.Skip(skip).Take(take).ToList();
        }

        public static List<DinhLuong> GetListByIdSP(int id)
        {
            return dbContext.DinhLuongs.Where(p => p.IdSanPham == id).ToList();
        }

        public static List<DinhLuong> GetListByIdNL(int id)
        {
            return dbContext.DinhLuongs.Where(p => p.IdNguyenLieu == id).ToList();
        }

        public static DinhLuong GetById(int id)
        {
            return dbContext.DinhLuongs.Where(p => p.Id == id).FirstOrDefault<DinhLuong>();
        }

        public static int CountByIdSP(int id)
        {
            return dbContext.DinhLuongs.Where(p => p.IdSanPham == id).Count();
        }

        public static bool Insert(DinhLuong data, User user)
        {
            try
            { 
                dbContext.DinhLuongs.InsertOnSubmit(data);
                dbContext.SubmitChanges();

                return true;
            }  
            catch
            {
                return false;
            }
        }

        public static bool Delete(DinhLuong data, User user)
        {
            try
            {
                if (data != null)
                {
                    dbContext.DinhLuongs.DeleteOnSubmit(data);
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
                            DinhLuong data = GetById(result);

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

        public static bool Update(DinhLuong data, User user)
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
