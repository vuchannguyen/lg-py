using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Common;
using CRM.Library.Common;
using CRM.Models;

namespace CRM.Models
{
    public class CandidateAnswerDao : BaseDao
    {        
        public LOT_CandidateAnswer GetItemByID(int id)
        {
            return dbContext.LOT_CandidateAnswers.Where(p => (p.ID == id)).FirstOrDefault<LOT_CandidateAnswer>();
        }

        public LOT_CandidateAnswer GetByCandidateExamID(int id)
        {
            return dbContext.LOT_CandidateAnswers.Where(p => (p.CandidateExamID == id)).FirstOrDefault<LOT_CandidateAnswer>();
        }

        /// <summary>
        /// Get the answer by Exam ID and Question ID
        /// </summary>
        /// <param name="candidateExamID"></param>
        /// <param name="questionID"></param>
        /// <returns></returns>
        public LOT_CandidateAnswer GetByCandidateExamIDAndQuestionID(int candidateExamID, int questionID)
        {
            return dbContext.LOT_CandidateAnswers.Where(p => (p.CandidateExamID == candidateExamID) && (p.QuestionID == questionID)).FirstOrDefault<LOT_CandidateAnswer>();
        }

        /// <summary>
        /// GetCandidateExamIDAndExamQuestionSectionID
        /// </summary>
        /// <param name="candidateExamID"></param>
        /// <param name="examQuestionSectionID"></param>
        /// <returns></returns>
        public List<LOT_CandidateAnswer> GetCandidateExamIDAndExamQuestionSectionID(int candidateExamID, int examQuestionSectionID)
        {
            return dbContext.LOT_CandidateAnswers.Where(p => (p.CandidateExamID == candidateExamID && p.ExamQuestionSectionID == examQuestionSectionID)).ToList<LOT_CandidateAnswer>();
        }

        //Added by Huy.Ly 29-Dec-2010
        /// <summary>
        /// Insert a List into table LOT_CandidateAnswer
        /// </summary>
        /// <param name="list">List of LOT_CandidateAnswer object</param>
        /// <returns>Message</returns>
        public Message InsertList(List<LOT_CandidateAnswer> list)
        {
            Message msg = null;
            try
            {
                if (list.Count > 0)
                {
                    dbContext.LOT_CandidateAnswers.InsertAllOnSubmit(list);
                    dbContext.SubmitChanges();

                    // Show success message
                    msg = new Message(MessageConstants.I0001, MessageType.Info, "CandidateAnswer", "added");
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
        /// Update LOT_CandidateAnswer
        /// </summary>
        /// <param name="id"></param>
        /// <param name="value"></param>
        /// <returns>Message</returns>
        public Message Update(int id, int value)
        {
            Message msg = null;
            try
            {
                LOT_CandidateAnswer candidateAnswer = this.GetItemByID(id);
                if (candidateAnswer != null)
                {
                    candidateAnswer.AnswerID = value;
                    dbContext.SubmitChanges();

                    // Show success message
                    msg = new Message(MessageConstants.I0001, MessageType.Info, "Candidate_Answer", "updated");
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
                msg = new Message(MessageConstants.I0001, MessageType.Info, "Candidate_Answer", "updated");
            }
            catch (Exception ex)
            {
                msg = new Message(MessageConstants.E0007, MessageType.Error);
                throw ex;
            }
            return msg;
        }
        
        /// <summary>
        /// Get candidate answer by candidate exam id and section
        /// </summary>
        /// <param name="candidateExamID"></param>
        /// <param name="examQuestionSectionID"></param>
        /// <returns></returns>
        public List<sp_GetCandidateAnswerListResult> GetCandidateAnswer(int candidateExamID, int examQuestionSectionID)
        {
            return dbContext.sp_GetCandidateAnswerList(candidateExamID, examQuestionSectionID).ToList<sp_GetCandidateAnswerListResult>();
        }

        /// <summary>
        /// Get max writing mark
        /// </summary>
        /// <param name="candidateExamID"></param>
        /// <returns></returns>
        public int GetMaxWritingMark(int candidateExamID, int sectionID)
        {
            sp_GetMaxMarkByCandidateExamIDResult item = dbContext.sp_GetMaxMarkByCandidateExamID(candidateExamID, sectionID).FirstOrDefault();
            int maxMark = 0;
            if (item != null)
            {
                maxMark = item.maxMark;
            }
            return maxMark;
        }
      

        //Added by Huy.Ly 03-jan-2011
        /// <summary>
        /// Get a list answers by section and candidate exam
        /// </summary>
        /// <param name="sectionID"></param>
        /// <param name="candidateExamID"></param>
        /// <returns></returns>
        public List<CandidateAnswerQuestion> GetListAnswerBySectionID(int sectionID, int candidateExamID)
        {
            var result = from answer in dbContext.LOT_CandidateAnswers
                         join question in dbContext.LOT_Questions on answer.QuestionID equals question.ID
                         where answer.CandidateExamID == candidateExamID
                            && (question.SectionID == sectionID
                                || (sectionID == Constants.LOT_LISTENING_TOPIC_ID && question.ListeningTopicID != null)
                                || (sectionID == Constants.LOT_COMPREHENSION_SKILL_ID && question.ParagraphID != null))
                         select new CandidateAnswerQuestion()
                         {
                             ID = answer.ID,
                             CandidateExamID = answer.CandidateExamID,
                             QuestionID = answer.QuestionID,
                             AnswerID = answer.AnswerID,
                             Essay = answer.Essay,
                             ListeningTopicID = question.ListeningTopicID,
                             ParagraphID = question.ParagraphID
                         };

            return result.ToList<CandidateAnswerQuestion>();
        }
    }

    /// <summary>
    /// Create new class to use for frontend
    /// </summary>
    public class CandidateAnswerQuestion : LOT_CandidateAnswer
    {
        private int _SectionID;

        public int SectionID
        {
            get { return _SectionID; }
            set { _SectionID = value; }
        }

        private bool _IsMultipleChoice;

        public bool IsMultipleChoice
        {
            get { return _IsMultipleChoice; }
            set { _IsMultipleChoice = value; }
        }

        private string _QuestionContent;

        public string QuestionContent
        {
            get { return _QuestionContent; }
            set { _QuestionContent = value; }
        }

        private System.Nullable<int> _ListeningTopicID;

        public System.Nullable<int> ListeningTopicID
        {
            get { return _ListeningTopicID; }
            set { _ListeningTopicID = value; }
        }

        private System.Nullable<int> _ParagraphID;

        public System.Nullable<int> ParagraphID
        {
            get { return _ParagraphID; }
            set { _ParagraphID = value; }
        }

        private bool _DeleteFlag;

        public bool DeleteFlag
        {
            get { return _DeleteFlag; }
            set { _DeleteFlag = value; }
        }
    }
}