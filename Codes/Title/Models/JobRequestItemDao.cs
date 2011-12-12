using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CRM.Library.Common;

namespace CRM.Models
{
    public class JobRequestItemDao:BaseDao
    {
        public JobRequestItem GetByID(string id)
        {
            id = id.Replace(Constants.JOB_REQUEST_ITEM_PREFIX, string.Empty);
            return dbContext.JobRequestItems.FirstOrDefault(p => p.ID == int.Parse(id));
        }

        public List<JobRequestItem> GetListByJrId(int id)
        {
            return dbContext.JobRequestItems.Where(p => p.JRID == id).ToList();
        }
    }
}