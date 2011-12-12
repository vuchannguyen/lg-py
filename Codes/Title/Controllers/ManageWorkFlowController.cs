using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CRM.Models;
using CRM.Library.Common;
using CRM.Library.Attributes;
using System.Collections;

namespace CRM.Controllers
{
    public class ManageWorkFlowController : BaseController
    {
        #region Variables
        private WorkflowDao workFlowDao = new WorkflowDao();
        private ManageWorkFlowDao manageDao = new ManageWorkFlowDao();
        #endregion

        public ActionResult Index()
        {
            Hashtable hashData = Session[SessionKey.MANAGE_WORKFLOW] == null ? new Hashtable() : (Hashtable)Session[SessionKey.MANAGE_WORKFLOW];

            string workflow = hashData[Constants.MWF_WORKFLOW] == null ? "" : (string)hashData[Constants.MWF_WORKFLOW];
            
            string role = hashData[Constants.MWF_ROLE] == null ? "" : (string)hashData[Constants.MWF_ROLE];
            string resolution = hashData[Constants.MWF_RESOLUTION] == null ? "" : (string)hashData[Constants.MWF_RESOLUTION];
            string status = hashData[Constants.MWF_STATUS] == null ? "" : (string)hashData[Constants.MWF_STATUS];

            List<WorkFlow> workflowList = new List<WorkFlow>();
            List<WFResolution> resolutionList = new List<WFResolution>();
            List<WFRole> roleList = new List<WFRole>();
            if (!string.IsNullOrEmpty(workflow) && ConvertUtil.ConvertToInt(workflow) > 0)
            {
                int wfID = int.Parse(workflow);                
                workflowList = workFlowDao.GetListByID(wfID);
                resolutionList = manageDao.GetResolutionListByWorkFlow(wfID);
                roleList = manageDao.GetRoleListByWorkFlow(wfID);
            }
            else
            {
                workflowList = workFlowDao.GetList(true);
                resolutionList = manageDao.GetResolutionList();
                roleList = manageDao.GetRoleList();
            }
            ViewData[CommonDataKey.MWF_WORKFLOW] = new SelectList(workflowList, "ID", "Name", workflow);
            ViewData[CommonDataKey.MWF_RESOLUTION] = new SelectList(resolutionList, "ID", "Name", resolution);
            ViewData[CommonDataKey.MWF_ROLE] = new SelectList(roleList, "ID", "Name", role);
            ViewData[CommonDataKey.MWF_STATUS] = new SelectList(Constants.WorkFlowStatus, "Value", "Text", status);

            ViewData[Constants.MWF_COLUMN] = hashData[Constants.MWF_COLUMN] == null ? "WorkFlow" : hashData[Constants.MWF_COLUMN];
            ViewData[Constants.MWF_ORDER] = hashData[Constants.MWF_ORDER] == null ? "asc" : hashData[Constants.MWF_ORDER];
            ViewData[Constants.MWF_PAGE_INDEX] = hashData[Constants.MWF_PAGE_INDEX] == null ? "1" : hashData[Constants.MWF_PAGE_INDEX].ToString();
            ViewData[Constants.MWF_ROW_COUNT] = hashData[Constants.MWF_ROW_COUNT] == null ? "20" : hashData[Constants.MWF_ROW_COUNT].ToString();

            return View();
        }

        public ActionResult Refresh()
        {
            Session.Remove(SessionKey.MANAGE_WORKFLOW);
            return RedirectToAction("Index");
        }

        private void SetSessionFilter(string wf, string role, string resolution, string status, string column, string order, int pageIndex, int rowCount)
        {
            Hashtable hashData = new Hashtable();
            hashData.Add(Constants.MWF_WORKFLOW, wf);
            hashData.Add(Constants.MWF_ROLE, role);
            hashData.Add(Constants.MWF_RESOLUTION, resolution);
            hashData.Add(Constants.MWF_STATUS, status);
            hashData.Add(Constants.MWF_COLUMN, column);
            hashData.Add(Constants.MWF_ORDER, order);
            hashData.Add(Constants.MWF_PAGE_INDEX, pageIndex);
            hashData.Add(Constants.MWF_ROW_COUNT, rowCount);

            Session[SessionKey.MANAGE_WORKFLOW] = hashData;
        }

