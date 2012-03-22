using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Common;
using System.Text.RegularExpressions;
using CRM.Library.Common;
using CRM.Models;
using CRM.Models.Entities;
using System.Linq.Dynamic;
using System.Data.Linq.SqlClient;

namespace CRM.Models
{
    /// <summary>
    /// Data access object of Question
    /// </summary>
    public class QuestionDao : BaseDao
    {
        #region global variables
        private ExamQuestionSectionQuestionDAO esqDao = new ExamQuestionSectionQuestionDAO();
        private AnswerDao answerDao = new AnswerDao();
        private int questionLengthShowedInMessage = Constants.QUESTION_CONTENT_LENGTH_SHOWED_IN_MESSAGE;
        #endregion

        #region New paging
        public IQueryable<QuestionList> GetQueryList(string name, string sectionID)
        {
            var sql = (from data in dbContext.LOT_Questions
                      select new QuestionList()
                      { 
                          ID = data.ID,
                          QuestionContent = data.QuestionContent,
                          SectionId = data.SectionID,
                          SectionName = data.LOT_Section.SectionName,
                          DeleteFlag = data.DeleteFlag
                      }
                      ).Union(
                        from comprehension in dbContext.LOT_ComprehensionParagraphs
                        select new QuestionList()
                        { 
                              ID = comprehension.ID,
                              QuestionContent = comprehension.ParagraphContent,
                              SectionId = Constants.LOT_COMPREHENSION_SKILL_ID,
                              SectionName = dbContext.LOT_Sections.Single(p => p.ID == Constants.LOT_COMPREHENSION_SKILL_ID).SectionName,
                              DeleteFlag = comprehension.DeleteFlag
                        }
                        ).Union(
                            from listening in dbContext.LOT_ListeningTopics
                            select new QuestionList()
                            {
                                ID = listening.ID,
                                QuestionContent = listening.TopicName,
                                SectionId = Constants.LOT_LISTENING_TOPIC_ID,
                                SectionName = dbContext.LOT_Sections.Single(p => p.ID == Constants.LOT_LISTENING_TOPIC_ID).SectionName,
                                DeleteFlag = listening.DeleteFlag
                            }
                            )

                      ;

            if (!string.IsNullOrEmpty(name))
            {
                name = CommonFunc.GetFilterText(name);
                sql = sql.Where(p => SqlMethods.Like(p.QuestionContent, name)
                    || SqlMethods.Like(p.ID.ToString(), name));
            }

            if (ConvertUtil.ConvertToInt(sectionID) != 0)
            {
                sql = sql.Where(p => p.SectionId == int.Parse(sectionID));
            }

            sql = sql.Where(p => p.DeleteFlag == false);

            var test = sql.ToList();

            return sql;
        }

        public int GetCountListLinq(string name, string sectionID)
        {
            return GetQueryList(name, sectionID).Count();
        }

        public List<QuestionList> GetListLinq(string name, string sectionID,
            string sortColumn, string sortOrder, int skip, int take)
        {
            string sortSQL = string.Empty;
            var sql = GetQueryList(name, sectionID);

            switch (sortColumn)
            {
                case "ID":
                    sortSQL += "ID " + sortOrder;
                    break;

                case "QuestionContent":
                    //sortSQL += "QuestionContent " + sortOrder; //cannot order by ntext
                    if (sortOrder == SortOrder.asc.ToString())
                    {
                        sql = sql.OrderBy(p => p.QuestionContent.Substring(0, Constants.QUESTION_LENGTH_TO_TRUNCATE));
                    }
                    else
                    {
                        sql = sql.OrderByDescending(p => p.QuestionContent.Substring(0, Constants.QUESTION_LENGTH_TO_TRUNCATE));
                    }
                    break;

                case "SectionName":
                    sortSQL += "SectionName " + sortOrder;
                    break;
            }

            if (sortColumn != "QuestionContent")
            {
                sql = GetQueryList(name, sectionID).OrderBy(sortSQL);
            }

            if (skip == 0 && take == 0)
            {
                return sql.ToList();
            }

            return sql.Skip(skip).Take(take).ToList();
        }
        #endregion

