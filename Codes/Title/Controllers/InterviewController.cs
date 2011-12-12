using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CRM.Library.Attributes;
using CRM.Models;
using CRM.Library.Common;
using System.Web.UI.WebControls;
using System.IO;
using System.Reflection;
using System.Net.Mail;
using System.Text;
using System.Net.Mime;
using System.Net;
using System.DirectoryServices;
using System.Collections;
using System.Configuration;
using CRM.Library.Utils;

namespace CRM.Controllers
{
    public class InterviewController : BaseController
    {
        #region Local Variable
        private JobTitleLevelDao levelDao = new JobTitleLevelDao();
        private InterviewDao interviewDao = new InterviewDao();
        private CandidateDao candidateDao = new CandidateDao();
        private EmployeeStatusDao empStatusDao = new EmployeeStatusDao();
        private DepartmentDao deptDao = new DepartmentDao();
        private InsuranceHospitalDao hospitalDao = new InsuranceHospitalDao();
        private JobRequestItemDao requestItemDao = new JobRequestItemDao();
        #endregion

        #region Add New Employee

        private void CopyFile(Candidate obj)
        {
            if (obj.Photograph != null)
            {
                //Copy photograph
                string pathS = Server.MapPath(Constants.PHOTO_PATH_ROOT_CANDIDATE) + obj.Photograph;
                string pathD = Server.MapPath(Constants.IMAGE_PATH) + obj.Photograph;
                if (System.IO.File.Exists(pathS))
                {
                    System.IO.File.Move(pathS, pathD);
                }

                // Copy CV file
                pathS = Server.MapPath(Constants.CV_PATH_ROOT_CANDIDATE) + obj.CVFile;
                pathD = Server.MapPath(Constants.CV_PATH) + obj.CVFile;
                if (System.IO.File.Exists(pathS))
                {
                    System.IO.File.Move(pathS, pathD);
                }
            }
        }

        [CrmAuthorizeAttribute(Module = Modules.STT, Rights = Permissions.Insert)]
        public ActionResult ToSTT(string id)
        {
            ////Check Candidate was become employee
            //if (candidateDao.IsEmployee(id))
            //{
            //    Message msg = new Message(MessageConstants.E0031, MessageType.Error, id);
            //    ShowAlertMessage(msg);
            //    return RedirectToAction("Index");
            //}

            Candidate can = candidateDao.GetById(id);

            if (can == null)
            {
                Message msg = new Message(MessageConstants.E0005, MessageType.Error, id, "Candidate list");
                ShowAlertMessage(msg);
                return RedirectToAction("Index");
            }

            ViewData["MarriedStatus"] = new SelectList(Constants.MarriedStatus, "Value", "Text", "");
            ViewData["Nationality"] = new SelectList(Constants.Nationality, "Value", "Text", "");
            ViewData["VnNationality"] = new SelectList(Constants.VnNationality, "Value", "Text", "");
            ViewData["Gender"] = new SelectList(Constants.Gender, "Value", "Text", can.Gender);

            List<EmployeeStatus> empStatusList = empStatusDao.GetList().Where(p => p.StatusId != Constants.RESIGNED).ToList<EmployeeStatus>();
            ViewData["EmpStatusId"] = new SelectList(empStatusList, "StatusId", "StatusName", "");
            IList<ListItem> title = new List<ListItem>();
            ViewData["TitleId"] = new SelectList(title, "Value", "Text", "");

            ViewData["Department"] = new SelectList(deptDao.GetList(), "DepartmentId", "DepartmentName", "");
            IList<ListItem> subDepartment = new List<ListItem>();
            ViewData["SubDepartment"] = new SelectList(subDepartment, "Value", "Text", "");
            return View(can);
        }

        [HttpPost]
        [CrmAuthorizeAttribute(Module = Modules.STT, Rights = Permissions.Insert)]
        public ActionResult ToSTT(STT obj)
        {
            JsonResult result = new JsonResult();
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            var principal = HttpContext.User as AuthenticationProjectPrincipal;
            string canId = Request["CandidateId"];
            #region edit for Address
            if (obj.PermanentAddress == Constants.ADDRESS)
            {
                obj.PermanentAddress = null;
            }
            if (obj.PermanentArea == Constants.AREA)
            {
                obj.PermanentArea = null;
            }
            if (obj.PermanentCityProvince == Constants.CITYPROVINCE)
            {
                obj.PermanentCityProvince = null;
            }
            if (obj.PermanentDistrict == Constants.DISTRICT)
            {
                obj.PermanentDistrict = null;
            }
            if (obj.TempAddress == Constants.ADDRESS)
            {
                obj.TempAddress = null;
            }
            if (obj.TempArea == Constants.AREA)
            {
                obj.TempArea = null;
            }
            if (obj.TempCityProvince == Constants.CITYPROVINCE)
            {
                obj.TempCityProvince = null;
            }
            if (obj.TempDistrict == Constants.DISTRICT)
            {
                obj.TempDistrict = null;
            }
            //VN
            if (obj.VnPermanentAddress == Constants.VN_ADDRESS)
            {
                obj.VnPermanentAddress = null;
            }
            if (obj.VnPermanentArea == Constants.VN_AREA)
            {
                obj.VnPermanentArea = null;
            }
            if (obj.VnPermanentCityProvince == Constants.VN_CITYPROVINCE)
            {
                obj.VnPermanentCityProvince = null;
            }
            if (obj.VnPermanentDistrict == Constants.VN_DISTRICT)
            {
                obj.VnPermanentDistrict = null;
            }
            if (obj.VnTempAddress == Constants.VN_ADDRESS)
            {
                obj.VnTempAddress = null;
            }
            if (obj.VnTempArea == Constants.VN_AREA)
            {
                obj.VnTempArea = null;
            }
            if (obj.VnTempCityProvince == Constants.VN_CITYPROVINCE)
            {
                obj.VnTempCityProvince = null;
            }
            if (obj.VnTempDistrict == Constants.VN_DISTRICT)
            {
                obj.VnTempDistrict = null;
            }
            #endregion
            obj.CreateDate = DateTime.Now;
            obj.UpdateDate = DateTime.Now;
            obj.CreatedBy = principal.UserData.UserName;
            obj.UpdatedBy = principal.UserData.UserName;
            obj.CandidateID = canId; 
            Message msg = null;
            try
            {
                STTDao sttDao = new STTDao();
                if (CommonFunc.IsLocationCodeValid(obj.LocationCode))
                    msg = sttDao.Insert(obj);
                else
                {
                    msg = new Message(MessageConstants.E0030, MessageType.Error, "Work location");
                    result.Data = msg;
                    return result;
                }
                
                InterviewDao intDao = new InterviewDao();
                Candidate can = new Candidate();
                // Case from candidate to employee 
                // Save candidate to history
                can = intDao.GetCandidateById(canId);
                if (can != null)
                {
                    can.UpdatedBy = principal.UserData.UserName;
                    // Save to history
                    intDao.SaveToHistory(can);
                    // update status of candidate
                    candidateDao.UpdateStatus(CandidateStatus.Passed, can.ID);
                }
                if (msg.MsgType == MessageType.Info)
                {
                    CopyFile(can);
                }
            }
            catch
            {
                msg = new Message(MessageConstants.E0007, MessageType.Error);
            }
            result.Data = msg;
            return result;
        }

        [CrmAuthorizeAttribute(Module = Modules.Employee, Rights = Permissions.Insert)]
        public ActionResult ToEmployee(string id)
        {
            //Check Candidate was become employee
            if (candidateDao.IsEmployee(id))
            {
                Message msg = new Message(MessageConstants.E0031, MessageType.Error, id);
                ShowAlertMessage(msg);
                return RedirectToAction("Index");
            }

            Candidate can = candidateDao.GetById(id);

            if (can == null)
            {
                Message msg = new Message(MessageConstants.E0005, MessageType.Error, id, "Candidate list");
                ShowAlertMessage(msg);
                return RedirectToAction("Index");
            }

            ViewData["MarriedStatus"] = new SelectList(Constants.MarriedStatus, "Value", "Text", "");
            ViewData["Nationality"] = new SelectList(Constants.Nationality, "Value", "Text", "");
            ViewData["VnNationality"] = new SelectList(Constants.VnNationality, "Value", "Text", "");
            ViewData["Gender"] = new SelectList(Constants.Gender, "Value", "Text", can.Gender);

            List<EmployeeStatus> empStatusList = empStatusDao.GetList().Where(p => p.StatusId != Constants.RESIGNED).ToList<EmployeeStatus>();
            ViewData["EmpStatusId"] = new SelectList(empStatusList, "StatusId", "StatusName", "");
            IList<ListItem> title = new List<ListItem>();
            ViewData["TitleId"] = new SelectList(title, "Value", "Text", "");

            ViewData["Department"] = new SelectList(deptDao.GetList(), "DepartmentId", "DepartmentName", "");
            IList<ListItem> subDepartment = new List<ListItem>();
            ViewData["SubDepartment"] = new SelectList(subDepartment, "Value", "Text", "");
            ViewData["Hospital"] = new SelectList(hospitalDao.GetList(), "ID", "Name", "");

            return View(can);
        }

        [HttpPost]
        [CrmAuthorizeAttribute(Module = Modules.Employee, Rights = Permissions.Insert)]
        public ActionResult ToEmployee(Employee obj)
        {
            JsonResult result = new JsonResult();
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            var principal = HttpContext.User as AuthenticationProjectPrincipal;
            #region edit for Address
            if (obj.PermanentAddress == Constants.ADDRESS)
            {
                obj.PermanentAddress = null;
            }
            if (obj.PermanentArea == Constants.AREA)
            {
                obj.PermanentArea = null;
            }
            if (obj.PermanentCityProvince == Constants.CITYPROVINCE)
            {
                obj.PermanentCityProvince = null;
            }
            if (obj.PermanentDistrict == Constants.DISTRICT)
            {
                obj.PermanentDistrict = null;
            }
            if (obj.TempAddress == Constants.ADDRESS)
            {
                obj.TempAddress = null;
            }
            if (obj.TempArea == Constants.AREA)
            {
                obj.TempArea = null;
            }
            if (obj.TempCityProvince == Constants.CITYPROVINCE)
            {
                obj.TempCityProvince = null;
            }
            if (obj.TempDistrict == Constants.DISTRICT)
            {
                obj.TempDistrict = null;
            }
            //VN
            if (obj.VnPermanentAddress == Constants.VN_ADDRESS)
            {
                obj.VnPermanentAddress = null;
            }
            if (obj.VnPermanentArea == Constants.VN_AREA)
            {
                obj.VnPermanentArea = null;
            }
            if (obj.VnPermanentCityProvince == Constants.VN_CITYPROVINCE)
            {
                obj.VnPermanentCityProvince = null;
            }
            if (obj.VnPermanentDistrict == Constants.VN_DISTRICT)
            {
                obj.VnPermanentDistrict = null;
            }
            if (obj.VnTempAddress == Constants.VN_ADDRESS)
            {
                obj.VnTempAddress = null;
            }
            if (obj.VnTempArea == Constants.VN_AREA)
            {
                obj.VnTempArea = null;
            }
            if (obj.VnTempCityProvince == Constants.VN_CITYPROVINCE)
            {
                obj.VnTempCityProvince = null;
            }
            if (obj.VnTempDistrict == Constants.VN_DISTRICT)
            {
                obj.VnTempDistrict = null;
            }
            #endregion
            obj.CreateDate = DateTime.Now;
            obj.UpdateDate = DateTime.Now;
            obj.CreatedBy = principal.UserData.UserName;
            obj.UpdatedBy = principal.UserData.UserName;
            Message msg = null;
            try
            {
                EmployeeDao empDao = new EmployeeDao();
                if (CommonFunc.IsLocationCodeValid(obj.LocationCode))
                    msg = empDao.Insert(obj);
                else
                {
                    msg = new Message(MessageConstants.E0030, MessageType.Error, "Work location");
                    result.Data = msg;
                    return result;
                }
                //msg = empDao.Insert(obj);
                InterviewDao intDao = new InterviewDao();
                Candidate can = new Candidate();
                // Case from candidate to employee 
                // Save candidate to history
                can = intDao.GetCandidateById(obj.CandidateId.ToString());
                if (can != null)
                {
                    can.UpdatedBy = principal.UserData.UserName;
                    can.ID = (obj.CandidateId == null ? -1 : (int)obj.CandidateId);
                    // Save to history
                    intDao.SaveToHistory(can);
                    // update status of candidate
                    candidateDao.UpdateStatus(CandidateStatus.Passed, (int)obj.CandidateId);
                }
                if (msg.MsgType == MessageType.Info)
                {
                    msg = empDao.InsertTracking(obj);
                    //Copy File
                    CopyFile(can);
                }
            }
            catch
            {
                msg = new Message(MessageConstants.E0007, MessageType.Error);
            }
            result.Data = msg;
            //ShowMessage(msg);
           // return RedirectToAction("Index");
            return result;
        }
        #endregion

