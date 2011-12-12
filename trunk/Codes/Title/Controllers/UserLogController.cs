using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CRM.Library.Common;
using CRM.Models;
using CRM.Library.Attributes;
using System.Collections;
using CRM.Areas.Portal.Models;

namespace CRM.Controllers
{
    public class UserLogController : BaseController
    {
        //
        // GET: /UserLog/
        #region Variable

        private LogDao logDao = new LogDao();

        #endregion

        [CrmAuthorizeAttribute(Module = Modules.UserLog, Rights = Permissions.Read)]
        public ActionResult Index()
        {
            Hashtable hashData = Session[SessionKey.USER_LOG_DEFAULT_VALUE] == null ? new Hashtable() : (Hashtable)Session[SessionKey.USER_LOG_DEFAULT_VALUE];

            ViewData[Constants.USER_LOG_NAME] = hashData[Constants.USER_LOG_NAME] == null ? Constants.USERNAME : !string.IsNullOrEmpty((string)hashData[Constants.USER_LOG_NAME]) ? hashData[Constants.USER_LOG_NAME] : Constants.USERNAME;
            ViewData[Constants.USER_LOG_DATE] = hashData[Constants.USER_LOG_DATE] == null ? string.Empty : hashData[Constants.USER_LOG_DATE];

            ViewData[Constants.USER_LOG_COLUMN] = hashData[Constants.USER_LOG_COLUMN] == null ? "Date" : hashData[Constants.USER_LOG_COLUMN];
            ViewData[Constants.USER_LOG_ORDER] = hashData[Constants.USER_LOG_ORDER] == null ? "desc" : hashData[Constants.USER_LOG_ORDER];
            ViewData[Constants.USER_LOG_PAGE_INDEX] = hashData[Constants.USER_LOG_PAGE_INDEX] == null ? "1" : hashData[Constants.USER_LOG_PAGE_INDEX].ToString();
            ViewData[Constants.USER_LOG_ROW_COUNT] = hashData[Constants.USER_LOG_ROW_COUNT] == null ? "20" : hashData[Constants.USER_LOG_ROW_COUNT].ToString();

            return View();
        }
        
        public ActionResult Refresh(string id)
        {
            string view = string.Empty;
            switch (id)
            {
                case "2":
                    Session.Remove(SessionKey.USER_LOG_STATISTIC_VALUE);
                    view = "UserLoginStatistic";
                    break;
                default:
                    Session.Remove(SessionKey.USER_LOG_DEFAULT_VALUE);
                    view = "Index";
                    break;
            }
            return RedirectToAction(view);
        }

