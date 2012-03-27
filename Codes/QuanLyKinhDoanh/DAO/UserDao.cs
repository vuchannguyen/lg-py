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
    public class UserDao: SQLConnection
    {
        public static IQueryable<User> GetQuery(string text)
        {
            var sql = from data in dbContext.Users
                      select data;

            if (!string.IsNullOrEmpty(text))
            {
                text = CommonDao.GetFilterText(text);
                sql = sql.Where(p => SqlMethods.Like(p.Ten, text) ||
                    SqlMethods.Like(p.UserName, text) ||
                    SqlMethods.Like(p.Email, text)
                    );
            }

            //if (ConvertUtil.ConvertToInt(examQuestionId) != 0)
            //{
            //    sql = sql.Where(p => p.ExamQuestionID == ConvertUtil.ConvertToInt(examQuestionId));
            //}

            //if (examDateFrom != null)
            //{
            //    sql = sql.Where(p => p.ExamDate >= examDateFrom);
            //}

            //if (examDateTo != null)
            //{
            //    sql = sql.Where(p => p.ExamDate <= examDateTo);
            //}

            sql = sql.Where(p => p.DeleteFlag == false);

            return sql;
        }

        public static int GetCount(string text)
        {
            return GetQuery(text).Count();
        }

        public static List<User> GetList(string text,
            string sortColumn, string sortOrder, int skip, int take)
        {
            string sortSQL = string.Empty;

            switch (sortColumn)
            {
                case "chTen":
                    sortSQL += "Ten " + sortOrder;
                    break;

                //case "ExamQuestion":
                //    sortSQL += "LOT_ExamQuestion.Title " + sortOrder;
                //    break;

                //case "ExamDate":
                //    sortSQL += "ExamDate " + sortOrder;
                //    break;

                //case "ExamType":
                //    sortSQL += "ExamType " + sortOrder;
                //    break;

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

        public static User GetById(int id)
        {
            return dbContext.Users.Where(p => p.Id == id).SingleOrDefault<User>();
        }

        public static User GetByUserName(string text)
        {
            return dbContext.Users.Where(p => p.UserName == text).SingleOrDefault<User>();
        }

        public static bool Insert(User data)
        {
            try
            {
                if (GetByUserName(data.UserName) != null)
                {
                    return false;
                }

                dbContext.Users.InsertOnSubmit(data);
                dbContext.SubmitChanges();

                return true;
            }
            catch
            {
                return false;
            }
        }

        public static bool Delete(User data)
        {
            if (data != null)
            {
                data.DeleteFlag = true;
                dbContext.SubmitChanges();

                return true;
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
                            User data = GetById(result);

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
    }
}
