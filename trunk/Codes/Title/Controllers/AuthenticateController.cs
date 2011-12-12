using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using CRM.Models;
using CRM.Library.Common;
using CRM.Library.Exceptions;
using CRM.Areas.Portal.Models;

namespace CRM.Controllers
{
    public class AuthenticateController : Controller
    {
        protected AuthenticateDao auDao = new AuthenticateDao();
        public ActionResult Index()
        {
            return View();
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [ValidateInput(false)]
        public ActionResult Index(FormCollection collection)
        {
            var userName = collection["username"];
            var password = collection["password"];
            if (!Authenticate(userName, password))
                return View();
            string urlParam = Request["ReturnUrl"];
            string roleParam = string.Empty;
            if(!string.IsNullOrEmpty(urlParam))
            {
               int stringIndex = urlParam.IndexOf("role=");//find Role
                if(stringIndex > 0)
                {
                    stringIndex = stringIndex + 5;// replace string
                    roleParam = urlParam.Substring(stringIndex,urlParam.Length -  stringIndex);
                }
            }
            bool hasGroup = auDao.IsUserInGroup(userName);
            if (hasGroup)
            {
                UserAdmin logonUser = auDao.GetUserByName(userName);
                if (logonUser != null)
                {
                    //Log in CRM
                    DoLogin(logonUser);
                    // tan.tran: Update code of Phi Hung
                    if (urlParam == null || !urlParam.StartsWith("Portal"))
                    {
                        UpdateHitCounter(logonUser);
                    }
                }
            }
            #region Log in Portal
            //Employee objEmp = new EmployeeDao().GetByOfficeEmailInActiveList(userName + Constants.PREFIX_EMAIL_LOGIGEAR);
            //if (objEmp != null)
            //{
                DoLoginPortal(userName);
            //}
            #endregion
            //check user go to job request by clicking on the link on routing email
            if (!string.IsNullOrEmpty(urlParam))
            {
                if (urlParam.Contains("JobRequest"))
                {
                    if (!string.IsNullOrEmpty(roleParam))
                    {
                        TempData["roleId"] = roleParam;
                        return RedirectToAction("Index", "JobRequest");
                    }
                }
                else if (urlParam.Contains("PurchaseRequest"))
                {
                    if (!string.IsNullOrEmpty(roleParam))
                    {
                        TempData["roleId"] = roleParam;
                        return RedirectToAction("Index", "PurchaseRequest");
                    }
                }
                //case go to CRM but no permisson ,automatic send to Portal
                if (!hasGroup)
                {
                    return Redirect("/Portal");
                }
                return Redirect(urlParam);
            }
            else
            {
                if (hasGroup)
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    return Redirect("/Portal");
                }
            }
         
        }
        
        private void DoLoginPortal(string userName)
        {
            int sessionTimeout = 400;
            Employee emp = CommonFunc.GetEmployeeByUserName(userName);
            PortalUserData ud;
            if (emp == null)
            {
                ud = new PortalUserData("", userName, userName);
            }
            else
            {
                ud = new PortalUserData(emp.ID, userName, CommonFunc.GetEmployeeFullName(emp));
            }
            FormsAuthenticationTicket authTicket = new FormsAuthenticationTicket(1, userName, DateTime.Now, DateTime.Now.AddMinutes(sessionTimeout), false, ud.ToXml());
            //Encrypt ticket
            string encryptedTicket = FormsAuthentication.Encrypt(authTicket);
            //Add to cookie
            HttpCookie authCookie = new HttpCookie(Constants.COOKIE_PORTAL, encryptedTicket);
            Response.Cookies.Add(authCookie);
        }

        public ActionResult Error()
        {
            ViewData["Error"] = Resources.Message.E0002;
            return View();
        }

