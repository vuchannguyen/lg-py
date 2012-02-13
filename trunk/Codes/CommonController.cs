using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;
using System.Web.Mvc;
using System.Text.RegularExpressions;
using CRM.Library.Common;
using System.IO;
using CRM.Models;
using System.Configuration;
using CRM.Areas.Portal.Models;

namespace CRM.Controllers
{
    public class CommonController : BaseController
    {
        //
        // GET: /Common/
        #region variable
        private TrainingMaterialDao materialDao = new TrainingMaterialDao();
        private CandidateDao candidateDao = new CandidateDao();
        private STTDao sttDao = new STTDao();
        private EmployeeDao empDao = new EmployeeDao();
        private JobRequestDao requestDao = new JobRequestDao();
        private PTODao ptoDao = new PTODao();
        private LocationDao locationDao = new LocationDao();
        private MenuDao menuDao = new MenuDao();
        private ModulePermissonDao mpDao = new ModulePermissonDao();
        SRStatusDao statusDao = new SRStatusDao();
        SRCategoryDao catDao = new SRCategoryDao();
        UserAdminDao userDao = new UserAdminDao();
        TrainingCertificationDao trainingCerDao = new TrainingCertificationDao();
        #endregion
        String saveTo = String.Empty;

        public JsonResult Test(string JSONstring)
        {
            JsonResult result = new JsonResult();
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            Session.Remove("header");
            return result;
        }



        public ActionResult Index()
        {
            
            return View();
        }
        public ActionResult PageNotFound()
        {            
            return View();
        }

        public ActionResult UnknowError()
        {
            return View();
        }

        private bool checkAuthorize()
        {
            bool isAuthorize = false;
            try
            {
                if (!string.IsNullOrEmpty(Request.QueryString["controller"]))
                {
                    string controller = Request.QueryString["controller"];
                    var principal = HttpContext.User as AuthenticationProjectPrincipal;
                    
                    switch (controller)
                    {
                        case "Candidate":
                            isAuthorize = CommonFunc.CheckAuthorized(principal.UserData.UserID, (int)Modules.Candidate, (int)Permissions.Insert);
                            break;
                        case "STT":
                            isAuthorize = CommonFunc.CheckAuthorized(principal.UserData.UserID, (int)Modules.STT, (int)Permissions.Insert);
                            break;
                        case "Employee":
                            isAuthorize = CommonFunc.CheckAuthorized(principal.UserData.UserID, (int)Modules.Employee, (int)Permissions.Insert);
                            break;
                    }
                }
            }
            catch
            {
                isAuthorize = false;
            }
            return isAuthorize;
        }

        #region Upload File

        public ActionResult UploadImage()
        {
            Message msg = null;
            if (!checkAuthorize())
            {
                msg = new Message(MessageConstants.E0002, MessageType.Error);
                TempData["Message"] = msg;
                return View("ErrorPage");
            }
            return View();
        }

        public ActionResult UploadFile()
        {
            Message msg = null;
            if (!checkAuthorize())
            {
                msg = new Message(MessageConstants.E0002, MessageType.Error);
                TempData["Message"] = msg;
                return View("ErrorPage");
            }
            return View();
        }

        [HttpPost]
        public FileUploadJsonResult UploadImage(FormCollection form)
        {
            int max_size = int.Parse(ConfigurationManager.AppSettings["IMAGE_MAX_SIZE"]);

            saveTo = form["path"].Replace("/undefined", "");
            string recordID = form["recordID"];
            string img = form["value"];
            string controller = form["controller"];
            var principal = HttpContext.User as AuthenticationProjectPrincipal;
            FileUploadJsonResult result = new FileUploadJsonResult();
            Message msg = null;
            foreach (string file in Request.Files)
            {
                string uniqueId = DateTime.Now.ToString("MMddyyyyhhmmss");
                HttpPostedFileBase hpf = Request.Files[file] as HttpPostedFileBase;

                if (hpf.ContentLength != 0)
                {
                    int maxSize = 1024 * 1024 * Constants.IMAGE_MAX_SIZE;
                    //int maxSize = 1024 * 1024 * 2;
                    float sizeFile = float.Parse(hpf.ContentLength.ToString("N"));

                    string extension = Path.GetExtension(hpf.FileName);
                    string[] extAllowList = Constants.IMAGE_EXT_ALLOW.Split(',');
                    if (!extAllowList.Contains(extension.ToLower())) //check extension file is valid
                    {
                        msg = new Message(MessageConstants.E0013, MessageType.Error, extension, Constants.IMAGE_EXT_ALLOW, Constants.IMAGE_MAX_SIZE);
                        result.Data = new { msg };
                    }
                    else if (sizeFile > maxSize) //check maxlength of uploaded file
                    {
                        msg = new Message(MessageConstants.E0012, MessageType.Error, Constants.IMAGE_MAX_SIZE.ToString());
                        result.Data = new { msg };
                    }
                    else  //successful case
                    {

                        string strPath = string.IsNullOrEmpty(recordID) == true ? Server.MapPath(Constants.UPLOAD_TEMP_PATH) : Server.MapPath(saveTo);
                        string imageName = uniqueId + "_" + Path.GetFileName(hpf.FileName);
                        imageName = ConvertUtil.FormatFileName(imageName);
                        hpf.SaveAs(strPath + imageName);
                        if (!string.IsNullOrEmpty(recordID))
                        {
                            switch (controller)
                            {
                                case Constants.CANDIDATE_DEFAULT_VALUE:
                                    Candidate candidateObj = candidateDao.GetById(recordID);
                                    if (candidateObj != null)
                                    {
                                        string oldImgFile = !string.IsNullOrEmpty(candidateObj.Photograph) ? candidateObj.Photograph : string.Empty;
                                        candidateObj.Photograph = imageName;
                                        candidateObj.UpdatedBy = principal.UserData.UserName;
                                        msg = candidateDao.UpdateImage(candidateObj);
                                    }
                                    break;
                                case Constants.STT_DEFAULT_VALUE:
                                    STT sttObj = sttDao.GetById(recordID);
                                    if (sttObj != null)
                                    {
                                        string oldImgFile = !string.IsNullOrEmpty(sttObj.Photograph) ? sttObj.Photograph : string.Empty;
                                        sttObj.Photograph = imageName;
                                        sttObj.UpdatedBy = principal.UserData.UserName;
                                        msg = sttDao.UpdateImage(sttObj);
                                    }
                                    break;
                                case Constants.EMPLOYEE_DEFAULT_VALUE:
                                    Employee emp = empDao.GetById(recordID);
                                    if (emp != null)
                                    {
                                        string oldImgFile = !string.IsNullOrEmpty(emp.Photograph) ? emp.Photograph : string.Empty;
                                        emp.Photograph = imageName;
                                        emp.UpdatedBy = principal.UserData.UserName;
                                        msg = empDao.UpdateImage(emp);
                                    }
                                    break;
                            }
                            if (msg.MsgType != MessageType.Error)
                            {
                                msg = new Message(MessageConstants.I0001, MessageType.Info, imageName, "uploaded");
                            }
                            else
                            {
                                msg = new Message(MessageConstants.E0014, MessageType.Error, "delete photo.");
                            }
                            result.Data = new { msg };
                        }
                        else // case add new
                        {
                            msg = new Message(MessageConstants.I0001, MessageType.Info, imageName, "uploaded");
                            result.Data = new { msg };
                        }
                        if (!String.IsNullOrEmpty(img))
                        {
                            if (System.IO.File.Exists(strPath + img))
                                System.IO.File.Delete(strPath + img);
                        }
                    }
                }
                else
                {
                    msg = new Message(MessageConstants.E0011, MessageType.Error);
                    result.Data = new { msg };
                }
            }
            return result;
        }

