<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<dynamic>" %>
<link href="/Scripts/AutoComplete/autoSuggest.css" rel="stylesheet" type="text/css" />
<% Employee_Certification empCer = (Employee_Certification)ViewData.Model;
%>
<script type="text/javascript">
    $(document).ready(function () {
        $("#EmployeeName").autocomplete('/Library/GenericHandle/AutoCompleteHandler.ashx/?Page=EmployeeWithID' + '&IsActive=1', { employee: true,hidField:"#EmployeeId" });
        $("#EmployeeId").result(function (event, data, formatted) {
            var submitToId = formatted.split(' - ')[0];
            $("#SubmitTo").val(submitToId);
            if (submitToId != "") {
                //CRM.loading();
                $.ajax({
                    async: true,
                    cache: false,
                    type: "GET",
                    dataType: "html",
                    timeout: 1000,
                    url: '/Portal/PTO/GetEmployeeLoginName?empId=' + submitToId,
                    success: function (msg) {
                        $("#submitDesc").show();
                        $("#submitDesc").html("More details about <a id=" + submitToId + " class='submit_desc' href='#'>" + msg + "</a>");
                        ShowTooltip($("a.submit_desc"), $("#shareit-box"), "/Portal/TrainingCenter/EmployeeToolTip");
                    }
                });
                //CRM.completed();
            } else {
                $("#submitDesc").hide();
            }
        });
        $("#TrainingEmployeeCertificationForm").validate({
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
                EmployeeId: {
                    required: true,
                    maxlength: 200
                },
                 CertificationId: {
                    required: true,
                    maxlength: 200
                }
            }
        });
    });
</script>
<%--<%using (Html.BeginForm("Create", "Customer1", FormMethod.Post, new { id = "addForm",@class="form" }))
  {%>
<uc1:UCInfo ID="UCInfo1" runat="server" />
<% } %>--%>
<table width="100%" border="0" cellpadding="0" cellspacing="0" class="edit">
    <tr>
        <td class="label required" style = "vertical-align:top; width:120px">
            Employee <span>*</span>
        </td>
        <td class="input">
             <% 
                 if (ViewData.Model == null)
               {
                   Response.Write(Html.TextBox("EmployeeName", "", new { @maxlength = "200", @style = "width:150px" }));
                   Response.Write(Html.Hidden("EmployeeId", "", new { @maxlength = "200", @style = "width:150px" }));
               }
               else
               {
                   Response.Write(Html.TextBox("EmployeeName", empCer.EmployeeId, new { @maxlength = "200", @style = "width:150px" }));
                   Response.Write(Html.Hidden("EmployeeId", empCer.EmployeeId, new { @maxlength = "200", @style = "width:150px" }));
               }
            %>
        </td>
    </tr>
     <tr>
        <td class="label required" style = "vertical-align:top; width:120px">
            Certification<span>*</span>
        </td>
        <td class="input">
             <% if (ViewData.Model == null)
               {
                   Response.Write(Html.TextBox("CertificationId", "", new { @maxlength = "200", @style = "width:150px" }));
               }
               else
               {
                   Response.Write(Html.TextBox("CertificationId", empCer.CertificationId, new { @maxlength = "200", @style = "width:150px" }));
               }
            %>
            <%-- <button type="button" class="icon select" title="Select Manager" onclick="CRM.listCertificationMaster(); return false;">
             </button>--%>
            <button type="button" class="icon select" title="Select Certification" onclick="CRM.pInPopup('/Common/ListCertificationMaster/?isOnPopup=1', 'Select Certification', 550)">
            </button>
            <button type="button" class="icon remove" title="Remove Certification" onclick="$('#CertificationId').val('');">
            </button>
        </td>
    </tr>
    <tr>
        <td class="label" style = "vertical-align:top">
            Remark
        </td>
         <td class="input">
             <% if (ViewData.Model == null)
               {
                   Response.Write(Html.TextArea("Remark", "", new { @maxlength = "500", @style = "width:285px; height:100px" }));
               }
               else
               {
                   Response.Write(Html.TextArea("Remark", empCer.Remark, new { @maxlength = "500", @style = "width:285px; height:100px" }));
                  
               }
            %>
        </td>
    </tr>
     <tr>
        <td>
        </td>
        <td>
            <input type="submit" class="save" value="" alt="Update" />
            <input type="button" class="cancel" value="" alt="" onclick="CRM.closePopup()" />
        </td>
    </tr>              
</table>