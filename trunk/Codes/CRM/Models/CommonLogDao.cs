using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CRM.Library.Common;
namespace CRM.Models
{
    public class CommonLogDao : BaseDao
    {
        #region Public Methods

        public string UniqueId
        {
            get { return new Guid(System.Guid.NewGuid().ToString()).ToString(); }
        }

        /// <summary>
        /// Delete master log by logid
        /// </summary>
        /// <param name="logId"></param>
        public void DeleteMasterLog(string logId)
        {
            try
            {
                if (!string.IsNullOrEmpty(logId))
                {                           
                    
                    MasterLog info = dbContext.MasterLogs.Where(m => m.LogId == logId).FirstOrDefault<MasterLog>();
                    if (info != null)
                    {
                        dbContext.MasterLogs.DeleteOnSubmit(info);
                        dbContext.SubmitChanges();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Insert Master Log
        /// </summary>
        /// <param name="logId"></param>
        /// <param name="username"></param>
        /// <param name="tableName"></param>
        /// <param name="actionName"></param>
        public void InsertMasterLog(string logId, string username, string tableName, string actionName, LogType logtype = LogType.Other)
        {
            try
            {
                if ((logId != null) && (!string.Empty.Equals(username)))
                {
                    // Set info
                    MasterLog objInfo = new MasterLog();
                    objInfo.LogId = logId;
                    objInfo.Username = username;
                    objInfo.LogDate = DateTime.Now;
                    objInfo.TableName = tableName;
                    objInfo.ActionName = actionName;
                    if (logtype != LogType.Other)
                        objInfo.LogType = (int)logtype;
                    // Insert to DB
                    dbContext.MasterLogs.InsertOnSubmit(objInfo);
                    dbContext.SubmitChanges();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Insert Log Detail
        /// </summary>
        /// <param name="logId"></param>
        /// <param name="colName"></param>
        /// <param name="disColName"></param>
        /// <param name="oldValue"></param>
        /// <param name="newValue"></param>
        public void InsertLogDetail(string logId, string colName, string disColName, string oldValue, string newValue)
        {
            try
            {
                if ((logId != null) && (!string.Empty.Equals(colName)))
                {
                    // Set info
                    LogDetail objInfo = new LogDetail();
                    objInfo.DetailId = UniqueId;
                    objInfo.LogId = logId;
                    objInfo.ColumnName = colName;
                    objInfo.DisplayColumnName = disColName;
                    objInfo.OldValue = oldValue;
                    objInfo.NewValue = newValue;

                    // Insert to DB
                    dbContext.LogDetails.InsertOnSubmit(objInfo);
                    dbContext.SubmitChanges();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Compare String Values
        /// </summary>
        /// <param name="str1"></param>
        /// <param name="str2"></param>
        /// <returns></returns>
        public bool IsStringValuesEqual(string str1, string str2)
        {
            try
            {
                if (str1 == null)
                {
                    str1 = string.Empty;
                }
                if (str2 == null)
                {
                    str2 = string.Empty;
                }
                if (str1.Length != str2.Length)
                {
                    return false;
                }
                string[] tmpStr1 = str1.Split(',');

                foreach (string str in tmpStr1)
                {
                    if (!str2.Contains(str))
                    {
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return true;
        }

        #endregion
        
    }
}