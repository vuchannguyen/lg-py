using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CRM.Models;
using CRM.Library.Common;
using System.Data.Common;

namespace CRM.Models
{
    public class JRAdminDao : BaseDao
    {
        #region Private Fields

        private AuthenticateDao auDao = new AuthenticateDao();
        #endregion

        #region Public methods

        /// <summary>
        /// Get By Id
        /// </summary>
        /// <param name="userAdminId"></param>
        /// <returns></returns>
        public UserAdmin_WFRole GetById(int id)
        {
            return dbContext.UserAdmin_WFRoles.Where(p => (p.ID == id)).FirstOrDefault<UserAdmin_WFRole>();
        }

        public List<UserAdmin_WFRole> GetListUserByWorkflowID(int wfID)
        {
            return dbContext.UserAdmin_WFRoles.Where(q => q.WFRole.WorkFlow.ID == wfID).ToList();
        }

        /// <summary>
        /// Get By Id
        /// </summary>
        /// <param name="userAdminId"></param>
        /// <returns></returns>
        public UserAdmin_WFRole GetByUserAdminId(int id)
        {
            return dbContext.UserAdmin_WFRoles.Where(p => (p.UserAdminId == id) && (p.IsActive)).FirstOrDefault<UserAdmin_WFRole>();
        }

        public UserAdmin_WFRole GetByUserNameAndRoleAndIsActive(string userName, int roleId, bool isActive)
        {
            return dbContext.UserAdmin_WFRoles.Where(p => ((p.UserAdmin.UserName == userName) && (p.WFRoleID == roleId) && (p.IsActive == isActive))).FirstOrDefault<UserAdmin_WFRole>();
        }

        /// <summary>
        /// Get By Role
        /// </summary>
        /// <param name="userAdminId"></param>
        /// <returns></returns>
        public List<UserAdmin> GetByRequestor()
        {
            List<UserAdmin> list = new List<UserAdmin>();
            var entities = from e in dbContext.UserAdmin_WFRoles
                           join f in dbContext.UserAdmins on e.UserAdminId equals f.UserAdminId
                           where e.WFRoleID == Constants.REQUESTOR_ID

                           select new
                           {
                               ID = e.UserAdminId,
                               Name = f.UserName
                           };

            foreach (var e in entities)
            {
                UserAdmin admin = new UserAdmin();
                admin.UserAdminId = e.ID;
                admin.UserName = e.Name;
                list.Add(admin);
            }

            return list;
        }

        public List<sp_GetUserInPurchaseRequestResult> GetUserInPurchaseRequest(bool isRequestor)
        {
            return dbContext.sp_GetUserInPurchaseRequest(isRequestor, false).ToList();
        }
        public List<sp_GetUserInPurchaseRequestResult> GetUserInUsPurchaseRequest(bool isRequestor)
        {
            return dbContext.sp_GetUserInPurchaseRequest(isRequestor, true).ToList();
        }
        /// <summary>
        /// Get List By Name
        /// </summary>
        /// <param name="userAdminId"></param>
        /// <returns></returns>
        public List<sp_GetJRForAdminResult> GetListByName(string name, int workflowID, int roleID)
        {
            return dbContext.sp_GetJRForAdmin(name, workflowID, roleID).ToList<sp_GetJRForAdminResult>();
        }

        /// <summary>
        /// Insert
        /// </summary>
        /// <param name="objUI"></param>
        /// <returns></returns>
        public Message Insert(List<UserAdmin_WFRole> list)
        {
            Message msg = null;
            DbTransaction trans = null;
            bool isSuccess = true;
            try
            {
                dbContext.Connection.Open();
                trans = dbContext.Connection.BeginTransaction();
                dbContext.Transaction = trans;

                foreach (UserAdmin_WFRole item in list)
                {
                    msg = Insert(item);
                    if (msg.MsgType == MessageType.Error)
                    {
                        isSuccess = false;
                        break;
                    }
                }
                if (!isSuccess)
                {
                    trans.Rollback();
                }
                else
                {
                    WFRole wfRole = new RoleDao().GetByID(list[0].WFRoleID);
                    msg = new Message(MessageConstants.I0001, MessageType.Info, 
                        "User \"" + string.Join("\"" + Constants.SEPARATE_USER_ADMIN_USERNAME + " \"",
                        list.Select(p => p.UserAdmin.UserName)) + "\"",
                        string.Format("added in role \"{0}\" of Workflow \"{1}\"", wfRole.Name, wfRole.WorkFlow.Name));
                    trans.Commit();
                }
            }
            catch
            {
                trans.Rollback();
                msg = new Message(MessageConstants.E0007, MessageType.Error);
            }
            return msg;
        }

