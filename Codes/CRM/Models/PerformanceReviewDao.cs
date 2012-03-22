using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CRM.Library.Common;
using System.Data.Common;
using System.Text.RegularExpressions;

namespace CRM.Models
{
    /// <summary>
    /// Performance review dao
    /// </summary>
    public class PerformanceReviewDao:BaseDao
    {
        /// <summary>
        /// PR Model for an item on list for manager
        /// </summary>
        public class PR_Model
        {
            /// <summary>
            /// PR_Model
            /// </summary>
            /// <param name="id"></param>
            /// <param name="empId"></param>
            /// <param name="empName"></param>
            /// <param name="departmentName"></param>
            /// <param name="managerName"></param>
            /// <param name="nextPrDate"></param>
            /// <param name="updateDate"></param>
            public PR_Model(string id, string empId, string empName, string departmentName,
                string managerName, DateTime? nextPrDate, DateTime updateDate)
            {
                this.id = id;
                this.empId = empId;
                this.empName = empName;
                this.departmentName = departmentName;
                //this.department = department;
                this.managerName = managerName;
                //this.manager = manager;
                this.nextPrDate = nextPrDate;
                this.updateDate = updateDate;
            }
            private string id;
            public string ID
            {
                get { return id; }
                set { id = value; }
            }
            private string empId;
            public string EmpId
            {
                get { return empId; }
                set { empId = value; }
            }
            private DateTime? nextPrDate;
            public DateTime? NextPrDate
            {
                get { return nextPrDate; }
                set { nextPrDate = value; }
            }
            private string empName;
            public string EmpName
            {
                get { return empName; }
                set { empName = value; }
            }
            private string managerName;
            public string ManagerName
            {
                get { return managerName; }
                set { managerName = value; }
            }
            private string departmentName;
            public string DepartmentName
            {
                get { return departmentName; }
                set { departmentName = value; }
            }
            private DateTime updateDate;
            public DateTime UpdateDate
            {
                get { return updateDate; }
                set { updateDate = value; }
            }
        }

        public List<PerformanceReview> GetListPRByEmployeeID(string empID)
        {
            return dbContext.PerformanceReviews.Where(q => q.EmployeeID == empID).ToList();
        }

        /// <summary>
        /// Insert comment
        /// </summary>
        /// <param name="obj"></param>
        public void InsertComment(PRComment obj)
        {
            if (obj != null)
            {
                dbContext.PRComments.InsertOnSubmit(obj);
                dbContext.SubmitChanges();
            }
        }

