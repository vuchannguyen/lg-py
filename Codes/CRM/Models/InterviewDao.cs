using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CRM.Library.Common;
using System.Data.Common;
using System.Text.RegularExpressions;
using System.Linq.Dynamic;
using System.Data.Linq.SqlClient;

namespace CRM.Models
{
    public class InterviewDao : BaseDao
    {
        #region DB manipulation
        #region New paging GetInterviewList
        public IQueryable<Interview> GetInterViewEmployee()
        {
            var sql = from interview in dbContext.Interviews
                      from emp in dbContext.Employees
                      where emp.DeleteFlag == false && emp.CandidateId == interview.CandidateId
                      select interview;

            return sql;
        }

        public IQueryable<Interview> GetQueryListInterview(string candidateName, int? status, int? result, string interviewedBy,
            DateTime? fromDate, DateTime? toDate)
        {
            var sql = from interview in dbContext.Interviews.Except(GetInterViewEmployee())
                      select interview;

            if (!string.IsNullOrEmpty(candidateName))
            {
                candidateName = CommonFunc.GetFilterText(candidateName);
                sql = sql.Where(p => (p.Candidate.MiddleName != null ? SqlMethods.Like(p.Candidate.FirstName + " " + p.Candidate.MiddleName + " " + p.Candidate.LastName, candidateName) : SqlMethods.Like(p.Candidate.FirstName + " " + p.Candidate.LastName, candidateName))
                                        || SqlMethods.Like(p.Candidate.Email, candidateName));
            }

            if (ConvertUtil.ConvertToInt(status) != 0)
            {
                sql = sql.Where(p => p.Round == ConvertUtil.ConvertToInt(status));
            }

            if (ConvertUtil.ConvertToInt(result) != 0)
            {
                sql = sql.Where(p => p.InterviewResultId == ConvertUtil.ConvertToInt(result));
            }

            if (!string.IsNullOrEmpty(interviewedBy))
            {
                sql = sql.Where(p => p.Pic != null && p.Pic.Contains(interviewedBy));
            }

            if (fromDate != null)
            {
                sql = sql.Where(p => p.InterviewDate >= ConvertUtil.ConvertToDatetime(fromDate));
            }

            if (toDate != null)
            {
                sql = sql.Where(p => p.InterviewDate <= ConvertUtil.ConvertToDatetime(toDate));
            }

            sql = sql.Where(p => p.Round == dbContext.GetCurrentRoundCandidate(p.CandidateId));
            sql = sql.Where(p => p.OldInterView == false);
            sql = sql.Where(p => p.DeleteFlag == false);
            sql = sql.Where(p => p.Candidate.DeleteFlag == false);
            sql = sql.Where(p => p.Candidate.SaveHistory == false);

            return sql;
        }

        public int GetCountListInterviewLinq(string candidateName, int? status, int? result, string interviewedBy,
            DateTime? fromDate, DateTime? toDate)
        {
            return GetQueryListInterview(candidateName, status, result, interviewedBy, fromDate, toDate).Count();
        }

        public List<Interview> GetListInterviewLinq(string candidateName, int? status, int? result, string interviewedBy,
            DateTime? fromDate, DateTime? toDate, string sortColumn, string sortOrder, int skip, int take)
        {
            Interview inter = new Interview();
            string sortSQL = string.Empty;

            switch (sortColumn)
            {
                case "ID":
                    sortSQL += "Candidate.ID " + sortOrder;
                    break;

                case "DisplayName":
                    sortSQL += "Candidate.FirstName " + sortOrder + "," + "Candidate.MiddleName " + sortOrder + "," + "Candidate.LastName " + sortOrder;
                    break;

                case "Status":
                    sortSQL += "InterviewStatus.Name " + sortOrder;
                    break;

                case "ResultName":
                    sortSQL += "InterviewResult.Name " + sortOrder;
                    break;

                case "SubDept":
                    sortSQL += "Candidate.JobRequestItem.JobRequest.Department.DepartmentName " + sortOrder;
                    break;

                case "Position":
                    sortSQL += "Candidate.JobTitleLevel.DisplayName " + sortOrder;
                    break;

                case "InterviewDate":
                    sortSQL += "InterviewDate " + sortOrder;
                    break;

                case "Venue":
                    sortSQL += "Venue " + sortOrder;
                    break;

                case "Pic":
                    sortSQL += "Pic " + sortOrder;
                    break;

                case "IsSendMailInterviewer":
                    sortSQL += "IsSendMailInterviewer " + sortOrder;
                    break;

                case "IsSentMailCandidate":
                    sortSQL += "IsSentMailCandidate " + sortOrder;
                    break;
            }

            var sql = GetQueryListInterview(candidateName, status, result, interviewedBy, fromDate, toDate).OrderBy(sortSQL);

            if (skip == 0 && take == 0)
            {
                return sql.ToList();
            }

            return sql.Skip(skip).Take(take).ToList();
        }
        #endregion