        [HttpPost]
        public FileUploadJsonResult UploadFile(FormCollection form)
        {
            saveTo = form["path"].Replace("/undefined", "");
            string filename = form["value"];
            string recordID = form["recordID"];
            string controller = form["controller"];
            var principal = HttpContext.User as AuthenticationProjectPrincipal;
            FileUploadJsonResult result = new FileUploadJsonResult();
            Message msg = null;
            foreach (string file in Request.Files)
            {
                string uniqueId = DateTime.Now.ToString("MMddyyyyhhmmss");
                HttpPostedFileBase hpf = Request.Files[file] as HttpPostedFileBase;
                //string value = form["EmpId"];

                if (hpf.ContentLength != 0)
                {
                    int maxSize = 1024 * 1024 * Constants.CV_MAX_SIZE;
                    float sizeFile = float.Parse(hpf.ContentLength.ToString("N"));

                    string extension = Path.GetExtension(hpf.FileName);
                    string[] extAllowList = Constants.CV_EXT_ALLOW.Split(',');
                    if (!extAllowList.Contains(extension.ToLower())) //check extension file is valid
                    {
                        msg = new Message(MessageConstants.E0013, MessageType.Error, extension, Constants.CV_EXT_ALLOW, Constants.CV_MAX_SIZE.ToString());
                        result.Data = new { msg };
                    }
                    else if (sizeFile > maxSize) //check maxlength of uploaded file
                    {
                        msg = new Message(MessageConstants.E0012, MessageType.Error, Constants.CV_MAX_SIZE.ToString());
                        result.Data = new { msg };
                    }
                    else  //successful case
                    {
                        //string strPath = Server.MapPath(saveTo);
                        string strPath = string.IsNullOrEmpty(recordID) == true ? Server.MapPath(Constants.UPLOAD_TEMP_PATH) : Server.MapPath(saveTo);
                        string cvName = uniqueId + "_" + Path.GetFileName(hpf.FileName);
                        cvName = ConvertUtil.FormatFileName(cvName);
                        hpf.SaveAs(strPath + cvName);

                        if (!string.IsNullOrEmpty(recordID))
                        {
                            switch (controller)
                            {
                                case "Candidate":
                                    Candidate candidateObj = candidateDao.GetById(recordID);
                                    if (candidateObj != null)
                                    {
                                        candidateObj.CVFile = cvName;
                                        candidateObj.UpdatedBy = principal.UserData.UserName;
                                        msg = candidateDao.UpdateFile(candidateObj);
                                    }
                                    break;
                                case "STT":
                                    STT sttObj = sttDao.GetById(recordID);
                                    if (sttObj != null)
                                    {
                                        sttObj.CVFile = cvName;
                                        sttObj.UpdatedBy = principal.UserData.UserName;
                                        msg = sttDao.UpdateCV(sttObj);
                                    }
                                    break;
                                case "Employee":
                                    Employee emp = empDao.GetById(recordID);
                                    if (emp != null)
                                    {
                                        emp.CVFile = cvName;
                                        emp.UpdatedBy = principal.UserData.UserName;
                                        msg = empDao.UpdateCV(emp);
                                    }
                                    break;
                            }
                            if (msg.MsgType != MessageType.Error)
                            {
                                msg = new Message(MessageConstants.I0001, MessageType.Info, cvName, "uploaded");
                            }
                            else
                            {
                                msg = new Message(MessageConstants.E0014, MessageType.Error, "delete CV.");
                            }
                            result.Data = new { msg };
                        }
                        else // Case when Create
                        {
                            msg = new Message(MessageConstants.I0001, MessageType.Info, cvName, "uploaded");
                            result.Data = new { msg };
                        }
                        if (!String.IsNullOrEmpty(filename))
                        {
                            if (System.IO.File.Exists(strPath + filename))
                                System.IO.File.Delete(strPath + filename);
                        }
                    }

                }
                else
                {
                    msg = new Message(MessageConstants.E0011, MessageType.Error);
                    result.Data = new { msg };

                }
            }
            return result;
        }

        #endregion

        #region Download File

        public JsonResult CheckFileAvailable(string file_path)
        {
            JsonResult result = new JsonResult();
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            string strPath = string.Empty;

            strPath = Server.MapPath(file_path);
            if (System.IO.File.Exists(strPath))
            {
                result.Data = true;
            }
            else
            {
                string[] filename=file_path.Split(Convert.ToChar("/"));
                if (System.IO.File.Exists(Server.MapPath(Constants.UPLOAD_TEMP_PATH + filename[filename.Length-1].ToString())))
                   result.Data = true;
                else
                    result.Data = Resources.Message.E0026;
            }
            
            return result;
          
        }
        /// <summary>
        /// Check if the material file exists
        /// </summary>
        /// <param name="id">Material id</param>
        /// <returns></returns>
        public JsonResult CheckMaterialFileAvailable(string id)
        {
            JsonResult result = new JsonResult();
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            var material = materialDao.GetById(ConvertUtil.ConvertToInt(id), null);
            string filePath = Server.MapPath(Constants.MATERIAL_FOLDER + material.UploadFile);
            if (material == null || !System.IO.File.Exists(filePath))
                result.Data = Resources.Message.E0026;
            else
                result.Data = true;
            return result;
            //string strPath = string.Empty;

            //strPath = Server.MapPath(material.UploadFile);
            //if (System.IO.File.Exists(strPath))
            //{
            //    result.Data = true;
            //}
            //else
            //{
            //    string[] filename = material.UploadFile.Split(Convert.ToChar("/"));
            //    if (System.IO.File.Exists(Server.MapPath(Constants.UPLOAD_TEMP_PATH + filename[filename.Length - 1].ToString())))
            //        result.Data = true;
            //    else
            //        result.Data = Resources.Message.E0026;
            //}

            //return result;

        }
        

        public void DownloadFile(string file_path, string output)
        {
            
            string strPath = string.Empty;
            strPath = Server.MapPath(file_path);
            if (!System.IO.File.Exists(strPath))
            {
                string[] filename=file_path.Split(Convert.ToChar("/"));
                strPath = Server.MapPath(Constants.UPLOAD_TEMP_PATH + filename[filename.Length - 1].ToString());
            }
            System.IO.FileInfo TargetFile = new System.IO.FileInfo(strPath);
            Response.Clear();
            Response.ContentType = "application/octet-stream";
            Response.AddHeader("Content-Disposition", "attachment; filename=" + output);
            Response.AddHeader("Content-Length", TargetFile.Length.ToString());

            FileStream sourceFile = new FileStream(TargetFile.FullName, FileMode.Open);
            long FileSize;
            FileSize = sourceFile.Length;
            byte[] getContent = new byte[(int)FileSize];
            sourceFile.Read(getContent, 0, (int)sourceFile.Length);
            sourceFile.Close();
            Response.BinaryWrite(getContent);
            //return View();
        }
        /// <summary>
        /// Download material file
        /// </summary>
        /// <param name="id">material ID</param>
        public void DownloadMaterial(string id)
        {
            try
            {
                materialDao.UpdateDownloadTimes(ConvertUtil.ConvertToInt(id));
                var material = materialDao.GetById(ConvertUtil.ConvertToInt(id), null);
                string file_path = Constants.MATERIAL_FOLDER + material.UploadFile;
                DownloadFile(file_path, material.UploadFileDisplayName);
            }
            catch{}
            
        }
        #endregion

