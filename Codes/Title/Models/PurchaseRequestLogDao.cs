using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CRM.Library.Common;
using CRM.Models;

namespace CRM.Models
{
    public class PurchaseRequestLogDao : BaseDao
    {
        private CommonLogDao commonDao = new CommonLogDao();

        #region Methods

        ///// <summary>
        ///// Write Update Log For When Role Manager
        ///// </summary>
        ///// <param name="newInfo"></param>
        ///// <param name="logId"></param>
        ///// <returns></returns>
        //public void WriteUpdateLogForRoleManager(PurchaseRequest newInfo, ELogAction action)
        //{
        //    bool isUpdated = false;
        //    try
        //    {
        //        // Get old info
        //        string logId = commonDao.UniqueId;
        //        commonDao.InsertMasterLog(logId, newInfo.UpdatedBy, ELogTable.PurchaseRequest.ToString(), action.ToString());
        //        PurchaseRequest oldInfo = new PurchaseRequestDao().GetByID(newInfo.ID);

        //        if ((oldInfo != null) && (newInfo != null) && (logId != null))
        //        {
        //            if (newInfo.WFStatusID != oldInfo.WFStatusID)
        //            {
        //                commonDao.InsertLogDetail(logId, "WFStatusID", "Status", new WFStatusDao().GetByID(oldInfo.WFStatusID).Name, new WFStatusDao().GetByID(newInfo.WFStatusID).Name);
        //                isUpdated = true;
        //            }
        //            if (newInfo.WFResolutionID != oldInfo.WFResolutionID)
        //            {
        //                WFResolution objRes = new ResolutionDao().GetByID(newInfo.WFResolutionID);
        //                if (objRes != null)
        //                {
        //                    commonDao.InsertLogDetail(logId, "WFResolutionID", "Resolution", oldInfo.WFResolutionID.ToString(), objRes.Name);
        //                    isUpdated = true;
        //                }
        //            }
        //            if (newInfo.AssignRole != oldInfo.AssignRole)
        //            {
        //                if (newInfo.AssignRole.HasValue)
        //                {
        //                    WFRole obj = new RoleDao().GetByID(newInfo.AssignRole.Value);
        //                    if (obj != null)
        //                    {
        //                        UserAdmin objUserAdmin = new UserAdminDao().GetById(newInfo.AssignID.Value);
        //                        if (objUserAdmin != null)
        //                        {
        //                            commonDao.InsertLogDetail(logId, "AssignID", " Forward to", oldInfo.UserAdmin.UserName + " (" + oldInfo.WFRole.Name + ")", objUserAdmin.UserName + " (" + obj.Name + ")");
        //                            isUpdated = true;
        //                        }
        //                    }
        //                }
        //            }
        //            if (isUpdated)
        //            {
        //                // Insert Key Name
        //                string key = Constants.PR_REQUEST_PREFIX + oldInfo.ID.ToString();
        //                commonDao.InsertLogDetail(logId, "EmployeeId", "Key for Update", key, null);
        //            }
        //            else
        //            {
        //                commonDao.DeleteMasterLog(logId);
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        /// <summary>
        /// Write Log For PurchaseRequest
        /// </summary>
        /// <param name="oldInfo"></param>
        /// <param name="newInfo"></param>
        /// <param name="action"></param>
        public void WriteLogForPurchaseRequest(PurchaseRequest oldInfo, PurchaseRequest newInfo, ELogAction action)
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
                        commonDao.InsertMasterLog(logId, newInfo.CreatedBy, ELogTable.PurchaseRequest.ToString(), action.ToString());
                        // Write Insert Log
                        WriteInsertLogForPurchaseRequest(newInfo, logId);
                        break;
                    case ELogAction.Update:
                        // Insert to Master Log
                        commonDao.InsertMasterLog(logId, newInfo.UpdatedBy, ELogTable.PurchaseRequest.ToString(), action.ToString());
                        // Write Update Log
                        bool isUpdated = false;
                        isUpdated = WriteUpdateLogForPurchaseRequest(newInfo, logId);
                        if (!isUpdated)
                        {
                            commonDao.DeleteMasterLog(logId);
                        }
                        break;
                    case ELogAction.Delete:
                        // Insert to Master Log
                        commonDao.InsertMasterLog(logId, newInfo.UpdatedBy, ELogTable.PurchaseRequest.ToString(), action.ToString());
                        // Write Delete Log
                        string key = newInfo.ID.ToString();
                        commonDao.InsertLogDetail(logId, "PurchaseRequest_ID", "Key for Delete", key, null);
                        break;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Write Insert Log For Employee
        /// </summary>
        /// <param name="objInfo"></param>
        /// <param name="logId"></param>
        private void WriteInsertLogForPurchaseRequest(PurchaseRequest objInfo, string logId)
        {
            try
            {
                if ((objInfo != null) && (logId != null))
                {
                    // Insert to Log Detail                       
                    commonDao.InsertLogDetail(logId, "ID", "ID", null, objInfo.ID.ToString());
                    commonDao.InsertLogDetail(logId, "WFStatusID", "Status", null, objInfo.WFStatus.Name);
                    commonDao.InsertLogDetail(logId, "WFResolutionID", "Resolution", null, objInfo.WFResolution.Name);
                    commonDao.InsertLogDetail(logId, "Requestor", "Requestor", null, objInfo.UserAdmin1.UserName);
                    commonDao.InsertLogDetail(logId, "RequestDate", "RequestDate", null, objInfo.RequestDate.ToString(Constants.DATETIME_FORMAT_VIEW));
                    if (objInfo.ExpectedDate.HasValue)
                    {
                        commonDao.InsertLogDetail(logId, "ExpectedDate", "Expected Date", null, objInfo.ExpectedDate.Value.ToString(Constants.DATETIME_FORMAT_VIEW));
                    }
                    commonDao.InsertLogDetail(logId, "SubDepartment", "Sub Department", null, objInfo.Department.DepartmentName);
                    commonDao.InsertLogDetail(logId, "Justification", "Justification", null, objInfo.Justification);
                    commonDao.InsertLogDetail(logId, "BillableToClient", "BillableToClient", null, objInfo.BillableToClient ? "Yes" : "No");
                    if (objInfo.Priority.HasValue)
                    {
                        commonDao.InsertLogDetail(logId, "Priority", "Priority", null, objInfo.Priority.Value == 1 ? "Urgent" : string.Empty);
                    }
                    if (!string.IsNullOrEmpty(objInfo.VendorName))
                    {
                        commonDao.InsertLogDetail(logId, "VendorName", "VendorName", null, objInfo.VendorName);
                    }
                    if (!string.IsNullOrEmpty(objInfo.VendorName))
                    {
                        commonDao.InsertLogDetail(logId, "VendorPhone", "VendorPhone", null, objInfo.VendorPhone);
                    }
                    if (!string.IsNullOrEmpty(objInfo.VendorName))
                    {
                        commonDao.InsertLogDetail(logId, "VendorEmail", "VendorEmail", null, objInfo.VendorEmail);
                    }
                    if (!string.IsNullOrEmpty(objInfo.VendorName))
                    {
                        commonDao.InsertLogDetail(logId, "VendorAddress", "VendorAddress", null, objInfo.VendorAddress);
                    }
                    commonDao.InsertLogDetail(logId, "MoneyType", "Money Type", null, objInfo.MoneyType == Constants.TYPE_MONEY_USD ? "USD" : "VND");
                    if (objInfo.OtherCost.HasValue)
                    {
                        commonDao.InsertLogDetail(logId, "OtherCost", "Other Cost", null, objInfo.OtherCost.Value.ToString());
                    }
                    if (!string.IsNullOrEmpty(objInfo.SaleTaxName))
                    {
                        commonDao.InsertLogDetail(logId, "SaleTaxName", "Sale Tax Name", null, objInfo.SaleTaxName);
                    }
                    if (objInfo.SaleTaxValue.HasValue)
                    {
                        commonDao.InsertLogDetail(logId, "SaleTaxValue", "Sale Tax Value", null, objInfo.SaleTaxValue.Value.ToString());
                    }
                    if (objInfo.Shipping.HasValue)
                    {
                        commonDao.InsertLogDetail(logId, "Shipping", "Shipping", null, objInfo.Shipping.Value.ToString());
                    }
                    if (objInfo.ServiceCharge.HasValue)
                    {
                        commonDao.InsertLogDetail(logId, "ServiceCharge", "Service Charge", null, objInfo.ServiceCharge.Value.ToString());
                    }
                    if (objInfo.Discount.HasValue)
                    {
                        commonDao.InsertLogDetail(logId, "Discount", "Discount", null, objInfo.Discount.Value.ToString());
                    }
                    if (!string.IsNullOrEmpty(objInfo.CCList))
                    {
                        commonDao.InsertLogDetail(logId, "CCList", "CCList", null, objInfo.CCList);
                    }
                    if (objInfo.AssignID.HasValue && objInfo.AssignRole.HasValue)
                    {
                        commonDao.InsertLogDetail(logId, "Assign", "Assign", null, objInfo.UserAdmin.UserName + " (" + objInfo.WFRole.Name + ")");
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Write Insert Log For Employee
        /// </summary>
        /// <param name="objInfo"></param>
        /// <param name="logId"></param>
        private bool WriteUpdateLogForPurchaseRequest(PurchaseRequest newInfo, string logId)
        {
            bool isUpdated = false;
            try
            {
                // Get old info
                PurchaseRequest oldInfo = new PurchaseRequestDao().GetByID(newInfo.ID);

                if ((oldInfo != null) && (newInfo != null) && (logId != null))
                {
                    if (newInfo.WFStatusID != oldInfo.WFStatusID)
                    {
                        commonDao.InsertLogDetail(logId, "WFStatusID", "Status", oldInfo.WFStatus.Name, new StatusDao().GetByID(newInfo.WFStatusID).Name);
                        isUpdated = true;
                    }
                    if (newInfo.WFResolutionID != oldInfo.WFResolutionID)
                    {
                        commonDao.InsertLogDetail(logId, "WFResolutionID", "Resolution", oldInfo.WFResolution.Name, new ResolutionDao().GetByID(newInfo.WFResolutionID).Name);
                        isUpdated = true;
                    }
                    if (newInfo.RequestDate != oldInfo.RequestDate)
                    {
                        commonDao.InsertLogDetail(logId, "RequestDate", "Request Date", oldInfo.RequestDate.ToString(Constants.DATETIME_FORMAT_VIEW), newInfo.RequestDate.ToString(Constants.DATETIME_FORMAT_VIEW));
                        isUpdated = true;
                    }
                    if (newInfo.ExpectedDate != oldInfo.ExpectedDate)
                    {
                        commonDao.InsertLogDetail(logId, "ExpectedDate", "Expected Date", oldInfo.ExpectedDate.HasValue ? oldInfo.ExpectedDate.Value.ToString(Constants.DATETIME_FORMAT_VIEW) : string.Empty, newInfo.ExpectedDate.HasValue ? newInfo.ExpectedDate.Value.ToString(Constants.DATETIME_FORMAT_VIEW) : string.Empty);
                        isUpdated = true;
                    }
                    if (newInfo.SubDepartment != oldInfo.SubDepartment)
                    {
                        commonDao.InsertLogDetail(logId, "SubDepartment", "Sub Department", oldInfo.Department.DepartmentName, new DepartmentDao().GetById(newInfo.SubDepartment).DepartmentName);
                        isUpdated = true;
                    }
                    if (newInfo.BillableToClient != oldInfo.BillableToClient)
                    {
                        commonDao.InsertLogDetail(logId, "BillableToClient", "Billable To Client", oldInfo.BillableToClient ? "Yes" : "No", newInfo.BillableToClient ? "Yes" : "No");
                        isUpdated = true;
                    }
                    if (newInfo.Priority != oldInfo.Priority)
                    {
                        commonDao.InsertLogDetail(logId, "Priority", "Priority", oldInfo.Priority.HasValue ? (oldInfo.Priority.Value == 1 ? "Urgent" : string.Empty) : string.Empty, newInfo.Priority.HasValue ? (newInfo.Priority.Value == 1 ? "Urgent" : string.Empty) : string.Empty);
                        isUpdated = true;
                    }
                    if (newInfo.VendorName != oldInfo.VendorName)
                    {
                        commonDao.InsertLogDetail(logId, "VendorName", "VendorName", oldInfo.VendorName, newInfo.VendorName);
                        isUpdated = true;
                    }
                    if (newInfo.VendorPhone != oldInfo.VendorPhone)
                    {
                        commonDao.InsertLogDetail(logId, "VendorPhone", "VendorPhone", oldInfo.VendorPhone, newInfo.VendorPhone);
                        isUpdated = true;
                    }
                    if (newInfo.VendorEmail != oldInfo.VendorEmail)
                    {
                        commonDao.InsertLogDetail(logId, "VendorEmail", "VendorEmail", oldInfo.VendorEmail, newInfo.VendorEmail);
                        isUpdated = true;
                    }
                    if (newInfo.VendorAddress != oldInfo.VendorAddress)
                    {
                        commonDao.InsertLogDetail(logId, "VendorAddress", "VendorAddress", oldInfo.VendorAddress, newInfo.VendorAddress);
                        isUpdated = true;
                    }
                    if (newInfo.MoneyType != oldInfo.MoneyType)
                    {
                        commonDao.InsertLogDetail(logId, "MoneyType", "Money Type", oldInfo.MoneyType == Constants.TYPE_MONEY_USD ? "USD" : "VND", newInfo.MoneyType == Constants.TYPE_MONEY_USD ? "USD" : "VND");
                        isUpdated = true;
                    }
                    if (newInfo.OtherCost != oldInfo.OtherCost)
                    {
                        commonDao.InsertLogDetail(logId, "OtherCost", "Other Cost", oldInfo.OtherCost.HasValue ? oldInfo.OtherCost.Value.ToString() : string.Empty, newInfo.OtherCost.HasValue ? newInfo.OtherCost.Value.ToString() : string.Empty);
                        isUpdated = true;
                    }
                    if (newInfo.SaleTaxName != oldInfo.SaleTaxName)
                    {
                        commonDao.InsertLogDetail(logId, "SaleTaxName", "SaleTaxName", oldInfo.SaleTaxName, newInfo.SaleTaxName);
                        isUpdated = true;
                    }
                    if (newInfo.SaleTaxValue != oldInfo.SaleTaxValue)
                    {
                        commonDao.InsertLogDetail(logId, "SaleTaxValue", "Sale Tax Value", oldInfo.SaleTaxValue.HasValue ? oldInfo.SaleTaxValue.Value.ToString() : string.Empty, newInfo.SaleTaxValue.HasValue ? newInfo.SaleTaxValue.Value.ToString() : string.Empty);
                        isUpdated = true;
                    }
                    if (newInfo.Shipping != oldInfo.Shipping)
                    {
                        commonDao.InsertLogDetail(logId, "Shipping", "Shipping", oldInfo.Shipping.HasValue ? oldInfo.Shipping.Value.ToString() : string.Empty, newInfo.Shipping.HasValue ? newInfo.Shipping.Value.ToString() : string.Empty);
                        isUpdated = true;
                    }
                    if (newInfo.ServiceCharge != oldInfo.ServiceCharge)
                    {
                        commonDao.InsertLogDetail(logId, "ServiceCharge", "ServiceCharge", oldInfo.ServiceCharge.HasValue ? oldInfo.ServiceCharge.Value.ToString() : string.Empty, newInfo.ServiceCharge.HasValue ? newInfo.ServiceCharge.Value.ToString() : string.Empty);
                        isUpdated = true;
                    }
                    if (newInfo.Discount != oldInfo.Discount)
                    {
                        commonDao.InsertLogDetail(logId, "Discount", "Discount", oldInfo.Discount.HasValue ? oldInfo.Discount.Value.ToString() : string.Empty, newInfo.Discount.HasValue ? newInfo.Discount.Value.ToString() : string.Empty);
                        isUpdated = true;
                    }
                    if (newInfo.CCList != oldInfo.CCList)
                    {
                        commonDao.InsertLogDetail(logId, "CCList", "CCList", oldInfo.CCList, newInfo.CCList);
                        isUpdated = true;
                    }
                    if (newInfo.AssignRole != oldInfo.AssignRole || newInfo.AssignID != oldInfo.AssignID)
                    {
                        commonDao.InsertLogDetail(logId, "Assign", "Assign", oldInfo.UserAdmin.UserName + " (" + oldInfo.WFRole.Name + ")", ((newInfo.AssignID.HasValue ? new UserAdminDao().GetById(newInfo.AssignID.Value).UserName : string.Empty)
                            + " (" + (newInfo.AssignRole.HasValue ? new RoleDao().GetByID(newInfo.AssignRole.Value).Name : string.Empty) + ")"));
                        isUpdated = true;
                    }
                    if (newInfo.USAccounting != oldInfo.USAccounting)
                    {
                        commonDao.InsertLogDetail(logId, "USAccounting", "USAccounting", oldInfo.USAccounting.HasValue ? new UserAdminDao().GetById(oldInfo.USAccounting.Value).UserName : string.Empty, newInfo.USAccounting.HasValue ? new UserAdminDao().GetById(newInfo.USAccounting.Value).UserName : string.Empty);
                        isUpdated = true;
                    }
                    if (isUpdated)
                    {
                        // Insert Key Name
                        string key = Constants.PER_REVIEW_EFORM_MASTER_PREFIX + oldInfo.ID.ToString();
                        commonDao.InsertLogDetail(logId, "ID", "Key for Update", key, null);
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
        /// Write Insert Log For Employee
        /// </summary>
        /// <param name="objInfo"></param>
        /// <param name="logId"></param>
        public bool WriteLogForSetUpApproval(PurchaseRequest newInfo, List<PurchaseRequestApproval> listApproval, bool insertApproval)
        {
            bool isUpdated = false;
            try
            {
                MasterLog objMasterLog = new MasterLog();

                string logId = commonDao.UniqueId;

                commonDao.InsertMasterLog(logId, newInfo.UpdatedBy, ELogTable.PurchaseRequest.ToString(), ELogAction.Update.ToString());
                // Get old info
                PurchaseRequest oldInfo = new PurchaseRequestDao().GetByID(newInfo.ID);

                if ((oldInfo != null) && (newInfo != null) && (logId != null))
                {
                    if (newInfo.WFResolutionID != oldInfo.WFResolutionID)
                    {
                        commonDao.InsertLogDetail(logId, "WFResolutionID", "Resolution", oldInfo.WFResolution.Name, new ResolutionDao().GetByID(newInfo.WFResolutionID).Name);
                        isUpdated = true;
                    }
                    if (newInfo.WFStatusID != oldInfo.WFStatusID)
                    {
                        commonDao.InsertLogDetail(logId, "WFStatusID", "Status", oldInfo.WFStatus.Name, new StatusDao().GetByID(newInfo.WFStatusID).Name);
                        isUpdated = true;
                    }
                    if (insertApproval)
                    {
                        foreach (PurchaseRequestApproval item in listApproval)
                        {
                            commonDao.InsertLogDetail(logId, "Approval", "Approval", string.Empty, new UserAdminDao().GetById(item.ApproverId).UserName +
                                " (" + new RoleDao().GetByID(item.ApproverGroup).Name + ")");
                            isUpdated = true;
                        }
                    }
                    else
                    {
                        if (newInfo.AssignRole != oldInfo.AssignRole || newInfo.AssignID != oldInfo.AssignID)
                        {
                            commonDao.InsertLogDetail(logId, "Assign", "Assign", oldInfo.UserAdmin.UserName + " (" + oldInfo.WFRole.Name + ")", ((newInfo.AssignID.HasValue ? new UserAdminDao().GetById(newInfo.AssignID.Value).UserName : string.Empty)
                                + " (" + (newInfo.AssignRole.HasValue ? new RoleDao().GetByID(newInfo.AssignRole.Value).Name : string.Empty) + ")"));
                            isUpdated = true;
                        }
                    }
                    if (isUpdated)
                    {
                        // Insert Key Name
                        string key = Constants.PER_REVIEW_EFORM_MASTER_PREFIX + oldInfo.ID.ToString();
                        commonDao.InsertLogDetail(logId, "ID", "Key for Update", key, null);
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
            return isUpdated;
        }

        /// <summary>
        /// Write Insert Log For Employee
        /// </summary>
        /// <param name="objInfo"></param>
        /// <param name="logId"></param>
        public bool WriteLogForPurchasingConfirm(PurchaseRequest newInfo)
        {
            bool isUpdated = false;
            try
            {
                MasterLog objMasterLog = new MasterLog();

                string logId = commonDao.UniqueId;

                commonDao.InsertMasterLog(logId, newInfo.UpdatedBy, ELogTable.PurchaseRequest.ToString(), ELogAction.Update.ToString());
                // Get old info
                PurchaseRequest oldInfo = new PurchaseRequestDao().GetByID(newInfo.ID);

                if ((oldInfo != null) && (newInfo != null) && (logId != null))
                {
                    if (newInfo.WFResolutionID != oldInfo.WFResolutionID)
                    {
                        commonDao.InsertLogDetail(logId, "WFResolutionID", "Resolution", oldInfo.WFResolution.Name, new ResolutionDao().GetByID(newInfo.WFResolutionID).Name);
                        isUpdated = true;
                    }
                    if (newInfo.AssignRole != oldInfo.AssignRole || newInfo.AssignID != oldInfo.AssignID)
                    {
                        commonDao.InsertLogDetail(logId, "Assign", "Assign", oldInfo.UserAdmin.UserName + " (" + oldInfo.WFRole.Name + ")", ((newInfo.AssignID.HasValue ? new UserAdminDao().GetById(newInfo.AssignID.Value).UserName : string.Empty)
                            + " (" + (newInfo.AssignRole.HasValue ? new RoleDao().GetByID(newInfo.AssignRole.Value).Name : string.Empty) + ")"));
                        isUpdated = true;
                    }
                    if (!string.IsNullOrEmpty(newInfo.PurchaseAppoval) && newInfo.PurchaseAppoval != oldInfo.PurchaseAppoval)
                    {
                        commonDao.InsertLogDetail(logId, "PurchaseAppoval", "Purchase Appoval", oldInfo.PurchaseAppoval, newInfo.PurchaseAppoval);
                        isUpdated = true;
                    }
                    if (!string.IsNullOrEmpty(newInfo.PaymentAppoval) && newInfo.PaymentAppoval != oldInfo.PaymentAppoval)
                    {
                        commonDao.InsertLogDetail(logId, "PaymentAppoval", "Payment Appoval", oldInfo.PaymentAppoval, newInfo.PaymentAppoval);
                        isUpdated = true;
                    }
                    if (isUpdated)
                    {
                        // Insert Key Name
                        string key = Constants.PER_REVIEW_EFORM_MASTER_PREFIX + oldInfo.ID.ToString();
                        commonDao.InsertLogDetail(logId, "ID", "Key for Update", key, null);
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
            return isUpdated;
        }

        #endregion
    }
}