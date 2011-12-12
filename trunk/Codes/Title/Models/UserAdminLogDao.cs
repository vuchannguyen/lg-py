using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CRM.Library.Common;

namespace CRM.Models
{
    public class UserAdminLogDao : BaseDao
    {
        
        //
        // GET: /ContractlLogDao/
        private CommonLogDao commonDao = new CommonLogDao();
        private UserAdminDao dao = new UserAdminDao();
        /// <summary>
        /// Write Log For UserAdmin
        /// </summary>
        /// <param name="oldInfo"></param>
        /// <param name="newInfo"></param>
        /// <param name="action"></param>
        public void WriteLogForUserAdmin(User_Group oldInfo, User_Group newInfo, ELogAction action)
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
                        commonDao.InsertMasterLog(logId, newInfo.CreatedBy, ELogTable.UserAdmin.ToString(), action.ToString());
                        // Write Insert Log
                        WriteInsertLogForUserAdmin(newInfo, logId);
                        break;
                    case ELogAction.Update:
                        // Insert to Master Log
                        commonDao.InsertMasterLog(logId, newInfo.UpdatedBy, ELogTable.UserAdmin.ToString(), action.ToString());
                        // Write Update Log
                        bool isUpdated = WriteUpdateLogForUserAdmin(newInfo, logId);
                        if (!isUpdated)
                        {
                            commonDao.DeleteMasterLog(logId);
                        }
                        break;
                    case ELogAction.Delete:
                        // Insert to Master Log
                        commonDao.InsertMasterLog(logId, newInfo.UpdatedBy, ELogTable.UserAdmin.ToString(), action.ToString());
                        // Write Delete Log
                        string key = "User name '" + newInfo.UserAdmin.UserName + "'";
                        commonDao.InsertLogDetail(logId, "UserAdminId", "Key for Delete ", key, null);
                        break;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Write Insert Log For UserAdmin
        /// </summary>
        /// <param name="objInfo"></param>
        /// <param name="logId"></param>
        private void WriteInsertLogForUserAdmin(User_Group objInfo, string logId)
        {
            try
            {
                if ((objInfo != null) && (logId != null))
                {
                    // Insert to Log Detail                       
                    commonDao.InsertLogDetail(logId, "UserName", "User Name", null, objInfo.UserAdmin.UserName);
                    if (!string.IsNullOrEmpty(objInfo.UserAdmin.UserName))
                    {
                        commonDao.InsertLogDetail(logId, "UserName", "UserName", null, objInfo.UserAdmin.UserName);
                    }
                    commonDao.InsertLogDetail(logId, "GroupId", "Group", null, objInfo.Group.GroupName);
                    commonDao.InsertLogDetail(logId, "IsActive", "Active", null, objInfo.IsActive.ToString());
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Write Update Log For UserAdmin
        /// </summary>
        /// <param name="newInfo"></param>
        /// <param name="logId"></param>
        /// <returns></returns>
        private bool WriteUpdateLogForUserAdmin(User_Group newInfo, string logId)
        {
            bool isUpdated = false;
            try
            {
                // Get old info
                User_Group oldInfo = dao.GetUser_Group(newInfo.ID);

                if ((oldInfo != null) && (newInfo != null) && (logId != null))
                {
                    if (!newInfo.UserAdmin.UserName.Equals(oldInfo.UserAdmin.UserName))
                    {
                        commonDao.InsertLogDetail(logId, "UserName", "User Name", oldInfo.UserAdmin.UserName, newInfo.UserAdmin.UserName);
                        isUpdated = true;
                    }

                    if (newInfo.GroupId != oldInfo.GroupId)
                    {
                        commonDao.InsertLogDetail(logId, "Group", "Group", oldInfo.Group.GroupName, new GroupDao().GetById(newInfo.GroupId).GroupName);
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
                        string key = "User name '" + oldInfo.UserAdmin.UserName + "'";
                        commonDao.InsertLogDetail(logId, "UserAdminId", "Key for Update", key, null);
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
