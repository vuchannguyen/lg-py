using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CRM.Models;
using CRM.Library.Common;

namespace CRM.Models
{
    public class WorkflowDao : BaseDao
    {
        #region Private Fields

        private AuthenticateDao auDao = new AuthenticateDao();
        #endregion

        #region Public methods

         /// <summary>
        /// Get List
        /// </summary>
        /// <returns></returns>
        public List<WorkFlow> GetList(bool isActive)
        {
           return dbContext.WorkFlows.Where(p => (p.DeleteFlag == false && p.IsActive == Constants.IS_ACTIVE))
                    .OrderByDescending(p => p.ID).OrderBy(i =>i.ID).ToList<WorkFlow>();
        }

        public List<WorkFlow> GetListByID(int workflow)
        {
            return dbContext.WorkFlows.Where(p => (p.ID == workflow && p.DeleteFlag == false && p.IsActive == true))
                     .OrderByDescending(p => p.ID).OrderBy(i => i.ID).ToList<WorkFlow>();
        }

        #endregion
    }
}