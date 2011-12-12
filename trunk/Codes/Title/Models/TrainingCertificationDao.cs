using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CRM.Library.Common;
using System.Data.Common;
using System.Text.RegularExpressions;


namespace CRM.Models
{
    public class TrainingCertificationDao : BaseDao
    {
        public List<sp_GetTrainingCertificationResult> GetTrainingCertificationList(string certificationName)
        {
            return dbContext.sp_GetTrainingCertification(certificationName).OrderByDescending(t => t.ID).ToList<sp_GetTrainingCertificationResult>();
        }

        public List<sp_GetTrainingCertificationResult> Sort(List<sp_GetTrainingCertificationResult> list, string sortColumn, string sortOrder)
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
                         delegate(sp_GetTrainingCertificationResult m1, sp_GetTrainingCertificationResult m2)
                         { return m1.ID.CompareTo(m2.ID) * order; });
                    break;
                case "Name":
                    list.Sort(
                         delegate(sp_GetTrainingCertificationResult m1, sp_GetTrainingCertificationResult m2)
                         { return m1.Name.CompareTo(m2.Name) * order; });
                    break;
                case "Description":
                    list.Sort(
                         delegate(sp_GetTrainingCertificationResult m1, sp_GetTrainingCertificationResult m2)
                         { return m1.Description.CompareTo(m2.Description) * order; });
                    break;
                case "IsActive":
                    list.Sort(
                         delegate(sp_GetTrainingCertificationResult m1, sp_GetTrainingCertificationResult m2)
                         { return m1.IsActive.CompareTo(m2.IsActive) * order; });
                    break;
                case "CreateDate":
                    list.Sort(
                         delegate(sp_GetTrainingCertificationResult m1, sp_GetTrainingCertificationResult m2)
                         { return m1.CreateDate.CompareTo(m2.CreateDate) * order; });
                    break;
                case "CreatedBy":
                    list.Sort(
                         delegate(sp_GetTrainingCertificationResult m1, sp_GetTrainingCertificationResult m2)
                         { return m1.CreatedBy.CompareTo(m2.CreatedBy) * order; });
                    break;
                case "UpdateDate":
                    list.Sort(
                         delegate(sp_GetTrainingCertificationResult m1, sp_GetTrainingCertificationResult m2)
                         { return m1.UpdateDate.CompareTo(m2.UpdateDate) * order; });
                    break;
                case "UpdatedBy":
                    list.Sort(
                         delegate(sp_GetTrainingCertificationResult m1, sp_GetTrainingCertificationResult m2)
                         { return m1.UpdatedBy.CompareTo(m2.UpdatedBy) * order; });
                    break;
            }

            return list;
        }
        public Message UpdateActiveStatus(int cusId, bool isActive)
        {
            try
            {
                Training_CertificationMaster obj = dbContext.Training_CertificationMasters.Where(p => p.ID == cusId).FirstOrDefault();
                if (obj == null)
                    return new Message(MessageConstants.E0005, MessageType.Error,
                        "Selected certification", "system");
                obj.IsActive = isActive;
                dbContext.SubmitChanges();
                return new Message(MessageConstants.I0001, MessageType.Info, "Certification " + obj.ID +
                    " in group \"" + obj.Name + "\"", "set " + (isActive ? "active" : "inactive"));
            }
            catch
            {
                return new Message(MessageConstants.E0007, MessageType.Error);
            }
        }

        public Training_CertificationMaster GetById(string id)
        {
            return dbContext.Training_CertificationMasters.Where(c => c.ID.Equals(id)).FirstOrDefault<Training_CertificationMaster>();
        }

        private void Delete(Training_CertificationMaster objUI)
        {
            if (objUI != null)
            {
                // Get current group in dbContext
                Training_CertificationMaster objDb = GetById(objUI.ID.ToString());
                if (objDb != null)
                {
                    // Set delete info                    
                    objDb.DeleteFlag = true;
                    objDb.UpdateDate = DateTime.Now;
                    objDb.UpdatedBy = objUI.UpdatedBy;
                    // Submit changes to dbContext
                    //new Customer1LogDao().WriteLogForCustomer(null, objUI, ELogAction.Delete);
                    dbContext.SubmitChanges();

                }
            }
        }

        public Message DeleteList(string ids, string userName)
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
                        Training_CertificationMaster trainingCer = GetById(empID);
                        if (trainingCer != null)
                        {
                            trainingCer.UpdatedBy = userName;
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

        public Message Insert(Training_CertificationMaster objUI)
        {
            Message msg = null;
            try
            {
                if (objUI != null)
                {
                    objUI.CreateDate = DateTime.Now;
                    objUI.UpdateDate = DateTime.Now;
                    objUI.DeleteFlag = false;
                    dbContext.Training_CertificationMasters.InsertOnSubmit(objUI);
                    dbContext.SubmitChanges();
                    // Show success message
                    msg = new Message(MessageConstants.I0001, MessageType.Info, "Certification '" + objUI.Name + "'", "added");
                }
            }
            catch (Exception ex)
            {
                msg = new Message(MessageConstants.E0007, MessageType.Error);
                throw ex;
            }
            return msg;

        }

        public Message UpdateTrainingCertification(Training_CertificationMaster objUI)
        {
            Message msg = null;

            try
            {
                if (objUI != null)
                {
                    // Get current group in dbContext
                    Training_CertificationMaster objDb = GetById(objUI.ID.ToString());

                    if (objDb != null)
                    {
                        // Check valid update date
                        if (IsValidUpdateDate(objUI, objDb, out msg))
                        {
                            //new Customer1LogDao().WriteLogForUpdateCustomer(objUI, ELogAction.Update);
                            // Update info by objUI                       
                            objDb.Name = objUI.Name;
                            objDb.Description = objUI.Description;
                            objDb.IsActive = objUI.IsActive;
                            objDb.UpdateDate = DateTime.Now;
                            objDb.UpdatedBy = objUI.UpdatedBy;

                            // Submit changes to dbContext
                            dbContext.SubmitChanges();

                            // Show success message
                            msg = new Message(MessageConstants.I0001, MessageType.Info, "Certification '" + objDb.Name + "'", "updated");
                        }
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

        private bool IsValidUpdateDate(Training_CertificationMaster objUI, Training_CertificationMaster objDb, out Message msg)
        {
            bool isValid = false;
            msg = null;

            try
            {
                if ((objUI != null) && (objUI.UpdateDate != null))
                {
                    if (objDb != null)
                    {
                        if (objDb.UpdateDate.ToString().Equals(objUI.UpdateDate.ToString()))
                        {
                            isValid = true;
                        }
                        else
                        {
                            msg = new Message(MessageConstants.E0025, MessageType.Error, "Training Certification ID " + objDb.ID);
                        }
                    }
                }
            }
            catch
            {
                throw;
            }

            return isValid;
        }
    }
       
}