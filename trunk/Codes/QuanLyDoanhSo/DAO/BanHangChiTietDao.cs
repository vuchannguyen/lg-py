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
    public class BanHangChiTietDao : SQLConnection
    {
        public static IQueryable<BanHangChiTiet> GetQuery(string text)
        {
            var sql = from data in dbContext.BanHangChiTiets
                      select data;

            if (!string.IsNullOrEmpty(text))
            {
                text = CommonDao.GetFilterText(text);
                sql = sql.Where(p => SqlMethods.Like(p.SanPham.Ten, text)
                    );
            }

            return sql;
        }

        public static int GetCount(string text)
        {
            return GetQuery(text).Count();
        }

        public static List<BanHangChiTiet> GetList(string text,
            string sortColumn, string sortOrder, int skip, int take)
        {
            string sortSQL = string.Empty;

            switch (sortColumn)
            {
                case "Mã KH":
                    sortSQL += "MaBanHangChiTiet " + sortOrder;
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

                case "ĐTDĐ":
                    sortSQL += "DTDD " + sortOrder;
                    break;

                case "Email":
                    sortSQL += "Email " + sortOrder;
                    break;

                case "Tích lũy":
                    sortSQL += "TichLuy " + sortOrder;
                    break;

                default:
                    sortSQL += "MaBanHangChiTiet " + sortOrder;
                    break;
            }

            var sql = GetQuery(text).OrderBy(sortSQL);

            if ((skip <= 0 && take <= 0) || (skip < 0 && take > 0) || (skip > 0 && take < 0))
            {
                return sql.ToList();
            }

            return sql.Skip(skip).Take(take).ToList();
        }

        public static List<BanHangChiTiet> GetListByIdBanHang(int idBanHang)
        {
            return dbContext.BanHangChiTiets.Where(p => p.IdBanHang == idBanHang).ToList();
        }

        public static BanHangChiTiet GetById(int id)
        {
            return dbContext.BanHangChiTiets.Where(p => p.Id == id).FirstOrDefault<BanHangChiTiet>();
        }

        public static BanHangChiTiet GetLastData()
        {
            return dbContext.BanHangChiTiets.OrderByDescending(p => p.Id).FirstOrDefault();
        }

        public static bool Insert(BanHangChiTiet data)
        {
            try
            {
                dbContext.BanHangChiTiets.InsertOnSubmit(data);
                dbContext.SubmitChanges();

                return true;
            }
            catch
            {
                return false;
            }
        }

        public static bool Delete(BanHangChiTiet data)
        {
            try
            {
                if (data != null)
                {
                    BanHangChiTiet objDb = GetById(data.Id);

                    if (objDb != null)
                    {
                        dbContext.BanHangChiTiets.DeleteOnSubmit(objDb);

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
                            BanHangChiTiet data = GetById(result);

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

        public static bool Update(BanHangChiTiet data)
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
