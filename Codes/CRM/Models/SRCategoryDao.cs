using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CRM.Library.Common;
using System.Data.Common;

namespace CRM.Models
{
    public class SRCategoryDao:BaseDao
    {
        /// <summary>
        /// Get List og Category
        /// </summary>
        /// <param name="name">Category name</param>
        /// <param name="categoryID">Parent category id (=-1 -> get sub-category, =0 -> Get parent-category, =null -> get parent-category and sub-category)</param>
        /// <param name="isActive">=null -> active and inactive</param>
        /// <returns></returns>
        public List<sp_GetSRCategoryResult> GetList(string name, int? categoryID ,bool? isActive)
        {
            return dbContext.sp_GetSRCategory(name, categoryID, isActive).ToList();
        }

        public SR_Category GetCategoryParentBySub(int categoryID)
        {
            SR_Category objSub = GetById(categoryID,true);
            int parentID = objSub.ParentId.HasValue?objSub.ParentId.Value:0;
            return dbContext.SR_Categories.Where(q => q.ID == parentID).FirstOrDefault();
        }

        public List<sp_GetSRCategoryResult> Sort(List<sp_GetSRCategoryResult> categoryList, string sortColumn, string sortOrder)
        {
            int order = 1;
            if (sortOrder == "desc")
            {
                order = -1;
            }
            switch (sortColumn)
            {
                case "Name":
                    categoryList.Sort(
                         delegate(sp_GetSRCategoryResult category1, sp_GetSRCategoryResult category2)
                         { return category1.Name.CompareTo(category2.Name) * order; });
                    break;
                case "Category":
                    categoryList.Sort(
                         delegate(sp_GetSRCategoryResult category1, sp_GetSRCategoryResult category2)
                         { 
                            string parent1 = category1.ParentId.HasValue ? category1.ParentName : "";
                            string parent2 = category2.ParentId.HasValue ? category2.ParentName : "";
                            return parent1.CompareTo(parent2) * order; 
                         });
                    break;
            }
            return categoryList;
        }
        public SR_Category GetById(int id, bool? isActive)
        {
            return dbContext.SR_Categories.Where(p => 
                (isActive == null || p.IsActive == isActive) && p.ID == id).FirstOrDefault();
        }
        public SR_Category GetByName(string name, int? parentId, bool? isActive)
        {
            return dbContext.SR_Categories.Where(p => 
                (isActive == null || p.IsActive == isActive) && p.Name.Trim().Equals(name.Trim()) && 
                (parentId == null || p.ParentId == parentId)).FirstOrDefault();
        }
        private Message CheckExisted(SR_Category category)
        {
            SR_Category objDB = GetByName(category.Name, category.ParentId, null);
            if (objDB != null)
            {
                if (category.ID == objDB.ID)
                    return null;
                if (objDB.ParentId == null)
                    return new Message(MessageConstants.E0020, MessageType.Error, "Category \"" + category.Name + "\"", "database");
                return new Message(MessageConstants.E0020, MessageType.Error, "Sub-category \"" + category.Name + 
                        "\" of category \"" + objDB.SR_Category1.Name + "\"", "database");
            }
            //Return null if category does not exist
            return null;
        }
        public Message Insert(SR_Category category)
        {
            Message msg = null;
            try
            {
                msg = CheckExisted(category);
                if (msg != null)
                    return msg;
                category.CreateDate = category.UpdateDate = DateTime.Now;
                dbContext.SR_Categories.InsertOnSubmit(category);
                msg = new Message(MessageConstants.I0001, MessageType.Info, "Category \"" + category.Name + "\"", "inserted");
                dbContext.SubmitChanges();
            }
            catch
            {
                msg = new Message(MessageConstants.E0007, MessageType.Error);
            }
            return msg;
        }


        public Message Update(SR_Category category)
        {
            Message msg = null;
            try
            {
                msg = CheckExisted(category);
                if (msg != null)
                    return msg;
                SR_Category objDB = GetById(category.ID, null);
                objDB.Description = category.Description;
                objDB.DisplayOrder = category.DisplayOrder;
                objDB.IsActive = category.IsActive;
                objDB.Name = category.Name;
                objDB.ParentId = category.ParentId;
                objDB.UpdateDate = DateTime.Now;
                objDB.UpdatedBy = category.UpdatedBy;
                //dbContext.SR_Categories.InsertOnSubmit(category);
                msg = new Message(MessageConstants.I0001, MessageType.Info, "Category \"" + category.Name + "\"", "updated");
                dbContext.SubmitChanges();
            }
            catch
            {
                msg = new Message(MessageConstants.E0007, MessageType.Error);
            }
            return msg;
        }


        public List<SR_Category> GetList()
        {
            return dbContext.SR_Categories.Where(c => c.ParentId == null && c.DeleteFlag == false).ToList<SR_Category>();
        }

        public List<SR_Category> GetSubList()
        {
            return dbContext.SR_Categories.Where(c => c.ParentId != null && c.DeleteFlag == false).ToList<SR_Category>();
        }

        public Message Delete(int id)
        {
            try
            {
                SR_Category cate = GetById(id, null);
                cate.DeleteFlag = true;
                cate.ParentId = null;
                cate.UpdateDate = DateTime.Now;
                cate.UpdatedBy = HttpContext.Current.User.Identity.Name;
                Message msg = new Message(MessageConstants.I0001, MessageType.Info, "Category " + cate.Name, "deleted");
                dbContext.SubmitChanges();
                return msg;
            }
            catch
            {
                return new Message(MessageConstants.E0007, MessageType.Error);
            }
        }

        public Message Delete(string[] idArr)
        {
            Message msg = null;
            DbTransaction trans = null;
            try
            {
                dbContext.Connection.Open();
                trans = dbContext.Connection.BeginTransaction();
                dbContext.Transaction = trans;
                foreach (string id in idArr)
                    Delete(ConvertUtil.ConvertToInt(id));
                if (idArr.Length > 1)
                    msg = new Message(MessageConstants.I0011, MessageType.Info, 
                        idArr.Length + " categories have been deleted");
                else
                    msg = new Message(MessageConstants.I0011, MessageType.Info,
                        idArr.Length + " category has been deleted");
                trans.Commit();
            }
            catch
            {
                trans.Rollback();
                msg = new Message(MessageConstants.E0007, MessageType.Error);
            }
            return msg;
        }

        public Message UpdateActiveStatus(int id, bool isActive)
        {
            try
            {
                SR_Category cate = GetById(id, null);
                if (cate == null)
                    return new Message(MessageConstants.E0005, MessageType.Error,
                        "Selected category", "system");
                cate.IsActive = isActive;
                dbContext.SubmitChanges();
                return new Message(MessageConstants.I0001, MessageType.Info,
                    "Category \"" + cate.Name + "\"", "set " + (isActive ? "active" : "inactive"));
            }
            catch
            {
                return new Message(MessageConstants.E0007, MessageType.Error);
            }
        }

        
    }
}