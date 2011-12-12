using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CRM.Library.Common;
using System.Data.Common;
using System.Web.UI.WebControls;

namespace CRM.Models
{
    /// <summary>
    /// Purchase request dao
    /// </summary>
    /// <remarks></remarks>
    public class PurchaseRequestDao :BaseDao
    {

        /// <summary>
        /// Gets the list assign.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <param name="workflow">The workflow.</param>
        /// <returns>List<sp_GetListAssignByResolutionIdResult></returns>
        /// <remarks></remarks>
        public List<sp_GetListAssignByResolutionIdResult> GetListAssign(int id,int workflow)
        {
            return dbContext.sp_GetListAssignByResolutionId(id, workflow).ToList<sp_GetListAssignByResolutionIdResult>();
        }

        public List<sp_GetListUserAdminResult> GetListAssign(int id, int workflow, bool isHold)
        {
            return dbContext.sp_GetListUserAdmin(id, workflow, isHold).ToList<sp_GetListUserAdminResult>();
        }

        private List<sp_GetListUSAccountingResult> GetListUSAccounting()
        {
            return dbContext.sp_GetListUSAccounting(Constants.PR_US_ACCOUNTING).ToList();
        }

        public List<ListItem> GetSelectedListUSAccounting()
        {
            List<ListItem> list = new List<ListItem>();
            foreach (sp_GetListUSAccountingResult item in GetListUSAccounting())
            {
                list.Add(new ListItem(item.UserName + " (US Accounting)", item.UserAdminId.ToString()));
            }
            return list;
        }

        /// <summary>
        /// Get approval assign list 
        /// </summary>
        /// <param name="request">purchase request id</param>
        /// <returns>List<sp_GetListApprovalAssignResult></returns>
        public List<sp_GetListApprovalAssignResult> GetListApprovalAssign(int request)
        {
            return dbContext.sp_GetListApprovalAssign(request).ToList<sp_GetListApprovalAssignResult>();
        }

        /// <summary>
        /// Get list resolution by role
        /// </summary>
        /// <param name="role">int</param>
        /// <returns>List<sp_GetResolutionByRoleResult></returns>
        public List<sp_GetResolutionByRoleResult> GetResolutionByRole(int role)
        {
            return dbContext.sp_GetResolutionByRole(role).ToList<sp_GetResolutionByRoleResult>();
        }

        /// <summary>
        /// Get list assign by role
        /// </summary>
        /// <param name="roleID">role id</param>
        /// <returns>List<sp_GetListAssignByRoleResult></returns>
        public List<sp_GetListAssignByRoleResult> GetListByRole(int roleID)
        {
            return dbContext.sp_GetListAssignByRole(roleID).ToList<sp_GetListAssignByRoleResult>();
        }

        /// <summary>
        /// Get sub total by purchase id
        /// </summary>
        /// <param name="purchaseID">int</param>
        /// <returns>double</returns>
        public double? GetSubTotalByPurchaseID(int purchaseID)
        {
            return dbContext.GetSubTotalItem(purchaseID);
        }
        /// <summary>
        /// Get PurchaseRequestApproval list by request id and IsImmediate
        /// </summary>
        /// <param name="roleID"></param>
        /// <returns></returns>
        public List<PurchaseRequestApproval> GetPurchaseRequestApprovalImmediately(int requestID)
        {
            return dbContext.PurchaseRequestApprovals.Where(p => p.RequestID == requestID && p.IsImmediate == true).ToList<PurchaseRequestApproval>();
        }
        /// <summary>
        /// Get comment by purchase id
        /// </summary>
        /// <param name="purchaseID">int</param>
        /// <returns>PurchaseComment</returns>
        public PurchaseComment GetCommentByPurchaseID(int purchaseID)
        {
            return dbContext.PurchaseComments.Where(p => p.RequestID == purchaseID).FirstOrDefault<PurchaseComment>();
        }

        /// <summary>
        /// Get list items by purchase id
        /// </summary>
        /// <param name="purchaseID">int</param>
        /// <returns>List<PurchaseItem></returns>
        public List<PurchaseItem> GetListItemsByPurchaseID(int purchaseID)
        {
            return dbContext.PurchaseItems.Where(p => p.RequestID == purchaseID).ToList<PurchaseItem>();
        }

