using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text.RegularExpressions;
using System.Web.Mvc;
using CRM.Library.Common;

namespace CRM.Models
{
    public class DepartmentDao : BaseDao
    {
        #region Public methods

        public List<Department> GetList()
        {
            return dbContext.Departments.Where(c => c.ParentId == null && c.DeleteFlag == false).OrderBy(p => p.DepartmentName).ToList<Department>();
        }

        public Department GetByName(string name)
        {
            return dbContext.Departments.Where(c => c.DepartmentName == name && c.DeleteFlag == false).FirstOrDefault<Department>();
        }

        public Department GetByNameContain(string name)
        {

            return dbContext.Departments.Where(c => c.ParentId != null && c.DepartmentName.Equals(name) && c.DeleteFlag == false).FirstOrDefault<Department>();
        }

        public List<Department> GetSubList()
        {
            return dbContext.Departments.Where(c => c.ParentId != null && c.DeleteFlag == false).OrderBy(p => p.DepartmentName).ToList<Department>();
        }
        public Department GetById(int id)
        {
            return dbContext.Departments.Where(c => c.DepartmentId.Equals(id) && c.DeleteFlag == false).FirstOrDefault<Department>();
        }


        public List<Department> GetSubListByParent(int parent)
        {
            return dbContext.Departments.Where(c => c.ParentId != null && c.DeleteFlag == false && c.ParentId.Value.Equals(parent)).OrderBy(p => p.DepartmentName).ToList<Department>();
        }

        public List<Department> GetParentById(int id)
        {
            return dbContext.Departments.Where(c => c.DeleteFlag == false && c.ParentId != null && c.DepartmentId.Equals(id)).OrderBy(p => p.DepartmentName).ToList<Department>();
        }

        public int? GetDepartmentIdBySub(int? subDepartment)
        {
            return dbContext.GetDepartmentIdBySubId(subDepartment);
        }

        public string GetDepartmentNameBySub(int? subDepartment)
        {
            return dbContext.GetDepartmentNameBySubId(subDepartment);
        }

        public string GetDepartmentNameBySubDepartmentName(string subDepartmentName)
        {
            return dbContext.GetDepartmentNameBySubDepartmentName(subDepartmentName);
        }

        #endregion

        public List<Department> GetLikeName(string departmentName, bool getOne)
        {
            //Remove duplicated space from department name
            departmentName = Regex.Replace(departmentName.Trim().ToLower(), @"\b\s+\b", " ");
            string[] arrDepName = departmentName.Split(' ', '.');
            List<Department> depList = dbContext.Departments.OrderBy(p => p.DepartmentName).ToList<Department>();
            foreach (string part in arrDepName)
            {
                depList = depList.Where(p => p.DepartmentName.ToLower().Trim().Contains(part)).ToList();
            }
            if (getOne && depList.Count > 1)
                depList = depList.Where(p =>
                    p.DepartmentName.Replace(". ", " ").Split(' ', '.').Length == arrDepName.Length).ToList();
            return depList;
        }

        // triet.dinh 21-12-2011
        /// <summary>
        /// Get list Department by hierarchy.
        /// </summary>
        /// <param name="selectedValue"></param>
        /// <returns></returns>
        public List<SelectListItem> GetDepartmentByHierarchy(object selectedValue = null)
        {
            var list = dbContext.sp_GetDepartmentByHierarchy().ToList();

            List<SelectListItem> listDepartment = new List<SelectListItem>();
            foreach (sp_GetDepartmentByHierarchyResult obj in list)
            {
                SelectListItem item = new SelectListItem();
                item.Value = obj.DepartmentId.ToString();
                item.Text = HttpUtility.HtmlDecode(obj.TreeDepartmentName);
                if (item.Value == ConvertUtil.ConvertToString( selectedValue)) item.Selected = true;
                listDepartment.Add(item);
            }
            return listDepartment.ToList();
        }

        /// <summary>
        /// Get root department id by sub department id
        /// </summary>
        /// <param name="subDeptId"></param>
        /// <returns></returns>
        public int GetDepartmentRoot(int subDeptId)
        {
            sp_GetDepartmentRootResult deptRoot = dbContext.sp_GetDepartmentRoot(subDeptId).First();
            if (deptRoot != null) 
                return ConvertUtil.ConvertToInt(deptRoot.DepartmentRootId);
            return 0;
        }
    }
}