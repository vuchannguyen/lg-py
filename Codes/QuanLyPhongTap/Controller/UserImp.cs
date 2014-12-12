using System;
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
    public class UserImp : SqlConnection
    {
        public static User currentUser;

        private static IQueryable<User> GetQuery(string text)
        {
            var sql = from data in dbContext.Users
                      select data;

            if (!string.IsNullOrEmpty(text))
            {
                text = CommonFunction.GetFilterText(text);
                sql = sql.Where(p => SqlMethods.Like(p.Ten, text) ||
                    SqlMethods.Like(p.UserName, text)
                    );
            }

            sql = sql.Where(p => p.DeleteFlag == false);
            return sql;
        }

        public static int GetCount(string text = "")
        {
            return GetQuery(text).Count();
        }

        public static List<User> GetList(string text = "",
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

        public static List<User> GetListByIdGroup(int idGroup)
        {
            return dbContext.Users.Where(p => p.IdGroup == idGroup && p.DeleteFlag == false).ToList();
        }

        public static List<User> GetListByGroupName(string text)
        {
            return dbContext.Users.Where(p => p.UserGroup.Ten.ToLower() == text.ToLower() && p.DeleteFlag == false).ToList();
        }

        public static User GetById(int id)
        {
            return dbContext.Users.Where(p => p.Id == id).FirstOrDefault<User>();
        }

        public static User GetByUserName(string text)
        {
            return dbContext.Users.Where(p => p.UserName == text && p.DeleteFlag == false).FirstOrDefault<User>();
        }

        private static bool Insert(User data)
        {
            try
            {
                dbContext.Users.InsertOnSubmit(data);
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
        public static int? Insert(byte idGroup, string ten, string userName, string Password, string gioiTinh = "Nam",
            DateTime? DOB = null, DateTime? ngayCap = null, string ma = "", string CMND = "", string noiCap = "",
            string diaChi = "", string dienThoai = "", string DTDD = "", string email = "", string ghiChu = "")
        {
            int? res = null;

            try
            {
                if (GetByUserName(userName) == null)
                {
                    User data = new User();
                    data.IdGroup = idGroup;
                    data.Ten = ten;
                    data.UserName = userName;
                    data.Password = Crypto.EncryptText(Password);
                    data.GioiTinh = gioiTinh;
                    data.DOB = DOB;
                    data.NgayCap = ngayCap;
                    data.CMND = CMND;
                    data.NoiCap = noiCap;
                    data.DiaChi = diaChi;
                    data.DienThoai = dienThoai;
                    data.DTDD = DTDD;
                    data.Email = email;
                    data.GhiChu = ghiChu;

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

        public static bool Delete(User data)
        {
            try
            {
                if (data != null && CheckDeletePermission(data))
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

        public static bool CheckDeletePermission(User data)
        {
            bool res = true;

            if (data.Id == currentUser.Id)
            {
                res = false;
            }
            else if (data.UserGroup.Ten.ToLower() == CommonConstants.DEFAULT_USER_GROUP_ADMIN_NAME &&
                currentUser.UserGroup.Ten.ToLower() == CommonConstants.DEFAULT_USER_GROUP_ADMIN_NAME)
            {
                res = false;
            }

            return res;
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
                            User data = GetById(result);

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

        public static bool Update(User data)
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

        public static bool Update(int id, byte idUserGroup, string ten, string userName, string Password, string gioiTinh = "Nam",
            DateTime? DOB = null, DateTime? ngayCap = null, string CMND = "", string noiCap = "",
            string diaChi = "", string dienThoai = "", string DTDD = "", string email = "", string ghiChu = "")
        {
            bool res = false;

            try
            {
                User data = GetById(id);

                if (data != null)
                {
                    data.UserGroup = UserGroupImp.GetById(idUserGroup);
                    data.Ten = ten;
                    data.UserName = userName;
                    data.Password = Crypto.EncryptText(Password);
                    data.GioiTinh = gioiTinh;
                    data.DOB = DOB;
                    data.NgayCap = ngayCap;
                    data.CMND = CMND;
                    data.NgayCap = ngayCap;
                    data.NoiCap = noiCap;
                    data.DiaChi = diaChi;
                    data.DienThoai = dienThoai;
                    data.DTDD = DTDD;
                    data.Email = email;
                    data.GhiChu = ghiChu;

                    res = Update(data);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return res;
        }

        public static bool Login(string username, string password)
        {
            bool res = false;

            try
            {
                currentUser = GetByUserName(username);

                if (currentUser != null)
                {
                    string s = Crypto.EncryptText(password);

                    if (currentUser.Password == Crypto.EncryptText(password))
                    {
                        res = true;
                    }
                    else
                    {
                        currentUser = null;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return res;
        }

        public static void CheckAndCreateDefaultUserGroup()
        {
            try
            {
                if (UserGroupImp.GetCount(CommonConstants.DEFAULT_USER_GROUP_ADMIN_NAME) > 0 &&
                    GetByUserName(CommonConstants.DEFAULT_USER_GROUP_ADMIN_NAME) == null)
                {
                    Insert(UserGroupImp.GetList(CommonConstants.DEFAULT_USER_GROUP_ADMIN_NAME)[0].Id,
                        CommonConstants.DEFAULT_USER_ADMIN_NAME, CommonConstants.DEFAULT_USER_ADMIN_NAME,
                        CommonConstants.DEFAULT_USER_ADMIN_PASSWORD);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
