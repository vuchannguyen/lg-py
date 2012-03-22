using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CRM.Library.Common;

namespace CRM.Models
{
    public class StatusDao : BaseDao
    {
        public List<WFStatus> GetListStatusByResolution(int resolutionId)
        {
            List<WFStatus> list = new List<WFStatus>();
            var entities = from e in dbContext.WFResolution_WFStatus
                           join f in dbContext.WFStatus on e.WFStatusID equals f.ID
                           where e.WFResolutionID == resolutionId

                           select new
                           {
                               ID = f.ID,
                               Name = f.Name
                           };

            foreach (var e in entities)
            {
                WFStatus status = new WFStatus();
                status.ID = e.ID;
                status.Name = e.Name;
                list.Add(status);
            }

            return list;
        }

        public WFStatus GetByID(int id)
        {
            return dbContext.WFStatus.Where(c => c.ID == id && c.DeleteFlag == false).FirstOrDefault<WFStatus>();            
        }

        public List<WFStatus> GetAll()
        {
            return dbContext.WFStatus.ToList();
        }
    }
}