        #region Interview

        /// <summary>
        /// Check interviewer exist
        /// </summary>
        /// <param name="userName">string</param>
        /// <returns>JsonResult</returns>
        public JsonResult CheckInterviewByExist(string userName)
        {
            //TODO: Do the validation
            Message msg = null;
            JsonResult result = new JsonResult();
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            UserAdminDao userDao = new UserAdminDao();
            UserAdmin obj = userDao.GetByUserName(userName);
            if (obj == null)
            {
                msg = new Message(MessageConstants.E0005, MessageType.Error, userName, "User admin");
                result.Data = msg.ToString();
            }
            else
            {
                result.Data = true;
            }

            return result;
        }

        /// <summary>
        /// Check Job request exist
        /// </summary>
        /// <param name="Jr">string</param>
        /// <returns>JsonResult</returns>
        public JsonResult CheckJRByExist(string jr)
        {
            //TODO: Do the validation
            Message msg = null;
            JsonResult result = new JsonResult();
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            JobRequestItemDao jrDao = new JobRequestItemDao();            
            JobRequestItem obj = jrDao.GetByID(jr);
            if (obj == null)
            {
                msg = new Message(MessageConstants.E0005, MessageType.Error, jr, "Job request");
                result.Data = msg.ToString();
            }
            else
            {
                result.Data = true;
            }

            return result;
        }

        /// <summary>
        /// Search Interviewer
        /// </summary>
        /// <param name="q">string</param>
        /// <returns>ActionResult</returns>
        public ActionResult SearchInterviewBy(string q)
        {
            UserAdminDao userAdminDao = new UserAdminDao();
            List<UserAdmin> listUserAdmin = userAdminDao.GetList().Where(p => p.UserName.Contains(q)).ToList<UserAdmin>();
            string content = "";
            foreach (UserAdmin item in listUserAdmin)
            {
                content += item.UserName + Environment.NewLine;
            }

            return Content(content);
        }

        /// <summary>
        /// Search Interviewer
        /// </summary>
        /// <param name="q">string</param>
        /// <returns>ActionResult</returns>
        public ActionResult SearchVenue(string q)
        {
            List<string> list = Constants.Venue;
            string content = "";
            foreach (string item in list)
            {
                content += item + Environment.NewLine;
            }

            return Content(content);
        }

        [CrmAuthorizeAttribute(Module = Modules.Hiring, Rights = Permissions.Update)]
        public ActionResult Create(string id)
        {
            if (interviewDao.IsHaveCandidateInterview(int.Parse(id)))
            {
                Message msg = new Message(MessageConstants.I0005, MessageType.Info);
                ShowMessage(msg);
                return RedirectToAction("index");
            }
            ViewData["Interview"] = 1;
            InterviewResultTemplateDao rtDao = new InterviewResultTemplateDao();
            List<EFormMaster> resultList = rtDao.GetList();
            ViewData["ResultTemplate"] = resultList;
            Candidate viewData = candidateDao.GetById(id);
            if (viewData == null || viewData.DeleteFlag)
                return RedirectToAction("index");

            return View(viewData);
        }

        [CrmAuthorizeAttribute(Module = Modules.Hiring, Rights = Permissions.Read)]
        public ActionResult Detail(string id)
        {            
            Candidate viewData = candidateDao.GetById(id);
            if (viewData == null)
                return RedirectToAction("Index");
            List<int> intList = GetListInterviewForNavigation();
            ViewData["ListInter"] = intList;
            List<sp_GetInterviewCandidateResult> list = interviewDao.GetInterviewCandidate(int.Parse(id),1);
            //ViewData["Interview"] = list.Count;
            ViewData["InterviewCandiActive"] = list.Where(p => p.OldInterView == false).ToList<sp_GetInterviewCandidateResult>();
            ViewData["InterviewCandiOld"] = list.Where(p => p.OldInterView == true).ToList<sp_GetInterviewCandidateResult>();

            return View(viewData);
        }

        [CrmAuthorizeAttribute(Module = Modules.Hiring, Rights = Permissions.Read)]
        public ActionResult DetailHistory(string id)
        {
            Candidate viewData = candidateDao.GetById(id);
            if (viewData == null)
                return RedirectToAction("HistoryInterview");
            List<int> intList = GetListInterviewHistoryForNavigation();
            ViewData["ListInter"] = intList;
            List<sp_GetInterviewCandidateResult> list = interviewDao.GetInterviewCandidate(int.Parse(id), 1);
            ViewData["Interview"] = list.Count;            
            ViewData["InterviewCandi"] = list;                  
            return View(viewData);
        }

        /// <summary>
        /// Get list interview for navigation
        /// </summary>
        /// <returns>List<int></returns>
        private List<int> GetListInterviewForNavigation()
        {
            List<int> interList = null;

            if (Session[SessionKey.INTERVIEW_FILTER] != null)
            {
                Hashtable hashData = (Hashtable)Session[SessionKey.INTERVIEW_FILTER];
                string name = (string)hashData[Constants.INTERVIEW_LIST_NAME];
                if (name == Constants.CANDIDATE_NAME)
                {
                    name = string.Empty;
                }
                
                int status = (!string.IsNullOrEmpty((string)hashData[Constants.INTERVIEW_LIST_STATUS]) ? int.Parse((string)hashData[Constants.INTERVIEW_LIST_STATUS]) : 0);
                int result = (!string.IsNullOrEmpty((string)hashData[Constants.INTERVIEW_LIST_RESULT]) ? int.Parse((string)hashData[Constants.INTERVIEW_LIST_RESULT]) : 0);
                string interviewer = !string.IsNullOrEmpty((string)hashData[Constants.INTERVIEW_LIST_INTERVIEWER]) ? 
                    (string)hashData[Constants.INTERVIEW_LIST_INTERVIEWER] : "";
                DateTime? from = null;
                if(!string.IsNullOrEmpty((string)hashData[Constants.INTERVIEW_LIST_DATE_FROM]))
                    from = DateTime.Parse((string)hashData[Constants.INTERVIEW_LIST_DATE_FROM]);
                DateTime? to = null;
                if (!string.IsNullOrEmpty((string)hashData[Constants.INTERVIEW_LIST_DATE_TO]))
                    to = DateTime.Parse((string)hashData[Constants.INTERVIEW_LIST_DATE_TO]);
                string column = !string.IsNullOrEmpty((string)hashData[Constants.INTERVIEW_LIST_COLUMN]) ?
                    (string)hashData[Constants.INTERVIEW_LIST_COLUMN] : "ID";
                string order = !string.IsNullOrEmpty((string)hashData[Constants.INTERVIEW_LIST_ORDER]) ?
                    (string)hashData[Constants.INTERVIEW_LIST_ORDER] : "desc";
                
                List<sp_GetInterviewListResult>  list = interviewDao.GetInterviewList(name, status, result, interviewer, from, to);
                var flist = interviewDao.Sort(list, column, order);
                interList = flist.Select(p => p.ID).ToList();
            }
            else
            {
                interList = interviewDao.GetInterviewList("", 0, 0, "", null, null).Select(p => p.ID).ToList();
            }

            return interList;
        }

        /// <summary>
        /// Get list interview history for navigation
        /// </summary>
        /// <returns>List<int></returns>
        private List<int> GetListInterviewHistoryForNavigation()
        {
            List<int> interList = null;

            if (Session[SessionKey.INTERVIEW_FILTER] != null)
            {
                Hashtable hashData = (Hashtable)Session[SessionKey.INTERVIEW_HISTORY_FILTER];
                string name = (string)hashData[Constants.INTERVIEW_LIST_HISTORY_NAME];
                if (name == Constants.CANDIDATE_NAME)
                {
                    name = string.Empty;
                }

                int position = (!string.IsNullOrEmpty((string)hashData[Constants.INTERVIEW_LIST_HISTORY_POSITION]) ?
                    int.Parse((string)hashData[Constants.INTERVIEW_LIST_HISTORY_POSITION]) : 0);
                int result = (!string.IsNullOrEmpty((string)hashData[Constants.INTERVIEW_LIST_HISTORY_RESULT]) ? 
                    int.Parse((string)hashData[Constants.INTERVIEW_LIST_HISTORY_RESULT]) : 0);
                int source = !string.IsNullOrEmpty((string)hashData[Constants.INTERVIEW_LIST_HISTORY_SOURCE]) ?
                    int.Parse((string)hashData[Constants.INTERVIEW_LIST_HISTORY_SOURCE]) : 0;
                DateTime? from = null;
                if (!string.IsNullOrEmpty((string)hashData[Constants.INTERVIEW_LIST_DATE_FROM]))
                    from = DateTime.Parse((string)hashData[Constants.INTERVIEW_LIST_DATE_FROM]);
                DateTime? to = null;
                if (!string.IsNullOrEmpty((string)hashData[Constants.INTERVIEW_LIST_DATE_TO]))
                    to = DateTime.Parse((string)hashData[Constants.INTERVIEW_LIST_DATE_TO]);
                string column = !string.IsNullOrEmpty((string)hashData[Constants.INTERVIEW_LIST_HISTORY_COLUMN]) ?
                    (string)hashData[Constants.INTERVIEW_LIST_HISTORY_COLUMN] : "ID";
                string order = !string.IsNullOrEmpty((string)hashData[Constants.INTERVIEW_LIST_HISTORY_ORDER]) ?
                    (string)hashData[Constants.INTERVIEW_LIST_HISTORY_ORDER] : "desc";

                List<sp_GetInterviewHistoryListResult> list = interviewDao.GetHistoryInterviewList(name, source, result, position, from, to);
                var flist = interviewDao.SortHistoryInterview(list, column, order);
                interList = flist.Select(p => p.ID).ToList();
            }
            else
            {
                interList = interviewDao.GetHistoryInterviewList("", 0, 0, 0, null, null).Select(p => p.ID).ToList();
            }

            return interList;
        }

