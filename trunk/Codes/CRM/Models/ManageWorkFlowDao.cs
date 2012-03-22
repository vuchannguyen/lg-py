using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CRM.Library.Common;
using System.Data.Common;

namespace CRM.Models
{
    public class ManageWorkFlowDao : BaseDao
    {
        public List<sp_GetManageWorkFlowResult> GetList(int workflow, int role, int resolution, int? status)
        {
            return dbContext.sp_GetManageWorkFlow(workflow, role, resolution, status).ToList();
        }

        public List<WFResolution> GetResolutionList()
        {
            return dbContext.WFResolutions.Where(q => q.DeletedFlag == false).ToList();
        }

        public List<WFResolution> GetResolutionListByWorkFlow(int wfID)
        {
            return dbContext.WFResolutions.Where(q => q.DeletedFlag == false && q.WFID == wfID).ToList();
        }

        public List<WFRole> GetRoleList()
        {
            return dbContext.WFRoles.Where(q => q.DeleteFlag == false).ToList();
        }

        public List<WFRole> GetRoleListByWorkFlow(int wfID)
        {
            return dbContext.WFRoles.Where(q => q.DeleteFlag == false && q.WFID == wfID).ToList();
        }

        public WFRole_WFResolution GetObjectByRsolutionAndRoleAndHold(int resolution, int role, bool isHold)
        {
            return dbContext.WFRole_WFResolutions.Where(q => q.WFResolutionID == resolution && q.WFRoleID == role && q.IsHold == isHold).FirstOrDefault();
        }

        /// <summary>
        /// Sort Job Title Level
        /// </summary>
        /// <param name="list"></param>
        /// <param name="sortColumn"></param>
        /// <param name="sortOrder"></param>
        /// <returns></returns>
        public List<sp_GetManageWorkFlowResult> Sort(List<sp_GetManageWorkFlowResult> list, string sortColumn, string sortOrder)
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
                case "WorkFlow":
                    list.Sort(
                         delegate(sp_GetManageWorkFlowResult m1, sp_GetManageWorkFlowResult m2)
                         { return m1.WorkFlowName.CompareTo(m2.WorkFlowName) * order; });
                    break;
                case "Role":
                    list.Sort(
                         delegate(sp_GetManageWorkFlowResult m1, sp_GetManageWorkFlowResult m2)
                         { return m1.RoleName.CompareTo(m2.RoleName) * order; });
                    break;
                case "Resolution":
                    list.Sort(
                         delegate(sp_GetManageWorkFlowResult m1, sp_GetManageWorkFlowResult m2)
                         { return m1.ResolutionName.CompareTo(m2.ResolutionName) * order; });
                    break;
                case "Status":
                    list.Sort(
                         delegate(sp_GetManageWorkFlowResult m1, sp_GetManageWorkFlowResult m2)
                         { return m1.IsHold.CompareTo(m2.IsHold) * order; });
                    break;
            }