        #region New paging GetInterviewHistoryList
        public IQueryable<Interview> GetQueryListInterviewHistory(string candidateName, int? source, int? status, int? position,
            DateTime? fromDate, DateTime? toDate)
        {
            var sql = from interview in dbContext.Interviews
                      select interview;

            if (!string.IsNullOrEmpty(candidateName))
            {
                candidateName = CommonFunc.GetFilterText(candidateName);
                sql = sql.Where(p => (p.Candidate.MiddleName != null ? SqlMethods.Like(p.Candidate.FirstName + " " + p.Candidate.MiddleName + " " + p.Candidate.LastName, candidateName) : SqlMethods.Like(p.Candidate.FirstName + " " + p.Candidate.LastName, candidateName))
                                        || SqlMethods.Like(p.Candidate.Email, candidateName));
            }

            if (ConvertUtil.ConvertToInt(source) != 0)
            {
                sql = sql.Where(p => p.Candidate.SourceId == ConvertUtil.ConvertToInt(source));
            }

            if (ConvertUtil.ConvertToInt(status) != 0)
            {
                sql = sql.Where(p => p.InterviewResultId == ConvertUtil.ConvertToInt(status));
            }

            if (ConvertUtil.ConvertToInt(position) != 0)
            {
                sql = sql.Where(p => p.Candidate.TitleId == ConvertUtil.ConvertToInt(position));
            }

            if (fromDate != null)
            {
                sql = sql.Where(p => p.Candidate.SearchDate >= ConvertUtil.ConvertToDatetime(fromDate));
            }

            if (toDate != null)
            {
                sql = sql.Where(p => p.Candidate.SearchDate <= ConvertUtil.ConvertToDatetime(toDate));
            }

            sql = sql.Where(p => p.Round == dbContext.GetCurrentRoundCandidate(p.CandidateId));
            sql = sql.Where(p => p.OldInterView == false);
            sql = sql.Where(p => p.DeleteFlag == false);
            sql = sql.Where(p => p.Candidate.DeleteFlag == false);
            sql = sql.Where(p => p.Candidate.SaveHistory == true);
            
            return sql;
        }

        public int GetCountListInterviewHistoryLinq(string candidateName, int? source, int? status, int? position,
            DateTime? fromDate, DateTime? toDate)
        {
            return GetQueryListInterviewHistory(candidateName, source, status, position, fromDate, toDate).Count();
        }

