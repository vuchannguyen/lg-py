using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using CRM.Library.Common;
using System.Data.Common;
using CRM.Models;

namespace CRM.Models
{
    /// <summary>
    /// Data Access Object of listening topic
    /// </summary>
    public class ListeningTopicDao : BaseDao
    {
        #region global variables
        private QuestionDao qDao = new QuestionDao();
        #endregion
        /// <summary>
        /// Get list of all listening topic
        /// </summary>
        /// <returns>List<LOT_ListeningTopic></returns>
        public List<LOT_ListeningTopic> GetList()
        {
            return dbContext.LOT_ListeningTopics.Where(p => p.DeleteFlag == false).ToList<LOT_ListeningTopic>();
        }

        /// <summary>
        /// Get a listening topic by its ID
        /// </summary>
        /// <param name="topicID"></param>
        /// <returns></returns>
        public LOT_ListeningTopic GetByID(int topicID)
        {
            return dbContext.LOT_ListeningTopics
                .Where(p => (p.ID == topicID)).SingleOrDefault<LOT_ListeningTopic>();
        }

        /// <summary>
        /// Get list of listening topic by question list
        /// </summary>
        /// <param name="questionList"></param>
        /// <returns> List<LOT_ListeningTopic></returns>
        public List<LOT_ListeningTopic> GetListByQuestionList(List<LOT_Question> questionList)
        {
            List<LOT_ListeningTopic> listeningTopiclist = new List<LOT_ListeningTopic>();
            foreach (LOT_Question question in questionList)
            {
                if (!listeningTopiclist.Contains(question.LOT_ListeningTopic))
                {
                    listeningTopiclist.Add(question.LOT_ListeningTopic);
                }
            }
            return listeningTopiclist;
        }

        /// <summary>
        /// Insert a listening topic without any question
        /// </summary>
        /// <param name="listeningTopic"></param>
        /// <returns>int</returns>
        public int Insert(LOT_ListeningTopic listeningTopic)
        {
            int result = 0;
            try
            {
                if (listeningTopic != null)
                {
                    listeningTopic.CreateDate = DateTime.Now;
                    listeningTopic.UpdateDate = DateTime.Now;
                    dbContext.LOT_ListeningTopics.InsertOnSubmit(listeningTopic);
                    dbContext.SubmitChanges();
                    result = listeningTopic.ID;
                }
            }
            catch
            {
                result = 0;
            }
            return result;
        }

