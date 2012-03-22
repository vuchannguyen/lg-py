using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CRM.Models
{
    public class SRUrgentDao : BaseDao
    {
        public List<SR_Urgency> GetAllSRUrgent()
        {
            return dbContext.SR_Urgencies.ToList();
        }
    }
}