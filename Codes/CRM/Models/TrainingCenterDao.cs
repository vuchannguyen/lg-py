using System;
using System.Collections.Generic;
using System.Linq;
using CRM.Library.Common;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;
using CRM.Library.Utils;
using System.Data.Linq.SqlClient;
using System.Web;
using System.Data.Common;
using System.Linq.Dynamic;
using CRM.Models.Entities;

namespace CRM.Models
{
    public class TrainingCenterDao : BaseDao
    {
        #region New paging
        public IQueryable<Training_Class> GetQueryList(string text, int course, int type, int status, string instructor, int typeCourse)
        {
            var sql = from data in dbContext.Training_Classes
                      select data;

            if (!string.IsNullOrEmpty(text))
            {
                text = CommonFunc.GetFilterText(text);
                sql = sql.Where(p => SqlMethods.Like(p.ClassId, text) || SqlMethods.Like(p.Training_Course.Name, text));
            }

            if (ConvertUtil.ConvertToInt(course) != 0)
            {
                sql = sql.Where(p => p.CourseId == ConvertUtil.ConvertToInt(course));
            }

            if (ConvertUtil.ConvertToInt(type) != 0)
            {
                sql = sql.Where(p => p.Training_Course.TypeId == ConvertUtil.ConvertToInt(type));
            }

            if (ConvertUtil.ConvertToInt(status) != 0)
            {
                sql = sql.Where(p => p.RegStatusId == ConvertUtil.ConvertToInt(status));
            }

            if (!string.IsNullOrEmpty(instructor))
            {
                instructor = CommonFunc.GetFilterText(instructor);
                sql = sql.Where(p => SqlMethods.Like(p.Instructors, instructor));
            }

            if (ConvertUtil.ConvertToInt(typeCourse) != 0)
            {
                sql = sql.Where(p => p.Training_Course.TypeOfCourse == ConvertUtil.ConvertToInt(typeCourse));
            }

            sql = sql.Where(p => p.DeleteFlag == false);
            sql = sql.Where(p => p.Active == true);

            return sql;
        }

        public int GetCountListLinq(string text, int course, int type, int status, string instructor, int typeCourse)
        {
            return GetQueryList(text, course, type, status, instructor, typeCourse).Count();
        }

