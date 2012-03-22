using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CRM.Library.Common;
using System.Data.Common;
using System.Linq.Dynamic;

namespace CRM.Models
{
    public class TrainingAttendeeDao : BaseDao
    {
        #region Variable
        EmployeeDao emp = new EmployeeDao();
        #endregion
        
        #region New paging
        public IQueryable<Training_Attendee> GetQueryList(int classId)
        {
            var sql = from data in dbContext.Training_Attendees
                      select data;

            if (ConvertUtil.ConvertToInt(classId) != 0)
            {
                sql = sql.Where(p => p.ClassId == ConvertUtil.ConvertToInt(classId));
            }

            sql = sql.Where(p => p.DeleteFlag == false);
            sql = sql.Where(p => p.Employee.DeleteFlag == false);

            return sql;
        }

        public int GetCountListLinq(int classsId)
        {
            return GetQueryList(classsId).Count();
        }

        public List<Training_Attendee> GetListLinq(int classId,
            string sortColumn, string sortOrder, int skip, int take)
        {
            string sortSQL = string.Empty;
            var sql = GetQueryList(classId);

            switch (sortColumn)
            {
                case "ID":
                    sortSQL += "Employee.ID " + sortOrder;
                    break;

                case "Name":
                    sortSQL += "Employee.FirstName " + sortOrder + "," + "Employee.MiddleName " + sortOrder + "," + "Employee.LastName " + sortOrder;
                    break;

                case "Manager":
                    sortSQL += "Employee1.FirstName " + sortOrder + "," + "Employee1.MiddleName " + sortOrder + "," + "Employee1.LastName " + sortOrder;
                    break;

                case "JobTitle":
                    sortSQL += "Employee.JobTitle.JobTitleName" + sortOrder;
                    break;

                case "Level":
                    sortSQL += "Employee.JobTitleLevel.DisplayName " + sortOrder;
                    break;

                case "Status":
                    sortSQL += "Training_RegistrationStatus.Name " + sortOrder;
                    break;

                case "SubmitingDate":
                    sortSQL += "SubmittingDate " + sortOrder;
                    break;

                case "Priority":
                    sortSQL += "Priority " + sortOrder;
                    break;

                default:
                    sortSQL += "ID " + SortOrder.asc.ToString();
                    break;
            }

            sql = sql.OrderBy(sortSQL);

            if (skip == 0 && take == 0)
            {
                return sql.ToList();
            }

            return sql.Skip(skip).Take(take).ToList();
        }

        public List<Training_Attendee> GetListLinqJobTitle(int classId,
           string sortColumn, string sortOrder, int skip, int take)
        {
            string sortSQL = string.Empty;
            var sql = GetQueryList(classId);

            switch (sortColumn)
            {
                case "ID":
                    sortSQL += "Employee.ID " + sortOrder;
                    break;

                case "Name":
                    sortSQL += "Employee.FirstName " + sortOrder + "," + "Employee.MiddleName " + sortOrder + "," + "Employee.LastName " + sortOrder;
                    break;

                case "JobTitle":
                    sortSQL += "Employee.JobTitle.JobTitleName" + sortOrder;
                    break;

                case "Level":
                    sortSQL += "Employee.JobTitleLevel.DisplayName " + sortOrder;
                    break;

                case "Status":
                    sortSQL += "Training_RegistrationStatus.Name " + sortOrder;
                    break;

                case "SubmitingDate":
                    sortSQL += "SubmittingDate " + sortOrder;
                    break;

                case "Priority":
                    sortSQL += "Priority " + sortOrder;
                    break;
            }

            sql = sql.OrderBy(sortSQL);

            if (skip == 0 && take == 0)
            {
                return sql.ToList();
            }

            return sql.Skip(skip).Take(take).ToList();
        }
        #endregion

        public int CountAttendees(int ClassId)
        {
            return (int)dbContext.func_CountAttendess(ClassId);
        }

        public List<Training_Attendee> GetListByClassTypeAndAttendeeId(int classType, string empId)
        {
            return dbContext.Training_Attendees.Where(p => p.Training_Class.Training_Course.TypeOfCourse == classType && p.EmpId == empId && p.DeleteFlag == false).ToList();
        }

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

