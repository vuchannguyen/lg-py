using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CRM.Library.Common;

namespace CRM.Models
{
    public class SRCommentDao:BaseDao
    {
        public Message Insert(SR_Comment comment)
        {
            Message msg = null;
            try
            {
                
                dbContext.SR_Comments.InsertOnSubmit(comment);                
                dbContext.SubmitChanges();
                msg = new Message(MessageConstants.I0001, MessageType.Info, "Comment", "added");
            }
            catch
            {
                msg = new Message(MessageConstants.E0007, MessageType.Error);
            }
            return msg;
        }

        public Message Update(SR_Comment comment)
        {
            Message msg = null;
            try
            {
                SR_Comment src = GetCommentByServiceRequestID(comment.ServiceRequestID);
                src.Contents += "\r\n=============================\r\n" + comment.Poster +
                    "(" + comment.PostTime.ToString(Constants.DATETIME_FORMAT_JR) + "):" + "\r\n" + comment.Contents;
                if (comment.Files != null)
                {
                    src.Contents += CommonFunc.SplitFileName(comment.Files, Constants.SR_UPLOAD_PATH, false).Replace("<a" ,"#begin#").Replace("</a>", "#end#");
                }
                dbContext.SubmitChanges();
                msg = new Message(MessageConstants.I0001, MessageType.Info, "Comment", "updated");
            }
            catch
            {
                msg = new Message(MessageConstants.E0007, MessageType.Error);
            }
            return msg;
        }

        public SR_Comment GetCommentByServiceRequestID(int id)
        {
            return dbContext.SR_Comments.Where(q => q.ServiceRequestID == id).FirstOrDefault<SR_Comment>();
        }

        public List<SR_Comment> GetList(int id)
        {
            return dbContext.SR_Comments.Where(q => q.ServiceRequestID == id).ToList();
        }

        public Message InsertList(List<SR_Comment> comment)
        {
            Message msg = null;
            try
            {
                dbContext.SR_Comments.InsertAllOnSubmit(comment);
                dbContext.SubmitChanges();
                msg = new Message(MessageConstants.I0001, MessageType.Info, "Comments", "added");
            }
            catch
            {
                msg = new Message(MessageConstants.E0007, MessageType.Error);
            }
            return msg;
        }
    }
}