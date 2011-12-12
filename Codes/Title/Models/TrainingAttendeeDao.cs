using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CRM.Library.Common;
using System.Data.Common;

namespace CRM.Models
{
    public class TrainingAttendeeDao : BaseDao
    {
        public List<sp_TC_GetListAttendeesOfClassResult> GetListAttendessOfClass(int classId)
        {
            return dbContext.sp_TC_GetListAttendeesOfClass(classId).ToList();
        }

        public Training_Attendee GetByEmpIdAndClassId(string empId, int classId, bool? deleteFlag)
        {
            return dbContext.Training_Attendees.FirstOrDefault(p=> (deleteFlag==null || p.DeleteFlag == deleteFlag) 
                && p.EmpId == empId && p.ClassId == classId);
        }

        public List<Training_Attendee> GetListByClassId(int classId)
        {
            return dbContext.Training_Attendees.Where(p => !p.DeleteFlag && p.ClassId == classId).ToList();
        }

        public void Delete(Training_Attendee attendee)
        {
            try
            {
                var objDb = GetByEmpIdAndClassId(attendee.EmpId, attendee.ClassId, null);
                objDb.DeleteFlag = true;
                objDb.Remark = attendee.Remark;
                objDb.Result = attendee.Result;
                objDb.UpdateDate = DateTime.Now;
                objDb.UpdatedBy = HttpContext.Current.User.Identity.Name;
                dbContext.SubmitChanges();
            }
            catch
            {
                return;
            }
        }
        public void Delete(List<Training_Attendee> deleteList)
        {
            DbTransaction trans = null;
            try
            {
                dbContext.Connection.Open();
                trans = dbContext.Connection.BeginTransaction();
                dbContext.Transaction = trans;
                foreach (var attendee in deleteList)
                    Delete(attendee);
                trans.Commit();
            }
            catch
            {
                if (trans != null) 
                    trans.Rollback();
            }
        }


        public Message Insert(Training_Attendee attendee)
        {
            try
            {
                attendee.CreatedBy = attendee.UpdatedBy = HttpContext.Current.User.Identity.Name;
                attendee.CreateDate = attendee.UpdateDate = DateTime.Now;
                dbContext.Training_Attendees.InsertOnSubmit(attendee);
                dbContext.SubmitChanges();
                return new Message(MessageConstants.I0001, MessageType.Info, "Attendee " + attendee.EmpId, "added");
            }
            catch
            {
                return new Message(MessageConstants.E0007, MessageType.Error);
            }
        }

        public Message Update(Training_Attendee attendee)
        {
            try
            {
                var objDb = GetByEmpIdAndClassId(attendee.EmpId, attendee.ClassId, null);
                objDb.UpdatedBy = HttpContext.Current.User.Identity.Name;
                objDb.UpdateDate = DateTime.Now;
                objDb.Remark = attendee.Remark;
                objDb.Result = attendee.Result;
                //Update delete flag to false for attendees which is deleted
                objDb.DeleteFlag = false;
                dbContext.SubmitChanges();
                return new Message(MessageConstants.I0001, MessageType.Info, "Attendee " + attendee.EmpId, "updated");
            }
            catch
            {
                return new Message(MessageConstants.E0007, MessageType.Error);
            }
        }
    }
}