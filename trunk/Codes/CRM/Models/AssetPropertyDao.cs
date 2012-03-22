using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CRM.Library.Common;
using System.Data.Common;

namespace CRM.Models
{
    public class AssetPropertyDao : BaseDao
    {
        /// <summary>
        /// Get asset category by Id
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public AssetProperty GetById(long Id)
        {
            return dbContext.AssetProperties.Where(a => a.ID == Id && a.DeleteFlag == false).OrderBy(a => a.AssetCategory.Name).FirstOrDefault<AssetProperty>();
        }
        public List<AssetProperty> GetByCategoryId(long categoryId)
        {
            return dbContext.AssetProperties.Where(a => a.AssetCategoryId == categoryId && a.DeleteFlag == false).OrderBy(a => a.Name).ToList<AssetProperty>();
        }
        public int CountProperty(long assCateId)
        {
            return this.GetByCategoryId(assCateId).Count;
        }
        /// <summary>
        /// Get asset category
        /// </summary>
        /// <returns></returns>
        public List<sp_GetAssetPropertyResult> GetList(string searchText, int assetCateId)
        {
            return dbContext.sp_GetAssetProperty(searchText, assetCateId).ToList<sp_GetAssetPropertyResult>();            
        }

        public List<sp_GetAssetPropertyResult> Sort(List<sp_GetAssetPropertyResult> list, string sortColumn, string sortOrder)
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
                case "PropertyName":
                        list.Sort(
                        delegate(sp_GetAssetPropertyResult m1, sp_GetAssetPropertyResult m2)
                        {
                            int sortIndex = m1.AssetCategoryName.CompareTo(m2.AssetCategoryName) * 1;
                            if (sortIndex != 0) return sortIndex;
                            return m1.Name.CompareTo(m2.Name) * order;
                        });
                    break;
                case "AssetCategoryName":
                    list.Sort(
                         delegate(sp_GetAssetPropertyResult m1, sp_GetAssetPropertyResult m2)
                         {
                             int sortIndex = m1.AssetCategoryName.CompareTo(m2.AssetCategoryName) * 1;
                             if (sortIndex != 0) return sortIndex;
                             string categoryName1 = string.Empty;
                             string categoryName2 = string.Empty;
                           
                             if (!String.IsNullOrEmpty(m1.AssetCategoryName))
                             {
                                 categoryName1 = m1.AssetCategoryName;
                             }

                             if (!String.IsNullOrEmpty(m2.AssetCategoryName))

                             {
                                 categoryName2 = m2.AssetCategoryName;
                             }
                             return categoryName1.CompareTo(categoryName2) * order;
                         });
                    break;
                case "MasterData":
                    list.Sort(
                         delegate(sp_GetAssetPropertyResult m1, sp_GetAssetPropertyResult m2)
                         {
                             int sortIndex = m1.AssetCategoryName.CompareTo(m2.AssetCategoryName) * 1;
                             if (sortIndex != 0) return sortIndex;
                             string masterData1 = string.Empty;
                             string masterData2 = string.Empty;
                             if (m1.MasterData != null)
                             {
                                 masterData1 = m1.MasterData.ToString();
                             }
                             if (m2.MasterData != null)
                             {
                                 masterData2 = m2.MasterData.ToString();
                             }
                             return masterData1.CompareTo(masterData2) * order;
                         });
                    break;
                case "DisplayOrder":
                    list.Sort(
                         delegate(sp_GetAssetPropertyResult m1, sp_GetAssetPropertyResult m2)
                         {
                             int sortIndex = m1.AssetCategoryName.CompareTo(m2.AssetCategoryName) * 1;
                             if (sortIndex != 0) return sortIndex;
                             int displayOrder1 = 0;
                             int displayOrder2 = 0;
                             if (m1.DisplayOrder != null)
                             {
                                 displayOrder1 = (int)m1.DisplayOrder;
                             }
                             if (m2.DisplayOrder != null)
                             {
                                 displayOrder2 = (int)m2.DisplayOrder;
                             }
                             return displayOrder1.CompareTo(displayOrder2) * order;
                         });
                    break;
                case "CreatedBy":
                    list.Sort(
                         delegate(sp_GetAssetPropertyResult m1, sp_GetAssetPropertyResult m2)
                         {
                             int sortIndex = m1.AssetCategoryName.CompareTo(m2.AssetCategoryName) * 1;
                             if (sortIndex != 0) return sortIndex;
                             return m1.CreatedBy.CompareTo(m2.CreatedBy) * order; });
                    break;
                case "UpdatedBy":
                    list.Sort(
                         delegate(sp_GetAssetPropertyResult m1, sp_GetAssetPropertyResult m2)
                         {
                             int sortIndex = m1.AssetCategoryName.CompareTo(m2.AssetCategoryName) * 1;
                             if (sortIndex != 0) return sortIndex;
                             return m1.UpdatedBy.CompareTo(m2.UpdatedBy) * order; });
                    break;                
            }

