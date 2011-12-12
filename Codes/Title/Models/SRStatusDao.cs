using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CRM.Library.Common;

namespace CRM.Models
{
    public class SRStatusDao : BaseDao
    {

        public SR_Status GetByID(int id)
        {
            return dbContext.SR_Status.Where(c => c.ID == id).FirstOrDefault<SR_Status>();            
        }

        public List<SR_Status> GetAll()
        {
            return dbContext.SR_Status.ToList();
        }
    }
}