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
    public class JobRequestDao : BaseDao
    {
        /// <summary>
        /// Get List 
        /// </summary>
        /// <param name="text"></param>
        /// <param name="departmentId"></param>
        /// <param name="positionId"></param>
        /// <param name="requestorId"></param>
        /// <param name="statusId"></param>
        /// <param name="assignId"></param>
        /// <returns></returns>
        public List<sp_GetJobRequestResult> GetList(string text, int department, int subdepartment, 
            int positionId, int requestorId, int statusId, string assignRole,int requestType)
        {
            //return dbContext.sp_GetJobRequest(text, department, subdepartment, positionId, requestorId, 
            //    statusId, requestType).Where(c => c.InvolveID.Split(',').Contains(assignId)).
            //    ToList<sp_GetJobRequestResult>();
            return dbContext.sp_GetJobRequest(text, department, subdepartment, positionId, requestorId,
                statusId, requestType).Where
                    (c =>
                        c.InvolveRole.Split(',').Contains(assignRole) || c.AssignRole.ToString().Equals(assignRole)
                    ).ToList<sp_GetJobRequestResult>();
        }


        //public List<sp_GetJobRequestResult> GetJrList(string text, int department, int subdepartment, int positionId, int requestorId, int statusId, string roleId, int requestType)
        //{
        //    return dbContext.sp_GetJobRequest(text, department, subdepartment, positionId, 
        //        requestorId, statusId, requestType).Where(p=>p.AssignRole.HasValue ? 
        //        p.AssignRole == int.Parse(roleId) : false).ToList();
        //}

        /// <summary>
        /// Get Job Request for HR
        /// </summary>
        /// <param name="text"></param>
        /// <param name="department"></param>
        /// <param name="subdepartment"></param>
        /// <param name="positionId"></param>
        /// <param name="requestorId"></param>
        /// <param name="statusId"></param>
        /// <param name="roleId"></param>
        /// <param name="requestType"></param>
        /// <returns></returns>
        public List<sp_GetJobRequestForHRResult> GetListForHR(string text, int department, int subdepartment, int positionId, int requestorId, int statusId, string roleId, int requestType)
        {
            return dbContext.sp_GetJobRequestForHR(text, department, subdepartment, positionId, requestorId, statusId, requestType).Where(c => c.InvolveRole.Split(',').Contains(roleId)).ToList<sp_GetJobRequestForHRResult>();
        }

        /// <summary>
        /// Get detail for Jon request
        /// </summary>
        /// <param name="JRID"></param>
        /// <returns></returns>
        public List<JobRequestItem> GetJRItems(int JRID)
        {
            return dbContext.JobRequestItems.Where(i => i.JRID == JRID && i.DeleteFlag == false).ToList<JobRequestItem>();
        }

        public List<sp_GetJobRequestResult> GetAllJRList(string text, int department, int subdepartment, int positionId, int requestorId, int requestType)
        {
            return dbContext.sp_GetJobRequest(text, department, subdepartment, positionId, requestorId, Constants.STATUS_CLOSE, requestType).Where(c => c.WFResolutionID == Constants.RESOLUTION_COMPLETED_ID).ToList<sp_GetJobRequestResult>();
        }

        public List<sp_GetJobRequestCompleteResult> GetJRListComplete(string text, int department, int subdepartment, int positionId, DateTime? issuedate,int requestType)
        {
            return dbContext.sp_GetJobRequestComplete(text, department, subdepartment, positionId, issuedate, Constants.RESOLUTION_COMPLETED_ID, Constants.STATUS_CLOSE, requestType).ToList<sp_GetJobRequestCompleteResult>();
        }

        public List<sp_GetJobRequestCompleteInterviewResult> GetJRListInterView(string text, int department, int subdepartment, int positionId, int requestType)
        {
            
            return dbContext.sp_GetJobRequestCompleteInterview(text, department, 
                subdepartment, positionId, requestType).ToList<sp_GetJobRequestCompleteInterviewResult>();
        }

        public JobRequest GetJRByAssign(int userAdminId, int roleId)
        {
            return dbContext.JobRequests.Where(p => p.AssignID == userAdminId && p.AssignRole == roleId && p.DeleteFlag == false).FirstOrDefault<JobRequest>();
        }

        public List<WFRole> GetRoleList(int userAdminId, int wfID)
        {
            List<WFRole> list = new List<WFRole>();
            var entities = from e in dbContext.UserAdmin_WFRoles
                           join f in dbContext.WFRoles on e.WFRoleID equals f.ID
                           where e.UserAdminId == userAdminId && e.IsActive == true && f.WFID == wfID
                           orderby f.ID ascending

                           select new
                           {
                               ID = e.WFRoleID,
                               Name = f.Name
                           };

            foreach (var e in entities)
            {
                WFRole role = new WFRole();
                role.ID = e.ID;
                role.Name = e.Name;
                list.Add(role);
            }

            return list;
        }

        /// <summary>
        /// Get Detail
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public sp_GetJobRequestResult GetDetailById(int id)
        {
            return dbContext.sp_GetJobRequest(null, 0, 0, 0, 0, 0,0).Where(p => p.ID == id).FirstOrDefault<sp_GetJobRequestResult>();
        }

        /// <summary>
        /// Get By Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public JobRequest GetById(int id)
        {
            return dbContext.JobRequests.Where(p => p.ID == id && p.DeleteFlag == false).FirstOrDefault<JobRequest>();
        }

        /// <summary>
        /// Get job request by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public JobRequestItem GetItemById(int id)
        {
            return dbContext.JobRequestItems.Where(i => i.ID == id && i.DeleteFlag == false).FirstOrDefault<JobRequestItem>();
        }

        /// <summary>
        /// Get JR Status
        /// </summary>
        /// <returns></returns>
        public List<JobRequestItemStatus> GetJRItemStatus()
        {
            return dbContext.JobRequestItemStatus.ToList<JobRequestItemStatus>();
        }

        /// <summary>
        /// Get By Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public JobRequest GetByJRComplete(int id)
        {
            return dbContext.JobRequests.Where(p => p.ID == id && p.WFResolutionID == Constants.RESOLUTION_COMPLETED_ID && p.WFStatusID == Constants.STATUS_CLOSE && p.DeleteFlag == false).FirstOrDefault<JobRequest>();
        }

        /// <summary>
        /// Get By AssignId
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public JobRequest GetByAssignId(int id)
        {
            return dbContext.JobRequests.Where(p => p.AssignID == id && p.DeleteFlag == false).FirstOrDefault<JobRequest>();
        }

        /// <summary>
        /// Sort Product
        /// </summary>
        /// <param name="list"></param>
        /// <param name="sortColumn"></param>
        /// <param name="sortOrder"></param>
        /// <returns></returns>
        public List<sp_GetJobRequestResult> Sort(List<sp_GetJobRequestResult> list, string sortColumn, string sortOrder)
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
                case "ID":
                    list.Sort(
                         delegate(sp_GetJobRequestResult m1, sp_GetJobRequestResult m2)
                         { return m1.ID.CompareTo(m2.ID) * order; });
                    break;
                case "Approval":
                    /*xxxxxxxxxxx
                    List<sp_GetJobRequestResult> approvalList = list.Where(c => c.Approval != null && c.Approval.Trim() != string.Empty).ToList<sp_GetJobRequestResult>();
                    List<sp_GetJobRequestResult> nonApprovalList = list.Where(c => !approvalList.Contains(c)).ToList<sp_GetJobRequestResult>();
                    approvalList.Sort(
                          delegate(sp_GetJobRequestResult m1, sp_GetJobRequestResult m2)
                          {
                              return m1.Approval.CompareTo(m2.Approval) * order;
                          });

                    nonApprovalList.Sort(
                         delegate(sp_GetJobRequestResult m1, sp_GetJobRequestResult m2)
                         { return m1.ID.CompareTo(m2.ID) * order; });


                    approvalList.AddRange(nonApprovalList);
                    return approvalList;
                    */
                    break;                
                case "RequestDate":
                    list.Sort(
                         delegate(sp_GetJobRequestResult m1, sp_GetJobRequestResult m2)
                         { return m1.RequestDate.CompareTo(m2.RequestDate) * order; });
                    break;
                case "JobTitleName":
                    list.Sort(
                         delegate(sp_GetJobRequestResult m1, sp_GetJobRequestResult m2)
                         { return m1.PositionFrom.CompareTo(m2.PositionFrom) * order; });
                    break;
                case "DepartmentName":
                    list.Sort(
                         delegate(sp_GetJobRequestResult m1, sp_GetJobRequestResult m2)
                         { return m1.SubDepartment.CompareTo(m2.SubDepartment) * order; });
                    break;
                case "ExpectedStartDate":
                    list.Sort(
                         delegate(sp_GetJobRequestResult m1, sp_GetJobRequestResult m2)
                         {
                             if (m1.ExpectedStartDate.HasValue && m2.ExpectedStartDate.HasValue)
                                 return m1.ExpectedStartDate.Value.CompareTo(m2.ExpectedStartDate.Value) * order;
                             else
                                 return m1.ID.CompareTo(m2.ID) * order;

                         });
                    break;
                case "StatusName":
                    list.Sort(
                         delegate(sp_GetJobRequestResult m1, sp_GetJobRequestResult m2)
                         { return m1.StatusName.CompareTo(m2.StatusName) * order; });
                    break;
                case "RequestorName":
                    list.Sort(
                         delegate(sp_GetJobRequestResult m1, sp_GetJobRequestResult m2)
                         { return m1.RequestorName.CompareTo(m2.RequestorName) * order; });
                    break;
                case "AssignName":
                    list.Sort(
                         delegate(sp_GetJobRequestResult m1, sp_GetJobRequestResult m2)
                         { return m1.AssignName.CompareTo(m2.AssignName) * order; });
                    break;
                case "ResolutionName":
                    list.Sort(
                         delegate(sp_GetJobRequestResult m1, sp_GetJobRequestResult m2)
                         { return m1.WFResolutionID.CompareTo(m2.WFResolutionID) * order; });
                    break;
                case "Request":
                    list.Sort(
                         delegate(sp_GetJobRequestResult m1, sp_GetJobRequestResult m2)
                         { return m1.RequestTypeId.CompareTo(m2.RequestTypeId) * order; });
                    break;
                case "Quantity":
                    list.Sort(
                         delegate(sp_GetJobRequestResult m1, sp_GetJobRequestResult m2)
                         { return m1.Quantity.CompareTo(m2.Quantity) * order; });
                    break;
            }

            return list;
        }

        public List<sp_GetJobRequestForHRResult> Sort(List<sp_GetJobRequestForHRResult> list, string sortColumn, string sortOrder)
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
                case "ItemID":
                    list.Sort(
                         delegate(sp_GetJobRequestForHRResult m1, sp_GetJobRequestForHRResult m2)
                         {
                             return m1.ID == m2.ID ? m1.JRItemID.CompareTo(m2.JRItemID) * order : m1.ID.CompareTo(m2.ID)* (-1);
                         });
                    break;
                case "Approval":                    
                     list.Sort(
                         delegate(sp_GetJobRequestForHRResult m1, sp_GetJobRequestForHRResult m2)
                         { return m1.ID == m2.ID ? m1.Approval.CompareTo(m2.Approval) * order : m1.ID.CompareTo(m2.ID)* (-1); });
                    break;
                case "Position":
                    list.Sort(
                        delegate(sp_GetJobRequestForHRResult m1, sp_GetJobRequestForHRResult m2)
                        { return m1.ID == m2.ID ? m1.Position.CompareTo(m2.Position) * order : m1.ID.CompareTo(m2.ID)* (-1); });
                    break;
                case "SubDepartment":
                    list.Sort(
                         delegate(sp_GetJobRequestForHRResult m1, sp_GetJobRequestForHRResult m2)
                         { return m1.ID == m2.ID ? m1.SubDepartment.CompareTo(m2.SubDepartment) * order : m1.ID.CompareTo(m2.ID)* (-1); });
                    break;
                case "RequestDate":
                    list.Sort(
                         delegate(sp_GetJobRequestForHRResult m1, sp_GetJobRequestForHRResult m2)
                         { return m1.ID == m2.ID ? m1.RequestDate.CompareTo(m2.RequestDate) * order : m1.ID.CompareTo(m2.ID)* (-1); });
                    break;
                
                case "ExpectedStartDate":
                    list.Sort(
                         delegate(sp_GetJobRequestForHRResult m1, sp_GetJobRequestForHRResult m2)
                         {
                             if (m1.ExpectedStartDate.HasValue && m2.ExpectedStartDate.HasValue)
                                 return m1.ID == m2.ID ? m1.ExpectedStartDate.Value.CompareTo(m2.ExpectedStartDate.Value) * order : m1.ID.CompareTo(m2.ID)* (-1);
                             else
                                 return m1.ID.CompareTo(m2.ID) * (-1);
                         });
                    break;
                case "Requestor":
                    list.Sort(
                         delegate(sp_GetJobRequestForHRResult m1, sp_GetJobRequestForHRResult m2)
                         { return m1.ID == m2.ID ? m1.RequestorName.CompareTo(m2.RequestorName) * order : m1.ID.CompareTo(m2.ID)* (-1); });
                    break;

                case "Status":
                    list.Sort(
                         delegate(sp_GetJobRequestForHRResult m1, sp_GetJobRequestForHRResult m2)
                         { return m1.ID == m2.ID ? m1.StatusName.CompareTo(m2.StatusName) * order : m1.ID.CompareTo(m2.ID)* (-1); });
                    break;
                case "Candidate":
                    list.Sort(
                         delegate(sp_GetJobRequestForHRResult m1, sp_GetJobRequestForHRResult m2)
                         {
                             return m1.ID == m2.ID ? m1.Candidate.CompareTo(m2.Candidate) * order : m1.ID.CompareTo(m2.ID) * (-1);
                         });
                    break;
                case "EmpID":
                    list.Sort(
                         delegate(sp_GetJobRequestForHRResult m1, sp_GetJobRequestForHRResult m2)
                         { return m1.ID == m2.ID ? m1.EmpID.CompareTo(m2.EmpID) * order : m1.ID.CompareTo(m2.ID)* (-1); });
                    break;
                case "FinalTitle":
                    list.Sort(
                         delegate(sp_GetJobRequestForHRResult m1, sp_GetJobRequestForHRResult m2)
                         { return m1.ID == m2.ID ? m1.FinalTitleName.CompareTo(m2.FinalTitleName) * order : m1.ID.CompareTo(m2.ID)* (-1); });
                    break;
                case "ActualStartDate":
                    list.Sort(
                        delegate(sp_GetJobRequestForHRResult m1, sp_GetJobRequestForHRResult m2)
                        {
                            if (m1.ActualStartDate.HasValue && m2.ActualStartDate.HasValue)
                                return m1.ID == m2.ID ? m1.ActualStartDate.Value.CompareTo(m2.ActualStartDate.Value) * order : m1.ID.CompareTo(m2.ID) * (-1);
                            else
                                return m1.ID.CompareTo(m2.ID) * (-1);
                        });
                    break;   
            }

            return list;
        }

        public List<sp_GetListAssignByRoleResult> GetListByRole(int roleID)
        {
            return dbContext.sp_GetListAssignByRole(roleID).ToList<sp_GetListAssignByRoleResult>();
        }

        public Message UpdateForApproval(JobRequest objUI, JobRequestComment objComment)
        {
            Message msg = null;
            DbTransaction trans = null;

            try
            {
                dbContext.Connection.Open();
                trans = dbContext.Connection.BeginTransaction();
                dbContext.Transaction = trans;
                if (objUI != null)
                {
                    JobRequest objDb = GetById(objUI.ID);
                    if (objDb != null)
                    {
                        #region Insert Comment
                        if (objComment != null && !string.IsNullOrEmpty(objComment.Contents))
                        {
                            JRCommentDao commentDao = new JRCommentDao();
                            commentDao.Insert(objComment);
                        }
                        #endregion
                        // Check valid update date
                        if (IsValidUpdateDate(objUI, objDb, out msg))
                        {
                            new JobRequestLogDao().WriteUpdateLogForRoleManager(objUI, ELogAction.Update);
                            objDb.WFResolutionID = objUI.WFResolutionID;
                            //if (!string.IsNullOrEmpty(objUI.Approval))
                            //{
                            //    objDb.Approval = objUI.Approval;
                            //}
                            objDb.WFResolution = objUI.WFResolution;
                            objDb.AssignID = objUI.AssignID;
                            objDb.AssignRole = objUI.AssignRole;
                            objDb.InvolveID = objUI.InvolveID;
                            objDb.InvolveRole = objUI.InvolveRole;
                            objDb.InvolveDate = objUI.InvolveDate;
                            objDb.UpdateDate = DateTime.Now;
                            dbContext.SubmitChanges();

                            msg = new Message(MessageConstants.I0001, MessageType.Info, " Job Request '" + Constants.JOB_REQUEST_PREFIX + objUI.ID + "'", "updated");
                            trans.Commit();
                        }
                        else
                        {
                            trans.Rollback();
                        }
                    }
                }
            }
            catch
            {
                // Rollback transaction
                if (trans != null)
                {
                    trans.Rollback();
                }
                // Show system error
                msg = new Message(MessageConstants.E0007, MessageType.Error);
            }
            finally
            {
                if ((trans.Connection != null) && (trans.Connection.State == System.Data.ConnectionState.Open))
                {
                    trans.Connection.Close();
                }
            }
            return msg;
        }

        /// <summary>
        /// Insert
        /// </summary>
        /// <param name="objUI"></param>
        /// <returns></returns>
        public Message Insert(JobRequest objUI)
        {
            Message msg = null;
            try
            {
                if (objUI != null)
                {
                    // Set more info
                    objUI.DeleteFlag = false;
                    objUI.CreateDate = DateTime.Now;
                    objUI.UpdateDate = DateTime.Now;
                    objUI.SalarySuggestion = string.IsNullOrEmpty(objUI.SalarySuggestion) ? null :
                        EncryptUtil.Encrypt(objUI.SalarySuggestion);

                    dbContext.JobRequests.InsertOnSubmit(objUI);
                    dbContext.SubmitChanges();
                    new JobRequestLogDao().WriteLogForJobRequest(null, objUI, ELogAction.Insert);
                    // Show success message
                    msg = new Message(MessageConstants.I0001, MessageType.Info, "Job Request '" + Constants.JOB_REQUEST_PREFIX + objUI.ID + "'", "added");
                }
            }
            catch(Exception ex)
            {
                // Show system error
                msg = new Message(MessageConstants.E0033, MessageType.Error, ex.Message);
                //msg = new Message(MessageConstants.E0007, MessageType.Error);
            }
            return msg;
        }

        /// <summary>
        /// Update
        /// </summary>
        /// <param name="objUI"></param>
        /// <returns></returns>
        public Message Update(JobRequest objUI)
        {
            Message msg = null;
            try
            {
                if (objUI != null)
                {
                    // Get current group in dbContext
                    JobRequest objDb = GetById(objUI.ID);

                    if (objDb != null)
                    {
                        // Check valid update date
                        if (IsValidUpdateDate(objUI, objDb, out msg))
                        {
                            // Update info by objUI
                            new JobRequestLogDao().WriteLogForJobRequest(null, objUI, ELogAction.Update);
                            objDb.DepartmentId = objUI.DepartmentId;
                            objDb.RequestDate = objUI.RequestDate;
                            objDb.ExpectedStartDate = objUI.ExpectedStartDate;
                            objDb.PositionFrom = objUI.PositionFrom;
                            objDb.PositionTo = objUI.PositionTo;
                            objDb.SalarySuggestion = string.IsNullOrEmpty(objUI.SalarySuggestion) ? null :
                                EncryptUtil.Encrypt(objUI.SalarySuggestion);
                            objDb.Justification = objUI.Justification;
                            if (objDb.WFResolutionID != 0 && objUI.WFResolutionID != 0)
                                objDb.WFResolutionID = objUI.WFResolutionID;
                            if (objDb.WFStatusID != 0 && objUI.WFStatusID != 0)
                                objDb.WFStatusID = objUI.WFStatusID;
                            objDb.AssignID = objUI.AssignID;
                            objDb.RequestTypeId = objUI.RequestTypeId;
                            objDb.AssignRole = objUI.AssignRole;
                            objDb.InvolveID = objUI.InvolveID;
                            objDb.InvolveRole = objUI.InvolveRole;
                            objDb.InvolveDate = objUI.InvolveDate;
                            objDb.UpdateDate = DateTime.Now;
                            objDb.UpdatedBy = objUI.UpdatedBy;
                            objDb.InvolveResolution = objUI.InvolveResolution;
                            objDb.CCList = objUI.CCList;
                            objDb.Quantity = objUI.Quantity;
                            // Submit changes to dbContext
                            dbContext.SubmitChanges();

                            // Show success message
                            msg = new Message(MessageConstants.I0001, MessageType.Info, "Job Request '" + Constants.JOB_REQUEST_PREFIX + objUI.ID + "'", "updated");

                        }
                    }
                }
            }
            catch
            {
                // Show system error
                msg = new Message(MessageConstants.E0007, MessageType.Error);
            }

            return msg;
        }

        public bool hasUpdate(JobRequest objUI)
        {
            bool hasUpdate = false;
            if (objUI != null)
            {
                // Get current group in dbContext
                JobRequest objDb = GetById(objUI.ID);

                if (objDb != null)
                {
                    if (objDb.WFStatusID != objUI.WFStatusID)
                    {
                        hasUpdate = true;
                    }
                    if (objDb.WFResolutionID != objUI.WFResolutionID)
                    {
                        hasUpdate = true;
                    }
                    if (objDb.RequestDate != objUI.RequestDate)
                    {
                        hasUpdate = true;
                    }
                    if (objDb.DepartmentId != objUI.DepartmentId)
                    {
                        hasUpdate = true;
                    }
                    if (objDb.PositionFrom != objUI.PositionFrom)
                    {
                        hasUpdate = true;
                    }
                    if (objDb.PositionTo != objUI.PositionTo)
                    {
                        hasUpdate = true;
                    }
                    if (objDb.ExpectedStartDate != objUI.ExpectedStartDate)
                    {
                        hasUpdate = true;
                    }
                    if (objDb.SalarySuggestion != objUI.SalarySuggestion)
                    {
                        hasUpdate = true;
                    }

                    /*if (objDb.ProbationMonths != objUI.ProbationMonths)
                    {
                        hasUpdate = true;
                    }*/

                    if (objDb.Justification != objUI.Justification)
                    {
                        hasUpdate = true;
                    }
                    
                    if (objDb.CCList != objUI.CCList)
                    {
                        hasUpdate = true;
                    }
                    if (objDb.AssignID != objUI.AssignID)
                    {
                        hasUpdate = true;
                    }
                    if (objDb.AssignRole != objUI.AssignRole)
                    {
                        hasUpdate = true;
                    }

                }
            }
            return hasUpdate;
        }

        /// <summary>
        /// Delete List
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public Message DeleteList(string ids, string userName)
        {
            Message msg = null;
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
                    int jrID = 0;
                    int totalID = idArr.Count();
                    foreach (string id in idArr)
                    {
                        bool isInterger = Int32.TryParse(id, out jrID);
                        JobRequest jobRequest = GetById(jrID);
                        if (jobRequest != null)
                        {
                            jobRequest.UpdatedBy = userName;
                            Delete(jobRequest);
                        }
                        else
                        {
                            totalID--;
                        }
                    }

                    // Show succes message
                    msg = new Message(MessageConstants.I0001, MessageType.Info, totalID.ToString() + " Job Request(s)", "deleted");
                    trans.Commit();
                }
            }
            catch
            {
                if (trans != null) trans.Rollback();
                // Show system error
                msg = new Message(MessageConstants.E0006, MessageType.Error, "delete", "it");
            }
            return msg;
        }

        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="objUI"></param>
        private void Delete(JobRequest objUI)
        {
            if (objUI != null)
            {
                // Get current group in dbContext
                JobRequest objDb = GetById(objUI.ID);

                if (objDb != null)
                {
                    new JobRequestLogDao().WriteLogForJobRequest(null, objUI, ELogAction.Delete);
                    // Set delete info
                    objDb.DeleteFlag = true;
                    objDb.UpdateDate = DateTime.Now;
                    objDb.UpdatedBy = objUI.UpdatedBy;

                    // Submit changes to dbContext
                    dbContext.SubmitChanges();
                }
            }
        }

        /// <summary>
        /// Get Assign List By Resolution ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<sp_GetListAssignByResolutionIdResult> GetListAssign(int id)
        {
            return dbContext.sp_GetListAssignByResolutionId(id, Constants.WORK_FLOW_JOB_REQUEST).ToList<sp_GetListAssignByResolutionIdResult>();
        }

        public bool IsApprovalExist(string approvalId)
        {
            JobRequestItem jr = dbContext.JobRequestItems.Where(c => c.Approval == approvalId && c.DeleteFlag == false).FirstOrDefault<JobRequestItem>();
            if (jr == null)
                return false;
            else
                return true;
        }

        #region HR fill data by Tan Tran

        public Message HRFillData(JobRequestItem objUI)
        {
            Message msg = null;
            try
            {
                JobRequestItem objDb = GetItemById(objUI.ID);
                
                if (objDb != null)
                {
                    // Check valid update date
                    if (IsValidUpdateDate(objUI, objDb, out msg))
                    {
                        //new JobRequestLogDao().WriteUpdateLogForRoleHR(objUI, ELogAction.Update);
                        // Update info by objUI                        
                        objDb.Candidate = objUI.Candidate;
                        objDb.Approval = objUI.Approval;
                        objDb.EmpID = objUI.EmpID;
                        objDb.FinalTitleId = objUI.FinalTitleId;
                        objDb.IssueDate = objUI.IssueDate;
                        objDb.Gender = objUI.Gender;
                        objDb.ActualStartDate = objUI.ActualStartDate;
                        objDb.ProbationMonths = objUI.ProbationMonths;
                        objDb.ProbationSalary = !string.IsNullOrEmpty(objUI.ProbationSalary) ? EncryptUtil.Encrypt(objUI.ProbationSalary) : null;
                        objDb.ContractedSalary = !string.IsNullOrEmpty(objUI.ContractedSalary) ? EncryptUtil.Encrypt(objUI.ContractedSalary) : null;
                        objDb.ProbationSalaryNote = objUI.ProbationSalaryNote;
                        objDb.ContractedSalaryNote = objUI.ContractedSalaryNote;
                        objDb.StatusID = objUI.StatusID;
                        objDb.Title = objUI.Title;

                        objDb.UpdateDate = DateTime.Now;
                        objDb.UpdatedBy = objUI.UpdatedBy;

                        // close Job request if all its items don't have Open status
                        int count = objDb.JobRequest.JobRequestItems.Where(i => i.StatusID == Constants.JR_ITEM_STATUS_OPEN && i.DeleteFlag == false).Count();
                        if (count == 0)
                        {
                            int completeCount = objDb.JobRequest.JobRequestItems.Where(p => p.StatusID == Constants.JR_ITEM_STATUS_SUCCESS).Count();
                            if (completeCount > 0)
                                objDb.JobRequest.WFResolutionID = Constants.RESOLUTION_COMPLETED_ID;
                            else
                                objDb.JobRequest.WFResolutionID = Constants.RESOLUTION_CANCEL_ID;
                            objDb.JobRequest.WFStatusID = Constants.STATUS_CLOSE;
                        }

                        // Submit changes to dbContext
                        dbContext.SubmitChanges();
                                                
                        // Show success message
                        msg = new Message(MessageConstants.I0001, MessageType.Info, "Job Request Item'" + Constants.JOB_REQUEST_ITEM_PREFIX + objUI.ID + "'", "updated");
                    }
                }
            }
            catch
            {
                msg = new Message(MessageConstants.E0007, MessageType.Error);
            }

            return msg;
        }

        public Message ForwardTo(JobRequest objUI)
        { 
            Message msg = null;
            try
            {
                JobRequest objDb = GetById(objUI.ID);

                if (objDb != null)
                {
                    // Check valid update date
                    if (IsValidUpdateDate(objUI, objDb, out msg))
                    {
                        new JobRequestLogDao().WriteUpdateLogForRoleHR(objUI, ELogAction.Update);
                        // Update info by objUI                        
                        objDb.AssignID = objUI.AssignID;
                        objDb.AssignRole = objUI.AssignRole;
                        objDb.InvolveID = objUI.InvolveID;
                        objDb.InvolveRole = objUI.InvolveRole;
                        objDb.InvolveDate = objUI.InvolveDate;
                        objDb.InvolveResolution = objUI.InvolveResolution;
                        objDb.UpdateDate = DateTime.Now;
                        objDb.UpdatedBy = objUI.UpdatedBy;

                        // Submit changes to dbContext
                        dbContext.SubmitChanges();

                        // Show success message
                        msg = new Message(MessageConstants.I0001, MessageType.Info, "Job Request'" + Constants.JOB_REQUEST_PREFIX + objUI.ID + "'", "updated");
                    }
                }
            }
            catch
            {
                // Show system error
                msg = new Message(MessageConstants.E0007, MessageType.Error);
            }

            return msg;
        }

        
        /// <summary>
        /// Check update date whether is valid
        /// </summary>
        /// <param name="objUI"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        private bool IsValidUpdateDate(JobRequest objUI, JobRequest objDb, out Message msg)
        {
            bool isValid = false;
            msg = null;

            try
            {
                if ((objUI != null) && (objUI.UpdateDate != null) && (objDb != null))
                {
                    if (objDb.UpdateDate.ToString().Equals(objUI.UpdateDate.ToString()))
                    {
                        isValid = true;
                    }
                    else
                    {
                        msg = new Message(MessageConstants.E0025, MessageType.Error, "Job Request Id " + objDb.ID);
                    }
                }
            }
            catch
            {
                throw;
            }

            return isValid;
        }

        /// <summary>
        /// Validate update date for JR item
        /// </summary>
        /// <param name="objUI"></param>
        /// <param name="objDb"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        private bool IsValidUpdateDate(JobRequestItem objUI, JobRequestItem objDb, out Message msg)
        {
            bool isValid = false;
            msg = null;

            try
            {
                if ((objUI != null) && (objUI.UpdateDate != null) && (objDb != null))
                {
                    if (objDb.UpdateDate.ToString().Equals(objUI.UpdateDate.ToString()))
                    {
                        isValid = true;
                    }
                    else
                    {
                        msg = new Message(MessageConstants.E0025, MessageType.Error, "Job Request Id " + objDb.ID);
                    }
                }
            }
            catch
            {
                throw;
            }

            return isValid;
        }

        /// <summary>
        /// Generate Job request item 
        /// </summary>
        /// <param name="id">id of job request</param>
        /// <param name="user">user update</param>
        /// <param name="assignId">int</param>
        /// <param name="assignRole">int</param>
        public void GenerateJobRequest(int? id, string user,  int assignId, int assignRole)
        {
            dbContext.sp_GenerateJobRequest(id, user, assignId, assignRole);
        }

        /// <summary>
        /// Get job request item list
        /// </summary>
        /// <param name="JRId">int</param>
        /// <returns>List<JobRequestItem></returns>
        public List<sp_GetJobRequestItemListResult> GetJobRequestItemList(int JRId)
        {
            return dbContext.sp_GetJobRequestItemList(JRId).ToList();
        }

        #endregion

    }
}