        /// <summary>
        /// Insert
        /// </summary>
        /// <param name="objUI"></param>
        /// <returns></returns>
        public Message Insert(UserAdmin_WFRole objUI)
        {
            Message msg = null;
            try
            {
                if (objUI != null)
                {
                    UserAdmin_WFRole objDb = GetById(objUI.ID);
                    if (IsUserNameDuplicated(objUI))
                    {
                        // Show error message
                        msg = new Message(MessageConstants.E0004, MessageType.Error, "Username " + objDb.UserAdmin.UserAdminId);
                    }
                    else
                    {
                        dbContext.UserAdmin_WFRoles.InsertOnSubmit(objUI);
                        dbContext.SubmitChanges();
                        new JRAdminLogDao().WriteLogForWorkflowAdmin(null, objUI, ELogAction.Insert);
                        // Show success message
                        msg = new Message(MessageConstants.I0001, MessageType.Info, "User '"
                            + objUI.UserAdmin.UserName+"'", "added in role '" + objUI.WFRole.Name+"'");
                    }
                }
            }
            catch (Exception)
            {
                // Show system error
                msg = new Message(MessageConstants.E0007, MessageType.Error);
            }
            return msg;
        }

        /// <summary>
        /// Update
        /// </summary>
        /// <param name="objUI"></param>
        /// <returns></returns>
        public Message Update(UserAdmin_WFRole objUI)
        {
            Message msg = null;
            try
            {
                if (objUI != null)
                {
                    // Get current group in dbContext
                    UserAdmin_WFRole objDb = GetById(objUI.ID);
                    if (objDb != null)
                    {
                        if (hasJRAssignTo(objUI.UserAdminId,objUI.WFRoleID))
                        {
                            UserAdminDao adminDao = new UserAdminDao();
                            UserAdmin obj = adminDao.GetById(objUI.UserAdminId);
                            if (obj != null)
                            {
                                msg = new Message(MessageConstants.E0023, MessageType.Error, "update " + obj.UserName, "Job Request");
                            }
                        }
                        else
                        {
                            // Check valid update date
                            if (IsValidUpdateDate(objUI, objDb, out msg))
                            {
                                new JRAdminLogDao().WriteLogForWorkflowAdmin(null, objUI, ELogAction.Update);
                                // Update info by objUI
                                objDb.UserAdminId = objUI.UserAdminId;
                                objDb.WFRoleID = objUI.WFRoleID;
                                objDb.UpdateDate = DateTime.Now;
                                objDb.UpdatedBy = objUI.UpdatedBy;
                                objDb.IsActive = objUI.IsActive;
                                // Submit changes to dbContext
                                dbContext.SubmitChanges();
                                // Show success message
                                msg = new Message(MessageConstants.I0001, MessageType.Info, " User admin '" + objDb.UserAdmin.UserName + "'", "updated");
                            }

                        }
                    }
                }
            }
            catch (Exception)
            {
                // Show system error
                msg = new Message(MessageConstants.E0007, MessageType.Error);
            }
            return msg;
        }

        public bool hasJRAssignTo(int userAdminId,int roleId)
        {
            bool hasJRAssignTo = false;
            JobRequestDao jrDao = new JobRequestDao();
            JobRequest obj = jrDao.GetJRByAssign(userAdminId, roleId);
            if (obj != null)
            {
                hasJRAssignTo = true;
            }
            return hasJRAssignTo;
        }

        public Message DeleteList(string ids, string stUpdatedBy)
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
                    ids = ids.TrimEnd(':');
                    string[] idArr = ids.Split(':');
                    int adminId = 0;
                    int total = idArr.Count();
                    foreach (string id in idArr)
                    {
                       bool isInterger = Int32.TryParse(id.Split(',')[0],out adminId);
                       UserAdmin_WFRole user = GetById(adminId);
                       if (user != null)
                       {
                           user.UpdatedBy = stUpdatedBy;
                           Delete(user);
                       }
                       else
                       {
                           total--;
                       }
                    }
                    // Show succes message
                    msg = new Message(MessageConstants.I0001, MessageType.Info, total.ToString() + " account(s)", "deleted");
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

        //<summary>
        //Sort Product
        //</summary>
        //<param name="list"></param>
        //<param name="sortColumn"></param>
        //<param name="sortOrder"></param>
        //<returns></returns>
        public List<sp_GetJRForAdminResult> Sort(List<sp_GetJRForAdminResult> list, string sortColumn, string sortOrder)
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
                case "UserName":
                    list.Sort(
                         delegate(sp_GetJRForAdminResult m1, sp_GetJRForAdminResult m2)
                         { return m1.UserName.CompareTo(m2.UserName) * order; });
                    break;
                case "Workflow":
                    list.Sort(
                         delegate(sp_GetJRForAdminResult m1, sp_GetJRForAdminResult m2)
                         { return m1.wfName.CompareTo(m2.wfName) * order; });
                    break;
                case "Active":
                    list.Sort(
                         delegate(sp_GetJRForAdminResult m1, sp_GetJRForAdminResult m2)
                         { return m1.IsActive.Value.CompareTo(m2.IsActive) * order; });
                    break;
                case "GroupName":
                    list.Sort(
                         delegate(sp_GetJRForAdminResult m1, sp_GetJRForAdminResult m2)
                         { return m1.roleName.CompareTo(m2.roleName) * order; });
                    break;
                case "UpdatedBy":
                    list.Sort(
                         delegate(sp_GetJRForAdminResult m1, sp_GetJRForAdminResult m2)
                         { return m1.UpdatedBy.CompareTo(m2.UpdatedBy) * order; });
                    break;
                case "CreatedBy":
                    list.Sort(
                         delegate(sp_GetJRForAdminResult m1, sp_GetJRForAdminResult m2)
                         { return m1.CreatedBy.CompareTo(m2.CreatedBy) * order; });
                    break;

            }

