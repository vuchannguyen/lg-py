using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using CRM.Models;
using System.Collections;
using CRM.Library.Common;
using CRM.Areas.Portal.Models;
using System.Web.UI.WebControls;

namespace CRM.Library.GenericHandle
{
    /// <summary>
    /// Summary description for DropdownListHandler
    /// </summary>
    public class DropdownListHandler : IHttpHandler
    {
        #region private variablees
        private ResolutionDao resolutionDao = new ResolutionDao();
        private RoleDao roleDao = new RoleDao();
        private PositionDao posDao = new PositionDao();
        private CommonDao commDao = new CommonDao();
        private JobRequestDao jrDao = new JobRequestDao();
        private PerformanceReviewDao prDao = new PerformanceReviewDao();
        private StatusDao staDao = new StatusDao();
        private PurchaseRequestDao purchaseDao = new PurchaseRequestDao();
        private UserAdminDao userAdminDao = new UserAdminDao();
        private DepartmentDao deptDao = new DepartmentDao(); //triet.dinh 27-12-2011
        #endregion
        /// <summary>
        /// Get Json String View Multi Column in Dropdownlist
        /// </summary>
        /// <param name="list">List of data </param>
        /// <param name="columns">Array column data</param>
        /// <returns></returns>
        private StringBuilder GetStringBuilder(IEnumerable list, string[] columns)
        {
            
               
            StringBuilder strBuilder = new StringBuilder();
            if (((IList)list).Count==0)
                return strBuilder;
            strBuilder.Append("[");
            foreach (Object item in list)
            {
                strBuilder.Append("{");
                foreach (string column in columns)
                {
                    strBuilder.Append("\"" + column + "\":\"" + item.GetType().GetProperty(column).GetValue(item, null) + "\",");
                }
                strBuilder.Remove(strBuilder.Length - 1, 1);
                strBuilder.Append("},");
               
            }
            strBuilder.Remove(strBuilder.Length - 1, 1);
            strBuilder.Append("]");
            return strBuilder;
        }
        /// <summary>
        /// Get Json String View Column name in Dropdownlist
        /// </summary>
        /// <param name="list">List of data</param>
        /// <param name="DataTextFiled">Column name view Text</param>
        /// <param name="DataValueField">Column name Value</param>
        /// <returns></returns>
        private StringBuilder GetStringBuilder(IEnumerable list,string DataTextFiled,string DataValueField)
        {


            StringBuilder strBuilder = new StringBuilder();
            if (((IList)list).Count == 0)
                return strBuilder;
            strBuilder.Append("[");
            foreach (Object item in list)
            {
                strBuilder.Append("{");
                strBuilder.Append("\"Name\":\"" + item.GetType().GetProperty(DataTextFiled).GetValue(item, null) + "\",");
                //strBuilder.Append("\"ID\":\"" + item.GetType().GetProperty(DataValueField).GetValue(item, null) + "\"");
                string id = item.GetType().GetProperty(DataValueField).GetValue(item, null).ToString();
                if (id.Equals("0"))
                    id = "";
                strBuilder.Append("\"ID\":\"" + id + "\"");

                strBuilder.Append("},");

            }
            strBuilder.Remove(strBuilder.Length - 1, 1);
            strBuilder.Append("]");
            return strBuilder;
        }

        //private StringBuilder GetStringBuilder(IEnumerable list, string DataTextFiled, string DataValueField, string selectedValue)
        //{


        //    StringBuilder strBuilder = new StringBuilder();
        //    if (((IList)list).Count == 0)
        //        return strBuilder;
        //    strBuilder.Append("[");
        //    foreach (Object item in list)
        //    {
        //        strBuilder.Append("{");
        //        strBuilder.Append("\"Name\":\"" + item.GetType().GetProperty(DataTextFiled).GetValue(item, null) + "\",");
        //        //strBuilder.Append("\"ID\":\"" + item.GetType().GetProperty(DataValueField).GetValue(item, null) + "\"");
        //        string id = item.GetType().GetProperty(DataValueField).GetValue(item, null).ToString();
        //        if (id.Equals("0"))
        //            id = "";
        //        strBuilder.Append("\"ID\":\"" + id + "\"");
        //        if(id.Equals(selectedValue))
        //            strBuilder.Append("\"Selected\":\"" + true + "\"");
        //        strBuilder.Append("},");

