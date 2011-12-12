using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CRM.Library.Common;
using System.Data.Common;

namespace CRM.Models
{
    public class SRSettingDao : BaseDao
    {
        /// <summary>
        /// Get List Service Request Setting
        /// </summary>
        /// <param name="text"></param>
        /// <param name="project"></param>
        /// <param name="branch"></param>
        /// <param name="office"></param>
        /// <returns></returns>
        public List<sp_GetSR_SettingResult> GetList(string text, string project, int branch, int office)
        {
            return dbContext.sp_GetSR_Setting(text, project, branch, office).ToList();
        }

        /// <summary>
        /// Get Service Request Setting when Submit new SR
        /// </summary>
        /// <param name="project"></param>
        /// <param name="office"></param>
        /// <returns></returns>
        public SR_Setting GetByProjectAndOffice(string project, int office)
        {
            if (!string.IsNullOrEmpty(project))
            {
                return dbContext.SR_Settings.Where(q => q.ProjectName == project && q.OfficeID == office && q.IsActive == true && q.DeleteFlag == false).FirstOrDefault();
            }
            else
            {
                return dbContext.SR_Settings.Where(q => q.ProjectName == null && q.OfficeID == office && q.IsActive == true && q.DeleteFlag == false).FirstOrDefault();
            }
        }

        /// <summary>
        /// Get Service Request Setting
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public SR_Setting GetByID(int id)
        {
            return dbContext.SR_Settings.Where(q => q.ID == id).FirstOrDefault();
        }

        /// <summary>
        /// Insert Service Request Setting
        /// </summary>
        /// <param name="objUI"></param>
        /// <returns></returns>
        public Message Insert(SR_Setting objUI)
        {
            Message msg = null;
            try
            {
                if (IsValidRoute(objUI, out msg, false))
                {
                    msg = new Message(MessageConstants.E0048, MessageType.Error, "The default routing");
                }
                else if (IsDuplicated(objUI, out msg, false))
                {
                    UserAdmin objUserAdmin = new UserAdminDao().GetById(objUI.UserAdminID);
                    msg = new Message(MessageConstants.E0004, MessageType.Error, "User Admin " + (objUserAdmin != null ? objUserAdmin.UserName : string.Empty) + " assigned to project " + objUI.ProjectName);
                }
                else
                {
                    dbContext.SR_Settings.InsertOnSubmit(objUI);
                    dbContext.SubmitChanges();
                    // Show success message
                    msg = new Message(MessageConstants.I0001, MessageType.Info, "User Admin " + objUI.UserAdmin.UserName, "assigned to project " + objUI.ProjectName);
                }
               
            }
            catch
            {
                msg = new Message(MessageConstants.E0007, MessageType.Error);
            }
            return msg;
        }

