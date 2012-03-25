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
        public static IQueryable<HoaDonDetail> GetQuery(string text)
        {
            var sql = from data in dbContext.HoaDonDetails
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

        public static List<HoaDonDetail> GetList(string text,
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

            var sql = GetQuery(text).OrderBy(sortSQL);

            if (skip == 0 && take == 0)
            {
                return sql.ToList();
            }

            return sql.Skip(skip).Take(take).ToList();
        }

        public static HoaDonDetail GetById(int id)
        {
            return dbContext.HoaDonDetails.Where(p => p.Id == id).SingleOrDefault<HoaDonDetail>();
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
            if (data != null)
            {
                HoaDonDetail objDb = GetById(data.Id);

                if (objDb != null)
                {
                    dbContext.HoaDonDetails.DeleteOnSubmit(objDb);

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
                dbContext.Connection.Open();
                trans = dbContext.Connection.BeginTransaction();
                dbContext.Transaction = trans;

                if (!string.IsNullOrEmpty(ids))
                {
                    ids = ids.TrimEnd(',');
                    string[] idArr = ids.Split(',');
                    int result = 0;

                    foreach (string id in idArr)
                    {
                        if (!int.TryParse(id, out result))
                        {
                            HoaDonDetail data = GetById(result);

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
