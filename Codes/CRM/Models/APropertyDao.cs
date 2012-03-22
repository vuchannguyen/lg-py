using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CRM.Library.Common;
using System.Data.Common;
using CRM.Models.Entities;
using System.Linq.Dynamic;
using System.Web.Mvc;
namespace CRM.Models
{
    public class APropertyDao : CustomBaseDao<A_Property>
    {
        #region Tai.Pham
        /// <summary>
        /// Getting List of Properties by category and permission of user
        /// </summary>
        /// <param name="categoryId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public List<A_Property> GetListPropertyByPermission(int categoryId, int userId)
        {
            List<string> gpStr = (from gp in PermissionCommon.GetListPermissionOfUser(userId)
                     select gp.ModuleId.ToString() + ";" + gp.PermissionId).ToList();

            var sql =
                from p in dbContext.A_Properties.Where(p => !p.DeleteFlag && p.CategoryId == categoryId)
                join pp in dbContext.A_PropertyPermissions on p.ID equals pp.PropertyId
                where (
                          gpStr.Contains(pp.ModuleId.ToString() + ";" + pp.PermissionId)
                      )
                select p
                ;
            return sql.ToList();
        }
        #endregion

        public A_Property GetById(long Id)
        {
            return GetItem(a => a.ID == Id && a.DeleteFlag == false);
        }

        /// <summary>
        /// (6/3/2012) Linh.Le: Get property list by category id
        /// </summary>
        /// <param name="categoryId">Category id</param>
        /// <param name="type">
        /// If type=0 return full property list.
        /// If type=1 return non-sensitive property list.
        /// If type=2 return sensitive property list.
        /// Default type value is 0.
        /// </param>
        /// <returns></returns>
        public List<A_Property> GetByCategoryId(int categoryId, int type = 0, int moduleId = 0, int[] permissionIds = null)
        {
            var query = from q in dbContext.A_Properties
                                where q.CategoryId == categoryId
                                   && q.DeleteFlag == false
                                select q;
            switch (type)
            {
                case 1:
                    {
                        query = query.Where(q => q.A_PropertyPermissions.Where(p => p.PropertyId == q.ID).Count() == 0);
                    } break;
                case 2:
                    {
                        if (moduleId != 0 && permissionIds != null)
                        {
                            query = query.Where(q => q.A_PropertyPermissions.Where(p => p.PropertyId == q.ID 
                                                                                     && p.ModuleId == moduleId 
                                                                                     && permissionIds.Contains(p.PermissionId)
                                                                                  ).Count() != 0);
                        }
                    } break;
                default: break;
            }
            return query.OrderBy(p => p.Name).ToList<A_Property>();
        }

        private IQueryable<AssetPropertyEntitiy> GetAssetPropertyQueryList(string searchText, int assetCategoryId)
        {
            var sql = (from assetProperties in dbContext.A_Properties
                      where assetProperties.DeleteFlag == false
                      select new AssetPropertyEntitiy()
                      {
                          Id = assetProperties.ID,
                          Name = assetProperties.Name,
                          MasterData = assetProperties.MasterData,
                          CategoryId = assetProperties.CategoryId,
                          CategoryName = assetProperties.A_Category.ParentId == null ? assetProperties.A_Category.Name : assetProperties.A_Category.A_Category1.Name + " / " + assetProperties.A_Category.Name,
                          ParentCategoryId = assetProperties.A_Category.ParentId,
                          ParentCategoryName = assetProperties.A_Category.ParentId != null ? assetProperties.A_Category.A_Category1.Name : string.Empty,
                          DisplayOrder = assetProperties.DisplayOrder,
                          DeleteFlag = assetProperties.DeleteFlag,
                          CreatedBy = assetProperties.CreatedBy,
                          CreateDate = assetProperties.CreateDate,
                          UpdatedBy = assetProperties.UpdatedBy,
                          UpdateDate = assetProperties.UpdateDate
                      });
            if (!string.IsNullOrEmpty(searchText))
            {
                sql = sql.Where(a=>a.Name.Contains(searchText) || a.MasterData.Contains(searchText));
            }
            if (assetCategoryId != 0)
            {
                sql = sql.Where(a=>a.CategoryId == assetCategoryId || a.ParentCategoryId == assetCategoryId);
            }
            return sql.OrderBy(a => a.DisplayOrder);
        }

        public int GetAssetPropertyCountList(string searchText, int assetCateId)
        {
            return GetAssetPropertyQueryList(searchText, assetCateId).Count();
        }

        public List<AssetPropertyEntitiy> GetAssetPropertyList(string searchText, int assetCateId, string sortColumn, string sortOrder, int skip, int take)
        {
            var sql = GetAssetPropertyQueryList(searchText, assetCateId);
            sql = sql.OrderBy("CategoryName asc, DisplayOrder asc, " + sortColumn + " " + sortOrder);
            return sql.Skip(skip).Take(take).ToList();
        }

        /// <summary>
        /// Insert asset category
        /// </summary>
        /// <param name="objUI"></param>
        /// <returns></returns>
        public Message Insert(A_Property objUI, List<A_PropertyPermission> permissionList )
        {
            Message msg = null;
            try
            {
                if (!IsDublicateName(objUI))
                {
                    Insert(objUI);
                    foreach (A_PropertyPermission ap in permissionList)
                        ap.PropertyId = objUI.ID;
                    dbContext.A_PropertyPermissions.InsertAllOnSubmit(permissionList);
                    dbContext.SubmitChanges();
                    msg = new Message(MessageConstants.I0001, MessageType.Info, "Asset Property '" + objUI.Name + "'", "added");
                }
                else
                {
                    msg = new Message(MessageConstants.E0020, MessageType.Error, "Name '" + objUI.Name + "'", "Asset Property");
                }
            }
            catch (Exception)
            {
                msg = new Message(MessageConstants.E0007, MessageType.Error);
            }
            return msg;
        }

