using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Configuration;
using CRM.Library.Common;
using Excel = Microsoft.Office.Interop.Excel;
using System.IO;
using CRM.Models;
using System.Text.RegularExpressions;
using System.Collections;
using System.Globalization;

namespace CRM.Controllers
{
    public class ImportController : BaseController
    {
        private EmployeeStatusDao empStatusDao = new EmployeeStatusDao();
        private EmployeeDao empDao = new EmployeeDao();
        private STTDao sttDao = new STTDao();
        private CandidateDao canDao = new CandidateDao();
        private InterviewDao interDao = new InterviewDao();
        private DepartmentDao departmentDao = new DepartmentDao();
        private JobTitleLevelDao jobTitleLevelDao = new JobTitleLevelDao();
        private PTOReportDao ptoReportDao = new PTOReportDao();
        //
        // GET: /Import/

        public ActionResult Index()
        {
            return View();
        }

        private Message CheckEmailExits(string email)
        {
            Message msg = null;
            string userName = email.Split('@')[0];
            string emailOffice = string.Empty;
            AuthenticateDao authenticateDao = new AuthenticateDao();
            bool isExitsinAD = false;
            bool isExitsinEmployee = false;
            isExitsinAD = authenticateDao.CheckExistInAD(userName);
            List<sp_GetEmployeeResult> empList = empDao.GetList("", 0, 0, 0, Constants.EMPLOYEE_ACTIVE, Constants.RESIGNED).Where(p => p.OfficeEmail != null &&  p.OfficeEmail.Contains(email)).ToList<sp_GetEmployeeResult>();
            List<sp_GetSTTResult> sttList = new STTDao().GetList("", 0, 0, null, null, null, null, "").Where(p => p.OfficeEmail != null && p.OfficeEmail.Contains(email)).ToList<sp_GetSTTResult>();
            if (empList.Count <= 0 && sttList.Count <= 0)
            {
                isExitsinEmployee = true;
            }
            if (isExitsinEmployee == false)
            {
                msg = new Message(MessageConstants.E0003, MessageType.Error, "Email " + email);
            }
            else if (isExitsinAD == false)
            {
                msg = new Message(MessageConstants.E0005, MessageType.Error, "Email " + email, "system");
            }
            return msg;
        }

        [HttpPost]
        public ActionResult Candidate(FormCollection form)
        {
            AuthenticationProjectPrincipal principal = HttpContext.User as AuthenticationProjectPrincipal;
            List<string> success = new List<string>();
            List<string> notSuccess = new List<string>();

            HttpPostedFileBase hpf = Request.Files["file"] as HttpPostedFileBase;
            string strPath = Server.MapPath(Constants.IMPORT_EMS);
            string fileName = Path.GetFileName(hpf.FileName);
            fileName = ConvertUtil.FormatFileName(fileName);
            if (System.IO.File.Exists(strPath + fileName))
            {
                System.IO.File.Delete(strPath + fileName);
            }
            hpf.SaveAs(strPath + fileName);

            List<Hashtable> items1 = ReadCandidateDataFromExcel(fileName, 4, 566, 1);

            for (int i = 0; i < items1.Count; i++)
            {

                Message msg = InsertCandidate(items1[i], principal);
                if (msg.MsgType == MessageType.Error)
                {
                    notSuccess.Add(items1[i]["A"].ToString() + " Exception: " + msg.MsgText);
                }
                else
                {
                    success.Add(items1[i]["A"].ToString());
                }
            }

            List<Hashtable> items2 = ReadCandidateDataFromExcel(fileName, 2, 958, 2);

            for (int i = 0; i < items2.Count; i++)
            {                
                Message msg = InsertCandidate(items2[i], principal);
                if (msg.MsgType == MessageType.Error)
                {
                    notSuccess.Add(items2[i]["A"].ToString() + " Exception: " + msg.MsgText);
                }
                else
                {
                    success.Add(items2[i]["A"].ToString());
                }
            }

            ViewData["SUCCESS"] = success;
            ViewData["NOTSUCCESS"] = notSuccess;

            return View();
        }


        [HttpPost]
        public ActionResult Interview(FormCollection form)
        {
            AuthenticationProjectPrincipal principal = HttpContext.User as AuthenticationProjectPrincipal;
            List<string> success = new List<string>();
            List<string> notSuccess = new List<string>();

            HttpPostedFileBase hpf = Request.Files["file"] as HttpPostedFileBase;
            string strPath = Server.MapPath(Constants.IMPORT_EMS);
            string fileName = Path.GetFileName(hpf.FileName);
            fileName = ConvertUtil.FormatFileName(fileName);
            if (System.IO.File.Exists(strPath + fileName))
            {
                System.IO.File.Delete(strPath + fileName);
            }
            hpf.SaveAs(strPath + fileName);

            List<Hashtable> items1 = ReadInterviewDataFromExcel(fileName, 3, 3, 2);

            for (int i = 0; i < items1.Count; i++)
            {
                Candidate can = null;
                if (!string.IsNullOrEmpty(items1[i]["F"].ToString()))
                {
                    can = canDao.GetByEmail(items1[i]["F"].ToString().Trim());
                }
                if (can == null)
                {
                    can =  canDao.GetByName(items1[i]["B"].ToString());

                    if (can == null)
                    {
                        // insert candidate and set new candidate to "can"
                        InsertCandidateForInterview(items1[i], principal);
                    }
                    can = canDao.GetByName(items1[i]["B"].ToString());
                }
                
                if (can != null)
                {
                    can.JRId = 111; // hard code
                    canDao.UpdateJR(can);

                    Message msg = InsertInterview1(items1[i], principal, can);

                    if (msg.MsgType == MessageType.Error)
                    {
                        notSuccess.Add("Round 1: " + items1[i]["A"].ToString() + " Exception: " + msg.MsgText);
                    }
                    else
                    {
                        success.Add("Round 1: " + items1[i]["A"].ToString());
                    }

                    msg = InsertInterview2(items1[i], principal, can);
                    if (msg != null)
                    {
                        if (msg.MsgType == MessageType.Error)
                        {
                            notSuccess.Add("Round 2: " + items1[i]["A"].ToString() + " Exception: " + msg.MsgText);
                        }
                        else
                        {
                            success.Add("Round 2: " + items1[i]["A"].ToString());
                        }
                    }

                    msg = InsertInterview3(items1[i], principal, can);
                    if (msg != null)
                    {
                        if (msg.MsgType == MessageType.Error)
                        {
                            notSuccess.Add("Round 3: " + items1[i]["A"].ToString() + " Exception: " + msg.MsgText);
                        }
                        else
                        {
                            success.Add("Round 3: " + items1[i]["A"].ToString());
                        }
                    }
                }
                else
                {
                    notSuccess.Add(items1[i]["A"].ToString() + " Exception: Not exist in Candidate table");
                }
            }

            ViewData["SUCCESS"] = success;
            ViewData["NOTSUCCESS"] = notSuccess;

            return View();
        }

        private Hashtable data;
        private int getResultId(string result)
        {
            // default is fail
            if (data == null)
            {
                data = new Hashtable();
                data["passed"] = 1;
                data["failed"] = 2;
                data["waiting"] = 3;
                data["recruit"] = 4;
                data["passed"] = 1;
            }

            // default is failed
            int intResult = 2;
            if (data[result] != null)
            {
                intResult = (int)data[result];
            }
            return intResult;
        }
        private List<CandidateSource> sources;

        private DateTime GetInterviewDate(Hashtable item, string dateCol, string timeCol)
        {
            DateTime date;

            try
            {
                string dateFormat = "d-MMM-yy";
                string dateTime = item[dateCol].ToString();
                if (item[timeCol].ToString() != "" && item[timeCol].ToString() != "N/A")
                {
                    dateTime = dateTime + " " + item[timeCol].ToString();
                    dateFormat = dateFormat + " h:m";
                }
                date = DateTime.ParseExact(dateTime, dateFormat, CultureInfo.InvariantCulture);
            }
            catch
            {
                date = DateTime.ParseExact(item["G"].ToString(), "d-MMM-yy", CultureInfo.InvariantCulture);
            }

            return date;
        }

        private Message InsertCandidateForInterview(Hashtable item, AuthenticationProjectPrincipal principal)
        {
            Message msg = null;
            try
            {
                Candidate candidate = new Candidate();
                string[] fullName = item["B"].ToString().Split(' ');
                candidate.FirstName = fullName[0];
                candidate.LastName = fullName[fullName.Length - 1];
                if (fullName.Length > 2)
                {
                    for (int i = 1; i < fullName.Length - 1; i++)
                    {
                        candidate.MiddleName += fullName[i] + " ";
                    }
                    candidate.MiddleName = candidate.MiddleName.Trim();
                }

                string[] fullVNName = item["C"].ToString().Split(' ');
                candidate.VnFirstName = fullVNName[0];
                candidate.VnLastName = fullVNName[fullVNName.Length - 1];
                if (fullVNName.Length > 2)
                {
                    for (int i = 1; i < fullVNName.Length - 1; i++)
                    {
                        candidate.VnMiddleName += fullVNName[i] + " ";
                    }
                    candidate.VnMiddleName = candidate.VnMiddleName.Trim();
                }
                try
                {
                    candidate.DOB = DateTime.ParseExact(item["D"].ToString(), "d-MMM-yy", CultureInfo.InvariantCulture);
                    candidate.Note = item["N"].ToString();
                }
                catch
                {
                    if (item["D"].ToString().Trim() == "N/A" || item["D"].ToString().Trim() == "")
                    {
                        candidate.Note = item["N"].ToString();
                    }
                    else
                    {
                        candidate.Note = item["N"].ToString() + " - DOB: " + item["D"].ToString();
                    }
                }
                candidate.CellPhone = item["E"].ToString();
                candidate.Email = item["F"].ToString();                

                candidate.SearchDate = DateTime.ParseExact(item["G"].ToString(), "d-MMM-yy", CultureInfo.InvariantCulture);
                
                candidate.Status = 1;
                candidate.Gender = (item["I"].ToString().ToLower() == "male");
                candidate.Address = item["M"].ToString();

                candidate.SourceId = GetSourceId(item["J"].ToString());

                candidate.UniversityId = GetUniversityId(item["K"].ToString());
                if (item["L"].ToString().Trim() == "N/A")
                {
                    item["L"] = "Software Developer";
                }

                candidate.TitleId = GetJobTitleId(item["L"].ToString());
                candidate.CreatedBy = principal.UserData.UserName;
                candidate.CreateDate = DateTime.Now;
                candidate.UpdatedBy = principal.UserData.UserName;
                candidate.UpdateDate = DateTime.Now;

                CandidateDao dao = new CandidateDao();
                msg = dao.Inser(candidate);
            }
            catch (Exception ex)
            {
                msg = new Message(MessageConstants.E0033, MessageType.Error, ex.Message);
            }

            return msg;
        }

        private Message InsertInterview1(Hashtable item, AuthenticationProjectPrincipal principal, Candidate can)
        {
            Message msg = null;

            // round 1                    
            Interview interview1 = new Interview();
            interview1.CandidateId = can.ID;
            interview1.OldInterView = false;
            interview1.InterviewDate = GetInterviewDate(item, "Q", "P");
            interview1.Venue = null;
            interview1.Pic = item["S"].ToString();
            interview1.Note = item["T"].ToString();
            interview1.InterviewFormId = "INT-1";

            interview1.InterviewStatusId = 1; // round => dependent on master data
            interview1.Round = 1;

            interview1.InterviewResultId = getResultId(item["U"].ToString().ToLower());
            if (item["U"].ToString().ToLower().Equals("absent"))
            {
                interview1.Note = interview1.Note + "  - Absent";
            }

            interview1.CreatedDate = DateTime.Now;
            interview1.CreatedBy = principal.UserData.UserName;
            interview1.UpdatedDate = DateTime.Now;
            interview1.UpdatedBy = principal.UserData.UserName;

            msg = interDao.Insert(interview1);
            return msg;
        }

        private Message InsertInterview2(Hashtable item, AuthenticationProjectPrincipal principal, Candidate can)
        {
            Message msg = null;

            // if round passed then insearch round 2
            if (item["U"].ToString().ToLower().Equals("passed"))
            {
                Interview interview2 = new Interview();
                interview2.CandidateId = can.ID;
                interview2.OldInterView = false;
                interview2.InterviewDate = GetInterviewDate(item, "W", "V");

                interview2.Venue = null;
                if (item["Y"].ToString().Equals("English Test"))
                {
                    interview2.Pic = "Kim.Nguyen";
                }
                else
                {
                    interview2.Pic = item["Y"].ToString();
                }


                interview2.Note = item["Z"].ToString();
                interview2.InterviewFormId = "INT-2";

                interview2.InterviewStatusId = 2; // round => dependent on master data
                interview2.Round = 2;

                interview2.InterviewResultId = getResultId(item["AA"].ToString().ToLower());
                if (item["AA"].ToString().ToLower().Equals("absent"))
                {
                    interview2.Note = interview2.Note + " - Absent";
                }

                interview2.CreatedDate = DateTime.Now;
                interview2.CreatedBy = principal.UserData.UserName;
                interview2.UpdatedDate = DateTime.Now;
                interview2.UpdatedBy = principal.UserData.UserName;

                msg = interDao.Insert(interview2);
            }            
            return msg;
        }

        private Message InsertInterview3(Hashtable item, AuthenticationProjectPrincipal principal, Candidate can)
        {
            Message msg = null;

            if (item["AA"].ToString().ToLower().Equals("passed"))
            {
                Interview interview3 = new Interview();
                interview3.CandidateId = can.ID;
                interview3.OldInterView = false;                
                interview3.InterviewDate = GetInterviewDate(item, "AC", "AB");
                
                interview3.Venue = null;
                interview3.Pic = item["AE"].ToString();
                interview3.Note = item["AF"].ToString();
                interview3.InterviewFormId = "INT-3";

                interview3.InterviewStatusId = 3; // round => dependent on master data
                interview3.Round = 3;

                interview3.InterviewResultId = getResultId(item["AG"].ToString().ToLower());
                if (item["AG"].ToString().ToLower().Equals("absent"))
                {
                    interview3.Note = interview3.Note + "  - Absent";
                }

                interview3.CreatedDate = DateTime.Now;
                interview3.CreatedBy = principal.UserData.UserName;
                interview3.UpdatedDate = DateTime.Now;
                interview3.UpdatedBy = principal.UserData.UserName;

                msg = interDao.Insert(interview3);
            }

            return msg;
        }
        
        private List<University> universities;
        private List<JobTitleLevel> jobTitleLevels;

        private int GetUniversityId(string universityName)
        {            
            if (universities == null)
            {
                universities = canDao.GetListUniversity();
            }
            University source = universities.Where(s => s.Name.ToLower().Trim() == universityName.ToLower().Trim()).SingleOrDefault<University>();

            return source != null ? source.ID : 390;
        }
        
        private int GetSourceId(string sourceName)
        {            
            if (sources == null)
            {
                sources = canDao.GetListSourceItem();
            }
            CandidateSource source = sources.Where(s => s.Name.ToLower().Trim() == sourceName.ToLower().Trim()).Single<CandidateSource>();

            return source != null ? source.SourceId : 0;
        }

        private int GetJobTitleId(string displayName)
        {
            if (jobTitleLevels == null)
            {
                JobTitleLevelDao titleDao = new JobTitleLevelDao();
                jobTitleLevels = titleDao.GetList();
            }

            JobTitleLevel title = jobTitleLevels.Where(j => j.DisplayName.Trim().ToLower() == displayName.Trim().ToLower()).Single<JobTitleLevel>();
            return title != null ? title.ID : 0;          
        }

        private Message InsertCandidate(Hashtable item, AuthenticationProjectPrincipal principal)
        {
            Message msg = null;
            try
            {
                Candidate candidate = new Candidate();
                string[] fullName = item["B"].ToString().Split(' ');
                candidate.FirstName = fullName[0];
                candidate.LastName = fullName[fullName.Length - 1];
                if (fullName.Length > 2)
                {
                    for (int i = 1; i < fullName.Length - 1; i++)
                    {
                        candidate.MiddleName += fullName[i] + " ";
                    }
                    candidate.MiddleName = candidate.MiddleName.Trim();
                }

                string[] fullVNName = item["C"].ToString().Split(' ');
                candidate.VnFirstName = fullVNName[0];
                candidate.VnLastName = fullVNName[fullVNName.Length - 1];
                if (fullVNName.Length > 2)
                {
                    for (int i = 1; i < fullVNName.Length - 1; i++)
                    {
                        candidate.VnMiddleName += fullVNName[i] + " ";
                    }
                    candidate.VnMiddleName = candidate.VnMiddleName.Trim();
                }
                try
                {
                    candidate.DOB = DateTime.ParseExact(item["D"].ToString(), "d-MMM-yy", CultureInfo.InvariantCulture);
                    candidate.Note = item["N"].ToString();
                }
                catch
                {
                    if (item["D"].ToString().Trim() == "N/A" || item["D"].ToString().Trim() == "")
                    {
                        candidate.Note = item["N"].ToString();
                    }
                    else
                    {
                        candidate.Note = item["N"].ToString() + " - DOB: " + item["D"].ToString();
                    }
                }                
                candidate.CellPhone = item["E"].ToString();
                candidate.Email = item["F"].ToString();

                if (item["G"].ToString().Trim() == "5 Arpil 2010")
                {
                    item["G"] = "4/5/2010";
                }

                if (item["G"].ToString().IndexOf("-") > 0)
                {
                    candidate.SearchDate = DateTime.ParseExact(item["G"].ToString(), "dd-MM-yyyy", CultureInfo.InvariantCulture);
                }
                else
                {
                    candidate.SearchDate = DateTime.ParseExact(item["G"].ToString(), "M/d/yyyy", CultureInfo.InvariantCulture);
                }
                candidate.Status = 1;
                candidate.Gender = (item["I"].ToString().ToLower() == "male");
                candidate.Address = item["L"].ToString();
                
                candidate.SourceId = GetSourceId(item["J"].ToString());
                candidate.UniversityId = GetUniversityId(item["K"].ToString());
                if (item["L"].ToString().Trim() == "N/A")
                {
                    item["L"] = "Software Developer";
                }

                candidate.TitleId = GetJobTitleId(item["L"].ToString());                
                candidate.CreatedBy = principal.UserData.UserName;
                candidate.CreateDate = DateTime.Now;
                candidate.UpdatedBy = principal.UserData.UserName;
                candidate.UpdateDate = DateTime.Now;
                
                CandidateDao dao = new CandidateDao();
                msg = dao.Inser(candidate);
            }
            catch (Exception ex)
            {
                msg = new Message(MessageConstants.E0033, MessageType.Error, ex.Message);                
            }

            return msg;
        }