            return list;
        }

        /// <summary>
        /// Get User Admin List
        /// </summary>
        /// <returns></returns>
        public List<sp_GetJRForAdminResult> GetList(string name, int workflow, int role)
        {
            return dbContext.sp_GetJRForAdmin(name, workflow, role).OrderByDescending(p => p.UserAdminId).ToList<sp_GetJRForAdminResult>();
        }

        /// <summary>
        /// Check user name whether is duplicated
        /// </summary>
        /// <param name="objUI"></param>
        /// <param name="isUpdateMode"></param>
        /// <returns></returns>
        public UserAdmin_WFRole GetByRoleAndUserID(int id, int role)
        {
            return dbContext.UserAdmin_WFRoles.Where(p => (
                        (p.UserAdminId == id) &&
                        (p.WFRoleID == role))).FirstOrDefault<UserAdmin_WFRole>();
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="objUI"></param>
        private void Delete(UserAdmin_WFRole objUI)
        {
            if (objUI != null)
            {
                // Get current info in dbContext
                UserAdmin_WFRole objDb = GetById(objUI.ID);

                if (objDb != null)
                {
                    new JRAdminLogDao().WriteLogForWorkflowAdmin(objDb,null, ELogAction.Delete);
                    dbContext.UserAdmin_WFRoles.DeleteOnSubmit(objUI);
                    // Submit changes to dbContext
                    dbContext.SubmitChanges();

                }
            }
        }

        private List<UserAdmin_WFRole> GetListAll()
        {
            return dbContext.UserAdmin_WFRoles.ToList<UserAdmin_WFRole>();
        }

        public bool CheckDuplicated(int id,int userAdminId,int roleId,bool update)
        {
            bool isDuplicated = false;
            List<UserAdmin_WFRole> list = GetListAll();
            if (update)
            {
                list = list.Where(p => p.ID != id && p.UserAdminId == userAdminId && p.WFRoleID == roleId).ToList<UserAdmin_WFRole>();
            }
            else
            {
                list = list.Where(p => p.UserAdminId == userAdminId && p.WFRoleID == roleId).ToList<UserAdmin_WFRole>();
            }
            if (list.Count > 0)
            {
                isDuplicated = true;
            }
            return isDuplicated;
        }

        /// <summary>
        /// Check user name whether is duplicated
        /// </summary>
        /// <param name="objUI"></param>
        /// <param name="isUpdateMode"></param>
        /// <returns></returns>
        private bool IsUserNameDuplicated(UserAdmin_WFRole objUI)
        {
            bool isDuplicated = false;
            List<UserAdmin_WFRole> objList = new List<UserAdmin_WFRole>();
            objList = dbContext.UserAdmin_WFRoles.Where(p => (
                (p.UserAdminId == objUI.UserAdminId) &&
                (p.WFRoleID == objUI.WFRoleID))).ToList<UserAdmin_WFRole>();
            if (objList.Count > 0)
            {
                isDuplicated = true;
            }
            return isDuplicated;
        }


        /// <summary>
        /// Check update date whether is valid
        /// </summary>
        /// <param name="objUI"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        private bool IsValidUpdateDate(UserAdmin_WFRole objUI, UserAdmin_WFRole objDb, out Message msg)
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
                            msg = new Message(MessageConstants.E0025, MessageType.Error, "User name " + objDb.UserAdmin.UserName + " with role " + objDb.WFRole.Name);
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
        #endregion

        public Message UpdateActiveStatus(int user_roleId, bool isActive)
        {
            try
            {
                UserAdmin_WFRole obj = GetById(user_roleId);
                if (obj == null)
                    return new Message(MessageConstants.E0005, MessageType.Error,
                        "Selected user", "system");
                obj.IsActive = isActive;
                dbContext.SubmitChanges();
                return new Message(MessageConstants.I0001, MessageType.Info, "The \"" + obj.WFRole.Name + 
                    " (" + obj.WFRole.WorkFlow.Name + ")\" role" +
                    " of user \"" + obj.UserAdmin.UserName + "\"", "set " + (isActive ? "active" : "inactive"));
            }
            catch
            {
                return new Message(MessageConstants.E0007, MessageType.Error);
            }
        }
    }
}