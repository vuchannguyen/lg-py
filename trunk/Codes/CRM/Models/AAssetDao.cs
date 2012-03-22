using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.Linq.SqlClient;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using CRM.Library.Common;
using CRM.Models.Entities;
using System.Web.Mvc;

namespace CRM.Models
{
    public class AAssetDao : CustomBaseDao<A_Asset>
    {
        #region Variables
        private ACategoryDao _aCategoryDao = new ACategoryDao();
        private AAllocationDao _aAllocationDao = new AAllocationDao();
        #endregion

        #region Tai.Pham

        #region CommonFunc - Tai.Pham
        /// <summary>
        /// Converting param string to List APropertyParam
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public List<APropertyParam> GetPropertyParams(string str)
        {
            List<APropertyParam> list = new List<APropertyParam>();
            if (string.IsNullOrEmpty(str))
                return list;
            str = str.Trim(Constants.A_ASSET_SEPARATION_PROPERTY_MASTER_DATA);
            foreach (string s in str.Split(Constants.A_ASSET_SEPARATION_PROPERTY_MASTER_DATA))
            {
                string[] kv = s.Split(Constants.A_ASSET_SEPARATION_PROPERTY_KEY_VALUE);
                if(kv.Length == 2 && CheckUtil.IsNaturalNumber(kv[0]))
                {
                    list.Add(new APropertyParam(ConvertUtil.ConvertToLong(kv[0]), kv[1]));
                }
            }
            return list;
        }
        #endregion

        public AAssetEntity GetFullInfoById(long id, int userId)
        {
            List<string> gpStr = (from gp in PermissionCommon.GetListPermissionOfUser(userId)
                  select gp.ModuleId.ToString() + ";" + gp.PermissionId).ToList();

            AAssetEntity aAsset = new AAssetEntity();
            aAsset.AAsset = dbContext.A_Assets.Where(p => !p.DeleteFlag && p.ID == id).FirstOrDefault();
            if (aAsset.AAsset == null)
                return null;
            aAsset.Properties =
                (from v in dbContext.A_AssetPropertyValues
                join pp in dbContext.A_PropertyPermissions on v.PropertyId equals pp.PropertyId into j
                from jj in j.DefaultIfEmpty()
                where v.AssetId == aAsset.AAsset.ID && jj == null
                select v).ToList();
            aAsset.PropertiesByPermission =
                (from v in dbContext.A_AssetPropertyValues
                 join pp in dbContext.A_PropertyPermissions on v.PropertyId equals pp.PropertyId into j
                 from jj in j.DefaultIfEmpty()
                 where v.AssetId == aAsset.AAsset.ID && jj != null
                       && gpStr.Contains(jj.ModuleId.ToString() + ";" + jj.PermissionId)
                 select v).ToList();

            return aAsset;
        }

        public A_Asset GetById(long id)
        {
            return dbContext.A_Assets.Where(p => !p.DeleteFlag && p.ID == id).FirstOrDefault<A_Asset>();
        }

        public List<A_Asset> GetSubAssets(long pId, string sortColumn, string sortOrder)
        {
            var sql = GetQuerySubAssets(pId);
            switch (sortColumn)
            {
                case "ID":
                    sql = sql.OrderBy(p => p.AssetId);
                    break;
                case "Category":
                    sql = sql.OrderBy(p => p.A_Category.Name);
                    break;
            }

            return sql.ToList();
        }

        private IQueryable<A_Asset> GetQuerySubAssets(long pId)
        {
            var sql =
                from a in dbContext.A_Assets
                where !a.DeleteFlag && a.ParentId == pId
                select a;
            return sql;
        }

