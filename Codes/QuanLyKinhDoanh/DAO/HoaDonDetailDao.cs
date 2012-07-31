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

                default:
                    sql = sql.Where(p => p.HoaDon.CreateDate.Day == date.Day && p.HoaDon.CreateDate.Month == date.Month && p.HoaDon.CreateDate.Year == date.Year);
                    break;
            }

            return sql;
        }

        public static int GetCount(string text, int type, string timeType, DateTime date)
        {
            return GetQuery(text, type, timeType, date).Count();
        }

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
