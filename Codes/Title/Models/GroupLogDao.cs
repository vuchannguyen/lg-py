using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CRM.Library.Common;

namespace CRM.Models
{
    public class GroupLogDao : BaseDao
    {
        //
        // GET: /ContractlLogDao/
        private CommonLogDao commonDao = new CommonLogDao();
        private GroupDao dao = new GroupDao();
        /// <summary>
        /// Write Log For Employee
        /// </summary>
        /// <param name="oldInfo"></param>
        /// <param name="newInfo"></param>
        /// <param name="action"></param>
        public void WriteLogForGroup(Group oldInfo, Group newInfo, ELogAction action)
        {

            try
            {
                if (newInfo == null)
                {
                    return;
                }

                MasterLog objMasterLog = new MasterLog();

                string logId = commonDao.UniqueId;

                switch (action)
                {
                    case ELogAction.Insert:
                        // Insert to Master Log
                        commonDao.InsertMasterLog(logId, newInfo.CreatedBy, ELogTable.Group.ToString(), action.ToString());
                        // Write Insert Log
                        WriteInsertLogForGroup(newInfo, logId);
                        break;
                    case ELogAction.Update:
                        // Insert to Master Log
                        commonDao.InsertMasterLog(logId, newInfo.UpdatedBy, ELogTable.Group.ToString(), action.ToString());
                        // Write Update Log
                        bool isUpdated = WriteUpdateLogForGroup(newInfo, logId);
                        if (!isUpdated)
                        {
                            commonDao.DeleteMasterLog(logId);
                        }
                        break;
                    case ELogAction.Delete:
                        // Insert to Master Log
                        commonDao.InsertMasterLog(logId, newInfo.UpdatedBy, ELogTable.Group.ToString(), action.ToString());
                        // Write Delete Log
                        string key = "Group name '" + newInfo.GroupName + "'";
                        commonDao.InsertLogDetail(logId, "GroupId", "Key for Delete ", key, null);
                        break;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Write Insert Log For Group
        /// </summary>
        /// <param name="objInfo"></param>
        /// <param name="logId"></param>
        private void WriteInsertLogForGroup(Group objInfo, string logId)
        {
            try
            {
                if ((objInfo != null) && (logId != null))
                {
                    // Insert to Log Detail                       
                    commonDao.InsertLogDetail(logId, "GroupName", "Group Name", null, objInfo.GroupName);
                    if (!string.IsNullOrEmpty(objInfo.Description))
                    {
                        commonDao.InsertLogDetail(logId, "Description", "Description", null, objInfo.Description);
                    }
                    if (objInfo.DisplayOrder != null)
                    {
                        commonDao.InsertLogDetail(logId, "DisplayOrder", "Display Order", null, objInfo.DisplayOrder.Value.ToString());
                    }
                    commonDao.InsertLogDetail(logId, "IsActive", "Active", null, objInfo.IsActive.ToString());
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Write Update Log For Group
        /// </summary>
        /// <param name="newInfo"></param>
        /// <param name="logId"></param>
        /// <returns></returns>
        private bool WriteUpdateLogForGroup(Group newInfo, string logId)
        {
            bool isUpdated = false;
            try
            {
                // Get old info
                Group oldInfo = dao.GetById(newInfo.GroupId);

                if ((oldInfo != null) && (newInfo != null) && (logId != null))
                {
                    if (!newInfo.GroupName.Equals(oldInfo.GroupName))
                    {
                        commonDao.InsertLogDetail(logId, "GroupName", "Group Name", oldInfo.GroupName, newInfo.GroupName);
                        isUpdated = true;
                    }

                    if (newInfo.Description != oldInfo.Description)
                    {
                        commonDao.InsertLogDetail(logId, "Description", "Description", oldInfo.Description, newInfo.Description);
                        isUpdated = true;
                    }

                    if ((newInfo.DisplayOrder == null && oldInfo.DisplayOrder != null) || (newInfo.DisplayOrder != null && oldInfo.DisplayOrder == null) ||
                    (newInfo.DisplayOrder != null && oldInfo.DisplayOrder != null && newInfo.DisplayOrder.Value != oldInfo.DisplayOrder.Value)
                    )
                    {
                        commonDao.InsertLogDetail(logId, "DisplayOrder", "DisplayOrder", 
                            oldInfo.DisplayOrder.HasValue? oldInfo.DisplayOrder.Value.ToString() : string.Empty,
                            newInfo.DisplayOrder.HasValue? newInfo.DisplayOrder.Value.ToString() : string.Empty);
                        isUpdated = true;
                    }
                    if (newInfo.IsActive != oldInfo.IsActive)
                    {
                        commonDao.InsertLogDetail(logId, "IsActive", "IsActive", oldInfo.IsActive.ToString(), newInfo.IsActive.ToString());
                        isUpdated = true;
                    }
                    
                    if (isUpdated)
                    {
                        // Insert Key Name
                        string key = "Group name '" + oldInfo.GroupName+ "'";
                        commonDao.InsertLogDetail(logId, "GroupId", "Key for Update", key, null);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return isUpdated;
        }
    }
}
