using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CRM.Library.Common;

namespace CRM.Models
{
    public class InsuranceHospitalDao  :BaseDao
    {
        public List<InsuranceHospital> GetList()
        {
            return dbContext.InsuranceHospitals.ToList<InsuranceHospital>();
        }

        public InsuranceHospital GetById(string id)
        {
            return dbContext.InsuranceHospitals.Where(c => c.ID.Equals(id)).FirstOrDefault<InsuranceHospital>();
        }

        public InsuranceHospital GetByName(string id)
        {
            return dbContext.InsuranceHospitals.Where(c => c.Name == id).FirstOrDefault<InsuranceHospital>();
        }

        public List<EmployeeInsuranceHospitalTracking> GetListHistoryById(string id)
        {
            return dbContext.EmployeeInsuranceHospitalTrackings.Where(c => c.EmployeeId.Equals(id)).ToList<EmployeeInsuranceHospitalTracking>();
        }

        public string GetHospitalName(string id)
        {
            string value = string.Empty;
            InsuranceHospital obj = GetById(id);
            if (obj != null)
            {
                value += obj.Name;
            }
            return value;
        }

        public Message Insert(EmployeeInsuranceHospitalTracking objUI)
        {
            Message msg = null;
            try
            {
                if (objUI != null)
                {
                    // Set more info

                    dbContext.EmployeeInsuranceHospitalTrackings.InsertOnSubmit(objUI);
                    dbContext.SubmitChanges();

                    // Show success message
                    msg = new Message(MessageConstants.I0001, MessageType.Info, "EmployeeInsuranceHospitalTrackings", "added");
                }
            }
            catch (Exception)
            {
                // Show system error
                msg = new Message(MessageConstants.E0007, MessageType.Error);
            }
            return msg;
        }

        public EmployeeInsuranceHospitalTracking GetLastByEmpId(string empId)
        {
            return dbContext.EmployeeInsuranceHospitalTrackings.Where(c => c.EmployeeId.Equals(empId)).OrderByDescending(c => c.StartDate).FirstOrDefault<EmployeeInsuranceHospitalTracking>();
        }

        public List<InsuranceHospital> Sort(List<InsuranceHospital> list, string sortColumn, string sortOrder)
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
                case "ID":
                    list.Sort(
                         delegate(InsuranceHospital m1, InsuranceHospital m2)
                         { return m1.ID.CompareTo(m2.ID) * order; });
                    break;
                case "Name":
                    list.Sort(
                         delegate(InsuranceHospital m1, InsuranceHospital m2)
                         { return m1.Name.CompareTo(m2.Name) * order; });
                    break;
                case "Address":
                    list.Sort(
                         delegate(InsuranceHospital m1, InsuranceHospital m2)
                         { return m1.Address.CompareTo(m2.Address) * order; });
                    break;
                case "IsPublic":
                    list.Sort(
                         delegate(InsuranceHospital m1, InsuranceHospital m2)
                         { return m1.IsPublic.CompareTo(m2.IsPublic) * order; });
                    break;
               
            }

            return list;
        }
    }
}