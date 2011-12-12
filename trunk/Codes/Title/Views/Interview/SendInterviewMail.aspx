<%@ Page Title="" Language="C#"  Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

    <link href="/Scripts/AutoComplete/autoSuggest.css" rel="stylesheet" type="text/css" />
    <%= TempData["Message"]%>
    <% Interview interview = ViewData["interview"] == null ? null : (Interview)ViewData["interview"];%>
    <%using (Html.BeginForm("SendInterviewMail", "Interview", FormMethod.Post, new { id = "frmSendMail", @class = "form" }))
      { %>
      <%= Html.Hidden("ID",interview.Id) %>
      <%= Html.Hidden("Page", (string)ViewData["Page"])%>
    <div style="width: 95%">
        <table cellspacing="0" cellpadding="0" border="0" width="95%" class="edit" style="border-bottom-style:solid"  >
            <tbody>
                <tr>
                    <td class="label">
                        To
                    </td>
                    <td class="input" >
                        <%
                            if (interview == null)
                            {
                                Response.Write(Html.TextBox("fieldTo", ""));
                                Response.Write(Html.Hidden("To", ""));
                            }
                            else
                            {
                                Response.Write(Html.TextBox("fieldTo", interview.Pic));
                                Response.Write(Html.Hidden("To", interview.Pic));
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
                    <td class="label">
                        Location
                    </td>
                    <td class="input">
                    <%
                            if(interview==null)
                                Response.Write(Html.TextBox("Location", "", new { @style = "width:690px" }));
                            else
                                Response.Write(Html.TextBox("Location", interview.Venue, new { @style = "width:690px" }));
                        %>
                        
                    </td>
                </tr>
                <tr>
                    <td class="label">
                        Date
                    </td>
                    <td class="input">
                        <%=Html.DatePicker("Date",interview.InterviewDate.Value.ToString(Constants.DATETIME_FORMAT),true,"-1:+1")%>
                        &nbsp;&nbsp; Time &nbsp;&nbsp;
                        <%=Html.DropDownList("FromHour",null,"...", new { @style = "width:40px" })%>
                        :
                        <%=Html.DropDownList("FromMinute",null,"...",  new { @style = "width:40px" })%>
                        &nbsp;&nbsp; To &nbsp;&nbsp;
                        <%=Html.DropDownList("ToHour",null,"...", new { @style = "width:40px" })%>
                        :
                        <%=Html.DropDownList("ToMinute",null,"...",  new { @style = "width:40px" })%>
                    </td>
                </tr>
                <tr>
                    <td>
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td colspan="2" align="center">
                       <%-- <%= Html.FCKEditor("Body", ViewData["template"].ToString(), 1000, 400,"")%>--%>
                       <%=Html.TextArea("Body", ViewData["template"].ToString(), new {@style="width:805px; height:300px" })%>
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
                            <input type="button" title="Cancel" onclick="window.location = '/Interview'" id="btnCancel" class="cancel" value="" />
                            
                        </div>
                    </td>
                </tr>
            </tbody>
        </table>
    </div>
    
    <% } %>
    <script type="text/javascript" charset="utf-8">

        $(document).ready(function () {
            $("#fieldTo").autocomplete('/Library/GenericHandle/AutoCompleteHandler.ashx/?Page=SendMail', { max: 50, highlightItem: true, multiple: true, multipleSeparator: ";", faceBook: true, hidField: "#To", employee: true });


            jQuery.validator.addMethod(
            "validateDate", function (value) {
                var now = new Date();
                var strNow = now.getDate() + "/" + (now.getMonth() + 1) + "/" + now.getFullYear();
                var result = CRM.compareDate(value, strNow, true);
                return result >= 0 ? true : false;
            }, 'Date must to Greater or Equal now !');
            jQuery.validator.addMethod(
            "validateHour", function (value) {
                var fromH = $("#FromHour").val();
                var fromM = $("#FromMinute").val();
                var toH = $("#ToHour").val();
                var toM = $("#ToMinute").val();

                if (fromH > toH) {

                    return false;
                }
                if (fromH == toH) {

                    if (fromM > toM) {

                        return false;
                    }
                }
                return true;
            }, 'To Time must to greate or equal from time !');
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
    <%  
        string rule = "To: { required: true },";
        rule += "Subject :{ required: true },";
        rule += "Location :{ required: true },";
        rule += "Date :{ required: true, validateDate: true },";
        rule += "FromHour :{ required: true },";
        rule += "FromMinute :{ required: true },";
        rule += "ToHour :{ required: true},";
        rule += "ToMinute :{ required: true, validateHour: true  }";

        string submitHandler = "";
         
    %>
    <%=Html.ValidateForm("frmSendMail", rule, submitHandler)%>