        [CrmAuthorizeAttribute(Module = Modules.Hiring, Rights = Permissions.Read)]
        public ActionResult PreviewForm(string id, string interviewID)
        {
            ViewData["Round"] = id;
            if (!string.IsNullOrEmpty(interviewID))
            {
                ViewData["InterviewID"] = interviewID;
            }
            return View();
        }

        [CrmAuthorizeAttribute(Module = Modules.Hiring, Rights = Permissions.Update)]
        public ActionResult Edit(string id)
        {
            Candidate viewData = candidateDao.GetById(id);
            if (viewData == null)
                return RedirectToAction("Index");

            List<sp_GetInterviewCandidateResult> list = interviewDao.GetInterviewCandidate(int.Parse(id));
            ViewData["Interview"] = list.Count;
            var jsonData = new
            {
                rows = (
                    from m in list
                    select new
                    {

                        i = m.Id,
                        cell = new string[] {                                                                                              
                                m.InterviewDate.Value.ToString(Constants.DATETIME_FORMAT_TIME),
                                m.Venue,
                                m.Pic,                                
                                m.Content
                            }
                    }
                ).ToArray()
            };
            ViewData["InterviewCandi"] = list;
            InterviewResultTemplateDao rtDao = new InterviewResultTemplateDao();
            List<EFormMaster> resultList = rtDao.GetList();
            ViewData["ResultTemplate"] = resultList;

            return View(viewData);
        }

