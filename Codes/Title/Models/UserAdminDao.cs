using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Web;
using System.Data.Linq;
using System.Data.Common;

using CRM.Library.Common;
using System.Web.UI.WebControls;

namespace CRM.Models
{
    public class UserAdminDao : BaseDao
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
        public UserAdmin GetById(int userAdminId)
        {
            return dbContext.UserAdmins.Where(p => (
                (p.UserAdminId == userAdminId) &&
                (p.DeleteFlag == false))).FirstOrDefault<UserAdmin>();
        }

        /// <summary>
        /// Get user_group by ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public User_Group GetUser_Group(int id)
        {
            return dbContext.User_Groups.Where(p => p.ID == id).FirstOrDefault<User_Group>();
        }

        /// <summary>
        /// Get By Id
        /// </summary>
        /// <param name="userAdminId"></param>
        /// <returns></returns>
        public UserAdmin GetByUserName(string userName)
        {
            return dbContext.UserAdmins.Where(p => (
                (p.UserName == userName) &&
                (p.DeleteFlag == false))).FirstOrDefault<UserAdmin>();
        }


        /// <summary>
        /// Get user_group by user name and group Id
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="groupId"></param>
        /// <returns></returns>
        public User_Group GetUser_Group(int userId, int groupId)
        {
            return dbContext.User_Groups.Where(u => u.UserAdminId == userId && u.GroupId == groupId).SingleOrDefault<User_Group>();
        }

        /// <summary>
        /// Get List
        /// </summary>
        /// <param name="userAdminId"></param>
        /// <returns></returns>
        public List<UserAdmin> GetList(string userName)
        {
            return dbContext.UserAdmins.Where(p => (
                (p.UserName.Contains(userName)) &&
                (p.DeleteFlag == false))).Take(Constants.AUTO_COMPLETE_ITEMS).ToList<UserAdmin>();
        }

        /// <summary>
        /// Get User Admin List
        /// </summary>
        /// <returns></returns>
        public List<UserAdmin> GetList()
        {
            return dbContext.UserAdmins.Where(p => p.DeleteFlag == false)
                .OrderByDescending(p => p.UserName).ToList<UserAdmin>();
        }

        public List<User_Group> GetListUser_Group()
        {
            return dbContext.User_Groups.ToList<User_Group>();
        }

