using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CRM.Library.Common;

namespace CRM.Models
{
    public class EmployeeStatusDao : BaseDao
    {
        public List<EmployeeStatus> GetList()
        {
            return dbContext.EmployeeStatus.ToList<EmployeeStatus>();
        }

        public EmployeeStatus GetById(int id)
        {
            return dbContext.EmployeeStatus.Where(p => p.StatusId == id).FirstOrDefault<EmployeeStatus>();
        }

        public EmployeeStatus GetByName(string name)
        {
            return dbContext.EmployeeStatus.Where(p => p.StatusName == name).FirstOrDefault<EmployeeStatus>();
        }
    }
}