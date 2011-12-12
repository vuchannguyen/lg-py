using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CRM.Library.Common;

namespace CRM.Models
{
    public class JRAdminLogDao : BaseDao
    {
        private CommonLogDao commonDao = new CommonLogDao();

        /// <summary>
        /// Write Log For Employee
        /// </summary>
        /// <param name="oldInfo"></param>
        /// <param name="newInfo"></param>
        /// <param name="action"></param>
        public void WriteLogForWorkflowAdmin(UserAdmin_WFRole oldInfo, UserAdmin_WFRole newInfo, ELogAction action)
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
                        commonDao.InsertMasterLog(logId, newInfo.CreatedBy, ELogTable.WorkFlowAdmin.ToString(), action.ToString());
                        // Write Insert Log
                        WriteInsertLogForWorkflowAdmin(newInfo, logId);
                        break;
                    case ELogAction.Update:
                        // Insert to Master Log
                        commonDao.InsertMasterLog(logId, newInfo.UpdatedBy, ELogTable.WorkFlowAdmin.ToString(), action.ToString());
                        // Write Update Log
                        bool isUpdated = WriteUpdateLogForWorkflowAdmin(newInfo, logId);
                        if (!isUpdated)
                        {
                            commonDao.DeleteMasterLog(logId);
                        }
                        break;
                    case ELogAction.Delete:
                        // Insert to Master Log
                        commonDao.InsertMasterLog(logId, newInfo.UpdatedBy, ELogTable.Employee.ToString(), action.ToString());
                        // Write Delete Log
                        string key = newInfo.ID + " [" + oldInfo.UserAdmin.UserName + "] with Role [" + oldInfo.WFRole.Name + "]";
                        commonDao.InsertLogDetail(logId, "ID", "Key for Delete", key, null);
                        break;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Write Insert Log Work Flow Admin
        /// </summary>
        /// <param name="objInfo"></param>
        /// <param name="logId"></param>
        private void WriteInsertLogForWorkflowAdmin(UserAdmin_WFRole objInfo, string logId)
        {
            try
            {
                if ((objInfo != null) && (logId != null))
                {
                    // Insert to Log Detail                       
                    commonDao.InsertLogDetail(logId, "UserAdminId", "UserName", null, objInfo.UserAdmin.UserName);
                    commonDao.InsertLogDetail(logId, "WFRoleID", "Role", null, objInfo.WFRole.Name);
                    commonDao.InsertLogDetail(logId, "IsActive", "Active", null, objInfo.IsActive == Constants.IS_ACTIVE ? "Yes":"No");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private bool WriteUpdateLogForWorkflowAdmin(UserAdmin_WFRole newInfo, string logId)
        {
            bool isUpdated = false;
            try
            {
                // Get old info
                UserAdmin_WFRole oldInfo = new JRAdminDao().GetById(newInfo.ID);

                if ((oldInfo != null) && (newInfo != null) && (logId != null))
                {
                    if (newInfo.UserAdminId != oldInfo.UserAdminId)
                    {
                        UserAdmin objUserName = new UserAdminDao().GetById(newInfo.UserAdminId);
                        if (objUserName != null)
                        {
                            commonDao.InsertLogDetail(logId, "UserAdminId", "User Name", oldInfo.UserAdmin.UserName, objUserName.UserName);
                            isUpdated = true;
                        }
                    }
                    if (newInfo.WFRoleID != oldInfo.WFRoleID)
                    {
                        WFRole obj = new RoleDao().GetByID(newInfo.WFRoleID);
                        if (obj != null)
                        {
                            commonDao.InsertLogDetail(logId, "WFRoleID", "Role", oldInfo.WFRole.Name, obj.Name);
                            isUpdated = true;
                        }
                    }
                    if (newInfo.IsActive != oldInfo.IsActive)
                    {
                        commonDao.InsertLogDetail(logId, "IsActive", "Active", oldInfo.IsActive == Constants.IS_ACTIVE ? "Yes" : "No", newInfo.IsActive == Constants.IS_ACTIVE ? "Yes" : "No");
                        isUpdated = true;
                    }                      
                    if (isUpdated)
                    {
                        // Insert Key Name
                        string key = newInfo.ID + " [" + oldInfo.UserAdmin.UserName + "] with Role [" + oldInfo.WFRole.Name + "]";
                        commonDao.InsertLogDetail(logId, "WorkFlowAdmin", "Key for Update", key, null);
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