using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CRM.Library.Common;
using System.Data.Common;

namespace CRM.Models
{
    public class AssetCategoryDao : BaseDao
    {
        /// <summary>
        /// Get asset category by Id
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public AssetCategory GetById(int Id)
        {
            return dbContext.AssetCategories.Where(a => a.ID == Id && a.DeleteFlag == false).FirstOrDefault<AssetCategory>();
        }

        /// <summary>
        /// Get asset category
        /// </summary>
        /// <returns></returns>
        public List<AssetCategory> GetList(string searchText)
        {
            return dbContext.AssetCategories.Where(a => (a.Name.Contains(searchText)
                || (a.Description != null && a.Description.Contains(searchText))) && a.DeleteFlag == false).ToList<AssetCategory>();            
        }
        // Get Asset Category List
        public List<AssetCategory> GetAssetCategoryList()
        {
            return dbContext.AssetCategories.Where(a => a.DeleteFlag == false && a.IsActive == true).ToList();
        }

        public List<AssetCategory> GetList(bool isActive)
        {
            return dbContext.AssetCategories.Where(a => (a.IsActive == isActive) && a.DeleteFlag == false).ToList<AssetCategory>();
        }

        public List<AssetCategory> Sort(List<AssetCategory> list, string sortColumn, string sortOrder)
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
                case "CategoryName":
                    list.Sort(
                         delegate(AssetCategory m1, AssetCategory m2)
                         { return m1.ID.CompareTo(m2.ID) * order; });
                    break;
                case "Description":
                    list.Sort(
                         delegate(AssetCategory m1, AssetCategory m2)
                         {
                             string displayname1 = string.Empty;
                             string displayname2 = string.Empty;
                             if (m1.Description != null)
                             {
                                 displayname1 = (string)m1.Description;
                             }
                             if (m2.Description != null)
                             {
                                 displayname2 = (string)m2.Description;
                             }
                             return displayname1.CompareTo(displayname2) * order;
                         });
                    break;
                case "IsActive":
                    list.Sort(
                         delegate(AssetCategory m1, AssetCategory m2)
                         { return m1.IsActive.CompareTo(m2.IsActive) * order; });
                    break;
                case "CreatedBy":
                    list.Sort(
                         delegate(AssetCategory m1, AssetCategory m2)
                         { return m1.CreatedBy.CompareTo(m2.CreatedBy) * order; });
                    break;
                case "UpdatedBy":
                    list.Sort(
                         delegate(AssetCategory m1, AssetCategory m2)
                         { return m1.UpdatedBy.CompareTo(m2.UpdatedBy) * order; });
                    break;                
            }

            return list;
        }
        
        /// <summary>
        /// Insert asset category
        /// </summary>
        /// <param name="objUI"></param>
        /// <returns></returns>
        public Message Insert(AssetCategory objUI)
        {
            Message msg = null;
            try
            {
                if (!isDublicateName(objUI))
                {
                    dbContext.AssetCategories.InsertOnSubmit(objUI);
                    dbContext.SubmitChanges();
                    msg = new Message(MessageConstants.I0001, MessageType.Info, "Asset Category '" + objUI.Name + "'", "added");
                }
                else
                {
                    msg = new Message(MessageConstants.E0020, MessageType.Error, "Name '" + objUI.Name + "'", "Asset Category");
                }
            }
            catch (Exception)
            {
                msg = new Message(MessageConstants.E0007, MessageType.Error);
            }
            return msg;
        }

        /// <summary>
        /// Delete asset categories
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
                            AssetCategory assetCategory = GetById(assCatId);
                            if (assetCategory != null)
                            {
                                assetCategory.UpdatedBy = userName;
                                msg = Delete(assetCategory);
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
        private Message Delete(AssetCategory objUI)
        {
            Message msg = null;
            try
            {
                if (objUI != null)
                {
                    AssetCategory objDb = GetById(objUI.ID);
                    if (objDb != null && !IsInUse(objUI))
                    {
                        List<AssetCategory> listAssertCategory = dbContext.AssetCategories.Where(c => c.ID == objUI.ID).ToList<AssetCategory>();

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
        public Message Update(AssetCategory objUI)
        {
            Message msg = null;
            try
            {
                AssetCategory objDb = GetById(objUI.ID);

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
        private void Update(AssetCategory objUI, AssetCategory objDb, ref Message msg)
        {
            if (!IsDBChanged(objUI, objDb))
            {
                if (!isDublicateName(objUI))
                {
                    objDb.Name = objUI.Name;
                    objDb.Description = objUI.Description;
                    objDb.IsActive = objUI.IsActive;

                    objDb.UpdateDate = DateTime.Now;
                    objDb.UpdatedBy = objUI.UpdatedBy;

                    dbContext.SubmitChanges();
                    msg = new Message(MessageConstants.I0001, MessageType.Info, "Asset Category '" + objUI.Name + "'", "updated");
                }
                else
                {
                    msg = new Message(MessageConstants.E0020, MessageType.Error, "'" + objUI.Name + "'", "Asset Category");
                }
            }
            else
            {
                msg = new Message(MessageConstants.E0025, MessageType.Error, "Asset Category'" + objDb.Name + "'");
            }
        }

        private bool isDublicateName(AssetCategory objUI)
        {
            bool isDublicateName = true;
            AssetCategory dublicateName = dbContext.AssetCategories.Where(a => a.Name.Equals(objUI.Name) && a.DeleteFlag == false).FirstOrDefault<AssetCategory>();
            if (dublicateName == null || dublicateName.ID == objUI.ID)
            {
                isDublicateName = false;
            }
            return isDublicateName;
        }

        public bool IsInUse(AssetCategory objUI)
        {
            AssetPropertyDao assPropDao = new AssetPropertyDao();
            AssetMasterDao assMasterDao = new AssetMasterDao();
            if (assPropDao.GetByCategoryId(objUI.ID).Count == 0 && assMasterDao.GetListByCategoryId(objUI.ID).Count == 0)
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
        private bool IsDBChanged(AssetCategory objUI, AssetCategory objDb)
        {
            bool isChannged = true;
            if (objDb.UpdateDate.ToString() == objUI.UpdateDate.ToString())
            {
                isChannged = false; 
            }
            return isChannged;
        }

        public Message UpdateActiveStatus(int customerId, bool isActive)
        {
            try
            {
                AssetCategory obj = dbContext.AssetCategories.Where(p => p.ID == customerId).FirstOrDefault();
                if (obj == null)
                    return new Message(MessageConstants.E0005, MessageType.Error,
                        "Selected customer", "system");
                obj.IsActive = isActive;
                dbContext.SubmitChanges();
                return new Message(MessageConstants.I0001, MessageType.Info, "Customer " + obj.Name, "set " + (isActive ? "active" : "inactive"));
            }
            catch
            {
                return new Message(MessageConstants.E0007, MessageType.Error);
            }
        }

    }
}