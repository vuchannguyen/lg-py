using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcApplication1.Controllers
{
    [HandleError]
    public class HomeController : Controller
    {
        public ActionResult Test(string sTest)
        {
            //Blah!  
            //....  
            Session.Remove("header");
            return Json(sTest, JsonRequestBehavior.AllowGet);
        }  

        public ActionResult Index()
        {
            ViewData["Message"] = "Welcome to ASP.NET MVC!";

            return View();
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult Fck()
        {
            return View();
        }

        public ActionResult FckJquery()
        {
            return View();
        }

        public ActionResult Browser()
        {
            return View();
        }
    }
}