        /// <summary>
        /// Deleting by delete flag
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Message DeleteAsset(long id, string userName)
        {
            Message msg = null;
            DbTransaction trans = null;
            try
            {
                if (dbContext.Connection.State != ConnectionState.Open)
                {
                    dbContext.Connection.Open();
                }
                trans = dbContext.Connection.BeginTransaction();
                dbContext.Transaction = trans;

                int total = DeleteById(id, userName);
                trans.Commit();
                msg = new Message(MessageConstants.I0001, MessageType.Info, total.ToString() + (total >= 2 ? " Assets" : " Asset"), "deleted");
            }
            catch (Exception ex)
            {
                if (trans != null) trans.Rollback();
                msg = new Message(MessageConstants.E0007, MessageType.Error);
            }
            dbContext.Connection.Close();
            return msg;
        }

        /// <summary>
        /// Deleting many assets by delete flag
        /// </summary>
        /// <param name="ids"></param>
        /// <param name="userName"></param>
        /// <returns></returns>
        public Message DeleteAssets(string[] ids, string userName)
        {
            Message msg = null;
            DbTransaction trans = null;
            try
            {
                if (dbContext.Connection.State != ConnectionState.Open)
                {
                    dbContext.Connection.Open();
                }
                trans = dbContext.Connection.BeginTransaction();
                dbContext.Transaction = trans;

                int total = 0;
                foreach (string id in ids)
                {
                    total += DeleteById(ConvertUtil.ConvertToLong(id), userName);
                }
                msg = new Message(MessageConstants.I0001, MessageType.Info, total.ToString() + (total >= 2 ? " Assets" : " Asset"), "deleted");
                trans.Commit();
            }
            catch (Exception ex)
            {
                if (trans != null) trans.Rollback();
                msg = new Message(MessageConstants.E0007, MessageType.Error);
            }
            dbContext.Connection.Close();
            return msg;
        }

        /// <summary>
        /// Must Be used in another transaction when calling it
        /// </summary>
        /// <param name="pId"></param>
        /// <param name="userName"></param>
        /// <returns></returns>
        private int DeleteById(long pId, string userName)
        {
            A_Asset aAsset = dbContext.A_Assets.Where(p => !p.DeleteFlag && p.ID == pId).FirstOrDefault();
            int total = 0;
            if (aAsset != null)
            {
                aAsset.DeleteFlag = true;
                aAsset.UpdatedBy = userName;
                aAsset.UpdateDate = DateTime.Now;
                total++;
                // Deleting Sub-Assets
                foreach (A_Asset subAA in GetQuerySubAssets(aAsset.ID))
                {
                    if (!subAA.DeleteFlag)
                    {
                        subAA.DeleteFlag = true;
                        subAA.UpdatedBy = userName;
                        subAA.UpdateDate = DateTime.Now;
                        total++;
                    }
                }
            }
            dbContext.SubmitChanges();
            return total;
        }

        /// <summary>
        /// Removing parent of this asset
        /// </summary>
        /// <param name="id"></param>
        /// <param name="userName"></param>
        /// <returns></returns>
        public Message RemoveParent(long id, string userName)
        {
            DbTransaction trans = null;
            Message msg;
            try
            {
                if (dbContext.Connection.State != ConnectionState.Open)
                {
                    dbContext.Connection.Open();
                }
                trans = dbContext.Connection.BeginTransaction();
                dbContext.Transaction = trans;

                DateTime now = DateTime.Now.Date;
                A_Asset aAsset = dbContext.A_Assets.Where(p => p.ID == id).FirstOrDefault();
                if (aAsset != null)
                {
                    aAsset.ParentId = null;
                    aAsset.UpdatedBy = userName;
                    aAsset.UpdateDate = DateTime.Now;

                    // Removing all allocations from the current time
                    List<A_Allocation> aAllocations = dbContext.A_Allocations.Where(p => p.AssetId == id && p.FromDate >= now).ToList();
                    dbContext.A_Allocations.DeleteAllOnSubmit(aAllocations);
                    // Updating allocation in the current time
                    A_Allocation aAllocation = dbContext.A_Allocations.Where(p => p.AssetId == id 
                        && p.FromDate < now && (p.ToDate == null ||p.ToDate > now)).FirstOrDefault();
                    if(aAllocation != null)
                    {
                        aAllocation.ToDate = now;
                    }

                    dbContext.SubmitChanges();
                    trans.Commit();
                    msg = new Message(MessageConstants.I0001, MessageType.Info, "Group relationship", "removed");
                }
                else
                {
                    msg = new Message(MessageConstants.I0012, MessageType.Error);
                }

            }
            catch (Exception ex)
            {
                if (trans != null) trans.Rollback();
                msg = new Message(MessageConstants.E0007, MessageType.Error);
            }
            dbContext.Connection.Close();
            return msg;
        }

