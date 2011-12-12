using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CRM.Library.Common;

namespace CRM.Models
{
    public class TrainingEmpEnglishInfoDao: BaseDao
    {
        public List<Training_MasterEnglishType> GetTypeList()
        {
            return dbContext.Training_MasterEnglishTypes.ToList();
        }
        public Training_MasterEnglishType GetMasterEnglishTypeByID(int id)
        {
            return dbContext.Training_MasterEnglishTypes.Where(q => q.ID.Equals(id)).FirstOrDefault();
        }
        public string GetTypeName(int typeId)
        {
            var type = dbContext.Training_MasterEnglishTypes.FirstOrDefault(p => p.ID == typeId);
            return type == null ? "" : type.Name;
        }
        public List<sp_GetListEmpEnglishInfoResult> GetList(string name, int? type)
        {
            return dbContext.sp_GetListEmpEnglishInfo(name, type).ToList();
        }
        public List<Training_EmpEnglishInfo> GetListByEmpId(string empId)
        {
            return dbContext.Training_EmpEnglishInfos.Where(p => p.EmployeeId == empId && !p.DeleteFlag).ToList();
        }

        public Training_EmpEnglishInfo GetScoreToeicWithEmployee(string empID)
        {
            return dbContext.Training_EmpEnglishInfos.Where(q => q.EmployeeId == empID && q.TypeId == Constants.TRAINING_CENTER_SKILL_TYPE_TOEIC &&
                !q.DeleteFlag && (q.ExpireDate==null || (q.ExpireDate!=null && q.ExpireDate.Value >= DateTime.Now))).FirstOrDefault();
        }

        public List<sp_GetListEmpEnglishInfoResult> Sort(List<sp_GetListEmpEnglishInfoResult> list, string sortColumn, string sortOrder)
        {
            int order = 1;
            if (sortOrder == "desc")
            {
                order = -1;
            }
            switch (sortColumn)
            {
                case "Name":
                    list.Sort(
                         delegate(sp_GetListEmpEnglishInfoResult m1, sp_GetListEmpEnglishInfoResult m2)
                         { return m1.EmployeeName.CompareTo(m2.EmployeeName) * order; });
                    break;
                case "EmpID":
                    list.Sort(
                         delegate(sp_GetListEmpEnglishInfoResult m1, sp_GetListEmpEnglishInfoResult m2)
                         { return m1.EmployeeId.CompareTo(m2.EmployeeId) * order; });
                    break;
                case "Type":
                    list.Sort(
                         delegate(sp_GetListEmpEnglishInfoResult m1, sp_GetListEmpEnglishInfoResult m2)
                         { return m1.TypeName.CompareTo(m2.TypeName) * order; });
                    break;
                case "Score":
                    list.Sort(
                         delegate(sp_GetListEmpEnglishInfoResult m1, sp_GetListEmpEnglishInfoResult m2)
                         { return m1.Score.CompareTo(m2.Score) * order; });
                    break;
                case "ExpireDate":
                    list.Sort(
                         delegate(sp_GetListEmpEnglishInfoResult m1, sp_GetListEmpEnglishInfoResult m2)
                         {
                             DateTime expire1 = m1.ExpireDate ?? DateTime.MinValue;
                             DateTime expire2 = m2.ExpireDate ?? DateTime.MinValue;
                             return expire1.CompareTo(expire2) * order; 
                         });
                    break;
            }
            return list;
        }

        public Message Insert(Training_EmpEnglishInfo eei)
        {
            try
            {
                eei.CreateDate = eei.UpdateDate = DateTime.Now;
                eei.CreatedBy = eei.UpdatedBy = HttpContext.Current.User.Identity.Name;
                dbContext.Training_EmpEnglishInfos.InsertOnSubmit(eei);
                dbContext.SubmitChanges();
                return new Message(MessageConstants.I0001, MessageType.Info, "Employee English Information", "added");
            }
            catch
            {
                return new Message(MessageConstants.E0007, MessageType.Error);
            }
        }

        public Training_EmpEnglishInfo GetById(int id)
        {
            return dbContext.Training_EmpEnglishInfos.FirstOrDefault(p=>p.ID==id && !p.DeleteFlag);
        }
        public Message Update(Training_EmpEnglishInfo eei)
        {
            try
            {
                Training_EmpEnglishInfo objDb = GetById(eei.ID);
                //objDb.EmployeeId = eei.EmployeeId;
                objDb.ExpireDate = eei.ExpireDate;
                objDb.Notes = eei.Notes;
                objDb.Score = eei.Score;
                objDb.TypeId = eei.TypeId;
                objDb.UpdateDate = DateTime.Now;
                objDb.UpdatedBy = HttpContext.Current.User.Identity.Name;
                dbContext.SubmitChanges();
                return new Message(MessageConstants.I0001, MessageType.Info, "English Information of employeee " + eei.EmployeeId, "updated");
            }
            catch
            {
                return new Message(MessageConstants.E0007, MessageType.Error);
            }
        }

        public Message Delete(int[] idArr)
        {
            try
            {
                if (idArr.Length > 1)
                {
                    var list = dbContext.Training_EmpEnglishInfos.Where(p => idArr.Contains(p.ID));
                    foreach (var item in list)
                    {
                        item.DeleteFlag = true;
                        item.UpdateDate = DateTime.Now;
                        item.UpdatedBy = HttpContext.Current.User.Identity.Name;
                    }
                    dbContext.SubmitChanges();
                    return new Message(MessageConstants.I0011, MessageType.Info,
                        idArr.Length + " English Information have been deleted");
                }
                else
                {
                    return Delete(idArr.First());
                }
            }
            catch
            {
                return new Message(MessageConstants.E0007, MessageType.Error);
            }
        }
        public Message Delete(int id)
        {
            try
            {
                Training_EmpEnglishInfo objDb = GetById(id);
                //objDb.EmployeeId = eei.EmployeeId;
                objDb.DeleteFlag = true;
                objDb.UpdateDate = DateTime.Now;
                objDb.UpdatedBy = HttpContext.Current.User.Identity.Name;
                dbContext.SubmitChanges();
                return new Message(MessageConstants.I0001, MessageType.Info, "English Information of employeee " + objDb.EmployeeId, "deleted");
            }
            catch
            {
                return new Message(MessageConstants.E0007, MessageType.Error);
            }
        }
    }
}