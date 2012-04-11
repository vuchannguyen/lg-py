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
    public class HoaDonDao : SQLConnection
    {
        public static IQueryable<HoaDon> GetQuery(string text, int type)
        {
            var sql = from data in dbContext.HoaDons
                      select data;

            if (!string.IsNullOrEmpty(text))
            {
                text = CommonDao.GetFilterText(text);
                sql = sql.Where(p => SqlMethods.Like(p.User.Ten, text) ||
                    SqlMethods.Like(p.KhachHang.Ten, text)
                    );
            }

            if (type != 0)
            {
                sql = sql.Where(p => p.IdType == type);

            }
            return sql;
        }

        public static int GetCount(string text, int type)
        {
            return GetQuery(text, type).Count();
        }

        public static List<HoaDon> GetList(string text, int type,
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

            var sql = GetQuery(text, type).OrderBy(sortSQL);

            if ((skip <= 0 && take <= 0) || (skip < 0 && take > 0) || (skip > 0 && take < 0))
            {
                return sql.ToList();
            }

            return sql.Skip(skip).Take(take).ToList();
        }

        public static HoaDon GetLastData()
        {
            var sql = dbContext.HoaDons.OrderBy("Id " + CommonDao.SORT_DESCENDING);

            return sql.Skip(0).Take(1).ToList().FirstOrDefault();
        }

        public static HoaDon GetById(int id)
        {
            return dbContext.HoaDons.Where(p => p.Id == id).SingleOrDefault<HoaDon>();
        }

        public static bool Insert(HoaDon data)
        {
            try
            {
                dbContext.HoaDons.InsertOnSubmit(data);
                dbContext.SubmitChanges();

                return true;
            }
            catch
            {
                return false;
            }
        }

        public static bool Delete(HoaDon data)
        {
            if (data != null)
            {
                HoaDon objDb = GetById(data.Id);
                if (objDb != null)
                {
                    objDb.DeleteFlag = true;
                    dbContext.SubmitChanges();

                    return true;
                }
            }

            return false;
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
                            HoaDon data = GetById(result);

                            if (!Delete(data))
                            {
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

                return false;
            }
            catch
            {
                if (trans != null) trans.Rollback();
                return false;
            }
        }

        public static bool Update(HoaDon data)
        {
            try
            {
                if (data != null)
                {
                    //HoaDon objDb = GetById(data.Id);

                    //objDb.IdType = data.IdType;
                    //objDb.IdStatus = data.IdStatus;
                    //objDb.IdUser = data.IdUser;
                    //objDb.IdKhachHang = data.IdKhachHang;
                    //objDb.ThanhTien = data.ThanhTien;
                    //objDb.GhiChu = data.GhiChu;

                    //objDb.CreateBy = data.CreateBy;
                    //objDb.CreateDate = data.CreateDate;
                    //objDb.UpdateBy = data.UpdateBy;
                    //objDb.UpdateDate = data.UpdateDate;

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