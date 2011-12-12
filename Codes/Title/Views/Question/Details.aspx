<%@ Page Title="" Language="C#" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>
<link type="text/css" href="/Content/Css/SynTaxHighLight.css" rel="stylesheet" />
<div class="details_view form">
<div class="details_content">
    <div class="edit">
    <table width="100%" border="0" cellpadding="0" cellspacing="0">
        <tr>
            <td class="input" align="justify" colspan="2">
                <% 
                    /*Response.Write("<b><u>Question content:</u></b><br/>"
                        + (((LOT_Question)ViewData[CommonDataKey.QUESTION]).SectionID != Constants.LOT_PROGRAMMING_SKILL_ID ?
                            CommonFunc.Encode(((LOT_Question)ViewData[CommonDataKey.QUESTION]).QuestionContent) : "<pre class=\"brush csharp\">" +
                            CommonFunc.SyntaxHighLight(((LOT_Question)ViewData[CommonDataKey.QUESTION]).QuestionContent) + "</pre>"
                        ));*/
                    string content = ((LOT_Question)ViewData[CommonDataKey.QUESTION]).QuestionContent;
                    if (((LOT_Question)ViewData[CommonDataKey.QUESTION]).SectionID == Constants.LOT_PROGRAMMING_SKILL_ID)
                    {
                        content = "<pre class=\"brush csharp\">" + HttpUtility.HtmlEncode(content) + "</pre>";
                    }
                    else
                    {
                        content = CommonFunc.Encode(content);
                    }
                    Response.Write("<b><u>Question content:</u></b><br/>" + content);
                %>
            </td>
        </tr>
        <tr>
            <td style="height: 10px; width: 30px" class="input">
            </td>
            <td class="input">
            </td>
        </tr>
        <%
            if (((LOT_Question)ViewData[CommonDataKey.QUESTION]).SectionID != Constants.LOT_WRITING_SKILL_ID && ((LOT_Question)ViewData[CommonDataKey.QUESTION]).SectionID != Constants.LOT_PROGRAMMING_SKILL_ID)
            {
        %>
        <tr>
            <td colspan="2" class="input">
                <%
                    Response.Write("<b><u>Answers:</u></b>");
                %>
            </td>
        </tr>
        <%  List<LOT_Answer> arrAnswer = (List<LOT_Answer>)ViewData[CommonDataKey.ANSWERS_LIST];
            foreach (LOT_Answer answer in arrAnswer)
            {
                int ASCIIcode = 65 + answer.AnswerOrder;// 65: ASCII code of character "A"
        %>
        <tr>
            <td class="order">
                <% if (answer.IsCorrect)
                   {
                       Response.Write("<span class='correct_check' /><b>" 
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
        <%  }
            }
        %>
    </table> 
    </div>   
</div>
</div>
