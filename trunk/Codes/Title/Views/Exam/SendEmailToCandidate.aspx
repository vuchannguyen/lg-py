<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>SendEmailToCandidate</title>
    <link href="/Scripts/AutoComplete/autoSuggest.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" charset="utf-8">
        $(document).ready(function () {
            $("#fieldCC").autocomplete('/Library/GenericHandle/AutoCompleteHandler.ashx/?Page=SendMail'
                , { max: 50, highlightItem: true, multiple: true, multipleSeparator: ";"
                , faceBook: true, hidField: "#CC", employee: true });
        });
    </script>
</head>
<body>
    <div id="divMessageConstant" style="display: none" class=""></div>
    <div id="summary" style="display: none" class=""></div>
    <%using (Html.BeginForm("SendEmailToCandidate", "Exam", FormMethod.Post, new { id = "frmSendMail", @class = "form" }))
      { 
          LOT_Candidate_Exam candidateExam = (LOT_Candidate_Exam)ViewData.Model;
          Response.Write(Html.Hidden("ID", candidateExam.ID));
          Response.Write(Html.Hidden("page", "CandidateTestList/" + candidateExam.ExamID));
    %>
    <div style="width: 95%">
        <table cellspacing="0" cellpadding="0" border="0" width="95%" class="edit" style="border-bottom-style: solid">
            <tbody>
                <tr>
                    <td class="label">
                        To
                    </td>
                    <td class="input" style="width:800px">
                        <%
                            if (candidateExam.LOT_Exam.ExamType == Constants.LOT_CANDIDATE_EXAM_ID)
                            {
                                Response.Write(Html.TextBox("To", candidateExam.Candidate.Email, new { @style = "width:800px" }));
                            }
                            else
                            {
                                Response.Write(Html.TextBox("To", candidateExam.Employee.OfficeEmail, new { @style = "width:800px" }));
                            }
                        %>
                    </td>
                </tr>
                <tr>
                    <td class="label">
                        CC
                    </td>
                    <td class="input" style="width:800px">
                        <%
                            Response.Write(Html.TextBox(("fieldCC"), "", new { @style = "width:200px" }));
                            Response.Write(Html.Hidden("CC", ""));
                        %>
                    </td>
                </tr>
                <tr>
                    <td class="label">
                        Subject
                    </td>
                    <td class="input" style="width:800px">
                        <%
                            Response.Write(Html.TextBox("Subject", Constants.CTL_EMAIL_SUBJECT, new { @style = "width:800px" }));
                        %>
                    </td>
                </tr>
                <tr>
                    <td>
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td colspan="2" align="center">
                        <%=Html.FCKEditor("body", ViewData["template"].ToString(), 850, 300, "BasicOnlineTest")%>
                    </td>
                </tr>
                <tr>
                    <td>
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td align="center" colspan="2">
                        <div class="cbutton">
                            <input type="submit" title="Send" class="send" value=""/>
                            <input type="button" onclick="CRM.closePopup();" alt="" value="" class="cancel" />
                        </div>
                    </td>
                </tr>
            </tbody>
        </table>
    </div>
    <% } %>
</body>
</html>
