using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CRM.Library.Common;

namespace CRM.Models
{
    public class PerformanceReviewCommentDao : BaseDao
    {
        #region Public methods

        /// <summary>
        /// Get By Id
        /// </summary>
        /// <param name="groupId"></param>
        /// <returns></returns>
        public List<PRComment> GetList(string requestID)
        {
            return dbContext.PRComments.Where(p => (
                    (p.PRID == requestID))).ToList<PRComment>();
        }

        /// <summary>
        /// Insert
        /// </summary>
        /// <param name="CommentId"></param>
        /// <returns></returns>
        public Message Insert(PRComment objUI)
        {
            Message msg = null;
            try
            {
                if (objUI != null)
                {
                    // Set more info

                    dbContext.PRComments.InsertOnSubmit(objUI);
                    dbContext.SubmitChanges();

                    // Show success message
                    msg = new Message(MessageConstants.I0001, MessageType.Info, objUI.Poster,"added a comment");
                }
            }
            catch
            {
                // Show system error
                msg = new Message(MessageConstants.E0007, MessageType.Error);
            }
            return msg;
        }

        #endregion

    }
}