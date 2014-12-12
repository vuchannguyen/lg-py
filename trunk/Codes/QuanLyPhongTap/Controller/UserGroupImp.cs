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

namespace Controller
{
    public class UserGroupImp : SqlConnection
    {
        private static IQueryable<UserGroup> GetQuery(string text)
        {
            var sql = from data in dbContext.UserGroups
                      select data;

            if (!string.IsNullOrEmpty(text))
            {
                text = CommonFunction.GetFilterText(text);
                sql = sql.Where(p => SqlMethods.Like(p.Ten, text) ||
                    SqlMethods.Like(p.GhiChu, text)
                    );
            }

            return sql;
        }

        public static int GetCount(string text = "")
        {
            return GetQuery(text).Count();
        }

        public static List<UserGroup> GetList(string text = "",
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

        public static UserGroup GetById(int id)
        {
            return dbContext.UserGroups.Where(p => p.Id == id).FirstOrDefault<UserGroup>();
        }

        private static bool Insert(UserGroup data)
        {
            try
            {
                dbContext.UserGroups.InsertOnSubmit(data);
                dbContext.SubmitChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Insert new data
        /// </summary>
        /// <param name="ten"></param>
        /// <param name="ghiChu"></param>
        /// <returns>Return id of the new data if success</returns>
        public static int? Insert(string ten, string ghiChu = "")
        {
            int? res = null;

            try
            {
                UserGroup data = new UserGroup();
                data.Ten = ten;
                data.GhiChu = ghiChu;

                if (Insert(data))
                {
                    res = data.Id;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return res;
        }

        public static bool Delete(UserGroup data)
        {
            try
            {
                if (data != null)
                {
                    dbContext.UserGroups.DeleteOnSubmit(data);
                    dbContext.SubmitChanges();
                    return true;
                }

                return false;
            }
            catch
            {
                //log
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
                            UserGroup data = GetById(result);

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

        public static bool Update(UserGroup data)
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

        public static bool Update(int id, string ten, string ghiChu = "")
        {
            bool res = false;

            try
            {
                UserGroup data = GetById(id);

                if (data != null)
                {
                    data.Ten = ten;
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

        public static void CheckAndCreateDefaultUserGroup()
        {
            try
            {
                if (GetCount(CommonConstants.DEFAULT_USER_GROUP_ADMIN_NAME) < 1)
                {
                    Insert(CommonConstants.DEFAULT_USER_GROUP_ADMIN_NAME);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
