using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Common;
using CRM.Library.Common;
using CRM.Models;

namespace CRM.Models
{
    public class CandidateTopicDao : BaseDao
    {
        //Added by Huy.Ly 29-Dec-2010
        /// <summary>
        /// 
        /// </summary>
        /// <param name="candidateExamID"></param>
        /// <param name="topicID"></param>
        /// <returns></returns>
        public LOT_Candidate_Topic GetItem(long candidateExamID, int topicID)
        {
            return dbContext.LOT_Candidate_Topics.Where(p => (p.CandidateExamID == candidateExamID && p.TopicID == topicID)).FirstOrDefault<LOT_Candidate_Topic>();
        }

        /// <summary>
        /// Insert a List into table LOT_Candidate_Topic
        /// </summary>
        /// <param name="list">List of LOT_Candidate_Topic object</param>
        /// <returns>Message</returns>
        public Message InsertList(List<LOT_Candidate_Topic> list)
        {
            Message msg = null;
            try
            {
                if (list.Count > 0)
                {
                    dbContext.LOT_Candidate_Topics.InsertAllOnSubmit(list);
                    dbContext.SubmitChanges();

                    // Show success message
                    msg = new Message(MessageConstants.I0001, MessageType.Info, "Candidate Topic", "added");
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
                msg = new Message(MessageConstants.I0001, MessageType.Info, "Candidate Topic", "updated");
            }
            catch (Exception ex)
            {
                msg = new Message(MessageConstants.E0007, MessageType.Error);
                throw ex;
            }
            return msg;
        }        
    }
}