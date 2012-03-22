using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Common;
using CRM.Library.Common;
using CRM.Models;
using CRM.Library.Utils;

namespace CRM.Models
{
    public class CandidateExamDao : BaseDao
    {
        private CandidateAnswerDao candidateAnswerDao = new CandidateAnswerDao();

        /// <summary>
        /// GetList
        /// </summary>
        /// <returns></returns>
        public List<LOT_Candidate_Exam> GetList()
        {
            return dbContext.LOT_Candidate_Exams.ToList<LOT_Candidate_Exam>();
        }

        /// <summary>
        /// Get Candidate Exam by the PIN
        /// </summary>
        /// <param name="pin"></param>
        /// <returns></returns>
        public LOT_Candidate_Exam GetItemByCandidatePin(string pin)
        {
            return dbContext.LOT_Candidate_Exams.Where(p => (p.CandidatePin == pin)).FirstOrDefault<LOT_Candidate_Exam>();
        }

        /// <summary>
        /// Update login date and IP of table candidate exam follow by objUI
        /// </summary>
        /// <param name="objUI">LOT_Candidate_Exam object that have been passed from view</param>
        /// <returns>Message</returns>
        public Message UpdateCandidateExam(LOT_Candidate_Exam objUI)
        {
            Message msg = null;
            try
            {
                LOT_Candidate_Exam candidate = this.GetItemByCandidatePin(objUI.CandidatePin);
                if (candidate != null)
                {
                    candidate.LoginDate = DateTime.Now;
                    candidate.IP = HttpContext.Current.Request.UserHostAddress;
                    dbContext.SubmitChanges();

                    // Show success message
                    msg = new Message(MessageConstants.I0001, MessageType.Info, "CandidateExam", "updated");
                }
            }
            catch (Exception ex)
            {
                msg = new Message(MessageConstants.E0007, MessageType.Error);
                throw ex;
            }
            return msg;
        }

        /// <summary>
        /// Here is update object follow by MVC, so do not pass the param into function
        /// </summary>
        /// <returns>Message</returns>
        public Message Update()
        {
            Message msg = null;
            try
            {
                dbContext.SubmitChanges();
                // Show success message
                msg = new Message(MessageConstants.I0001, MessageType.Info, "CandidateExam", "updated");
            }
            catch (Exception ex)
            {
                msg = new Message(MessageConstants.E0007, MessageType.Error);
                throw ex;
            }
            return msg;
        }

        /// <summary>
        /// GetByID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public LOT_Candidate_Exam GetByID(long id)
        {
            return dbContext.LOT_Candidate_Exams.Where(c => c.ID == id).FirstOrDefault<LOT_Candidate_Exam>();
        }

