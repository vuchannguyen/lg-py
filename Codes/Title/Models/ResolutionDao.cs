using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CRM.Library.Common;

namespace CRM.Models
{
    public class ResolutionDao : BaseDao
    {
        public List<WFResolution> GetListResolutionChangeByRole(int roleId)
        {
            List<WFResolution> list = new List<WFResolution>();
            var entities = from e in dbContext.WFRole_WFResolutions
                           join f in dbContext.WFResolutions on e.WFResolutionID equals f.ID
                           where e.WFRoleID == roleId && e.IsHold == false 

                           select new
                           {
                               ID = f.ID,
                               Name = f.Name
                           };

            foreach (var e in entities)
            {
                WFResolution resolution = new WFResolution();
                resolution.ID = e.ID;
                resolution.Name = e.Name;
                list.Add(resolution);
            }

            return list;
        }

        public List<WFResolution> GetListByRoleAndResolution(PurchaseRequest obj)
        {
            if (!obj.AssignRole.HasValue)
                return new List<WFResolution>();
            var list = GetListResolutionChangeByRole(obj.AssignRole.Value);
            if (obj.AssignRole == Constants.PR_REQUESTOR_ID)
            {
                if (obj.WFResolutionID == Constants.PR_RESOLUTION_REJECT)//case PR Reject
                {
                    list = list.Where(p => p.ID != Constants.PR_RESOLUTION_NEW_ID &&
                        p.ID != Constants.PR_RESOLUTION_SR_MANAGER_REJECT_TO_REQUESTOR).ToList<WFResolution>();
                }
                else if (obj.WFResolutionID == Constants.PR_RESOLUTION_SR_MANAGER_REJECT_TO_REQUESTOR)
                {
                    list = list.Where(p => p.ID != Constants.PR_RESOLUTION_NEW_ID &&
                        p.ID != Constants.PR_RESOLUTION_REJECT).ToList<WFResolution>();
                }
                else if (obj.WFResolutionID == Constants.PR_RESOLUTION_NEW_ID)
                {
                    list = list.Where(p => p.ID != Constants.PR_RESOLUTION_REJECT
                      && p.ID != Constants.PR_RESOLUTION_SR_MANAGER_REJECT_TO_REQUESTOR &&
                      p.ID != Constants.PR_RESOLUTION_CANCEL).ToList<WFResolution>();
                }
                else
                {
                    list = list.Where(p => p.ID != Constants.PR_RESOLUTION_REJECT
                        && p.ID != Constants.PR_RESOLUTION_SR_MANAGER_REJECT_TO_REQUESTOR).ToList<WFResolution>();
                }
            }
            else if (obj.AssignRole == Constants.PR_PURCHASING_ID)
            {
                if (obj.WFResolutionID == Constants.PR_RESOLUTION_SR_MANAGER_REJECT_TO_PURCHASING)
                {
                    list = list.Where(p =>  p.ID == Constants.PR_RESOLUTION_TO_BE_APPROVED_BY_SR_MANAGER
                        || p.ID == Constants.PR_RESOLUTION_SR_MANAGER_REJECT_TO_PURCHASING 
                        || p.ID == Constants.PR_RESOLUTION_REJECT).ToList<WFResolution>();
                }
                else
                {
                    //case Requestor send PR to Group Purchasing
                    list = list.Where(p => p.ID == Constants.PR_RESOLUTION_TO_BE_APPROVED_BY_PURCHASING 
                        || p.ID == Constants.PR_RESOLUTION_TO_BE_APPROVED_BY_SR_MANAGER
                        || p.ID == Constants.PR_RESOLUTION_REJECT).ToList<WFResolution>();
                }
            }
            else
            { 
                list = new List<WFResolution>();
                list.Add(obj.WFResolution);
            }
            
            return list;
        }

