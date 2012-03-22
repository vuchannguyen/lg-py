using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CRM.Models
{
    public class WFStatusDao : BaseDao
    {
        #region Public methods

        public List<WFStatus> GetList()
        {
            return dbContext.WFStatus.Where(c => c.DeleteFlag == false).ToList<WFStatus>();
        }

        public List<WFStatus> GetList(int wfId, int roleId, int resolutionId)
        {
            return dbContext.WFStatus.Where(c => c.DeleteFlag == false).ToList<WFStatus>();
        }

        public WFStatus GetByID(int id)
        {
            return dbContext.WFStatus.Where(c => c.ID.Equals(id)).FirstOrDefault<WFStatus>();
        }

        #endregion
    }
}