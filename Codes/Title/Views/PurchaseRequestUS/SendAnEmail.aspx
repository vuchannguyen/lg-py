<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<link href="/Scripts/AutoComplete/autoSuggest.css" rel="stylesheet" type="text/css" />
<script type="text/javascript">

    $(document).ready(function () {
        $("#fieldCC").autocomplete('/Library/GenericHandle/AutoCompleteHandler.ashx/?Page=SendMail', { max: 50, highlightItem: true, multiple: true, multipleSeparator: ";", faceBook: true, hidField: "#CC", employee: true });
        $("#fieldTo").autocomplete('/Library/GenericHandle/AutoCompleteHandler.ashx/?Page=SendMail', { max: 50, highlightItem: true, multiple: true, multipleSeparator: ";", faceBook: true, hidField: "#To", employee: true });
        $("#btnCancel").click(function () {
            CRM.closePopup();
        });
        $("#frmSendMail").validate({
            debug: false,
            errorElement: "span",
            errorPlacement: function (error, element) {
                error.tooltip({
                    bodyHandler: function () {
                        return error.html();
                    }
                });
                error.insertAfter(element);
            },
            rules: {
                To: { required: true }

            },
            submitHandler: function (form) {
                if (i == 0) {
                    form.submit();
                    i++;
                }
            }
        });
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

<%using (Html.BeginForm("SendAnEmail", "PurchaseRequestUS", FormMethod.Post, new { id = "frmSendMail", @class = "form" }))
  {
      sp_GetPurchaseRequestResult request = (sp_GetPurchaseRequestResult)ViewData.Model;
%>

<div style="width: 95%">
<%= Html.Hidden("ID", request.ID)%>
    <table cellspacing="0" cellpadding="0" border="0" width="95%" class="edit" style="border-bottom-style: solid">
        <tbody>
            <tr>
                <td class="label">
                    To
                </td>
                <td class="input" style="width: 690px">
                    <%
                        
                            Response.Write(Html.TextBox("fieldTo", "", new { @style = "width:690px" }));
                            Response.Write(Html.Hidden("To", ""));
                                
                    %>
                </td>
            </tr>
            <tr>
                <td class="label">
                    CC
                </td>
                <td class="input">
                    <%
                        
                            Response.Write(Html.TextBox(("fieldCC"), ""));
                            Response.Write(Html.Hidden("CC", ""));
                       
                                
                    %>
                </td>
            </tr>
            <tr>
                <td class="label">
                    Subject
                </td>
                <td class="input">
                    <%
                        string title="[CRM-PR] Purchase Request PR-"+ request.ID;
                        Response.Write(Html.TextBox("Subject", title, new { @style = "width:690px" }));
                        //if (1==1)
                        //    Response.Write(Html.TextBox("Subject", "Interview Invitation ", new { @style = "width:690px" }));
                        //else
                        //    Response.Write(Html.TextBox("Subject", "Interview Invitation - " + interview.Candidate.FirstName + " " + interview.Candidate.MiddleName + " " + interview.Candidate.LastName, new { @style = "width:690px" }));
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
                    <%--<%=Html.FCKEditor("body", ViewData["template"].ToString(), 805, 300, null)%>--%>
                    <%=Html.FCKEditor("body", ViewData["template"].ToString(), 805, 300, null)%>
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
                        <input type="button" id="btnCancel" value="" class="cancel" />
                    </div>
                </td>
            </tr>
        </tbody>
    </table>
</div>

<% } %>
