using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CRM.Library.Common;
using System.Data.Common;
using System.Text.RegularExpressions;


namespace CRM.Models
{
    public class TrainingEmployeeCertificationDao : BaseDao
    {
        public List<sp_GetEmployeeCertificationResult> GetTrainingEmployeeCertificationList(string Name, string jobTitle, string manager, int certification)
        {
            return dbContext.sp_GetEmployeeCertification(Name, jobTitle, manager, certification).OrderByDescending(t => t.ID).ToList<sp_GetEmployeeCertificationResult>();
        }

        public Employee_Certification GetById(string id)
        {
            return dbContext.Employee_Certifications.Where(e => e.ID.Equals(id)).FirstOrDefault<Employee_Certification>();
        }

        public List<sp_GetEmployeeCertificationManagerResult> GetEmployeeCertificationManagerList()
        {
            return dbContext.sp_GetEmployeeCertificationManager().OrderByDescending(p => p.ManagerId).ToList<sp_GetEmployeeCertificationManagerResult>();
        }

        public List<Training_CertificationMaster> GetTrainingCertificationList()
        {
            return dbContext.Training_CertificationMasters.ToList();
        }

        public List<sp_GetEmployeeJobtitleCertificationResult> GetEmployeeJobtitleCertificationList()
        {
            return dbContext.sp_GetEmployeeJobtitleCertification().OrderByDescending(p => p.JobTitleId).ToList<sp_GetEmployeeJobtitleCertificationResult>();
        }

        public List<sp_GetEmployeeCertificationResult> Sort(List<sp_GetEmployeeCertificationResult> list, string sortColumn, string sortOrder)
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
                         delegate(sp_GetEmployeeCertificationResult m1, sp_GetEmployeeCertificationResult m2)
                         { return m1.ID.CompareTo(m2.ID) * order; });
                    break;
                case "DisplayName":
                    list.Sort(
                         delegate(sp_GetEmployeeCertificationResult m1, sp_GetEmployeeCertificationResult m2)
                         { return m1.DisplayName.CompareTo(m2.DisplayName) * order; });
                    break;
                case "JobTitleName":
                    list.Sort(
                         delegate(sp_GetEmployeeCertificationResult m1, sp_GetEmployeeCertificationResult m2)
                         { return m1.TitleName.CompareTo(m2.TitleName) * order; });
                    break;
                case "ManagerName":
                    list.Sort(
                         delegate(sp_GetEmployeeCertificationResult m1, sp_GetEmployeeCertificationResult m2)
                         { return m1.ManagerName.CompareTo(m2.ManagerName) * order; });
                    break;
                case "Name":
                    list.Sort(
                         delegate(sp_GetEmployeeCertificationResult m1, sp_GetEmployeeCertificationResult m2)
                         { return m1.Name.CompareTo(m2.Name) * order; });
                    break;
                case "Remark":
                    list.Sort(
                         delegate(sp_GetEmployeeCertificationResult m1, sp_GetEmployeeCertificationResult m2)
                         { return m1.Remark.CompareTo(m2.Remark) * order; });
                    break;

            }

            return list;
        }

        public Message Insert(Employee_Certification objUI)
        {
            Message msg = null;
            try
            {
                if (objUI != null)
                {
                   
                    dbContext.Employee_Certifications.InsertOnSubmit(objUI);
                    dbContext.SubmitChanges();
                    // Show success message
                    msg = new Message(MessageConstants.I0001, MessageType.Info, "Certification '" + objUI.CertificationId + "'", "added");
                }
            }
            catch (Exception ex)
            {
                msg = new Message(MessageConstants.E0007, MessageType.Error);
                throw ex;
            }
            return msg;

        }

        public Message UpdateTrainingEmployeeCertification(Employee_Certification objUI)
        {
            Message msg = null;

            try
            {
                if (objUI != null)
                {
                    // Get current group in dbContext
                    Employee_Certification objDb = GetById(objUI.ID.ToString());

                    if (objDb != null)
                    {
                        objDb.EmployeeId = objUI.EmployeeId;
                        objDb.CertificationId = objUI.CertificationId;
                        objDb.Remark = objUI.Remark;
                        dbContext.SubmitChanges();
                        msg = new Message(MessageConstants.I0001, MessageType.Info, "Certification '" + objDb.CertificationId + "'", "updated");
                        
                    }
                }
            }
            catch
            {
                // Show system error
                msg = new Message(MessageConstants.E0007, MessageType.Error);
            }

            return msg;
        }

        private void Delete(Employee_Certification objUI)
        {
            if (objUI != null)
            {
                // Get current group in dbContext
                Employee_Certification objDb = GetById(objUI.EmployeeId.ToString());
                if (objDb != null)
                {
                    // Set delete info                    
                    // Submit changes to dbContext
                    //new Customer1LogDao().WriteLogForCustomer(null, objUI, ELogAction.Delete);
                    dbContext.Employee_Certifications.DeleteOnSubmit(objDb);
                    dbContext.SubmitChanges();

                }
            }
        }

        public Message DeleteList(string ids)
        {
            Message msg = null;
            DbTransaction trans = null;
            try
            {
                dbContext.Connection.Open();
                trans = dbContext.Connection.BeginTransaction();
                dbContext.Transaction = trans;

                if (!string.IsNullOrEmpty(ids))
                {
                    ids = ids.TrimEnd(',');
                    string[] idArr = ids.Split(',');
                    int total = idArr.Count();
                    foreach (string id in idArr)
                    {
                        string empID = id;
                        Employee_Certification trainingCer = GetById(empID);
                        if (trainingCer != null)
                        {
                            //trainingCer.UpdatedBy = userName;
                            Delete(trainingCer);
                        }
                        else
                        {
                            total--;
                        }
                    }

                    // Show succes message
                    msg = new Message(MessageConstants.I0001, MessageType.Info, total.ToString() + " Certification(s)", "deleted");
                    trans.Commit();
                }
            }
            catch
            {
                if (trans != null) trans.Rollback();
                // Show system error
                msg = new Message(MessageConstants.E0007, MessageType.Error);
            }
            return msg;
        }
    }
}