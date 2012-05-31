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
    public class HoaDonDao : SQLConnection
    {
        public static IQueryable<HoaDon> GetQuery(string text, int type, int status, string timeType, DateTime date)
        {
            var sql = from data in dbContext.HoaDons
                      select data;

            if (!string.IsNullOrEmpty(text))
            {
                text = CommonDao.GetFilterText(text);
                sql = sql.Where(p => SqlMethods.Like(p.MaHoaDon, text) ||
                    SqlMethods.Like(p.KhachHang.MaKhachHang, text) ||
                    SqlMethods.Like(p.KhachHang.Ten, text) ||
                    SqlMethods.Like(p.GhiChu, text)
                    );
            }

            switch (type)
            {
                case CommonDao.ID_TYPE_BAN_THU:
                    sql = sql.Where(p => p.IdType == CommonDao.ID_TYPE_BAN || p.IdType == CommonDao.ID_TYPE_THU);
                    break;

                case CommonDao.ID_TYPE_MUA_CHI:
                    sql = sql.Where(p => p.IdType == CommonDao.ID_TYPE_MUA || p.IdType == CommonDao.ID_TYPE_CHI);
                    break;

                default:
                    sql = sql.Where(p => p.IdType == type);
                    break;
            }

            sql = sql.Where(p => p.IdStatus == status);
            sql = sql.Where(p => p.DeleteFlag == false);

            switch (timeType)
            {
                case CommonDao.DEFAULT_TYPE_DAY:
                    sql = sql.Where(p => p.CreateDate.Day == date.Day);
                    break;

                case CommonDao.DEFAULT_TYPE_MONTH:
                    sql = sql.Where(p => p.CreateDate.Month == date.Month);
                    break;

                case CommonDao.DEFAULT_TYPE_YEAR:
                    sql = sql.Where(p => p.CreateDate.Year == date.Year);
                    break;

                default:
                    sql = sql.Where(p => p.CreateDate.Day == date.Day);
                    break;
            }

            return sql;
        }

        public static int GetCount(string text, int type, int status, string timeType, DateTime date)
        {
            return GetQuery(text, type, status, timeType, date).Count();
        }

        public static List<HoaDon> GetList(string text, int type, int status, string timeType, DateTime date,
            string sortColumn, string sortOrder, int skip, int take)
        {
            string sortSQL = string.Empty;

            switch (sortColumn)
            {
                case "Mã HĐ":
                    sortSQL += "MaHoaDon " + sortOrder;
                    break;

                case "Người nhập":
                    sortSQL += "User.UserName " + sortOrder;
                    break;

                case "Khách hàng":
                    sortSQL += "KhachHang.MaKhachHang " + sortOrder;
                    break;

                case "Ngày tạo":
                    sortSQL += "CreateDate " + sortOrder;
                    break;

                case "Ngày cập nhật":
                    sortSQL += "UpdateDate " + sortOrder;
                    break;

                case "Ghi chú":
                    sortSQL += "GhiChu " + sortOrder;
                    break;

                case "Còn lại":
                    sortSQL += "ConLai " + sortOrder;
                    break;

                case "Tổng HĐ":
                    sortSQL += "ThanhTien " + sortOrder;
                    break;

                default:
                    sortSQL += "MaHoaDon " + sortOrder;
                    break;
            }

            var sql = GetQuery(text, type, status, timeType, date).OrderBy(sortSQL);

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

        public static HoaDon GetLastData(int idType)
        {
            return dbContext.HoaDons.Where(p => p.IdType == idType).OrderByDescending(p => p.MaHoaDon).FirstOrDefault();
        }

        public static HoaDon GetById(int id)
        {
            return dbContext.HoaDons.Where(p => p.Id == id).SingleOrDefault<HoaDon>();
        }

        public static bool Insert(HoaDon data, User user)
        {
            try
            {
                data.CreateBy = data.UpdateBy = user.UserName;
                data.CreateDate = data.UpdateDate = DateTime.Now;

                dbContext.HoaDons.InsertOnSubmit(data);
                dbContext.SubmitChanges();

                return true;
            }
            catch
            {
                return false;
            }
        }

        public static bool Delete(HoaDon data, User user)
        {
            if (data != null)
            {
                HoaDon objDb = GetById(data.Id);

                if (objDb != null)
                {
                    data.UpdateBy = user.UserName;
                    data.UpdateDate = DateTime.Now;

                    objDb.DeleteFlag = true;
                    dbContext.SubmitChanges();

                    return true;
                }
            }

            return false;
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
                            HoaDon data = GetById(result);

                            if (!Delete(data, user))
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

        public static bool Update(HoaDon data, User user)
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