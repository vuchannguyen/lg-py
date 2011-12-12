<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<link href="/Scripts/AutoComplete/autoSuggest.css" rel="stylesheet" type="text/css" />
<script type="text/javascript">

    $(document).ready(function () {
        $("#fieldCC").autocomplete('/Library/GenericHandle/AutoCompleteHandler.ashx/?Page=SendMail', { max: 50, highlightItem: true, multiple: true, multipleSeparator: ";", faceBook: true, hidField: "#CC", employee: true });
    });

    function CheckEmailList(form) {
        jQuery.ajax({
            url: "/Common/CheckCCList",
            type: "POST",
            datatype: "json",
            data: ({
                'userNameList': $('#To').val()
            }),
            success: function (result) {
                if (result == true) {
                    form.submit();
                }
                else {
                    CRM.message(result, 'block', 'msgError');
                    return false;
                }
            }
        })
    }
</script>
<%= TempData["Message"]%>
<% Interview interview = ViewData["interview"] == null ? null : (Interview)ViewData["interview"];%>
<%using (Html.BeginForm("SendCandidateMail", "Interview", FormMethod.Post, new { id = "frmSendMail", @class = "form" }))
  { %>
<%= Html.Hidden("ID",interview.Id) %>
<%= Html.Hidden("Page", (string)ViewData["Page"])%>
<div style="width: 95%">
    <table cellspacing="0" cellpadding="0" border="0" width="95%" class="edit" style="border-bottom-style: solid">
        <tbody>
            <tr>
                <td class="label">
                    To
                </td>
                <td class="input" style="width: 690px">
                    <%
                        if (interview == null)
                            Response.Write(Html.TextBox("To", "", new { @style = "width:690px" }));
                        else
                            Response.Write(Html.TextBox("To", interview.Candidate.Email, new { @style = "width:690px", @readonly="true" }));
                                
                    %>
                </td>
            </tr>
            <tr>
                <td class="label">
                    CC
                </td>
                <td class="input">
                    <%
                        if (interview == null)
                        {
                            Response.Write(Html.TextBox(("fieldCC"), ""));
                            Response.Write(Html.Hidden("CC", ""));
                        }
                        else
                        {
                            Response.Write(Html.TextBox("fieldCC", interview.Pic));
                            Response.Write(Html.Hidden("CC", interview.Pic ));
                        }
                                
                    %>
                </td>
            </tr>
            <tr>
                <td class="label">
                    Subject
                </td>
                <td class="input">
                    <%
                        if (interview == null)
                            Response.Write(Html.TextBox("Subject", "Interview Invitation ", new { @style = "width:690px" }));
                        else
                            Response.Write(Html.TextBox("Subject", "Interview Invitation - " + interview.Candidate.FirstName + " " + interview.Candidate.MiddleName + " " + interview.Candidate.LastName, new { @style = "width:690px" }));
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
                    <%=Html.FCKEditor("body", ViewData["template"].ToString(), 805, 300, null)%>
                    <%--<%=Html.TextArea("Body", ViewData["template"].ToString(), new {@style="width:1000px; height:300px" })%>--%>
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
                        <input type="submit" title="Send" class="send" value="" />
                        <input type="button" onclick="window.location = '/Interview'" alt="" value="" class="cancel" />
                    </div>
                </td>
            </tr>
        </tbody>
    </table>
</div>
<% } %>