        [CrmAuthorizeAttribute(Module = Modules.JobTitleLevel, Rights = Permissions.Read)]
        public ActionResult GetListJQGrid(string wf, string role,string resolution,string status)
        {
            #region JQGrid Params

            string sortColumn = (Request.Params[GridConstants.SORT_COLUMN]).ToString();
            string sortOrder = (Request.Params[GridConstants.SORT_ORDER]).ToString();
            int pageIndex = Convert.ToInt32(Request.Params[GridConstants.PAGE_INDEX]);
            int rowCount = Convert.ToInt32(Request.Params[GridConstants.ROW_COUNT]);

            #endregion
            SetSessionFilter(wf, role, resolution, status, sortColumn, sortOrder, pageIndex, rowCount);
            int wfID = 0;
            int roleID = 0;
            int resolutionID = 0;
            int? statusID = null;
            if (!string.IsNullOrEmpty(wf))
            {
                wfID = int.Parse(wf);
            }
            if (!string.IsNullOrEmpty(role))
            {
                roleID = int.Parse(role);
            }
            if (!string.IsNullOrEmpty(resolution))
            {
                resolutionID = int.Parse(resolution);
            }
            if (!string.IsNullOrEmpty(status))
            {
                statusID = int.Parse(status);
            }
            List<sp_GetManageWorkFlowResult> list = manageDao.GetList(wfID, roleID, resolutionID, statusID);

            //for paging
            int totalRecords = list.Count();
            int totalPages = (int)Math.Ceiling((float)totalRecords / (float)rowCount);
            List<sp_GetManageWorkFlowResult> finalList = manageDao.Sort(list, sortColumn, sortOrder)
                                  .Skip((pageIndex - 1) * rowCount)
                                   .Take(rowCount).ToList<sp_GetManageWorkFlowResult>();
            TempData["RoleAndResolutionList"] = finalList;
            //bind to jqGrid
            var jsonData = new
            {
                total = totalPages,
                page = pageIndex,
                records = totalRecords,
                rows = (
                    from m in finalList
                    select new
                    {
                        cell = new string[] {
                            m.WFRoleID.ToString(),
                            m.WFResolutionID.ToString(),
                            m.IsHold.ToString(),
                            HttpUtility.HtmlEncode(m.RoleName),   
                            HttpUtility.HtmlEncode(m.ResolutionName),   
                            CommonFunc.ShowActiveImage(m.IsHold),
                            HttpUtility.HtmlEncode(m.WorkFlowName),
                            "<input type=\"button\" class=\"icon edit\" title=\"Edit\" onclick=\"CRM.popup('/ManageWorkFlow/Edit/" + m.WFResolutionID.ToString()+ "@"+m.WFRoleID.ToString()+"@"+m.IsHold + "', 'Update', 400)\" />"                            
                        }
                    }
                ).ToArray()
            };
            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Create()
        {
            ViewData["WorkFlow"] = new SelectList(workFlowDao.GetList(true), "ID", "Name", "");
            ViewData["WFResolutionID"] = new SelectList(manageDao.GetResolutionList(), "ID", "Name", "");
            ViewData["WFRoleID"] = new SelectList(manageDao.GetRoleList(), "ID", "Name", "");
            return View();
        }

        [HttpPost]
        public ActionResult Create(WFRole_WFResolution objUI)
        {
            Message msg = manageDao.Insert(objUI);
            ShowMessage(msg);
            return RedirectToAction("Index");
        }

        public ActionResult Edit(string id)
        {
            string[] array = id.Split('@');
            int resolution = int.Parse(array[0]);
            int role = int.Parse(array[1]);
            bool isHold = bool.Parse(array[2]);
            WFRole_WFResolution obj = manageDao.GetObjectByRsolutionAndRoleAndHold(resolution, role, isHold);
            TempData["OldObject"] = obj;
            WFRole objRole = new RoleDao().GetWorkflowByRole(obj.WFRoleID);
            ViewData["WorkFlow"] = new SelectList(workFlowDao.GetList(true), "ID", "Name", objRole.WFID);
            ViewData["WFResolutionID"] = new SelectList(manageDao.GetResolutionListByWorkFlow(objRole.WFID), "ID", "Name", obj.WFResolutionID);
            ViewData["WFRoleID"] = new SelectList(manageDao.GetRoleListByWorkFlow(objRole.WFID), "ID", "Name", obj.WFRoleID);
            return View(obj);
        }

        [HttpPost]
        public ActionResult Edit(WFRole_WFResolution objUI)
        {
            Message msg = manageDao.Update(objUI, (WFRole_WFResolution)TempData["OldObject"]);
            ShowMessage(msg);
            return RedirectToAction("Index");
        }

        /// <summary>
        /// Delete List
        /// </summary>
        /// <param name="id">ids</param>
        /// <returns></returns      
        [HttpPost]
        public ActionResult DeleteList(string name)
        {
            Message msg = manageDao.DeleteList(name);
            //ShowMessage(msg);
            //return RedirectToAction("Index");
            JsonResult result = new JsonResult();
            return Json(msg);
        }
    }
}
