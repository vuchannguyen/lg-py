using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CRM.Library.Common;

namespace CRM.Models
{
    public class CandidateLogDao : BaseDao
    {
        private CommonLogDao commonDao = new CommonLogDao();
        #region Methods

        /// <summary>
        /// Write Log For Employee
        /// </summary>
        /// <param name="oldInfo"></param>
        /// <param name="newInfo"></param>
        /// <param name="action"></param>
        public void WriteLogForCandidate(Candidate oldInfo, Candidate newInfo, ELogAction action)
        {

            try
            {
                if (newInfo == null)
                {
                    return;
                }

                MasterLog objMasterLog = new MasterLog();

                string logId = commonDao.UniqueId;

                switch (action)
                {
                    case ELogAction.Insert:
                        // Insert to Master Log
                        commonDao.InsertMasterLog(logId, newInfo.CreatedBy, ELogTable.Candidate.ToString(), action.ToString());
                        // Write Insert Log
                        WriteInsertLogForCandidate(newInfo, logId);
                        break;
                    case ELogAction.Update:
                        // Insert to Master Log
                        commonDao.InsertMasterLog(logId, newInfo.UpdatedBy, ELogTable.Candidate.ToString(), action.ToString());
                        // Write Update Log
                        bool isUpdated = WriteUpdateLogForCandidate(newInfo, logId);
                        if (!isUpdated)
                        {
                            commonDao.DeleteMasterLog(logId);
                        }
                        break;
                    case ELogAction.Delete:
                        // Insert to Master Log
                        commonDao.InsertMasterLog(logId, newInfo.UpdatedBy, ELogTable.Candidate.ToString(), action.ToString());
                        // Write Delete Log
                        string key = newInfo.ID.ToString() + " [" + newInfo.FirstName + " " + newInfo.MiddleName + " " + newInfo.LastName + "]";
                        commonDao.InsertLogDetail(logId, "ID", "Key for Delete", key, null);
                        break;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Insert Master Log
        /// </summary>
        /// <param name="logId"></param>
        /// <param name="username"></param>
        /// <param name="tableName"></param>
        /// <param name="actionName"></param>
        public void InsertMasterLog(string logId, string username, string tableName, string actionName)
        {
            try
            {
                if ((logId != null) && (!string.Empty.Equals(username)))
                {
                    // Set info
                    MasterLog objInfo = new MasterLog();
                    objInfo.LogId = logId;
                    objInfo.Username = username;
                    objInfo.LogDate = DateTime.Now;
                    objInfo.TableName = tableName;
                    objInfo.ActionName = actionName;

                    // Insert to DB
                    dbContext.MasterLogs.InsertOnSubmit(objInfo);
                    dbContext.SubmitChanges();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Write Insert Log For Employee
        /// </summary>
        /// <param name="objInfo"></param>
        /// <param name="logId"></param>
        private void WriteInsertLogForCandidate(Candidate objInfo, string logId)
        {
            try
            {
                if ((objInfo != null) && (logId != null))
                {
                    // Insert to Log Detail                       
                    commonDao.InsertLogDetail(logId, "ID", "ID", null, objInfo.ID.ToString());
                    commonDao.InsertLogDetail(logId, "FirstName", "First Name", null, objInfo.FirstName);
                    commonDao.InsertLogDetail(logId, "LastName", "Last Name", null, objInfo.LastName);
                    if (!string.IsNullOrEmpty(objInfo.MiddleName))
                    {
                        commonDao.InsertLogDetail(logId, "MiddleName", "Middle Name", null, objInfo.MiddleName);
                    }
                    if (objInfo.UniversityId.HasValue)
                    {
                        commonDao.InsertLogDetail(logId, "University", "University", null, objInfo.University.Name);
                    }
                    commonDao.InsertLogDetail(logId, "VnFirstName", "Vn First Name", null, objInfo.VnFirstName);
                    if (!string.IsNullOrEmpty(objInfo.VnMiddleName))
                    {
                        commonDao.InsertLogDetail(logId, "VnMiddleName", "Vn Middle Name", null, objInfo.VnMiddleName);
                    }
                    commonDao.InsertLogDetail(logId, "VnLastName", "Vn Last Name", null, objInfo.VnLastName);
                    if (objInfo.DOB.HasValue)
                    {
                        commonDao.InsertLogDetail(logId, "DOB", "Date Of Birth", null, objInfo.DOB.Value.ToString(Constants.DATETIME_FORMAT_VIEW));
                    }
                    if (!string.IsNullOrEmpty(objInfo.CellPhone))
                    {
                        commonDao.InsertLogDetail(logId, "CellPhone", "Cell Phone", null, objInfo.CellPhone);
                    }
                    if (!string.IsNullOrEmpty(objInfo.Email))
                    {
                        commonDao.InsertLogDetail(logId, "Email", "Email", null, objInfo.Email);
                    }
                    commonDao.InsertLogDetail(logId, "Gender", "Gender", null, objInfo.Gender == Constants.MALE ? "Male" : "Female");
                    commonDao.InsertLogDetail(logId, "SearchDate", "Searched Date", null, objInfo.SearchDate.ToString(Constants.DATETIME_FORMAT_VIEW));
                    commonDao.InsertLogDetail(logId, "Source", "Source", null, new CandidateDao().GetSourceById(objInfo.SourceId).Name);
                    commonDao.InsertLogDetail(logId, "Job Title", "Job Title", null, new JobTitleLevelDao().GetById(objInfo.TitleId).DisplayName);
                    if (!string.IsNullOrEmpty(objInfo.Address))
                    {
                        commonDao.InsertLogDetail(logId, "Address", "Address", null, objInfo.Address);
                    }
                    if (!string.IsNullOrEmpty(objInfo.Note))
                    {
                        commonDao.InsertLogDetail(logId, "Note", "Note", null, objInfo.Note);
                    }
                    if (!string.IsNullOrEmpty(objInfo.Photograph))
                    {
                        commonDao.InsertLogDetail(logId, "Photograph", "Photograph", null, objInfo.Photograph);
                    }
                    if (!string.IsNullOrEmpty(objInfo.CVFile))
                    {
                        commonDao.InsertLogDetail(logId, "CVFile", "CV File", null, objInfo.CVFile);
                    }

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Write Update Log For Employee
        /// </summary>
        /// <param name="newInfo"></param>
        /// <param name="logId"></param>
        /// <returns></returns>
        private bool WriteUpdateLogForCandidate(Candidate newInfo, string logId)
        {
            bool isUpdated = false;
            try
            {
                // Get old info
                Candidate oldInfo = new CandidateDao().GetById(newInfo.ID.ToString());

                if ((oldInfo != null) && (newInfo != null) && (logId != null))
                {

                    if (oldInfo.FirstName != newInfo.FirstName)
                    {
                        commonDao.InsertLogDetail(logId, "FirstName", "First Name", oldInfo.FirstName, newInfo.FirstName);
                        isUpdated = true;
                    }
                    if (oldInfo.LastName != newInfo.LastName)
                    {
                        commonDao.InsertLogDetail(logId, "LastName", "Last Name", oldInfo.LastName, newInfo.LastName);
                        isUpdated = true;
                    }
                    if (oldInfo.UniversityId != newInfo.UniversityId)
                    {

                        commonDao.InsertLogDetail(logId, "LastName", "Last Name", oldInfo.UniversityId.HasValue?oldInfo.University.Name:"",
                            newInfo.UniversityId.HasValue?new CandidateDao().GetUniversityByID(newInfo.UniversityId.Value).Name:"");
                        isUpdated = true;
                    }
                    if (oldInfo.MiddleName != newInfo.MiddleName)
                    {
                        commonDao.InsertLogDetail(logId, "MiddleName", "Middle Name", oldInfo.MiddleName, newInfo.MiddleName);
                        isUpdated = true;
                    }
                    if (oldInfo.VnFirstName != newInfo.VnFirstName)
                    {
                        commonDao.InsertLogDetail(logId, "VnFirstName", "Vn First Name", oldInfo.VnFirstName, newInfo.VnFirstName);
                        isUpdated = true;
                    }
                    if (oldInfo.VnMiddleName != newInfo.VnMiddleName)
                    {
                        commonDao.InsertLogDetail(logId, "VnMiddleName", "Vn Middle Name", oldInfo.VnFirstName, newInfo.VnMiddleName);
                        isUpdated = true;
                    }
                    if (oldInfo.VnLastName != newInfo.VnLastName)
                    {
                        commonDao.InsertLogDetail(logId, "VnLastName", "Vn Last Name", oldInfo.VnLastName, newInfo.VnLastName);
                        isUpdated = true;
                    }
                    if (oldInfo.DOB != newInfo.DOB)
                    {
                        isUpdated = true;
                        commonDao.InsertLogDetail(logId, "DOB", "Date Of Birth",oldInfo.DOB.HasValue?oldInfo.DOB.Value.ToString(Constants.DATETIME_FORMAT_VIEW):"", newInfo.DOB.HasValue?newInfo.DOB.Value.ToString(Constants.DATETIME_FORMAT_VIEW):"");
                    }
                    if (oldInfo.CellPhone != newInfo.CellPhone)
                    {
                        isUpdated = true;
                        commonDao.InsertLogDetail(logId, "CellPhone", "Cell Phone", oldInfo.CellPhone, newInfo.CellPhone);
                    }
                    if (oldInfo.Email != newInfo.Email)
                    {
                        isUpdated = true;
                        commonDao.InsertLogDetail(logId, "Email", "Email", oldInfo.Email, newInfo.Email);
                    }
                    if (oldInfo.Gender != newInfo.Gender)
                    {
                        commonDao.InsertLogDetail(logId, "SearchBy", "Search By", oldInfo.Gender == Constants.MALE ? "Male" : "Female", newInfo.Gender == Constants.MALE ? "Male" : "Female");
                        isUpdated = true;
                    }
                    if (oldInfo.SearchDate != newInfo.SearchDate)
                    {
                        isUpdated = true;
                        commonDao.InsertLogDetail(logId, "SearchDate", "Searched Date", oldInfo.SearchDate.ToString(Constants.DATETIME_FORMAT_VIEW), newInfo.SearchDate.ToString(Constants.DATETIME_FORMAT_VIEW));
                    }
                    if (oldInfo.SourceId != newInfo.SourceId)
                    {
                        isUpdated = true;
                        commonDao.InsertLogDetail(logId, "Source", "Source", oldInfo.CandidateSource.Name, new CandidateDao().GetSourceById(newInfo.SourceId).Name);
                    }
                    if (oldInfo.TitleId != newInfo.TitleId)
                    {
                        isUpdated = true;
                        commonDao.InsertLogDetail(logId, "Job Title", "Job Title", oldInfo.JobTitleLevel.DisplayName, new JobTitleLevelDao().GetById(newInfo.TitleId).DisplayName);
                    }
                    if (oldInfo.Address != newInfo.Address)
                    {
                        isUpdated = true;
                        commonDao.InsertLogDetail(logId, "Address", "Address", oldInfo.Address, newInfo.Address);
                    }
                    if (oldInfo.Note != newInfo.Note)
                    {
                        isUpdated = true;
                        commonDao.InsertLogDetail(logId, "Note", "Note", oldInfo.Note, newInfo.Note);
                    }
                    if (oldInfo.Photograph != newInfo.Photograph)
                    {
                        isUpdated = true;
                        commonDao.InsertLogDetail(logId, "Photograph", "Photograph", oldInfo.Photograph, newInfo.Photograph);
                    }
                    if (oldInfo.CVFile != newInfo.CVFile)
                    {
                        isUpdated = true;
                        commonDao.InsertLogDetail(logId, "CVFile", "CV File", null, newInfo.CVFile);
                    }
                    // Insert Key Name
                    if (isUpdated)
                    {
                        string key = oldInfo.ID + " [" + oldInfo.FirstName + " " + oldInfo.MiddleName + " " + oldInfo.LastName + "]";
                        commonDao.InsertLogDetail(logId, "ID", "Key for Update", key, null);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return isUpdated;
        }

        public void WriteLogForUpdateImage(Candidate newInfo, ELogAction action)
        {

            try
            {
                bool isUpdated = false;
                if (newInfo == null)
                {
                    return;
                }
                MasterLog objMasterLog = new MasterLog();
                string logId = commonDao.UniqueId;
                commonDao.InsertMasterLog(logId, newInfo.UpdatedBy, ELogTable.Candidate.ToString(), action.ToString());
                Candidate oldInfo = new CandidateDao().GetById(newInfo.ID.ToString());

                if ((oldInfo != null) && (newInfo != null) && (logId != null))
                {
                    if (newInfo.Photograph != oldInfo.Photograph)
                    {
                        commonDao.InsertLogDetail(logId, "Photograph", "Photograph", oldInfo.Photograph, newInfo.Photograph);
                        isUpdated = true;
                    }
                    if (isUpdated)
                    {
                        // Insert Key Name
                        string key = oldInfo.ID + " [" + oldInfo.FirstName + " " + oldInfo.MiddleName + " " + oldInfo.LastName + "]";
                        commonDao.InsertLogDetail(logId, "ID", "Key for Update", key, null);
                    }
                    else
                    {
                        commonDao.DeleteMasterLog(logId);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void WriteLogForUpdateJR(Candidate newInfo, ELogAction action)
        {

            try
            {
                bool isUpdated = false;
                if (newInfo == null)
                {
                    return;
                }
                MasterLog objMasterLog = new MasterLog();
                string logId = commonDao.UniqueId;
                commonDao.InsertMasterLog(logId, newInfo.UpdatedBy, ELogTable.Candidate.ToString(), action.ToString());
                Candidate oldInfo = new CandidateDao().GetById(newInfo.ID.ToString());

                if ((oldInfo != null) && (newInfo != null) && (logId != null))
                {
                    if (newInfo.JRId != oldInfo.JRId)
                    {
                        commonDao.InsertLogDetail(logId, "JRId", "Job Request", oldInfo.JRId.HasValue ? oldInfo.JRId.Value.ToString() : "", newInfo.JRId.HasValue ? newInfo.JRId.Value.ToString() : "");
                        isUpdated = true;
                    }
                    if (isUpdated)
                    {
                        // Insert Key Name
                        string key = oldInfo.ID + " [" + oldInfo.FirstName + " " + oldInfo.MiddleName + " " + oldInfo.LastName + "]";
                        commonDao.InsertLogDetail(logId, "ID", "Key for Update", key, null);
                    }
                    else
                    {
                        commonDao.DeleteMasterLog(logId);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public void WriteLogForUpdateHistory(Candidate newInfo, ELogAction action)
        {

            try
            {
                bool isUpdated = false;
                if (newInfo == null)
                {
                    return;
                }
                MasterLog objMasterLog = new MasterLog();
                string logId = commonDao.UniqueId;
                commonDao.InsertMasterLog(logId, newInfo.UpdatedBy, ELogTable.Candidate.ToString(), action.ToString());
                Candidate oldInfo = new CandidateDao().GetById(newInfo.ID.ToString());

                if ((oldInfo != null) && (newInfo != null) && (logId != null))
                {
                    if (newInfo.SaveHistory != oldInfo.SaveHistory)
                    {
                        commonDao.InsertLogDetail(logId, "SaveHistory", "Save History", oldInfo.SaveHistory.HasValue?oldInfo.SaveHistory.Value == false?"No":"Yes":"", newInfo.SaveHistory.HasValue?newInfo.SaveHistory.Value == false?"No":"Yes":"");
                        isUpdated = true;
                    }
                    if (isUpdated)
                    {
                        // Insert Key Name
                        string key = oldInfo.ID + " [" + oldInfo.FirstName + " " + oldInfo.MiddleName + " " + oldInfo.LastName + "]";
                        commonDao.InsertLogDetail(logId, "ID", "Key for Update", key, null);
                    }
                    else
                    {
                        commonDao.DeleteMasterLog(logId);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void WriteLogForUpdateCV(Candidate newInfo, ELogAction action)
        {

            try
            {
                bool isUpdated = false;
                if (newInfo == null)
                {
                    return;
                }
                MasterLog objMasterLog = new MasterLog();
                string logId = commonDao.UniqueId;
                commonDao.InsertMasterLog(logId, newInfo.UpdatedBy, ELogTable.Candidate.ToString(), action.ToString());
                Candidate oldInfo = new CandidateDao().GetById(newInfo.ID.ToString());

                if ((oldInfo != null) && (newInfo != null) && (logId != null))
                {
                    if (newInfo.CVFile != oldInfo.CVFile)
                    {
                        commonDao.InsertLogDetail(logId, "CVFile", "CVFile", oldInfo.CVFile, newInfo.CVFile);
                        isUpdated = true;
                    }
                    if (isUpdated)
                    {
                        // Insert Key Name
                        string key = oldInfo.ID + " [" + oldInfo.FirstName + " " + oldInfo.MiddleName + " " + oldInfo.LastName + "]";
                        commonDao.InsertLogDetail(logId, "ID", "Key for Update", key, null);
                    }
                    else
                    {
                        commonDao.DeleteMasterLog(logId);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
    }
}