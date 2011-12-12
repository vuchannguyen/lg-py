using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using CRM.Library.Common;

namespace CRM.Controllers
{
    public class HelpController : BaseController
    {
        //
        // GET: /Help/

        public ActionResult Index()
        {
            return View();
        }        
        [AcceptVerbs(HttpVerbs.Post)]
        [ValidateInput(false)]
        public string GetData(string id)
        {
            return CommonFunc.GetHelpData(id);
        }
    }
}