        private List<Hashtable> ReadInterviewDataFromExcel(string fileName, int startRow, int endRow, int sheetIndex)
        {
            List<Hashtable> list = new List<Hashtable>();

            string[] colNames = new string[] { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", 
                "L", "M", "N","O","P","Q","R","S","T","U","V", "W","X","Y","Z",
                "AA", "AB", "AC", "AD", "AE", "AF", "AG", "AH"};

            Excel.Application xlApp = new Excel.Application();
            Excel.Workbook xlWorkBook = xlApp.Workbooks.Open(Server.MapPath(Constants.IMPORT_EMS + fileName));
            Excel.Worksheet xlWorkSheet = xlWorkBook.Worksheets.get_Item(sheetIndex);
            try
            {
                for (int i = startRow; i <= endRow; i++)
                {
                    Hashtable item = new Hashtable();
                    for (int j = 0; j < colNames.Length; j++)
                    {
                        Excel.Range range = xlWorkSheet.get_Range(colNames[j] + i);
                        string text = range.Text;
                        item.Add(colNames[j], text.Trim());
                    }
                    list.Add(item);
                }
            }
            finally
            {
                xlWorkBook.Close();
                xlApp.Quit();
                System.Runtime.InteropServices.Marshal.ReleaseComObject(xlWorkSheet);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(xlWorkBook);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(xlApp);
                xlWorkSheet = null;
                xlWorkBook = null;
                xlApp = null;
            }

            return list;
        }

        private List<Hashtable> ReadSeatCodeDataFromExcel(string fileName, int startRow, int endRow, int sheetIndex)
        {
            List<Hashtable> list = new List<Hashtable>();

            string[] colNames = new string[] { "A", "B", "C", "D", "E", "F", "G"};

            Excel.Application xlApp = new Excel.Application();
            Excel.Workbook xlWorkBook = xlApp.Workbooks.Open(Server.MapPath(Constants.IMPORT_EMS + fileName));
            Excel.Worksheet xlWorkSheet = xlWorkBook.Worksheets.get_Item(sheetIndex);
            int maxRow = xlWorkSheet.UsedRange.Rows.Count;
            try
            {
                for (int i = startRow; i <= maxRow; i++)
                {
                    Hashtable item = new Hashtable();
                    for (int j = 0; j < colNames.Length; j++)
                    {
                        Excel.Range range = xlWorkSheet.get_Range(colNames[j] + i);
                        string text = range.Text;
                        item.Add(colNames[j], text.Trim());
                    }
                    list.Add(item);
                }
            }
            finally
            {
                xlWorkBook.Close();
                xlApp.Quit();
                System.Runtime.InteropServices.Marshal.ReleaseComObject(xlWorkSheet);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(xlWorkBook);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(xlApp);
                xlWorkSheet = null;
                xlWorkBook = null;
                xlApp = null;
            }

            return list;
        }
        private List<Hashtable> ReadDataFromExcel(string fileName, int startRow, int endRow, int sheetIndex, string[] colNames)
        {
            List<Hashtable> list = new List<Hashtable>();

            //string[] colNames = new string[] { "C", "E"};//C: EmpId, E: Balance


            Excel.Application xlApp = new Excel.Application();
            Excel.Workbook xlWorkBook = xlApp.Workbooks.Open(Server.MapPath(Constants.IMPORT_EMS + fileName));
            Excel.Worksheet xlWorkSheet = xlWorkBook.Worksheets.get_Item(sheetIndex);
            int maxRow = xlWorkSheet.UsedRange.Rows.Count;
            try
            {
                for (int i = startRow; i <= maxRow; i++)
                {
                    Hashtable item = new Hashtable();
                    for (int j = 0; j < colNames.Length; j++)
                    {
                        Excel.Range range = xlWorkSheet.get_Range(colNames[j] + i);
                        string text = range.Text;
                        item.Add(colNames[j], text.Trim());
                    }
                    list.Add(item);
                }
            }
            finally
            {
                xlWorkBook.Close();
                xlApp.Quit();
                System.Runtime.InteropServices.Marshal.ReleaseComObject(xlWorkSheet);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(xlWorkBook);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(xlApp);
                xlWorkSheet = null;
                xlWorkBook = null;
                xlApp = null;
            }

            return list;
        }
        private List<Hashtable> ReadCandidateDataFromExcel(string fileName, int startRow, int endRow, int sheetIndex)
        {
            List<Hashtable> list = new List<Hashtable>();

            string[] colNames = new string[] { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M","N" };

            Excel.Application xlApp = new Excel.Application();
            Excel.Workbook xlWorkBook = xlApp.Workbooks.Open(Server.MapPath(Constants.IMPORT_EMS + fileName));
            Excel.Worksheet xlWorkSheet = xlWorkBook.Worksheets.get_Item(sheetIndex);
            try
            {
                for (int i = startRow; i <= endRow; i++)
                {
                    Hashtable item = new Hashtable();
                    for (int j = 0; j < colNames.Length; j++)
                    {
                        Excel.Range range = xlWorkSheet.get_Range(colNames[j] + i);
                        string text = range.Text;                        
                        item.Add(colNames[j], text.Trim());
                    }
                    list.Add(item);
                }
            }
            finally
            {
                xlWorkBook.Close();
                xlApp.Quit();
                System.Runtime.InteropServices.Marshal.ReleaseComObject(xlWorkSheet);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(xlWorkBook);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(xlApp);
                xlWorkSheet = null;
                xlWorkBook = null;
                xlApp = null;
            }

            return list;
        }

        [HttpPost]
        public ActionResult EmployeeContract(FormCollection form)
        {
            Message msg = null;
            AuthenticationProjectPrincipal principal = HttpContext.User as AuthenticationProjectPrincipal;
            List<string> success = new List<string>();
            List<string> notSuccess = new List<string>();

            HttpPostedFileBase hpf = Request.Files["file"] as HttpPostedFileBase;
            string strPath = Server.MapPath(Constants.IMPORT_EMS);
            string fileName = Path.GetFileName(hpf.FileName);
            fileName = ConvertUtil.FormatFileName(fileName);
            if (System.IO.File.Exists(strPath + fileName))
            {
                System.IO.File.Delete(strPath + fileName);
            }
            hpf.SaveAs(strPath + fileName);

            List<Hashtable> items = ReadDataFromExcel(fileName, 3, 377);

            for (int i = 0; i < items.Count; i++)
            {
                try
                {
                    bool isSuccess = InsertContract(items[i], principal);
                    if (isSuccess)
                    {
                        success.Add((string)items[i]["A"]);
                    }
                    else
                    {
                        notSuccess.Add((string)items[i]["A"]);
                    }
                }
                catch (Exception ex)
                {
                    notSuccess.Add((string)items[i]["A"] + " - Exception: " + ex.Message);
                }
            }

            if (msg != null)
            {
                ShowMessage(msg);
            }
            ViewData["SUCCESS"] = success;
            ViewData["NOTSUCCESS"] = notSuccess;
            return View();
        }

        EmployeeDao employeeDAO = new EmployeeDao();
        ContractRenewalDao contractDAO = new ContractRenewalDao();
        string dateFormat1 = "d-MMM-yy";
        string dateFormat2 = "d:M:yyyy";

        private bool InsertContract(Hashtable item, AuthenticationProjectPrincipal principal)
        {
            bool isSuccess = true;
            int Probation = 1;
            int ExtendedProbation = 2;
            int FirstYearContract = 3;
            int SecondYearContract = 4;
            int PermanentContract = 5;
            string fileDefault = "empty.doc";

            List<string> permanentContracts = new List<string>(){"1001","1013","1017","1029","1031","1034","1046",
            "1049","1051","1052","1053","1057","1059","1064","1065"};
            
            //try
            //{                
                if (employeeDAO.GetById(item["A"].ToString()) != null)
                {                
                    Contract contract = new Contract();
                    contract.EmployeeId = item["A"].ToString();                    
                    contract.NotificationClosed = isNotificationClose(item);
                    contract.ContractFile = fileDefault;
                                       
                    contract.CreatedBy = principal.UserData.UserName;
                    contract.UpdatedBy = principal.UserData.UserName;
                    contract.UpdatedDate = DateTime.Now;
                    contract.CreatedDate = DateTime.Now;
                    contract.DeleteFlag = false;

                    if (permanentContracts.Contains(contract.EmployeeId))
                    {
                        contract.ContractType = PermanentContract;
                        contract.NotificationClosed = true;
                        contract.StartDate = DateTime.ParseExact(item["E"].ToString(), dateFormat2, CultureInfo.InvariantCulture);
                        Message msg = contractDAO.Insert(contract);
                        if (msg.MsgType == MessageType.Error)
                        {
                            throw new Exception(msg.MsgText);
                        }
                    }
                    else
                    {
                        // probation contract
                        if (!string.IsNullOrEmpty(item["C"].ToString()))
                        {
                            Contract tmpContract = cloneContract(contract);

                            if (!string.IsNullOrEmpty(item["E"].ToString()))
                            {
                                tmpContract.NotificationClosed = true;
                            }

                            tmpContract.ContractType = Probation;
                            tmpContract.StartDate = DateTime.ParseExact(item["C"].ToString(), dateFormat1, CultureInfo.InvariantCulture);
                            if (!string.IsNullOrEmpty(item["D"].ToString()))
                            {
                                tmpContract.EndDate = DateTime.ParseExact(item["D"].ToString(), dateFormat1, CultureInfo.InvariantCulture);
                            }

                            Message msg = contractDAO.Insert(tmpContract);
                            if (msg.MsgType == MessageType.Error)
                            {
                                throw new Exception(msg.MsgText);
                            }
                        }

                        // first year contract
                        if (!string.IsNullOrEmpty(item["E"].ToString()))
                        {                            
                            Contract tmpContract = cloneContract(contract);
                            if (!string.IsNullOrEmpty(item["G"].ToString()))
                            {
                                tmpContract.NotificationClosed = true;
                            }
   
                            tmpContract.ContractType = FirstYearContract;                                                        
                            tmpContract.StartDate = DateTime.ParseExact(item["E"].ToString(), dateFormat2, CultureInfo.InvariantCulture);
                            if (!string.IsNullOrEmpty(item["F"].ToString()))
                            {
                                tmpContract.EndDate = DateTime.ParseExact(item["F"].ToString(), dateFormat2, CultureInfo.InvariantCulture);
                            }
                            
                            Message msg = contractDAO.Insert(tmpContract);
                            if (msg.MsgType == MessageType.Error)
                            {
                                throw new Exception(msg.MsgText);
                            }
                        }

                        // second year contract
                        if (!string.IsNullOrEmpty(item["G"].ToString()))
                        {                            
                            Contract tmpContract = cloneContract(contract);                            
                            if (!string.IsNullOrEmpty(item["I"].ToString()))
                            {
                                tmpContract.NotificationClosed = true;
                            }
                            
                            tmpContract.ContractType = SecondYearContract;
                            tmpContract.StartDate = DateTime.ParseExact(item["G"].ToString(), dateFormat2, CultureInfo.InvariantCulture);
                            if (!string.IsNullOrEmpty(item["H"].ToString()))
                            {
                                tmpContract.EndDate = DateTime.ParseExact(item["H"].ToString(), dateFormat2, CultureInfo.InvariantCulture);
                            }

                            Message msg = contractDAO.Insert(tmpContract);
                            if (msg.MsgType == MessageType.Error)
                            {
                                throw new Exception(msg.MsgText);
                            }
                        }

                        // permanent contract
                        if (!string.IsNullOrEmpty(item["I"].ToString()))
                        {
                            Contract tmpContract = cloneContract(contract);
                            
                            tmpContract.ContractType = PermanentContract;                            
                            tmpContract.StartDate = DateTime.ParseExact(item["I"].ToString(), dateFormat2, CultureInfo.InvariantCulture);
                            if (!string.IsNullOrEmpty(item["J"].ToString()))
                            {
                                tmpContract.EndDate = DateTime.ParseExact(item["J"].ToString(), dateFormat2, CultureInfo.InvariantCulture);
                            }

                            Message msg = contractDAO.Insert(tmpContract);
                            if (msg.MsgType == MessageType.Error)
                            {
                                throw new Exception(msg.MsgText);
                            }
                        }
                    }
                }
                else
                {
                    isSuccess = false;
                }
            //}
            //catch(Exception ex)
            //{                
            //    throw ex;
            //}

            return isSuccess;
        }
        
        private Contract cloneContract(Contract contract)
        {
            Contract tmpContract = new Contract();
            tmpContract.EmployeeId = contract.EmployeeId;
            tmpContract.NotificationClosed = contract.NotificationClosed;
            tmpContract.ContractFile = contract.ContractFile;

            tmpContract.CreatedBy = contract.CreatedBy;
            tmpContract.UpdatedBy = contract.UpdatedBy;
            tmpContract.UpdatedDate = contract.UpdatedDate;
            tmpContract.CreatedDate = contract.CreatedDate;
            tmpContract.DeleteFlag = contract.DeleteFlag;
            return tmpContract;
        }

        private bool isNotificationClose(Hashtable item)
        {
            bool isNotification = true;

            // permanent contract
            if (!string.IsNullOrEmpty(item["I"].ToString()))
            {
                isNotification = false;
            }
            else
            {
                if (!string.IsNullOrEmpty(item["G"].ToString()))
                {
                    if (!string.IsNullOrEmpty(item["H"].ToString()))
                    {
                        DateTime endDate = DateTime.ParseExact(item["H"].ToString(), dateFormat2, CultureInfo.InvariantCulture);
                        if (endDate > DateTime.Now)
                        {
                            isNotification = false;
                        }
                        else
                        {
                            isNotification = true;
                        }
                    }
                    else
                    {
                        isNotification = false;
                    }
                }
                else
                {
                    if (!string.IsNullOrEmpty(item["E"].ToString()))
                    {
                        if (!string.IsNullOrEmpty(item["F"].ToString()))
                        {
                            DateTime endDate = DateTime.ParseExact(item["F"].ToString(), dateFormat2, CultureInfo.InvariantCulture);
                            if (endDate > DateTime.Now)
                            {
                                isNotification = false;
                            }
                            else
                            {
                                isNotification = true;
                            }
                        }
                        else 
                        {
                            isNotification = false;
                        }
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(item["C"].ToString()))
                        {
                            if (!string.IsNullOrEmpty(item["D"].ToString()))
                            {
                                DateTime endDate = DateTime.ParseExact(item["D"].ToString(), dateFormat1, CultureInfo.InvariantCulture);
                                if (endDate > DateTime.Now)
                                {
                                    isNotification = false;
                                }
                                else
                                {
                                    isNotification = true;
                                }
                            }
                            else
                            {
                                isNotification = false;
                            }
                        }
                    }
                }
            }

            return !isNotification;
        }