        public int GetFilterCount(AConditions aCond)
        {
            return GetQueryFilter(aCond).Count();
        }

        public List<AAssetEntity> GetFilter(AConditions aCond, string sortColumn, string sortOrder, int skip, int take)
        {
            var sql = GetQueryFilter(aCond);
            switch (sortColumn)
            {
                case "AssetId":
                    sql = sortOrder == "asc" ? sql.OrderBy(p => p.AAsset.AssetId) : sql.OrderByDescending(p => p.AAsset.AssetId);
                    break;
                case "Category":
                    sql = sortOrder == "asc" ? sql.OrderBy(p => p.AAsset.A_Category.Name) : sql.OrderByDescending(p => p.AAsset.A_Category.Name);
                    break;
                case "Status":
                    sql = sortOrder == "asc" ? sql.OrderBy(p => p.AAsset.A_Status.Name) : sql.OrderByDescending(p => p.AAsset.A_Status.Name);
                    break;
                case "Branch":
                    // Being similar to Time Mgt because of Branch is complexly detected by Location Code
                    sql = sortOrder == "asc" ? sql.OrderBy(p => p.Employee.LocationCode) : sql.OrderByDescending(p => p.Employee.LocationCode);
                    break;
                case "Dept":
                    sql = sortOrder == "asc" ? sql.OrderBy(p => p.Employee.Department.DepartmentName) : sql.OrderByDescending(p => p.Employee.Department.DepartmentName);
                    break;
                case "Project":
                    sql = sortOrder == "asc" ? sql.OrderBy(p => p.Employee.Project) : sql.OrderByDescending(p => p.Employee.Project);
                    break;
                case "Manager":
                    sql = sortOrder == "asc" ? sql.OrderBy(p => p.Employee.ManagerId) : sql.OrderByDescending(p => p.Employee.ManagerId);
                    break;
            }
            // Skip Take
            sql = sql.Skip(skip).Take(take);

            return sql.ToList();
        }

        private IQueryable<AAssetEntity> GetQueryFilter(AConditions aCond)
        {
            #region Handling exceptions
            if (aCond.Keyword != null)
                aCond.Keyword = "%" + Regex.Replace(aCond.Keyword.Trim(), @"\s+", "%") + "%";
            #endregion

            IQueryable<AAssetEntity> sql;
            if (aCond.Status == Constants.A_STATUS_AVAILABLE || aCond.Status == Constants.A_STATUS_IN_USE)
            {
                if (aCond.FromDate != null || aCond.ToDate != null)
                    sql = GetQueryFilterStatus(aCond);
            }
            sql = GetQueryFilterSimple(aCond);

            #region Advance Search
            if (aCond.Owner != null)
            {
                sql = sql.Where(p => p.AAsset.OwnerId == aCond.Owner);
            }
            if (aCond.AdvanceParams != null)
            {
                sql = GetQueryAdvanceSearch(sql, aCond.AdvanceParams);
            }
            #endregion

            return sql;
        }

