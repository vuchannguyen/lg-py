using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CRM.Library.Common;

namespace CRM.Models
{
    public class EmployeeDepartmentJobTitleTrackingDao :BaseDao
    {
        public Message Insert(EmployeeDepartmentJobTitleTracking objUI)
        {
            Message msg = null;
            try
            {
                if (objUI != null)
                {
                    // Set more info

                    dbContext.EmployeeDepartmentJobTitleTrackings.InsertOnSubmit(objUI);
                    dbContext.SubmitChanges();

                    // Show success message
                    msg = new Message(MessageConstants.I0001, MessageType.Info, "DepartmentTracking", "added");
                }
            }
            catch (Exception)
            {
                // Show system error
                msg = new Message(MessageConstants.E0007, MessageType.Error);
            }
            return msg;
        }

        public EmployeeDepartmentJobTitleTracking GetLastByEmpId(string empId)
        {
            return dbContext.EmployeeDepartmentJobTitleTrackings.Where(c => c.EmployeeId.Equals(empId)).OrderByDescending(c => c.StartDate).FirstOrDefault<EmployeeDepartmentJobTitleTracking>();
        }

        public void Update(string empId,DateTime startDate,string subDepartment)
        {
            EmployeeDepartmentJobTitleTracking objDb = GetLastByEmpId(empId);
            if (objDb != null)
            {
                objDb.DepartmentName = subDepartment;
                dbContext.SubmitChanges();
            }
        }
    }
}