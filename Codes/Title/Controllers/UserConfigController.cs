using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CRM.Models;
using CRM.Library.Common;

namespace CRM.Controllers
{
    public class UserConfigController : BaseController
    {
        UserConfigDao configDao = new UserConfigDao();
        //
        // GET: /UserConfig/

        public ActionResult Index()
        {
            UserConfig obj = configDao.GetByID(principal.UserData.UserID);
            if (obj == null)
            {
                ViewData["IsCreate"] = true;
            }
            else
            {
                ViewData["IsCreate"] = false;
            }
            ViewData["UserID"] = principal.UserData.UserID.ToString();
            return View(obj);
        }

        [HttpPost]
        public ActionResult Index(UserConfig obj)
        {
            Message msg = null;
            if (Request["IsCreate"] == "True")
            {
               msg = configDao.Create(obj);
            }
            else
            {
                msg = configDao.Update(obj);
            }
            ShowMessage(msg);
            return RedirectToAction("Index");
        }

    }
}