        #region Delete File

        public ActionResult RemovePhoto(string controller, string image, string id)
        {
            var principal = HttpContext.User as AuthenticationProjectPrincipal;
            string strPath = string.Empty;
            Message msg = null;
            if (!string.IsNullOrEmpty(id))
            {
                switch (controller)
                {
                    case Constants.CANDIDATE_DEFAULT_VALUE:
                        Candidate candidateObj = null;
                        candidateObj = candidateDao.GetById(id);
                        if (candidateObj != null)
                        {
                            candidateObj.Photograph = null;
                            candidateObj.UpdatedBy = principal.UserData.UserName;
                            msg = candidateDao.UpdateImage(candidateObj);
                        }
                        break;
                    case Constants.STT_DEFAULT_VALUE:
                        STT stt = null;
                        stt = sttDao.GetById(id);
                        if (stt != null)
                        {
                            stt.Photograph = null;
                            stt.UpdatedBy = principal.UserData.UserName;
                            msg = sttDao.UpdateImage(stt);
                        }
                        break;
                    case Constants.EMPLOYEE_DEFAULT_VALUE:
                        Employee emp = null;
                        emp = empDao.GetById(id);
                        if (emp != null)
                        {
                            emp.Photograph = null;
                            emp.UpdatedBy = principal.UserData.UserName;
                            msg = empDao.UpdateImage(emp);
                        }
                        break;
                }
            }
            if (msg != null && msg.MsgType == MessageType.Error)
            {
                return Json(msg);
            }
            else
            {
                string strPathTemp = Server.MapPath(Constants.UPLOAD_TEMP_PATH);
                if (System.IO.File.Exists(strPathTemp + "\\" + image))
                    System.IO.File.Delete(strPathTemp + "\\" + image);
                switch (controller)
                {
                    case Constants.CANDIDATE_DEFAULT_VALUE:
                        strPath = Server.MapPath(Constants.PHOTO_PATH_ROOT_CANDIDATE);
                        strPath += "\\" + image;
                        if (System.IO.File.Exists(strPath))
                            System.IO.File.Delete(strPath);
                        if (!System.IO.File.Exists(strPath))
                        {
                            msg = new Message(MessageConstants.I0001, MessageType.Info, "Candidate Photo", "deleted");
                        }
                        else
                        {
                            msg = new Message(MessageConstants.E0014, MessageType.Error, "delete candidate Photo.");
                        }
                        break;
                    case Constants.STT_DEFAULT_VALUE:
                        strPath = Server.MapPath(Constants.IMAGE_PATH);
                        strPath += "\\" + image;
                        if (System.IO.File.Exists(strPath))
                            System.IO.File.Delete(strPath);
                        if (!System.IO.File.Exists(strPath))
                        {
                            msg = new Message(MessageConstants.I0001, MessageType.Info, "STT Photo", "deleted");
                        }
                        else
                        {
                            msg = new Message(MessageConstants.E0014, MessageType.Error, "delete stt Photo.");
                        }
                        break;
                    case Constants.EMPLOYEE_DEFAULT_VALUE:
                        strPath = Server.MapPath(Constants.IMAGE_PATH);
                        strPath += "\\" + image;
                        if (System.IO.File.Exists(strPath))
                            System.IO.File.Delete(strPath);
                        if (!System.IO.File.Exists(strPath))
                        {
                            msg = new Message(MessageConstants.I0001, MessageType.Info, "Employee Photo", "deleted");
                        }
                        else
                        {
                            msg = new Message(MessageConstants.E0014, MessageType.Error, "delete Employee Photo.");
                        }
                        break;
                }
            }
            return Json(msg);
        }

        public ActionResult RemoveCV(string controller, string file, string id)
        {
            var principal = HttpContext.User as AuthenticationProjectPrincipal;
            string strPath = string.Empty;
            Message msg = null;
            if (!string.IsNullOrEmpty(id))
            {
                switch (controller)
                {
                    case Constants.CANDIDATE_DEFAULT_VALUE:
                        Candidate candidateObj = null;
                        candidateObj = candidateDao.GetById(id);
                        if (candidateObj != null)
                        {
                            candidateObj.CVFile = null;
                            candidateObj.UpdatedBy = principal.UserData.UserName;
                            msg = candidateDao.UpdateFile(candidateObj);
                        }
                        break;
                    case Constants.STT_DEFAULT_VALUE:
                        STT stt = null;
                        stt = sttDao.GetById(id);
                        if (stt != null)
                        {
                            stt.CVFile = null;
                            stt.UpdatedBy = principal.UserData.UserName;
                            msg = sttDao.UpdateCV(stt);
                        }
                        break;
                    case Constants.EMPLOYEE_DEFAULT_VALUE:
                        Employee emp = null;
                        emp = empDao.GetById(id);
                        if (emp != null)
                        {
                            emp.CVFile = null;
                            emp.UpdatedBy = principal.UserData.UserName;
                            msg = empDao.UpdateCV(emp);
                        }
                        break;
                }
            }
            if (msg != null && msg.MsgType == MessageType.Error)
            {
                return Json(msg);
            }
            else
            {
                string strPathTemp = Server.MapPath(Constants.UPLOAD_TEMP_PATH);
                if (System.IO.File.Exists(strPathTemp + "\\" + file))
                    System.IO.File.Delete(strPathTemp + "\\" + file);
                switch (controller)
                {
                    case Constants.CANDIDATE_DEFAULT_VALUE:
                        strPath = Server.MapPath(Constants.CV_PATH_ROOT_CANDIDATE);
                        strPath += "\\" + file;
                        if (System.IO.File.Exists(strPath))
                            System.IO.File.Delete(strPath);
                        if (!System.IO.File.Exists(strPath))
                        {
                            msg = new Message(MessageConstants.I0001, MessageType.Info, "Candidate CV", "deleted");
                        }
                        else
                        {
                            msg = new Message(MessageConstants.E0014, MessageType.Error, "delete candidate cv.");
                        }
                        break;
                    case Constants.STT_DEFAULT_VALUE:
                        strPath = Server.MapPath(Constants.CV_PATH);
                        strPath += "\\" + file;
                        if (System.IO.File.Exists(strPath))
                            System.IO.File.Delete(strPath);
                        if (!System.IO.File.Exists(strPath))
                        {
                            msg = new Message(MessageConstants.I0001, MessageType.Info, "STT CV", "deleted");
                        }
                        else
                        {
                            msg = new Message(MessageConstants.E0014, MessageType.Error, "delete stt cv.");
                        }
                        break;
                    case Constants.EMPLOYEE_DEFAULT_VALUE:
                        strPath = Server.MapPath(Constants.CV_PATH);
                        strPath += "\\" + file;
                        if (System.IO.File.Exists(strPath))
                            System.IO.File.Delete(strPath);
                        if (!System.IO.File.Exists(strPath))
                        {
                            msg = new Message(MessageConstants.I0001, MessageType.Info, "Employee CV", "deleted");
                        }
                        else
                        {
                            msg = new Message(MessageConstants.E0014, MessageType.Error, "delete Employee cv.");
                        }
                        break;
                }
            }
            return Json(msg);
        }