        public List<User_Group> Sort(List<User_Group> list, string sortColumn, string sortOrder)
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
                         delegate(User_Group m1, User_Group m2)
                         { return m1.UserAdmin.UserName.CompareTo(m2.UserAdmin.UserName) * order; });
                    break;
                case "GroupName":
                    list.Sort(
                         delegate(User_Group m1, User_Group m2)
                         { return m1.Group.GroupName.CompareTo(m2.Group.GroupName) * order; });
                    break;
                case "Active":
                    list.Sort(
                         delegate(User_Group m1, User_Group m2)
                         { return m1.IsActive.CompareTo(m2.IsActive) * order; });
                    break;
                case "CreatedBy":
                    list.Sort(
                         delegate(User_Group m1, User_Group m2)
                         { return m1.CreatedBy.CompareTo(m2.CreatedBy) * order; });
                    break;
                case "UpdatedBy":
                    list.Sort(
                         delegate(User_Group m1, User_Group m2)
                         { return m1.UpdatedBy.CompareTo(m2.UpdatedBy) * order; });
                    break;
            }

            return list;
        }
        public Message InsertMany(List<string> list_userName, string groupId, bool isActive, AuthenticationProjectPrincipal principal)
        {
            Message msg = null;
            DbTransaction trans = null;
            bool isSuccess = true;
            try
            {
                dbContext.Connection.Open();
                trans = dbContext.Connection.BeginTransaction();
                dbContext.Transaction = trans;

                foreach (string userName in list_userName)
                {
                    msg = Insert(userName, groupId, isActive, principal);
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
                    string groupName = new GroupDao().GetById(int.Parse(groupId)).GroupName;
                    msg = new Message(MessageConstants.I0001, MessageType.Info,
                        string.Format("User \"{0}\" belong to group \"{1}\"", 
                        string.Join("\"" + Constants.SEPARATE_USER_ADMIN_USERNAME + " \"", list_userName), groupName), "added");
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
        /// Insert user admin and user group
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="groupId"></param>
        /// <param name="isActive"></param>
        /// <param name="principal"></param>
        /// <returns></returns>
        public Message Insert(string userName, string groupId, bool isActive, AuthenticationProjectPrincipal principal)
        {
            Message msg = null;
            try 
            {
                if (auDao.CheckExistInAD(userName))
                {
                    UserAdmin user = GetByUserName(userName);
                    if (user == null)
                    {
                        user = new UserAdmin();
                        user.UserName = userName;                        
                        user.DeleteFlag = false;
                        user.CreateDate = DateTime.Now;
                        user.UpdateDate = DateTime.Now;
                        user.CreatedBy = principal.UserData.UserName;
                        user.UpdatedBy = principal.UserData.UserName;

                        dbContext.UserAdmins.InsertOnSubmit(user);
                        dbContext.SubmitChanges();
                    }

                    int _groupId = int.Parse(groupId);

                    User_Group user_group = GetUser_Group(user.UserAdminId, _groupId);
                    if (user_group == null)
                    {
                        user_group = new User_Group();
                        user_group.UserAdminId = user.UserAdminId;
                        user_group.GroupId = int.Parse(groupId);
                        user_group.IsActive = isActive;
                        user_group.CreateDate = DateTime.Now;
                        user_group.UpdateDate = DateTime.Now;
                        user_group.CreatedBy = principal.UserData.UserName;
                        user_group.UpdatedBy = principal.UserData.UserName;

                        dbContext.User_Groups.InsertOnSubmit(user_group);
                        dbContext.SubmitChanges();

                        //Write Log
                        new UserAdminLogDao().WriteLogForUserAdmin(null, user_group, ELogAction.Insert);

                        msg = new Message(MessageConstants.I0001, MessageType.Info, "User " + userName + " belong group " + user_group.Group.GroupName, "added");
                    }
                    else
                    {
                        //msg = new Message(MessageConstants.E0020, MessageType.Error, "User " + userName + " belong group " + user_group.Group.GroupName, "database");
                        msg = new Message(MessageConstants.E0020, MessageType.Error, "User " + userName, "group " + user_group.Group.GroupName);
                    }
                }
                else
                {
                    msg = new Message(MessageConstants.E0005, MessageType.Error, "User " + userName, "Active Directory");
                }
            }
            catch (Exception)
            {
                msg = new Message(MessageConstants.E0007, MessageType.Error);
            }
            return msg;
        }

        /// <summary>
        /// Update user group
        /// </summary>
        /// <param name="objUI"></param>
        /// <returns></returns>
        public Message Update(string id, string userName, string groupId, bool isActive, AuthenticationProjectPrincipal principal)
        {
            Message msg = null;
            try
            {
                if (auDao.CheckExistInAD(userName))
                {
                    UserAdmin user = GetByUserName(userName);
                    if (user == null)
                    {
                        user = new UserAdmin();
                        user.UserName = userName;
                        user.DeleteFlag = false;
                        user.CreateDate = DateTime.Now;
                        user.UpdateDate = DateTime.Now;
                        user.CreatedBy = principal.UserData.UserName;
                        user.UpdatedBy = principal.UserData.UserName;

                        dbContext.UserAdmins.InsertOnSubmit(user);
                        dbContext.SubmitChanges();
                    }

                    int _groupId = int.Parse(groupId);

                    User_Group user_group = GetUser_Group(user.UserAdminId, _groupId);
                    
                    if (user_group == null)
                    {
                        user_group = GetUser_Group(int.Parse(id));
                    }
                    else if(user_group.ID.ToString() != id)
                    {
                        msg = new Message(MessageConstants.E0020, MessageType.Error, "User " + userName + " belong group " + user_group.Group.GroupName, "database");
                    }

                    if (msg == null)
                    {
                        if (user_group != null)
                        {
                            user_group.UserAdminId = user.UserAdminId;
                            user_group.GroupId = int.Parse(groupId);
                            user_group.IsActive = isActive;

                            user_group.UpdateDate = DateTime.Now;
                            user_group.UpdatedBy = principal.UserData.UserName;
                            //Write Log
                            new UserAdminLogDao().WriteLogForUserAdmin(null, user_group, ELogAction.Update);

                            dbContext.SubmitChanges();
                            msg = new Message(MessageConstants.I0001, MessageType.Info, "User " + userName + " belong group " + user_group.Group.GroupName, "updated");                            
                        }
                        else
                        {
                            msg = new Message(MessageConstants.E0020, MessageType.Error, "User " + userName + " belong group " + user_group.Group.GroupName + " does not", "database");
                        }
                    }
                }
                else
                {
                    msg = new Message(MessageConstants.E0005, MessageType.Error, "User " + userName, "Active Directory");
                }
            }
            catch (Exception)
            {
                msg = new Message(MessageConstants.E0007, MessageType.Error);
            }
            return msg;
        }

        public Message DeleteList(string ids)
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
                    DeleteList(ids, ref msg);
                }
                trans.Commit();
            }
            catch
            {
                if (trans != null) trans.Rollback();

                msg = new Message(MessageConstants.E0007, MessageType.Error);
            }
            return msg;
        }

        private void DeleteList(string ids,ref Message msg)
        {
            ids = ids.TrimEnd(',');
            string[] idArr = ids.Split(',');
            int total = idArr.Count();
            foreach (string id in idArr)
            {
                int intID = int.Parse(id);
                User_Group user_group = GetUser_Group(intID);
                if (user_group != null)
                {
                    UserAdmin user = user_group.UserAdmin;
                    dbContext.User_Groups.DeleteOnSubmit(user_group);

                    new UserAdminLogDao().WriteLogForUserAdmin(null, user_group, ELogAction.Delete);

                    dbContext.SubmitChanges();
                    if (user.User_Groups.Count == 0 && user.JobRequests.Count == 0
                        && user.PurchaseRequestApprovals.Count == 0 && user.PurchaseRequests.Count == 0
                        && user.UserAdmin_WFRoles.Count == 0 && user.JobRequests1.Count == 0)
                    {
                        dbContext.UserAdmins.DeleteOnSubmit(user);
                        dbContext.SubmitChanges();
                    }
                }
                else
                {
                    total--;
                }
            }
            msg = new Message(MessageConstants.I0001, MessageType.Info, total.ToString() + " account(s)", "deleted");
        }

        /// <summary>
        /// Get Active By Id
        /// </summary>
        /// <param name="userAdminId"></param>
        /// <returns></returns>
        public UserAdmin GetActiveUserById(int userAdminId)
        {
            return dbContext.UserAdmins.Where(p => (
                (p.UserAdminId == userAdminId) &&                
                (p.DeleteFlag == false))).FirstOrDefault<UserAdmin>();
        }

        /// <summary>
        /// Check Login
        /// </summary>
        /// <param name="objUI"></param>
        /// <returns></returns>
        public UserAdmin CheckLogin(UserAdmin objUI)
        {
            UserAdmin objRes = null;
            objRes = dbContext.UserAdmins.Where(p => (
                (p.UserName == objUI.UserName) &&                
                (p.DeleteFlag == false))).FirstOrDefault<UserAdmin>();

            return objRes;
        }

        #endregion

        public List<UserAdmin> GetListByRole(int roleId)
        {
            int[] arrUserAdminId = dbContext.UserAdmin_WFRoles.
                Where(p=>p.WFRoleID == roleId && p.IsActive).Select(p=>p.UserAdminId).ToArray();
            return dbContext.UserAdmins.Where(p => arrUserAdminId.Contains(p.UserAdminId)).Distinct().ToList();
        }

        public List<ListItem> GetListWithRole(int roleId)
        {
            var list = GetListByRole(roleId);
            RoleDao roleDao = new RoleDao();
            List<ListItem> result = new List<ListItem>();
            foreach (var item in list)
            {
                result.Add(new ListItem(
                    item.UserName + "(" + roleDao.GetByID(roleId).Name + ")",
                    item.UserAdminId + Constants.SEPARATE_USER_ADMIN_ID_STRING + roleId));
            }
            return result;
        }

        public bool CanForwardToOther(string userId, int roleId, string perReviewId)
        {
            PerformanceReviewDao perDao = new PerformanceReviewDao();
            PerformanceReview pr = perDao.GetById(perReviewId);
            if (pr.AssignRole == roleId && pr.AssignID.Equals(userId))
                return true;
            return false;
        }

        public Message UpdateActiveStatus(int user_groupId, bool isActive)
        {
            try
            {
                User_Group obj = dbContext.User_Groups.Where(p => p.ID == user_groupId).FirstOrDefault();
                if (obj == null)
                    return new Message(MessageConstants.E0005, MessageType.Error,
                        "Selected user", "system");
                obj.IsActive = isActive;
                dbContext.SubmitChanges();
                return new Message(MessageConstants.I0001, MessageType.Info,"User " + obj.UserAdmin.UserName + 
                    " in group \"" + obj.Group.GroupName  + "\"", "set " + (isActive ? "active" : "inactive"));
            }
            catch
            {
                return new Message(MessageConstants.E0007, MessageType.Error);
            }
        }

        //public List<sp_GetListUserOfModuleResult> GetListUserOfModule(int moduleId, int groupId, string name = null)
        //{
        //    return dbContext.sp_GetListUserOfModule(name, moduleId, groupId).ToList();
        //}
        public List<sp_GetListItHelpDeskResult> GetListITHelpDesk(int moduleId, int permissionid)
        {
            return dbContext.sp_GetListItHelpDesk(null, moduleId, permissionid).ToList();
        }

        
    }
}