            return list;
        }
        
        /// <summary>
        /// Insert asset category
        /// </summary>
        /// <param name="objUI"></param>
        /// <returns></returns>
        public Message Insert(AssetProperty objUI)
        {
            Message msg = null;
            try
            {
                if (!IsDublicateName(objUI))
                {
                    dbContext.AssetProperties.InsertOnSubmit(objUI);
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
                    int assCatId = 0;
                    int totalID = idArr.Count();

                    //loop each id to delete
                    foreach (string id in idArr)
                    {
                        //is check all records to delete 
                        bool isInterger = Int32.TryParse(id, out assCatId);
                        if (isInterger)
                        {
                            AssetProperty assetProperty = GetById(assCatId);
                            if (assetProperty != null)
                            {
                                assetProperty.UpdatedBy = userName;
                                msg = Delete(assetProperty);
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
                        msg = new Message(MessageConstants.I0001, MessageType.Info, totalID.ToString() + " customer name(s)", "deleted");
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
        /// Delete by set DeleteFlag = true
        /// </summary>
        /// <param name="objUI"></param>
        private Message Delete(AssetProperty objUI)
        {
            Message msg = null;
            try
            {
                if (objUI != null)
                {
                    AssetProperty objDb = GetById(objUI.ID);
                    if (objDb != null && !IsInUse(objUI))
                    {
                        List<AssetProperty> list = dbContext.AssetProperties.Where(c => c.ID == objUI.ID).ToList<AssetProperty>();

                        // Set delete info
                        objDb.DeleteFlag = true;
                        objDb.UpdateDate = DateTime.Now;
                        objDb.UpdatedBy = objUI.UpdatedBy;
                        msg = new Message(MessageConstants.I0001, MessageType.Info, "Asset Category", "deleted");
                    }
                    else
                    {
                        msg = new Message(MessageConstants.E0006, MessageType.Error, "delete", "it");
                    }
                    // Submit changes to dbContext

                    dbContext.SubmitChanges();
                }
            }
            catch
            {
                msg = new Message(MessageConstants.E0006, MessageType.Error, "delete", "it");
            }
            return msg;
        }
        
        /// <summary>
        /// Update asset category
        /// </summary>
        /// <param name="objUI"></param>
        /// <returns></returns>
        public Message Update(AssetProperty objUI)
        {
            Message msg = null;
            try
            {
                AssetProperty objDb = GetById(objUI.ID);

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

        /// <summary>
        /// Update Asset category
        /// </summary>
        /// <param name="objUI"></param>
        /// <param name="objDb"></param>
        /// <param name="msg"></param>
        private void Update(AssetProperty objUI, AssetProperty objDb, ref Message msg)
        {
            if (!IsDBChanged(objUI, objDb))
            {
                if (!IsDublicateName(objUI))
                {
                    objDb.Name = objUI.Name;
                    objDb.AssetCategoryId = objUI.AssetCategoryId;
                    objDb.DisplayOrder = objUI.DisplayOrder;
                    objDb.MasterData = objUI.MasterData;

                    objDb.UpdateDate = DateTime.Now;
                    objDb.UpdatedBy = objUI.UpdatedBy;

                    dbContext.SubmitChanges();
                    msg = new Message(MessageConstants.I0001, MessageType.Info, "Asset Property '" + objUI.Name + "'", "updated");
                }
                else
                {
                    msg = new Message(MessageConstants.E0020, MessageType.Error, "'" + objUI.Name + "'", "Asset Property");
                }
            }
            else
            {
                msg = new Message(MessageConstants.E0025, MessageType.Error, "Asset Category'" + objDb.Name + "'");
            }
        }

        private bool IsDublicateName(AssetProperty objUI)
        {
            bool isDublicateName = true;
            AssetProperty dublicateName = dbContext.AssetProperties.Where(a => a.Name.Equals(objUI.Name) && !a.DeleteFlag && a.AssetCategoryId.Equals(objUI.AssetCategoryId)).FirstOrDefault<AssetProperty>();
            if (dublicateName == null || dublicateName.ID == objUI.ID)
            {
                isDublicateName = false;
            }
            return isDublicateName;
        }

        public bool IsInUse(AssetProperty objUI)
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
        private bool IsDBChanged(AssetProperty objUI, AssetProperty objDb)
        {
            bool isChannged = true;
            if (objDb.UpdateDate.ToString() == objUI.UpdateDate.ToString())
            {
                isChannged = false; 
            }
            return isChannged;
       }
        
    }
}