        public Training_Attendee GetTrainingAteendeeClassById(int classId)
        {
            return dbContext.Training_Attendees.Where(p=> !p.DeleteFlag && p.ClassId==classId).FirstOrDefault<Training_Attendee>();
        }

        public Training_Attendee GetById(int id)
        {
            return dbContext.Training_Attendees.FirstOrDefault(p => p.Id == id && p.DeleteFlag == false);
        }

        public Training_Attendee GetAttendeeByClassId(int classId)
        {
            return dbContext.Training_Attendees.FirstOrDefault(p => p.ClassId == classId);
        }

        public List<Training_Attendee> GetAttendeeApproveOrReject(int idd, int classId)
        {
            return dbContext.Training_Attendees.Where(p => p.Training_RegistrationStatus.Id == idd && p.Training_Class.ID == classId).ToList();
        }

        public int GetAttendeeByLoginName(string userId)
        {
            return dbContext.Training_Attendees.Where(p => p.EmpId == userId).ToList().Count();
        }

        public void Delete(Training_Attendee attendee)
        {
            try
            {
                var objDb = GetById(attendee.Id);
                objDb.DeleteFlag = true;
                objDb.Remark = attendee.Remark;
                objDb.Result = attendee.Result;
                objDb.Priority = attendee.Priority;
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

        /// <summary>
        /// Delete List
        /// </summary>
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
                    //split ids by char ','
                    ids = ids.TrimEnd(Constants.SEPARATE_IDS_CHAR);
                    string[] idArr = ids.Split(Constants.SEPARATE_IDS_CHAR).Distinct().ToArray();

                    int total = idArr.Count();

                    foreach (string id in idArr)
                    {
                        Training_Attendee obj = GetById(ConvertUtil.ConvertToInt(id));

                        if (obj != null)
                        {
                            obj.UpdatedBy = userName;
                            Delete(obj);
                        }
                        else
                        {
                            total--;
                        }
                    }

                    // Show succes message
                    msg = new Message(MessageConstants.I0011, MessageType.Info, (total > 1 ? total + " attendees have" : total + " attendee has") + " been deleted");
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


        public Message Insert(Training_Attendee attendee)
        {
            try
            {
                attendee.CreatedBy = attendee.UpdatedBy = HttpContext.Current.User.Identity.Name;
                attendee.CreateDate = attendee.UpdateDate = attendee.SubmittingDate= DateTime.Now;
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
                var objDb = GetById(attendee.Id);
                objDb.UpdatedBy = HttpContext.Current.User.Identity.Name;
                objDb.UpdateDate = DateTime.Now;
                objDb.EmpId = attendee.EmpId;
                objDb.ManagerId = attendee.ManagerId;
                objDb.Remark = attendee.Remark;
                objDb.Result = attendee.Result;
                objDb.Priority = attendee.Priority;
                objDb.StatusId = attendee.StatusId;
                dbContext.SubmitChanges();
                return new Message(MessageConstants.I0001, MessageType.Info, "Attendee " + emp.FullName(attendee.EmpId, Constants.FullNameFormat.FirstMiddleLast), "updated");
            }
            catch
            {
                return new Message(MessageConstants.E0007, MessageType.Error);
            }
        }

        public Message ApproveAttendee(Training_Attendee objUI)
        {
            Message msg = null;

            try
            {
                if (objUI != null)
                {
                    // Get current group in dbContext
                    Training_Attendee objDb = GetAttendeeByClassId(objUI.ClassId);

                    if (objDb != null)
                    {
                            // Update info by objUI                       
                            objDb.StatusId = Constants.TRAINING_REGISTRATION_STATUS_APPROVED;
                            objDb.UpdateDate = DateTime.Now;
                            objDb.UpdatedBy = objUI.UpdatedBy;

                            // Submit changes to dbContext
                            dbContext.SubmitChanges();

                            // Show success message
                            msg = new Message(MessageConstants.I0001, MessageType.Info, "Attendee '" + objDb.ClassId + "'", "approved");
                    }
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