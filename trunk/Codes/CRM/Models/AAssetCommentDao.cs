using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic;
using System.Web;
using CRM.Library.Common;
using System.Data.Common;
using CRM.Models.Entities;
using System.Web.Mvc;

namespace CRM.Models
{
    public class AAssetCommentDao : CustomBaseDao<A_AssetComment>
    {
        /// <summary>
        /// Get By Id
        /// </summary>
        /// <param name="groupId"></param>
        /// <returns></returns>
        public List<A_AssetComment> GetList(long assetId)
        {
            return dbContext.A_AssetComments.Where(p => (
                    (p.AssetId == assetId))).ToList<A_AssetComment>();
        }

        /// <summary>
        /// Insert
        /// </summary>
        /// <param name="CommentId"></param>
        /// <returns></returns>
        public Message Insert(A_AssetComment objUI)
        {
            Message msg = null;
            try
            {
                if (objUI != null)
                {
                    // Set more info

                    dbContext.A_AssetComments.InsertOnSubmit(objUI);
                    dbContext.SubmitChanges();

                    // Show success message
                    msg = new Message(MessageConstants.I0001, MessageType.Info, objUI.Poster, "added a comment");
                }
            }
            catch
            {
                // Show system error
                msg = new Message(MessageConstants.E0007, MessageType.Error);
            }
            return msg;
        }
    }
}