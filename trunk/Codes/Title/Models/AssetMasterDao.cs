using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CRM.Library.Common;
using System.Data.Common;
using System.Text.RegularExpressions;

namespace CRM.Models
{
    public class AssetMasterDao : BaseDao
    {
        public List<sp_GetAssetMasterResult> GetList(string name, int category, int status, string project)
        {
            return dbContext.sp_GetAssetMaster(name, category, status, project).OrderByDescending(p => p.AssetId).ToList<sp_GetAssetMasterResult>();
        }
        public List<sp_GetEmployeeProjectResult> GetEmployeeProject()
        {
            return dbContext.sp_GetEmployeeProject().OrderByDescending(p => p.Project).ToList<sp_GetEmployeeProjectResult>();
        }
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
        public List<sp_GetAssetMasterResult> GetListById(string name, int category, int status, string project)
        {
            return dbContext.sp_GetAssetMaster(name, category, status, project).OrderByDescending(p => p.ID).ToList<sp_GetAssetMasterResult>();
        }

        public List<sp_GetEmployeeProjectResult> GetProjectList()
        {
            return dbContext.sp_GetEmployeeProject().ToList<sp_GetEmployeeProjectResult>();
        }

        //public List<sp_GetAssetMasterResult> GetListForExport(string name, int category, int status, string project)
        //{
        //    return dbContext.sp_GetAssetMaster(name, category, status, project).OrderByDescending(p => p.ID).ToList<sp_GetAssetMasterResult>();
        //}

        //public List<sp_GetAssetMasterResult> SortForExport(List<sp_GetAssetMasterResult> list, string sortColumn, string sortOrder)
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
        //                 delegate(sp_GetAssetMasterResult m1, sp_GetAssetMasterResult m2)
        //                 { return m1.ID.CompareTo(m2.ID) * order; });
        //            break;
        //        case "Name":
        //            list.Sort(
        //                 delegate(sp_GetAssetMasterResult m1, sp_GetAssetMasterResult m2)
        //                 { return m1.AssetCategoryName.CompareTo(m2.AssetCategoryName) * order; });
        //            break;
        //        case "UserName":
        //            list.Sort(
        //                 delegate(sp_GetAssetMasterResult m1, sp_GetAssetMasterResult m2)
        //                 { return m1.DisplayName.CompareTo(m2.DisplayName) * order; });
        //            break;
        //        case "Status":
        //            list.Sort(
        //                 delegate(sp_GetAssetMasterResult m1, sp_GetAssetMasterResult m2)
        //                 { return m1.StatusName.CompareTo(m2.StatusName) * order; });
        //            break;
        //        case "Remark":
        //            list.Sort(
        //                 delegate(sp_GetAssetMasterResult m1, sp_GetAssetMasterResult m2)
        //                 { return m1.Remark.CompareTo(m2.Remark) * order; });
        //            break;
        //        case "IsActive":
        //            list.Sort(
        //                 delegate(sp_GetAssetMasterResult m1, sp_GetAssetMasterResult m2)
        //                 { return m1.IsActive.CompareTo(m2.IsActive) * order; });
        //            break;
        //    }

        //    return list;
        //}

        public List<AssetMaster> GetList()
        {
            return dbContext.AssetMasters.Where(c => c.CategoryId == null && c.DeleteFlag == false).OrderBy(p => p.ID).ToList<AssetMaster>();
        }

        //public List<sp_GetAssetMasterCategoryResult> GetAssetMasterPropertyList()
        //{
        //    return dbContext.sp_GetAssetMasterCategory().OrderBy(p => p.ID).ToList<sp_GetAssetMasterCategoryResult>();
        //}

        public List<AssetMaster> GetAssetMasterList()
        {
            return dbContext.AssetMasters.ToList();
        }

        public Message UpdateActiveStatus(int cusId, bool isActive)
        {
            try
            {
                AssetMaster obj = dbContext.AssetMasters.Where(p => p.ID == cusId).FirstOrDefault();
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

        public AssetMaster GetById(string id)
        {
            return dbContext.AssetMasters.Where(c => c.ID.Equals(id)).FirstOrDefault<AssetMaster>();
        }
    }
}