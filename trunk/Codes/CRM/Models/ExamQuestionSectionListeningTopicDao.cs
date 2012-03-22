using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Common;
using CRM.Library.Common;
using CRM.Models;

namespace CRM.Models
{
    public class ExamQuestionSectionListeningTopicDao : BaseDao
    {
        /// <summary>
        /// GetByID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public LOT_ExamQuestion_Section_ListeningTopic GetByID(int id)
        {
            return dbContext.LOT_ExamQuestion_Section_ListeningTopics.Where(c => c.ID == id).FirstOrDefault<LOT_ExamQuestion_Section_ListeningTopic>();
        }

        /// <summary>
        /// Get List By Exam_Question_Section_ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<LOT_ExamQuestion_Section_ListeningTopic> GetByExamQuestionSectionID(int id)
        {
            return dbContext.LOT_ExamQuestion_Section_ListeningTopics.Where(c => c.ExamQuestionSectionID == id).ToList<LOT_ExamQuestion_Section_ListeningTopic>();
        }

        public bool GetQuestionSectionListeningTopicAssignedStatus(int questionId)
        {
            int count = dbContext.LOT_ExamQuestion_Section_ListeningTopics.Where(p => p.ListeningTopicID == questionId && p.LOT_ListeningTopic.DeleteFlag == false).Count();

            if (count > 0)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Assign Question
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public Message AssignQuestion(List<LOT_ExamQuestion_Section_ListeningTopic> list)
        {
            Message msg = null;
            try
            {
                if (list.Count > 0)
                {
                    int totalID = list.Count;
                    dbContext.LOT_ExamQuestion_Section_ListeningTopics.InsertAllOnSubmit(list);
                    dbContext.SubmitChanges();
                    // Show succes message
                    msg = new Message(MessageConstants.I0001, MessageType.Info, totalID.ToString() + " Question(s)", "assigned");
                }
                else
                {
                    // Show system error
                    msg = new Message(MessageConstants.E0033, MessageType.Error, "Please select listening question(s) to assign!");
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
        /// Remove Assigned List by ids selected
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public Message RemoveAssignList(string ids)
        {
            Message msg = null;
            try
            {
                ids = ids.TrimEnd(Constants.SEPARATE_IDS_CHAR);
                List<LOT_ExamQuestion_Section_ListeningTopic> list = new List<LOT_ExamQuestion_Section_ListeningTopic>();
                foreach (string strId in ids.Split(Constants.SEPARATE_IDS_CHAR))
                {
                    int id = 0;
                    bool isInterger = Int32.TryParse(strId, out id);
                    //check does user select all records to delete
                    if (isInterger)
                    {
                        LOT_ExamQuestion_Section_ListeningTopic item = GetByID(id);
                        if (item != null)
                            list.Add(item);
                    }
                }

                if (list.Count > 0)
                {
                    int totalID = list.Count;

                    dbContext.LOT_ExamQuestion_Section_ListeningTopics.DeleteAllOnSubmit(list);
                    dbContext.SubmitChanges();
                    // Show succes message
                    msg = new Message(MessageConstants.I0001, MessageType.Info, totalID.ToString() + " Question(s)", "removed");
                }
                else
                {
                    // Show system error
                    msg = new Message(MessageConstants.E0033, MessageType.Error, "Please select listening question(s) to remove!");
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
        /// Get list by Listening topic id
        /// </summary>
        /// <param name="listeningTopicID"></param>
        /// <returns></returns>
        public List<LOT_ExamQuestion_Section_ListeningTopic> GetListByListeningTopicID(int listeningTopicID)
        {
            return dbContext.LOT_ExamQuestion_Section_ListeningTopics.Where(p => p.ListeningTopicID == listeningTopicID).ToList<LOT_ExamQuestion_Section_ListeningTopic>();
        }
    }
}