        private IQueryable<AAssetEntity> GetQueryAdvanceSearch(IQueryable<AAssetEntity> query, List<APropertyParam> advanceParams )
        {
            var sql =
                from q in query
                join p in
                    (
                        from p in dbContext.A_AssetPropertyValues
                        group p by p.AssetId
                            into g
                            select new
                            {
                                Id = g.Key,
                                PValue = (from pp in dbContext.A_AssetPropertyValues
                                          where pp.AssetId == g.Key
                                          select pp).ToList()
                            }
                    ) on q.AAsset.ID equals p.Id into qp
                from pp in qp.DefaultIfEmpty()
                select new AAssetEntity()
                {
                    AAsset = q.AAsset,
                    Employee = q.Employee,
                    Manager = q.Manager,
                    Properties = pp.PValue
                };

            foreach (APropertyParam param in advanceParams)
            {
                // Foreach and LinQ Bug
                long id = param.Id;
                string value = param.Value;

                sql = sql.Where(p =>
                    p.Properties.Where(q =>
                        q.PropertyId == id && (
                            // LinQ can't translation string.isNullOrEmpty
                            ((q.A_Property.MasterData == null || q.A_Property.MasterData == "") && q.Value.Contains(value))
                            || (q.A_Property.MasterData != null && q.A_Property.MasterData != "" && q.Value == value)
                        )
                    ).Count() > 0
                );
            }

            return sql;
        }

        private IQueryable<AAssetEntity> GetQueryFilterSimple(AConditions aCond)
        {
            // Getting only Date not Time
            DateTime now = DateTime.Now.Date;
            // Getting list Ids of sub-dept
            List<int> subDeptIds = (
                    from p in dbContext.sp_GetDepartmentRoot(aCond.Dept)
                    select p.DepartmentId
                ).ToList();

            var sql =
                from a in dbContext.A_Assets
                // left join A_Allocation : to detect Employee who possesses this asset at the current time
                join l in dbContext.A_Allocations.Where(p => 
                    p.FromDate <= now && (p.ToDate == null || p.ToDate >= now)) on a.ID equals l.AssetId into al
                from ll in al.DefaultIfEmpty()
                // left join Manager
                join m in dbContext.Employees.Where(p => !p.DeleteFlag) on ll.Employee.ManagerId equals m.ID into alm
                from mm in alm.DefaultIfEmpty()
                where (
                    !a.DeleteFlag && a.ParentId == null
                    && (aCond.Keyword == null
                        || SqlMethods.Like(a.AssetId, aCond.Keyword)
                        || SqlMethods.Like(ll.Employee.ID + " " + ll.Employee.FirstName + " " 
                            + (ll.Employee.MiddleName != null ? (ll.Employee.MiddleName + " ") : "") 
                            + ll.Employee.LastName, aCond.Keyword)
                        || SqlMethods.Like(ll.Employee.LastName + " " + ll.Employee.FirstName, aCond.Keyword)
                        || SqlMethods.Like(ll.Employee.OfficeEmail, aCond.Keyword + "@%")
                    )
                    && (aCond.Status == null || a.StatusId == aCond.Status)
                    && (aCond.Branch == null || ll.Employee.LocationCode.Contains(Constants.LOCATION_CODE_BRANCH_PREFIX + aCond.Branch))
                    && (aCond.Dept == null || subDeptIds.Contains(ll.Employee.DepartmentId))
                    && (string.IsNullOrEmpty(aCond.Project) || ll.Employee.Project == aCond.Project)
                    && (string.IsNullOrEmpty(aCond.Manager) || ll.Employee.ManagerId == aCond.Manager)
                )
                select new AAssetEntity()
                {
                    AAsset = a,
                    Employee = ll.Employee,
                    Manager = mm
                };
            // Where by category
            if (aCond.Category != null)
            {
                List<int> categoryIds = (from c in _aCategoryDao.GetListByParentId(aCond.Category.Value) select c.ID).ToList();
                if (categoryIds.Count <= 0)
                {
                    sql = sql.Where(p => p.AAsset.CategoryId == aCond.Category);
                }
                else
                {
                    sql = sql.Where(p => categoryIds.Contains(p.AAsset.CategoryId));
                }
            }

            return sql;
        }

