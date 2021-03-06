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
    public class BanHang_LoaiTienDao : SQLConnection
    {
        public static IQueryable<BanHang_LoaiTien> GetQuery(string text)
        {
            var sql = from data in dbContext.BanHang_LoaiTiens
                      select data;

            if (!string.IsNullOrEmpty(text))
            {
                text = CommonDao.GetFilterText(text);
                sql = sql.Where(p => SqlMethods.Like(p.BanHang.User.Ten, text)
                    );
            }

            return sql;
        }

        public static int GetCount(string text)
        {
            return GetQuery(text).Count();
        }

        public static List<BanHang_LoaiTien> GetList(string text,
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
                    sortSQL += "SanPham.BanHang_LoaiTiens.Count " + sortOrder;
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

            var sql = GetQuery(text).OrderBy(sortSQL);

            if ((skip <= 0 && take <= 0) || (skip < 0 && take > 0) || (skip > 0 && take < 0))
            {
                return sql.ToList();
            }

            return sql.Skip(skip).Take(take).ToList();
        }

        public static List<BanHang_LoaiTien> GetListByIdBanHang(int idBanHang)
        {
            return dbContext.BanHang_LoaiTiens.Where(p => p.IdBanHang == idBanHang).ToList();
        }

        public static BanHang_LoaiTien GetById(int id)
        {
            return dbContext.BanHang_LoaiTiens.Where(p => p.Id == id).FirstOrDefault<BanHang_LoaiTien>();
        }

        public static bool Insert(BanHang_LoaiTien data)
        {
            try
            { 
                dbContext.BanHang_LoaiTiens.InsertOnSubmit(data);
                dbContext.SubmitChanges();

                return true;
            }  
            catch
            {
                return false;
            }
        }

        public static bool Delete(BanHang_LoaiTien data)
        {
            try
            {
                if (data != null)
                {
                    dbContext.BanHang_LoaiTiens.DeleteOnSubmit(data);
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
                            BanHang_LoaiTien data = GetById(result);

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

        public static bool Update(BanHang_LoaiTien data)
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
