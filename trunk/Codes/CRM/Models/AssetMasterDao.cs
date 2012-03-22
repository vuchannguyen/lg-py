using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CRM.Library.Common;
using CRM.Library.Utils;
using System.Data.Common;
using System.Text.RegularExpressions;
using System.Linq.Dynamic;

namespace CRM.Models
{
    public class AssetMasterDao : BaseDao
    {
        #region Get List
        public List<sp_GetAssetMasterResult> GetList(string name, int category, int status, string project)
        {
            return dbContext.sp_GetAssetMaster(name, category, status, project).OrderByDescending(p => p.AssetId).ToList<sp_GetAssetMasterResult>();
        }
        public List<sp_GetAssetMasterResult> GetListById(string name, int category, int status, string project)
        {
            return dbContext.sp_GetAssetMaster(name, category, status, project).OrderByDescending(p => p.ID).ToList<sp_GetAssetMasterResult>();
        }

        public List<sp_GetEmployeeProjectResult> GetEmployeeProject()
        {
            return dbContext.sp_GetEmployeeProject().OrderByDescending(p => p.Project).ToList<sp_GetEmployeeProjectResult>();
        }
        public List<sp_GetEmployeeProjectResult> GetProjectList()
        {
            return dbContext.sp_GetEmployeeProject().ToList<sp_GetEmployeeProjectResult>();
        }

        public List<sp_GetEmployeeForAssetManagementResult> GetEmployeeListForAssetManagement(string text, int departmentId, int jobTitleId, string project)
        {
            return dbContext.sp_GetEmployeeForAssetManagement(text, departmentId, jobTitleId, project, Constants.RESIGNED).ToList<sp_GetEmployeeForAssetManagementResult>();
        }

        //Get list export to excel for sub list
        public List<sp_GetEmployeeAssetManagementForExportResult> GetEmployeeAssetListForExport(string text, int departmentId, int jobTitleId, string project)
        {
            return dbContext.sp_GetEmployeeAssetManagementForExport(text, departmentId, jobTitleId, project, Constants.RESIGNED).ToList<sp_GetEmployeeAssetManagementForExportResult>();
        }

        public List<AssetMaster> GetListByCategoryId(int categoryId)
        {
            return dbContext.AssetMasters.Where(p => p.CategoryId == categoryId).ToList<AssetMaster>();
        }

        public List<AssetMaster> GetListByTableEmployeeId(string employeeId)
        {
            return dbContext.AssetMasters.Where(p => p.EmployeeId == employeeId).ToList<AssetMaster>();
        }

        public List<sp_GetEmployeeAssetResult> GetEmployeeAssetListByEmployeeId(string employeeId)
        {
            return dbContext.sp_GetEmployeeAsset(employeeId).Where(p => p.EmployeeId.Equals(employeeId)).ToList<sp_GetEmployeeAssetResult>();
        }
        public List<AssetMaster> GetList()
        {
            return dbContext.AssetMasters.Where(c => c.CategoryId == null && c.DeleteFlag == false).OrderBy(p => p.ID).ToList<AssetMaster>();
        }
        public List<AssetMaster> GetAssetMasterList()
        {
            return dbContext.AssetMasters.ToList();
        }

        public List<sp_GetAssetMasterPropertyResult> GetAssetMasterPropertyList(string id)
        {
            return dbContext.sp_GetAssetMasterProperty(id).OrderByDescending(p => p.ID).ToList<sp_GetAssetMasterPropertyResult>();
        }

        public List<sp_GetEmployeeNameResult> GetEmployeeNameList(string empName)
        {
            return dbContext.sp_GetEmployeeName().Where(e => (e.DisplayName + " - " + e.ID).Equals(empName)).ToList<sp_GetEmployeeNameResult>();
        }
        #endregion

        #region Get item
        public sp_GetAssetMasterResult GetItemById(string Id)
        {
            return dbContext.sp_GetAssetMaster(string.Empty, null, null, string.Empty).Where(p => p.ID.Equals(Id)).FirstOrDefault<sp_GetAssetMasterResult>();
        }
        public AssetMaster GetById(string id)
        {
            return dbContext.AssetMasters.Where(c => c.ID.Equals(id)).FirstOrDefault<AssetMaster>();
        }
        public AssetMaster GetByAssetId(string assetId)
        {
            return dbContext.AssetMasters.Where(c => c.AssetID.Equals(assetId)).FirstOrDefault<AssetMaster>();
        }
        #endregion

