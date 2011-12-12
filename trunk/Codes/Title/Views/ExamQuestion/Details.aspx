<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>
<%@ Import Namespace= "CRM.Models" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">         
    <%= TempData["Message"]%> 
    <%                 
        ExamQuestionSectionQuestionDAO examQuestionSectionQuestionDao = new ExamQuestionSectionQuestionDAO();
        ExamQuestionSectionListeningTopicDao examQuestionSectionListeningTopicDao = new ExamQuestionSectionListeningTopicDao();
        ExamQuestionSectionComprehensionDao examQuestionSectionComprehensionDao = new ExamQuestionSectionComprehensionDao();
        ExamQuestionSectionDAO examQuestionSectionDao = new ExamQuestionSectionDAO();
        ExamQuestionDAO examQuestionDao = new ExamQuestionDAO();
        SectionDAO sectionDao = new SectionDAO();
        LOT_ExamQuestion examQuestion = (LOT_ExamQuestion)ViewData.Model;
        AnswerDao answerDao = new AnswerDao();   
        QuestionDao questionDao = new QuestionDao();   
        Response.Write(Html.Hidden("totalcorrectAnswer"));           
     %>       
     <div id="cactionbutton">                       
        <button id="btnShowCorrectAnswer" type="button" class="button preview" style="width:170px">Show Correct Answers</button>                    
        <button type="button" id="btnBack" title="Back" class="button back">Back</button>                      
    </div>
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
                        </legend>         
                        <div id = "divbtnActive<%=sectionIndex %>">
                    <% 
                        if (examQuestionSection.IsRandom)
                        {
                             %>
                                <br/>Type: Random
                                <br/>Number of quesion: <%=examQuestionSection.NumberOfQuestions%>
                             <% 
                        }
                        else
                        { 
                            %>                                
                                <% 
                                    if (examQuestionSection.SectionID == Constants.LOT_LISTENING_TOPIC_ID)
                                    {
                                        List<LOT_ExamQuestion_Section_ListeningTopic> examQuestion_Section_ListeningTopic_List = examQuestionSectionListeningTopicDao.GetByExamQuestionSectionID(examQuestionSection.ID);
                                        if (examQuestion_Section_ListeningTopic_List.Count > 0)
                                        {
                                            int listeningIndex = 1;
                                            foreach (LOT_ExamQuestion_Section_ListeningTopic exQuSecListeningItem in examQuestion_Section_ListeningTopic_List)
                                            { 
                                                %>
                                                <table class="form">
                                                    <tr>                                                        
                                                        <td>
                                                            <%
                                                                Response.Write(HttpUtility.HtmlEncode(exQuSecListeningItem.LOT_ListeningTopic.TopicName));
                                                                Response.Write(" <span id=\"btnPlay" + listeningIndex + "\" class=\"picon play\" title=\"Play\" onclick=\"play('" + listeningIndex + "','" + exQuSecListeningItem.LOT_ListeningTopic.FileName + "')\"></span>");
                                                                Response.Write(" <span id=\"btnStop" + listeningIndex + "\" style=\"display:none\" class=\"picon stop\" title=\"Stop\" onclick=\"stop('" + listeningIndex + "')\"></span>");
                                                                %>
                                                            <table class="form">
                                                            <% 
                                                                int questionIndex = 1;
                                                                List<LOT_Question> questions = questionDao.GetListByListeningTopicID(exQuSecListeningItem.ListeningTopicID);
                                                                foreach (LOT_Question question in questions)
                                                                {
                                                                    %>
                                                                      <tr>                                                                        
                                                                        <td valign="top"> 
                                                                            <%=questionIndex.ToString()%>) <%=CommonFunc.Encode(question.QuestionContent)%>         
                                                                            <table>                                                               
                                                                            <%
                                                                                List<LOT_Answer> answerList = answerDao.GetListByQuestionID(question.ID);
                                                                                    int answerIndex = 1;
                                                                                    foreach (LOT_Answer answer in answerList)
                                                                                    {
                                                                                        if (answer.IsCorrect)
                                                                                        {
                                                                                            Response.Write("<tr><td width=\"17px\" align=\"right\"><img src=\"/Content/Images/Icons/tick.png\" id=\"CorrectAnswer" + correctAnswer + "\" style=\"height:15px;display:none\"/></td><td>&#0" + (answerIndex + 64) + ";) " + HttpUtility.HtmlEncode(answer.AnswerContent) + "</td></tr>");
                                                                                            correctAnswer++;
                                                                                        }
                                                                                        else
                                                                                        {
                                                                                            Response.Write("<tr><td width=\"17px\" align=\"right\"></td><td>&#0" + (answerIndex + 64) + ";) " + HttpUtility.HtmlEncode(answer.AnswerContent) + "</td></tr>");
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
                                                <% if (listeningIndex < examQuestion_Section_ListeningTopic_List.Count)
                                                   { 
                                                    %>
                                                        <hr size=1/>
                                                    <%
                                                }%>                                                
                                                <%
                                                    listeningIndex++;
                                            }
                                                    Response.Write(Html.Hidden("hdNumberOfListeningTopic", examQuestion_Section_ListeningTopic_List.Count));
                                        }
                                    }
                                    else if (examQuestionSection.SectionID == Constants.LOT_COMPREHENSION_SKILL_ID)
                                    {
                                        List<LOT_ExamQuestion_Section_Comprehension> examQuestion_Section_Comprehension_List = examQuestionSectionComprehensionDao.GetByExamQuestionSectionID(examQuestionSection.ID);
                                        if (examQuestion_Section_Comprehension_List.Count > 0)
                                        {
                                            int comprehensionIndex = 1;
                                            foreach (LOT_ExamQuestion_Section_Comprehension exQuSecComprehenstionItem in examQuestion_Section_Comprehension_List)
                                            { 
                                                %>
                                                <table class="form">
                                                    <tr>                                                        
                                                        <td>
                                                            <%
                                                                Response.Write("<b>Paragraph: </b>" + CommonFunc.Encode(exQuSecComprehenstionItem.LOT_ComprehensionParagraph.ParagraphContent));                                                                
                                                                %>
                                                            <table class="form">
                                                            <% 
                                                                int questionIndex = 1;
                                                                List<LOT_Question> questions = questionDao.GetListByParagraphID(exQuSecComprehenstionItem.ParagraphID);
                                                                foreach (LOT_Question question in questions)
                                                                {
                                                                    %>
                                                                      <tr>                                                                        
                                                                        <td valign="top"> 
                                                                            <%=questionIndex.ToString()%>) <%=CommonFunc.Encode(question.QuestionContent)%>         
                                                                            <table>                                                               
                                                                            <%
                                                                                List<LOT_Answer> answerList = answerDao.GetListByQuestionID(question.ID);
                                                                                    int answerIndex = 1;
                                                                                    foreach (LOT_Answer answer in answerList)
                                                                                    {
                                                                                        if (answer.IsCorrect)
                                                                                        {
                                                                                            Response.Write("<tr><td width=\"17px\" align=\"right\"><img src=\"/Content/Images/Icons/tick.png\" id=\"CorrectAnswer" + correctAnswer + "\" style=\"height:15px;display:none\"/></td><td>&#0" + (answerIndex + 64) + ";) " + HttpUtility.HtmlEncode(answer.AnswerContent) + "</td></tr>");
                                                                                            correctAnswer++;
                                                                                        }
                                                                                        else
                                                                                        {
                                                                                            Response.Write("<tr><td width=\"17px\" align=\"right\"></td><td>&#0" + (answerIndex + 64) + ";) " + HttpUtility.HtmlEncode(answer.AnswerContent) + "</td></tr>");
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
                                                <% if (comprehensionIndex < examQuestion_Section_Comprehension_List.Count)
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
                                    else
                                    {
                                        List<LOT_ExamQuestion_Section_Question> examQuestion_Section_Question_List = examQuestionSectionQuestionDao.GetByExamQuestionSectionID(examQuestionSection.ID);
                                        if (examQuestion_Section_Question_List.Count > 0)
                                        { 
                                            %>
                                                <table class="form" width="100%">
                                                    <%
                                                        int questionIndex = 1;
                                                        foreach (LOT_ExamQuestion_Section_Question exQuSecQuitem in examQuestion_Section_Question_List)
                                                        { 
                                                            %>
                                                                <tr>
                                                                    <td valign="top">                                                                                                                                                      
                                                                        <%
                                                                            if (exQuSecQuitem.LOT_ExamQuestion_Section.SectionID == Constants.LOT_PROGRAMMING_SKILL_ID)
                                                                            {
                                                                                Response.Write("<pre>" + questionIndex.ToString() + ")" + HttpUtility.HtmlEncode(exQuSecQuitem.LOT_Question.QuestionContent) + "</pre>");                                                                                
                                                                            }
                                                                            else
                                                                            {
                                                                                Response.Write(questionIndex.ToString() + ")" + CommonFunc.Encode(exQuSecQuitem.LOT_Question.QuestionContent));
                                                                            }
                                                                         %>
                                                                        <table>
                                                                        <% 
                                                                            int answerIndex = 1;
                                                                            List<LOT_Answer> answerList = answerDao.GetListByQuestionID(exQuSecQuitem.QuestionID);
                                                                            foreach(LOT_Answer answer in answerList)
                                                                            {
                                                                                if (answer.IsCorrect)
                                                                                {
                                                                                    Response.Write("<tr><td width=\"17px\" align=\"right\"><img src=\"/Content/Images/Icons/tick.png\" id=\"CorrectAnswer" + correctAnswer + "\" style=\"height:15px;display:none\"/></td><td>&#0" + (answerIndex + 64) + ";) " + HttpUtility.HtmlEncode(answer.AnswerContent) + "</td></tr>"); 
                                                                                    correctAnswer ++;
                                                                                }               
                                                                                else
                                                                                {
                                                                                    Response.Write("<tr><td width=\"17px\" align=\"right\"></td><td>&#0" + (answerIndex + 64) + ";) " + HttpUtility.HtmlEncode(answer.AnswerContent) + "</td></tr>");                                                                                 
                                                                                }
                                                                                answerIndex++;
                                                                            }
                                                                         %>
                                                                         <tr>
                                                                            <td colspan="2"></td>
                                                                         </tr>
                                                                         </table>  
                                                                         <% if (questionIndex < examQuestion_Section_Question_List.Count) { 
                                                                                %>
                                                                                    <hr size=1/>
                                                                                <%
                                                                            }%>                                                                        
                                                                    </td>                                                                 
                                                                </tr>
                                                            <%
                                                                questionIndex++;
                                                        }
                                                     %>
                                                </table>                                             
                                            <%
                                        }                                    
                                    }
                                %>                                                                                            
                            <% 
                        }
                    %>
                    </div>
                    </fieldset>                                  
                </td>
            </tr>                   
          <%
              sectionIndex++;          
        }        
        //Set No of total correct answer 
        Response.Write("<script>SetTotalQuestion('" + correctAnswer + "'); </script>");
     %>    
     </table>     
     <div id="div_jp_Media" style="width:0px;height:0px;"></div>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="PageTitle" runat="server">
    <%= LOTPageInfo.MenuName + CommonPageInfo.AppSepChar+ LOTPageInfo.ViewDetail + CommonPageInfo.AppSepChar + LOTPageInfo.ComName + CommonPageInfo.AppSepChar + CommonPageInfo.AppName%>    
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
    <script src="/Scripts/jquery.validate.js" type="text/javascript"></script>
    <script type="text/javascript" src="/Scripts/jquery.jplayer.min.js"></script>
    <script type="text/javascript" src="/Scripts/jquery.sound.js"></script>
    <script type="text/javascript">
        jQuery(document).ready(function () {
            $("#btnBack").click(function () {
                window.location = "/ExamQuestion/Index";
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
                        $("#btnShowCorrectAnswer").text("Hide Correct Answers");
                    }
                    else {
                        $("#CorrectAnswer" + i).css("display", "none");
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
                url: '/Question/CheckFileExist?filename=' + filename,
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

        function SetTotalQuestion(value)     
        {
            $("#totalcorrectAnswer").val(value);
        }

    </script>
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="ModuleName" runat="server">
    <%= LOTPageInfo.ComName%>
</asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="FunctionTitle" runat="server">
<%--<%= LOTPageInfo.MenuName + CommonPageInfo.AppDetailSepChar +
    "<a href='/ExamQuestion/Index'>"+ LOTPageInfo.ExamQuestion+"</a>"+ CommonPageInfo.AppDetailSepChar
    +ViewData[CommonDataKey.EXAM_QUESTION_TITLE]
     %>--%>
     <%= CommonFunc.GetCurrentMenu(Request.RawUrl)
    +ViewData[CommonDataKey.EXAM_QUESTION_TITLE]
     %>
</asp:Content>

<asp:Content ID="Content6" ContentPlaceHolderID="LoginRoles" runat="server">
</asp:Content>
