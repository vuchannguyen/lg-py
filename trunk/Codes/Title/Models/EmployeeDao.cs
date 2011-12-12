using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CRM.Library.Common;
using System.Data.Common;
using System.Text.RegularExpressions;

namespace CRM.Models
{
    public class EmployeeDao : BaseDao
    {

        public List<sp_GetEmployeeForExportResult> GetListForExport(string name, int department, int subDepartment, int title, int isActive, int status, string locationCode)
        {
            return dbContext.sp_GetEmployeeForExport(name, department, subDepartment, title, isActive, status, locationCode).OrderByDescending(p => p.ID).ToList<sp_GetEmployeeForExportResult>();
        }
        public List<Employee> GetListNotResigned()
        {
            return dbContext.Employees.Where(p => p.EmpStatusId != Constants.RESIGNED).ToList();
        }
        public Employee GetByJR(string jrID)
        {
            return dbContext.Employees.Where(q => q.JR == jrID).FirstOrDefault();
        }

        public List<sp_GetEmployeeManagerResult> GetEmployeeManager(string name)
        {
            return dbContext.sp_GetEmployeeManager(name).ToList();
        }
        public List<Employee> GetListByOfficeEmail(string email)
        {
            return dbContext.Employees.Where(p => p.OfficeEmail.ToLower().
                Remove(p.OfficeEmail.IndexOf("@")).Contains(email.ToLower()) && 
                (p.EmpStatusId == null || p.EmpStatusId.Value != Constants.RESIGNED)).ToList();
        }

        public List<sp_GetEmployeeResult> GetList(string name, int department, int subDepartment, int title, int isActive, int status)
        {
            return dbContext.sp_GetEmployee(name, department, subDepartment, title, isActive, status, null).OrderByDescending(p => p.ID).ToList<sp_GetEmployeeResult>();
        }

        public static bool IsSqlLikeMatch(string input, string pattern)
        {            
            pattern = Regex.Escape(pattern);            
            pattern = pattern.Replace("%", ".*?").Replace("_", ".");            
            pattern = pattern.Replace(@"\[", "[").Replace(@"\]", "]").Replace(@"\^", "^");
            return Regex.IsMatch(input, pattern, RegexOptions.IgnoreCase);
        }        

        public List<sp_GetEmployeeResult> GetList(string name, int department, int subDepartment, int title, int isActive, int status, string locationCode)
        {                        
            return dbContext.sp_GetEmployee(name, department, subDepartment, title, isActive, status, locationCode).OrderByDescending(p => p.ID).ToList<sp_GetEmployeeResult>();
        }        

        public bool IsManager(string empId)
        {
            Employee emp = GetById(empId);
            if (emp != null)
            {
                return dbContext.Employees.Where(p => p.ID.Equals(empId)).FirstOrDefault().
                    JobTitleLevel.JobTitle.IsManager;
            }
            return false;
        }   

        /// <summary>
        /// Get Name By AutoComplete
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public List<sp_GetEmployeeResult> GetListByName(string name, int isActive, int status)
        {
            return this.GetList(name, 0, 0, 0, isActive, status).ToList<sp_GetEmployeeResult>();
        }

        /// <summary>
        /// Get employee by OfficeEmail
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public Employee GetByOfficeEmail(string email)
        {
            return dbContext.Employees.Where(e => e.OfficeEmail == email).FirstOrDefault<Employee>();
        }

        /// <summary>
        /// Get employee by OfficeEmail
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public Employee GetByOfficeEmailInActiveList(string email)
        {
            return dbContext.Employees.Where(e => e.OfficeEmail == email && (e.EmpStatusId == null || e.EmpStatusId.Value != Constants.RESIGNED)).FirstOrDefault<Employee>();
        }