        /// <summary>
        /// Get list for manager
        /// </summary>
        /// <param name="empNameOrId"></param>
        /// <param name="status"></param>
        /// <param name="needSetup"></param>
        /// <returns></returns>
        public Message UpdateResolution(PerformanceReview objUI, PRComment objComment)
        {
            Message msg = null;
            DbTransaction trans = null;
            dbContext.Connection.Open();
            trans = dbContext.Connection.BeginTransaction();
            dbContext.Transaction = trans;
            try
            {
                PerformanceReview objDb = GetById(objUI.ID);
                if (objDb != null)
                {
                    objDb.WFResolutionID = objUI.WFResolutionID;
                    objDb.AssignID = objUI.ManagerID;
                    objDb.AssignRole = objUI.AssignRole;
                    objDb.InvolveID = objUI.InvolveID;
                    objDb.InvolveRole = objUI.InvolveRole;
                    objDb.InvolveDate = objUI.InvolveDate;
                    objDb.InvolveResolution = objUI.InvolveResolution;
                    objDb.UpdatedBy = objUI.UpdatedBy;
                    objDb.UpdateDate = DateTime.Now;
                    if (objComment != null)
                    {
                        InsertComment(objComment);
                    }
                    dbContext.SubmitChanges();
                    msg = new Message(MessageConstants.I0001, MessageType.Info, "Performance Review '" + objUI.ID + "'", "updated");
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
        /// Get list for manager
        /// </summary>
        /// <param name="empNameOrId"></param>
        /// <param name="status"></param>
        /// <param name="needSetup"></param>
        /// <returns></returns>
        public List<PR_Model> GetListForManager(string empNameOrId, string status, string needSetup)
        {
            empNameOrId = empNameOrId.Trim().ToLower();
            var empList = dbContext.Employees.Where(p => p.DeleteFlag == false).ToList();
            var perList = dbContext.PerformanceReviews.Where(p => p.DeleteFlag == false).ToList();
            if (!string.IsNullOrEmpty(empNameOrId))
            {
                empList = empList.Where(p => 
                    CommonFunc.GetLoginNameByEmail(p.OfficeEmail).Contains(empNameOrId) ||
                    p.ID.Contains(empNameOrId)).ToList();
            }
            if (!string.IsNullOrEmpty(status))
            {
                perList = perList.Where(p => p.WFStatusID.ToString().Equals(status)).ToList();
            }
            if (!string.IsNullOrEmpty(needSetup))
            {
                //perList = perList.Where(p => p.WFStatusID.ToString().Equals(status)).ToList();
            }
            var result = empList.Join(perList, e => e.ID, p => p.EmployeeID,
                (e, p) => new PR_Model
                (
                    p.ID,
                    e.ID, 
                    CommonFunc.GetLoginNameByEmail(e.OfficeEmail),
                    e.Department.DepartmentName, 
                    CommonFunc.GetLoginNameByEmail(p.Employee.OfficeEmail), 
                    p.NextReviewDate,
                    p.UpdateDate
                )).ToList();
            
            return result;
        }

        /// <summary>
        /// Get sub list performance review by employee id
        /// </summary>
        /// <param name="empId"></param>
        /// <returns></returns>
        public List<sp_GetSubListPerformanceReviewHRResult> GetListPRByEmployeeId(string empId)
        {
            return dbContext.sp_GetSubListPerformanceReviewHR(empId).ToList();

        }

        /// <summary>
        /// Get performance review by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public PerformanceReview GetById(string id)
        {
            return dbContext.PerformanceReviews.Where(p => p.ID.Equals(id)).FirstOrDefault();
        }

        /// <summary>
        /// Update performance review
        /// </summary>
        /// <param name="per">PerformanceReview</param>
        /// <returns>Message</returns>
        public Message UpdatePerformanceReviewForHr(PerformanceReview objUI)
        {
            Message msg = null;
            try
            {

                if (objUI != null)
                {
                    PerformanceReview objDb = GetById(objUI.ID);
                    if (objDb != null)
                    {
                        if (objDb.UpdateDate.ToString() == objUI.UpdateDate.ToString())
                        {
                            objDb.WFStatusID = objUI.WFStatusID;
                            objDb.WFResolutionID = objUI.WFResolutionID;
                            objDb.AssignID = objUI.AssignID;
                            objDb.AssignRole = objUI.AssignRole;
                            objDb.InvolveID = objUI.InvolveID;
                            objDb.InvolveRole = objUI.InvolveRole;
                            objDb.InvolveDate = objUI.InvolveDate;
                            objDb.InvolveResolution = objUI.InvolveResolution;
                            objDb.UpdateDate = DateTime.Now;
                            dbContext.SubmitChanges();

                            msg = new Message(MessageConstants.I0001, MessageType.Info, "Performance review '" + objUI.ID + "'", "updated");
                        }
                        else
                        {
                            msg = new Message(MessageConstants.E0025, MessageType.Error, "Performance review '"  + objUI.ID + "'");
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

        /// <summary>
        /// Get list performance review by empoyee id
        /// </summary>
        /// <param name="empId"></param>
        /// <returns></returns>
        public List<PerformanceReview> GetListByEmployeeId(string empId)
        {
            return dbContext.PerformanceReviews.Where(p => p.EmployeeID.Equals(empId)).ToList();
        }

        /// <summary>
        /// Sort list
        /// </summary>
        /// <param name="list"></param>
        /// <param name="sortColumn"></param>
        /// <param name="sortOrder"></param>
        /// <returns></returns>
        public List<PR_Model> Sort(List<PR_Model> list, string sortColumn, string sortOrder)
        {
            int order = 1;
            //DepartmentDao depDao=new DepartmentDao ();
            if (sortOrder == "desc")
            {
                order = -1;
            }
            switch (sortColumn)
            {
                case "EmpId":
                    list.Sort(
                         delegate(PR_Model pr1, PR_Model pr2)
                         { return pr1.EmpId.CompareTo(pr2.EmpId) * order; });
                    break;
                case "EmpName":
                    list.Sort(
                         delegate(PR_Model pr1, PR_Model pr2)
                         { return pr1.EmpName.CompareTo(pr2.EmpName) * order; });
                    break;
                case "Department":
                    list.Sort(
                         delegate(PR_Model pr1, PR_Model pr2)
                         { return pr1.DepartmentName.CompareTo(pr2.DepartmentName) * order; });
                    break;
                case "Manager":
                    list.Sort(
                         delegate(PR_Model pr1, PR_Model pr2)
                         {
                             return pr1.ManagerName.CompareTo(pr2.ManagerName) * order;
                         });
                    break;
                case "NextPrDate":
                    list.Sort(
                         delegate(PR_Model pr1, PR_Model pr2)
                         {
                             DateTime d1 = DateTime.MinValue;
                             if (pr1.NextPrDate.HasValue)
                                 d1 = pr1.NextPrDate.Value;
                             DateTime d2 = DateTime.MinValue;
                             if (pr2.NextPrDate.HasValue)
                                 d2 = pr2.NextPrDate.Value;
                             return d1.CompareTo(d2) * order;
                         });
                    break;
                case "UpdateDate":
                    list.Sort(
                         delegate(PR_Model pr1, PR_Model pr2)
                         {
                             return pr1.UpdateDate.CompareTo(pr2.UpdateDate) * order;
                         });
                    break;
            }
            return list;
        }

        /// <summary>
        /// Get list performance review
        /// </summary>
        /// <param name="text"></param>
        /// <param name="department"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public List<sp_GetPerformanceReviewListResult> GetList(string text, int department, int status)
        {
            return dbContext.sp_GetPerformanceReviewList(text, department, status).ToList();
        }

        /// <summary>
        /// Get list performance review by assign role
        /// </summary>
        /// <param name="text">string</param>
        /// <param name="department">int</param>
        /// <param name="status">int</param>
        /// <param name="assignRole">string</param>
        /// <returns>List<sp_GetPerformanceReviewListResult></returns>
        public List<sp_GetEmployeesPRForHRResult> GetList(string text, int status, bool needPR, string assignId)
        {
            int iDay = 0;
            if (needPR)
                iDay = Constants.PER_REVIEW_LOCKED_DAYS;
            return dbContext.sp_GetEmployeesPRForHR(text, status, iDay).ToList();
        }

        /// <summary>
        /// Sort list performance review for HR
        /// </summary>
        /// <param name="list">sp_GetEmployeesPRForHRResult</param>
        /// <param name="sortColumn">string</param>
        /// <param name="sortOrder">string</param>
        /// <returns>sp_GetEmployeesPRForHRResult</returns>
        public List<sp_GetEmployeesPRForHRResult> Sort(List<sp_GetEmployeesPRForHRResult> list, string sortColumn, string sortOrder)
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
                         delegate(sp_GetEmployeesPRForHRResult m1, sp_GetEmployeesPRForHRResult m2)
                         { return m1.ID.CompareTo(m2.ID) * order; });
                    break;
                case "LoginName":
                    list.Sort(
                         delegate(sp_GetEmployeesPRForHRResult m1, sp_GetEmployeesPRForHRResult m2)
                         { return m1.EmployeeName.CompareTo(m2.EmployeeName) * order; });
                    break;
                case "Department":
                    list.Sort(
                         delegate(sp_GetEmployeesPRForHRResult m1, sp_GetEmployeesPRForHRResult m2)
                         { return m1.Department.CompareTo(m2.Department) * order; });
                    break;
                case "ManagerName":
                    list.Sort(
                         delegate(sp_GetEmployeesPRForHRResult m1, sp_GetEmployeesPRForHRResult m2)
                         { return m1.ManagerName.CompareTo(m2.ManagerName) * order; });
                    break;
                
            }
            return list;
        }

        /// <summary>
        /// Sort list performance review
        /// </summary>
        /// <param name="list">sp_GetPerformanceReviewListResult</param>
        /// <param name="sortColumn">string</param>
        /// <param name="sortOrder">string</param>
        /// <returns>sp_GetPerformanceReviewListResult</returns>
        public List<sp_GetPerformanceReviewListResult> Sort(List<sp_GetPerformanceReviewListResult> list, string sortColumn, string sortOrder)
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
                         delegate(sp_GetPerformanceReviewListResult m1, sp_GetPerformanceReviewListResult m2)
                         { return m1.ID.CompareTo(m2.ID) * order; });
                    break;
                case "Employee":
                    list.Sort(
                         delegate(sp_GetPerformanceReviewListResult m1, sp_GetPerformanceReviewListResult m2)
                         { return m1.EmployeeName.CompareTo(m2.EmployeeName) * order; });
                    break;
                case "Manager":
                    list.Sort(
                         delegate(sp_GetPerformanceReviewListResult m1, sp_GetPerformanceReviewListResult m2)
                         { return m1.ManagerName.CompareTo(m2.ManagerName) * order; });
                    break;
                case "PRDate":
                    list.Sort(
                         delegate(sp_GetPerformanceReviewListResult m1, sp_GetPerformanceReviewListResult m2)
                         { return m1.PRDate.CompareTo(m2.PRDate) * order; });
                    break;
                case "ResolutionName":
                    list.Sort(
                         delegate(sp_GetPerformanceReviewListResult m1, sp_GetPerformanceReviewListResult m2)
                         { return m1.ResolutionName.CompareTo(m2.ResolutionName) * order; });
                    break;
                case "Assign":
                    list.Sort(
                         delegate(sp_GetPerformanceReviewListResult m1, sp_GetPerformanceReviewListResult m2)
                         { return m1.AssignName.CompareTo(m2.AssignName) * order; });
                    break;
            }
            return list;
        }

        /// <summary>
        /// Get Assign List By Resolution ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<sp_GetListAssignByResolutionIdResult> GetListAssign(int id)
        {
            return dbContext.sp_GetListAssignByResolutionId(id, Constants.WORK_FLOW_PERFORMANCE_REVIEW).ToList<sp_GetListAssignByResolutionIdResult>();
        }

        /// <summary>
        /// Insert performance review
        /// </summary>
        /// <param name="pr">PerformanceReview</param>
        /// <param name="eformMasterId">string</param>
        /// <returns>Message</returns>
        public Message Insert(PerformanceReview pr, string eformMasterId)
        {
            Message msg = null;
            EmployeeDao empDao = new EmployeeDao();
            EformDao eformDao = new EformDao();
            DbTransaction trans = null;
            try
            {
                if (dbContext.Connection.State == System.Data.ConnectionState.Closed)
                {
                    dbContext.Connection.Open();
                }
                trans = dbContext.Connection.BeginTransaction();
                dbContext.Transaction = trans;

                EForm eform = new EForm();
                eform.MasterID = eformMasterId;
                eform.PersonID = pr.EmployeeID;
                eform.PersonType = (int)Constants.PersonType.Employee;
                eform.FormIndex = 1;
                eform.CreateDate = DateTime.Now;
                eform.CreatedBy = pr.CreatedBy;
                dbContext.EForms.InsertOnSubmit(eform);
                dbContext.SubmitChanges();

                pr.ID = GetNewPrId(pr.EmployeeID);
                pr.EFormID = eform.ID;
                dbContext.PerformanceReviews.InsertOnSubmit(pr);
                
                msg = new Message(MessageConstants.I0001, MessageType.Info, "PR", "setup");
                dbContext.SubmitChanges();
                trans.Commit();
            }
            catch
            {
                trans.Rollback();
                msg = new Message(MessageConstants.E0007, MessageType.Error);
            }
            return msg;
        }
        public Message Update(PerformanceReview pr, string eformMasterId)
        {
            Message msg = null;
            EmployeeDao empDao = new EmployeeDao();
            EformDao eformDao = new EformDao();
            
            DbTransaction trans = null;
            try
            {
                if (dbContext.Connection.State == System.Data.ConnectionState.Closed)
                {
                    dbContext.Connection.Open();
                }
                trans = dbContext.Connection.BeginTransaction();
                dbContext.Transaction = trans;
                EForm oldEform = null;
                PerformanceReview prDB = GetById(pr.ID);
                //Update eform
                EForm eform = new EForm();
                if (!pr.EForm.MasterID.Equals(eformMasterId))
                {
                    //Delete old eform
                    oldEform = pr.EForm;
                    //Insert new eform
                    eform.MasterID = eformMasterId;
                    eform.PersonID = pr.EmployeeID;
                    eform.PersonType = (int)Constants.PersonType.Employee;
                    eform.FormIndex = 1;
                    eform.CreateDate = DateTime.Now;
                    eform.CreatedBy = pr.CreatedBy;
                    dbContext.EForms.InsertOnSubmit(eform);
                    dbContext.SubmitChanges();
                    pr.EForm = eform;
                }
                //Update performance Review
                prDB.AssignID = pr.AssignID;
                prDB.EForm = pr.EForm;
                prDB.AssignRole = pr.AssignRole;
                prDB.CCEmail = pr.CCEmail;
                prDB.InvolveDate = pr.InvolveDate;
                prDB.InvolveID = pr.InvolveID;
                prDB.InvolveResolution = pr.InvolveResolution;
                prDB.InvolveRole = pr.InvolveRole;
                prDB.NextReviewDate = pr.NextReviewDate;
                prDB.PRDate = pr.PRDate;
                prDB.UpdateDate = DateTime.Now;
                prDB.UpdatedBy = pr.UpdatedBy;
                prDB.WFResolutionID = pr.WFResolutionID;
                if(oldEform!=null)
                    dbContext.EForms.DeleteOnSubmit(oldEform);
                dbContext.SubmitChanges();
                msg = new Message(MessageConstants.I0001, MessageType.Info, 
                    "Performance Review \"" + pr.ID + "\"", "updated");
                trans.Commit();
            }
            catch
            {
                trans.Rollback();
                msg = new Message(MessageConstants.E0007, MessageType.Error);
            }
            return msg;
        }
        /// <summary>
        /// Get new PR Id
        /// </summary>
        /// <param name="empId">employee id</param>
        /// <returns>string</returns>
        public string GetNewPrId(string empId)
        {
            string sep = Constants.PER_REVIEW_ID_SEPARATOR;
            string result = Constants.PER_REVIEW_ID_PREFIX + empId + sep;
            int[] arrId = GetListByEmployeeId(empId).
                Select(p => int.Parse(Regex.Match(p.ID, @"\d+", RegexOptions.RightToLeft).Value)).ToArray();
            if (arrId.Length != 0)
                result += (arrId.Max() + 1);
            else
                result += 1;
            return result;
        }

        /// <summary>
        /// Update holder message
        /// </summary>
        /// <param name="prUI">PerformanceReview</param>
        /// <returns>Message</returns>
        public Message UpdateHolder(PerformanceReview prUI)
        {
            try
            {
                PerformanceReview prDB = GetById(prUI.ID);
                prDB.AssignID = prUI.AssignID;
                prDB.AssignRole = prUI.AssignRole;
                prDB.InvolveID = prUI.InvolveID;
                prDB.InvolveDate = prUI.InvolveDate;
                prDB.InvolveResolution = prUI.InvolveResolution;
                prDB.InvolveRole = prUI.InvolveRole;
                prDB.WFResolutionID = prUI.WFResolutionID;
                dbContext.SubmitChanges();
                return new Message(MessageConstants.I0001, MessageType.Info, prDB.ID, "updated");
            }
            catch
            {
                return new Message(MessageConstants.E0007, MessageType.Error);
            }
        }

        public List<PRComment> GetListComment(string prId)
        {
            return dbContext.PRComments.Where(p=> p.PRID.Equals(prId)).ToList();
        }

        
    }
}