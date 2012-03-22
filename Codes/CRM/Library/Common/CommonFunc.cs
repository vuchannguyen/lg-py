using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Linq.Mapping;
using System.DirectoryServices;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Net.Mime;
using System.Reflection;
using System.Resources;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using CRM.Areas.Portal.Models;
using CRM.Library.Utils;
using CRM.Models;


namespace CRM.Library.Common
{
    public class CommonFunc
    {
        private static string templateForManager = null;
        private static string templateForEmployee = null;
        private static string templateForDeletion = null;
        private static LocationDao locationDao = new LocationDao();
        private static ListeningTopicDao listeningTopicDao = new ListeningTopicDao();
        private static ComprehensionParagraphDao paragraphDao = new ComprehensionParagraphDao();
        private static RoleDao roleDao = new RoleDao();
        private static UserAdminDao userAdminDao = new UserAdminDao();
        private static ServiceRequestDao srDao = new ServiceRequestDao();
        private static SRCommentDao srCommentDao = new SRCommentDao();
        private static SRStatusDao srStatusDao = new SRStatusDao();
        private static TrainingAttendeeDao trainingAttendeeDao = new TrainingAttendeeDao();
        /// <summary>
        /// Email template for Manager
        /// </summary>
        private static string GetTemplateForManager
        {
            get
            {
                string path = HttpContext.Current.Server.MapPath(Constants.PTO_EMAIL_MANAGER_TEMPLATE_PATH);
                if (templateForManager == null && File.Exists(path))
                {
                    templateForManager = File.ReadAllText(path);
                }
                return templateForManager;
            }
        }
        private static string GetTemplateForDeletion
        {
            get
            {
                string path = HttpContext.Current.Server.MapPath(Constants.PTO_EMAIL_DELETE_TEMPLATE_PATH);
                if (templateForDeletion == null && File.Exists(path))
                {
                    templateForDeletion = File.ReadAllText(path);
                }
                return templateForDeletion;
            }
        }
        //Hang
        /// <summary>
        /// Get non employee fullname
        /// </summary>
        /// <param name="emp"></param>
        /// <returns></returns>
        public static string GetNonEmployeeFullName(NonEmployee nonemp)
        {
            string st = string.Empty;
            if (nonemp != null)
            {
                if (string.IsNullOrEmpty(nonemp.MiddleName))
                {
                    st = HttpUtility.HtmlEncode(nonemp.FirstName) + " " + HttpUtility.HtmlEncode(nonemp.LastName);
                }
                else
                {
                    st = HttpUtility.HtmlEncode(nonemp.FirstName) + " " + HttpUtility.HtmlEncode(nonemp.MiddleName) + " " + HttpUtility.HtmlEncode(nonemp.LastName);
                }
            }
            return st;
        }
        public static string GetEmployeeFullNameWithID(Employee emp)
        {
            string st = string.Empty;
            if (emp != null)
            {
                if (string.IsNullOrEmpty(emp.MiddleName))
                {
                    st = HttpUtility.HtmlEncode(emp.FirstName) + " " + HttpUtility.HtmlEncode(emp.LastName);
                }
                else
                {
                    st = HttpUtility.HtmlEncode(emp.FirstName) + " " + HttpUtility.HtmlEncode(emp.MiddleName) + " " + HttpUtility.HtmlEncode(emp.LastName) + " - " + HttpUtility.HtmlEncode(emp.ID);
                }
            }
            return st;
        }

        public static string GetEmployeeFullNameByEmpID(string empID)
        {
            string st = string.Empty;
            Employee emp = new EmployeeDao().GetById(empID);
            if (emp != null)
            {
                if (string.IsNullOrEmpty(emp.MiddleName))
                {
                    st = HttpUtility.HtmlEncode(emp.FirstName) + " " + HttpUtility.HtmlEncode(emp.LastName);
                }
                else
                {
                    st = HttpUtility.HtmlEncode(emp.FirstName) + " " + HttpUtility.HtmlEncode(emp.MiddleName) + " " + HttpUtility.HtmlEncode(emp.LastName) + " - " + HttpUtility.HtmlEncode(emp.ID);
                }
            }
            return st;
        }

        public static string GetManagerFullNameByEmpID(string empID)
        {
            string st = string.Empty;
            Employee emp = new EmployeeDao().GetById(empID);
            string mId = emp.ManagerId;
            Employee manaId = new EmployeeDao().GetById(mId);
            if (manaId != null)
            {
                if (string.IsNullOrEmpty(manaId.MiddleName))
                {
                    st = HttpUtility.HtmlEncode(manaId.FirstName) + " " + HttpUtility.HtmlEncode(manaId.LastName);
                }
                else
                {
                    st = HttpUtility.HtmlEncode(manaId.FirstName) + " " + HttpUtility.HtmlEncode(manaId.MiddleName) + " " + HttpUtility.HtmlEncode(manaId.LastName) + " - " + HttpUtility.HtmlEncode(manaId.ID);
                }
            }
            return st;
        }

        public static string GetManagerIdByEmpID(string empID)
        {
            string mId = string.Empty;
            Employee emp = new EmployeeDao().GetById(empID);
            return mId = emp.ManagerId;
        }
        /// <summary>
        /// Get employee fullname /*Added by Tai Nguyen*/
        /// </summary>
        /// <param name="emp"></param>
        /// <param name="nameFormat"></param>
        /// <returns></returns>
        /// 

       
        //Hang
        /// <summary>
        /// Get Nonempoyee Full Name
        /// </summary>
        /// <returns></returns>
        public static string GetNonemployeeFullName(NonEmployee nonemp, Constants.FullNameFormat nameFormat)
        {
            string st = string.Empty;
            if (nonemp != null)
            {
                if (string.IsNullOrEmpty(nonemp.MiddleName))
                {
                    switch (nameFormat)
                    {
                        case Constants.FullNameFormat.FirstLastMiddle:
                        case Constants.FullNameFormat.FirstMiddleLast:
                        case Constants.FullNameFormat.MiddleFirstLast:
                            st = HttpUtility.HtmlEncode(nonemp.FirstName) + " " + HttpUtility.HtmlEncode(nonemp.LastName);
                            break;
                        case Constants.FullNameFormat.LastMiddleFirst:
                        case Constants.FullNameFormat.LastFirstMiddle:
                        case Constants.FullNameFormat.MiddleLastFirst:
                            st = HttpUtility.HtmlEncode(nonemp.LastName) + " " + HttpUtility.HtmlEncode(nonemp.FirstName);
                            break;
                    }
                }
                else
                {
                    switch (nameFormat)
                    {
                        case Constants.FullNameFormat.FirstLastMiddle:
                            st = HttpUtility.HtmlEncode(nonemp.FirstName) + " " + HttpUtility.HtmlEncode(nonemp.LastName) + " " + HttpUtility.HtmlEncode(nonemp.MiddleName);
                            break;
                        case Constants.FullNameFormat.FirstMiddleLast:
                            st = HttpUtility.HtmlEncode(nonemp.FirstName) + " " + HttpUtility.HtmlEncode(nonemp.MiddleName) + " " + HttpUtility.HtmlEncode(nonemp.LastName);
                            break;
                        case Constants.FullNameFormat.MiddleFirstLast:
                            st = HttpUtility.HtmlEncode(nonemp.MiddleName) + " " + HttpUtility.HtmlEncode(nonemp.FirstName) + " " + HttpUtility.HtmlEncode(nonemp.LastName);
                            break;
                        case Constants.FullNameFormat.LastMiddleFirst:
                            st = HttpUtility.HtmlEncode(nonemp.LastName) + " " + HttpUtility.HtmlEncode(nonemp.MiddleName) + " " + HttpUtility.HtmlEncode(nonemp.FirstName);
                            break;
                        case Constants.FullNameFormat.LastFirstMiddle:
                            st = HttpUtility.HtmlEncode(nonemp.LastName) + " " + HttpUtility.HtmlEncode(nonemp.FirstName) + " " + HttpUtility.HtmlEncode(nonemp.MiddleName);
                            break;
                        case Constants.FullNameFormat.MiddleLastFirst:
                            st = HttpUtility.HtmlEncode(nonemp.MiddleName) + " " + HttpUtility.HtmlEncode(nonemp.LastName) + " " + HttpUtility.HtmlEncode(nonemp.FirstName);
                            break;
                    }
                }
            }
            return st;
        }
        /// <summary>
        /// Email template for employee
        /// </summary>
        private static string GetTemplateForEmployee
        {
            get
            {
                string path = HttpContext.Current.Server.MapPath(Constants.PTO_EMAIL_EMPLOYEE_TEMPLATE_PATH);
                if (templateForEmployee == null && File.Exists(path))
                {
                    templateForEmployee = File.ReadAllText(path);
                }
                return templateForEmployee;
            }
        }

        public static string ShowActiveImage(object obj)
        {
            string str = "<span class=\"{0}\"></span>";
            return string.Format(str, (obj != null && (bool)obj) ? "iactive" : "iinactive");
        }

        public static string ShowActiveImage(object obj, string onClickAction)
        {
            string str = "<span class=\"{0}\" title='{1}' style='cursor:pointer' onclick=\"{2}\"></span>";
            return string.Format(str, (obj != null && (bool)obj) ? "iactive" : "iinactive",
                (obj != null && (bool)obj) ? "Set Inactive" : "Set Active", onClickAction);
        }

        //public static bool SendMail(string stTo, string stCC, string stBCC, string stSubj, string stBody, System.Web.Mail.MailPriority pri)
        //{
        //    System.Web.Mail.MailMessage objEmail = new System.Web.Mail.MailMessage();
        //    objEmail.BodyFormat = MailFormat.Html;
        //    objEmail.From = Constants.CRM_MAIL_FROM;

        //    objEmail.To = stTo;
        //    objEmail.Cc = stCC;
        //    objEmail.Bcc = stBCC;
        //    objEmail.Subject = stSubj;
        //    objEmail.Body = stBody;
        //    objEmail.Priority = pri;
        //    SmtpMail.SmtpServer = Constants.SMTP_MAIL_SERVER;
        //    try
        //    {
        //        SmtpMail.Send(objEmail);
        //    }
        //    catch
        //    {
        //        return false;
        //    }

        //    return true;
        //}

        public static string GetCandidateStatus(int status)
        {
            switch (status)
            {
                case (int)CandidateStatus.Available:
                    return "Available";
                case (int)CandidateStatus.Failed:
                    return "Failed";
                case (int)CandidateStatus.Interviewing:
                    return "Interviewing";
                case (int)CandidateStatus.Passed:
                    return "Passed";
                case (int)CandidateStatus.Unavailable:
                    return "Unavailable";
                case (int)CandidateStatus.Waiting:
                    return "Waiting";

            }
            return "";
        }

        public static bool CheckAuthorized(int userId, byte moduleId, int permisson)
        {
            AuthenticateDao auDao = new AuthenticateDao();
            ArrayList permissionIds = new ArrayList();
            permissionIds.Add(permisson);
            return auDao.CheckPermission(userId, moduleId, Array.ConvertAll(permissionIds.ToArray(), arr => (int)arr));
        }

        public static bool CheckPortalAuthorized(string userName, byte moduleId, int permisson)
        {
            UserAdmin objAdmin = new UserAdminDao().GetByUserName(userName);
            if (objAdmin != null)
            {
                AuthenticateDao auDao = new AuthenticateDao();
                ArrayList permissionIds = new ArrayList();
                permissionIds.Add(permisson);
                return auDao.CheckPermission(objAdmin.UserAdminId, moduleId, Array.ConvertAll(permissionIds.ToArray(), arr => (int)arr));
            }
            return false;
        }

        public static List<ListItem> GetRolePortal(string userName)
        {
            List<ListItem> roleList = new List<ListItem>();
            roleList.Add(new ListItem("Employee", Constants.PRW_ROLE_EMPLOYEE_ID.ToString()));
            UserAdmin objAdmin = new UserAdminDao().GetByUserName(userName);
            if (objAdmin != null)
            {
                List<WFRole> list = new CommonDao().GetRoleList(objAdmin.UserAdminId, Constants.WORK_FLOW_PERFORMANCE_REVIEW);
                list = list.Where(q => q.ID == Constants.PRW_ROLE_EM_ID || q.ID == Constants.PRW_ROLE_PL || q.ID == Constants.PRW_ROLE_MANAGER_ID).ToList();
                if (list.Count > 0)
                {
                    foreach (WFRole item in list)
                    {
                        roleList.Add(new ListItem(item.Name, item.ID.ToString()));
                    }
                }
            }
            return roleList;
        }

        /// <summary>
        /// Get the html code of the view details button
        /// </summary>
        /// <param name="id"></param>
        /// <param name="sectionID"></param>
        /// <returns></returns>
        public static string GetDetailsViewLink(int id, int sectionID)
        {
            string result = "<input type=\"button\" class=\"icon detailview\" title=\"View Detail\""
                    + "onclick=\"";
            string topicDetailLink = "/Question/TopicDetails";
            string detailLink = "/Question/Details";
            if (sectionID == Constants.LOT_LISTENING_TOPIC_ID
                || sectionID == Constants.LOT_COMPREHENSION_SKILL_ID)
            {
                result += "CRM.showTopicDetailsPopup('" + topicDetailLink + "?id=" + id + "&sectionID=" + sectionID + "', 'View', "
                    + Constants.DETAILS_POPUP_WIDTH + ")";
            }
            else
            {
                result += "CRM.showTopicDetailsPopup('" + detailLink + "?id=" + id + "', 'View', "
                    + Constants.DETAILS_POPUP_WIDTH + ")";
            }
            result += " \" />";
            return result;
        }

        public static string TruncateAroundSubText(string text, string subtext)
        {
            int startIndex = text.IndexOf(subtext);
            if (startIndex != -1)
            {
                startIndex = ((startIndex < Constants.ADDED_KEYWORD_LENGTH) ? 0 : startIndex - Constants.ADDED_KEYWORD_LENGTH);
                int textLength = (text.Length - startIndex) < (subtext.Length + 2 * Constants.ADDED_KEYWORD_LENGTH) ?
                    (text.Length - startIndex) : (subtext.Length + 2 * Constants.ADDED_KEYWORD_LENGTH);
                return text.Substring(startIndex, textLength);
            }
            else
                return "Not found";
        }

        public static string ButtonEdit(string actionUrl)
        {
            return "<input type=\"button\" class=\"icon edit\" title=\"Edit\" onclick=\"window.location = '" + actionUrl + "'\" />";
        }

        public static string Link(string id, string actionUrl, string display, bool haveTooltip)
        {
            if (id != null)
            {
                if (haveTooltip)
                {
                    return "<a id=" + id.Replace(" ", string.Empty) + " class='showTooltip' href='" + actionUrl + "'>" +
                           display + "</a>";
                }
                else
                {
                    return "<a id=" + id.Replace(" ", string.Empty) + "  href='" + actionUrl + "'>" + display + "</a>";
                }
            }
            else
            {
                if (haveTooltip)
                {
                    return "<a id=" + id + " class='showTooltip' href='" + actionUrl + "'>" +
                           display + "</a>";
                }
                else
                {
                    return "<a id=" + id + "  href='" + actionUrl + "'>" + display + "</a>";
                }
            }
        }

        public static string Link(string id, string actionUrl, string display, bool haveTooltip, bool isSubTooltip)
        {
            if (haveTooltip)
            {
                if (isSubTooltip)
                    return "<a id=" + id + " class='showTooltip' isSub='true' href='" + actionUrl + "'>" + display + "</a>";
                else
                    return "<a id=" + id + " class='showTooltip' isSub='false' href='" + actionUrl + "'>" + display + "</a>";
            }
            else
            {
                return "<a id=" + id + "  href='" + actionUrl + "'>" + display + "</a>";
            }
        }
        public static string Link(string id, string actionUrl, string display, int toolTipNumber)
        {
            return "<a id=" + id.Replace(" ", string.Empty) + " class='showTooltip' toolTipNumber='" + toolTipNumber + "' href='" + actionUrl + "'>" + display + "</a>";
        }
        public static string Link(string id, string cssClass, string actionUrl, string display)
        {
            string sResult = "<a id='{0}' class='{1}' href='{2}'>{3}</a>";
            return string.Format(sResult, id, cssClass, actionUrl, display);
        }

        public static string Button(string css, string title, string action)
        {
            return "<input type='button' class='icon " + css + "' title='" + title + "' onclick=\"" + action + "\" />&nbsp;";
        }

        public static string ButtonWithID(string css, string title, string action, string id)
        {
            return "<input id='" + id + "' type='button' class='icon " + css + "' title='" + title + "' onclick=\"" + action + "\" />&nbsp;";
        }

        public static string Button(string css, string title, string action, string displayText)
        {
            string sButton = "<button class='button {0}' title='{1}' onclick=\"{2}\">{3}</button>";
            return string.Format(sButton, css, title, action, displayText);
        }

        public static string ButtonWithID(string css, string title, string action, string id, string displayText)
        {
            string sButton = "<button id='{3}' type='button' class='{0}' title='{1}' onclick=\"{2}\">{4}</button>";
            return string.Format(sButton, css, title, action, id, displayText);
        }

        /// <summary>
        /// Get the button with or without text
        /// </summary>
        /// <param name="sParam">
        /// <para>{0} css, {1} title, {2} action, {3} displayText.</para>
        /// <para>If displayText is null or empty return CommonFunc.Button(string, string, string)</para>
        /// <para>Else return CommonFunc.Button(string, string, string, string);</para>
        /// </param>
        /// <returns>
        /// </returns>
        public static string ButtonWithParams(params string[] sParam)
        {
            string css = sParam[0];
            string title = sParam[1];
            string action = sParam[2];
            string displayText = sParam[3];

            if (string.IsNullOrEmpty(displayText))
                return Button(css, title, action);
            return Button(css, title, action, displayText);
        }

