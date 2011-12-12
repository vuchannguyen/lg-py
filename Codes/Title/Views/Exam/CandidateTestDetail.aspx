<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<%@ Import Namespace= "CRM.Models" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">         
    <%= TempData["Message"]%> 
    <%                                 
        ExamQuestionSectionDAO examQuestionSectionDao = new ExamQuestionSectionDAO();
        ExamQuestionDAO examQuestionDao = new ExamQuestionDAO();
        SectionDAO sectionDao = new SectionDAO();
        LOT_ExamQuestion examQuestion = (LOT_ExamQuestion)ViewData.Model;
        AnswerDao answerDao = new AnswerDao();
        CandidateAnswerDao candidateAnswerDao = new CandidateAnswerDao();   
        QuestionDao questionDao = new QuestionDao();
        ListeningTopicDao listeningTopicDao = new ListeningTopicDao();
        ComprehensionParagraphDao comprehensionParagraphDao = new ComprehensionParagraphDao();
        ExamDao examDao = new ExamDao();
        LOT_Candidate_Exam candidateExam = (LOT_Candidate_Exam)ViewData[CommonDataKey.CANDIDATE_EXAM]; 
        Response.Write(Html.Hidden("totalcorrectAnswer"));
        Response.Write(Html.Hidden(CommonDataKey.EXAM_ID));
        Response.Write(Html.Hidden("Exam_Candidate_ID", candidateExam.ID));
        double totalMark = 0;
        double totalMaxMark = 0;
     %>       
     <div id="cactionbutton">      
        <button id="btnExport" type="button" title="Export" class="button export">Export</button>                  
        <button id="btnShowCorrectAnswer" type="button" class="button preview" style="width:170px">Show Correct Answers</button>                    
        <button type="button" id="btnBack" title="Back" class="button back">Back</button>                      
    </div>
    <table>
        <tr>
            <td style="padding-left:5px; font-size:large; font-weight:bold; color:Red"><span id="stotalMark"></span> </td>
        </tr>
    </table>
    <table id="list" width="800px" class="form">    
    <%
        int correctAnswer = 0;        
        List<LOT_ExamQuestion_Section> examQuestionSectionList = examQuestionSectionDao.GetListByExamQuestionID(examQuestion.ID).OrderBy(c => c.SectionID).ToList<LOT_ExamQuestion_Section>();
        %>        
        <%
        int sectionIndex = 0;
        foreach (LOT_ExamQuestion_Section examQuestionSection in examQuestionSectionList)    
        { 
          %>          
            <tr>
                <td>
                    <fieldset class="form" style="width: 1008px; background-color: White !important">        
                        <legend>
                            <button type="button" class="icon minus" id="btnActive<%=sectionIndex %>" onclick = "toggleDivActive(this)">
                            </button>
                            <%=examQuestionSection.LOT_Section.SectionName%>: <%=examQuestionSection.LOT_Section.Description%>                             
                            <span style="padding-left:20px; color:Red">Mark: 
                            <% 
                                double mark = 0;
                                double maxMark = 0;
                                examDao.GetMark(candidateExam.ID, examQuestionSection.ID,ref mark,ref maxMark);
                                totalMaxMark += (examQuestionSection.SectionID == Constants.LOT_VERBAL_SKILL_ID) ? 0 : maxMark;
                                if (examQuestionSection.SectionID == Constants.LOT_VERBAL_SKILL_ID)
                                {
                                    if (candidateExam.VerbalMarkType == Constants.LOT_VERBAL_MARK_TYPE_LEVEL)
                                        Response.Write(" Level " + mark);
                                    else
                                    {
                                        if (candidateExam.VerbalMarkType != null)
                                        {
                                            var objType = new TrainingEmpEnglishInfoDao().GetTypeName(candidateExam.VerbalMarkType.Value);
                                            Response.Write(" " + objType + " " + mark);
                                        }
                                    }
                                }
                                else if (mark != Constants.WRITTING_MARK_NULL && !double.IsNaN(mark))
                                {
                                    totalMark += (examQuestionSection.SectionID == Constants.LOT_VERBAL_SKILL_ID) ? 0 : mark;
                                    Response.Write(mark.ToString() + "/" + maxMark.ToString());
                                }
                                else
                                {
                                    Response.Write("0/" + maxMark.ToString());
                                }                      
                            %>
                            </span>
                        </legend>                                 
                        <div id = "divbtnActive<%=sectionIndex %>">
                    <%                                               
                            if (examQuestionSection.SectionID == Constants.LOT_LISTENING_TOPIC_ID)
                            {
                                List<LOT_Question> questionList = candidateAnswerDao.GetCandidateExamIDAndExamQuestionSectionID(candidateExam.ID, examQuestionSection.ID).Select(c => c.LOT_Question).ToList<LOT_Question>();
                                List<LOT_ListeningTopic> listeningTopics = listeningTopicDao.GetListByQuestionList(questionList);
                                if (listeningTopics.Count > 0)
                                {
                                    int listeningIndex = 1;
                                    foreach (LOT_ListeningTopic listeningTopic in listeningTopics)
                                    { 
                                        %>
                                        <table class="form">
                                            <tr>                                                        
                                                <td>
                                                    <%
                                                        Response.Write(HttpUtility.HtmlEncode(listeningTopic.TopicName));
                                                        Response.Write(" <span id=\"btnPlay" + listeningIndex + "\" class=\"picon play\" title=\"Play\" onclick=\"play('" + listeningIndex + "','" + listeningTopic.FileName + "')\"></span>");
                                                        Response.Write(" <span id=\"btnStop" + listeningIndex + "\" style=\"display:none\" class=\"picon stop\" title=\"Stop\" onclick=\"stop('" + listeningIndex + "')\"></span>");
                                                        %>
                                                    <table class="form">
                                                    <% 
                                                        int questionIndex = 1;
                                                        List<LOT_Question> questions = questionDao.GetListByListeningTopicID(listeningTopic.ID);
                                                        foreach (LOT_Question question in questions)
                                                        {
                                                            %>
                                                                <tr>                                                                        
                                                                <td valign="top"> 
                                                                    <%=questionIndex.ToString()%>) <%=CommonFunc.Encode(question.QuestionContent) %>         
                                                                    <table>                                                               
                                                                    <%
                                                                        LOT_CandidateAnswer candidateAnswer = candidateAnswerDao.GetByCandidateExamIDAndQuestionID(candidateExam.ID, question.ID);
                                                                        List<LOT_Answer> answerList = answerDao.GetListByQuestionID(question.ID);
                                                                        int answerIndex = 1;
                                                                        foreach (LOT_Answer answer in answerList)
                                                                        {
                                                                            string canAnswer = string.Empty;
                                                                            if (candidateAnswer.AnswerID == answer.ID)
                                                                            {
                                                                                if (answer.IsCorrect)
                                                                                    canAnswer = "<img id=\"CandidateAnswer" + correctAnswer + "\" src=\"/Content/Images/Icons/tick1.png\" style=\"height:15px;display:block\"/>";
                                                                                else
                                                                                    canAnswer = "<img src=\"/Content/Images/Icons/tick1.png\"/>";
                                                                            }
                                                                            if (answer.IsCorrect)
                                                                            {
                                                                                Response.Write("<tr><td width=\"17px\" align=\"right\">" + canAnswer + "<img src=\"/Content/Images/Icons/tick.png\" id=\"CorrectAnswer" + correctAnswer + "\" style=\"height:15px;display:none\"/></td><td>&#0" + (answerIndex + 64) + ";) " + HttpUtility.HtmlEncode(answer.AnswerContent) + "</td></tr>");
                                                                                correctAnswer++;
                                                                            }
                                                                            else
                                                                            {
                                                                                Response.Write("<tr><td width=\"17px\" align=\"right\">" + canAnswer + "</td><td>&#0" + (answerIndex + 64) + ";) " + HttpUtility.HtmlEncode(answer.AnswerContent) + "</td></tr>");
                                                                            }
                                                                            answerIndex++;
                                                                        }
                                                                    %>
                                                                    <tr>
                                                                        <td colspan="2"></td>
                                                                    </tr>
                                                                    </table>
                                                                </td>
                                                                </tr>
                                                            <%
                                                                questionIndex++;
                                                        }
                                                    %>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                        <% if (listeningIndex < listeningTopics.Count)
                                            { 
                                            %>
                                                <hr size=1/>
                                            <%
                                        }%>                                                
                                        <%
                                            listeningIndex++;
                                    }
                                            Response.Write(Html.Hidden("hdNumberOfListeningTopic", listeningTopics.Count));                                            
                                }
                            }
                            else if (examQuestionSection.SectionID == Constants.LOT_COMPREHENSION_SKILL_ID)
                            {
                                List<LOT_Question> questionList = candidateAnswerDao.GetCandidateExamIDAndExamQuestionSectionID(candidateExam.ID, examQuestionSection.ID).Select(c => c.LOT_Question).ToList<LOT_Question>();
                                List<LOT_ComprehensionParagraph> paragraphs = comprehensionParagraphDao.GetListByQuestionList(questionList);
                                if (paragraphs.Count > 0)
                                {
                                    int comprehensionIndex = 1;
                                    foreach (LOT_ComprehensionParagraph paragraph in paragraphs)
                                    { 
                                        %>
                                        <table class="form">
                                            <tr>                                                        
                                                <td>
                                                    <%
                                                        Response.Write("<b>Paragraph: </b>" + CommonFunc.Encode(paragraph.ParagraphContent));                                                        
                                                        %>
                                                    <table class="form">
                                                    <% 
                                                        int questionIndex = 1;
                                                        List<LOT_Question> questions = questionDao.GetListByParagraphID(paragraph.ID);
                                                        foreach (LOT_Question question in questions)
                                                        {
                                                            %>
                                                                <tr>                                                                        
                                                                <td valign="top"> 
                                                                    <%=questionIndex.ToString()%>) <%=CommonFunc.Encode(question.QuestionContent) %>         
                                                                    <table>                                                               
                                                                    <%
                                                                        LOT_CandidateAnswer candidateAnswer = candidateAnswerDao.GetByCandidateExamIDAndQuestionID(candidateExam.ID, question.ID);
                                                                        List<LOT_Answer> answerList = answerDao.GetListByQuestionID(question.ID);
                                                                        int answerIndex = 1;
                                                                        foreach (LOT_Answer answer in answerList)
                                                                        {
                                                                            string canAnswer = string.Empty;
                                                                            if (candidateAnswer.AnswerID == answer.ID)
                                                                            {
                                                                                if (answer.IsCorrect)
                                                                                    canAnswer = "<img id=\"CandidateAnswer" + correctAnswer + "\" src=\"/Content/Images/Icons/tick1.png\" style=\"height:15px;display:block\"/>";
                                                                                else
                                                                                    canAnswer = "<img src=\"/Content/Images/Icons/tick1.png\"/>";
                                                                            }
                                                                            if (answer.IsCorrect)
                                                                            {
                                                                                Response.Write("<tr><td width=\"17px\" align=\"right\">" + canAnswer + "<img src=\"/Content/Images/Icons/tick.png\" id=\"CorrectAnswer" + correctAnswer + "\" style=\"height:15px;display:none\"/></td><td>&#0" + (answerIndex + 64) + ";) " + HttpUtility.HtmlEncode(answer.AnswerContent) + "</td></tr>");
                                                                                correctAnswer++;
                                                                            }
                                                                            else
                                                                            {
                                                                                Response.Write("<tr><td width=\"17px\" align=\"right\">" + canAnswer + "</td><td>&#0" + (answerIndex + 64) + ";) " + HttpUtility.HtmlEncode(answer.AnswerContent) + "</td></tr>");
                                                                            }
                                                                            answerIndex++;
                                                                        }
                                                                    %>
                                                                    <tr>
                                                                        <td colspan="2"></td>
                                                                    </tr>
                                                                    </table>
                                                                </td>
                                                                </tr>
                                                            <%
                                                                questionIndex++;
                                                        }
                                                    %>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                        <% if (comprehensionIndex < paragraphs.Count)
                                            { 
                                            %>
                                                <hr size=1/>
                                            <%
                                        }%>                                                
                                        <%
                                            comprehensionIndex++;
                                    }                                            
                                }
                            }
                            else if (examQuestionSection.SectionID == Constants.LOT_VERBAL_SKILL_ID)
                            {
                                Response.Write("<table class='form'><tr><td><b>Tested by: </b><i>" + candidateExam.VerbalTestedBy + "</i></td></tr>");
                                Response.Write("<tr><td><b>Comment: </b>" + HttpUtility.HtmlEncode(candidateExam.VerbalComment) + "</td></tr></table>");
                            }
                            else
                            {
                                List<LOT_CandidateAnswer> candidateAnswerList = candidateAnswerDao.GetCandidateExamIDAndExamQuestionSectionID(candidateExam.ID, examQuestionSection.ID);
                                if (candidateAnswerList.Count > 0)
                                { 
                                    %>
                                        <table class="form" width="100%">
                                            <%
                                            int questionIndex = 1;
                                            foreach (LOT_CandidateAnswer candidateAnswer in candidateAnswerList)
                                            {
                                                    %>
                                                        <tr>
                                                            <td valign="top">    
                                                                <%
                                            if (candidateAnswer.LOT_Question.SectionID == Constants.LOT_PROGRAMMING_SKILL_ID)
                                            {
                                                Response.Write("<pre>" + questionIndex.ToString() + ")" + HttpUtility.HtmlEncode(candidateAnswer.LOT_Question.QuestionContent.Trim()) + "</pre>");
                                            }
                                            else
                                            {
                                                Response.Write(questionIndex.ToString() + ")" + CommonFunc.Encode(candidateAnswer.LOT_Question.QuestionContent.Trim()));
                                            }
                                                                %>                                                                                                                                
                                                                <table>
                                                                <%                                                                         
                                            if (candidateAnswer.LOT_Question.SectionID == Constants.LOT_PROGRAMMING_SKILL_ID)
                                            {
                                                Response.Write("<tr><td width=\"17px\" align=\"right\" valign=\"top\"><b>Answer:</b></td><td><pre>" + (candidateAnswer.Essay == null ? string.Empty : HttpUtility.HtmlEncode(candidateAnswer.Essay.Trim())) + "</pre></td></tr>");
                                            }
                                            else if (candidateAnswer.LOT_Question.SectionID == Constants.LOT_WRITING_SKILL_ID)
                                            {
                                                Response.Write("<tr><td width=\"17px\" align=\"right\" valign=\"top\"><b>Essay:</b></td><td> " + (candidateAnswer.Essay == null ? string.Empty : HttpUtility.HtmlEncode(candidateAnswer.Essay.Trim())) + "</td></tr>");
                                            }
                                            else
                                            {
                                                int answerIndex = 1;
                                                List<LOT_Answer> answerList = answerDao.GetListByQuestionID(candidateAnswer.QuestionID);
                                                foreach (LOT_Answer answer in answerList)
                                                {
                                                    string canAnswer = string.Empty;
                                                    if (candidateAnswer.AnswerID == answer.ID)
                                                    {
                                                        if (answer.IsCorrect)
                                                            canAnswer = "<img id=\"CandidateAnswer" + correctAnswer + "\" src=\"/Content/Images/Icons/tick1.png\" style=\"height:15px;display:block\"/>";
                                                        else
                                                            canAnswer = "<img src=\"/Content/Images/Icons/tick1.png\"/>";
                                                    }
                                                    if (answer.IsCorrect)
                                                    {
                                                        Response.Write("<tr><td width=\"17px\" align=\"right\">" + canAnswer + "<img src=\"/Content/Images/Icons/tick.png\" id=\"CorrectAnswer" + correctAnswer + "\" style=\"height:15px;display:none\"/></td><td>&#0" + (answerIndex + 64) + ";) " + HttpUtility.HtmlEncode(answer.AnswerContent) + "</td></tr>");
                                                        correctAnswer++;
                                                    }
                                                    else
                                                    {
                                                        Response.Write("<tr><td width=\"17px\" align=\"right\">" + canAnswer + "</td><td>&#0" + (answerIndex + 64) + ";) " + HttpUtility.HtmlEncode(answer.AnswerContent) + "</td></tr>");
                                                    }
                                                    answerIndex++;
                                                }
                                            }
                                                                    %>
                                                                    <tr>
                                                                    <td colspan="2"></td>
                                                                    </tr>
                                                                    </table>  
                                                                    <% if (questionIndex < candidateAnswerList.Count)
                                                                       { 
                                                                        %>
                                                                            <hr size="1"/>
                                                                        <%
                                            }%>                                                                        
                                                            </td>                                                                 
                                                        </tr>
                                                    <%
                                            questionIndex++;
                                            }

                                            if (examQuestionSection.SectionID == Constants.LOT_WRITING_SKILL_ID)
                                            {
                                                Response.Write("<tr><td><b>Comment of teacher: </b>" + HttpUtility.HtmlEncode(candidateExam.WritingComment) + "</td></tr>");
                                            }
                                            else if (examQuestionSection.SectionID == Constants.LOT_PROGRAMMING_SKILL_ID)
                                            {
                                                Response.Write("<tr><td><b>Comment: </b>" + HttpUtility.HtmlEncode(candidateExam.ProgramingComment) + "</td></tr>");
                                            }  
                                                %>
                                        </table>                                             
                                    <%
                                            }
                            }                                                    
                    %>
                    </div>
                    </fieldset>                                  
                </td>
            </tr>                   
          <%
              sectionIndex++;          
        }
              if (!double.IsNaN(totalMark) && !double.IsNaN(totalMaxMark))
              {
                  Response.Write("<script>SetTotalMark('" + Math.Round(totalMark) + "','" + Math.Round(totalMaxMark) + "')</script>");
              }
        //Set No of total correct answer 
        Response.Write("<script>SetTotalQuestion('" + correctAnswer + "'); </script>");
     %>    
     </table>     
     <div id="div_jp_Media" style="width:0px;height:0px;"></div>