        private IQueryable<AAssetEntity> GetQueryFilterStatus(AConditions aCond)
        {
            // Getting only Date not Time
            DateTime now = DateTime.Now.Date;
            #region Handling params
            if (aCond.FromDate == null)
                aCond.FromDate = DateTime.MinValue;
            if (aCond.ToDate == null)
                aCond.ToDate = DateTime.MaxValue;
            #endregion

            // Getting list Ids of sub-dept
            List<int> subDeptIds = (
                    from p in dbContext.sp_GetDepartmentRoot(aCond.Dept)
                    select p.DepartmentId
                ).ToList();

            var sql =
                from a in dbContext.A_Assets
                // left join A_Allocation : to detect Employee who possesses this asset at the current time
                join l in dbContext.A_Allocations.Where(p => 
                    p.FromDate <= now && (p.ToDate == null || p.ToDate >= now)) on a.ID equals l.AssetId into al
                from ll in al.DefaultIfEmpty()
                // status available or in use with time range
                join s in
                    (
                        from s in dbContext.A_Allocations.Where(p => 
                               (p.ToDate == null && p.FromDate <= aCond.ToDate)
                            // (aCond.From, aCond.To) v (p.From, p.To) != null
                               || (p.ToDate != null && aCond.FromDate <= aCond.ToDate && p.FromDate <= aCond.ToDate && p.ToDate >= aCond.FromDate)
                       ).GroupBy(p => p.AssetId)
                        select new
                        {
                            ID = s.Key
                        }
                        ) on a.ID equals s.ID into als
                from ss in als.DefaultIfEmpty()
                // left join Manager
                join m in dbContext.Employees.Where(p => !p.DeleteFlag) on ll.Employee.ManagerId equals m.ID into alm
                from mm in alm.DefaultIfEmpty()
                where (
                    !a.DeleteFlag && a.ParentId == null
                    && (aCond.Keyword == null
                        || SqlMethods.Like(a.AssetId, aCond.Keyword)
                        || SqlMethods.Like(ll.Employee.ID + " " + ll.Employee.FirstName + " "
                            + (ll.Employee.MiddleName != null ? (ll.Employee.MiddleName + " ") : "")
                            + ll.Employee.LastName, aCond.Keyword)
                        || SqlMethods.Like(ll.Employee.LastName + " " + ll.Employee.FirstName, aCond.Keyword)
                        || SqlMethods.Like(ll.Employee.OfficeEmail, aCond.Keyword + "@%")
                    )
                    // status available or in use
                    && (aCond.Status == ConvertUtil.ConvertToInt(Constants.A_STATUS_IN_USE) ? ss.ID != null : ss.ID == null)
                    && (aCond.Branch == null || ll.Employee.LocationCode.Contains(Constants.LOCATION_CODE_BRANCH_PREFIX + aCond.Branch))
                    && (aCond.Dept == null || subDeptIds.Contains(ll.Employee.DepartmentId))
                    && (string.IsNullOrEmpty(aCond.Project) || ll.Employee.Project == aCond.Project)
                    && (string.IsNullOrEmpty(aCond.Manager) || ll.Employee.ManagerId == aCond.Manager)
                )
                select new AAssetEntity()
                {
                    AAsset = a,
                    Employee = ll.Employee,
                    Manager = mm
                };

            // Where by category
            if (aCond.Category != null)
            {
                List<int> categoryIds = (from c in _aCategoryDao.GetListByParentId(aCond.Category.Value) select c.ID).ToList();
                if (categoryIds.Count <= 0)
                {
                    sql = sql.Where(p => p.AAsset.CategoryId == aCond.Category);
                }
                else
                {
                    sql = sql.Where(p => categoryIds.Contains(p.AAsset.CategoryId));
                }
            }
            return sql;
        }

        #region Adding sub-assets
        public int GetFilterAddSubCount(long pId, string keyword, int? cateId, int? statusId)
        {
            return GetQueryFilterAddSub(pId, keyword, cateId, statusId).Count();
        }