        public ActionResult GetListJQGrid(string name,string date)
        {
            #region JQGrid Params
            string sortColumn = (Request.Params[GridConstants.SORT_COLUMN]).ToString();
            string sortOrder = (Request.Params[GridConstants.SORT_ORDER]).ToString();
            int pageIndex = Convert.ToInt32(Request.Params[GridConstants.PAGE_INDEX]);
            int rowCount = Convert.ToInt32(Request.Params[GridConstants.ROW_COUNT]);
            #endregion

            SetSessionFilter(name, date, sortColumn, sortOrder, pageIndex, rowCount);

            #region search
            string userName = string.Empty;
            string dateSort = string.Empty;            
            DateTime dt = new DateTime();

            if (name.Trim().ToLower().Equals(Constants.USERNAME.ToLower()))
            {
                name = string.Empty;
            }

            if (!string.IsNullOrEmpty(name))
            {
                userName = name;
            }

            if (!string.IsNullOrEmpty(date))
            {
               bool isValid = DateTime.TryParse(date, out dt);
               if (isValid)
               {
                   dateSort = dt.ToString(Constants.DATETIME_FORMAT_DB);
               }                
            }

            #endregion

            List<sp_LogMasterResult> logList = logDao.GetList(userName, dateSort);

            int totalRecords = logList.Count();
            int totalPages = (int)Math.Ceiling((float)totalRecords / (float)rowCount);
            int currentPage = CommonFunc.SetJqGridPageIndex(totalRecords, pageIndex, rowCount);
            var finalList = logDao.Sort(logList, sortColumn, sortOrder)
                                  .Skip((currentPage - 1) * rowCount)
                                   .Take(rowCount);

            var jsonData = new
            {
                total = totalPages,
                page = currentPage,
                records = totalRecords,
                rows = (
                    from m in finalList
                    select new
                    {
                        cell = new string[] {
                            m.UserName,
                            m.LogDate.Value.ToString(Constants.DATETIME_FORMAT_VIEW),
                            GetAction(m.UserName,m.LogDate.Value)
                            
                        }
                    }
                ).ToArray()
            };
            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        private string GetAction(string userName, DateTime date)
        {
            string stAction = string.Empty;
            List<sp_LogMasterGroupResult> listdetail = logDao.GetDetailOnList(userName, date);
            for (int j = 0; j < listdetail.Count; j++)
            {
                sp_LogMasterGroupResult sub = (sp_LogMasterGroupResult)listdetail[j];
                stAction += " - <a style=\"padding-top:3px;padding-bottom:2px\" onclick=\"CRM.popup('/UserLog/Detail/?UserName=" + userName + "&Date=" + date.ToString(Constants.DATETIME_FORMAT) + "&TableName=" + sub.TableName + "&ActionName=" + sub.ActionName + "&Count=" + sub.Count + "', 'User Log Details', 800)\"'>" + sub.ActionName + " " + HttpUtility.HtmlEncode(sub.TableName) + "</a> (" + sub.Count + ")";
                if (j < listdetail.Count - 1)
                {
                    stAction += "<br />";
                }

            }
            return stAction;
        }

        public ActionResult Detail(string userName, string date, string tableName, string actionName, string count, string type = null)
        {
            ViewData["UserName"] = userName;
            ViewData["Date"] = date;
            ViewData["Action"] = actionName;
            ViewData["Count"] = count;
            ViewData["Table"] = tableName;
            ViewData["Type"] = type;
            return View();
        }

        public ActionResult GetListInsertJQGrid(string name, string date, string table, string type = null)
        {
            #region JQGrid Params
            string sortColumn = (Request.Params[GridConstants.SORT_COLUMN]).ToString();
            string sortOrder = (Request.Params[GridConstants.SORT_ORDER]).ToString();
            int pageIndex = Convert.ToInt32(Request.Params[GridConstants.PAGE_INDEX]);
            int rowCount = Convert.ToInt32(Request.Params[GridConstants.ROW_COUNT]);
            #endregion
            int? itype = null;
            if (!string.IsNullOrEmpty(type))
                itype=  ConvertUtil.ConvertToInt(type);
            List<sp_LogDetailGroupResult> logList = logDao.GetLogDetailGroup(name, DateTime.Parse(date), Constants.INSERT, table, itype);
            int totalRecords = logList.Count();
            int totalPages = (int)Math.Ceiling((float)totalRecords / (float)rowCount);

            var finalList = logList.Skip((pageIndex - 1) * rowCount).Take(rowCount);
            int i = 1;
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
                            i++.ToString(),
                            table,
                            GetColumnName(m.LogId),
                            GetNewValue(m.LogId),
                        }
                    }
                ).ToArray()
            };
            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetListUpdateJQGrid(string name, string date, string table, string type = null)
        {
            #region JQGrid Params
            string sortColumn = (Request.Params[GridConstants.SORT_COLUMN]).ToString();
            string sortOrder = (Request.Params[GridConstants.SORT_ORDER]).ToString();
            int pageIndex = Convert.ToInt32(Request.Params[GridConstants.PAGE_INDEX]);
            int rowCount = Convert.ToInt32(Request.Params[GridConstants.ROW_COUNT]);
            #endregion
            int? itype = null;
            if (!string.IsNullOrEmpty(type))
                itype = ConvertUtil.ConvertToInt(type);
            List<sp_LogDetailGroupResult> logList = logDao.GetLogDetailGroup(name, DateTime.Parse(date), Constants.UPDATE, table, itype);
            int totalRecords = logList.Count();
            int totalPages = (int)Math.Ceiling((float)totalRecords / (float)rowCount);

            var finalList = logList.Skip((pageIndex - 1) * rowCount).Take(rowCount);
            int i = 1;
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
                            i++.ToString(),
                            table,
                            GetColumnName(m.LogId),
                            GetOldValue(m.LogId),
                            GetNewValue(m.LogId),
                        }
                    }
                ).ToArray()
            };
            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }


        public ActionResult GetListDeleteJQGrid(string name, string date, string table, string type = null)
        {
            #region JQGrid Params
            string sortColumn = (Request.Params[GridConstants.SORT_COLUMN]).ToString();
            string sortOrder = (Request.Params[GridConstants.SORT_ORDER]).ToString();
            int pageIndex = Convert.ToInt32(Request.Params[GridConstants.PAGE_INDEX]);
            int rowCount = Convert.ToInt32(Request.Params[GridConstants.ROW_COUNT]);
            #endregion
            int? itype = null;
            if (!string.IsNullOrEmpty(type))
                itype = ConvertUtil.ConvertToInt(type);
            List<sp_LogDetailGroupResult> logList = logDao.GetLogDetailGroup(name, DateTime.Parse(date), Constants.DELETE, table, itype);
            int totalRecords = logList.Count();
            int totalPages = (int)Math.Ceiling((float)totalRecords / (float)rowCount);

            var finalList = logList.Skip((pageIndex - 1) * rowCount).Take(rowCount);
            int i = 1;
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
                            i++.ToString(),
                            table,
                            GetColumnName(m.LogId),
                            GetOldValue(m.LogId)
                        }
                    }
                ).ToArray()
            };
            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        private string GetColumnName(string logId)
        {
            string value = string.Empty;
            List<sp_LogDetailResult> list = logDao.GetLogDetail(logId);
            foreach (sp_LogDetailResult item in list)
            {
                if (item.DisplayColumnName != "Justification")
                {
                    value += item.DisplayColumnName + "<br />";
                }
            }
            return value;
        }

        private string GetNewValue(string logId)
        {
            var principal = HttpContext.User as AuthenticationProjectPrincipal;
            string value = string.Empty;
            List<sp_LogDetailResult> list = logDao.GetLogDetail(logId);
            foreach (sp_LogDetailResult item in list)
            {

                if (item.ColumnName == Constants.KEY_ATTACH_FILE)
                {
                    if (item.NewValue.Contains('.'))
                    {
                        int lastCharWithExtension = item.NewValue.LastIndexOf('.');
                        string fileWithExtension = item.NewValue.Remove(lastCharWithExtension, item.NewValue.Length - lastCharWithExtension);
                        string ext = item.NewValue.Substring(lastCharWithExtension);
                        int indexChar = fileWithExtension.LastIndexOf('.');
                        string fileName = fileWithExtension.Remove(indexChar, fileWithExtension.Length - indexChar);
                        string fileView = fileName + ext;
                        value += fileView + "&nbsp;<br />";
                    }
                    else
                    {
                        value += item.NewValue + "&nbsp;<br />";
                    }
                }
                else if (item.ColumnName == Constants.KEY_PRIVATE_SUGGESTION_SALARY || item.ColumnName == Constants.KEY_PRIVATE_PROBATION_SALARY || item.ColumnName == Constants.KEY_PRIVATE_CONTRACTED_SALARY)
                {
                    if (CommonFunc.CheckAuthorized(principal.UserData.UserID, (int)Modules.ViewSalaryInfo, (int)Permissions.Read))
                    {
                        if (!string.IsNullOrEmpty(item.NewValue))
                        {
                            value += EncryptUtil.Decrypt(item.NewValue) + "&nbsp;<br />";
                        }
                        else
                        {
                            value +=  "&nbsp;<br />";
                        }
                    }
                    else
                    {
                        value += Constants.PRIVATE_DATA + "&nbsp;<br />";
                    }
                }
                else
                {
                    if (item.DisplayColumnName != "Justification")
                    {
                        value += item.NewValue + "&nbsp;<br />";
                    }
                    
                }

            }
            return value;
        }
        /// <summary>
        /// Action: Show list of user who logged in to Portal pages
        /// </summary>
        /// <returns></returns>
        [CrmAuthorizeAttribute(Module = Modules.UserLog, Rights = Permissions.Read)]
        public ActionResult UserLoginStatistic()
        {
            Hashtable hashData = Session[SessionKey.USER_LOG_STATISTIC_VALUE] == null ? new Hashtable() : (Hashtable)Session[SessionKey.USER_LOG_STATISTIC_VALUE];
            ViewData[Constants.HOME_STATISTIC_USER_ADMIN] = hashData[Constants.HOME_STATISTIC_USER_ADMIN] == null ? Constants.SELECT_USER_ADMIN : !string.IsNullOrEmpty((string)hashData[Constants.HOME_STATISTIC_USER_ADMIN]) ? hashData[Constants.HOME_STATISTIC_USER_ADMIN] : Constants.SELECT_USER_ADMIN;
            ViewData[Constants.HOME_STATISTIC_FROM_DATE] = hashData[Constants.HOME_STATISTIC_FROM_DATE] == null ? "" : hashData[Constants.HOME_STATISTIC_FROM_DATE];
            ViewData[Constants.HOME_STATISTIC_TO_DATE] = hashData[Constants.HOME_STATISTIC_TO_DATE] == null ? "" : hashData[Constants.HOME_STATISTIC_TO_DATE];
            ViewData[Constants.HOME_STATISTIC_COLUMN] = hashData[Constants.HOME_STATISTIC_COLUMN] == null ? "DatetimeAccess" : hashData[Constants.HOME_STATISTIC_COLUMN].ToString();
            ViewData[Constants.HOME_STATISTIC_ORDER] = hashData[Constants.HOME_STATISTIC_ORDER] == null ? "desc" : hashData[Constants.HOME_STATISTIC_ORDER].ToString();
            ViewData[Constants.HOME_STATISTIC_PAGE_INDEX] = hashData[Constants.HOME_STATISTIC_PAGE_INDEX] == null ? "1" : hashData[Constants.HOME_STATISTIC_PAGE_INDEX].ToString();
            ViewData[Constants.HOME_STATISTIC_ROW_COUNT] = hashData[Constants.HOME_STATISTIC_ROW_COUNT] == null ? "20" : hashData[Constants.HOME_STATISTIC_ROW_COUNT].ToString();

            UserAdminDao userDao = new UserAdminDao();
            List<UserAdmin> listUsers = userDao.GetList();
            ViewData["UserAdmin"] = new SelectList(listUsers, "UserName", "UserName", ViewData[Constants.HOME_STATISTIC_USER_ADMIN]);

            return View();
        }
        /// <summary>
        /// Set Session Filter
        /// </summary>
        /// <param name="userAdmin"></param>
        /// <param name="dateFrom"></param>
        /// <param name="dateTo"></param>
        /// <param name="column"></param>
        /// <param name="order"></param>
        /// <param name="pageIndex"></param>
        /// <param name="rowCount"></param>
        [NonAction]
        private void SetSessionFilter(string userAdmin, string dateFrom, string dateTo, string column, string order, int pageIndex, int rowCount)
        {
            Hashtable statisticState = new Hashtable();
            statisticState.Add(Constants.HOME_STATISTIC_USER_ADMIN, userAdmin);
            statisticState.Add(Constants.HOME_STATISTIC_FROM_DATE, dateFrom);
            statisticState.Add(Constants.HOME_STATISTIC_TO_DATE, dateTo);
            statisticState.Add(Constants.CANDIDATE_LIST_COLUMN, column);
            statisticState.Add(Constants.CANDIDATE_LIST_ORDER, order);
            statisticState.Add(Constants.CANDIDATE_LIST_PAGE_INDEX, pageIndex);
            statisticState.Add(Constants.CANDIDATE_LIST_ROW_COUNT, rowCount);
            Session[SessionKey.USER_LOG_STATISTIC_VALUE] = statisticState;
        }
        /// <summary>
        /// Get list for the UserLoginStatistic page
        /// </summary>
        /// <param name="userAdmin"></param>
        /// <param name="dateFrom"></param>
        /// <param name="dateTo"></param>
        /// <returns></returns>
        [CrmAuthorizeAttribute(Module = Modules.UserLog, Rights = Permissions.Read)]
        public ActionResult GetListJQGridForPortalUserLog(string userAdmin, string dateFrom, string dateTo)
        {
            PortalLogAccessDao logDao = new PortalLogAccessDao();

            #region JQGrid Params
            string sortColumn = (Request.Params[GridConstants.SORT_COLUMN]).ToString();
            string sortOrder = (Request.Params[GridConstants.SORT_ORDER]).ToString();
            int pageIndex = Convert.ToInt32(Request.Params[GridConstants.PAGE_INDEX]);
            int rowCount = Convert.ToInt32(Request.Params[GridConstants.ROW_COUNT]);
            #endregion

            SetSessionFilter(userAdmin, dateFrom, dateTo, sortColumn, sortOrder, pageIndex, rowCount);
            List<Portal_LogAccess> logList = logDao.GetList(userAdmin, dateFrom, dateTo);

            int totalRecords = logList.Count();
            int totalPages = (int)Math.Ceiling((float)totalRecords / (float)rowCount);

            // Sort
            List<Portal_LogAccess> logListFinal = logDao.Sort(logList, sortColumn, sortOrder);

            var jsonData = new
            {
                total = totalPages,
                page = pageIndex,
                records = totalRecords,
                rows = (
                    from m in logListFinal
                    select new
                    {
                        i = m.Id,
                        cell = new string[] {
                           m.UserAdmin,
                           m.UserIp,
                           m.DatetimeAccess.ToString(Constants.DATETIME_FORMAT_FULL),
                           m.DatetimeOut != null? m.DatetimeOut.Value.ToString(Constants.DATETIME_FORMAT_FULL): string.Empty
                        }
                    }
                ).ToArray()
            };
            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }
        private string GetOldValue(string logId)
        {
            var principal = HttpContext.User as AuthenticationProjectPrincipal;
            string value = string.Empty;
            List<sp_LogDetailResult> list = logDao.GetLogDetail(logId);
            foreach (sp_LogDetailResult item in list)
            {
                if (item.ColumnName == Constants.KEY_ATTACH_FILE)
                {
                    if (!string.IsNullOrEmpty(item.OldValue))
                    {
                        if (item.OldValue.Contains('.'))
                        {
                            int lastCharWithExtension = item.OldValue.LastIndexOf('.');
                            string fileWithExtension = item.OldValue.Remove(lastCharWithExtension, item.OldValue.Length - lastCharWithExtension);
                            string ext = item.OldValue.Substring(lastCharWithExtension);
                            int indexChar = fileWithExtension.LastIndexOf('.');
                            string fileName = fileWithExtension.Remove(indexChar, fileWithExtension.Length - indexChar);
                            string fileView = fileName + ext;
                            value += fileView + "&nbsp;<br />";
                        }
                        else
                        {
                            value += item.OldValue + "&nbsp;<br />";
                        }
                    }
                    else
                    {
                        value += item.OldValue + "&nbsp;<br />";
                    }
                }                
                else if (item.ColumnName == Constants.KEY_PRIVATE_SUGGESTION_SALARY || item.ColumnName == Constants.KEY_PRIVATE_PROBATION_SALARY || item.ColumnName == Constants.KEY_PRIVATE_CONTRACTED_SALARY)
                {
                    if (!string.IsNullOrEmpty(item.OldValue))
                    {
                        if (CommonFunc.CheckAuthorized(principal.UserData.UserID, (int)Modules.ViewSalaryInfo, (int)Permissions.Read))
                        {
                            value += EncryptUtil.Decrypt(item.OldValue) + "&nbsp;<br />";
                        }
                        else
                        {
                            value += Constants.PRIVATE_DATA + "&nbsp;<br />";
                        }
                    }
                }
                else
                {
                    value += item.OldValue + "&nbsp;<br />";
                }
            }
            return value;
        }

        /// <summary>
        /// Set Session to Filter
        /// </summary>
        /// <param name="accountName"></param>
        /// <param name="groupId"></param>
        /// <param name="column"></param>
        /// <param name="order"></param>
        /// <param name="pageIndex"></param>
        /// <param name="rowCount"></param>
        private void SetSessionFilter(string accountName, string date,
            string column, string order, int pageIndex, int rowCount)
        {
            Hashtable hashData = new Hashtable();
            hashData.Add(Constants.USER_LOG_NAME, accountName);
            hashData.Add(Constants.USER_LOG_DATE, date);
            hashData.Add(Constants.USER_LOG_COLUMN, column);
            hashData.Add(Constants.USER_LOG_ORDER, order);
            hashData.Add(Constants.USER_LOG_PAGE_INDEX, pageIndex);
            hashData.Add(Constants.USER_LOG_ROW_COUNT, rowCount);

            Session[SessionKey.USER_LOG_DEFAULT_VALUE] = hashData;
        }

        /// <summary>
        /// tan.tran 2011.05.25
        /// Clear Logs Data
        /// </summary>
        /// <returns></returns>
        public ActionResult ClearLog()
        {
            ViewData["TimeToClearLog"] = new SelectList(Constants.TimeRangeToClearLog, "value", "text");
            string logType = Request["type"];
            ViewData["LogType"] = logType;
            if (string.IsNullOrEmpty(logType))
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        /// <summary>
        /// tan.tran 2011.05.25
        /// Clear Logs Data
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [CrmAuthorizeAttribute(Module = Modules.UserLog, Rights = Permissions.Delete)]
        public ActionResult ClearLog(FormCollection form)
        {
            string stLogType = form["hiddenType"].ToString();
            int month = int.Parse(form["TimeToClearLog"].ToString());
            Message msg = null;

            switch (stLogType)
            {
                case Constants.LOG_TYPE_DATA:
                    msg = logDao.DeleteList(month);
                    ShowMessage(msg);
                    return RedirectToAction("Index");
                case Constants.LOG_TYPE_ADMIN_ACCESS:                   
                    LogAccessDao logAccessDao = new LogAccessDao();
                    msg = logAccessDao.DeleteList(month);
                    ShowMessage(msg);
                    return RedirectToAction("UserLoginStatistic", "Home");
                case Constants.LOG_TYPE_PORTAL_ACCESS:
                    PortalLogAccessDao logPortalDao = new PortalLogAccessDao();
                    msg = logPortalDao.DeleteList(month);
                    ShowMessage(msg);
                    return RedirectToAction("UserLoginStatistic", "UserLog");
            }

            return RedirectToAction("Index");
        }
    }
}