        public List<EmployeeDepartmentJobTitleTracking> GetListHistoryById(string id)
        {
            return dbContext.EmployeeDepartmentJobTitleTrackings.Where(c => c.EmployeeId.Equals(id)).ToList<EmployeeDepartmentJobTitleTracking>();
        }
        /// <summary>
        /// Sort Emplooyee
        /// </summary>
        /// <param name="list"></param>
        /// <param name="sortColumn"></param>
        /// <param name="sortOrder"></param>
        /// <returns></returns>
        public List<sp_GetEmployeeResult> Sort(List<sp_GetEmployeeResult> list, string sortColumn, string sortOrder)
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
                         delegate(sp_GetEmployeeResult m1, sp_GetEmployeeResult m2)
                         { return m1.ID.CompareTo(m2.ID) * order; });
                    break;
                case "DisplayName":
                    list.Sort(
                         delegate(sp_GetEmployeeResult m1, sp_GetEmployeeResult m2)
                         { return m1.DisplayName.CompareTo(m2.DisplayName) * order; });
                    break;
                case "JobTitle":
                    list.Sort(
                         delegate(sp_GetEmployeeResult m1, sp_GetEmployeeResult m2)
                         { return m1.TitleName.CompareTo(m2.TitleName) * order; });
                    break;
                case "Department":
                    list.Sort(
                         delegate(sp_GetEmployeeResult m1, sp_GetEmployeeResult m2)
                         { return m1.Department.CompareTo(m2.Department) * order; });
                    break;
                case "SubDepartment":
                    list.Sort(
                         delegate(sp_GetEmployeeResult m1, sp_GetEmployeeResult m2)
                         { return m1.DepartmentName.CompareTo(m2.DepartmentName) * order; });
                    break;
                case "StartDate":
                    list.Sort(
                         delegate(sp_GetEmployeeResult m1, sp_GetEmployeeResult m2)
                         { return m1.StartDate.CompareTo(m2.StartDate) * order; });
                    break;
                case "ResignDate":
                    list.Sort(
                         delegate(sp_GetEmployeeResult m1, sp_GetEmployeeResult m2)
                         { return m1.ResignedDate.Value.CompareTo(m2.ResignedDate.Value) * order; });
                    break;
                case "Status":
                    list.Sort(
                         delegate(sp_GetEmployeeResult m1, sp_GetEmployeeResult m2)
                         { return (m1.EmpStatusId.HasValue ? m1.EmpStatusId.Value : 0).CompareTo((m2.EmpStatusId.HasValue ? m2.EmpStatusId.Value : 0)) * order; });
                    break;
            }

            return list;
        }
        //public List<sp_GetEmployeesForPRResult> Sort(List<sp_GetEmployeesForPRResult> list, string sortColumn, string sortOrder)
        //{
        //    int order;

        //    if (sortOrder == "desc")
        //    {
        //        order = -1;
        //    }
        //    else
        //    {
        //        order = 1;
        //    }
        //    switch (sortColumn)
        //    {
        //        case "ID":
        //            list.Sort(
        //                 delegate(sp_GetEmployeeResult m1, sp_GetEmployeeResult m2)
        //                 { return m1.ID.CompareTo(m2.ID) * order; });
        //            break;
        //        case "DisplayName":
        //            list.Sort(
        //                 delegate(sp_GetEmployeeResult m1, sp_GetEmployeeResult m2)
        //                 { return m1.DisplayName.CompareTo(m2.DisplayName) * order; });
        //            break;
        //        case "JobTitle":
        //            list.Sort(
        //                 delegate(sp_GetEmployeeResult m1, sp_GetEmployeeResult m2)
        //                 { return m1.TitleName.CompareTo(m2.TitleName) * order; });
        //            break;
        //        case "Department":
        //            list.Sort(
        //                 delegate(sp_GetEmployeeResult m1, sp_GetEmployeeResult m2)
        //                 { return m1.Department.CompareTo(m2.Department) * order; });
        //            break;
        //        case "SubDepartment":
        //            list.Sort(
        //                 delegate(sp_GetEmployeeResult m1, sp_GetEmployeeResult m2)
        //                 { return m1.DepartmentName.CompareTo(m2.DepartmentName) * order; });
        //            break;
        //        case "StartDate":
        //            list.Sort(
        //                 delegate(sp_GetEmployeeResult m1, sp_GetEmployeeResult m2)
        //                 { return m1.StartDate.CompareTo(m2.StartDate) * order; });
        //            break;
        //        case "ResignDate":
        //            list.Sort(
        //                 delegate(sp_GetEmployeeResult m1, sp_GetEmployeeResult m2)
        //                 { return m1.ResignedDate.Value.CompareTo(m2.ResignedDate.Value) * order; });
        //            break;
        //        case "Status":
        //            list.Sort(
        //                 delegate(sp_GetEmployeeResult m1, sp_GetEmployeeResult m2)
        //                 { return (m1.EmpStatusId.HasValue ? m1.EmpStatusId.Value : 0).CompareTo((m2.EmpStatusId.HasValue ? m2.EmpStatusId.Value : 0)) * order; });
        //            break;
        //    }

        //    return list;
        //}
        public List<sp_GetEmployeeForExportResult> SortForExport(List<sp_GetEmployeeForExportResult> list, string sortColumn, string sortOrder)
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
                         delegate(sp_GetEmployeeForExportResult m1, sp_GetEmployeeForExportResult m2)
                         { return m1.ID.CompareTo(m2.ID) * order; });
                    break;
                case "DisplayName":
                    list.Sort(
                         delegate(sp_GetEmployeeForExportResult m1, sp_GetEmployeeForExportResult m2)
                         { return m1.DisplayName.CompareTo(m2.DisplayName) * order; });
                    break;
                case "JobTitle":
                    list.Sort(
                         delegate(sp_GetEmployeeForExportResult m1, sp_GetEmployeeForExportResult m2)
                         { return m1.TitleName.CompareTo(m2.TitleName) * order; });
                    break;
                case "Department":
                    list.Sort(
                         delegate(sp_GetEmployeeForExportResult m1, sp_GetEmployeeForExportResult m2)
                         { return m1.DepartmentName.CompareTo(m2.DepartmentName) * order; });
                    break;
                case "StartDate":
                    list.Sort(
                         delegate(sp_GetEmployeeForExportResult m1, sp_GetEmployeeForExportResult m2)
                         { return m1.StartDate.CompareTo(m2.StartDate) * order; });
                    break;
                case "ResignDate":
                    list.Sort(
                         delegate(sp_GetEmployeeForExportResult m1, sp_GetEmployeeForExportResult m2)
                         { return m1.ResignedDate.Value.CompareTo(m2.ResignedDate.Value) * order; });
                    break;
                case "Status":
                    list.Sort(
                         delegate(sp_GetEmployeeForExportResult m1, sp_GetEmployeeForExportResult m2)
                         { return (m1.EmpStatusid.HasValue ? m1.EmpStatusid.Value : 0).CompareTo((m2.EmpStatusid.HasValue ? m2.EmpStatusid.Value : 0)) * order; });
                    break;
            }

            return list;
        }
        public string FullName(string empId, Constants.FullNameFormat format)
        {
            return dbContext.GetEmployeeFullName(empId, (int)format);
        }
        public Message InsertMulti(List<Employee> list)
        {
            Message msg = null;
            DbTransaction trans = null;
            bool ok = true;
            try
            {
                dbContext.Connection.Open();
                trans = dbContext.Connection.BeginTransaction();
                dbContext.Transaction = trans;
                if (list.Count > 0)
                {
                    foreach (Employee item in list)
                    {
                        if (ok)
                        {
                            // dbContext.Employees.InsertAllOnSubmit(list);
                            msg = Insert(item);
                            if (msg.MsgType == MessageType.Error)
                            {
                                ok = false;
                            }
                        }
                        else
                        {
                            break;
                        }
                    }

                    //foreach (Employee item in list)
                    //{
                    //    InsertTracking(item);
                    //}
                }
                if (ok)
                {
                    trans.Commit();
                }
                else
                {
                    trans.Rollback();
                }
            }
            catch(Exception ex)
            {

                if (trans != null) trans.Rollback();
                // Show system error
                msg = new Message(MessageConstants.E0007, MessageType.Error);
            }
            return msg;
        }

        public Employee GetById(string id)
        {
            return dbContext.Employees.Where(c => c.ID.Equals(id)).FirstOrDefault<Employee>();
        }
        public Employee GetById(string id, bool? isResigned)
        {
            return dbContext.Employees.FirstOrDefault(c => c.ID.Equals(id) && !c.DeleteFlag &&  
                (isResigned == null || 
                (!isResigned.Value && (c.EmpStatusId != Constants.RESIGNED || c.EmpStatusId == null)) ||
                (isResigned.Value && c.EmpStatusId == Constants.RESIGNED)
                ));
        }

        public Employee GetEmployeeHasSeatcodeByID(string id)
        {
            return dbContext.Employees.Where(c => c.ID.Equals(id) && c.LocationCode != null).FirstOrDefault<Employee>();
        }

        /// <summary>
        /// Delete List
        /// </summary>
        /// <param name="ids"></param>
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
                    string[] idArr = ids.Split(',');
                    int total = idArr.Count();
                    foreach (string id in idArr)
                    {
                        string empID = id;
                        Employee emp = GetById(empID);
                        if (emp != null)
                        {
                            emp.UpdatedBy = userName;
                            Delete(emp);
                        }
                        else
                        {
                            total--;
                        }
                    }

                    // Show succes message
                    msg = new Message(MessageConstants.I0001, MessageType.Info, total.ToString() + " Employee(s)", "deleted");
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
        public List<Employee> GetListWithAllAttr(string name)
        {
            if (name == null)
                name = "";
            return dbContext.Employees.Where( p => 
                (
                name == "" ||
                (p.MiddleName == null || p.MiddleName == "") ?
                (p.FirstName + " " + p.LastName).ToLower().Contains(name.Trim().ToLower()) :
                (p.FirstName + " " + p.MiddleName + " " + p.LastName).ToLower().Contains(name.Trim().ToLower())     
                )
                && !p.DeleteFlag && p.EmpStatusId != Constants.RESIGNED
                ).ToList();
        }
        
        public List<Employee> GetListManagerWithAllAttr(string name)
        {
            return GetListWithAllAttr(name).Where(p=>p.JobTitleLevel.JobTitle.IsManager).ToList();
        }
        public List<sp_GetManagerResult> GetManager(string text, int department, int title)
        {
            return dbContext.sp_GetManager(text, department, title).ToList<sp_GetManagerResult>();
        }

        public Message Insert(Employee objUI)
        {
            Message msg = null;
            try
            {
                if (objUI != null)
                {
                    objUI.ResignedDate = null;
                    objUI.ResignedAllowance = null;
                    objUI.ResignedReason = null;
                    objUI.DeleteFlag = false;
                    dbContext.Employees.InsertOnSubmit(objUI);
                    dbContext.SubmitChanges();
                    //Write Log
                    new EmployeeLogDao().WriteLogForEmployee(null, objUI, ELogAction.Insert);
                    // Show success message
                    msg = new Message(MessageConstants.I0001, MessageType.Info, "Employee '" + objUI.FirstName + " " + objUI.MiddleName + " " + objUI.LastName + "'", "added");
                }
            }
            catch (Exception ex) 
            {
                msg = new Message(MessageConstants.E0007, MessageType.Error);
                throw ex;
            }
            return msg;

        }

        public Message InsertTracking(Employee objUI)
        {
            Message msg = null;
            try
            {
                // Insert Department Tracking
                EmployeeDepartmentJobTitleTracking deptTracking = new EmployeeDepartmentJobTitleTracking();
                deptTracking.EmployeeId = objUI.ID;
                deptTracking.DepartmentName = objUI.Department.DepartmentName;
                deptTracking.JobTitleName = objUI.JobTitleLevel.DisplayName;
                deptTracking.StartDate = objUI.StartDate;
                deptTracking.CreatedDate = objUI.CreateDate;
                deptTracking.CreatedBy = objUI.CreatedBy;
                deptTracking.UpdatedDate = objUI.UpdateDate;
                deptTracking.UpdatedBy = objUI.UpdatedBy;
                EmployeeDepartmentJobTitleTrackingDao deptDao = new EmployeeDepartmentJobTitleTrackingDao();
               msg = deptDao.Insert(deptTracking);

                //Insert Insurance Hospital Tracking
                if (!string.IsNullOrEmpty(objUI.InsuranceHospitalID))
                {
                    EmployeeInsuranceHospitalTracking hospitalTracking = new EmployeeInsuranceHospitalTracking();
                    hospitalTracking.EmployeeId = objUI.ID;
                    hospitalTracking.InsuranceHospitalID = objUI.InsuranceHospitalID;
                    hospitalTracking.StartDate = objUI.StartDate;
                    hospitalTracking.CreatedDate = objUI.CreateDate;
                    hospitalTracking.CreatedBy = objUI.CreatedBy;
                    hospitalTracking.UpdatedDate = objUI.UpdateDate;
                    hospitalTracking.UpdatedBy = objUI.UpdatedBy;
                    InsuranceHospitalDao hospitalDao = new InsuranceHospitalDao();
                   msg = hospitalDao.Insert(hospitalTracking);
                }
                msg = new Message(MessageConstants.I0001, MessageType.Info, "Employee '" + objUI.FirstName + " " + objUI.MiddleName + " " + objUI.LastName + "'", "added");
            }
            catch
            {
                msg = new Message(MessageConstants.E0007, MessageType.Error);
            }
            return msg;
        }

        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="objUI"></param>
        private void Delete(Employee objUI)
        {
            if (objUI != null)
            {
                // Get current group in dbContext
                Employee objDb = GetById(objUI.ID);
                if (objDb != null)
                {
                    // Set delete info                    
                    objDb.DeleteFlag = true;
                    objDb.UpdateDate = DateTime.Now;
                    objDb.UpdatedBy = objUI.UpdatedBy;
                    // Submit changes to dbContext

                    new EmployeeLogDao().WriteLogForEmployee(null, objUI, ELogAction.Delete);
                    dbContext.SubmitChanges();

                }
            }
        }

        public Message UpdateForResign(Employee objUI)
        {
            Message msg = null;
            try
            {
                if (objUI != null)
                {
                    // Get current group in dbContext
                    Employee objDb = GetById(objUI.ID);
                    if (objDb != null)
                    {
                        // Check valid update date
                        if (IsValidUpdateDate(objUI, objDb, out msg))
                        {
                            new EmployeeLogDao().WriteLogForEmployee(null, objUI, ELogAction.Delete);
                            // Update info by objUI
                            objDb.ResignedDate = objUI.ResignedDate;
                            objDb.ResignedAllowance = objUI.ResignedAllowance;
                            objDb.ResignedReason = objUI.ResignedReason;
                            objDb.UpdateDate = DateTime.Now;
                            objDb.UpdatedBy = objUI.UpdatedBy;
                            objDb.EmpStatusId = objUI.EmpStatusId;

                            #region Update Contract
                            bool isComplete = true;
                            ContractRenewalDao contractDao = new ContractRenewalDao();
                            List<Contract> contractList = contractDao.GetList(objDb.ID);
                            if (contractList.Count > 0)
                            {
                                foreach (Contract item in contractList)
                                {
                                    if (item.EndDate.HasValue)
                                    {
                                        if ((item.StartDate < objUI.ResignedDate) && (item.EndDate.Value < objUI.ResignedDate))
                                        {
                                            msg = contractDao.UpdateNotification(item.ContractId);
                                        }
                                        else if ((item.StartDate <= objUI.ResignedDate) && (item.EndDate.Value >= objUI.ResignedDate))
                                        {
                                            msg = contractDao.UpdateNotificationCurrent(item.ContractId, objUI.ResignedDate.Value);
                                        }
                                        else if ((item.StartDate > objUI.ResignedDate))
                                        {
                                            contractDao.Delete(item);
                                        }
                                    }
                                    else
                                    {
                                        if (item.StartDate <= objUI.ResignedDate)
                                        {
                                            msg = contractDao.UpdateNotificationCurrent(item.ContractId, objUI.ResignedDate.Value);
                                        }
                                        else
                                        {
                                            contractDao.Delete(item);
                                        }

                                    }
                                    if (msg != null)
                                    {
                                        if (msg.MsgType == MessageType.Error)
                                        {
                                            isComplete = false;
                                        }
                                    }
                                }

                            }

                            #endregion
                            // Submit changes to dbContext
                            if (isComplete != false)
                            {
                                dbContext.SubmitChanges();

                                // Show success message
                                msg = new Message(MessageConstants.I0001, MessageType.Info, "Employee '" + objDb.FirstName + " " + objDb.MiddleName + " " + objDb.LastName + "'", "resigned");

                            }
                            else
                            {
                                msg = new Message(MessageConstants.E0007, MessageType.Error);

                            }
                        }

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

        public Message UpdateCV(Employee objUI)
        {
            Message msg = null;
            try
            {
                if (objUI != null)
                {
                    // Get current group in dbContext
                    Employee objDb = GetById(objUI.ID);
                    if (objDb != null)
                    {
                        // Check valid update date
                        if (IsValidUpdateDate(objUI, objDb, out msg))
                        {
                            new EmployeeLogDao().WriteLogForUpdateCV(objUI, ELogAction.Update);
                            // Update info by objUI                       
                            objDb.CVFile = objUI.CVFile;
                            //objDb.UpdateDate = DateTime.Now;
                            //objDb.UpdatedBy = objUI.UpdatedBy;
                            // Submit changes to dbContext
                            dbContext.SubmitChanges();

                            // Show success message
                            msg = new Message(MessageConstants.I0001, MessageType.Info, "Employee CV", "uploaded");
                        }
                    }
                }
            }
            catch
            {
                msg = new Message(MessageConstants.E0007, MessageType.Error);
            }
            return msg;
        }

        public Message UpdateImage(Employee objUI)
        {
            Message msg = null;
            try
            {
                if (objUI != null)
                {
                    // Get current group in dbContext
                    Employee objDb = GetById(objUI.ID);

                    if (objDb != null)
                    {
                        // Check valid update date
                        if (IsValidUpdateDate(objUI, objDb, out msg))
                        {
                            new EmployeeLogDao().WriteLogForUpdateImage(objUI, ELogAction.Update);
                            // Update info by objUI                       
                            objDb.Photograph = objUI.Photograph;
                            //objDb.UpdateDate = DateTime.Now;
                            //objDb.UpdatedBy = objUI.UpdatedBy;

                            // Submit changes to dbContext
                            dbContext.SubmitChanges();

                            // Show success message
                            msg = new Message(MessageConstants.I0001, MessageType.Info, "Employee photo", "uploaded");
                        }
                    }
                }
            }
            catch
            {
                msg = new Message(MessageConstants.E0007, MessageType.Error);
            }
            return msg;
        }

        public Message UpdatePersonalInfo(Employee objUI)
        {
            Message msg = null;
            try
            {
                if (objUI != null)
                {
                    // Get current group in dbContext
                    Employee objDb = GetById(objUI.ID);

                    if (objDb != null)
                    {
                        // Check valid update date
                        if (IsValidUpdateDate(objUI, objDb, out msg))
                        {
                            new EmployeeLogDao().WriteLogForUpdatePersonal(objUI, ELogAction.Update);
                            // Update info by objUI                       
                            objDb.FirstName = objUI.FirstName;
                            objDb.MiddleName = objUI.MiddleName;
                            objDb.Major = objUI.Major;
                            objDb.LastName = objUI.LastName;
                            objDb.DOB = objUI.DOB;
                            objDb.POB = objUI.POB;
                            objDb.PlaceOfOrigin = objUI.PlaceOfOrigin;
                            objDb.Nationality = objUI.Nationality;
                            objDb.IDNumber = objUI.IDNumber;
                            objDb.OtherDegree = objUI.OtherDegree;
                            objDb.IssueDate = objUI.IssueDate;
                            objDb.Gender = objUI.Gender;
                            objDb.Religion = objUI.Religion;
                            objDb.MarriedStatus = objUI.MarriedStatus;
                            objDb.EmpStatusId = objUI.EmpStatusId;
                            objDb.UpdateDate = DateTime.Now;
                            objDb.UpdatedBy = objUI.UpdatedBy;
                            objDb.VnFirstName = objUI.VnFirstName;
                            objDb.Race = objUI.Race;
                            objDb.VnMiddleName = objUI.VnMiddleName;
                            objDb.VnPlaceOfOrigin = objUI.VnPlaceOfOrigin;
                            objDb.VnLastName = objUI.VnLastName;
                            objDb.VnPOB = objUI.VnPOB;
                            objDb.Degree = objUI.Degree;
                            objDb.IDIssueLocation = objUI.IDIssueLocation;
                            objDb.VnIDIssueLocation = objUI.VnIDIssueLocation;
                            // Submit changes to dbContext
                            dbContext.SubmitChanges();

                            // Show success message
                            msg = new Message(MessageConstants.I0001, MessageType.Info, "Employee '" + objDb.FirstName + " " + objDb.MiddleName + " " + objDb.LastName + "'", "updated");
                        }
                    }
                }
            }
            catch
            {
                msg = new Message(MessageConstants.E0007, MessageType.Error);
            }


            return msg;
        }

        public Message UpdateCompanyInfo(Employee objUI, DateTime department, DateTime title,DateTime hospital)
        {
            Message msg = null;
            try
            {
                if (objUI != null)
                {
                    // Get current group in dbContext
                    Employee objDb = GetById(objUI.ID);
                   
                        if (objDb != null)
                        {
                            // Check valid update date
                            if (IsValidUpdateDate(objUI, objDb, out msg))
                            {
                                new EmployeeLogDao().WriteLogForUpdateCompany(objUI, ELogAction.Update);
                                if (department == title)
                                {
                                    if (department != new DateTime())
                                    {
                                        EmployeeDepartmentJobTitleTracking deptTracking = new EmployeeDepartmentJobTitleTracking();
                                        deptTracking.EmployeeId = objDb.ID;
                                        deptTracking.DepartmentName = new DepartmentDao().GetById(objUI.DepartmentId).DepartmentName;
                                        deptTracking.JobTitleName = new JobTitleLevelDao().GetById(objUI.TitleId).DisplayName;
                                        deptTracking.StartDate = department;
                                        deptTracking.CreatedDate = department;
                                        deptTracking.CreatedBy = objUI.UpdatedBy;
                                        deptTracking.UpdatedDate = department;
                                        deptTracking.UpdatedBy = objUI.UpdatedBy;
                                        EmployeeDepartmentJobTitleTrackingDao deptDao = new EmployeeDepartmentJobTitleTrackingDao();
                                        deptDao.Insert(deptTracking);
                                    }
                                }
                                else
                                {
                                    if (objDb.DepartmentId != objUI.DepartmentId)
                                    {
                                        EmployeeDepartmentJobTitleTracking deptTracking = new EmployeeDepartmentJobTitleTracking();
                                        deptTracking.EmployeeId = objDb.ID;
                                        deptTracking.DepartmentName = new DepartmentDao().GetById(objUI.DepartmentId).DepartmentName;
                                        deptTracking.JobTitleName = new JobTitleLevelDao().GetById(objDb.TitleId).DisplayName;
                                        deptTracking.StartDate = department;
                                        deptTracking.CreatedDate = department;
                                        deptTracking.CreatedBy = objUI.UpdatedBy;
                                        deptTracking.UpdatedDate = department;
                                        deptTracking.UpdatedBy = objUI.UpdatedBy;
                                        EmployeeDepartmentJobTitleTrackingDao deptDao = new EmployeeDepartmentJobTitleTrackingDao();
                                        deptDao.Insert(deptTracking);
                                    }
                                    if (objDb.TitleId != objUI.TitleId)
                                    {
                                        EmployeeDepartmentJobTitleTrackingDao deptDao = new EmployeeDepartmentJobTitleTrackingDao();
                                        EmployeeDepartmentJobTitleTracking deptTracking = new EmployeeDepartmentJobTitleTracking();
                                        deptTracking.EmployeeId = objDb.ID;
                                        deptTracking.DepartmentName = new DepartmentDao().GetById(objUI.DepartmentId).DepartmentName;
                                        deptTracking.JobTitleName = new JobTitleLevelDao().GetById(objUI.TitleId).DisplayName;
                                        deptTracking.StartDate = title;
                                        deptTracking.CreatedDate = title;
                                        deptTracking.CreatedBy = objUI.UpdatedBy;
                                        deptTracking.UpdatedDate = title;
                                        deptTracking.UpdatedBy = objUI.UpdatedBy;
                                        deptDao.Insert(deptTracking);
                                    }
                                }
                                if (objDb.InsuranceHospitalID != objUI.InsuranceHospitalID && hospital != null)
                                {
                                    if (hospital != new DateTime())
                                    {
                                        EmployeeInsuranceHospitalTracking hospitalTracking = new EmployeeInsuranceHospitalTracking();
                                        hospitalTracking.EmployeeId = objDb.ID;
                                        hospitalTracking.InsuranceHospitalID = objUI.InsuranceHospitalID;
                                        hospitalTracking.StartDate = hospital;
                                        hospitalTracking.CreatedDate = hospital;
                                        hospitalTracking.CreatedBy = objUI.UpdatedBy;
                                        hospitalTracking.UpdatedDate = hospital;
                                        hospitalTracking.UpdatedBy = objUI.UpdatedBy;
                                        InsuranceHospitalDao hospitalDao = new InsuranceHospitalDao();
                                        hospitalDao.Insert(hospitalTracking);
                                        objDb.InsuranceHospitalID = objUI.InsuranceHospitalID;
                                    }
                                }
                                objDb.DepartmentId = objUI.DepartmentId;
                                objDb.TitleId = objUI.TitleId;
                                // Update info by objUI                       
                                objDb.JR = objUI.JR;
                                objDb.JRApproval = objUI.JRApproval;
                                objDb.Project = objUI.Project;
                                objDb.ManagerId = objUI.ManagerId;
                                objDb.StartDate = objUI.StartDate;
                                objDb.ContractedDate = objUI.ContractedDate;
                                objDb.LaborUnion = objUI.LaborUnion;
                                objDb.LaborUnionDate = objUI.LaborUnionDate;
                                objDb.UpdateDate = DateTime.Now;
                                objDb.UpdatedBy = objUI.UpdatedBy;
                                objDb.TaxID = objUI.TaxID;
                                objDb.TaxIssueDate = objUI.TaxIssueDate;
                                objDb.SocialInsuranceNo = objUI.SocialInsuranceNo;
                                objDb.InsuranceHospitalID = objUI.InsuranceHospitalID;
                                objDb.LocationCode = objUI.LocationCode;
                                // Submit changes to dbContext
                                dbContext.SubmitChanges();

                                // Show success message
                                msg = new Message(MessageConstants.I0001, MessageType.Info, "Employee '" + objDb.FirstName + " " + objDb.MiddleName + " " + objDb.LastName + "'", "updated");
                            }
                        }
                }
            }
            catch
            {
                msg = new Message(MessageConstants.E0007, MessageType.Error);
            }

            return msg;
        }

        public Message UpdateContactInfo(Employee objUI)
        {
            Message msg = null;

            try
            {
                if (objUI != null)
                {
                    // Get current group in dbContext
                    Employee objDb = GetById(objUI.ID);

                    if (objDb != null)
                    {
                        // Check valid update date
                        if (IsValidUpdateDate(objUI, objDb, out msg))
                        {
                            new EmployeeLogDao().WriteLogForUpdateContact(objUI, ELogAction.Update);
                            // Update info by objUI                       
                            objDb.HomePhone = objUI.HomePhone;
                            objDb.CellPhone = objUI.CellPhone;
                            objDb.Floor = objUI.Floor;
                            objDb.ExtensionNumber = objUI.ExtensionNumber;
                            objDb.SeatCode = objUI.SeatCode;
                            objDb.SkypeId = objUI.SkypeId;
                            objDb.YahooId = objUI.YahooId;
                            objDb.PersonalEmail = objUI.PersonalEmail;
                            objDb.OfficeEmail = objUI.OfficeEmail;
                            objDb.EmergencyContactName = objUI.EmergencyContactName;
                            objDb.EmergencyContactPhone = objUI.EmergencyContactPhone;
                            objDb.EmergencyContactRelationship = objUI.EmergencyContactRelationship;
                            objDb.UpdateDate = DateTime.Now;
                            objDb.UpdatedBy = objUI.UpdatedBy;

                            // Submit changes to dbContext
                            dbContext.SubmitChanges();

                            // Show success message
                            msg = new Message(MessageConstants.I0001, MessageType.Info, "Employee '" + objDb.FirstName + " " + objDb.MiddleName + " " + objDb.LastName + "'", "updated");
                        }
                    }
                }
            }
            catch
            {
                msg = new Message(MessageConstants.E0007, MessageType.Error);
            }


            return msg;
        }

        public Message UpdateAddressInfo(Employee objUI)
        {
            Message msg = null;

            try
            {
                if (objUI != null)
                {
                    // Get current group in dbContext
                    Employee objDb = GetById(objUI.ID);

                    if (objDb != null)
                    {
                        // Check valid update date
                        if (IsValidUpdateDate(objUI, objDb, out msg))
                        {
                            new EmployeeLogDao().WriteLogForUpdateAddress(objUI, ELogAction.Update);
                            // Update info by objUI                       
                            objDb.PermanentAddress = objUI.PermanentAddress;
                            objDb.PermanentArea = objUI.PermanentArea;
                            objDb.PermanentDistrict = objUI.PermanentDistrict;
                            objDb.PermanentCityProvince = objUI.PermanentCityProvince;
                            objDb.PermanentCountry = objUI.PermanentCountry;
                            objDb.TempAddress = objUI.TempAddress;
                            objDb.TempArea = objUI.TempArea;
                            objDb.TempDistrict = objUI.TempDistrict;
                            objDb.TempCityProvince = objUI.TempCityProvince;
                            objDb.TempCountry = objUI.TempCountry;
                            objDb.VnPermanentAddress = objUI.VnPermanentAddress;
                            objDb.VnPermanentArea = objUI.VnPermanentArea;
                            objDb.VnPermanentDistrict = objUI.VnPermanentDistrict;
                            objDb.VnPermanentCityProvince = objUI.VnPermanentCityProvince;
                            objDb.VnPermanentCountry = objUI.VnPermanentCountry;
                            objDb.VnTempAddress = objUI.VnTempAddress;
                            objDb.VnTempArea = objUI.VnTempArea;
                            objDb.VnTempDistrict = objUI.VnTempDistrict;
                            objDb.VnTempCityProvince = objUI.VnTempCityProvince;
                            objDb.VnTempCountry = objUI.VnTempCountry;
                            objDb.UpdateDate = DateTime.Now;
                            objDb.UpdatedBy = objUI.UpdatedBy;

                            // Submit changes to dbContext
                            dbContext.SubmitChanges();

                            // Show success message
                            msg = new Message(MessageConstants.I0001, MessageType.Info, "Employee '" + objDb.FirstName + " " + objDb.MiddleName + " " + objDb.LastName + "'", "updated");
                        }
                    }
                }
            }
            catch
            {
                msg = new Message(MessageConstants.E0007, MessageType.Error);
            }
            return msg;
        }

        public Message UpdateBankAccountInfo(Employee objUI)
        {
            Message msg = null;

            try
            {
                if (objUI != null)
                {
                    // Get current group in dbContext
                    Employee objDb = GetById(objUI.ID);

                    if (objDb != null)
                    {
                        // Check valid update date
                        if (IsValidUpdateDate(objUI, objDb, out msg))
                        {
                            new EmployeeLogDao().WriteLogForUpdateBanks(objUI, ELogAction.Update);
                            // Update info by objUI                       
                            objDb.BankName = objUI.BankName;
                            objDb.BankAccount = objUI.BankAccount;
                            objDb.UpdateDate = DateTime.Now;
                            objDb.UpdatedBy = objUI.UpdatedBy;

                            // Submit changes to dbContext
                            dbContext.SubmitChanges();

                            // Show success message
                            msg = new Message(MessageConstants.I0001, MessageType.Info, "Employee '" + objDb.FirstName + " " + objDb.MiddleName + " " + objDb.LastName + "'", "updated");
                        }
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

        public Message UpdateRemark(Employee objUI)
        {
            Message msg = null;

            try
            {

                if (objUI != null)
                {
                    // Get current group in dbContext
                    Employee objDb = GetById(objUI.ID);

                    if (objDb != null)
                    {
                        // Check valid update date
                        if (IsValidUpdateDate(objUI, objDb, out msg))
                        {
                            new EmployeeLogDao().WriteLogForUpdateRemarks(objUI, ELogAction.Update);
                            // Update info by objUI                       
                            objDb.Remarks = objUI.Remarks;
                            objDb.UpdateDate = DateTime.Now;
                            objDb.UpdatedBy = objUI.UpdatedBy;

                            // Submit changes to dbContext
                            dbContext.SubmitChanges();

                            // Show success message
                            msg = new Message(MessageConstants.I0001, MessageType.Info, "Employee '" + objDb.FirstName + " " + objDb.MiddleName + " " + objDb.LastName + "'", "updated");
                        }
                    }
                }
            }
            catch
            {
                msg = new Message(MessageConstants.E0007, MessageType.Error);
            }
            return msg;
        }

        public Message Update(Employee objUI, DateTime department, DateTime title, DateTime hospital)
        {
            Message msg = null;
            try
            {
                if (objUI != null)
                {
                    Employee objDb = GetById(objUI.ID);
                    if (objDb != null)
                    {
                        // Check valid update date
                        if (IsValidUpdateDate(objUI, objDb, out msg))
                        {
                            new EmployeeLogDao().WriteLogForEmployee(null, objUI, ELogAction.Update);
                            objDb.FirstName = objUI.FirstName;
                            objDb.MiddleName = objUI.MiddleName;
                            objDb.LastName = objUI.LastName;
                            objDb.DOB = objUI.DOB;
                            objDb.POB = objUI.POB;
                            objDb.Major = objUI.Major;
                            objDb.PlaceOfOrigin = objUI.PlaceOfOrigin;
                            objDb.OtherDegree = objUI.OtherDegree;
                            objDb.Nationality = objUI.Nationality;
                            objDb.IDNumber = objUI.IDNumber;
                            objDb.IssueDate = objUI.IssueDate;
                            objDb.Gender = objUI.Gender;
                            objDb.Religion = objUI.Religion;
                            objDb.MarriedStatus = objUI.MarriedStatus;
                            objDb.EmpStatusId = objUI.EmpStatusId;
                            objDb.JR = objUI.JR;
                            objDb.Project = objUI.Project;
                            objDb.ManagerId = objUI.ManagerId;
                            objDb.JRApproval = objUI.JRApproval;
                            objDb.StartDate = objUI.StartDate;
                            objDb.ContractedDate = objUI.ContractedDate;
                            if (department == title)
                            {
                                if (department != new DateTime())
                                {
                                    EmployeeDepartmentJobTitleTracking deptTracking = new EmployeeDepartmentJobTitleTracking();
                                    deptTracking.EmployeeId = objDb.ID;
                                    deptTracking.DepartmentName = new DepartmentDao().GetById(objUI.DepartmentId).DepartmentName;
                                    deptTracking.JobTitleName = new JobTitleLevelDao().GetById(objUI.TitleId).DisplayName;
                                    deptTracking.StartDate = department;
                                    deptTracking.CreatedDate = department;
                                    deptTracking.CreatedBy = objUI.UpdatedBy;
                                    deptTracking.UpdatedDate = department;
                                    deptTracking.UpdatedBy = objUI.UpdatedBy;
                                    EmployeeDepartmentJobTitleTrackingDao deptDao = new EmployeeDepartmentJobTitleTrackingDao();
                                    deptDao.Insert(deptTracking);
                                }
                            }
                            else
                            {
                                EmployeeDepartmentJobTitleTrackingDao deptDao = new EmployeeDepartmentJobTitleTrackingDao();
                                if (objDb.DepartmentId != objUI.DepartmentId)
                                {
                                    EmployeeDepartmentJobTitleTracking deptTracking = new EmployeeDepartmentJobTitleTracking();
                                    deptTracking.EmployeeId = objDb.ID;
                                    deptTracking.DepartmentName = new DepartmentDao().GetById(objUI.DepartmentId).DepartmentName;
                                    deptTracking.JobTitleName = new JobTitleLevelDao().GetById(objDb.TitleId).DisplayName;
                                    deptTracking.StartDate = department;
                                    deptTracking.CreatedDate = department;
                                    deptTracking.CreatedBy = objUI.UpdatedBy;
                                    deptTracking.UpdatedDate = department;
                                    deptTracking.UpdatedBy = objUI.UpdatedBy;
                                    deptDao.Insert(deptTracking);
                                }
                                if (objDb.TitleId != objUI.TitleId)
                                {
                                    
                                    EmployeeDepartmentJobTitleTracking titleTracking = new EmployeeDepartmentJobTitleTracking();
                                    titleTracking.EmployeeId = objDb.ID;
                                    titleTracking.DepartmentName = new DepartmentDao().GetById(objUI.DepartmentId).DepartmentName;
                                    titleTracking.JobTitleName = new JobTitleLevelDao().GetById(objUI.TitleId).DisplayName;
                                    titleTracking.StartDate = title;
                                    titleTracking.CreatedDate = title;
                                    titleTracking.CreatedBy = objUI.UpdatedBy;
                                    titleTracking.UpdatedDate = title;
                                    titleTracking.UpdatedBy = objUI.UpdatedBy;
                                    deptDao.Insert(titleTracking);
                                }
                            }

                            if (objDb.InsuranceHospitalID != objUI.InsuranceHospitalID && hospital != null)
                            {
                                if (hospital != new DateTime())
                                {
                                    EmployeeInsuranceHospitalTracking hospitalTracking = new EmployeeInsuranceHospitalTracking();
                                    hospitalTracking.EmployeeId = objDb.ID;
                                    hospitalTracking.InsuranceHospitalID = objUI.InsuranceHospitalID;
                                    hospitalTracking.StartDate = hospital;
                                    hospitalTracking.CreatedDate = hospital;
                                    hospitalTracking.CreatedBy = objUI.UpdatedBy;
                                    hospitalTracking.UpdatedDate = hospital;
                                    hospitalTracking.UpdatedBy = objUI.UpdatedBy;
                                    InsuranceHospitalDao hospitalDao = new InsuranceHospitalDao();
                                    hospitalDao.Insert(hospitalTracking);
                                    objDb.InsuranceHospitalID = objUI.InsuranceHospitalID;
                                }
                            }
                            objDb.DepartmentId = objUI.DepartmentId;
                            objDb.TitleId = objUI.TitleId;
                            objDb.LaborUnion = objUI.LaborUnion;
                            objDb.LaborUnionDate = objUI.LaborUnionDate;
                            objDb.HomePhone = objUI.HomePhone;
                            objDb.CellPhone = objUI.CellPhone;
                            objDb.Floor = objUI.Floor;
                            objDb.ExtensionNumber = objUI.ExtensionNumber;
                            objDb.SeatCode = objUI.SeatCode;
                            objDb.SkypeId = objUI.SkypeId;
                            objDb.YahooId = objUI.YahooId;
                            objDb.PersonalEmail = objUI.PersonalEmail;
                            objDb.OfficeEmail = objUI.OfficeEmail;
                            objDb.EmergencyContactName = objUI.EmergencyContactName;
                            objDb.EmergencyContactPhone = objUI.EmergencyContactPhone;
                            objDb.EmergencyContactRelationship = objUI.EmergencyContactRelationship;
                            objDb.PermanentAddress = objUI.PermanentAddress;
                            objDb.PermanentArea = objUI.PermanentArea;
                            objDb.PermanentDistrict = objUI.PermanentDistrict;
                            objDb.PermanentCityProvince = objUI.PermanentCityProvince;
                            objDb.PermanentCountry = objUI.PermanentCountry;
                            objDb.TempAddress = objUI.TempAddress;
                            objDb.TempArea = objUI.TempArea;
                            objDb.TempDistrict = objUI.TempDistrict;
                            objDb.TempCityProvince = objUI.TempCityProvince;
                            objDb.TempCountry = objUI.TempCountry;
                            objDb.BankName = objUI.BankName;
                            objDb.BankAccount = objUI.BankAccount;
                            objDb.Remarks = objUI.Remarks;
                            objDb.UpdateDate = DateTime.Now;
                            objDb.UpdatedBy = objUI.UpdatedBy;
                            objDb.VnFirstName = objUI.VnFirstName;
                            objDb.VnMiddleName = objUI.VnMiddleName;
                            objDb.VnLastName = objUI.VnLastName;
                            objDb.VnPOB = objUI.VnPOB;
                            objDb.VnPlaceOfOrigin = objUI.VnPlaceOfOrigin;
                            objDb.Degree = objUI.Degree;
                            objDb.Race = objUI.Race;
                            objDb.IDIssueLocation = objUI.IDIssueLocation;
                            objDb.VnIDIssueLocation = objUI.VnIDIssueLocation;
                            objDb.TaxID = objUI.TaxID;
                            objDb.TaxIssueDate = objUI.TaxIssueDate;
                            objDb.SocialInsuranceNo = objUI.SocialInsuranceNo;
                            objDb.InsuranceHospitalID = objUI.InsuranceHospitalID;
                            objDb.VnPermanentAddress = objUI.VnPermanentAddress;
                            objDb.VnPermanentArea = objUI.VnPermanentArea;
                            objDb.VnPermanentDistrict = objUI.VnPermanentDistrict;
                            objDb.VnPermanentCityProvince = objUI.VnPermanentCityProvince;
                            objDb.VnPermanentCountry = objUI.VnPermanentCountry;
                            objDb.VnTempAddress = objUI.VnTempAddress;
                            objDb.VnTempArea = objUI.VnTempArea;
                            objDb.VnTempDistrict = objUI.VnTempDistrict;
                            objDb.VnTempCityProvince = objUI.VnTempCityProvince;
                            objDb.VnTempCountry = objUI.VnTempCountry;
                            objDb.ResignedAllowance = objUI.ResignedAllowance;
                            objDb.ResignedDate = objUI.ResignedDate;
                            objDb.ResignedReason = objUI.ResignedReason;
                            objDb.LocationCode = objUI.LocationCode;
                            // Submit changes to dbContext
                            dbContext.SubmitChanges();
                            // Show success message
                            msg = new Message(MessageConstants.I0001, MessageType.Info, "Employee '" + objDb.FirstName + " " + objDb.MiddleName + " " + objDb.LastName + "'", "updated");

                        }
                    }
                }
            }
            catch(Exception ex)
            {                             // Show system error
                msg = new Message(MessageConstants.E0007, MessageType.Error);
            }
            return msg;
        }

        public Message UpdateFlag(Employee objUI)
        {
            Message msg = null;
            if (objUI != null)
            {
                Employee objDb = GetById(objUI.ID);
                if (objDb != null)
                {
                    objDb.UpdateDate = DateTime.Now;
                    objDb.UpdatedBy = objUI.UpdatedBy;
                    objDb.DeleteFlag = true;
                    dbContext.SubmitChanges();
                    msg = new Message(MessageConstants.I0001, MessageType.Info, "Employee '" + objDb.FirstName + " " + objDb.MiddleName + " " + objDb.LastName + "'", "updated");
                }
            }
            return msg;
        }

        /// <summary>
        /// Check update date whether is valid
        /// </summary>
        /// <param name="objUI"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        private bool IsValidUpdateDate(Employee objUI, Employee objDb, out Message msg)
        {
            bool isValid = false;
            msg = null;

            try
            {
                if ((objUI != null) && (objUI.UpdateDate != null))
                {
                    if (objDb != null)
                    {
                        if (objDb.UpdateDate.ToString().Equals(objUI.UpdateDate.ToString()))
                        {
                            isValid = true;
                        }
                        else
                        {
                            msg = new Message(MessageConstants.E0025, MessageType.Error, "Employee ID " + objDb.ID);
                        }
                    }
                }
            }
            catch
            {
                throw;
            }

            return isValid;
        }

        public Message UpdateLaborUnion(Employee objUI)
        {
            Message msg = null;
            try
            {
                if (objUI != null)
                {
                    // Get current group in dbContext
                    Employee objDb = GetById(objUI.ID);

                    if (objDb != null)
                    {
                        
                        //new EmployeeLogDao().WriteLogForUpdateImage(objUI, ELogAction.Update);
                        // Update info by objUI                       
                        objDb.LaborUnion = objUI.LaborUnion;
                        objDb.LaborUnionDate = objUI.LaborUnionDate;
                        objDb.UpdateDate = DateTime.Now;
                        objDb.UpdatedBy = objUI.UpdatedBy; 
                        // Submit changes to dbContext
                        dbContext.SubmitChanges();
                        // Show success message
                        msg = new Message(MessageConstants.I0001, MessageType.Info, "Employee photo", "uploaded");
                    }
                }
            }
            catch
            {
                msg = new Message(MessageConstants.E0007, MessageType.Error);
            }
            return msg;
        }

        /*public List<Employee> GetListEmployeeAndSTT(string filterText, string deptId, string subDeptId, string titleId, string locationCode, string project )
        {
            DepartmentDao deptDao = new DepartmentDao ();
            filterText = filterText.Trim().ToLower();
            
            project = project.Trim().ToLower(); ;
            var empList = dbContext.Employees.OrderBy(q => q.ID).Where( p=> 
                    !p.DeleteFlag &&
                    (!p.EmpStatusId.HasValue ||
                    (p.EmpStatusId.HasValue && p.EmpStatusId.Value != Constants.RESIGNED))
                    ).ToList();
            var sttList = dbContext.STTs.OrderBy(q => q.ID).Where(p=>!p.DeleteFlag)
                .ToList().ParseEmployeeList();
            if (!string.IsNullOrEmpty(filterText))
            {
                empList = empList.Where(p => CommonFunc.GetEmployeeFullName(p, 
                    Constants.FullNameFormat.FirstMiddleLast).ToLower().Contains(filterText) ||
                    p.ID.ToLower().Contains(filterText)).ToList();
                sttList = sttList.Where(p => CommonFunc.GetEmployeeFullName(p,
                    Constants.FullNameFormat.FirstMiddleLast).ToLower().Contains(filterText) ||
                    p.ID.ToLower().Contains(filterText)).ToList();
            }
            empList.AddRange(sttList);
            if (!string.IsNullOrEmpty(deptId))
            {
                empList = empList.Where(p => p.Department.ParentId == int.Parse(deptId)).ToList();
            }
            if (!string.IsNullOrEmpty(subDeptId))
            {
                empList = empList.Where(p => p.DepartmentId == int.Parse(subDeptId)).ToList();
            }
            if (!string.IsNullOrEmpty(titleId))
            {
                JobTitleLevel titleLevel = new JobTitleLevelDao().GetById(int.Parse(titleId));
                if (titleLevel.DisplayName.ToLower().Contains(Constants.STT_ID_PREFIX.Trim('-').ToLower()))
                {
                    empList = empList.Where(p => p.ID.ToLower().
                        Contains(Constants.STT_ID_PREFIX.ToLower())).ToList();
                }
                else
                {
                    empList = empList.Where(p => p.TitleId == int.Parse(titleId)).ToList();
                }
            }
            if (!string.IsNullOrEmpty(locationCode))
                empList = empList.Where(p => !string.IsNullOrEmpty(p.LocationCode) &&  
                    CommonFunc.ContainsExact(p.LocationCode, locationCode)).ToList();
            if (!string.IsNullOrEmpty(project))
                empList = empList.Where(p => !string.IsNullOrEmpty(p.Project)
                    && p.Project.Trim().ToLower().Equals(project)).ToList();
            return empList;
        }*/
        public List<sp_GetPositionResult> GetListEmployeeAndSTT(string filterText, int deptId, int subDeptId, int titleId, string project, string locationCode)
        {
            return dbContext.sp_GetPosition(filterText, deptId, subDeptId, titleId, project, 
                Constants.JOB_TITLE_LEVEL_STT_ID, locationCode).OrderByDescending(p => p.ID).ToList<sp_GetPositionResult>();
        }
        public List<sp_GetPositionResult> Sort(List<sp_GetPositionResult> empList, string sortColumn, string sortOrder)
        {
            int order;
            DepartmentDao depDao = new DepartmentDao();
            JobTitleLevelDao levelDao = new JobTitleLevelDao();
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
                    empList.Sort(
                         delegate(sp_GetPositionResult m1, sp_GetPositionResult m2)
                         { return m1.ID.CompareTo(m2.ID) * order; });
                    break;
                case "DisplayName":
                    empList.Sort(
                         delegate(sp_GetPositionResult m1, sp_GetPositionResult m2)
                         {
                             return m1.FullName.CompareTo(m2.FullName)
                             * order;
                         });
                    break;
                case "JobTitle":
                    empList.Sort(
                         delegate(sp_GetPositionResult m1, sp_GetPositionResult m2)
                         { 
                             string sTitle1 = m1.TitleId.HasValue ? levelDao.GetById(m1.TitleId.Value).DisplayName : "";
                             string sTitle2 = m2.TitleId.HasValue ? levelDao.GetById(m2.TitleId.Value).DisplayName : "";
                             return sTitle1.CompareTo(sTitle2) * order;
                         });
                    break;
                case "DirectManager":
                    empList.Sort(
                         delegate(sp_GetPositionResult m1, sp_GetPositionResult m2)
                         {
                             string displayname1 = "";
                             string displayname2 = "";
                             if (!string.IsNullOrEmpty(m1.ManagerId))
                             {
                                 //Employee emp1 = GetById(m1.ManagerId);
                                 //if (emp1 != null)
                                 //{
                                 displayname1 = dbContext.GetEmployeeFullName(m1.ManagerId, (int)Constants.FullNameFormat.FirstMiddleLast);
                                 //}
                             }
                             if (!string.IsNullOrEmpty(m2.ManagerId))
                             {
                                 //Employee emp2 = GetById(m2.ManagerId);
                                 //if (emp2 != null)
                                 //{
                                 displayname2 = dbContext.GetEmployeeFullName(m2.ManagerId, (int)Constants.FullNameFormat.FirstMiddleLast);
                                 //}
                             }
                             return displayname1.CompareTo(displayname2) * order;
                         });
                    break;
                case "Project":
                    empList.Sort(
                         delegate(sp_GetPositionResult m1, sp_GetPositionResult m2)
                         {
                             string project1 = string.IsNullOrEmpty(m1.Project) ? "" : m1.Project;
                             string project2 = string.IsNullOrEmpty(m2.Project) ? "" : m2.Project;
                             return project1.CompareTo(project2) * order;
                         });
                    break;
                
                case "NextReviewDate":
                    empList.Sort(
                         delegate(sp_GetPositionResult m1, sp_GetPositionResult m2)
                         {
                             DateTime? date1 = GetNextReviewDate(m1.ID);
                             DateTime? date2 = GetNextReviewDate(m2.ID);
                             if (!date1.HasValue)
                                 date1 = DateTime.MinValue;
                             if (!date2.HasValue)
                                 date2 = DateTime.MinValue;
                             return date1.Value.CompareTo(date2.Value) * order;
                         });
                    break;                
            }
            return empList;
        }

        public List<Employee> Sort(List<Employee> empList, string sortColumn, string sortOrder)
        {
            int order;
            DepartmentDao depDao=new DepartmentDao ();
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
                    empList.Sort(
                         delegate(Employee m1, Employee m2)
                         { return m1.ID.CompareTo(m2.ID) * order; });
                    break;
                case "DisplayName":
                    empList.Sort(
                         delegate(Employee m1, Employee m2)
                         {
                             return CommonFunc.GetEmployeeFullName(m1, Constants.FullNameFormat.FirstMiddleLast).
                             CompareTo(CommonFunc.GetEmployeeFullName(m2, Constants.FullNameFormat.FirstMiddleLast)) 
                             * order;
                         });
                    break;
                case "DirectManager":
                    empList.Sort(
                         delegate(Employee m1, Employee m2)
                         {
                             string displayname1 = "";
                             string displayname2 = "";
                             if (!string.IsNullOrEmpty(m1.ManagerId))
                             {
                                 //Employee emp1 = GetById(m1.ManagerId);
                                 //if (emp1 != null)
                                 //{
                                 displayname1 = dbContext.GetEmployeeFullName(m1.ManagerId, (int)Constants.FullNameFormat.FirstMiddleLast);
                                 //}
                             }
                             if (!string.IsNullOrEmpty(m2.ManagerId))
                             {
                                 //Employee emp2 = GetById(m2.ManagerId);
                                 //if (emp2 != null)
                                 //{
                                 displayname2 = dbContext.GetEmployeeFullName(m2.ManagerId, (int)Constants.FullNameFormat.FirstMiddleLast);
                                 //}
                             }
                             return displayname1.CompareTo(displayname2) * order; 
                         });
                    break;
                case "Project":
                    empList.Sort(
                         delegate(Employee m1, Employee m2)
                         {
                             string project1 = string.IsNullOrEmpty(m1.Project) ? "" : m1.Project;
                             string project2 = string.IsNullOrEmpty(m2.Project) ? "" : m2.Project;
                             return project1.CompareTo(project2) * order;
                         });
                    break;
                case "SeatCode":
                    empList.Sort(
                         delegate(Employee m1, Employee m2)
                         {
                             string st1 = string.IsNullOrEmpty(m1.SeatCode) ? "" : m1.SeatCode;
                             string st2 = string.IsNullOrEmpty(m2.SeatCode) ? "" : m2.SeatCode;
                             return st1.CompareTo(st2) * order;
                         });
                    break;
                case "Floor":
                    empList.Sort(
                         delegate(Employee m1, Employee m2)
                         {
                             string f1 = string.IsNullOrEmpty(m1.Floor) ? "" : m1.Floor;
                             string f2 = string.IsNullOrEmpty(m2.Floor) ? "" : m2.Floor;
                             return f1.CompareTo(f2) * order;
                         });
                    break;
                case "NextReviewDate":
                    empList.Sort(
                         delegate(Employee m1, Employee m2)
                         {
                             DateTime? date1 = GetNextReviewDate(m1.ID);
                             DateTime? date2 = GetNextReviewDate(m2.ID);
                             if (!date1.HasValue)
                                 date1 = DateTime.MinValue;
                             if (!date2.HasValue)
                                 date2 = DateTime.MinValue;
                             return date1.Value.CompareTo(date2.Value) * order;
                         });
                    break;
                case "Department":
                    empList.Sort(
                         delegate(Employee m1, Employee m2)
                         {
                             string d1 = depDao.GetById(m1.Department.ParentId.Value).DepartmentName;
                             string d2 = depDao.GetById(m2.Department.ParentId.Value).DepartmentName;
                             return d1.CompareTo(d2) * order;
                         });
                    break;
                case "LoginName":
                    empList.Sort(
                         delegate(Employee m1, Employee m2)
                         {
                             string s1 = CommonFunc.GetLoginNameByEmail(m1.OfficeEmail);
                             string s2 = CommonFunc.GetLoginNameByEmail(m2.OfficeEmail);
                             return s1.CompareTo(s2) * order;
                         });
                    break;
                case "ManagerLoginName":
                    empList.Sort(
                         delegate(Employee m1, Employee m2)
                         {
                             string s1 = CommonFunc.GetLoginNameByEmail(m1.Employee1.OfficeEmail);
                             string s2 = CommonFunc.GetLoginNameByEmail(m2.Employee1.OfficeEmail);
                             return s1.CompareTo(s2) * order;
                         });
                    break;
            }
            return empList;
        }
        public DateTime? GetNextReviewDate(string empId)
        { 
            empId = empId.Trim().ToLower();
            if(string.IsNullOrEmpty(empId))
                return null;
            var list = dbContext.PerformanceReviews.Where(p => 
                p.EmployeeID.Trim().ToLower().Equals(empId)).OrderByDescending(p=>p.NextReviewDate).ToList();
            if (list.Count > 0)
                return list.FirstOrDefault().NextReviewDate;
            return null;
        }

        public Message UpdatePosition(Employee objUI, bool onlyLocation = false)
        {
            Message msg = null;
            try
            {

                Employee emp = GetById(objUI.ID);
                if (emp != null)
                {
                    new EmployeeLogDao().WriteLogForUpdatePosition(objUI, ELogAction.Update);
                    if (!onlyLocation)
                    {
                        emp.ManagerId = objUI.ManagerId;
                        emp.Project = objUI.Project;
                    }
                    emp.Floor = objUI.Floor;
                    emp.SeatCode = objUI.SeatCode;
                    emp.LocationCode = objUI.LocationCode;
                    dbContext.SubmitChanges();
                    msg = new Message(MessageConstants.I0001, MessageType.Info, "Employee " + emp.ID, "updated");
                }
            }
            catch
            {
                msg =  new Message(MessageConstants.E0033, MessageType.Error);
            }

            return msg;
        }

        public List<Employee> GetLikeName(string managerName)
        {
            managerName = Regex.Replace(managerName.Trim().ToLower(), @"\b\s+\b", " ");
            string[] arrManagerName = managerName.Split(' ', '.');
            List<Employee> managerList = GetListManagerWithAllAttr("");
            foreach (string part in arrManagerName)
            {
                managerList = managerList.Where(p => p.FirstName.ToLower().Trim().Contains(part) ||
                    (!string.IsNullOrEmpty(p.MiddleName) ? p.MiddleName.ToLower().Trim().Contains(part) : false) ||
                    p.LastName.ToLower().Trim().Contains(part)).ToList();
            }
            return managerList;
        }

        public Message ImportResignedEmp(Employee emp)
        {
            try
            {
                dbContext.Employees.InsertOnSubmit(emp);
                dbContext.SubmitChanges();
                return new Message(MessageConstants.I0001, MessageType.Info, "Employee", "imported");
            }
            catch (Exception ex)
            {
                return new Message(MessageConstants.E0033, MessageType.Error, ex.Message);
            }
        }
        public List<Employee> GetListByManagerId(string mgrId)
        {
            mgrId = mgrId.Trim().ToLower();
            return dbContext.Employees.Where(p => p.ManagerId.Trim().ToLower().Equals(mgrId)).ToList();
        }
        public List<sp_GetEmployeesForPRResult> GetPRList(string mgrId, string filterText, string status, bool needPR)
        {
            if (string.IsNullOrEmpty(status))
                status = "0";
            filterText = filterText.Trim().ToLower();
            var result = dbContext.sp_GetEmployeesForPR(mgrId, int.Parse(status)).Where(p=>!p.DeleteFlag).ToList();
            if (!string.IsNullOrEmpty(filterText))
                result = result.Where(p => p.ID.Contains(filterText) || string.IsNullOrEmpty(filterText) ||
                    FullName(p.ID, Constants.FullNameFormat.FirstMiddleLast).ToLower().Contains(filterText) ||
                    (p.FirstName + " " + p.LastName).ToLower().Contains(filterText) ||
                    p.OfficeEmail.Substring(0, p.OfficeEmail.IndexOf("@")).ToLower().Contains(filterText)).ToList();
            if (needPR)
                result = result.Where(p => IsNeedPR(p.ID, Constants.PER_REVIEW_LOCKED_DAYS)).ToList();
            return result;
        }
        public bool IsNeedPR(string empId, int days)
        {
            Employee emp = GetById(empId);
            PerformanceReviewDao perDao=new PerformanceReviewDao ();
            List<PerformanceReview> prList = perDao.GetListByEmployeeId(emp.ID).OrderByDescending(p=>p.CreateDate).ToList();
            //Return true if there's no record
            if (prList.Count == 0)
                return true;
            //Return false if there's at least 1 PR in "Open" status
            bool isOpen = false;
            foreach (var item in prList)
            {
                if (item.WFStatusID == Constants.STATUS_OPEN)
                {
                    isOpen = true;
                    break;
                }
            }
            if (isOpen)
                return false;
            else if( !isOpen && days==0 )
                return true;
            else if (prList.FirstOrDefault().NextReviewDate.HasValue && days != 0)
            {
                PerformanceReview pr = prList.FirstOrDefault();
                if (pr.NextReviewDate.Value < DateTime.Now)
                    return true;
                int iNum = pr.NextReviewDate.Value.Subtract(DateTime.Now).Days;
                if(iNum < 0 || iNum <= days)
                {
                    return true;
                }
            }
            return false;
        }
        public List<sp_GetEmployeesForPRResult> Sort(List<sp_GetEmployeesForPRResult> empList, string sortColumn, string sortOrder)
        {
            int order;
            DepartmentDao depDao = new DepartmentDao();
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
                    empList.Sort(
                         delegate(sp_GetEmployeesForPRResult m1, sp_GetEmployeesForPRResult m2)
                         { return m1.ID.CompareTo(m2.ID) * order; });
                    break;
                case "NextReviewDate":
                    empList.Sort(
                         delegate(sp_GetEmployeesForPRResult m1, sp_GetEmployeesForPRResult m2)
                         {
                             DateTime? date1 = GetNextReviewDate(m1.ID);
                             DateTime? date2 = GetNextReviewDate(m2.ID);
                             if (!date1.HasValue)
                                 date1 = DateTime.MinValue;
                             if (!date2.HasValue)
                                 date2 = DateTime.MinValue;
                             return date1.Value.CompareTo(date2.Value) * order;
                         });
                    break;
                case "Department":
                    empList.Sort(
                         delegate(sp_GetEmployeesForPRResult m1, sp_GetEmployeesForPRResult m2)
                         {
                             string d1 = depDao.GetDepartmentNameBySub(m1.DepartmentId);
                             string d2 = depDao.GetDepartmentNameBySub(m2.DepartmentId);
                             return d1.CompareTo(d2) * order;
                         });
                    break;
                case "LoginName":
                    empList.Sort(
                         delegate(sp_GetEmployeesForPRResult m1, sp_GetEmployeesForPRResult m2)
                         {
                             string s1 = FullName(m1.ID, Constants.FullNameFormat.FirstMiddleLast);
                             string s2 = FullName(m2.ID, Constants.FullNameFormat.FirstMiddleLast);
                             return s1.CompareTo(s2) * order;
                         });
                    break;
                case "ManagerLoginName":
                    empList.Sort(
                         delegate(sp_GetEmployeesForPRResult m1, sp_GetEmployeesForPRResult m2)
                         {
                             string s1 = string.IsNullOrEmpty(m1.ManagerId) ? 
                                 "" : FullName(m1.ManagerId, Constants.FullNameFormat.FirstMiddleLast);
                             string s2 = string.IsNullOrEmpty(m2.ManagerId) ? 
                                 "" : FullName(m2.ManagerId, Constants.FullNameFormat.FirstMiddleLast);
                             return s1.CompareTo(s2) * order;
                         });
                    break;
            }
            return empList;
        }
        /// <summary>
        /// Used for import Project, Manager, Seat code, Floor
        /// </summary>
        /// <param name="firstName"></param>
        /// <param name="lastName"></param>
        /// <returns></returns>
        public List<Employee> GetManagerByFirstAndLastName(string firstName, string lastName)
        {
            firstName = firstName.ToLower().Trim();
            lastName = lastName.ToLower().Trim();
            return dbContext.Employees.
                Where(p => p.FirstName.ToLower().Equals(firstName) && p.LastName.ToLower().Equals(lastName) 
                    && p.JobTitleLevel.JobTitle.IsManager && p.EmpStatusId != Constants.RESIGNED ).ToList();
        }

        public Message UpdatePMSF(Employee emp)
        {
            try
            {
                Employee empDB = GetById(emp.ID);
                if (empDB == null)
                    return new Message(MessageConstants.E0033, MessageType.Error, "Employee does not exist !");
                else
                {
                    empDB.Project = emp.Project;
                    empDB.ManagerId = emp.ManagerId;
                    empDB.SeatCode = emp.SeatCode;
                    empDB.Floor = emp.Floor;
                    dbContext.SubmitChanges();
                    return new Message(MessageConstants.I0001, MessageType.Info, "PMSF ", "updated");
                }
            }
            catch(Exception ex)
            {
                return new Message(MessageConstants.E0033, MessageType.Error, ex.Message);
            }
        }

        /*public List<string> GetFloorList()
        {
            var list = GetListEmployeeAndSTT("", "", "", "", "", "").Select(p => p.Floor).Distinct().ToList();
            list = list.Where(p=>!string.IsNullOrEmpty(p)).ToList();
            return list;
        }*/
        public List<string> GetProjectList()
        {
            var list = GetListEmployeeAndSTT("", 0, 0, 0, null, null).Select(p => p.Project).Distinct().ToList();
            list = list.Where(p => !string.IsNullOrEmpty(p)).ToList();
            return list;
        }

        public List<sp_GetListEmployeeSttNotSeatResult> GetListEmployeeNotSeat(string name)
        {
            List<sp_GetListEmployeeSttNotSeatResult> list = dbContext.sp_GetListEmployeeSttNotSeat(name).ToList();    
            
            return list;
        }
        public List<sp_GetEmployeeSTTListResult> GetEmpSttList(string name)
        {
            return dbContext.sp_GetEmployeeSTTList(name).ToList<sp_GetEmployeeSTTListResult>();
        }
    }
}
