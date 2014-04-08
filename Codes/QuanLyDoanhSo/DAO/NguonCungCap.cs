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
    public class NguonCungCapDao: SQLConnection
    {
        public static IQueryable<NguonCungCap> GetQuery(string text)
        {
            var sql = from data in dbContext.NguonCungCaps
                      select data;

            if (!string.IsNullOrEmpty(text))
            {
                text = CommonDao.GetFilterText(text);
                sql = sql.Where(p => SqlMethods.Like(p.Ten, text) ||
                    SqlMethods.Like(p.Email, text)
                    );
            }

            sql = sql.Where(p => p.DeleteFlag == false);

            return sql;
        }

        public static int GetCount(string text)
        {
            return GetQuery(text).Count();
        }

        public static List<NguonCungCap> GetList(string text,
            string sortColumn, string sortOrder, int skip, int take)
        {
            string sortSQL = string.Empty;

            switch (sortColumn)
            {
                case "Tên":
                    sortSQL += "Ten " + sortOrder;
                    break;

                case "Tên đăng nhập":
                    sortSQL += "NguonCungCapName " + sortOrder;
                    break;

                case "Địa chỉ":
                    sortSQL += "DiaChi " + sortOrder;
                    break;

                case "Điện thoại":
                    sortSQL += "DienThoai " + sortOrder;
                    break;

                case "ĐTDĐ":
                    sortSQL += "DTDD " + sortOrder;
                    break;

                case "Email":
                    sortSQL += "Email " + sortOrder;
                    break;

                case "Fax":
                    sortSQL += "Fax " + sortOrder;
                    break;

                case "Mã số thuế":
                    sortSQL += "MaSoThue " + sortOrder;
                    break;

                case "Ghi chú":
                    sortSQL += "GhiChu " + sortOrder;
                    break;

                default:
                    sortSQL += "Ten " + sortOrder;
                    break;
            }

            var sql = GetQuery(text).OrderBy(sortSQL);

            if ((skip <= 0 && take <= 0) || (skip < 0 && take > 0) || (skip > 0 && take < 0))
            {
                return sql.ToList();
            }

            return sql.Skip(skip).Take(take).ToList();
        }

        public static NguonCungCap GetById(int id)
        {
            return dbContext.NguonCungCaps.Where(p => p.Id == id).FirstOrDefault<NguonCungCap>();
        }

        public static NguonCungCap GetByName(string text)
        {
            return dbContext.NguonCungCaps.Where(p => p.Ten == text && p.DeleteFlag == false).FirstOrDefault<NguonCungCap>();
        }

        public static bool Insert(NguonCungCap data)
        {
            try
            {
                if (GetByName(data.Ten) != null)
                {
                    return false;
                }

                dbContext.NguonCungCaps.InsertOnSubmit(data);
                dbContext.SubmitChanges();

                return true;
            }
            catch
            {
                return false;
            }
        }

        public static bool Delete(NguonCungCap data)
        {
            try
            {
                if (data != null)
                {
                    data.DeleteFlag = true;
                    dbContext.SubmitChanges();

                    return true;
                }
            }
            catch
            { 
                
            }

            CreateSQlConnection();

            return false;
        }

        public static bool DeleteList(string ids)
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
                            NguonCungCap data = GetById(result);

                            if (!Delete(data))
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

        public static bool Update(NguonCungCap data)
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