        public List<A_Asset> GetFilterAddSub(long pId, string keyword, int? cateId, int? statusId, string sortColumn, string sortOrder)
        {
            var sql = GetQueryFilterAddSub(pId, keyword, cateId, statusId);
            switch (sortColumn)
            {
                case "AssetId":
                    sql = sortOrder == "asc" ? sql.OrderBy(p => p.AssetId) : sql.OrderByDescending(p => p.AssetId);
                    break;
                case "Category":
                    sql = sortOrder == "asc" ? sql.OrderBy(p => p.A_Category.Name) : sql.OrderByDescending(p => p.A_Category.Name);
                    break;
                case "Status":
                    sql = sortOrder == "asc" ? sql.OrderBy(p => p.A_Status.Name) : sql.OrderByDescending(p => p.A_Status.Name);
                    break;
                case "Owner":
                    sql = sortOrder == "asc" ? sql.OrderBy(p => p.Department.DepartmentName) : sql.OrderByDescending(p => p.Department.DepartmentName);
                    break;
                case "Remark":
                    sql = sortOrder == "asc" ? sql.OrderBy(p => p.Remark) : sql.OrderByDescending(p => p.Remark);
                    break;
            }

            return sql.ToList();
        }

        private IQueryable<A_Asset> GetQueryFilterAddSub(long pId, string keyword, int? cateId, int? statusId)
        {
            DateTime now = DateTime.Now.Date;

            var sql =
                from a in dbContext.A_Assets
                where !a.DeleteFlag
                    // not include itself
                    && a.ID != pId
                    // Have no parent
                    && a.ParentId == null
                    // Have no children
                    && !(from p in dbContext.A_Assets where !p.DeleteFlag select p.ParentId).Contains(a.ID)
                    // Have no employee from the current time
                    && (
                        from l in dbContext.A_Allocations
                        where l.AssetId == a.ID
                        && (l.ToDate == null || l.ToDate >= now)
                        select l
                    ).Count() == 0
                select a;
            if (keyword != null)
            {
                sql = sql.Where(p => p.AssetId.Contains(keyword));
            }
            if (cateId != null)
            {
                sql = sql.Where(p => p.CategoryId == cateId);
            }
            if (statusId != null)
            {
                sql = sql.Where(p => p.StatusId == statusId);
            }
            return sql;
        }

        public Message AddSubAssets(long pId, List<long> idArr, string userName)
        {
            Message msg = null;
            DbTransaction trans = null;
            try
            {
                if (dbContext.Connection.State != ConnectionState.Open)
                {
                    dbContext.Connection.Open();
                }
                trans = dbContext.Connection.BeginTransaction();
                dbContext.Transaction = trans;

                A_Asset aAsset = GetById(pId);
                if (aAsset == null)
                {
                    trans.Rollback();
                    msg = new Message(MessageConstants.E0007, MessageType.Error);
                }
                else
                {
                    List<A_Asset> aAssets = (from a in dbContext.A_Assets where !a.DeleteFlag && idArr.Contains(a.ID) select a).ToList();
                    DateTime now = DateTime.Now.Date;
                    foreach (A_Asset a in aAssets)
                    {
                        a.ParentId = pId;
                        a.UpdateDate = DateTime.Now;
                        a.UpdatedBy = userName;
                    }

                    // Getting allocation plans of parent
                    List<A_Allocation> aAllocations = (from l in dbContext.A_Allocations
                                                       where l.AssetId == pId
                                                             && (l.ToDate == null || l.ToDate >= now)
                                                       select l).ToList();
                    // Updating that allocaion to sub-assets
                    if(aAllocations.Count > 0)
                    {
                        List<A_Allocation> uAllocations = new List<A_Allocation>();
                        foreach (long subId in idArr)
                        {
                            foreach (A_Allocation al in aAllocations)
                            {
                                A_Allocation newAl = new A_Allocation();
                                newAl.AssetId = subId;
                                newAl.ParentId = pId;
                                newAl.EmployeeId = al.EmployeeId;
                                newAl.FromDate = al.FromDate < now ? now : al.FromDate;
                                newAl.ToDate = al.ToDate;
                                newAl.Remark = al.Remark;
                                newAl.CreateDate = now;
                                newAl.CreateBy = userName;
                                newAl.UpdateDate = now;
                                newAl.UpdateBy = userName;
                                uAllocations.Add(newAl);
                            }
                        }
                        dbContext.A_Allocations.InsertAllOnSubmit(uAllocations);
                    }

                    dbContext.SubmitChanges();
                    trans.Commit();
                    msg = new Message(MessageConstants.I0016, MessageType.Info, aAssets.Count + (aAssets.Count >= 2 ? " Assets" : " Asset"), "grouped", aAsset.AssetId + " Asset");
                }
            }
            catch (Exception ex)
            {
                if (trans != null) trans.Rollback();
                msg = new Message(MessageConstants.E0007, MessageType.Error);
            }
            dbContext.Connection.Close();

            return msg;
        }
        #endregion