        public List<Training_Class> GetListLinq(string text, int course, int type, int status, string instructor, int typeCourse,
            string sortColumn, string sortOrder, int skip, int take)
        {
            string sortSQL = string.Empty;
            var sql = GetQueryList(text, course, type, status, instructor, typeCourse);

            switch (sortColumn)
            {
                case "ID":
                    sortSQL += "ID " + sortOrder;
                    break;

                case "ClassID":
                    sortSQL += "ClassId " + sortOrder;
                    break;

                case "Course":
                    sortSQL += "Training_Course.Name " + sortOrder;
                    break;

                case "Type":
                    sortSQL += "Training_Course.Training_SkillType.Name " + sortOrder;
                    break;

                case "Status":
                    sortSQL += "Training_RegStatus.Name " + sortOrder;
                    break;

                case "ClassTime":
                    sortSQL += "ClassTime " + sortOrder;
                    break;

                case "Duration":
                    sortSQL += "Training_Course.Duration " + sortOrder;
                    break;

                case "StartDate":
                    sortSQL += "StartDate " + sortOrder;
                    break;

                case "Instructors":
                    sortSQL += "Instructors " + sortOrder;
                    break;

                case "#ofAttendees":
                    if (sortOrder == SortOrder.asc.ToString())
                    {
                        sql.OrderBy(p => dbContext.func_CountAttendess(p.ID));
                    }
                    else
                    {
                        sql = sql.OrderByDescending(p => dbContext.func_CountAttendess(p.ID));
                    }

                    sortSQL += "ID " + sortOrder;
                    break;

                case "Objectives":
                    sortSQL += "Training_Course.Objectives " + sortOrder;
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
        #endregion

        public List<sp_GetClassPlanningResult> GetList(string text, int course, int type, int status, string instructor, int typeCourse)
        {
            return dbContext.sp_GetClassPlanning(text, course, type, status, instructor, typeCourse).ToList();
        }

        public List<sp_GetClassPlanningResult> Sort(List<sp_GetClassPlanningResult> list, string sortColumn, string sortOrder)
        {
            int order;

            if (sortOrder == "asc")
            {
                order = -1;
            }
            else
            {
                order = 1;
            }
            switch (sortColumn)
            {
                case "ID":
                    list.Sort(
                         delegate(sp_GetClassPlanningResult m1, sp_GetClassPlanningResult m2)
                         { return m1.ID.CompareTo(m2.ID) * order; });
                    break;
                case "ClassID":
                    list.Sort(
                         delegate(sp_GetClassPlanningResult m1, sp_GetClassPlanningResult m2)
                         { return m1.ClassID.CompareTo(m2.ClassID) * order; });
                    break;
                case "Course":
                    list.Sort(
                         delegate(sp_GetClassPlanningResult m1, sp_GetClassPlanningResult m2)
                         { return m1.CourseName.CompareTo(m2.CourseName) * order; });
                    break;
                case "Type":
                    list.Sort(
                         delegate(sp_GetClassPlanningResult m1, sp_GetClassPlanningResult m2)
                         { return (string.IsNullOrEmpty(m1.SkillTypeName) ? "" : m1.SkillTypeName).CompareTo((string.IsNullOrEmpty(m2.SkillTypeName) ? "" : m2.SkillTypeName)) * order; });
                    break;
                case "Status":
                    list.Sort(
                         delegate(sp_GetClassPlanningResult m1, sp_GetClassPlanningResult m2)
                         { return m1.StatusName.CompareTo(m2.StatusName) * order; });
                    break;
                case "ClassTime":
                    list.Sort(
                         delegate(sp_GetClassPlanningResult m1, sp_GetClassPlanningResult m2)
                         { return (string.IsNullOrEmpty(m1.ClassTime) ? "" : m1.ClassTime).CompareTo((string.IsNullOrEmpty(m2.ClassTime) ? "" : m2.ClassTime)) * order; });
                    break;
                case "Duration":
                    list.Sort(
                         delegate(sp_GetClassPlanningResult m1, sp_GetClassPlanningResult m2)
                         { return (m1.Duration.HasValue ? m1.Duration.Value : 0).CompareTo((m2.Duration.HasValue ? m2.Duration.Value : 0)) * order; });
                    break;
                case "StartDate":
                    list.Sort(
                         delegate(sp_GetClassPlanningResult m1, sp_GetClassPlanningResult m2)
                         { return m1.StartDate.CompareTo(m2.StartDate) * order; });
                    break;
                case "Instructors":
                    list.Sort(
                         delegate(sp_GetClassPlanningResult m1, sp_GetClassPlanningResult m2)
                         { return m1.Instructors.CompareTo(m2.Instructors) * order; });
                    break;
                case "Attendess":
                    list.Sort(
                         delegate(sp_GetClassPlanningResult m1, sp_GetClassPlanningResult m2)
                         { return (m1.Attendess.HasValue ? m1.Attendess.Value : 0).CompareTo((m2.Attendess.HasValue ? m2.Attendess.Value : 0)) * order; });
                    break;
                case "Objectives":
                    list.Sort(
                         delegate(sp_GetClassPlanningResult m1, sp_GetClassPlanningResult m2)
                         { return (string.IsNullOrEmpty(m1.Objectives) ? "" : m1.Objectives).CompareTo((string.IsNullOrEmpty(m2.Objectives) ? "" : m2.Objectives)) * order; });
                    break;
            }
            return list;
        }

        

        public List<Training_Status> GetListCourseStatus()
        {
            return dbContext.Training_Status.ToList();
        }

        public List<Training_RegStatus> GetListRegStatus()
        {
            return dbContext.Training_RegStatus.ToList();
        }

        public List<Training_Course> GetListCourse()
        {
            return dbContext.Training_Courses.Where(p => !p.DeleteFlag).ToList();
        }

        public List<ListItem> GetListIntructor()
        {
            List<string> listInstructors = dbContext.Training_Classes.Select(q => q.Instructors).Distinct().ToList<string>();
            List<ListItem> result = new List<ListItem>();
            foreach (string item in listInstructors)
            {
                string[] array = item.Trim().Split(Constants.SEPARATE_CC_LIST);
                foreach (string intructor in array)
                {
                    if (!string.IsNullOrEmpty(intructor))
                    {
                        result.Add(new ListItem(intructor, intructor));
                    }
                }

            }
            return result.Distinct().ToList();
        }

        public List<sp_TC_GetClassEmpAttendResult> GetListClassEmployeeAttend(int type, int empId, int skillType = 0)
        {
            return dbContext.sp_TC_GetClassEmpAttend(type, empId, skillType).ToList();

        }

        public List<sp_TC_GetClassEmpNotAttendResult> GetListClassEmployeeNotAttend(int type, int empId, int skillType = 0)
        {
            return dbContext.sp_TC_GetClassEmpNotAttend(type, empId, skillType).ToList();
        }

        public List<sp_TC_GetCourseEmpAttendResult> GetCourseEmpAttend(int type, string empId)
        {
            return dbContext.sp_TC_GetCourseEmpAttend(type, empId).ToList();
        }
        public string GetTrainingResult(int courseKeyId, string empId)
        {
            //var course = dbContext.Training_Courses.FirstOrDefault(p=>p.ID == courseKeyId && !p.DeleteFlag);
            return dbContext.GetTrainingResultOfCourse(empId, courseKeyId);
        }

        public List<sp_GetListEnglishExamOfEmployeeResult> GetListExam(string empId)
        {
            return dbContext.sp_GetListEnglishExamOfEmployee(empId,
                Constants.LOT_MULTIPLE_CHOICE_QUESTION,
                Constants.LOT_SENTENCE_CORRECTION_QUESTION,
                Constants.LOT_COMPREHENSION_SKILL_ID,
                Constants.LOT_WRITING_SKILL_ID,
                Constants.LOT_LISTENING_TOPIC_ID,
                Constants.LOT_SECTION_VERBAL_TOEIC_ID,
                Constants.LOT_SECTION_VERBAL_LEVEL_ID
                ).ToList();
        }


        public int GetMaxEmpEnglishTest(List<EmployeeEntity> empList)
        {
            int max = 0;
            foreach (EmployeeEntity emp in empList)
            {
                int count = this.GetListEmployeeEnglishTestTracking(emp.Id, 1).Count;
                if (count > max)
                    max = count;
            }
            return max;
        }

        public float GetEnglishLevel(int score, bool isVerbal, bool? isTOEIC)
        {
            if (isVerbal)
                return (float)dbContext.GetVerbalSkillLevel(score);
            else
                return (float)dbContext.GetEnglishSkillLevel(score, isTOEIC);
        }
        public Training_SkillType GetTrainingSkillByID(int id)
        {
            return dbContext.Training_SkillTypes.Where(t => t.ID == id).FirstOrDefault();
        }
        public Training_Status GetTrainingStatusByID(int id)
        {
            return dbContext.Training_Status.Where(t => t.ID == id).FirstOrDefault();
        }

        public List<Training_RegistrationStatus> GetListRegistrationStatus()
        {
            return dbContext.Training_RegistrationStatus.Where(p => p.Id == Constants.TRAINING_REGISTRATION_STATUS_APPROVED || p.Id == Constants.TRAINING_REGISTRATION_STATUS_REJECTED).ToList();
        }

        public List<Training_RegistrationStatus> GetListConfirmAndRejectRegistrationStatus()
        {
            return dbContext.Training_RegistrationStatus.Where(p => p.Id == Constants.TRAINING_REGISTRATION_STATUS_CONFIRMED || p.Id == Constants.TRAINING_REGISTRATION_STATUS_REJECTED).ToList();
        }

        //Professional class detail comment(s) and English class detail comment(s)
        public Message Insert(Training_ClassComment objUI)
        {
            Message msg = null;
            try
            {
                if (objUI != null)
                {
                    // Set more info

                    dbContext.Training_ClassComments.InsertOnSubmit(objUI);
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

        //Get list for Professional class detail comment(s) and English class detail comment(s)
        public List<Training_ClassComment> GetList(int requestID)
        {
            return dbContext.Training_ClassComments.Where(p => (
                    (p.ClassId == requestID))).ToList<Training_ClassComment>();
        }

        // Hung.bui 10-01-2012
        public IQueryable<Employee> GetQueryListEnglishCourseAttendees(string text, int titleId, int departmentId, int managerId)
        {
            if (text.Equals(Constants.TRAINING_EEI_TXT_KEYWORD_LABEL))
                text = string.Empty;

            var sql = from e in dbContext.Employees
                      select e;
            text = text.Trim();
            text = CommonFunc.GetFilterText(text);

            if (text != string.Empty)
                sql = sql.Where(e => SqlMethods.Like((e.FirstName + (e.MiddleName != null ? "%" + e.MiddleName + "%" : "%") + e.LastName), text)
                    || SqlMethods.Like(e.ID, text)
                    || SqlMethods.Like(e.OfficeEmail, "%" + text + "%@%"));
            if (titleId > 0)
                sql = sql.Where(e => e.TitleId == ConvertUtil.ConvertToInt(titleId));
            if (managerId > 0)
                sql = sql.Where(e => e.ManagerId == managerId.ToString());
            sql = sql.Where(e => e.DeleteFlag == false);

            if (departmentId > 0)
            {
                DepartmentDao depDao = new DepartmentDao();
                List<sp_GetDepartmentRootResult> subDepartmentList = dbContext.sp_GetDepartmentRoot(departmentId).ToList();
                if (subDepartmentList.Count > 0)
                {
                    List<int> subDepartmentIds = new List<int>();
                    foreach (sp_GetDepartmentRootResult d in subDepartmentList)
                        subDepartmentIds.Add(d.DepartmentId);

                    sql = sql.Where(e => subDepartmentIds.Contains(e.DepartmentId));
                }
                else
                {
                    sql = sql.Where(e => e.DepartmentId == departmentId);
                }
            }

            return sql;
        }

        public int GetCountListEnglishCourseAttendees(string text, int titleId, int departmentId, int managerId)
        {
            var sql = GetQueryListEnglishCourseAttendees(text, titleId, departmentId, managerId);
            return sql.Count();
        }

        public IQueryable<Employee> SetSortEnglishCourseAttendees(IQueryable<Employee> sql, string sortColumn, string sortOrder)
        {
            switch (sortColumn)
            {
                case "FullName":
                    if (sortOrder == "asc")
                        sql = sql.OrderBy(e => e.FirstName).ThenBy(e => e.MiddleName).ThenBy(e => e.LastName);
                    else
                        sql = sql.OrderByDescending(e => e.FirstName).ThenByDescending(e => e.MiddleName).ThenByDescending(e => e.LastName);
                    break;
                case "TitleName":
                    if (sortOrder == "asc")
                        sql = sql.OrderBy(e => e.JobTitleLevel.DisplayName);
                    else
                        sql = sql.OrderByDescending(e => e.JobTitleLevel.DisplayName);
                    break;
                case "DepartmentName":
                    if (sortOrder == "asc")
                        sql = sql.OrderBy(e => e.Department.DepartmentName);
                    else
                        sql = sql.OrderByDescending(e => e.Department.DepartmentName);
                    break;
                case "ManagerName":
                    if (sortOrder == "asc")
                        sql = sql.OrderBy(e => e.Employee1.FirstName).ThenBy(e => e.Employee1.MiddleName).ThenBy(e => e.Employee1.LastName);
                    else
                        sql = sql.OrderByDescending(e => e.Employee1.FirstName).ThenByDescending(e => e.Employee1.MiddleName).ThenByDescending(e => e.Employee1.LastName);
                    break;
                default:
                    if (sortOrder == "asc")
                        sql = sql.OrderBy(e => e.ID);
                    else
                        sql = sql.OrderByDescending(e => e.ID);
                    break;
            }
            return sql;
        }

        public List<Employee> GetListEnglishCourseAttendeesLinq(string text, int titleId, int departmentId, int managerId, string sortColumn, string sortOrder, int skip, int take)
        {
            var sql = GetQueryListEnglishCourseAttendees(text, titleId, departmentId, managerId);
            sql = this.SetSortEnglishCourseAttendees(sql, sortColumn, sortOrder);
            List<Employee> resultList = sql.Skip(skip).Take(take).ToList();
            return resultList;
        }
        // end Hung.Bui 10-01-2012
        #region Linh Le: New English Test Tracking paging

        public List<sp_TC_GetEnglishTestTrackingResult> GetListEnglishTestTracking(string text, int department, int subDepartment, string testTitle, string fromDate, string toDate)
        {
            return dbContext.sp_TC_GetEnglishTestTracking(text, department, subDepartment, testTitle, fromDate, toDate, Constants.RESIGNED).ToList<sp_TC_GetEnglishTestTrackingResult>();
        }

        public IQueryable<EmployeeEntity> GetEnglishTestTrackingQueryList(string searchText, int departmentId, string fromDate, string toDate)
        {
            var sql = from emp in dbContext.Employees
                      where emp.EmpStatusId != Constants.RESIGNED
                      select new EmployeeEntity() 
                        { 
                            Id = emp.ID,
                            FullName = dbContext.GetEmployeeFullName(emp.ID,1),
                            DepartmentId = emp.DepartmentId,
                            DepartmentName = emp.Department.DepartmentName
                        };

            if (!string.IsNullOrEmpty(searchText) || !string.IsNullOrEmpty(fromDate) || !string.IsNullOrEmpty(toDate))
            {
                DateTime iFromDate = !string.IsNullOrEmpty(fromDate) ? ConvertUtil.ConvertToDatetime(fromDate) : ConvertUtil.ConvertToDatetime("1753-01-01");
                DateTime iToDate = !string.IsNullOrEmpty(toDate) ? ConvertUtil.ConvertToDatetime(toDate) : DateTime.MaxValue;
                sql = sql.Where(a => dbContext.CheckEmpEnglishTest(a.Id, searchText, iFromDate, iToDate).Value == true);
            }

            if (departmentId > 0)
            {
                var listDept = dbContext.sp_GetDepartmentRoot(departmentId).ToList();

                List<int> listSubDept = new List<int>();
                foreach (sp_GetDepartmentRootResult dept in listDept)
                {
                    listSubDept.Add(dept.DepartmentId);
                }
                sql = sql.Where(a => listSubDept.Contains(a.DepartmentId == null ? 0 : (int)a.DepartmentId));
            }

            return sql;
        }

        public int GetListEnglishTestTrackingCountList(string searchText, int departmentId, string fromDate, string toDate)
        {
            return GetEnglishTestTrackingQueryList(searchText, departmentId, fromDate, toDate).Count();
        }

        public List<Entities.EmployeeEnglishTestTrackingEntity> GetListEmployeeEnglishTestTracking(string employeeId, int type)
        {
            if (type == 1)
            {
                var sql = from exam in dbContext.LOT_Exams
                          join canExam in dbContext.LOT_Candidate_Exams on exam.ID equals canExam.ExamID
                          where canExam.EmployeeID == employeeId
                          select new Entities.EmployeeEnglishTestTrackingEntity()
                          {
                              Title = exam.Title,
                              ExamDate = exam.ExamDate,
                              Score = dbContext.GetEnglishSkillScore(canExam.ID),
                              Level = dbContext.GetEnglishSkillLevel(dbContext.GetEnglishSkillScore(canExam.ID), false),
                              Remark = dbContext.LOT_Candidate_Exam_Results.Where(p => p.CandidateExamId == canExam.ID
                                            && p.SectionId == Constants.LOT_WRITING_SKILL_ID).Select(p=>p.Comment).FirstOrDefault()
                              
                          };
                return sql.OrderByDescending(e => e.ExamDate).ToList<Entities.EmployeeEnglishTestTrackingEntity>();
            }
            else
            {
                var sql = from exam in dbContext.LOT_Exams
                          join canExam in dbContext.LOT_Candidate_Exams on exam.ID equals canExam.ExamID
                          where canExam.EmployeeID == employeeId
                          select new Entities.EmployeeEnglishTestTrackingEntity()
                          {
                              Title = exam.Title,
                              ExamDate = exam.ExamDate,
                              Score = dbContext.LOT_Candidate_Exam_Results.Where(p => p.CandidateExamId == canExam.ID &&
                                                                   p.LOT_Section.SectionTypeId == Constants.LOT_VERBAL_SECTION_TYPE_ID &&
                                                                   p.SectionId == Constants.LOT_SECTION_VERBAL_TOEIC_ID).Select(p=>p.Mark).FirstOrDefault(),
                              Level = dbContext.LOT_Candidate_Exam_Results.Where(p => p.CandidateExamId == canExam.ID &&
                                                                   p.LOT_Section.SectionTypeId == Constants.LOT_VERBAL_SECTION_TYPE_ID &&
                                                                   p.SectionId == Constants.LOT_SECTION_VERBAL_LEVEL_ID).Select(p => p.Mark).FirstOrDefault() != null ?
                                      dbContext.LOT_Candidate_Exam_Results.Where(p => p.CandidateExamId == canExam.ID &&
                                                                   p.LOT_Section.SectionTypeId == Constants.LOT_VERBAL_SECTION_TYPE_ID &&
                                                                   p.SectionId == Constants.LOT_SECTION_VERBAL_LEVEL_ID).Select(p => p.Mark).FirstOrDefault() :
                                      (dbContext.LOT_Candidate_Exam_Results.Where(p => p.CandidateExamId == canExam.ID &&
                                                                   p.LOT_Section.SectionTypeId == Constants.LOT_VERBAL_SECTION_TYPE_ID &&
                                                                   p.SectionId == Constants.LOT_SECTION_VERBAL_TOEIC_ID).Select(p=>p.Mark).FirstOrDefault() != null ?
                                      dbContext.GetVerbalSkillLevel(dbContext.LOT_Candidate_Exam_Results.Where(p => p.CandidateExamId == canExam.ID &&
                                                                   p.LOT_Section.SectionTypeId == Constants.LOT_VERBAL_SECTION_TYPE_ID &&
                                                                   p.SectionId == Constants.LOT_SECTION_VERBAL_TOEIC_ID).Select(p => p.Mark).FirstOrDefault()) : null),
                              Remark = dbContext.LOT_Candidate_Exam_Results.Where(p => p.CandidateExamId == canExam.ID &&
                                                                   p.LOT_Section.SectionTypeId == Constants.LOT_VERBAL_SECTION_TYPE_ID &&
                                                                   p.SectionId != Constants.LOT_VERBAL_SKILL_ID).Select(p => p.Comment).FirstOrDefault()
                          };

                return sql.OrderByDescending(e => e.ExamDate).ToList<Entities.EmployeeEnglishTestTrackingEntity>();
            }
        }

        public List<EmployeeEntity> GetListEnglishTestTracking(string searchText, int departmentId, string fromDate, string toDate, string sortColumn, string sortOrder, int currentPage, int rowCount)
        {
            var sql = GetEnglishTestTrackingQueryList(searchText, departmentId, fromDate, toDate);

            sql = sql.OrderBy(sortColumn + " " + sortOrder);
            return sql.Skip((currentPage - 1) * rowCount).Take(rowCount).ToList<EmployeeEntity>();
        }
       
        #endregion

        public Message ClassRegister(Training_Attendee objUI, int statusId)
        {
            Message msg = null;
            try
            {
                if (!isDublicateEmpId(objUI))
                {
                    objUI.CreateDate = DateTime.Now;
                    objUI.UpdateDate = DateTime.Now;
                    objUI.DeleteFlag = false;
                    objUI.StatusId = statusId;
                    objUI.SubmittingDate = DateTime.Now;
                    dbContext.Training_Attendees.InsertOnSubmit(objUI);
                    dbContext.SubmitChanges();
                    // Show success message
                    msg = new Message(MessageConstants.I0001, MessageType.Info, "Registration for this class ", "added");
                }
                else
                {
                    msg = new Message(MessageConstants.E0020, MessageType.Error, "Your name is ", "in this class");
                }

            }
            catch (Exception ex)
            {
                msg = new Message(MessageConstants.E0007, MessageType.Error);
                throw ex;
            }
            return msg;

        }

        public Message ProClassRegister(Training_Attendee objUI)
       {
            Message msg = null;
            try
            {
                if (!isDublicateEmpId(objUI))
                {
                    objUI.CreateDate = DateTime.Now;
                    objUI.UpdateDate = DateTime.Now;
                    objUI.DeleteFlag = false;
                    objUI.StatusId = Constants.TRAINING_REGISTRATION_STATUS_NEW;
                    objUI.SubmittingDate = DateTime.Now;
                    dbContext.Training_Attendees.InsertOnSubmit(objUI);
                    dbContext.SubmitChanges();
                    // Show success message
                    msg = new Message(MessageConstants.I0001, MessageType.Info, "Registration for this class ", "added");
                }
                else
                {
                    msg = new Message(MessageConstants.E0020, MessageType.Error, "Your name is ", "in this class");
                }
                
            }
            catch (Exception ex)
            {
                msg = new Message(MessageConstants.E0007, MessageType.Error);
                throw ex;
            }
            return msg;

        }

        #region Register a ProClass Or EngClass

        public Message EngClassRegister(Training_Attendee objUI)
        {
            Message msg = null;
            try
            {
                if (!isDublicateEmpId(objUI))
                {
                    objUI.CreateDate = DateTime.Now;
                    objUI.UpdateDate = DateTime.Now;
                    objUI.DeleteFlag = false;
                    objUI.StatusId = Constants.TRAINING_REGISTRATION_STATUS_NEW;
                    objUI.SubmittingDate = DateTime.Now;
                    dbContext.Training_Attendees.InsertOnSubmit(objUI);
                    dbContext.SubmitChanges();
                    // Show success message
                    msg = new Message(MessageConstants.I0001, MessageType.Info, "Registration for this class ", "added");
                }
                else
                {
                    msg = new Message(MessageConstants.E0020, MessageType.Error, "Your name is ", "in this class");
                }

            }
            catch (Exception ex)
            {
                msg = new Message(MessageConstants.E0007, MessageType.Error);
                throw ex;
            }
            return msg;

        }

        private bool isDublicateEmpId(Training_Attendee objUI)
        {
            bool isDublicateName = true;
            Training_Attendee dublicateName = dbContext.Training_Attendees.Where(a => a.ClassId.Equals(objUI.ClassId) && a.DeleteFlag == false && a.EmpId == objUI.EmpId).FirstOrDefault<Training_Attendee>();
            if (dublicateName == null || dublicateName.EmpId != objUI.EmpId)
            {
                isDublicateName = false;
            }
            return isDublicateName;
        }

        #endregion

        //public void SetSendMailToAttendee(string id, string userUpdate)
        //{
        //    if (!string.IsNullOrEmpty(id))
        //    {
        //        Training_Attendee objDb = GetById(id);
        //        if (objDb != null)
        //        {
        //            new InterviewLogDao().WriteUpdateLogForSendMailToCandidate(id, userUpdate, ELogAction.Update);
        //            objDb.IsSentMailCandidate = true;
        //            dbContext.SubmitChanges();
        //        }
        //    }
        //}

        //public Training_Attendee GetById(string id)
        //{
        //    if (!string.IsNullOrEmpty(id))
        //        return dbContext.Interviews.Where(c => c.CandidateId.Equals(id)).FirstOrDefault<Training_Attendee>();
        //    else
        //        return null;
        //}

    }
}