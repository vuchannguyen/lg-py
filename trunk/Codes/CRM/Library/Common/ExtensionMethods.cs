using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text.RegularExpressions;
using System.Text;
using CRM.Models;

namespace CRM.Library.Common
{
    public static class ExtensionMethods
    {
        private static EmployeeDao empDao = new EmployeeDao();
        private static MenuDao menuDao = new MenuDao();

        public static string RemoveNonNumeric(this string s, int n)
        {
            MatchCollection col = Regex.Matches(s, "[0-9]");
            StringBuilder sb = new StringBuilder();
            foreach (Match m in col)
                sb.Append(m.Value);
            return sb.ToString();
        }
        public static bool IsManager(this Employee emp)
        {
            return empDao.GetById(emp.ID).JobTitleLevel.JobTitle.IsManager;
        }
        public static Employee ParseEmployee(this STT stt)
        {
            Employee emp = new Employee();
            emp.ID = stt.ID;
            emp.FirstName = stt.FirstName;
            emp.VnFirstName = stt.VnFirstName;
            emp.MiddleName = stt.MiddleName;
            emp.VnMiddleName = stt.VnMiddleName;
            emp.LastName = stt.VnLastName;
            //emp.Employee1 = stt.Employ
            emp.Project = stt.Project;
            emp.ManagerId = stt.ManagerId;
            emp.Floor = stt.Floor;
            emp.SeatCode = stt.SeatCode;
            emp.CreatedBy = stt.CreatedBy;
            emp.CreateDate = stt.CreateDate;
            emp.UpdateDate = stt.UpdateDate;
            emp.UpdatedBy = stt.UpdatedBy;
            emp.DepartmentId = stt.DepartmentId;
            emp.Department = stt.Department;
            emp.LocationCode = stt.LocationCode;
            return emp;
        }
        public static List<Employee> ParseEmployeeList(this List<STT> sttList)
        {
            List<Employee> empList = new List<Employee>();
            foreach (STT stt in sttList)
            {
                stt.ID = Constants.STT_ID_PREFIX + stt.ID;
                empList.Add(stt.ParseEmployee());
            }
            return empList;
        }
        public static bool IsSttId(this string empId)
        {
            return Regex.IsMatch(empId, @"\b" + Constants.STT_ID_PREFIX);
        }
        public static string GetRealEmpId(this string empId)
        {
            if (empId.IsSttId())
                return empId.Substring(Constants.STT_ID_PREFIX.Length);
            return empId;
        }
        public static bool IsEmployeeId(this string empId)
        {
            return Regex.IsMatch(empId, @"\b" + Constants.EMP_ID_PREFIX);
        }

        public static bool IsStt(this string empId)
        {
            string pattern = @"^\w{3}\d{3}(-\d{3})$";
            return Regex.IsMatch(empId, pattern);
        }

        private static string menuItemFormat = "<li><a href=\"{0}\" title=\"{1}\" class=\"{2}\">{3}{4}</a>{5}{6}</li>";
        private static string imgFormat = "<img align=\"absmiddle\" style=\"width:16px;height:16px\" src=\"{0}\"/>";

