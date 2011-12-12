using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Web;
using System.Data.Linq;
using System.Data.Common;
using CRM.Library.Common;

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
        public List<sp_LogMasterResult> GetList(string searchUsername, string searchDate, int? type = null)
        {    
            return dbContext.sp_LogMaster(searchUsername, searchDate, type).ToList<sp_LogMasterResult>();    
        }

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
        public List<sp_LogMasterResult> Sort(List<sp_LogMasterResult> list, string sortColumn, string sortOrder)
        {
            int order;

            if (sortOrder == "desc")
            {
                order = -1;
            }
            else
            {
                order = 1;
            }
            switch (sortColumn)
            {
                case "Date":
                    list.Sort(
                         delegate(sp_LogMasterResult m1, sp_LogMasterResult m2)
                         { return m1.LogDate.Value.CompareTo(m2.LogDate.Value) * order; });
                    break;
                case "UserName":
                    list.Sort(
                         delegate(sp_LogMasterResult m1, sp_LogMasterResult m2)
                         { return m1.UserName.CompareTo(m2.UserName) * order; });
                    break;
            }
            return list;
        }
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

    }
}