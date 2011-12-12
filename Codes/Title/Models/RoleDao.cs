using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CRM.Models;
using CRM.Library.Common;

namespace CRM.Models
{
    public class RoleDao : BaseDao
    {
        #region

        private AuthenticateDao auDao = new AuthenticateDao();

        #endregion

        #region Public methods

        public List<WFRole> GetList()
        {
            // Get by isActive
            return dbContext.WFRoles.Where(p => p.DeleteFlag == false).ToList<WFRole>();
        }

        public WFRole GetByResolutionID(int resID)
        {
            return dbContext.WFRole_WFResolutions.Where(p => p.WFResolutionID == resID).FirstOrDefault().WFRole;
        }

        public string GetWorkFlowByRoleID(int role)
        {
            WFRole obj = GetByID(role);
           return obj.WorkFlow.Name;
        }

        /// <summary>
        /// Get List
        /// </summary>
        /// <returns></returns>
        public List<WFRole> GetList(bool isActive)
        {
            // Get by isActive
            return dbContext.WFRoles.Where(p => (p.DeleteFlag == false))
                .OrderByDescending(p => p.ID).OrderBy( i =>i.ID).ToList<WFRole>();
        }

        /// <summary>
        /// Get List By Workflow
        /// </summary>
        /// <returns></returns>
        public List<WFRole> GetListByWorkflow(int wfID)
        {
            // Get by isActive
            return dbContext.WFRoles.Where(p => (p.WFID == wfID && p.DeleteFlag == false)).ToList<WFRole>();
        }

        /// <summary>
        /// Get Workflow By Role
        /// </summary>
        /// <returns></returns>
        public WFRole GetWorkflowByRole(int role)
        {
            return dbContext.WFRoles.Where(p => (p.ID == role && p.DeleteFlag == false)).FirstOrDefault<WFRole>();
        }

        public WFRole GetByID(int id)
        {
            return dbContext.WFRoles.Where(p => p.ID == id).FirstOrDefault<WFRole>();
        }

        #endregion
    }
}