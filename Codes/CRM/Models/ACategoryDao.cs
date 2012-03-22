using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic;
using System.Web;
using CRM.Library.Common;
using System.Data.Common;
using CRM.Models.Entities;
using System.Web.Mvc;

namespace CRM.Models
{
    public class ACategoryDao : CustomBaseDao<A_Category>
    {
        /// <summary>
        /// (29/2/2012) Linh.Le: Get asset category by Id
        /// </summary>
        /// <param name="Id">Asset Category Id</param>
        /// <returns></returns>
        public A_Category GetById(int Id)
        {
            return this.GetItem(a => a.ID == Id && a.DeleteFlag == false);
        }

        /// <summary>
        /// (29/2/2012) Linh.Le: Get asset category list
        /// </summary>
        /// <returns></returns>
        public List<A_Category> GetAssetCategoryList()
        {
            return this.GetList(a => a.DeleteFlag == false);
        }

        /// <summary>
        /// (29/2/2012) Linh.Le: Get list Asset Categories by hierarchy.
        /// </summary>
        /// <param name="selectedValue">select value in Category</param>
        /// <returns></returns>
        public List<SelectListItem> GetAssetCategoriesByHierarchy(object selectedValue = null)
        {
            var list = dbContext.sp_GetAssetCategoriesByHierarchy().ToList();

            List<SelectListItem> assetCategoryList = new List<SelectListItem>();
            foreach (sp_GetAssetCategoriesByHierarchyResult obj in list)
            {
                SelectListItem item = new SelectListItem();
                item.Value = obj.ID.ToString();
                item.Text = HttpUtility.HtmlDecode(obj.TreeCategoryName);
                if (item.Value == ConvertUtil.ConvertToString(selectedValue)) 
                    item.Selected = true;
                assetCategoryList.Add(item);
            }
            return assetCategoryList.ToList();
        }

        /// <summary>
        /// (29/2/2012) Linh.Le: Get Non-Parent Asset Category SelectList.
        /// </summary>
        /// <param name="selectedValue">select value in CategorySelectList</param>
        /// <param name="excludeItem">exclude an item out of CategorySelectList</param>
        /// <returns></returns>
        public List<SelectListItem> GetNonParentAssetCategorySelectList(object selectedValue = null, A_Category excludeItem = null)
        {
            List<A_Category> list = GetNonParentAssetCategoryList(excludeItem);

            List<SelectListItem> assetCategoryList = new List<SelectListItem>();
            foreach (A_Category obj in list)
            {
                SelectListItem item = new SelectListItem();
                item.Value = obj.ID.ToString();
                item.Text = HttpUtility.HtmlDecode(obj.Name);
                if (item.Value == ConvertUtil.ConvertToString(selectedValue))
                    item.Selected = true;
                assetCategoryList.Add(item);
            }
            return assetCategoryList.ToList();
        }

        /// <summary>
        /// (29/2/2012) Linh.Le: Get Non-Parent Asset Category List
        /// </summary>
        /// <param name="excludeItem">exclude an item out of Category List</param>
        /// <returns></returns>
        public List<A_Category> GetNonParentAssetCategoryList(A_Category excludeItem = null)
        {
            var parentList = (from parentCatetories in dbContext.A_Categories
                                       where !parentCatetories.DeleteFlag
                                         && parentCatetories.ParentId == null
                                        select parentCatetories);
            if (excludeItem != null)
            {
                parentList = parentList.Where(a => a.ID != excludeItem.ID);
            }
            return parentList.ToList<A_Category>();
        }

        /// <summary>
        /// (29/2/2012) Linh.Le: Get Asset Category Query List
        /// </summary>
        /// <param name="searchText">A string to filter asset category list</param>
        /// <returns></returns>
        private IQueryable<AssetCategoryEntitiy> GetAssetCategoryQueryList(string searchText)
        {
            List<int?> parentIdList = GetParentIdList();

            var sql = (from aCatetories in dbContext.A_Categories
                      where aCatetories.DeleteFlag == false
                      && !parentIdList.Contains(aCatetories.ID)
                      select new AssetCategoryEntitiy() { 
                        Id = aCatetories.ID,
                        Name = aCatetories.Name,
                        Description = aCatetories.Description,
                        ParentId = aCatetories.ParentId,
                        ParentName = aCatetories.ParentId != null ? aCatetories.A_Category1.Name : Constants.ASSET_CATEGORY_NON_PARENT,
                        DeleteFlag = aCatetories.DeleteFlag,
                        CreatedBy = aCatetories.CreatedBy,
                        CreateDate = aCatetories.CreateDate,
                        UpdatedBy = aCatetories.UpdatedBy,
                        UpdateDate = aCatetories.UpdateDate
                      });
            if (!string.IsNullOrEmpty(searchText))
                sql = sql.Where(a => a.Name.Contains(searchText) || a.Description.Contains(searchText));
            return sql.OrderBy(a => a.ParentName);
        }