        public string GetLastInvolveIdByRole(PurchaseRequest pr, string roleId)
        {
            string result = "";
            string[] arrInvoleId = string.IsNullOrEmpty(pr.InvolveID) ? new string[0] :
                pr.InvolveID.Split(new char[]{Constants.SEPARATE_INVOLVE_CHAR},
                    StringSplitOptions.RemoveEmptyEntries);
            string[] arrInvoleRole = string.IsNullOrEmpty(pr.InvolveRole) ? new string[0] :
                pr.InvolveRole.Split(new char[]{Constants.SEPARATE_INVOLVE_CHAR},
                    StringSplitOptions.RemoveEmptyEntries);
            for (int i = arrInvoleRole.Length - 1; i >= 0; i--)
                if (arrInvoleRole[i].Equals(roleId))
                    result = arrInvoleId[i];
            return result;
        }

        /// <summary>
        /// Get By ID
        /// </summary>
        /// <param name="id">id of purchase request</param>
        /// <returns>PurchaseRequest</returns>
        public PurchaseRequest GetByID(int id)
        {
            return dbContext.PurchaseRequests.Where(p => p.ID == id).FirstOrDefault<PurchaseRequest>();
        }

        /// <summary>
        /// Get purchase item by purchase request id
        /// </summary>
        /// <param name="id">purchase request id</param>
        /// <returns>PurchaseItem</returns>
        public PurchaseItem GetItemByID(int id)
        {
            return dbContext.PurchaseItems.Where(p => p.ID == id).FirstOrDefault<PurchaseItem>();
        }

        /// <summary>
        /// Get purchase comment by id
        /// </summary>
        /// <param name="id">id of purchase comment</param>
        /// <returns>PurchaseComment</returns>
        public PurchaseComment GetCommentByID(int id)
        {
            return dbContext.PurchaseComments.Where(p => p.ID == id).FirstOrDefault<PurchaseComment>();
        }

        /// <summary>
        /// Get By ID incluse some relative information
        /// </summary>
        /// <param name="id">id of purchase request</param>
        /// <returns>sp_GetPurchaseRequestResult</returns>
        public sp_GetPurchaseRequestResult GetPurchaseRequestByID(string id)
        {
            return dbContext.sp_GetPurchaseRequest(id, 0, 0, 0, 0, null, 0, 0, 0, 0, null, null,"","", null)
                .Where(p => p.ID.ToString() == id).FirstOrDefault<sp_GetPurchaseRequestResult>();
        }

        /// <summary>
        /// Get By ID
        /// </summary>
        /// <param name="id">id of purchase request</param>
        /// <returns>PurchaseRequest</returns>
        public PurchaseRequest GetPurchaseRequestByID(int id)
        {
            return dbContext.PurchaseRequests.Where(c => c.ID == id).FirstOrDefault<PurchaseRequest>();
        }
        
        /// <summary>
        /// Get all items of purchase request
        /// </summary>
        /// <param name="id">int</param>
        /// <returns>List<PurchaseItem></returns>
        public List<PurchaseItem> GetPurchaseRequestItems(int id)
        {
            return dbContext.PurchaseItems.Where(c => c.RequestID == id).ToList<PurchaseItem>();
        }

        /// <summary>
        /// Get list invoice of purchase request
        /// </summary>
        /// <param name="id">int</param>
        /// <returns>List<PurchaseInvoice></returns>
        public List<PurchaseInvoice> GetPurchaseInvoice(int id)
        {
            return dbContext.PurchaseInvoices.Where(c => c.RequestID == id).ToList<PurchaseInvoice>();
        }