        public List<WFResolution> GetListByRoleAndResolutionUS(PurchaseRequest obj)
        {
            if (!obj.AssignRole.HasValue)
                return new List<WFResolution>();
            var list = GetListResolutionChangeByRole(obj.AssignRole.Value);
            if (obj.AssignRole == Constants.PR_REQUESTOR_ID_US)
            {
                if (obj.WFResolutionID == Constants.PRUS_RESOLUTION_REJECTED_TO_REQUESTOR)//case PR Reject
                {
                    list = list.Where(p => p.ID != Constants.PRUS_RESOLUTION_REJECTED_TO_CORP_CTRLER).ToList<WFResolution>();
                }
                else if (obj.WFResolutionID == Constants.PRUS_RESOLUTION_REJECTED_TO_DEPT_HEAD)
                {
                    list = list.Where(p => p.ID != Constants.PRUS_RESOLUTION_NEW &&
                      p.ID != Constants.PRUS_RESOLUTION_CANCELLED).ToList<WFResolution>();
                }
                else if (obj.WFResolutionID == Constants.PRUS_RESOLUTION_NEW)
                {
                    list = list.Where(p => p.ID != Constants.PR_RESOLUTION_REJECT
                      && p.ID != Constants.PRUS_RESOLUTION_REJECTED_TO_CORP_CTRLER &&
                      p.ID != Constants.PRUS_RESOLUTION_CANCELLED).ToList<WFResolution>();
                }
                else
                {
                    list = list.Where(p => p.ID != Constants.PRUS_RESOLUTION_CANCELLED
                        && p.ID != Constants.PRUS_RESOLUTION_REJECTED_TO_CORP_CTRLER).ToList<WFResolution>();
                }
            }
            else if (obj.AssignRole == Constants.PR_PURCHASING_ID_US)
            {
                if (obj.WFResolutionID == Constants.PRUS_RESOLUTION_REJECTED_TO_DEPT_HEAD)
                {
                    list = list.Where(p => p.ID == Constants.PRUS_RESOLUTION_WAITING_FOR_CORP_CTRLER_APPROVAL
                        || p.ID == Constants.PRUS_RESOLUTION_REJECTED_TO_REQUESTOR
                        || p.ID == Constants.PRUS_RESOLUTION_CANCELLED).ToList<WFResolution>();
                }
                else if (obj.WFResolutionID == Constants.PRUS_RESOLUTION_TO_BE_PROCESSED)
                {
                    list = list.Where(p => p.ID == Constants.PRUS_RESOLUTION_TO_BE_PROCESSED).ToList<WFResolution>();
                }
                else
                {
                    //case Requestor send PR to Group Purchasing
                    list = list.Where(p => p.ID == Constants.PRUS_RESOLUTION_WAITING_FOR_DEPT_HEAD_APPROVAL
                        || p.ID == Constants.PRUS_RESOLUTION_REJECTED_TO_REQUESTOR
                        || p.ID == Constants.PR_RESOLUTION_REJECT).ToList<WFResolution>();
                }
            }
            else
            {
                list = new List<WFResolution>();
                list.Add(obj.WFResolution);
            }

            return list;
        }

        public List<WFResolution> GetListForwardTo(int fromRoleId, int toRoleId)
        { 
            var fromList = dbContext.WFRole_WFResolutions.Where(p=>p.WFRoleID == fromRoleId && !p.IsHold).
                Select(p=>p.WFResolutionID).ToList();
            var toList = dbContext.WFRole_WFResolutions.Where(p => p.WFRoleID == toRoleId && p.IsHold).
                Select(p => p.WFResolutionID).ToList();
            return dbContext.WFResolutions.
                Where(p => fromList.Contains(p.ID) && toList.Contains(p.ID)).ToList();
        }
        public WFResolution GetByID(int id)
        {
            return dbContext.WFResolutions.Where(c => c.ID == id && c.DeletedFlag ==false).FirstOrDefault<WFResolution>();
        }

        public List<WFResolution> GetResolutionByWorkFlow(int wfID)
        {
            return dbContext.WFResolutions.Where(c => c.WFID == wfID && c.DeletedFlag == false).ToList();
        }
        public List<WFResolution> GetResolutionByWorkFlow(int wfID, int status)
        {
            int[] arrRes = dbContext.WFResolution_WFStatus.Where(p=>p.WFStatusID == status && 
                p.WFResolution.WFID == wfID).Select(p=>p.WFResolutionID).ToArray();
            return GetResolutionByWorkFlow(wfID).Where(p=>arrRes.Contains(p.ID)).ToList();
        }

        public List<WFResolution> GetList()
        {
            return dbContext.WFResolutions.Where(c => c.DeletedFlag == false).ToList();
        }

        public List<WFResolution> GetListResolutionChangeByRole(int roleId, bool isReject, int workFlow)
        {
            List<WFResolution> list = GetListResolutionChangeByRole(roleId);
            if (list != null)
            {
                switch(workFlow)
                {
                    case Constants.WORK_FLOW_JOB_REQUEST:
                        if (isReject)
                        {
                            list = list.Where(c => c.ID != Constants.RESOLUTION_NEW_ID).ToList<WFResolution>();
                        }
                        else
                        {
                            list = list.Where(c => c.ID != Constants.RESOLUTION_CANCEL_ID).ToList<WFResolution>();
                        }
                    break;
                    case Constants.WORK_FLOW_PURCHASE_REQUEST:
                        if (isReject)
                        {
                            list = list.Where(c => c.ID != Constants.PR_RESOLUTION_NEW_ID).ToList<WFResolution>();
                        }
                        else
                        {
                            list = list.Where(c => c.ID != Constants.PR_RESOLUTION_NOT_COMPLETE && c.ID != Constants.PR_RESOLUTION_REJECT && c.ID != Constants.PR_RESOLUTION_SR_MANAGER_REJECT_TO_REQUESTOR
                                && c.ID != Constants.PR_RESOLUTION_CANCEL).ToList<WFResolution>();
                        }
                    break;
                    case Constants.WORK_FLOW_PURCHASE_REQUEST_US:
                    if (isReject)
                    {
                        list = list.Where(c => c.ID != Constants.RESOLUTION_NEW_ID).ToList<WFResolution>();
                    }
                    else
                    {
                        list = list.Where(c => c.ID != Constants.PRUS_RESOLUTION_CANCELLED).ToList<WFResolution>();
                    }
                    break;
            }
            }
            return list;
        }

        public List<string> GetListResolutionName(int wfID)
        {
            List<string> list = GetResolutionByWorkFlow(wfID).Select(p => p.Name).Distinct().ToList();
            return list;
        }
    }
}