        private List<Hashtable> ReadDataFromExcel(string fileName, int startRow, int endRow)
        {
            List<Hashtable> list = new List<Hashtable>();
                        
            string[] colNames = new string[] { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", };

            Excel.Application xlApp = new Excel.Application();
            Excel.Workbook xlWorkBook = xlApp.Workbooks.Open(Server.MapPath(Constants.IMPORT_EMS + fileName));
            Excel.Worksheet xlWorkSheet = xlWorkBook.Worksheets.get_Item(1);
            try
            {
                for (int i = startRow; i <= endRow; i++)
                {
                    Hashtable item = new Hashtable();
                    for (int j = 0; j < colNames.Length; j++)
                    {
                        Excel.Range range = xlWorkSheet.get_Range(colNames[j] + i);
                        string text = "";
                        if (j >= 2 && range.Text != null)
                        {
                            text = range.Text;
                            text = text.ToLower();
                            text = text.Replace("tháng", ":");
                            text = text.Replace("năm", ":");
                            text = text.Replace(" ", "");
                            
                            if (text == "29:02:2009")
                            {
                                text = "1:3:2009";
                            }
                            else if (text == "29:02:2010")
                            {
                                text = "1:3:2010";
                            }
                            else if (text == "29:02:2011")
                            {
                                text = "1:3:2011";
                            }
                            else if (text == "29-feb-09")
                            {
                                text = "29-mar-09";
                            }
                            else if (text == "29-feb-10")
                            {
                                text = "29-mar-10";
                            }
                            else if (text == "29-feb-11")
                            {
                                text = "29-mar-11";
                            }

                        }
                        else
                        {
                            text = range.Text;
                        }
                        item.Add(colNames[j], text.Trim());
                    }
                    list.Add(item);
                }
            }
            finally
            {
                xlWorkBook.Close();
                xlApp.Quit();
                System.Runtime.InteropServices.Marshal.ReleaseComObject(xlWorkSheet);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(xlWorkBook);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(xlApp);
                xlWorkSheet = null;
                xlWorkBook = null;
                xlApp = null;
            }
           
            return list;
        }

        [HttpPost]
        public ActionResult Import(FormCollection form)
        {
            Message msg = null;
            try
            {
                var principal = HttpContext.User as AuthenticationProjectPrincipal;
                Employee emp = new Employee();
                STT stt = new STT();
                Candidate can = new Candidate();
                int max_size = int.Parse(ConfigurationManager.AppSettings["IMAGE_MAX_SIZE"]);
                HttpPostedFileBase hpf = Request.Files["file"] as HttpPostedFileBase;
                List<Employee> list = new List<Employee>();
                List<STT> sttList = new List<STT>();
                bool isValid = true;
                List<Candidate> canList = new List<Candidate>();
                bool hasEmpty = false;
                string resultDate = string.Empty;
                string id = string.Empty;
                string emailList = string.Empty;
                string action = string.Empty;
                if (!string.IsNullOrEmpty(form["inp_Employee"]))
                {
                    action = form["inp_Employee"];
                }
                else if (!string.IsNullOrEmpty(form["inp_ResignedEmployee"]))
                {
                    action = form["inp_ResignedEmployee"];
                }
                else if (!string.IsNullOrEmpty(form["inp_STT"]))
                {
                    ImportSTTFromPRM();
                    return View("Index");

                    //action = form["inp_STT"];
                }
                else if (!string.IsNullOrEmpty(form["inp_Can"]))
                {
                    action = form["inp_Can"];
                }
                if (hpf.ContentLength != 0)
                {
                    int maxSize = 1024 * 1024 * Constants.CV_MAX_SIZE;
                    //int maxSize = 1024 * 1024 * 2;
                    float sizeFile = float.Parse(hpf.ContentLength.ToString("N"));

                    string extension = Path.GetExtension(hpf.FileName);
                    if (extension.ToLower() != ".xls") //check extension file is valid
                    {
                        msg = new Message(MessageConstants.E0013, MessageType.Error, extension, ".xls", Constants.CV_MAX_SIZE);
                    }
                    else if (sizeFile > maxSize) //check maxlength of uploaded file
                    {
                        msg = new Message(MessageConstants.E0012, MessageType.Error, Constants.CV_MAX_SIZE.ToString());
                    }
                    else
                    {
                        string strPath = Server.MapPath(Constants.IMPORT_EMS);
                        string fileName = Path.GetFileName(hpf.FileName);
                        fileName = ConvertUtil.FormatFileName(fileName);
                        if (System.IO.File.Exists(strPath + fileName))
                        {
                            System.IO.File.Delete(strPath + fileName);
                        }
                        hpf.SaveAs(strPath + fileName);
                        Excel.Application xlApp = new Excel.Application();
                        Excel.Workbook xlWorkBook = xlApp.Workbooks.Open(Server.MapPath(Constants.IMPORT_EMS + fileName));
                        Excel.Worksheet xlWorkSheet = xlWorkBook.Worksheets.get_Item(1);
                        string[] column = null;
                        #region column
                        switch (action)
                        {
                            case "Employee":
                                column = new string[] {  
                            "No",
                            "ID", 
                            "FullName",
                            "VnFullName", 
                            "StatusName", 
                            "DOB", 
                            "POB", 
                            "VnPOB",
                            "PlaceOfOrigin",
                            "VnPlaceOfOrigin",
                            "Nationality",
                            "Gender",
                            "MarriedStatus",
                            "Degree",
                            "Major",
                            "OtherDegree",                            
                            "IDNumber",
                            "IssueDate",
                            "IDIssueLocation",
                            "VnIDIssueLocation",
                            "Race",
                            "Religion",
                            "JR",
                            "JRApproval",
                            "StartDate",
                            "ContractedDate",
                            "Department",
                            "DepartmentName",
                            "TitleName",
                            "TaxID",
                            "TaxIssueDate",
                            "SocialInsuranceNo",
                            "Hospital" ,
                            "LaborUnion",
                            "LaborUnionDate",
                            "HomePhone",
                            "CellPhone",
                            "Floor",
                            "ExtensionNumber",
                            "SeatCode",
                            "SkypeId",
                            "YahooId",
                            "PersonalEmail",
                            "OfficeEmail",
                            "EmergencyContactName",
                            "EmergencyContactPhone",
                            "EmergencyContactRelationship",
                            "PermanentAddress",
                            "VnPermanentAddress",
                            "TempAddress",
                            "VnTempAddress",
                            "BankAccount",
                            "BankName",
                            "Remarks"
                            };
                                break;

                            case "ResignedEmployee":
                                column = new string[] {  
                            "No",
                            "ID", 
                            "FullName",
                            "VnFullName", 
                            "StatusName", 
                            "DOB", 
                            "POB", 
                            "VnPOB",
                            "PlaceOfOrigin",
                            "VnPlaceOfOrigin",
                            "Nationality",
                            "Gender",
                            "MarriedStatus",
                            "Degree",
                            "Major",
                            "OtherDegree",                            
                            "IDNumber",
                            "IssueDate",
                            "IDIssueLocation",
                            "VnIDIssueLocation",
                            "Race",
                            "Religion",
                            "JR",
                            "JRApproval",
                            "StartDate",
                            "ContractedDate",
                            "Department",
                            "DepartmentName",
                            "TitleName",
                            "TaxID",
                            "TaxIssueDate",
                            "SocialInsuranceNo",
                            "Hospital" ,
                            "LaborUnion",
                            "LaborUnionDate",
                            "HomePhone",
                            "CellPhone",
                            "Floor",
                            "ExtensionNumber",
                            "SeatCode",
                            "SkypeId",
                            "YahooId",
                            "PersonalEmail",
                            "OfficeEmail",
                            "EmergencyContactName",
                            "EmergencyContactPhone",
                            "EmergencyContactRelationship",
                            "PermanentAddress",
                            "VnPermanentAddress",
                            "TempAddress",
                            "VnTempAddress",
                            "BankAccount",
                            "BankName",
                            "Remarks"
                            };
                                break;
                            case "STT":



                                /*
                                column = new string[] { 
                                    "No",
                                    "ID", 
                                    "FullName", 
                                    "VnFullName", 
                                    "StatusName", 
                                    "Result",
                                    "ResultDate", 
                                    "DOB", 
                                    "POB", 
                                    "VnPOB", 
                    "PlaceOfOrigin", 
                    "VnPlaceOfOrigin", 
                    "Nationality", 
                    "Gender", 
                    "MarriedStatus", 
                    "Degree",
                    "OtherDegree",
                    "Major", 
                    "IDNumber", 
                    "IssueDate",
                    "IDIssueLocation", 
                    "VnIDIssueLocation", 
                    "Race", 
                    "Religion", 
                    "JR", 
                    "JRApproval", 
                    "StartDate", 
                    "ContractedDate", 
                    "Department",
                    "DepartmentName", 
                    "TitleName", 
                    "LaborUnion", 
                    "LaborUnionDate", 
                    "HomePhone", 
                    "CellPhone",
                    "Floor", 
                    "ExtensionNumber", 
                    "SeatCode", 
                    "SkypeId", 
                    "YahooId", 
                    "PersonalEmail", 
                    "OfficeEmail",
                    "EmergencyContactName", 
                    "EmergencyContactPhone", 
                    "EmergencyContactRelationship", 
                    "BankAccount", 
                    "BankName", 
                    "Remarks",
                    "PermanentAddress", 
                    "VnPermanentAddress", 
                    "TempAddress", 
                    "VnTempAddress" };
                                break;
                            case "Candidate":
                                column = new string[] { 
                                    "No", 
                                    "FullName", 
                                    "VnFullName", 
                                    "DOB", 
                                    "CellPhone", 
                                    "Email", 
                                    "SearchDate", 
                                    "StatusName", 
                                    "Gender", 
                                    "Source", 
                                    "TitleName", 
                                    "Address", 
                                    "Remarks" };
                                 */
                                break;
                        }
                        #endregion
                        string[] _colum_letters = new string[column.Count()];
                        Excel.Range range = null;

                        for (int i = 0; i < column.Count(); i++)
                        {
                            _colum_letters[i] = ExcelColumnIndexToName(i);
                        }
                        for (int index = 4; index < 500; index++)
                        {
                            if (action == "Employee")
                            {
                                emp = new Employee();
                                emp.CreatedBy = principal.UserData.UserName;
                                emp.UpdatedBy = principal.UserData.UserName;
                                emp.UpdateDate = DateTime.Now;
                                emp.CreateDate = DateTime.Now;
                                emp.DeleteFlag = false;
                            }
                            else if (action == "STT")
                            {
                                stt = new STT();
                                stt.CreatedBy = principal.UserData.UserName;
                                stt.UpdatedBy = principal.UserData.UserName;
                                stt.UpdateDate = DateTime.Now;
                                stt.CreateDate = DateTime.Now;
                                stt.DeleteFlag = false;
                            }
                            else if (action == "Candidate")
                            {
                                can = new Candidate();
                                can.CreatedBy = principal.UserData.UserName;
                                can.UpdatedBy = principal.UserData.UserName;
                                can.UpdateDate = DateTime.Now;
                                can.CreateDate = DateTime.Now;
                                can.DeleteFlag = false;
                            }
                            int character = 0;
                            bool firstChar = true;
                            foreach (string col in _colum_letters)
                            {
                                range = xlWorkSheet.get_Range(col + index);
                                //string value = range.Value2 != null ? range.Value2.ToString() : "";

                                string value = (string)range.Text;
                                if (value == "" && firstChar)
                                {
                                    hasEmpty = true;
                                    break;
                                }
                                else
                                {
                                    #region insert
                                    switch (column[character])
                                    {
                                        case "ID":
                                            if (string.IsNullOrEmpty(value))
                                            {
                                                msg = new Message(MessageConstants.E0033, MessageType.Error, "Field " + (col + index) + " is a required field.");
                                                isValid = false;
                                            }
                                            else if (id.TrimEnd(',').Split(',').Contains(value))
                                            {
                                                msg = new Message(MessageConstants.E0033, MessageType.Error, "Field " + (col + index) + " is duplicated.");
                                                isValid = false;
                                            }
                                            else
                                            {
                                                //check exit ind DB
                                                if (action == "Employee")
                                                {
                                                    Employee objDb = empDao.GetById(value);
                                                    if (objDb == null)
                                                    {
                                                        emp.ID = value;
                                                        id += value + ",";
                                                    }
                                                    else
                                                    {
                                                        msg = new Message(MessageConstants.E0033, MessageType.Error, "Field " + (col + index) + " is duplicated in database.");
                                                        isValid = false;
                                                    }
                                                }
                                                else if (action == "STT")
                                                {
                                                    STT objDb = sttDao.GetById(value);
                                                    if (objDb == null)
                                                    {
                                                        stt.ID = value;
                                                        id += value + ",";
                                                    }
                                                    else
                                                    {
                                                        msg = new Message(MessageConstants.E0033, MessageType.Error, "Field " + (col + index) + " is duplicated in database.");
                                                        isValid = false;
                                                    }
                                                }
                                            }
                                            break;
                                        case "FullName":
                                            if (!string.IsNullOrEmpty(value))
                                            {
                                                if (action == "Employee")
                                                {
                                                    string[] arrName = value.Split(' ');
                                                    emp.FirstName = arrName[0];
                                                    emp.LastName = arrName[arrName.Count() - 1];
                                                    string text = value.Replace(arrName[0] + " ", "").Replace(" " + arrName[arrName.Count() - 1], "");
                                                    emp.MiddleName = string.IsNullOrEmpty(text) ? null : text;
                                                }
                                                else if (action == "STT")
                                                {
                                                    string[] arrName = value.Split(' ');
                                                    stt.FirstName = arrName[0];
                                                    stt.LastName = arrName[arrName.Count() - 1];
                                                    string text = value.Replace(arrName[0] + " ", "").Replace(" " + arrName[arrName.Count() - 1], "");
                                                    stt.MiddleName = string.IsNullOrEmpty(text) ? null : text;
                                                }
                                                else if (action == "Candidate")
                                                {
                                                    string[] arrName = value.Split(' ');
                                                    can.FirstName = arrName[0];
                                                    can.LastName = arrName[arrName.Count() - 1];
                                                    string text = value.Replace(arrName[0] + " ", "").Replace(" " + arrName[arrName.Count() - 1], "");
                                                    can.MiddleName = string.IsNullOrEmpty(text) ? null : text;
                                                }
                                            }
                                            else
                                            {
                                                msg = new Message(MessageConstants.E0033, MessageType.Error, "Field " + (col + index) + " is a required field.");
                                                isValid = false;
                                            }
                                            break;
                                        case "VnFullName":
                                            if (!string.IsNullOrEmpty(value))
                                            {
                                                if (action == "Employee")
                                                {
                                                    string[] arrVnName = value.Split(' ');
                                                    emp.VnFirstName = arrVnName[0];
                                                    emp.VnLastName = arrVnName[arrVnName.Count() - 1];
                                                    string text = value.Replace(arrVnName[0] + " ", "").Replace(" " + arrVnName[arrVnName.Count() - 1], "");
                                                    emp.VnMiddleName = string.IsNullOrEmpty(text) ? null : text;
                                                }
                                                else if (action == "STT")
                                                {
                                                    string[] arrVnName = value.Split(' ');
                                                    stt.VnFirstName = arrVnName[0];
                                                    stt.VnLastName = arrVnName[arrVnName.Count() - 1];
                                                    string text = value.Replace(arrVnName[0] + " ", "").Replace(" " + arrVnName[arrVnName.Count() - 1], "");
                                                    stt.VnMiddleName = string.IsNullOrEmpty(text) ? null : text;
                                                }
                                                else if (action == "Candidate")
                                                {
                                                    string[] arrVnName = value.Split(' ');
                                                    can.VnFirstName = arrVnName[0];
                                                    can.VnLastName = arrVnName[arrVnName.Count() - 1];
                                                    string text = value.Replace(arrVnName[0] + " ", "").Replace(" " + arrVnName[arrVnName.Count() - 1], "");
                                                    can.VnMiddleName = string.IsNullOrEmpty(text) ? null : text;
                                                }
                                            }
                                            else
                                            {
                                                msg = new Message(MessageConstants.E0033, MessageType.Error, "Field " + (col + index) + " is a required field.");
                                                isValid = false;
                                            }
                                            break;
                                        case "StatusName":
                                            if (!string.IsNullOrEmpty(value))
                                            {
                                                if (value != "Promoted")
                                                {
                                                    if (action == "Employee")
                                                    {
                                                        emp.EmpStatusId = empStatusDao.GetByName(value).StatusId;
                                                    }
                                                    else if (action == "STT")
                                                    {
                                                        stt.STTStatusId = new STTStatusDao().GetByName(value).ID;
                                                    }
                                                    else if (action == "Candidate")
                                                    {
                                                        if (value == "Available")
                                                        {
                                                            can.Status = (int)CRM.Library.Common.CandidateStatus.Available;
                                                        }
                                                        else if (value == "Unavailable")
                                                        {
                                                            can.Status = (int)CRM.Library.Common.CandidateStatus.Unavailable;
                                                        }
                                                        else
                                                        {
                                                            msg = new Message(MessageConstants.E0033, MessageType.Error, "Field " + (col + index) + " is invalid format.");
                                                            isValid = false;
                                                        }
                                                    }
                                                }
                                                else
                                                {
                                                    msg = new Message(MessageConstants.E0033, MessageType.Error, "Field " + (col + index) + " is invalid format.");
                                                    isValid = false;
                                                }
                                            }
                                            else
                                            {
                                                msg = new Message(MessageConstants.E0033, MessageType.Error, "Field " + (col + index) + " is a required field.");
                                                isValid = false;
                                            }
                                            break;
                                        case "Result":
                                            if (!string.IsNullOrEmpty(value))
                                            {
                                                stt.ResultId = new STTResultDao().GetByName(value).ID;
                                            }
                                            else
                                            {
                                                stt.ResultId = null;
                                            }
                                            break;
                                        case "Major":
                                            if (action == "Employee")
                                            {
                                                emp.Major = string.IsNullOrEmpty(value) ? null : value;
                                            }
                                            else if (action == "STT")
                                            {
                                                stt.Major = string.IsNullOrEmpty(value) ? null : value;
                                            }
                                            break;
                                        case "Source":
                                            if (!string.IsNullOrEmpty(value))
                                            {
                                                CandidateSource objSource = canDao.GetSourceByName(value);
                                                if (objSource != null)
                                                {
                                                    can.SourceId = objSource.SourceId;
                                                }
                                                else
                                                {
                                                    msg = new Message(MessageConstants.E0033, MessageType.Error, "Field " + (col + index) + " is invalid format.");
                                                    isValid = false;
                                                }
                                            }
                                            else
                                            {
                                                msg = new Message(MessageConstants.E0033, MessageType.Error, "Field " + (col + index) + " is a required field.");
                                                isValid = false;
                                            }
                                            break;
                                        case "ResultDate":
                                            if (!string.IsNullOrEmpty(value))
                                            {
                                                if (!stt.ResultId.HasValue)
                                                {
                                                    msg = new Message(MessageConstants.E0033, MessageType.Error, "Field " + (col + index) + " has value when Result has value.");
                                                    isValid = false;
                                                }
                                                else
                                                {
                                                    DateTime date = new DateTime();
                                                    bool isDate = DateTime.TryParse(value, out date);
                                                    if (isDate)
                                                    {
                                                        resultDate += value + ",";
                                                    }
                                                    else
                                                    {
                                                        msg = new Message(MessageConstants.E0033, MessageType.Error, "Field " + (col + index) + " is invalid date.");
                                                        isValid = false;
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                if (stt.ResultId.HasValue)
                                                {
                                                    msg = new Message(MessageConstants.E0033, MessageType.Error, "Field " + (col + index) + " is a required field when Result has value.");
                                                    isValid = false;
                                                }
                                            }
                                            break;
                                        case "DOB":
                                            if (!string.IsNullOrEmpty(value))
                                            {
                                                DateTime dob = new DateTime();
                                                bool valid = DateTime.TryParse(value, out dob);
                                                if (valid)
                                                {
                                                    if (action == "Employee")
                                                    {
                                                        emp.DOB = dob;
                                                    }
                                                    else if (action == "STT")
                                                    {
                                                        stt.DOB = dob;
                                                    }
                                                    else if (action == "Candidate")
                                                    {
                                                        can.DOB = dob;
                                                    }
                                                }
                                                else
                                                {
                                                    msg = new Message(MessageConstants.E0033, MessageType.Error, "Field " + (col + index) + " is invalid date.");
                                                    isValid = false;
                                                }
                                            }
                                            else
                                            {
                                                if (action != "Candidate")
                                                {
                                                    msg = new Message(MessageConstants.E0033, MessageType.Error, "Field " + (col + index) + " is a required field.");
                                                    isValid = false;
                                                }
                                            }
                                            break;
                                        case "SearchDate":
                                            if (!string.IsNullOrEmpty(value))
                                            {
                                                DateTime searchDate = new DateTime();
                                                bool valid = DateTime.TryParse(value, out searchDate);
                                                if (valid)
                                                {
                                                    can.SearchDate = searchDate;
                                                }
                                                else
                                                {
                                                    msg = new Message(MessageConstants.E0033, MessageType.Error, "Field " + (col + index) + " is invalid date.");
                                                    isValid = false;
                                                }
                                            }
                                            else
                                            {
                                                msg = new Message(MessageConstants.E0033, MessageType.Error, "Field " + (col + index) + " is a required field.");
                                                isValid = false;
                                            }
                                            break;
                                        case "POB":
                                            if (action == "Employee")
                                            {
                                                emp.POB = string.IsNullOrEmpty(value) ? null : value;
                                            }
                                            else if (action == "STT")
                                            {
                                                stt.POB = string.IsNullOrEmpty(value) ? null : value;
                                            }
                                            break;
                                        case "VnPOB":
                                            if (action == "Employee")
                                            {
                                                emp.VnPOB = string.IsNullOrEmpty(value) ? null : value;
                                            }
                                            else if (action == "STT")
                                            {
                                                stt.VnPOB = string.IsNullOrEmpty(value) ? null : value;
                                            }
                                            break;
                                        case "PlaceOfOrigin":
                                            if (action == "Employee")
                                            {
                                                emp.PlaceOfOrigin = string.IsNullOrEmpty(value) ? null : value;
                                            }
                                            else if (action == "STT")
                                            {
                                                stt.PlaceOfOrigin = string.IsNullOrEmpty(value) ? null : value;
                                            }
                                            break;
                                        case "VnPlaceOfOrigin":
                                            if (action == "Employee")
                                            {
                                                emp.VnPlaceOfOrigin = string.IsNullOrEmpty(value) ? null : value;
                                            }
                                            else if (action == "STT")
                                            {
                                                stt.VnPlaceOfOrigin = string.IsNullOrEmpty(value) ? null : value;
                                            }
                                            break;
                                        case "Nationality":
                                            if (action == "Employee")
                                            {
                                                emp.Nationality = string.IsNullOrEmpty(value) ? null : value;
                                            }
                                            else if (action == "STT")
                                            {
                                                stt.Nationality = string.IsNullOrEmpty(value) ? null : value;
                                            }
                                            break;
                                        case "Gender":
                                            if (!string.IsNullOrEmpty(value))
                                            {
                                                if (value == "Male")
                                                {
                                                    if (action == "Employee")
                                                    {
                                                        emp.Gender = true;
                                                    }
                                                    else if (action == "STT")
                                                    {
                                                        stt.Gender = true;
                                                    }
                                                }
                                                else if (value == "Female")
                                                {
                                                    if (action == "Employee")
                                                    {
                                                        emp.Gender = false;
                                                    }
                                                    else if (action == "STT")
                                                    {
                                                        stt.Gender = false;
                                                    }
                                                }
                                                else
                                                {
                                                    msg = new Message(MessageConstants.E0033, MessageType.Error, "Field " + (col + index) + " is invalid format.");
                                                    isValid = false;
                                                }
                                            }
                                            else
                                            {
                                                msg = new Message(MessageConstants.E0033, MessageType.Error, "Field " + (col + index) + " is a required field.");
                                                isValid = false;
                                            }
                                            break;
                                        case "MarriedStatus":
                                            if (!string.IsNullOrEmpty(value))
                                            {
                                                if (value == "Single")
                                                {
                                                    if (action == "Employee")
                                                    {
                                                        emp.MarriedStatus = false;
                                                    }
                                                    else if (action == "STT")
                                                    {
                                                        stt.MarriedStatus = false;
                                                    }
                                                }
                                                else if (value == "Married")
                                                {
                                                    if (action == "Employee")
                                                    {
                                                        emp.MarriedStatus = true;
                                                    }
                                                    else if (action == "STT")
                                                    {
                                                        stt.MarriedStatus = true;
                                                    }
                                                }
                                                else
                                                {
                                                    msg = new Message(MessageConstants.E0033, MessageType.Error, "Field " + (col + index) + " is invalid format.");
                                                    isValid = false;
                                                }
                                            }
                                            else
                                            {
                                                msg = new Message(MessageConstants.E0033, MessageType.Error, "Field " + (col + index) + " is a required field.");
                                                isValid = false;
                                            }
                                            break;
                                        case "Degree":
                                            if (action == "Employee")
                                            {
                                                emp.Degree = string.IsNullOrEmpty(value) ? null : value;
                                            }
                                            else if (action == "STT")
                                            {
                                                stt.Degree = string.IsNullOrEmpty(value) ? null : value;
                                            }
                                            break;
                                        case "OtherDegree":
                                            if (action == "Employee")
                                            {
                                                emp.OtherDegree = string.IsNullOrEmpty(value) ? null : value;
                                            }
                                            else if (action == "STT")
                                            {
                                                stt.OtherDegree = string.IsNullOrEmpty(value) ? null : value;
                                            }
                                            break;
                                        case "IDNumber":
                                            if (!string.IsNullOrEmpty(value))
                                            {
                                                if (action == "Employee")
                                                {
                                                    emp.IDNumber = value;
                                                }
                                                else if (action == "STT")
                                                {
                                                    stt.IDNumber = value;
                                                }
                                            }
                                            else
                                            {
                                                msg = new Message(MessageConstants.E0033, MessageType.Error, "Field " + (col + index) + " is a required field.");
                                                isValid = false;
                                            }
                                            break;
                                        case "IssueDate":
                                            if (!string.IsNullOrEmpty(value))
                                            {
                                                DateTime issueDate = new DateTime();
                                                bool valid = DateTime.TryParse(value, out issueDate);
                                                if (valid)
                                                {
                                                    if (action == "Employee")
                                                    {
                                                        emp.IssueDate = issueDate;
                                                    }
                                                    else if (action == "STT")
                                                    {
                                                        stt.IssueDate = issueDate;
                                                    }
                                                }
                                                else
                                                {
                                                    msg = new Message(MessageConstants.E0033, MessageType.Error, "Field " + (col + index) + " is invalid date.");
                                                    isValid = false;
                                                }
                                            }
                                            else
                                            {
                                                msg = new Message(MessageConstants.E0033, MessageType.Error, "Field " + (col + index) + " is a required field.");
                                                isValid = false;
                                            }
                                            break;
                                        case "IDIssueLocation":
                                            if (action == "Employee")
                                            {
                                                emp.IDIssueLocation = string.IsNullOrEmpty(value) ? null : value;
                                            }
                                            else if (action == "STT")
                                            {
                                                stt.IDIssueLocation = string.IsNullOrEmpty(value) ? null : value;
                                            }
                                            break;
                                        case "VnIDIssueLocation":
                                            if (action == "Employee")
                                            {
                                                emp.VnIDIssueLocation = string.IsNullOrEmpty(value) ? null : value;
                                            }
                                            else if (action == "STT")
                                            {
                                                stt.VnIDIssueLocation = string.IsNullOrEmpty(value) ? null : value;
                                            }
                                            break;
                                        case "Race":
                                            if (action == "Employee")
                                            {
                                                emp.Race = string.IsNullOrEmpty(value) ? null : value;
                                            }
                                            else if (action == "STT")
                                            {
                                                stt.Race = string.IsNullOrEmpty(value) ? null : value;
                                            }
                                            break;
                                        case "Religion":
                                            if (action == "Employee")
                                            {
                                                emp.Religion = string.IsNullOrEmpty(value) ? null : value;
                                            }
                                            else if (action == "STT")
                                            {
                                                stt.Religion = string.IsNullOrEmpty(value) ? null : value;
                                            }
                                            break;
                                        case "JR":
                                            if (!string.IsNullOrEmpty(value))
                                            {
                                                int jr = 0;
                                                bool isNumber = Int32.TryParse(value, out jr);
                                                if (isNumber)
                                                {
                                                    JobRequest obj = new JobRequestDao().GetByJRComplete(jr);
                                                    if (obj != null)
                                                    {
                                                        if (action == "Employee")
                                                        {
                                                            emp.JR = obj.ID.ToString();
                                                            //emp.JRApproval = obj.Approval;
                                                        }
                                                        else if (action == "STT")
                                                        {
                                                            stt.JR = obj.ID.ToString();
                                                            //stt.JRApproval = obj.Approval;
                                                        }
                                                    }
                                                    else
                                                    {
                                                        msg = new Message(MessageConstants.E0033, MessageType.Error, "Field " + (col + index) + " does not exits in database.");
                                                        isValid = false;
                                                    }
                                                }
                                                else
                                                {
                                                    msg = new Message(MessageConstants.E0033, MessageType.Error, "Field " + (col + index) + " is invalid number.");
                                                    isValid = false;
                                                }
                                            }
                                            break;
                                        //case "JRApproval":
                                        //    if (action == "Employee")
                                        //    {
                                        //        emp.JRApproval = string.IsNullOrEmpty(value) ? null : value;
                                        //    }
                                        //    else if (action == "STT")
                                        //    {
                                        //        stt.JRApproval = string.IsNullOrEmpty(value) ? null : value;
                                        //    }
                                        //    break;
                                        case "StartDate":
                                            if (!string.IsNullOrEmpty(value))
                                            {
                                                DateTime startDate = new DateTime();
                                                bool valid = DateTime.TryParse(value, out startDate);
                                                if (valid)
                                                {
                                                    if (action == "Employee")
                                                    {
                                                        emp.StartDate = startDate;
                                                    }
                                                    else if (action == "STT")
                                                    {
                                                        stt.StartDate = startDate;
                                                    }
                                                }
                                                else
                                                {
                                                    msg = new Message(MessageConstants.E0033, MessageType.Error, "Field " + (col + index) + " is invalid date.");
                                                    isValid = false;
                                                }
                                            }
                                            else
                                            {
                                                msg = new Message(MessageConstants.E0033, MessageType.Error, "Field " + (col + index) + " is a required field.");
                                                isValid = false;
                                            }
                                            break;
                                        case "ContractedDate":
                                            if (!string.IsNullOrEmpty(value))
                                            {
                                                DateTime contractDate = new DateTime();
                                                bool valid = DateTime.TryParse(value, out contractDate);
                                                if (valid)
                                                {
                                                    if (action == "Employee")
                                                    {
                                                        emp.ContractedDate = contractDate;
                                                    }
                                                    else if (action == "STT")
                                                    {
                                                        stt.ExpectedEndDate = contractDate;
                                                    }
                                                }
                                                else
                                                {
                                                    msg = new Message(MessageConstants.E0033, MessageType.Error, "Field " + (col + index) + " is invalid date.");
                                                    isValid = false;
                                                }
                                            }
                                            else
                                            {
                                                msg = new Message(MessageConstants.E0033, MessageType.Error, "Field " + (col + index) + " is a required field.");
                                                isValid = false;
                                            }
                                            break;
                                        case "DepartmentName":
                                            if (!string.IsNullOrEmpty(value))
                                            {
                                                Department obj = new Department();
                                                obj = new DepartmentDao().GetByNameContain(value);
                                                if (obj != null)
                                                {
                                                    if (action == "Employee")
                                                    {
                                                        emp.DepartmentId = obj.DepartmentId;
                                                    }
                                                    else if (action == "STT")
                                                    {
                                                        stt.DepartmentId = obj.DepartmentId;
                                                    }
                                                }
                                                else
                                                {
                                                    msg = new Message(MessageConstants.E0033, MessageType.Error, "Field " + (col + index) + " is invalid department.");
                                                    isValid = false;
                                                }
                                            }
                                            else
                                            {
                                                msg = new Message(MessageConstants.E0033, MessageType.Error, "Field " + (col + index) + " is a required field.");
                                                isValid = false;
                                            }
                                            break;
                                        case "TitleName":
                                            if (!string.IsNullOrEmpty(value))
                                            {



                                                if (action == "Employee" || action == "Candidate")
                                                {
                                                    JobTitleLevel obj = new JobTitleLevel();
                                                    obj = new JobTitleLevelDao().GetByName(value);
                                                    if (obj != null)
                                                    {
                                                        if (action == "Employee")
                                                        {
                                                            emp.TitleId = obj.ID;
                                                        }
                                                        if (action == "Candidate")
                                                        {
                                                            can.TitleId = obj.ID;
                                                        }
                                                    }
                                                    else
                                                    {
                                                        msg = new Message(MessageConstants.E0033, MessageType.Error, "Field " + (col + index) + " is invalid job title.");
                                                        isValid = false;
                                                    }
                                                }
                                                else if (action == "STT")
                                                {
                                                    if (value == "STT")
                                                    {
                                                        stt.Title = value;
                                                    }
                                                    else
                                                    {
                                                        msg = new Message(MessageConstants.E0033, MessageType.Error, "Field " + (col + index) + " is invalid job title.");
                                                        isValid = false;
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                msg = new Message(MessageConstants.E0033, MessageType.Error, "Field " + (col + index) + " is a required field.");
                                                isValid = false;
                                            }
                                            break;
                                        case "TaxID":
                                            if (action == "Employee")
                                            {
                                                emp.TaxID = string.IsNullOrEmpty(value) ? null : value;
                                            }
                                            break;
                                        case "TaxIssueDate":
                                            if (!string.IsNullOrEmpty(value))
                                            {
                                                DateTime taxDate = new DateTime();
                                                bool valid = DateTime.TryParse(value, out taxDate);
                                                if (valid)
                                                {
                                                    if (action == "Employee")
                                                    {
                                                        emp.TaxIssueDate = taxDate;
                                                    }
                                                }
                                                else
                                                {
                                                    msg = new Message(MessageConstants.E0033, MessageType.Error, "Field " + (col + index) + " is invalid date.");
                                                    isValid = false;
                                                }
                                            }
                                            break;
                                        case "SocialInsuranceNo":
                                            if (action == "Employee")
                                            {
                                                emp.SocialInsuranceNo = value;
                                            }
                                            break;
                                        case "Hospital":
                                            if (!string.IsNullOrEmpty(value))
                                            {
                                                InsuranceHospital obj = new InsuranceHospital();
                                                obj = new InsuranceHospitalDao().GetByName(value);
                                                if (obj != null)
                                                {
                                                    emp.InsuranceHospitalID = obj.ID;
                                                }
                                                else
                                                {
                                                    msg = new Message(MessageConstants.E0033, MessageType.Error, "Field " + (col + index) + " is invalid hospital.");
                                                    isValid = false;
                                                }
                                            }
                                            break;
                                        case "LaborUnion":
                                            if (!string.IsNullOrEmpty(value))
                                            {
                                                if (value == "Yes")
                                                {
                                                    emp.LaborUnion = true;

                                                }
                                                else if (value == "No")
                                                {
                                                    emp.LaborUnion = false;
                                                }
                                                else
                                                {
                                                    msg = new Message(MessageConstants.E0033, MessageType.Error, "Field " + (col + index) + " is invalid format labor union.");
                                                    isValid = false;
                                                }
                                            }
                                            else
                                            {
                                                emp.LaborUnion = false;
                                            }
                                            break;
                                        case "LaborUnionDate":
                                            if (!string.IsNullOrEmpty(value))
                                            {
                                                DateTime laborDate = new DateTime();
                                                bool valid = DateTime.TryParse(value, out laborDate);
                                                if (valid)
                                                {
                                                    if (action == "Employee")
                                                    {
                                                        if (emp.LaborUnion == true)
                                                        {
                                                            emp.LaborUnionDate = laborDate;
                                                        }
                                                        else
                                                        {
                                                            msg = new Message(MessageConstants.E0033, MessageType.Error, "Field " + (col + index) + " has value when field Labor Union equal yes.");
                                                            isValid = false;
                                                        }
                                                    }
                                                    else if (action == "STT")
                                                    {
                                                        if (stt.LaborUnion == true)
                                                        {
                                                            stt.LaborUnionDate = laborDate;
                                                        }
                                                        else
                                                        {
                                                            msg = new Message(MessageConstants.E0033, MessageType.Error, "Field " + (col + index) + " has value when field Labor Union equal yes.");
                                                            isValid = false;
                                                        }
                                                    }
                                                }
                                                else
                                                {
                                                    msg = new Message(MessageConstants.E0033, MessageType.Error, "Field " + (col + index) + " is invalid date.");
                                                    isValid = false;
                                                }
                                            }
                                            break;
                                        case "HomePhone":
                                            if (action == "Employee")
                                            {
                                                emp.HomePhone = string.IsNullOrEmpty(value) ? null : value;
                                            }
                                            else if (action == "STT")
                                            {
                                                stt.HomePhone = string.IsNullOrEmpty(value) ? null : value;
                                            }
                                            break;
                                        case "CellPhone":
                                            if (action == "Employee")
                                            {
                                                emp.CellPhone = string.IsNullOrEmpty(value) ? null : value;
                                            }
                                            else if (action == "STT")
                                            {
                                                stt.CellPhone = string.IsNullOrEmpty(value) ? null : value;
                                            }
                                            else if (action == "Candidate")
                                            {
                                                can.CellPhone = string.IsNullOrEmpty(value) ? null : value;
                                            }
                                            break;
                                        case "Floor":
                                            if (action == "Employee")
                                            {
                                                emp.Floor = string.IsNullOrEmpty(value) ? null : value;
                                            }
                                            else if (action == "STT")
                                            {
                                                stt.Floor = string.IsNullOrEmpty(value) ? null : value;
                                            }
                                            break;
                                        case "ExtensionNumber":
                                            if (action == "Employee")
                                            {
                                                emp.ExtensionNumber = string.IsNullOrEmpty(value) ? null : value;
                                            }
                                            else if (action == "STT")
                                            {
                                                stt.ExtensionNumber = string.IsNullOrEmpty(value) ? null : value;
                                            }
                                            break;
                                        case "SeatCode":
                                            if (action == "Employee")
                                            {
                                                emp.SeatCode = string.IsNullOrEmpty(value) ? null : value;
                                            }
                                            else if (action == "STT")
                                            {
                                                stt.SeatCode = string.IsNullOrEmpty(value) ? null : value;
                                            }
                                            break;
                                        case "SkypeId":
                                            if (action == "Employee")
                                            {
                                                emp.SkypeId = string.IsNullOrEmpty(value) ? null : value;
                                            }
                                            else if (action == "STT")
                                            {
                                                stt.SkypeId = string.IsNullOrEmpty(value) ? null : value;
                                            }
                                            break;
                                        case "YahooId":
                                            if (action == "Employee")
                                            {
                                                emp.YahooId = string.IsNullOrEmpty(value) ? null : value;
                                            }
                                            else if (action == "STT")
                                            {
                                                stt.YahooId = string.IsNullOrEmpty(value) ? null : value;
                                            }
                                            break;
                                        case "OfficeEmail":
                                            if (!string.IsNullOrEmpty(value))
                                            {

                                                if (emailList.Split(',').Contains(value))
                                                {
                                                    msg = new Message(MessageConstants.E0033, MessageType.Error, "Field " + (col + index) + " is duplicated.");
                                                    isValid = false;
                                                }
                                                else
                                                {
                                                    bool valid = Regex.IsMatch(value,
                                                      @"^(?("")("".+?""@)|(([0-9a-zA-Z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-zA-Z])@))" +
                                                      @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-zA-Z][-\w]*[0-9a-zA-Z]\.)+[a-zA-Z]{2,6}))$");
                                                    if (valid)
                                                    {
                                                        if (CheckEmailExits(value) == null)
                                                        {
                                                            if (action == "Employee")
                                                            {
                                                                emp.OfficeEmail = value;
                                                                emailList += value + ",";
                                                            }
                                                            else if (action == "STT")
                                                            {
                                                                stt.OfficeEmail = value;
                                                                emailList += value + ",";
                                                            }
                                                        }
                                                        else
                                                        {
                                                            msg = CheckEmailExits(value);
                                                            isValid = false;
                                                        }
                                                    }
                                                    else
                                                    {
                                                        msg = new Message(MessageConstants.E0033, MessageType.Error, "Field " + (col + index) + " is invalid email.");
                                                        isValid = false;
                                                    }
                                                }
                                            }
                                            break;
                                        case "Email":
                                            if (string.IsNullOrEmpty(value))
                                            {
                                                msg = new Message(MessageConstants.E0033, MessageType.Error, "Field " + (col + index) + " is a required field.");
                                                isValid = false;
                                            }
                                            else
                                            {
                                                bool valid = Regex.IsMatch(value,
                                                  @"^(?("")("".+?""@)|(([0-9a-zA-Z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-zA-Z])@))" +
                                                  @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-zA-Z][-\w]*[0-9a-zA-Z]\.)+[a-zA-Z]{2,6}))$");
                                                if (valid)
                                                {
                                                    can.Email = value;
                                                }
                                                else
                                                {
                                                    msg = new Message(MessageConstants.E0033, MessageType.Error, "Field " + (col + index) + " is invalid email.");
                                                    isValid = false;
                                                }
                                            }

                                            break;
                                        case "PersonalEmail":
                                            if (!string.IsNullOrEmpty(value))
                                            {
                                                value = value.Trim();
                                                bool valid = CheckUtil.IsEmail(value);

                                                //bool valid = Regex.IsMatch(value,
                                                //  @"^(?("")("".+?""@)|(([0-9a-zA-Z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-zA-Z])@))" +
                                                //  @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-zA-Z][-\w]*[0-9a-zA-Z]\.)+[a-zA-Z]{2,6}))$");
                                                if (valid)
                                                {
                                                    if (action == "Employee")
                                                    {
                                                        emp.PersonalEmail = value;
                                                    }
                                                    else if (action == "STT")
                                                    {
                                                        stt.PersonalEmail = value;
                                                    }
                                                }
                                                else
                                                {
                                                    msg = new Message(MessageConstants.E0033, MessageType.Error, "Field " + (col + index) + " is invalid email.");
                                                    isValid = false;
                                                }
                                            }
                                            break;
                                        case "EmergencyContactName":
                                            if (action == "Employee")
                                            {
                                                emp.EmergencyContactName = string.IsNullOrEmpty(value) ? null : value;
                                            }
                                            else if (action == "STT")
                                            {
                                                stt.EmergencyContactName = string.IsNullOrEmpty(value) ? null : value;
                                            }
                                            break;
                                        case "EmergencyContactPhone":
                                            if (action == "Employee")
                                            {
                                                emp.EmergencyContactPhone = string.IsNullOrEmpty(value) ? null : value;
                                            }
                                            else if (action == "STT")
                                            {
                                                stt.EmergencyContactPhone = string.IsNullOrEmpty(value) ? null : value;
                                            }
                                            break;
                                        case "EmergencyContactRelationship":
                                            if (action == "Employee")
                                            {
                                                emp.EmergencyContactRelationship = string.IsNullOrEmpty(value) ? null : value;
                                            }
                                            else if (action == "STT")
                                            {
                                                stt.EmergencyContactRelationship = string.IsNullOrEmpty(value) ? null : value;
                                            }
                                            break;
                                        case "BankAccount":
                                            if (action == "Employee")
                                            {
                                                emp.BankAccount = string.IsNullOrEmpty(value) ? null : value;
                                            }
                                            else if (action == "STT")
                                            {
                                                stt.BankAccount = string.IsNullOrEmpty(value) ? null : value;
                                            }
                                            break;
                                        case "BankName":
                                            if (action == "Employee")
                                            {
                                                emp.BankName = string.IsNullOrEmpty(value) ? null : value;
                                            }
                                            else if (action == "STT")
                                            {
                                                stt.BankName = string.IsNullOrEmpty(value) ? null : value;
                                            }
                                            break;
                                        case "Remarks":
                                            if (action == "Employee")
                                            {
                                                emp.Remarks = string.IsNullOrEmpty(value) ? null : value;
                                            }
                                            else if (action == "STT")
                                            {
                                                stt.Remarks = string.IsNullOrEmpty(value) ? null : value;
                                            }
                                            else if (action == "Candidate")
                                            {
                                                can.Note = string.IsNullOrEmpty(value) ? null : value;
                                            }
                                            break;
                                        case "PermanentAddress":
                                            if (action == "Employee")
                                            {
                                                emp.PermanentAddress = string.IsNullOrEmpty(value) ? null : value;
                                            }
                                            else if (action == "STT")
                                            {
                                                stt.PermanentAddress = string.IsNullOrEmpty(value) ? null : value;
                                            }
                                            break;
                                        //case "PermanentArea":
                                        //    emp.PermanentArea = value;
                                        //    break;
                                        //case "PermanentDistrict":
                                        //    emp.PermanentDistrict = value;
                                        //    break;
                                        //case "PermanentCityProvince":
                                        //    emp.PermanentCityProvince = value;
                                        //    break;
                                        //case "PermanentCountry":
                                        //    emp.PermanentCountry = value;
                                        //    break;
                                        case "VnPermanentAddress":
                                            if (action == "Employee")
                                            {
                                                emp.VnPermanentAddress = string.IsNullOrEmpty(value) ? null : value;
                                            }
                                            else if (action == "STT")
                                            {
                                                stt.VnPermanentAddress = string.IsNullOrEmpty(value) ? null : value;
                                            }
                                            break;
                                        //case "VnPermanentArea":
                                        //    emp.VnPermanentArea = value;
                                        //    break;
                                        //case "VnPermanentDistrict":
                                        //    emp.VnPermanentDistrict = value;
                                        //    break;
                                        //case "VnPermanentCityProvince":
                                        //    emp.VnPermanentCityProvince = value;
                                        //    break;
                                        //case "VnPermanentCountry":
                                        //    emp.VnPermanentCountry = value;
                                        //    break;
                                        case "TempAddress":
                                            if (action == "Employee")
                                            {
                                                emp.TempAddress = string.IsNullOrEmpty(value) ? null : value;
                                            }
                                            else if (action == "STT")
                                            {
                                                stt.TempAddress = string.IsNullOrEmpty(value) ? null : value;
                                            }
                                            break;
                                        //case "TempDistrict":
                                        //    emp.TempDistrict = value;
                                        //    break;
                                        //case "TempArea":
                                        //    emp.TempArea = value;
                                        //    break;
                                        //case "TempCityProvince":
                                        //    emp.TempCityProvince = value;
                                        //    break;
                                        //case "TempCountry":
                                        //    emp.TempCountry = value;
                                        //    break;
                                        case "VnTempAddress":
                                            if (action == "Employee")
                                            {
                                                emp.VnTempAddress = string.IsNullOrEmpty(value) ? null : value;
                                            }
                                            else if (action == "STT")
                                            {
                                                stt.VnTempAddress = string.IsNullOrEmpty(value) ? null : value;
                                            }
                                            break;
                                        //case "VnTempDistrict":
                                        //    emp.VnTempDistrict = value;
                                        //    break;
                                        //case "VnTempArea":
                                        //    emp.VnTempArea = value;
                                        //    break;
                                        //case "VnTempCityProvince":
                                        //    emp.VnTempCityProvince = value;
                                        //    break;
                                        //case "VnTempCountry":
                                        //    emp.VnTempCountry = value;
                                        //    break;
                                        case "Address":
                                            can.Address = string.IsNullOrEmpty(value) ? null : value;
                                            break;
                                    }
                                    #endregion
                                    if (isValid)
                                    {
                                        character++;
                                        firstChar = false;
                                    }
                                    else
                                        break;
                                }

                            }
                            if (isValid && (hasEmpty == false))
                            {
                                if (action == "Employee")
                                {
                                    list.Add(emp);
                                }
                                else if (action == "STT")
                                {
                                    sttList.Add(stt);
                                }
                                else if (action == "Candidate")
                                {
                                    canList.Add(can);
                                }
                            }
                            else
                                break;
                        }
                        xlWorkBook.Close();
                        xlApp.Quit();
                        System.Runtime.InteropServices.Marshal.ReleaseComObject(xlWorkSheet);
                        System.Runtime.InteropServices.Marshal.ReleaseComObject(xlWorkBook);
                        System.Runtime.InteropServices.Marshal.ReleaseComObject(xlApp);
                        xlWorkSheet = null;
                        xlWorkBook = null;
                        xlApp = null;
                    }
                }
                if (isValid)
                {
                    if (action == "Employee")
                    {
                        msg = empDao.InsertMulti(list);

                    }
                    else if (action == "STT")
                    {
                        //msg = sttDao.InsertMulti(sttList, resultDate, principal.UserData.UserName);
                    }
                    else if (action == "Candidate")
                    {
                        //msg = canDao.InsertMulti(canList);
                    }

                }

            }
            catch (Exception ex)
            {
                throw ex;
                //Response.Write(ex.ToString());
            }
            if (msg != null)
            {
                ShowMessage(msg);
            }
            return View("Index");
        }