        public static string PtoIcon(int iStatus)
        {
            string sIconFormat = "<img class='icon' title='{0}' src='{1}' />";
            string imgSrc = "/Content/Images/PTO/";
            string sTitle = "";
            if (iStatus == Constants.PTO_STATUS_NEW)
            {
                imgSrc += "New.png";
                sTitle = "New";
            }
            else if (iStatus == Constants.PTO_STATUS_APPROVED)
            {
                imgSrc += "approve.png";
                sTitle = "Approved";
            }
            else if (iStatus == Constants.PTO_STATUS_REJECTED)
            {
                imgSrc += "reject.png";
                sTitle = "Rejected";
            }
            else
            {
                imgSrc += "lock.png";
                sTitle = "Confirmed";
            }
            return String.Format(sIconFormat, sTitle, imgSrc);
        }

        #region phihung.nguyen

        /// <summary>
        /// Split File For Display,Download
        /// </summary>
        /// <param name="originalString"></param>
        /// <returns></returns>
        public static string SplitFileName(string originalString, string path, bool isLimit)
        {

            string link = string.Empty;
            string outputFile = string.Empty;
            if (!string.IsNullOrEmpty(originalString))
            {
                string[] arrLink = originalString.TrimEnd(Constants.FILE_CHAR_PREFIX).Split(Constants.FILE_CHAR_PREFIX);//Split File
                foreach (string arr in arrLink)
                {
                    int lastCharWithExtension = arr.LastIndexOf('.');
                    string fileWithExtension = arr.Remove(lastCharWithExtension, arr.Length - lastCharWithExtension);
                    string ext = arr.Substring(lastCharWithExtension);
                    int indexChar = fileWithExtension.LastIndexOf('.');
                    string fileName = fileWithExtension.Remove(indexChar, fileWithExtension.Length - indexChar);
                    string fileView = fileName + ext;
                    link += "<a href='#' title='" + fileView + "' onClick=\"CRM.downLoadFile('" + path + arr + "','" + fileName + "')\" >" + (isLimit ? FileDisplay(fileView) : fileView) + "</a>" + " <br />";
                }
            }
            return link;
        }