        #region Sort
        public List<sp_GetAssetMasterResult> Sort(List<sp_GetAssetMasterResult> list, string sortColumn, string sortOrder)
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
                case "AssetId":
                    list.Sort(
                         delegate(sp_GetAssetMasterResult m1, sp_GetAssetMasterResult m2)
                         { return m1.AssetId.CompareTo(m2.AssetId) * order; });
                    break;

                case "AssetCategoryName":
                    list.Sort(
                         delegate(sp_GetAssetMasterResult m1, sp_GetAssetMasterResult m2)
                         { return m1.AssetCategoryName.CompareTo(m2.AssetCategoryName) * order; });
                    break;
                case "UserName":
                    list.Sort(
                         delegate(sp_GetAssetMasterResult m1, sp_GetAssetMasterResult m2)
                         { return m1.UserName.CompareTo(m2.UserName) * order; });
                    break;
                case "StatusName":
                    list.Sort(
                         delegate(sp_GetAssetMasterResult m1, sp_GetAssetMasterResult m2)
                         { return m1.StatusName.CompareTo(m2.StatusName) * order; });
                    break;
                case "Remark":
                    list.Sort(
                         delegate(sp_GetAssetMasterResult m1, sp_GetAssetMasterResult m2)
                         { return m1.Remark.CompareTo(m2.Remark) * order; });
                    break;
                case "IsActive":
                    list.Sort(
                         delegate(sp_GetAssetMasterResult m1, sp_GetAssetMasterResult m2)
                         { return m1.IsActive.CompareTo(m2.IsActive) * order; });
                    break;
            }

