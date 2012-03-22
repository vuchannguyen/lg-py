using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CRM.Models;
using CRM.Areas.Portal.Models;
using CRM.Library.Exceptions;
using CRM.Library.Common;

namespace CRM.Library.Attributes
{
    public class PortalAuthorizeAttribute : AuthorizeAttribute
    {
        public Role Role { get; set; }
        public bool ShowAtCurrentPage { get; set; }
        public bool ShowInPopup { get; set; }

        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            base.OnAuthorization(filterContext);

            var principal = filterContext.HttpContext.User as PortalAuthenticationProjectPrincipal;
            if (principal == null)
            {
                ShowMessage(filterContext);
                return;
            }

            var isAuthorized = false;

            isAuthorized= IsUserAuthorized(principal.PortalData.UserID);

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

        public bool IsUserAuthorized(string userID)
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

        #endregion
    }
}