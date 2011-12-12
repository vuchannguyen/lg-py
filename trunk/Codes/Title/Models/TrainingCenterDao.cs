using System.Collections.Generic;
using System.Linq;
using CRM.Library.Common;
using System.Web.UI.WebControls;

namespace CRM.Models
{
    public class TrainingCenterDao : BaseDao
    {
        public List<sp_GetClassPlanningResult> GetList(string text,int course,int type,int status,string instructor,int typeCourse)
        {
            return dbContext.sp_GetClassPlanning(text, course, type, status, instructor, typeCourse).ToList();
        }

        public List<sp_GetClassPlanningResult> Sort(List<sp_GetClassPlanningResult> list, string sortColumn, string sortOrder)
        {
            int order;

            if (sortOrder == "desc")
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
                         { return (m1.Attendess.HasValue?m1.Attendess.Value:0).CompareTo((m2.Attendess.HasValue?m2.Attendess.Value:0)) * order;});
                    break;
                case "Objectives":
                    list.Sort(
                         delegate(sp_GetClassPlanningResult m1, sp_GetClassPlanningResult m2)
                         { return (string.IsNullOrEmpty(m1.Objectives) ? "" : m1.Objectives).CompareTo((string.IsNullOrEmpty(m2.Objectives) ? "" : m2.Objectives)) * order; });
                    break;
            }

            return list;
        }

        public List<sp_TC_GetEnglishCourseAttendanceResult> CourseAttend(List<sp_TC_GetEnglishCourseAttendanceResult> list, string sortColumn, string sortOrder)
        {
            int order;

            if (sortOrder == "desc")
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
                         delegate(sp_TC_GetEnglishCourseAttendanceResult m1, sp_TC_GetEnglishCourseAttendanceResult m2)
                         { return m1.ID.CompareTo(m2.ID) * order; });
                    break;
                case "FullName":
                    list.Sort(
                         delegate(sp_TC_GetEnglishCourseAttendanceResult m1, sp_TC_GetEnglishCourseAttendanceResult m2)
                         { return m1.FullName.CompareTo(m2.FullName) * order; });
                    break;
                case "TitleName":
                    list.Sort(
                         delegate(sp_TC_GetEnglishCourseAttendanceResult m1, sp_TC_GetEnglishCourseAttendanceResult m2)
                         { return m1.TitleName.CompareTo(m2.TitleName) * order; });
                    break;
                case "DepartmentName":
                    list.Sort(
                         delegate(sp_TC_GetEnglishCourseAttendanceResult m1, sp_TC_GetEnglishCourseAttendanceResult m2)
                         { return m1.DepartmentName.CompareTo(m2.DepartmentName) * order; });
                    break;
                case "ManagerName":
                    list.Sort(
                         delegate(sp_TC_GetEnglishCourseAttendanceResult m1, sp_TC_GetEnglishCourseAttendanceResult m2)
                         { return m1.ManagerName.CompareTo(m2.ManagerName) * order; });
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
            return dbContext.Training_Courses.Where(p=>!p.DeleteFlag).ToList();
        }

        public List<ListItem> GetListIntructor()
        {
            List<string> listInstructors = dbContext.Training_Classes.Select(q => q.Instructors).Distinct().ToList<string>();
            List<ListItem> result = new List<ListItem>();
            foreach (string item in listInstructors)
            {
                string[] array = item.Trim().Split(Constants.SEPARATE_CC_LIST);
                foreach(string intructor in array)
                {
                    if(!string.IsNullOrEmpty(intructor))
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

        public List<sp_TC_GetEnglishCourseAttendanceResult> GetListEnglishCourseAttendance(string text, int title, int departmant, int manager)
        {
            return dbContext.sp_TC_GetEnglishCourseAttendance(text, departmant, title, manager).ToList();
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
                Constants.LOT_VERBAL_SKILL_ID).ToList();
        }

        public float GetEnglishLevel(int score, bool isVerbal, bool? isTOEIC)
        {
            if(isVerbal)
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
    }
}