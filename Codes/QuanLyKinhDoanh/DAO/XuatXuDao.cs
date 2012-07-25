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
    public class XuatXuDao : SQLConnection
    {
        public static IQueryable<XuatXu> GetQuery(string text)
        {
            var sql = from data in dbContext.XuatXus
                      select data;

            if (!string.IsNullOrEmpty(text))
            {
                text = CommonDao.GetFilterText(text);
                sql = sql.Where(p => SqlMethods.Like(p.Ten, text) ||
                    SqlMethods.Like(p.DiaChi, text) ||
                    SqlMethods.Like(p.Email, text)
                    );
            }

            return sql;
        }

        public static int GetCount(string text)
        {
            return GetQuery(text).Count();
        }

        public static List<XuatXu> GetList(string text,
            string sortColumn, string sortOrder, int skip, int take)
        {
            string sortSQL = string.Empty;

            switch (sortColumn)
            {
                case "Tên":
                    sortSQL += "Ten " + sortOrder;
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

                case "Fax":
                    sortSQL += "Fax " + sortOrder;
                    break;

                case "Email":
                    sortSQL += "Email " + sortOrder;
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

        public static XuatXu GetById(int id)
        {
            return dbContext.XuatXus.Where(p => p.Id == id).FirstOrDefault<XuatXu>();
        }

        public static bool Insert(XuatXu data, User user)
        {
            try
            {
                data.CreateBy = data.UpdateBy = user.UserName;
                data.CreateDate = data.UpdateDate = DateTime.Now;

                dbContext.XuatXus.InsertOnSubmit(data);
                dbContext.SubmitChanges();

                return true;
            }
            catch
            {
                return false;
            }
        }

        public static bool Delete(XuatXu data, User user)
        {
            try
            {
                if (data != null)
                {
                    data.UpdateBy = user.UserName;
                    data.UpdateDate = DateTime.Now;

                    dbContext.XuatXus.DeleteOnSubmit(data);
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
                            XuatXu data = GetById(result);

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

        public static bool Update(XuatXu data, User user)
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
