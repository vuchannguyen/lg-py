using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using CRM.Models;
using CRM.Library.Common;
using CRM.Library.Attributes;
using CRM.Controllers;
using System.Collections;

namespace CRM.Controllers
{
    /// <summary>
    /// Exam Question Controller
    /// </summary>
    public class ExamQuestionController : BaseController
    {
        #region

        private ExamQuestionSectionQuestionDAO examQuestionSectionQuestionDao = new ExamQuestionSectionQuestionDAO();
        private ExamQuestionSectionListeningTopicDao examQuestionSectionListeningTopicDao = new ExamQuestionSectionListeningTopicDao();
        private ExamQuestionSectionComprehensionDao examQuestionSectionComprehensionDao = new ExamQuestionSectionComprehensionDao();
        private ExamQuestionSectionDAO examQuestionSectionDao = new ExamQuestionSectionDAO();
        private ExamQuestionDAO examQuestionDao = new ExamQuestionDAO();
        private SectionDAO sectionDao = new SectionDAO();
        private QuestionDao questionDao = new QuestionDao();

        #endregion
        //
        // GET: /ExamQuestion/
        [CrmAuthorizeAttribute(Module = Modules.ExamQuestion, Rights = Permissions.Read)]
        public ActionResult Index()
        {
            if (Session[SessionKey.TRAINING_CENTER_EXAM_QUESTION] != null)
            {
                Hashtable hash = (Hashtable)Session[SessionKey.TRAINING_CENTER_EXAM_QUESTION];
                ViewData[Constants.TRAINING_EXAM_QUESTIONS_KEYWORD] = (string)hash[Constants.TRAINING_EXAM_QUESTIONS_KEYWORD];
                ViewData[Constants.TRAINING_EXAM_QUESTIONS_SORT_COLUMN] = (string)hash[Constants.TRAINING_EXAM_QUESTIONS_SORT_COLUMN];
                ViewData[Constants.TRAINING_EXAM_QUESTIONS_SORT_ORDER] = (string)hash[Constants.TRAINING_EXAM_QUESTIONS_SORT_ORDER];
                ViewData[Constants.TRAINING_EXAM_QUESTIONS_SORT_PAGE_INDEX] = (ConvertUtil.ConvertToInt(hash[Constants.TRAINING_EXAM_QUESTIONS_SORT_PAGE_INDEX])).ToString();
                ViewData[Constants.TRAINING_EXAM_QUESTIONS_SORT_ROW_COUNT] = (ConvertUtil.ConvertToInt(hash[Constants.TRAINING_EXAM_QUESTIONS_SORT_ROW_COUNT])).ToString();
            }
            else
            {
                ViewData[Constants.TRAINING_EXAM_QUESTIONS_KEYWORD] = Constants.TRAINING_EXAM_QUESTIONS_DEFAULT_VALUE;
                ViewData[Constants.TRAINING_EXAM_QUESTIONS_SORT_COLUMN] = "ID";
                ViewData[Constants.TRAINING_EXAM_QUESTIONS_SORT_ORDER] = "desc";
                ViewData[Constants.TRAINING_EXAM_QUESTIONS_SORT_PAGE_INDEX] = "1";
                ViewData[Constants.TRAINING_EXAM_QUESTIONS_SORT_ROW_COUNT] = "20";
            }
            return View();
        }

        private void SetSessionFilter(string className, string sortColumn, string sortOrder, int pageIndex, int rowCount)
        {
            Hashtable hash = new Hashtable();
            hash[Constants.TRAINING_EXAM_QUESTIONS_KEYWORD] = className;
            hash[Constants.TRAINING_EXAM_QUESTIONS_SORT_COLUMN] = sortColumn;
            hash[Constants.TRAINING_EXAM_QUESTIONS_SORT_ORDER] = sortOrder;
            hash[Constants.TRAINING_EXAM_QUESTIONS_SORT_PAGE_INDEX] = pageIndex;
            hash[Constants.TRAINING_EXAM_QUESTIONS_SORT_ROW_COUNT] = rowCount;
            Session[SessionKey.TRAINING_CENTER_EXAM_QUESTION] = hash;
        }

