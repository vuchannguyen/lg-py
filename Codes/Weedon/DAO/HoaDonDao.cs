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
        public static IQueryable<HoaDon> GetQuery(string text, int idUser, int idKH, DateTime date)
        {
            var sql = from data in dbContext.HoaDons
                      select data;

            if (!string.IsNullOrEmpty(text))
            {
                text = CommonDao.GetFilterText(text);
                sql = sql.Where(p => SqlMethods.Like(p.User.UserName, text) ||
                    SqlMethods.Like(p.KhachHang.MaKhachHang, text) ||
                    SqlMethods.Like(p.GhiChu, text)
                    );
            }

            if (idUser != 0)
            {
                sql = sql.Where(p => p.IdUser == idUser);
            }

            if (idKH != 0)
            {
                sql = sql.Where(p => p.IdKhachHang == idKH);
            }

            if (date != null)
            {
                sql = sql.Where(p => p.Date.Day == date.Day && p.Date.Month == date.Month && p.Date.Year == date.Year);
            }

            sql = sql.Where(p => p.DeleteFlag == false);

            return sql;
        }

        public static int GetCount(string text, int idUser, int idKH, DateTime date)
        {
            return GetQuery(text, idUser, idKH, date).Count();
        }

        public static List<HoaDon> GetList(string text, int idUser, int idKH, DateTime date,
            string sortColumn, string sortOrder, int skip, int take)
        {
            string sortSQL = string.Empty;

            switch (sortColumn)
            {
                case "Id":
                    sortSQL += "Id " + sortOrder;
                    break;

                case "Nhân viên":
                    sortSQL += "User.UserName " + sortOrder;
                    break;

                case "Thời gian":
                    sortSQL += "Date " + sortOrder;
                    break;

                case "Tổng tiền":
                    sortSQL += "ThanhTien " + sortOrder;
                    break;

                case "Ghi chú":
                    sortSQL += "GhiChu " + sortOrder;
                    break;

                case "Trạng thái":
                    sortSQL += "Duyet " + sortOrder;
                    break;

                default:
                    sortSQL += "Id " + sortOrder;
                    break;
            }

            var sql = GetQuery(text, idUser, idKH, date).OrderBy(sortSQL);

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
            //return dbContext.HoaDons.Where(p => p.IdType == idType).OrderByDescending(p => p.MaHoaDon).FirstOrDefault();
            return null;
        }

        public static HoaDon GetById(int id)
        {
            return dbContext.HoaDons.Where(p => p.Id == id).FirstOrDefault<HoaDon>();
        }

        public static HoaDon GetByDate(DateTime date)
        {
            return dbContext.HoaDons.Where(p => p.Date.Day == date.Day &&
                p.Date.Month == date.Month && p.Date.Year == date.Year).FirstOrDefault<HoaDon>();
        }

        public static bool Insert(HoaDon data, User user)
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

        public static bool Delete(HoaDon data, User user)
        {
            try
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
            }
            catch
            { 
                
            }

            CreateSQlConnection();

            return false;
        }

        public static bool DeleteList(string ids, User user)
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
                            HoaDon data = GetById(result);

                            if (!Delete(data, user))
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

        public static bool Update(HoaDon data, User user)
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