        #endregion

        #region Linh.Quang.Le
        public A_Asset GetbyAssetId(string assetId)
        {
            return dbContext.A_Assets.Where(a=>a.AssetId == assetId && a.DeleteFlag == false).FirstOrDefault();
        }
        public List<A_Asset> GetParentAsset()
        {
            return dbContext.A_Assets.Where(a => a.ParentId == null && a.DeleteFlag == false).ToList<A_Asset>();
        }
        public List<Department> GetAssetOwnerList()
        {
            CRM_MasterData masterData = dbContext.CRM_MasterDatas.Where(m => m.ID == "ASSET_OWNER").FirstOrDefault();
            string[] ownerIdArray = masterData.Value.Split(';');
            DepartmentDao deptDao = new DepartmentDao();
            List<Department> OwnerList = new List<Department>();
            if (ownerIdArray.Count() != 0)
            {
                foreach (string idStr in ownerIdArray)
                {
                    int id = ConvertUtil.ConvertToInt(idStr);
                    Department owner = deptDao.GetById(id);
                    OwnerList.Add(owner);
                }
            }
            return OwnerList;
        }

        /// <summary>
        /// (29/2/2012) Linh.Le: Get Asset Owner SelectList.
        /// </summary>
        /// <param name="selectedValue">select value in owner SelectList</param>
        /// <returns></returns>
        public List<SelectListItem> GetAssetOwnerSelectList(object selectedValue = null)
        {
            List<Department> list = GetAssetOwnerList();

            List<SelectListItem> assetStatusList = new List<SelectListItem>();
            foreach (Department obj in list)
            {
                SelectListItem item = new SelectListItem();
                item.Value = obj.DepartmentId.ToString();
                item.Text = HttpUtility.HtmlDecode(obj.DepartmentName);
                if (item.Value == ConvertUtil.ConvertToString(selectedValue))
                    item.Selected = true;
                assetStatusList.Add(item);
            }
            return assetStatusList.ToList();
        }

        public bool IsDublicateAssetId(A_Asset objUI)
        {
            bool isDublicateName = true;
            A_Asset dublicateName = dbContext.A_Assets.Where(a => a.AssetId.Equals(objUI.AssetId) && a.DeleteFlag == false).FirstOrDefault<A_Asset>();
            if (dublicateName == null || dublicateName.AssetId == objUI.AssetId)
            {
                isDublicateName = false;
            }
            return isDublicateName;
        }

        public bool IsDBChanged(A_Asset objUI)
        {
            A_Asset objDb = GetById(objUI.ID);
            bool isChannged = true;
            if (objDb.UpdateDate.ToString() == objUI.UpdateDate.ToString())
            {
                isChannged = false;
            }
            return isChannged;
        }
        #endregion
    }
}