        private string ExcelColumnIndexToName(int Index)
        {
            string range = "";
            if (Index < 0) return range;
            for (int i = 1; Index + i > 0; i = 0)
            {
                range = ((char)(65 + Index % 26)).ToString() + range;
                Index /= 26;
            }
            if (range.Length > 1) range = ((char)((int)range[0] - 1)).ToString() + range.Substring(1);
            return range;
        }

        private void ImportSTTFromPRM()
        {
            /*
            PRMDAO prmDao = new PRMDAO();
            List<STTResource> listPRM_STT = prmDao.PRM_GetSTTList();

            List<STT> listSTT = new List<STT>();
            for (int i = 0; i < listPRM_STT.Count; i++)
            {
                STTResource prmStt = listPRM_STT[i];
                STT stt = new STT();

                stt.ID = "STT0" + prmStt.STTResourceId.Substring(3, 2) + "-" + prmStt.STTResourceId.Substring(6, 2);
                stt.FirstName = prmStt.FirstName;
                //stt.MiddleName = prmStt.MiddleName;
                stt.LastName = prmStt.LastName;
                stt.Major = string.Empty;
                stt.DOB = prmStt.DOB.Value;
                stt.POB = "-ND-";
                stt.PlaceOfOrigin = "-ND-";
                stt.Nationality = "VietNam";
                stt.IDNumber = "111111111"; // temp
                stt.IssueDate = prmStt.DOB.Value.AddYears(15); // temp
                stt.Gender = false;
                stt.Religion = string.Empty;
                stt.MarriedStatus = false;
                //stt.STTStatusId = stt.STTStatusId;
                stt.JR = string.Empty;
                stt.JRApproval = string.Empty;
                stt.StartDate = prmStt.StartDate.Value;
                stt.ExpectedEndDate = prmStt.ExpectedEndDate != null ? prmStt.ExpectedEndDate.Value : prmStt.StartDate.Value;
                stt.DepartmentId = 501; // ES - Service-501
                stt.Title = "STT";
                stt.LaborUnion = false;
                stt.LaborUnionDate = null;
                stt.HomePhone = string.Empty;
                stt.CellPhone = string.Empty;
                stt.Floor = string.Empty;
                stt.ExtensionNumber = string.Empty;
                stt.SeatCode = prmStt.SeatCode;
                stt.SkypeId = prmStt.SkypeId;
                stt.YahooId = prmStt.YahooId;
                stt.PersonalEmail = string.Empty;
                stt.OfficeEmail = string.Empty;
                stt.EmergencyContactName = string.Empty;
                stt.EmergencyContactPhone = string.Empty;
                stt.EmergencyContactRelationship = string.Empty;
                stt.PermanentAddress = string.Empty;
                stt.PermanentArea = string.Empty;
                stt.PermanentDistrict = string.Empty;
                stt.PermanentCityProvince = string.Empty;
                stt.PermanentCountry = string.Empty;
                stt.TempAddress = string.Empty;
                stt.TempArea = string.Empty;
                stt.TempDistrict = string.Empty;
                stt.TempCityProvince = string.Empty;
                stt.TempCountry = string.Empty;
                stt.BankName = string.Empty;
                stt.BankAccount = string.Empty;
                stt.Remarks = prmStt.Comment;

                stt.VnFirstName = prmStt.FirstName;
                stt.VnMiddleName = string.Empty;
                stt.VnLastName = prmStt.LastName;
                stt.VnPOB = "-ND-";
                stt.VnPlaceOfOrigin = "-ND-";
                stt.Degree = string.Empty;
                stt.OtherDegree = string.Empty;
                stt.Race = string.Empty;
                stt.IDIssueLocation = "-ND-";
                stt.VnIDIssueLocation = "-ND-";
                stt.VnPermanentAddress = string.Empty;
                stt.VnPermanentArea = string.Empty;
                stt.VnPermanentDistrict = string.Empty;
                stt.VnPermanentCityProvince = string.Empty;
                stt.VnPermanentCountry = string.Empty;
                stt.VnTempAddress = string.Empty;
                stt.VnTempArea = string.Empty;
                stt.VnTempDistrict = string.Empty;
                stt.VnTempCityProvince = string.Empty;
                stt.VnTempCountry = string.Empty;

                stt.CreateDate = DateTime.Now;
                stt.CreatedBy = "tan.tran";
                stt.UpdateDate = DateTime.Now;
                stt.UpdatedBy = "tan.tran";

                if (prmStt.ResultId != null)
                {
                    // Set Result
                    stt.ResultId = prmStt.ResultId;

                    if (prmStt.ResultId.Value == 1) // Passed
                    {
                        stt.STTStatusId = 5; // Promoted
                    }
                    else if (prmStt.ResultId.Value == 2) // Failed
                    {
                        stt.STTStatusId = 6; // Rejected
                    }
                }
                else
                {
                    stt.STTStatusId = 2; // In Class
                }


                // Insert into STT
                STTDao sttDao = new STTDao();
                sttDao.Insert(stt);

                // Insert into STT Result if this STT had result               
                STTRefResultDao resultDao = new STTRefResultDao();
                if (prmStt.ResultId != null)
                {
                    STT_RefResult result = new STT_RefResult();
                    result.SttID = stt.ID;
                    result.EndDate = prmStt.EndDate.Value;
                    result.ResultId = prmStt.ResultId.Value;
                    result.DeleteFlag = false;

                    result.CreatedDate = DateTime.Now;
                    result.UpdatedDate = DateTime.Now;
                    result.CreatedBy = "tan.tran";
                    result.UpdatedBy = "tan.tran";

                    resultDao.Insert(result);

                    // get newsest ID from DB
                    string newestId = resultDao.GetList().Max(p => p.Id).ToString();

                    //stt.ResultId = int.Parse(newestId);
                }


            }
            */
        }