</asp:Content>


<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
    <script src="/Scripts/jquery.validate.js" type="text/javascript"></script>
    <script type="text/javascript" src="/Scripts/jquery.jplayer.min.js"></script>
    <script type="text/javascript" src="/Scripts/jquery.sound.js"></script>
    <script type="text/javascript">
        jQuery(document).ready(function () {
            $("#btnBack").click(function () {
                window.location = '<%=Request["urlback"] %>';
            });

            $("#btnExport").click(function () {
                CRM.loading();
                window.location = "/Exam/ExportResultToExcel?candidateExamID=" + $("#Exam_Candidate_ID").val(); ;                
                CRM.completed();
            });

            //constructor options for jplayer
            $('#div_jp_Media').jPlayer({
                swfPath: '/Scripts',
                solution: 'html, flash',
                supplied: 'mp3',
                preload: 'metadata',
                volume: 0.8,
                muted: false,
                errorAlerts: false,
                warningAlerts: false
            });

            $("#btnShowCorrectAnswer").click(function () {
                var total = $("#totalcorrectAnswer").val();
                for (var i = 0; i < total; i++) {
                    if ($("#CorrectAnswer" + i).css("display") == "none") {
                        $("#CorrectAnswer" + i).css("display", "block");
                        $("#CandidateAnswer" + i).css("display", "none");
                        $("#btnShowCorrectAnswer").text("Hide Correct Answers");
                    }
                    else {
                        $("#CorrectAnswer" + i).css("display", "none");
                        $("#CandidateAnswer" + i).css("display", "block");
                        $("#btnShowCorrectAnswer").text("Show Correct Answers");
                    }
                }
            });
        });

        function play(index, filename) {

            var numberOfListeningTopic = $("#hdNumberOfListeningTopic").val();
            for (var i = 1; i <= numberOfListeningTopic; i++) {
                stop(i);
            }

            $.ajax({
                async: false,
                cache: false,
                type: "POST",
                dataType: "html",
                timeout: 1000,
                url: "/Question/CheckFileExist?filename=" + filename,
                error: function () {
                    alert("System Error!");
                },
                success: function (data) {
                    result = $.parseJSON(data);
                    if (result.file == "") {
                        CRM.msgBox('<%=Constants.SOUND_FILE_NOT_EXIST_MESSAGE%>', 300);
                    }
                    else {
                        playSound("div_jp_Media", result.file);
                        $("#btnPlay" + index).css("display", "none");
                        $("#btnStop" + index).css("display", "");
                    }
                }
            });
        }

        function stop(index) {
            stopSound("div_jp_Media");
            $(this).css("display", "none");
            $("#btnPlay" + index).css("display", "");
            $("#btnStop" + index).css("display", "none");
        }

        function toggleDivActive(obj) {
            $("#div" + $(obj).attr("id")).toggleClass('show').toggleClass('hidden');
            $(obj).toggleClass('icon minus').toggleClass('icon plus');
        }

        function SetTotalQuestion(value) {
            $("#totalcorrectAnswer").val(value);
        }

        function SetTotalMark(totalMark, totalMaxMark) {
            $("#stotalMark").text('Total Mark: ' + totalMark + '/' + totalMaxMark);
        }

    </script>
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="ModuleName" runat="server">
    <%= LOTPageInfo.ComName%>
</asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="FunctionTitle" runat="server">
<%--<%= LOTPageInfo.MenuName + CommonPageInfo.AppDetailSepChar +
        "<a href='/Exam/Index'>" + LOTPageInfo.Exam + "</a>" + CommonPageInfo.AppDetailSepChar
   +"<a href='/Exam/CandidateTestList/"+ViewData[CommonDataKey.EXAM_ID]+"'>"+ ViewData[CommonDataKey.EXAM_TITLE]+"</a>"
   + CommonPageInfo.AppDetailSepChar +ViewData[CommonDataKey.CANDIDATE_NAME]
    %>--%>
    <%= CommonFunc.GetCurrentMenu(Request.RawUrl)
   +"<a href='/Exam/CandidateTestList/"+ViewData[CommonDataKey.EXAM_ID]+"'>"+ ViewData[CommonDataKey.EXAM_TITLE]+"</a>"
   + CommonPageInfo.AppDetailSepChar +ViewData[CommonDataKey.CANDIDATE_NAME]
    %>
</asp:Content>

<asp:Content ID="Content6" ContentPlaceHolderID="LoginRoles" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PageTitle" runat="server">
    <%= LOTPageInfo.MenuName + CommonPageInfo.AppSepChar +LOTPageInfo.CandidateTestList + CommonPageInfo.AppSepChar + LOTPageInfo.ComName + CommonPageInfo.AppSepChar + CommonPageInfo.AppName%>    
</asp:Content>
