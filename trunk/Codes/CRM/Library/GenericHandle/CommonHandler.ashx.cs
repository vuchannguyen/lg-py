using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CRM.Library.GenericHandle
{
    /// <summary>
    /// Summary description for CommonHandler
    /// </summary>
    public class CommonHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            context.Response.Write("Hello World");
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