        private void UpdateHitCounter(UserAdmin admin)
        {
            string stSession = Session.SessionID;
            string oldSession = string.Empty;
            if (Session[SessionKey.SESSION_BROWSE_ID] != null)
            {
                oldSession = Session[SessionKey.SESSION_BROWSE_ID].ToString();
            }
            // update session id
            Session[SessionKey.SESSION_BROWSE_ID] = stSession;

            // compare if it is deifferent
            if (stSession != oldSession)
            {
                // insert new record into db
                LogAccessDao accDao = new LogAccessDao();
                Message msg = accDao.InsertNewAccess(stSession, admin.UserName, Request.UserHostAddress);
                
                if (!msg.MsgType.Equals(MessageType.Error))
                {
                    HttpContext.Application.Lock();
                    int count = int.Parse(HttpContext.Application["Online"].ToString());
                    HttpContext.Application["Online"] = count > 0 ? count + 1 : 1;
                    HttpContext.Application.UnLock();
                }
                 
            }
        }

        #region Helper methods

        [NonAction()]
        private bool Authenticate(string username, string password)
        {
            //ViewData["ErrorDetails"] = string.Empty;
            var fieldAuthenticated = true;
            if (String.IsNullOrEmpty(username) || String.IsNullOrEmpty(password))
            {
                ViewData["ErrorDetails"] = Resources.Message.E0009;
                fieldAuthenticated = false;

            }
            else if (!Login(username, password))
            {
                ViewData["ErrorDetails"] = Resources.Message.E0008;
                fieldAuthenticated = false;
            }
            return fieldAuthenticated;
        }

        public ActionResult DoLogOff()
        {
            string endSess = Session.SessionID;
            if (endSess != null)
            {
                LogAccessDao dao = new LogAccessDao();
                PortalLogAccessDao portalDao = new PortalLogAccessDao();
                portalDao.SetTimeOut(endSess);
                Message msg = dao.SetTimeOut(endSess);
            }
            this.ExpireCookie(Constants.COOKIE_PORTAL);
            this.ExpireCookie(Constants.COOKIE_CRM);
            this.ExpireCookie("AccessibleMenus");
            SessionManager.ClearAll(Session);
            SessionManager.Destroy(Session);
            
            return Redirect("/Authenticate/Index");
        }


        public ActionResult PermissionError()
        {
            throw new ForbiddenException();
        }

        [NonAction()]
        private bool Login(string username, string pass)
        {
            bool hasPermisson = auDao.CheckExistInAD(username, pass);
            if (hasPermisson)
            {
                return true;
            }
            return false;
        }

        [NonAction()]
        private void DoLogin(UserAdmin user)
        {
            int sessionTimeout = 400; 
            UserData ud = new UserData(user.UserAdminId, user.UserName, 0);
            //put user data to authentication ticket
            FormsAuthenticationTicket authTicket = new FormsAuthenticationTicket(1, user.UserName, DateTime.Now, DateTime.Now.AddMinutes(sessionTimeout), false, ud.ToXml());
            //Encrypt ticket
            string encryptedTicket = FormsAuthentication.Encrypt(authTicket);
            //Add to cookie
            HttpCookie authCookie = new HttpCookie(Constants.COOKIE_CRM, encryptedTicket);
            Response.Cookies.Add(authCookie);
            //invalidate cookie
            InvalidateModulesCookie();
        }

        [NonAction()]
        private void InvalidateModulesCookie()
        {
            HttpCookie cookie = HttpContext.Request.Cookies["AccessibleMenus"];
            if (cookie != null)
            {
                cookie.Expires = new DateTime(1970, 1, 1);
                HttpContext.Response.Cookies.Add(cookie);
            }
        }

        [NonAction()]
        private void ExpireCookie(string cookieName)
        {
            if (Request.Cookies[cookieName] != null)
            {
                HttpCookie cookie = new HttpCookie(cookieName);
                cookie.Expires = DateTime.Now.AddDays(-1d);
                Response.Cookies.Add(cookie);
            }
        }

        #endregion
    }
}