        public List<Interview> GetListInterviewHistoryLinq(string candidateName, int? source, int? status, int? position,
            DateTime? fromDate, DateTime? toDate, string sortColumn, string sortOrder, int skip, int take)
        {
            string sortSQL = string.Empty;
            
            switch (sortColumn)
            {
                case "ID":
                    sortSQL += "Candidate.ID " + sortOrder;
                    break;

                case "DisplayName":
                    sortSQL += "Candidate.FirstName " + sortOrder + "," + "Candidate.MiddleName " + sortOrder + "," + "Candidate.LastName " + sortOrder;
                    break;

                case "Source":
                    sortSQL += "Candidate.CandidateSource.Name " + sortOrder;
                    break;

                case "Email":
                    sortSQL += "Candidate.Email " + sortOrder;
                    break;

                case "CellPhone":
                    sortSQL += "Candidate.CellPhone " + sortOrder;
                    break;

                case "SearchBy":
                    sortSQL += "Candidate.Gender " + sortOrder;
                    break;

                case "SearchDate":
                    sortSQL += "Candidate.SearchDate " + sortOrder;
                    break;

                case "Title":
                    sortSQL += "Candidate.JobTitleLevel.DisplayName " + sortOrder;
                    break;

                case "ResultName":
                    sortSQL += "InterviewResult.Name " + sortOrder;
                    break;

                case "Note":
                    sortSQL += "Candidate.Note " + sortOrder;
                    break;
            }

            var sql = GetQueryListInterviewHistory(candidateName, source, status, position, fromDate, toDate).OrderBy(sortSQL);

            if (skip == 0 && take == 0)
            {
                return sql.ToList();
            }

            return sql.Skip(skip).Take(take).ToList();
        }
        #endregion



        /// <summary>
        /// Get List of Interview 
        /// </summary>
        /// <param name="candidateName">string</param>
        /// <param name="status">int</param>
        /// <param name="position">string</param>
        /// <param name="fromDate">DateTime</param>
        /// <param name="toDate">DateTime</param>
        /// <returns></returns>
        public List<sp_GetInterviewListResult> GetInterviewList(string candidateName, int? status, int? result, string interviewedBy,
            DateTime? fromDate, DateTime? toDate) 
        {
            return dbContext.sp_GetInterviewList(Constants.JOB_REQUEST_ITEM_PREFIX ,
                                            candidateName,
                                            status,
                                            result,
                                            interviewedBy,
                                            fromDate, 
                                            toDate).ToList<sp_GetInterviewListResult>();
        }

        /// <summary>
        /// Get list of history interview 
        /// </summary>
        /// <param name="candidateName">string</param>
        /// <param name="status">int</param>
        /// <param name="interviewedBy">string</param>
        /// <param name="fromDate">DateTime</param>
        /// <param name="toDate">DateTime</param>
        /// <returns></returns>
        public List<sp_GetInterviewHistoryListResult> GetHistoryInterviewList(string candidateName, int source, int? status, int? position,
            DateTime? fromDate, DateTime? toDate)
        {
            return dbContext.sp_GetInterviewHistoryList(candidateName,
                                            source,
                                            position,
                                            status,                                            
                                            fromDate,
                                            toDate).ToList<sp_GetInterviewHistoryListResult>();
        }

        /// <summary>
        /// Check candidate had interview
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool IsHaveCandidateInterview(int id)
        {
            //Check Candidate is have interview , not get History
            List<sp_GetInterviewCandidateResult> list = GetInterviewCandidate(id, 0);
            return (list != null && list.Count >0) ? true : false;
        }

        /// <summary>
        /// Get Interview of Candidate
        /// </summary>
        /// <param name="id">int</param>
        /// <returns>List</returns>
        public List<sp_GetInterviewCandidateResult> GetInterviewCandidate(int id, int checkHistory = 0 )
        {
            return dbContext.sp_GetInterviewCandidate(id, checkHistory).ToList<sp_GetInterviewCandidateResult>();

        }

        public void SetSendMailToInterView(string id, string userUpdate)
        {
            if (!string.IsNullOrEmpty(id))
            {
                Interview objDb = GetById(id);
                if (objDb != null)
                {
                    new InterviewLogDao().WriteUpdateLogForSendMailToInterView(id, userUpdate, ELogAction.Update);
                    objDb.IsSendMailInterviewer = true;
                    dbContext.SubmitChanges();
                }
            }
        }

        public void SetSendMailToCandidate(string id,string userUpdate)
        {
            if (!string.IsNullOrEmpty(id))
            {
                Interview objDb = GetById(id);
                if (objDb != null)
                {
                    new InterviewLogDao().WriteUpdateLogForSendMailToCandidate(id, userUpdate,ELogAction.Update);
                    objDb.IsSentMailCandidate = true;
                    dbContext.SubmitChanges();
                }
            }
        }        