        /// <summary>
        /// Split File For Display,Download
        /// </summary>
        /// <param name="originalString"></param>
        /// <returns></returns>
        public static string SplitFileName(string originalString, string path, bool isLimit,char character)
        {
            try
            {
                string link = string.Empty;
                string outputFile = string.Empty;
                if (!string.IsNullOrEmpty(originalString))
                {
                    string[] arrLink = originalString.TrimEnd(character).Split(character);//Split File
                    foreach (string arr in arrLink)
                    {
                        int lastCharWithExtension = arr.LastIndexOf('.');
                        string fileWithExtension = arr.Remove(lastCharWithExtension, arr.Length - lastCharWithExtension);
                        string ext = arr.Substring(lastCharWithExtension);
                        int indexChar = fileWithExtension.LastIndexOf('.');
                        string fileName = "";
                        if (indexChar > 0)
                        {
                            fileName = fileWithExtension.Remove(indexChar, fileWithExtension.Length - indexChar);
                        }
                        else
                        {
                            fileName = fileWithExtension;
                        }
                        string fileView = fileName + ext;
                        link += "<a href='#' title='" + fileView + "' onClick=\"CRM.downLoadFile('" + path + arr + "','" + fileName + "')\" >" + (isLimit ? FileDisplay(fileView) : fileView) + "</a>" + " <br />";
                    }
                }
                return link;
            }
            catch
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// Split File For Display,Download
        /// </summary>
        /// <param name="originalString"></param>
        /// <returns></returns>
        public static string SplitFileNameForPurchaseRequest(string originalString, string path)
        {

            string link = string.Empty;
            string outputFile = string.Empty;
            if (!string.IsNullOrEmpty(originalString))
            {
                string[] arrLink = originalString.TrimEnd(Constants.FILE_CHAR_PREFIX).Split(Constants.FILE_CHAR_PREFIX);//Split File
                foreach (string arr in arrLink)
                {
                    link += "<a href='#' title='" + arr + "' onClick=\"CRM.downLoadFile('" + path + arr + "','" + arr + "')\" >" + arr + "</a>" + " <br />";
                }
            }
            return link;
        }

        /// <summary>
        ///  Split File For Display,Download When Edit
        /// </summary>
        /// <param name="originalString"></param>
        /// <param name="path"></param>
        /// <param name="table"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public static string SplitFileNameForView(string originalString, string path, string table, string id, string hidField)
        {

            string link = string.Empty;
            string outputFile = string.Empty;
            int y = 0;
            if (!string.IsNullOrEmpty(originalString))
            {
                string[] arrLink = originalString.TrimEnd(Constants.FILE_CHAR_PREFIX).Split(Constants.FILE_CHAR_PREFIX);
                foreach (string arr in arrLink)
                {
                    int lastCharWithExtension = arr.LastIndexOf('.');
                    string fileWithExtension = arr.Remove(lastCharWithExtension, arr.Length - lastCharWithExtension);
                    string ext = arr.Substring(lastCharWithExtension);
                    int indexChar = fileWithExtension.LastIndexOf('.');
                    string fileName = fileWithExtension.Remove(indexChar, fileWithExtension.Length - indexChar);
                    string fileView = fileName + ext;
                    link += "<tr id='row_upload" + y + "'>" +
                                "<td class='label'>Attachment</td>" +
                                "<td id='contentFile" + y + "' class='input'>" +
                                    "<a id='link" + y + "' title='" + fileView + "' onClick=\"CRM.downLoadFile('" + path + arr + "','" + fileName + "')\">" + fileView + "</a> " +
                                "</td>" +
                                "<td id='contentButton" + y + "'>" +
                        //"<button  type='button'  title='Remove Contract' onclick=\"return CRM.removeMultiFile('" + arr + "','" + table + "','" + id + "','" + y + "');\" class='icon minus'></button>" +
                                    "<button  type='button'  title='Remove Contract' onclick=\"return CRM.removeMultiFile('" + arr + "','" + hidField + "','" + y + "','" + table + "');\" class='icon minus'></button>" +
                                "</td>" +
                            "</tr>";
                    y++;
                }
            }
            else
            {
                link += "<tr id='row_upload0'>" +
                            "<td class='label'>Attachment</td>" +
                            "<td id='contentFile0' class='input'>" +
                                "<input type='file' name='file' /></td>" +
                            "<td id='contentButton" + y + "'>" +
                                "<button type='button' onclick='return RemoveRow(this);' class='icon minus' id='0'  title='Remove Contract' ></button>" +
                            "</td>" +
                        "</tr>";
            }
            return link;
        }

        public static string FileDisplay(string originalFile)
        {
            if (originalFile.Length > Constants.FILE_NAME_VIEW)
            {
                int charIndex = originalFile.Length - originalFile.LastIndexOf('.');
                string ext = originalFile.Substring(originalFile.Length - charIndex);
                int fileview = originalFile.Length - Constants.FILE_NAME_VIEW;
                originalFile = originalFile.Remove(Constants.FILE_NAME_VIEW, fileview) + ".." + ext;
            }
            return originalFile;
        }

        /// <summary>
        /// Get Employee ID by User Name 
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public static Employee GetEmployeeByUserName(string userName)
        {
            string result = string.Empty;
            string officeEmail = userName + Constants.PREFIX_EMAIL_LOGIGEAR;
            Employee emp = new EmployeeDao().GetByOfficeEmailInActiveList(officeEmail);
            return emp;
        }

        public static string GetEmployeeIDByUserName(string userName)
        {
            string result = string.Empty;
            string officeEmail = userName + Constants.PREFIX_EMAIL_LOGIGEAR;
            Employee emp = new EmployeeDao().GetByOfficeEmailInActiveList(officeEmail);
            if (emp != null)
            {
                result = emp.ID;
            }
            return result;
        }

        public static string GetUserNameLoginByEmpID(string empID)
        {
            string result = string.Empty;
            Employee emp = new EmployeeDao().GetById(empID);
            if (emp != null && !string.IsNullOrEmpty(emp.OfficeEmail))
            {
                result = emp.OfficeEmail.Replace(Constants.PREFIX_EMAIL_LOGIGEAR, "");
            }
            return result;
        }

        public static string SetHTMLForPTO_Detail(string ptoID, int pto_Type)
        {
            string result = string.Empty;
            List<PTO_Detail> listPTO_Detail = new PTODao().GetPTO_DetailByPTO_ID(ptoID);
            PTO_Type objPTO_Type = new PTOTypeDao().GetByID(pto_Type);
            if (objPTO_Type != null)
            {
                if (objPTO_Type.IsHourType)
                {
                    int index = 0;
                    string oldField = string.Empty;
                    foreach (PTO_Detail pto_Detail in listPTO_Detail)
                    {
                        string date = pto_Detail.DateOff.HasValue ? pto_Detail.DateOff.Value.ToString(Constants.DATETIME_FORMAT) : string.Empty;
                        result += "<tr class='trTypeDay'>" +
                                 "<td class='label'>Date</td>" +
                                "<td class='input'><input type='text' style='width:120px' id='Date" + index + "' name='Date" + index + "' value='" + date + "' /></td>" +
                                "<td class='label'>Hour(s)</td>" +
                                "<td class='input'><input type='text' style='width:120px' id='Hour" + index + "' name='Hour" + index + "' value='" + (pto_Detail.TimeOff.HasValue ? pto_Detail.TimeOff.Value.ToString() : string.Empty) + "'/></td>";
                        if (index == 0)
                        {
                            result += "<td valign='top'><button type='button' class='icon plus' onclick='AddDate(" + index + ");'> </button>" +
                                    "<button type='button' class='icon minus' onclick='RemoveDate();'></button></td>";
                        }
                        result += "<script type='text/javascript'>" +
                                    "$(function () {" +
                                        "$('#Date" + index + "').datepicker({" +
                                            "onClose: function () {$(this).valid();}" +
                                        "});" +
                                    "});" +
                                    "$('#Date" + index + "').rules('add', { required: true, checkDate: true });" +
                                    "$('#Hour" + index + "').rules('add', { required: true, number: true, min: 1, max: 8 });" +
                            "</script>";
                        oldField += index + ",";
                        index++;
                    }
                    result += "<script type='text/javascript'>" +
                                  "$('#hidDate').val('" + oldField + "');" +
                            "</script>";
                }
                else
                {
                    PTO_Detail pto_Detail = listPTO_Detail[0];
                    string fromDate = pto_Detail.DateOffFrom.HasValue ? pto_Detail.DateOffFrom.Value.ToString(Constants.DATETIME_FORMAT) : string.Empty;
                    string toDate = pto_Detail.DateOffTo.HasValue ? pto_Detail.DateOffTo.Value.ToString(Constants.DATETIME_FORMAT) : string.Empty;
                    result += "<tr id='trRangeDay'>" +
                                      "<td class='label'>From Date <span>*</span></td>" +
                                      "<td class='input'><input type='text' style='width:120px' id='FromDate' name='FromDate' value='" + fromDate + "' /></td>" +
                                      "<td class='label'>To Date <span>*</span></td>" +
                                      "<td class='input'><input type='text' style='width:120px' id='ToDate' name='ToDate'  value='" + toDate + "'/></td>" +
                                  "</tr>";
                    result += "<script type='text/javascript'>" +
                                    "$('#hidDate').val('');" +
                                    "$(function () {" +
                                        "$('#FromDate').datepicker({" +
                                            "onClose: function () {$(this).valid();}" +
                                        "});" +
                                        "$('#ToDate').datepicker({" +
                                            "onClose: function () {$(this).valid();}" +
                                        "});" +
                                    "});" +
                            "</script>";
                }
            }
            return result;
        }
        #endregion

        #region (Tuan.Minh.Nguyen)=> Function Get Control Value Support Eform
        public static bool GetRadioStatus(string radioButtonID, List<EForm_Detail> eFormDetailList, string radioValue)
        {
            bool isCheck = false;
            if (eFormDetailList != null)
            {
                EForm_Detail eFormDetail = eFormDetailList.Where(e => e.ControlID == radioButtonID).SingleOrDefault();
                if (eFormDetail != null)
                {
                    if (radioValue == eFormDetail.Value)
                        isCheck = true;
                }

            }
            return isCheck;
        }
        public static string GetTextBoxValue(string textBoxID, List<EForm_Detail> eFormDetailList)
        {
            string value = "";
            if (eFormDetailList != null)
            {
                EForm_Detail eFormDetail = eFormDetailList.Where(e => e.ControlID == textBoxID).SingleOrDefault();
                if (eFormDetail != null)
                    value = eFormDetail.Value;
            }
            return value;
        }

        public Message SaveControlToDB(FormCollection form, bool isInsert)
        {

            Message msg = null;
            List<EForm_Detail> detailList = new List<EForm_Detail>();
            string EformID = form.Get("Hidden1");
            for (int i = 0; i < form.Count; i++)
            {

                string controlID = form.GetKey(i);
                if (!controlID.StartsWith("Hidden"))
                {
                    CRM.Models.EForm_Detail detailItem = new Models.EForm_Detail();
                    string value = form.Get(controlID);
                    if (controlID.StartsWith("CheckBox"))
                    {
                        value = value.Split(Convert.ToChar(","))[0];
                    }
                    detailItem.ControlID = controlID;
                    detailItem.Value = value;
                    detailItem.EFormID = Int32.Parse(EformID);
                    detailList.Add(detailItem);

                }
            }
            CRM.Models.EformDao efDao = new EformDao();
            msg = isInsert == true ? efDao.Inser(detailList) : efDao.Update(detailList);

            return msg;
        }
        #endregion

        #region ==>Tuan.minh.nguyen (Functions support Active Directory and send Meeting request email)
        public static bool SendMeetingRequest(string start, string end, string location, string formMail, string fromName, string subject, string decs, string toMail)
        {

            SmtpClient sc = new SmtpClient(Constants.SMTP_MAIL_SERVER);
            System.Net.Mail.MailMessage msg = new System.Net.Mail.MailMessage();
            msg.From = new MailAddress(formMail, fromName);

            string[] to = toMail.Trim().Split(';');
            foreach (string strTo in to)
            {
                if (!string.IsNullOrWhiteSpace(strTo))
                    msg.To.Add(new MailAddress(strTo));
            }

            msg.Subject = subject;
            msg.Body = decs.Replace("\n", "<br/>");

            StringBuilder str = new StringBuilder();
            str.AppendLine("BEGIN:VCALENDAR");
            str.AppendLine("PRODID:-//Microsoft Corporation//Outlook 11.0 MIMEDIR//EN");
            str.AppendLine("VERSION:2.0");
            str.AppendLine("METHOD:REQUEST");
            str.AppendLine("BEGIN:VEVENT");
            str.AppendLine(string.Format("DTSTART:{0:yyyyMMddTHHmmssZ}", DateTime.Parse(start).ToUniversalTime()));
            str.AppendLine(string.Format("DTSTAMP:{0:yyyyMMddTHHmmssZ}", DateTime.Now));
            str.AppendLine(string.Format("DTEND:{0:yyyyMMddTHHmmssZ}", DateTime.Parse(end).ToUniversalTime()));
            str.AppendLine("LOCATION:" + location);
            str.AppendLine(string.Format("UID:{0}", Guid.NewGuid()));
            str.AppendLine(string.Format("DESCRIPTION:{0}", msg.Body));
            str.AppendLine(string.Format("X-ALT-DESC;FMTTYPE=text/html:{0}", msg.Body));
            str.AppendLine(string.Format("SUMMARY:{0}", msg.Subject));
            //str.AppendLine(string.Format("ORGANIZER:MAILTO:{0}", msg.From.Address));
            str.AppendLine(string.Format("ORGANIZER;NAME=\"{0}\":MAILTO:{1}",msg.From.DisplayName, msg.From.Address));
            str.AppendLine(string.Format("ATTENDEE;CN=\"{0}\";RSVP=TRUE:mailto:{1}", "phihung.nguyen@logigear.com", msg.To[0].Address));
            str.AppendLine("BEGIN:VALARM");
            str.AppendLine("TRIGGER:-PT15M");
            str.AppendLine("ACTION:DISPLAY");
            str.AppendLine("DESCRIPTION:Reminder");
            str.AppendLine("END:VALARM");
            str.AppendLine("END:VEVENT");
            str.AppendLine("END:VCALENDAR");

            System.Net.Mime.ContentType ct = new System.Net.Mime.ContentType("text/calendar");
            ct.Parameters.Add("method", "REQUEST");
            AlternateView avCal = AlternateView.CreateAlternateViewFromString(str.ToString(), ct);
            msg.AlternateViews.Add(avCal);

            AlternateView body = AlternateView.CreateAlternateViewFromString(msg.Body, new ContentType("text/html"));
            msg.AlternateViews.Add(body);

            try
            { sc.Send(msg); return true; }
            catch { return false; }
        }

        public static DirectoryEntry GetDomainUser(string loginName)
        {

            System.DirectoryServices.ActiveDirectory.Domain thisDomain = System.DirectoryServices.ActiveDirectory.Domain.GetCurrentDomain();

            DirectoryEntry deRoot = new DirectoryEntry("LDAP://" + thisDomain.Name);
            DirectorySearcher deSrch = new DirectorySearcher(deRoot);

            deSrch.Filter = "(SAMAccountName=" + loginName + ")";
            deSrch.PropertiesToLoad.Add("distinguishedName");
            deSrch.PropertiesToLoad.Add("cn");
            deSrch.PropertiesToLoad.Add("SAMAccountName");


            SearchResult result = deSrch.FindOne();
            DirectoryEntry directoryEntry = result.GetDirectoryEntry();

            return directoryEntry;
        }



        public static SearchResultCollection GetDomainUserList(string str)
        {

            System.DirectoryServices.ActiveDirectory.Domain thisDomain = System.DirectoryServices.ActiveDirectory.Domain.GetCurrentDomain();

            DirectoryEntry deRoot = new DirectoryEntry("LDAP://" + thisDomain.Name);
            DirectorySearcher deSrch = new DirectorySearcher(deRoot);

            deSrch.Filter = "(SAMAccountName=" + str + "*)";
            deSrch.SearchScope = SearchScope.Subtree;
            deSrch.ReferralChasing = ReferralChasingOption.All;
            deSrch.PropertiesToLoad.Add("distinguishedName");
            deSrch.PropertiesToLoad.Add("cn");
            deSrch.PropertiesToLoad.Add("SAMAccountName");
            deSrch.PropertiesToLoad.Add("mail");
            deSrch.PropertiesToLoad.Add("userPrincipalName");
            deSrch.PropertiesToLoad.Add("displayName");
            SearchResultCollection Results = deSrch.FindAll();
            return Results;

        }


        public static string GetEmailByLoginName(string loginName)
        {
            return GetDomainUser(loginName).Properties[GetDomainUserProperty(DomainUserProperty.OutlookEmail)][0].ToString();
        }

        public static string GetLoginNameByEmail(string email)
        {
            string value = string.Empty;
            if (!string.IsNullOrEmpty(email))
            {
                DirectoryEntry dentry = new DirectoryEntry("LDAP://logigear.com");
                DirectorySearcher deSearch = new DirectorySearcher(dentry);

                deSearch.Filter = "(mail=" + email + ")";

                SearchResult result = deSearch.FindOne();

                if (result != null)
                {
                    value = result.Properties[GetDomainUserProperty(DomainUserProperty.LoginName)][0].ToString();
                }
            }
            return value;
        }

        public static string GetDomainUserProperty(DomainUserProperty property)
        {
            switch (property)
            {
                case DomainUserProperty.AccountName:
                    return "Name";
                case DomainUserProperty.ChangedDate:
                    return "whenChanged";
                case DomainUserProperty.Company:
                    return "company";
                case DomainUserProperty.CreatedDate:
                    return "whenCreated";
                case DomainUserProperty.Department:
                    return "department";
                case DomainUserProperty.DisplayName:
                    return "displayName";
                case DomainUserProperty.LoginName:
                    return "sAMAccountName";
                case DomainUserProperty.OutlookEmail:
                    return "mail";
                case DomainUserProperty.SeatCode:
                    return "physicalDeliveryOfficeName";
                case DomainUserProperty.Title:
                    return "title";
                default:
                    return "";
            }
        }


        #endregion

        #region Huy.Ly added by 16-Nov-2010

        public static string OutputContractListByDate(List<Contract> list)
        {
            string result = string.Empty;
            if (list.Count > 0)
            {
                result += "<button id='btnExport' type='button' title='Export' class='button export' style='margin-bottom:7px;margin-right:20px;'>Export</button>";

                if (list.Count > 10)
                {
                    result += "<div id='div_notification' style='overflow-x: scroll;overflow-y: auto; padding-top: 0px; height:280px; width:650px'>";
                }
                result += "<table class='grid'>";
                result += "<tr>";
                result += "<th>ID</th>";
                result += "<th>Sub Department</th>";
                result += "<th>Contract Type</th>";
                result += "<th>Start Date</th>";
                result += "<th>End Date</th>";
                result += "<th>Action</th>";
                result += "</tr>";
                foreach (Contract item in list)
                {
                    result += "<tr>";
                    result += "<td align='center'><a  href='/Employee/Detail/" + item.Employee.ID + "' id=" + item.Employee.ID + " class=showTooltip >" + item.Employee.ID + "</a></td>";
                    result += "<td align='left'>" + item.Employee.Department.DepartmentName + "</td>";
                    result += "<td align='center'>" + item.ContractType1.ContractTypeName + "</td>";
                    result += "<td align='center'>" + item.StartDate.ToString(Constants.DATETIME_FORMAT_VIEW) + "</td>";
                    result += "<td align='center'>" + item.EndDate.Value.ToString(Constants.DATETIME_FORMAT_VIEW) + "</td>";
                    result += "<td align='center'>" + "<a href='/Employee/ContractRenewal/" + item.Employee.ID + "' title='Add contract' ><img border='0' src='/Content/Images/Icons/notepad.png' alt='Add contract'></a>" + "</td>";
                    result += "</tr>";
                }
                result += "</table>";
                if (list.Count > 10)
                {
                    result += "</div>";
                }
            }
            return result;
        }

        public static string OutputPTOListByDate(List<sp_GetPTOEmployeeListResult> list)
        {
            string result = string.Empty;
            if (list.Count > 0)
            {
                result += "<table class='grid'>";
                result += "<tr>";
                result += "<th>ID</th>";
                result += "<th>Sub Department</th>";
                result += "<th>Contract Type</th>";
                result += "<th>Start Date</th>";
                result += "<th>End Date</th>";
                result += "<th>Action</th>";
                result += "</tr>";
                foreach (sp_GetPTOEmployeeListResult item in list)
                {
                    result += "<tr>";
                    result += "<td align='left'>" + CommonFunc.PtoIcon(item.StatusID) + "</td>";
                    result += "<td align='left'>" + CommonFunc.Link(item.ID, "#", item.ID.ToString(), true) + "</td>";
                    result += "<td align='left'>" + item.Hours.Value.ToString() + "</td>";
                    result += "<td align='left'>" + item.CreateDate.ToString(Constants.DATETIME_FORMAT_VIEW) + "</td>";
                    result += "<td align='left'>" + item.StatusName + "</td>";
                    result += "<td align='left'>" + item.TypeName + "</td>";
                    result += "<td align='left'>" + item.SubmitTo + "</td>";
                    result += "</tr>";
                }
                result += "</table>";
            }
            return result;
        }

        /// <summary>
        /// Encode and replace multiple line
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public static string SyntaxHighLight(string content)
        {
            string result = content;
            string[] keywordArray = { "public", "string", "int", "return", "double", "float" };
            string[] objectArray = { "Array", "ArrayList", "DateTime", "Object", "Int", "String", "Float", "Double" };
            string strFormat = "<span class=\"{0}\">{1}</span>";
            foreach (string keyword in keywordArray)
            {
                result = result.Replace(keyword, string.Format(strFormat, "keyword", keyword));
            }
            foreach (string keyword in objectArray)
            {
                result = result.Replace(keyword, string.Format(strFormat, "object", keyword));
            }
            return result;
        }

        #endregion

        #region Duy Hung Nguyen - String Util

        /// <summary>
        /// Sub string the content, loop to round a word 
        /// </summary>
        /// <param name="template"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public static string SubStringRoundWord(string template, int length)
        {
            if (!string.IsNullOrEmpty(template))
            {
                template = template.Trim();
                int maxLength = template.Length > length ? length : template.Length;
                if (template.Length > maxLength)
                {
                    while (template[maxLength - 1] != ' ' && template.Length > maxLength)
                    {
                        maxLength++;
                    }
                }
                if (template.Length > length)
                {
                    return template.Substring(0, maxLength) + "...";
                }
                else
                {
                    return template.Substring(0, maxLength);
                }
            }
            return string.Empty;
        }

        /// <summary>
        /// Sub string the content, loop to round a word, use for programming skill 
        /// </summary>
        /// <param name="template"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public static string SubStringRoundWordNotMultiline(string template, int length)
        {
            if (!string.IsNullOrEmpty(template))
            {
                template = template.Trim();
                int maxLength = template.Length > length ? length : template.Length;
                if (template.Length > maxLength)
                {
                    while (template[maxLength - 1] != ' ' && template.Length > maxLength)
                    {
                        maxLength++;
                    }
                }
                if (template.Length > length)
                {
                    return HttpUtility.HtmlEncode(template.Substring(0, maxLength)) + "...";
                }
                else
                {
                    return HttpUtility.HtmlEncode(template.Substring(0, maxLength));
                }
            }
            return string.Empty;
        }

        /// <summary>
        /// cut the content to a specific length with only one row
        /// </summary>
        /// <param name="template"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public static string SubStringRoundWordToOneRow(string template, int length)
        {
            if (!string.IsNullOrEmpty(template))
            {
                string breakLine = "\r\n";
                string sMore = "...";
                template = template.Trim();
                if (template.Contains(breakLine))
                {
                    template = template.Substring(0, template.IndexOf(breakLine));
                    if (template.Length <= length)
                        return template + sMore;
                }
                return SubStringRoundWord(template, length);
            }
            return string.Empty;
        }

        /// <summary>
        /// Encode and replace multiple line
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public static string Encode(string content)
        {
            if (string.IsNullOrEmpty(content))
            {
                return string.Empty;
            }
            else
            {
                return content;
            }
        }

        /// <summary>
        /// Remove the script
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string RemoveScript(string value)
        {
            var result = Regex.Replace(value, "<script.*?>.*?</script>", "", RegexOptions.IgnoreCase);
            result = Regex.Replace(result, "<script.*?>", "", RegexOptions.IgnoreCase);
            result = Regex.Replace(result, ".*?</script>", "", RegexOptions.IgnoreCase);
            //result = result.Replace("eval\\((.*)\\)", "");
            //result = result.Replace("[\\\"\\\'][\\s]*javascript:(.*)[\\\"\\\']", "\"\"");
            //result = result.Replace("\'", "&#39;");

            return result;
        }

        /// <summary>
        /// Remove html tags
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string RemoveHtmlTags(string value)
        {
            string[] tagsAllowed = Constants.LOT_TAG_NAME_ALLOWED.Split(',');
            return StripTags(value, tagsAllowed);
        }

        /// <summary>
        /// Remove html tags
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string RemoveAllHtmlWithNoTagsAllowed(string value)
        {
            return StripTags(value, new string[] { }).Replace("&nbsp;", " ");
        }

        /// <summary>
        /// encrypt Data by Md5 type
        /// </summary>
        /// <param name="data"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public static string encryptDataMd5(string data, int length)
        {
            System.Security.Cryptography.MD5CryptoServiceProvider md5Hasher = new System.Security.Cryptography.MD5CryptoServiceProvider();
            byte[] hashedBytes;
            System.Text.UTF8Encoding encoder = new System.Text.UTF8Encoding();
            hashedBytes = md5Hasher.ComputeHash(encoder.GetBytes(data));
            StringBuilder sbuilder = new StringBuilder();
            for (int i = 0; i < length; i++)
            {
                sbuilder.Append(hashedBytes[i].ToString("x2"));
            }
            return sbuilder.ToString();
        }

        /// <summary>
        /// Replace First
        /// </summary>
        /// <param name="haystack"></param>
        /// <param name="needle"></param>
        /// <param name="replacement"></param>
        /// <returns></returns>
        private static string ReplaceFirst(string haystack, string needle, string replacement)
        {
            int pos = haystack.IndexOf(needle);
            if (pos < 0) return haystack;
            return haystack.Substring(0, pos) + replacement + haystack.Substring(pos + needle.Length);
        }

        /// <summary>
        /// Replace All
        /// </summary>
        /// <param name="haystack"></param>
        /// <param name="needle"></param>
        /// <param name="replacement"></param>
        /// <returns></returns>
        private static string ReplaceAll(string haystack, string needle, string replacement)
        {
            int pos;
            // Avoid a possible infinite loop
            if (needle == replacement) return haystack;
            while ((pos = haystack.IndexOf(needle)) > 0)
                haystack = haystack.Substring(0, pos) + replacement + haystack.Substring(pos + needle.Length);
            return haystack;
        }

        /// <summary>
        /// Remove html tags
        /// </summary>
        /// <param name="Input"></param>
        /// <param name="AllowedTags">Tags name allowed (dont remove when meet these tags)</param>
        /// <returns></returns>
        public static string StripTags(string Input, string[] AllowedTags)
        {
            Regex StripHTMLExp = new Regex(@"(<\/?[^>]+>)");
            string Output = Input;

            foreach (Match Tag in StripHTMLExp.Matches(Input))
            {
                string HTMLTag = Tag.Value.ToLower();
                bool IsAllowed = false;

                foreach (string AllowedTag in AllowedTags)
                {
                    int offset = -1;

                    // Determine if it is an allowed tag
                    // "<tag>" , "<tag " and "</tag"
                    if (offset != 0) offset = HTMLTag.IndexOf('<' + AllowedTag + '>');
                    if (offset != 0) offset = HTMLTag.IndexOf('<' + AllowedTag + ' ');
                    if (offset != 0) offset = HTMLTag.IndexOf("</" + AllowedTag);

                    // If it matched any of the above the tag is allowed
                    if (offset == 0)
                    {
                        IsAllowed = true;
                        break;
                    }
                }

                // Remove tags that are not allowed
                if (!IsAllowed) Output = ReplaceFirst(Output, Tag.Value, "");
            }

            return Output;
        }

        /// <summary>
        /// Remove html tags and it's attribute 
        /// </summary>
        /// <param name="Input"></param>
        /// <param name="AllowedTags"></param>
        /// <returns></returns>
        public static string StripTagsAndAttributes(string Input, string[] AllowedTags)
        {
            /* Remove all unwanted tags first */
            string Output = StripTags(Input, AllowedTags);

            /* Lambda functions */
            MatchEvaluator HrefMatch = m => m.Groups[1].Value + "href..;,;.." + m.Groups[2].Value;
            MatchEvaluator ClassMatch = m => m.Groups[1].Value + "class..;,;.." + m.Groups[2].Value;
            MatchEvaluator StyleMatch = m => m.Groups[1].Value + "style..;,;.." + m.Groups[2].Value;
            MatchEvaluator UnsafeMatch = m => m.Groups[1].Value + m.Groups[4].Value;

            /* Allow the "href" attribute */
            Output = new Regex("(<a.*)href=(.*>)").Replace(Output, HrefMatch);

            /* Allow the "class" attribute */
            Output = new Regex("(<a.*)class=(.*>)").Replace(Output, ClassMatch);

            /* Allow the "style" attribute */
            Output = new Regex("(<span.*)style=(.*>)").Replace(Output, StyleMatch);

            /* Remove unsafe attributes in any of the remaining tags */
            Output = new Regex(@"(<.*) .*=(\'|\""|\w)[\w|.|(|)]*(\'|\""|\w)(.*>)").Replace(Output, UnsafeMatch);

            /* Return the allowed tags to their proper form */
            Output = ReplaceAll(Output, "..;,;..", "=");

            return Output;
        }
        #endregion

        /// <summary>
        /// Read all message from resource and paste to JS
        /// </summary>
        /// <returns></returns>
        public static string ParseResourceToJS()
        {
            StringBuilder jsConstant = new StringBuilder();
            ResourceSet rs = Resources.Message.ResourceManager.GetResourceSet(CultureInfo.CurrentCulture, true, true);
            jsConstant.AppendLine("<script type=\"text/javascript\">");
            foreach (DictionaryEntry r in rs)
            {
                jsConstant.AppendLine("\tvar " + r.Key.ToString() + " = \"" + r.Value.ToString().Replace("\"", "\\\"") + "\";");
            }
            jsConstant.AppendLine("</script>");
            return jsConstant.ToString();
        }

        /// <summary>
        /// Remove a file
        /// </summary>
        /// <param name="fileName">file url including filename</param>
        public static void RemoveFile(string fileUrl)
        {
            string serverPath = System.Web.HttpContext.Current.Server.MapPath("~");
            File.Delete(serverPath + fileUrl);
        }

        /// <summary>
        /// Set Page Index of JqGrid
        /// </summary>
        /// <param name="countGrid"></param>
        /// <param name="currentPage"></param>
        /// <param name="rowNumber"></param>
        /// <returns></returns>
        #region Set Page Index
        public static int SetJqGridPageIndex(int countGrid, int currentPage, int rowNumber)
        {
            int pageIndex = 1;
            if (countGrid > rowNumber)
            {
                pageIndex = currentPage;
            }
            return pageIndex;
        }
        #endregion
        public static string SetPTO_ID(string empID)
        {
            PTODao ptoDao = new PTODao();
            string pto_ID = string.Empty;
            List<PTO> list = ptoDao.GetPTOByEmpIDIncludeDeleted(empID);
            if (list.Count > 0)
            {
                string[] array = list[list.Count - 1].ID.Split(Constants.PTO_CHAR_PREFIX);
                int index = int.Parse(array[array.Count() - 1]) + 1;
                pto_ID = Constants.PTO_PREFIX + Constants.PTO_STRING_PREFIX + empID + Constants.PTO_STRING_PREFIX + index;
            }
            else
            {
                pto_ID = Constants.PTO_PREFIX + Constants.PTO_STRING_PREFIX + empID + Constants.PTO_STRING_PREFIX + 1;
            }
            return pto_ID;
        }

        /// <summary>
        /// Get fullname default: first-middle-last
        /// </summary>
        /// <param name="firstName"></param>
        /// <param name="middleName"></param>
        /// <param name="lastName"></param>
        /// <returns></returns>
        public static string GetFullName(string firstName, string middleName, string lastName)
        {
            string fullName = firstName + (string.IsNullOrEmpty(middleName) ? " " : " " + middleName + " ") + lastName;

            return fullName;
        }

        /// <summary>
        /// Get fullname with format
        /// </summary>
        /// <param name="firstName"></param>
        /// <param name="middleName"></param>
        /// <param name="lastName"></param>
        /// <param name="nameFormat">The format of fullname</param>
        /// <returns></returns>
        public static string GetFullName(string firstName, string middleName, string lastName, Constants.FullNameFormat nameFormat)
        {
            string fullName = string.Empty;

            switch (nameFormat)
            {
                case Constants.FullNameFormat.FirstLastMiddle:
                    fullName = firstName + " " + lastName + (string.IsNullOrEmpty(middleName) ? "" : " " + middleName);
                    break;
                case Constants.FullNameFormat.FirstMiddleLast:
                    fullName = firstName + (string.IsNullOrEmpty(middleName) ? " " : " " + middleName + " ") + lastName;
                    break;
                case Constants.FullNameFormat.MiddleFirstLast:
                    fullName = (string.IsNullOrEmpty(middleName) ? "" :  middleName + " ") + firstName + " " + lastName;
                    break;
                case Constants.FullNameFormat.LastMiddleFirst:
                    fullName = lastName + (string.IsNullOrEmpty(middleName) ? " " : " " + middleName + " ") + firstName;
                    break;
                case Constants.FullNameFormat.LastFirstMiddle:
                    fullName = lastName + " " + firstName + (string.IsNullOrEmpty(middleName) ? "" : " " + middleName);
                    break;
                case Constants.FullNameFormat.MiddleLastFirst:
                    fullName = (string.IsNullOrEmpty(middleName) ? "" : middleName + " ") + lastName + " " + firstName;
                    break;
            }

            return fullName;
        }

        /// <summary>
        /// Get Fullname of Employee
        /// </summary>
        /// <param name="emp"></param>
        /// <returns></returns>
        public static string GetEmployeeFullName(Employee emp)
        {
            string st = string.Empty;
            if (emp != null)
            {
                if (string.IsNullOrEmpty(emp.MiddleName))
                {
                    st = emp.FirstName + " " + emp.LastName;
                }
                else
                {
                    st = emp.FirstName + " " + emp.MiddleName + " " + emp.LastName;
                }
            }
            return st;
        }

        /// <summary>
        /// Get employee fullname /*Added by Tai Nguyen*/
        /// </summary>
        /// <param name="emp"></param>
        /// <param name="nameFormat"></param>
        /// <returns></returns>
        /// 

        public static string GetEmployeeFullName(Employee emp, Constants.FullNameFormat nameFormat)
        {
            string st = string.Empty;
            if (emp != null)
            {
                if (string.IsNullOrEmpty(emp.MiddleName))
                {
                    switch (nameFormat)
                    {
                        case Constants.FullNameFormat.FirstLastMiddle:
                        case Constants.FullNameFormat.FirstMiddleLast:
                        case Constants.FullNameFormat.MiddleFirstLast:
                            st = emp.FirstName + " " + emp.LastName;
                            break;
                        case Constants.FullNameFormat.LastMiddleFirst:
                        case Constants.FullNameFormat.LastFirstMiddle:
                        case Constants.FullNameFormat.MiddleLastFirst:
                            st = emp.LastName + " " + emp.FirstName;
                            break;
                    }
                }
                else
                {
                    switch (nameFormat)
                    {
                        case Constants.FullNameFormat.FirstLastMiddle:
                            st = emp.FirstName + " " + emp.LastName + " " + emp.MiddleName;
                            break;
                        case Constants.FullNameFormat.FirstMiddleLast:
                            st = emp.FirstName + " " + emp.MiddleName + " " + emp.LastName;
                            break;
                        case Constants.FullNameFormat.MiddleFirstLast:
                            st = emp.MiddleName + " " + emp.FirstName + " " + emp.LastName;
                            break;
                        case Constants.FullNameFormat.LastMiddleFirst:
                            st = emp.LastName + " " + emp.MiddleName + " " + emp.FirstName;
                            break;
                        case Constants.FullNameFormat.LastFirstMiddle:
                            st = emp.LastName + " " + emp.FirstName + " " + emp.MiddleName;
                            break;
                        case Constants.FullNameFormat.MiddleLastFirst:
                            st = emp.MiddleName + " " + emp.LastName + " " + emp.FirstName;
                            break;
                    }
                }
            }
            return st;
        }

        public static string GetEmailFooter()
        {
            string tmpFilePath = HttpContext.Current.Server.MapPath(Constants.PTO_EMAIL_FOOTER_TEMPLATE_PATH);
            string sTemplate = string.Empty;
            if (System.IO.File.Exists(tmpFilePath))
            {
                sTemplate = System.IO.File.ReadAllText(tmpFilePath);
            }
            return sTemplate;
        }

        public static void SendEmailToEmployee(PTODao ptoDao, PTO pto, List<PTO_Detail> listPTO_Detail, bool? isManager, string ccMail="")
        {
            try
            {
                EmployeeDao empDao = new EmployeeDao();
                string template="";
                if (pto.DeleteFlag)
                    template = GetTemplateForDeletion;
                else if(isManager.HasValue)
                    template = isManager.Value ? GetTemplateForManager : GetTemplateForEmployee;


                string sTemplate = string.Empty;
                string sFooter = GetEmailFooter();

                sTemplate =  template + sFooter;
                //PTO pto = ptoDao.GetPTOById(id);
                Employee emp = empDao.GetById(pto.Submitter);
                Employee manager = empDao.GetById(pto.SubmitTo);
                string actor = CommonFunc.GetEmployeeFullName(manager);
                
                sTemplate = sTemplate.Replace(Constants.PTO_EMAIL_FIELD_PTO_REASON, HttpUtility.HtmlEncode(pto.Reason));
                sTemplate = sTemplate.Replace(Constants.PTO_EMAIL_FIELD_PTO_STATUS_NAME, pto.PTO_Status.Name);
                sTemplate = sTemplate.Replace(Constants.PTO_EMAIL_FIELD_PTO_TYPE_NAME, pto.PTO_Type.Name);

                string sPTO_ID = "";
                string sAction = "";
                string sEmailSubject = "";
                string sEmpName = CommonFunc.GetEmployeeFullName(emp, Constants.FullNameFormat.FirstMiddleLast);
                string toEmail = pto.Employee.OfficeEmail;
                if (pto.DeleteFlag)
                {
                    sEmailSubject = String.Format(Constants.PTO_EMAIL_SUBJECT_DELETE, pto.ID);
                }
                else if (pto.Status_ID == Constants.PTO_STATUS_NEW)
                {
                    sPTO_ID = "Your PTO (" + pto.ID + ")";
                    sAction = "submitted to";
                    sEmailSubject = String.Format(Constants.PTO_EMAIL_SUBJECT_NEW, pto.ID);
                }
                else if (pto.Status_ID == Constants.PTO_STATUS_APPROVED)
                {
                    sPTO_ID = "The " + pto.ID;
                    sAction = "approved by";
                    sEmailSubject = String.Format(Constants.PTO_EMAIL_SUBJECT_APPROVE_REJECT, pto.ID, "approved");
                }
                else if (pto.Status_ID == Constants.PTO_STATUS_REJECTED)
                {
                    sPTO_ID = "The " + pto.ID;
                    sAction = "rejected by";
                    sEmailSubject = String.Format(Constants.PTO_EMAIL_SUBJECT_APPROVE_REJECT, pto.ID, "rejected");
                }
                else if (pto.Status_ID == Constants.PTO_STATUS_CONFIRM)
                {
                    sPTO_ID = "The " + pto.ID;
                    sAction = "approved by";
                    sEmailSubject = String.Format(Constants.PTO_EMAIL_SUBJECT_APPROVE_REJECT, pto.ID, "approved");
                }
                else if (pto.Status_ID == Constants.PTO_STATUS_VERIFIED)
                {
                    sPTO_ID = "The " + pto.ID;
                    sAction = "verified by";
                    actor = CommonFunc.GetEmployeeFullName(CommonFunc.GetEmployeeByUserName(HttpContext.Current.User.Identity.Name), 
                        Constants.FullNameFormat.FirstMiddleLast);
                    sEmailSubject = String.Format(Constants.PTO_EMAIL_SUBJECT_VERIFIED, pto.ID);
                }
                if (!isManager.HasValue)
                {
                    toEmail = HttpContext.Current.User.Identity.Name + Constants.LOGIGEAR_EMAIL_DOMAIN;
                }
                else if (isManager.Value)
                {
                    if(!pto.DeleteFlag)
                        sEmailSubject = String.Format(Constants.PTO_EMAIL_SUBJECT_TO_MANAGER, sEmpName);
                    toEmail = pto.Employee1.OfficeEmail;
                }
                var empUser = CommonFunc.GetEmployeeByUserName(HttpContext.Current.User.Identity.Name);
                string recipent = "";
                if (!isManager.HasValue)
                    recipent = empDao.FullName(empUser.ID, Constants.FullNameFormat.FirstMiddleLast);
                else if (isManager.Value)
                    recipent = empDao.FullName(manager.ID, Constants.FullNameFormat.FirstMiddleLast);
                else
                    recipent = sEmpName;

                string commentTemplate = "<br/><b>{0}</b>: <i>{1}</i>";
                sTemplate = sTemplate.Replace(Constants.PTO_EMAIL_FIELD_MANAGER_NAME, actor);
                sTemplate = sTemplate.Replace(Constants.PTO_EMAIL_FIELD_EMPLOYEE_NAME, sEmpName);
                sTemplate = sTemplate.Replace(Constants.PTO_EMAIL_FIELD_EMPLOYEE_SUBMIT_DATE, pto.CreateDate.ToString(Constants.DATETIME_FORMAT_VIEW));
                sTemplate = sTemplate.Replace(Constants.PTO_EMAIL_FIELD_PTO_ID, sPTO_ID);
                sTemplate = sTemplate.Replace(Constants.PTO_EMAIL_FIELD_PTO_REAL_ID, pto.ID);
                sTemplate = sTemplate.Replace(Constants.PTO_EMAIL_FIELD_PTO_ACTION, sAction);
                sTemplate = sTemplate.Replace(Constants.PTO_EMAIL_FIELD_PTO_USER, empDao.FullName(empUser.ID, Constants.FullNameFormat.FirstMiddleLast));
                sTemplate = sTemplate.Replace(Constants.PTO_EMAIL_FIELD_PTO_PTO_ID, pto.ID);
                sTemplate = sTemplate.Replace(Constants.PTO_EMAIL_FIELD_PTO_RECIPENT, recipent);
                sTemplate = sTemplate.Replace(Constants.PTO_PM_COMMENT_FIELD, string.IsNullOrEmpty(pto.ManagerComment) ? "" : 
                    string.Format(commentTemplate, "Manager comment (" + 
                    CommonFunc.GetEmployeeFullName(pto.Employee1, Constants.FullNameFormat.FirstMiddleLast) + 
                    ")", HttpUtility.HtmlEncode(pto.ManagerComment)));
                sTemplate = sTemplate.Replace(Constants.PTO_HR_COMMENT_FIELD, string.IsNullOrEmpty(pto.HRComment) ? "" :
                    string.Format(commentTemplate, "HR comment", HttpUtility.HtmlEncode(pto.HRComment)));
                string sDetail = "";
                //List<PTO_Detail> listPTO_Detail = ptoDao.GetPTO_DetailByPTO_ID(pto.ID).OrderBy(p => p.DateOff).ToList();
                foreach (PTO_Detail detail in listPTO_Detail)
                {
                    if (pto.PTO_Type.IsHourType)
                    {
                        sDetail += detail.DateOff.Value.ToString(Constants.DATETIME_FORMAT_VIEW) +
                            ": " + (detail.HourFrom < 10 ? "0" + detail.HourFrom : detail.HourFrom.ToString()) + ":00 » " + 
                            (detail.HourTo < 10 ? "0" + detail.HourTo : detail.HourTo.ToString()) + ":00<br/>";
                    }
                    else
                    {
                        sDetail += "From " + detail.DateOffFrom.Value.ToString(Constants.DATETIME_FORMAT_VIEW);
                        sDetail += " to " + detail.DateOffTo.Value.ToString(Constants.DATETIME_FORMAT_VIEW);
                        sDetail += "<br/>";
                    }
                }
                sTemplate = sTemplate.Replace(Constants.PTO_EMAIL_FIELD_PTO_DETAIL, sDetail);

                string host = ConfigurationManager.AppSettings["mailserver_host"];
                string port = ConfigurationManager.AppSettings["mailserver_port"];

                string from = Constants.CRM_MAIL_FROM_ADDRESS;
                string fromName = Constants.PTO_SENDER_NAME;
                
                WebUtils.SendMail(host, port, from, fromName, toEmail, ccMail, sEmailSubject, sTemplate);
            }
            catch
            {

            }
        }

        public static string CurrencyDisplay(string value, string currency)
        {
            string result = value;
            int count = 0;
            for (int i = result.Length - 1; i > 0; i--)
            {
                count++;
                if (count == 3)
                {
                    result = result.Insert(i, Constants.CURRENCY_DELIMITER);
                    count = 0;
                }
            }
            return result + " " + currency;
        }

        /// <summary>
        /// Descypt salary follow permission
        /// </summary>
        /// <param name="text"></param>
        /// <param name="principal"></param>
        /// <param name="groupDao"></param>
        /// <param name="userAdminDao"></param>
        /// <returns></returns>
        public static string DescyptSalary(string text, AuthenticationProjectPrincipal principal, GroupDao groupDao, UserAdminDao userAdminDao)
        {
            bool canViewSalary = groupDao.HasPermisionOnModule(
                userAdminDao.GetByUserName(principal.UserData.UserName).UserAdminId, (int)Permissions.Read, (int)Modules.ViewSalaryInfo);
            string result = "";
            if (!string.IsNullOrEmpty(text))
            {
                result = canViewSalary ? EncryptUtil.Decrypt(text) : Constants.PRIVATE_DATA;
            }
            return result;
        }

        public static string GetJobRequestByJobRequestItem(string requestId)
        {
            JobRequestItemDao itemDao = new JobRequestItemDao();
            return itemDao.GetByID(requestId).JobRequest.ID.ToString();
        }

        public static bool IsPortalUserAuthorized(string userID)
        {
            bool isAuthorized = false;
            Employee obj = new EmployeeDao().GetById(userID);
            if (obj != null)
            {
                JobTitleLevel objTitle = new JobTitleLevelDao().GetById(obj.TitleId);
                if (objTitle != null)
                {
                    if (objTitle.JobTitle.IsManager)
                    {
                        isAuthorized = true;
                    }
                }
            }
            return isAuthorized;
        }

        public static string FormatCurrency(double number)
        {
            string value = string.Empty;
            if (number > 0)
            {
                value = number.ToString("N", CultureInfo.InvariantCulture);
                if (value.EndsWith(".00"))
                {
                    value = value.Replace(".00", "");
                }
            }
            else
            {
                value = "0.0";
            }
            return value;
        }

        public static DateTime GetPtoDateTo(DateTime month)
        {
            return new DateTime(month.Year, month.Month, int.Parse(Constants.DATE_LOCK_PTO));
        }

        public static DateTime GetPtoDateFrom(DateTime month)
        {
            return GetPtoDateTo(month).AddMonths(-1).AddDays(1);
        }

        /// <summary>
        /// Check moving request
        /// </summary>
        /// <param name="userID">int</param>
        /// <param name="request">FlowType</param>
        /// <param name="id">int</param>
        /// <param name="action">ActionType</param>
        /// <param name="role">int</param>
        /// <returns>bool</returns>
        public static bool CheckMovingRequest(int userID, Constants.FlowType request, string id, Constants.ActionType action, int role, bool isPortal = false)
        {
            bool allow = false;
            try
            {
                string srole = role.ToString();
                switch (request)
                {

                    case Constants.FlowType.FLOW_JOB_REQUEST:
                        JobRequestDao jobDao = new JobRequestDao();
                        JobRequest job = jobDao.GetById(int.Parse(id));
                        switch (action)
                        {

                            case Constants.ActionType.List:
                                if (job.InvolveRole.Contains(srole))
                                    allow = true;
                                break;
                            case Constants.ActionType.Update:
                                if (job.AssignRole.Equals(role) && job.AssignID.Equals(userID))
                                    allow = true;
                                break;
                        }

                        break;
                    case Constants.FlowType.FLOW_JOB_REQUEST_ITEM:
                        JobRequestItemDao jobItemDao = new JobRequestItemDao();
                        JobRequestItem jobItem = jobItemDao.GetByID(id);
                        switch (action)
                        {

                            case Constants.ActionType.Update:
                                if (jobItem.JobRequest.AssignRole.Equals(role) && jobItem.JobRequest.AssignID.Equals(userID))
                                    allow = true;
                                break;
                        }

                        break;
                    case Constants.FlowType.FLOW_PURCHASE_REQUEST:
                        PurchaseRequestDao purDao = new PurchaseRequestDao();
                        PurchaseRequest pur = purDao.GetByID(int.Parse(id));
                        switch (action)
                        {

                            case Constants.ActionType.List:
                                if (pur.InvolveRole.Contains(srole))
                                    allow = true;
                                break;
                            case Constants.ActionType.Update:
                                string loginName = new UserAdminDao().GetById(userID).UserName;
                                if (pur.WFStatusID == Constants.STATUS_OPEN)
                                {
                                    if (purDao.HasEditPermision(pur.ID, loginName, role))
                                        allow = true;
                                    else if (role != Constants.PR_REQUESTOR_ID && purDao.IsAssigned(pur, userID, role))
                                        allow = true;
                                }
                                break;
                        }

                        break;
                    case Constants.FlowType.FLOW_PURCHASE_REQUEST_US:
                        PurchaseRequestDao purUSDao = new PurchaseRequestDao();
                        PurchaseRequest purUS = purUSDao.GetByID(int.Parse(id));
                        switch (action)
                        {

                            case Constants.ActionType.List:
                                if (purUS.InvolveRole.Contains(srole))
                                    allow = true;
                                break;
                            case Constants.ActionType.Update:
                                string loginName = new UserAdminDao().GetById(userID).UserName;
                                if (purUS.WFStatusID == Constants.STATUS_OPEN)
                                {
                                    if (purUSDao.HasEditPermisionUS(purUS.ID, loginName, role))
                                        allow = true;
                                    else if (role != Constants.PR_REQUESTOR_ID_US && purUSDao.IsAssigned(purUS, userID, role))
                                        allow = true;
                                }
                                break;
                        }

                        break;
                    case Constants.FlowType.FLOW_PERFORMANCE_REVIEW:
                        PerformanceReviewDao perDao = new PerformanceReviewDao();
                        PerformanceReview per = perDao.GetById(id);
                        CommonDao comm = new CommonDao();
                        int userAdminId = GetUserAdminIdFromEmpId(userID);
                        List<int> roles = comm.GetRoleListInt(userAdminId, Constants.WORK_FLOW_PERFORMANCE_REVIEW);
                        switch (action)
                        {

                            case Constants.ActionType.List:
                                if (isPortal)
                                {
                                    if (roles.Contains(role))
                                        allow = true;
                                }
                                else if (per.InvolveRole.Contains(srole))
                                    allow = true;

                                break;
                            case Constants.ActionType.Update:
                                if (isPortal)
                                {
                                    if (roles.Contains(role) && per.AssignID.Equals(userID.ToString()) && per.AssignRole.Equals(role))
                                        allow = true;
                                }
                                else if (roles.Contains(per.AssignRole) && per.AssignID.Equals(userID.ToString()))
                                    allow = true;

                                break;
                        }

                        break;
                }
            }
            catch
            { }
            return allow;
        }

        public static int GetUserAdminIdFromEmpId(int Id)
        {
            string username = GetUserNameLoginByEmpID(Id.ToString());
            UserAdmin user = new UserAdminDao().GetByUserName(username);
            if (user != null)
                return user.UserAdminId;
            else return -1;
        }

        public static string RemoveExtraSpace(string userName)
        {
            if (string.IsNullOrEmpty(userName))
                return string.Empty;
            return Regex.Replace(userName, @"\s+", " ");
        }

        /// <summary>
        /// Parse string of username to list
        /// </summary>
        /// <param name="input">Must be in type: "username1; username2; username3; "</param>
        /// <returns></returns>
        public static List<string> GetListOfUserName(string input)
        {
            input = RemoveExtraSpace(input);
            List<string> list_userName = input.Split(
                new string[] { Constants.SEPARATE_USER_ADMIN_USERNAME, Constants.SEPARATE_USER_ADMIN_USERNAME.Trim() },
                StringSplitOptions.RemoveEmptyEntries).Distinct().ToList();
            return list_userName;
        }

        public static string GetHelpData(string id)
        {
            string result = string.Empty;
            string filename = HttpContext.Current.Server.MapPath(Constants.HELP_PATH) + id;
            if (System.IO.File.Exists(filename))
            {
                //open the file
                TextReader tr = new StreamReader(filename);
                // write a line of text to the file
                result = tr.ReadToEnd();
                // close the stream
                tr.Close();
            }

            return result;
        }

        public static string GenerateLocationCode(int seatCodeID)
        {
            if (seatCodeID <= 0)
                return null;
            string sLocationCode = "";
            SeatCode seatCode = locationDao.GetSeatCodeByID(seatCodeID, true, false);
            int[] iCode = new int[] { Constants.LOCATION_CODE_BRANCH_INDEX,
                Constants.LOCATION_CODE_FLOOR_INDEX, 
                Constants.LOCATION_CODE_OFFICE_INDEX,
                Constants.LOCATION_CODE_SEATCODE_INDEX }.OrderBy(p => p).ToArray();
            for (int i = 0; i < iCode.Length; i++)
            {
                switch (iCode[i])
                {
                    case Constants.LOCATION_CODE_SEATCODE_INDEX:
                        sLocationCode += Constants.LOCATION_CODE_SEATCODE_PREFIX + seatCode.ID;
                        break;
                    case Constants.LOCATION_CODE_OFFICE_INDEX:
                        sLocationCode += Constants.LOCATION_CODE_OFFICE_PREFIX + seatCode.Floor.OfficeID;
                        break;
                    case Constants.LOCATION_CODE_BRANCH_INDEX:
                        sLocationCode += Constants.LOCATION_CODE_BRANCH_PREFIX + seatCode.Floor.Office.BranchID;
                        break;
                    case Constants.LOCATION_CODE_FLOOR_INDEX:
                        sLocationCode += Constants.LOCATION_CODE_FLOOR_PREFIX + seatCode.FloorID;
                        break;
                }
            }
            return sLocationCode;
        }

        /// <summary>
        /// Get the work location text to show on the Work Location field
        /// </summary>
        /// <param name="seatCodeID"></param>
        /// <returns></returns>
        public static string GetWorkLocationText(int seatCodeID)
        {
            SeatCode seatCode = locationDao.GetSeatCodeByID(seatCodeID, true, false);
            return seatCode == null ? string.Empty : seatCode.Name + " - " + seatCode.Floor.Name;
        }

        public static string GenerateLocationCode(string branchID, string officeID, string floorID, string seatCodeID)
        {
            string result = string.Empty;
            int branch = ConvertUtil.ConvertToInt(branchID);
            int office = ConvertUtil.ConvertToInt(officeID);
            int floor = ConvertUtil.ConvertToInt(floorID);
            int seatCode = ConvertUtil.ConvertToInt(seatCodeID);
            if (branch > 0)
            {
                result += Constants.LOCATION_CODE_BRANCH_PREFIX + branch.ToString();
            }
            if (office > 0)
            {
                result += Constants.LOCATION_CODE_OFFICE_PREFIX + office.ToString();
            }
            if (floor > 0)
            {
                result += Constants.LOCATION_CODE_FLOOR_PREFIX + floor.ToString();
            }
            if (seatCode > 0)
            {
                result += Constants.LOCATION_CODE_SEATCODE_PREFIX + seatCodeID.ToString();
            }
            return result;
        }

        public static string GenerateStringOfLocation(string locationCode)
        {
            string result = string.Empty;
            if (!string.IsNullOrEmpty(locationCode))
            {
                Office objOffice = locationDao.GetOfficeByOfficeID(int.Parse(GetLocation(locationCode, LocationType.Office)));
                Branch objBranch = locationDao.GetBranchByID(int.Parse(GetLocation(locationCode, LocationType.Branch)));
                Floor objFloor = locationDao.GetFloorByID(int.Parse(GetLocation(locationCode, LocationType.Floor)));
                SeatCode objSeatCode = locationDao.GetSeatCodeByID(int.Parse(GetLocation(locationCode, LocationType.SeatCode)));
                result = (objSeatCode != null ? objSeatCode.Name : string.Empty) + " - " + (objFloor != null ? objFloor.Name : string.Empty) + " - "
                  + (objOffice != null ? objOffice.Name : string.Empty) + " - " + (objBranch != null ? objBranch.Name : string.Empty);
            }
            return result;
        }

        /// <summary>
        /// Get SeatCodeID/FloorID/OfficeID/BranchID from LocationCode
        /// </summary>
        /// <param name="locationCode"></param>
        /// <returns></returns>
        public static string GetLocation(string locationCode, LocationType type)
        {
            if (string.IsNullOrEmpty(locationCode))
                return string.Empty;
            string pattern = @"^\d+";
            int index = 0;
            switch (type)
            {
                case LocationType.Branch:
                    if (!locationCode.Contains(Constants.LOCATION_CODE_BRANCH_PREFIX))
                        return string.Empty;
                    index = locationCode.IndexOf(Constants.LOCATION_CODE_BRANCH_PREFIX) +
                        Constants.LOCATION_CODE_BRANCH_PREFIX.Length;
                    break;
                case LocationType.Floor:
                    if (!locationCode.Contains(Constants.LOCATION_CODE_FLOOR_PREFIX))
                        return string.Empty;
                    index = locationCode.IndexOf(Constants.LOCATION_CODE_FLOOR_PREFIX) +
                        Constants.LOCATION_CODE_FLOOR_PREFIX.Length;
                    break;
                case LocationType.Office:
                    if (!locationCode.Contains(Constants.LOCATION_CODE_OFFICE_PREFIX))
                        return string.Empty;
                    index = locationCode.IndexOf(Constants.LOCATION_CODE_OFFICE_PREFIX) +
                        Constants.LOCATION_CODE_OFFICE_PREFIX.Length;
                    break;
                case LocationType.SeatCode:
                    if (!locationCode.Contains(Constants.LOCATION_CODE_SEATCODE_PREFIX))
                        return string.Empty;
                    index = locationCode.IndexOf(Constants.LOCATION_CODE_SEATCODE_PREFIX) +
                        Constants.LOCATION_CODE_SEATCODE_PREFIX.Length;
                    break;
            }
            Match match = Regex.Match(locationCode.Substring(index), pattern);
            return match.Success ? match.Value : string.Empty;
        }

        public static string GetLocation(string locationCode, LocationType type, bool getName)
        {
            string sCodeID = GetLocation(locationCode, type);
            if (getName)
            {
                sCodeID = GetLocation(locationCode, LocationType.SeatCode);
                SeatCode seatCodeObj = locationDao.GetSeatCodeByID(ConvertUtil.ConvertToInt(sCodeID), true, false);
                if (seatCodeObj == null)
                    return string.Empty;
                switch (type)
                {
                    case LocationType.Branch:
                        return seatCodeObj.Floor.Office.Branch.Name;
                    case LocationType.Office:
                        return seatCodeObj.Floor.Office.Name;
                    case LocationType.Floor:
                        return seatCodeObj.Floor.Name;
                    case LocationType.SeatCode:
                        return seatCodeObj.Name;
                    default:
                        return string.Empty;
                }
            }
            else
                return sCodeID;
        }

        /// <summary>
        /// Check if the location code is valid
        /// </summary>
        /// <param name="locationCode"></param>
        /// <returns></returns>
        public static bool IsLocationCodeValid(string locationCode)
        {
            //location code can be null or empty
            if (string.IsNullOrEmpty(locationCode))
                return true;
            string sSeatCode = GetLocation(locationCode, LocationType.SeatCode);
            string sFloor = GetLocation(locationCode, LocationType.Floor);
            string sOffice = GetLocation(locationCode, LocationType.Office);
            string sBranch = GetLocation(locationCode, LocationType.Branch);

            //Location code must include SeatCode, Floor, Office, Branch and these values must be integer
            if (string.IsNullOrEmpty(sSeatCode) || !CheckUtil.IsInteger(sSeatCode) ||
                string.IsNullOrEmpty(sFloor) || !CheckUtil.IsInteger(sFloor) ||
                string.IsNullOrEmpty(sOffice) || !CheckUtil.IsInteger(sOffice) ||
                string.IsNullOrEmpty(sBranch) || !CheckUtil.IsInteger(sBranch))
                return false;

            SeatCode seatCode = locationDao.GetSeatCodeByID(int.Parse(sSeatCode), true, false);
            //Seat code must exist in database
            if (seatCode == null || seatCode.FloorID != int.Parse(sFloor) ||
                seatCode.Floor.OfficeID != int.Parse(sOffice) ||
                seatCode.Floor.Office.BranchID != int.Parse(sBranch))
                return false;
            return true;
        }

        public static bool ContainsExact(string locationCode, string subCode)
        {
            string pattern = @"(^|\d)" + subCode + @"($|\D)";
            return Regex.IsMatch(locationCode, pattern);
        }

        public static List<int> GetAccessibleModules(int userAdminID)
        {
            ModulePermissonDao gpDao = new ModulePermissonDao();
            return gpDao.GetAccessibleModules(userAdminID);
        }



        #region Menu
        /*
        private class MenuItem
        {
            public MenuItem(string text, string url, string imgUrl, int moduleID, List<MenuItem> childs)
            {
                _url = url;
                _imgUrl = imgUrl;
                _text = text;
                _moduleID = moduleID;
                _childs = childs;
            }
            private string _url;

            protected string Url
            {
                get { return _url; }
                set { _url = value; }
            }
            private string _imgUrl;

            protected string ImgUrl
            {
                get { return _imgUrl; }
                set { _imgUrl = value; }
            }
            private string _text;

            protected string Text
            {
                get { return _text; }
                set { _text = value; }
            }
            private int _moduleID;

            protected int ModuleID
            {
                get { return _moduleID; }
                set { _moduleID = value; }
            }
            private List<MenuItem> _childs;

            protected List<MenuItem> Childs
            {
                get { return _childs; }
                set { _childs = value; }
            }
        }
        private static List<MenuItem> listMenuItems = new List<MenuItem>() { 
            new MenuItem("Home", "/Home/Index", null, 0, null),
            new MenuItem("Management", "javascript:void(0);", null, 0, new List<MenuItem>(){
                new MenuItem("Employee", "javascript:void(0);", "/Content/Images/Icons/proj_projection.png", 0, new List<MenuItem>(){
                    new MenuItem("Active List","/Employee","/Content/Images/Icons/proj_projection.png", (int)Modules.Employee,null),
                    new MenuItem("Resigned List","/Employee/EmployeeResignList","/Content/Images/Icons/scissors__arrow.png", (int)Modules.Employee,null),
                    new MenuItem("STT List","/STT","/Content/Images/Icons/proj_projection.png", (int)Modules.STT,null)
                }),
                new MenuItem("Hiring Center", "javascript:void(0);", "/Content/Images/Icons/book_open.png", 0, new List<MenuItem>(){
                    new MenuItem("Candidate Profiles","/Candidate","/Content/Images/Icons/blogs.png", (int)Modules.Candidate,null),
                    new MenuItem("Interview List","/Interview","/Content/Images/Icons/scissors__arrow.png", (int)Modules.Hiring,null),
                    new MenuItem("Interview History","/Interview/HistoryInterview","/Content/Images/Icons/bin.png", (int)Modules.Hiring,null)
                }),
                new MenuItem("PTO", "javascript:void(0);", "/Content/Images/Icons/folder.png", 0, new List<MenuItem>(){
                    new MenuItem("Annual Holiday","/AnnualHoliday","/Content/Images/Icons/star.png", (int)Modules.AnnualHoliday,null),
                    new MenuItem("PTO Admin","/PTOAdmin","/Content/Images/Icons/bookmark_book_open.png", (int)Modules.PTO_Admin,null),
                    new MenuItem("PTO Report","/PTOReport","/Content/Images/Icons/book_open_previous.png", (int)Modules.PTO_Report,null),
                    new MenuItem("PTO Reminder","/PTOAdmin/PtoToConfirm","/Content/Images/Icons/alarm_clock.png", (int)Modules.PTO_Admin,null)
                }),
                new MenuItem("Online Test", "javascript:void(0);", "/Content/Images/Icons/address_book.png", 0, new List<MenuItem>(){
                    new MenuItem("Exam List","/Exam","/Content/Images/Icons/blogs.png", (int)Modules.Exam,null),
                    new MenuItem("Exam Question","/ExamQuestion","/Content/Images/Icons/books.png", (int)Modules.ExamQuestion,null),
                    new MenuItem("Question List","/Question","/Content/Images/Icons/books_stack.png", (int)Modules.Question,null)
                }),
                new MenuItem("Performance Review", "javascript:void(0);", "/Content/Images/Icons/group.png", 0, new List<MenuItem>(){
                    new MenuItem("PR List","/PerformanceReviewHr","/Content/Images/Icons/edit.png", 0,null)
                })
            }),
            new MenuItem("Requests", "javascript:void(0);", null, 0, new List<MenuItem>(){
                new MenuItem("Job Request","/JobRequest","/Content/Images/Icons/proj_projection.png", (int)Modules.JobRequest,null),
                new MenuItem("Purchase Request","/PurchaseRequest","/Content/Images/Icons/television__plus.png", (int)Modules.PurchaseRequest,null)
            }),
            new MenuItem("System", "javascript:void(0);", null, 0, new List<MenuItem>(){
                new MenuItem("Master Data", "javascript:void(0);", "/Content/Images/Icons/proj_projection.png", 0, new List<MenuItem>(){
                    new MenuItem("Job Title","/Employee","/Content/Images/Icons/proj_projection.png", (int)Modules.Employee,null),
                    new MenuItem("Job Title Level","/Employee/EmployeeResignList","/Content/Images/Icons/scissors__arrow.png", (int)Modules.Employee,null)
                }),
                new MenuItem("Admin Accounts", "javascript:void(0);", "/Content/Images/Icons/book_open.png", 0, new List<MenuItem>(){
                    new MenuItem("Group","/Candidate","/Content/Images/Icons/blogs.png", (int)Modules.Candidate,null),
                    new MenuItem("Group Permisson","/Interview","/Content/Images/Icons/scissors__arrow.png", (int)Modules.Hiring,null),
                    new MenuItem("Account","/Interview/HistoryInterview","/Content/Images/Icons/bin.png", (int)Modules.Hiring,null)
                }),
                new MenuItem("Workflow", "javascript:void(0);", "/Content/Images/Icons/folder.png", 0, new List<MenuItem>(){
                    new MenuItem("Workflow Accounts","/AnnualHoliday","/Content/Images/Icons/star.png", (int)Modules.AnnualHoliday,null),
                    new MenuItem("WF Role Resolution","/PTOAdmin","/Content/Images/Icons/bookmark_book_open.png", (int)Modules.PTO_Admin,null)
                }),
                new MenuItem("System Logs", "javascript:void(0);", "/Content/Images/Icons/address_book.png", 0, new List<MenuItem>(){
                    new MenuItem("Data Logs","/Exam","/Content/Images/Icons/blogs.png", (int)Modules.Exam,null),
                    new MenuItem("Admin Access Logs","/ExamQuestion","/Content/Images/Icons/books.png", (int)Modules.ExamQuestion,null),
                    new MenuItem("User Access Logs","/Question","/Content/Images/Icons/books_stack.png", (int)Modules.Question,null)
                })
            })
        };
        public static void DisplayMenu()
        {

        }
        */
        #endregion

        /// <summary>
        /// Get the max length of a nchar/nvarchar field
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="field"></param>
        /// <returns></returns>
        public static int GetLengthLimit(object obj, string field)
        {
            int dblenint = 0;   // default value = we can't determine the length

            Type type = obj.GetType();
            PropertyInfo prop = type.GetProperty(field);
            // Find the Linq 'Column' attribute
            // e.g. [Column(Storage="_FileName", DbType="NChar(256) NOT NULL", CanBeNull=false)]
            object[] info = prop.GetCustomAttributes(typeof(ColumnAttribute), true);
            // Assume there is just one
            if (info.Length == 1)
            {
                ColumnAttribute ca = (ColumnAttribute)info[0];
                string dbtype = ca.DbType;

                if (dbtype.StartsWith("NChar") || dbtype.StartsWith("NVarChar"))
                {
                    int index1 = dbtype.IndexOf("(");
                    int index2 = dbtype.IndexOf(")");
                    string dblen = dbtype.Substring(index1 + 1, index2 - index1 - 1);
                    int.TryParse(dblen, out dblenint);
                }
            }
            return dblenint;
        }

        public static int[] ResizeImage(int oldWidth, int oldHeight, int fixWidth, int fixHeight)
        {
            //double ratioW = oldHeight * 1.0 / oldWidth * 1.0;
            //double ratioH = oldWidth * 1.0 / oldHeight * 1.0;
            double distance = (oldWidth - oldHeight);
            int width = 0;
            int height = 0;

            if (distance != 0)
            {
                if (distance < 0)
                {
                    distance *= -1;
                }
                double ratioW = oldWidth * 1.0 / distance;
                double ratioH = oldHeight * 1.0 / distance;
                //ratio = W:H
                /*Formula
                 * 
                 * Height = fixWidth * ratioH / ratioW
                 * Width = fixHeight * ratioW / ratioH
                 * 
                 */

                if (oldWidth > fixWidth)
                {
                    width = fixWidth;
                }
                else
                {
                    width = oldWidth;
                }

                height = Convert.ToInt32(width * (ratioH / ratioW));

                if (height > fixHeight)
                {
                    height = fixHeight;
                    width = Convert.ToInt32(height * (ratioW / ratioH));
                }
            }
            else
            {
                width = fixWidth;
                height = fixHeight;
            }
            int[] arrWH = { width, height };
            return arrWH;
        }

        public static string FixImage(string path, int fixWidth, int fixHeight)
        {
            try
            {
                path = HttpContext.Current.Server.MapPath("~" + path);
                if (File.Exists(path))
                {
                    System.Drawing.Image img = System.Drawing.Image.FromFile(path);
                    int[] arr = ResizeImage(img.Width, img.Height, fixWidth, fixHeight);

                    return arr[0].ToString() + ":" + arr[1].ToString();
                }
                else
                    return string.Empty;
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }

        public static string ImageThumbnail(string path, int fixWidth, int fixHeight)
        {
            string result = "<img src='/Content/Images/Common/nopic.gif' height='120px' width='120px' />";
            try
            {
                string filename = HttpContext.Current.Server.MapPath("~" + path);
                if (File.Exists(filename))
                {
                    System.Drawing.Image img = System.Drawing.Image.FromFile(filename);
                    int[] arr = ResizeImage(img.Width, img.Height, fixWidth, fixHeight);

                    result = string.Format("<img src='{0}' width='{1}' height='{2}' />", path, arr[0], arr[1]);
                }
                return result;
            }
            catch (Exception)
            {
                return result;
            }
        }

        public static void RemoveAllFile(string folderPath, string regex)
        {
            DirectoryInfo di = new DirectoryInfo(HttpContext.Current.Server.MapPath(folderPath));
            foreach (var fFile in di.GetFiles(regex))
                fFile.Delete();
        }

        public static int GetWorkingHours(DateTime fromDate, DateTime toDate, int hoursPerday = 8)
        {
            int count = 0;
            for (DateTime i = fromDate; i <= toDate; i=i.AddDays(1))
            {
                if (i.DayOfWeek != DayOfWeek.Saturday && i.DayOfWeek != DayOfWeek.Sunday)
                    count += hoursPerday;
            }
            return count;
        }
        public static int GetWorkingHours(int hoursFrom, int hourTo)
        {
            return hoursFrom <= 12 && hourTo >= 13 ? hourTo - hoursFrom - 1 : hourTo - hoursFrom;
        }

        public static String GetCurrentMenu(string orginalLink)
        {
            string result = string.Empty;
            string[] array = orginalLink.Trim('/').Split('/');

            for (int i = 0; i < array.Count(); i++)
            {
                CRM.Models.Menu objMenu = new MenuDao().GetByLink(orginalLink);
                if (objMenu == null)
                {
                    int lastCharIndex = orginalLink.LastIndexOf('/');
                    orginalLink = orginalLink.Remove(lastCharIndex, orginalLink.Count() - lastCharIndex);
                }
                else
                {
                    string link = string.Empty;
                    result = RecusionLinkMenu(objMenu, link);
                    break;
                }
            }
            return result;
        }

        public static String GetCurrentMenu(string orginalLink,bool isIndex)
        {
            string result = string.Empty;
            string[] array = orginalLink.Trim('/').Split('/');

            for (int i = 0; i < array.Count(); i++)
            {
                CRM.Models.Menu objMenu = new MenuDao().GetByLink(orginalLink);
                if (objMenu == null)
                {
                    int lastCharIndex = orginalLink.LastIndexOf('/');
                    orginalLink = orginalLink.Remove(lastCharIndex, orginalLink.Count() - lastCharIndex);
                }
                else
                {
                    if (isIndex)
                        objMenu.Link = string.Empty;
                    string link = string.Empty;
                    result = RecusionLinkMenu(objMenu, link);
                    if (isIndex)
                    {
                        result = result.Trim().TrimEnd('»');
                    }
                    break;
                }
            }
            return result;
        }

        private static string RecusionLinkMenu(CRM.Models.Menu obj, string link)
        {
            string result = string.Empty;
            if (obj.ParentId.HasValue)
            {
                CRM.Models.Menu objParent = new MenuDao().GetByID(obj.ParentId.Value, true);
                link = RecusionLinkMenu(objParent, link);
                link += FormatLink(obj);
            }
            else
            {
                link = link.Insert(0, FormatLink(obj));
            }
            return link;
        }

        private static string FormatLink(CRM.Models.Menu obj)
        {
            string result = string.Empty;
            if (!string.IsNullOrEmpty(obj.Link))
            {
                result = "<a href='" + obj.Link + "'>" + obj.Name + "</a> » ";
            }
            else
            {
                result = obj.Name + " » ";
            }
            return result;
        }

        /// <summary>
        /// Get hours list
        /// </summary>
        /// <param name="hoursFrom">0->24</param>
        /// <param name="hoursTo">0->24</param>
        /// <param name="stepLength">1->60 (minutes)</param>
        /// <returns></returns>
        public static List<ListItem> GetHoursList(int hoursFrom, int hoursTo, int stepLength, string timeFormat)
        {
            if (hoursTo < hoursFrom)
                return null;
            List<ListItem> hoursList = new List<ListItem>();
            var dNow = DateTime.Now;
            //DateTime from = new DateTime(dNow.Year, dNow.Month, 
            //    hoursFrom >= 24 ? dNow.Day + 1 : dNow.Day, hoursFrom >= 24 ? 0 : hoursFrom ,0,0);
            DateTime dfrom = dNow.AddHours(hoursFrom - dNow.Hour).AddMinutes(-dNow.Minute);
            DateTime dto = dfrom.AddHours(hoursTo-hoursFrom);
            //DateTime to = new DateTime(dNow.Year, dNow.Month, 
            //    hoursTo >= 24 ? dNow.Day + 1 : dNow.Day, hoursTo >= 24 ? 0 : hoursTo ,0,0);
            int count = 0;
            while(true)
            {
                hoursList.Add(new ListItem(dfrom.ToString(timeFormat)));
                dfrom = dfrom.AddMinutes(stepLength);
                count++;
                if (dfrom > dto || count > 100)
                    break;
            }
            return hoursList;
        }
        public static List<ListItem> GetHoursList(int hoursFrom, int hoursTo)
        {
            if (hoursTo < hoursFrom)
                return null;
            List<ListItem> hoursList = new List<ListItem>();
            int numHours = (hoursTo - hoursFrom) + 1;
            for (int i = hoursFrom; i <= hoursTo; i++)
            {
                if (i < 10)
                    hoursList.Add(new ListItem("0" + i));
                else
                    hoursList.Add(new ListItem(i.ToString()));
            }
            return hoursList;
        }
        public static List<ListItem> GetMinutesList()
        {
            List<ListItem> minutesList = new List<ListItem>();
            for (int i = 0; i < 60; i++)
            {
                if (i < 10)
                    minutesList.Add(new ListItem("0" + i));
                else
                    minutesList.Add(new ListItem(i.ToString()));
            }

            return minutesList;
        }
        public static string FormatTime(double time)
        {
            
            int hours =  (int)time / 60;
            int minutes = (int)time - (hours * 60);
            return hours.ToString() + ":" + minutes.ToString();
        }

        #region Send Service Request E-mail
        /// <summary>
        /// Send Purchase Request Mail
        /// </summary>
        /// <param name="prId">service request id</param>
        public static void SendSRMail(int srId,int type)
        {
            // Get body detail
            SR_ServiceRequest obj = new ServiceRequestDao().GetById(srId);

            //WFRole role = null;
            if (obj == null)
            {
                return;
            }
            //else
            //{
            //    role = roleDao.GetByID(purReq.AssignRole.Value);
            //}

            string from_email = ConfigurationManager.AppSettings["from_email"];
            string host = ConfigurationManager.AppSettings["mailserver_host"];
            string port = ConfigurationManager.AppSettings["mailserver_port"];
            string poster = "Service Request";
            string subject = string.Empty;
            switch (type)
            {
                case Constants.SR_SEND_MAIL_COMMENT:
                    subject = "[CRM-SR] " + Constants.SR_SERVICE_REQUEST_PREFIX + obj.ID + " has been modified";
                    break;
                default:
                    if (obj.StatusID == Constants.SR_STATUS_CLOSED)
                    {
                        subject = "[CRM-SR] " + Constants.SR_SERVICE_REQUEST_PREFIX + obj.ID + " has been closed";
                    }
                    else
                    {
                        subject = "[CRM-SR] " + Constants.SR_SERVICE_REQUEST_PREFIX + obj.ID + " has been forwarded to " + obj.AssignUser;
                    }
                    break;
            }
            string body = CreateBodyOfEmail(obj);
           // string requestorBody = CreateRequestorBodyOfEmail(obj);

            string to_email = string.Empty;
            string cc_email = string.Empty;
            string[] arrIds = new string[]{};
            arrIds = obj.InvolveUser.Split(Constants.SEPARATE_INVOLVE_CHAR);
            List<string> sendList = new List<string>();
            List<string> duplicateEmail = new List<string>();

            for (int i = 0; i < arrIds.Length - 1; i++)
            {
                //check duplicate person on user name
                if (!sendList.Contains(arrIds[i]))
                {
                    sendList.Add(arrIds[i]);
                    if (obj.StatusID == Constants.SR_STATUS_CLOSED) //If an email has "Close" status, just only send by "To" section, not cc 
                    {
                        if (!duplicateEmail.Contains(arrIds[i] + Constants.LOGIGEAR_EMAIL_DOMAIN + ";"))
                        {
                            // only send survey email to SubmitUser
                            if (i == 0)
                            {
                                string surveyBody = CreateSurveyBodyEmail(obj);
                                string survey_email = arrIds[i] + Constants.LOGIGEAR_EMAIL_DOMAIN + ";";
                                WebUtils.SendMail(host, port, from_email, poster, survey_email, cc_email, subject, surveyBody);
                            }
                            duplicateEmail.Add(arrIds[i] + Constants.LOGIGEAR_EMAIL_DOMAIN + ";");
                        }
                    }
                    else
                    {
                        //Just send by "To" section only to person who has been assigned, the involved others are by "CC" section.
                        if (string.IsNullOrEmpty(to_email))
                        {
                            to_email = obj.AssignUser + Constants.LOGIGEAR_EMAIL_DOMAIN;
                            cc_email = arrIds[i] + Constants.LOGIGEAR_EMAIL_DOMAIN + ";";                            
                            duplicateEmail.Add(arrIds[i] + Constants.LOGIGEAR_EMAIL_DOMAIN + ";");
                            duplicateEmail.Add(obj.AssignUser + Constants.LOGIGEAR_EMAIL_DOMAIN + ";");

                            cc_email = obj.RequestUser + Constants.LOGIGEAR_EMAIL_DOMAIN + ";";
                            duplicateEmail.Add(obj.RequestUser + Constants.LOGIGEAR_EMAIL_DOMAIN + ";");
                        }
                        else //make a cc list mail send.
                        {
                            if (!duplicateEmail.Contains(arrIds[i] + Constants.LOGIGEAR_EMAIL_DOMAIN + ";"))
                            {
                                duplicateEmail.Add(arrIds[i] + Constants.LOGIGEAR_EMAIL_DOMAIN + ";");
                                cc_email += arrIds[i] + Constants.LOGIGEAR_EMAIL_DOMAIN + ";";
                            }
                        }
                    }
                }
            }
            if (!string.IsNullOrEmpty(obj.CCList))
            {
                string[] array = obj.CCList.Split(';');
                foreach (string item in array)
                {
                    if (!string.IsNullOrEmpty(item))
                    {
                        string userAdminID = item;
                        if (!duplicateEmail.Contains(item + Constants.LOGIGEAR_EMAIL_DOMAIN + ";"))
                        {
                            duplicateEmail.Add(item + Constants.LOGIGEAR_EMAIL_DOMAIN + ";");
                            cc_email += item + Constants.LOGIGEAR_EMAIL_DOMAIN + ";";
                        }
                    }
                }
            }
            
            if (!string.IsNullOrEmpty(to_email))
            {
                WebUtils.SendMail(host, port, from_email, poster, to_email, cc_email, subject, body);
                //WebUtils.SendMail(host, port, from_email, poster, to_email, cc_email, bcc_email, subject, body);
            }
        }
        /// <summary>
        /// Send SR_Request to only Requestor
        /// </summary>
        /// <param name="srId"></param>
        /// <param name="type"></param>
        public static void SendSRMailFromPortal(int srId, int type)
        {
            // Get body detail
            SR_ServiceRequest obj = new ServiceRequestDao().GetById(srId);

            //WFRole role = null;
            if (obj == null)
            {
                return;
            }

            string from_email = ConfigurationManager.AppSettings["from_email"];
            string host = ConfigurationManager.AppSettings["mailserver_host"];
            string port = ConfigurationManager.AppSettings["mailserver_port"];
            string poster = "Service Request";
            string subject = string.Empty;
            switch (type)
            {
                case Constants.SR_SEND_MAIL_COMMENT:
                    subject = "[CRM-SR] " + Constants.SR_SERVICE_REQUEST_PREFIX + obj.ID + " has been modified";
                    break;
                default:
                    if (obj.StatusID == Constants.SR_STATUS_CLOSED)
                    {
                        subject = "[CRM-SR] " + Constants.SR_SERVICE_REQUEST_PREFIX + obj.ID + " has been closed";
                    }
                    else
                    {
                        subject = "[CRM-SR] " + Constants.SR_SERVICE_REQUEST_PREFIX + obj.ID + " has been forwarded to " + obj.AssignUser;
                    }
                    break;
            }
            string body = CreateBodyOfEmail(obj);
            // string requestorBody = CreateRequestorBodyOfEmail(obj);

            string to_email = string.Empty;
            string cc_email = string.Empty;
            string[] arrIds = new string[] { };
            arrIds = obj.InvolveUser.Split(Constants.SEPARATE_INVOLVE_CHAR);
            List<string> sendList = new List<string>();
            List<string> duplicateEmail = new List<string>();

            for (int i = 0; i < arrIds.Length - 1; i++)
            {
                //check duplicate person on user name
                if (!sendList.Contains(arrIds[i]))
                {
                    sendList.Add(arrIds[i]);
                    if (obj.StatusID == Constants.SR_STATUS_CLOSED) //If an email has "Close" status, just only send by "To" section, not cc 
                    {
                        if (!duplicateEmail.Contains(arrIds[i] + Constants.LOGIGEAR_EMAIL_DOMAIN + ";"))
                        {
                            // only send survey email to SubmitUser
                            if (i == 0)
                            {
                                string surveyBody = CreateSurveyBodyEmail(obj);
                                string survey_email = arrIds[i] + Constants.LOGIGEAR_EMAIL_DOMAIN + ";";
                                WebUtils.SendMail(host, port, from_email, poster, survey_email, cc_email, subject, surveyBody);
                            }
                            else
                                to_email += arrIds[i] + Constants.LOGIGEAR_EMAIL_DOMAIN + ";";
                            duplicateEmail.Add(arrIds[i] + Constants.LOGIGEAR_EMAIL_DOMAIN + ";");
                        }
                    }
                    else
                    {
                        //Just send by "To" section only to person who has been assigned, the involved others are by "CC" section.
                        if (string.IsNullOrEmpty(to_email))
                        {
                            to_email = obj.AssignUser + Constants.LOGIGEAR_EMAIL_DOMAIN + ";";
                            cc_email = arrIds[i] + Constants.LOGIGEAR_EMAIL_DOMAIN + ";";
                            duplicateEmail.Add(arrIds[i] + Constants.LOGIGEAR_EMAIL_DOMAIN + ";");
                            duplicateEmail.Add(obj.AssignUser + Constants.LOGIGEAR_EMAIL_DOMAIN + ";");

                            cc_email = obj.RequestUser + Constants.LOGIGEAR_EMAIL_DOMAIN + ";";
                            duplicateEmail.Add(obj.RequestUser + Constants.LOGIGEAR_EMAIL_DOMAIN + ";");
                        }
                        else //make a cc list mail send.
                        {
                            if (!duplicateEmail.Contains(arrIds[i] + Constants.LOGIGEAR_EMAIL_DOMAIN + ";"))
                            {
                                duplicateEmail.Add(arrIds[i] + Constants.LOGIGEAR_EMAIL_DOMAIN + ";");
                                cc_email += arrIds[i] + Constants.LOGIGEAR_EMAIL_DOMAIN + ";";
                            }
                        }
                    }
                }
            }
            if (!string.IsNullOrEmpty(obj.CCList))
            {
                string[] array = obj.CCList.Split(';');
                foreach (string item in array)
                {
                    if (!string.IsNullOrEmpty(item))
                    {
                        string userAdminID = item;
                        if (!duplicateEmail.Contains(item + Constants.LOGIGEAR_EMAIL_DOMAIN + ";"))
                        {
                            duplicateEmail.Add(item + Constants.LOGIGEAR_EMAIL_DOMAIN + ";");
                            cc_email += item + Constants.LOGIGEAR_EMAIL_DOMAIN + ";";
                        }
                    }
                }
            }
            #region Get default email from routing page

            ServiceRequestDao srDao = new ServiceRequestDao();
            EmployeeDao emplDao = new EmployeeDao();
            SRSettingDao srSettingDao = new SRSettingDao();
            SR_Setting srSetting = new SR_Setting();
            string bcc_email = String.Empty;
            string[] aTo = new string[] { };
            string[] aCc = new string[] { };
            string[] aBcc = new string[] { };
            string locationCode = (emplDao.GetByOfficeEmail((obj.RequestUser + Constants.LOGIGEAR_EMAIL_DOMAIN).Trim()) as Employee).LocationCode;
            if (!String.IsNullOrEmpty(locationCode))
            {
                int officeId = ConvertUtil.ConvertToInt(CommonFunc.GetLocation(locationCode, LocationType.Office));
                string proName = (emplDao.GetByOfficeEmail(obj.RequestUser + Constants.LOGIGEAR_EMAIL_DOMAIN) as Employee).Project;
                if (String.IsNullOrEmpty(proName))
                {
                    srSetting = srSettingDao.GetByProjectAndOffice(String.Empty, officeId);
                }
                else
                {
                    srSetting = srSettingDao.GetByProjectAndOffice(proName, officeId);
                }
                if (!string.IsNullOrEmpty(srSetting.EmailTo))
                {
                    aTo = srSetting.EmailTo.Split(Constants.SEPARATE_CC_LIST);
                    for (int i = 0; i < aTo.Count(); i++)
                    {
                        if (!duplicateEmail.Contains(aTo[i] + Constants.LOGIGEAR_EMAIL_DOMAIN + ";") && !String.IsNullOrEmpty(aTo[i]))
                        {
                            duplicateEmail.Add(aTo[i] + Constants.LOGIGEAR_EMAIL_DOMAIN + ";");
                            to_email += aTo[i] + Constants.LOGIGEAR_EMAIL_DOMAIN + ";";
                        }
                    }
                }
                if(!string.IsNullOrEmpty(srSetting.EmailCc))
                {
                    aCc = srSetting.EmailCc.Split(Constants.SEPARATE_CC_LIST);
                    for (int i = 0; i < aCc.Count(); i++)
                    {
                        if (!duplicateEmail.Contains(aCc[i] + Constants.LOGIGEAR_EMAIL_DOMAIN + ";") && !String.IsNullOrEmpty(aCc[i]))
                        {
                            duplicateEmail.Add(aCc[i] + Constants.LOGIGEAR_EMAIL_DOMAIN + ";");
                            cc_email += aCc[i] + Constants.LOGIGEAR_EMAIL_DOMAIN + ";";
                        }
                    }
                }
                if(!string.IsNullOrEmpty(srSetting.EmailBcc))
                {
                    aBcc = srSetting.EmailBcc.Split(Constants.SEPARATE_CC_LIST);
                    for (int i = 0; i < aBcc.Count(); i++)
                    {
                        if (!duplicateEmail.Contains(aBcc[i] + Constants.LOGIGEAR_EMAIL_DOMAIN + ";") && !String.IsNullOrEmpty(aBcc[i]))
                        {
                            duplicateEmail.Add(aBcc[i] + Constants.LOGIGEAR_EMAIL_DOMAIN + ";");
                            bcc_email += aBcc[i] + Constants.LOGIGEAR_EMAIL_DOMAIN + ";";
                        }
                    }
                }

            }
            #endregion
            if (!string.IsNullOrEmpty(to_email))
            {
                //WebUtils.SendMail(host, port, from_email, poster, to_email, cc_email, subject, body);
                WebUtils.SendMail(host, port, from_email, poster, to_email, cc_email, bcc_email, subject, body);
            }            
        }

        /// <summary>
        /// Create body of email
        /// </summary>
        /// <param name="purReq">PurchaseRequest</param>
        /// <param name="role">WFRole</param>
        /// <returns>string</returns>
        private static string CreateBodyOfEmail(SR_ServiceRequest objSr)
        {
            string path = string.Empty;

            //load template emails by pr status   
            path = System.Web.HttpContext.Current.Server.MapPath("~/Views/ServiceRequestAdmin/TemplateMail.htm");
            string content = WebUtils.ReadFile(path);

            //replace the holders on template emails.
            if (objSr != null)
            {
                SR_Category objParent = new SRCategoryDao().GetCategoryParentBySub(objSr.CategoryID);
                // create message
                content = content.Replace(Constants.SR_REQUEST_ID_HOLDER, objSr.ID.ToString());
                content = content.Replace(Constants.SR_ASSIGN_TO_HOLDER, objSr.AssignUser);

                content = content.Replace(Constants.SR_CATEGORY_HOLDER, objParent != null ? objParent.Name : string.Empty);
                content = content.Replace(Constants.SR_SUB_CATEGORY_HOLDER, objSr.SR_Category.Name);
                content = content.Replace(Constants.SR_TITLE_HOLDER, objSr.Title);
                content = content.Replace(Constants.SR_DESCRIPTION_HOLDER, HttpUtility.HtmlEncode(objSr.Description).Replace("\r\n", "<br />"));
                content = content.Replace(Constants.SR_SUBMITER_HOLDER, objSr.SubmitUser);
                content = content.Replace(Constants.SR_REQUESTOR_HOLDER, objSr.RequestUser);
                content = content.Replace(Constants.SR_SUBMITDATE_HOLDER, objSr.CreateDate.ToString(Constants.DATETIME_FORMAT_SR));
                content = content.Replace(Constants.SR_DUEDATE_HOLDER, objSr.DueDate.HasValue ? objSr.DueDate.Value.ToString(Constants.DATETIME_FORMAT_SR) : string.Empty);
                content = content.Replace(Constants.SR_CCLIST_HOLDER, objSr.CCList);
                content = content.Replace(Constants.SR_URGENCY_HOLDER, objSr.SR_Urgency.Name);
                content = content.Replace(Constants.SR_PARENTID_HOLDER, objSr.ParentID.HasValue ? Constants.SR_SERVICE_REQUEST_PREFIX+ objSr.ParentID.Value.ToString() : string.Empty);
                content = content.Replace(Constants.SR_STATUS_HOLDER, objSr.SR_Status.Name);
                content = content.Replace(Constants.SR_ASSIGNEDTO_HOLDER, objSr.AssignUser);
                content = content.Replace(Constants.SR_HISTORY_HOLDER, GetSRHistory(objSr.ID));
                content = content.Replace(Constants.SR_COMMENTS_HOLDER, GetSRComments(objSr.ID));
                string linkEmployee = string.Empty;
                string linkIT = string.Empty;
                string linkSurvey = string.Empty;
                if (objSr.StatusID == Constants.SR_STATUS_CLOSED)
                {
                    content = content.Replace(Constants.SR_FORWARD_TO_HOLDER, " has been closed");
                    linkEmployee = "http://" + HttpContext.Current.Request["SERVER_NAME"] + ":" + HttpContext.Current.Request["SERVER_PORT"] + "/Portal/ServiceRequest/Detail/" + objSr.ID;
                    linkIT = "http://" + HttpContext.Current.Request["SERVER_NAME"] + ":" + HttpContext.Current.Request["SERVER_PORT"] + "/ServiceRequestAdmin/Detail/" + objSr.ID;
                }
                else
                {

                    content = content.Replace(Constants.SR_FORWARD_TO_HOLDER, " has been forwarded to you");
                    //content = content.Replace(Constants.PR_FORWARD_TO_NAME_HOLDER, "Forwarded To: " + new UserAdminDao().GetById((int)purReq.AssignID).UserName + " (" + purReq.WFRole.Name + " )");
                    linkEmployee = "http://" + HttpContext.Current.Request["SERVER_NAME"] + ":" + HttpContext.Current.Request["SERVER_PORT"] + "/Portal/ServiceRequest/Detail/" + objSr.ID;
                    linkIT = "http://" + HttpContext.Current.Request["SERVER_NAME"] + ":" + HttpContext.Current.Request["SERVER_PORT"] + "/ServiceRequestAdmin/Detail/" + objSr.ID;
                }

                content = content.Replace(Constants.SR_LINK_EMPLOYEE_HOLDER, linkEmployee);
                content = content.Replace(Constants.SR_LINK_IT_HOLDER, linkIT);
            }

            return content;
        }

        private static string CreateSurveyBodyEmail(SR_ServiceRequest objSr)
        {
            string path = string.Empty;

            //load template emails by pr status   
            path = System.Web.HttpContext.Current.Server.MapPath("~/Views/ServiceRequestAdmin/TemplateMail.htm");
            string content = WebUtils.ReadFile(path);

            //replace the holders on template emails.
            if (objSr != null)
            {
                SR_Category objParent = new SRCategoryDao().GetCategoryParentBySub(objSr.CategoryID);
                // create message
                content = content.Replace(Constants.SR_REQUEST_ID_HOLDER, objSr.ID.ToString());
                //content = content.Replace(Constants.SR_ASSIGN_TO_HOLDER, objSr.AssignUser);
                content = content.Replace(Constants.SR_ASSIGN_TO_HOLDER, objSr.SubmitUser);

                content = content.Replace(Constants.SR_CATEGORY_HOLDER, objParent != null ? objParent.Name : string.Empty);
                content = content.Replace(Constants.SR_SUB_CATEGORY_HOLDER, objSr.SR_Category.Name);
                content = content.Replace(Constants.SR_TITLE_HOLDER, objSr.Title);
                content = content.Replace(Constants.SR_DESCRIPTION_HOLDER, HttpUtility.HtmlEncode(objSr.Description).Replace("\r\n", "<br />"));
                content = content.Replace(Constants.SR_SUBMITER_HOLDER, objSr.SubmitUser);
                content = content.Replace(Constants.SR_REQUESTOR_HOLDER, objSr.RequestUser);
                content = content.Replace(Constants.SR_SUBMITDATE_HOLDER, objSr.CreateDate.ToString(Constants.DATETIME_FORMAT_SR));
                content = content.Replace(Constants.SR_DUEDATE_HOLDER, objSr.DueDate.HasValue ? objSr.DueDate.Value.ToString(Constants.DATETIME_FORMAT_SR) : string.Empty);
                content = content.Replace(Constants.SR_CCLIST_HOLDER, objSr.CCList);
                content = content.Replace(Constants.SR_URGENCY_HOLDER, objSr.SR_Urgency.Name);
                content = content.Replace(Constants.SR_PARENTID_HOLDER, objSr.ParentID.HasValue ? Constants.SR_SERVICE_REQUEST_PREFIX + objSr.ParentID.Value.ToString() : string.Empty);
                content = content.Replace(Constants.SR_STATUS_HOLDER, objSr.SR_Status.Name);
                content = content.Replace(Constants.SR_ASSIGNEDTO_HOLDER, objSr.AssignUser);
                content = content.Replace(Constants.SR_HISTORY_HOLDER, GetSRHistory(objSr.ID));
                content = content.Replace(Constants.SR_COMMENTS_HOLDER, GetSRComments(objSr.ID));
                string linkEmployee = string.Empty;
                string linkIT = string.Empty;
                string linkSurvey = string.Empty;
                if (objSr.StatusID == Constants.SR_STATUS_CLOSED)
                {
                    content = content.Replace(Constants.SR_FORWARD_TO_HOLDER, " has been closed");
                    //content = content.Replace(Constants.PR_FORWARD_TO_NAME_HOLDER, "");
                    linkEmployee = "http://" + HttpContext.Current.Request["SERVER_NAME"] + ":" + HttpContext.Current.Request["SERVER_PORT"] + "/Portal/ServiceRequest/Detail/" + objSr.ID;
                    linkIT = "http://" + HttpContext.Current.Request["SERVER_NAME"] + ":" + HttpContext.Current.Request["SERVER_PORT"] + "/ServiceRequestAdmin/Detail/" + objSr.ID;
                    linkSurvey = "http://" + HttpContext.Current.Request["SERVER_NAME"] + ":" + HttpContext.Current.Request["SERVER_PORT"] + "/Portal/ServiceRequest/CreateEvaluation/" + objSr.ID;
                    //content = content.Replace(Constants.SR_STATUS, objSr.ID.ToString());
                    content = content.Replace("none", "table-row");
                    content = content.Replace(Constants.SR_LINK_SURVEY, linkSurvey);
                }
                else
                {

                    content = content.Replace(Constants.SR_FORWARD_TO_HOLDER, " has been forwarded to you");
                    //content = content.Replace(Constants.PR_FORWARD_TO_NAME_HOLDER, "Forwarded To: " + new UserAdminDao().GetById((int)purReq.AssignID).UserName + " (" + purReq.WFRole.Name + " )");
                    linkEmployee = "http://" + HttpContext.Current.Request["SERVER_NAME"] + ":" + HttpContext.Current.Request["SERVER_PORT"] + "/Portal/ServiceRequest/Detail/" + objSr.ID;
                    linkIT = "http://" + HttpContext.Current.Request["SERVER_NAME"] + ":" + HttpContext.Current.Request["SERVER_PORT"] + "/ServiceRequestAdmin/CreateEvaluation/" + objSr.ID;
                }

                content = content.Replace(Constants.SR_LINK_EMPLOYEE_HOLDER, linkEmployee);
                content = content.Replace(Constants.SR_LINK_IT_HOLDER, linkIT);
            }

            return content;
        }
        public static string GetSRHistory(int id)
        {
            //string htmlResult = string.Empty;
            SR_ServiceRequest sr = null;
            sr = srDao.GetById(id);
            string htmlResult = "<table class='grid' style='width:500px'>"
                                    + "<tr>"
                                        + "<th width='190px'>Name</th>"
                                        + "<th width='100px'>Action</th>"
                                        + "<th >Date</th>"
                                    + "</tr>{0}</table>";
            string[] arrIds = sr.InvolveUser.Split(Constants.SEPARATE_INVOLVE_CHAR);
            string[] arrStatus = sr.InvolveStatus.Split(Constants.SEPARATE_INVOLVE_CHAR);
            string[] arrDate = sr.InvolveDate.Split(Constants.SEPARATE_INVOLVE_CHAR);
            int maxLength = new int[] { arrIds.Length, arrDate.Length, arrStatus.Length }.Min();
            
            string content = string.Empty;
            string tdTag = "<td>";
            for (int i = 0; i < maxLength - 1; i++)//last item is empty
            {
                SR_Status objStatus = srStatusDao.GetByID(arrStatus[i] != null ? int.Parse(arrStatus[i]) : 0);
                content += "<tr>";
                content += tdTag + (string.IsNullOrEmpty(arrIds[i]) ? "" : arrIds[i]) + "</td>" + tdTag +
                    (string.IsNullOrEmpty(arrStatus[i]) ? "" : objStatus.Name) + "</td>" + tdTag +
                    (string.IsNullOrEmpty(arrDate[i]) ? "" : arrDate[i]) + "</td>";
                content += "</tr>";
            }
            return string.Format(htmlResult, content);
        }
        public static string GetSRComments(int id)
        {
            //sp_GetPurchaseRequestResult pr = null;
            //pr = dao.GetPurchaseRequestByID(id.ToString());
            var comments = srCommentDao.GetList(id);
            string htmlResult = "<table cellspacing='0' cellpadding='0' border='0' class='tb_comment'><tbody>{0}</tbody></table>";
            string commentTemplate = "<tr {0}><td>"
                                        + "<span class='bold'>{1}</span>"
                                        + "<span class='gray'>({2})</span>"
                                        + "<br>{3}<br>"
                                    + "</td></tr>";
            string content = comments.Count > 0 ? string.Empty : "<tr><td><i>There's no comment!</i></td></tr>";
            for (int i = 0; i < comments.Count; i++)
            {
                content += string.Format(commentTemplate, i % 2 == 0 ? "" : "class='even'",
                    comments[i].Poster, comments[i].PostTime, string.IsNullOrWhiteSpace(comments[i].Contents) ? "" :
                    HttpUtility.HtmlEncode(comments[i].Contents).Replace("\r\n", "<br />"));
            }
            htmlResult = string.Format(htmlResult, content);
            return htmlResult;
        }
        public static string GetAssignUserInSRSetting(string userName)
        {

            Employee objEmployee = new EmployeeDao().GetByOfficeEmailInActiveList(userName + Constants.PREFIX_EMAIL_LOGIGEAR);
            if (objEmployee != null)
            {
                string project = null;
                int officeID = 0;
                if (!string.IsNullOrEmpty(objEmployee.Project))
                {
                    project = objEmployee.Project.Trim();
                }
                if (!string.IsNullOrEmpty(objEmployee.LocationCode))
                {
                    officeID = ConvertUtil.ConvertToInt(CommonFunc.GetLocation(objEmployee.LocationCode, LocationType.Office));
                }
                SR_Setting objSetting = new SRSettingDao().GetByProjectAndOffice(project, officeID);
                if (objSetting == null)
                {
                    //if project not been set routing then change to Office
                    objSetting = new SRSettingDao().GetByProjectAndOffice(null, officeID);
                }
                if (objSetting != null)
                {
                    return objSetting.UserAdmin.UserName;
                }
            }
            return string.Empty;
        }

        #endregion

        public static Message SR_CheckUploadedFiles(HttpFileCollectionBase files)
        {
            string[] extAllow = Constants.CONTRACT_EXT_NOT_ALLOW.Split(Constants.SEPARATE_INVOLVE_CHAR);
            foreach (string fileName in files)
            {
                HttpPostedFileBase file = files[fileName];
                if (file.ContentLength > 0)
                {
                    string ext = file.FileName.Substring(file.FileName.LastIndexOf('.')).Trim().ToLower();
                    //Check extension
                    if (extAllow.Contains(ext))
                        return new Message(MessageConstants.E0043, MessageType.Error, ext, Constants.SR_FILE_MAX_SIZE);
                    //Check size
                    if (file.ContentLength > Constants.SR_FILE_MAX_SIZE * 1024 * 1024)
                        return new Message(MessageConstants.E0012, MessageType.Error, Constants.SR_FILE_MAX_SIZE);
                }
            }
            return null;
        }
        public static string SR_RemoveDeletedFiles(string fileNames, string deletedFielNames)
        {
            var arrFiles = fileNames.Split(
                new char[]{Constants.SR_FILE_SEPARATE_CHAR}, StringSplitOptions.RemoveEmptyEntries);
            var arrDeletedFiles = deletedFielNames.Split(
                new char[] { Constants.SR_FILE_SEPARATE_CHAR }, StringSplitOptions.RemoveEmptyEntries);
            string result = string.Join(Constants.SR_FILE_SEPARATE_SIGN, arrFiles.Where(p => !arrDeletedFiles.Contains(p)));
            return string.IsNullOrEmpty(result) ? "" : (result + Constants.SR_FILE_SEPARATE_SIGN);
        }
        public static void SR_RemoveFiles(string fileNames)
        {
            if (string.IsNullOrEmpty(fileNames))
                return;
            var arrFiles = fileNames.Split(Constants.SR_FILE_SEPARATE_CHAR);
            foreach (string name in arrFiles)
            {
                string filePath = HttpContext.Current.Server.MapPath(Constants.SR_UPLOAD_PATH + name);
                if (File.Exists(filePath))
                {
                    FileInfo fi = new FileInfo(filePath);
                    fi.Delete();
                }
            }
        }
        public static int SR_ShowUploadedFile(string fileNames, int nameLength)
        {
            try
            {
                if (string.IsNullOrEmpty(fileNames))
                    return 0;
                //HttpContext.Current.Response.Write(Hidden("hidRemovedFiles"));
                //HttpContext.Current.Response.Write("<intput type='hidden' id='hidRemovedFiles' name='hidRemovedFiles'>");
                string[] nameArr = fileNames.Split(Constants.SR_FILE_SEPARATE_CHAR);
                int fileCount = nameArr.Length - 1;
                string removeButtonFormat = "<input class=\"icon delete\" type=\"button\" " +
                    " onclick=\"CRM.sr_RemoveExistingFile('{0}" + Constants.SR_FILE_SEPARATE_SIGN + "', this); " +
                    "\" title=\"Remove\"/>";
                string linkFormat = "<table style='width:100%' class='attachedFile'><tr><td><a href=\"#\" " +
                        "onclick=\"CRM.downLoadFile('" + Constants.SR_UPLOAD_PATH + "{0}" + "', '{1}');\">" +
                        "{2}" + "</a></td><td style='width:20px; text-align:right'>" + "{3}</td></tr></table>";
                for (int i = 0; i < nameArr.Length - 1; i++)
                {
                    string realName = nameArr[i].Remove(0, nameArr[i].IndexOf(Constants.SR_FILENAME_SEPARATE_SIGN) + 1);
                    string displayName = realName.Length <= nameLength ? realName : ("..." + realName.Remove(0,realName.Length-nameLength + 3));
                    string downloadName = realName.Substring(0, realName.LastIndexOf("."));
                    HttpContext.Current.Response.Write(string.Format(linkFormat, nameArr[i], downloadName,
                        displayName, string.Format(removeButtonFormat, nameArr[i])));
                }
                return fileCount;
            }
            catch
            {
                return -1;       
            }
        }
        /// <summary>
        /// Check if 2 time ranges are overlapped.
        /// </summary>
        /// <param name="dFrom1"></param>
        /// <param name="dTo1"></param>
        /// <param name="dFrom2"></param>
        /// <param name="dTo2"></param>
        /// <returns></returns>
        public static bool IsOverlappedPeriod(DateTime dFrom1, DateTime dTo1, DateTime dFrom2, DateTime dTo2)
        { 
            return (
                (dFrom2 >= dFrom1 && dFrom2 < dTo1) ||
                (dTo2 <= dTo1 && dTo2 > dFrom1) ||
                (dFrom1 >= dFrom2 && dFrom1 < dTo2) ||
                (dTo1 <= dTo2 && dTo1 > dFrom2)
                );
        }
        //Service Request Notification Added by Tai Nguyen 05-July-2011
        public static string OutputUndoneServiceRequest(List<Contract> list)
        {
            string result = string.Empty;
            if (list.Count > 0)
            {
                result += "<button id='btnExport' type='button' title='Export' class='button export' style='margin-bottom:7px;margin-right:20px;'>Export</button>";

                if (list.Count > 10)
                {
                    result += "<div id='div_notification' style='overflow-x: scroll;overflow-y: auto; padding-top: 0px; height:280px; width:650px'>";
                }
                result += "<table class='grid'>";
                result += "<tr>";
                result += "<th>ID</th>";
                result += "<th>Sub Department</th>";
                result += "<th>Contract Type</th>";
                result += "<th>Start Date</th>";
                result += "<th>End Date</th>";
                result += "<th>Action</th>";
                result += "</tr>";
                foreach (Contract item in list)
                {
                    result += "<tr>";
                    result += "<td align='center'><a  href='/Employee/Detail/" + item.Employee.ID + "' id=" + item.Employee.ID + " class=showTooltip >" + item.Employee.ID + "</a></td>";
                    result += "<td align='left'>" + item.Employee.Department.DepartmentName + "</td>";
                    result += "<td align='center'>" + item.ContractType1.ContractTypeName + "</td>";
                    result += "<td align='center'>" + item.StartDate.ToString(Constants.DATETIME_FORMAT_VIEW) + "</td>";
                    result += "<td align='center'>" + item.EndDate.Value.ToString(Constants.DATETIME_FORMAT_VIEW) + "</td>";
                    result += "<td align='center'>" + "<a href='/Employee/ContractRenewal/" + item.Employee.ID + "' title='Add contract' ><img border='0' src='/Content/Images/Icons/notepad.png' alt='Add contract'></a>" + "</td>";
                    result += "</tr>";
                }
                result += "</table>";
                if (list.Count > 10)
                {
                    result += "</div>";
                }
            }
            return result;
        }
        //End Service Request Notification

        public static bool CheckHasPermission(string userName, int permisson)
        {
            
            AuthenticateDao auDao = new AuthenticateDao();
            return auDao.CheckUserHasPermission(userName, permisson);
        }

        public static SortOrder SetSortOrder(object order)
        {
            try
            {
                if (order != null)
                {
                    if (order.ToString().Trim().ToLower() == "asc")
                    {
                        return SortOrder.asc;
                    }
                    else
                    {
                        return SortOrder.desc;
                    }
                }
                return SortOrder.desc;
            }
            catch
            {
                return SortOrder.desc;
            }
        }

        public static string ShowMaterial(sp_GetListMaterialResult material, int searchType, int totalItem, int itemIndex, bool onPortal)
        {
            try
            {
                var result= "<div class=\"material-item\">" +
                                "<div class=\"cimg-s\"></div>" +
                                "<div class=\"ctxt\">" +
                                    "<div class=\"headtitle\">{0}</div>" +
                                    "<div class=\"headcoursename\">{2}</div>" +
                                    "<div class=\"headdesc\">{1}</div>" +
                                "</div>" +
                                "<div class=\"clrfix\"></div>" +
                            "</div>";
                string desc = "<div class='materialContent'>" +
                    "<div class='materialLastModify'>{0}</div>" +
                    "<div class='materialDescription'><u>Description: </u>{1}</div>" +
                    "<div class='materialAction'>{2}</div>" +
                    "</div>";
                //string actionTempalte = "<table><tr>" +
                //        "<td style='text-align:right'>{0}</td><td class='downloadbutton'>{1}</td>";
                //if (!onPortal)
                //    actionTempalte += "<td class='editbutton'>{2}</td>" +
                //        "<td class='moveupbutton'>{3}</td><td class='movedownbutton'>{4}</td>";
                //actionTempalte += "</tr></table>";
                string actionTempalte = "<div>{0}{4}{3}{2}{1}</div>";
                if(onPortal)
                    actionTempalte = "<div>{0}{1}</div>";
                
                string lastModify = "Last modified at " + material.UpdateDate.ToString("dd-MMM-yyyy hh:mm tt") +
                    " by [<b>" + material.UpdatedBy + "</b>]";
                string description = CommonFunc.SubStringRoundWord(HttpUtility.HtmlEncode(material.Description),
                    Constants.TRAINING_CENTER_MATERIAL_LIST_DESCRIPTION_MAXLENGTH);
                string downloadText = "<i>Download (" + material.DownloadTimes + ")</i>";
                string filePath = Constants.TRAINING_CENTER_MATERIAL_UPLOAD_FOLDER + material.UploadFile;
                string downloadButton = CommonFunc.Button("download", "Download", "CRM.downloadMaterial(" + material.ID + ")", "Download");

                string action = "";
                if (!onPortal)
                {
                    string moveUpButton = "", moveDownButton = "", editButton = "";
                    if (itemIndex != 0)
                        moveUpButton = CommonFunc.Button("up", "Up", "materialMove(" + material.ID + ", true)", "Up");
                    if (itemIndex != totalItem - 1)
                        moveDownButton = CommonFunc.Button("down", "Down", "materialMove(" + material.ID + ", false)", "Down");
                    editButton = CommonFunc.Button("edit", "Edit", "showPopup('/TrainingMaterial/Edit/" + material.ID +
                        "', 'Edit Material', 700)", "Edit");
                    action = string.Format(actionTempalte, downloadText, downloadButton, editButton, moveUpButton, moveDownButton);
                }
                else
                {
                    action = string.Format(actionTempalte, downloadText, downloadButton);
                }
                
                desc = string.Format(desc, lastModify, description, action);
                
                string parentLabel = "";
                string parentName = "";
                string key = "";
                if (searchType == Constants.TRAINING_CENTER_MATERIAL_SEARCH_TYPE_CATEGORY)
                {
                    parentLabel = "<b>Category:</b> ";
                    key = HttpUtility.HtmlEncode(HttpUtility.UrlEncode(material.Category));
                    parentName = HttpUtility.HtmlEncode(material.Category);
                }
                else
                {
                    parentLabel = "<b>Course:</b> ";
                    parentName = HttpUtility.HtmlEncode( material.CourseName);
                    key = material.CourseId.Value.ToString();
                }
                string courseName = parentLabel + parentName;
                if(!onPortal)
                {
                    courseName = parentLabel + "<a style='color:Blue' href='/TrainingMaterial/SubList?type=" + searchType + "&key=" + key + "'>" +
                        parentName + "</a>";
                }
                return string.Format(result, HttpUtility.HtmlEncode(material.Title), desc, courseName);
            }
            catch
            {
                return Resources.Message.E0007;
            }
        }
        
        public static float GetEngLishSkillLevel(int score)
        {
            TrainingCenterDao trainDao = new TrainingCenterDao();
            return trainDao.GetEnglishLevel(score, false, false);
        }

        public static float GetEngLishCertificateLevel(int score)
        {
            TrainingCenterDao trainDao = new TrainingCenterDao();
            return trainDao.GetEnglishLevel(score, false, true);
        }

        public static float GetEngLishVerbalLevel(int verbalScore)
        {
            TrainingCenterDao trainDao = new TrainingCenterDao();
            return trainDao.GetEnglishLevel(verbalScore, true, null);
        }

        public static double? Average(params double?[] arrInput)
        {
            double total = 0;
            int count = arrInput.Where(p => p.HasValue).Count();
            if (count == 0)
                return null;
            foreach (var input in arrInput)
                if (input.HasValue)
                    total += input.Value;
            return total/count;
        }

        public static float? GetVerbalLevel(sp_GetListEnglishExamOfEmployeeResult candidateExam)
        {
            if (candidateExam != null && (candidateExam.VerbalToeicScore.HasValue || candidateExam.VerbalLevel.HasValue))
            {
                //return verbal mark as the level
                if (candidateExam.VerbalLevel.HasValue)
                    return (float)candidateExam.VerbalLevel;
                //return the level according to the score of TOEIC
                else
                    return GetEngLishVerbalLevel((int)Math.Floor(candidateExam.VerbalToeicScore.Value + 0.5));
            }
            return null;
        }

        public static void SetChartData(List<sp_GetListEnglishExamOfEmployeeResult> examList, ref string skills, ref string verbals)
        {
            try
            {
                string dataFormat = "{0},{1},{2},{3}";
                string separator = Constants.TRAINING_CENTER_MY_PROFILE_CHART_SEPARATOR;
                List<string> listSkill = new List<string>();
                List<string> listVerbal = new List<string>();
                for (int i = examList.Count - 1; i >= 0; i--)
                {
                    var item = examList[i];
                    double? score = Average(item.ComprehensionSkillScore, item.ListeningSkillScore, item.MultipleChoiceScore,
                        item.SentenceCorrectionScore, item.WritingSkillScore);
                    double? scoreVerbal = CommonFunc.GetVerbalLevel(item);


                    double nLevel = 0;
                    if (score.HasValue)
                    {
                        nLevel = GetEngLishSkillLevel((int)Math.Floor(score.Value + 0.5));
                        listSkill.Add(string.Format(dataFormat, item.ExamDate.Year, item.ExamDate.Month - 1, item.ExamDate.Day, nLevel));
                    }
                    if (scoreVerbal.HasValue)
                    {
                        //nLevel = GetEngLishVerbalevel((int)Math.Floor(scoreVerbal.Value + 0.5));
                        listVerbal.Add(string.Format(dataFormat, item.ExamDate.Year, item.ExamDate.Month - 1, item.ExamDate.Day, scoreVerbal));
                    }
                }
                skills = string.Join(separator, listSkill);
                verbals = string.Join(separator, listVerbal);
            }
            catch { }
        }

        public static List<int> LOTGetRandomListeningTopics(int totalQuestions) 
        {
            var listTopic = listeningTopicDao.GetList();
            List<int> topics = new List<int>();
            int loopTime = 0;
            while(loopTime < Constants.LOT_MAX_RANDOM_TIMES && topics.Count == 0)
            {
                loopTime++;
                int remain = totalQuestions;
                while (remain > 0)
                {
                    List<LOT_ListeningTopic> list = null;
                    //random fist topic
                    if (remain == totalQuestions)
                    {
                        //Get topics that have no more than [totalQuestions] questions
                        list = listTopic.Where(p =>
                            //topic must have more than 0 and less than [totalQuestions]
                            p.LOT_Questions.Count <= totalQuestions && p.LOT_Questions.Count > 0 ).ToList();
                        if (list.Count > 0)
                        {
                            int index = new Random().Next(0, list.Count - 1);
                            topics.Add(list[index].ID);
                            remain -= list[index].LOT_Questions.Count;
                        }
                        else
                        {
                            topics = new List<int>();
                            break;
                        }
                    }
                    else
                    {
                        list = listTopic.Where(p => p.LOT_Questions.Count <= remain && p.LOT_Questions.Count > 0 && !topics.Contains(p.ID)).ToList();
                        if (list.Count > 0)
                        {
                            int index = new Random().Next(0, list.Count - 1);
                            topics.Add(list[index].ID);
                            remain -= list[index].LOT_Questions.Count;
                        }
                        else
                        {
                            topics = new List<int>();
                            break;
                        }
                    }
                }
            }
            return topics;
        }

        public static void SetVacationSeniority(PTO pto, DateTime curDate, string month)
        {
            if (!pto.PTO_Type.IsHourType && pto.PTOType_ID == Constants.PTO_TYPE_MATERINITY_LEAVE)
            {
                PTOReportDao ptoReportDao = new PTOReportDao();
                PTO_Detail ptoDetail = new PTOReportDao().GetPToDetailByPTOID(pto.ID);
                if (ptoDetail != null)
                {
                    if (ptoDetail.DateOffFrom.HasValue)
                    {
                        if (ptoDetail.DateOffFrom.Value <= curDate)
                        {
                            DateTime datePTO_Report = DateTime.Parse(Constants.DAY_MONTH_YEAR_REPORT + month);
                            int vacetionseniority = ptoReportDao.SetVacationSeniority(pto.Submitter, datePTO_Report);
                            PTO_Report objReport = ptoReportDao.GetBalanceByDate(pto.Submitter, datePTO_Report);
                            if (objReport != null)
                            {
                                ptoReportDao.UpdateVacationSerinority(objReport, vacetionseniority);
                            }
                        }
                    }
                }
            }
        }

        public static List<int> LOTGetRandomParagraphs(int totalQuestions)
        {
            int remain = totalQuestions;
            var listParagraphs = paragraphDao.GetList();
            List<int> paragraphs = new List<int>();
            int loopTime = 0;
            while (loopTime < Constants.LOT_MAX_RANDOM_TIMES && paragraphs.Count == 0)
            {
                loopTime++;
                while (remain > 0)
                {
                    List<LOT_ComprehensionParagraph> list = null;
                    //random fist topic
                    if (remain == totalQuestions)
                    {
                        //Get paragraphs that have no more than [totalQuestions] questions
                        list = listParagraphs.Where(p =>
                            //paragraphs must have more than 0 and less than [totalQuestions]
                            p.LOT_Questions.Count <= totalQuestions && p.LOT_Questions.Count > 0).ToList();
                        if (list.Count > 0)
                        {
                            int index = new Random().Next(0, list.Count - 1);
                            paragraphs.Add(list[index].ID);
                            remain -= list[index].LOT_Questions.Count;
                        }
                        else
                        {
                            paragraphs = new List<int>();
                            break;
                        }
                    }
                    else
                    {
                        list = listParagraphs.Where(p => p.LOT_Questions.Count > 0 &&
                            p.LOT_Questions.Count <= remain && !paragraphs.Contains(p.ID)).ToList();
                        if (list.Count > 0)
                        {
                            int index = new Random().Next(0, list.Count - 1);
                            paragraphs.Add(list[index].ID);
                            remain -= list[index].LOT_Questions.Count;
                        }
                        else
                        {
                            paragraphs = new List<int>();
                            break;
                        }
                    }
                }
            }
            return paragraphs;
        }
        public static string NumberSuffix(int number)
        {
            string suffix = string.Empty;
            int a = number % 100;
            int b = a / 10;
            int c = a % 10;
            if (b == 1)
            {
                return "th";
            }
            else
            {
                if (c == 1)
                    return "st";
                else if (c == 2)
                    return suffix += "nd";
                else if (c == 3)
                    return suffix += "rd";
                else
                    return suffix += "th";
            }
        }

        public static string GetFilterText(string text)
        {
            string filter = string.Empty;

            if (!string.IsNullOrEmpty(text))
            {
                filter = text.Replace("%", "[%]");
                filter = filter.Replace("[", "[[]");
                filter = filter.Replace("_", "[_]");
                filter = "%" + Regex.Replace(filter.Trim(), @"\s+", "%") + "%";
            }

            return filter;
        }
    }
}