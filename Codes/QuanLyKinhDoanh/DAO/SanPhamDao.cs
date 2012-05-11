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
    public class SanPhamDao : SQLConnection
    {
        #region SanPham
        public static IQueryable<SanPham> GetQuery(string text, int idGroup, bool isHavePrice, bool isNotZero)
        {
            var sql = from data in dbContext.SanPhams
                      select data;

            if (!string.IsNullOrEmpty(text))
            {
                text = CommonDao.GetFilterText(text);
                sql = sql.Where(p => SqlMethods.Like(p.MaSanPham, text) ||
                    SqlMethods.Like(p.Ten, text) ||
                    SqlMethods.Like(p.MoTa, text)
                    );
            }

            if (idGroup != 0)
            {
                sql = sql.Where(p => p.IdGroup == idGroup);
            }

            if (isHavePrice)
            {
                sql = sql.Where(p => p.GiaBan != null && p.GiaBan > 0);
            }

            if (isNotZero)
            {
                sql = sql.Where(p => p.SoLuong > 0);
            }

            sql = sql.Where(p => p.DeleteFlag == false);

            return sql;
        }

        public static int GetCount(string text, int idGroup, bool isHavePrice, bool isNotZero)
        {
            return GetQuery(text, idGroup, isHavePrice, isNotZero).Count();
        }

        public static List<SanPham> GetList(string text, int idGroup, bool isHaveGiaBan, bool isNotZero,
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

                default:
                    sortSQL += "Ten " + CommonDao.SORT_ASCENDING;
                    break;
            }

            var sql = GetQuery(text, idGroup, isHaveGiaBan, isNotZero).OrderBy(sortSQL);

            if ((skip <= 0 && take <= 0) || (skip < 0 && take > 0) || (skip > 0 && take < 0))
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
                sql = sql.Where(p => SqlMethods.Like(p.MaSanPham, text) ||
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

                default:
                    sortSQL += "Ten " + CommonDao.SORT_ASCENDING;
                    break;
            }

            var sql = GetQueryKho(text).OrderBy(sortSQL);

            if ((skip <= 0 && take <= 0) || (skip < 0 && take > 0) || (skip > 0 && take < 0))
            {
                return sql.ToList();
            }

            return sql.Skip(skip).Take(take).ToList();
        }
        #endregion



        public static SanPham GetLastData()
        {
            return dbContext.SanPhams.OrderByDescending(p => p.Id).FirstOrDefault();

            //return sql.Skip(0).Take(1).ToList().FirstOrDefault();
        }

        public static SanPham GetLastData(int idGroup)
        {
            return dbContext.SanPhams.Where(p => p.IdGroup == idGroup).OrderByDescending(p => p.MaSanPham).FirstOrDefault();

            //return sql.Skip(0).Take(1).ToList().FirstOrDefault();
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
                List<HoaDonDetail> listHoaDonDetail = HoaDonDetailDao.GetList(string.Empty, CommonDao.ID_TYPE_MUA,
                    string.Empty, string.Empty, 0, 0);

                if (listHoaDonDetail != null)
                {
                    listHoaDonDetail = listHoaDonDetail.Where(p => p.HoaDon.IdType == CommonDao.ID_TYPE_MUA &&
                        p.IdSanPham == data.Id && p.SanPham.DeleteFlag == false).ToList();
                    int total = 0;

                    foreach (HoaDonDetail detail in listHoaDonDetail)
                    {
                        total += detail.SoLuong;
                    }

                    if (total == data.SoLuong)
                    {
                        SanPham objDb = GetById(data.Id);

                        if (objDb != null)
                        {
                            objDb.DeleteFlag = true;
                            dbContext.SubmitChanges();

                            return true;
                        }
                    }
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
