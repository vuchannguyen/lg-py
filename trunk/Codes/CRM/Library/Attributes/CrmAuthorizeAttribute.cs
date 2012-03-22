using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Web.Mvc;
using System.Collections;
using System.Text;
using CRM.Models;
using CRM.Library.Common;
using CRM.Library.Exceptions;

namespace CRM.Library.Attributes
{
    public class CrmAuthorizeAttribute : AuthorizeAttribute
    {
        private AuthenticateDao auDao = new AuthenticateDao();
        public Modules Module { get; set; }
        public Permissions Rights { get; set; }
        public bool ShowAtCurrentPage { get; set; }
        public bool ShowInPopup { get; set; }

        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            base.OnAuthorization(filterContext);

            var principal = filterContext.HttpContext.User as AuthenticationProjectPrincipal;
            if (principal == null)
            {
                ShowMessage(filterContext);
                return;
            }

            var isAuthorized = false;
            var moduleId = (byte)Module;
            ArrayList permissionIds = new ArrayList();

            HttpSessionStateBase session = filterContext.HttpContext.Session;
            StringBuilder strBuilder = new StringBuilder();
            strBuilder.AppendFormat("{0}_{1}_", principal.UserData.UserID, moduleId);

            //loop all permission to check the permission is allowed and add to array 
            foreach (Permissions right in Enum.GetValues(typeof(Permissions)))
            {
                //if (!((Convert.ToInt32(Rights) & Convert.ToInt32(right)) == Convert.ToInt32(right))) continue;
                if (Convert.ToInt32(Rights) != Convert.ToInt32(right)) continue;
                permissionIds.Add(right);
                strBuilder.Append((byte)right);
            }

            string cacheKey = strBuilder.ToString();

            if (SessionManager.Get(session, cacheKey) != null)
            {
                isAuthorized = (bool)SessionManager.Get(session, cacheKey);
                if (isAuthorized)
                    return;                
            }

            isAuthorized = this.IsUserAuthorized(principal.UserData.UserID, moduleId, Array.ConvertAll(permissionIds.ToArray(), arr => (int)arr));

            SessionManager.Add(session, cacheKey, isAuthorized);            

            if (!isAuthorized)
                ShowMessage(filterContext);
        }

        /// <summary>
        /// Closes the connection and sets the status code to Unauthorized
        /// </summary>
        /// <param name="filterContext">The AuthorizationContext</param>
        private void ShowMessage(AuthorizationContext filterContext)
        {
            HttpResponseBase response = filterContext.HttpContext.Response;
            //response.StatusCode = 401;//Unauthorized status code
            if (ShowAtCurrentPage)
            {                
                throw new ForbiddenExceptionOnCurrentPage();                
            }
            else if (ShowInPopup)
            {
                Message msg = new Message(MessageConstants.E0002, MessageType.Info);
                response.Write(msg.ToString());
                response.Flush();
                response.End();
            }
            else
            {
                throw new ForbiddenException();                
            }
        }

        #region helper methods

        public bool IsUserAuthorized(int userId, byte moduleId, int[] permissionIds)
        {
            return auDao.CheckPermission(userId, moduleId, permissionIds);
        }

        #endregion
    }
}