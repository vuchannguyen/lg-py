using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Common;
using CRM.Library.Common;
using CRM.Models;

namespace CRM.Models
{
    /// <summary>
    /// author duyhung.nguyen
    /// </summary>
    public class ExamQuestionDAO : BaseDao
    {
        #region Local Variables

        private ExamQuestionSectionDAO examQuestionSectionDAO = new ExamQuestionSectionDAO();

        #endregion
        
        /// <summary>
        /// Get List
        /// </summary>
        /// <returns></returns>
        public List<LOT_ExamQuestion> GetList()
        {
            return dbContext.LOT_ExamQuestions.Where(c => c.DeleteFlag == false).OrderBy(c=> c.Title).ToList<LOT_ExamQuestion>();
        }

        /// <summary>
        /// Get Exam Question By ID
        /// </summary>
        /// <returns></returns>
        public LOT_ExamQuestion GetByID(int id)
        {
            return dbContext.LOT_ExamQuestions.Where(c => c.ID == id).FirstOrDefault<LOT_ExamQuestion>();
        }

        /// <summary>
        /// Insert to db
        /// </summary>
        /// <param name="objUI"></param>
        /// <returns></returns>
        public int Insert(LOT_ExamQuestion objUI)
        {
            try
            {
                if (objUI != null)
                {
                    // Set more info
                    objUI.DeleteFlag = false;
                    objUI.CreateDate = DateTime.Now;
                    objUI.UpdateDate = DateTime.Now;

                    dbContext.LOT_ExamQuestions.InsertOnSubmit(objUI);
                    dbContext.SubmitChanges();


                }
            }
            catch (Exception)
            {
                // return O means there is no question is inserted
                return 0;
            }
            return objUI.ID;
        }