        [HttpPost]
        public ActionResult ImportContractIntoCRM(FormCollection form)
        {
            Message msg = null;
            bool hasValue = false;
            bool isValid = false;
            try
            {
                if (!string.IsNullOrEmpty(form["inp_Contract"]))
                {
                    HttpPostedFileBase hpf = Request.Files["file"] as HttpPostedFileBase;
                    if (hpf.ContentLength != 0)
                    {
                        string extension = Path.GetExtension(hpf.FileName);
                        if (extension.ToLower() != ".xls") //check extension file is valid
                        {
                            msg = new Message(MessageConstants.E0013, MessageType.Error, extension, ".xls", Constants.CV_MAX_SIZE);
                        }
                        else
                        {
                            string strPath = Server.MapPath(Constants.IMPORT_EMS);
                            string fileName = Path.GetFileName(hpf.FileName);
                            fileName = ConvertUtil.FormatFileName(fileName);
                            if (System.IO.File.Exists(strPath + fileName))
                            {
                                System.IO.File.Delete(strPath + fileName);
                            }
                            hpf.SaveAs(strPath + fileName);
                            Excel.Application xlApp = new Excel.Application();
                            Excel.Workbook xlWorkBook = xlApp.Workbooks.Open(Server.MapPath(Constants.IMPORT_EMS + fileName));
                            Excel.Worksheet xlWorkSheet = xlWorkBook.Worksheets.get_Item(1);
                            string[] column = new string[] {  
                            "No",
                            "ID", 
                            "FullName",
                            "VnFullName", 
                            "StatusName", 
                            "DOB", 
                            "POB", 
                            "VnPOB",
                            "PlaceOfOrigin",
                            "VnPlaceOfOrigin",
                            "Nationality",
                            "Gender",
                            "MarriedStatus",
                            "Degree"                            
                            };

                            string[] _colum_letters = new string[column.Count()];
                            Excel.Range range = null;

                            for (int i = 0; i < column.Count(); i++)
                            {
                                _colum_letters[i] = ExcelColumnIndexToName(i);
                            }
                            for (int index = 3; index < 500; index++)
                            {
                                Employee emp = new Employee();
                                int character = 0;
                                bool firstChar = true;
                                foreach (string col in _colum_letters)
                                {
                                    range = xlWorkSheet.get_Range(col + index);

                                    string value = (string)range.Text;
                                    if (value == "" && firstChar)
                                    {
                                        hasValue = true;
                                        break;
                                    }
                                    else
                                    {
                                        switch (column[character])
                                        {
                                            case "ID":
                                                if (string.IsNullOrEmpty(value))
                                                {
                                                    msg = new Message(MessageConstants.E0033, MessageType.Error, "Field " + (col + index) + " is a required field.");
                                                    isValid = false;
                                                }
                                                else
                                                {
                                                    emp.ID = value;
                                                }
                                                break;
                                        }
                                        if (isValid)
                                        {
                                            character++;
                                            firstChar = false;
                                        }
                                        else
                                        {
                                            break;
                                        }
                                    }
                                }
                            }
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            if (msg != null)
            {
                ShowMessage(msg);
            }
            return View("Index");
        }

        public ActionResult ImportLaborUnion()
        {
            TempData[CommonDataKey.TEMP_SYSTEM_MESSAGE] = 
                string.Format(Constants.DIV_MESSAGE_FORMAT, "msgSuccess", "none", "");
            return View();
        }
        [HttpPost]
        public ActionResult ImportLaborUnion(FormCollection collection)
        {
            AuthenticationProjectPrincipal principal = HttpContext.User as AuthenticationProjectPrincipal;
            List<string> success = new List<string>();
            List<string> notSuccess = new List<string>();
            Message msg = null;
            HttpPostedFileBase hpf = Request.Files["file"] as HttpPostedFileBase;
            bool importLaborUnion = string.IsNullOrEmpty(Request["ckbLaborUnion"]) ? false : true;
            bool importContract = string.IsNullOrEmpty(Request["ckbContract"]) ? false : true;
            if (!importContract && !importLaborUnion)
            {
                msg = new Message(MessageConstants.E0033, MessageType.Error,
                   "Please select items to import !");
                ShowMessage(msg);
                return View();
            }
            int startRow = 0;
            int endRow = 0;
            int sheetIndex = 0;
            if (string.IsNullOrEmpty(Request["startRow"]))
            {
                startRow = 4;
            }
            else if (!int.TryParse(Request["startRow"], out startRow))
            {
                msg = new Message(MessageConstants.E0033, MessageType.Error,
                    "Start Row must be integer or empty !" +
                    " (default value: 4)");
                ShowMessage(msg);
                return View();
            }
            if (string.IsNullOrEmpty(Request["endRow"]))
            {
                endRow = 500;
            }
            else if (!int.TryParse(Request["endRow"], out endRow))
            {
                msg = new Message(MessageConstants.E0033, MessageType.Error,
                    "End Row must be integer or empty !" +
                    " (default value: 500)");
                ShowMessage(msg);
                return View();
            }
            if (string.IsNullOrEmpty(Request["sheetIndex"]))
            {
                sheetIndex = 1;
            }
            else if (!int.TryParse(Request["sheetIndex"], out sheetIndex))
            {
                msg = new Message(MessageConstants.E0033, MessageType.Error,
                    "Sheet Index must be integer or empty !" +
                    " (default value: 1)");
                ShowMessage(msg);
                return View();
            }
            if (hpf.ContentLength != 0)
            {
                int maxSizeinMegabytes = 5;
                int maxSize = 1024 * 1024 * maxSizeinMegabytes;//Max size is 5M
                float sizeFile = float.Parse(hpf.ContentLength.ToString("N"));
                string fileName = Path.GetFileName(hpf.FileName);
                string extension = Path.GetExtension(hpf.FileName);
                if (extension.ToLower() != ".xls" && extension.ToLower() != ".xlsx") //check extension file is valid
                {
                    msg = new Message(MessageConstants.E0013, MessageType.Error, extension, ".xls, .xlsx", maxSizeinMegabytes);
                }
                else if (sizeFile > maxSize) //check maxlength of uploaded file
                {
                    msg = new Message(MessageConstants.E0012, MessageType.Error, maxSizeinMegabytes.ToString());
                }
                else
                {
                    string strPath = Server.MapPath(Constants.IMPORT_EMS);
                    if (System.IO.File.Exists(strPath + fileName))
                    {
                        System.IO.File.Delete(strPath + fileName);
                    }
                    hpf.SaveAs(strPath + fileName);
                    List<Hashtable> items = GetLaborUnionAndContractDataFromExcel(fileName, importLaborUnion, 
                        importContract, startRow, endRow, sheetIndex);
                    for (int i = 0; i < items.Count; i++)
                    {
                        msg = UpdateLaborUnionAndContract(items[i], principal, importLaborUnion, importContract);
                        if (msg!=null && msg.MsgType == MessageType.Error)
                        {
                            notSuccess.Add(items[i]["A"].ToString() + 
                                " Exception: <i style='color:red'>" + msg.MsgText + "</i>");
                        }
                        else
                        {
                            success.Add(items[i]["A"].ToString());
                        }
                    }
                    ViewData["SUCCESS"] = success;
                    ViewData["NOTSUCCESS"] = notSuccess;
                }
                return View();
            }
            else
            {
                msg = new Message(MessageConstants.E0033, MessageType.Error, "Please select file to upload!");
                ShowMessage(msg);
                return View(); 
            }
            
        }

        public Message UpdateLaborUnionAndContract(Hashtable item, AuthenticationProjectPrincipal principal,
            bool laborUnion, bool contract)
        {
            Message msg = null;
            try
            {
                if (string.IsNullOrEmpty(item["A"].ToString()))
                {
                    msg = new Message(MessageConstants.E0033, MessageType.Error, "Employee ID is null");
                    return msg;
                }
                Employee emp = empDao.GetById(item["A"].ToString());
                if (emp == null)
                {
                    msg = new Message(MessageConstants.E0033, MessageType.Error, "Employee does not exist!");
                }
                else
                {
                    if (laborUnion)
                    {
                        bool isInLaborUnion = !string.IsNullOrEmpty(item["G"].ToString());
                        emp.LaborUnion = isInLaborUnion;
                        if (!isInLaborUnion)
                        {
                            emp.LaborUnionDate = null;
                        }
                        if (!string.IsNullOrEmpty(item["H"].ToString()))
                        {
                            DateTime startDate = DateTime.Parse("01-" + item["H"].ToString());
                            emp.LaborUnionDate = startDate;
                        }
                        emp.UpdatedBy = principal.UserData.UserName;
                        empDao.UpdateLaborUnion(emp);
                    }
                    if (contract)
                    {
                        string _1st_Number = GetHashtableValue(item, "I");
                        string _1st_From = GetHashtableValue(item, "J");
                        string _1st_To = GetHashtableValue(item, "K");
                        string _2nd_Number = GetHashtableValue(item, "L");
                        string _2nd_From = GetHashtableValue(item, "M");
                        string _2nd_To = GetHashtableValue(item, "N");
                        string _Permanent_Number = GetHashtableValue(item, "O");
                        string _Permanent_From = GetHashtableValue(item, "P");
                        contractDAO.ImportContract(emp, _1st_Number, _1st_From, _1st_To,
                            _2nd_Number, _2nd_From, _2nd_To, _Permanent_Number, _Permanent_From);
                    }
                }
            }
            catch (Exception ex)
            {
                msg = new Message(MessageConstants.E0033, MessageType.Error, ex.Message);
            }

            return msg;
        }
        public string GetHashtableValue(Hashtable item, string key)
        {
            string result = item[key].ToString().Trim();
            return string.IsNullOrEmpty(result) ? null : result.Replace(". ", " ");
        }
        public List<Hashtable> GetLaborUnionAndContractDataFromExcel(string fileName, bool laborUnion, bool contract,
            int startRow, int endRow, int sheetIndex)
        {
            List<Hashtable> list = new List<Hashtable>();
            if (!laborUnion && !contract)
                return list;
            string[] colNames_Labor = new string[] { "G", "H" };
            string[] colNames_Contract = new string[] { "I", "J", "K", "L", "M", "N", "O", "P" };

            Excel.Application xlApp = new Excel.Application();
            Excel.Workbook xlWorkBook = xlApp.Workbooks.Open(Server.MapPath(Constants.IMPORT_EMS + fileName));
            Excel.Worksheet xlWorkSheet = xlWorkBook.Worksheets.get_Item(sheetIndex);
            try
            {
                for (int i = startRow; i <= endRow; i++)
                {
                    Hashtable item = new Hashtable();
                    Excel.Range range = xlWorkSheet.get_Range("A" + i);
                    string text = range.Text;
                    item.Add("A", text.Trim());
                    if (laborUnion)
                    {
                        for (int j = 0; j < colNames_Labor.Length; j++)
                        {
                            range = xlWorkSheet.get_Range(colNames_Labor[j] + i);
                            text = range.Text;
                            item.Add(colNames_Labor[j], text.Trim());
                        }
                    }
                    if (contract)
                    {
                        for (int j = 0; j < colNames_Contract.Length; j++)
                        {
                            range = xlWorkSheet.get_Range(colNames_Contract[j] + i);
                            text = range.Text;
                            item.Add(colNames_Contract[j], text.Trim());
                        }
                    }
                    list.Add(item);
                }
            }
            finally
            {
                xlWorkBook.Close();
                xlApp.Quit();
                System.Runtime.InteropServices.Marshal.ReleaseComObject(xlWorkSheet);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(xlWorkBook);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(xlApp);
                xlWorkSheet = null;
                xlWorkBook = null;
                xlApp = null;
            }
            return list;   
        }

        public enum ResignedColsNum: int
        {
            EmpId,
            FullName,
            VnFullName,
            EmpStatus,
            DateOfBirth,
            PlaceOfBirth,
            VNPlaceOfBirth,
            PlaceOfOrigin,
            VNPlaceOfOrigin,
            Nationality,
            Gender,
            MarriedStatus,
            Degree,
            Major,
            OtherDegree,
            IDNumber,
            IssueDate,
            IssueLocation,
            VNIssueLocation,
            Race,
            Religion,
            JR,
            JRApproval,
            StartDate,
            ContractedDate,
            Department,
            SubDepartment,
            JobTitle,
            Project,
            DirectManager,
            TaxID,
            TaxIssueDate,
            SocialInsuranceNo,
            Hospital,
            LaborUnion,
            LaborUnionDate,
            HomePhone,
            CellPhone,
            Floor,
            ExtensionNumber,
            SeatCode,
            SkypeId,
            YahooId,
            PersonalEmail,
            OfficeEmail,
            EmergencyContactName,
            EmergencyContactPhone,
            EmergencyContactRelationship,
            PermanentAddress,
            VNPermanentAddress,
            TempAddress,
            VNTempAddress,
            BankAccount,
            BankName,
            ResignedDate,
            ResignedReason,
            Remark    
        }

        public Dictionary<ResignedColsNum, string> ResignedCols = new Dictionary<ResignedColsNum, string>()
        { 
            {ResignedColsNum.EmpId,"B"},
            {ResignedColsNum.FullName,"C"},
            {ResignedColsNum.VnFullName,"D"},
            {ResignedColsNum.EmpStatus,"E"},
            {ResignedColsNum.DateOfBirth,"F"},
            {ResignedColsNum.PlaceOfBirth,"G"},
            {ResignedColsNum.VNPlaceOfBirth,"H"},
            {ResignedColsNum.PlaceOfOrigin,"I"},
            {ResignedColsNum.VNPlaceOfOrigin,"J"},
            {ResignedColsNum.Nationality,"K"},
            {ResignedColsNum.Gender,"L"},
            {ResignedColsNum.MarriedStatus,"M"},
            {ResignedColsNum.Degree,"N"},
            {ResignedColsNum.Major,"O"},
            {ResignedColsNum.OtherDegree,"P"},
            {ResignedColsNum.IDNumber,"Q"},
            {ResignedColsNum.IssueDate,"R"},
            {ResignedColsNum.IssueLocation,"S"},
            {ResignedColsNum.VNIssueLocation,"T"},
            {ResignedColsNum.Race,"U"},
            {ResignedColsNum.Religion,"V"},
            {ResignedColsNum.JR,"W"},
            {ResignedColsNum.JRApproval,"X"},
            {ResignedColsNum.StartDate,"Y"},
            {ResignedColsNum.ContractedDate,"Z"},
            {ResignedColsNum.Department,"AA"},
            {ResignedColsNum.SubDepartment,"AB"},
            {ResignedColsNum.JobTitle,"AC"},
            {ResignedColsNum.Project,"AD"},
            {ResignedColsNum.DirectManager,"AE"},
            {ResignedColsNum.TaxID,"AF"},
            {ResignedColsNum.TaxIssueDate,"AG"},
            {ResignedColsNum.SocialInsuranceNo,"AH"},
            {ResignedColsNum.Hospital,"AI"},
            {ResignedColsNum.LaborUnion,"AJ"},
            {ResignedColsNum.LaborUnionDate,"AK"},
            {ResignedColsNum.HomePhone,"AL"},
            {ResignedColsNum.CellPhone,"AM"},
            {ResignedColsNum.Floor,"AN"},
            {ResignedColsNum.ExtensionNumber,"AO"},
            {ResignedColsNum.SeatCode,"AP"},
            {ResignedColsNum.SkypeId,"AQ"},
            {ResignedColsNum.YahooId,"AR"},
            {ResignedColsNum.PersonalEmail,"AS"},
            {ResignedColsNum.OfficeEmail,"AT"},
            {ResignedColsNum.EmergencyContactName,"AU"},
            {ResignedColsNum.EmergencyContactPhone,"AV"},
            {ResignedColsNum.EmergencyContactRelationship,"AW"},
            {ResignedColsNum.PermanentAddress,"AX"},
            {ResignedColsNum.VNPermanentAddress,"AY"},
            {ResignedColsNum.TempAddress,"AZ"},
            {ResignedColsNum.VNTempAddress,"BA"},
            {ResignedColsNum.BankAccount,"BB"},
            {ResignedColsNum.BankName,"BC"},
            {ResignedColsNum.ResignedDate,"BD"},
            {ResignedColsNum.ResignedReason,"BE"},
            {ResignedColsNum.Remark,"BF"}
        };
        public ActionResult ResignedList()
        {
            TempData[CommonDataKey.TEMP_SYSTEM_MESSAGE] =
                string.Format(Constants.DIV_MESSAGE_FORMAT, "msgSuccess", "none", "");
            return View();
        }
        [HttpPost]
        public ActionResult ResignedList(FormCollection collection)
        {
            
            AuthenticationProjectPrincipal principal = HttpContext.User as AuthenticationProjectPrincipal;
            List<string> success = new List<string>();
            List<string> notSuccess = new List<string>();
            Message msg = null;
            HttpPostedFileBase hpf = Request.Files["file"] as HttpPostedFileBase;
            bool importLaborUnion = string.IsNullOrEmpty(Request["ckbLaborUnion"]) ? false : true;
            bool importContract = string.IsNullOrEmpty(Request["ckbContract"]) ? false : true;
            int startRow = 0;
            int endRow = 0;
            int sheetIndex = 0;
            if (string.IsNullOrEmpty(Request["startRow"]))
            {
                startRow = 4;
            }
            else if (!int.TryParse(Request["startRow"], out startRow))
            {
                msg = new Message(MessageConstants.E0033, MessageType.Error,
                    "Start Row must be integer or empty !" +
                    " (default value: 4)");
                ShowMessage(msg);
                return View();
            }
            if (string.IsNullOrEmpty(Request["endRow"]))
            {
                endRow = 500;
            }
            else if (!int.TryParse(Request["endRow"], out endRow))
            {
                msg = new Message(MessageConstants.E0033, MessageType.Error,
                    "End Row must be integer or empty !" +
                    " (default value: 500)");
                ShowMessage(msg);
                return View();
            }
            if (string.IsNullOrEmpty(Request["sheetIndex"]))
            {
                sheetIndex = 2;
            }
            else if (!int.TryParse(Request["sheetIndex"], out sheetIndex))
            {
                msg = new Message(MessageConstants.E0033, MessageType.Error,
                    "Sheet Index must be integer or empty !" +
                    " (default value: 1)");
                ShowMessage(msg);
                return View();
            }
            if (hpf.ContentLength != 0)
            {
                int maxSizeinMegabytes = 5;
                int maxSize = 1024 * 1024 * maxSizeinMegabytes;//Max size is 5M
                float sizeFile = float.Parse(hpf.ContentLength.ToString("N"));
                string fileName = Path.GetFileName(hpf.FileName);
                string extension = Path.GetExtension(hpf.FileName);
                if (extension.ToLower() != ".xls" && extension.ToLower() != ".xlsx") //check extension file is valid
                {
                    msg = new Message(MessageConstants.E0013, MessageType.Error, extension, ".xls, .xlsx", maxSizeinMegabytes);
                }
                else if (sizeFile > maxSize) //check maxlength of uploaded file
                {
                    msg = new Message(MessageConstants.E0012, MessageType.Error, maxSizeinMegabytes.ToString());
                }
                else
                {
                    string strPath = Server.MapPath(Constants.IMPORT_EMS);
                    if (System.IO.File.Exists(strPath + fileName))
                    {
                        System.IO.File.Delete(strPath + fileName);
                    }
                    hpf.SaveAs(strPath + fileName);
                    List<Hashtable> items = GetResignedDataFromExcel(fileName, startRow, endRow, sheetIndex);
                    /*---*/
                    msg = ImportResignedList(items, ref success, ref notSuccess);
                    
                    ViewData["SUCCESS"] = success;
                    ViewData["NOTSUCCESS"] = notSuccess;
                }
                ShowMessage(msg);
                return View();
            }
            else
            {
                msg = new Message(MessageConstants.E0033, MessageType.Error, "Please select file to upload!");
                ShowMessage(msg);
                return View();
            }
        }

        public string GetName(string input, WhichName whichName)
        {
            input = input.Trim();
            string[] names = input.Split(' ');
            switch (whichName)
            {
                case WhichName.Last:
                    return names[0];
                case WhichName.First:
                    return names[names.Length-1];
                case WhichName.Middle:
                    input = input.Remove(0, input.IndexOf(' '));
                    input = input.Remove(input.LastIndexOf(' '));
                    return string.IsNullOrEmpty(input.Trim()) ? null : input.Trim();
                default:
                    return string.Empty;
            }
        }
        public enum WhichName : int
        { 
            Last = 3,
            Middle = 2,
            First = 1
        }
        public enum DefaultValue : int
        {
            DepartmentId = 900,//Unknown
            TitleId = 74 //Unknown
        }
        public DateTime? GetDateValueFromDateString(string input)
        {
            if (string.IsNullOrEmpty(input))
                return null;
            input = input.Trim().Replace(" ", string.Empty);
            MatchCollection mcDate = Regex.Matches(input, @"\b\d+\b");
            string sMonth = Regex.Match(input, @"\b\D+\b").Value.Trim('-');
            string sYear = mcDate[1].Value.Trim();
            string sDay = mcDate[0].Value.Trim();
            return DateTime.Parse(sDay + "-" + sMonth + "-" + sYear);
        }
        public void SetErrorMessage(ref string msgOut, ref string msgOutNotHtml, string addedText)
        {
            string stylePrefix = "<br/><i style='color:blue'>";
            string styleSuffix = "</i>";
            msgOut += stylePrefix + addedText +styleSuffix;
            msgOutNotHtml = addedText + ", " + msgOutNotHtml;
        }
        public Message ImportResignedList(List<Hashtable> items, ref List<string> success, ref List<string> notsuccess)
        {
            success = new List<string>();
            notsuccess = new List<string>();
            string msgNotExistFormat = "{0} \"{1}\" does not exist > Temp value is {2}";
            string msgManyExistFormat = "There're {0} {1} with name \"{2}\" > Temp value is {3}";
            string msgNullFormat = "{0} is Null > Temp value is {1}";
            
            foreach (Hashtable item in items)
            {
                string empId = GetHashtableValue(item, ResignedCols[ResignedColsNum.EmpId]);
                try
                {
                    if (string.IsNullOrEmpty(empId))
                    {
                        notsuccess.Add("Empty row !");   
                        continue;
                    }
                    Employee emp = empDao.GetById(empId);
                    if(emp!=null)
                    {
                        notsuccess.Add(empId + ": " + "Employee exists !");   
                    }
                    else
                    {
                        string msgError = "";
                        string remarks = "";
                        emp = new Employee();
                        emp.Remarks = GetHashtableValue(item, ResignedCols[ResignedColsNum.Remark]);
                        emp.ID = GetHashtableValue(item, ResignedCols[ResignedColsNum.EmpId]).Replace(" ","");
                        emp.LastName = GetName(GetHashtableValue(item, ResignedCols[ResignedColsNum.FullName]), WhichName.Last);
                        string midName = GetName(GetHashtableValue(item, ResignedCols[ResignedColsNum.FullName]), WhichName.Middle);
                        emp.MiddleName = string.IsNullOrEmpty(midName) ? null : midName;
                        emp.FirstName = GetName(GetHashtableValue(item, ResignedCols[ResignedColsNum.FullName]), WhichName.First);
                        emp.VnLastName = emp.LastName;
                        emp.VnMiddleName = emp.MiddleName;
                        emp.VnFirstName = emp.FirstName;
                        string vnFullName = GetHashtableValue(item, ResignedCols[ResignedColsNum.VnFullName]);
                        if (!string.IsNullOrEmpty(vnFullName))
                        {
                            emp.VnLastName = GetName(vnFullName, WhichName.Last);
                            emp.VnMiddleName = GetName(vnFullName, WhichName.Middle);
                            emp.VnFirstName = GetName(vnFullName, WhichName.First);
                        }
                        emp.EmpStatusId = Constants.RESIGNED;
                        emp.DOB = GetDateValueFromDateString(GetHashtableValue(item, ResignedCols[ResignedColsNum.DateOfBirth]));
                        emp.POB = GetHashtableValue(item, ResignedCols[ResignedColsNum.PlaceOfBirth]);
                        emp.VnPOB = GetHashtableValue(item, ResignedCols[ResignedColsNum.VNPlaceOfBirth]);
                        emp.PlaceOfOrigin = GetHashtableValue(item, ResignedCols[ResignedColsNum.PlaceOfOrigin]);
                        emp.VnPlaceOfOrigin = GetHashtableValue(item, ResignedCols[ResignedColsNum.VNPlaceOfOrigin]);
                        emp.Nationality = GetHashtableValue(item, ResignedCols[ResignedColsNum.Nationality]);
                        string gender = GetHashtableValue(item, ResignedCols[ResignedColsNum.Gender]);
                        if (string.IsNullOrEmpty(gender))
                            emp.Gender = null;
                        else
                            emp.Gender = gender.Equals("Male");
                    
                        emp.Degree = GetHashtableValue(item, ResignedCols[ResignedColsNum.Degree]);
                        emp.Major = GetHashtableValue(item, ResignedCols[ResignedColsNum.Major]);
                        emp.OtherDegree = GetHashtableValue(item, ResignedCols[ResignedColsNum.OtherDegree]);
                        string idNumber = GetHashtableValue(item, ResignedCols[ResignedColsNum.IDNumber]);
                        emp.IDNumber = string.IsNullOrEmpty(idNumber) ? null : idNumber.Trim('\'');
                        emp.IssueDate = GetDateValueFromDateString(GetHashtableValue(item, ResignedCols[ResignedColsNum.IssueDate]));
                        emp.IDIssueLocation = GetHashtableValue(item, ResignedCols[ResignedColsNum.IssueLocation]);
                        string vnIssueLoc = GetHashtableValue(item, ResignedCols[ResignedColsNum.VNIssueLocation]);
                        emp.VnIDIssueLocation = string.IsNullOrEmpty(vnIssueLoc) ? emp.IDIssueLocation : vnIssueLoc;
                        emp.Race = GetHashtableValue(item, ResignedCols[ResignedColsNum.Race]);
                        emp.Religion = GetHashtableValue(item, ResignedCols[ResignedColsNum.Religion]);
                        emp.JR = GetHashtableValue(item, ResignedCols[ResignedColsNum.JR]);
                        emp.JRApproval = GetHashtableValue(item, ResignedCols[ResignedColsNum.JRApproval]);
                        string startDate = GetHashtableValue(item, ResignedCols[ResignedColsNum.StartDate]);
                        if (string.IsNullOrEmpty(startDate))
                        {
                            string defaultStartDate = "01-Jan-2001";
                            emp.StartDate = DateTime.Parse(defaultStartDate);
                            SetErrorMessage(ref msgError, ref remarks, string.Format(msgNullFormat, "Start Date", defaultStartDate));
                        }
                        else
                            emp.StartDate = GetDateValueFromDateString(startDate).Value;
                        
                        string contractedDate = GetHashtableValue(item, ResignedCols[ResignedColsNum.ContractedDate]);
                        emp.ContractedDate = string.IsNullOrEmpty(contractedDate) ? emp.StartDate : 
                            GetDateValueFromDateString(GetHashtableValue(item, ResignedCols[ResignedColsNum.ContractedDate]));
                        string departmentName = GetHashtableValue(item, ResignedCols[ResignedColsNum.SubDepartment]);
                        //string msgDepartment = "";
                        if (string.IsNullOrEmpty(departmentName))
                        {
                            emp.DepartmentId = (int)DefaultValue.DepartmentId;
                            SetErrorMessage(ref msgError, ref remarks, 
                                string.Format(msgNullFormat, "Sub Department", (int)DefaultValue.DepartmentId));
                        }
                        else
                        {
                            List<Department> dep = departmentDao.GetLikeName(departmentName, true);
                            if (dep.Count != 1)
                            {
                                emp.DepartmentId = (int)DefaultValue.DepartmentId;
                                if (dep.Count < 1)
                                    SetErrorMessage(ref msgError, ref remarks, 
                                        string.Format(msgNotExistFormat, "Sub Department", departmentName, emp.DepartmentId));
                                else
                                    SetErrorMessage(ref msgError, ref remarks,
                                        string.Format(msgManyExistFormat, dep.Count, " Sub Departments", departmentName, emp.DepartmentId));
                            }
                            else
                                emp.DepartmentId = dep[0].DepartmentId;
                        }
                        string jobTitleLevel = GetHashtableValue(item, ResignedCols[ResignedColsNum.JobTitle]);
                        //string msgJobTitleLevel = "";
                        if (string.IsNullOrEmpty(jobTitleLevel))
                        {
                            emp.TitleId = (int)DefaultValue.TitleId;
                            SetErrorMessage(ref msgError, ref remarks,
                                string.Format(msgNullFormat, "Job title level", (int)DefaultValue.TitleId));
                        }
                        else
                        {
                            List<JobTitleLevel> titleLevel = jobTitleLevelDao.GetLikeName(jobTitleLevel, true);
                            if (titleLevel.Count != 1)
                            {
                                emp.TitleId = (int)DefaultValue.TitleId;
                                if (titleLevel.Count < 1)
                                    SetErrorMessage(ref msgError, ref remarks,
                                        string.Format(msgNotExistFormat, "Job title level", jobTitleLevel, emp.TitleId));
                                else
                                    SetErrorMessage(ref msgError, ref remarks,
                                        string.Format(msgManyExistFormat, titleLevel.Count, "job titles", jobTitleLevel, emp.TitleId));
                            }
                            else
                                emp.TitleId = titleLevel[0].ID;
                        }
                        emp.Project = GetHashtableValue(item, ResignedCols[ResignedColsNum.Project]);
                        string managerName = GetHashtableValue(item, ResignedCols[ResignedColsNum.DirectManager]);
                        //string msgManager = "";
                        if (string.IsNullOrEmpty(managerName))
                        {
                            SetErrorMessage(ref msgError, ref remarks,
                                string.Format(msgNullFormat, "Manager", "Null"));
                        }
                        else
                        {
                            List<Employee> manager = empDao.GetLikeName(managerName);
                            if (manager.Count == 1)
                                emp.ManagerId = manager[0].ID;
                            else if(manager.Count < 1)
                                SetErrorMessage(ref msgError, ref remarks,
                                    string.Format(msgNotExistFormat, "Manager", managerName, "null"));
                            else
                                SetErrorMessage(ref msgError, ref remarks,
                                    string.Format(msgManyExistFormat, manager.Count, "managers", managerName, "null"));
                        }
                        emp.TaxID = GetHashtableValue(item, ResignedCols[ResignedColsNum.TaxID]);
                        emp.TaxIssueDate = GetDateValueFromDateString(GetHashtableValue(item, ResignedCols[ResignedColsNum.TaxIssueDate]));
                    
                        string homePhone = GetHashtableValue(item, ResignedCols[ResignedColsNum.HomePhone]);
                        emp.HomePhone = string.IsNullOrEmpty(homePhone) ? null : homePhone.Trim('\'');
                        string cellPhone = GetHashtableValue(item, ResignedCols[ResignedColsNum.CellPhone]);
                        emp.CellPhone = string.IsNullOrEmpty(cellPhone) ? null : cellPhone.Trim('\'');
                    
                        emp.PersonalEmail = GetHashtableValue(item, ResignedCols[ResignedColsNum.PersonalEmail]);
                        emp.OfficeEmail = GetHashtableValue(item, ResignedCols[ResignedColsNum.OfficeEmail]);
                    
                        emp.PermanentAddress = GetHashtableValue(item, ResignedCols[ResignedColsNum.PermanentAddress]);
                        emp.VnPermanentAddress = emp.PermanentAddress;
                        emp.TempAddress = GetHashtableValue(item, ResignedCols[ResignedColsNum.TempAddress]);
                        emp.VnTempAddress = emp.TempAddress;
                    
                        emp.ResignedDate = GetDateValueFromDateString(GetHashtableValue(item, ResignedCols[ResignedColsNum.ResignedDate]));
                        emp.ResignedReason = GetHashtableValue(item, ResignedCols[ResignedColsNum.ResignedReason]);
                        

                        #region Empty field
                        //emp.SocialInsuranceNo = GetHashtableValue(item, ResignedCols[ResignedColsNum.SocialInsuranceNo]);
                        //emp.InsuranceHospitalID;
                        //emp.LaborUnion;
                        //emp.LaborUnionDate;
                        //emp.MarriedStatus;
                        //emp.Floor;
                        //emp.ExtensionNumber;
                        //emp.SeatCode;
                        //emp.YahooId;
                        //emp.SkypeId;
                        //emp.EmergencyContactName;
                        //emp.EmergencyContactPhone;
                        //emp.EmergencyContactRelationship;
                        //emp.BankAccount;
                        //emp.BankName;
                        //emp.DOB = DateTime.ParseExact(item[ResignedColsNum.DateOfBirth].ToString().
                        //    Replace(" ", string.Empty),"MMM-dd-yy", null);
                        #endregion

                        var principal = HttpContext.User as AuthenticationProjectPrincipal;
                        emp.CreatedBy = emp.UpdatedBy = principal.UserData.UserName;
                        emp.CreateDate = emp.UpdateDate = DateTime.Now;
                        if(!string.IsNullOrEmpty(remarks))
                            emp.Remarks = "(Importation note: " + remarks + emp.Remarks + ")";
                        Message msg = empDao.ImportResignedEmp(emp);
                        if (msg.MsgType == MessageType.Error)
                            notsuccess.Add(emp.ID + ": " + msg.MsgText);
                        else
                            success.Add(emp.ID + ": " + msg.MsgText + msgError + "<br/>");
                    }
                }
                catch (Exception ex)
                {
                    notsuccess.Add(empId + ": " + "<i style='color:red'>" + ex.Message + "</i>");
                }
            }
            return new Message(MessageConstants.E0033, MessageType.Info, "Imported !");
        }
        public List<Hashtable> GetResignedDataFromExcel(string fileName, int startRow, int endRow, int sheetIndex)
        {
            List<Hashtable> list = new List<Hashtable>();
            Excel.Application xlApp = new Excel.Application();
            Excel.Workbook xlWorkBook = xlApp.Workbooks.Open(Server.MapPath(Constants.IMPORT_EMS + fileName));
            Excel.Worksheet xlWorkSheet = xlWorkBook.Worksheets.get_Item(sheetIndex);
            try
            {
                for (int i = startRow; i <= endRow; i++)
                {
                    Hashtable item = new Hashtable();
                    foreach( var dicItem in ResignedCols )
                    {
                        Excel.Range range = xlWorkSheet.get_Range(dicItem.Value + i);
                        string text = range.Text;
                        item.Add(dicItem.Value, text.Trim());
                    }
                    list.Add(item);
                }
            }
            finally
            {
                xlWorkBook.Close();
                xlApp.Quit();
                System.Runtime.InteropServices.Marshal.ReleaseComObject(xlWorkSheet);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(xlWorkBook);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(xlApp);
                xlWorkSheet = null;
                xlWorkBook = null;
                xlApp = null;
            }
            return list;
        }
        /// <summary>
        /// Import Project, Manager, Seat Code and Floor for employee
        /// </summary>
        /// <returns></returns>
        public ActionResult ImportPMSF()
        {
            TempData[CommonDataKey.TEMP_SYSTEM_MESSAGE] =
                string.Format(Constants.DIV_MESSAGE_FORMAT, "msgSuccess", "none", "");
            return View();
        }
        /// <summary>
        /// POST: Import Project, Manager, Seat Code and Floor for employee
        /// </summary>
        /// <returns></returns>
        /// 
        [HttpPost]
        public ActionResult ImportPMSF(FormCollection collection)
        {
            AuthenticationProjectPrincipal principal = HttpContext.User as AuthenticationProjectPrincipal;
            List<string> success = new List<string>();
            List<string> notSuccess = new List<string>();
            Message msg = null;
            HttpPostedFileBase hpf = Request.Files["file"] as HttpPostedFileBase;
            int startRow = 0;
            int endRow = 0;
            int sheetIndex = 0;
            if (string.IsNullOrEmpty(Request["startRow"]))
            {
                startRow = 3;
            }
            else if (!int.TryParse(Request["startRow"], out startRow))
            {
                msg = new Message(MessageConstants.E0033, MessageType.Error,
                    "Start Row must be integer or empty !" +
                    " (default value: 4)");
                ShowMessage(msg);
                return View();
            }
            if (string.IsNullOrEmpty(Request["endRow"]))
            {
                endRow = 367;
            }
            else if (!int.TryParse(Request["endRow"], out endRow))
            {
                msg = new Message(MessageConstants.E0033, MessageType.Error,
                    "End Row must be integer or empty !" +
                    " (default value: 500)");
                ShowMessage(msg);
                return View();
            }
            if (string.IsNullOrEmpty(Request["sheetIndex"]))
            {
                sheetIndex = 1;
            }
            else if (!int.TryParse(Request["sheetIndex"], out sheetIndex))
            {
                msg = new Message(MessageConstants.E0033, MessageType.Error,
                    "Sheet Index must be integer or empty !" +
                    " (default value: 1)");
                ShowMessage(msg);
                return View();
            }
            if (hpf.ContentLength != 0)
            {
                int maxSizeinMegabytes = 5;
                int maxSize = 1024 * 1024 * maxSizeinMegabytes;//Max size is 5M
                float sizeFile = float.Parse(hpf.ContentLength.ToString("N"));
                string fileName = Path.GetFileName(hpf.FileName);
                string extension = Path.GetExtension(hpf.FileName);
                if (extension.ToLower() != ".xls" && extension.ToLower() != ".xlsx") //check extension file is valid
                {
                    msg = new Message(MessageConstants.E0013, MessageType.Error, extension, ".xls, .xlsx", maxSizeinMegabytes);
                }
                else if (sizeFile > maxSize) //check maxlength of uploaded file
                {
                    msg = new Message(MessageConstants.E0012, MessageType.Error, maxSizeinMegabytes.ToString());
                }
                else
                {
                    string strPath = Server.MapPath(Constants.IMPORT_EMS);
                    if (System.IO.File.Exists(strPath + fileName))
                    {
                        System.IO.File.Delete(strPath + fileName);
                    }
                    hpf.SaveAs(strPath + fileName);
                    List<Hashtable> items = GetPMSF_FromExcel(fileName, startRow, endRow, sheetIndex);
                    /*---*/
                    msg = ImportPMSF_List(items, ref success, ref notSuccess);

                    ViewData["SUCCESS"] = success;
                    ViewData["NOTSUCCESS"] = notSuccess;
                }
                ShowMessage(msg);
                return View();
            }
            else
            {
                msg = new Message(MessageConstants.E0033, MessageType.Error, "Please select file to upload!");
                ShowMessage(msg);
                return View();
            }
        }

        private Message ImportPMSF_List(List<Hashtable> items, ref List<string> success, ref List<string> notsuccess)
        {
            success = new List<string>();
            notsuccess = new List<string>();

            foreach (Hashtable item in items)
            {
                string empId = GetHashtableValue(item, PMSFCols[PMSFColsNum.EmpID]);
                try
                {
                    if (string.IsNullOrEmpty(empId))
                    {
                        notsuccess.Add("<i style='color:red'>Empty row !</i>");
                        continue;
                    }
                    Employee emp = empDao.GetById(empId);
                    if (emp == null)
                    {
                        notsuccess.Add(empId + ": " + "<i style='color:red'>Employee does not exist !</i>");
                    }
                    else
                    {
                        string msgError = "";

                        emp.Project = GetHashtableValue(item, PMSFCols[PMSFColsNum.Project]);
                        emp.SeatCode = GetHashtableValue(item, PMSFCols[PMSFColsNum.SeatCode]);
                        emp.Floor = GetHashtableValue(item, PMSFCols[PMSFColsNum.Floor]);

                        string[] empName = RemoveDuplicatedSpace(
                            GetHashtableValue(item, PMSFCols[PMSFColsNum.DirectManager]).Split('/').First() ).
                            Split(' ');
                        string firstName = empName.Last();
                        string lastName = empName.Length > 2 ? empName[1] : empName.First();

                        List<Employee> mgrList = empDao.GetManagerByFirstAndLastName(firstName, lastName);
                        if (mgrList.Count == 1)
                            emp.ManagerId = mgrList.FirstOrDefault().ID;
                        else if (mgrList.Count == 0)
                            msgError += "<br/><i style='color:blue'>Manager " + string.Join(" ", empName) + " does not exist !</i>";
                        else
                            msgError += "<br/><i style='color:blue'>There're " + mgrList.Count + " Manager with name " + string.Join(" ", empName) + " !</i>";

                        Message msg = empDao.UpdatePMSF(emp);
                        if (msg.MsgType == MessageType.Error)
                            notsuccess.Add(emp.ID + ": " + msg.MsgText);
                        else
                        {
                            if(string.IsNullOrEmpty(msgError))
                                success.Add(emp.ID + ": " + msg.MsgText + msgError + "<br/>");
                            else
                                notsuccess.Add(emp.ID + ": " + msgError + "<br/>");
                        }
                    }
                }
                catch (Exception ex)
                {
                    notsuccess.Add(empId + ": " + "<i style='color:red'>" + ex.Message + "</i>");
                }
            }
            return new Message(MessageConstants.E0033, MessageType.Info, "Imported !");
        }
        public string RemoveDuplicatedSpace(string input)
        {
            return Regex.Replace(input, @"\b\s+\b", " ").Trim();
        }
        public enum PMSFColsNum : int
        {
            EmpID,
            EmpName,
            Project,
            DirectManager,
            SeatCode,
            Floor
        }
        public Dictionary<PMSFColsNum, string> PMSFCols = new Dictionary<PMSFColsNum, string>()
        { 
            {PMSFColsNum.EmpID, "A"},
            {PMSFColsNum.EmpName, "B"},
            {PMSFColsNum.Project, "C"},
            {PMSFColsNum.DirectManager, "D"},
            {PMSFColsNum.SeatCode, "E"},
            {PMSFColsNum.Floor, "F"}
        };
        private List<Hashtable> GetPMSF_FromExcel(string fileName, int startRow, int endRow, int sheetIndex)
        {
            List<Hashtable> list = new List<Hashtable>();
            Excel.Application xlApp = new Excel.Application();
            Excel.Workbook xlWorkBook = xlApp.Workbooks.Open(Server.MapPath(Constants.IMPORT_EMS + fileName));
            Excel.Worksheet xlWorkSheet = xlWorkBook.Worksheets.get_Item(sheetIndex);
            try
            {
                for (int i = startRow; i <= endRow; i++)
                {
                    Hashtable item = new Hashtable();
                    foreach (var dicItem in PMSFCols)
                    {
                        Excel.Range range = xlWorkSheet.get_Range(dicItem.Value + i);
                        string text = range.Text;
                        item.Add(dicItem.Value, text.Trim());
                    }
                    list.Add(item);
                }
            }
            finally
            {
                xlWorkBook.Close();
                xlApp.Quit();
                System.Runtime.InteropServices.Marshal.ReleaseComObject(xlWorkSheet);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(xlWorkBook);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(xlApp);
                xlWorkSheet = null;
                xlWorkBook = null;
                xlApp = null;
            }
            return list;
        }

        [HttpPost]
        public ActionResult EnglishResult(FormCollection form)
        {
            AuthenticationProjectPrincipal principal = HttpContext.User as AuthenticationProjectPrincipal;
            LocationDao locDao = new LocationDao();
            List<string> success = new List<string>();
            List<string> notSuccess = new List<string>();

            HttpPostedFileBase hpf = Request.Files["file"] as HttpPostedFileBase;
            string strPath = Server.MapPath(Constants.IMPORT_EMS);
            string fileName = Path.GetFileName(hpf.FileName);
            fileName = ConvertUtil.FormatFileName(fileName);            
            if (System.IO.File.Exists(strPath + fileName))
            {
                System.IO.File.Delete(strPath + fileName);
            }
            hpf.SaveAs(strPath + fileName);
            int examID = ConvertUtil.ConvertToInt(form["examID"]);
            
            //Colnames: Emp.Id   | Exam date writing | Score | Exam date verbal | Score
            List<Hashtable> items1 = ReadCandidateDataFromExcel(fileName, 4, 40, 1);
            ExamDao examDao = new ExamDao();
            CandidateExamDao canExamDao = new CandidateExamDao();
            for (int i = 0; i < items1.Count; i++)
            {
                string empId = items1[i]["B"].ToString();
                if (string.IsNullOrEmpty(empId))
                    continue;
                DateTime examWriDate = DateTime.Now;
                if (!string.IsNullOrEmpty(items1[i]["D"].ToString()))
                    examWriDate = DateTime.Parse(items1[i]["D"].ToString());
                int writingScore = ConvertUtil.ConvertToInt(items1[i]["E"].ToString());
                DateTime examVerbalDate = DateTime.Now;
                if (!string.IsNullOrEmpty(items1[i]["H"].ToString()))
                    DateTime.Parse(items1[i]["H"].ToString());
                string verbalScore = items1[i]["J"].ToString();
                List<LOT_Candidate_Exam> list = new List<LOT_Candidate_Exam>();
                //var candidateExam = canExamDao.GetByExamIdEmpId(examWritingID, empId);

                Message msg = null;
                
                int uniqueId = canExamDao.getlastID();                    
                uniqueId = uniqueId + 1;
                //add to list
                LOT_Candidate_Exam item = new LOT_Candidate_Exam();
                item.IsFinish = true;
                item.ExamID = examID;
                item.EmployeeID = empId;
                item.CandidatePin = uniqueId + CommonFunc.encryptDataMd5(uniqueId + DateTime.Now.ToString(), 2);
                item.CreatedBy = principal.UserData.UserName;
                item.CreateDate = DateTime.Now;
                item.UpdatedBy = principal.UserData.UserName;
                item.UpdateDate = DateTime.Now;
                item.WritingMark = writingScore;
                item.WritingComment = examWriDate.ToString(Constants.DATETIME_FORMAT_VIEW);
                if (!string.IsNullOrEmpty(verbalScore))
                {
                    item.VerbalMarkType = Constants.LOT_VERBAL_MARK_TYPE_LEVEL;
                    item.VerbalComment = examVerbalDate.ToString(Constants.DATETIME_FORMAT_VIEW);
                    item.VerbalMark = float.Parse(verbalScore);
                    item.UpdateDate = examVerbalDate;
                    item.UpdatedBy = principal.UserData.UserName;
                    item.VerbalTestedBy = "tan.tran";
                }
                list.Add(item);
                //perform to assign
                msg = canExamDao.AssignCandidate(list);
                if (msg.MsgType == MessageType.Info)
                {
                    success.Add("Add english writing result for employee ( " + empId + " )");
                }
                else
                {
                    notSuccess.Add(msg.MsgText);
                }
                
            }
            
            ViewData["SUCCESS"] = success;
            ViewData["NOTSUCCESS"] = notSuccess;

            return View("SeatCode");
        }

        [HttpPost]
        public ActionResult SeatCode(FormCollection form)
        {
            AuthenticationProjectPrincipal principal = HttpContext.User as AuthenticationProjectPrincipal;
            LocationDao locDao = new LocationDao();
            List<string> success = new List<string>();
            List<string> notSuccess = new List<string>();

            HttpPostedFileBase hpf = Request.Files["file"] as HttpPostedFileBase;
            string strPath = Server.MapPath(Constants.IMPORT_EMS);
            string fileName = Path.GetFileName(hpf.FileName);
            fileName = ConvertUtil.FormatFileName(fileName);
            int maxRow = ConvertUtil.ConvertToInt(form["maxRow"]);
            if (System.IO.File.Exists(strPath + fileName))
            {
                System.IO.File.Delete(strPath + fileName);
            }
            hpf.SaveAs(strPath + fileName);

            List<Hashtable> items1 = ReadSeatCodeDataFromExcel(fileName, 2, 40, 1);
            string seatcode = "";
            Message msg = null;
            for (int i = 0; i < items1.Count; i++)
            {
                string empId = items1[i]["B"].ToString();
                if (string.IsNullOrEmpty(empId))
                    continue;
                Floor floor = locDao.GetFloorByName(items1[i]["F"].ToString());
                SeatCode seat = locDao.GetSeatCodeByName(items1[i]["E"].ToString(), (floor != null ? floor.ID:0));                
                
                Employee emp = empDao.GetById(empId);
                //if (emp == null)
                //    emp = empDao.GetById("9999");
                if (emp == null || floor == null || seat == null) 
                {
                    if (floor == null)
                    {
                        if (items1[i]["F"].ToString() != "No seat required")
                            notSuccess.Add("<strong>" + "(Emp Id:" + empId + ") \"" + items1[i]["F"].ToString() + "\"</strong>" + " not exist in Floor table");
                    }
                    else if (seat == null)
                    {
                        if (items1[i]["E"].ToString() != "No seat required")
                            notSuccess.Add("<strong>" + "(Emp Id:" + empId + ") \"" + items1[i]["E"].ToString() + "\" </strong>" + " not exist in Seat code table");
                    }
                    else

                        notSuccess.Add("<strong>" + empId + "</strong>" + " not exist in Employee table");
                }
                else 
                {
                    seatcode = CommonFunc.GenerateLocationCode(floor.Office.BranchID.ToString(), floor.Office.ID.ToString(), floor.ID.ToString(), seat.ID.ToString());
                    emp.UpdateDate = DateTime.Now;
                    emp.LocationCode = seatcode;                      
                    msg = empDao.UpdatePosition(emp);
                    if (msg.MsgType == MessageType.Info)
                    {
                        success.Add("Updated position for employee ( " + empId + " )");
                    }
                    else
                    {
                        notSuccess.Add(empId + " not exist in Emloyee table");
                    }
                }
                
            }

            ViewData["SUCCESS"] = success;
            ViewData["NOTSUCCESS"] = notSuccess;

            return View();
        }

        public ActionResult PTOBalance()
        {
            return View();
        }
        [HttpPost]
        public ActionResult PTOBalance(FormCollection form)
        {
            //AuthenticationProjectPrincipal principal = HttpContext.User as AuthenticationProjectPrincipal;
            //LocationDao locDao = new LocationDao();
            try
            {
                List<string> success = new List<string>();
                List<string> notSuccess = new List<string>();
                //var probationList = new string[] { 
                //    "1874","1855","5044","1868","1888","1891","1889","1892","1893","1890",
                //    "1887","1884","1885","5052","1877","1895","1894","1883","1886","5056",
                //    "5058","5057","5059","5048","5046","5061","5060","5047","1896","1876"
                //};

                HttpPostedFileBase hpf = Request.Files["file"] as HttpPostedFileBase;
                string strPath = Server.MapPath(Constants.IMPORT_EMS);
                string fileName = Path.GetFileName(hpf.FileName);
                int startRow = ConvertUtil.ConvertToInt(form["startRow"]);
                int endRow = ConvertUtil.ConvertToInt(form["endRow"]);
                int sheetIndex = ConvertUtil.ConvertToInt(form["sheetIndex"]);
                int reportMonth = ConvertUtil.ConvertToInt(form["reportMonth"]);
                int reportYear = ConvertUtil.ConvertToInt(form["reportYear"]);
                fileName = ConvertUtil.FormatFileName(fileName);
                int maxRow = ConvertUtil.ConvertToInt(form["maxRow"]);
                var probationList = form["probationId"].Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                if (System.IO.File.Exists(strPath + fileName))
                {
                    System.IO.File.Delete(strPath + fileName);
                }
                hpf.SaveAs(strPath + fileName);
                string[] colNames = new string[] { "C", "E" };//C: EmpId, E: Balance
                List<Hashtable> data = ReadDataFromExcel(fileName, startRow, endRow, sheetIndex, colNames);
                Message msg = null;
                for (int i = 0; i < data.Count; i++)
                {
                    string empId = data[i]["C"].ToString();
                    if (string.IsNullOrEmpty(empId))
                        continue;
                    Employee emp = empDao.GetById(empId, false);
                    //if (emp == null)
                    //    emp = empDao.GetById("9999");
                    if (emp == null)
                    {
                        notSuccess.Add("<strong>" + empId + "</strong>" + " not exist in Employee table");
                    }
                    else
                    {
                        int balance = 0;
                        if (!int.TryParse(data[i]["E"].ToString(), out balance))
                        {
                            notSuccess.Add("Invalid balance (" + data[i]["E"] + ") for employee " + empId);
                            continue;
                        }

                        msg = ptoReportDao.Import(empId, probationList.Contains(empId),new DateTime(reportYear, reportMonth, 1), balance);

                        if (msg.MsgType == MessageType.Info)
                            success.Add(empId + " -> " + balance);
                        else
                            notSuccess.Add(empId + " -> " + msg.MsgText);
                    }

                }

                ViewData["SUCCESS"] = success;
                ViewData["NOTSUCCESS"] = notSuccess;
            }
            catch(Exception ex)
            {
                ShowMessage(new Message(MessageConstants.E0033, MessageType.Error, ex.Message));
            }
            return View();
        }

    }
}