        /// <summary>
        /// Assign Candidate
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public Message AssignCandidate(List<LOT_Candidate_Exam> list)
        {
            Message msg = null;
            try
            {
                if (list.Count > 0)
                {
                    int totalID = list.Count;
                    dbContext.LOT_Candidate_Exams.InsertAllOnSubmit(list);
                    dbContext.SubmitChanges();
                    // Show succes message
                    msg = new Message(MessageConstants.I0001, MessageType.Info, totalID.ToString() + " candidates(s)", "assigned");
                }
                else
                {
                    // Show system error
                    msg = new Message(MessageConstants.E0033, MessageType.Error, "Please select candidate(s) to assign!");
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
        /// Sort Emplooyee
        /// </summary>
        /// <param name="list"></param>
        /// <param name="sortColumn"></param>
        /// <param name="sortOrder"></param>
        /// <returns></returns>
        public List<sp_GetEmployeeExamResult> SortEmployee(List<sp_GetEmployeeExamResult> list, string sortColumn, string sortOrder)
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
                         delegate(sp_GetEmployeeExamResult m1, sp_GetEmployeeExamResult m2)
                         { return m1.ID.CompareTo(m2.ID) * order; });
                    break;
                case "EmployeeID":
                    list.Sort(
                         delegate(sp_GetEmployeeExamResult m1, sp_GetEmployeeExamResult m2)
                         { return m1.EmployeeID.CompareTo(m2.EmployeeID) * order; });
                    break;
                case "DisplayName":
                    list.Sort(
                         delegate(sp_GetEmployeeExamResult m1, sp_GetEmployeeExamResult m2)
                         { return m1.DisplayName.CompareTo(m2.DisplayName) * order; });
                    break;
                case "JobTitle":
                    list.Sort(
                         delegate(sp_GetEmployeeExamResult m1, sp_GetEmployeeExamResult m2)
                         { return m1.TitleName.CompareTo(m2.TitleName) * order; });
                    break;
                case "Department":
                    list.Sort(
                         delegate(sp_GetEmployeeExamResult m1, sp_GetEmployeeExamResult m2)
                         { return m1.Department.CompareTo(m2.Department) * order; });
                    break;
                case "SubDepartment":
                    list.Sort(
                         delegate(sp_GetEmployeeExamResult m1, sp_GetEmployeeExamResult m2)
                         { return m1.DepartmentName.CompareTo(m2.DepartmentName) * order; });
                    break;
                case "StartDate":
                    list.Sort(
                         delegate(sp_GetEmployeeExamResult m1, sp_GetEmployeeExamResult m2)
                         { return m1.StartDate.CompareTo(m2.StartDate) * order; });
                    break;
                case "ResignDate":
                    list.Sort(
                         delegate(sp_GetEmployeeExamResult m1, sp_GetEmployeeExamResult m2)
                         { return m1.ResignedDate.Value.CompareTo(m2.ResignedDate.Value) * order; });
                    break;
                case "Status":
                    list.Sort(
                         delegate(sp_GetEmployeeExamResult m1, sp_GetEmployeeExamResult m2)
                         { return (m1.EmpStatusId.HasValue ? m1.EmpStatusId.Value : 0).CompareTo((m2.EmpStatusId.HasValue ? m2.EmpStatusId.Value : 0)) * order; });
                    break;
                case "CandidatePin":
                    list.Sort(
                         delegate(sp_GetEmployeeExamResult m1, sp_GetEmployeeExamResult m2)
                         { return m1.CandidatePin.CompareTo(m2.CandidatePin) * order; });
                    break;
            }

            return list;
        }

        /// <summary>
        /// Sort Candidate 
        /// </summary>
        /// <param name="list"></param>
        /// <param name="sortColumn"></param>
        /// <param name="sortOrder"></param>
        /// <returns></returns>
        public List<sp_GetCandidateExamResult> Sort(List<sp_GetCandidateExamResult> list, string sortColumn, string sortOrder)
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

                case "CandidateName":
                    list.Sort(
                         delegate(sp_GetCandidateExamResult m1, sp_GetCandidateExamResult m2)
                         { return m1.DisplayName.CompareTo(m2.DisplayName) * order; });
                    break;
                case "DOB":
                    if (order == -1)
                    {
                        list = list.OrderByDescending(p => p.DOB).ToList<sp_GetCandidateExamResult>();
                    }
                    else
                    {
                        list = list.OrderBy(p => p.DOB).ToList<sp_GetCandidateExamResult>();
                    }
                    break;
                case "Gender":
                    list.Sort(
                         delegate(sp_GetCandidateExamResult m1, sp_GetCandidateExamResult m2)
                         {
                             return m1.Gender.CompareTo(m2.Gender) * order;
                         });
                    break;
                case "SearchDate":
                    list.Sort(
                         delegate(sp_GetCandidateExamResult m1, sp_GetCandidateExamResult m2)
                         { return m1.SearchDate.CompareTo(m2.SearchDate) * order; });
                    break;
                case "Source":
                    list.Sort(
                         delegate(sp_GetCandidateExamResult m1, sp_GetCandidateExamResult m2)
                         { return m1.SourceName.CompareTo(m2.SourceName) * order; });
                    break;
                case "Title":
                    list.Sort(
                         delegate(sp_GetCandidateExamResult m1, sp_GetCandidateExamResult m2)
                         { return m1.Title.CompareTo(m2.Title) * order; });
                    break;
                case "Status":
                    list.Sort(
                         delegate(sp_GetCandidateExamResult m1, sp_GetCandidateExamResult m2)
                         { return m1.Status.CompareTo(m2.Status) * order; });
                    break;
                case "CellPhone":
                    list.Sort(
                         delegate(sp_GetCandidateExamResult m1, sp_GetCandidateExamResult m2)
                         { return m1.CellPhone.CompareTo(m2.CellPhone) * order; });
                    break;
                case "Note":
                    list.Sort(
                         delegate(sp_GetCandidateExamResult m1, sp_GetCandidateExamResult m2)
                         { return m1.Note.CompareTo(m2.Note) * order; });
                    break;
                case "CandidatePin":
                    list.Sort(
                         delegate(sp_GetCandidateExamResult m1, sp_GetCandidateExamResult m2)
                         { return m1.CandidatePin.CompareTo(m2.CandidatePin) * order; });
                    break;
            }

