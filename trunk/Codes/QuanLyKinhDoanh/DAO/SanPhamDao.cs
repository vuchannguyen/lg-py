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
        public static IQueryable<SanPham> GetQuery(string text, int idGroup, bool isHavePrice, string status,
            bool isExpired, int warningDays)
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
            }

            if (isExpired)
            {
                sql = sql.Where(p => p.ThoiHan.Value != 0 &&
                    DateTime.Now.AddDays(warningDays) >= (p.DonViThoiHan == CommonDao.DEFAULT_TYPE_DAY ?
                    p.CreateDate.AddDays(p.ThoiHan.Value) : p.DonViThoiHan == CommonDao.DEFAULT_TYPE_MONTH ?
                    p.CreateDate.AddMonths(p.ThoiHan.Value) : p.CreateDate.AddYears(p.ThoiHan.Value)));
            }

            sql = sql.Where(p => p.DeleteFlag == false);

            return sql;
        }

        public static int GetCount(string text, int idGroup, bool isHavePrice, string status,
            bool isExpired, int warningDays)
        {
            return GetQuery(text, idGroup, isHavePrice, status, isExpired, warningDays).Count();
        }

        public static List<SanPham> GetList(string text, int idGroup, bool isHavePrice, string status,
            bool isExpired, int warningDays,
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

                case "Xuất kho":
                    sortSQL += "IsSold " + sortOrder;
                    break;

                default:
                    sortSQL += "MaSanPham " + sortOrder;
                    break;
            }

            IQueryable<SanPham> sql = null;

            if (sortColumn != "Ngày hết hạn")
            {
                sql = GetQuery(text, idGroup, isHavePrice, status, isExpired, warningDays).OrderBy(sortSQL);
            }
            else
            {
                if (sortOrder == CommonDao.SORT_ASCENDING)
                {
                    sql = GetQuery(text, idGroup, isHavePrice, status, isExpired, warningDays).OrderBy(
                        p => p.ThoiHan.Value != 0 &&
                        p.DonViThoiHan == CommonDao.DEFAULT_TYPE_DAY ?
                        p.CreateDate.AddDays(p.ThoiHan.Value) : p.DonViThoiHan == CommonDao.DEFAULT_TYPE_MONTH ?
                        p.CreateDate.AddMonths(p.ThoiHan.Value) : p.CreateDate.AddYears(p.ThoiHan.Value));
                }
                else
                {
                    sql = GetQuery(text, idGroup, isHavePrice, status, isExpired, warningDays).OrderByDescending(
                        p => p.ThoiHan.Value != 0 &&
                        p.DonViThoiHan == CommonDao.DEFAULT_TYPE_DAY ?
                        p.CreateDate.AddDays(p.ThoiHan.Value) : p.DonViThoiHan == CommonDao.DEFAULT_TYPE_MONTH ?
                        p.CreateDate.AddMonths(p.ThoiHan.Value) : p.CreateDate.AddYears(p.ThoiHan.Value));
                }
            }

            if ((skip <= 0 && take <= 0) || (skip < 0 && take > 0) || (skip > 0 && take < 0))
            {
                return sql.ToList();
            }

            return sql.Skip(skip).Take(take).ToList();
        }

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
            return dbContext.SanPhams.Where(p => p.Id == id).FirstOrDefault<SanPham>();
        }

        public static bool Insert(SanPham data, User user)
        {
            try
            {
                data.CreateBy = data.UpdateBy = user.UserName;
                data.CreateDate = data.UpdateDate = DateTime.Now;

                dbContext.SanPhams.InsertOnSubmit(data);
                dbContext.SubmitChanges();

                return true;
            }
            catch
            {
                return false;
            }
        }

        public static bool Delete(SanPham data, User user)
        {
            try
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
                                data.UpdateBy = user.UserName;
                                data.UpdateDate = DateTime.Now;

                                objDb.DeleteFlag = true;
                                dbContext.SubmitChanges();

                                return true;
                            }
                        }
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
                            SanPham data = GetById(result);

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

        public static bool Update(SanPham data, User user)
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
