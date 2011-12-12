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
    public class GroupDao : BaseDao
    {
        #region Public methods

        /// <summary>
        /// Get By Id
        /// </summary>
        /// <param name="groupId"></param>
        /// <returns></returns>
        public Group GetById(int groupId)
        {
            return dbContext.Groups.Where(p => (
                    (p.GroupId == groupId) &&
                    (p.DeleteFlag == false))).FirstOrDefault<Group>();
        }

        /// <summary>
        /// Get List
        /// </summary>
        /// <returns></returns>
        public List<Group> GetList(bool isActive)
        {
            // Get by isActive
            return dbContext.Groups.Where(p => (p.DeleteFlag == false && p.IsActive == isActive))
                .OrderBy(p => p.DisplayOrder).ToList<Group>();
        }

        /// <summary>
        /// Get List
        /// </summary>
        /// <returns></returns>
        public List<Group> GetList()
        {
            return dbContext.Groups.Where(p => (p.DeleteFlag == false))
                .OrderByDescending(p => p.GroupId).ToList<Group>();
        }

        /// <summary>
        /// Get List By Parameter
        /// </summary>
        /// <returns></returns>
        public List<Group> GetList(string name)
        {
            return dbContext.Groups.Where(p => (p.GroupName.Contains(name) && p.DeleteFlag == false))
                .OrderByDescending(p => p.GroupId).ToList<Group>();
        }

        /// <summary>
        /// Get List By Name
        /// </summary>
        /// <returns></returns>
        public List<Group> GetListByName(string name)
        {
            return dbContext.Groups.Where(p => (p.GroupName.ToLower().Contains(name.ToLower()) && p.DeleteFlag == false))
                    .OrderByDescending(p => p.GroupId).Take(10).ToList<Group>();
        }

        /// <summary>
        /// Get List
        /// </summary>
        /// <returns></returns>
        public Group GetByGroupName(string groupName, int id)
        {
            return dbContext.Groups.Where(p => (p.DeleteFlag == false)
                && (p.GroupId != id) && (p.GroupName == groupName)).FirstOrDefault<Group>();
        }

        /// <summary>
        /// Get List
        /// </summary>
        /// <returns></returns>
        public Group GetByGroupName(string groupName)
        {
            return dbContext.Groups.Where(p => (p.DeleteFlag == false)
                && (p.GroupName == groupName)).FirstOrDefault<Group>();
        }

        /// <summary>
        /// Sort Product
        /// </summary>
        /// <param name="list"></param>
        /// <param name="sortColumn"></param>
        /// <param name="sortOrder"></param>
        /// <returns></returns>
        public List<Group> Sort(List<Group> list, string sortColumn, string sortOrder)
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
                case "GroupName":
                    list.Sort(
                         delegate(Group m1, Group m2)
                         { return m1.GroupName.CompareTo(m2.GroupName) * order; });
                    break;
                case "Active":
                    list.Sort(
                         delegate(Group m1, Group m2)
                         { return m1.IsActive.CompareTo(m2.IsActive) * order; });
                    break;
                case "DisplayOrder":
                    List<Group> orderList = list.Where(c => c.DisplayOrder.HasValue).ToList<Group>();
                    List<Group> nonOrderList = list.Where(c => !c.DisplayOrder.HasValue).ToList<Group>();
                    orderList.Sort(
                          delegate(Group m1, Group m2)
                          {
                              return m1.DisplayOrder.Value.CompareTo(m2.DisplayOrder.Value) * order;
                          });

                    nonOrderList.Sort(
                         delegate(Group m1, Group m2)
                         { return m1.GroupName.CompareTo(m2.GroupName) * order; });


                    orderList.AddRange(nonOrderList);
                    return orderList;
                case "CreatedBy":
                    list.Sort(
                         delegate(Group m1, Group m2)
                         { return m1.CreatedBy.CompareTo(m2.CreatedBy) * order; });
                    break;
                case "UpdatedBy":
                    list.Sort(
                         delegate(Group m1, Group m2)
                         { return m1.UpdatedBy.CompareTo(m2.UpdatedBy) * order; });
                    break;
            }

            return list;
        }

        public Message Insert(Group objUI)
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

                    dbContext.Groups.InsertOnSubmit(objUI);
                    dbContext.SubmitChanges();

                    //Write Log
                    new GroupLogDao().WriteLogForGroup(null, objUI, ELogAction.Insert);
                    // Show success message
                    msg = new Message(MessageConstants.I0001, MessageType.Info, "Group '" + objUI.GroupName + "'", "added");
                }
            }
            catch (Exception)
            {
                // Show system error
                msg = new Message(MessageConstants.E0007, MessageType.Error);
            }
            return msg;
        }

        public Message Update(Group objUI)
        {
            Message msg = null;
            try
            {
                if (objUI != null)
                {
                    // Get current group in dbContext
                    Group objDb = GetById(objUI.GroupId);

                    if (objDb != null)
                    {
                        // Check valid update date
                        if (IsValidUpdateDate(objUI,objDb, out msg))
                        {
                            // Update info by objUI
                            objDb.GroupName = objUI.GroupName;
                            objDb.Description = objUI.Description;
                            objDb.DisplayOrder = objUI.DisplayOrder;
                            objDb.IsActive = objUI.IsActive;
                            objDb.UpdateDate = DateTime.Now;
                            objDb.UpdatedBy = objUI.UpdatedBy;

                            // Write logs
                            new GroupLogDao().WriteLogForGroup(null, objUI, ELogAction.Update);

                            // Submit changes to dbContext
                            dbContext.SubmitChanges();                           
                            // Show success message
                            msg = new Message(MessageConstants.I0001, MessageType.Info, "Group '" + objUI.GroupName + "'", "updated");
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



        public Message DeleteList(string ids, string stUpdatedBy)
        {
            Message msg = null;
            DbTransaction trans = null;
            bool canDelete = true;
            try
            {
                dbContext.Connection.Open();
                trans = dbContext.Connection.BeginTransaction();
                dbContext.Transaction = trans;

                if (!string.IsNullOrEmpty(ids))
                {
                    ids = ids.TrimEnd(',');
                    int groupID = 0;
                    
                    string[] idArr = ids.Split(',');
                    int total = idArr.Count();
                    foreach (string id in idArr)
                    {
                        bool isValid = Int32.TryParse(id, out groupID);
                        Group group = GetById(groupID);
                        if (group != null)
                        {                            
                            if (group.User_Groups.Count > 0)
                            {
                                canDelete = false;
                                // Show succes message
                                msg = new Message(MessageConstants.E0006, MessageType.Error, "delete group '" + group.GroupName + "'", "this group");
                                break;
                            }
                            group.UpdatedBy = stUpdatedBy;
                            Delete(group);
                        }
                        else
                        {
                            total--; 
                        }
                    }
                    if (canDelete)
                    {
                        // Show succes message
                        msg = new Message(MessageConstants.I0001, MessageType.Info, total.ToString() + " group(s)", "deleted");
                        trans.Commit();
                    }
                    else
                    {
                        trans.Rollback();
                    }
                }
            }
            catch (Exception)
            {
                if (trans != null) trans.Rollback();
                // Show system error
                msg = new Message(MessageConstants.E0007, MessageType.Error);
            }
            return msg;
        }




        /// <summary>
        /// Is Name Duplicated
        /// </summary>
        /// <param name="groupId"></param>
        /// <param name="groupName"></param>
        /// <param name="isUpdate"></param>
        /// <returns></returns>
        public bool IsNameDuplicated(int? groupId, string groupName, bool isUpdate)
        {
            bool isDup = false;
            List<Group> groupList = null;
            if (isUpdate && groupId != null)
            {
                groupList = dbContext.Groups.Where(p => ((p.GroupName == groupName)
                    && (p.DeleteFlag == false) && (p.GroupId != groupId))).ToList<Group>();
            }
            else if (!isUpdate)
            {
                groupList = dbContext.Groups.Where(p => ((p.GroupName == groupName)
                    && (p.DeleteFlag == false))).ToList<Group>();
            }

            if ((groupList != null) && (groupList.Count > 0))
            {
                isDup = true;
            }

            return isDup;
        }


        #endregion

        #region Private methods
        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="objUI"></param>
        private void Delete(Group objUI)
        {
            if (objUI != null)
            {
                // Get current group in dbContext
                Group objDb = GetById(objUI.GroupId);

                if (objDb != null)
                {
                    // Set delete info
                    objUI.GroupName = objDb.GroupName;
                    objDb.DeleteFlag = true;
                    objDb.UpdateDate = DateTime.Now;
                    objDb.UpdatedBy = objUI.UpdatedBy;

                    // Write logs
                    new GroupLogDao().WriteLogForGroup(null, objUI, ELogAction.Delete);
                    // Submit changes to dbContext
                    dbContext.SubmitChanges();
                }
            }
        }

        /// <summary>
        /// Check update date whether is valid
        /// </summary>
        /// <param name="objUI"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        private bool IsValidUpdateDate(Group objUI, Group objDb, out Message msg)
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
                            msg = new Message(MessageConstants.E0025, MessageType.Error, "Group name " + objDb.GroupName);
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


        public bool HasPermisionOnModule(int userAdminId, int permissionId, int moduleId)
        {
            GroupDao groupDao = new GroupDao();
            var listGroup = dbContext.User_Groups.Where(p => p.UserAdminId == userAdminId && p.Group.IsActive && p.IsActive).ToList();
            foreach (User_Group ug in listGroup)
            {
                var mpList = dbContext.GroupPermissions.Where(p => p.ModuleId == moduleId &&
                    p.PermissionId == permissionId && p.GroupId == ug.GroupId).ToList();
                if (mpList.Count > 0)
                    return true;
            }
            return false;
        }


        public Message UpdateActiveStatus(int groupId, bool isActive)
        {
            try
            {
                Group group = GetById(groupId);
                if (group == null)
                    return new Message(MessageConstants.E0005, MessageType.Error, 
                        "Selected group", "system");
                group.IsActive = isActive;
                dbContext.SubmitChanges();
                return new Message(MessageConstants.I0001, MessageType.Info, 
                    "Group \"" + group.GroupName + "\"", "set " + (isActive ? "active" : "inactive") );
            }
            catch
            {
                return new Message(MessageConstants.E0007, MessageType.Error);
            }
        }
    }
}