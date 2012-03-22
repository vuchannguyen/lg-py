using System.Data.Common;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using CRM.Library.Common;
using CRM.Models;
using System;
using System.Web;


namespace CRM.Models
{
    /// <summary>
    /// Data Access Object of Comprehension Paragraph
    /// </summary>
    public class ComprehensionParagraphDao : BaseDao
    {
        /// <summary>
        /// Insert a Comprehension Paragraph with its questions
        /// </summary>
        /// <param name="paragraph"></param>
        /// <param name="questionIDs"></param>
        /// <returns></returns>
        public Message Insert(LOT_ComprehensionParagraph paragraph, string[] questionIDs)
        {
            Message msg = null;
            DbTransaction trans = null;
            try
            {
                dbContext.Connection.Open();
                trans = dbContext.Connection.BeginTransaction();
                dbContext.Transaction = trans;
                //Insert new paragraph and get its ID
                paragraph.CreateDate = DateTime.Now;
                paragraph.UpdateDate = DateTime.Now;                
                dbContext.LOT_ComprehensionParagraphs.InsertOnSubmit(paragraph);
                dbContext.SubmitChanges();
                int paragraphIDInserted = paragraph.ID;
                if (questionIDs != null)
                {
                    foreach (string questionID in questionIDs)
                    {
                        //Get question from db
                        LOT_Question questionDB = dbContext.LOT_Questions.Single(p => p.DeleteFlag == false &&
                            p.ID == int.Parse(questionID));
                        questionDB.ParagraphID = paragraphIDInserted;
                        questionDB.UpdateDate = DateTime.Now;
                        questionDB.UpdatedBy = paragraph.UpdatedBy;
                        //Submit changes to db
                        dbContext.SubmitChanges();
                    }
                }
                msg = new Message(MessageConstants.I0001, MessageType.Info, "Comprehension paragraph \"" + 
                    CommonFunc.SubStringRoundWord(CommonFunc.RemoveAllHtmlWithNoTagsAllowed(paragraph.ParagraphContent), 
                    Constants.QUESTION_CONTENT_LENGTH_SHOWED_IN_MESSAGE)  + "\"", "added");
                trans.Commit();
            }
            catch
            {
                if (trans != null)
                {
                    trans.Rollback();
                }
                // Show system error
                msg = new Message(MessageConstants.E0007, MessageType.Error);
            }
            return msg;
        }
        /// <summary>
        /// Update a paragraph with it questions
        /// </summary>
        /// <param name="paragraphUI"></param>
        /// <param name="questionIDs"></param>
        /// <returns></returns>
        public Message Update(LOT_ComprehensionParagraph paragraphUI, string[] questionIDs)
        {
            Message msg = null;
            DbTransaction trans = null;
            try
            {
                dbContext.Connection.Open();
                trans = dbContext.Connection.BeginTransaction();
                dbContext.Transaction = trans;
                LOT_ComprehensionParagraph paragraphDB = GetByID(paragraphUI.ID);
                //Set the updated information
                paragraphDB.ParagraphContent = paragraphUI.ParagraphContent;
                paragraphDB.UpdatedBy = paragraphUI.UpdatedBy;
                paragraphDB.UpdateDate = DateTime.Now;
                //Submit changes to db
                dbContext.SubmitChanges();
                List<LOT_Question> currentQuestionsInDB = dbContext.LOT_Questions
                    .Where(p => p.DeleteFlag == false && p.ParagraphID == paragraphDB.ID).ToList();
                //Free all questions of the paragraph: set their paragraph id to null
                foreach (LOT_Question question in currentQuestionsInDB)
                {
                    if (questionIDs == null || !questionIDs.Contains(question.ID.ToString()))
                    {
                        //Set updated information
                        question.UpdateDate = DateTime.Now;
                        question.UpdatedBy = paragraphDB.UpdatedBy;
                        question.ParagraphID = null;
                        //Submit changes to db
                        dbContext.SubmitChanges();
                    }
                }
                //Update question list of the paragraph
                if (questionIDs != null)
                {
                    foreach (string questionID in questionIDs)
                    {
                        //Get question from db
                        LOT_Question questionDB = dbContext.LOT_Questions
                            .Where(c => c.ID == int.Parse(questionID)).FirstOrDefault<LOT_Question>();
                        //Set the undated information
                        questionDB.UpdateDate = DateTime.Now;
                        questionDB.UpdatedBy = paragraphUI.UpdatedBy;
                        questionDB.ParagraphID = paragraphDB.ID;
                        //Submit changes to db
                        dbContext.SubmitChanges();
                    }
                }
                string shortContent = CommonFunc.SubStringRoundWord(CommonFunc.RemoveAllHtmlWithNoTagsAllowed(paragraphDB.ParagraphContent),
                    Constants.QUESTION_CONTENT_LENGTH_SHOWED_IN_MESSAGE);
                msg = new Message(MessageConstants.I0001, MessageType.Info,
                    "Comprehension Paragraph \"" + CommonFunc.RemoveAllHtmlWithNoTagsAllowed(shortContent) + "\"", "updated");
                trans.Commit();
            }
            catch
            {
                if (trans != null)
                {
                    trans.Rollback();
                }
                // Show system error
                msg = new Message(MessageConstants.E0007, MessageType.Error);
            }
            return msg;
        }
        /// <summary>
        /// Check if the paragraph is in any exam
        /// </summary>
        /// <param name="paragraphID"></param>
        /// <returns></returns>
        public bool IsInAnyExam(int paragraphID)
        {
            bool result = false;
            List<LOT_CandidateAnswer> caList = dbContext.LOT_CandidateAnswers.ToList();
            caList = caList.Where(p=>p.LOT_Question.ParagraphID == paragraphID).ToList();
            if (caList.Count > 0)
            {
                result = true;
            }
            return result;
        }
        /// <summary>
        /// Check if paragraph is used
        /// </summary>
        /// <param name="paragraphID"></param>
        /// <returns></returns>
        public bool IsUsed(int paragraphID)
        {
            bool result = false;
            //Check if the paragraph is assigned to any exam
            List<LOT_ExamQuestion_Section_Comprehension> eqscList = dbContext
                .LOT_ExamQuestion_Section_Comprehensions.Where(p => p.ParagraphID == paragraphID).ToList();
            if (eqscList.Count > 0)
            {
                result = true;
            }
            //Check if the paragraph is in any exam (random)
            if (IsInAnyExam(paragraphID))
            {
                result = true;
            }
            return result;
        }

        public LOT_ComprehensionParagraph GetByID(int id)
        {
            return dbContext.LOT_ComprehensionParagraphs.Where(c => c.ID == id).FirstOrDefault<LOT_ComprehensionParagraph>();
        }


        /// <summary>
        /// Get list of listening topic by question list
        /// </summary>
        /// <param name="questionList"></param>
        /// <returns> List<LOT_ListeningTopic></returns>
        public List<LOT_ComprehensionParagraph> GetListByQuestionList(List<LOT_Question> questionList)
        {
            List<LOT_ComprehensionParagraph> comprehensionlist = new List<LOT_ComprehensionParagraph>();
            foreach (LOT_Question question in questionList)
            {
                if (!comprehensionlist.Contains(question.LOT_ComprehensionParagraph))
                {
                    comprehensionlist.Add(question.LOT_ComprehensionParagraph);
                }
            }
            return comprehensionlist;
        }

        public List<LOT_ComprehensionParagraph> GetList()
        {
            return dbContext.LOT_ComprehensionParagraphs.Where(p=>!p.DeleteFlag).ToList();
        }
    }
}