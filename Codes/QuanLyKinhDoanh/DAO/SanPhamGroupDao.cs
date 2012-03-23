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
    public class SanPhamGroupDao : SQLConnection
    {
        public IQueryable<SanPhamGroup> GetQuery(string text)
        {
            var sql = from data in dbContext.SanPhamGroups
                      select data;

            if (!string.IsNullOrEmpty(text))
            {
                text = CommonDao.GetFilterText(text);
                sql = sql.Where(p => SqlMethods.Like(p.Ten, text) ||
                    SqlMethods.Like(p.Mota, text)
                    );
            }

            return sql;
        }

        public int GetCount(string text)
        {
            return GetQuery(text).Count();
        }

        public List<SanPhamGroup> GetList(string text,
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

        public SanPhamGroup GetById(string id)
        {
            return dbContext.SanPhamGroups.Where(p => p.Id == id).SingleOrDefault<SanPhamGroup>();
        }

        public bool Insert(SanPhamGroup data)
        {
            try
            {
                dbContext.SanPhamGroups.InsertOnSubmit(data);
                dbContext.SubmitChanges();

                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool Delete(SanPhamGroup data)
        {
            if (data != null)
            {
                SanPhamGroup objDb = GetById(data.Id);

                if (objDb != null)
                {
                    dbContext.SanPhamGroups.DeleteOnSubmit(objDb);

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

                    foreach (string id in idArr)
                    {
                        if (!string.IsNullOrEmpty(id))
                        {
                            SanPhamGroup data = GetById(id);

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

        public bool Update(SanPhamGroup data)
        {
            try
            {
                if (data != null)
                {
                    SanPhamGroup objDb = GetById(data.Id);

                    objDb.Ten = data.Ten;
                    objDb.Mota = data.Mota;

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
