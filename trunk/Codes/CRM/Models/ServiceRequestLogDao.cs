using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CRM.Library.Common;
using CRM.Models;

namespace CRM.Models
{
    public class ServiceRequestLogDao : BaseDao
    {
        private CommonLogDao commonDao = new CommonLogDao();
        #region Methods

        /// <summary>
        /// Write Log For Employee
        /// </summary>
        /// <param name="oldInfo"></param>
        /// <param name="newInfo"></param>
        /// <param name="action"></param>
        public void WriteLogForSR(SR_ServiceRequest oldInfo, SR_ServiceRequest newInfo, ELogAction action)
        {

            try
            {
                if (newInfo == null)
                {
                    return;
                }              
                
                MasterLog objMasterLog = new MasterLog();

                string logId = commonDao.UniqueId;
                string[] colNameArr = new string[] { "CategoryID", "Title", "Description", 
                            "SubmitUser", "RequestUser", "CCList", "UrgencyID", "Files", "OldId","StatusID", "AssignUser", "DueDate"};
                string[] colDisplayNameArr = new string[] { "Category", "Title", "Description", 
                            "Submit User", "Request User", "CCList", "Urgency", "Attachments", "Related SR","Status", "Assign User", "Due Date"};
                string[] oldValueArr = null;
                if (oldInfo != null)
                {
                    oldValueArr = new string[] { oldInfo.CategoryID.ToString(), CommonFunc.SubStringRoundWord(oldInfo.Title, 100),
                            CommonFunc.SubStringRoundWord(oldInfo.Description, 100), oldInfo.SubmitUser, 
                            oldInfo.RequestUser, oldInfo.CCList, oldInfo.UrgencyID.ToString(), 
                            oldInfo.Files, oldInfo.OldId.HasValue ? oldInfo.OldId.Value.ToString() : null, 
                            oldInfo.StatusID.ToString(), oldInfo.AssignUser, 
                            oldInfo.DueDate.HasValue ? oldInfo.DueDate.Value.ToString(Constants.DATETIME_FORMAT_FULL) : null};
                }
                string[] newValueArr = new string[] { newInfo.CategoryID.ToString(), CommonFunc.SubStringRoundWord(newInfo.Title, 100),
                            CommonFunc.SubStringRoundWord(newInfo.Description, 100), newInfo.SubmitUser, newInfo.RequestUser, 
                            newInfo.CCList, newInfo.UrgencyID.ToString(), 
                            newInfo.Files, newInfo.OldId.HasValue ? newInfo.OldId.Value.ToString() : null, 
                            newInfo.StatusID.ToString(), newInfo.AssignUser, 
                            newInfo.DueDate.HasValue ? newInfo.DueDate.Value.ToString(Constants.DATETIME_FORMAT_FULL) : null};
                switch (action)
                {
                    
                    case ELogAction.Delete:
                        // Insert to Master Log
                        commonDao.InsertMasterLog(logId, newInfo.UpdatedBy, ELogTable.ServiceRequest.ToString(), action.ToString());
                        // Write Delete Log
                        string key = newInfo.ID + " [" + newInfo.Title + "]";
                        commonDao.InsertLogDetail(logId, "ID", "Key for Delete", key, null);
                        break;
                    case ELogAction.Insert:
                        commonDao.InsertMasterLog(logId, newInfo.UpdatedBy, ELogTable.ServiceRequest.ToString(), action.ToString(), LogType.ServiceRequest);
                        commonDao.InsertLogDetail(logId, "ID", "Key for Insert", null, newInfo.ID.ToString());
                        for (int i = 0; i < colNameArr.Length; i++)
                            commonDao.InsertLogDetail(logId, colNameArr[i], colDisplayNameArr[i], null, newValueArr[i]);
                        break;
                    case ELogAction.Update:
                        commonDao.InsertMasterLog(logId, newInfo.UpdatedBy, ELogTable.ServiceRequest.ToString(), 
                            action.ToString(), LogType.ServiceRequest);
                        bool isChanged = false;
                        for (int i = 0; i < colNameArr.Length; i++)
                        {
                            if (oldValueArr[i] != newValueArr[i])
                            {
                                commonDao.InsertLogDetail(logId, colNameArr[i], colDisplayNameArr[i],
                                    oldValueArr[i], newValueArr[i]);
                                isChanged = true;
                            }
                        }
                        if(isChanged)
                            commonDao.InsertLogDetail(logId, "ID", "Key for Update", oldInfo.ID.ToString() + "[" + newInfo.UpdatedBy + "]", null);
                        break;
                    default:
                        break;
                        
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        #endregion
    }
}