        /// <summary>
        /// (29/2/2012) Linh.Le: Get Parent Id List
        /// </summary>
        /// <returns></returns>
        private static List<int?> GetParentIdList()
        {
            List<int?> parentIdList = (from exCatetories in dbContext.A_Categories
                                       where !exCatetories.DeleteFlag
                                         && exCatetories.ParentId != null
                                       select exCatetories.ParentId).Distinct().ToList<int?>();
            return parentIdList;
        }

        /// <summary>
        /// (29/2/2012) Linh.Le: Get Asset Category Count List
        /// </summary>
        /// <param name="searchText">A string to filter asset category list</param>
        /// <returns></returns>
        public int GetAssetCategoryCountList(string searchText)
        {
            return GetAssetCategoryQueryList(searchText).Count();
        }

        /// <summary>
        /// (29/2/2012) Linh.Le: Get asset category list
        /// </summary>
        /// <param name="searchText">A string to filter asset category list</param>
        /// <param name="sortColumn">A column to sort</param>
        /// <param name="sortOrder">Sort Order</param>
        /// <param name="skip">skip param records</param>
        /// <param name="take">take param records</param>
        /// <returns></returns>
        public List<AssetCategoryEntitiy> GetAssetCategoryList(string searchText, string sortColumn, string sortOrder, int skip, int take)
        {
            var sql = GetAssetCategoryQueryList(searchText);
            sql = sql.OrderBy("ParentName asc, " + sortColumn + " " + sortOrder);
            return sql.Skip(skip).Take(take).ToList();
        }

        /// <summary>
        /// (29/2/2012) Linh.Le: Delete List
        /// </summary>
        /// <param name="ids">An array of id need to delete</param>
        /// <param name="userName">Username of person who want to delete record(s)</param>
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
                    int assCatId = 0;
                    int totalID = idArr.Count();

                    //loop each id to delete
                    foreach (string id in idArr)
                    {
                        //is check all records to delete 
                        bool isInterger = Int32.TryParse(id, out assCatId);
                        if (isInterger)
                        {
                            A_Category assetCategory = GetById(assCatId);
                            if (assetCategory != null && !IsInUse(assetCategory))
                            {
                                assetCategory.UpdatedBy = userName;
                                assetCategory.DeleteFlag = true;
                                assetCategory.UpdateDate = DateTime.Now;
                                msg = Update(assetCategory);
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
                        msg = new Message(MessageConstants.I0001, MessageType.Info, totalID.ToString() + (totalID >1 ? " categories" : " category" ), "deleted");
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

        /// <summary>
        /// (29/2/2012) Linh.Le: Check dublicate name
        /// </summary>
        /// <param name="objUI">A_Category object</param>
        /// <returns></returns>
        public bool IsDublicateName(A_Category objUI)
        {
            bool isDublicateName = true;
            A_Category dublicateName = dbContext.A_Categories.Where(a => a.Name.Equals(objUI.Name)
                                                                 && (objUI.ParentId != null ? objUI.ParentId.Equals(a.ParentId) : a.ParentId == null)
                                                                 && a.DeleteFlag == false).FirstOrDefault<A_Category>();
            if (dublicateName == null || dublicateName.ID == objUI.ID)
            {
                isDublicateName = false;
            }
            return isDublicateName;
        }

        /// <summary>
        /// (29/2/2012) Linh.Le: Check In Use
        /// </summary>
        /// <param name="objUI">A_Category object</param>
        /// <returns></returns>
        public bool IsInUse(A_Category objUI)
        {
            AssetPropertyDao assPropDao = new AssetPropertyDao();
            AssetMasterDao assMasterDao = new AssetMasterDao();
            if (assPropDao.GetByCategoryId(objUI.ID).Count == 0 && assMasterDao.GetListByCategoryId(objUI.ID).Count == 0)
                return false;
            return true;
        }

        /// <summary>
        /// (29/2/2012) Linh.Le: Check data was changed by another user. Return true if data of this item is changed
        /// </summary>
        /// <param name="objUI">A_Category object</param>
        /// <returns></returns>
        public bool IsDBChanged(A_Category objUI)
        {
            A_Category objDb = GetById(objUI.ID);
            if (objDb.UpdateDate.ToString().CompareTo(objUI.UpdateDate.ToString()) == 0)
            {
                return false;
            }
            return true;
        }

        public bool IsCategoryParent(string categoryId)
        {
            if (!string.IsNullOrEmpty(categoryId))
            {
                int iCategoryId = 0;
                iCategoryId = ConvertUtil.ConvertToInt(categoryId);
                List<int?> parentIdList = GetParentIdList();
                if (parentIdList != null)
                {
                    foreach (int parentId in parentIdList)
                    {
                        if (parentId == iCategoryId)
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        #region Tai.Pham
        /// <summary>
        /// @author : Tai.Pham
        /// </summary>
        /// <param name="pId"></param>
        /// <returns></returns>
        public List<A_Category> GetListByParentId(int pId)
        {
            var sql =
                from c in dbContext.A_Categories
                where !c.DeleteFlag
                      && c.ParentId == pId
                select c;
            return sql.ToList();
        } 
        #endregion
    }
}