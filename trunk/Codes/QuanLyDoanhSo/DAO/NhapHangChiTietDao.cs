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
    public class NhapHangChiTietDao : SQLConnection
    {
        #region NhapHangChiTiet
        /// <summary>
        /// Query with NhapHangChiTiet variables
        /// </summary>
        public static IQueryable<NhapHangChiTiet> GetQuery(string text)
        {
            var sql = from data in dbContext.NhapHangChiTiets
                      select data;

            if (!string.IsNullOrEmpty(text))
            {
                text = CommonDao.GetFilterText(text);
                sql = sql.Where(p => SqlMethods.Like(p.SanPham.Ten, text));
            }

            sql = sql.Where(p => p.NhapHang.DeleteFlag == false);

            return sql;
        }

        /// <summary>
        /// Count with NhapHangChiTiet variables
        /// </summary>
        public static int GetCount(string text)
        {
            return GetQuery(text).Count();
        }

        /// <summary>
        /// Get list with NhapHangChiTiet variables
        /// </summary>
        public static List<NhapHangChiTiet> GetList(string text,
            string sortColumn, string sortOrder, int skip, int take)
        {
            string sortSQL = string.Empty;

            switch (sortColumn)
            {
                case "Mã HĐ":
                    sortSQL += "HoaDon.MaHoaDon " + sortOrder;
                    break;

                case "Sản phẩm":
                    sortSQL += "SanPham.MaSanPham " + sortOrder;
                    break;

                case "Người nhập":
                    sortSQL += "HoaDon.User.UserName " + sortOrder;
                    break;

                case "Ngày tạo":
                    sortSQL += "HoaDon.Date " + sortOrder;
                    break;

                case "SL":
                    sortSQL += "SoLuong " + sortOrder;
                    break;

                //case "Giá bán":
                //    sortSQL += "NguyenLieu.GiaBan " + sortOrder;
                //    break;

                case "Tổng nhập":
                    sortSQL += "ThanhTien " + sortOrder;
                    break;

                default:
                    sortSQL += "HoaDon.MaHoaDon " + sortOrder;
                    break;
            }

            var sql = GetQuery(text).OrderBy(sortSQL);

            if ((skip <= 0 && take <= 0) || (skip < 0 && take > 0) || (skip > 0 && take < 0))
            {
                return sql.ToList();
            }

            return sql.Skip(skip).Take(take).ToList();
        }
        #endregion



        //#region SanPham
        ///// <summary>
        ///// Query with SanPham variables
        ///// </summary>
        //public static IQueryable<NhapHangChiTiet> GetQuery(int type, string text, int idGroup)
        //{
        //    SanPhamDao.GetQuery(text, idGroup);

        //    var sql = from data in dbContext.NhapHangChiTiets
        //              select data;

        //    if (type != 0)
        //    {
        //        //sql = sql.Where(p => p.HoaDon.IdType == type);
        //    }

        //    sql = sql.Where(p => p.HoaDon.DeleteFlag == false);

        //    if (!string.IsNullOrEmpty(text))
        //    {
        //        text = CommonDao.GetFilterText(text);
        //        sql = sql.Where(p => SqlMethods.Like(p.SanPham.MaSanPham, text) ||
        //            SqlMethods.Like(p.SanPham.Ten, text) ||
        //            SqlMethods.Like(p.SanPham.MoTa, text)
        //            );
        //    }

        //    sql = sql.Where(p => p.SanPham.DeleteFlag == false);

        //    return sql;
        //}

        ///// <summary>
        ///// Count with SanPham variables
        ///// </summary>
        //public static int GetCount(int type, string text, int idGroup)
        //{
        //    return GetQuery(type, text, idGroup).Count();
        //}

        ///// <summary>
        ///// Get list with SanPham variables
        ///// </summary>
        //public static List<NhapHangChiTiet> GetList(int type, string text, int idGroup,
        //    string sortColumn, string sortOrder, int skip, int take)
        //{
        //    string sortSQL = string.Empty;

        //    switch (sortColumn)
        //    {
        //        case "Mã SP":
        //            sortSQL += "SanPham.MaSanPham " + sortOrder;
        //            break;

        //        case "Tên":
        //            sortSQL += "SanPham.Ten " + sortOrder;
        //            break;

        //        case "Mô tả":
        //            sortSQL += "SanPham.MoTa " + sortOrder;
        //            break;

        //        case "Chi":
        //            sortSQL += "ThanhTien " + sortOrder;
        //            break;

        //        case "SL-B":
        //            //sortSQL += "NguyenLieu.SoLuong " + sortOrder;
        //            break;

        //        //case "Thực thu":
        //        //    sortSQL += "NguyenLieu.SoLuong " + sortOrder;
        //        //    break;

        //        default:
        //            sortSQL += "NguyenLieu.MaNguyenLieu " + sortOrder;
        //            break;
        //    }

        //    IQueryable<NhapHangChiTiet> sql = null;

        //    switch (sortColumn)
        //    {
        //        default:
        //            sql = GetQuery(type, text, idGroup).OrderBy(sortSQL);
        //            break;
        //    }

        //    if ((skip <= 0 && take <= 0) || (skip < 0 && take > 0) || (skip > 0 && take < 0))
        //    {
        //        return sql.ToList();
        //    }

        //    return sql.Skip(skip).Take(take).ToList();
        //}
        //#endregion



        public static List<NhapHangChiTiet> GetListByIdNhapHang(int idNhapHang)
        {
            return dbContext.NhapHangChiTiets.Where(p => p.IdNhapHang == idNhapHang).ToList();
        }

        public static NhapHangChiTiet GetLastData()
        {
            var sql = dbContext.NhapHangChiTiets.OrderBy("Id " + CommonDao.SORT_DESCENDING);

            return sql.Skip(0).Take(1).ToList().FirstOrDefault();
        }

        public static NhapHangChiTiet GetById(int id)
        {
            return dbContext.NhapHangChiTiets.Where(p => p.Id == id).FirstOrDefault<NhapHangChiTiet>();
        }

        public static bool Insert(NhapHangChiTiet data)
        {
            try
            {
                dbContext.NhapHangChiTiets.InsertOnSubmit(data);
                dbContext.SubmitChanges();

                return true;
            }
            catch
            {
                return false;
            }
        }

        public static bool Delete(NhapHangChiTiet data)
        {
            try
            {
                if (data != null)
                {
                    NhapHangChiTiet objDb = GetById(data.Id);

                    if (objDb != null)
                    {
                        dbContext.NhapHangChiTiets.DeleteOnSubmit(objDb);

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
                            NhapHangChiTiet data = GetById(result);

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

        public static bool Update(NhapHangChiTiet data)
        {
            try
            {
                if (data != null)
                {
                    NhapHangChiTiet objDb = GetById(data.Id);

                    objDb.IdNhapHang = data.IdNhapHang;
                    objDb.SoLuong = data.SoLuong;
                    objDb.ThanhTien = data.ThanhTien;

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