        /// <summary>
        /// Get list of all question
        /// </summary>
        /// <returns>List<LOT_Question></returns>
        //public List<LOT_Question> GetList()
        //{
        //    return dbContext.LOT_Questions.Where(p => (p.DeleteFlag == false)).ToList<LOT_Question>();
        //}
        
        /// <summary>
        /// Get list of auto complete question
        /// </summary>
        /// <returns>List<LOT_Question></returns>
        public List<LOT_Question> GetListAutoComplete(string name)
        {
            name = CommonFunc.GetFilterText(name);

            return dbContext.LOT_Questions.Where(p => (p.DeleteFlag == false) && SqlMethods.Like(p.QuestionContent, name)).ToList<LOT_Question>();
        }

        /// <summary>
        /// Get list of questions by listening topic ID
        /// </summary>
        /// <param name="topicID"></param>
        /// <returns>List<LOT_Question></returns>
        public List<LOT_Question> GetListByListeningTopicID(int topicID)
        {
            return dbContext.LOT_Questions
                .Where(p => ((p.DeleteFlag == false) && (p.ListeningTopicID == topicID)))
                .ToList<LOT_Question>();
        }

        /// <summary>
        /// Get list of questions by paragraph id
        /// </summary>
        /// <param name="topicID"></param>
        /// <returns>List<LOT_Question></returns>
        public List<LOT_Question> GetListByParagraphID(int paragraphID)
        {
            return dbContext.LOT_Questions
                .Where(p => ((p.DeleteFlag == false) && (p.ParagraphID == paragraphID)))
                .ToList<LOT_Question>();
        }

        /// <summary>
        /// Get List of questions by section ID
        /// </summary>
        /// <param name="sectionID"></param>
        /// <returns>List<LOT_Question></returns>
        public List<LOT_Question> GetListBySectionID(int sectionID)
        {
            if (sectionID == Constants.LOT_COMPREHENSION_SKILL_ID)
                return dbContext.LOT_Questions.Where(p=>!p.DeleteFlag && p.ParagraphID.HasValue).ToList();
            if (sectionID == Constants.LOT_LISTENING_TOPIC_ID)
                return dbContext.LOT_Questions.Where(p => !p.DeleteFlag && p.ListeningTopicID.HasValue).ToList();
            return dbContext.LOT_Questions
                .Where(p => ((p.DeleteFlag == false) && (p.SectionID == sectionID)))
                .ToList<LOT_Question>();
        }

        /// <summary>
        /// Get list of questions of a listening topic except the question has ID in the ids
        /// </summary>
        /// <param name="topicID"></param>
        /// <param name="ids"></param>
        /// <returns>List<LOT_Question></returns>
        public List<LOT_Question> GetListByListeningTopicIDNotContainIDs(int topicID, string ids)
        {
            string[] arrID = ids.Split(',');
            return dbContext.LOT_Questions
                .Where(p => ((p.DeleteFlag == false)
                    && (p.ListeningTopicID == topicID)
                    && (p.ListeningTopicID == null)
                    && (!arrID.Contains(p.ID.ToString()))
                    && (p.SectionID == Constants.LOT_LISTENING_QUESTION))
                    ).ToList<LOT_Question>();
        }

        /// <summary>
        /// Get list of question that is not in any topic
        /// </summary>
        /// <param name="ids"></param>
        /// <returns>List<LOT_Question></returns>
        public List<LOT_Question> GetListNotInAnyTopic(string ids)
        {
            string[] arrID = ids.Split(',');
            return dbContext.LOT_Questions
                .Where(p => ((p.DeleteFlag == false)
                    && (p.ListeningTopicID == null)
                    && (!arrID.Contains(p.ID.ToString()))
                    && (p.SectionID == Constants.LOT_LISTENING_QUESTION))
                    ).ToList<LOT_Question>();
        }