        /// <summary>
        /// Update Service Request Setting
        /// </summary>
        /// <param name="objUI"></param>
        /// <returns></returns>
        public Message Update(SR_Setting objUI)
        {
            Message msg = null;

            try
            {
                if (objUI != null)
                {
                    // Get current group in dbContext
                    SR_Setting objDb = GetByID(objUI.ID);

                    if (objDb != null)
                    {
                        // Check valid update date
                        if (!IsValidUpdateDate(objUI, objDb, out msg))
                        {
                            UserAdmin objUserAdmin = new UserAdminDao().GetById(objUI.UserAdminID);
                            msg = new Message(MessageConstants.E0025, MessageType.Error, "User Admin " + objUserAdmin != null ? objUserAdmin.UserName : string.Empty, "assigned to project " + objUI.ProjectName);
                        }
                        else if(IsDuplicated(objUI, out msg,true))
                        {
                            UserAdmin objUserAdmin = new UserAdminDao().GetById(objUI.UserAdminID);
                            msg = new Message(MessageConstants.E0004, MessageType.Error, "User Admin " + ( objUserAdmin != null ? objUserAdmin.UserName : string.Empty) +  " assigned to project " + objUI.ProjectName);
                        }
                        else if (IsValidRoute(objUI, out msg, true))
                        {
                            msg = new Message(MessageConstants.E0048, MessageType.Error, "The default routing");
                        }
                        else
                        {
                             // Update info by objUI                       
                            objDb.OfficeID = objUI.OfficeID;
                            objDb.ProjectName = objUI.ProjectName;
                            objDb.UserAdminID = objUI.UserAdminID;
                            objDb.IsActive = objUI.IsActive;
                            objDb.DeleteFlag = objUI.DeleteFlag;
                            objDb.UpdateDate = DateTime.Now;
                            objDb.UpdatedBy = objUI.UpdatedBy;

                            // Submit changes to dbContext
                            dbContext.SubmitChanges();

                            // Show success message
                            UserAdmin objUserAdmin = new UserAdminDao().GetById(objUI.UserAdminID);
                            msg = new Message(MessageConstants.I0001, MessageType.Info, "User Admin " + objUserAdmin != null?objUserAdmin.UserName:string.Empty, "assigned to project " + objUI.ProjectName);
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

        private bool IsValidRoute(SR_Setting obj, out Message msg, bool isUpdate)
        {
            bool isDuplicated = false;
            SR_Setting objDb = new SR_Setting();
            msg = null;

            try
            {
                if (!isUpdate)
                {
                    if (!string.IsNullOrEmpty(obj.ProjectName))
                    {
                        objDb = dbContext.SR_Settings.Where(q =>  q.ProjectName == obj.ProjectName
                             && q.OfficeID == obj.OfficeID && q.DeleteFlag == false).FirstOrDefault();
                    }
                    else
                    {
                        objDb = dbContext.SR_Settings.Where(q => (q.ProjectName == null || q.ProjectName.Trim() == string.Empty)
                            && q.OfficeID == obj.OfficeID && q.DeleteFlag == false).FirstOrDefault();
                    }
                }
                else
                {
                    if (!string.IsNullOrEmpty(obj.ProjectName))
                    {
                        objDb = dbContext.SR_Settings.Where(q => q.ProjectName == obj.ProjectName
                             && q.OfficeID == obj.OfficeID && q.ID != obj.ID && q.DeleteFlag == false).FirstOrDefault();
                    }
                    else
                    {
                        objDb = dbContext.SR_Settings.Where(q => (q.ProjectName == null || q.ProjectName.Trim() == string.Empty) &&
                            q.OfficeID == obj.OfficeID  && q.ID != obj.ID && q.DeleteFlag == false).FirstOrDefault();
                    }
                }
            }
            catch
            {
                throw;
            }
            if (objDb != null)
            {
                isDuplicated = true;
            }
            return isDuplicated;
        }

        /// <summary>
        /// Check Service Request Setting Duplicated
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="msg"></param>
        /// <param name="isUpdate"></param>
        /// <returns></returns>
        private bool IsDuplicated(SR_Setting obj, out Message msg,bool isUpdate)
        {
            bool isDuplicated = false;
            SR_Setting objDb =  new SR_Setting();
            msg = null;

            try
            {
                if (!isUpdate)
                {
                    if (!string.IsNullOrEmpty(obj.ProjectName))
                    {
                        objDb = dbContext.SR_Settings.Where(q => q.UserAdminID == obj.UserAdminID && q.ProjectName == obj.ProjectName
                             && q.OfficeID == obj.OfficeID && q.DeleteFlag == false).FirstOrDefault();
                    }
                    else
                    {
                        objDb = dbContext.SR_Settings.Where(q => q.UserAdminID == obj.UserAdminID && (q.ProjectName == null || q.ProjectName.Trim() == string.Empty)
                            && q.OfficeID == obj.OfficeID && q.DeleteFlag == false).FirstOrDefault();
                    }
                }
                else
                {
                    if (!string.IsNullOrEmpty(obj.ProjectName))
                    {
                        objDb = dbContext.SR_Settings.Where(q => q.UserAdminID == obj.UserAdminID && q.ProjectName == obj.ProjectName
                             && q.OfficeID == obj.OfficeID && q.ID != obj.ID && q.DeleteFlag == false).FirstOrDefault();
                    }
                    else
                    {
                        objDb = dbContext.SR_Settings.Where(q => q.UserAdminID == obj.UserAdminID
                             && q.OfficeID == obj.OfficeID && (q.ProjectName == null || q.ProjectName.Trim() == string.Empty) && q.ID != obj.ID && q.DeleteFlag == false).FirstOrDefault();
                    }
                }
            }
            catch
            {
                throw;
            }
            if (objDb != null)
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
        private bool IsValidUpdateDate(SR_Setting objUI, SR_Setting objDb, out Message msg)
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
                            msg = new Message(MessageConstants.E0025, MessageType.Error, "This setting");
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

        /// <summary>
        /// Filter Service Request Setting
        /// </summary>
        /// <param name="list"></param>
        /// <param name="sortColumn"></param>
        /// <param name="sortOrder"></param>
        /// <returns></returns>
        public List<sp_GetSR_SettingResult> Sort(List<sp_GetSR_SettingResult> list, string sortColumn, string sortOrder)
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
                         delegate(sp_GetSR_SettingResult m1, sp_GetSR_SettingResult m2)
                         { return m1.UserName.CompareTo(m2.UserName) * order; });
                    break;
                case "Project":
                    list.Sort(
                         delegate(sp_GetSR_SettingResult m1, sp_GetSR_SettingResult m2)
                         { return ConvertUtil.ConvertToString(m1.ProjectName).CompareTo(ConvertUtil.ConvertToString(m2.ProjectName)) * order; });
                    break;
                case "Office":
                    list.Sort(
                         delegate(sp_GetSR_SettingResult m1, sp_GetSR_SettingResult m2)
                         { return m1.OfficeName.CompareTo(m2.OfficeName) * order; });
                    break;
                case "Active":
                    list.Sort(
                         delegate(sp_GetSR_SettingResult m1, sp_GetSR_SettingResult m2)
                         { return m1.IsActive.CompareTo(m2.IsActive) * order; });
                    break;
            }

            return list;
        }

        /// <summary>
        /// Update Status of Service Request Setting
        /// </summary>
        /// <param name="id"></param>
        /// <param name="isActive"></param>
        /// <returns></returns>
        public Message UpdateActiveStatus(int id, bool isActive,string updateDate,string userName)
        {
            try
            {
                Message msg = null;
                SR_Setting objDb = GetByID(id);
                if (objDb == null)
                    return new Message(MessageConstants.E0005, MessageType.Error,
                        "Selected user", "system");
                if (updateDate != objDb.UpdateDate.ToString())
                {
                    UserAdmin objUserAdmin = new UserAdminDao().GetById(objDb.UserAdminID);
                    msg = new Message(MessageConstants.E0025, MessageType.Error, objUserAdmin != null ? "User Admin " + objUserAdmin.UserName : string.Empty);
                    return msg;
                }
                else
                {
                    objDb.IsActive = isActive;
                    objDb.UpdateDate = DateTime.Now;
                    objDb.UpdatedBy = userName;
                    dbContext.SubmitChanges();
                    return new Message(MessageConstants.I0001, MessageType.Info, " User Admin " + objDb.UserAdmin.UserName, "set " + (isActive ? "active" : "inactive"));
                }
            }
            catch
            {
                return new Message(MessageConstants.E0007, MessageType.Error);
            }
        }


        /// <summary>
        /// Delete List
        /// </summary>
        /// <param name="ids"></param>
        /// <param name="userName"></param>
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
                    int total = idArr.Count();
                    foreach (string id in idArr)
                    {
                        SR_Setting objSetting = GetByID(ConvertUtil.ConvertToInt(id));
                        if (objSetting != null)
                        {
                            objSetting.UpdatedBy = userName;
                            Delete(objSetting);
                        }
                        else
                        {
                            total--;
                        }
                    }

                    // Show succes message
                    msg = new Message(MessageConstants.I0001, MessageType.Info, total.ToString() + " setting(s)", "deleted");
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
        /// Delete Service Request Setting
        /// </summary>
        /// <param name="objUI"></param>
        private void Delete(SR_Setting objUI)
        {
            if (objUI != null)
            {
                // Get current group in dbContext
                SR_Setting objDb = GetByID(objUI.ID);
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
    }
}