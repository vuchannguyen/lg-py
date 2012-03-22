using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CRM.Library.Common;

namespace CRM.Models
{
    public class JobRequestLogDao : BaseDao
    {
        private CommonLogDao commonDao = new CommonLogDao();
        private DepartmentDao deptDao = new DepartmentDao();
        private UserAdminDao userAdmintDao = new UserAdminDao();

        public void WriteLogForJobRequest(JobRequest oldInfo, JobRequest newInfo, ELogAction action)
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
                        commonDao.InsertMasterLog(logId, newInfo.CreatedBy, ELogTable.JobRequest.ToString(), action.ToString());
                        // Write Insert Log
                        WriteInsertLogForJobRequest(newInfo, logId);
                        break;
                    case ELogAction.Update:
                        // Insert to Master Log
                        commonDao.InsertMasterLog(logId, newInfo.UpdatedBy, ELogTable.JobRequest.ToString(), action.ToString());
                        // Write Update Log
                        bool isUpdated = WriteUpdateLogForJobRequest(newInfo, logId);
                        if (!isUpdated)
                        {
                            commonDao.DeleteMasterLog(logId);
                        }
                        break;
                    case ELogAction.Delete:
                        // Insert to Master Log
                        commonDao.InsertMasterLog(logId, newInfo.UpdatedBy, ELogTable.JobRequest.ToString(), action.ToString());
                        // Write Delete Log
                        string key = newInfo.ID.ToString() + " [" + newInfo.UserAdmin.UserName + " (Requestor) ]";
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
        /// Write Insert Log Job Request
        /// </summary>
        /// <param name="objInfo"></param>
        /// <param name="logId"></param>
        private void WriteInsertLogForJobRequest(JobRequest objInfo, string logId)
        {
            try
            {
                if ((objInfo != null) && (logId != null))
                {
                    // Insert to Log Detail                
                    commonDao.InsertLogDetail(logId, "ID", "Request", null, Constants.JOB_REQUEST_PREFIX + objInfo.ID.ToString());
                    commonDao.InsertLogDetail(logId, "WFStatusID", "Status", null, objInfo.WFStatus.Name);
                    commonDao.InsertLogDetail(logId, "WFResolutionID", "Resolution", null, objInfo.WFResolution.Name);
                    commonDao.InsertLogDetail(logId, "RequestorId", "Requestor", null, objInfo.UserAdmin.UserName + " (Requestor) ");
                    commonDao.InsertLogDetail(logId, "RequestDate", "Request Date", null, objInfo.RequestDate.ToString(Constants.DATETIME_FORMAT_VIEW));
                    commonDao.InsertLogDetail(logId, "Department", "Department", null, deptDao.GetDepartmentNameBySub(objInfo.DepartmentId));
                    commonDao.InsertLogDetail(logId, "DepartmentId", "Sub Department", null, objInfo.Department.DepartmentName);
                    //commonDao.InsertLogDetail(logId, "PositionFrom", "Position From", null, objInfo.JobTitleLevel.DisplayName);
                    commonDao.InsertLogDetail(logId, "RequestTypeId", "Request Type", null, objInfo.RequestTypeId == Constants.JR_REQUEST_TYPE_NEW ? "New" : "Replace");
                    //if (objInfo.PositionTo.HasValue)
                    //{
                    //    commonDao.InsertLogDetail(logId, "PositionTo", "Position To", null, objInfo.JobTitleLevel1.DisplayName);
                    //}
                    if (objInfo.ExpectedStartDate.HasValue)
                    {
                        commonDao.InsertLogDetail(logId, "ExpectedStartDate", "Expected Start Date", null, objInfo.ExpectedStartDate.Value.ToString(Constants.DATETIME_FORMAT_VIEW));
                    }
                    if (!string.IsNullOrEmpty(objInfo.SalarySuggestion))
                    {
                        commonDao.InsertLogDetail(logId, "SalarySuggestion", "Salary Suggestion", null, objInfo.SalarySuggestion);
                    }
                    if (!string.IsNullOrEmpty(objInfo.CCList))
                    {
                        commonDao.InsertLogDetail(logId, "CCList", "CC List", null, objInfo.CCList);
                    }
                    /*if (!string.IsNullOrEmpty(objInfo.Justification))
                    {
                        commonDao.InsertLogDetail(logId, "Justification", "Justification", null, objInfo.Justification);
                    }*/
                    if (objInfo.AssignID.HasValue)
                    {
                        if (objInfo.WFResolutionID != Constants.RESOLUTION_NEW_ID)
                        {
                            commonDao.InsertLogDetail(logId, "AssignID", "Forward to", null, objInfo.UserAdmin1.UserName + " (Manager) ");
                        }                             
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Write Update Log For Job Request
        /// </summary>
        /// <param name="newInfo"></param>
        /// <param name="logId"></param>
        /// <returns></returns>
        private bool WriteUpdateLogForJobRequest(JobRequest newInfo, string logId)
        {
            bool isUpdated = false;
            try
            {
                // Get old info
                JobRequest oldInfo = new JobRequestDao().GetById(newInfo.ID);

                if ((oldInfo != null) && (newInfo != null) && (logId != null))
                {
                    if (newInfo.WFStatusID != oldInfo.WFStatusID)
                    {
                        if (newInfo.WFStatusID != 0)
                        {
                            commonDao.InsertLogDetail(logId, "WFStatusID", "Status", oldInfo.WFStatus.Name, new WFStatusDao().GetByID(newInfo.WFStatusID).Name);
                            isUpdated = true;
                        }
                    }
                    if (newInfo.CCList != oldInfo.CCList)
                    {
                        commonDao.InsertLogDetail(logId, "CCList", "CC List", oldInfo.CCList, newInfo.CCList);
                        isUpdated = true;
                    }
                    if (newInfo.RequestTypeId != oldInfo.RequestTypeId)
                    {
                        commonDao.InsertLogDetail(logId, "CCList", "CC List", oldInfo.RequestTypeId == Constants.JR_REQUEST_TYPE_NEW ? "New":"Replace", newInfo.RequestTypeId == Constants.JR_REQUEST_TYPE_NEW ? "New":"Replace");
                        isUpdated = true;
                    }
                    if (newInfo.WFResolutionID != oldInfo.WFResolutionID)
                    {
                        if (newInfo.WFResolutionID != 0)
                        {
                            WFResolution objRes = new ResolutionDao().GetByID(newInfo.WFResolutionID);
                            if (objRes != null)
                            {
                                commonDao.InsertLogDetail(logId, "WFResolutionID", "Resolution", oldInfo.WFResolution.Name, objRes.Name);
                                isUpdated = true;
                            }
                        }
                    }
                    if (newInfo.RequestDate != oldInfo.RequestDate)
                    {
                        commonDao.InsertLogDetail(logId, "RequestDate", "Request Date", oldInfo.RequestDate.ToString(Constants.DATETIME_FORMAT_VIEW), newInfo.RequestDate.ToString(Constants.DATETIME_FORMAT_VIEW));
                        isUpdated = true;
                    }
                    if (newInfo.DepartmentId != oldInfo.DepartmentId)
                    {
                        Department objDepart = new DepartmentDao().GetById(newInfo.DepartmentId);
                        if (objDepart != null)
                        {
                            commonDao.InsertLogDetail(logId, "DepartmentId", "Sub Department", oldInfo.Department.DepartmentName, objDepart.DepartmentName);
                            commonDao.InsertLogDetail(logId, "Department", "Department", deptDao.GetDepartmentNameBySub(oldInfo.DepartmentId), deptDao.GetDepartmentNameBySub(newInfo.DepartmentId));
                            isUpdated = true;
                        }
                    }
                    //if (newInfo.PositionFrom != oldInfo.PositionFrom)
                    //{
                    //    JobTitleLevel objJobFrom = new JobTitleLevelDao().GetById(newInfo.PositionFrom);
                    //    if (objJobFrom != null)
                    //    {
                    //        commonDao.InsertLogDetail(logId, "PositionFrom", "Position From", oldInfo.JobTitleLevel.DisplayName, objJobFrom.DisplayName);
                    //        isUpdated = true;
                    //    }
                    //}
                    //if (newInfo.PositionTo != oldInfo.PositionTo)
                    //{
                    //    if (newInfo.PositionTo.HasValue)
                    //    {
                    //        JobTitleLevel objJobTo = new JobTitleLevelDao().GetById(newInfo.PositionTo.Value);
                    //        if (objJobTo != null)
                    //        {
                    //            commonDao.InsertLogDetail(logId, "PositionTo", "Position To", oldInfo.PositionTo.HasValue?oldInfo.JobTitleLevel1.DisplayName:"", objJobTo.DisplayName);
                    //            isUpdated = true;
                    //        }
                    //    }
                    //}                    
                    string oldSalarySuggestionValue = string.IsNullOrEmpty(oldInfo.SalarySuggestion) ? null : EncryptUtil.Decrypt(oldInfo.SalarySuggestion);
                    if (newInfo.SalarySuggestion != oldSalarySuggestionValue)
                    {
                        if (!string.IsNullOrEmpty(newInfo.SalarySuggestion))
                        {
                            commonDao.InsertLogDetail(logId, "SalarySuggestion", "Salary Suggestion", oldInfo.SalarySuggestion, EncryptUtil.Encrypt(newInfo.SalarySuggestion));
                            isUpdated = true;
                        }
                        else
                        {
                            commonDao.InsertLogDetail(logId, "SalarySuggestion", "Salary Suggestion", oldInfo.SalarySuggestion,newInfo.SalarySuggestion);
                            isUpdated = true;
                        }
                    }
                    if (newInfo.AssignRole != oldInfo.AssignRole || newInfo.AssignID != oldInfo.AssignID)
                    {
                        if (newInfo.AssignRole.HasValue)
                        {
                            WFRole obj = new RoleDao().GetByID(newInfo.AssignRole.Value);
                            if (obj != null)
                            {
                                UserAdmin objUserAdmin = new UserAdminDao().GetById(newInfo.AssignID.Value);
                                if (objUserAdmin != null)
                                {
                                    commonDao.InsertLogDetail(logId, "AssignID", "Forward to", oldInfo.UserAdmin1.UserName + " ( " + oldInfo.WFRole.Name + " )", objUserAdmin.UserName + " ( " + obj.Name + " )");
                                    isUpdated = true;
                                }
                            }
                        }
                    }
                    if (isUpdated)
                    {
                        // Insert Key Name
                        string key = Constants.JOB_REQUEST_PREFIX + oldInfo.ID.ToString();
                        commonDao.InsertLogDetail(logId, "EmployeeId", "Key for Update", key, null);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return isUpdated;
        }

        /// <summary>
        /// Write Update Log For When Role Manager
        /// </summary>
        /// <param name="newInfo"></param>
        /// <param name="logId"></param>
        /// <returns></returns>
        public void WriteUpdateLogForRoleManager(JobRequest newInfo,ELogAction action)
        {
            bool isUpdated = false;
            try
            {
                // Get old info
                string logId = commonDao.UniqueId;
                commonDao.InsertMasterLog(logId, newInfo.UpdatedBy, ELogTable.JobRequest.ToString(), action.ToString());
                JobRequest oldInfo = new JobRequestDao().GetById(newInfo.ID);

                if ((oldInfo != null) && (newInfo != null) && (logId != null))
                {
                    if (newInfo.WFStatusID != oldInfo.WFStatusID)
                    {
                        commonDao.InsertLogDetail(logId, "WFStatusID", "Status", oldInfo.WFStatus.Name, new WFStatusDao().GetByID(newInfo.WFStatusID).Name);
                        isUpdated = true;
                    }
                    if (newInfo.WFResolutionID != oldInfo.WFResolutionID)
                    {
                        WFResolution objRes = new ResolutionDao().GetByID(newInfo.WFResolutionID);
                        if (objRes != null)
                        {
                            commonDao.InsertLogDetail(logId, "WFResolutionID", "Resolution", oldInfo.WFResolution.Name, objRes.Name);
                            isUpdated = true;
                        }
                    }
                    if (newInfo.AssignRole != oldInfo.AssignRole)
                    {
                        if (newInfo.AssignRole.HasValue)
                        {
                            WFRole obj = new RoleDao().GetByID(newInfo.AssignRole.Value);
                            if (obj != null)
                            {
                                UserAdmin objUserAdmin = new UserAdminDao().GetById(newInfo.AssignID.Value);
                                if (objUserAdmin != null)
                                {
                                    commonDao.InsertLogDetail(logId, "AssignID", " Forward to", oldInfo.UserAdmin1.UserName + " (" + oldInfo.WFRole.Name + ")", objUserAdmin.UserName + " (" + obj.Name + ")");
                                    isUpdated = true;
                                }
                            }
                        }
                    }
                    if (isUpdated)
                    {
                        // Insert Key Name
                        string key = Constants.JOB_REQUEST_PREFIX + oldInfo.ID.ToString();
                        commonDao.InsertLogDetail(logId, "EmployeeId", "Key for Update", key, null);
                    }
                    else
                    {
                       commonDao.DeleteMasterLog(logId);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Write Update Log For When Approve or Reject
        /// </summary>
        /// <param name="newInfo"></param>
        /// <param name="logId"></param>
        /// <returns></returns>
        public void WriteUpdateLogForRoleHR(JobRequest newInfo, ELogAction action)
        {
            try
            {
                bool isUpdate = false;
                // Get old info
                string logId = commonDao.UniqueId;
                commonDao.InsertMasterLog(logId, newInfo.UpdatedBy, ELogTable.JobRequest.ToString(), action.ToString());
                JobRequest oldInfo = new JobRequestDao().GetById(newInfo.ID);

                if ((oldInfo != null) && (newInfo != null) && (logId != null))
                {
                    if (oldInfo.AssignID != newInfo.AssignID)
                    {
                        string oldUser = "";                        
                        if (oldInfo.AssignID != null && oldInfo.AssignID.HasValue)
                        {
                            UserAdmin userAdmin = userAdmintDao.GetById(oldInfo.AssignID.Value);
                            oldUser = userAdmin != null ? userAdmin.UserName : "";
                        }

                        string newUser = "";
                        if (newInfo.AssignID != null && newInfo.AssignID.HasValue)
                        {
                            UserAdmin userAdmin = userAdmintDao.GetById(newInfo.AssignID.Value);
                            newUser = userAdmin != null ? userAdmin.UserName : "";
                        }

                        commonDao.InsertLogDetail(logId, "Forward To", "Forward To", oldUser, newUser);
                        isUpdate = true;
                    }
                   /* if (oldInfo.AssignRole != newInfo.AssignRole)
                    {
                        string oldVal = oldInfo.AssignRole.HasValue ? oldInfo.AssignRole.ToString() : "";
                        string newVal = (newInfo.AssignRole != null && newInfo.AssignRole.HasValue) ? newInfo.AssignRole.ToString() : "";

                        commonDao.InsertLogDetail(logId, "AssignRole", "AssignRole", oldVal, newVal);
                        isUpdate = true;
                    }
                    if (oldInfo.InvolveID != newInfo.InvolveID)
                    {
                        commonDao.InsertLogDetail(logId, "InvolveID", "InvolveID", oldInfo.InvolveID, newInfo.InvolveID);
                        isUpdate = true;
                    }
                    if (oldInfo.InvolveRole != newInfo.InvolveRole)
                    {
                        commonDao.InsertLogDetail(logId, "InvolveRole", "InvolveRole", oldInfo.InvolveRole, newInfo.InvolveRole);
                        isUpdate = true;
                    }
                    if (oldInfo.InvolveDate != newInfo.InvolveDate)
                    {
                        commonDao.InsertLogDetail(logId, "InvolveDate", "InvolveDate", oldInfo.InvolveDate, newInfo.InvolveDate);
                        isUpdate = true;
                    }
                    if (oldInfo.InvolveResolution != newInfo.InvolveResolution)
                    {
                        commonDao.InsertLogDetail(logId, "InvolveResolution", "InvolveResolution", oldInfo.InvolveResolution, newInfo.InvolveResolution);
                        isUpdate = true;
                    }*/

                    if (isUpdate)
                    {
                        // Insert Key Name
                        string key = Constants.JOB_REQUEST_PREFIX + oldInfo.ID.ToString();
                        commonDao.InsertLogDetail(logId, "EmployeeId", "Key for Update", key, null);
                    }
                    else
                    {
                        commonDao.DeleteMasterLog(logId);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}