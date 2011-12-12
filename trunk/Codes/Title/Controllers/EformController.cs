using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CRM.Library.Common;
using CRM.Models;


namespace CRM.Controllers
{
    public class EformController : BaseController
    {
       
        #region Common method to call view page
        /// <summary>
        /// Using to call views of other page
        /// </summary>
        /// <param name="id">ID of Eform Table</param>
        /// <returns></returns>
        public ActionResult ViewPage(string id)
        {
            EformDao eDao = new EformDao();
            ViewData["eform"] = id;
            Int32 int_id = 0;
            try
            {
                int_id = int.Parse(id);
            }
            catch (FormatException)
            {
                return RedirectToAction("Index", "Home");
            }
            ViewData["detail_list"] = eDao.GetEformDetailByEFormID(int.Parse(id));
            var principal = HttpContext.User as AuthenticationProjectPrincipal;
            ViewData["user"] = principal.UserData.UserName;
            return View();
        }

        /// <summary>
        /// Using to call views of other page
        /// </summary>
        /// <param name="id">ID of Eform Table</param>
        /// <returns></returns>
        public ActionResult ViewPage(string id,string interviewID)
        {
            EformDao eDao = new EformDao();
            ViewData["eform"] = id;
            Int32 int_id = 0;
            try
            {
                int_id = int.Parse(id);
            }
            catch (FormatException)
            {
                return RedirectToAction("Index", "Home");
            }
            ViewData["detail_list"] = eDao.GetEformDetailByEFormID(int.Parse(id));
            ViewData["InterviewID"] = interviewID;
            var principal = HttpContext.User as AuthenticationProjectPrincipal;
            ViewData["user"] = principal.UserData.UserName;
            return View();
        }
        /// <summary>
        /// Using to call post form to server
        /// </summary>
        /// <param name="form">Form contain control</param>
        /// <param name="page">Page name aspx</param>
        /// <returns></returns>
        public ActionResult PostPage(FormCollection form, string page)
        {
            EformDao eDao = new EformDao();
            int id = 0;
            try
            {
                id = int.Parse(form.Get("Hidden1"));
            }
            catch (System.FormatException)
            {
                ViewData["eform"] = form.Get("Hidden1");
                return RedirectToAction(page + "/" + id);
            }
            List<EForm_Detail> list = eDao.GetEformDetailByEFormID(id);

            ShowMessage(SaveControlToDB(form, list.Count == 0 ? true : false));

            return RedirectToAction(page + "/" + id);
        }
        #endregion
        
        #region CommonFunction
        /// <summary>
        /// Insert colection control id and value on form into database,if not exits then addnew else then update
        /// Rule to define ControlID is:
        /// 1.If control is Checkbox then start with "CheckBox" letter and after is number(ex:CheckBox1).
        /// 2.If control is Textbox then start with "TextBox" letter and after is number(ex:TextBox1).
        /// ....
        /// </summary>
        /// <param name="form"></param>
        /// <returns></returns>
        private Message SaveControlToDB(FormCollection form, bool isInsert)
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
            CRM.Models.EformDao efDao=new EformDao();
            msg = isInsert == true ? efDao.Inser(detailList) : efDao.Update(detailList);

            return msg;
        }
        
        #endregion
        
        #region Demo call other pages
        //Demo call Interview Round 1 with aspx name (Index.aspx)
        public ActionResult Index(string id, string interviewID)
        {
            return ViewPage(id, interviewID);
        }
        public ActionResult DetaiInteviewR1(string id)
        {
            return ViewPage(id);
        }
        public ActionResult DetaiInteviewR2(string id)
        {
            return ViewPage(id);
        }
        public ActionResult DetaiInteviewR3(string id)
        {
            return ViewPage(id);
        }
        [HttpPost]
        public ActionResult Index(FormCollection form)
        {
            return PostPage(form, "index");
        }
        //Demo call Interview Round 2 with aspx name (Round2.aspx)
        public ActionResult Round2(string id, string interviewID)
        {
            return ViewPage(id, interviewID);
        }
        [HttpPost]
        public ActionResult Round2(FormCollection form)
        {
            return PostPage(form,"Round2");
        }
        //Demo call Interview Round 3 with aspx name (Round3.aspx)
        public ActionResult Round3(string id, string interviewID)
        {
            return ViewPage(id, interviewID);
        }
        [HttpPost]
        public ActionResult Round3(FormCollection form)
        {
            return PostPage(form, "Round3");
        }
        public ActionResult PerformanceReviewManager(string id)
        {
            return ViewPage(id);
        }

