﻿using System;
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
                case "chTen":
                    sortSQL += "Ten " + sortOrder;
                    break;

                default:
                    sortSQL += "Ten " + CommonDao.SORT_ASCENDING;
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
            return dbContext.XuatXus.Where(p => p.Id == id).SingleOrDefault<XuatXu>();
        }

        public static XuatXu GetByUserMa(string text)
        {
            return dbContext.XuatXus.Where(p => p.Ten == text).SingleOrDefault<XuatXu>();
        }

        public static bool Insert(XuatXu data)
        {
            if (GetByUserMa(data.Ten) != null)
            {
                return false;
            }

            dbContext.XuatXus.InsertOnSubmit(data);
            dbContext.SubmitChanges();

            return true;
        }

        public static bool Delete(XuatXu data)
        {
            try
            {
                if (data != null)
                {
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

        public static bool DeleteList(string ids)
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

                            if (!Delete(data))
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

        public static bool Update(XuatXu data)
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