        /// <summary>
        /// Insert purchase request
        /// </summary>
        /// <param name="objUI">PurchaseRequest</param>
        /// <returns>Message</returns>
        public Message Insert(PurchaseRequest objUI)
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
                    objUI.DeleteFlag = false;
                    dbContext.PurchaseRequests.InsertOnSubmit(objUI);
                    dbContext.SubmitChanges();
                    //Write Log
                    new PurchaseRequestLogDao().WriteLogForPurchaseRequest(null, objUI, ELogAction.Insert);
                    msg = new Message(MessageConstants.I0001, MessageType.Info, "Purchase Request '" + Constants.PR_REQUEST_PREFIX + objUI.ID + "'", "added");
                    trans.Commit();
                }
            }
            catch
            {
                if (trans != null) trans.Rollback();
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
        /// Update purchase item
        /// </summary>
        /// <param name="objUI">PurchaseItem</param>
        private void UpdatePurchaseItem(PurchaseItem objUI)
        {
            PurchaseItem objItemDb = GetItemByID(objUI.ID);
            if (objItemDb != null)
            {
                objItemDb.ItemName = objUI.ItemName;
                objItemDb.Quantity = objUI.Quantity;
                objItemDb.Price = objUI.Price;
                objItemDb.TotalPrice = objUI.TotalPrice;
                dbContext.SubmitChanges();
            }
        }

        /// <summary>
        /// Update purchase comment
        /// </summary>
        /// <param name="objUI">PurchaseComment</param>
        private void UpdatePurchaseComment(PurchaseComment objUI)
        {
            PurchaseComment objCommentDb = GetCommentByID(objUI.ID);
            if (objCommentDb != null)
            {
                if (!string.IsNullOrEmpty(objUI.Contents))
                {
                    objCommentDb.Contents = objUI.Contents;
                    dbContext.SubmitChanges();
                }
                else
                {
                    dbContext.PurchaseComments.DeleteOnSubmit(objUI);
                    dbContext.SubmitChanges();
                }
            }
        }

        /// <summary>
        /// Get list of old purchase items
        /// </summary>
        /// <param name="requestID">int</param>
        /// <returns>List<PurchaseItem></returns>
        private List<PurchaseItem> GetListOldPurchaseItem(int requestID)
        {
            return dbContext.PurchaseItems.Where(c => c.RequestID == requestID).ToList<PurchaseItem>();
        }

        /// <summary>
        /// Delete purchase item
        /// </summary>
        /// <param name="objUI">PurchaseItem</param>
        private void DeletePurchaseItem(PurchaseItem objUI)
        {
            if (objUI != null)
            {
                // Get current purchase item in dbContext
                PurchaseItem objDb = GetItemByID(objUI.ID);

                if (objDb != null)
                {
                    dbContext.PurchaseItems.DeleteOnSubmit(objDb);
                    // Submit changes to dbContext
                    dbContext.SubmitChanges();
                }
            }
        }

        private void InsertMultiApproval(List<PurchaseRequestApproval> listApproval)
        {
            dbContext.PurchaseRequestApprovals.InsertAllOnSubmit(listApproval);
            dbContext.SubmitChanges();
        }

        /// <summary>
        /// Update setup approval
        /// </summary>
        /// <param name="objUI">PurchaseRequest</param>
        /// <returns>Message</returns>
        public Message UpdateForSetupApproval(PurchaseRequest objUI,List<PurchaseRequestApproval> listApproval,bool insertApproval)
        {
            Message msg = null;
            try
            {
               
                if (objUI != null)
                {
                    PurchaseRequest objDb = GetByID(objUI.ID);
                    if (objDb != null)
                    {
                        if (objDb.UpdateDate.ToString() == objUI.UpdateDate.ToString())
                        {
                            if (insertApproval)
                            {
                                InsertMultiApproval(listApproval);
                            }
                            new PurchaseRequestLogDao().WriteLogForSetUpApproval(objUI, listApproval, insertApproval);
                            //new PurchaseRequestLogDao().WriteUpdateLogForRoleManager(objUI, ELogAction.Update);
                            objDb.WFResolutionID = objUI.WFResolutionID;
                            objDb.AssignID = objUI.AssignID;
                            objDb.AssignRole = objUI.AssignRole;
                            objDb.InvolveID = objUI.InvolveID;
                            objDb.InvolveRole = objUI.InvolveRole;
                            objDb.InvolveDate = objUI.InvolveDate;
                            objDb.InvolveResolution = objUI.InvolveResolution;
                            objDb.UpdateDate = DateTime.Now;
                            dbContext.SubmitChanges();

                            msg = new Message(MessageConstants.I0001, MessageType.Info, "Purchase Request '" + Constants.PR_REQUEST_PREFIX + objUI.ID + "'", "updated");
                        }
                        else
                        {
                            msg = new Message(MessageConstants.E0025, MessageType.Error, "Purchase Request '" + Constants.PR_REQUEST_PREFIX + objUI.ID + "'");
                        }
                }
                }
            }
            catch
            {
                msg = new Message(MessageConstants.E0007, MessageType.Error);
            }

            return msg;
        }

        /// <summary>
        /// Update setup approval
        /// </summary>
        /// <param name="objUI">PurchaseRequest</param>
        /// <returns>Message</returns>
        public Message UpdateForUSAccouting(PurchaseRequest objUI)
        {
            Message msg = null;
            try
            {

                if (objUI != null)
                {
                    PurchaseRequest objDb = GetByID(objUI.ID);
                    if (objDb != null)
                    {
                        if (objDb.UpdateDate.ToString() == objUI.UpdateDate.ToString())
                        {
                            new PurchaseRequestLogDao().WriteLogForPurchasingConfirm(objUI);
                            //new PurchaseRequestLogDao().WriteUpdateLogForRoleManager(objUI, ELogAction.Update);
                            objDb.WFResolutionID = objUI.WFResolutionID;
                            objDb.AssignID = objUI.AssignID;
                            objDb.AssignRole = objUI.AssignRole;
                            objDb.InvolveID = objUI.InvolveID;
                            objDb.InvolveRole = objUI.InvolveRole;
                            objDb.InvolveDate = objUI.InvolveDate;
                            objDb.InvolveResolution = objUI.InvolveResolution;
                            objDb.USAccounting = objUI.USAccounting;
                            objDb.WFStatusID = objUI.WFStatusID;
                            objDb.UpdateDate = DateTime.Now;
                            dbContext.SubmitChanges();

                            msg = new Message(MessageConstants.I0001, MessageType.Info, "Purchase Request '" + Constants.PR_REQUEST_PREFIX + objUI.ID + "'", "updated");
                        }
                        else
                        {
                            msg = new Message(MessageConstants.E0025, MessageType.Error, "Purchase Request '" + Constants.PR_REQUEST_PREFIX + objUI.ID + "'");
                        }
                    }
                }
            }
            catch
            {
                msg = new Message(MessageConstants.E0007, MessageType.Error);
            }

            return msg;
        }

        /// <summary>
        /// Update approval with comment
        /// </summary>
        /// <param name="objUI">PurchaseRequest</param>
        /// <param name="objComment">PurchaseComment</param>
        /// <returns>Message</returns>
        public Message UpdateForApproval(PurchaseRequest objUI, PurchaseComment objComment)
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
                    PurchaseRequest objDb = GetByID(objUI.ID);
                    if (objDb != null)
                    {
                        #region Insert Comment
                        if (objComment != null && !string.IsNullOrEmpty(objComment.Contents))
                        {
                            PurchaseRequestCommentDao commentDao = new PurchaseRequestCommentDao();
                            commentDao.Insert(objComment);
                        }
                        #endregion
                        // Check valid update date
                        if (IsValidUpdateDate(objUI, objDb, out msg))
                        {
                            //new PurchaseRequestLogDao().WriteUpdateLogForRoleManager(objUI, ELogAction.Update);
                            objDb.WFResolutionID = objUI.WFResolutionID;
                            objDb.AssignID = objUI.AssignID;
                            objDb.AssignRole = objUI.AssignRole;
                            objDb.InvolveID = objUI.InvolveID;
                            objDb.InvolveRole = objUI.InvolveRole;
                            objDb.InvolveDate = objUI.InvolveDate;
                            objDb.UpdateDate = DateTime.Now;
                            dbContext.SubmitChanges();

                            msg = new Message(MessageConstants.I0001, MessageType.Info, " Purchase Request '" + Constants.PR_REQUEST_PREFIX + objUI.ID + "'", "updated");
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
        /// Update purchase request
        /// </summary>
        /// <param name="objUI">PurchaseRequest</param>
        /// <param name="oldItemList">List of old purchase items</param>
        /// <param name="newItemList">List of new purchase items</param>
        /// <returns>Message</returns>
        public Message Update(PurchaseRequest objUI,List<PurchaseItem> oldItemList,List<PurchaseItem> newItemList)
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
                    // Get current group in dbContext
                    PurchaseRequest objDb = GetByID(objUI.ID);

                    if (objDb != null)
                    {
                        if (objDb.UpdateDate.ToString() == objUI.UpdateDate.ToString())
                        {
                            new PurchaseRequestLogDao().WriteLogForPurchaseRequest(null, objUI, ELogAction.Update);
                            #region Purchase Request
                            // Update info by objUI
                            //objDb.RequestDate = objUI.RequestDate;
                            objDb.ExpectedDate = objUI.ExpectedDate;
                            objDb.CCList = objUI.CCList;
                            objDb.WFStatusID = objUI.WFStatusID;
                            objDb.PaymentID = objUI.PaymentID;
                            objDb.WFResolutionID = objUI.WFResolutionID;
                            objDb.SubDepartment = objUI.SubDepartment;
                            objDb.Justification = objUI.Justification;
                            objDb.BillableToClient = objUI.BillableToClient;
                            objDb.VendorName = objUI.VendorName;
                            objDb.VendorPhone = objUI.VendorPhone;
                            objDb.VendorEmail = objUI.VendorEmail;
                            objDb.VendorAddress = objUI.VendorAddress;
                            objDb.MoneyType = objUI.MoneyType;
                            objDb.SaleTaxName = objUI.SaleTaxName;
                            objDb.SaleTaxValue = objUI.SaleTaxValue;
                            objDb.OtherCost = objUI.OtherCost;
                            objDb.Shipping = objUI.Shipping;
                            objDb.Discount = objUI.Discount;
                            objDb.ServiceCharge = objUI.ServiceCharge;
                            objDb.AssignID = objUI.AssignID;
                            objDb.AssignRole = objUI.AssignRole;
                            objDb.InvolveID = objUI.InvolveID;
                            objDb.InvolveRole = objUI.InvolveRole;
                            objDb.InvolveDate = objUI.InvolveDate;
                            objDb.InvolveResolution = objUI.InvolveResolution;
                            objDb.UpdateDate = DateTime.Now;
                            objDb.UpdatedBy = objUI.UpdatedBy;
                            objDb.Priority = objUI.Priority;
                            #endregion

                            #region Purchase Item
                            if (oldItemList.Count > 0)
                            {
                                string oldItemID = string.Empty;
                                foreach (PurchaseItem oldItem in oldItemList) //case Update Old item
                                {
                                    UpdatePurchaseItem(oldItem);
                                    oldItemID += oldItem.ID + ",";
                                }
                                foreach (PurchaseItem deleteItem in GetListOldPurchaseItem(objDb.ID))
                                {
                                    if (!oldItemID.TrimEnd(',').Split(',').Contains(deleteItem.ID.ToString()))
                                    {
                                        DeletePurchaseItem(deleteItem);
                                    }
                                }

                            }
                            if (newItemList.Count > 0)
                            {
                                InsertMultiItem(newItemList);
                            }
                            #endregion
                            // Submit changes to dbContext
                            dbContext.SubmitChanges();

                            // Show success message
                            msg = new Message(MessageConstants.I0001, MessageType.Info, "Purchase Request '" + Constants.PR_REQUEST_PREFIX + objUI.ID + "'", "updated");
                            trans.Commit();
                        }
                        else
                        {
                            msg = new Message(MessageConstants.E0025, MessageType.Error, "Purchase Request '" + Constants.PR_REQUEST_PREFIX + objUI.ID + "'");
                            trans.Rollback();
                        }
                    }
                }
            }
            catch
            {
                if (trans != null) trans.Rollback();
                // Show system error
                msg = new Message(MessageConstants.E0007, MessageType.Error);
            }

            return msg;
        }

        /// <summary>
        /// Item purchase item
        /// </summary>
        /// <param name="objUI">PurchaseItem</param>
        public void InsertItem(PurchaseItem objUI)
        {
            try
            {
                if (objUI != null)
                {
                    dbContext.PurchaseItems.InsertOnSubmit(objUI);
                    dbContext.SubmitChanges();
                }
            }
            catch
            {
            }
        }

        /// <summary>
        /// Insert multi item
        /// </summary>
        /// <param name="listObj">list of purchase items</param>
        public void InsertMultiItem(List<PurchaseItem> listObj)
        {
            try
            {
                if (listObj.Count >0)
                {
                    dbContext.PurchaseItems.InsertAllOnSubmit(listObj);
                    dbContext.SubmitChanges();
                }
            }
            catch
            {
            }
        }

        

        public bool IsRequestor(PurchaseRequest pr, int userAdminID, int loginRole)
        {
            if (loginRole == Constants.PR_REQUESTOR_ID && pr.Requestor == userAdminID)
                return true;
            return false;
        }

        public bool IsAssigned(PurchaseRequest pr, int userAdminID, int loginRole)
        {
            if ((pr.AssignID.HasValue && pr.AssignID.Value == userAdminID &&
                        pr.AssignRole.HasValue && pr.AssignRole.Value == loginRole) )
                return true;
            return false;
        }

        public bool IsInInvolveList(PurchaseRequest pr, int userAdminID, int loginRole)
        {
            //char[] splitChars = new char[] { Constants.SEPARATE_INVOLVE_CHAR };
            //int tmpInt = 0;
            //int[] arrInvolveID = string.IsNullOrEmpty(pr.InvolveID) ? null :
            //    pr.InvolveID.Split(splitChars, StringSplitOptions.RemoveEmptyEntries).
            //    Select(p => int.TryParse(p, out tmpInt) ? tmpInt : 0).ToArray();
            //int[] arrInvoleRole = string.IsNullOrEmpty(pr.InvolveRole) ? null :
            //    pr.InvolveRole.Split(splitChars, StringSplitOptions.RemoveEmptyEntries).
            //    Select(p => int.TryParse(p, out tmpInt) ? tmpInt : 0).ToArray();
            //for (int i = 0; arrInvoleRole != null && arrInvolveID != null && i < arrInvolveID.Length; i++)
            //    if (arrInvolveID[i] == userAdminID && arrInvoleRole[i] == loginRole)
            //        return true;
            //return false;
            int? result = dbContext.check_in_list_int(pr.InvolveID, userAdminID, 
                pr.InvolveRole, loginRole, Constants.SEPARATE_INVOLVE_SIGN);
            return result.HasValue ? result.Value == 1 ? true : false : false;
            
        }

        public bool IsInCCList(PurchaseRequest pr, string loginName)
        {
            int? result = dbContext.check_in_list_string(pr.CCList, loginName, Constants.SEPARATE_USER_ADMIN_USERNAME);
            return result.HasValue ? result.Value == 1 ? true : false : false;
            //if (!string.IsNullOrEmpty(pr.CCList) && pr.CCList.Split(';').Contains(loginName))
            //    return true;
            //return false;
        }

        public bool HasViewPermision(int prID, string loginName, int loginRole)
        {
            try
            {
                PurchaseRequest pr = GetByID(prID);
                if (!string.IsNullOrEmpty(loginName) && loginRole != 0 && pr !=null)
                {
                    UserAdmin userAdmin = new UserAdminDao().GetByUserName(loginName);
                    if (userAdmin == null)
                        return false;
                    int loginID = userAdmin.UserAdminId;
                    if (new GroupDao().HasPermisionOnModule(loginID, (int)Permissions.ViewAllPR, (int)Modules.PurchaseRequest))
                        return true;
                    return IsRequestor(pr, loginID, loginRole) || IsAssigned(pr, loginID, loginRole) ||
                        IsInCCList(pr, loginName) || IsInInvolveList(pr, loginID, loginRole);
                    //return IsInCCList(pr, loginName) || IsInInvolveList(pr, loginID, loginRole);
                }
                return false;
            }
            catch
            {
                return false;
            }
        }

        public bool HasEditPermision(int prID, string loginName, int loginRole)
        {
            try
            {
                PurchaseRequest pr = GetByID(prID);
                if (pr.WFStatusID == Constants.STATUS_CLOSE)
                    return false;
                if (!string.IsNullOrEmpty(loginName) && loginRole != 0 && pr != null)
                {
                    UserAdmin userAdmin = new UserAdminDao().GetByUserName(loginName);
                    if (userAdmin == null)
                        return false;
                    int loginID = userAdmin.UserAdminId;
                    if (new GroupDao().HasPermisionOnModule(loginID, (int)Permissions.ForceEdit, (int)Modules.PurchaseRequest))
                        return true;
                    return IsAssigned(pr, loginID, loginRole) && loginRole == Constants.PR_REQUESTOR_ID;
                }
                return false;
            }
            catch
            {
                return false;
            }
        }
        public bool HasEditPermisionUS(int prID, string loginName, int loginRole)
        {
            try
            {
                PurchaseRequest pr = GetByID(prID);
                if (pr.WFStatusID == Constants.STATUS_CLOSE)
                    return false;
                if (!string.IsNullOrEmpty(loginName) && loginRole != 0 && pr != null)
                {
                    UserAdmin userAdmin = new UserAdminDao().GetByUserName(loginName);
                    if (userAdmin == null)
                        return false;
                    int loginID = userAdmin.UserAdminId;
                    if (new GroupDao().HasPermisionOnModule(loginID, (int)Permissions.ForceEdit, (int)Modules.PurchaseRequestUS))
                        return true;
                    return IsAssigned(pr, loginID, loginRole) && loginRole == Constants.PR_REQUESTOR_ID_US;
                }
                return false;
            }
            catch
            {
                return false;
            }
        }
        /// <summary>
        /// Get list purchase request
        /// </summary>
        /// <param name="text"></param>
        /// <param name="department"></param>
        /// <param name="subdepartment"></param>
        /// <param name="assignID"></param>
        /// <param name="statusId"></param>
        /// <param name="loginName"></param>
        /// <param name="loginRole"></param>
        /// <returns></returns>
        public List<sp_GetPurchaseRequestResult> GetList(string text,int department, int subdepartment,
            int requestorId, int assignID, string resolutionId, int loginId, int loginRole, 
            int statusID, bool? isUsPurchasing, string fromDate="",string toDate="")
        {
            if (isUsPurchasing.HasValue)
            {
                if ((!isUsPurchasing.Value && new GroupDao().HasPermisionOnModule(loginId, (int)Permissions.ViewAllPR, (int)Modules.PurchaseRequest)) ||
                    (isUsPurchasing.Value && new GroupDao().HasPermisionOnModule(loginId, (int)Permissions.ViewAllPR, (int)Modules.PurchaseRequestUS)))
                {
                    loginId = 0;
                    loginRole = 0;
                }
            }
            var result = dbContext.sp_GetPurchaseRequest(text, department, subdepartment, requestorId, assignID, resolutionId, statusID,
                loginId, loginRole, Constants.PR_REQUESTOR_ID, Constants.SEPARATE_USER_ADMIN_USERNAME,
                Constants.SEPARATE_INVOLVE_SIGN,fromDate,toDate, isUsPurchasing).ToList();
            //if (loginRole != 0)
            //    result = result.Where(p => HasViewPermision(p.ID, loginName, loginRole)).ToList();
            return result;
        }

        private string SetAssignName(int resolution, int status, string assignName)
        {
            if ((resolution == Constants.PR_RESOLUTION_COMPLETE_ID || resolution == Constants.PR_RESOLUTION_NOT_COMPLETE ||
                resolution == Constants.PR_RESOLUTION_CANCEL) && (status == Constants.STATUS_CLOSE))
            {
                assignName = string.Empty;
            }

            return assignName;
        }

        /// <summary>
        /// Sort Product
        /// </summary>
        /// <param name="list">List<sp_GetPurchaseRequestResult></param>
        /// <param name="sortColumn">string</param>
        /// <param name="sortOrder">string</param>
        /// <returns>List<sp_GetPurchaseRequestResult></returns>
        public List<sp_GetPurchaseRequestResult> Sort(List<sp_GetPurchaseRequestResult> list, string sortColumn, string sortOrder)
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
                         delegate(sp_GetPurchaseRequestResult m1, sp_GetPurchaseRequestResult m2)
                         { return m1.ID.CompareTo(m2.ID) * order; });
                    break;                

                case "RequestDate":
                    list.Sort(
                         delegate(sp_GetPurchaseRequestResult m1, sp_GetPurchaseRequestResult m2)
                         { return m1.RequestDate.CompareTo(m2.RequestDate) * order; });
                    break;
                
                case "DepartmentName":
                    list.Sort(
                         delegate(sp_GetPurchaseRequestResult m1, sp_GetPurchaseRequestResult m2)
                         { return m1.SubDepartment.CompareTo(m2.SubDepartment) * order; });
                    break;

                case "ExpectedDate":
                    list.Sort(
                         delegate(sp_GetPurchaseRequestResult m1, sp_GetPurchaseRequestResult m2)
                         {
                             if (m1.ExpectedDate.HasValue && m2.ExpectedDate.HasValue)
                                 return m1.ExpectedDate.Value.CompareTo(m2.ExpectedDate.Value) * order;
                             else
                                 return m1.ID.CompareTo(m2.ID) * order;

                         });
                    break;

                case "StatusName":
                    list.Sort(
                         delegate(sp_GetPurchaseRequestResult m1, sp_GetPurchaseRequestResult m2)
                         { return m1.StatusName.CompareTo(m2.StatusName) * order; });
                    break;

                case "RequestorName":
                    list.Sort(
                         delegate(sp_GetPurchaseRequestResult m1, sp_GetPurchaseRequestResult m2)
                         { return m1.RequestorName.CompareTo(m2.RequestorName) * order; });
                    break;

                case "AssignName":
                    list.Sort(
                         delegate(sp_GetPurchaseRequestResult m1, sp_GetPurchaseRequestResult m2)
                         { return SetAssignName(m1.WFResolutionID, m1.WFStatusID, m1.AssignName).CompareTo(SetAssignName(m2.WFResolutionID, m2.WFStatusID, m2.AssignName)) * order; });
                    break;

                case "ResolutionName":
                    list.Sort(
                         delegate(sp_GetPurchaseRequestResult m1, sp_GetPurchaseRequestResult m2)
                         { return m1.ResolutionName.CompareTo(m2.ResolutionName) * order; });
                    break;

                case "Justification":
                    list.Sort(
                         delegate(sp_GetPurchaseRequestResult m1, sp_GetPurchaseRequestResult m2)
                         { return m1.Justification.CompareTo(m2.Justification) * order; });
                    break;
                
            }

            return list;
        }

        /// <summary>
        /// Insert multi invoice
        /// </summary>
        /// <param name="listInvoice">List<PurchaseInvoice></param>
        private void InsertMultiInvoice(List<PurchaseInvoice> listInvoice)
        {
            if (listInvoice.Count > 0)
            {
                dbContext.PurchaseInvoices.InsertAllOnSubmit(listInvoice);
                dbContext.SubmitChanges();
            }
        }

        /// <summary>
        /// Delete invoices of purchase
        /// </summary>
        /// <param name="purchaseId"></param>
        private void DeleteInvoices(int purchaseId)
        {
            List<PurchaseInvoice> list = dbContext.PurchaseInvoices.Where(e => e.PurchaseRequest.ID.Equals(purchaseId)).ToList();

            dbContext.PurchaseInvoices.DeleteAllOnSubmit(list);
            dbContext.SubmitChanges();
        }

        /// <summary>
        /// Update confirm
        /// </summary>
        /// <param name="objUI">PurchaseRequest</param>
        /// <param name="listInvoice">List<PurchaseInvoice></param>
        /// <returns>Message</returns>
        public Message UpdateConfirm(PurchaseRequest objUI, List<PurchaseInvoice> listInvoice)
        {
            Message msg = null;
            DbTransaction trans = null;
            try
            {
                dbContext.Connection.Open();
                trans = dbContext.Connection.BeginTransaction();
                dbContext.Transaction = trans;
                PurchaseRequest objDb = GetByID(objUI.ID);
                if (objDb != null)
                {
                    new PurchaseRequestLogDao().WriteLogForPurchasingConfirm(objUI);
                    objDb.PurchaseAppoval = objUI.PurchaseAppoval;
                    objDb.PaymentAppoval = objUI.PaymentAppoval;
                    if (objDb.WFResolutionID != 0 && objUI.WFResolutionID != 0)
                        objDb.WFResolutionID = objUI.WFResolutionID;
                    if (objDb.WFStatusID != 0 && objUI.WFStatusID != 0)
                        objDb.WFStatusID = objUI.WFStatusID;
                    objDb.AssignID = objUI.AssignID;
                    objDb.AssignRole = objUI.AssignRole;
                    objDb.InvolveID = objUI.InvolveID;
                    objDb.InvolveRole = objUI.InvolveRole;
                    objDb.InvolveDate = objUI.InvolveDate;
                    objDb.InvolveResolution = objUI.InvolveResolution;
                    objDb.UpdateDate = DateTime.Now;
                    objDb.UpdatedBy = objUI.UpdatedBy;
                    dbContext.SubmitChanges();
                    DeleteInvoices(objDb.ID);
                    InsertMultiInvoice(listInvoice);
                    msg = new Message(MessageConstants.I0001, MessageType.Info, "Purchase Request '" + Constants.PR_REQUEST_PREFIX + objUI.ID + "'", "updated");
                    trans.Commit();
                }
            }
            catch
            {
                if (trans != null) trans.Rollback();
                // Show system error
                msg = new Message(MessageConstants.E0007, MessageType.Error);
            }

            return msg;
        }
        
        /// <summary>
        /// Delete purchase request
        /// </summary>
        /// <param name="objUI">PurchaseRequest</param>
        private void Delete(PurchaseRequest objUI)
        {
            if (objUI != null)
            {
                // Get current group in dbContext
                PurchaseRequest objDb = GetByID(objUI.ID);
                if (objDb != null)
                {
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
        /// Delete list of purchase request
        /// </summary>
        /// <param name="ids">list of purchase request id</param>
        /// <param name="userName">user delete</param>
        /// <returns>Message</returns>
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
                    int total = idArr.Count();
                    foreach (string id in idArr)
                    {
                        PurchaseRequest emp = GetByID(int.Parse(id));
                        if (emp != null)
                        {
                            emp.UpdatedBy = userName;
                            Delete(emp);
                        }
                        else
                        {
                            total--;
                        }
                    }

                    // Show succes message
                    msg = new Message(MessageConstants.I0001, MessageType.Info, total.ToString() + " purchase request(s)", "deleted");
                    trans.Commit();
                }
            }
            catch
            {
                if (trans != null) trans.Rollback();
                // Show system error
                msg = new Message(MessageConstants.E0007, MessageType.Error);
            }

            return msg;
        }

        /// <summary>
        /// Check update date whether is valid
        /// </summary>
        /// <param name="objUI">PurchaseRequest</param>
        /// <param name="objDb">PurchaseRequest</param>
        /// <param name="msg">Message</param>
        /// <returns>bool</returns>
        private bool IsValidUpdateDate(PurchaseRequest objUI, PurchaseRequest objDb, out Message msg)
        {
            bool isValid = false;
            msg = null;

            try
            {
                if ((objUI != null) && (objUI.UpdateDate != null))
                {
                    if (objDb != null)
                    {
                        if (objDb.UpdateDate.ToString().Equals(objUI.UpdateDate.ToString()))
                        {
                            isValid = true;
                        }
                        else
                        {
                            msg = new Message(MessageConstants.E0025, MessageType.Error, "Purchase Request Id " + objDb.ID);
                        }
                    }
                }
            }
            catch
            {
                throw;
            }

            return isValid;
        }
    }
}