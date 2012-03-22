using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using System.Data.Common;
using CRM.Library.Common;
using CRM.Models;
using System.Linq.Dynamic;
using CRM.Models.Entities;
using System.Data.Linq.SqlClient;

namespace CRM.Models
{
    /// <summary>
    /// Exam Data Access object
    /// </summary>
    public class ExamDao : BaseDao
    {

        #region variables

        private CandidateDao candidateDao = new CandidateDao();
        private EmployeeDao empDao = new EmployeeDao();
        private CandidateAnswerDao candidateAnswerDAO = new CandidateAnswerDao();
        private ExamQuestionSectionDAO examQuestionSectionDAO = new ExamQuestionSectionDAO();

        #endregion

        

        #region New paging GetExamList
        public IQueryable<LOT_Exam> GetQueryListExam(string text, int? examQuestionId, DateTime? examDateFrom, DateTime? examDateTo)
        {
            var sql = from exam in dbContext.LOT_Exams
                      select exam;

            if (!string.IsNullOrEmpty(text))
            {
                text = CommonFunc.GetFilterText(text);
                sql = sql.Where(p => SqlMethods.Like(p.Title, text) || dbContext.FilterCandidateExamWithCandidateName(p.ID, text, p.ExamType) == true);
            }

            if (ConvertUtil.ConvertToInt(examQuestionId) != 0)
            {
                sql = sql.Where(p => p.ExamQuestionID == ConvertUtil.ConvertToInt(examQuestionId));
            }

            if (examDateFrom != null)
            {
                sql = sql.Where(p => p.ExamDate >= examDateFrom);
            }

            if (examDateTo != null)
            {
                sql = sql.Where(p => p.ExamDate <= examDateTo);
            }

            sql = sql.Where(p => p.DeleteFlag == false);

            return sql;
        }

        public int GetCountListExamLinq(string text, int? examQuestionId, DateTime? examDateFrom, DateTime? examDateTo)
        {
            return GetQueryListExam(text, examQuestionId, examDateFrom, examDateTo).Count();
        }

        public List<LOT_Exam> GetListExamLinq(string text, int? examQuestionId, DateTime? examDateFrom, DateTime? examDateTo,
            string sortColumn, string sortOrder, int skip, int take)
        {
            string sortSQL = string.Empty;

            switch (sortColumn)
            {
                case "Title":
                    sortSQL += "Title " + sortOrder;
                    break;

                case "ExamQuestion":
                    sortSQL += "LOT_ExamQuestion.Title " + sortOrder;
                    break;

                case "ExamDate":
                    sortSQL += "ExamDate " + sortOrder;
                    break;

                case "ExamType":
                    sortSQL += "ExamType " + sortOrder;
                    break;
            }

            var sql = GetQueryListExam(text, examQuestionId, examDateFrom, examDateTo).OrderBy(sortSQL);

            if (skip == 0 && take == 0)
            {
                return sql.ToList();
            }

            return sql.Skip(skip).Take(take).ToList();
        }
        #endregion

        public string GetExamMarkStatus(int examId, int sectionId)
        {
            return dbContext.GetExamMarkStatus(examId, sectionId);
        }

