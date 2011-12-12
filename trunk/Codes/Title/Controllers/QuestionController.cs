using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;

using CRM.Models;
using CRM.Library.Common;
using CRM.Library.Attributes;
using CRM.Controllers;
using System.Web.Routing;


namespace CRM.Controllers
{
    /// <summary>
    /// Controller of question
    /// </summary>
    public class QuestionController : BaseController
    {
        //
        // GET: /Question/

        #region global variables

        /// <summary>
        /// Data access object of Question
        /// </summary>
        private QuestionDao questionDao = new QuestionDao();
        /// <summary>
        /// Data access object of Section
        /// </summary>
        private SectionDAO sectionDao = new SectionDAO();
        /// <summary>
        /// Data access object of Answer
        /// </summary>
        private AnswerDao answerDao = new AnswerDao();
        /// <summary>
        /// Data access object of Listening Topic
        /// </summary>
        private ListeningTopicDao listeningTopicDao = new ListeningTopicDao();
        /// <summary>
        /// Data access object of comprehension paragraph
        /// </summary>
        private ComprehensionParagraphDao paragraphDao = new ComprehensionParagraphDao();
        /// <summary>
        /// Data access object of ExamQuestion_Section_Question
        /// </summary>
        private ExamQuestionSectionQuestionDAO esqDao = new ExamQuestionSectionQuestionDAO();

        #endregion

        /// <summary>
        /// Action: Index (Question list)
        /// </summary>
        /// <returns></returns>
        [CrmAuthorizeAttribute(Module = Modules.Question, Rights = Permissions.Read)]
        public ActionResult Index()
        {
            Hashtable hashData = Session[SessionKey.QUESTION_DEFAULT_VALUE] == null ? new Hashtable() : (Hashtable)Session[SessionKey.QUESTION_DEFAULT_VALUE];

            ViewData[Constants.QUESTION_TEXT] = hashData[Constants.QUESTION_TEXT] == null ? Constants.QUESTIONNAME : !string.IsNullOrEmpty((string)hashData[Constants.QUESTION_TEXT]) ? hashData[Constants.QUESTION_TEXT] : Constants.QUESTIONNAME;
            ViewData[Constants.QUESTION_SECTION] = new SelectList(sectionDao.GetListAll(), "ID", "SectionName", hashData[Constants.QUESTION_SECTION] == null ? Constants.FIRST_ITEM_SECTION : hashData[Constants.QUESTION_SECTION]);

            ViewData[Constants.QUESTION_COLUMN] = hashData[Constants.QUESTION_COLUMN] == null ? "QuestionContent" : hashData[Constants.QUESTION_COLUMN];
            ViewData[Constants.QUESTION_ORDER] = hashData[Constants.QUESTION_ORDER] == null ? "asc" : hashData[Constants.QUESTION_ORDER];
            ViewData[Constants.QUESTION_PAGE_INDEX] = hashData[Constants.QUESTION_PAGE_INDEX] == null ? "1" : hashData[Constants.QUESTION_PAGE_INDEX].ToString();
            ViewData[Constants.QUESTION_ROW_COUNT] = hashData[Constants.QUESTION_ROW_COUNT] == null ? "20" : hashData[Constants.QUESTION_ROW_COUNT].ToString();
            
            return View();
        }

        public ActionResult Refresh()
        {
            Session.Remove(SessionKey.QUESTION_DEFAULT_VALUE);
            return RedirectToAction("Index");
        }

        /// <summary>
        /// Action: Details (View details of question)
        /// </summary>
        /// <param name="id"></param>
        /// <returns>ActionResult</returns>
        [CrmAuthorizeAttribute(Module = Modules.Question, Rights = Permissions.Read, ShowInPopup = true)]
        public ActionResult Details(string id)
        {
            //Get information of question
            LOT_Question question = questionDao.GetById(int.Parse(id));
            //Get all answers of the question
            List<LOT_Answer> answers = answerDao.GetListByQuestionID(int.Parse(id));
            ViewData[CommonDataKey.QUESTION] = question;
            ViewData[CommonDataKey.ANSWERS_LIST] = answers;
            return View();
        }

        /// <summary>
        /// Action: TopicDetails (View details of listening topic)
        /// </summary>
        /// <param name="id"></param>
        /// <returns>ActionResult</returns>
        [CrmAuthorizeAttribute(Module = Modules.Question, Rights = Permissions.Read, ShowInPopup = true)]
        public ActionResult TopicDetails(string id, string sectionID)
        {
            List<LOT_Question> arrQuestions = new List<LOT_Question>(0);
            object viewModel = null;
            if (int.Parse(sectionID) == Constants.LOT_LISTENING_TOPIC_ID)
            {
                //Get information of listening topic
                LOT_ListeningTopic topic = listeningTopicDao.GetByID(int.Parse(id));
                //Get all question of the listening topic
                arrQuestions = questionDao.GetListByListeningTopicID(int.Parse(id));
                //ViewData[CommonDataKey.TOPIC] = topic;
                viewModel = topic;
            }
            else if (int.Parse(sectionID) == Constants.LOT_COMPREHENSION_SKILL_ID)
            {
                //Get information of comprehension paragraph
                LOT_ComprehensionParagraph paragraph = paragraphDao.GetByID(int.Parse(id));
                //Get all question of the comprehension paragraph
                arrQuestions = questionDao.GetList().Where(p => p.ParagraphID == int.Parse(id)).ToList();
                //ViewData[CommonDataKey.TOPIC] = topic;
                viewModel = paragraph;
            }
            ViewData[CommonDataKey.QUESTION_ARR] = arrQuestions;
            return View(viewModel);
        }