        #endregion

        #region Pop Up Job Request

        public ActionResult ListJR(string page, string isOnPopup)
        {
            //// triet.dinh 26-12-2011
            ViewData["SubDepartment"] = new DepartmentDao().GetDepartmentByHierarchy();
            //ViewData["Department"] = new SelectList(new DepartmentDao().GetList(), "DepartmentId", "DepartmentName");
            //ViewData["SubDepartment"] = new SelectList(new DepartmentDao().GetSubList(), "DepartmentId", "DepartmentName");

            ViewData["positionId"] = new SelectList(new JobTitleLevelDao().GetList(), "ID", "DisplayName");
            ViewData["Func"] = page;
            ViewData["RequestType"] = new SelectList(Constants.JR_REQUEST_TYPE, "Value", "Text", string.Empty);
            ViewData["IsOnPopup"] = isOnPopup;
            return View();
        }

        public ActionResult ListJRInterview(string page, string isOnPopup)
        {
            //// triet.dinh 26-12-2011
            ViewData["SubDepartment"] = new DepartmentDao().GetDepartmentByHierarchy();
            //ViewData["Department"] = new SelectList(new DepartmentDao().GetList(), "DepartmentId", "DepartmentName");
            //ViewData["SubDepartment"] = new SelectList(new DepartmentDao().GetSubList(), "DepartmentId", "DepartmentName");

            ViewData["positionId"] = new SelectList(new JobTitleLevelDao().GetList(), "ID", "DisplayName");
            ViewData["Func"] = page;
            ViewData["RequestType"] = new SelectList(Constants.JR_REQUEST_TYPE, "Value", "Text", string.Empty);
            ViewData["IsOnPopup"] = isOnPopup;
            return View();
        }
        public ActionResult GetListJRGrid(string text, string department, string sub, string positionId, string issueDate, string Func,string request)
        {
            var principal = HttpContext.User as AuthenticationProjectPrincipal;
            #region JQGrid Params
            string sortColumn = (Request.Params[GridConstants.SORT_COLUMN]).ToString();
            string sortOrder = (Request.Params[GridConstants.SORT_ORDER]).ToString();
            int pageIndex = Convert.ToInt32(Request.Params[GridConstants.PAGE_INDEX]);
            int rowCount = Convert.ToInt32(Request.Params[GridConstants.ROW_COUNT]);
            #endregion
            #region Search
            string textSearch = string.Empty;
            int idepartment = 0;
            int isubdepartment = 0;
            int ipositionId = 0;
            int requestType = 0;
            
            string containsJR = Constants.JOB_REQUEST_ITEM_PREFIX;
            if (!String.IsNullOrEmpty(text))
            {

                if (text.Contains(containsJR))
                {
                    textSearch = text.Substring(containsJR.Length);
                }
                else
                {
                    textSearch = text;
                }
            }
            if (!string.IsNullOrEmpty(department))
            {
                idepartment = int.Parse(department);
            }
            if (!string.IsNullOrEmpty(sub))
            {
                isubdepartment = int.Parse(sub);
            }
            if (!string.IsNullOrEmpty(positionId))
            {
                ipositionId = int.Parse(positionId);
            }
            if (!string.IsNullOrEmpty(request))
            {
                requestType = int.Parse(request);
            }

            #endregion
            List<sp_GetJobRequestCompleteResult> list = new List<sp_GetJobRequestCompleteResult>();
            
            if (string.IsNullOrEmpty(issueDate))
            {
                list = requestDao.GetJRListComplete(textSearch, idepartment, isubdepartment, ipositionId, null, requestType);
            }
            else
            {
                list = requestDao.GetJRListComplete(textSearch, idepartment, isubdepartment, ipositionId, DateTime.Parse(issueDate), requestType);
            }
           
            int totalRecords = list.Count();
            int totalPages = (int)Math.Ceiling((float)totalRecords / (float)rowCount);
            int currentPage = CommonFunc.SetJqGridPageIndex(totalRecords, pageIndex, rowCount);
            var finalList = list.Skip((currentPage - 1) * rowCount).Take(rowCount);

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
                            m.ID.ToString(),
                            "<a id='"+m.ID.ToString()+"' href='javascript:ChooseJR("+m.ID.ToString()+");'>"+ Constants.JOB_REQUEST_ITEM_PREFIX + m.ID.ToString() +"</a>",                            
                            m.Approval,
                            m.Candidate,
                            m.EmpID,
                            m.Department,
                            m.SubDepartment, 
                            m.FinalTitleName,
                            m.RequestTypeId == Constants.JR_REQUEST_TYPE_NEW ? "New":"Replace",
                            m.Justification,
                        }
                    }
                ).ToArray()
            };

            return Json(jsonData, JsonRequestBehavior.AllowGet);
            //return null;
        }

        public ActionResult GetListJRGridInterview(string text, string department, string sub, string positionId,string request)
        {
            var principal = HttpContext.User as AuthenticationProjectPrincipal;
            #region JQGrid Params
            string sortColumn = (Request.Params[GridConstants.SORT_COLUMN]).ToString();
            string sortOrder = (Request.Params[GridConstants.SORT_ORDER]).ToString();
            int pageIndex = Convert.ToInt32(Request.Params[GridConstants.PAGE_INDEX]);
            int rowCount = Convert.ToInt32(Request.Params[GridConstants.ROW_COUNT]);
            #endregion
            #region Search
            string textSearch = string.Empty;
            int idepartment = 0;
            int isubdepartment = 0;
            int ipositionId = 0;
            int requestType = 0;
            
            string containsJR = Constants.JOB_REQUEST_ITEM_PREFIX;
            if (!String.IsNullOrEmpty(text))
            {

                if (text.Contains(containsJR))
                {
                    textSearch = text.Substring(containsJR.Length);
                }
                else
                {
                    textSearch = text;
                }
            }
            if (!string.IsNullOrEmpty(department))
            {
                idepartment = int.Parse(department);
            }
            if (!string.IsNullOrEmpty(sub))
            {
                isubdepartment = int.Parse(sub);
            }
            if (!string.IsNullOrEmpty(positionId))
            {
                ipositionId = int.Parse(positionId);
            }
            if (!string.IsNullOrEmpty(request))
            {
                requestType = int.Parse(request);
            }

            #endregion
            List<sp_GetJobRequestCompleteInterviewResult> list = new List<sp_GetJobRequestCompleteInterviewResult>();
            
            list = requestDao.GetJRListInterView(textSearch, idepartment, isubdepartment, ipositionId, requestType);
            
            int totalRecords = list.Count();
            int totalPages = (int)Math.Ceiling((float)totalRecords / (float)rowCount);
            int currentPage = CommonFunc.SetJqGridPageIndex(totalRecords, pageIndex, rowCount);
            var finalList = list.Skip((currentPage - 1) * rowCount).Take(rowCount);

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
                            m.ID.ToString(),
                            "<a id='"+m.ID.ToString()+"' href='javascript:ChooseJR("+m.ID.ToString()+");'>"+ Constants.JOB_REQUEST_ITEM_PREFIX + m.ID.ToString() +"</a>",                            
                            m.RequestDate.ToString(Constants.DATETIME_FORMAT_VIEW),
                            m.Position,
                            /*m.Quantity.ToString(),*/
                            m.Department,
                            m.SubDepartment, 
                            m.ExpectedStartDate.HasValue? m.ExpectedStartDate.Value.ToString(Constants.DATETIME_FORMAT_VIEW):"",
                            m.RequestTypeId == Constants.JR_REQUEST_TYPE_NEW ? "New":"Replace",
                            m.Justification,
                        }
                    }
                ).ToArray()
            };

            return Json(jsonData, JsonRequestBehavior.AllowGet);
            //return null;
        }
        #endregion

        #region Support For Email
        [ValidateInput(false)]
        public JsonResult CheckCCList(string userNameList)
        {
            UserAdminDao userAdminDao = new UserAdminDao();
            JsonResult result = new JsonResult();
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            if (!string.IsNullOrEmpty(userNameList))
            {
                string[] array = userNameList.Trim().TrimEnd(';').Split(';');
                foreach (string item in array)
                {
                    if (!string.IsNullOrEmpty(item))
                    {
                        UserAdmin obj = userAdminDao.GetByUserName(item.Split('@')[0].Trim());
                        if (obj != null)
                        {
                            result.Data = true;
                        }
                        else
                        {
                            result.Data = string.Format(Resources.Message.E0005, item, " User Admin");
                        }
                    }
                }
            }
            else
            {
                result.Data = true;
            }
            return result;
        }
        #endregion

        #region pop up ListCertificationMaster

        public ActionResult ListCertificationMaster()
        {
            return View();
        }

        public ActionResult GetListCertificationGrid(string certificationName)
        {
            #region JQGrid Params
            string sortColumn = (Request.Params[GridConstants.SORT_COLUMN]).ToString();
            string sortOrder = (Request.Params[GridConstants.SORT_ORDER]).ToString();
            int pageIndex = Convert.ToInt32(Request.Params[GridConstants.PAGE_INDEX]);
            int rowCount = Convert.ToInt32(Request.Params[GridConstants.ROW_COUNT]);
            #endregion
            SetSessionFilter(certificationName, sortColumn, sortOrder, pageIndex, rowCount);
            string name = string.Empty;
            if (certificationName != Constants.TRAINING_CERTIFICATION_MASTER_SEARCH_NAME && !string.IsNullOrEmpty(certificationName))
            {
                name = certificationName;
            }
            List<sp_GetTrainingCertificationResult> trainingCerList = new TrainingCertificationDao().GetTrainingCertificationList(name).Where(p => p.IsActive == true).ToList();

            int totalRecords = trainingCerList.Count();
            int totalPages = (int)Math.Ceiling((float)totalRecords / (float)rowCount);
            int currentPage = CommonFunc.SetJqGridPageIndex(totalRecords, pageIndex, rowCount);

            //List<sp_GetTrainingCertificationResult> finalList = trainingCerDao.Sort(trainingCerList, sortColumn, sortOrder).Skip((currentPage - 1) * rowCount)
            //                       .Take(rowCount).ToList<sp_GetTrainingCertificationResult>();
            var finalList = trainingCerList.Skip((currentPage - 1) * rowCount).Take(rowCount);
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
                             HttpUtility.HtmlEncode(m.ID.ToString()),
                             HttpUtility.HtmlEncode(m.ID.ToString()),
                             CommonFunc.Link(HttpUtility.HtmlEncode(m.Name.ToString()), "javascript:ChooseCertificationID(\"" + m.ID + "\", \"" + m.Name + "\");", HttpUtility.HtmlEncode(m.Name.ToString()), false),
                             HttpUtility.HtmlEncode(m.Description)
                         }
                    }
                ).ToArray()
            };
            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        private void SetSessionFilter(string certificationName, string column, string order, int pageIndex, int rowCount)
        {
            Hashtable hashData = new Hashtable();
            hashData.Add(Constants.TRAINING_CERTIFICATION_MASTER_LIST_NAME, certificationName);

            hashData.Add(Constants.TRAINING_CERTIFICATION_MASTER_LIST_COLUMN, column);
            hashData.Add(Constants.TRAINING_CERTIFICATION_MASTER_LIST_ORDER, order);
            hashData.Add(Constants.TRAINING_CERTIFICATION_MASTER_LIST_PAGE_INDEX, pageIndex);
            hashData.Add(Constants.TRAINING_CERTIFICATION_MASTER_LIST_ROW_COUNT, rowCount);

            Session[SessionKey.TRAINING_CERTIFICATION_MASTER_DEFAULT_VALUE] = hashData;
        }
        #endregion

        #region Pop Up Manager
        public ActionResult ListManager()
        {
            ViewData["Department"] = new SelectList(new DepartmentDao().GetList(), "DepartmentId", "DepartmentName");
            ViewData["JobTitle"] = new SelectList(new JobTitleDao().GetManagerTitle(), "JobTitleId", "JobTitleName");
            return View();
        }

       
        public ActionResult GetListManagerGrid(string text, string department, string title)
        {
            var principal = HttpContext.User as AuthenticationProjectPrincipal;
            #region JQGrid Params
            string sortColumn = (Request.Params[GridConstants.SORT_COLUMN]).ToString();
            string sortOrder = (Request.Params[GridConstants.SORT_ORDER]).ToString();
            int pageIndex = Convert.ToInt32(Request.Params[GridConstants.PAGE_INDEX]);
            int rowCount = Convert.ToInt32(Request.Params[GridConstants.ROW_COUNT]);
            #endregion
            #region Search
            string textSearch = string.Empty;
            int idepartment = 0;
            int ititle = 0;
            if (!string.IsNullOrEmpty(text))
            {
                textSearch = text;
            }
            if (!string.IsNullOrEmpty(department))
            {
                idepartment = int.Parse(department);
            }
            if (!string.IsNullOrEmpty(title))
            {
                ititle = int.Parse(title);
            }

            #endregion
            List<sp_GetManagerResult> list = empDao.GetManager(textSearch, idepartment, ititle);
            
            int totalRecords = list.Count();
            int totalPages = (int)Math.Ceiling((float)totalRecords / (float)rowCount);
            int currentPage = CommonFunc.SetJqGridPageIndex(totalRecords, pageIndex, rowCount);
            var finalList = list.Skip((currentPage - 1) * rowCount).Take(rowCount);

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
                            m.ID.ToString(),
                            "<a href='javascript: ChooseManager(\""+m.ID+"\",\"" +  
                                CommonFunc.GetEmployeeFullName(empDao.GetById(m.ID), 
                                Constants.FullNameFormat.FirstMiddleLast) + "\");'>" + m.DisplayName +"</a>",
                            m.Title,
                            m.DepartmentName
                        }
                    }
                ).ToArray()
            };
            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Pop Up University
       
        public ActionResult ListUniversity()
        {
            return View();
        }

        public ActionResult GetListUniversityGrid(string text)
        {
            var principal = HttpContext.User as AuthenticationProjectPrincipal;
            #region JQGrid Params
            string sortColumn = (Request.Params[GridConstants.SORT_COLUMN]).ToString();
            string sortOrder = (Request.Params[GridConstants.SORT_ORDER]).ToString();
            int pageIndex = Convert.ToInt32(Request.Params[GridConstants.PAGE_INDEX]);
            int rowCount = Convert.ToInt32(Request.Params[GridConstants.ROW_COUNT]);
            #endregion
            #region Search
            string textSearch = string.Empty;
            if (!string.IsNullOrEmpty(text))
            {
                textSearch = text;
            }
            #endregion
            List<University> list = candidateDao.GetUniversityList(textSearch);

            int totalRecords = list.Count();
            int totalPages = (int)Math.Ceiling((float)totalRecords / (float)rowCount);
            int currentPage = CommonFunc.SetJqGridPageIndex(totalRecords, pageIndex, rowCount);
            var finalList = list.Skip((currentPage - 1) * rowCount).Take(rowCount);

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
                            m.ID.ToString(),
                            "<a id='lnk"+m.ID.ToString()+"' university='"+m.Name+"' href='javascript:ChooseUniversity("+m.ID.ToString()+");'>"+  m.Name +"</a>",
                            m.Address
                        }
                    }
                ).ToArray()
            };
            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        #endregion

        /*Added by Tai Nguyen on 18-Jan-2011*/
        #region Check valid email
        /// <summary>
        /// Check if Emails is valid format 
        /// </summary>
        /// <param name="toEmailList"></param>
        /// <param name="ccEmailList"></param>
        /// <returns></returns>
        public static bool IsValidEmail(string email)
        {
            // Return true if email is in valid e-mail format.
            return Regex.IsMatch(email,
                   @"^(?("")("".+?""@)|(([0-9a-zA-Z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-zA-Z])@))" +
                   @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-zA-Z][-\w]*[0-9a-zA-Z]\.)+[a-zA-Z]{2,6}))$");
        }
        #endregion

        #region Check valid email list
        /// <summary>
        /// Check if Emails is valid format 
        /// </summary>
        /// <param name="toEmailList"></param>
        /// <param name="ccEmailList"></param>
        /// <returns></returns>
        public static bool IsValidEmailList(string emailList)
        {
            string[] arrEmail = emailList.Trim().TrimEnd(';').Split(';')
                    .Where(p => !string.IsNullOrEmpty(p)).ToArray();
            foreach (string email in arrEmail)
            {
                if (!IsValidEmail(email))
                {
                    return false;
                }
            }
            return true;
        }
        #endregion

        #region Check valid email send
        /// <summary>
        /// Check if the email can be sent
        /// </summary>
        /// <param name="toEmailList"></param>
        /// <param name="ccEmailList"></param>
        /// <returns></returns>
        [ValidateInput(false)]
        public static string CanSendEmail(string toEmailList, string ccEmailList)
        {
            if (string.IsNullOrEmpty(toEmailList))
            {
                return MessageConstants.E0001;
            }
            if (!IsValidEmailList(toEmailList) ||
                (!string.IsNullOrEmpty(ccEmailList) && !IsValidEmailList(ccEmailList)))
            {
                return MessageConstants.E0030;
            }
            return MessageConstants.I0002;
        }
        #endregion
        /*End Tai Nguyen*/

        /// <summary>
        /// Show PTO detail tooltip
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult PTODetailTooltip(string id, string month)
        {
            PTO pto = ptoDao.GetPTOById(id);
            ViewData["PTO"] = pto;
            ViewData["PTO_Details"] = pto.PTO_Details.ToList<PTO_Detail>();
            return View();
        }

        public ActionResult GetPTODetailToolTip(string id, string month)
        {
           // string getRange = null;
            DateTime? fromDate = null;
            DateTime? toDate = null;
            PTO pto = ptoDao.GetPTOById(id);
            ViewData["PTO"] = pto;
            fromDate = DateTime.Parse(Constants.DATE_LOCK_PTO + month).AddMonths(-1).AddDays(1);
            toDate = fromDate.Value.AddMonths(1).AddDays(-1);

            List<sp_GetPTODetailResult> listPTODetail = ptoDao.GetPTODetailForToolTip(id, fromDate, toDate);

            PTO_Detail pto_detail = ptoDao.GetPTODetail(id);
            DateTime? DateOff = pto_detail.DateOff;

            if (listPTODetail.Count <= 0 && DateOff != null)
            {
                DateTime dateOffFrom =
                    DateTime.Parse(Constants.DATE_LOCK_PTO + "/" + pto_detail.DateOff.Value.Month + "/" + pto_detail.DateOff.Value.Year).AddMonths(-1).AddDays(1);
                DateTime dateOffTo = dateOffFrom.AddMonths(1).AddDays(-1);
                listPTODetail = ptoDao.GetPTODetailForToolTip(id, dateOffFrom, dateOffTo);
            }
            if (listPTODetail.Count <= 0 && DateOff == null)
            {
                listPTODetail = ptoDao.GetPTODetailForToolTip(id, null, null);
            }
            ViewData["PTO_Details"] = listPTODetail;

            return View();
        }


        public ActionResult NotPermission()
        {
            return View();
        }
        public ActionResult NotPermissionPopup()
        {
            return View();
        }

        public ActionResult ListSeatCode()
        {
            ViewData[CommonDataKey.LOCATION_LIST_BRANCH] = new SelectList(locationDao.GetListBranchAll(true, false), "ID", "Name", Constants.LOCATION_BRANCH_SAI_GON);
            ViewData[CommonDataKey.LOCATION_LIST_FLOOR] = new SelectList(locationDao.GetListFloor(Constants.LOCATION_BRANCH_SAI_GON, 0, true, false), "ID", "Name");
            ViewData[CommonDataKey.LOCATION_LIST_OFFICE] = new SelectList(locationDao.GetListOffice(Constants.LOCATION_BRANCH_SAI_GON, true, false), "ID", "Name");
            ViewData[CommonDataKey.LOCATION_LIST_AVAILABLE] = new SelectList(Constants.SeatCodeStatus, "value", "text", "1"); 
            return View();
        }
        public ActionResult GetListWorkLocationGrid(string text, string branch, string office, string floor, string isAvailable)
        {
            var principal = HttpContext.User as AuthenticationProjectPrincipal;
            #region JQGrid Params
            string sortColumn = (Request.Params[GridConstants.SORT_COLUMN]).ToString();
            string sortOrder = (Request.Params[GridConstants.SORT_ORDER]).ToString();
            int pageIndex = Convert.ToInt32(Request.Params[GridConstants.PAGE_INDEX]);
            int rowCount = Convert.ToInt32(Request.Params[GridConstants.ROW_COUNT]);
            #endregion
            #region Search
            string textSearch = string.IsNullOrEmpty(text) || 
                text.Equals(Constants.LOCATION_TEXTBOX_KEYWORD) ? string.Empty : text.Trim().ToLower();
            int iBranch = !string.IsNullOrEmpty(branch) && CheckUtil.IsInteger(branch) ? int.Parse(branch) : 0;
            int iOffice = !string.IsNullOrEmpty(office) && CheckUtil.IsInteger(office) ? int.Parse(office) : 0;
            int iFloor = !string.IsNullOrEmpty(floor) && CheckUtil.IsInteger(floor) ? int.Parse(floor) : 0;
            bool? bIsAvailable = null;
            if(!string.IsNullOrEmpty(isAvailable) && CheckUtil.IsInteger(isAvailable))
                bIsAvailable = Convert.ToBoolean(int.Parse(isAvailable));
            
            #endregion
            List<sp_GetSeatCodeResult> list = locationDao.GetListSeatCode(textSearch, iBranch, iOffice, iFloor, bIsAvailable);
            string sIsOnPopup = "false";
            if(!string.IsNullOrEmpty(Request["isOnPopup"]))
                sIsOnPopup = Request["isOnPopup"];
            int totalRecords = list.Count();
            int totalPages = (int)Math.Ceiling((float)totalRecords / (float)rowCount);
            int currentPage = CommonFunc.SetJqGridPageIndex(totalRecords, pageIndex, rowCount);
            var finalList = locationDao.Sort(list, sortColumn, sortOrder).
                Skip((currentPage - 1) * rowCount).Take(rowCount);
            var jsonData = new
            {
                total = totalPages,
                page = currentPage,
                records = totalRecords,
                rows = (
                    from m in finalList
                    select new
                    {
                        i = m.SeatCodeID,
                        cell = new string[] {
                            m.IsAvailable.Value ? CommonFunc.Link(m.SeatCodeID.ToString(), "", 
                                string.Format("javascript: chooseSeatCode(\"{0}\",\"{1}\", {2});", 
                                CommonFunc.GenerateLocationCode(m.SeatCodeID), CommonFunc.GetWorkLocationText(m.SeatCodeID), sIsOnPopup), 
                                m.SeatCodeName) : m.SeatCodeName,
                            m.FloorName,
                            m.OfficeName,
                            m.BranchName,
                            m.IsAvailable.Value ? Constants.LOCATION_SEATCODE_AVAILABLE_TEXT : 
                                locationDao.GetOwner(m.SeatCodeID).DisplayName()
                        }
                    }
                ).ToArray()
            };
            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }
        public ActionResult BranchListOnChange(string branchID)
        {
            try
            {
                branchID = string.IsNullOrEmpty(branchID) || !CheckUtil.IsInteger(branchID) ? "0" : branchID;
                int iBranchID = int.Parse(branchID);
                var officeList = locationDao.GetListOffice(iBranchID, true, false).Select(p => new { p.ID, p.Name }).ToArray();
                var floorList = locationDao.GetListFloor(iBranchID, 0, true, false).Select(p => new { p.ID, p.Name }).ToArray();
                return Json(new
                {
                    success = true,
                    offices = officeList,
                    floors = floorList
                }, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json(new { 
                    success = false
                }, JsonRequestBehavior.AllowGet);
                
            }
        }
        public ActionResult OfficeListOnChange(string branchID, string officeID)
        {
            try
            {
                branchID = string.IsNullOrEmpty(branchID) || !CheckUtil.IsInteger(branchID) ? "0" : branchID;
                officeID = string.IsNullOrEmpty(officeID) || !CheckUtil.IsInteger(officeID) ? "0" : officeID;
                int iBranchID = int.Parse(branchID);
                int iOfficeID = int.Parse(officeID);
                var floorList = locationDao.GetListFloor(iBranchID, iOfficeID, true, false).Select(p => new { p.ID, p.Name }).ToArray();
                return Json(new
                {
                    success = true,
                    floors = floorList
                }, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json(new
                {
                    success = false
                }, JsonRequestBehavior.AllowGet);

            }
        }
        public string WorkLocationTooltip(string locationCode)
        {
            if (string.IsNullOrEmpty(locationCode))
                return string.Empty;
            string sOfficeID = CommonFunc.GetLocation(locationCode, LocationType.Office);
            if (string.IsNullOrEmpty(sOfficeID))
                return string.Format(Resources.Message.E0030, "Work location");
            Office office = locationDao.GetOfficeByID(int.Parse(sOfficeID), true, false);
            return string.Format("<b>Branch: </b> {0}<br/><b>Office: </b> {1}", 
                office.Branch.Name, office.Name);
        }        
       
        public string CreateMenu()
        {
            var principal = HttpContext.User as AuthenticationProjectPrincipal;
            string result = string.Empty;
            List<Menu> menuList = menuDao.GetList(true);
            List<Menu> menuNotChild = menuDao.GetNotChildList(menuList, true);
            var mpList = mpDao.GetList(principal.UserData.UserID);
            foreach (Menu menu in menuNotChild)
            {
                result += ExtensionMethods.RecursionMenu(menuList, menu, mpList);
            }
            return result;
        }

        public ActionResult ListServiceRequest(string id)
        {
            if (id == "false")
            {
                ViewData[Constants.SR_LIST_TITLE] = Constants.SR_FIRST_KEY_WORD ;
                ViewData[Constants.SR_STATUS_LIST] = new SelectList(statusDao.GetAll(), "ID", "Name",Constants.SR_FIRST_STATUS);
                ViewData[Constants.SR_CATEGORY_LIST] = new SelectList(catDao.GetList(), "ID", "Name", Constants.SR_FIRST_CATEGORY);
                ViewData[Constants.SR_SUBCATEGORY_LIST] = new SelectList(catDao.GetSubList(), "ID", "Name",Constants.SR_FIRST_SUBCATEGORY );
                ViewData[Constants.SR_ASSIGNTO_LIST] = new SelectList(userDao.GetListITHelpDesk((int)Modules.ServiceRequestAdmin, (int)Permissions.ItHelpDesk), "Name", "Name",Constants.SR_FIRST_ASSIGNTO );

                //ViewData[Constants.SR_USER_LOGIN] = principal.PortalUserData.UserName;
                return View("ListServiceRequestPortal");
            }
            ViewData[Constants.SR_LIST_ADMIN_TITLE] =  Constants.SR_FIRST_KEY_WORD;
            ViewData[Constants.SR_ADMIN_STATUS_LIST] = new SelectList(statusDao.GetAll(), "ID", "Name", Constants.SR_FIRST_STATUS);
            ViewData[Constants.SR_ADMIN_CATEGORY_LIST] = new SelectList(catDao.GetList(), "ID", "Name",  Constants.SR_FIRST_CATEGORY );
            ViewData[Constants.SR_ADMIN_SUBCATEGORY_LIST] = new SelectList(catDao.GetSubList(), "ID", "Name",Constants.SR_FIRST_SUBCATEGORY );
            ViewData[Constants.SR_ADMIN_ASSIGNTO_LIST] = new SelectList(userDao.GetListITHelpDesk((int)Modules.ServiceRequestAdmin, (int)Permissions.ItHelpDesk), "Name", "Name", Constants.SR_FIRST_ASSIGNTO );
            return View("ListServiceRequestAdmin");
        }

        [ValidateInput(false)]
        public ActionResult GetListPortalJQGrid(string name, string status, string category, string subcate, string assignto, string role)
        {
            #region JQGrid Params
            string sortColumn = (Request.Params[GridConstants.SORT_COLUMN]).ToString();
            string sortOrder = (Request.Params[GridConstants.SORT_ORDER]).ToString();
            int pageIndex = Convert.ToInt32(Request.Params[GridConstants.PAGE_INDEX]);
            int rowCount = Convert.ToInt32(Request.Params[GridConstants.ROW_COUNT]);
            #endregion
            // User login
            var userName = HttpContext.User.Identity.Name;

            #region search
            string title = string.Empty;
            int subcat = 0;
            int categoryId = 0;
            int statusId = 0;
            string assignName = null;
            if (name != Constants.SR_FIRST_KEY_WORD)
            {
                title = name;
            }
            if (!string.IsNullOrEmpty(subcate))
            {
                subcat = int.Parse(subcate);
            }
            if (!string.IsNullOrEmpty(status))
            {
                statusId = int.Parse(status);
            }
            if (!string.IsNullOrEmpty(category))
            {
                categoryId = int.Parse(category);
            }
            if (!string.IsNullOrEmpty(assignto))
            {
                assignName = assignto;
            }
            #endregion
            List<sp_SR_GetServiceRequestResult> empList = new List<sp_SR_GetServiceRequestResult>();
            if (role == Constants.PORTAL_ROLE_MANAGER)
                empList = new ServiceRequestDao().GetList(title, subcat, categoryId, statusId, null, null, role, userName);
            else
                empList = new ServiceRequestDao().GetList(title, subcat, categoryId, statusId, assignName, userName, null,null);

            int totalRecords = empList.Count();
            int totalPages = (int)Math.Ceiling((float)totalRecords / (float)rowCount);
            int currentPage = CommonFunc.SetJqGridPageIndex(totalRecords, pageIndex, rowCount);

            List<sp_SR_GetServiceRequestResult> finalList = new ServiceRequestDao().Sort(empList, sortColumn, sortOrder)
                                 .Skip((currentPage - 1) * rowCount)
                                  .Take(rowCount).ToList<sp_SR_GetServiceRequestResult>();
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
                            "<span class='"+ m.Icon + "'>" + "&nbsp;&nbsp;&nbsp;&nbsp; </span>" ,
                            "<a href='javascript: CRM.chooseSR(\""+ Constants.SR_SERVICE_REQUEST_PREFIX + m.ID+"\");'>" +Constants.SR_SERVICE_REQUEST_PREFIX + m.ID +"</a>",
                           m.Title.Length > 20? m.Title.Substring(0, 20)+ "..." :m.Title,
                            m.Category,
                            m.SubCategory,                            
                            m.Status,
                            m.AssginName,    
                            m.RequestDate.ToString(Constants.DATETIME_FORMAT_VIEW)
                        }
                    }
                ).ToArray()
            };
            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        // triet.dinh 2012-02-06
        [ValidateInput(false)]
        public ActionResult GetListAdminJQGrid(string name, string status, string category, string subcate, string assignto,
            string startdate, string enddate, string requestor)
        {
            #region JQGrid Params
            string sortColumn = (Request.Params[GridConstants.SORT_COLUMN]).ToString();
            string sortOrder = (Request.Params[GridConstants.SORT_ORDER]).ToString();
            int pageIndex = Convert.ToInt32(Request.Params[GridConstants.PAGE_INDEX]);
            int rowCount = Convert.ToInt32(Request.Params[GridConstants.ROW_COUNT]);
            #endregion

            #region search
            string title = string.Empty;
            int subcat = 0;
            int categoryId = 0;
            int statusId = 0;
            string assignName = null;
            string requestUser = null;
            DateTime? fromDate = null;
            DateTime? toDate = null;
            if (name != Constants.SR_FIRST_KEY_WORD)
            {
                title = name;
            }
            if (!string.IsNullOrEmpty(subcate))
            {
                subcat = int.Parse(subcate);
            }
            if (!string.IsNullOrEmpty(status))
            {
                statusId = int.Parse(status);
            }
            if (!string.IsNullOrEmpty(category))
            {
                categoryId = int.Parse(category);
            }
            if (!string.IsNullOrEmpty(assignto))
            {
                assignName = assignto;
            }
            if (!string.IsNullOrEmpty(requestor))
            {
                requestUser = requestor;
            }
            if (!string.IsNullOrEmpty(startdate))
            {
                fromDate = DateTime.Parse(startdate);
            }
            if (!string.IsNullOrEmpty(enddate))
            {
                toDate = DateTime.Parse(enddate);
            }
            #endregion

            int totalRecords = new ServiceRequestDao().GetCountList4Admin(title, subcat, categoryId, statusId, assignName, fromDate, toDate, requestUser);
            int totalPages = (int)Math.Ceiling((float)totalRecords / (float)rowCount);
            int currentPage = CommonFunc.SetJqGridPageIndex(totalRecords, pageIndex, rowCount);

            List<SR_ServiceRequest> finalList = new ServiceRequestDao().GetList4Admin(sortColumn,sortOrder, (currentPage - 1) * rowCount, rowCount,
                                                                                    title, subcat, categoryId, statusId, assignName, fromDate, toDate, requestUser);

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
                             m.ID.ToString(),         
                            "<span class='"+ m.SR_Urgency.Icon + "'>" + "&nbsp;&nbsp;&nbsp;&nbsp; </span>" ,
                            "<a href='javascript: CRM.chooseSR(\""+ Constants.SR_SERVICE_REQUEST_PREFIX + m.ID+"\");'>" +Constants.SR_SERVICE_REQUEST_PREFIX+ m.ID +"</a>",
                            m.Title.Length > Constants.SR_MAX_LENGTH_TITLE? m.Title.Substring(0, Constants.SR_MAX_LENGTH_TITLE)+ "..." :m.Title,
                            m.SR_Category.SR_Category1.Name ,
                            m.SR_Category.Name,                            
                            m.SR_Status.Name,
                            m.AssignUser,    
                            m.CreateDate.ToString(Constants.DATETIME_FORMAT_VIEW)
                        }
                    }
                ).ToArray()
            };
            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ListCourse()
        {
            ViewData[CommonDataKey.TRAINING_CENTER_COURSE_TYPE] = new SelectList(new TrainingSkillTypeDao().GetList(), "ID", "Name");
            return View();
        }

        [ValidateInput(false)]
        public ActionResult GetListCourseJQGrid(string name, string courseType, string type)
        {
            #region JQGrid Params
            string sortColumn = (Request.Params[GridConstants.SORT_COLUMN]).ToString();
            string sortOrder = (Request.Params[GridConstants.SORT_ORDER]).ToString();
            int pageIndex = Convert.ToInt32(Request.Params[GridConstants.PAGE_INDEX]);
            int rowCount = Convert.ToInt32(Request.Params[GridConstants.ROW_COUNT]);
            #endregion

            if (name.Trim().ToLower().Equals(Constants.TRAINING_CENTER_COURSE_LIST_TXT_KEYWORD_LABEL.ToLower()))
                name = string.Empty;
            int? ntype = ConvertUtil.ConvertToInt(type);
            int? nCourseType = ConvertUtil.ConvertToInt(courseType);
            if (ntype == 0)
                ntype = null;
            if (nCourseType == 0)
                nCourseType = null;
            var list = new TrainingCourseDao().GetList(name, nCourseType, ntype);
            int totalRecords = list.Count;
            int totalPages = (int)Math.Ceiling((float)totalRecords / (float)rowCount);
            //Sort the list
            var finalList = new TrainingCourseDao().Sort(list, sortColumn, sortOrder)
                                  .Skip((pageIndex - 1) * rowCount).Take(rowCount).ToList();

            var jsonData = new
            {
                total = totalPages,
                page = pageIndex,
                records = totalRecords,
                rows = (
                    from m in finalList
                    select new
                    {
                        i = m.ID,
                        cell = new string[] {
                            m.ID.ToString(),
                            m.CourseId,
                            CommonFunc.Link(m.ID.ToString(), "javascript:chooseCourse(\"" + m.Name + "\", " + m.ID + ");", m.Name, false),
                            m.TypeName,
                            m.StatusName,
                            CommonFunc.ShowActiveImage(m.Active),               
                            m.KeyTrainers
                        }
                    }
                ).ToArray()
            };
            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }
    }
}
