﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Dynamic;
using System.Data.Linq.SqlClient;
using System.Data.Common;
using Controller.Common;
using Model;
using CryptoFunction;

namespace Controller
{
    public class KhachHangImp : SqlConnection
    {
        private static IQueryable<KhachHang> GetQuery(string text)
        {
            var sql = from data in dbContext.KhachHangs
                      select data;

            if (!string.IsNullOrEmpty(text))
            {
                text = CommonFunction.GetFilterText(text);
                sql = sql.Where(p => SqlMethods.Like(p.Ten, text) ||
                    SqlMethods.Like(p.Ma, text) ||
                    SqlMethods.Like(p.DTDD, text) ||
                    SqlMethods.Like(p.Email, text)
                    );
            }

            sql = sql.Where(p => p.DeleteFlag == false);
            return sql;
        }

        public static int GetCount(string text = "")
        {
            return GetQuery(text).Count();
        }

        public static List<KhachHang> GetList(string text = "",
            string sortColumn = "", string sortOrder = CommonConstants.SORT_ASCENDING, int skip = 0, int take = 0)
        {
            string sortSQL = string.Empty;

            switch (sortColumn)
            {
                case "Id":
                    sortSQL += "Id " + sortOrder;
                    break;

                default:
                    sortSQL += "Ten " + sortOrder;
                    break;
            }

            var sql = GetQuery(text).OrderBy(sortSQL);

            if ((skip <= 0 && take <= 0) || (skip < 0 && take > 0) || (skip > 0 && take < 0))
            {
                return sql.ToList();
            }

            return sql.Skip(skip).Take(take).ToList();
        }

        public static List<KhachHang> GetListByIdGroup(int idGroup)
        {
            return dbContext.KhachHangs.Where(p => p.IdGroup == idGroup && p.DeleteFlag == false).ToList();
        }

        public static List<KhachHang> GetListByGroupName(string text)
        {
            return dbContext.KhachHangs.Where(p => p.KhachHangGroup.Ten.ToLower() == text.ToLower() && p.DeleteFlag == false).ToList();
        }

        public static KhachHang GetById(int id)
        {
            return dbContext.KhachHangs.Where(p => p.Id == id).FirstOrDefault<KhachHang>();
        }

        public static KhachHang GetByMa(string ma)
        {
            return dbContext.KhachHangs.Where(p => p.Ma == ma).FirstOrDefault<KhachHang>();
        }

        private static bool Insert(KhachHang data)
        {
            try
            {
                dbContext.KhachHangs.InsertOnSubmit(data);
                dbContext.SubmitChanges();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Insert new data
        /// </summary>
        /// <returns>Return id of the new data if success</returns>
        public static int? Insert(byte idGroup, string ten, string KhachHangName, string gioiTinh = "Nam", string ma = "",
            DateTime? ngayHetHan = null, string soXe = "", double dien = 0, double nuoc = 0,
            DateTime? DOB = null, string CMND = "", DateTime? ngayCap = null, string noiCap = "",
            string diaChi = "", string dtdd = "", string email = "", string ghiChu = "")
        {
            int? res = null;

            try
            {
                if (GetByMa(ma) == null)
                {
                    KhachHang data = new KhachHang();
                    data.Ma = ma;
                    data.IdGroup = idGroup;
                    data.Ten = ten;
                    data.GioiTinh = gioiTinh;
                    data.NgayHetHan = ngayHetHan;
                    data.SoXe = soXe;
                    data.Dien = dien;
                    data.Nuoc = nuoc;
                    data.DOB = DOB;
                    data.CMND = CMND;
                    data.NgayCap = ngayCap;
                    data.NoiCap = noiCap;
                    data.DiaChi = diaChi;
                    data.DTDD = dtdd;
                    data.Email = email;
                    data.GhiChu = ghiChu;
                    data.UpdateDate = DateTime.Now;
                    data.UpdateBy = UserImp.currentUser.Id;

                    if (Insert(data))
                    {
                        res = data.Id;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return res;
        }

        public static bool Delete(KhachHang data)
        {
            try
            {
                if (data != null)
                {
                    data.DeleteFlag = true;
                    dbContext.SubmitChanges();
                    return true;
                }
            }
            catch
            {

            }

            NewConnection();
            return false;
        }

        public static bool DeleteList(string ids)
        {
            bool isSuccess = true;

            try
            {
                OpenConnection();

                if (!string.IsNullOrEmpty(ids))
                {
                    string[] idArr = ids.Split(new string[] { CommonConstants.DELIMITER_STRING }, StringSplitOptions.RemoveEmptyEntries);
                    int result = 0;

                    foreach (string id in idArr)
                    {
                        if (int.TryParse(id, out result))
                        {
                            KhachHang data = GetById(result);

                            if (!Delete(data))
                            {
                                isSuccess = false;
                                break;
                            }
                        }
                        else
                        {
                            isSuccess = false;
                            break;
                        }
                    }
                }
                else
                {
                    isSuccess = false;
                }
            }
            catch
            {
                isSuccess = false;
            }

            CloseConnection(isSuccess);
            return isSuccess;
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

        public static bool Update(int id, byte idGroup, string ten, string KhachHangName, string gioiTinh = "Nam", string ma = "",
            DateTime? ngayHetHan = null, string soXe = "", double dien = 0, double nuoc = 0,
            DateTime? DOB = null, string CMND = "", DateTime? ngayCap = null, string noiCap = "",
            string diaChi = "", string dtdd = "", string email = "", string ghiChu = "")
        {
            bool res = false;

            try
            {
                KhachHang data = GetById(id);

                if (data != null)
                {
                    data.KhachHangGroup = KhachHangGroupImp.GetById(idGroup);
                    data.Ma = ma;
                    data.IdGroup = idGroup;
                    data.Ten = ten;
                    data.GioiTinh = gioiTinh;
                    data.NgayHetHan = ngayHetHan;
                    data.SoXe = soXe;
                    data.Dien = dien;
                    data.Nuoc = nuoc;
                    data.DOB = DOB;
                    data.CMND = CMND;
                    data.NgayCap = ngayCap;
                    data.NoiCap = noiCap;
                    data.DiaChi = diaChi;
                    data.DTDD = dtdd;
                    data.Email = email;
                    data.GhiChu = ghiChu;
                    data.UpdateDate = DateTime.Now;
                    data.User = UserImp.currentUser;

                    res = Update(data);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return res;
        }
    }
}
