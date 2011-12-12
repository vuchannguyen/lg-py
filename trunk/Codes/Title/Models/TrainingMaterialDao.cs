using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using CRM.Library.Common;
using System.Data.Common;
using System.IO;

namespace CRM.Models
{
    /// <summary>
    /// Training Material Dao class
    /// </summary>
    public class TrainingMaterialDao : BaseDao
    {
        public List<string> GetCategoryList()
        {
            return dbContext.Training_Materials.Where(p=>p.Category != null && p.Category!= "" && !p.DeleteFlag).
                Select(p=>p.Category).Distinct().ToList();
        }
        public List<ListItem> GetListCategory()
        {
            List<string> listCategory = dbContext.Training_Materials.Select(q => q.Category).Distinct().ToList<string>();
            List<ListItem> result = new List<ListItem>();
            foreach (string item in listCategory)
            {
                result.Add(new ListItem(item, item));
            }
            return result.ToList();
        }
        /// <summary>
        /// Get material list for Admin
        /// </summary>
        /// <param name="name">Title or Description</param>
        /// <param name="nType">Search type: 1 -> procourse, 2 -> Englishcourse, 3 -> Category</param>
        /// <param name="nCourseId"></param>
        /// <param name="category"></param>
        /// <param name="isActive">active or not or whatever</param>
        /// <returns></returns>
        public List<sp_GetListMaterialResult> GetList(string employeeId, string name, int? nType, int? nCourseId, string category, bool? isActive)
        {
            return dbContext.sp_GetListMaterial( employeeId, name, nType, nCourseId, category, isActive, 
                Constants.TRAINING_CENTER_COURSE_TYPE_PRO_SKILL, Constants.TRAINING_CENTER_COURSE_TYPE_ENGLISH,
                Constants.TRAINING_MATERIAL_TYPE_COURSE, Constants.TRAINING_MATERIAL_TYPE_CATEGORY, 
                Constants.TRAINING_CENTER_MATERIAL_SEARCH_TYPE_PRO_COURSE, Constants.TRAINING_CENTER_MATERIAL_SEARCH_TYPE_ENGLISH_COURSE,
                Constants.TRAINING_CENTER_MATERIAL_SEARCH_TYPE_CATEGORY, Constants.TRAINING_MATERIAL_PERMISSON_PUBLIC,
                Constants.TRAINING_MATERIAL_PERMISSON_INCOURSE, Constants.TRAINING_MATERIAL_PERMISSON_ADMIN).ToList();
        }

        public int CountMaterialByTrainingCourseId(int id)
        {
            return dbContext.Training_Materials.Where(p => p.CourseId.HasValue && p.CourseId.Value == id && !p.DeleteFlag && p.Permission != Constants.TRAINING_MATERIAL_PERMISSON_ADMIN).Count();
        }

        public List<sp_GetListMaterialResult> Sort(List<sp_GetListMaterialResult> list, string sortColumn, string sortOrder)
        {
            int order = 1;
            if (sortOrder == "desc")
            {
                order = -1;
            }
            switch (sortColumn)
            {
                case "DisplayOrder":
                    list.Sort(
                         delegate(sp_GetListMaterialResult m1, sp_GetListMaterialResult m2)
                         { return m1.DisplayOrder.CompareTo(m2.DisplayOrder) * order; });
                    break;
            }
            return list;
        }

        public Message Insert(Training_Material objUI)
        {
            Message msg = null;
            try
            {
                if (objUI != null)
                {
                    objUI.DeleteFlag = false;
                    objUI.DisplayOrder = 1;
                    objUI.CreateDate = objUI.UpdateDate = DateTime.Now;
                    dbContext.Training_Materials.InsertOnSubmit(objUI);
                    dbContext.SubmitChanges();
                    // Show success message
                    msg = new Message(MessageConstants.I0001, MessageType.Info, "Material Title '" + objUI.Title + "'", "added");
                }
            }
            catch (Exception ex)
            {
                msg = new Message(MessageConstants.E0007, MessageType.Error);
                throw ex;
            }
            return msg;

        }

