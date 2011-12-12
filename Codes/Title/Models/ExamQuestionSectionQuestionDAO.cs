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
    /// Exam Question Section Question DAO
    /// </summary>
    public class ExamQuestionSectionQuestionDAO : BaseDao
    {
        /// <summary>
        /// GetByID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public LOT_ExamQuestion_Section_Question GetByID(int id)
        {
            return dbContext.LOT_ExamQuestion_Section_Questions.Where(c => c.ID == id).FirstOrDefault<LOT_ExamQuestion_Section_Question>();    
        }

        /// <summary>
        /// Get List ByExam_Question_Section_ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<LOT_ExamQuestion_Section_Question> GetByExamQuestionSectionID(int id)
        {
            return dbContext.LOT_ExamQuestion_Section_Questions.Where(c => c.ExamQuestionSectionID == id).ToList<LOT_ExamQuestion_Section_Question>();
        }
        /// <summary>
        /// Get Question List By Exam_Question_Section_ID
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public Message AssignQuestion(List<LOT_ExamQuestion_Section_Question> list)
        {
            Message msg = null;
            try
            {
                //check valid param
                if (list.Count > 0)
                {
                    int totalID = list.Count;                    
                    dbContext.LOT_ExamQuestion_Section_Questions.InsertAllOnSubmit(list);
                    dbContext.SubmitChanges();
                    // Show succes message
                    msg = new Message(MessageConstants.I0001, MessageType.Info, totalID.ToString() + " Question(s)", "assigned");
                }
                else
                {
                    // Show system error
                    msg = new Message(MessageConstants.E0033, MessageType.Error, "Please select question(s) to assign!");
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
        /// Remove Assigned List by the selected ids
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public Message RemoveAssignList(string ids)
        {
            Message msg = null;
            try
            {
                ids = ids.TrimEnd(Constants.SEPARATE_IDS_CHAR);
                List<LOT_ExamQuestion_Section_Question> list = new List<LOT_ExamQuestion_Section_Question>();
                foreach (string strId in ids.Split(Constants.SEPARATE_IDS_CHAR))
                {
                    int id = 0;
                    bool isInterger = Int32.TryParse(strId, out id);
                    //check does user select all records to remove
                    if (isInterger)
                    {
                        LOT_ExamQuestion_Section_Question item = GetByID(id);
                        if (item != null)
                        {
                            list.Add(item);
                        }
                    }                    
                }

                //check valid 
                if (list.Count > 0)
                {
                    int totalID = list.Count;

                    dbContext.LOT_ExamQuestion_Section_Questions.DeleteAllOnSubmit(list);
                    dbContext.SubmitChanges();
                    // Show succes message
                    msg = new Message(MessageConstants.I0001, MessageType.Info, totalID.ToString() + " Question(s)", "removed");
                }
                else
                {
                    // Show system error
                    msg = new Message(MessageConstants.E0033, MessageType.Error, "Please select question(s) to remove!");
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
        /// Get list by question id
        /// </summary>
        /// <param name="questionID"></param>
        /// <returns></returns>
        public List<LOT_ExamQuestion_Section_Question> GetListByQuestionID(int questionID)
        {
            return dbContext.LOT_ExamQuestion_Section_Questions.Where(p => p.QuestionID == questionID).ToList<LOT_ExamQuestion_Section_Question>();
        }
    }
}