        /// <summary>
        /// Delete asset properties
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public Message DeleteList(string ids, string userName)
        {
            Message msg = null;
            DbTransaction trans = null;
            bool isOK = true;
            try
            {
                //begin transaction
                dbContext.Connection.Open();
                trans = dbContext.Connection.BeginTransaction();
                dbContext.Transaction = trans;

                if (!string.IsNullOrEmpty(ids))
                {
                    //split ids by char ','
                    ids = ids.TrimEnd(Constants.SEPARATE_IDS_CHAR);
                    string[] idArr = ids.Split(Constants.SEPARATE_IDS_CHAR);
                    int assPropId = 0;
                    int totalID = idArr.Count();

                    //loop each id to delete
                    foreach (string id in idArr)
                    {
                        //is check all records to delete 
                        bool isInterger = Int32.TryParse(id, out assPropId);
                        if (isInterger)
                        {
                            A_Property assetProperty = GetById(assPropId);
                            if (assetProperty != null && !IsInUse(assetProperty))
                            {
                                assetProperty.UpdatedBy = userName;
                                assetProperty.DeleteFlag = true;
                                assetProperty.UpdateDate = DateTime.Now;
                                msg = Update(assetProperty);
                                if (msg.MsgType == MessageType.Error)
                                {
                                    isOK = false;
                                    break;
                                }
                            }
                        }
                        else
                        {
                            totalID--;
                        }
                    }

                    if (isOK)
                    {
                        // Show succes message
                        msg = new Message(MessageConstants.I0001, MessageType.Info, totalID.ToString() + (totalID > 1 ? " properties" : " property"), "deleted");
                        trans.Commit();
                    }
                    else
                    {
                        trans.Rollback();
                    }
                }
            }
            catch
            {
                if (trans != null) { trans.Rollback(); }
                // Show system error
                msg = new Message(MessageConstants.E0006, MessageType.Error, "delete", "it");
            }
            return msg;
        }

        public Message Update(A_Property objUI, List<A_PropertyPermission> permissionList)
        {
            Message msg = null;
            try
            {
                if (!IsDBChanged(objUI))
                {
                    if (!IsDublicateName(objUI))
                    {
                        A_Property objDb = GetById(objUI.ID);
                        objDb.Name = objUI.Name;
                        objDb.MasterData = objUI.MasterData;
                        objDb.CategoryId = objUI.CategoryId;
                        objDb.DisplayOrder = objUI.DisplayOrder;
                        objDb.UpdatedBy = objUI.UpdatedBy;
                        objDb.UpdateDate = System.DateTime.Now;
                        Update(objDb);

                        List<A_PropertyPermission> ppList = GetPropertyPermissionList(objUI.ID);
                        dbContext.A_PropertyPermissions.DeleteAllOnSubmit(ppList);
                        foreach (A_PropertyPermission ap in permissionList)
                            ap.PropertyId = objUI.ID;
                        dbContext.A_PropertyPermissions.InsertAllOnSubmit(permissionList);
                        dbContext.SubmitChanges();

                        msg = new Message(MessageConstants.I0001, MessageType.Info, "Asset Property '" + objUI.Name + "'", "added");
                    }
                    else
                    {
                        msg = new Message(MessageConstants.E0020, MessageType.Error, "Name '" + objUI.Name + "'", "Asset Property");
                    }
                }
                else
                {
                    msg = new Message(MessageConstants.E0025, MessageType.Error, objUI.Name);
                }
            }
            catch (Exception)
            {
                msg = new Message(MessageConstants.E0007, MessageType.Error);
            }
            return msg;
        }

        private bool IsDublicateName(A_Property objUI)
        {
            bool isDublicateName = true;
            A_Property dublicateName = dbContext.A_Properties.Where(a => a.Name.Equals(objUI.Name) && !a.DeleteFlag && a.CategoryId.Equals(objUI.CategoryId)).FirstOrDefault<A_Property>();
            if (dublicateName == null || dublicateName.ID == objUI.ID)
            {
                isDublicateName = false;
            }
            return isDublicateName;
        }

        private bool IsInUse(A_Property objUI)
        {
            AssetPropertyDetailDao assPropDetailDao = new AssetPropertyDetailDao();
            if (assPropDetailDao.GetByPropertyId(objUI.ID.ToString()).Count == 0)
                return false;
            return true;
        }

        /// <summary>
        /// Check data was changed by another user.
        /// Return true if data of this item is changed
        /// </summary>
        /// <param name="objUI"></param>
        /// <param name="objDb"></param>
        /// <returns></returns>
        private bool IsDBChanged(A_Property objUI)
        {
            A_Property objDb = GetById(objUI.ID);
            bool isChannged = true;
            if (objDb.UpdateDate.ToString() == objUI.UpdateDate.ToString())
            {
                isChannged = false;
            }
            return isChannged;
        }

        public List<A_PropertyPermission> GetPropertyPermissionList(long propertyID)
        {
            return dbContext.A_PropertyPermissions.Where(p => p.PropertyId == propertyID).ToList();
        }

        public bool IsAuthorized(long propertyId,int moduleId, int permissionId)
        {
            var sql = from p in dbContext.A_PropertyPermissions
                      where p.PropertyId == propertyId
                         && p.ModuleId == moduleId
                         && p.PermissionId == permissionId
                      select p;
            if (sql.Count() != 0)
                return true;
            return false;
        }
    }
}