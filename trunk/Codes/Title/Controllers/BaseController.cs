using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CRM.Models;
using CRM.Library.Exceptions;
using CRM.Library.Attributes;
using CRM.Library.Common;
using System.Configuration;
using System.Web.Security;
namespace CRM.Controllers
{
    public class BaseController : Controller
    {
        public AuthenticationProjectPrincipal principal = null;
        protected AuthenticateDao auDao = new AuthenticateDao();
        private GroupDao groupDao = new GroupDao();
        private GroupPermissionDao groupPermissionDao = new GroupPermissionDao();
        private const string cookieName = "AccessibleMenus";
        private string msgHoder = "<div id=\"systemmessage\" class=\"{0}\">{1}</div>";



        protected override void Initialize(System.Web.Routing.RequestContext requestContext)
        {
            HttpCookie cookie = requestContext.HttpContext.Request.Cookies[Constants.COOKIE_CRM];
            if (cookie != null)
            {
                base.Initialize(requestContext);
                    FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(cookie.Value);
                    FormsIdentity identity = new FormsIdentity(ticket);
                    UserData udata = UserData.CreateUserData(ticket.UserData);
                    principal = new AuthenticationProjectPrincipal(identity, udata);
                    requestContext.HttpContext.User = principal;
                    cookie = requestContext.HttpContext.Request.Cookies[cookieName];
                    if (cookie != null)
                    {
                        ViewData[cookieName] = cookie.Value.Split(',').ToList();
                        return;
                    }
                    else
                    {
                        AuthenticationProjectPrincipal raPrincipal = requestContext.HttpContext.User as AuthenticationProjectPrincipal;
                        if (raPrincipal == null)
                            return;
                        principal = raPrincipal;
                        IList<string> modules = this.GetAccessableModuleNames(raPrincipal.UserData.UserID);
                        ViewData[cookieName] = modules;

                        cookie = new HttpCookie(cookieName, string.Join(",", modules.ToArray()));
                        cookie.Expires = DateTime.Now.AddMinutes(20);
                        requestContext.HttpContext.Response.Cookies.Add(cookie);
                    }
            }
            else
            {
                string url = requestContext.HttpContext.Request.Url.PathAndQuery;
                requestContext.HttpContext.Response.Redirect("/Authenticate/Index/?ReturnUrl=" + url);
            }

        }


        protected override void OnException(ExceptionContext filterContext)
        {
            if (filterContext.HttpContext.IsCustomErrorEnabled)
            {
                string message = "";

                Exception ex = filterContext.Exception;
                filterContext.ExceptionHandled = true;

                if (ex is CrmException)
                {
                    CrmException ge = (CrmException)ex;
                    if (ex is ForbiddenExceptionOnCurrentPage)
                    {
                        string controllerName = filterContext.Controller.ValueProvider.GetValue("controller").RawValue.ToString();
                        Message msg = new Message(MessageConstants.E0002, MessageType.Error);
                        ShowMessage(msg);
                        this.RedirectToAction("Index", controllerName).ExecuteResult(this.ControllerContext);
                    }
                    else
                    {
                        message = ge.ErrorMessage;
                        ViewData["ErrorMessage"] = message;
                        this.View("Error").ExecuteResult(this.ControllerContext);
                    }
                }
                else if (ex is Exception)
                {
                    message = ex.Message;
                    ViewData["ErrorMessage"] = message;
                    this.View("Error").ExecuteResult(this.ControllerContext);
                }
            }
        }
        #region helper methods

        [NonAction()]
        private IList<string> GetAccessableModuleNames(int userId)
        {
            try
            {
                IList<string> modNames = new List<string>();
                foreach (var module in Enum.GetValues(typeof(Modules)))
                {
                    if (this.HasUserAccessToModule(userId, Convert.ToByte(module)))
                        modNames.Add(module.ToString());
                }

                return modNames;
            }

            catch (Exception e)
            {
                throw e;
            }
        }

        [NonAction()]
        private bool HasUserAccessToModule(int userId, int moduleId)
        {
            try
            {
                int permissionId = Convert.ToInt32(Permissions.Read);

                AuthenticateDao auDao = new AuthenticateDao();
                ArrayList permissionIds = new ArrayList();
                permissionIds.Add(permissionId);
                return auDao.CheckPermission(userId, (byte)moduleId, Array.ConvertAll(permissionIds.ToArray(), arr => (int)arr));

            }
            catch (Exception e)
            {
                throw e;
            }

        }

        //duy hung sua
        protected void ShowMessage(Message msg)
        {
            string message = string.Empty;
            if (msg.MsgType == MessageType.Error)
            {
                message = string.Format(msgHoder, "msgError", msg.MsgText);
            }
            else if (msg.MsgType == MessageType.Warning)
            {
                message = string.Format(msgHoder, "msgWarning", msg.MsgText);
            }
            else
            {
                message = string.Format(msgHoder, "msgSuccess", msg.MsgText);
            }

            TempData["Message"] = message;
        }
        protected void ShowMessage(string msg, MessageType msgType)
        {
            string message = string.Empty;
            if (msgType == MessageType.Error)
            {
                message = string.Format(msgHoder, "msgError", msg);
            }
            else if (msgType == MessageType.Warning)
            {
                message = string.Format(msgHoder, "msgWarning", msg);
            }
            else
            {
                message = string.Format(msgHoder, "msgSuccess", msg);
            }

            TempData["Message"] = message;
        }

        //duy hung sua
        protected void ShowAlertMessage(Message msg)
        {
            TempData["Message"] = "<script>alert('" + msg.MsgText + "');</script>";
        }

        #endregion

        public class FileUploadJsonResult : JsonResult
        {
            public override void ExecuteResult(ControllerContext context)
            {
                this.ContentType = "text/html";
                context.HttpContext.Response.Write("<textarea>");
                base.ExecuteResult(context);
                context.HttpContext.Response.Write("</textarea>");
            }
        }

    }
}