        public static string GetDisplayString(List<Menu> menuList, Menu menu)
        {
            if (menu == null)
                return string.Empty;
            List<Menu> childList = menuDao.GetChild(menuList, menu.ID, null);
            string result = "";
            /// <summary>
            /// 0: link
            /// 1: title
            /// 2: link class
            /// 3: image tag
            /// 4: display text
            /// 5: Edit button
            /// 6: Child menu
            /// </summary>

            string editAction = "showPopup('/Menu/Edit/" + menu.ID + "','Edit menu " + menu.Name + "', 550)";
            string addAction = "showPopup('/Menu/Create/" + menu.ID + "','Add child for " + menu.Name + "', 550)";
            string sMessage = string.Format(Resources.Message.I0004, "delete menu " + menu.Name);
            //string deleteAction = string.Format("CRM.confirmDelete('{0}')", "/Menu/Delete/" + menu.ID);
            //{0}: action
            //{1}: id
            //{2}: url redirect
            //{3}: listname
            string deleteAction = string.Format("CRM.deleteItemConfirm('{0}','{1}','{2}')", "/Menu/Delete", menu.ID, "/Menu");
            string img = string.IsNullOrEmpty(menu.ImageUrl) ? string.Empty : string.Format(imgFormat,
                Constants.MENU_PAGE_ICON_FOLDER + menu.ImageUrl);
            string link = "javascript:" + editAction;
            string title = "Click to Edit";
            string editButton = "<span style=\"padding-left:10px;\">" +
                CommonFunc.Button("add", "Add Child Menu", addAction) +
                CommonFunc.Button("delete", "Delete", deleteAction) + "</span>";
            if (childList.Count > 0)
            {
                string childItem = "";
                foreach (Menu child in childList)
                    childItem +=  GetDisplayString(menuList, child);
                result = string.Format(menuItemFormat, link, title, menu.IsActive ? "menu_active" : "menu_inactive", 
                    img, menu.Name, editButton, "<ul>" + childItem + "</ul>");
            }
            else
            {
                result = string.Format(menuItemFormat, link, title, menu.IsActive ? "menu_active" : "menu_inactive", 
                    img, menu.Name, editButton, string.Empty);
            }
            return result;
        }
        public static string RecursionMenu(List<Menu> menuList, Menu menu, List<sp_GetModulePermissionResult> modulePermissionList)
        {
            List<Menu> childList = menuDao.GetChild(menuList, menu.ID);
            string result = "";
            /// <summary>
            /// 0: link            
            /// 1: image tag
            /// 2: display text            
            /// 3: Child menu
            /// </summary>

            string img = string.IsNullOrEmpty(menu.ImageUrl) ? string.Empty : 
                string.Format(menuItemImage, Constants.MENU_PAGE_ICON_FOLDER + menu.ImageUrl);
            string link = string.IsNullOrEmpty(menu.Link) ? "javascript:void(0);" : menu.Link;

            if (childList.Count > 0)
            {
                string childItem = "";
                foreach (Menu child in childList)
                    childItem += RecursionMenu(menuList, child, modulePermissionList);
                if(!string.IsNullOrEmpty(childItem))
                    result = string.Format(menuItem, link, img, menu.Name, "<ul>" + childItem + "</ul>");
            }
            else
            {
                bool isDisplay = false;
                if (menu.MenuPermissions.Count == 0)
                    isDisplay = true;
                else
                {
                    foreach (var menuPer in menu.MenuPermissions)
                    {
                        isDisplay = modulePermissionList.Where(p => p.ModuleId == menuPer.ModuleId &&
                            p.PermissionId == menuPer.PermissionId).Count() > 0 ? true : false;

                    }
                }
                if(isDisplay)
                    result = string.Format(menuItem, link, img, menu.Name, string.Empty);
            }
            return result;
        }
        //Added by Huy Ly 6-1-2011
        //Generate Menu ==> Doing...
        private static string menuItem = "<li><a href=\"{0}\">{1}{2}</a>{3}</li>";
        private static string menuItemImage = "<img align=\"absmiddle\" style=\"width:16px;height:16px\" src=\"{0}\"/>";
        public static string RecursionMenu(List<Menu> menuList, Menu menu)
        {            
            List<Menu> childList = menuDao.GetChild(menuList, menu.ID);
            string result = "";
            /// <summary>
            /// 0: link            
            /// 1: image tag
            /// 2: display text            
            /// 3: Child menu
            /// </summary>

            string img = string.IsNullOrEmpty(menu.ImageUrl) ? string.Empty : string.Format(menuItemImage, menu.ImageUrl);
            string link = string.IsNullOrEmpty(menu.Link) ? "javascript:void(0);" : menu.Link;

            if (childList.Count > 0)
            {
                string childItem = "";
                foreach (Menu child in childList)
                    childItem += RecursionMenu(menuList, child);
                result = string.Format(menuItem, link, img, menu.Name, "<ul>" + childItem + "</ul>");
            }
            else
            {
                result = string.Format(menuItem, link, img, menu.Name, string.Empty);
            }
            return result;
        }
        
        public static string CreateMenu()
        {
            string result = string.Empty;
            List<Menu> menuList = menuDao.GetList(true);
            List<Menu> menuNotChild = menuDao.GetNotChildList(menuList);
            foreach (Menu menu in menuNotChild)
            {
                result += RecursionMenu(menuList, menu);
            }
            return result;
        }

        #region tam thoi comment sua code- Chau.Ly- 03-02-2012
        //public static double? GetEnglishSkillMark(this LOT_Candidate_Exam candidateExam)
        //{
        //    return new CandidateExamDao().GetEnglishSkillMark(candidateExam.ID);
        //}
        //public static int? GetEnglishSkillMaxMark(this LOT_Candidate_Exam candidateExam)
        //{
        //    return new CandidateExamDao().GetEnglishSkillMaxMark(candidateExam.ID);
        //}
        //public static double? GetMarkOfSection(this LOT_Candidate_Exam candidateExam, int sectionId)
        //{
        //    return new CandidateExamDao().GetMarkOfSection(candidateExam.ID, sectionId);
        //}
        //public static int? GetMaxMarkOfSection(this LOT_Candidate_Exam candidateExam, int sectionId)
        //{
        //    return new CandidateExamDao().GetMaxMarkOfSection(candidateExam.ID, sectionId);
        //}
        #endregion
    }
}