        /// <summary>
        /// Get a question by ID
        /// </summary>
        /// <param name="questionID"></param>
        /// <returns>LOT_Question</returns>
        public LOT_Question GetById(int questionID)
        {
            return dbContext.LOT_Questions
                .Where(c => c.ID == questionID && c.DeleteFlag == false).FirstOrDefault<LOT_Question>();
        }

        /// <summary>
        /// Get list of questions by name
        /// </summary>
        /// <param name="name"></param>
        /// <returns>List<LOT_Question></returns>
        public List<LOT_Question> GetListByName(string name)
        {
            return dbContext.LOT_Questions
                .Where(p => ((p.DeleteFlag == false) && (p.QuestionContent.Contains(name))))
                .ToList<LOT_Question>();
        }

        /// <summary>
        /// Sort the list od questions
        /// </summary>
        /// <param name="questionList"></param>
        /// <param name="sortColumn"></param>
        /// <param name="sortOrder"></param>
        /// <returns>List<LOT_Question></returns>
        public List<LOT_Question> Sort(List<LOT_Question> questionList, string sortColumn, string sortOrder)
        {
            int order = 1;
            if (sortOrder == "desc")
            {
                order = -1;
            }
            switch (sortColumn)
            {
                case "ID":
                    questionList.Sort(
                        delegate (LOT_Question question1, LOT_Question question2)
                         { return question1.ID.CompareTo(question2.ID)*order; });
                    break;
                case "QuestionContent":
                    questionList.Sort(
                         delegate(LOT_Question question1, LOT_Question question2)
                         { return question1.QuestionContent.CompareTo(question2.QuestionContent) * order; });
                    break;
                case "SectionName":
                    questionList.Sort(
                         delegate(LOT_Question question1, LOT_Question question2)
                         { return question1.LOT_Section.SectionName.CompareTo(question2.LOT_Section.SectionName) * order; });
                    break;
                case "CreatedBy":
                    questionList.Sort(
                         delegate(LOT_Question question1, LOT_Question question2)
                         { return question1.CreatedBy.CompareTo(question2.CreatedBy) * order; });
                    break;
                case "UpdatedBy":
                    questionList.Sort(
                         delegate(LOT_Question question1, LOT_Question question2)
                         { return question1.UpdatedBy.CompareTo(question2.UpdatedBy) * order; });
                    break;
            }
            return questionList;
        }

        /// <summary>
        /// Delete a question
        /// </summary>
        /// <param name="questionUI"></param>
        /// <returns>bool</returns>
        private bool Delete(LOT_Question questionUI)
        {
            bool result = false;
            if (questionUI != null)
            {
                // Get current info in dbContext
                LOT_Question questionDb = GetById(questionUI.ID);
                if (questionDb != null && !IsUsed(questionDb.ID))
                {
                    // Set delete info
                    questionDb.DeleteFlag = true;
                    questionDb.UpdateDate = DateTime.Now;
                    questionDb.UpdatedBy = questionUI.UpdatedBy;
                    // Submit changes to dbContext
                    dbContext.SubmitChanges();
                    result = true;
                }
            }
            return result;
        }
        /// <summary>
        /// Delete a listening topic
        /// </summary>
        /// <param name="listeningTopicUI"></param>
        /// <returns>bool</returns>
        private bool DeleteTopic(LOT_ListeningTopic listeningTopicUI)
        {
            bool result = false;
            if (listeningTopicUI != null)
            {
                // Get current info in dbContext
                LOT_ListeningTopic listeningTopicDb = dbContext.LOT_ListeningTopics
                    .Single(p => p.DeleteFlag == false && p.ID == listeningTopicUI.ID);
                if (listeningTopicDb != null)
                {
                    bool isUsed = IsTopicUsed(listeningTopicDb.ID);
                    if (!isUsed)
                    {
                        // Set delete info
                        listeningTopicDb.DeleteFlag = true;
                        listeningTopicDb.UpdateDate = DateTime.Now;
                        listeningTopicDb.UpdatedBy = listeningTopicUI.UpdatedBy;
                        // Submit changes to dbContext
                        dbContext.SubmitChanges();
                        //Free the all question of topic, set their listening topic id to null
                        List<LOT_Question> arrQuestion = dbContext.LOT_Questions
                                            .Where(p => p.ListeningTopicID == listeningTopicUI.ID)
                                            .ToList<LOT_Question>();
                        foreach (LOT_Question question in arrQuestion)
                        {
                            question.ListeningTopicID = null;
                            question.UpdatedBy = listeningTopicDb.UpdatedBy;
                            question.UpdateDate = DateTime.Now;
                            dbContext.SubmitChanges();
                        }
                        result = true;
                    }
                }
            }
            return result;
        }

