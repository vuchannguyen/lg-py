using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CRM.Library.Common;
using System.Data.Common;

namespace CRM.Models
{
    public class TrainingCourseDao: BaseDao
    {
        public Training_Course GetById(string courseId)
        {
            if (string.IsNullOrEmpty(courseId))
                courseId = courseId.Trim().ToLower();
            return dbContext.Training_Courses.FirstOrDefault(p => p.CourseId.Trim().ToLower() == courseId && !p.DeleteFlag);
        }
        public Training_Course GetById(int id)
        {
            return dbContext.Training_Courses.FirstOrDefault(p => p.ID == id && !p.DeleteFlag);
        }
        public Message Insert(Training_Course course)
        {
            try
            {
                course.CreatedBy = course.UpdatedBy = HttpContext.Current.User.Identity.Name;
                course.CreateDate = course.UpdateDate = DateTime.Now;
                dbContext.Training_Courses.InsertOnSubmit(course);
                dbContext.SubmitChanges();
                return new Message(MessageConstants.I0001, MessageType.Info, "Course " + course.CourseId, "added");
            }
            catch
            {
                return new Message(MessageConstants.E0007, MessageType.Error);
            }
        }

        public List<Training_Course> GetList(int typeOfCourse=0)
        {
            List<Training_Course> list = new List<Training_Course>();
            if(typeOfCourse==0)
                list = dbContext.Training_Courses.Where(q => q.Active && !q.DeleteFlag).ToList();
            else 
                list = dbContext.Training_Courses.Where(q => q.Active && !q.DeleteFlag && q.TypeOfCourse==typeOfCourse).ToList();
            return list;
        }

        public Message Update(Training_Course course)
        {
            try
            {
                Training_Course objDb = GetById(course.ID);
                objDb.Active = course.Active;
                objDb.CourseId = course.CourseId;
                objDb.Duration = course.Duration;
                objDb.KeyTrainers = course.KeyTrainers;
                objDb.Name = course.Name;
                objDb.Notes = course.Notes;
                objDb.Objectives = course.Objectives;
                objDb.Overview = course.Overview;
                objDb.Requirements = course.Requirements;
                objDb.TypeId = course.TypeId;
                objDb.StatusId = course.StatusId;
                objDb.UpdatedBy = HttpContext.Current.User.Identity.Name;
                objDb.UpdateDate = DateTime.Now;
                dbContext.SubmitChanges();
                return new Message(MessageConstants.I0001, MessageType.Info, "Course " + course.CourseId, "updated");
            }
            catch
            {
                return new Message(MessageConstants.E0007, MessageType.Error);
            }
        }
        public List<sp_GetTrainingCoursesResult> GetList(string name, int? typeOfCourse, int? typeId)
        {
            return dbContext.sp_GetTrainingCourses(name, typeOfCourse, typeId).ToList();
        }

        public List<sp_GetTrainingCoursesResult> Sort(List<sp_GetTrainingCoursesResult> list, string sortColumn, string sortOrder)
        {
            int order = 1;
            if (sortOrder == "desc")
            {
                order = -1;
            }
            switch (sortColumn)
            {
                case "CourseName":
                    list.Sort(
                         delegate(sp_GetTrainingCoursesResult m1, sp_GetTrainingCoursesResult m2)
                         { return m1.Name.CompareTo(m2.Name) * order; });
                    break;
                case "CourseId":
                    list.Sort(
                         delegate(sp_GetTrainingCoursesResult m1, sp_GetTrainingCoursesResult m2)
                         {return m1.CourseId.CompareTo(m2.CourseId) * order;});
                    break;
                case "TypeName":
                    list.Sort(
                         delegate(sp_GetTrainingCoursesResult m1, sp_GetTrainingCoursesResult m2)
                         {
                             string s1 = m1.TypeName ?? "";
                             string s2 = m2.TypeName ?? "";
                             return s1.CompareTo(s2) * order; 
                         });
                    break;
                case "StatusName":
                    list.Sort(
                         delegate(sp_GetTrainingCoursesResult m1, sp_GetTrainingCoursesResult m2)
                         { return m1.StatusName.CompareTo(m2.StatusName) * order; });
                    break;
                case "Duration":
                    list.Sort(
                         delegate(sp_GetTrainingCoursesResult m1, sp_GetTrainingCoursesResult m2)
                         {
                             var d1 = m1.Duration.HasValue ? m1.Duration.Value : 0;
                             var d2 = m2.Duration.HasValue ? m2.Duration.Value : 0;
                             return d1.CompareTo(d2) * order; 
                         });
                    break;
            }
            return list;
        }
        public int Delete(int id)
        {
            
            var course = dbContext.Training_Courses.FirstOrDefault(p=>p.ID == id);
            course.DeleteFlag = true;
            course.UpdateDate = DateTime.Now;
            course.UpdatedBy = HttpContext.Current.User.Identity.Name;
            dbContext.SubmitChanges();
            return course.TypeOfCourse;
        }
        public Message DeleteList(string[] ids, ref int typeOfCourse)
        {
            DbTransaction trans = null;
            try
            {
                dbContext.Connection.Open();
                trans = dbContext.Connection.BeginTransaction();
                dbContext.Transaction = trans;
                foreach (var id in ids)
                    typeOfCourse = Delete(ConvertUtil.ConvertToInt(id));
                trans.Commit();
                return new Message(MessageConstants.I0011, MessageType.Info, ids.Length + 
                    (ids.Length > 1 ? " courses have" : " course has") + " been deleted");
            }
            catch
            {
                trans.Rollback();
                return new Message(MessageConstants.E0007, MessageType.Error);
            }
        }
    }
}