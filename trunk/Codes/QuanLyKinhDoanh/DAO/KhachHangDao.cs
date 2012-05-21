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
    public class KhachHangDao : SQLConnection
    {
        public static IQueryable<KhachHang> GetQuery(string text, bool isBirthDay)
        {
            var sql = from data in dbContext.KhachHangs
                      select data;

            if (isBirthDay)
            {
                sql = sql.Where(p => p.DOB.Value.AddYears(DateTime.Now.Year - p.DOB.Value.Year) >= DateTime.Now.AddDays(-1) &&
                    p.DOB.Value.AddYears(DateTime.Now.Year - p.DOB.Value.Year) <= DateTime.Now.AddDays(7));
            }
            else if (!string.IsNullOrEmpty(text))
            {
                text = CommonDao.GetFilterText(text);
                sql = sql.Where(p => SqlMethods.Like(p.MaKhachHang, text) ||
                    SqlMethods.Like(p.Ten, text) ||
                    SqlMethods.Like(p.DienThoai, text) ||
                    SqlMethods.Like(p.Email, text)
                    );
            }

            sql = sql.Where(p => p.DeleteFlag == false);

            return sql;
        }

        public static int GetCount(string text, bool isBirthDay)
        {
            return GetQuery(text, isBirthDay).Count();
        }

        public static List<KhachHang> GetList(string text, bool isBirthDay,
            string sortColumn, string sortOrder, int skip, int take)
        {
            string sortSQL = string.Empty;

            switch (sortColumn)
            {
                case "Mã KH":
                    sortSQL += "MaKhachHang " + sortOrder;
                    break;

                case "Họ và tên":
                    sortSQL += "Ten " + sortOrder;
                    break;

                case "Ngày sinh":
                    sortSQL += "DOB " + sortOrder;
                    break;

                case "Địa chỉ":
                    sortSQL += "DiaChi " + sortOrder;
                    break;

                case "Điện thoại":
                    sortSQL += "DienThoai " + sortOrder;
                    break;

                case "Email":
                    sortSQL += "Email " + sortOrder;
                    break;

                case "Tích lũy":
                    sortSQL += "TichLuy " + sortOrder;
                    break;

                default:
                    sortSQL += "Ten " + sortOrder;
                    break;
            }

            var sql = GetQuery(text, isBirthDay).OrderBy(sortSQL);

            if ((skip <= 0 && take <= 0) || (skip < 0 && take > 0) || (skip > 0 && take < 0))
            {
                return sql.ToList();
            }

            return sql.Skip(skip).Take(take).ToList();
        }

        public static KhachHang GetById(int id)
        {
            return dbContext.KhachHangs.Where(p => p.Id == id).SingleOrDefault<KhachHang>();
        }

        public static KhachHang GetLastData()
        {
            return dbContext.KhachHangs.OrderByDescending(p => p.Id).FirstOrDefault();

            //return sql.Skip(0).Take(1).ToList().FirstOrDefault();
        }

        public static KhachHang GetLastData(int idGroup)
        {
            return dbContext.KhachHangs.Where(p => p.IdGroup == idGroup).OrderByDescending(p => p.MaKhachHang).FirstOrDefault();

            //return sql.Skip(0).Take(1).ToList().FirstOrDefault();
        }

        public static bool Insert(KhachHang data)
        {
            try
            {
                dbContext.KhachHangs.InsertOnSubmit(data);
                dbContext.SubmitChanges();

                return true;
            }
            catch
            {
                return false;
            }
        }

        public static bool Delete(KhachHang data)
        {
            if (data != null)
            {
                KhachHang objDb = GetById(data.Id);

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
                            KhachHang data = GetById(result);

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

                dbContext.Connection.Close();

                return false;
            }
            catch
            {
                if (trans != null) trans.Rollback();
                return false;
            }
        }

        public static bool Update(KhachHang data)
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