        public Message InsertCandidateForExam(LOT_Candidate_Exam objUI)
        {
            Message msg = null;
            try
            {
                LOT_Candidate_Exam objDb = dbContext.LOT_Candidate_Exams.Where(q=>q.EmployeeID==objUI.EmployeeID
                    && q.ExamID==objUI.ExamID).FirstOrDefault();
                if (objDb == null)
                {
                    if (objUI != null)
                    {
                        // Set more info
                        dbContext.LOT_Candidate_Exams.InsertOnSubmit(objUI);
                        dbContext.SubmitChanges();
                        msg = new Message(MessageConstants.I0001, MessageType.Info, "Exam '" + objUI.EmployeeID + "'", "added");
                    }
                }
                else
                {
                    //objDb.WritingComment = objUI.WritingComment;
                    //objDb.WritingMark = objUI.WritingMark;
                    //objDb.VerbalComment = objUI.VerbalComment;
                    //objDb.VerbalMark = objUI.VerbalMark;
                    //objDb.VerbalMarkType = objUI.VerbalMarkType;
                    //objDb.VerbalTestedBy = objUI.VerbalTestedBy;
                    dbContext.SubmitChanges();
                    msg = new Message(MessageConstants.I0001, MessageType.Info, "Exam '" + objUI.EmployeeID + "'", "added");
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
        /// Get Exam By it's ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public LOT_Exam GetByID(int id)
        {
            return dbContext.LOT_Exams.Where(c => c.ID == id).FirstOrDefault<LOT_Exam>();
        }

        public LOT_Exam GetByExamDateAndClassType(DateTime examDate, int classType)
        {
            return dbContext.LOT_Exams.Where(q => q.ExamDate.Equals(examDate) && q.ExamQuestionID.Equals(classType)).FirstOrDefault();
        }

        /// <summary>
        /// Insert to database
        /// </summary>
        /// <param name="objUI"></param>
        /// <returns></returns>
        public Message Insert(LOT_Exam objUI)
        {
            Message msg = null;
            try
            {
                if (objUI != null)
                {
                    // Set more info
                    objUI.DeleteFlag = false;
                    objUI.CreateDate = DateTime.Now;
                    objUI.UpdateDate = DateTime.Now;

                    dbContext.LOT_Exams.InsertOnSubmit(objUI);
                    dbContext.SubmitChanges();
                    msg = new Message(MessageConstants.I0001, MessageType.Info, "Exam '" + objUI.Title + "'", "added");
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
        /// Update
        /// </summary>
        /// <param name="objUI"></param>
        /// <returns></returns>
        public Message Update(LOT_Exam objUI)
        {
            Message msg = null;
            try
            {
                if (objUI != null)
                {
                    LOT_Exam objDB = GetByID(objUI.ID);
                    if (IsValidExamUpdateDate(objUI, objDB,out msg))
                    {
                        objDB.Title = objUI.Title;
                        objDB.ExamQuestionID = objUI.ExamQuestionID;
                        objDB.ExamDate = objUI.ExamDate;
                        objDB.ExamType = objUI.ExamType;

                        // Set more info                                        
                        objDB.UpdateDate = DateTime.Now;
                        objDB.UpdatedBy = objUI.UpdatedBy;
                        dbContext.SubmitChanges();
                        msg = new Message(MessageConstants.I0001, MessageType.Info, "Exam '" + objUI.Title + "'", "updated");
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
        /// Check update date whether is valid
        /// </summary>
        /// <param name="objUI"></param>
        /// <param name="objDb"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        private bool IsValidExamUpdateDate(LOT_Exam objUI, LOT_Exam objDb, out Message msg)
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
                            msg = new Message(MessageConstants.E0025, MessageType.Error, "Exam '" + objDb.Title + "'");
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

        /// <summary>
        /// Delete a list of exam
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public Message DeleteList(string ids, string userName)
        {
            Message msg = null;
            DbTransaction trans = null;
            bool isOK = true;
            try
            {
                //begin transaction
                dbContext.Connection.Open();
                trans = dbContext.Connection.BeginTransaction();
                dbContext.Transaction = trans;

                if (!string.IsNullOrEmpty(ids))
                {
                    //split ids by char ','
                    ids = ids.TrimEnd(Constants.SEPARATE_IDS_CHAR);
                    string[] idArr = ids.Split(Constants.SEPARATE_IDS_CHAR);
                    int examId = 0;
                    int totalID = idArr.Count();

                    //loop each id to delete
                    foreach (string id in idArr)
                    {
                        //is check all records to delete 
                        bool isInterger = Int32.TryParse(id, out examId);
                        if (isInterger)
                        {
                            LOT_Exam exam = GetByID(examId);
                            if (exam != null)
                            {
                                exam.UpdatedBy = userName;
                                //is it used for the test?
                                if (IsTested(exam.ID))
                                {
                                    msg = new Message(MessageConstants.E0035, MessageType.Error, "exam", exam.Title);
                                    isOK = false;
                                    break;
                                }
                                else
                                {
                                    //delete from db
                                    msg = Delete(exam);
                                    if (msg.MsgType == MessageType.Error)
                                    {
                                        isOK = false;
                                        break;
                                    }
                                }
                            }
                        }
                        else
                        {
                            //minus the first item which is 'false' value
                            totalID--;
                        }
                    }

                    if (isOK)
                    {
                        // Show succes message
                        msg = new Message(MessageConstants.I0001, MessageType.Info, totalID.ToString() + " exam(s)", "deleted");
                        trans.Commit();
                    }
                    else
                    {
                        trans.Rollback();
                    }
                }
            }
            catch
            {
                if (trans != null) { trans.Rollback(); }
                // Show system error
                msg = new Message(MessageConstants.E0006, MessageType.Error, "delete", "it");
            }
            return msg;
        }

        /// <summary>
        /// Delete by set DeleteFlag = true
        /// </summary>
        /// <param name="objUI"></param>
        private Message Delete(LOT_Exam objUI)
        {
            Message msg = null;
            try
            {
                if (objUI != null)
                {
                    LOT_Exam objDb = GetByID(objUI.ID);
                    if (objDb != null)
                    {
                        // Set delete info
                        objDb.DeleteFlag = true;
                        objDb.UpdateDate = DateTime.Now;
                        objDb.UpdatedBy = objUI.UpdatedBy;
                    }
                    // Submit changes to dbContext
                    msg = new Message(MessageConstants.I0001, MessageType.Info, "Exam", "deleted");
                    dbContext.SubmitChanges();
                }
            }
            catch
            {
                msg = new Message(MessageConstants.E0006, MessageType.Error, "delete", "it");
            }
            return msg;
        }


        /// <summary>
        /// Is the exam used for the test
        /// </summary>
        /// <param name="examQuestionID"></param>
        /// <returns></returns>
        public bool IsTested(int examID)
        {
            bool result = false;
            LOT_Exam exam = GetByID(examID);
            if (exam != null && exam.ExamDate.Date < DateTime.Now.Date)
            {
                result = true;
            }
            else
            {
                //check did any candidate already perform this exam question
                result = CheckCandidateDoTheExam(examID);
            }
            return result;
        }

        /// <summary>
        /// //check did any candidate already perform this exam
        /// </summary>
        /// <returns></returns>
        private bool CheckCandidateDoTheExam(long examID)
        {
            bool result = false;
            List<long> candidate_Exam_Id_List = dbContext.LOT_Candidate_Exams.Where(c => c.ExamID == examID).Select(c => c.ID).ToList<long>();
            //loop all records of candidate answer to check, if match the condition the break loop
            foreach (long id in candidate_Exam_Id_List)
            {
                List<long> candidate_Answer_id_list = dbContext.LOT_CandidateAnswers.Where(c => candidate_Exam_Id_List.Contains(c.CandidateExamID)).Select(c => c.ID).ToList<long>();
                if (candidate_Answer_id_list.Count > 0) //match the condition
                {
                    result = true;
                    break;
                }
            }

            return result;
        }


        /// <summary>
        /// Get Candidate List By the exam ID
        /// </summary>
        /// <param name="examQuestionSectionID"></param>
        /// <returns></returns>
        public List<sp_GetCandidateResult> GetCandidateListByExamID(int examID, string candidate_name, int source_search, int title, int statusId, string from, string to)
        {
            List<sp_GetCandidateResult> candidates = candidateDao.GetList(candidate_name, source_search, title, statusId, from, to,0);
            List<int> assignedCandidateListID = AssignedCandidateListID(examID);
            List<sp_GetCandidateResult> candidateList = candidates.Where(c => !assignedCandidateListID.Contains(c.ID)).ToList<sp_GetCandidateResult>();
            return candidateList;
        }

        /// <summary>
        /// Get Assigned Candidate id List By the exam ID
        /// </summary>
        /// <param name="examQuestionSectionID"></param>
        /// <returns></returns>
        public List<int> AssignedCandidateListID(int examID)
        {
            List<int> assignedCandidateListID = dbContext.LOT_Candidate_Exams.Where(c => c.ExamID == examID).Select(c => c.CandidateID.HasValue ? c.CandidateID.Value : 0).ToList<int>();
            return assignedCandidateListID;
        }

        /// <summary>
        /// Get Employee List By the exam ID
        /// </summary>
        /// <param name="examQuestionSectionID"></param>
        /// <returns></returns>
        public List<sp_GetEmployeeResult> GetEmployeeByExamID(int examID, string name, int department, int subDepartment, int title, int isActive, int status)
        {
            List<sp_GetEmployeeResult> employees = empDao.GetList(name, department, subDepartment, title, isActive, status);
            List<string> assignedEmployeeListID = AssignedEmployeeListID(examID);
            List<sp_GetEmployeeResult> employeeList = employees.Where(c => !assignedEmployeeListID.Contains(c.ID)).ToList<sp_GetEmployeeResult>();
            return employeeList;
        }

        /// <summary>
        /// Get Assigned Employee id List By the exam ID
        /// </summary>
        /// <param name="examQuestionSectionID"></param>
        /// <returns></returns>
        public List<string> AssignedEmployeeListID(int examID)
        {            
            List<string> assignedEmployeeListID = dbContext.LOT_Candidate_Exams.Where(c => c.ExamID == examID).Select(c => c.EmployeeID).ToList<string>();
            return assignedEmployeeListID;
        }

        /// <summary>
        /// Get records from LOT_Candidate_Exam table by the exam id
        /// </summary>
        /// <param name="examID"></param>
        /// <returns></returns>
        public List<sp_GetCandidateExamResult> GetCandidateExam(int examID)
        {
            return dbContext.sp_GetCandidateExam(examID).ToList<sp_GetCandidateExamResult>();
        }

        /// <summary>
        /// Get records from LOT_Candidate_Exam table by the exam id
        /// </summary>
        /// <param name="examID"></param>
        /// <returns></returns>
        public List<sp_GetEmployeeExamResult> GetEmployeeExam(int examID)
        {
            return dbContext.sp_GetEmployeeExam(examID).ToList<sp_GetEmployeeExamResult>();
        }

        /// <summary>
        /// Get single record from LOT_Candidate_Exam by the Candidate_Exam_ID
        /// </summary>
        /// <param name="candidateExamId"></param>
        /// <returns></returns>
        public LOT_Candidate_Exam GetCandidateExamById(long candidateExamId)
        {
            return dbContext.LOT_Candidate_Exams.Where(c => c.ID == candidateExamId).SingleOrDefault<LOT_Candidate_Exam>();
        }

        /// <summary>
        /// Get candidate list by exam id
        /// </summary>
        /// <param name="excamID"></param>
        /// <param name="sortColumn"></param>
        /// <param name="sortOrder"></param>
        /// <returns></returns>
        public List<sp_GetCandidateListByExamResult> GetCandidateListByExam(int examID, string sortColumn, string sortOrder)
        {
            List<sp_GetCandidateListByExamResult> list = new List<sp_GetCandidateListByExamResult>();
            LOT_Exam exam = GetByID(examID);
            if (exam != null)
            {
                list = dbContext.sp_GetCandidateListByExam(examID, exam.ExamType).ToList<sp_GetCandidateListByExamResult>();

                int order = sortOrder == Constants.SORT_DESC ? -1 : 1;

                switch (sortColumn) // switch column name
                {
                    case "CandidateName":
                        list.Sort(
                             delegate(sp_GetCandidateListByExamResult m1, sp_GetCandidateListByExamResult m2)
                             { return m1.DisplayName.CompareTo(m2.DisplayName) * order; });
                        break;
                    case "Email":
                        list.Sort(
                             delegate(sp_GetCandidateListByExamResult m1, sp_GetCandidateListByExamResult m2)
                             { return (m1.Email == null ? "" : m1.Email).CompareTo(m2.Email == null ? "" : m2.Email) * order; });
                        break;
                    case "ID":
                        list.Sort(
                             delegate(sp_GetCandidateListByExamResult m1, sp_GetCandidateListByExamResult m2)
                             { return m1.CandidateID.CompareTo(m2.CandidateID) * order; });
                        break;
                    case "SendMail":
                        list.Sort(
                             delegate(sp_GetCandidateListByExamResult m1, sp_GetCandidateListByExamResult m2)
                             { return m1.SendMail.CompareTo(m2.SendMail) * order; });
                        break;
                    case "WritingMark":
                        if (order == -1)
                        {
                            list = list.OrderByDescending(p => p.WritingMark).ToList<sp_GetCandidateListByExamResult>();
                        }
                        else
                        {
                            list = list.OrderBy(p => p.WritingMark).ToList<sp_GetCandidateListByExamResult>();
                        }
                        break;
                    case "ProgrammingMark":
                        if (order == -1)
                        {
                            list = list.OrderByDescending(p => p.ProgramingMark).ToList<sp_GetCandidateListByExamResult>();
                        }
                        else
                        {
                            list = list.OrderBy(p => p.ProgramingMark).ToList<sp_GetCandidateListByExamResult>();
                        }
                        break;
                }
            }
            return list;
        }

        /// <summary>
        /// Get writing information by candidate exam id
        /// </summary>
        /// <param name="candidateExamId"></param>
        /// <returns></returns>
        public List<sp_GetEssayInfoByCandidateExamIDResult> GetEssayInfo(long candidateExamId, int sectionID)
        {
            return dbContext.sp_GetEssayInfoByCandidateExamID(candidateExamId, sectionID).ToList<sp_GetEssayInfoByCandidateExamIDResult>();
        }

        /// <summary>
        /// Check update date whether is valid
        /// </summary>
        /// <param name="objUI"></param>
        /// <param name="objDb"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        private bool IsValidUpdateDate(LOT_Candidate_Exam objUI, LOT_Candidate_Exam objDb, out Message msg)
        {
            bool isValid = false;
            msg = null;

            if (objUI != null && objDb != null)
            {                
                if (objDb.UpdateDate.ToString().Equals(objUI.UpdateDate.ToString()))
                {
                    isValid = true;
                }
                else
                {
                    string displayName = "";
                    if (objDb.Candidate.MiddleName == null)
                    {
                        displayName = objDb.Candidate.FirstName + " " + objDb.Candidate.LastName;
                    }
                    else 
                    {
                        displayName = objDb.Candidate.FirstName + " " + objDb.Candidate.MiddleName + " " + objDb.Candidate.LastName;
                    }
                    msg = new Message(MessageConstants.E0025, MessageType.Error, "Writing information of candidate '" + displayName + "'");
                }                
            }

            return isValid;
        }

        /// <summary>
        /// Update writing mark or Tc and bug mark and comment for cadidate exam
        /// </summary>
        /// <param name="objUI"></param>
        /// <returns></returns>
        public Message Update(LOT_Candidate_Exam objUI, int sectionID)
        {
            Message msg = null;
            try
            {
                if (objUI != null)
                {
                    List<LOT_CandidateAnswer> candidateAnswers = dbContext.LOT_CandidateAnswers.ToList<LOT_CandidateAnswer>();
                    LOT_Candidate_Exam objDB = GetCandidateExamById(objUI.ID);
                    objUI.ExamID = objDB.ExamID;
                    if (IsValidUpdateDate(objUI, objDB, out msg))
                    {
                        msg = UpdateNotValidate(objUI, objDB, sectionID);
                    }
                }
            }
            catch (Exception)
            {
                msg = new Message(MessageConstants.E0007, MessageType.Error);
            }
            return msg;
        }

        /// <summary>
        /// Updating Candidate exam does not need validate
        /// </summary>
        /// <param name="objUI"></param>
        /// <param name="objDB"></param>
        /// <returns></returns>
        private Message UpdateNotValidate(LOT_Candidate_Exam objUI, LOT_Candidate_Exam objDB, int sectionID)
        {
            Message msg = null;

            objDB.UpdateDate = DateTime.Now;
            objDB.UpdatedBy = objUI.UpdatedBy;

            //double totalWritingMaxMark = candidateAnswerDAO.GetMaxWritingMark(objUI.ID, Constants.LOT_WRITING_SKILL_ID);
            double totalWritingMaxMark = candidateAnswerDAO.GetMaxWritingMark(objUI.ID, sectionID);
            // Round writing mark
            //if (objUI.WritingMark != null)
            //{
            //    objUI.WritingMark = Math.Round(objUI.WritingMark.Value);
            //}

            //if (objUI.WritingMark < 0)
            //{
            //    msg = new Message(MessageConstants.E0028, MessageType.Error, "Writing mark", "0");
            //}
            //else if (objUI.WritingMark > totalWritingMaxMark)
            //{
            //    msg = new Message(MessageConstants.E0029, MessageType.Error, "Writing mark", "writing max mark (" + totalWritingMaxMark + ")");
            //}
            //else
            //{
            //    objDB.WritingMark = objUI.WritingMark;
            //    objDB.WritingComment = objUI.WritingComment;
                dbContext.SubmitChanges();

                string displayName = "";

                if (objDB.LOT_Exam.ExamType == Constants.LOT_CANDIDATE_EXAM_ID)
                {
                    if (string.IsNullOrEmpty(objDB.Candidate.MiddleName))
                    {
                        displayName = objDB.Candidate.FirstName + " " + objDB.Candidate.LastName;
                    }
                    else
                    {
                        displayName = objDB.Candidate.FirstName + " " + objDB.Candidate.MiddleName + " " + objDB.Candidate.LastName;
                    }
                }
                else
                {
                    if (string.IsNullOrEmpty(objDB.Employee.MiddleName))
                    {
                        displayName = objDB.Employee.FirstName + " " + objDB.Employee.LastName;
                    }
                    else
                    {
                        displayName = objDB.Employee.FirstName + " " + objDB.Employee.MiddleName + " " + objDB.Employee.LastName;
                    }
                }

                msg = new Message(MessageConstants.I0001, MessageType.Info, "Writing information of candidate '" + displayName + "'", "updated");
            //}
            return msg;
        }

        /// <summary>
        /// Update Programming mark & comment
        /// </summary>
        /// <param name="objUI"></param>
        /// <returns></returns>
        public Message UpdateProgramingMark(LOT_Candidate_Exam objUI)
        {
            Message msg = null;
            try
            {
                if (objUI != null)
                {                    
                    LOT_Candidate_Exam objDB = GetCandidateExamById(objUI.ID);
                    objUI.ExamID = objDB.ExamID;
                    //check valid update date
                    if (IsValidUpdateDate(objUI, objDB, out msg))
                    {
                        msg = UpdateProgramingMark(objDB, objUI);
                    }
                }
            }
            catch (Exception)
            {
                msg = new Message(MessageConstants.E0007, MessageType.Error);
            }
            return msg;
        }

        /// <summary>
        /// Update Programming mark & comment to db
        /// </summary>
        /// <param name="objDB"></param>
        /// <param name="objUI"></param>
        /// <returns></returns>
        private Message UpdateProgramingMark(LOT_Candidate_Exam objDB, LOT_Candidate_Exam objUI)
        {
            Message msg = null;
            objDB.UpdateDate = DateTime.Now;
            objDB.UpdatedBy = objUI.UpdatedBy;

            double totalProgramingMaxMark = candidateAnswerDAO.GetMaxWritingMark(objUI.ID, Constants.LOT_PROGRAMMING_SKILL_ID);
            // Round writing mark
            //if (objUI.ProgramingMark != null)
            //{
            //    objUI.ProgramingMark = Math.Round(objUI.ProgramingMark.Value);
            //}

            //if (objUI.ProgramingMark < 0)
            //{
            //    msg = new Message(MessageConstants.E0028, MessageType.Error, "Programming mark", "0");
            //}
            //else if (objUI.ProgramingMark > totalProgramingMaxMark)
            //{
            //    msg = new Message(MessageConstants.E0029, MessageType.Error, "Programming mark", "Programming max mark (" + totalProgramingMaxMark + ")");
            //}
            //else
            //{
            //    objDB.ProgramingMark = objUI.ProgramingMark;
            //    objDB.ProgramingComment = objUI.ProgramingComment;
                dbContext.SubmitChanges();

                string displayName = "";

                if (objDB.LOT_Exam.ExamType == Constants.LOT_CANDIDATE_EXAM_ID)
                {
                    if (string.IsNullOrEmpty(objDB.Candidate.MiddleName))
                    {
                        displayName = objDB.Candidate.FirstName + " " + objDB.Candidate.LastName;
                    }
                    else
                    {
                        displayName = objDB.Candidate.FirstName + " " + objDB.Candidate.MiddleName + " " + objDB.Candidate.LastName;
                    }
                }
                else
                {
                    if (string.IsNullOrEmpty(objDB.Employee.MiddleName))
                    {
                        displayName = objDB.Employee.FirstName + " " + objDB.Employee.LastName;
                    }
                    else
                    {
                        displayName = objDB.Employee.FirstName + " " + objDB.Employee.MiddleName + " " + objDB.Employee.LastName;
                    }
                }

                msg = new Message(MessageConstants.I0001, MessageType.Info, "Programming skill information of candidate '" + displayName + "'", "updated");
            //}
            return msg;            
        }

        /// <summary>
        /// Calculate mark by candidate and question section
        /// Mark is -1 if writing mark is null
        /// </summary>
        /// <param name="candidateExamId"></param>
        /// <param name="examQuestionSessionId"></param>
        public void GetMark(long candidateExamId, int examQuestionSessionId, ref double? mark, ref double maxMark)
        {   
            LOT_ExamQuestion_Section examQuestionSection = dbContext.LOT_ExamQuestion_Sections.Where(e => e.ID == examQuestionSessionId).SingleOrDefault<LOT_ExamQuestion_Section>();
            maxMark = examQuestionSection.MaxMark;
            if (examQuestionSection.SectionID == Constants.LOT_WRITING_SKILL_ID)
            {
                // get max from candidate_exam
                LOT_Candidate_Exam canExam = dbContext.LOT_Candidate_Exams.Where(c => c.ID == candidateExamId).SingleOrDefault<LOT_Candidate_Exam>();
                //mark = canExam.WritingMark != null ? canExam.WritingMark.Value : Constants.WRITTING_MARK_NULL;
                mark = Constants.WRITTING_MARK_NULL;
            }
            else if (examQuestionSection.SectionID == Constants.LOT_PROGRAMMING_SKILL_ID)
            {
                // get max from candidate_exam
                LOT_Candidate_Exam canExam = dbContext.LOT_Candidate_Exams.Where(c => c.ID == candidateExamId).SingleOrDefault<LOT_Candidate_Exam>();
                //mark = canExam.ProgramingMark != null ? canExam.ProgramingMark.Value : Constants.WRITTING_MARK_NULL;
                mark = Constants.WRITTING_MARK_NULL;
            }
            else if (examQuestionSection.SectionID == Constants.LOT_VERBAL_SKILL_ID)
            {
                //get max from candidate_exam
                maxMark = Constants.LOT_VERBAL_MAX_MARK;
                LOT_Candidate_Exam canExam = dbContext.LOT_Candidate_Exams.Where(c => c.ID == candidateExamId).SingleOrDefault<LOT_Candidate_Exam>();
                //mark = canExam.VerbalMark != null ? canExam.VerbalMark.Value : 0;
                mark = 0;
            }
            else
            {
                List<sp_GetCandidateAnswerListResult> listAnswer = candidateAnswerDAO.GetCandidateAnswer(candidateExamId, examQuestionSessionId);
                List<sp_GetCandidateAnswerListResult> correctAnswers = listAnswer.Where(c => c.IsCorrect == true).ToList<sp_GetCandidateAnswerListResult>();

                mark = Math.Round((((float)examQuestionSection.MaxMark) / listAnswer.Count) * correctAnswers.Count);
            }
        }

        /// <summary>
        /// Calculate mark  by candidate ID
        /// </summary>
        /// <param name="candidateExamId"></param>
        /// <param name="examQuestionSectionDAO"></param>
        /// <param name="candidateAnswerDAO"></param>
        /// <param name="totalMaxMark">Total MaxMark for test</param>
        /// <param name="totalMark">Total Mark for test</param>
        /// <returns></returns>
        public void CalculateMark(int candidateExamId, ref double totalMaxMark, ref double totalMark, ref double totalPrograminMark)
        {
            List<LOT_ExamQuestion_Section> examQuestionSections = examQuestionSectionDAO.GetLOT_ExamQuestion_SectionListID(candidateExamId);

            foreach (LOT_ExamQuestion_Section examQuestionSection in examQuestionSections)
            {
                List<sp_GetCandidateAnswerListResult> listAnswer = candidateAnswerDAO.GetCandidateAnswer(candidateExamId, examQuestionSection.ID);
                List<sp_GetCandidateAnswerListResult> correctAnswers = listAnswer.Where(c => c.IsCorrect == true).ToList<sp_GetCandidateAnswerListResult>();
                if (examQuestionSection.SectionID == Constants.LOT_TECHNICAL_SKILL_ID)
                {
                    totalPrograminMark = (((float)examQuestionSection.MaxMark) / listAnswer.Count) * correctAnswers.Count;
                    totalMaxMark += examQuestionSection.MaxMark;
                }
                else
                {
                    float mark = (((float)examQuestionSection.MaxMark) / listAnswer.Count) * correctAnswers.Count;
                    totalMaxMark += examQuestionSection.MaxMark;
                    totalMark += mark;
                }
            }            
        }

        public string CalculateMarkSection(LOT_Exam exam, LOT_ExamQuestion_Section section, sp_GetCandidateListByExamResult candidate,
            ref double totalMark, ref double totalMaxMark, ref bool isNan)
        {
            List<sp_GetCandidateAnswerListResult> listAnswer = candidateAnswerDAO.GetCandidateAnswer(candidate.ID, section.ID);
            List<sp_GetCandidateAnswerListResult> correctAnswers = listAnswer.Where(c => c.IsCorrect == true).ToList<sp_GetCandidateAnswerListResult>();
            double mark = (((double)section.MaxMark) / listAnswer.Count) * correctAnswers.Count;
           
            string result=string.Empty;
            if (!double.IsNaN(mark))
            {
                result = Math.Round(mark) + "/" + section.MaxMark;
                totalMaxMark += (double)section.MaxMark;
                totalMark += mark;
            }
            else
            {
                totalMaxMark += 0;
                totalMark += 0;
                isNan = false;
            }
            return result;
        }
        
        /// <summary>
        /// Get Exam Type List
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public List<ListItem> GetExamTypeList(int userID)
        {
            List<ListItem> list = new List<ListItem>();

            bool hasAssignCandidatePer = CommonFunc.CheckAuthorized(userID, (byte)Modules.Exam, (int)Permissions.AssignCandidate);
            bool hasAssignEmployeePer = CommonFunc.CheckAuthorized(userID, (byte)Modules.Exam, (int)Permissions.AssignEmployee);

            if (hasAssignCandidatePer) //has assign candidate permission
            {
                ListItem item = new ListItem(Constants.LOT_CANDIDATE_EXAM_NAME,Constants.LOT_CANDIDATE_EXAM_ID.ToString());
                list.Add(item);
            }

            if (hasAssignEmployeePer) //has assign employee permission
            {
                ListItem item = new ListItem(Constants.LOT_EMPLOYEE_EXAM_NAME, Constants.LOT_EMPLOYEE_EXAM_ID.ToString());
                list.Add(item);
            }

            if (!hasAssignCandidatePer && !hasAssignEmployeePer) //default case
            {
                ListItem item = new ListItem(Constants.LOT_CANDIDATE_EXAM_NAME, Constants.LOT_CANDIDATE_EXAM_ID.ToString());
                list.Add(item);
            }

            return list;
        }

        /// <summary>
        /// Check whether the exam question has Programming Skill
        /// </summary>
        /// <param name="examID"></param>
        /// <returns></returns>
        public bool CheckProgrammingSkillSection(int examID)
        {
            bool result = false;
            LOT_Exam exam = dbContext.LOT_Exams.Where(c => c.ID == examID).FirstOrDefault<LOT_Exam>();
            if (exam != null)
            {
                LOT_ExamQuestion_Section oject = dbContext.LOT_ExamQuestion_Sections.Where(c => c.ExamQuestionID == exam.ExamQuestionID && c.SectionID == Constants.LOT_PROGRAMMING_SKILL_ID).FirstOrDefault<LOT_ExamQuestion_Section>();
                if (oject != null)
                {
                    result = true;
                }
            }
            return result;
        }

        public bool HasWritingSection(int examId)
        {
            var examQuestionId = GetByID(examId).ExamQuestionID;
            return dbContext.LOT_ExamQuestion_Sections.FirstOrDefault(p => p.ExamQuestionID == examQuestionId &&
                p.SectionID == Constants.LOT_WRITING_SKILL_ID) != null;
        }

        public bool HasProgrammingSection(int examId)
        {
            var examQuestionId = GetByID(examId).ExamQuestionID;
            return dbContext.LOT_ExamQuestion_Sections.FirstOrDefault(p => p.ExamQuestionID == examQuestionId &&
                p.SectionID == Constants.LOT_PROGRAMMING_SKILL_ID) != null;
        }

        public bool HasTcAndBugSection(int examId)
        {
            var examQuestionId = GetByID(examId).ExamQuestionID;
            return dbContext.LOT_ExamQuestion_Sections.FirstOrDefault(p => p.ExamQuestionID == examQuestionId &&
                p.SectionID == Constants.LOT_TC_COVERAGE_AND_BUG_REPORT_TEST_ID) != null;
        }

        public bool HasVerbalSection(int examId)
        {
            var examQuestionId = GetByID(examId).ExamQuestionID;
            return dbContext.LOT_ExamQuestion_Sections.FirstOrDefault(p=>p.ExamQuestionID == examQuestionId && 
                p.SectionID == Constants.LOT_VERBAL_SKILL_ID) != null;
        }

        public Message UpdateVerbalMark(LOT_Candidate_Exam candidateExam)
        {
            Message msg = null;
            try
            {
                if (candidateExam != null)
                {
                    LOT_Candidate_Exam objDB = GetCandidateExamById(candidateExam.ID);
                    
                    //check valid update date
                    if (IsValidUpdateDate(candidateExam, objDB, out msg))
                    {
                        //objDB.VerbalMark = candidateExam.VerbalMark;
                        //objDB.VerbalMarkType = candidateExam.VerbalMarkType;
                        //objDB.VerbalComment = candidateExam.VerbalComment;
                        //objDB.VerbalTestedBy = candidateExam.VerbalTestedBy;
                        objDB.UpdateDate = DateTime.Now;
                        objDB.UpdatedBy = HttpContext.Current.User.Identity.Name;
                        dbContext.SubmitChanges();
                        msg = new Message(MessageConstants.I0001, MessageType.Info, "Verbal mark", "updated");
                    }
                }
            }
            catch (Exception)
            {
                msg = new Message(MessageConstants.E0007, MessageType.Error);
            }
            return msg;
        }

        public List<LOT_Candidate_Exam> GetListByCandidateId(int candidateId)
        {
            return dbContext.LOT_Candidate_Exams.Where(p=>p.CandidateID == candidateId).ToList();
        }

    }
}