        [ValidateInput(false)]
        [HttpPost]
        public ActionResult PerformanceReviewManager(FormCollection form)
        {
            //var principal = HttpContext.User as AuthenticationProjectPrincipal;
            //EForm eForm = new EForm();
            //EformDao eFormDao = new EformDao();
            //eForm.PersonType = (int)Constants.PersonType.Candidate;
            //eForm.MasterID = "PR-1";
            //eForm.PersonID = "12345";
            //eForm.CreatedBy = principal.UserData.UserName;
            //eForm.FormIndex = 1;            
            //eFormDao.InsertEForm(eForm);
            Message msg = SaveControlToDB(form, false);
            return View();
        }
        public ActionResult PerformanceReviewIT(string id)
        {
            return ViewPage(id);
        }

        [ValidateInput(false)]
        [HttpPost]
        public ActionResult PerformanceReviewIT(FormCollection form)
        {
            var principal = HttpContext.User as AuthenticationProjectPrincipal;
            EForm eForm = new EForm();
            EformDao eFormDao = new EformDao();
            eForm.PersonType = (int)Constants.PersonType.Candidate;
            eForm.MasterID = "PR-2";
            eForm.PersonID = "12345";
            eForm.CreatedBy = principal.UserData.UserName;
            eForm.FormIndex = 1;
            eFormDao.InsertEForm(eForm);
            Message msg = SaveControlToDB(form, false);
            return View();
        }
        public ActionResult PerformanceReviewAdmin(string id)
        {
            return ViewPage(id);
        }

        [ValidateInput(false)]
        [HttpPost]
        public ActionResult PerformanceReviewAdmin(FormCollection form)
        {
            var principal = HttpContext.User as AuthenticationProjectPrincipal;
            EForm eForm = new EForm();
            EformDao eFormDao = new EformDao();
            eForm.PersonType = (int)Constants.PersonType.Candidate;
            eForm.MasterID = "PR-4";
            eForm.PersonID = "12345";
            eForm.CreatedBy = principal.UserData.UserName;
            eForm.FormIndex = 1;
            eFormDao.InsertEForm(eForm);
            Message msg = SaveControlToDB(form, false);
            return View();
        }
        public ActionResult PerformanceReviewEngineerService(string id)
        {
            return ViewPage(id);
        }

        [ValidateInput(false)]
        [HttpPost]
        public ActionResult PerformanceReviewEngineerService(FormCollection form)
        {
            var principal = HttpContext.User as AuthenticationProjectPrincipal;
            EForm eForm = new EForm();
            EformDao eFormDao = new EformDao();
            eForm.PersonType = (int)Constants.PersonType.Candidate;
            eForm.MasterID = "PR-5";
            eForm.PersonID = "12345";
            eForm.CreatedBy = principal.UserData.UserName;
            eForm.FormIndex = 1;
            eFormDao.InsertEForm(eForm);
            Message msg = SaveControlToDB(form, false);
            return View();
        }
        public ActionResult PerformanceReview60Days(string id)
        {
            return ViewPage(id);
        }

        [ValidateInput(false)]
        [HttpPost]
        public ActionResult PerformanceReview60Days(FormCollection form)
        {
            var principal = HttpContext.User as AuthenticationProjectPrincipal;
            EForm eForm = new EForm();
            EformDao eFormDao = new EformDao();
            eForm.PersonType = (int)Constants.PersonType.Candidate;
            eForm.MasterID = "PR-6";
            eForm.PersonID = "12345";
            eForm.CreatedBy = principal.UserData.UserName;
            eForm.FormIndex = 1;
            eFormDao.InsertEForm(eForm);
            Message msg = SaveControlToDB(form, false);
            return View();
        }
        public ActionResult PerformanceReviewForm(string id)
        {
            var principal = HttpContext.User as AuthenticationProjectPrincipal;
            ViewData["login"] = principal.UserData.UserName;
            return ViewPage(id);
        }

        [ValidateInput(false)]
        [HttpPost]
        public ActionResult PerformanceReviewForm(FormCollection form)
        {
            var principal = HttpContext.User as AuthenticationProjectPrincipal;
            EForm eForm = new EForm();
            EformDao eFormDao = new EformDao();
            eForm.PersonType = (int)Constants.PersonType.Candidate;
            eForm.MasterID = "PR-7";
            eForm.PersonID = "12345";
            eForm.CreatedBy = principal.UserData.UserName;
            eForm.FormIndex = 1;
            eFormDao.InsertEForm(eForm);
            Message msg = SaveControlToDB(form, false);
            return View();
        }
        #endregion
    }
}
