<%@ Page Title="" Language="C#" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>
<script type="text/javascript" src="/Scripts/jquery.jplayer.min.js"></script>
<script type="text/javascript" src="/Scripts/jquery.sound.js"></script>
<script type="text/javascript">
    jQuery(document).ready(function () {
        //constructor options for jplayer
        $('#div_jp_Media').jPlayer({
            swfPath: '/Scripts',
            solution: $.browser.msie ? 'html, flash' : 'flash,html',
            supplied: '<%=Constants.SOUND_FILE_EXT_ALLOW%>',
            preload: 'metadata',
            volume: 1.0,
            muted: false,
            errorAlerts: false,
            warningAlerts: false,
            ended: function () { // The $.jPlayer.event.ended event
                $("#btnStop").css("display", "none");
                $("#btnPlay").css("display", "");
            }
        });        
        $("#btnPlay").click(function () {
            var fullFileName = '<%=Constants.SOUND_FOLDER%>' + $("#FullFileName").val();
            try {
                playSound("div_jp_Media", fullFileName);
                $("#btnPlay").css("display", "none");
                $("#btnStop").css("display", "");
            }
            catch (e) {
                CRM.summary('<%=Constants.SOUND_FILE_NOT_EXIST_MESSAGE%>', "block", "msgError");
            }
        });
        $("#btnStop").click(function () {
            stopSound("div_jp_Media");
            $(this).css("display", "none");
            $("#btnPlay").css("display", "");
        });
    });
</script>
<div class="details_view form">
<div class="details_content">
    <div class="edit">
    <table width="100%" border="0" cellpadding="0" cellspacing="0">
    <%
        Type modelType = ViewData.Model.GetType();
        if (modelType.Equals(typeof(LOT_ListeningTopic)))
        {
    %>
            <tr>
                <td class="input" align="justify" colspan="2">
                    <div id="div_jp_Media"></div>
                    <div id="summary" class='msgError' style='display: none'>
                    </div>
                    <% 
                    LOT_ListeningTopic topic = (LOT_ListeningTopic)ViewData.Model;
                    Response.Write(Html.Hidden("FullFileName", topic.FileName));
                    Response.Write("<b><u>Topic Name:</u></b><br/>" + HttpUtility.HtmlEncode(topic.TopicName));
                    string fullFileName = "";
                    string filename = Constants.NO_FILE_ERROR;
                    string playDisplay = "none";
                    if (System.IO.File.Exists(Server.MapPath("~" + Constants.SOUND_FOLDER) + topic.FileName))
                    {
                        fullFileName = topic.FileName;
                        filename = fullFileName.Substring(Constants.UNIQUEID_STRING_FORMAT.Length + 1);
                        playDisplay = Constants.CONST_JPLAYER_BUTTON_DISPLAY_VISIBLE;
                    }
                    Response.Write("<br/><br/><b><u>File Name:</u></b><br/>" + filename);
                    Response.Write(" <span id=\"btnPlay\" class=\"picon play\" style='display:"
                        + playDisplay + "'title=\"Play\"/></span>");
                    Response.Write("<span id=\"btnStop\" class=\"picon stop\" "
                        + "style='display:none' title=\"Stop\"/></span>");
                    %>
                </td>
            </tr>
    <%
        }
        else
        { 
    %>
            <tr>
                <td class="input" align="justify" colspan="2">
                    <% 
                    LOT_ComprehensionParagraph paragraph = (LOT_ComprehensionParagraph)ViewData.Model;
                    Response.Write("<b><u>Paragraph Content:</u></b><br/>"
                        + CommonFunc.Encode(paragraph.ParagraphContent));
                    %>
                </td>
            </tr>
    <%        
        }    
        %>
            <tr>
                <td style="height: 10px; width: 30px" class="input">
                </td>
                <td class="input">
                </td>
            </tr>
            <tr>
                <td colspan="2" class="input">
                    <%
                        Response.Write("<b><u>Questions List:</u></b>");
                    %>
                </td>
            </tr>
    <%  CRM.Models.AnswerDao aDao = 
            new CRM.Models.AnswerDao();

        List<LOT_Question> arrQuestion = (List<LOT_Question>)ViewData[CommonDataKey.QUESTION_ARR];
        for (int i = 0; i < arrQuestion.Count; i++)
        {
            LOT_Question question = arrQuestion[i];
            Response.Write("<tr><td colspan='2'>" + (i + 1) + ") "
                + CommonFunc.Encode(question.QuestionContent) + "</td></tr>");
            List<LOT_Answer> arrAnswer = aDao.GetListByQuestionID(question.ID);
            foreach (LOT_Answer answer in arrAnswer)
            {
                int ASCIIcode = 65 + answer.AnswerOrder;// 65: ASCII code of character "A"
    %>
            <tr>
                <td class="order">
            <% if (answer.IsCorrect)
                {
                    Response.Write("<span class='correct_check'/><b>" 
                        + Convert.ToChar(ASCIIcode) + ".</b>");
                }
                else
                {
                    Response.Write("<b>" + Convert.ToChar(ASCIIcode) + ".</b>");
                }
            %>
                </td>
                <td class="answercontent">
            <% 
                Response.Write(HttpUtility.HtmlEncode(answer.AnswerContent));
            %>
                </td>
            </tr>
    <%      }
        }
    %>
    </table>
    </div>
</div>
</div>
