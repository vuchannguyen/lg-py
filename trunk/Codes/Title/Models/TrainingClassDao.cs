using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CRM.Library.Common;
using System.Data.Common;

namespace CRM.Models
{
    /// <summary>
    /// TrainingClassDao class
    /// </summary>
    public class TrainingClassDao : BaseDao
    {
        public TrainingClassDao()
        { 

        }

        /// <summary>
        /// Get training class by id
        /// </summary>
        /// <param name="id">id of training class</param>
        /// <returns>Training_Class</returns>
        public Training_Class GetTrainingClassById(string id, int type)
        {
            return dbContext.Training_Classes.Where(p => p.ID.Equals(id) && 
                p.Training_Course.TypeOfCourse == type && p.DeleteFlag == false).FirstOrDefault<Training_Class>();
        }



        public Message Insert(Training_Class tClass)
        {
            try
            {
                tClass.CreatedBy = tClass.UpdatedBy = HttpContext.Current.User.Identity.Name;
                tClass.CreateDate = tClass.UpdateDate = DateTime.Now;
                dbContext.Training_Classes.InsertOnSubmit(tClass);
                dbContext.SubmitChanges();
                return new Message(MessageConstants.I0001, MessageType.Info, "Class " + tClass.ClassId, "added");
            }
            catch
            {
                return new Message(MessageConstants.E0007, MessageType.Error);
            }
        }
        /// <summary>
        /// Get class by display ID (ClassId)
        /// </summary>
        /// <param name="classId"></param>
        /// <returns></returns>
        public Training_Class GetById(string classId)
        {
            if (string.IsNullOrEmpty(classId))
                classId = classId.Trim().ToLower();
            return dbContext.Training_Classes.FirstOrDefault(p => p.ClassId.Trim().ToLower() == classId && !p.DeleteFlag);
        }
        /// <summary>
        /// Get Class by real ID (ID)
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Training_Class GetById(int id)
        {
            return dbContext.Training_Classes.FirstOrDefault(p => p.ID == id && !p.DeleteFlag);
        }

        public Message Update(Training_Class tClass)
        {
            try
            {
                Training_Class objDb = GetById(tClass.ID);
                objDb.Active = tClass.Active;
                objDb.CourseId = tClass.CourseId;
                objDb.Notes = tClass.Notes;
                objDb.AttendeeQuantity = tClass.AttendeeQuantity;
                objDb.ClassId = tClass.ClassId;
                objDb.ClassTime = tClass.ClassTime;
                objDb.Instructors = tClass.Instructors;
                objDb.RegStatusId = tClass.RegStatusId;
                objDb.ResultType = tClass.ResultType;
                objDb.StartDate = tClass.StartDate;
                objDb.UpdatedBy = HttpContext.Current.User.Identity.Name;
                objDb.UpdateDate = DateTime.Now;
                dbContext.SubmitChanges();
                return new Message(MessageConstants.I0001, MessageType.Info, "Class " + tClass.ClassId, "updated");
            }
            catch
            {
                return new Message(MessageConstants.E0007, MessageType.Error);
            }
        }

        /// <summary>
        /// Delete List
        /// </summary>
        /// <param name="ids"></param>
        /// <param name="userName"></param>
        /// <returns></returns>
        public Message DeleteList(string ids, string userName)
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
                    ids = ids.TrimEnd(',');
                    string[] idArr = ids.Split(',').Distinct().ToArray();
                    int total = idArr.Count();
                   
                    foreach (string id in idArr)
                    {
                        Training_Class objClass = GetById(ConvertUtil.ConvertToInt(id));
                        
                        if (objClass != null)
                        {
                            //Check item is closed then rollback and show message
                            if (objClass.RegStatusId == Constants.TRAINING_CENTER_COURSE_STATUS_CLOSED)
                            {
                                if (trans != null) trans.Rollback();
                                msg = new Message(MessageConstants.E0050, MessageType.Error, total.ToString() + " class(es)", "");
                                return msg;
                            }

                            objClass.UpdatedBy = userName;
                            Delete(objClass);
                        }
                        else
                        {
                            total--;
                        }
                    }

                    // Show succes message
                    msg = new Message(MessageConstants.I0011, MessageType.Info, (total >1 ? total + " classes have":total + " class has") + " been deleted" );
                    trans.Commit();
                }
            }
            catch
            {
                if (trans != null) trans.Rollback();
                // Show system error
                msg = new Message(MessageConstants.E0007, MessageType.Error);
            }
            return msg;
        }

        /// <summary>
        /// Delete Service Request Setting
        /// </summary>
        /// <param name="objUI"></param>
        private void Delete(Training_Class objUI)
        {
            if (objUI != null)
            {
                // Get current group in dbContext
                Training_Class objDb = GetById(objUI.ID);
                if (objDb != null)
                {
                    // Set delete info                    
                    objDb.DeleteFlag = true;
                    objDb.UpdateDate = DateTime.Now;
                    objDb.UpdatedBy = objUI.UpdatedBy;
                    // Submit changes to dbContext
                    dbContext.SubmitChanges();

                }
            }
        }

        public Message UpdateAttendees(int classId, List<Training_Attendee> attendeelist)
        {
            Message msg = null;
            DbTransaction trans = null;
            try
            {
                dbContext.Connection.Open();
                trans = dbContext.Connection.BeginTransaction();
                dbContext.Transaction = trans;
                TrainingAttendeeDao atDao = new TrainingAttendeeDao();
                var attendeeListDb = atDao.GetListByClassId(classId);
                var deleteList = attendeeListDb.Where(p => !attendeelist.Select(q => q.EmpId).Contains(p.EmpId)).ToList();
                atDao.Delete(deleteList);
                foreach (var attendee in attendeelist)
                {
                    var objDb = atDao.GetByEmpIdAndClassId(attendee.EmpId, classId, null);
                    if (objDb == null)
                        atDao.Insert(attendee);
                    else
                        atDao.Update(attendee);
                }
                trans.Commit();
                msg = new Message(MessageConstants.I0001, MessageType.Info, "Attendees", "updated" );
            }
            catch
            {
                if (trans != null) trans.Rollback();
                // Show system error
                msg = new Message(MessageConstants.E0007, MessageType.Error);
            }
            return msg;
        }
    }
}
