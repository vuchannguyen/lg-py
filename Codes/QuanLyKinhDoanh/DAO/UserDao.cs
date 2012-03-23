﻿using System;
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
        public IQueryable<User> GetQuery(string text)
        {
            var sql = from data in dbContext.Users
                      select data;

            if (!string.IsNullOrEmpty(text))
            {
                text = CommonDao.GetFilterText(text);
                sql = sql.Where(p => SqlMethods.Like(p.Ten, text) ||
                    SqlMethods.Like(p.Email, text) ||
                    SqlMethods.Like(p.DienThoai, text)
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

        public int GetCount(string text)
        {
            return GetQuery(text).Count();
        }

        public List<User> GetList(string text,
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

        public User GetById(int id)
        {
            return dbContext.Users.Where(p => p.Id == id).SingleOrDefault<User>();
        }

        public bool Insert(User data)
        {
            try
            {
                dbContext.Users.InsertOnSubmit(data);
                dbContext.SubmitChanges();

                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool Delete(User data)
        {
            if (data != null)
            {
                User objDb = GetById(data.Id);

                if (objDb != null)
                {
                    objDb.DeleteFlag = true;
                    dbContext.SubmitChanges();

                    return true;
                }
            }

            return false;
        }

        public bool DeleteList(string ids)
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

        public bool Update(User data)
        {
            try
            {
                if (data != null)
                {
                    User objDb = GetById(data.Id);

                    objDb.Ten = data.Ten;
                    objDb.IdGroup = data.IdGroup;
                    objDb.MatKhau = data.MatKhau;
                    objDb.GioiTinh = data.GioiTinh;
                    objDb.CMND = data.CMND;
                    objDb.DienThoai = data.DienThoai;
                    objDb.Email = data.Email;
                    objDb.GhiChu = data.GhiChu;

                    objDb.CreateBy = data.CreateBy;
                    objDb.CreateDate = data.CreateDate;
                    objDb.UpdateBy = data.UpdateBy;
                    objDb.UpdateDate = data.UpdateDate;
                    
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