        /// <summary>
        /// Delete a list of listening topics/questions
        /// </summary>
        /// <param name="ids"></param>
        /// <param name="stUpdatedBy"></param>
        /// <returns>Message</returns>
        public Message DeleteList(string ids, string stUpdatedBy)
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
                    string[] idArr = ids.TrimEnd(',').Split(',');
                    List<string> soundFileList = new List<string>(0);
                    int total = idArr.Count();
                    bool hasUsedQuestion = false;
                    bool hasUsedTopic = false;
                    bool hasUsedParagraph = false;
                    foreach (string id in idArr)
                    {
                        //prefix of id is "q": Delete question
                        if (id.Substring(0, 1).Equals(Constants.PREFIX_QUESTION_ID))
                        {
                            int questionID = int.Parse(id.Substring(Constants.PREFIX_QUESTION_ID.Length));
                            LOT_Question question = GetById(questionID);
                            if (question != null)
                            {
                                question.UpdatedBy = stUpdatedBy;
                                hasUsedQuestion = !Delete(question);
                            }
                            else
                            {
                                total--;
                            }
                        }
                        //prefix of id is "t": Delete listening topic
                        else if (id.Substring(0, 1).Equals(Constants.PREFIX_TOPIC_ID))
                        {
                            int topicID = int.Parse(id.Substring(Constants.PREFIX_TOPIC_ID.Length));
                            LOT_ListeningTopic topic = dbContext.LOT_ListeningTopics.Single(p =>
                                p.DeleteFlag == false && p.ID == topicID);
                            if (topic!=null)
                            {
                                topic.UpdatedBy = stUpdatedBy;
                                hasUsedTopic = !DeleteTopic(topic);
                                soundFileList.Add(topic.FileName);
                            }
                            else
                            {
                                total--;
                            }
                        }
                        //prefix of id is "p": Delete comprehension paragraph
                        else if (id.Substring(0, 1).Equals(Constants.PREFIX_PARAGRAPH_ID))
                        {
                            int paragraphID = int.Parse(id.Substring(Constants.PREFIX_PARAGRAPH_ID.Length));
                            LOT_ComprehensionParagraph paragraph = dbContext.LOT_ComprehensionParagraphs.Single(p =>
                                p.DeleteFlag == false && p.ID == paragraphID);
                            if (paragraph != null)
                            {
                                paragraph.UpdatedBy = stUpdatedBy;
                                hasUsedParagraph = !DeleteParagraph(paragraph);
                            }
                            else
                            {
                                total--;
                            }
                        }
                        //Value of id is false when the jqgrid header row is selected too
                        else if (id.Equals("false"))
                        {
                            total--;
                        }
                        //There's question/listening topic is used
                        if (hasUsedQuestion || hasUsedTopic || hasUsedParagraph)
                        {
                            msg = new Message(MessageConstants.E0006, MessageType.Error,
                                "delete", " selected question(s)");
                            trans.Rollback();
                            return msg;
                        }
                    }
                    msg = new Message(MessageConstants.I0001, MessageType.Info,
                        total.ToString() + " Question(s)", "deleted");
                    trans.Commit();
                    foreach (string fileName in soundFileList)
                    {
                        CommonFunc.RemoveFile(Constants.SOUND_FOLDER + fileName);
                    }
                }
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
        /// Delete a paragraph
        /// </summary>
        /// <param name="paragraph"></param>
        /// <returns></returns>
        public bool DeleteParagraph(LOT_ComprehensionParagraph paragraph)
        {
            bool result = false;
            ComprehensionParagraphDao paragraphDao = new ComprehensionParagraphDao();
            if (paragraph != null)
            {
                // Get current info in dbContext
                LOT_ComprehensionParagraph paragraphDb = dbContext.LOT_ComprehensionParagraphs
                    .Single(p => p.DeleteFlag == false && p.ID == paragraph.ID);
                if (paragraphDb != null)
                {
                    bool isUsed = paragraphDao.IsUsed(paragraphDb.ID);
                    if (!isUsed)
                    {
                        // Set delete info
                        paragraphDb.DeleteFlag = true;
                        paragraphDb.UpdateDate = DateTime.Now;
                        paragraphDb.UpdatedBy = paragraph.UpdatedBy;
                        // Submit changes to dbContext
                        dbContext.SubmitChanges();
                        //Free the all question of paragraph, set their paragraph id to null
                        List<LOT_Question> arrQuestion = dbContext.LOT_Questions
                                            .Where(p => p.ParagraphID == paragraph.ID)
                                            .ToList<LOT_Question>();
                        foreach (LOT_Question question in arrQuestion)
                        {
                            question.ParagraphID = null;
                            question.UpdatedBy = paragraphDb.UpdatedBy;
                            question.UpdateDate = DateTime.Now;
                            dbContext.SubmitChanges();
                        }
                        result = true;
                    }
                }
            }
            return result;
        }