        /// <summary>
        /// Insert a listening topic with its questions
        /// </summary>
        /// <param name="listeningTopic"></param>
        /// <param name="questionIDs"></param>
        /// <returns>Message</returns>
        public Message Insert(LOT_ListeningTopic listeningTopic, string[] questionIDs)
        {
            Message msg = null;
            DbTransaction trans = null;
            try
            {
                dbContext.Connection.Open();
                trans = dbContext.Connection.BeginTransaction();
                dbContext.Transaction = trans;
                //Insert new listening topic and get its ID
                int topicIDInserted = Insert(listeningTopic);
                string serverPath = System.Web.HttpContext.Current.Server.MapPath("~");
                //move sound file from temp folder to LOT sound folder
                File.Move(serverPath + Constants.UPLOAD_TEMP_PATH + listeningTopic.FileName,
                    serverPath + Constants.SOUND_FOLDER + listeningTopic.FileName);
                if (questionIDs != null)
                {
                    foreach (string questionID in questionIDs)
                    {
                        //Get question from db
                        LOT_Question questionDB = dbContext.LOT_Questions.Where(p => p.DeleteFlag == false &&
                            p.ID == int.Parse(questionID)).FirstOrDefault<LOT_Question>();
                        questionDB.ListeningTopicID = topicIDInserted;
                        //Submit changes to db
                        dbContext.SubmitChanges();
                    }
                }
                msg = new Message(MessageConstants.I0001, MessageType.Info, "Listening topic \"" + 
                    HttpUtility.HtmlDecode(CommonFunc.SubStringRoundWord(listeningTopic.TopicName, 
                    Constants.QUESTION_CONTENT_LENGTH_SHOWED_IN_MESSAGE))  + "\"", "added");
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
        /// Sort the listening topic list
        /// </summary>
        /// <param name="listeningTopiclist"></param>
        /// <param name="sortColumn"></param>
        /// <param name="sortOrder"></param>
        /// <returns>List<LOT_ListeningTopic></returns>
        public List<LOT_ListeningTopic> Sort(List<LOT_ListeningTopic> listeningTopiclist,
            string sortColumn, string sortOrder)
        {
            int order = 1;
            if (sortOrder == "desc")
            {
                order = -1;
            }
            switch (sortColumn)
            {
                case "TopicName":
                    listeningTopiclist.Sort(
                         delegate(LOT_ListeningTopic listeningTopic1, LOT_ListeningTopic listeningTopic2)
                         { return listeningTopic1.TopicName.CompareTo(listeningTopic2.TopicName) * order; });
                    break;
                case "ID":
                    listeningTopiclist.Sort(
                         delegate(LOT_ListeningTopic listeningTopic1, LOT_ListeningTopic listeningTopic2)
                         { return listeningTopic1.ID.CompareTo(listeningTopic2.ID) * order; });
                    break;
                case "CreatedBy":
                    listeningTopiclist.Sort(
                         delegate(LOT_ListeningTopic listeningTopic1, LOT_ListeningTopic listeningTopic2)
                         { return listeningTopic1.CreatedBy.CompareTo(listeningTopic2.CreatedBy) * order; });
                    break;
                case "UpdatedBy":
                    listeningTopiclist.Sort(
                         delegate(LOT_ListeningTopic listeningTopic1, LOT_ListeningTopic listeningTopic2)
                         { return listeningTopic1.UpdatedBy.CompareTo(listeningTopic2.UpdatedBy) * order; });
                    break;
            }
            return listeningTopiclist;
        }

        /// <summary>
        /// Check if the topic is is any exam
        /// </summary>
        /// <param name="topicID"></param>
        /// <returns>bool</returns>
        public bool IsInAnyExam(int topicID) 
        {
            bool result = false;
            List<LOT_CandidateAnswer> caList = dbContext.LOT_CandidateAnswers.
                Where(p => p.LOT_Question.ListeningTopicID == topicID).ToList<LOT_CandidateAnswer>();
            if (caList.Count > 0)
            {
                result = true;
            }
            return result;
        }


        /// <summary>
        /// Update a listening topic with its questions
        /// </summary>
        /// <param name="topicUI"></param>
        /// <param name="questionIDs"></param>
        /// <returns>Message</returns>
        public Message Update(LOT_ListeningTopic topicUI, string[] questionIDs)
        {
            Message msg = null;
            DbTransaction trans = null;
            try
            {
                dbContext.Connection.Open();
                trans = dbContext.Connection.BeginTransaction();
                dbContext.Transaction = trans;
                string serverPath = System.Web.HttpContext.Current.Server.MapPath("~");
                LOT_ListeningTopic topicDB = GetByID(topicUI.ID);
                //Another sound file was uploaded
                if (topicUI.FileName != topicDB.FileName)
                {
                    //Remove old sound file
                    File.Delete(serverPath + Constants.SOUND_FOLDER + topicDB.FileName);
                    //move sound file from temp folder to LOT sound folder
                    File.Move(serverPath + Constants.UPLOAD_TEMP_PATH + topicUI.FileName,
                        serverPath + Constants.SOUND_FOLDER + topicUI.FileName);
                }
                //Set the updated information
                topicDB.FileName = topicUI.FileName;
                topicDB.RepeatTimes = topicUI.RepeatTimes;
                topicDB.TopicName = topicUI.TopicName;
                topicDB.UpdatedBy = topicUI.UpdatedBy;
                topicDB.UpdateDate = DateTime.Now;
                //Submit changes to db
                dbContext.SubmitChanges();
                List<LOT_Question> currentQuestionsInDB = qDao.GetListByListeningTopicID(topicDB.ID);
                //Free all questions of the listening topic: set their listeing topic id to null
                foreach (LOT_Question question in currentQuestionsInDB)
                {
                    if (questionIDs == null || !questionIDs.Contains(question.ID.ToString()))
                    {
                        //Get question from db
                        LOT_Question questionDB = dbContext.LOT_Questions
                            .Where(c => c.ID == question.ID).FirstOrDefault<LOT_Question>();
                        //Set updated information
                        questionDB.UpdateDate = DateTime.Now;
                        questionDB.UpdatedBy = topicUI.UpdatedBy;
                        questionDB.ListeningTopicID = null;
                        //Submit changes to db
                        dbContext.SubmitChanges();
                    }
                }
                //Update question list of the listening topic
                if (questionIDs != null)
                {
                    foreach (string questionID in questionIDs)
                    {
                        //Get question from db
                        LOT_Question questionDB = dbContext.LOT_Questions
                            .Where(c => c.ID == int.Parse(questionID)).FirstOrDefault<LOT_Question>();
                        //Set the undated information
                        questionDB.UpdateDate = DateTime.Now;
                        questionDB.UpdatedBy = topicUI.UpdatedBy;
                        questionDB.ListeningTopicID = topicDB.ID;
                        //Submit changes to db
                        dbContext.SubmitChanges();
                    }
                }
                msg = new Message(MessageConstants.I0001, MessageType.Info, "Listening topic \"" + 
                    HttpUtility.HtmlDecode(CommonFunc.SubStringRoundWord(topicDB.TopicName, 
                    Constants.QUESTION_CONTENT_LENGTH_SHOWED_IN_MESSAGE)) + "\"", "updated");
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
    }
}