        /// <summary>
        /// Get Exam Question List and bind to grid JQGrid
        /// </summary>
        /// <returns></returns>
        [CrmAuthorizeAttribute(Module = Modules.ExamQuestion, Rights = Permissions.Read)]
        public ActionResult GetListJQGrid(string filterClassName)
        {

            #region JQGrid Params
            string sortColumn = (Request.Params[GridConstants.SORT_COLUMN]).ToString();
            string sortOrder = (Request.Params[GridConstants.SORT_ORDER]).ToString();
            int pageIndex = Convert.ToInt32(Request.Params[GridConstants.PAGE_INDEX]);
            int rowCount = Convert.ToInt32(Request.Params[GridConstants.ROW_COUNT]);
            #endregion
            SetSessionFilter(filterClassName,sortColumn,sortOrder,pageIndex,rowCount);

            string classname = "";
            int classID = 0;
            if (filterClassName != Constants.TRAINING_EXAM_QUESTIONS_DEFAULT_VALUE && !string.IsNullOrEmpty(filterClassName))
            {
                classname = filterClassName;
                classID = ConvertUtil.ConvertToInt(filterClassName);
            }
            List<LOT_ExamQuestion> examQuestionList = examQuestionDao.GetList()
                .Where(q =>(classID==0 ? q.Title.Contains(classname): (q.Title.Contains(classname) 
                    || q.ID==classID))).OrderByDescending(q => q.ID).ToList();
            //for paging
            int totalRecords = examQuestionList.Count();
            int totalPages = (int)Math.Ceiling((float)totalRecords / (float)rowCount);

            var finalList = examQuestionDao.Sort(examQuestionList, sortColumn, sortOrder)
                                  .Skip((pageIndex - 1) * rowCount)
                                   .Take(rowCount);
            //bind data to grid
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
                            "<a href='/ExamQuestion/Details/" + m.ID.ToString() + "'>" + HttpUtility.HtmlEncode(m.Title) + "</a>",
                            m.ExamQuestionTime.ToString(),                             
                            !examQuestionDao.IsTested(m.ID) ? "<input type=\"button\" class=\"icon edit\" title=\"Edit\" onclick=\"CRM.popup('/ExamQuestion/Edit/" + m.ID.ToString() + "', 'Update', 800)\" />&nbsp;"
                            + (examQuestionDao.GetNotRandomSectionList(m.ID).Count == 0 || examQuestionDao.HasOnlyVerbalSection(m.ID) ? "" :
                            "<input type=\"button\" class=\"icon add-question\" title=\"Assign Question\" onclick=\"CRM.redirect('/ExamQuestion/AssignList/" + m.ID.ToString() + "')\" />") : string.Empty
                        }
                    }
                ).ToArray()
            };
            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }
        //
        // GET: /ExamQuestion/Details/id
        [CrmAuthorizeAttribute(Module = Modules.ExamQuestion, Rights = Permissions.Read)]
        public ActionResult Details(int id)
        {
            LOT_ExamQuestion examQuestion = examQuestionDao.GetByID(id);
            ViewData[CommonDataKey.EXAM_QUESTION_TITLE] = HttpUtility.HtmlEncode(examQuestion.Title);
            return View(examQuestion);
        }

        //
        // GET: /ExamQuestion/Create
        [CrmAuthorizeAttribute(Module = Modules.ExamQuestion, Rights = Permissions.Assign, ShowAtCurrentPage = true)]
        public ActionResult AssignList(int id)
        {
            List<sp_GetSectionListByExamQuestionIDResult> list = examQuestionDao.GetNotRandomSectionList(id).OrderBy(c => c.SectionID).ToList<sp_GetSectionListByExamQuestionIDResult>();
            list = list.Where(p=>p.SectionID != Constants.LOT_VERBAL_SKILL_ID).ToList();
            if (list.Count == 0) //if invalid case return to index page
            {
                return RedirectToAction("Index");
            }
            else
            {
                LOT_ExamQuestion examQuestion = examQuestionDao.GetByID(id);
                ViewData[CommonDataKey.SECTION_LIST] = new SelectList(list, "ID", "SectionName", TempData[CommonDataKey.SECTION_SELECTED] == null ? string.Empty : TempData[CommonDataKey.SECTION_SELECTED]);
                ViewData[CommonDataKey.EXAM_QUESTION_ID] = id;
                ViewData[CommonDataKey.EXAM_QUESTION_TITLE] = HttpUtility.HtmlEncode(examQuestion.Title);
            }
            return View();
        }

        //
        // GET: /ExamQuestion/Assign/id
        [CrmAuthorizeAttribute(Module = Modules.ExamQuestion, Rights = Permissions.Assign, ShowInPopup = true)]
        public ActionResult Assign(int id)
        {
            ViewData[CommonDataKey.EXAM_QUESTION_SECTION_ID] = id;
            LOT_ExamQuestion_Section examQuestionSection = examQuestionSectionDao.GetByID(id);
            if (examQuestionSection != null)
            {
                ViewData[CommonDataKey.EXAM_QUESTION_ID] = examQuestionSection.ExamQuestionID;
            }

            return View();
        }

        //
        // POST: /ExamQuestion/Assign
        [HttpPost]
        [ValidateInput(false)]
        [CrmAuthorizeAttribute(Module = Modules.ExamQuestion, Rights = Permissions.Assign, ShowInPopup = true)]
        public ActionResult Assign(FormCollection collection)
        {
            Message msg = null;
            string strExamQuestionSectionID = collection["ExamQuestionSectionID"];
            TempData[CommonDataKey.SECTION_SELECTED] = collection["ExamQuestionSectionID"];
            string selectedIDs = collection["AssignIDs"];
            //check valid params
            if (!string.IsNullOrEmpty(selectedIDs) && !string.IsNullOrEmpty(strExamQuestionSectionID))
            {
                //put all ids to array
                selectedIDs = selectedIDs.TrimEnd(Constants.SEPARATE_IDS_CHAR);
                string[] idArr = selectedIDs.Split(Constants.SEPARATE_IDS_CHAR);

                int examQuestionSectionID = int.Parse(strExamQuestionSectionID);
                //get exam question - section
                LOT_ExamQuestion_Section examQuestion_Section = examQuestionSectionDao.GetByID(examQuestionSectionID);

                //insert to db
                if (examQuestion_Section.SectionID == Constants.LOT_LISTENING_TOPIC_ID)
                {
                    List<LOT_ExamQuestion_Section_ListeningTopic> listeningTopics = AddToListeningTopicList(idArr, examQuestion_Section, examQuestionSectionID);
                    msg = examQuestionSectionListeningTopicDao.AssignQuestion(listeningTopics);
                }
                else if (examQuestion_Section.SectionID == Constants.LOT_COMPREHENSION_SKILL_ID)
                {
                    List<LOT_ExamQuestion_Section_Comprehension> paragraphs = AddToParagraphList(idArr, examQuestion_Section, examQuestionSectionID);
                    msg = examQuestionSectionComprehensionDao.AssignQuestion(paragraphs);
                }
                else
                {
                    List<LOT_ExamQuestion_Section_Question> questionlist = AddToQuestionList(idArr, examQuestion_Section, examQuestionSectionID);
                    msg = examQuestionSectionQuestionDao.AssignQuestion(questionlist);
                }

                ShowMessage(msg);
            }
            return RedirectToAction("AssignList/" + collection[CommonDataKey.EXAM_QUESTION_ID]);
        }

        /// <summary>
        /// Add all selected listening topic to list
        /// </summary>
        /// <param name="idArr"></param>
        /// <param name="examQuestion_Section"></param>
        /// <param name="examQuestionSectionID"></param>
        /// <returns></returns>
        [NonAction]
        private List<LOT_ExamQuestion_Section_ListeningTopic> AddToListeningTopicList(string[] idArr, LOT_ExamQuestion_Section examQuestion_Section, int examQuestionSectionID)
        {
            List<LOT_ExamQuestion_Section_ListeningTopic> listeningTopics = new List<LOT_ExamQuestion_Section_ListeningTopic>();
            //loop each selected id
            foreach (string strId in idArr)
            {
                int id = 0;
                bool isInterger = Int32.TryParse(strId, out id);
                //is check all question in list to assign
                if (isInterger)
                {
                    LOT_ExamQuestion_Section_ListeningTopic item = new LOT_ExamQuestion_Section_ListeningTopic();
                    item.ListeningTopicID = id;
                    item.ExamQuestionSectionID = examQuestionSectionID;
                    listeningTopics.Add(item);

                }
            }

            return listeningTopics;
        }

        /// <summary>
        /// Add all selected paragraph to list
        /// </summary>
        /// <param name="idArr"></param>
        /// <param name="examQuestion_Section"></param>
        /// <param name="examQuestionSectionID"></param>
        /// <returns></returns>
        [NonAction]
        private List<LOT_ExamQuestion_Section_Comprehension> AddToParagraphList(string[] idArr, LOT_ExamQuestion_Section examQuestion_Section, int examQuestionSectionID)
        {
            List<LOT_ExamQuestion_Section_Comprehension> paragraphs = new List<LOT_ExamQuestion_Section_Comprehension>();
            //loop each selected id
            foreach (string strId in idArr)
            {
                int id = 0;
                bool isInterger = Int32.TryParse(strId, out id);
                //is check all question in list to assign
                if (isInterger)
                {
                    LOT_ExamQuestion_Section_Comprehension item = new LOT_ExamQuestion_Section_Comprehension();
                    item.ParagraphID = id;
                    item.ExamQuestionSectionID = examQuestionSectionID;
                    paragraphs.Add(item);

                }
            }

            return paragraphs;
        }

        /// <summary>
        /// Add all selected question to list
        /// </summary>
        /// <param name="idArr"></param>
        /// <param name="examQuestion_Section"></param>
        /// <param name="examQuestionSectionID"></param>
        /// <returns></returns>
        [NonAction]
        private List<LOT_ExamQuestion_Section_Question> AddToQuestionList(string[] idArr, LOT_ExamQuestion_Section examQuestion_Section, int examQuestionSectionID)
        {
            List<LOT_ExamQuestion_Section_Question> questionlist = new List<LOT_ExamQuestion_Section_Question>();
            //loop each selected id
            foreach (string strId in idArr)
            {
                int id = 0;
                bool isInterger = Int32.TryParse(strId, out id);
                //is check all question in list to assign
                if (isInterger)
                {
                    LOT_ExamQuestion_Section_Question item = new LOT_ExamQuestion_Section_Question();
                    item.QuestionID = id;
                    item.ExamQuestionSectionID = examQuestionSectionID;
                    questionlist.Add(item);
                }
            }

            return questionlist;
        }

        /// <summary>
        /// Remove Assigned Questions out of List
        /// </summary>
        /// <param name="ids"></param>
        /// <param name="examQuestionID"></param>
        /// <param name="examQuestionSectionId"></param>
        /// <returns></returns>
        [CrmAuthorizeAttribute(Module = Modules.ExamQuestion, Rights = Permissions.Assign)]
        public ActionResult RemoveAssignList(string ids, string examQuestionID, string examQuestionSectionId)
        {
            Message msg = null;
            //check valid params
            if (!string.IsNullOrEmpty(examQuestionSectionId) && !string.IsNullOrEmpty(ids))
            {
                LOT_ExamQuestion_Section examQuestionSection = examQuestionSectionDao.GetByID(int.Parse(examQuestionSectionId));
                if (examQuestionSection != null)
                {
                    //remove from db
                    if (examQuestionSection.SectionID == Constants.LOT_LISTENING_TOPIC_ID)
                    {
                        msg = examQuestionSectionListeningTopicDao.RemoveAssignList(ids);
                    }
                    else if (examQuestionSection.SectionID == Constants.LOT_COMPREHENSION_SKILL_ID)
                    {
                        msg = examQuestionSectionComprehensionDao.RemoveAssignList(ids);
                    }
                    else
                    {
                        msg = examQuestionSectionQuestionDao.RemoveAssignList(ids);
                    }
                }

                TempData[CommonDataKey.SECTION_SELECTED] = examQuestionSectionId;
            }
            ShowMessage(msg);
            return RedirectToAction("AssignList/" + examQuestionID);
        }        

        /// <summary>
        /// Get assigned questions and bind to jqgrid
        /// </summary>
        /// <param name="examQuestionSectionId"></param>
        /// <returns></returns>
        [CrmAuthorizeAttribute(Module = Modules.ExamQuestion, Rights = Permissions.Assign)]
        public ActionResult GetAssignListJQGrid(string examQuestionSectionId)
        {

            #region JQGrid Params
            string sortColumn = (Request.Params[GridConstants.SORT_COLUMN]).ToString();
            string sortOrder = (Request.Params[GridConstants.SORT_ORDER]).ToString();
            int pageIndex = Convert.ToInt32(Request.Params[GridConstants.PAGE_INDEX]);
            int rowCount = Convert.ToInt32(Request.Params[GridConstants.ROW_COUNT]);
            #endregion

            //check valid params
            if (!string.IsNullOrEmpty(examQuestionSectionId))
            {
                int exQuestionSectionId = int.Parse(examQuestionSectionId);
                LOT_ExamQuestion_Section examQuestionSection = examQuestionSectionDao.GetByID(exQuestionSectionId);

                if (examQuestionSection.SectionID == Constants.LOT_LISTENING_TOPIC_ID) //listening topic case
                {
                    //get listening topic list
                    List<sp_GetListeningTopicListByExamQuestionSectionIDResult> listeningTopicList = examQuestionDao.GetListeningTopicListByExamQuestionID(exQuestionSectionId);

                    //for paging
                    int totalRecords = listeningTopicList.Count();
                    int totalPages = (int)Math.Ceiling((float)totalRecords / (float)rowCount);

                    var finalList = listeningTopicList.Skip((pageIndex - 1) * rowCount)
                                       .Take(rowCount);
                    //bind to grid
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
                                CommonFunc.SubStringRoundWord(m.TopicName,Constants.QUESTION_LENGTH_TO_TRUNCATE),   
                                CommonFunc.GetDetailsViewLink(m.ListeningTopicID,examQuestionSection.SectionID)                                
                                }
                            }
                        ).ToArray()
                    };

                    return Json(jsonData, JsonRequestBehavior.AllowGet);
                }
                else if (examQuestionSection.SectionID == Constants.LOT_COMPREHENSION_SKILL_ID) //comprehension case
                {
                    //get listening topic list
                    List<sp_GetComprehensionParagraphListByExamQuestionSectionIDResult> paragraphList = examQuestionDao.GetComprehensionParagraphListByExamQuestionID(exQuestionSectionId);

                    //for paging
                    int totalRecords = paragraphList.Count();
                    int totalPages = (int)Math.Ceiling((float)totalRecords / (float)rowCount);

                    var finalList = paragraphList.Skip((pageIndex - 1) * rowCount)
                                       .Take(rowCount);
                    //bind to grid
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
                                CommonFunc.SubStringRoundWord(m.ParagraphContent,Constants.QUESTION_LENGTH_TO_TRUNCATE),                              
                                CommonFunc.GetDetailsViewLink(m.ParagraphID, examQuestionSection.SectionID)                                
                                }
                            }
                        ).ToArray()
                    };

                    return Json(jsonData, JsonRequestBehavior.AllowGet);
                }
                else //question case
                {
                    //get list
                    List<sp_GetQuestionListByExamQuestionSectionIDResult> questionList = examQuestionDao.GetQuestionListByExamQuestionID(exQuestionSectionId);

                    //for paging
                    int totalRecords = questionList.Count();
                    int totalPages = (int)Math.Ceiling((float)totalRecords / (float)rowCount);

                    var finalList = questionList.Skip((pageIndex - 1) * rowCount)
                                       .Take(rowCount);

                    //bind to grid
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
                            (examQuestionSection.SectionID == Constants.LOT_PROGRAMMING_SKILL_ID ? "<pre>" + CommonFunc.SubStringRoundWordNotMultiline(m.QuestionContent,Constants.QUESTION_LENGTH_TO_TRUNCATE) + "</pre>" : CommonFunc.SubStringRoundWord(m.QuestionContent,Constants.QUESTION_LENGTH_TO_TRUNCATE)),                           
                            CommonFunc.GetDetailsViewLink(m.QuestionID, examQuestionSection.SectionID)                            
                        }
                            }
                        ).ToArray()
                    };
                    return Json(jsonData, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Get Question List to assign
        /// </summary>
        /// <param name="examQuestionSectionID"></param>
        /// <returns></returns>
        [ValidateInput(false)]
        [CrmAuthorizeAttribute(Module = Modules.ExamQuestion, Rights = Permissions.Assign)]
        public ActionResult GetQuestionListJQGrid(string examQuestionSectionId, string text)
        {

            #region JQGrid Params
            string sortColumn = (Request.Params[GridConstants.SORT_COLUMN]).ToString();
            string sortOrder = (Request.Params[GridConstants.SORT_ORDER]).ToString();
            int pageIndex = Convert.ToInt32(Request.Params[GridConstants.PAGE_INDEX]);
            int rowCount = Convert.ToInt32(Request.Params[GridConstants.ROW_COUNT]);
            #endregion
            //check valid param
            if (!string.IsNullOrEmpty(examQuestionSectionId))
            {                
                int exQuestionSectionId = int.Parse(examQuestionSectionId);
                //get list
                List<LOT_Question> questionList = examQuestionDao.GetQuestionListByExamQuestionSectionID(exQuestionSectionId);

                //for filtering
                if (!string.IsNullOrEmpty(text))
                {
                    questionList = questionList.Where(c => c.QuestionContent.ToLower().Contains(text.ToLower().Trim())).ToList<LOT_Question>();
                }

                //for paging
                int totalRecords = questionList.Count();
                int totalPages = (int)Math.Ceiling((float)totalRecords / (float)rowCount);

                var finalList = questionList.Skip((pageIndex - 1) * rowCount)
                                   .Take(rowCount);
                //bind to grid
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
                            (m.SectionID == Constants.LOT_PROGRAMMING_SKILL_ID ? "<pre>" + CommonFunc.SubStringRoundWordNotMultiline(m.QuestionContent,Constants.QUESTION_LENGTH_TO_TRUNCATE) + "</pre>": CommonFunc.SubStringRoundWord(m.QuestionContent,Constants.QUESTION_LENGTH_TO_TRUNCATE))                            
                            }
                        }
                    ).ToArray()
                };
                return Json(jsonData, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return null;
            }
        }

        //
        // GET: /ExamQuestion/Create
        [CrmAuthorizeAttribute(Module = Modules.ExamQuestion, Rights = Permissions.Insert, ShowInPopup = true)]
        public ActionResult Create()
        {
            List<LOT_Section> list = sectionDao.GetList();
            ViewData.Add(CommonDataKey.SECTION, list);
            return View();
        }

        //
        // POST: /ExamQuestion/Create
        [HttpPost]
        [ValidateInput(false)]
        [CrmAuthorizeAttribute(Module = Modules.ExamQuestion, Rights = Permissions.Insert, ShowInPopup = true)]
        public JsonResult Create(FormCollection collection)
        {
            var principal = HttpContext.User as AuthenticationProjectPrincipal;
            JsonResult result = new JsonResult();
            Message msg = null;
            bool canSave = false;
            try
            {
                //Initialize objects
                List<LOT_ExamQuestion_Section> list = new List<LOT_ExamQuestion_Section>();
                LOT_ExamQuestion examQuestion = new LOT_ExamQuestion();

                //get data
                examQuestion.Title = collection["Title"];
                if (!string.IsNullOrEmpty(collection["Time"]))
                {
                    examQuestion.ExamQuestionTime = int.Parse(collection["Time"]);
                }

                //loop all the checkboxs on GUI to get data
                foreach (string key in collection.AllKeys)
                {
                    if (key.ToString().StartsWith(Constants.LOT_SECTIONID_STARTWITH))
                    {
                        string value = collection.GetValues(key)[0];

                        //Set data to object
                        LOT_ExamQuestion_Section item = SetDataToObject(value, collection, key, ref msg, null);
                        if (msg == null && item != null) //check valid object
                        {
                            canSave = true;
                            list.Add(item);
                        }
                    }
                }

                //all conditions are ok
                if (canSave)
                {
                    examQuestion.CreatedBy = principal.UserData.UserName;
                    examQuestion.UpdatedBy = principal.UserData.UserName;
                    msg = examQuestionDao.InsertMulti(examQuestion, list);
                }
                else
                {
                    if(msg == null)
                        msg = new Message(MessageConstants.E0034, MessageType.Error);
                }
                result.Data = new { msg };
            }
            catch
            {
                msg = new Message(MessageConstants.E0007, MessageType.Error);
                result.Data = new { msg };
            }
            return result;
        }

        /// <summary>
        /// Add Section for Exam Question
        /// </summary>
        [NonAction]
        private LOT_ExamQuestion_Section SetDataToObject(string value, FormCollection collection, string key, ref Message msg, int? examQuestionID)
        {
            bool isOk = true;
            bool check = false;
            //bool canSave = false;
            if (!string.IsNullOrEmpty(value))
            {
                check = bool.Parse(value);
            }
            if (check)
            {
                //get data
                LOT_ExamQuestion_Section item = new LOT_ExamQuestion_Section();
                int sectionId = ConvertUtil.ConvertToInt(key.Split('_')[1]);
                item.SectionID = sectionId;
                if (examQuestionID.HasValue)
                    item.ExamQuestionID = examQuestionID.Value;
                if (sectionId == Constants.LOT_VERBAL_SKILL_ID)
                {
                    item.IsRandom = false;
                    item.MaxMark = Constants.LOT_VERBAL_MAX_MARK;
                    item.NumberOfQuestions = null;
                    return item;
                }
                string maxMark = collection["MaxMark_" + key];
                string isRandomValue = collection.GetValues("IsRandom_" + key)[0];
                string numberOfQuestions = collection["NumberOfQuestions_IsRandom_" + key];
                bool isRandom = bool.Parse(isRandomValue);
                int maxNumberOfQuestions = questionDao.GetListBySectionID(sectionId).Count;
                var sectionObj = sectionDao.GetByID(sectionId);
                if (!string.IsNullOrEmpty(numberOfQuestions) && ConvertUtil.ConvertToInt(numberOfQuestions) > maxNumberOfQuestions)
                {
                    isOk = false;
                    msg = new Message(MessageConstants.E0029, MessageType.Error, "Number of Question in " + 
                        sectionObj.SectionName + " section", maxNumberOfQuestions);
                }
                //check valid marmark
                if (string.IsNullOrEmpty(maxMark))
                {
                    isOk = false;
                    msg = new Message(MessageConstants.E0001, MessageType.Error, "Max Mark");
                }
                else if (isRandom)
                {
                    if (string.IsNullOrEmpty(numberOfQuestions)) //check valid number of question
                    {
                        isOk = false;
                        msg = new Message(MessageConstants.E0001, MessageType.Error, "Number Of Question");
                    }
                    else if (sectionId == Constants.LOT_COMPREHENSION_SKILL_ID &&
                    CommonFunc.LOTGetRandomParagraphs(ConvertUtil.ConvertToInt(numberOfQuestions)).Count == 0)
                    {
                        isOk = false;
                        msg = new Message(MessageConstants.E0049, MessageType.Error, "enter another number of questions for the Comprehension Skill section");
                    }
                    else if(sectionId == Constants.LOT_LISTENING_TOPIC_ID &&  
                        CommonFunc.LOTGetRandomListeningTopics(ConvertUtil.ConvertToInt(numberOfQuestions)).Count == 0 )
                    {
                        isOk = false;
                        msg = new Message(MessageConstants.E0049, MessageType.Error, "enter another number of questions for the Listening Skill section");
                    }
                }

                //if all objects are valid
                if (isOk)
                {
                    //if (examQuestionID.HasValue)
                    //{
                    //    item.ExamQuestionID = examQuestionID.Value;
                    //}

                    //item.SectionID = int.Parse(key.Split('_')[1]);
                    item.IsRandom = isRandom;
                    if (isRandom)
                    {
                        item.NumberOfQuestions = int.Parse(numberOfQuestions);
                    }
                    item.MaxMark = int.Parse(maxMark);
                }
                return item;
            }
            return null;
        }

        //
        // GET: /ExamQuestion/Edit/id
        [CrmAuthorizeAttribute(Module = Modules.ExamQuestion, Rights = Permissions.Update, ShowInPopup = true)]
        public ActionResult Edit(int id)
        {
            LOT_ExamQuestion examQuestion = examQuestionDao.GetByID(id);
            List<LOT_Section> sectionList = sectionDao.GetList();
            List<LOT_ExamQuestion_Section> examQuestionList = examQuestionSectionDao.GetListByExamQuestionID(id);
            ViewData.Add(CommonDataKey.SECTION, sectionList);
            ViewData.Add(CommonDataKey.EXAM_QUESTION_SECTION, examQuestionList);
            return View(examQuestion);
        }

        //
        // POST: /ExamQuestion/Edit/5
        [HttpPost]
        [ValidateInput(false)]
        [CrmAuthorizeAttribute(Module = Modules.ExamQuestion, Rights = Permissions.Update, ShowInPopup = true)]
        public ActionResult Edit(int id, FormCollection collection)
        {
            var principal = HttpContext.User as AuthenticationProjectPrincipal;
            JsonResult result = new JsonResult();
            Message msg = null;
            bool canSave = false;
            try
            {
                //Initialize objects
                List<LOT_ExamQuestion_Section> list = new List<LOT_ExamQuestion_Section>();
                LOT_ExamQuestion examQuestion = examQuestionDao.GetByID(id);

                //Get data
                examQuestion.Title = collection["Title"];
                examQuestion.ExamQuestionTime = int.Parse(collection["Time"]);

                //loop all the checkboxs on GUI to get data                
                foreach (string key in collection.AllKeys)
                {
                    if (key.ToString().StartsWith(Constants.LOT_SECTIONID_STARTWITH))
                    {
                        string value = collection.GetValues(key)[0];
                        //set data to object
                        LOT_ExamQuestion_Section item = SetDataToObject(value, collection, key, ref msg, examQuestion.ID);

                        if (msg == null && item != null) //check valid object
                        {
                            canSave = true;
                            list.Add(item);
                        }
                    }
                }

                //if all conditions are ok
                if (canSave)
                {
                    examQuestion.UpdatedBy = principal.UserData.UserName;
                    msg = examQuestionDao.UpdateMulti(examQuestion, list);
                }
                else if(msg  == null)
                {
                    msg = new Message(MessageConstants.E0034, MessageType.Error);
                }
                result.Data = new { msg };
            }
            catch
            {
                msg = new Message(MessageConstants.E0007, MessageType.Error);
                result.Data = new { msg };
            }
            return result;
        }


        /// <summary>
        /// Delete List
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>        
        [HttpPost]
        [CrmAuthorizeAttribute(Module = Modules.ExamQuestion, Rights = Permissions.Delete, ShowAtCurrentPage = true)]
        public ActionResult DeleteList(string id)
        {
            var principal = HttpContext.User as AuthenticationProjectPrincipal;
            Message msg = examQuestionDao.DeleteList(id, principal.UserData.UserName);
            //ShowMessage(msg);
            //return RedirectToAction("Index");
            JsonResult result = new JsonResult();
            return Json(msg);
        }
    }
}