        /// <summary>
        /// Check if the question is in any exam
        /// </summary>
        /// <param name="questionID"></param>
        /// <returns>bool</returns>
        public bool IsInAnyExam(int questionID)
        {
            bool result = false;
            List<LOT_CandidateAnswer> caList = dbContext.LOT_CandidateAnswers.
                Where(p => p.QuestionID == questionID).ToList<LOT_CandidateAnswer>();
            if (caList.Count > 0)
            {
                result = true;
            }
            return result;
        }
        /// <summary>
        /// Check if the question is assigned or not
        /// </summary>
        /// <param name="questionID"></param>
        /// <returns>bool</returns>
        public bool IsAssigned(int questionID)
        {
            bool result = false;
            List<LOT_ExamQuestion_Section_Question> esqList = esqDao.GetListByQuestionID(questionID);
            if (esqList.Count > 0)
            {
                result = true;
            }
            return result;
        }
        /// <summary>
        /// Check if the question is used in any function
        /// </summary>
        /// <param name="questionID"></param>
        /// <returns>bool</returns>
        public bool IsUsed(int questionID)
        {
            bool result = false;
            //Check if the question is assigned to any exam
            if (IsAssigned(questionID))
            {
                result = true;
            }
            //Check if the question is in any exam (random)
            if (IsInAnyExam(questionID))
            {
                result = true;
            }
            //Check if the question is assign to any topic
            if (GetById(questionID).ListeningTopicID != null)
            {
                result = true;
            }
            return result;
        }