        /// <summary>
        /// Action: AssignQuestion (View a list of question to be assigned to listening topic)
        /// </summary>
        /// <param name="ids"></param>
        /// <returns>ActionResult</returns>
        [CrmAuthorizeAttribute(Module = Modules.Question, Rights = Permissions.Assign, ShowInPopup = true)]
        public ActionResult AssignQuestion(string ids)
        {
            ViewData[CommonDataKey.QUESTION_ID_ARR] = ids;
            return View();
        }

        /// <summary>
        /// Action: GetListJQGrid_Assign (Get list of question to show in the assign question popup window)
        /// </summary>
        /// <param name="questionName"></param>
        /// <param name="topicID"></param>
        /// <param name="ids"></param>
        /// <returns>ActionResult</returns>
        [ValidateInput(false)]
        [CrmAuthorizeAttribute(Module = Modules.Question, Rights = Permissions.Assign, ShowInPopup = true)]
        public ActionResult GetListJQGrid_Assign(string questionName, string sectionID, string id, string ids)
        {
            #region JQGrid Params
            string sortColumn = (Request.Params[GridConstants.SORT_COLUMN]).ToString();
            string sortOrder = (Request.Params[GridConstants.SORT_ORDER]).ToString();
            int pageIndex = Convert.ToInt32(Request.Params[GridConstants.PAGE_INDEX]);
            int rowCount = Convert.ToInt32(Request.Params[GridConstants.ROW_COUNT]);
            #endregion
            string[] arrID = !string.IsNullOrEmpty(ids) ? ids.Split(',') : new string[0];
            if (string.IsNullOrEmpty(id))
            {
                id = "0";
            }
            List<LOT_Question> questionList = new List<LOT_Question>();
            if (int.Parse(sectionID) == Constants.LOT_LISTENING_TOPIC_ID)
            {
                //Get all unassigned listening skill questions except the question has id in arrID
                questionList = questionDao.GetListBySectionID(Constants.LOT_LISTENING_QUESTION).
                        Where(p => (!arrID.Contains(p.ID.ToString())
                        && (p.ListeningTopicID == null || p.ListeningTopicID == int.Parse(id))
                        )).ToList();
            }
            else if (int.Parse(sectionID) == Constants.LOT_COMPREHENSION_SKILL_ID)
            {
                //Get all unassigned comprehension skill questions except the question has id in arrID
                questionList = questionDao.GetListBySectionID(Constants.LOT_COMPREHENSION_QUESTION_ID).
                        Where(p => (!arrID.Contains(p.ID.ToString())
                        && (p.ParagraphID == null || p.ParagraphID == int.Parse(id))
                        )).ToList();
            }
            if (!string.IsNullOrEmpty(questionName))
            {
                //Filter list by question name
                questionList = questionList.Where(p => p.QuestionContent.Trim().ToLower()
                    .Contains(questionName.Trim().ToLower())).ToList<LOT_Question>();
            }
            int totalRecords = questionList.Count();
            int totalPages = (int)Math.Ceiling((float)totalRecords / (float)rowCount);
            var finalList = questionDao.Sort(questionList, sortColumn, sortOrder)
                                  .Skip((pageIndex - 1) * rowCount)
                                   .Take(rowCount);
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
                            CommonFunc.SubStringRoundWord(m.QuestionContent,
                                Constants.QUESTION_LENGTH_TO_TRUNCATE)
                        }
                    }
                ).ToArray()
            };
            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Action: UploadSoundFile (Upload a sound file to listening topic)
        /// </summary>
        /// <returns>ActionResult</returns>
        [CrmAuthorizeAttribute(Module = Modules.Question, Rights = Permissions.Insert, ShowInPopup = true)]
        public ActionResult UploadSoundFile()
        {
            return View();
        }

        /// <summary>
        /// Action: DeleteSoundFile (Delete sound file from temp folder)
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns>ActionResult</returns>
        [CrmAuthorizeAttribute(Module = Modules.Question, Rights = Permissions.Delete, ShowAtCurrentPage = true)]
        public bool DeleteSoundFile(string fileName)
        {
            bool result = false;
            string srcFile = Server.MapPath("~/" + Constants.UPLOAD_TEMP_PATH + fileName.Split('/').Last());
            if (System.IO.File.Exists(srcFile))
            {
                System.IO.File.Delete(srcFile);
                result = true;
            }
            return result;
        }

        /// <summary>
        /// Action: UploadSoundFile (Upload sound file to temp folder after user submit the form)
        /// </summary>
        /// <param name="form"></param>
        /// <returns>FileUploadJsonResult</returns>
        [ValidateInput(false)]
        [HttpPost]
        [CrmAuthorizeAttribute(Module = Modules.Question, Rights = Permissions.Insert, ShowAtCurrentPage = true)]
        public FileUploadJsonResult UploadSoundFile(FormCollection form)
        {
            int max_size = Constants.SOUND_FILE_MAX_SIZE;
            FileUploadJsonResult result = new FileUploadJsonResult();
            Message msg = null;
            foreach (string file in Request.Files)
            {
                string uniqueId = DateTime.Now.ToString(Constants.UNIQUEID_STRING_FORMAT);
                HttpPostedFileBase hpf = Request.Files[file] as HttpPostedFileBase;
                if (hpf.ContentLength != 0)
                {
                    int maxSize = 1024 * 1024 * Constants.SOUND_FILE_MAX_SIZE;
                    float sizeFile = float.Parse(hpf.ContentLength.ToString("N"));
                    // remove "." character
                    string extension = Path.GetExtension(hpf.FileName).Substring(1);
                    string[] extAllowList = Constants.SOUND_FILE_EXT_ALLOW.Split(',');
                    string fileName = uniqueId + "_" + Path.GetFileName(hpf.FileName);
                    fileName = ConvertUtil.FormatFileName(fileName);
                    //check extension file is valid
                    if (!extAllowList.Contains(extension.ToLower()) &&
                        !extAllowList.Contains(" " + extension.ToLower()))
                    {
                        msg = new Message(MessageConstants.E0013, MessageType.Error, extension,
                            Constants.SOUND_FILE_EXT_ALLOW, Constants.SOUND_FILE_MAX_SIZE);
                        result.Data = new { msg };
                    }
                    //check file size
                    else if (maxSize < sizeFile)
                    {
                        msg = new Message(MessageConstants.E0012, MessageType.Error,
                            Constants.SOUND_FILE_MAX_SIZE.ToString());
                        result.Data = new { msg };
                    }
                    //Check if filename is too long
                    else if (Constants.FILE_NAME_MAX_LENGTH < fileName.Length)
                    {
                        msg = new Message(MessageConstants.E0029, MessageType.Error,
                            "File name (including file extension)",
                            Constants.FILE_NAME_MAX_LENGTH - Constants.UNIQUEID_STRING_FORMAT.Length - 1
                            + " characters.");
                        result.Data = new { msg };
                    }
                    //successful case
                    else
                    {
                        string savePath = Server.MapPath(Constants.UPLOAD_TEMP_PATH);
                        hpf.SaveAs(savePath + fileName);
                        msg = new Message(MessageConstants.I0001, MessageType.Info, fileName, "uploaded");
                        result.Data = new { msg };
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

        /// <summary>
        /// Action: Create (Show the "Add New Question" page)
        /// </summary>
        /// <returns>ActionResult</returns>
        [CrmAuthorizeAttribute(Module = Modules.Question, Rights = Permissions.Insert, ShowAtCurrentPage = true)]
        public ActionResult Create()
        {
            ViewData[CommonDataKey.SECTION_NAME] = new SelectList(sectionDao.GetListAll().
                Where(p=>p.ID != Constants.LOT_VERBAL_SKILL_ID), "ID", "SectionName");
            List<SelectListItem> listRepeatTimes = new List<SelectListItem>();
            
            for (int i = Constants.REPEAT_TIMES_MIN; i <= Constants.REPEAT_TIMES_MAX; i++)
            {
                SelectListItem newItem = new SelectListItem();
                newItem.Text = "" + i;
                newItem.Value = "" + i;
                listRepeatTimes.Add(newItem);
            }
            ViewData[CommonDataKey.REPEAT_TIMES] = new SelectList(listRepeatTimes, "Value", "Text", Constants.REPEAT_TIMES_DEFAULT);
            List<LOT_ProgrammingSkillType> listProgrammingSkillType = questionDao.GetListProgrammingType();
            ViewData[CommonDataKey.PROGRAMMING_SKILL_TYPE] = new SelectList(listProgrammingSkillType, "ID", "Name");
            string msg = string.Format(Constants.DIV_MESSAGE_FORMAT, "msgSuccess", "none", "");
            TempData[CommonDataKey.TEMP_SYSTEM_MESSAGE] = msg;
            return View();
        }

        public ActionResult Test()
        {
            return View();
        }

        /// <summary>
        /// Action: Create (Create a new question/topic)
        /// </summary>
        /// <param name="collection"></param>
        /// <returns>ActionResult</returns>
        [ValidateInput(false)]
        [HttpPost]
        [CrmAuthorizeAttribute(Module = Modules.Question, Rights = Permissions.Insert, ShowAtCurrentPage = true)]
        public ActionResult Create(FormCollection collection)
        {
            Message msg = null;
            try
            {
                var principal = HttpContext.User as AuthenticationProjectPrincipal;
                LOT_Question question = new LOT_Question();
                int sectionID = int.Parse(collection.GetValues("SectionName")[0]);
                LOT_Section section = sectionDao.GetByID(sectionID);
                if (section.ID == Constants.LOT_WRITING_SKILL_ID || section.ID == Constants.LOT_PROGRAMMING_SKILL_ID)
                {
                    //Set information for question to be added
                    question.SectionID = sectionID;
                    if (question.SectionID == Constants.LOT_PROGRAMMING_SKILL_ID)
                    {
                        question.QuestionContent = collection.GetValues("QuestionContentProgramming")[0];
                        question.ProgrammingTypeID = ConvertUtil.ConvertToInt(collection[CommonDataKey.PROGRAMMING_SKILL_TYPE]);
                    }
                    else
                    {
                        question.QuestionContent = CommonFunc.RemoveHtmlTags(collection.GetValues("QuestionContent")[0]);
                    }

                    //if (section.ID == Constants.LOT_WRITING_SKILL_ID)
                    //    question.QuestionContent = CommonFunc.RemoveHtmlTags(collection.GetValues("QuestionContent")[0]);
                    //else
                    //    question.QuestionContent = collection.GetValues("QuestionContent")[0];

                    question.UpdatedBy = principal.UserData.UserName;
                    question.CreatedBy = principal.UserData.UserName;
                    //Insert new writing skill question
                    msg = questionDao.Insert(question, null);
                }
                else if (section.ID == Constants.LOT_LISTENING_TOPIC_ID)
                {
                    LOT_ListeningTopic listeningTopic = new LOT_ListeningTopic();
                    //Set information for listening topic to be added
                    listeningTopic.CreatedBy = principal.UserData.UserName;
                    listeningTopic.UpdatedBy = principal.UserData.UserName;
                    listeningTopic.FileName = collection.GetValues("fullFileName")[0].Split('/').Last();
                    listeningTopic.RepeatTimes = int.Parse(collection.GetValues("RepeatTimes")[0]);
                    listeningTopic.TopicName = CommonFunc.RemoveHtmlTags(collection.GetValues("TopicName")[0]);
                    listeningTopic.DeleteFlag = false;
                    //Insert new listening topic
                    msg = listeningTopicDao.Insert(listeningTopic, collection.GetValues("hidListQuestionID"));
                }
                else if (section.ID == Constants.LOT_COMPREHENSION_SKILL_ID)
                {
                    LOT_ComprehensionParagraph paragraph = new LOT_ComprehensionParagraph();
                    //Set information for comprehension paragraph to be added
                    paragraph.CreatedBy = principal.UserData.UserName;
                    paragraph.UpdatedBy = principal.UserData.UserName;
                    paragraph.DeleteFlag = false;
                    paragraph.ParagraphContent = CommonFunc.RemoveHtmlTags(collection.GetValues("ParagraphContent")[0]);
                    //insert new Comprehension paragraph
                    msg = paragraphDao.Insert(paragraph, collection.GetValues("hidListQuestionID"));
                }
                else//Normal question type: Multiple Choice, Sentence Correction...
                {
                    //Set information for question
                    question.SectionID = sectionID;
                    question.QuestionContent = CommonFunc.RemoveHtmlTags(collection.GetValues("QuestionContent")[0]);
                    question.UpdatedBy = principal.UserData.UserName;
                    question.CreatedBy = principal.UserData.UserName;
                    //Get the answer list of question
                    List<LOT_Answer> arrAnswer = new List<LOT_Answer>();
                    int correctAnswerIndex = 0;
                    correctAnswerIndex = int.Parse(collection.GetValues("hidCorectAnswerRowIndex")[0]) - 1;
                    for (int i = 0; i < collection.GetValues("txtAnswer").Length; i++)
                    {
                        LOT_Answer answer = new LOT_Answer();
                        answer.AnswerContent = collection.GetValues("txtAnswer")[i];
                        answer.AnswerOrder = i;
                        answer.IsCorrect = (i == correctAnswerIndex ? true : false);
                        arrAnswer.Add(answer);
                    }
                    //Insert new normal question with its answers
                    msg = questionDao.Insert(question, arrAnswer);
                }
            }
            catch
            {
                msg = new Message(MessageConstants.E0007, MessageType.Error);
            }
            ShowMessage(msg);
            return RedirectToAction("Index");
        }
        /// <summary>
        /// Get section list
        /// </summary>
        /// <param name="sectionID"></param>
        /// <returns></returns>
        [NonAction]
        public SelectList GetSectionList(int sectionID)
        {
            SelectList result = null;
            if (sectionID == Constants.LOT_COMPREHENSION_SKILL_ID
                || sectionID == Constants.LOT_LISTENING_TOPIC_ID
                || sectionID == Constants.LOT_WRITING_SKILL_ID
                || sectionID == Constants.LOT_PROGRAMMING_SKILL_ID)
            {
                //Get only the section of sectionID
                result = new SelectList(sectionDao.GetListAll()
                            .Where(p => p.ID == sectionID), "ID", "SectionName", sectionID);
            }
            else
            {
                //Get all section except Writing skill, listening topic, verbal skill and comprehension paragraph
                result = new SelectList(sectionDao.GetListAll()
                            .Where(p => p.ID != Constants.LOT_LISTENING_TOPIC_ID
                                && p.ID != Constants.LOT_WRITING_SKILL_ID
                                && p.ID != Constants.LOT_COMPREHENSION_SKILL_ID
                                && p.ID != Constants.LOT_PROGRAMMING_SKILL_ID
                                && p.ID != Constants.LOT_VERBAL_SKILL_ID)
                                , "ID", "SectionName", sectionID);
            }
            return result;
        }
        /// <summary>
        /// Get repeat times list
        /// </summary>
        /// <param name="repeatTimes"></param>
        /// <returns></returns>
        [NonAction]
        public SelectList GetRepeatTimesList(int repeatTimes)
        {
            List<SelectListItem> listRepeatTimes = new List<SelectListItem>();
            for (int i = Constants.REPEAT_TIMES_MIN; i <= Constants.REPEAT_TIMES_MAX; i++)
            {
                SelectListItem newItem = new SelectListItem();
                newItem.Text = "" + i;
                newItem.Value = "" + i;
                listRepeatTimes.Add(newItem);
            }
            return new SelectList(listRepeatTimes, "Value", "Text", repeatTimes);
        }
        /// <summary>
        /// Action: Edit (Show edit page)
        /// </summary>
        /// <param name="id"></param>
        /// <returns>ActionResult</returns>
        [CrmAuthorizeAttribute(Module = Modules.Question, Rights = Permissions.Update, ShowAtCurrentPage = true)]
        public ActionResult Edit(string sectionID, string id)
        {
            string msg = "";
            try
            {
                ViewData[CommonDataKey.IS_IN_ANY_EXAM] = 0;
                ViewData[CommonDataKey.IS_ASSIGNED] = 0;
                Object viewModel = null;
                //If system message does not exist, create it
                if (!TempData.Keys.Contains(CommonDataKey.TEMP_SYSTEM_MESSAGE)
                    || string.IsNullOrEmpty(TempData[CommonDataKey.TEMP_SYSTEM_MESSAGE].ToString()))
                {
                    msg = string.Format(Constants.DIV_MESSAGE_FORMAT, "msgSuccess", "none", "");
                    TempData[CommonDataKey.TEMP_SYSTEM_MESSAGE] = msg;
                }
                //resquest is comprehension paragraph
                if (int.Parse(sectionID) == Constants.LOT_COMPREHENSION_SKILL_ID)
                {
                    LOT_ComprehensionParagraph paragraph = paragraphDao.GetByID(int.Parse(id));
                    if (paragraphDao.IsInAnyExam(int.Parse(id)))
                    {
                        ViewData[CommonDataKey.IS_IN_ANY_EXAM] = 1;
                    }
                    List<LOT_Question> questionList = questionDao.GetList()
                        .Where(p => p.ParagraphID == int.Parse(id)).ToList();
                    ViewData[CommonDataKey.QUESTION_ARR] = questionList;
                    viewModel = paragraph;
                    ViewData[CommonDataKey.QUESTION_CONTENT] = paragraph.ParagraphContent;
                }
                //request is listening topic
                else if (int.Parse(sectionID) == Constants.LOT_LISTENING_TOPIC_ID)
                {
                    LOT_ListeningTopic listeningTopic = listeningTopicDao.GetByID(int.Parse(id));
                    if (listeningTopicDao.IsInAnyExam(int.Parse(id)))
                    {
                        ViewData[CommonDataKey.IS_IN_ANY_EXAM] = 1;
                    }
                    ViewData[CommonDataKey.REPEAT_TIMES] = GetRepeatTimesList(listeningTopic.RepeatTimes);
                    ViewData[CommonDataKey.QUESTION_ARR] = questionDao.GetListByListeningTopicID(int.Parse(id));
                    viewModel = listeningTopic;
                }
                //request is question
                else
                {
                    LOT_Question question = questionDao.GetById(int.Parse(id));
                    if (questionDao.IsInAnyExam(int.Parse(id)))
                    {
                        ViewData[CommonDataKey.IS_IN_ANY_EXAM] = 1;
                    }
                    else if (questionDao.IsAssigned(int.Parse(id)))
                    {
                        ViewData[CommonDataKey.IS_ASSIGNED] = 1;
                    }
                    ViewData[CommonDataKey.ANSWER_ARR] = answerDao.GetListByQuestionID(int.Parse(id));
                    viewModel = question;

                    if (question.SectionID == Constants.LOT_PROGRAMMING_SKILL_ID)
                    {
                        ViewData[CommonDataKey.QUESTION_CONTENT_PROGRAMMING] = question.QuestionContent;
                        List<LOT_ProgrammingSkillType> listProgrammingSkillType = questionDao.GetListProgrammingType();
                        ViewData[CommonDataKey.PROGRAMMING_SKILL_TYPE] = new SelectList(listProgrammingSkillType, "ID", "Name", question.ProgrammingTypeID);
                    }
                    else
                    {
                        ViewData[CommonDataKey.QUESTION_CONTENT] = question.QuestionContent;
                    }
                }
                
                ViewData[CommonDataKey.SECTION_NAME] = GetSectionList(int.Parse(sectionID));
                return View(viewModel);
            }
            catch (Exception ex)
            {
                msg = string.Format(Constants.DIV_MESSAGE_FORMAT, "msgError", "block", ex.Message);
                TempData[CommonDataKey.TEMP_SYSTEM_MESSAGE] = msg;
                return View();
            }
        }

        /// <summary>
        /// Action: Edit (Save changes of question/topic to database)
        /// </summary>
        /// <param name="collection"></param>
        /// <returns>ActionResult</returns>
        [ValidateInput(false)]
        [HttpPost]
        [CrmAuthorizeAttribute(Module = Modules.Question, Rights = Permissions.Update, ShowAtCurrentPage = true)]
        public ActionResult Edit(FormCollection collection)
        {
            Message msg = null;
            string actionToRedirect = "index";
            var actionParam = new RouteValueDictionary();
            try
            {
                var principal = HttpContext.User as AuthenticationProjectPrincipal;
                //Get the updatedate before calling Edit action
                string updateDate = collection.GetValues("UpdateDate")[0].Trim();
                int sectionID = int.Parse(collection.GetValues("SectionName")[0]);
                //Update listening topic
                if (sectionID == Constants.LOT_LISTENING_TOPIC_ID)
                {
                    int topicID = int.Parse(collection.GetValues("ID")[0]);
                    //Get the last updatedate
                    string updateDateDb = listeningTopicDao.GetByID(topicID).UpdateDate.ToString().Trim();
                    LOT_ListeningTopic topicUI = new LOT_ListeningTopic();
                    //Check if any user has updated the listening topic before calling Edit action
                    if (updateDate.Equals(updateDateDb))
                    {
                        topicUI.ID = topicID;
                        topicUI.UpdatedBy = principal.UserData.UserName;
                        topicUI.FileName = collection.GetValues("fullFileName")[0].Split('/').Last();
                        topicUI.RepeatTimes = int.Parse(collection.GetValues("RepeatTimes")[0]);
                        topicUI.TopicName = CommonFunc.RemoveHtmlTags(collection.GetValues("TopicName")[0]);
                        msg = listeningTopicDao.Update(topicUI, collection.GetValues("hidListQuestionID"));
                    }
                    //if other user has updated this topic before calling Edit action
                    //Show Error message
                    else
                    {
                        msg = new Message(MessageConstants.E0025, MessageType.Error, "This listening topic");
                        actionToRedirect = "Edit";
                        actionParam.Add("id", topicID);
                        actionParam.Add("sectionID", sectionID);
                        //ShowMessage(msg);
                        //return RedirectToAction("Edit", new { id = Constants.PREFIX_TOPIC_ID + topicID });
                    }
                }
                //Update comprehension paragraph
                else if (sectionID == Constants.LOT_COMPREHENSION_SKILL_ID)
                {
                    int paragraphID = int.Parse(collection.GetValues("ID")[0]);
                    //Get the last updatedate
                    string updateDateDb = paragraphDao.GetByID(paragraphID).UpdateDate.ToString().Trim();
                    LOT_ComprehensionParagraph paragraphUI = new LOT_ComprehensionParagraph();
                    //Check if any user has updated the paragraph before calling Edit action
                    if (updateDate.Equals(updateDateDb))
                    {
                        paragraphUI.ID = paragraphID;
                        paragraphUI.UpdatedBy = principal.UserData.UserName;
                        paragraphUI.ParagraphContent = CommonFunc.RemoveHtmlTags(collection.GetValues("ParagraphContent")[0]);
                        msg = paragraphDao.Update(paragraphUI, collection.GetValues("hidListQuestionID"));
                    }
                    //if other user has updated this paragraph before calling Edit action
                    //Show Error message
                    else
                    {
                        msg = new Message(MessageConstants.E0025, MessageType.Error, "This comprehension paragraph");
                        actionToRedirect = "Edit";
                        actionParam.Add("id", paragraphID);
                        actionParam.Add("sectionID", sectionID);
                        //ShowMessage(msg);
                        //return RedirectToAction("Edit", new { id = Constants.PREFIX_PARAGRAPH_ID + paragraphID });
                    }
                }
                //Update question
                else
                {
                    LOT_Question question = questionDao.GetById(int.Parse(collection.GetValues("ID")[0]));
                    //Check if any user has updated the question before calling Edit action
                    bool isValidUpdate = updateDate.Equals(question.UpdateDate.ToString().Trim());
                    //Update is valid and the question is a writing skill question
                    if ((isValidUpdate && question.SectionID == Constants.LOT_WRITING_SKILL_ID) ||
                        (isValidUpdate && question.SectionID == Constants.LOT_PROGRAMMING_SKILL_ID))
                    {
                        if (isValidUpdate && question.SectionID == Constants.LOT_PROGRAMMING_SKILL_ID)
                        {
                            question.QuestionContent = collection.GetValues("QuestionContentProgramming")[0];
                            question.ProgrammingTypeID = ConvertUtil.ConvertToInt(collection[CommonDataKey.PROGRAMMING_SKILL_TYPE]);
                        }
                        else
                        {
                            question.QuestionContent = CommonFunc.RemoveHtmlTags(collection.GetValues("QuestionContent")[0]);
                        }
                        question.UpdatedBy = principal.UserData.UserName;
                        msg = questionDao.Update(question, null);
                    }
                    //The question is a normal question
                    else if (isValidUpdate && question.SectionID != Constants.LOT_WRITING_SKILL_ID)
                    {
                        question.SectionID = int.Parse(collection.GetValues("SectionName")[0]);
                        question.QuestionContent = CommonFunc.RemoveHtmlTags(collection.GetValues("QuestionContent")[0]);
                        question.UpdatedBy = principal.UserData.UserName;
                        //Get all answers and save it to a list
                        List<LOT_Answer> arrAnswer = new List<LOT_Answer>();
                        int correctAnswerIndex = 0;
                        correctAnswerIndex = int.Parse(collection.GetValues("hidCorectAnswerRowIndex")[0]) - 1;
                        for (int i = 0; i < collection.GetValues("txtAnswer").Length; i++)
                        {
                            LOT_Answer answer = new LOT_Answer();
                            answer.ID = int.Parse(collection.GetValues("hidAnswerID")[i]);
                            answer.QuestionID = question.ID;
                            answer.AnswerContent = collection.GetValues("txtAnswer")[i];
                            answer.AnswerOrder = i;
                            answer.IsCorrect = (i == correctAnswerIndex ? true : false);
                            arrAnswer.Add(answer);
                        }
                        //Update question with new answers
                        msg = questionDao.Update(question, arrAnswer);
                    }
                    //if other user has updated this question before doing this action
                    else
                    {
                        msg = new Message(MessageConstants.E0025, MessageType.Error, "This question");
                        actionToRedirect = "Edit";
                        actionParam.Add("id", question.ID);
                        actionParam.Add("sectionID", sectionID);
                    }
                }
            }
            catch
            {
                msg = new Message(MessageConstants.E0007, MessageType.Error);
            }
            ShowMessage(msg);
            return RedirectToAction(actionToRedirect, actionParam);
        }
        /// <summary>
        /// Get the id of the question with a prefix character: p,q,t
        /// </summary>
        /// <param name="question"></param>
        /// <returns></returns>
        [NonAction]
        public string GetIDWithPrefix(LOT_Question question)
        {
            string prefix = "";
            if (question.SectionID == Constants.LOT_LISTENING_TOPIC_ID)
            {
                prefix = Constants.PREFIX_TOPIC_ID;
            }
            else if (question.SectionID == Constants.LOT_COMPREHENSION_SKILL_ID)
            {
                prefix = Constants.PREFIX_PARAGRAPH_ID;
            }
            else
            {
                prefix = Constants.PREFIX_QUESTION_ID;
            }
            return prefix + question.ID;
        }

        /// <summary>
        /// Action: GetListJQGrid (Get list of questions/topics to show on jqgrid at index page)
        /// </summary>
        /// <param name="name"></param>
        /// <param name="sectionID"></param>
        /// <returns>ActionResult</returns>
        [ValidateInput(false)]
        [CrmAuthorizeAttribute(Module = Modules.Question, Rights = Permissions.Read)]
        public ActionResult GetListJQGrid(string name, string sectionID)
        {
            #region JQGrid Params
            string sortColumn = (Request.Params[GridConstants.SORT_COLUMN]).ToString();
            string sortOrder = (Request.Params[GridConstants.SORT_ORDER]).ToString();
            int pageIndex = Convert.ToInt32(Request.Params[GridConstants.PAGE_INDEX]);
            int rowCount = Convert.ToInt32(Request.Params[GridConstants.ROW_COUNT]);
            #endregion

            SetSessionFilter(name, sectionID, sortColumn, sortOrder, pageIndex, rowCount);

            if (name.Trim().ToLower().Equals(Constants.QUESTIONNAME.ToLower()))
            {
                name = string.Empty;
            }
            

            //Get list of questions, listening topic, comprehension paragraph
            List<LOT_Question> questionList = questionDao.GetListMergeWithTopicAndParagraph(name, sectionID);
            int totalRecords = questionList.Count;
            int totalPages = (int)Math.Ceiling((float)totalRecords / (float)rowCount);
            //Sort the list
            var finalList = questionDao.Sort(questionList, sortColumn, sortOrder)
                                  .Skip((pageIndex - 1) * rowCount)
                                   .Take(rowCount).ToDictionary(p => GetIDWithPrefix(p));
            var jsonData = new
            {
                total = totalPages,
                page = pageIndex,
                records = totalRecords,
                rows = (
                    from m in finalList
                    select new
                    {
                        i = m.Key,
                        cell = new string[] {
                            m.Key,
                            (m.Value.SectionID == Constants.LOT_PROGRAMMING_SKILL_ID ? "<pre>" + 
                                CommonFunc.SubStringRoundWordNotMultiline(m.Value.QuestionContent,Constants.QUESTION_LENGTH_TO_TRUNCATE) + 
                                "</pre>": CommonFunc.SubStringRoundWord(m.Value.QuestionContent,Constants.QUESTION_LENGTH_TO_TRUNCATE)), 
                            m.Value.LOT_Section.SectionName,
                            "<input type=\"button\" class=\"icon edit\" title=\"Edit\" "
                            +"onclick=\"window.location='/Question/Edit?id="
                            + m.Value.ID + "&sectionID=" + m.Value.SectionID + "'\" />"
                            + CommonFunc.GetDetailsViewLink(m.Value.ID, m.Value.SectionID)
                        }
                    }
                ).ToArray()
            };
            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Action: GetListJQGrid_ListeningTopic (Get list of Questions to show in Edit page)
        /// </summary>
        /// <param name="id"></param>
        /// <returns>ActionResult</returns>
        [ValidateInput(false)]
        [CrmAuthorizeAttribute(Module = Modules.Question, Rights = Permissions.Insert, ShowAtCurrentPage = true)]
        public ActionResult GetListJQGrid_Edit(string sectionID, string id)
        {
            #region JQGrid Params
            string sortColumn = (Request.Params[GridConstants.SORT_COLUMN]).ToString();
            string sortOrder = (Request.Params[GridConstants.SORT_ORDER]).ToString();
            #endregion

            List<LOT_Question> questionList = new List<LOT_Question>(0);
            if (int.Parse(sectionID) == Constants.LOT_LISTENING_TOPIC_ID)
            {
                //Get all questions of the listening topic
                questionList = questionDao.GetListByListeningTopicID(int.Parse(id));
            }
            else if (int.Parse(sectionID) == Constants.LOT_COMPREHENSION_SKILL_ID)
            {
                //Get all questions of the comprehension skill
                questionList = questionDao.GetList().Where(p => p.ParagraphID == int.Parse(id)).ToList();
            }
            var finalList = questionDao.Sort(questionList, sortColumn, sortOrder);
            var jsonData = new
            {
                rows = (
                    from m in finalList
                    select new
                    {
                        i = m.ID,
                        cell = new string[] {
                            m.ID.ToString()+"<input type='hidden' name='hidListQuestionID' value='" + m.ID + "'/>",
                            finalList.IndexOf(m).ToString(),
                            CommonFunc.SubStringRoundWord(
                                m.QuestionContent,Constants.QUESTION_LENGTH_TO_TRUNCATE), 
                            CommonFunc.GetDetailsViewLink(m.ID, m.SectionID)
                        }
                    }
                ).ToArray()
            };
            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Action: Delete List (Delete a list of questions)
        /// </summary>
        /// <param name="id"></param>
        /// <returns>ActionResult</returns>
        [HttpPost]
        [CrmAuthorizeAttribute(Module = Modules.Question, Rights = Permissions.Delete, ShowAtCurrentPage = true)]
        public ActionResult DeleteList(string id)
        {
            Message msg = null;
            try
            {
                var principal = HttpContext.User as AuthenticationProjectPrincipal;
                msg = questionDao.DeleteList(id, principal.UserData.UserName);
            }
            catch
            {
                msg = new Message(MessageConstants.E0007, MessageType.Error);
            }
            //ShowMessage(msg);
            //return RedirectToAction("Index");
            JsonResult result = new JsonResult();
            return Json(msg);

        }

        /// <summary>
        /// Check listening topic sound file exist or not?
        /// </summary>
        /// <returns></returns>
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult CheckFileExist(string filename)
        {
            string file = string.Empty;
            if (System.IO.File.Exists(Server.MapPath(Constants.SOUND_FOLDER + filename)))
            {
                file = Constants.SOUND_FOLDER + filename;
            }

            var jsonData = new
            {
                file = file
            };
            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Set Session to Filter
        /// </summary>
        /// <param name="accountName"></param>
        /// <param name="groupId"></param>
        /// <param name="column"></param>
        /// <param name="order"></param>
        /// <param name="pageIndex"></param>
        /// <param name="rowCount"></param>
        private void SetSessionFilter(string content, string section,
            string column, string order, int pageIndex, int rowCount)
        {
            Hashtable hashData = new Hashtable();
            hashData.Add(Constants.QUESTION_TEXT, content);
            hashData.Add(Constants.QUESTION_SECTION, section);            
            hashData.Add(Constants.QUESTION_COLUMN, column);
            hashData.Add(Constants.QUESTION_ORDER, order);
            hashData.Add(Constants.QUESTION_PAGE_INDEX, pageIndex);
            hashData.Add(Constants.QUESTION_ROW_COUNT, rowCount);

            Session[SessionKey.QUESTION_DEFAULT_VALUE] = hashData;
        }
    }
}
