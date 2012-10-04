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
    public class HoaDonDetailDao : SQLConnection
    {
        /// <summary>
        /// Query with HoaDonDetail variables
        /// </summary>
        /// <param name="text"></param>
        /// <param name="type"></param>
        /// <param name="timeType"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        public static IQueryable<HoaDonDetail> GetQuery(string text, int type, string timeType, DateTime date)
        {
            var sql = from data in dbContext.HoaDonDetails
                      select data;

            if (!string.IsNullOrEmpty(text))
            {
                text = CommonDao.GetFilterText(text);
                sql = sql.Where(p => SqlMethods.Like(p.SanPham.Ten, text) ||
                    SqlMethods.Like(p.SanPham.MaSanPham, text)
                    );
            }

            if (type != 0)
            {
                sql = sql.Where(p => p.HoaDon.IdType == type);
            }

            sql = sql.Where(p => p.HoaDon.IdStatus == CommonDao.ID_STATUS_DONE);
            sql = sql.Where(p => p.HoaDon.DeleteFlag == false);

            switch (timeType)
            {
                case CommonDao.DEFAULT_TYPE_DAY:
                    sql = sql.Where(p => p.HoaDon.CreateDate.Day == date.Day && p.HoaDon.CreateDate.Month == date.Month && p.HoaDon.CreateDate.Year == date.Year);
                    break;

                case CommonDao.DEFAULT_TYPE_MONTH:
                    sql = sql.Where(p => p.HoaDon.CreateDate.Month == date.Month && p.HoaDon.CreateDate.Year == date.Year);
                    break;

                case CommonDao.DEFAULT_TYPE_YEAR:
                    sql = sql.Where(p => p.HoaDon.CreateDate.Year == date.Year);
                    break;
            }

            return sql;
        }

        /// <summary>
        /// Query with SanPham variables
        /// </summary>
        /// <param name="text"></param>
        /// <param name="idGroup"></param>
        /// <param name="isHavePrice"></param>
        /// <param name="status"></param>
        /// <param name="isExpired"></param>
        /// <param name="warningDays"></param>
        /// <returns></returns>
        public static IQueryable<HoaDonDetail> GetQuery(int type, string text, int idGroup, bool isHavePrice, string status,
            bool isExpired, int warningDays)
        {
            SanPhamDao.GetQuery(text, idGroup, isHavePrice, status, isExpired, warningDays);

            var sql = from data in dbContext.HoaDonDetails
                      select data;

            if (type != 0)
            {
                sql = sql.Where(p => p.HoaDon.IdType == type);
            }

            sql = sql.Where(p => p.HoaDon.IdStatus == CommonDao.ID_STATUS_DONE);
            sql = sql.Where(p => p.HoaDon.DeleteFlag == false);

            if (!string.IsNullOrEmpty(text))
            {
                text = CommonDao.GetFilterText(text);
                sql = sql.Where(p => SqlMethods.Like(p.SanPham.MaSanPham, text) ||
                    SqlMethods.Like(p.SanPham.Ten, text) ||
                    SqlMethods.Like(p.SanPham.MoTa, text)
                    );
            }

            if (idGroup != 0)
            {
                sql = sql.Where(p => p.SanPham.IdGroup == idGroup);
            }

            if (isHavePrice)
            {
                sql = sql.Where(p => p.SanPham.GiaBan != null && p.SanPham.GiaBan > 0);
            }

            switch (status)
            {
                case CommonDao.DEFAULT_STATUS_SP_ALL:
                    sql = sql.Where(p => p.SanPham.SoLuong >= 0);
                    break;

                case CommonDao.DEFAULT_STATUS_SP_NOT_ZERO:
                    sql = sql.Where(p => p.SanPham.SoLuong > 0);
                    break;

                case CommonDao.DEFAULT_STATUS_SP_ZERO:
                    sql = sql.Where(p => p.SanPham.SoLuong == 0);
                    break;
            }

            if (isExpired)
            {
                sql = sql.Where(p => p.SanPham.ThoiHan.Value != 0 &&
                    DateTime.Now.AddDays(warningDays) >= (p.SanPham.DonViThoiHan == CommonDao.DEFAULT_TYPE_DAY ?
                    p.SanPham.CreateDate.AddDays(p.SanPham.ThoiHan.Value) : p.SanPham.DonViThoiHan == CommonDao.DEFAULT_TYPE_MONTH ?
                    p.SanPham.CreateDate.AddMonths(p.SanPham.ThoiHan.Value) : p.SanPham.CreateDate.AddYears(p.SanPham.ThoiHan.Value)));
            }

            sql = sql.Where(p => p.SanPham.DeleteFlag == false);

            return sql;
        }

        /// <summary>
        /// Count with HoaDonDetail variables
        /// </summary>
        /// <param name="text"></param>
        /// <param name="type"></param>
        /// <param name="timeType"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        public static int GetCount(string text, int type, string timeType, DateTime date)
        {
            return GetQuery(text, type, timeType, date).Count();
        }

        /// <summary>
        /// Count with SanPham variables
        /// </summary>
        /// <param name="text"></param>
        /// <param name="idGroup"></param>
        /// <param name="isHavePrice"></param>
        /// <param name="status"></param>
        /// <param name="isExpired"></param>
        /// <param name="warningDays"></param>
        /// <returns></returns>
        public static int GetCount(int type, string text, int idGroup, bool isHavePrice, string status,
            bool isExpired, int warningDays)
        {
            return GetQuery(type, text, idGroup, isHavePrice, status, isExpired, warningDays).Count();
        }

        /// <summary>
        /// Get list with HoaDonDetail variables
        /// </summary>
        /// <param name="text"></param>
        /// <param name="type"></param>
        /// <param name="timeType"></param>
        /// <param name="date"></param>
        /// <param name="sortColumn"></param>
        /// <param name="sortOrder"></param>
        /// <param name="skip"></param>
        /// <param name="take"></param>
        /// <returns></returns>
        public static List<HoaDonDetail> GetList(string text, int type, string timeType, DateTime date,
            string sortColumn, string sortOrder, int skip, int take)
        {
            string sortSQL = string.Empty;

            switch (sortColumn)
            {
                case "Mã Nhập":
                    sortSQL += "HoaDon.MaHoaDon " + sortOrder;
                    break;

                case "Sản phẩm":
                    sortSQL += "SanPham.MaSanPham " + sortOrder;
                    break;

                case "Người nhập":
                    sortSQL += "HoaDon.User.UserName " + sortOrder;
                    break;

                case "Ngày tạo":
                    sortSQL += "HoaDon.CreateDate " + sortOrder;
                    break;

                case "Ngày cập nhật":
                    sortSQL += "HoaDon.UpdateDate " + sortOrder;
                    break;

                case "SL":
                    sortSQL += "SoLuong " + sortOrder;
                    break;

                case "ĐVT":
                    sortSQL += "SanPham.DonViTinh " + sortOrder;
                    break;

                case "Giá nhập":
                    sortSQL += "SanPham.GiaMua " + sortOrder;
                    break;

                case "Giá bán":
                    sortSQL += "SanPham.GiaBan " + sortOrder;
                    break;

                case "Tổng nhập":
                    sortSQL += "ThanhTien " + sortOrder;
                    break;

                default:
                    sortSQL += "HoaDon.MaHoaDon " + sortOrder;
                    break;
            }

            var sql = GetQuery(text, type, timeType, date).OrderBy(sortSQL);

            if ((skip <= 0 && take <= 0) || (skip < 0 && take > 0) || (skip > 0 && take < 0))
            {
                return sql.ToList();
            }

            return sql.Skip(skip).Take(take).ToList();
        }

        /// <summary>
        /// Get list with SanPham variables
        /// </summary>
        /// <param name="text"></param>
        /// <param name="idGroup"></param>
        /// <param name="isHaveGiaBan"></param>
        /// <param name="status"></param>
        /// <param name="isExpired"></param>
        /// <param name="warningDays"></param>
        /// <param name="sortColumn"></param>
        /// <param name="sortOrder"></param>
        /// <param name="skip"></param>
        /// <param name="take"></param>
        /// <returns></returns>
        public static List<HoaDonDetail> GetList(int type, string text, int idGroup, bool isHavePrice, string status,
            bool isExpired, int warningDays,
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

                case "SL-N":
                    sortSQL += "SoLuong " + sortOrder;
                    break;

                case "ĐVT":
                    sortSQL += "SanPham.DonViTinh " + sortOrder;
                    break;

                case "Chi":
                    sortSQL += "ThanhTien " + sortOrder;
                    break;

                case "SL-B":
                    //sortSQL += "SanPham.SoLuong " + sortOrder;
                    break;

                //case "Thực thu":
                //    sortSQL += "SanPham.SoLuong " + sortOrder;
                //    break;

                default:
                    sortSQL += "SanPham.MaSanPham " + sortOrder;
                    break;
            }

            IQueryable<HoaDonDetail> sql = null;

            switch (sortColumn)
            {
                case "SL-B":
                    if (sortOrder == CommonDao.SORT_ASCENDING)
                    {
                        sql = GetQuery(type, text, idGroup, isHavePrice, status, isExpired, warningDays).OrderBy(
                            p => p.SoLuong - p.SanPham.SoLuong);
                    }
                    else
                    {
                        sql = GetQuery(type, text, idGroup, isHavePrice, status, isExpired, warningDays).OrderByDescending(
                            p => p.SoLuong - p.SanPham.SoLuong);
                    }
                    break;

                case "Thực thu":
                    if (sortOrder == CommonDao.SORT_ASCENDING)
                    {
                        sql = GetQuery(type, text, idGroup, isHavePrice, status, isExpired, warningDays).OrderBy(
                            p => p.SanPham.GiaBan * (p.SoLuong - p.SanPham.SoLuong));
                    }
                    else
                    {
                        sql = GetQuery(type, text, idGroup, isHavePrice, status, isExpired, warningDays).OrderByDescending(
                            p => p.SanPham.GiaBan * (p.SoLuong - p.SanPham.SoLuong));
                    }
                    break;

                case "Thống kê":
                    if (sortOrder == CommonDao.SORT_ASCENDING)
                    {
                        sql = GetQuery(type, text, idGroup, isHavePrice, status, isExpired, warningDays).OrderBy(
                            p => p.SanPham.GiaBan * (p.SoLuong - p.SanPham.SoLuong) - p.ThanhTien);
                    }
                    else
                    {
                        sql = GetQuery(type, text, idGroup, isHavePrice, status, isExpired, warningDays).OrderByDescending(
                            p => p.SanPham.GiaBan * (p.SoLuong - p.SanPham.SoLuong) - p.ThanhTien);
                    }
                    break;

                default:
                    sql = GetQuery(type, text, idGroup, isHavePrice, status, isExpired, warningDays).OrderBy(sortSQL);
                    break;
            }

            if ((skip <= 0 && take <= 0) || (skip < 0 && take > 0) || (skip > 0 && take < 0))
            {
                return sql.ToList();
            }

            return sql.Skip(skip).Take(take).ToList();
        }

        public static List<HoaDonDetail> GetListByIdHoaDon(int idHoaDon)
        {
            return dbContext.HoaDonDetails.Where(p => p.IdHoaDon == idHoaDon).ToList();
        }

        public static HoaDonDetail GetLastData()
        {
            var sql = dbContext.HoaDonDetails.OrderBy("Id " + CommonDao.SORT_DESCENDING);

            return sql.Skip(0).Take(1).ToList().FirstOrDefault();
        }

        public static HoaDonDetail GetById(int id)
        {
            return dbContext.HoaDonDetails.Where(p => p.Id == id).FirstOrDefault<HoaDonDetail>();
        }

        public static bool CheckIfSold(int idSP)
        {
            return dbContext.HoaDonDetails.Where(p => p.IdSanPham == idSP && p.HoaDon.IdType == CommonDao.ID_TYPE_BAN).FirstOrDefault<HoaDonDetail>() != null;
        }

        public static bool Insert(HoaDonDetail data)
        {
            try
            {
                dbContext.HoaDonDetails.InsertOnSubmit(data);
                dbContext.SubmitChanges();

                return true;
            }
            catch
            {
                return false;
            }
        }

        public static bool Delete(HoaDonDetail data)
        {
            try
            {
                if (data != null)
                {
                    HoaDonDetail objDb = GetById(data.Id);

                    if (objDb != null)
                    {
                        dbContext.HoaDonDetails.DeleteOnSubmit(objDb);

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
                            HoaDonDetail data = GetById(result);

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

        public static bool Update(HoaDonDetail data)
        {
            try
            {
                if (data != null)
                {
                    HoaDonDetail objDb = GetById(data.Id);

                    objDb.IdHoaDon = data.IdHoaDon;
                    objDb.IdSanPham = data.IdSanPham;
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