        /// <summary>
        /// Sort Interview
        /// </summary>
        /// <param name="list"></param>
        /// <param name="sortColumn"></param>
        /// <param name="sortOrder"></param>
        /// <returns></returns>
        public List<sp_GetInterviewListResult> Sort(List<sp_GetInterviewListResult> list, string sortColumn, string sortOrder)
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
                         delegate(sp_GetInterviewListResult m1, sp_GetInterviewListResult m2)
                         { return m1.ID.CompareTo(m2.ID) * order; });
                    break;
                case "DisplayName":
                    list.Sort(
                         delegate(sp_GetInterviewListResult m1, sp_GetInterviewListResult m2)
                         { return m1.DisplayName.CompareTo(m2.DisplayName) * order; });
                    break;
                case "Status":
                    list.Sort(
                         delegate(sp_GetInterviewListResult m1, sp_GetInterviewListResult m2)
                         { return m1.Status.CompareTo(m2.Status) * order; });
                    break;
                case "ResultName":
                    list.Sort(
                         delegate(sp_GetInterviewListResult m1, sp_GetInterviewListResult m2)
                         { return m1.ResultName.CompareTo(m2.ResultName) * order; });
                    break;
                case "Dept":
                    list.Sort(
                         delegate(sp_GetInterviewListResult m1, sp_GetInterviewListResult m2)
                         { return m1.Dept.CompareTo(m2.Dept) * order; });
                    break;
                case "Position":
                    list.Sort(
                         delegate(sp_GetInterviewListResult m1, sp_GetInterviewListResult m2)
                         { return m1.Position.CompareTo(m2.Position) * order; });
                    break;
                case "InterviewDate":
                    list.Sort(
                         delegate(sp_GetInterviewListResult m1, sp_GetInterviewListResult m2)
                         { return m1.InterviewDate.Value.CompareTo(m2.InterviewDate) * order; });
                    break;
                case "Venue":
                    list.Sort(
                         delegate(sp_GetInterviewListResult m1, sp_GetInterviewListResult m2)
                         { return m1.Venue.CompareTo(m2.Venue) * order; });
                    break;
                case "Pic":
                    list.Sort(
                         delegate(sp_GetInterviewListResult m1, sp_GetInterviewListResult m2)
                         { return m1.Pic.CompareTo(m2.Pic) * order; });
                    break;
                case "IsSendMailInterviewer":
                    list.Sort(
                    delegate(sp_GetInterviewListResult m1, sp_GetInterviewListResult m2)
                         { return m1.IsSendMailInterviewer.Value.CompareTo(m2.IsSendMailInterviewer.Value) * order; });
                    break;
                case "IsSentMailCandidate":
                    list.Sort(
                         delegate(sp_GetInterviewListResult m1, sp_GetInterviewListResult m2)
                         { return m1.IsSentMailCandidate.Value.CompareTo(m2.IsSentMailCandidate.Value) * order; });
                    break;

            }

            return list;
        }

        /// <summary>
        /// Sort History Interview List
        /// </summary>
        /// <param name="list"></param>
        /// <param name="sortColumn"></param>
        /// <param name="sortOrder"></param>
        /// <returns></returns>
        public List<sp_GetInterviewHistoryListResult> SortHistoryInterview(List<sp_GetInterviewHistoryListResult> list, string sortColumn, string sortOrder)
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
                         delegate(sp_GetInterviewHistoryListResult m1, sp_GetInterviewHistoryListResult m2)
                         { return m1.ID.CompareTo(m2.ID) * order; });
                    break;
                case "DisplayName":
                    list.Sort(
                         delegate(sp_GetInterviewHistoryListResult m1, sp_GetInterviewHistoryListResult m2)
                         { return m1.DisplayName.CompareTo(m2.DisplayName) * order; });
                    break;
                case "Source":
                    list.Sort(
                         delegate(sp_GetInterviewHistoryListResult m1, sp_GetInterviewHistoryListResult m2)
                         { return m1.SourceName.CompareTo(m2.SourceName) * order; });
                    break;
                case "Email":
                    list.Sort(
                         delegate(sp_GetInterviewHistoryListResult m1, sp_GetInterviewHistoryListResult m2)
                         { return m1.Email.CompareTo(m2.Email) * order; });
                    break;
                case "CellPhone":
                    list.Sort(
                         delegate(sp_GetInterviewHistoryListResult m1, sp_GetInterviewHistoryListResult m2)
                         { return (!string.IsNullOrEmpty(m1.CellPhone) ? m1.CellPhone : "").CompareTo((!string.IsNullOrEmpty(m2.CellPhone) ? m2.CellPhone : "")) * order; });
                    break;
                case "Gender":
                    list.Sort(
                         delegate(sp_GetInterviewHistoryListResult m1, sp_GetInterviewHistoryListResult m2)
                         { return m1.Gender.CompareTo(m2.Gender) * order; });
                    break;
                case "SearchDate":
                    list.Sort(
                         delegate(sp_GetInterviewHistoryListResult m1, sp_GetInterviewHistoryListResult m2)
                         { return m1.SearchDate.CompareTo(m2.SearchDate) * order; });
                    break;
                case "Title":
                    list.Sort(
                         delegate(sp_GetInterviewHistoryListResult m1, sp_GetInterviewHistoryListResult m2)
                         { return m1.Title.CompareTo(m2.Title) * order; });
                    break;
                case "ResultName":
                    list.Sort(
                         delegate(sp_GetInterviewHistoryListResult m1, sp_GetInterviewHistoryListResult m2)
                         { return m1.ResultName.CompareTo(m2.ResultName) * order; });
                    break;
                case "Note":
                    list.Sort(
                         delegate(sp_GetInterviewHistoryListResult m1, sp_GetInterviewHistoryListResult m2)
                         { return (!string.IsNullOrEmpty(m1.Note) ? m1.Note : "").CompareTo((!string.IsNullOrEmpty(m2.Note) ? m2.Note : "")) * order; });
                    break;                

            }

            return list;
        }

        /// <summary>
        /// Insert interview
        /// </summary>
        /// <param name="objUI">Interview</param>
        /// <returns>Messsage</returns>
        public Message Insert(Interview objUI)
        {
            Message msg = null;
            try
            {
                if (objUI != null)
                {
                    objUI.DeleteFlag = false;
                    objUI.IsSendMailInterviewer = false;
                    objUI.IsSentMailCandidate = false;
                    dbContext.Interviews.InsertOnSubmit(objUI);
                    dbContext.SubmitChanges();
                    Candidate candi = new CandidateDao().GetById(objUI.CandidateId.Value.ToString());
                    msg = new Message(MessageConstants.I0001, MessageType.Info, "Candidate '" + candi.FirstName + " " +
                        candi.MiddleName + " " + candi.LastName + "'", "added to interview history");
                    new InterviewLogDao().WriteLogForInterview(null, objUI, ELogAction.Insert);
                }
            }
            catch (Exception)
            {
                // Show system error
                msg = new Message(MessageConstants.E0007, MessageType.Error);
            }
            return msg;

        }

        /// <summary>
        /// Save to history
        /// </summary>
        /// <param name="objUI">Candidate</param>
        /// <returns>Message</returns>
        public Message SaveToHistory(Candidate objUI)
        {
            Message msg = null;
            try
            {
                if (objUI != null)
                {
                    Candidate candi =  GetCandidateById(objUI.ID.ToString());
                    candi.SaveHistory = true;
                    new CandidateLogDao().WriteLogForUpdateHistory(candi, ELogAction.Update);
                    candi.UpdatedBy = objUI.UpdatedBy;
                    candi.UpdateDate = DateTime.Now;
                    dbContext.SubmitChanges();
                    msg = new Message(MessageConstants.I0001, MessageType.Info, "Candidate '" + candi.FirstName + " " +
                        candi.MiddleName + " " + candi.LastName + "'", "added into interview history");
                }
            }
            catch (Exception)
            {
                // Show system error
                msg = new Message(MessageConstants.E0007, MessageType.Error);
            }
            return msg;

        }

        /// <summary>
        ///  Unsave to history
        /// </summary>
        /// <param name="objUI">Candidate</param>
        /// <returns>Message</returns>
        public Message UnSaveToHistory(Candidate objUI)
        {
            Message msg = null;
            try
            {
                if (objUI != null)
                {
                    Candidate candi = GetCandidateById(objUI.ID.ToString());
                    candi.SaveHistory = false;
                    new CandidateLogDao().WriteLogForUpdateHistory(candi, ELogAction.Update);
                    candi.UpdatedBy = objUI.UpdatedBy;
                    candi.UpdateDate = DateTime.Now;
                    dbContext.SubmitChanges();
                    msg = new Message(MessageConstants.I0001, MessageType.Info, "The round of interview", "added");
                }
            }
            catch (Exception)
            {
                // Show system error
                msg = new Message(MessageConstants.E0007, MessageType.Error);
            }
            return msg;

        }

        /// <summary>
        /// Update result
        /// </summary>
        /// <param name="objUI">Interview</param>
        /// <returns>Message</returns>
        public Message UpdateResult(Interview objUI)
        {
            Message msg = null;
            try
            {
                if (objUI != null)
                {
                    Interview inter = GetById(objUI.Id.ToString());
                    new InterviewLogDao().WriteUpdateLogForResult(inter, objUI.InterviewResultId.Value,objUI.Note,objUI.UpdatedBy,ELogAction.Update);
                    inter.InterviewResultId = objUI.InterviewResultId;
                    inter.Note = objUI.Note;
                    inter.UpdatedBy = objUI.UpdatedBy;
                    inter.UpdatedDate = DateTime.Now;
                    dbContext.SubmitChanges();
                    msg = new Message(MessageConstants.I0001, MessageType.Info, "The result of "+inter.Candidate.FirstName + " " + inter.Candidate.MiddleName + " " 
                        +inter.Candidate.LastName +" for interview at Round " + inter.Round.Value.ToString(), "updated");
                }
            }
            catch (Exception)
            {
                // Show system error
                msg = new Message(MessageConstants.E0007, MessageType.Error);
            }

            return msg;
        }
        
        /// <summary>
        /// Delete Eform of Candidate
        /// </summary>
        /// <param name="CandidateId">string</param>
        public void DeleteEform(string CandidateId)
        {
            List<EForm> list = dbContext.EForms.Where(e => e.PersonID.Equals(CandidateId)).ToList().
                        Where(e => e.PersonType == (int)Constants.PersonType.Candidate).ToList();

            dbContext.EForms.DeleteAllOnSubmit(list);
            dbContext.SubmitChanges();
        }

        /// <summary>
        /// Get EForm
        /// </summary>
        /// <param name="masterId">string</param>
        /// <param name="personId">string</param>
        /// <param name="personType">int</param>
        /// <param name="formIndex">int</param>
        /// <param name="interviewForm">string</param>
        private void GetEForm(string masterId, string personId, int personType, int formIndex,string interviewForm )
        {
           List<EForm> list = dbContext.EForms.Where(c => c.MasterID == masterId && c.PersonID == personId
                && c.PersonType == personType && c.FormIndex == formIndex).ToList<EForm>();
           if (list.Count > 0)
           {
               EForm objEForm = list.LastOrDefault<EForm>();
               objEForm.MasterID = interviewForm;
               dbContext.SubmitChanges();
           }
        }

        /// <summary>
        /// Update interview
        /// </summary>
        /// <param name="obj">Interview</param>
        /// <param name="jr">string</param>
        /// <returns>Message</returns>
        public Message UpdateInterview(Interview obj,string jr)
        {
            Message msg = null;
            try
            {
                if (obj != null)
                {
                    Interview inter = GetByCandidateRound((int)obj.CandidateId, (int) obj.Round);
                    GetEForm(inter.InterviewFormId, inter.CandidateId.Value.ToString(), (int)Constants.PersonType.Candidate, inter.Round.Value,obj.InterviewFormId);
                    //Write Log
                    obj.Id = inter.Id;
                    obj.OldInterView = inter.OldInterView;
                    obj.IsSendMailInterviewer = inter.IsSendMailInterviewer;
                    obj.IsSentMailCandidate = inter.IsSentMailCandidate;
                    obj.InterviewStatusId = inter.InterviewStatusId;
                    new InterviewLogDao().WriteUpdateLogForInterview(obj, jr, ELogAction.Update);
                    //inter.InterviewResultId = objUI.InterviewResultId;
                    inter.InterviewFormId = obj.InterviewFormId;
                    inter.InterviewDate = obj.InterviewDate;
                    inter.Pic = obj.Pic;
                    inter.Venue = obj.Venue;
                    inter.Note = obj.Note;
                    inter.CClist = obj.CClist;
                    inter.Content = obj.Content;
                    inter.UpdatedBy = obj.UpdatedBy;
                    inter.UpdatedDate = DateTime.Now;
                    dbContext.SubmitChanges();
                    msg = new Message(MessageConstants.I0001, MessageType.Info, "The round of interview", "updated");
                }
            }
            catch (Exception)
            {
                // Show system error
                msg = new Message(MessageConstants.E0007, MessageType.Error);
            }

            return msg;
        }

        /// <summary>
        /// Check exist interview (-1: null ; 1: had resulted; 0: not result yet)
        /// </summary>
        /// <param name="CandidateId">int</param>
        /// <param name="Round">int</param>
        /// <returns>int </returns>
        public int CheckExistInterview(int CandidateId, int Round)
        {
            Interview inter =  dbContext.Interviews.Where(e => e.CandidateId.Equals(CandidateId)
                    && e.Round == Round && e.DeleteFlag == false && e.OldInterView == false).FirstOrDefault();
            if (inter == null)
                return -1;
            else if (inter.InterviewResultId > 0)
                return 1;
            else
                return 0;
        }

        #region Old Code
        ///// <summary>
        ///// Insert list interview
        ///// </summary>
        ///// <param name="objUI">List of Interview</param>
        ///// <returns>Message</returns>
        //public Message InsertListDetail(List<Interview> objUI, string templateRound, string jr, Constants.ManipulateEnum act = Constants.ManipulateEnum.Insert)
        //{
        //    Message msg = null;
        //    try
        //    {
        //        if (objUI != null)
        //        {
                    
        //            dbContext.Interviews.InsertAllOnSubmit(objUI);
        //            dbContext.SubmitChanges();
        //            Interview objInterview = objUI[0];
        //            Candidate objCandidate = new CandidateDao().GetById(objInterview.CandidateId.Value.ToString());
        //            if (act == Constants.ManipulateEnum.Insert)
        //            {
        //                msg = new Message(MessageConstants.I0001, MessageType.Info, "The round of interview", "added");
        //            }
        //            else
        //            {
        //                msg = new Message(MessageConstants.I0001, MessageType.Info, "The round of interview", "updated");
        //            }
        //            string[] array = templateRound.TrimEnd(',').Split(',');
        //            int i = 0;
        //            foreach (Interview obj in objUI)
        //            {
        //                new InterviewLogDao().WriteLogForInterview(null, obj, ELogAction.Insert);
        //                i++;
        //            }
        //        }
        //    }
        //    catch (Exception)
        //    {
        //        // Show system error
        //        msg = new Message(MessageConstants.E0007, MessageType.Error);
        //    }

        //    return msg;
        //}
        #endregion

        /// <summary>
        /// Insert list interview
        /// </summary>
        /// <param name="objUI">List of Interview</param>
        /// <returns>Message</returns>
        public Message InsertListDetail(List<Interview> objUI, string templateRound, string jr)
        {
            Message msg = null;
            try
            {
                if (objUI != null)
                {

                    dbContext.Interviews.InsertAllOnSubmit(objUI);
                    dbContext.SubmitChanges();
                    msg = new Message(MessageConstants.I0001, MessageType.Info, "The round of interview", "added");
                   
                    string[] array = templateRound.TrimEnd(',').Split(',');
                    int i = 0;
                    foreach (Interview obj in objUI)
                    {
                        new InterviewLogDao().WriteInsertLogForInterview(obj, array[i], jr, ELogAction.Insert);
                        i++;
                    }
                }
            }
            catch (Exception)
            {
                // Show system error
                msg = new Message(MessageConstants.E0007, MessageType.Error);
            }

            return msg;
        }


        /// <summary>
        /// GetById
        /// </summary>
        /// <param name="id">string</param>
        /// <returns>Interview</returns>
        public Interview GetById(string id)
        {
            if (!string.IsNullOrEmpty(id))
                return dbContext.Interviews.Where(c => c.Id.Equals(id)).FirstOrDefault<Interview>();
            else
                return null;
        }

        /// <summary>
        /// Get by candidate id, round
        /// </summary>
        /// <param name="id">string</param>
        /// <returns>Interview</returns>
        public Interview GetByCandidateRound(int CandidateId, int Round)
        {
            return dbContext.Interviews.Where(c => c.CandidateId.Equals(CandidateId) &&
                c.Round == Round && c.DeleteFlag == false && c.OldInterView == false ).FirstOrDefault<Interview>();
        }

        /// <summary>
        /// Get max round interview of Candidate
        /// </summary>
        /// <param name="CandidateId">int</param>
        /// <returns>int</returns>
        public int GetMaxRound(int CandidateId)
        {
            return dbContext.GetMaxRound(CandidateId).Value;
        }

        /// <summary>
        /// Get Current Round of candidate
        /// </summary>
        /// <param name="CandidateId">int</param>
        /// <returns>int</returns>
        public int GetCurrentRound(int CandidateId)
        {
            return dbContext.GetCurrentRoundCandidate(CandidateId).Value;
        }


        public int? GetCurrentInterviewedRound(int CandidateId)
        {
            return dbContext.GetRoundInterviewedCandidate(CandidateId);
        }

        /// <summary>
        /// GetCandidateById
        /// </summary>
        /// <param name="id">string</param>
        /// <returns>Interview</returns>
        public Candidate GetCandidateById(string id)
        {
            return dbContext.Candidates.Where(c => c.ID.Equals(id)).FirstOrDefault<Candidate>();
        }

        /// <summary>
        /// Delete Interview Of Candidate
        /// </summary>
        /// <param name="id">int</param>
        /// <returns>int</returns>
        public int DeleteInterviewOfCandidate(int id)
        {
            return dbContext.sp_DeleteInterviewCandidate(id);
        }

        /// <summary>
        /// Delete Old Interview Of Candidate
        /// </summary>
        /// <param name="id">int</param>
        /// <returns>int</returns>
        public int DeleteOldInterview(int id)
        {
            return dbContext.sp_DeleteOldInterview(id);
        }

        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="id">string</param>
        public void Delete(string id, string userUpdate)
        {
            if (id != null)
            {
                // Get current group in dbContext
                Interview objDb = GetById(id);
                
                if (objDb != null)
                {
                    //Delete  e form 
                    EformDao eDao = new EformDao();
                    eDao.DeleteEform(objDb.InterviewFormId, objDb.CandidateId.ToString(), (int)Constants.PersonType.Candidate, (int)objDb.Round,userUpdate);
                    // Set delete info      
                    objDb.DeleteFlag = true;
                    objDb.UpdatedDate = DateTime.Now;
                    objDb.UpdatedBy = userUpdate;

                    new InterviewLogDao().WriteLogForInterview(null, objDb, ELogAction.Delete);
                    // Submit changes to dbContext

                    dbContext.Interviews.DeleteOnSubmit(objDb);
                    dbContext.SubmitChanges();

                }
            }
        }

        /// <summary>
        /// Get List Interviewed by
        /// </summary>
        /// <returns>List</returns>
        public List<sp_GetListInterviewedByResult> GetListInterviewedBy()
        {
            return dbContext.sp_GetListInterviewedBy().ToList<sp_GetListInterviewedByResult>();
        } 
        #endregion
    }
}