        /// <summary>
        /// Checked if the topic is used in any function
        /// </summary>
        /// <param name="topicID"></param>
        /// <returns>bool</returns>
        public bool IsTopicUsed(int topicID)
        {
            bool result = false;
            //Check if the topic is assigned to any exam
            List<LOT_ExamQuestion_Section_ListeningTopic> eqslList =
                dbContext.LOT_ExamQuestion_Section_ListeningTopics
                .Where(p => p.ListeningTopicID == topicID).ToList<LOT_ExamQuestion_Section_ListeningTopic>();
            if (eqslList.Count > 0)
            {
                result = true;
            }
            //Check if the topic is in any exam (random)
            List<LOT_CandidateAnswer> caList = dbContext.LOT_CandidateAnswers.
                Where(p => p.LOT_Question.ListeningTopicID == topicID).ToList<LOT_CandidateAnswer>();
            if (caList.Count > 0)
            {
                result = true;
            }
            return result;
        }

        /// <summary>
        /// Insert a question without answer
        /// </summary>
        /// <param name="question"></param>
        /// <returns>int</returns>
        public int Insert(LOT_Question question)
        {
            int result = 0;
            try
            {
                if (question != null)
                {
                    //Set the updated information
                    question.CreatedDate = DateTime.Now;
                    question.UpdateDate = DateTime.Now;
                    dbContext.LOT_Questions.InsertOnSubmit(question);
                    //Submit changes to database
                    dbContext.SubmitChanges();
                    result = question.ID;
                }
            }
            catch
            {
                result = 0;
            }
            return result;
        }