            return list;
        }


        /// <summary>
        /// Remove Assigned Candidate List
        /// </summary>
        /// <param name="examQuestionSectionID"></param>
        /// <returns></returns>
        public Message RemoveAssignList(string ids)
        {
            Message msg = null;
            bool isOk = true;
            try
            {
                ids = ids.TrimEnd(Constants.SEPARATE_IDS_CHAR);
                List<LOT_Candidate_Exam> list = new List<LOT_Candidate_Exam>();
                foreach (string strId in ids.Split(Constants.SEPARATE_IDS_CHAR))
                {
                    int id = 0;
                    bool isInterger = Int32.TryParse(strId, out id);
                    if (isInterger)
                    {
                        LOT_Candidate_Exam item = GetByID(id);
                        if (item != null)
                        {
                            LOT_CandidateAnswer candidateAnswer = candidateAnswerDao.GetByCandidateExamID(id);
                            if (candidateAnswer == null)
                            {
                                list.Add(item);
                            }
                            else
                            {
                                msg = new Message(MessageConstants.E0006, MessageType.Error, "remove", "candidate(s)");
                                isOk = false;
                                break;
                            }
                        }
                    }
                }

                if (isOk)
                {
                    if (list.Count > 0)
                    {
                        int totalID = list.Count;

                        dbContext.LOT_Candidate_Exams.DeleteAllOnSubmit(list);
                        dbContext.SubmitChanges();
                        // Show succes message
                        msg = new Message(MessageConstants.I0001, MessageType.Info, totalID.ToString() + " candidate(s)", "removed");
                    }
                    else
                    {
                        // Show system error
                        msg = new Message(MessageConstants.E0033, MessageType.Error, "Please select candidate(s) to remove!");
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
        /// Get last id of exam_candidate
        /// </summary>
        /// <returns></returns>
        public long getlastID()
        {
            LOT_Candidate_Exam lastItem = dbContext.LOT_Candidate_Exams.OrderByDescending(x=> x.ID).FirstOrDefault<LOT_Candidate_Exam>();
            if (lastItem != null)
            {
                return lastItem.ID;
            }
            else
            {
                return 0;
            }
        }

        public LOT_Candidate_Exam GetByExamAndCandidate(int examID,int? candidateID,string employeeID)
        {
            if (candidateID.HasValue)
            {
                return dbContext.LOT_Candidate_Exams.Where(q => q.ExamID.Equals(examID) && q.CandidateID.HasValue
                    && q.CandidateID.Value.Equals(candidateID.Value)).FirstOrDefault();
            }
            else
            {
                return dbContext.LOT_Candidate_Exams.Where(q => q.ExamID.Equals(examID) && q.EmployeeID.Equals(employeeID)).FirstOrDefault();
            }
        }

        /// <summary>
        /// Send Mail and update db set SendMail field = true
        /// </summary>
        /// <param name="id"></param>
        /// <param name="host"></param>
        /// <param name="port"></param>
        /// <param name="from"></param>
        /// <param name="fromName"></param>
        /// <param name="toEmailList"></param>
        /// <param name="ccMail"></param>
        /// <param name="subject"></param>
        /// <param name="body"></param>
        /// <returns></returns>
        public Message SendMailAndUpdate(string strId, string host, string port, string from, string fromName, string toEmailList, string ccMail, string subject, string body)
        {
            Message msg = null;
            DbTransaction trans = null;
            try
            {
                //begin transaction
                dbContext.Connection.Open();
                trans = dbContext.Connection.BeginTransaction();
                dbContext.Transaction = trans;

                if (!string.IsNullOrEmpty(strId))
                {
                    //update db 
                    int id = int.Parse(strId);
                    LOT_Candidate_Exam exam_Candidate = GetByID(id);
                    exam_Candidate.SendMail = true;
                    dbContext.SubmitChanges();

                    //send email 
                    bool result = WebUtils.SendMail(host, port, from, fromName, toEmailList, ccMail, subject, body);
                    if (result)
                    {
                        trans.Commit();
                        msg = new Message(MessageConstants.I0002, MessageType.Info);
                    }
                    else
                    {
                        msg = new Message(MessageConstants.E0032, MessageType.Error);
                        trans.Rollback();
                    }
                }
                else
                {
                    msg = new Message(MessageConstants.E0032, MessageType.Error);
                }
            }
            catch
            {
                if (trans != null) { trans.Rollback(); }
                // Show system error
                msg = new Message(MessageConstants.E0032, MessageType.Error);
            }
            return msg;

        }

        public double? GetMarkOfSection(int candidateExamId, int sectionId)
        {
            if (candidateExamId == 0 || sectionId == 0)
                return null;
            return dbContext.GetScoreOfSection(candidateExamId, sectionId);
        }

        public double? GetEnglishSkillMark(int candidateExamId)
        {
            double? markMultipleChoice = GetMarkOfSection(candidateExamId, Constants.LOT_MULTIPLE_CHOICE_QUESTION);
            double? markSentenceCorrection = GetMarkOfSection(candidateExamId, Constants.LOT_SENTENCE_CORRECTION_QUESTION);
            double? markComprehensionSkill = GetMarkOfSection(candidateExamId, Constants.LOT_COMPREHENSION_SKILL_ID);
            double? markListeningSkill = GetMarkOfSection(candidateExamId, Constants.LOT_LISTENING_TOPIC_ID);
            double? markWriting = GetMarkOfSection(candidateExamId, Constants.LOT_WRITING_SKILL_ID);
            return CommonFunc.Average(markComprehensionSkill, markListeningSkill, markMultipleChoice, markSentenceCorrection, markWriting);
        }
        public int? GetEnglishSkillMaxMark(int candidateExamId)
        {
            if (candidateExamId == 0)
                return null;
            int? markMultipleChoice = GetMaxMarkOfSection(candidateExamId, Constants.LOT_MULTIPLE_CHOICE_QUESTION);
            int? markSentenceCorrection = GetMaxMarkOfSection(candidateExamId, Constants.LOT_SENTENCE_CORRECTION_QUESTION);
            int? markComprehensionSkill = GetMaxMarkOfSection(candidateExamId, Constants.LOT_COMPREHENSION_SKILL_ID);
            int? markListeningSkill = GetMaxMarkOfSection(candidateExamId, Constants.LOT_LISTENING_TOPIC_ID);
            int? markWriting = GetMaxMarkOfSection(candidateExamId, Constants.WRITTING_MARK_NULL);
            return markMultipleChoice ?? 0 + markSentenceCorrection ?? 0 + 
                markComprehensionSkill ?? 0 + markListeningSkill ?? 0 + markWriting ?? 0;
        }
        public int? GetMaxMarkOfSection(int candidateExamId, int sectionId)
        {
            if (candidateExamId == 0)
                return null;
            var obj = dbContext.sp_GetMaxMarkByCandidateExamID(candidateExamId, sectionId).FirstOrDefault();
            if (obj == null)
                return null;
            return obj.maxMark;
        }

        /// <summary>
        /// Get By Exam Id and Employee Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>LOT_Candidate_Exam</returns>
        public LOT_Candidate_Exam GetByExamIdEmpId(int examId, string empId)
        {
            return dbContext.LOT_Candidate_Exams.Where(c => c.EmployeeID == empId && c.ExamID == examId).FirstOrDefault<LOT_Candidate_Exam>();
        }

        public string GetCandidateName(LOT_Candidate_Exam objUI)
        {
            if (objUI.LOT_Exam.ExamType == 2)
                return dbContext.GetEmployeeFullName(objUI.EmployeeID, 1);
            if (objUI.LOT_Exam.ExamType == 1)
                return dbContext.GetCandidateFullName(objUI.CandidateID, 1);
            return string.Empty;
        }
    }
}