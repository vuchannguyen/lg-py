using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Web;
using System.Data.Linq;
using System.Data.Common;
using CRM.Library.Common;
using System.Linq.Dynamic;
using CRM.Models.Entities;


namespace CRM.Models
{
    public class LogDao : BaseDao
    {              

        #region Public methods
        /// <summary>
        /// Get List
        /// </summary>
        /// <param name="groupId"></param>
        /// <returns></returns>
        //public List<sp_LogMasterResult> GetList(string searchUsername, string searchDate, int? type = null)
        //{    
        //    return dbContext.sp_LogMaster(searchUsername, searchDate, type).ToList<sp_LogMasterResult>();    
        //}

        #region  //Change User Log by LinQ
        // LINQ Query
        public IQueryable<MasterLogEntity> GetQueryList(string searchUsername, string searchDate, int? type = null)
        {
            var sql = (from log in dbContext.MasterLogs
                       select new MasterLogEntity()
                       {
                           UserName = log.Username,
                           LogDate = new DateTime(log.LogDate.Year, log.LogDate.Month, log.LogDate.Day),
                           LogType = log.LogType,

                       }).Distinct();

            if (!string.IsNullOrEmpty(searchUsername))
            {
                sql = sql.Where(p => p.UserName.Contains(searchUsername));
            }
            if (!string.IsNullOrEmpty(searchDate))
            {
                sql = sql.Where(p => p.LogDate.Date == ConvertUtil.ConvertToDatetime(searchDate));
            }
            if (type == null)
                sql = sql.Where(p => p.LogType == null);
            else
                sql = sql.Where(p => p.LogType == type);
            return sql;
        }

        public int GetCountListLogsMasterLinq(string searchUsername, string searchDate, int? type = null)
        {
            var sql = GetQueryList(searchUsername, searchDate ,type);

            return sql.Count();
        }

        public List<MasterLogEntity> GetListLogMasterLinq(string searchUsername, string searchDate, string sortColumn, string sortOrder, int skip, int take, int? type = null)
        {
            var sql = GetQueryList(searchUsername, searchDate, type);

            switch (sortColumn)
            {
                case "UserName":
                    sql = sql.OrderBy("Username" + " " + sortOrder);
                    break;

                case "Date":
                    sql = sql.OrderBy("LogDate" + " " + sortOrder);
                    break;

                default:
                    sql = sql.OrderBy(sortColumn + " " + sortOrder);
                    break;
            }
            return sql.Skip(skip).Take(take).ToList();

        }
        #endregion
        /// <summary>
        /// Get List
        /// </summary>
        /// <param name="groupId"></param>
        /// <returns></returns>
        public List<sp_LogMasterGroupResult> GetDetailOnList(string username, DateTime date, int? type = null)
        {
            return dbContext.sp_LogMasterGroup(username, date, type).ToList<sp_LogMasterGroupResult>();

        }
        
        /// <summary>
        /// Get List Log Detail
        /// </summary>
        /// <param name="groupId"></param>
        /// <returns></returns>
        public List<sp_LogDetailResult> GetLogDetail(string logId)
        {
            return dbContext.sp_LogDetail(logId).ToList<sp_LogDetailResult>();

        }

        /// <summary>
        /// Get List Log Detail Group
        /// </summary>
        /// <param name="groupId"></param>
        /// <returns></returns>
        public List<sp_LogDetailGroupResult> GetLogDetailGroup(string username, DateTime date, string action,string table, int? type)
        {

            return dbContext.sp_LogDetailGroup(username, date, action, table, type).ToList<sp_LogDetailGroupResult>();
           
        }

        /// <summary>
        /// Sort Emplooyee
        /// </summary>
        /// <param name="list"></param>
        /// <param name="sortColumn"></param>
        /// <param name="sortOrder"></param>
        /// <returns></returns>
      

        #endregion

        #region clear logs
        /// <summary>
        /// tan.tran 2011.05.25
        /// </summary>
        /// <param name="month"></param>
        /// <returns></returns>
        public Message DeleteList(int month)
        {
            Message msg = null;
            DbTransaction trans = null;
            long number = 0;
            try
            {
                dbContext.Connection.Open();
                trans = dbContext.Connection.BeginTransaction();
                dbContext.Transaction = trans;

                List<MasterLog> listMaster = dbContext.MasterLogs.Where(p => p.LogDate.AddMonths(month) <= DateTime.Now).ToList<MasterLog>();

                foreach (MasterLog mas in listMaster)
                {
                    List<LogDetail> listDetail = dbContext.LogDetails.Where(p => p.MasterLog.LogId == mas.LogId).ToList<LogDetail>();
                    foreach (LogDetail det in listDetail)
                    {
                        dbContext.LogDetails.DeleteOnSubmit(det);
                        number++;
                    }
                    dbContext.MasterLogs.DeleteOnSubmit(mas);
                    number++;
                }
                dbContext.SubmitChanges();

                // Show succes message
                if (number > 0)
                {
                    msg = new Message(MessageConstants.I0001, MessageType.Info, number.ToString() + " log(s)", "deleted");
                }
                else
                {
                    msg = new Message(MessageConstants.I0010, MessageType.Info);
                }
                trans.Commit();
            }
            catch (Exception)
            {
                if (trans != null) trans.Rollback();
                // Show system error
                msg = new Message(MessageConstants.E0007, MessageType.Error);
            }
            return msg;
        }
        #endregion

        /// <summary>
        /// Get logs by type (Service Request, Purchase Request...) and object id
        /// </summary>
        /// <param name="logType">The log type (Job Request, Purchase Request, Service Request)</param>
        /// <param name="id">The ID of the object</param>
        /// <returns></returns>
        public List<MasterLog> GetLogsByTypeAndKeyID(LogType logType, string id)
        {
            var masterLogIds = dbContext.LogDetails.Where(p => dbContext.MasterLogs.
                Where(t => t.LogType == (int)LogType.ServiceRequest).Select(r => r.LogId).Contains(p.LogId) &&
                p.ColumnName == "ID" && (p.OldValue.Remove(p.OldValue.IndexOf('[')) == id || p.NewValue == id))
                .Select(p=>p.LogId);
            if(masterLogIds.Count() == 0)
                return new List<MasterLog>();
            return dbContext.MasterLogs.Where(p=>masterLogIds.Contains(p.LogId)).OrderBy(p=>p.LogDate).ToList();
        }

        public MasterLog GetMasterLogById(string id)
        {
            return dbContext.MasterLogs.FirstOrDefault(p=>p.LogId == id);
        }


    }
};