            return list;
        }

        /// <summary>
        /// Insert to database
        /// </summary>
        /// <param name="objUI"></param>
        /// <returns></returns>
        public Message Insert(WFRole_WFResolution objUI)
        {
            Message msg = null;
            try
            {
                if (objUI != null)
                {
                    if (!IsDuplicated(objUI))
                    {
                        // Set more info                    
                        dbContext.WFRole_WFResolutions.InsertOnSubmit(objUI);
                        dbContext.SubmitChanges();
                        msg = new Message(MessageConstants.I0001, MessageType.Info, "Resolution " + objUI.WFResolution.Name + " of WorkFlow " + new RoleDao().GetWorkFlowByRoleID(objUI.WFRoleID), "assigned to role " + objUI.WFRole.Name);
                    }
                    else
                    {
                        msg = new Message(MessageConstants.E0033, MessageType.Error, "Cannot assigned resolution" + new ResolutionDao().GetByID(objUI.WFResolutionID).Name
                            + "of WorkFlow " + new RoleDao().GetWorkFlowByRoleID(objUI.WFRoleID) +
                          "to role" + new RoleDao().GetByID(objUI.WFRoleID).Name + " because it has been used");
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
        /// Insert to database
        /// </summary>
        /// <param name="objUI"></param>
        /// <returns></returns>
        public Message Update(WFRole_WFResolution objUI, WFRole_WFResolution objOldDb)
        {
            Message msg = null;
            try
            {
                if (objUI != null)
                {

                    if (!IsDuplicated(objUI))
                    {
                        WFRole_WFResolution objDb = GetObjectByRsolutionAndRoleAndHold(objOldDb.WFResolutionID, objOldDb.WFRoleID, objOldDb.IsHold);
                        dbContext.WFRole_WFResolutions.DeleteOnSubmit(objDb);
                        dbContext.WFRole_WFResolutions.InsertOnSubmit(objUI);
                        dbContext.SubmitChanges();
                        msg = new Message(MessageConstants.I0001, MessageType.Info, "Resolution " + new ResolutionDao().GetByID(objUI.WFResolutionID).Name + " of WorkFlow " + new RoleDao().GetWorkFlowByRoleID(objUI.WFRoleID), "assigned to role " + new RoleDao().GetByID(objUI.WFRoleID).Name, "updated");
                    }
                    else
                    {
                        msg = new Message(MessageConstants.E0033, MessageType.Error, "Cannot assigned resolution" + new ResolutionDao().GetByID(objUI.WFResolutionID).Name
                            + "of WorkFlow " + new RoleDao().GetWorkFlowByRoleID(objUI.WFRoleID) +
                          "to role" + new RoleDao().GetByID(objUI.WFRoleID).Name + " because it has been used");
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

        private bool IsDuplicated(WFRole_WFResolution objUI)
        {
            bool isDuplicated = false;
            WFRole_WFResolution obj = dbContext.WFRole_WFResolutions.Where(q => q.WFResolutionID == objUI.WFResolutionID &&
                    q.WFRoleID == objUI.WFRoleID && q.IsHold == objUI.IsHold).FirstOrDefault();
            if (obj != null)
            {
                isDuplicated = true;
            }
            return isDuplicated;
        }

        /// <summary>
        /// Delete a list of job title
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public Message DeleteList(string ids)
        {
            Message msg = null;
            DbTransaction trans = null;
            bool isOK = true;
            try
            {
                //begin transaction
                dbContext.Connection.Open();
                trans = dbContext.Connection.BeginTransaction();
                dbContext.Transaction = trans;

                if (!string.IsNullOrEmpty(ids))
                {
                    //split ids by char ','
                    ids = ids.TrimEnd(':');
                    string[] idArr = ids.Split(':');
                    int totalID = idArr.Count();

                    //loop each id to delete
                    foreach (string sID in idArr)
                    {
                        string[] array = sID.Split(',');
                        int role = int.Parse(array[0]);
                        int resolution = int.Parse(array[1]);
                        bool hold = bool.Parse(array[2]);
                        msg = Delete(resolution, role, hold);
                        if (msg.MsgType == MessageType.Error)
                        {
                            isOK = false;
                            break;
                        }
                    }

                    if (isOK)
                    {
                        // Show succes message
                        msg = new Message(MessageConstants.I0001, MessageType.Info, totalID.ToString() + "  role-resolution(s)", "deleted");
                        trans.Commit();
                    }
                    else
                    {
                        trans.Rollback();
                    }
                }
            }
            catch
            {
                if (trans != null) { trans.Rollback(); }
                // Show system error
                msg = new Message(MessageConstants.E0006, MessageType.Error, "delete", "it");
            }
            return msg;
        }

        /// <summary>
        /// Delete by set DeleteFlag = true
        /// </summary>
        /// <param name="objUI"></param>
        private Message Delete(int resolution, int role, bool hold)
        {
            Message msg = null;
            try
            {
                WFRole_WFResolution objDb = GetObjectByRsolutionAndRoleAndHold(resolution, role, hold);
                if (objDb != null)
                {
                    dbContext.WFRole_WFResolutions.DeleteOnSubmit(objDb);
                    dbContext.SubmitChanges();
                    msg = new Message(MessageConstants.I0001, MessageType.Info, "Job Title Level", "deleted");
                }
                else
                {
                    msg = new Message(MessageConstants.E0006, MessageType.Error, "delete", "it");
                }
            }
            catch
            {
                msg = new Message(MessageConstants.E0006, MessageType.Error, "delete", "it");
            }
            return msg;
        }
    }
}