using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CRM.Library.Common;

namespace CRM.Models
{
    public class SRActivityDao: BaseDao
    {
        public Message Insert(SR_Activity srActivity)
        {
            try
            {
                srActivity.CreateDate = srActivity.UpdateDate = DateTime.Now;
                srActivity.CreatedBy = srActivity.UpdatedBy = HttpContext.Current.User.Identity.Name;
                dbContext.SR_Activities.InsertOnSubmit(srActivity);
                dbContext.SubmitChanges();
                return new Message(MessageConstants.I0001, MessageType.Info, "Activity", "added");
            }
            catch
            {
                return new Message(MessageConstants.E0007, MessageType.Info);
            }
        }
        public SR_Activity GetById(int id)
        {
            return dbContext.SR_Activities.Where(p=>p.ID == id && !p.DeleteFlag).FirstOrDefault();
        }

        public Message Delete(int id)
        {
            try
            {
                SR_Activity srActivity = GetById(id);
                srActivity.DeleteFlag = true;
                srActivity.UpdateDate = DateTime.Now;
                srActivity.UpdatedBy = HttpContext.Current.User.Identity.Name;
                dbContext.SubmitChanges();
                return new Message(MessageConstants.I0001, MessageType.Info, "Activity", "deleted");
            }
            catch
            {
                return new Message(MessageConstants.E0007, MessageType.Info);
            }
        }
        /// <summary>
        /// Get list by Service Request Id
        /// </summary>
        /// <param name="srId"></param>
        /// <returns></returns>
        public List<SR_Activity> GetList(int srId)
        {
            return dbContext.SR_Activities.Where(p=>!p.DeleteFlag && p.ServiceRequestID == srId).ToList();
        }
    }
}