        public Message Update(Training_Material objUI)
        {
            Message msg = null;
            try
            {
                Training_Material objDb = GetByID(objUI.ID);
                if(objDb != null)
                {
                    objDb.TypeOfMaterial = objUI.TypeOfMaterial;
                    objDb.CourseId = objUI.CourseId;
                    objDb.Category = objUI.Category;
                    objDb.UploadFile = objUI.UploadFile;
                    objDb.UploadFileDisplayName = objUI.UploadFileDisplayName;
                    objDb.Title = objUI.Title;
                    objDb.Permission = objUI.Permission;
                    objDb.IsActive = objUI.IsActive;
                    objDb.Description = objUI.Description;
                    objDb.UpdateDate = DateTime.Now;
                    objDb.UpdatedBy = objUI.UpdatedBy;
                    dbContext.SubmitChanges();
                    // Show success message
                    msg = new Message(MessageConstants.I0001, MessageType.Info, "Material Title '" + objUI.Title + "'", "updated");
                }
            }
            catch (Exception ex)
            {
                msg = new Message(MessageConstants.E0007, MessageType.Error);
                throw ex;
            }
            return msg;

        }

        public Training_Material GetByID(int id)
        {
            return dbContext.Training_Materials.Where(q => q.ID == id && !q.DeleteFlag).FirstOrDefault();
        }

        public void UpdateDownloadTimes(int materialId)
        {
            try
            {
                var material = GetById(materialId, null);
                material.DownloadTimes += 1;
                dbContext.SubmitChanges();
            }
            catch
            {
                return;
            }
        }

        public Training_Material GetById(int materialId, bool? isActive)
        {
            return dbContext.Training_Materials.FirstOrDefault(p=>!p.DeleteFlag && 
                (p.IsActive == isActive || isActive == null) && p.ID == materialId);
        }

        public List<sp_TC_GetMaterialListResult> GetMaterialList(int type)
        {
            return dbContext.sp_TC_GetMaterialList(type).ToList();
        }
        

        public void Move(Training_Material material, bool isUp)
        {
            try
            {
                if (material == null)
                    return;
                int? searchType = null;
                if (material.TypeOfMaterial == Constants.TRAINING_MATERIAL_TYPE_CATEGORY)
                    searchType = Constants.TRAINING_CENTER_MATERIAL_SEARCH_TYPE_CATEGORY;
                else if(material.Training_Course.TypeOfCourse == Constants.TRAINING_CENTER_COURSE_TYPE_PRO_SKILL)
                    searchType = Constants.TRAINING_CENTER_MATERIAL_SEARCH_TYPE_PRO_COURSE;
                else
                    searchType = Constants.TRAINING_CENTER_MATERIAL_SEARCH_TYPE_ENGLISH_COURSE;
                var list = GetList(null, null, searchType, material.CourseId, material.Category, null);
                var mat = list.FirstOrDefault(p => p.ID == material.ID);
                int matIndex = list.IndexOf(mat);
                bool isSwap = false;
                int swappedId = 0;
                //Move up
                if (isUp && list.First().ID != mat.ID)
                {
                    isSwap = true;
                    swappedId = list[matIndex - 1].ID;
                }
                //Move Down
                else if(!isUp && list.Last().ID != mat.ID)
                {
                    isSwap = true;
                    swappedId = list[matIndex + 1].ID;
                }
                if (isSwap)
                {
                    var mat1 = GetById(material.ID, null);
                    var mat2 = GetById(swappedId, null);
                    int tmp = mat1.DisplayOrder;
                    mat1.DisplayOrder = mat2.DisplayOrder;
                    mat2.DisplayOrder = tmp;
                    mat1.UpdateDate = mat2.UpdateDate = DateTime.Now;
                    mat1.UpdatedBy = mat2.UpdatedBy = HttpContext.Current.User.Identity.Name;
                    dbContext.SubmitChanges();
                }
            }
            catch
            {
                return;
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
                        idArr.Length + " materials have been deleted");
                else
                    msg = new Message(MessageConstants.I0011, MessageType.Info,
                        idArr.Length + " material has been deleted");
                trans.Commit();
            }
            catch
            {
                trans.Rollback();
                msg = new Message(MessageConstants.E0007, MessageType.Error);
            }
            return msg;
        }

        public Message Delete(int id)
        {
            try
            {
                Training_Material mat = GetById(id, null);
                mat.DeleteFlag = true;
                mat.UpdateDate = DateTime.Now;
                mat.UpdatedBy = HttpContext.Current.User.Identity.Name;
                Message msg = new Message(MessageConstants.I0001, MessageType.Info, "Material", "deleted");
                //Delete File
                string filepath = HttpContext.Current.Server.MapPath(Constants.TRAINING_CENTER_MATERIAL_UPLOAD_FOLDER + mat.UploadFile);
                if (File.Exists(filepath))
                    File.Delete(filepath);
                dbContext.SubmitChanges();
                return msg;
            }
            catch
            {
                return new Message(MessageConstants.E0007, MessageType.Error);
            }
        }
    }
}