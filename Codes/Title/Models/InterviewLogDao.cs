using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CRM.Library.Common;

namespace CRM.Models
{
    public class InterviewLogDao : BaseDao
    {
        private CommonLogDao commonDao = new CommonLogDao();

        /// <summary>
        /// Write Log For Employee
        /// </summary>
        /// <param name="oldInfo"></param>
        /// <param name="newInfo"></param>
        /// <param name="action"></param>
        public void WriteLogForInterview(Interview oldInfo, Interview newInfo, ELogAction action)
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
                    //case ELogAction.Insert:
                    //    // Insert to Master Log
                    //    commonDao.InsertMasterLog(logId, newInfo.CreatedBy, ELogTable.Interview.ToString(), action.ToString());
                    //    // Write Insert Log
                    //    WriteInsertLogForInterview(newInfo, logId);
                    //    break;
                    //case ELogAction.Update:
                    //    // Insert to Master Log
                    //    commonDao.InsertMasterLog(logId, newInfo.UpdatedBy, ELogTable.Interview.ToString(), action.ToString());
                    //    // Write Update Log
                    //    bool isUpdated = WriteUpdateLogForInterview(newInfo, logId);
                    //    if (!isUpdated)
                    //    {
                    //        commonDao.DeleteMasterLog(logId);
                    //    }
                    //    break;
                    case ELogAction.Delete:
                        // Insert to Master Log
                        commonDao.InsertMasterLog(logId, newInfo.UpdatedBy, ELogTable.Candidate.ToString(), action.ToString());
                        // Write Delete Log
                        string key = newInfo.CandidateId.ToString() + " [" + newInfo.Candidate.FirstName + " " + newInfo.Candidate.MiddleName + " " + newInfo.Candidate.LastName + "]" + " for Interview in round " 
                            + (newInfo.Round.HasValue?newInfo.Round.Value.ToString():"") + " in " + (newInfo.InterviewDate.HasValue?newInfo.InterviewDate.Value.ToString(Constants.DATETIME_FORMAT_VIEW):"");
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
        /// Write Insert Log For Interview
        /// </summary>
        /// <param name="objInfo"></param>
        /// <param name="logId"></param>
        public void WriteInsertLogForInterview(Interview objInfo, string templateRound, string jr, ELogAction action)
        {
            try
            {
                string logId = commonDao.UniqueId;
                if ((objInfo != null) && (logId != null))
                {
                    commonDao.InsertMasterLog(logId, objInfo.CreatedBy, ELogTable.Interview.ToString(), action.ToString());
                    // Insert to Log Detail                       
                    commonDao.InsertLogDetail(logId, "ID", "ID", null, objInfo.Id.ToString());
                    if (objInfo.InterviewDate.HasValue)
                    {
                        commonDao.InsertLogDetail(logId, "InterviewDate", "Interview Date", null, objInfo.InterviewDate.Value.ToString(Constants.DATETIME_FORMAT_FULL_LOG));
                    }
                    if (!string.IsNullOrEmpty(objInfo.Venue))
                    {
                        commonDao.InsertLogDetail(logId, "Venue", "Venue", null, objInfo.Venue);
                    }
                    if (!string.IsNullOrEmpty(objInfo.Pic))
                    {
                        commonDao.InsertLogDetail(logId, "Pic", "Pic", null, objInfo.Pic);
                    }
                    if (!string.IsNullOrEmpty(objInfo.Content))
                    {
                        commonDao.InsertLogDetail(logId, "Content", "Content", null, objInfo.Content);
                    }
                    if (objInfo.CandidateId.HasValue)
                    {
                        commonDao.InsertLogDetail(logId, "CandidateId", "Candidate Id", null, objInfo.CandidateId.Value.ToString());
                    }
                    if (objInfo.InterviewStatusId.HasValue)
                    {
                        commonDao.InsertLogDetail(logId, "InterviewStatusId", "Interview Status Id", null, new InterviewStatusDao().GetById(objInfo.InterviewStatusId.Value).Name);
                    }
                    if (objInfo.Round.HasValue)
                    {
                        commonDao.InsertLogDetail(logId, "Round", "Round", null, objInfo.Round.Value.ToString());
                    }
                    if (objInfo.InterviewResultId.HasValue)
                    {
                        commonDao.InsertLogDetail(logId, "InterviewResultId", "Interview Result Id", null, new InterviewResultDao().GetById(objInfo.InterviewResultId.Value).Name);
                    }
                    if (!string.IsNullOrEmpty(objInfo.Note))
                    {
                        commonDao.InsertLogDetail(logId, "Note", "Note", null, objInfo.Note);
                    }
                    if (objInfo.OldInterView.HasValue)
                    {
                        commonDao.InsertLogDetail(logId, "OldInterView", "Old InterView", null, objInfo.OldInterView.HasValue ? objInfo.OldInterView.Value == false ? "No" : "Yes" : "");
                    }
                    if (objInfo.IsSentMailCandidate.HasValue)
                    {
                        commonDao.InsertLogDetail(logId, "IsSentMailCandidate", "IsSentMailCandidate", null,objInfo.IsSentMailCandidate.HasValue?objInfo.IsSentMailCandidate.Value == false ? "No" : "Yes":"");
                    }
                    if (objInfo.IsSendMailInterviewer.HasValue)
                    {
                        commonDao.InsertLogDetail(logId, "IsSendMailInterviewer", "IsSendMailInterviewer", null, objInfo.IsSendMailInterviewer.HasValue?objInfo.IsSendMailInterviewer.Value == false ? "No" : "Yes":"");
                    }
                    if (!string.IsNullOrEmpty(templateRound))
                    {
                        commonDao.InsertLogDetail(logId, "TemplateRound", "TemplateRound", null, new EformDao().GetEFormMasterByID(templateRound).Name);
                    }
                    if (!string.IsNullOrEmpty(jr))
                    {
                        commonDao.InsertLogDetail(logId, "JobRequest", "Job Request", null, jr);
                    }
                    // Insert Key Name
                    string key = objInfo.CandidateId.ToString() + " [" + objInfo.Candidate.FirstName + " " + objInfo.Candidate.MiddleName + " " + objInfo.Candidate.LastName + "]" + " for Interview in round "
                        + (objInfo.Round.HasValue?objInfo.Round.Value.ToString():"") + " in " + (objInfo.InterviewDate.HasValue? objInfo.InterviewDate.Value.ToString(Constants.DATETIME_FORMAT_VIEW):"");
                    commonDao.InsertLogDetail(logId, "ID", "Key for Insert", null, key);
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
        public bool WriteUpdateLogForInterview(Interview newInfo, string jr, ELogAction action)
        {
            bool isUpdated = false;
            try
            {
                string logId = commonDao.UniqueId;
                // Get old info
                Interview oldInfo = new InterviewDao().GetById(newInfo.Id.ToString());

                if ((oldInfo != null) && (newInfo != null) && (logId != null))
                {
                    commonDao.InsertMasterLog(logId, newInfo.UpdatedBy, ELogTable.Interview.ToString(), action.ToString());
                    if (oldInfo.InterviewDate != newInfo.InterviewDate)
                    {
                        commonDao.InsertLogDetail(logId, "InterviewDate", "Interview Date", newInfo.InterviewDate.HasValue ? newInfo.InterviewDate.Value.ToString(Constants.DATETIME_FORMAT_FULL_LOG) : "", oldInfo.InterviewDate.HasValue ? oldInfo.InterviewDate.Value.ToString(Constants.DATETIME_FORMAT_FULL_LOG) : "");
                        isUpdated = true;
                    }
                    if (oldInfo.Venue != newInfo.Venue)
                    {
                        commonDao.InsertLogDetail(logId, "Venue", "Venue", oldInfo.Venue, newInfo.Venue);
                        isUpdated = true;
                    }
                    if (oldInfo.Pic != newInfo.Pic)
                    {
                        commonDao.InsertLogDetail(logId, "Pic", "Pic", oldInfo.Pic, newInfo.Pic);
                        isUpdated = true;
                    }
                    if (oldInfo.Content != newInfo.Content)
                    {
                        commonDao.InsertLogDetail(logId, "Content", "Content", oldInfo.Content, newInfo.Content);
                        isUpdated = true;
                    }
                    if (oldInfo.CandidateId != newInfo.CandidateId)
                    {
                        isUpdated = true;
                        commonDao.InsertLogDetail(logId, "CandidateId", "Candidate Id", oldInfo.CandidateId.HasValue?oldInfo.CandidateId.Value.ToString():"", newInfo.CandidateId.HasValue?newInfo.CandidateId.Value.ToString():"");
                    }
                    if (oldInfo.InterviewStatusId != newInfo.InterviewStatusId)
                    {
                        isUpdated = true;
                        commonDao.InsertLogDetail(logId, "InterviewStatusId", "Interview Status Id", oldInfo.InterviewStatusId.HasValue?oldInfo.InterviewStatus.Name:"", newInfo.InterviewStatusId.HasValue?new InterviewStatusDao().GetById(newInfo.InterviewStatusId.Value).Name:"");
                    }
                    if (oldInfo.Round != newInfo.Round)
                    {
                        isUpdated = true;
                        commonDao.InsertLogDetail(logId, "Round", "Round", oldInfo.Round.HasValue?oldInfo.Round.Value.ToString():"", newInfo.Round.HasValue?newInfo.Round.Value.ToString():"");
                    }
                    if (oldInfo.InterviewResultId != newInfo.InterviewResultId)
                    {
                        isUpdated = true;
                        commonDao.InsertLogDetail(logId, "InterviewResultId", "Interview Result Id",oldInfo.InterviewResultId.HasValue?oldInfo.InterviewResult.Name:"", newInfo.InterviewResultId.HasValue?new InterviewResultDao().GetById(newInfo.InterviewResultId.Value).Name:"");
                    }
                    if (oldInfo.Note != newInfo.Note)
                    {
                        isUpdated = true;
                        commonDao.InsertLogDetail(logId, "Note", "Note", newInfo.Note, oldInfo.Note);
                    }
                    if (oldInfo.OldInterView != newInfo.OldInterView)
                    {
                        isUpdated = true;
                        commonDao.InsertLogDetail(logId, "OldInterView", "Old InterView", oldInfo.OldInterView.HasValue ? oldInfo.OldInterView.Value == false ? "No" : "Yes" : "", newInfo.OldInterView.HasValue ? newInfo.OldInterView.Value == false ? "No" : "Yes" : "");
                    }
                    if (oldInfo.IsSendMailInterviewer != newInfo.IsSendMailInterviewer)
                    {
                        isUpdated = true;
                        commonDao.InsertLogDetail(logId, "IsSendMailInterviewer", "IsSendMailInterviewer", oldInfo.IsSendMailInterviewer.Value == false ? "No" : "Yes",oldInfo.IsSendMailInterviewer.Value == false ? "No" : "Yes");
                    }
                    if (oldInfo.IsSentMailCandidate != newInfo.IsSentMailCandidate)
                    {
                        isUpdated = true;
                        commonDao.InsertLogDetail(logId, "IsSentMailCandidate", "IsSentMailCandidate", oldInfo.IsSendMailInterviewer.Value == false ? "No" : "Yes", newInfo.IsSentMailCandidate.Value == false ? "No" : "Yes");
                    }
                    if (oldInfo.InterviewFormId != newInfo.InterviewFormId)
                    {
                        isUpdated = true;
                        commonDao.InsertLogDetail(logId, "TemplateRound", "TemplateRound", new EformDao().GetEFormMasterByID(oldInfo.InterviewFormId).Name, new EformDao().GetEFormMasterByID(newInfo.InterviewFormId).Name);
                    }
                    if (!string.IsNullOrEmpty(jr))
                    {
                        Candidate objCan = new CandidateDao().GetById(oldInfo.CandidateId.Value.ToString());
                        if (objCan.JRId.Value.ToString() != jr)
                        {
                            commonDao.InsertLogDetail(logId, "JobRequest", "Job Request", objCan.JRId.Value.ToString(), jr);
                            isUpdated = true;
                        }
                    }
                    // Insert Key Name
                    if (isUpdated)
                    {
                        string key = oldInfo.CandidateId.ToString() + " [" + oldInfo.Candidate.FirstName + " " + oldInfo.Candidate.MiddleName + " " + oldInfo.Candidate.LastName + "]" + " for Interview in round "
                                + oldInfo.Round.Value.ToString() + " in " + oldInfo.InterviewDate.Value.ToString(Constants.DATETIME_FORMAT_VIEW);
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
            return isUpdated;
        }

         /// <summary>
        /// Write Update Log For Interview
        /// </summary>
        /// <param name="newInfo"></param>
        /// <param name="logId"></param>
        /// <returns></returns>
        public void WriteUpdateLogForSendMailToCandidate(string id, string userUpdate, ELogAction action)
        {
            try
            {
                string logId = commonDao.UniqueId;
                // Get old info
                Interview oldInfo = new InterviewDao().GetById(id);
                commonDao.InsertMasterLog(logId, userUpdate, ELogTable.Interview.ToString(), action.ToString());
                commonDao.InsertLogDetail(logId, "IsSentMailCandidate", "Email To Candidate", "No", "Yes");
                string key = oldInfo.CandidateId.ToString() + " [" + oldInfo.Candidate.FirstName + " " + oldInfo.Candidate.MiddleName + " " + oldInfo.Candidate.LastName + "]" + " for Interview in round "
                             + oldInfo.Round.Value.ToString() + " in " + oldInfo.InterviewDate.Value.ToString(Constants.DATETIME_FORMAT_VIEW);
                commonDao.InsertLogDetail(logId, "ID", "Key for Update", key, null);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Write Update Log For Interview
        /// </summary>
        /// <param name="newInfo"></param>
        /// <param name="logId"></param>
        /// <returns></returns>
        public void WriteUpdateLogForResult(Interview obj,int result,string note, string userUpdate,ELogAction action)
        {
            try
            {
                string logId = commonDao.UniqueId;
                // Get old info
                commonDao.InsertMasterLog(logId, userUpdate, ELogTable.Interview.ToString(), action.ToString());
                commonDao.InsertLogDetail(logId, "Interview Result", "Interview Result", obj.InterviewResultId.HasValue ? new InterviewResultDao().GetById((int)obj.InterviewResultId).Name : "", new InterviewResultDao().GetById(result).Name);
                if (!string.IsNullOrEmpty(note))
                {
                    commonDao.InsertLogDetail(logId, "Interview Note", "Interview Note", obj.Note, note);
                }
                string key = obj.CandidateId.ToString() + " [" + obj.Candidate.FirstName + " " + obj.Candidate.MiddleName + " " + obj.Candidate.LastName + "]" + " for Interview in round "
                            + obj.Round.Value.ToString() + " in " + obj.InterviewDate.Value.ToString(Constants.DATETIME_FORMAT_VIEW);
                commonDao.InsertLogDetail(logId, "ID", "Key for Update", key, null);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Write Update Log For Interview
        /// </summary>
        /// <param name="newInfo"></param>
        /// <param name="logId"></param>
        /// <returns></returns>
        public void WriteUpdateLogForSendMailToInterView(string id, string userUpdate, ELogAction action)
        {
            try
            {
                string logId = commonDao.UniqueId;
                // Get old info
                Interview oldInfo = new InterviewDao().GetById(id);
                commonDao.InsertMasterLog(logId, userUpdate, ELogTable.Interview.ToString(), action.ToString());
                commonDao.InsertLogDetail(logId, "IsSendMailInterviewer", "Send Meeting Request", "No", "Yes");
                string key = oldInfo.CandidateId.ToString() + " [" + oldInfo.Candidate.FirstName + " " + oldInfo.Candidate.MiddleName + " " + oldInfo.Candidate.LastName + "]" + " for Interview in round "
                            + oldInfo.Round.Value.ToString() + " in " + oldInfo.InterviewDate.Value.ToString(Constants.DATETIME_FORMAT_VIEW);
                commonDao.InsertLogDetail(logId, "ID", "Key for Update", key, null);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}