        /// <summary>
        /// Insert a question with its answers
        /// </summary>
        /// <param name="question"></param>
        /// <param name="arrAnswer"></param>
        /// <returns>Message</returns>
        public Message Insert(LOT_Question question, List<LOT_Answer> arrAnswer)
        {
            Message msg = null;
            DbTransaction trans = null;
            try
            {
                dbContext.Connection.Open();
                trans = dbContext.Connection.BeginTransaction();
                dbContext.Transaction = trans;
                //Remove white space
                question.QuestionContent = Regex.Replace(question.QuestionContent, @"[ ]{2,}", " ");
                //Insert a question and get its ID
                int questionIDInserted = Insert(question);                
                //Insert answer if question is multiplechoice
                if (arrAnswer != null)
                {                   
                    foreach (LOT_Answer answer in arrAnswer)
                    {
                        answer.QuestionID = questionIDInserted;
                        dbContext.LOT_Answers.InsertOnSubmit(answer);                    
                        dbContext.SubmitChanges();                        
                    }
                }
                msg = new Message(MessageConstants.I0001, MessageType.Info, "Question \"" +
                    CommonFunc.SubStringRoundWord(CommonFunc.RemoveAllHtmlWithNoTagsAllowed(question.QuestionContent),
                    Constants.QUESTION_CONTENT_LENGTH_SHOWED_IN_MESSAGE) + "\"", "added");                
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
        /// Check if the answer is exist in list of answers on page
        /// </summary>
        /// <param name="answerDb"></param>
        /// <param name="arrAnswerUI"></param>
        /// <returns>bool</returns>
        public bool IsDeletedOnUI(LOT_Answer answerDb, List<LOT_Answer> arrAnswerUI)
        {
            bool result = true;
            foreach (LOT_Answer answerUI in arrAnswerUI)
            {
                if (answerUI.ID == answerDb.ID)
                {
                    result = false;
                }
            }
            return result;
        }

        /// <summary>
        /// Update a question with its answers
        /// </summary>
        /// <param name="questionUI"></param>
        /// <param name="arrAnswer"></param>
        /// <returns>Message</returns>
        public Message Update(LOT_Question questionUI, List<LOT_Answer> arrAnswer)
        {
            Message msg = null;
            DbTransaction trans = null;
            try
            {
                dbContext.Connection.Open();
                trans = dbContext.Connection.BeginTransaction();
                dbContext.Transaction = trans;
                //Get question from database
                LOT_Question questionDB = GetById(questionUI.ID);
                //Set updated information
                questionDB.QuestionContent = Regex.Replace(questionUI.QuestionContent, @"[ ]{2,}", " ");
                questionDB.UpdatedBy = questionUI.UpdatedBy;
                questionDB.UpdateDate = DateTime.Now;
                questionDB.SectionID = questionUI.SectionID;
                dbContext.SubmitChanges();
                //Update answers of the question
                if (arrAnswer != null)
                {
                    //Get list of answer before updating
                    List<LOT_Answer> currentAnswersInDb = answerDao.GetListByQuestionID(questionDB.ID);
                    foreach (LOT_Answer a in currentAnswersInDb)
                    {
                        //Delete the answer if it was delete on page
                        if (IsDeletedOnUI(a, arrAnswer))
                        {
                            answerDao.Delete(a);
                        }
                    }
                    foreach (LOT_Answer answer in arrAnswer)
                    {
                        //Update the answer if it exists
                        if (answer.ID != 0)
                        {
                            LOT_Answer answerDB = dbContext.LOT_Answers
                                .Where(c => c.ID == answer.ID).SingleOrDefault<LOT_Answer>();
                            //Set the updated information
                            answerDB.AnswerContent = answer.AnswerContent;
                            answerDB.AnswerOrder = answer.AnswerOrder;
                            answerDB.IsCorrect = answer.IsCorrect;
                            //Submit changes to database
                            dbContext.SubmitChanges();
                        }
                        //Insert new answer if it does not exist
                        else
                        {
                            answer.QuestionID = questionDB.ID;
                            answerDao.Insert(answer);
                        }
                    }
                }
                msg = new Message(MessageConstants.I0001, MessageType.Info, "Question \"" +
                    CommonFunc.SubStringRoundWord(CommonFunc.RemoveAllHtmlWithNoTagsAllowed(questionDB.QuestionContent), 
                    Constants.QUESTION_CONTENT_LENGTH_SHOWED_IN_MESSAGE) + "\"", "updated");
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
        //Added by Huy.Ly 28-Dec-2010
        /// <summary>
        /// Get question list random by section to use for updating data into table LOT_CandidateAnswer
        /// </summary>
        /// <param name="section"></param>
        /// <param name="numberOfQuestions"></param>
        /// <returns></returns>
        public List<LOT_Question> GetQuestionListRandonBySectionID(int section, int numberOfQuestions)
        {
            var result = from p in dbContext.sp_GetQuestionListRandomBySectionID(section, numberOfQuestions)
                         select new LOT_Question()
                         {
                             ID = p.ID,
                             SectionID = p.SectionID,
                             QuestionContent = p.QuestionContent,
                             ListeningTopicID = p.ListeningTopicID
                         };
            return result.ToList<LOT_Question>();
        }
        //Added by Huy.Ly 28-Dec-2010
        /// <summary>
        /// Get question list random by section that its type is Listening Skill  to use for updating data into table LOT_CandidateAnswer
        /// </summary>
        /// <param name="section"></param>
        /// <param name="numberOfQuestions"></param>
        /// <returns></returns>
        public List<LOT_Question> GetQuestionListRandomBySectionListeningSkillID(int numberOfQuestions, long candidateExamID)
        {
            try
            {
                CandidateTopicDao candidateTopicDao = new CandidateTopicDao();
                //List<sp_GetRandomListeningTopicResult> listRandom = dbContext.sp_GetRandomListeningTopic(numberOfQuestions).ToList();
                var listRandom = CommonFunc.LOTGetRandomListeningTopics(numberOfQuestions);
                List<LOT_Question> listQuestion = new List<LOT_Question>();
                List<LOT_Candidate_Topic> listCandidateTopic = new List<LOT_Candidate_Topic>();
                LOT_Candidate_Topic ct = null;
                foreach (var topicId in listRandom)
                {
                    //add item to table LOT_Candidate_Topic
                    ct = new LOT_Candidate_Topic() { CandidateExamID = candidateExamID, TopicID = topicId };
                    listCandidateTopic.Add(ct);

                    List<LOT_Question> temp = GetListByListeningTopicID(topicId);
                    foreach (LOT_Question q in temp)
                    {
                        listQuestion.Add(q);
                    }
                }

                //insert into candidate topic
                candidateTopicDao.InsertList(listCandidateTopic);

                return listQuestion;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        //Added by Huy.Ly 28-Dec-2010
        /// <summary>
        /// Get question list random by section that its type is Comprehension Skill
        /// </summary>
        /// <param name="section"></param>
        /// <param name="numberOfQuestions"></param>
        /// <returns></returns>
        public List<LOT_Question> GetQuestionListRandomBySectionComprehensionSkillID(int numberOfQuestions)
        {
            try
            {                
                //List<sp_GetRandomComprehensionParagraphResult> listRandom = dbContext.sp_GetRandomComprehensionParagraph(numberOfQuestions).ToList();
                List<int> listRandom = CommonFunc.LOTGetRandomParagraphs(numberOfQuestions);
                List<LOT_Question> listQuestion = new List<LOT_Question>();
                foreach (var paragraphId in listRandom)
                {
                    List<LOT_Question> temp = GetListByParagraphID(paragraphId);
                    foreach (LOT_Question q in temp)
                    {
                        listQuestion.Add(q);
                    }
                }                

                return listQuestion;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        //Added by Huy.Ly 29-Dec-2010
        /// <summary>
        /// Get question list to display on the frontpage
        /// </summary>
        /// <param name="candidateExamID"></param>
        /// <param name="examQuestionSectionID"></param>
        /// <returns></returns>
        public List<CandidateAnswerQuestion> GetQuestionListFromCandidateAnswer(long candidateExamID, long examQuestionSectionID)
        {
            var result = from answer in dbContext.LOT_CandidateAnswers
                         join question in dbContext.LOT_Questions on answer.QuestionID equals question.ID
                         where answer.CandidateExamID == candidateExamID && answer.ExamQuestionSectionID == examQuestionSectionID
                         select new CandidateAnswerQuestion()
                           {
                               ID = answer.ID,
                               CandidateExamID = answer.CandidateExamID,
                               ExamQuestionSectionID = answer.ExamQuestionSectionID,
                               QuestionID = answer.QuestionID,
                               AnswerID = answer.AnswerID,
                               Essay = answer.Essay,
                               SectionID = question.SectionID,
                               IsMultipleChoice = question.LOT_Section.IsMultipleChoice,
                               QuestionContent = question.QuestionContent,
                               ListeningTopicID = question.ListeningTopicID,
                               ParagraphID = question.ParagraphID
                           };

            return result.ToList<CandidateAnswerQuestion>();
        }
        /*Added by Tai Nguyen 24-Aug-2011*/
        public List<LOT_Question> GetQuestionListRandomBySectionProgrammingSkill(int numberOfQuestions)
        {
            try
            {
                //List<sp_GetRandomComprehensionParagraphResult> listRandom = dbContext.sp_GetRandomComprehensionParagraph(numberOfQuestions).ToList();
                List<LOT_Question> listQuestion = new List<LOT_Question>();
                for (int i = 0; i < numberOfQuestions; i++)
                {
                    var type = listQuestion.LastOrDefault();
                    int preType = 0;
                    if(type==null)
                    {
                        int numType = dbContext.LOT_ProgrammingSkillTypes.Count();
                        preType = DateTime.Now.Millisecond % numType + 1;
                    }
                    else
                        preType = type.ProgrammingTypeID.Value;
                    var list = dbContext.LOT_Questions.Where(p => p.SectionID == Constants.LOT_PROGRAMMING_SKILL_ID &&
                        !listQuestion.Contains(p) && p.LOT_ProgrammingSkillType.ID != preType).ToList();
                    listQuestion.Add(list[new Random().Next(0, list.Count()-1)]);
                }
                return listQuestion;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<LOT_ProgrammingSkillType> GetListProgrammingType()
        {
            return dbContext.LOT_ProgrammingSkillTypes.ToList();
        }
    }
}