        //    }
        //    strBuilder.Remove(strBuilder.Length - 1, 1);
        //    strBuilder.Append("]");
        //    return strBuilder;
        //}
        public void ProcessRequest(HttpContext context)
        {

            string ID = context.Request.QueryString["ID"];
            string pageName = context.Request.QueryString["Page"];
            StringBuilder strBuilder = new StringBuilder();
            bool toGroup = false;
            switch (pageName)
            {
                case "JRAdmin":
                    List<WFRole> list = null;
                    if (string.IsNullOrEmpty(ID))
                    {
                        list = roleDao.GetList(true);
                    }
                    else
                    {
                        list = roleDao.GetListByWorkflow(int.Parse(ID));
                    }
                    strBuilder = GetStringBuilder(list, "Name", "ID");
                    
                    break;
                case "JobRequest":
                    List<JobTitleLevel> posList = posDao.GetListByLevelId(int.Parse(ID));
                    strBuilder = GetStringBuilder(posList, "DisplayName", "ID");
                   
                    break;
                //Tuc 13-12-11
                //Change name of method
                case "Department":
                    IList<JobTitleLevel> jobTitleList = null;
                    if (!string.IsNullOrEmpty(ID))
                    {
                        // triet.dinh 27-12-2011
                        int rootDeptId = deptDao.GetDepartmentRoot(ConvertUtil.ConvertToInt(ID));
                        jobTitleList = commDao.GetJobTitleList(rootDeptId);
                        //jobTitleList = commDao.GetJobTitleList(int.Parse(ID));
                    }
                    else
                    {
                        jobTitleList = commDao.GetJobTitleLevelList();
                    }
                    strBuilder = GetStringBuilder(jobTitleList, "DisplayName", "ID");
                   
                    break;
                
                //Show list of Job Title which are fixed with Department when Create or Edit
                case "NewDepartment":
                    IList<JobTitle> jobTitleParentList = null;
                    if (!string.IsNullOrEmpty(ID))
                    {
                        // triet.dinh 27-12-2011
                        int rootDeptId = deptDao.GetDepartmentRoot(ConvertUtil.ConvertToInt(ID));
                        jobTitleParentList = commDao.GetJobTitleListByDepartmentId(rootDeptId);
                        //jobTitleParentList = commDao.GetJobTitleListByDepartmentId(int.Parse(ID));
                    }
                    else
                    {
                        jobTitleParentList = commDao.GetJobTitleList();
                    }
                    strBuilder = GetStringBuilder(jobTitleParentList, "JobTitleName", "JobTitleId");

                    break;

                //Tuc 13-12-11
                case "JobTitleParent":
                    IList<JobTitleLevel> jobTitleLevelList = null;
                    if (!string.IsNullOrEmpty(ID))
                    {
                        jobTitleLevelList = commDao.GetJobTitleLevelListByJobTitleId(int.Parse(ID));
                    }
                    else
                    {
                        jobTitleLevelList = commDao.GetJobTitleLevelList();
                    }
                    strBuilder = GetStringBuilder(jobTitleLevelList, "DisplayName", "ID");

                    break;

                case "SubDepartment":
                    DepartmentDao departmentDao = new DepartmentDao();
                    IList<Department> subDepartmentList = null;
                    if (!string.IsNullOrEmpty(ID))
                    {
                        subDepartmentList = departmentDao.GetSubListByParent(int.Parse(ID));
                    }
                    else
                    {
                        subDepartmentList = departmentDao.GetSubList();
                    }
                    strBuilder = GetStringBuilder(subDepartmentList, "DepartmentName", "DepartmentId");
                    
                    break;
                
                case "Assign":
                    List<sp_GetListAssignByResolutionIdResult> assignList = jrDao.GetListAssign(int.Parse(ID));
                    strBuilder = GetStringBuilder(assignList, "DisplayName", "UserAdminRole");
                   
                    break;

                case "PReviewAssign":
                    List<sp_GetListAssignByResolutionIdResult> assignPrList = prDao.GetListAssign(int.Parse(ID));
                    strBuilder = GetStringBuilder(assignPrList, "DisplayName", "UserAdminRole");

                    break;
                case "Status":
                    List<WFStatus> staList = new List<WFStatus> ();
                    //staList.Add(new WFStatus() { Name = Constants.PURCHASE_REQUEST_STATUS_FIRST_ITEM});
                    staList.AddRange(staDao.GetListStatusByResolution(int.Parse(ID)));
                    strBuilder = GetStringBuilder(staList, "Name", "ID");
                    
                    break;

                case "Employee":
                    JobTitleLevelDao levelDao = new JobTitleLevelDao();
                    List<JobTitleLevel> titleList = null;
                    if (string.IsNullOrEmpty(ID))
                    {
                        titleList = levelDao.GetList();
                    }
                    else
                    {
                        titleList = levelDao.GetJobTitleListByDepId(int.Parse(ID));
                    }
                    strBuilder = GetStringBuilder(titleList, "DisplayName", "ID");
                 
                    break;
                case "PurChaseAssign":
                    toGroup = false;
                    bool isUsPurchasing = context.Request.QueryString.AllKeys.Contains("isus") ? 
                        bool.Parse(context.Request.QueryString["isus"]) : false;
                    if(context.Request.QueryString.AllKeys.Contains("toGroup"))
                        toGroup = bool.Parse(context.Request.QueryString["toGroup"]);
                    if (toGroup)
                    {
                        int roleId = int.Parse(context.Request.QueryString["roleId"]);
                        List<ListItem> prList = new List<ListItem>();
                        prList.Add(new ListItem(Constants.PURCHASE_REQUEST_APPROVAL_MAN, ""));
                        prList.AddRange(userAdminDao.GetListWithRole(roleId));
                        strBuilder = GetStringBuilder(prList, "Text", "Value");
                    }
                    else
                    {
                        List<sp_GetListAssignByResolutionIdResult> pruchaseAssignList = new List<sp_GetListAssignByResolutionIdResult>();
                        pruchaseAssignList.Add(new sp_GetListAssignByResolutionIdResult()
                        {
                            DisplayName = Constants.PURCHASE_REQUEST_APPROVAL_MAN,
                            UserAdminRole = ""
                        });
                        if(isUsPurchasing)
                            pruchaseAssignList.AddRange(purchaseDao.GetListAssign(int.Parse(ID), Constants.WORK_FLOW_PURCHASE_REQUEST_US));
                        else
                            pruchaseAssignList.AddRange(purchaseDao.GetListAssign(int.Parse(ID), Constants.WORK_FLOW_PURCHASE_REQUEST));
                        strBuilder = GetStringBuilder(pruchaseAssignList, "DisplayName", "UserAdminRole");
                    }
                    break;
                case "PurChaseAssignUS":
                    toGroup = false;
                    if (context.Request.QueryString.AllKeys.Contains("toGroup"))
                        toGroup = bool.Parse(context.Request.QueryString["toGroup"]);
                    if (toGroup)
                    {
                        int roleId = int.Parse(context.Request.QueryString["roleId"]);
                        List<ListItem> prList = new List<ListItem>();
                        prList.Add(new ListItem(Constants.PURCHASE_REQUEST_APPROVAL_MAN, ""));
                        prList.AddRange(userAdminDao.GetListWithRole(roleId));
                        strBuilder = GetStringBuilder(prList, "Text", "Value");
                    }
                    else
                    {
                        List<sp_GetListAssignByResolutionIdResult> pruchaseAssignList = new List<sp_GetListAssignByResolutionIdResult>();
                        pruchaseAssignList.Add(new sp_GetListAssignByResolutionIdResult()
                        {
                            DisplayName = Constants.PURCHASE_REQUEST_APPROVAL_MAN,
                            UserAdminRole = ""
                        });
                        pruchaseAssignList.AddRange(purchaseDao.GetListAssign(int.Parse(ID), Constants.WORK_FLOW_PURCHASE_REQUEST_US));
                        strBuilder = GetStringBuilder(pruchaseAssignList, "DisplayName", "UserAdminRole");
                    }
                    break;
                case "AssignListByRole":
                    List<UserAdmin> adminList = commDao.GetUserAdminByRole(int.Parse(ID));
                    strBuilder = GetStringBuilder(adminList, "UserName", "UserAdminId");
                    break;
                case "WFRole":
                    List<WFRole> wfRoleList = new List<WFRole>();
                    if (string.IsNullOrEmpty(ID))
                    {
                       wfRoleList= roleDao.GetList();
                    }
                    else
                    {
                        wfRoleList = roleDao.GetListByWorkflow(int.Parse(ID));
                    }
                    strBuilder = GetStringBuilder(wfRoleList, "Name", "ID");
                    break;
                case "WFResolution":
                    List<WFResolution> wfResolutionList = new List<WFResolution>();
                    if (string.IsNullOrEmpty(ID))
                    {
                       wfResolutionList= resolutionDao.GetList();
                    }
                    else
                    {
                        wfResolutionList = resolutionDao.GetResolutionByWorkFlow(int.Parse(ID));
                    }
                    strBuilder = GetStringBuilder(wfResolutionList, "Name", "ID");
                    break;
                case "PTOType":
                    List<PTO_Type> ptoTypeList = new PTOTypeDao().GetTypeListByParentID(int.Parse(ID));
                    strBuilder = GetStringBuilder(ptoTypeList, "Name","ID" );
                    break;
                
                case "Category":
                    List<sp_GetSRCategoryResult> catList = new SRCategoryDao().GetList( null, int.Parse(ID), true);
                    strBuilder = GetStringBuilder(catList, "Name", "ID");
                    break;
                case "USAccouting":
                    List<sp_GetListAssignByResolutionIdResult> usAccountingAssignList = new List<sp_GetListAssignByResolutionIdResult>();
                    usAccountingAssignList.Add(new sp_GetListAssignByResolutionIdResult()
                        {
                            DisplayName = Constants.PURCHASE_REQUEST_APPROVAL_MAN,
                            UserAdminRole = ""
                        });
                    usAccountingAssignList.AddRange(purchaseDao.GetListAssign(int.Parse(ID), Constants.WORK_FLOW_PURCHASE_REQUEST));
                    strBuilder = GetStringBuilder(usAccountingAssignList, "DisplayName", "UserAdminRole");
                    break;
            }
            context.Response.ContentType = "application/json";
            context.Response.Write(strBuilder.ToString());
            context.Response.End();
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}