        /// <summary>
        /// Update to db
        /// </summary>
        /// <param name="objUI"></param>
        /// <returns></returns>
        public Message Update(LOT_ExamQuestion objUI)
        {
            Message msg = null;
            try
            {
                if (objUI != null)
                {
                    // Set more info                    
                    LOT_ExamQuestion objDB = GetByID(objUI.ID);
                    if (objDB != null)
                    {
                        objDB.Title = objUI.Title;
                        objDB.ExamQuestionTime = objUI.ExamQuestionTime;

                        // Set more info
                        objDB.UpdatedBy = objUI.UpdatedBy;
                        objDB.UpdateDate = DateTime.Now;

                        dbContext.SubmitChanges();
                    }
                    else
                    {
                        msg = new Message(MessageConstants.E0005, MessageType.Error, "section name '" + objUI.Title + "'", "database");
                    }
                    // Show success message
                    msg = new Message(MessageConstants.I0001, MessageType.Info, "Exam Question-Section ", "updated");

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
        /// Insert exam question and sections of it to db
        /// </summary>
        /// <param name="examQuestion"></param>
        /// <param name="list"></param>
        /// <returns></returns>
        public Message InsertMulti(LOT_ExamQuestion examQuestion, List<LOT_ExamQuestion_Section> list)
        {
            Message msg = null;
            bool isOk = true;
            DbTransaction trans = null;
            try
            {
                //begin transaction
                dbContext.Connection.Open();
                trans = dbContext.Connection.BeginTransaction();
                dbContext.Transaction = trans;

                if (examQuestion != null)
                {
                    //insert exam question
                    int id = Insert(examQuestion);

                    if (id != 0)
                    {
                        //insert questions for each section of this exam question
                        foreach (LOT_ExamQuestion_Section item in list)
                        {
                            item.ExamQuestionID = id;
                            msg = examQuestionSectionDAO.Insert(item);
                            if (msg.MsgType == MessageType.Error)
                            {
                                isOk = false;
                                break;
                            }
                        }
                    }
                    else // there is no exam question is inserted
                    {
                        msg = new Message(MessageConstants.E0007, MessageType.Error);
                        isOk = false;
                    }

                    if (isOk) //inserted successfully
                    {
                        msg = new Message(MessageConstants.I0001, MessageType.Info, "Exam question '" + examQuestion.Title + "'", "added");
                        trans.Commit();
                    }
                    else //error
                    {
                        msg = new Message(MessageConstants.E0007, MessageType.Error);
                        trans.Rollback();
                    }
                }
            }
            catch (Exception)
            {
                if (trans != null) trans.Rollback();
                // Show system error
                msg = new Message(MessageConstants.E0007, MessageType.Error);
            }
            return msg;
        }

        /// <summary>
        /// Update exam question and sections of it
        /// </summary>
        /// <param name="examQuestion"></param>
        /// <param name="list"></param>
        /// <returns></returns>
        public Message UpdateMulti(LOT_ExamQuestion examQuestion, List<LOT_ExamQuestion_Section> list)
        {
            Message msg = null;
            bool isOk = true;
            DbTransaction trans = null;
            try
            {
                //begin transaction
                dbContext.Connection.Open();
                trans = dbContext.Connection.BeginTransaction();
                dbContext.Transaction = trans;

                if (examQuestion != null)
                {
                    // update exam question
                    msg = Update(examQuestion);
                    List<LOT_ExamQuestion_Section> sectionList = examQuestionSectionDAO.GetListByExamQuestionID(examQuestion.ID);
                    if (msg.MsgType != MessageType.Error) //update exam question successfully
                    {
                        foreach (LOT_ExamQuestion_Section item in list)
                        {
                            LOT_ExamQuestion_Section exQuestionSection = sectionList.Where(c => c.SectionID == item.SectionID && c.ExamQuestionID == examQuestion.ID).FirstOrDefault<LOT_ExamQuestion_Section>();
                            if (exQuestionSection == null) // does not exist
                            {
                                msg = examQuestionSectionDAO.Insert(item); //insert to database
                            }
                            else//exist --> go to update
                            {
                                msg = examQuestionSectionDAO.Update(item); //update to database
                            }

                            if (msg.MsgType == MessageType.Error)
                            {
                                isOk = false;
                                break;
                            }
                        }

                        //remove the items existed in db but are not seleted on GUI
                        foreach (LOT_ExamQuestion_Section item in sectionList)
                        {
                            //check item is seleted on GUI
                            LOT_ExamQuestion_Section exQuestionSection = list.Where(c => c.SectionID == item.SectionID && c.ExamQuestionID == examQuestion.ID).FirstOrDefault<LOT_ExamQuestion_Section>();
                            if (exQuestionSection == null)
                            {
                                //delete out of db
                                msg = examQuestionSectionDAO.Delete(item);
                                if (msg.MsgType == MessageType.Error)
                                {
                                    isOk = false;
                                    break;
                                }
                            }
                        }
                    }
                    else //update exam question error
                    {
                        msg = new Message(MessageConstants.E0007, MessageType.Error);
                        isOk = false;
                    }

                    //everything is ok, perform to commit
                    if (isOk)
                    {
                        msg = new Message(MessageConstants.I0001, MessageType.Info, "Exam question '" + examQuestion.Title + "'", "updated");
                        trans.Commit();
                    }
                    else //error
                    {                     
                        trans.Rollback();
                    }
                }
            }
            catch (Exception)
            {
                if (trans != null) trans.Rollback();
                // Show system error
                msg = new Message(MessageConstants.E0007, MessageType.Error);
            }
            return msg;
        }

        /// <summary>
        /// Sort Exam Question
        /// </summary>
        /// <param name="list"></param>
        /// <param name="sortColumn"></param>
        /// <param name="sortOrder"></param>
        /// <returns></returns>
        public List<LOT_ExamQuestion> Sort(List<LOT_ExamQuestion> list, string sortColumn, string sortOrder)
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
                case "Title":
                    list.Sort(
                         delegate(LOT_ExamQuestion m1, LOT_ExamQuestion m2)
                         { return m1.Title.CompareTo(m2.Title) * order; });
                    break;
                case "Time":
                    list.Sort(
                         delegate(LOT_ExamQuestion m1, LOT_ExamQuestion m2)
                         { return m1.ExamQuestionTime.CompareTo(m2.ExamQuestionTime) * order; });
                    break;
                case "ID":
                    list.Sort(
                         delegate(LOT_ExamQuestion m1, LOT_ExamQuestion m2)
                         { return m1.ID.CompareTo(m2.ID) * order; });
                    break;
            }

            return list;
        }


        /// <summary>
        /// Delete List
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

                //check valid param
                if (!string.IsNullOrEmpty(ids))
                {
                    ids = ids.TrimEnd(Constants.SEPARATE_IDS_CHAR);
                    string[] idArr = ids.Split(Constants.SEPARATE_IDS_CHAR);
                    int examQuestionId = 0;
                    int totalID = idArr.Count();

                    //loop id to delete
                    foreach (string id in idArr)
                    {
                        bool isInterger = Int32.TryParse(id, out examQuestionId);

                        //check does user select all records to delete?
                        if (isInterger)
                        {
                            isOK = CheckValidAndDelete(examQuestionId,userName,ref msg);
                            if (!isOK)
                            {
                                break;
                            }                            
                        }
                        else
                        {
                            totalID--;
                        }
                    }

                    if (isOK)
                    {
                        // Show succes message
                        msg = new Message(MessageConstants.I0001, MessageType.Info, totalID.ToString() + " exam question(s)", "deleted");
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
        /// Check valid object then delete
        /// </summary>
        /// <returns></returns>
        private bool CheckValidAndDelete(int examQuestionId, string userName,ref Message msg)
        {
            bool isOK = true;
            LOT_ExamQuestion examQuestion = GetByID(examQuestionId);
            if (examQuestion != null)
            {
                examQuestion.UpdatedBy = userName;
                //check is the exam used for the test
                if (IsTested(examQuestion.ID))
                {
                    msg = new Message(MessageConstants.E0035, MessageType.Error, "exam question", examQuestion.Title);                          
                }
                else
                {
                    //delete from db
                    msg = Delete(examQuestion);                    
                }

                if (msg.MsgType == MessageType.Error)
                {
                    isOK = false;
                }
            }
            return isOK;
        }
       
        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="objUI"></param>
        private Message Delete(LOT_ExamQuestion objUI)
        {
            Message msg = null;
            if (objUI != null)
            {
                // Get list of exam
                List<LOT_Exam> list = dbContext.LOT_Exams.Where(c => c.ExamQuestionID == objUI.ID && c.DeleteFlag == false).ToList<LOT_Exam>();
                //check whether the exam question is used for the exams
                if (list.Count == 0)
                {
                    LOT_ExamQuestion objDb = GetByID(objUI.ID);

                    if (objDb != null)
                    {
                        // Set delete info
                        objDb.DeleteFlag = true;
                        objDb.UpdateDate = DateTime.Now;
                        objDb.UpdatedBy = objUI.UpdatedBy;
                    }
                    // Submit changes to dbContext
                    msg = new Message(MessageConstants.I0001, MessageType.Info, "Exam question", "deleted");
                    dbContext.SubmitChanges();
                }
                else
                {
                    msg = new Message(MessageConstants.E0006, MessageType.Error, "delete", "it");
                }
            }
            return msg;
        }

        /// <summary>
        /// Check is the exam question used for the test
        /// </summary>
        /// <param name="examQuestionID"></param>
        /// <returns></returns>
        public bool IsTested(int examQuestionID)
        {
            bool result = false;

            List<LOT_Exam> list = dbContext.LOT_Exams.Where(c => c.ExamQuestionID == examQuestionID && c.DeleteFlag == false).ToList<LOT_Exam>();

            foreach (LOT_Exam item in list)
            {
                //check did the exam already happen?
                if (item.ExamDate.Date < DateTime.Now.Date)
                {
                    result = true;
                    break;
                }
                else
                {
                    //check did any candidate already perform this exam question
                    result = CheckCandidateDoTheExam(item.ID);
                    if (result)
                    {
                        break;
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// //check did any candidate already perform this exam
        /// </summary>
        /// <returns></returns>
        private bool CheckCandidateDoTheExam(int examID)
        {
            bool result = false;
            List<int> candidate_Exam_Id_List = dbContext.LOT_Candidate_Exams.Where(c => c.ExamID == examID).Select(c => c.ID).ToList<int>();
            //loop all records of candidate answer to check, if match the condition the break loop
            foreach (int id in candidate_Exam_Id_List)
            {
                List<int> candidate_Answer_id_list = dbContext.LOT_CandidateAnswers.Where(c => candidate_Exam_Id_List.Contains(c.CandidateExamID)).Select(c => c.ID).ToList<int>();
                if (candidate_Answer_id_list.Count > 0) //match the condition
                {
                    result = true;
                    break;
                }
            }

            return result;
        }

        /// <summary>
        /// Get question list by exam question id
        /// </summary>
        /// <param name="examQuestionSectionID"></param>
        /// <returns></returns>
        public List<sp_GetQuestionListByExamQuestionSectionIDResult> GetQuestionListByExamQuestionID(int examQuestionSectionID)
        {
            return dbContext.sp_GetQuestionListByExamQuestionSectionID(examQuestionSectionID).ToList<sp_GetQuestionListByExamQuestionSectionIDResult>();
        }

        /// <summary>
        /// Get listening topic by exam question id
        /// </summary>
        /// <param name="examQuestionSectionID"></param>
        /// <returns></returns>
        public List<sp_GetListeningTopicListByExamQuestionSectionIDResult> GetListeningTopicListByExamQuestionID(int examQuestionSectionID)
        {
            return dbContext.sp_GetListeningTopicListByExamQuestionSectionID(examQuestionSectionID).ToList<sp_GetListeningTopicListByExamQuestionSectionIDResult>();
        }
        /// <summary>
        /// Get comprehension by exam question section id
        /// </summary>
        /// <param name="examQuestionSectionID"></param>
        /// <returns></returns>
        public List<sp_GetComprehensionParagraphListByExamQuestionSectionIDResult> GetComprehensionByExamQuestionSectionID(int examQuestionSectionID)
        {
            return dbContext.sp_GetComprehensionParagraphListByExamQuestionSectionID(examQuestionSectionID).ToList<sp_GetComprehensionParagraphListByExamQuestionSectionIDResult>();
        }

        /// <summary>
        /// Get comprehension paragraph by exam question id
        /// </summary>
        /// <param name="examQuestionSectionID"></param>
        /// <returns></returns>
        public List<sp_GetComprehensionParagraphListByExamQuestionSectionIDResult> GetComprehensionParagraphListByExamQuestionID(int examQuestionSectionID)
        {
            return dbContext.sp_GetComprehensionParagraphListByExamQuestionSectionID(examQuestionSectionID).ToList<sp_GetComprehensionParagraphListByExamQuestionSectionIDResult>();
        }

        //Added by Huy.Ly 28-Dec-10
        /// <summary>
        /// Get question list that has been assigned
        /// </summary>
        /// <param name="examQuestionSectionID"></param>
        /// <returns></returns>
        public List<LOT_Question> GetQuestionListAssigned(int examQuestionSectionID)
        {
            var result = from p in dbContext.sp_GetQuestionListByExamQuestionSectionID(examQuestionSectionID)
                         select new LOT_Question()
                         {
                             ID = p.QuestionID,                                                          
                             QuestionContent = p.QuestionContent
                         };
            return result.ToList<LOT_Question>();
        }
        //Added by Huy.Ly 29-Dec-10
        /// <summary>
        /// Get question list from table LOT_ListeningTopic
        /// </summary>
        /// <param name="examQuestionSectionID"></param>
        /// <returns></returns>
        public List<LOT_Question> GetQuestionListOfListeningTopic(int examQuestionSectionID)
        {
            QuestionDao questionDao = new QuestionDao();
            List<sp_GetListeningTopicListByExamQuestionSectionIDResult> list = GetListeningTopicListByExamQuestionID(examQuestionSectionID);
            List<LOT_Question> questions = new List<LOT_Question>();
            foreach(sp_GetListeningTopicListByExamQuestionSectionIDResult item in list)
            {
                List<LOT_Question> qs = questionDao.GetListByListeningTopicID(item.ListeningTopicID);
                foreach (LOT_Question q in qs)
                {
                    questions.Add(q);
                }
            }
            return questions;
        }

        /// <summary>
        /// Get question list from table LOT_ComprehensionParagraph
        /// </summary>
        /// <param name="examQuestionSectionID"></param>
        /// <returns></returns>
        public List<LOT_Question> GetQuestionListOfComprehension(int examQuestionSectionID)
        {
            QuestionDao questionDao = new QuestionDao();
            List<sp_GetComprehensionParagraphListByExamQuestionSectionIDResult> list = GetComprehensionByExamQuestionSectionID(examQuestionSectionID);
            List<LOT_Question> questions = new List<LOT_Question>();
            foreach (sp_GetComprehensionParagraphListByExamQuestionSectionIDResult item in list)
            {
                List<LOT_Question> qs = questionDao.GetListByParagraphID(item.ParagraphID);
                foreach (LOT_Question q in qs)
                {
                    questions.Add(q);
                }
            }
            return questions;
        }

        /// <summary>
        /// Get section list by exam question id
        /// </summary>
        /// <param name="examQuestionID"></param>
        /// <returns></returns>
        public List<sp_GetSectionListByExamQuestionIDResult> GetSectionListByExamQuestionID(int examQuestionID)
        {
            return dbContext.sp_GetSectionListByExamQuestionID(examQuestionID).ToList<sp_GetSectionListByExamQuestionIDResult>();
        }

        /// <summary>
        /// Get not random section list by exam question id
        /// </summary>
        /// <param name="examQuestionID"></param>
        /// <returns></returns>
        public List<sp_GetSectionListByExamQuestionIDResult> GetNotRandomSectionList(int examQuestionID)
        {
            return dbContext.sp_GetSectionListByExamQuestionID(examQuestionID).Where(c => !c.IsRandom).ToList<sp_GetSectionListByExamQuestionIDResult>();
        }
        public bool HasOnlyVerbalSection(int examQuestionId)
        {
            var list = dbContext.LOT_ExamQuestion_Sections.Where(p =>p.ExamQuestionID == examQuestionId);
            var obj = list.FirstOrDefault(p => p.ExamQuestionID == examQuestionId && p.SectionID == Constants.LOT_VERBAL_SKILL_ID);
            return obj != null && list.Count() == 1;
        }
        /// <summary>
        /// Get Question List By Exam_Question_Section_ID
        /// </summary>
        /// <param name="examQuestionSectionID"></param>
        /// <returns></returns>
        public List<LOT_Question> GetQuestionListByExamQuestionSectionID(int examQuestionSectionID)
        {
            List<LOT_Question> questionList = new List<LOT_Question>();
            LOT_ExamQuestion_Section examQuestionSection = dbContext.LOT_ExamQuestion_Sections.Where(c => c.ID == examQuestionSectionID).FirstOrDefault<LOT_ExamQuestion_Section>();
            //check valid object
            if (examQuestionSection != null)
            {                
                if (examQuestionSection.SectionID == Constants.LOT_LISTENING_TOPIC_ID)
                {
                    //get assigned listening topic list
                    List<int> assignedQuestionListID = dbContext.LOT_ExamQuestion_Section_ListeningTopics.Where(c => c.ExamQuestionSectionID == examQuestionSectionID).Select(c => c.ListeningTopicID).ToList<int>();
                    //get a list which does not contain items of the list above
                    var listeningTopics = from a in dbContext.LOT_ListeningTopics
                                          where !assignedQuestionListID.Contains(a.ID) && a.DeleteFlag == false
                                          select a;
                    //add to list
                    foreach (var listeningTopic in listeningTopics)
                    {
                        LOT_Question item = new LOT_Question();
                        item.ID = listeningTopic.ID;
                        item.QuestionContent = listeningTopic.TopicName;
                        questionList.Add(item);
                    }
                }
                else if (examQuestionSection.SectionID == Constants.LOT_COMPREHENSION_SKILL_ID)
                {
                    //get assigned listening topic list
                    List<int> assignedQuestionListID = dbContext.LOT_ExamQuestion_Section_Comprehensions.Where(c => c.ExamQuestionSectionID == examQuestionSectionID).Select(c => c.ParagraphID).ToList<int>();
                    //get a list which does not contain items of the list above
                    var paragraphs = from a in dbContext.LOT_ComprehensionParagraphs
                                          where !assignedQuestionListID.Contains(a.ID) && a.DeleteFlag == false
                                          select a;
                    //add to list
                    foreach (var paragraph in paragraphs)
                    {
                        LOT_Question item = new LOT_Question();
                        item.ID = paragraph.ID;
                        item.QuestionContent = paragraph.ParagraphContent;
                        questionList.Add(item);
                    }
                }
                else
                {
                    //get assigned question list
                    List<int> assignedQuestionListID = dbContext.LOT_ExamQuestion_Section_Questions.Where(c => c.ExamQuestionSectionID == examQuestionSectionID).Select(c => c.QuestionID).ToList<int>();

                    //get a list which does not contain items of the list above
                    questionList = dbContext.LOT_Questions.Where(c => c.SectionID == examQuestionSection.SectionID && !assignedQuestionListID.Contains(c.ID) && c.DeleteFlag == false).ToList<LOT_Question>();                 
                }                
            }
            return questionList;
        }        
    }
}