            return list;
        }

        public List<sp_GetEmployeeAssetResult> EmployeeAssetSublistSort(List<sp_GetEmployeeAssetResult> list, string sortColumn, string sortOrder)
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
                case "AssetID":
                    list.Sort(
                         delegate(sp_GetEmployeeAssetResult m1, sp_GetEmployeeAssetResult m2)
                         { return m1.AssetId.CompareTo(m2.AssetId) * order; });
                    break;

                case "Category":
                    list.Sort(
                         delegate(sp_GetEmployeeAssetResult m1, sp_GetEmployeeAssetResult m2)
                         { return m1.AssetCategoryName.CompareTo(m2.AssetCategoryName) * order; });
                    break;
                case "Status":
                    list.Sort(
                         delegate(sp_GetEmployeeAssetResult m1, sp_GetEmployeeAssetResult m2)
                         { return m1.StatusName.CompareTo(m2.StatusName) * order; });
                    break;
                case "Remark":
                    list.Sort(
                         delegate(sp_GetEmployeeAssetResult m1, sp_GetEmployeeAssetResult m2)
                         { return m1.Remark.CompareTo(m2.Remark) * order; });
                    break;
            }

            return list;
        }

        public List<sp_GetEmployeeForAssetManagementResult> EmployeeAssetSort(List<sp_GetEmployeeForAssetManagementResult> list, string sortColumn, string sortOrder)
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
                case "Displayname":
                    list.Sort(
                         delegate(sp_GetEmployeeForAssetManagementResult m1, sp_GetEmployeeForAssetManagementResult m2)
                         { return m1.DisplayName.CompareTo(m2.DisplayName) * order; });
                    break;

                case "Department":
                    list.Sort(
                         delegate(sp_GetEmployeeForAssetManagementResult m1, sp_GetEmployeeForAssetManagementResult m2)
                         { return m1.Department.CompareTo(m2.Department) * order; });
                    break;
                case "SeatCode":
                    list.Sort(
                         delegate(sp_GetEmployeeForAssetManagementResult m1, sp_GetEmployeeForAssetManagementResult m2)
                         { return m1.SeatCode.CompareTo(m2.SeatCode) * order; });
                    break;
                case "TitleName":
                    list.Sort(
                         delegate(sp_GetEmployeeForAssetManagementResult m1, sp_GetEmployeeForAssetManagementResult m2)
                         { return m1.TitleName.CompareTo(m2.TitleName) * order; });
                    break;
                case "Project":
                    list.Sort(
                         delegate(sp_GetEmployeeForAssetManagementResult m1, sp_GetEmployeeForAssetManagementResult m2)
                         { return m1.Project.CompareTo(m2.Project) * order; });
                    break;
            }

            return list;
        }

        //Sort for export sublist
        public List<sp_GetEmployeeAssetManagementForExportResult> EmployeeAssetSortExport(List<sp_GetEmployeeAssetManagementForExportResult> list, string sortColumn, string sortOrder)
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
                case "Displayname":
                    list.Sort(
                         delegate(sp_GetEmployeeAssetManagementForExportResult m1, sp_GetEmployeeAssetManagementForExportResult m2)
                         { return m1.DisplayName.CompareTo(m2.DisplayName) * order; });
                    break;

                case "Department":
                    list.Sort(
                         delegate(sp_GetEmployeeAssetManagementForExportResult m1, sp_GetEmployeeAssetManagementForExportResult m2)
                         { return m1.Department.CompareTo(m2.Department) * order; });
                    break;
                case "SeatCode":
                    list.Sort(
                         delegate(sp_GetEmployeeAssetManagementForExportResult m1, sp_GetEmployeeAssetManagementForExportResult m2)
                         { return m1.SeatCode.CompareTo(m2.SeatCode) * order; });
                    break;
                case "JobTitle":
                    list.Sort(
                         delegate(sp_GetEmployeeAssetManagementForExportResult m1, sp_GetEmployeeAssetManagementForExportResult m2)
                         { return m1.TitleName.CompareTo(m2.TitleName) * order; });
                    break;
                case "Project":
                    list.Sort(
                         delegate(sp_GetEmployeeAssetManagementForExportResult m1, sp_GetEmployeeAssetManagementForExportResult m2)
                         { return m1.Project.CompareTo(m2.Project) * order; });
                    break;
            }

            return list;
        }

        #endregion

        public Message UpdateActiveStatus(long assId, bool isActive)
        {
            try
            {
                AssetMaster obj = dbContext.AssetMasters.Where(p => p.ID == assId).FirstOrDefault();
                if (obj == null)
                    return new Message(MessageConstants.E0005, MessageType.Error,
                        "Selected Asset", "system");
                obj.IsActive = isActive;
                dbContext.SubmitChanges();
                return new Message(MessageConstants.I0001, MessageType.Info, "Asset " + obj.ID +
                    " in group \"" + obj.ID + "\"", "set " + (isActive ? "active" : "inactive"));
            }
            catch
            {
                return new Message(MessageConstants.E0007, MessageType.Error);
            }
        }

        private bool IsDublicateAssetId(AssetMaster objUI)
        {
            bool isDublicateName = true;
            AssetMaster dublicateName = dbContext.AssetMasters.Where(a => a.AssetID.Equals(objUI.AssetID) && a.DeleteFlag == false).FirstOrDefault<AssetMaster>();
            if (dublicateName == null || dublicateName.ID == objUI.ID)
            {
                isDublicateName = false;
            }
            return isDublicateName;
        }

        private bool IsDBChanged(AssetMaster objUI, AssetMaster objDb)
        {
            bool isChannged = true;
            if (objDb.UpdateDate.ToString() == objUI.UpdateDate.ToString())
            {
                isChannged = false;
            }
            return isChannged;
        }

        public Message Insert(AssetMaster objUI)
        {
            Message msg = null;
            try
            {
                if (!IsDublicateAssetId(objUI))
                {
                    dbContext.AssetMasters.InsertOnSubmit(objUI);
                    dbContext.SubmitChanges();
                    msg = new Message(MessageConstants.I0001, MessageType.Info, "Asset Master '" + objUI.AssetID + "'", "added");
                }
                else
                {
                    msg = new Message(MessageConstants.E0020, MessageType.Error, "Name '" + objUI.AssetID + "'", "Asset Master");
                }
            }
            catch (Exception)
            {
                msg = new Message(MessageConstants.E0007, MessageType.Error);
            }
            return msg;
        }

        public Message Update(AssetMaster objUI)
        {
            Message msg = null;
            try
            {
                AssetMaster objDb = GetById(objUI.ID.ToString());

                if (objDb != null)
                {
                    Update(objUI, objDb, ref msg);
                }
                else
                {
                    msg = new Message(MessageConstants.E0040, MessageType.Error, "Asset Category '" + objUI.ID + "'");
                }
            }
            catch (Exception)
            {
                msg = new Message(MessageConstants.E0007, MessageType.Error);
            }

            return msg;
        }

        private void Update(AssetMaster objUI, AssetMaster objDb, ref Message msg)
        {
            if (!IsDBChanged(objUI, objDb))
            {
                if (!IsDublicateAssetId(objUI))
                {
                    objDb.AssetID = objUI.AssetID;
                    objDb.CategoryId = objUI.CategoryId;
                    objDb.StatusId = objUI.StatusId;
                    objDb.EmployeeId = (String.IsNullOrEmpty(objUI.EmployeeId)) ? null : objUI.EmployeeId;
                    objDb.Remark = objUI.Remark;
                    objDb.IsActive = objUI.IsActive;
                    objDb.UpdateDate = DateTime.Now;
                    objDb.UpdatedBy = objUI.UpdatedBy;

                    dbContext.SubmitChanges();
                    msg = new Message(MessageConstants.I0001, MessageType.Info, "Asset Master '" + objUI.AssetID + "'", "updated");
                }
                else
                {
                    msg = new Message(MessageConstants.E0020, MessageType.Error, "'" + objUI.AssetID + "'", "Asset Property");
                }
            }
            else
            {
                msg = new Message(MessageConstants.E0025, MessageType.Error, "Asset Master'" + objDb.AssetID + "'");
            }
        }

        private void Delete(AssetMaster objUI)
        {
            if (objUI != null)
            {
                // Get current group in dbContext
                AssetMaster objDb = GetById(objUI.ID.ToString());
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
                        AssetMaster assMas = GetById(empID);
                        if (assMas != null)
                        {
                            assMas.UpdatedBy = userName;
                            Delete(assMas);
                        }
                        else
                        {
                            total--;
                        }
                    }

                    // Show succes message
                    msg = new Message(MessageConstants.I0001, MessageType.Info, total.ToString() + " Asset(s)", "deleted");
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

        #region Assign Asset To Employee

        public List<sp_GetAssetForAssignToEmployeeResult> GetAssignAssetToEmpListById(string name, int category)
        {
            return dbContext.sp_GetAssetForAssignToEmployee(name, category).Where(p => p.IsActive == true).OrderByDescending(p => p.AssetId).ToList<sp_GetAssetForAssignToEmployeeResult>();
        }

        public List<sp_GetAssetForAssignToEmployeeInUseResult> GetAssignAssetToEmpListInUse(string employeeID)
        {
            return dbContext.sp_GetAssetForAssignToEmployeeInUse(employeeID).ToList<sp_GetAssetForAssignToEmployeeInUseResult>();
        }

        public List<sp_GetAssetForAssignToEmployeeResult> SortForAssetAvailable(List<sp_GetAssetForAssignToEmployeeResult> list, string sortColumn, string sortOrder)
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
                case "AssetId":
                    list.Sort(
                         delegate(sp_GetAssetForAssignToEmployeeResult m1, sp_GetAssetForAssignToEmployeeResult m2)
                         { return m1.AssetId.CompareTo(m2.AssetId) * order; });
                    break;

                case "AssetCategoryName":
                    list.Sort(
                         delegate(sp_GetAssetForAssignToEmployeeResult m1, sp_GetAssetForAssignToEmployeeResult m2)
                         { return m1.AssetCategoryName.CompareTo(m2.AssetCategoryName) * order; });
                    break;
               
                case "StatusName":
                    list.Sort(
                         delegate(sp_GetAssetForAssignToEmployeeResult m1, sp_GetAssetForAssignToEmployeeResult m2)
                         { return m1.StatusName.CompareTo(m2.StatusName) * order; });
                    break;
                case "Remark":
                    list.Sort(
                         delegate(sp_GetAssetForAssignToEmployeeResult m1, sp_GetAssetForAssignToEmployeeResult m2)
                         { return m1.Remark.CompareTo(m2.Remark) * order; });
                    break;
            }

            return list;
        }

        public List<sp_GetAssetForAssignToEmployeeInUseResult> SortForAssetInUse(List<sp_GetAssetForAssignToEmployeeInUseResult> list, string sortColumn, string sortOrder)
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
                case "AssetId":
                    list.Sort(
                         delegate(sp_GetAssetForAssignToEmployeeInUseResult m1, sp_GetAssetForAssignToEmployeeInUseResult m2)
                         { return m1.AssetId.CompareTo(m2.AssetId) * order; });
                    break;

                case "AssetCategoryName":
                    list.Sort(
                         delegate(sp_GetAssetForAssignToEmployeeInUseResult m1, sp_GetAssetForAssignToEmployeeInUseResult m2)
                         { return m1.AssetCategoryName.CompareTo(m2.AssetCategoryName) * order; });
                    break;

                case "StatusName":
                    list.Sort(
                         delegate(sp_GetAssetForAssignToEmployeeInUseResult m1, sp_GetAssetForAssignToEmployeeInUseResult m2)
                         { return m1.StatusName.CompareTo(m2.StatusName) * order; });
                    break;
                case "Remark":
                    list.Sort(
                         delegate(sp_GetAssetForAssignToEmployeeInUseResult m1, sp_GetAssetForAssignToEmployeeInUseResult m2)
                         { return m1.Remark.CompareTo(m2.Remark) * order; });
                    break;
            }

            return list;
        }

        public Message Assign(AssetMaster objUI)
        {
            Message msg = null;
            try
            {
                AssetMaster objDb = GetById(objUI.ID.ToString());

                if (objDb != null)
                {
                    Assign(objUI, objDb, ref msg);
                }
                else
                {
                    msg = new Message(MessageConstants.E0040, MessageType.Error, "Asset '" + objUI.ID + "'");
                }
            }
            catch (Exception)
            {
                msg = new Message(MessageConstants.E0007, MessageType.Error);
            }

            return msg;
        }

        private void Assign(AssetMaster objUI, AssetMaster objDb, ref Message msg)
        {
            if (IsDBChanged(objUI, objDb))
            {
                if (!IsDublicateAssetId(objUI))
                {
                    objDb.AssetID = objDb.AssetID;
                    objDb.CategoryId = objDb.CategoryId;
                    objDb.StatusId = Constants.ASSIGN_ASSET_INUSE_STATUS;
                    objDb.EmployeeId = (String.IsNullOrEmpty(objUI.EmployeeId)) ? null : objUI.EmployeeId; ;
                    objDb.Remark = objDb.Remark;
                    objDb.IsActive = objDb.IsActive;
                    objDb.UpdateDate = DateTime.Now;
                    objDb.UpdatedBy = objDb.UpdatedBy;

                    dbContext.SubmitChanges();
                }
            }
        }


        public Message ReAssign(AssetMaster objUI)
        {
            Message msg = null;
            try
            {
                AssetMaster objDb = GetById(objUI.ID.ToString());

                if (objDb != null)
                {
                    ReAssign(objUI, objDb, ref msg);
                }
                else
                {
                    msg = new Message(MessageConstants.E0040, MessageType.Error, "Asset '" + objUI.ID + "'");
                }
            }
            catch (Exception)
            {
                msg = new Message(MessageConstants.E0007, MessageType.Error);
            }

            return msg;
        }

        private void ReAssign(AssetMaster objUI, AssetMaster objDb, ref Message msg)
        {
            if (IsDBChanged(objUI, objDb))
            {
                if (!IsDublicateAssetId(objUI))
                {
                    objDb.AssetID = objDb.AssetID;
                    objDb.CategoryId = objDb.CategoryId;
                    objDb.StatusId = Constants.ASSIGN_ASSET_AVAILABLE_STATUS;
                    objDb.EmployeeId = Constants.ASSIGN_ASSET_EMPLOYEE_NULL;
                    objDb.Remark = objDb.Remark;
                    objDb.IsActive = objDb.IsActive;
                    objDb.UpdateDate = DateTime.Now;
                    objDb.UpdatedBy = objDb.UpdatedBy;

                    dbContext.SubmitChanges();
                }
            }
        }


        public void ResignAllAssetOfEmployee( string id)
        {
            List<AssetMaster> empAssetList = this.GetListByTableEmployeeId(id);
            foreach (AssetMaster empAss in empAssetList)
            {
                empAss.StatusId = Constants.ASSIGN_ASSET_AVAILABLE_STATUS;
                empAss.EmployeeId = Constants.ASSIGN_ASSET_EMPLOYEE_NULL;
                dbContext.SubmitChanges();
            }
        }

        #endregion

        #region Project Asset

        public string GetEmployeeFullName(string empId)
        {
            if (string.IsNullOrEmpty(empId))
                return string.Empty;
            return dbContext.GetEmployeeFullName(empId, 1).ToString();
        }
        public IQueryable<Entities.ProjectAssetEntity> GetProjectAssetQueryList(string searchText, int categoryId, int departmentId, string project)
        {
            var sql = from asset in dbContext.AssetMasters 
                      join emp in dbContext.Employees on asset.EmployeeId equals emp.ID
                      join manager in dbContext.Employees on emp.ManagerId equals manager.ID into emps from manager in emps.DefaultIfEmpty()
                      where asset.DeleteFlag == false
                        && asset.StatusId != Constants.ASSIGN_ASSET_AVAILABLE_STATUS
                        && asset.Employee.EmpStatusId != Constants.RESIGNED
                      select new Entities.ProjectAssetEntity { 
                        Id = asset.ID,
                        AssetID = asset.AssetID,
                        CategoryId =asset.CategoryId,
                        CategoryName = asset.AssetCategory.Name,
                        DepartmentId = asset.Employee.DepartmentId,
                        DepartmentName = asset.Employee.Department.DepartmentName,
                        EmployeeId = asset.EmployeeId,
                        EmployeeName = asset.EmployeeId == null ? string.Empty : asset.Employee.MiddleName == null ? asset.Employee.FirstName + " " + asset.Employee.LastName + " - " + asset.EmployeeId : asset.Employee.FirstName + " " + asset.Employee.MiddleName +" " + asset.Employee.LastName + " - " + asset.EmployeeId,
                        EmployeeOfficeEmail = asset.EmployeeId == null ? string.Empty : asset.Employee.OfficeEmail,
                        ManagerId = asset.Employee.ManagerId,
                        ManagerName = emp.ManagerId == null ? string.Empty : manager.MiddleName == null ? manager.FirstName + " " + manager.LastName + " - " + manager.ID : manager.FirstName + " " + manager.MiddleName + " " + manager.LastName + " - " + manager.ID,
                        ManagerOfficeEmail = emp.ManagerId == null ? string.Empty : manager.OfficeEmail,
                        Project = asset.Employee.Project == null ? "Un-assigned Project" : asset.Employee.Project,
                        Remark = asset.Remark,
                        SeatCode = dbContext.GetEmployeeSeatCode(emp.LocationCode),
                        StatusId = asset.StatusId,
                        StatusName = asset.AssetStatus.StatusName
                      };

            if (searchText != string.Empty)
            {
                sql = sql.Where(a => a.EmployeeId.Contains(searchText)
                                  || a.EmployeeName.Contains(searchText)
                                  || a.EmployeeOfficeEmail.Contains(searchText)
                                  || a.ManagerId.Contains(searchText)
                                  || a.ManagerName.Contains(searchText)
                                  || a.ManagerOfficeEmail.Contains(searchText)
                                  || a.Project.Contains(searchText)
                                  || a.SeatCode.Contains(searchText)
                                  || a.AssetID.Contains(searchText)
                                );
            }
            if (project != string.Empty)
            {
                sql = sql.Where(a => (a.Project.Contains(project)));
            }
            if (categoryId >0)
            {
                sql = sql.Where(a => (a.CategoryId == categoryId));
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

        public int GetProjectAssetCountList(string searchText, int categoryId, int subDepartmentId, string project)
        {
            return GetProjectAssetQueryList(searchText, categoryId, subDepartmentId, project).Count();
        }

        public List<Entities.ProjectAssetEntity> GetProjectAssetList(string searchText, int categoryId, int subDepartmentId, string project, string sortColumn, string sortOrder, int skip, int take)
        {
            var sql = GetProjectAssetQueryList(searchText, categoryId, subDepartmentId, project);
            switch (sortColumn)
            {
                case "AssetId":
                    sql = sql.OrderBy("Project asc, AssetId " + sortOrder);
                    break;
                case "Category":
                    sql = sql.OrderBy("Project asc, CategoryName " + sortOrder);
                    break;
                case "Status":
                    sql = sql.OrderBy("Project asc, StatusName " + sortOrder);
                    break;
                case "AssignTo":
                    sql = sql.OrderBy("Project asc, EmployeeName " + sortOrder);
                    break;
                case "Manager":
                    sql = sql.OrderBy("Project asc, ManagerName " + sortOrder);
                    break;
                case "Department":
                    sql = sql.OrderBy("Project asc, DepartmentName " + sortOrder);
                    break;
                case "SeatCode":
                    sql = sql.OrderBy("Project asc, SeatCode " + sortOrder);
                    break;
                case "Project":
                    sql = sql.OrderBy("Project " + sortOrder);
                    break;
                case "Remark":
                    sql = sql.OrderBy("Project asc, Remark " + sortOrder);
                    break;
                default:
                    sql = sql.OrderBy("Project asc, " + sortColumn + " " + sortOrder);
                    break;
            }

            return sql.Skip(skip).Take(take).ToList();
        }

        #endregion

    }
}