        private void ViewEform(string id)
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
                //return RedirectToAction("Index", "Home");
            }
            ViewData["detail_list"] = eDao.GetEformDetailByEFormID(int.Parse(id));
            var principal = HttpContext.User as AuthenticationProjectPrincipal;
            ViewData["user"] = principal.UserData.UserName;
        }

        [CrmAuthorizeAttribute(Module = Modules.Hiring, Rights = Permissions.Update)]
        public ActionResult ResultForm(string id)
        {
            Interview inter = interviewDao.GetById(id);
            if (inter == null)
                return RedirectToAction("Index");
            Candidate viewData = candidateDao.GetById(inter.CandidateId.ToString());
            if (viewData == null)
                return RedirectToAction("Index");

            var principal = HttpContext.User as AuthenticationProjectPrincipal;
            Message msg = null;

            if ((!inter.Pic.Split(Constants.SEPARATE_CC_LIST).Contains(principal.UserData.UserName)) && !IsInCClist(inter, principal.UserData.UserName))
            {
                msg = new Message(MessageConstants.E0002, MessageType.Error);
                ShowMessage(msg);
                return RedirectToAction("index");
            }

            InterviewResultTemplateDao rtDao = new InterviewResultTemplateDao();
            List<EFormMaster> resultList = rtDao.GetList();

            ViewData["InterviewResultId"] = (inter.InterviewResultId == null ? 0 : inter.InterviewResultId);
            ViewData["InterviewID"] = id;
            ViewData["InterviewForm"] = (inter != null ? inter.InterviewFormId : Constants.INTERVIEW_RESULT_TEMPLATE_1);
            ViewData["Round"] = (inter != null ? inter.Round : 1);
            ViewData["Note"] = (inter != null ? inter.Note : "");
            //Call eform
            EformDao eDao = new EformDao();
            int Id = eDao.GetIndexEform(inter.InterviewFormId, Convert.ToString(inter.CandidateId), (int)Constants.PersonType.Candidate, (int)inter.Round);
            ViewEform(Id.ToString());

            return View(viewData);
        }

        /// <summary>
        /// Edit result of interview
        /// </summary>
        /// <param name="id">interview id</param>
        /// <returns>ActionResult</returns>
        [CrmAuthorizeAttribute(Module = Modules.Hiring, Rights = Permissions.Update)]
        public ActionResult EditResult(string id)
        {
            Interview inter = interviewDao.GetById(id);
            if (inter == null)
                return RedirectToAction("Index");
            Candidate viewData = candidateDao.GetById(inter.CandidateId.ToString());
            if (viewData == null)
                return RedirectToAction("Index");

            var principal = HttpContext.User as AuthenticationProjectPrincipal;
            Message msg = null;

            if ((!inter.Pic.Split(Constants.SEPARATE_CC_LIST).Contains(principal.UserData.UserName)) && !IsInCClist(inter, principal.UserData.UserName))
            {
                msg = new Message(MessageConstants.E0002, MessageType.Error);
                ShowMessage(msg);
                return RedirectToAction("index");
            }

            if (!IsCanEditCurrentRound(inter))
            {
                msg = new Message(MessageConstants.E0045, MessageType.Error);
                ShowMessage(msg);
                return RedirectToAction("index");
            }

            InterviewResultTemplateDao rtDao = new InterviewResultTemplateDao();
            List<EFormMaster> resultList = rtDao.GetList();

            ViewData["InterviewResultId"] = (inter.InterviewResultId == null ? 0 : inter.InterviewResultId);
            ViewData["InterviewID"] = id;
            ViewData["InterviewForm"] = (inter != null ? inter.InterviewFormId : Constants.INTERVIEW_RESULT_TEMPLATE_1);
            ViewData["Round"] = (inter != null ? inter.Round : 1);
            ViewData["Note"] = (inter != null ? inter.Note : "");
            //Call eform
            EformDao eDao = new EformDao();
            int Id = eDao.GetIndexEform(inter.InterviewFormId, Convert.ToString(inter.CandidateId), (int)Constants.PersonType.Candidate, (int)inter.Round);
            ViewEform(Id.ToString());

            return View(viewData);
        }

        /// <summary>
        /// Is in CC list
        /// </summary>
        /// <param name="inter">interview</param>
        /// <param name="username">user login</param>
        /// <returns>bool</returns>
        private bool IsInCClist(Interview inter, string username)
        {
            if (inter.CClist != null)
            {
                if (inter.CClist.Split(Constants.SEPARATE_CC_LIST).Contains(username))
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Is can edit the current interview round
        /// </summary>
        /// <param name="inter">Interview</param>
        /// <returns></returns>
        private bool IsCanEditCurrentRound(Interview inter)
        {
            int? currentRount = interviewDao.GetCurrentInterviewedRound((int)inter.CandidateId);
            if (currentRount == inter.Round)
                return true;
            else
                return false;
        }

        private void PostEform(FormCollection form)
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

            }
            List<EForm_Detail> list = eDao.GetEformDetailByEFormID(id);
            CRM.Library.Common.Eform eform = new CRM.Library.Common.Eform();
            eform.SaveControlToDB(form, list.Count == 0 ? true : false);

        }

        [HttpPost]
        [CrmAuthorizeAttribute(Module = Modules.Hiring, Rights = Permissions.Update)]
        public JsonResult ResultForm(FormCollection form, Interview objUI)
        {
            JsonResult result = new JsonResult();
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            var principal = HttpContext.User as AuthenticationProjectPrincipal;
            Message msg = null;
            objUI.UpdatedBy = principal.UserData.UserName;
            objUI.InterviewResultId = int.Parse(form["InterviewResultId"].ToString());
            msg = interviewDao.UpdateResult(objUI);
            int candidateId = int.Parse(form["CandidateId"].ToString());
            Candidate viewData = candidateDao.GetById(candidateId.ToString());

            // Because only have 3 eform check round equal less then 3
            if (objUI.Round < 4)
            {
                PostEform(form);
            }
            // Update status of candidate
            if ((Constants.InterviewResult)objUI.InterviewResultId == Constants.InterviewResult.Recruit)
            {
                candidateDao.UpdateStatus(CandidateStatus.Passed, candidateId);
            }
            else
            {
                candidateDao.UpdateStatus(CandidateStatus.Interviewing, candidateId);
            }
            if (msg.MsgType != MessageType.Error)
            {
                // Get the next round
                string successMsg = msg.MsgText;
                int currentRound = interviewDao.GetCurrentRound(candidateId);
                
                // If remain interview then send mail 
                if (objUI.Round < currentRound && (Constants.InterviewResult)objUI.InterviewResultId == Constants.InterviewResult.Passes)
                {
                    Interview aInt = interviewDao.GetByCandidateRound(candidateId, currentRound);
                    string ids = aInt.Id.ToString();
                    msg = new Message(MessageConstants.I0001, MessageType.Info, successMsg, ids);
                    result.Data = msg;                     
                }
                else
                {
                    msg = new Message(MessageConstants.I0001, MessageType.Info, "Result of " + viewData.FirstName + " " + viewData.MiddleName + " " + viewData.LastName, "updated");
                    ShowMessage(msg);
                   result.Data = new Message(MessageConstants.I0001, MessageType.Info,successMsg, ""); 
                }
            }
            else
            {
                result.Data = msg;
            }
            return result;
        }

        [HttpPost]
        [CrmAuthorizeAttribute(Module = Modules.Hiring, Rights = Permissions.Update)]
        public JsonResult EditResult(FormCollection form, Interview objUI)
        {
            JsonResult result = new JsonResult();
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            var principal = HttpContext.User as AuthenticationProjectPrincipal;
            Message msg = null;
            if (!IsCanEditCurrentRound(objUI))
            {
                msg = new Message(MessageConstants.E0045, MessageType.Error);
                result.Data = msg;
                return result;
            }

            objUI.UpdatedBy = principal.UserData.UserName;
            objUI.InterviewResultId = int.Parse(form["InterviewResultId"].ToString());
            msg = interviewDao.UpdateResult(objUI);
            int candidateId = int.Parse(form["CandidateId"].ToString());
            Candidate viewData = candidateDao.GetById(candidateId.ToString());

            // Because only have 3 eform check round equal less then 3
            if (objUI.Round < 4)
            {
                PostEform(form);
            }
            // Update status of candidate
            if ((Constants.InterviewResult)objUI.InterviewResultId == Constants.InterviewResult.Recruit)
            {
                candidateDao.UpdateStatus(CandidateStatus.Passed, candidateId);
            }
            else
            {
                candidateDao.UpdateStatus(CandidateStatus.Interviewing, candidateId);
            }
            if (msg.MsgType != MessageType.Error)
            {
                // Get the next round
                string successMsg = msg.MsgText;
                int currentRound = interviewDao.GetCurrentRound(candidateId);

                // If remain interview then send mail 
                if (objUI.Round < currentRound && (Constants.InterviewResult)objUI.InterviewResultId == Constants.InterviewResult.Passes)
                {
                    Interview aInt = interviewDao.GetByCandidateRound(candidateId, currentRound);
                    string ids = aInt.Id.ToString();
                    msg = new Message(MessageConstants.I0001, MessageType.Info, successMsg, ids);
                    result.Data = msg;
                }
                else
                {
                    msg = new Message(MessageConstants.I0001, MessageType.Info, "Result of " + viewData.FirstName + " " + viewData.MiddleName + " " + viewData.LastName, "updated");
                    ShowMessage(msg);
                    result.Data = new Message(MessageConstants.I0001, MessageType.Info, successMsg, "");
                }
            }
            else
            {
                result.Data = msg;
            }
            return result;
        }
        public ActionResult ActionSendMail(string id)
        {

            ViewData["IDs"] = id;
            return View();
        }

        public ActionResult JobRequestLink(string id)
        {
            ViewData["IDs"] = id;
            return View();
        }

        [HttpPost]
        [CrmAuthorizeAttribute(Module = Modules.Hiring, Rights = Permissions.Insert)]
        public JsonResult Create(FormCollection content)
        {
            JsonResult result = new JsonResult();
             Message msg = null;
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            var number = 1;
            if (!string.IsNullOrEmpty(content["txtNumber"]))
            {
                number = int.Parse(content["txtNumber"].ToString());
            }

            var CandidateId = 0;
            if (!string.IsNullOrEmpty(content["canId"]))
            {
                CandidateId = int.Parse(content["canId"].ToString());
            }

            var principal = HttpContext.User as AuthenticationProjectPrincipal;
            Interview aInt = new Interview();
            List<Interview> iList = new List<Interview>();
            List<EForm> eList = new List<EForm>();
            EformDao eFormDao = new EformDao();
            string pic = string.Empty;
            string templateRound = string.Empty;
            for (int i = 1; i <= number; i++)
            {
                Interview iDetail = new Interview();

                iDetail.DeleteFlag = false;
                string date = content["InterviewDate" + i.ToString()] + " " + content["Hour" + i.ToString()] + ":" + content["Minute" + i.ToString()] + ":00";
                DateTime interviewDate = DateTime.Parse(date);
                iDetail.InterviewDate = interviewDate;
                if (!string.IsNullOrEmpty(content["txtLocation" + i.ToString()]))
                    iDetail.Venue = content["txtLocation" + i.ToString()].Trim();
                iDetail.Pic = content["txtPicHid" + i.ToString()].Trim().ToLower();
                iDetail.InterviewFormId = content["ResultTemplate" + i.ToString()].ToString();
                if (!string.IsNullOrEmpty(content["Comment" + i.ToString()]))
                    iDetail.Content = content["Comment" + i.ToString()].Trim();
                iDetail.Round = i;
                iDetail.InterviewStatusId = 1;
                iDetail.CClist = content["CCList" + i.ToString()].Trim();
                iDetail.CreatedDate = DateTime.Now;
                iDetail.UpdatedDate = DateTime.Now;
                iDetail.CandidateId = CandidateId;
                iDetail.CreatedBy = principal.UserData.UserName;
                iDetail.UpdatedBy = principal.UserData.UserName;
                iDetail.OldInterView = false;
                iDetail.IsSendMailInterviewer = false;
                iDetail.IsSentMailCandidate = false;
                iList.Add(iDetail);
                if (i == 1)
                {
                    pic = iDetail.Pic;
                }
                EForm eForm = new EForm();
                eForm.PersonType = (int)Constants.PersonType.Candidate;
                eForm.MasterID = iDetail.InterviewFormId;
                eForm.PersonID = CandidateId.ToString();
                eForm.CreatedBy = principal.UserData.UserName;
                eForm.FormIndex = i;
                templateRound += iDetail.InterviewFormId + ",";
                eFormDao.InsertEForm(eForm);
            }
            if (iList.Count > 0)
            {
                Candidate can = new Candidate();
                can.UpdatedBy = principal.UserData.UserName;
                int? JDId = null;

                if (!String.IsNullOrEmpty(content["JR"]))
                {
                    JDId = int.Parse(content["JR"].ToString());
                }

                can = candidateDao.GetById(CandidateId.ToString());
                can.JRId = JDId;
                candidateDao.UpdateJR(can);
                // Update status of Candidate
                candidateDao.UpdateStatus(CandidateStatus.Interviewing, CandidateId);
                //Refresh candidate
                interviewDao.UnSaveToHistory(can);
                //Delete old interview
                interviewDao.DeleteOldInterview(CandidateId);

                msg = interviewDao.InsertListDetail(iList, templateRound, JDId.Value.ToString()); 
            }

            // Send mail
            int currentRound = interviewDao.GetCurrentRound(CandidateId);
            aInt =  interviewDao.GetByCandidateRound(CandidateId, currentRound);
            string message = msg.MsgText;
            result.Data = new Message(MessageConstants.I0001, MessageType.Info, message, aInt.Id.ToString());
            //return RedirectToAction("SendInterviewMail", new { id=aInt.Id });
            return result;
        }

        /// <summary>
        /// Edit
        /// </summary>
        /// <param name="content">FormCollection</param>
        /// <returns>ActionResult</returns>
        [HttpPost]
        [CrmAuthorizeAttribute(Module = Modules.Hiring, Rights = Permissions.Update)]
        public JsonResult Edit(FormCollection content)
        {
            var number = 1;
            JsonResult result = new JsonResult();
            if (!string.IsNullOrEmpty(content["txtNumber"]))
            {
                number = int.Parse(content["txtNumber"].ToString());
            }

            var CandidateId = 0;
            if (!string.IsNullOrEmpty(content["canId"]))
            {
                CandidateId = int.Parse(content["canId"].ToString());
            }

            var principal = HttpContext.User as AuthenticationProjectPrincipal;
            Interview aInt = new Interview();
            List<Interview> iList = new List<Interview>();
            EformDao eFormDao = new EformDao();
            //Delete old eform 
            //interviewDao.DeleteEform(CandidateId.ToString());
            int currentRound = interviewDao.GetCurrentRound(CandidateId);
            Interview oldInter = interviewDao.GetByCandidateRound(CandidateId, currentRound);
            Message msg =null;
            bool isDiff = true;
            int? JDId = null;
            if (!String.IsNullOrEmpty(content["JR"]))
            {
                JDId = int.Parse(content["JR"].ToString());
            }
            for (int i = 1; i <= number; i++)
            {
                Interview iDetail = new Interview();
                
                iDetail.Round = i;
                iDetail.CandidateId = CandidateId;
                //If exist then Update
                int check = interviewDao.CheckExistInterview((int)iDetail.CandidateId, (int)iDetail.Round);
                iDetail.DeleteFlag = false;
                if (check != 1)
                {
                    string date = content["InterviewDate" + i.ToString()] + " " + content["Hour" + i.ToString()] + ":" + content["Minute" + i.ToString()] + ":00";
                    DateTime interviewDate = DateTime.Parse(date);
                    iDetail.InterviewDate = interviewDate;
                    if (!string.IsNullOrEmpty(content["txtLocation" + i.ToString()]))
                        iDetail.Venue = content["txtLocation" + i.ToString()].Trim();
                    iDetail.Pic = content["txtPicHid" + i.ToString()].Trim();
                    iDetail.InterviewFormId = content["ResultTemplate" + i.ToString()].ToString();
                    if (!string.IsNullOrEmpty(content["Comment" + i.ToString()]))
                        iDetail.Content = content["Comment" + i.ToString()].Trim();
                    iDetail.CClist = content["CCList" + i.ToString()].Trim();
                    iDetail.CreatedDate = DateTime.Now;
                    iDetail.UpdatedDate = DateTime.Now;
                    iDetail.CreatedBy = principal.UserData.UserName;
                    iDetail.UpdatedBy = principal.UserData.UserName;

                    iList.Add(iDetail);

                    if (check == 0)
                    {
                        // Check is have difference
                        if (iDetail.Round == currentRound)
                            isDiff = !CompareInterview(oldInter, iDetail);
                        msg = interviewDao.UpdateInterview(iDetail,JDId.Value.ToString());
                    }
                    else if (check == -1)
                    {
                        iDetail.OldInterView = false;
                        msg = interviewDao.Insert(iDetail);
                    }

                    EForm eForm = new EForm();
                    eForm.PersonType = (int)Constants.PersonType.Candidate;
                    eForm.MasterID = iDetail.InterviewFormId;
                    eForm.PersonID = CandidateId.ToString();
                    eForm.CreatedBy = principal.UserData.UserName;
                    eForm.FormIndex = i;
                    // If not exist eform then insert new
                    if (eFormDao.GetIndexEform(eForm.MasterID, eForm.PersonID, eForm.PersonType, i) < 1)
                        eFormDao.InsertEForm(eForm);
                }
            }
            Candidate can = new Candidate();
            can = candidateDao.GetById(CandidateId.ToString());
            if (iList.Count > 0)
            {
                can.JRId = JDId;
                candidateDao.UpdateJR(can);
            }
            
            aInt = interviewDao.GetByCandidateRound(CandidateId, currentRound);
            string message = string.Empty;
            if (msg != null)
            {
                message = msg.MsgText;
            }
           
            // If interview is changed then send mail
            if (isDiff && !string.IsNullOrEmpty(message))
            {
                result.Data = new Message(MessageConstants.I0001, MessageType.Info, message, aInt.Id.ToString()); 
            }
            else
            {
                msg = new Message(MessageConstants.I0001, MessageType.Info, "The round of " + can.FirstName + " " + can.MiddleName + " " + can.LastName, "updated");
                ShowMessage(msg);
                result.Data = new Message(MessageConstants.I0001, MessageType.Info, message, "");
            }
            return result;

        }

        #endregion

        #region Interview List

        /// <summary>
        /// Set Session Filter
        /// </summary>
        /// <param name="name">string</param>
        /// <param name="status">string</param>
        /// <param name="result">string</param>
        /// <param name="interviewer">string</param>
        /// <param name="dateFrom">string</param>
        /// <param name="dateTo">string</param>
        /// <param name="column">string</param>
        /// <param name="order">string</param>
        /// <param name="pageIndex">int</param>
        /// <param name="rowCount">int</param>
        private void SetSessionFilter(string name, string status, string result, string interviewer, string dateFrom, string dateTo,
            string column, string order, int pageIndex, int rowCount)
        {
            Hashtable hashData = new Hashtable();
            hashData.Add(Constants.INTERVIEW_LIST_NAME, name);
            hashData.Add(Constants.INTERVIEW_LIST_INTERVIEWER, interviewer);
            hashData.Add(Constants.INTERVIEW_LIST_STATUS, status);
            hashData.Add(Constants.INTERVIEW_LIST_RESULT, result);
            hashData.Add(Constants.INTERVIEW_LIST_DATE_FROM, dateFrom);
            hashData.Add(Constants.INTERVIEW_LIST_DATE_TO, dateTo);
            hashData.Add(Constants.INTERVIEW_LIST_COLUMN, column);
            hashData.Add(Constants.INTERVIEW_LIST_ORDER, order);
            hashData.Add(Constants.INTERVIEW_LIST_PAGE_INDEX, pageIndex);
            hashData.Add(Constants.INTERVIEW_LIST_ROW_COUNT, rowCount);

            Session[SessionKey.INTERVIEW_FILTER] = hashData;
        }

        /// <summary>
        /// Set History Session Filter
        /// </summary>
        /// <param name="name">string</param>
        /// <param name="source">string</param>
        /// <param name="result">string</param>
        /// <param name="position">string</param>
        /// <param name="dateFrom">string</param>
        /// <param name="dateTo">string</param>
        /// <param name="column">string</param>
        /// <param name="order">string</param>
        /// <param name="pageIndex">int</param>
        /// <param name="rowCount">int</param>
        private void SetHistorySessionFilter(string name, string source, string result, string position, string dateFrom, string dateTo,
            string column, string order, int pageIndex, int rowCount)
        {
            Hashtable hashData = new Hashtable();
            hashData.Add(Constants.INTERVIEW_LIST_HISTORY_NAME, name);
            hashData.Add(Constants.INTERVIEW_LIST_HISTORY_POSITION, position);
            hashData.Add(Constants.INTERVIEW_LIST_HISTORY_SOURCE, source);
            hashData.Add(Constants.INTERVIEW_LIST_HISTORY_RESULT, result);
            hashData.Add(Constants.INTERVIEW_LIST_HISTORY_DATE_FROM, dateFrom);
            hashData.Add(Constants.INTERVIEW_LIST_HISTORY_DATE_TO, dateTo);
            hashData.Add(Constants.INTERVIEW_LIST_HISTORY_COLUMN, column);
            hashData.Add(Constants.INTERVIEW_LIST_HISTORY_ORDER, order);
            hashData.Add(Constants.INTERVIEW_LIST_HISTORY_PAGE_INDEX, pageIndex);
            hashData.Add(Constants.INTERVIEW_LIST_HISTORY_ROW_COUNT, rowCount);

            Session[SessionKey.INTERVIEW_HISTORY_FILTER] = hashData;
        }

        /// <summary>
        /// Index action
        /// </summary>
        /// <returns>ActionResult</returns>
        [CrmAuthorizeAttribute(Module = Modules.Hiring, Rights = Permissions.Read)]
        public ActionResult Index()
        {
            InterviewStatusDao inStatusDao = new InterviewStatusDao();
            List<InterviewStatus> lStatus = inStatusDao.GetList();
            InterviewResultDao inResultDao = new InterviewResultDao();
            List<InterviewResult> lResult = inResultDao.GetList();
            Hashtable hashData = Session[SessionKey.INTERVIEW_FILTER] == null ? new Hashtable() : (Hashtable)Session[SessionKey.INTERVIEW_FILTER];

            ViewData[Constants.INTERVIEW_LIST_NAME] = hashData[Constants.INTERVIEW_LIST_NAME] == null ? Constants.CANDIDATE_NAME : !string.IsNullOrEmpty((string)hashData[Constants.INTERVIEW_LIST_NAME]) ? hashData[Constants.INTERVIEW_LIST_NAME] : Constants.CANDIDATE_NAME;
            ViewData[Constants.INTERVIEW_LIST_STATUS] = new SelectList(lStatus, "id", "name", hashData[Constants.INTERVIEW_LIST_STATUS] == null ? "" : hashData[Constants.INTERVIEW_LIST_STATUS].ToString());
            ViewData[Constants.INTERVIEW_LIST_RESULT] = new SelectList(lResult, "id", "Name", hashData[Constants.INTERVIEW_LIST_RESULT] == null ? "" : hashData[Constants.INTERVIEW_LIST_RESULT].ToString());
            ViewData[Constants.INTERVIEW_LIST_INTERVIEWER] = new SelectList(interviewDao.GetListInterviewedBy(), "pic", "pic", hashData[Constants.INTERVIEW_LIST_INTERVIEWER] == null ? "" : hashData[Constants.INTERVIEW_LIST_INTERVIEWER].ToString());            
            ViewData[Constants.INTERVIEW_LIST_DATE_FROM] = hashData[Constants.INTERVIEW_LIST_DATE_FROM] == null ? "" : hashData[Constants.INTERVIEW_LIST_DATE_FROM].ToString();
            ViewData[Constants.INTERVIEW_LIST_DATE_TO] = hashData[Constants.INTERVIEW_LIST_DATE_TO] == null ? "" : hashData[Constants.INTERVIEW_LIST_DATE_TO].ToString();
            ViewData[Constants.INTERVIEW_LIST_COLUMN] = hashData[Constants.INTERVIEW_LIST_COLUMN] == null ? "ID" : hashData[Constants.INTERVIEW_LIST_COLUMN].ToString();
            ViewData[Constants.INTERVIEW_LIST_ORDER] = hashData[Constants.INTERVIEW_LIST_ORDER] == null ? "desc" : hashData[Constants.INTERVIEW_LIST_ORDER].ToString();
            ViewData[Constants.INTERVIEW_LIST_PAGE_INDEX] = hashData[Constants.INTERVIEW_LIST_PAGE_INDEX] == null ? "1" : hashData[Constants.INTERVIEW_LIST_PAGE_INDEX].ToString();
            ViewData[Constants.INTERVIEW_LIST_ROW_COUNT] = hashData[Constants.INTERVIEW_LIST_ROW_COUNT] == null ? "20" : hashData[Constants.INTERVIEW_LIST_ROW_COUNT].ToString();
            
            Session[Constants.INTERVIEW_LIST_ACTION] = Constants.INTERVIEW_LIST_INDEX;
            return View();
        }

        public ActionResult Refresh(string id)
        {
            string view = string.Empty;
            switch (id)
            {
                case "2":
                    Session.Remove(SessionKey.INTERVIEW_HISTORY_FILTER);
                    view = "HistoryInterview";
                    break;
                default:
                    Session.Remove(SessionKey.INTERVIEW_FILTER);
                    view = "Index";
                    break;
            }
            return RedirectToAction(view);
        }

        /// <summary>
        /// Index action
        /// </summary>
        /// <returns>ActionResult</returns>
        [CrmAuthorizeAttribute(Module = Modules.Hiring, Rights = Permissions.Read)]
        public ActionResult HistoryInterview()
        {
            InterviewResultDao inStatusDao = new InterviewResultDao();
            List<InterviewResult> lStatus = inStatusDao.GetList();
            Hashtable hashData = Session[SessionKey.INTERVIEW_HISTORY_FILTER] == null ? new Hashtable() : (Hashtable)Session[SessionKey.INTERVIEW_HISTORY_FILTER];
            ViewData[Constants.INTERVIEW_LIST_HISTORY_NAME] = hashData[Constants.INTERVIEW_LIST_HISTORY_NAME] == null ? Constants.CANDIDATE_NAME : !string.IsNullOrEmpty((string)hashData[Constants.INTERVIEW_LIST_HISTORY_NAME]) ? hashData[Constants.INTERVIEW_LIST_HISTORY_NAME] : Constants.CANDIDATE_NAME;
            ViewData[Constants.INTERVIEW_LIST_HISTORY_RESULT] = new SelectList(lStatus, "id", "name", hashData[Constants.INTERVIEW_LIST_HISTORY_RESULT] == null ? "" : hashData[Constants.INTERVIEW_LIST_HISTORY_RESULT].ToString());
            ViewData[Constants.INTERVIEW_LIST_HISTORY_POSITION] = new SelectList(levelDao.GetList(), "ID", "DisplayName", hashData[Constants.INTERVIEW_LIST_HISTORY_POSITION] == null ? "" : hashData[Constants.INTERVIEW_LIST_HISTORY_POSITION].ToString());
            ViewData[Constants.INTERVIEW_LIST_HISTORY_SOURCE] = new SelectList(candidateDao.GetListSource(), "Value", "Text", hashData[Constants.INTERVIEW_LIST_HISTORY_SOURCE] == null ? "" : hashData[Constants.INTERVIEW_LIST_HISTORY_SOURCE].ToString());
            ViewData[Constants.INTERVIEW_LIST_HISTORY_DATE_FROM] = hashData[Constants.INTERVIEW_LIST_HISTORY_DATE_FROM] == null ? "" : (string)hashData[Constants.INTERVIEW_LIST_HISTORY_DATE_FROM];
            ViewData[Constants.INTERVIEW_LIST_HISTORY_DATE_TO] = hashData[Constants.INTERVIEW_LIST_HISTORY_DATE_TO] == null ? "" : (string)hashData[Constants.INTERVIEW_LIST_HISTORY_DATE_TO];
            Session[Constants.INTERVIEW_LIST_ACTION] = Constants.INTERVIEW_LIST_HISTORY;

            return View();
        }

      

        /// <summary>
        /// Get List JQGrid History
        /// </summary>
        /// <param name="can_name">string</param>
        /// <param name="status">string</param>
        /// <param name="position">string</param>
        /// <param name="from_date">string</param>
        /// <param name="to_date">string</param>
        /// <returns>ActionResult</returns>
        public ActionResult GetListJQGridHistory(string can_name, string source, string result, string position, string from_date, string to_date)
        {
            #region JQGrid Params
            string sortColumn = (Request.Params[GridConstants.SORT_COLUMN]).ToString();
            string sortOrder = (Request.Params[GridConstants.SORT_ORDER]).ToString();
            int pageIndex = Convert.ToInt32(Request.Params[GridConstants.PAGE_INDEX]);
            int rowCount = Convert.ToInt32(Request.Params[GridConstants.ROW_COUNT]);
            #endregion

            SetHistorySessionFilter(can_name, source, result, position, from_date, to_date, sortColumn, sortOrder, pageIndex, rowCount);

            #region search
            string candidate_name = String.Empty;
            int iStatus = 0;
            int iPosition = 0;
            DateTime? from = null;
            DateTime? to = null;
            if (can_name != Constants.CANDIDATE_NAME)
            {
                candidate_name = can_name;
            }
            if (!string.IsNullOrEmpty(result))
            {
                iStatus = int.Parse(result);
            }
            if (!string.IsNullOrEmpty(position))
            {
                iPosition = int.Parse(position);
            }
            try
            {
                if (!string.IsNullOrEmpty(from_date))
                {
                    from = DateTime.Parse(from_date);
                }

                if (!string.IsNullOrEmpty(to_date))
                {
                    to = DateTime.Parse(to_date);
                }
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.Write(e.Message);
            }

            #endregion

            List<sp_GetInterviewHistoryListResult> InterviewList = interviewDao.GetHistoryInterviewList(candidate_name, string.IsNullOrEmpty(source)? 0 : int.Parse(source),
                iStatus, iPosition, from, to);
            int totalRecords = InterviewList.Count();
            int totalPages = (int)Math.Ceiling((float)totalRecords / (float)rowCount);
            int currentPage = CommonFunc.SetJqGridPageIndex(totalRecords, pageIndex, rowCount);
            var finalList = interviewDao.SortHistoryInterview(InterviewList, sortColumn, sortOrder)
                                  .Skip((currentPage - 1) * rowCount)
                                   .Take(rowCount);
            
            Session[SessionKey.INTERVIEW_HISTORY_LIST] = InterviewList.Select(p => p.ID).ToList();

            int j = 0;

            var jsonData = new
            {
                total = totalPages,
                page = currentPage,
                records = totalRecords,
                rows = (
                    from m in finalList
                    select new
                    {

                        i = m.ID,
                        cell = new string[] {         
                                Convert.ToString(++j),
                                m.ID.ToString(),
                                TooltipLink(m.ID.ToString(),"/Interview/DetailHistory",m.DisplayName,"Detail","850"),                                
                                m.Email,                                
                                m.CellPhone,
                                m.Gender == Constants.MALE?"Male":"Female",
                                m.SearchDate.ToString(Constants.DATETIME_FORMAT_VIEW),
                                m.SourceName,
                                m.Title,
                                m.ResultName,
                                m.Note,
                                ((m.ResultId.Value != 1 && m.ResultId.Value != 4 )?  "<input type=\"button\" class=\"icon re_active\" title=\"Setup an interview\" onclick=\"window.location = '/Interview/Create/" + m.ID + "'\" />" + "&nbsp;":"")
                                    
                            }
                    }
                ).ToArray()
            };

            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

       

        /// <summary>
        /// Get List JQGrid
        /// </summary>
        /// <param name="can_name">string</param>
        /// <param name="status">string</param>
        /// <param name="interviewedBy">string</param>
        /// <param name="from_date">string</param>
        /// <param name="to_date">string</param>
        /// <returns>ActionResult</returns>
        public ActionResult GetListJQGrid(string can_name, string status, string result, string interviewedBy, string from_date, string to_date)
        {
            #region JQGrid Params
            string sortColumn = (Request.Params[GridConstants.SORT_COLUMN]).ToString();
            string sortOrder = (Request.Params[GridConstants.SORT_ORDER]).ToString();
            int pageIndex = Convert.ToInt32(Request.Params[GridConstants.PAGE_INDEX]);
            int rowCount = Convert.ToInt32(Request.Params[GridConstants.ROW_COUNT]);

            #endregion
            SetSessionFilter(can_name, status, result, interviewedBy, from_date, to_date, sortColumn, sortOrder, pageIndex, rowCount);
            #region search
            string candidate_name = String.Empty;
            int iStatus = 0;
            int iResult = 0;
            DateTime? from = null;
            DateTime? to = null;
            if (!string.IsNullOrEmpty(can_name) && !can_name.Equals(Constants.CANDIDATE_NAME))
            {
                candidate_name = can_name;
            }
            if (!string.IsNullOrEmpty(status))
            {
                iStatus = int.Parse(status);
            }
            if (!string.IsNullOrEmpty(result))
            {
                iResult = int.Parse(result);
            }

            try
            {
                if (!string.IsNullOrEmpty(from_date))
                {
                    from = DateTime.Parse(from_date);
                }

                if (!string.IsNullOrEmpty(to_date))
                {
                    to = DateTime.Parse(to_date);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Write(ex.Message);
            }
            #endregion

            List<sp_GetInterviewListResult> InterviewList = interviewDao.GetInterviewList(candidate_name, iStatus,
                    iResult, interviewedBy, from, to);
            int totalRecords = InterviewList.Count();
            int totalPages = (int)Math.Ceiling((float)totalRecords / (float)rowCount);
            int currentPage = CommonFunc.SetJqGridPageIndex(totalRecords, pageIndex, rowCount);
            var finalList = interviewDao.Sort(InterviewList, sortColumn, sortOrder)
                                  .Skip((currentPage - 1) * rowCount)
                                   .Take(rowCount);
            // Save session to navigation
            Session[SessionKey.INTERVIEW_LIST] = finalList.Select(p => p.ID).ToList();

            var jsonData = new
            {
                total = totalPages,
                page = currentPage,
                records = totalRecords,
                rows = (
                    from m in finalList
                    select new
                    {

                        i = m.ID,
                        cell = new string[] {                                
                                m.Dept,
                                "<a href='JobRequest/Detail/" + requestItemDao.GetByID(m.JRId).JobRequest.ID.ToString() + "'>" +
                                m.JRId + "<a/>",
                                TooltipLink(m.ID.ToString(),"/Interview/Detail",m.DisplayName,"Detail","900"),
                                m.SubDept,
                                m.Position,                                
                                m.Status,
                                m.ResultName,
                                m.Pic.TrimEnd(';'),
                                m.InterviewDate.Value.ToString(Constants.DATETIME_FORMAT_TIME),
                                m.Venue, 
                                CommonFunc.Link("linkInterview"+m.InterviewId.ToString(),"javascript:CRM.popup(\"/Interview/SendCandidateMail/?ids=" + m.InterviewId.ToString() + "&page=\",\"Email to Candidate\", 860);",(m.IsSentMailCandidate != false?"Yes":"No"),false),
                                CommonFunc.Link("linkCandidate"+m.InterviewId.ToString(),"javascript:CRM.popup(\"/Interview/SendInterviewMail/?ids=" + m.InterviewId.ToString() + "&page=\",\"Send Meeting Request\", 860);",(m.IsSendMailInterviewer != false?"Yes":"No"),false),                                     
                                SetAction(m.MaxRound == m.round, m.ResultId != null?(Constants.InterviewResult) m.ResultId: Constants.InterviewResult.Null, 
                                    m.ID.ToString(), m.InterviewId.ToString(), m.round.ToString(), m.Pic, m.CCList, m.RoundInterviewed)
                            }
                    }
                ).ToArray()
            };

            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Compare two interview 
        /// </summary>
        /// <param name="a">Interview</param>
        /// <param name="b">Interview</param>
        /// <returns>bool</returns>
        private bool CompareInterview(Interview a, Interview b)
        {
            return (a.CandidateId.Equals(b.CandidateId)
                && a.InterviewDate.Equals(b.InterviewDate)
                && a.Venue.Equals(b.Venue)
                && a.InterviewFormId.Equals(b.InterviewFormId)
                && a.InterviewResultId.Equals(b.InterviewResultId)
                && (a.Content != null ? a.Content.Equals(b.Content):true)
                && a.Pic.Equals(b.Pic));
        }

        /// <summary>
        /// Navigation
        /// </summary>
        /// <param name="name">string</param>
        /// <param name="id">int</param>
        /// <returns>ActionResult</returns>
        public ActionResult Navigation(string name, int id)
        {
            List<sp_GetInterviewListResult> interviewList;
            if (Session[SessionKey.STT_LIST] != null)
            {
                interviewList = (List<sp_GetInterviewListResult>)Session[SessionKey.STT_LIST];
            }
            else
            {
                interviewList = interviewDao.GetInterviewList("", 0, 0, "", null, null);
            }
            int testID = 0;
            int index = 0;
            string url = string.Empty;
            switch (name)
            {
                case "First":
                    testID = interviewList[0].ID;
                    break;
                case "Prev":
                    index = interviewList.IndexOf(interviewList.Where(p => p.ID == id).FirstOrDefault<sp_GetInterviewListResult>());
                    if (index != 0)
                    {
                        testID = interviewList[index - 1].ID;
                    }
                    else
                    {
                        testID = id;
                    }
                    break;
                case "Next":
                    index = interviewList.IndexOf(interviewList.Where(p => p.ID == id).FirstOrDefault<sp_GetInterviewListResult>());
                    if (index != interviewList.Count - 1)
                    {
                        testID = interviewList[index + 1].ID;
                    }
                    else
                    {
                        testID = id;
                    }
                    break;
                case "Last":
                    testID = interviewList[interviewList.Count - 1].ID;
                    break;

            }

            return RedirectToAction("Detail/" + testID);
        }

        /// <summary>
        /// Set action
        /// </summary>
        /// <param name="isMaxRound">is max round</param>
        /// <param name="status">interview status</param>
        /// <param name="id">candidate id</param>
        /// <param name="interview_id">interview id</param>
        /// <param name="round">current round</param>
        /// <param name="interviewBy">interviewer</param>
        /// <param name="ccList">CC list</param>
        /// <param name="roundInterviewed">current interviewed round</param>
        /// <returns>string</returns>
        private string SetAction(bool isMaxRound, Constants.InterviewResult status, string id, string interview_id, string round, string interviewBy, string ccList, int? roundInterviewed)
        {
            string value = string.Empty;
            var principal = HttpContext.User as AuthenticationProjectPrincipal;
            Interview aInter = interviewDao.GetByCandidateRound(ConvertUtil.ConvertToInt(id), ConvertUtil.ConvertToInt(roundInterviewed));
            bool isInCClist1 = false;
            bool isInCClist2 = false;
            bool plus = false;  
            if (ccList != null)
            {
                if (ccList.Split(Constants.SEPARATE_CC_LIST).Contains(principal.UserData.UserName.ToLower()))
                {
                    isInCClist1 = true;
                }

            }
            if (aInter != null && aInter.CClist != null)
            {
                if (aInter.CClist.Split(Constants.SEPARATE_CC_LIST).Contains(principal.UserData.UserName.ToLower()))
                {
                    isInCClist2 = true;
                }

            }
            
            
            switch (status)
            {
                case Constants.InterviewResult.Passes:
                    value = "<input type=\"button\" class=\"icon edit\" title=\"Edit\" onclick=\"window.location = '/Interview/Edit/" + id + "'\" />" + "&nbsp;";
                    if (aInter != null)
                    {
                        if (aInter.Pic.Split(Constants.SEPARATE_CC_LIST).Contains(principal.UserData.UserName) || isInCClist2)
                            value += "<input type=\"button\" class=\"icon edit_result\" title=\"Edit Interview Result\" onclick=\"window.location= '/Interview/EditResult/" + aInter.Id + "';\" />" + "&nbsp;";
                        else                        
                            plus = true;
                        
                    }
                    else
                    {
                        plus = true;
                    }

                    if (isMaxRound)
                    {
                        value += "<input type=\"button\" class=\"icon promote\" title=\"Transfer to Employee Profile\" onclick=\"window.location='/Interview/ToEmployee/" + id + "'\" />" + "&nbsp;";
                        value += CommonFunc.Button("icon promotestt", "Transfer to STT Profile", "window.location='/Interview/ToSTT/" + id + "'");
                    }
                    else
                    {
                        value += CommonFunc.Button("clpointer", "", "");
                        value += CommonFunc.Button("clpointer", "", "");
                    }
                    if(plus)
                         value += CommonFunc.Button("clpointer", "", "");

                    break;
                case Constants.InterviewResult.Fails:
                    if (aInter != null)
                    {
                        if (aInter.Pic.Split(Constants.SEPARATE_CC_LIST).Contains(principal.UserData.UserName) || isInCClist2)
                            value = "<input type=\"button\" class=\"icon edit_result\" title=\"Edit Interview Result\" onclick=\"window.location= '/Interview/EditResult/" + aInter.Id + "';\" />" + "&nbsp;";
                        else
                            plus = true;
                    }
                    else
                        plus = true;
                    
                    value += CommonFunc.Button("history", "Transfer to history list", "CRM.msgConfirmBox('Are you sure you want to transfer this candidate to Interview History List?','500','javascript:ToHistory(" + interview_id + ");')");
                    value += CommonFunc.Button("clpointer", "", "") + CommonFunc.Button("clpointer", "", "");
                    if (plus)
                        value += CommonFunc.Button("clpointer", "", "");        
                    break;
                case Constants.InterviewResult.WaitingList:
                    bool plus2 = false;                  
                    value = "<input type=\"button\" class=\"icon edit\" title=\"Edit\" onclick=\"window.location = '/Interview/Edit/" + id + "'\" />" + "&nbsp;";
                    //if (aInter != null)
                    //{
                    //    if (aInter.Pic.Split(Constants.SEPARATE_CC_LIST).Contains(principal.UserData.UserName) || isInCClist2)
                    //        value += "<input type=\"button\" class=\"icon edit_result\" title=\"Edit Interview Result\" onclick=\"window.location= '/Interview/EditResult/" + aInter.Id + "';\" />" + "&nbsp;";
                    //    else
                    //        plus = true;
                    //}
                    //else
                    //    plus = true;
                    if (interviewBy.Split(Constants.SEPARATE_CC_LIST).Contains(principal.UserData.UserName) || isInCClist1)
                    {
                        value += "<input type=\"button\" class=\"icon result\" title=\"Interview Result\" onclick=\"window.location='/Interview/ResultForm/" + interview_id + "';\" />" + "&nbsp;";
                    }
                    else
                        plus2 = true;  
                    value += CommonFunc.Button("history", "Transfer to History List", "CRM.msgConfirmBox('Are you sure you want to transfer this candidate to Interview History List?','500','javascript:ToHistory(" + interview_id + ");')");
                    if (plus)
                        value += CommonFunc.Button("clpointer", "", "");
                    if (plus2)
                        value += CommonFunc.Button("clpointer", "", "");      
                    break;
                case Constants.InterviewResult.Recruit:
                    if (aInter != null)
                    {
                        if (aInter.Pic.Split(Constants.SEPARATE_CC_LIST).Contains(principal.UserData.UserName) || isInCClist2)
                            value = "<input type=\"button\" class=\"icon edit_result\" title=\"Edit Interview Result\" onclick=\"window.location= '/Interview/EditResult/" + aInter.Id + "';\" />" + "&nbsp;";
                        else
                            plus = true;
                    }
                    else
                        plus = true;
                    value += "<input type=\"button\" class=\"icon promote\" title=\"Transfer to Employee Profile\" onclick=\"window.location='/Interview/ToEmployee/" + id + "'\" />" + "&nbsp;";
                    value += CommonFunc.Button("icon promotestt", "Transfer to STT Profile", "window.location='/Interview/ToSTT/" + id + "'");
                    value+= CommonFunc.Button("clpointer", "", "");        
                    if (plus)
                        value += CommonFunc.Button("clpointer", "", "");        
                    break;
                default:
                    value = "<input type=\"button\" class=\"icon edit\" title=\"Edit\" onclick=\"window.location = '/Interview/Edit/" + id + "'\" />" + "&nbsp;";
                    if (aInter != null)
                    {
                        if (aInter.Pic.Split(Constants.SEPARATE_CC_LIST).Contains(principal.UserData.UserName) || isInCClist2)
                            value += "<input type=\"button\" class=\"icon edit_result\" title=\"Edit Interview Result\" onclick=\"window.location= '/Interview/EditResult/" + aInter.Id + "';\" />" + "&nbsp;";
                        else
                            plus = true;
                    }
                    else
                        plus = true;

                    if (interviewBy.Split(Constants.SEPARATE_CC_LIST).Contains(principal.UserData.UserName) || isInCClist1)
                    {
                        value += "<input type=\"button\" class=\"icon result\" title=\"Interview Result\" onclick=\"window.location= '/Interview/ResultForm/" + interview_id + "';\" />" + "&nbsp;";
                    }
                    else 
                        value += CommonFunc.Button("clpointer", "", "");
                    value += CommonFunc.Button("clpointer", "", "");        
                    if(plus)
                        value += CommonFunc.Button("clpointer", "", "");        
                    break;
            }

            return value;
        }

        /// <summary>
        /// TooltipLink
        /// </summary>
        /// <param name="id">string</param>
        /// <param name="actionUrl">string</param>
        /// <param name="textLink">string</param>
        /// <param name="tilte">string</param>
        /// <param name="width">string</param>
        /// <returns>string</returns>
        private string TooltipLink(string id, string actionUrl, string textLink, string tilte, string width)
        {
            string onclick = "(window.location='" + actionUrl + "/" + id + "');";

            return "<a id=" + id + " href=javascript:void(0); class='showTooltip' onclick=" + onclick + ">" + textLink + "</a>";

        }

        /// <summary>
        /// Transfer to history
        /// </summary>
        /// <param name="id">int</param>
        /// <returns>ActionResult</returns>
        [CrmAuthorizeAttribute(Module = Modules.Hiring, Rights = Permissions.Update)]
        public ActionResult TransferToHistory(int id)
        {
            Message msg = null;
            Interview inter = interviewDao.GetById(id.ToString());
            if (inter == null)
            {
                msg = new Message(MessageConstants.E0007, MessageType.Error);
                ShowMessage(msg);
                return RedirectToAction("Index");
            }

            var principal = HttpContext.User as AuthenticationProjectPrincipal;
            Candidate can = interviewDao.GetCandidateById(inter.CandidateId.ToString());
            can.UpdatedBy = principal.UserData.UserName;
            msg = interviewDao.SaveToHistory(can);
            CandidateStatus status = CandidateStatus.Interviewing;
            if (inter.InterviewResultId == 2)
                status = CandidateStatus.Failed;
            else if (inter.InterviewResultId == 3)
                status = CandidateStatus.Waiting;
            candidateDao.UpdateStatus(status, (int)inter.CandidateId);
            // Update status of candidate            
            ShowMessage(msg);
            return RedirectToAction("Index");

        }

        /// <summary>
        /// Export to excel
        /// </summary>
        /// <param name="can_name">string</param>
        /// <param name="status">string</param>
        /// <param name="interviewedBy">string</param>
        /// <param name="from_date">string</param>
        /// <param name="to_date">string</param>
        /// <returns>ActionResult</returns>
        [CrmAuthorizeAttribute(Module = Modules.Hiring, Rights = Permissions.Read)]
        public ActionResult ExportToExcel(string can_name, string status, string result, string interviewedBy, string from_date, string to_date)
        {
            try
            {
                #region search
                string candidate_name = String.Empty;
                int iStatus = 0;
                int iResult = 0;
                DateTime? from = null;
                DateTime? to = null;
                if (!string.IsNullOrEmpty(can_name))
                {
                    candidate_name = can_name;
                }
                if (!string.IsNullOrEmpty(status))
                {
                    iStatus = int.Parse(status);
                }
                if (!string.IsNullOrEmpty(result))
                {
                    iResult = int.Parse(result);
                }
                if (!string.IsNullOrEmpty(from_date))
                {
                    from = DateTime.Parse(from_date);
                }

                if (!string.IsNullOrEmpty(to_date))
                {
                    to = DateTime.Parse(to_date);
                }

                #endregion
                List<sp_GetInterviewListResult> InterviewList = interviewDao.GetInterviewList(candidate_name, iStatus, iResult, interviewedBy, from, to);
                Hashtable hashData = (Hashtable)Session[SessionKey.INTERVIEW_FILTER];
                string column = !string.IsNullOrEmpty((string)hashData[Constants.INTERVIEW_LIST_COLUMN]) ?
                    (string)hashData[Constants.INTERVIEW_LIST_COLUMN] : "ID";
                string order = !string.IsNullOrEmpty((string)hashData[Constants.INTERVIEW_LIST_ORDER]) ?
                    (string)hashData[Constants.INTERVIEW_LIST_ORDER] : "desc";

                var finalList = interviewDao.Sort(InterviewList, column, order);
                ExportExcel exp = new ExportExcel();
                exp.Title = Constants.INTERVIEW_TILE_EXPORT_EXCEL;
                exp.FileName = Constants.INTERVIEW_EXPORT_EXCEL_NAME;
                exp.ColumnList = new string[] { "JRId", "DisplayNameExport", "Dept", "Position", "Status", "ResultName", "Pic", "InterviewDate:DateTime", "Venue", "IsSentMailCandidate:ActionSendMail", "IsSendMailInterviewer:ActionSendMail" };
                exp.HeaderExcel = new string[] { "JR", "Fullname", "Sub Dept", "Position", "Status", "Result", "Interviewed By", "Time", "Venue", "Email to Candidate", "Send Meeting Request" };
                exp.List = finalList;
                exp.IsRenderNo = true;
                exp.Execute();
                
            }
            catch
            {
                return View();
            }

            return View();
        }

        /// <summary>
        /// Export to excel
        /// </summary>
        /// <param name="can_name">string</param>
        /// <param name="status">string</param>
        /// <param name="interviewedBy">string</param>
        /// <param name="from_date">string</param>
        /// <param name="to_date">string</param>
        /// <returns>ActionResult</returns>
        [CrmAuthorizeAttribute(Module = Modules.Hiring, Rights = Permissions.Read)]
        public ActionResult ExportHistoryToExcel(string can_name, string source, string position, string result, string from_date, string to_date)
        {
            try
            {
                #region search
                string candidate_name = String.Empty;
                int iStatus = 0;
                int iPosition = 0;
                DateTime? from = null;
                DateTime? to = null;
                if (can_name != Constants.CANDIDATE_NAME)
                {
                    candidate_name = can_name;
                }
                if (!string.IsNullOrEmpty(result))
                {
                    iStatus = int.Parse(result);
                }
                if (!string.IsNullOrEmpty(position))
                {
                    iPosition = int.Parse(position);
                }
                if (!string.IsNullOrEmpty(from_date))
                {
                    from = DateTime.Parse(from_date);
                }

                if (!string.IsNullOrEmpty(to_date))
                {
                    to = DateTime.Parse(to_date);
                }

                #endregion
                List<sp_GetInterviewHistoryListResult> InterviewList = interviewDao.GetHistoryInterviewList(candidate_name, string.IsNullOrEmpty(source)? 0 : int.Parse(source),
                        iStatus, iPosition, from, to);
                Hashtable hashData = (Hashtable)Session[SessionKey.INTERVIEW_HISTORY_FILTER];
                string column = !string.IsNullOrEmpty((string)hashData[Constants.INTERVIEW_LIST_HISTORY_COLUMN]) ?
                    (string)hashData[Constants.INTERVIEW_LIST_HISTORY_COLUMN] : "ID";
                string order = !string.IsNullOrEmpty((string)hashData[Constants.INTERVIEW_LIST_HISTORY_ORDER]) ?
                    (string)hashData[Constants.INTERVIEW_LIST_HISTORY_ORDER] : "desc";

                var finalList = interviewDao.SortHistoryInterview(InterviewList, column, order);

                ExportExcel exp = new ExportExcel();
                exp.Title = Constants.INTERVIEW_HISTORY_EXPORT_EXCEL_NAME;
                exp.FileName = Constants.INTERVIEW_HISTORY_TILE_EXPORT_EXCEL;
                exp.ColumnList = new string[] { "DisplayNameExport", "Email", "CellPhone", "Gender:Gender", "SearchDate:DateTime", "SourceName", "Title", "ResultName", "Note" };
                exp.HeaderExcel = new string[] { "Full Name", "Email", "Phone", "Search By", "Gender", "Source", "Position", "Result", "Note" };
                exp.List = finalList;
                exp.IsRenderNo = true;
                
                exp.Execute();
            }
            catch
            {
                return View();
            }

            return View();
        }

        public JsonResult Complete()
        {
            var name = Request["q"];
            List<sp_GetJRForAdminResult> listUserAdminRoleInt = new JRAdminDao().GetListByName(name, Constants.WORK_FLOW_JOB_REQUEST, 0);
            List<string> listStringNameI = listUserAdminRoleInt.Select(q => q.UserName).Distinct().ToList();
            List<ListItem> list = new List<ListItem>();
            foreach (string item in listStringNameI)
            {
                Employee objEmployee = new EmployeeDao().GetByOfficeEmailInActiveList(item + Constants.PREFIX_EMAIL_LOGIGEAR);
                string displayName = item;
                if (objEmployee != null)
                {
                    displayName += " - " + objEmployee.ID;

                  list.Add(new ListItem() { Value = displayName, Text = displayName }) ;
                }
                //context.Response.Write(displayName + Environment.NewLine);
            }
            return this.Json(list,JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// Delete 
        /// </summary>
        /// <param name="id">string</param>
        /// <returns>ActionResult</returns>
        [CrmAuthorizeAttribute(Module = Modules.Hiring, Rights = Permissions.Delete, ShowAtCurrentPage = true)]
        public JsonResult Delete(string id)
        {
            var principal = HttpContext.User as AuthenticationProjectPrincipal;
            interviewDao.Delete(id, principal.UserData.UserName);
            JsonResult result = new JsonResult();
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            result.Data = true;
            return result;
        }
        #endregion

        #region "Interview email notification"
        public ActionResult SendInterviewMail(string ids,string page)
        {

            Interview interview = interviewDao.GetById(ids);
            ViewData["FromHour"] = new SelectList(Constants.HourList, "Value", "Text", interview.InterviewDate.Value.Hour.ToString());
            ViewData["FromMinute"] = new SelectList(Constants.MinuteList, "Value", "Text", interview.InterviewDate.Value.Minute.ToString());



            ViewData["ToHour"] = new SelectList(Constants.HourList, "Value", "Text", interview.InterviewDate.Value.Hour.ToString());
            ViewData["ToMinute"] = new SelectList(Constants.MinuteList, "Value", "Text", interview.InterviewDate.Value.Minute);

            ViewData["interview"] = interview;
            string tmpFilePath = Server.MapPath(Constants.HTML_TEMPLATE_PATH_INTERVIEW_MEETING);
            string temp = string.Empty;
            if (System.IO.File.Exists(tmpFilePath))
            {

                string host = Request.Url.Host;
                string port = Request.Url.Port.ToString();
                string host_port = "";
                if (port == "80" || port == "")
                    host_port = host;
                else
                    host_port = host + ":" + port;

                temp = System.IO.File.ReadAllText(tmpFilePath);
                temp = temp.Replace(Constants.CONTENT_HOST, host_port);
                temp = temp.Replace(Constants.CONTENT_ID, interview.CandidateId.ToString());
                temp = temp.Replace(Constants.CONTENT_DATE, interview.InterviewDate.Value.ToString(Constants.DATETIME_FORMAT_VIEW));
                temp = temp.Replace(Constants.CONTENT_TIME, interview.InterviewDate.Value.ToShortTimeString());
                temp = temp.Replace(Constants.CONTENT_LOCATION, interview.Venue);
                temp = temp.Replace(Constants.CONTENT_CANDIDATE, interview.Candidate.FirstName + " " + 
                    interview.Candidate.MiddleName + " " + interview.Candidate.LastName);
                string pos = candidateDao.GetById(interview.CandidateId.ToString()).JobTitleLevel.DisplayName;
                temp = temp.Replace(Constants.CONTENT_POSITION, pos);
                temp = temp.Replace(Constants.CONTENT_INTERVIEW, interview.Pic);
            }
            ViewData["template"] = temp;
            ViewData["Page"] = page;
            return View();
        }

        [ValidateInput(false)]
        [HttpPost]
        public ActionResult SendInterviewMail(FormCollection form)
        {

            var principal = HttpContext.User as AuthenticationProjectPrincipal;
            string id = Request["ID"];
            interviewDao.SetSendMailToInterView(id, principal.UserData.UserName);
            string from = CommonFunc.GetEmailByLoginName(principal.UserData.UserName);
            string fromName = CommonFunc.GetDomainUser(principal.UserData.UserName).Properties["displayName"][0].ToString();
                        
            string[] toName = form.Get("To").Trim().Split(';');
            string toMail="";
            foreach (string name in toName)
            {
                if(!String.IsNullOrWhiteSpace(name))
                    toMail += CommonFunc.GetEmailByLoginName(name) + ";";
            }
            string start = form.Get("Date") + " " + form.Get("FromHour") + ":" + form.Get("FromMinute") + ":00";
            string end = form.Get("Date") + " " + form.Get("ToHour") + ":" + form.Get("ToMinute") + ":00";

            bool result = CommonFunc.SendMeetingRequest(start, end, form.Get("Location"), from, fromName, form.Get("Subject"), form.Get("body"), toMail);
            Message msg;
            if (result)
            {
                msg = new Message(MessageConstants.I0002, MessageType.Info);
            }
            else
            {
                msg = new Message(MessageConstants.E0032, MessageType.Error);
            }
            ShowMessage(msg);
            string page = Request["Page"];
            if(!string.IsNullOrEmpty(page))
            {
                return RedirectToAction(page);
            }
            else
                return RedirectToAction("Index");

        }
                
        #endregion

        #region "Interview email to candidate"
        private string getTemplateHtml(string tmpFilePath)
        {
            string temp = "";
            if (System.IO.File.Exists(tmpFilePath))
            {
                temp = System.IO.File.ReadAllText(tmpFilePath);
            }
            return temp;
        }
        public ActionResult SendCandidateMail(string ids,string page)
        {
            Interview interview = interviewDao.GetById(ids);
            ViewData["interview"] = interview;
            string tmpFilePath = ""; 
            string temp = string.Empty;

            Constants.InterviewResult result = Constants.InterviewResult.Null;
            if (interview.InterviewResultId != null)
            {
                result = (Constants.InterviewResult)interview.InterviewResultId;
            }

            //Constants.InterviewResult result = (Constants.InterviewResult)interview.InterviewResultId;
            
            switch (result)
            {
                case Constants.InterviewResult.Fails:
                    tmpFilePath = Server.MapPath(Constants.HTML_TEMPLATE_PATH_FAIL_CANDIDATE_MAIL);
                    temp = getTemplateHtml(tmpFilePath);
                    
                    break;
                case Constants.InterviewResult.Passes:
                    tmpFilePath = Server.MapPath(Constants.HTML_TEMPLATE_PATH_PASS_CANDIDATE_MAIL);
                    temp = getTemplateHtml(tmpFilePath);
                    break;
                case Constants.InterviewResult.Recruit:
                    tmpFilePath = Server.MapPath(Constants.HTML_TEMPLATE_PATH_PASSED_CANDIDATE_MAIL);
                    temp = getTemplateHtml(tmpFilePath);
                    break;
                default:
                    tmpFilePath = Server.MapPath(Constants.HTML_TEMPLATE_PATH_CANDIDATE_MAIL);
                    if (System.IO.File.Exists(tmpFilePath))
                    {

                        string host = Request.Url.Host;
                        string port = Request.Url.Port.ToString();
                        string host_port = "";
                        if (port == "80" || port == "")
                            host_port = host;
                        else
                            host_port = host + ":" + port;

                        temp = System.IO.File.ReadAllText(tmpFilePath);
                        temp = temp.Replace(Constants.CONTENT_HOST, host_port);
                        temp = temp.Replace(Constants.CONTENT_ID, interview.CandidateId.ToString());
                        temp = temp.Replace(Constants.CONTENT_DATE, interview.InterviewDate.Value.ToString(Constants.DATETIME_FORMAT_VIEW));
                        temp = temp.Replace(Constants.CONTENT_TIME, interview.InterviewDate.Value.ToShortTimeString());
                        temp = temp.Replace(Constants.CONTENT_LOCATION, interview.Venue);
                        temp = temp.Replace(Constants.CONTENT_CANDIDATE, interview.Candidate.FirstName + " " + interview.Candidate.MiddleName + " " + interview.Candidate.LastName);
                        string pos = candidateDao.GetById(interview.CandidateId.ToString()).JobTitleLevel.DisplayName;
                        temp = temp.Replace(Constants.CONTENT_POSITION, pos);
                        temp = temp.Replace(Constants.CONTENT_INTERVIEW, interview.Pic);
                    }
                    break;
            }



            ViewData["template"] = temp.Replace("[@Name]", interview.Candidate.FirstName + " " + interview.Candidate.MiddleName + " " + interview.Candidate.LastName);
            ViewData["Page"] = page;
            return View();
        }

        [ValidateInput(false)]
        [HttpPost]
        public ActionResult SendCandidateMail(FormCollection form)
        {
            string host = ConfigurationManager.AppSettings["mailserver_host"];
            string port = ConfigurationManager.AppSettings["mailserver_port"];

            var principal = HttpContext.User as AuthenticationProjectPrincipal;
            string id = Request["ID"];
            if (!string.IsNullOrEmpty(id))
            {
                interviewDao.SetSendMailToCandidate(id, principal.UserData.UserName);
                string from = CommonFunc.GetEmailByLoginName(principal.UserData.UserName);
                string fromName = CommonFunc.GetDomainUser(principal.UserData.UserName).Properties["displayName"][0].ToString();

                string[] toName = form.Get("To").Trim().Split(';');
                string toMail = "";
                foreach (string name in toName)
                {
                    if (!String.IsNullOrWhiteSpace(name))
                        toMail += name + ";";
                }

                string[] ccName = form.Get("CC").Trim().Split(';');
                string ccMail = "";
                foreach (string name in ccName)
                {
                    if (!String.IsNullOrWhiteSpace(name))
                        ccMail += CommonFunc.GetEmailByLoginName(name) + ";";
                }
                string body = form.Get("body");
                bool result = WebUtils.SendMail(host, port, from, fromName, toMail, ccMail, form.Get("Subject"), body);
                Message msg = null;
                if (result)
                {
                    msg = new Message(MessageConstants.I0002, MessageType.Info);
                }
                else
                {
                    msg = new Message(MessageConstants.E0032, MessageType.Error);
                }

                ShowMessage(msg);
            }
            string page = Request["Page"];
            if (!string.IsNullOrEmpty(page))
            {
                return RedirectToAction(page);
            }
            else
                return RedirectToAction("Index");
        }

        #endregion
    }
}
