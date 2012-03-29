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
    public class SanPhamDao : SQLConnection
    {
        #region SanPham
        public static IQueryable<SanPham> GetQuery(string text)
        {
            var sql = from data in dbContext.SanPhams
                      select data;

            if (!string.IsNullOrEmpty(text))
            {
                text = CommonDao.GetFilterText(text);
                sql = sql.Where(p => SqlMethods.Like(p.IdSanPham, text) ||
                    SqlMethods.Like(p.Ten, text) ||
                    SqlMethods.Like(p.MoTa, text)
                    );
            }

            sql = sql.Where(p => p.DeleteFlag == false);

            return sql;
        }

        public static int GetCount(string text)
        {
            return GetQuery(text).Count();
        }

        public static List<SanPham> GetList(string text,
            string sortColumn, string sortOrder, int skip, int take)
        {
            string sortSQL = string.Empty;

            switch (sortColumn)
            {
                case "chId":
                    sortSQL += "Id " + sortOrder;
                    break;

                case "chTen":
                    sortSQL += "Ten " + sortOrder;
                    break;

                //case "ExamQuestion":
                //    sortSQL += "LOT_ExamQuestion.Title " + sortOrder;
                //    break;

                //case "ExamDate":
                //    sortSQL += "ExamDate " + sortOrder;
                //    break;

                //case "ExamType":
                //    sortSQL += "ExamType " + sortOrder;
                //    break;

                default:
                    sortSQL += "Ten " + CommonDao.SORT_ASCENDING;
                    break;
            }

            var sql = GetQuery(text).OrderBy(sortSQL);

            if (skip == 0 && take == 0)
            {
                return sql.ToList();
            }

            return sql.Skip(skip).Take(take).ToList();
        }
        #endregion



        #region Kho hang
        public static IQueryable<SanPham> GetQueryKho(string text)
        {
            var sql = from data in dbContext.SanPhams
                      select data;

            if (!string.IsNullOrEmpty(text))
            {
                text = CommonDao.GetFilterText(text);
                sql = sql.Where(p => SqlMethods.Like(p.IdSanPham, text) ||
                    SqlMethods.Like(p.Ten, text) ||
                    SqlMethods.Like(p.SanPhamGroup.Ten, text)
                    );
            }

            sql = sql.Where(p => p.DeleteFlag == false);

            return sql;
        }

        public static int GetCountKho(string text)
        {
            return GetQueryKho(text).Count();
        }

        public static List<SanPham> GetListKho(string text,
            string sortColumn, string sortOrder, int skip, int take)
        {
            string sortSQL = string.Empty;

            switch (sortColumn)
            {
                case "chId":
                    sortSQL += "Id " + sortOrder;
                    break;

                case "chTen":
                    sortSQL += "Ten " + sortOrder;
                    break;

                //case "ExamQuestion":
                //    sortSQL += "LOT_ExamQuestion.Title " + sortOrder;
                //    break;

                //case "ExamDate":
                //    sortSQL += "ExamDate " + sortOrder;
                //    break;

                //case "ExamType":
                //    sortSQL += "ExamType " + sortOrder;
                //    break;

                default:
                    sortSQL += "Ten " + CommonDao.SORT_ASCENDING;
                    break;
            }

            var sql = GetQueryKho(text).OrderBy(sortSQL);

            if (skip == 0 && take == 0)
            {
                return sql.ToList();
            }

            return sql.Skip(skip).Take(take).ToList();
        }
        #endregion

        public static SanPham GetLastData()
        {
            var sql = dbContext.SanPhams.OrderBy("Id " + CommonDao.SORT_DESCENDING);

            return sql.Skip(0).Take(1).ToList().FirstOrDefault();
        }

        public static SanPham GetById(int id)
        {
            return dbContext.SanPhams.Where(p => p.Id == id).SingleOrDefault<SanPham>();
        }

        public static bool Insert(SanPham data)
        {
            try
            {
                dbContext.SanPhams.InsertOnSubmit(data);
                dbContext.SubmitChanges();

                return true;
            }
            catch
            {
                return false;
            }
        }

        public static bool Delete(SanPham data)
        {
            if (data != null)
            {
                SanPham objDb = GetById(data.Id);
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
                            SanPham data = GetById(result);

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

        public static bool Update(SanPham data)
        {
            try
            {
                if (data != null)
                {
                    SanPham objDb = GetById(data.Id);

                    objDb.Ten = data.Ten;
                    objDb.IdSanPham = data.IdSanPham;
                    objDb.IdGroup = data.IdGroup;
                    objDb.MoTa = data.MoTa;
                    objDb.GiaMua = data.GiaMua;
                    objDb.GiaBan = data.GiaBan;
                    objDb.LaiSuat = data.LaiSuat;
                    objDb.SoLuong = data.SoLuong;
                    objDb.DonViTinh = data.DonViTinh;
                    objDb.XuatXu = data.XuatXu;
                    objDb.Hieu = data.Hieu;
                    objDb.Size = data.Size;
                    objDb.ThoiGianBaoHanh = data.ThoiGianBaoHanh;
                    objDb.DonViBaoHanh = data.DonViBaoHanh;
                    objDb.GhiChu = data.GhiChu;

                    objDb.CreateBy = data.CreateBy;
                    objDb.CreateDate = data.CreateDate;
                    objDb.UpdateBy = data.UpdateBy;
                    objDb.UpdateDate = data.UpdateDate;

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
