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
        public static IQueryable<SanPham> GetQuery(string text, int idGroup, bool isHavePrice, string status)
        {
            var sql = from data in dbContext.SanPhams
                      select data;

            if (!string.IsNullOrEmpty(text))
            {
                text = CommonDao.GetFilterText(text);
                sql = sql.Where(p => SqlMethods.Like(p.MaSanPham, text) ||
                    SqlMethods.Like(p.Ten, text) ||
                    SqlMethods.Like(p.XuatXu.Ten, text) ||
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

            switch (status)
            {
                case CommonDao.DEFAULT_STATUS_SP_ALL:
                    sql = sql.Where(p => p.SoLuong >= 0);
                    break;

                case CommonDao.DEFAULT_STATUS_SP_NOT_ZERO:
                    sql = sql.Where(p => p.SoLuong > 0);
                    break;

                case CommonDao.DEFAULT_STATUS_SP_ZERO:
                    sql = sql.Where(p => p.SoLuong == 0);
                    break;

                default:
                    sql = sql.Where(p => p.SoLuong >= 0);
                    break;
            }

            sql = sql.Where(p => p.DeleteFlag == false);

            return sql;
        }

        public static int GetCount(string text, int idGroup, bool isHavePrice, string status)
        {
            return GetQuery(text, idGroup, isHavePrice, status).Count();
        }

        public static List<SanPham> GetList(string text, int idGroup, bool isHaveGiaBan, string status,
            string sortColumn, string sortOrder, int skip, int take)
        {
            string sortSQL = string.Empty;

            switch (sortColumn)
            {
                case "Mã SP":
                    sortSQL += "MaSanPham " + sortOrder;
                    break;

                case "Nhóm":
                    sortSQL += "SanPhamGroup.Ten " + sortOrder;
                    break;

                case "Tên":
                    sortSQL += "Ten " + sortOrder;
                    break;

                case "Xuất xứ":
                    sortSQL += "XuatXu.Ten " + sortOrder;
                    break;

                case "ĐVT":
                    sortSQL += "DonViTinh " + sortOrder;
                    break;

                case "Mô tả":
                    sortSQL += "MoTa " + sortOrder;
                    break;

                case "SL":
                    sortSQL += "SoLuong " + sortOrder;
                    break;

                case "Giá bán":
                    sortSQL += "GiaBan " + sortOrder;
                    break;

                default:
                    sortSQL += "Ten " + sortOrder;
                    break;
            }

            var sql = GetQuery(text, idGroup, isHaveGiaBan, status).OrderBy(sortSQL);

            if ((skip <= 0 && take <= 0) || (skip < 0 && take > 0) || (skip > 0 && take < 0))
            {
                return sql.ToList();
            }

            return sql.Skip(skip).Take(take).ToList();
        }
        #endregion



        public static List<SanPham> GetListByIdGroup(int idGroup)
        {
            return dbContext.SanPhams.Where(p => p.IdGroup == idGroup).ToList();
        }

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
                List<HoaDonDetail> listHoaDonDetail = HoaDonDetailDao.GetList(string.Empty, CommonDao.ID_TYPE_MUA, string.Empty, DateTime.Now,
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
