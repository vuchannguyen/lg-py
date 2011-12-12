<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<%@ Register Src="UCInfo.ascx" TagName="UCInfo" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <%= TempData["Message"]%>
    <br />
    <%using (Html.BeginForm("ToEmployee", "Interview", FormMethod.Post, new { id = "addEmployee" }))
      {%>
    <% Response.Write(Html.Hidden("CandidateId", ((CRM.Models.Candidate)ViewData.Model).ID)); %>
    <uc1:UCInfo ID="UCInfo1" runat="server" />
    <% } %>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PageTitle" runat="server">
    <%=HiringCenterPageInfo.MenuName + CommonPageInfo.AppSepChar + HiringCenterPageInfo.FuncTransferToEmployee + CommonPageInfo.AppSepChar + CommonPageInfo.AppName%>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
    <script type="text/javascript">
        function ChooseHospital(id) {
            CRM.closePopup();
            $("#InsuranceHospitalID").attr('value', ($("#" + id).attr('id')));
            $("#InsuranceName").attr('value', ($("#" + id).text()));
            $("#InsuranceName").attr('title', ($("#" + id).text()));
        }
        function RemoveHospital() {
            $("#InsuranceName").attr('value', '');
            $("#InsuranceHospitalID").attr('value', '');
            $("#trHospitalEffectDate").css("display", "none");
            $("#hospitalEffectDate").val("");
        }
        function actionForJobRequest() {
            window.location = "/JobRequest/Index/<%=Constants.JOB_REQUEST_ITEM_PREFIX%>" + $("#JR").val();
        }

        function formSubmit(form) {
            $(form).ajaxSubmit({
                url: form.action,
                dataType: 'json',
                success: function (result) {
                    if (result.MsgType == 1) {
                        CRM.message(result.MsgText, 'block', 'msgError');
                    }
                    else {
                        CRM.msgConfirmBox('<br/>Do you want to close Job Request <b><%=Constants.JOB_REQUEST_ITEM_PREFIX%>' + $("#JR").val() + '</b> now?', 500, 'actionForJobRequest()', 'Please Confirm');
                        CRM.summary(result.MsgText, "", "msgSuccess");
                        $(".cancel").click(function () {
                            window.location = "/Interview/Index/";
                        });
                    }
                }
            });
        }

        $(document).ready(function () {
            $("#DepartmentName").change(function () {
                $("#TitleId").html("");
                $("#DepartmentId").html("");
                var department = $("#DepartmentName").val();
                $("#TitleId").append($("<option value=''><%= Constants.FIRST_ITEM_JOBTITLE %></option>"));
                $("#DepartmentId").append($("<option value=''><%= Constants.FIRST_ITEM_SUB_DEPARTMENT %></option>"));
                if (department != 0) {
                    $.getJSON('/Library/GenericHandle/DropdownListHandler.ashx?ID=' + department + '&Page=Department', function (item) {
                        $.each(item, function () {
                            if (this['ID'] != undefined) {
                                $("#TitleId").append($("<option value='" + this['ID'] + "'>" + this['Name'] + "</option>"));
                            }
                        });
                    });
                    $.getJSON('/Library/GenericHandle/DropdownListHandler.ashx?ID=' + department + '&Page=SubDepartment', function (item) {
                        $.each(item, function () {
                            if (this['ID'] != undefined) {
                                $("#DepartmentId").append($("<option value='" + this['ID'] + "'>" + this['Name'] + "</option>"));
                            }
                        });
                    });
                }
            });

            $("#addEmployee").validate({
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
                    VnFirstName: { required: true },
                    VnLastName: { required: true },
                    ID: { required: true, remote: '<%= Url.Action("CheckIDExits", "Employee") %>' },
                    LastName: { required: true },
                    FirstName: { required: true },
                    Gender: { required: true },
                    IDNumber: { required: true, min: 1, number: true },
                    ContractedDate: { required: true, checkDate: true, compareDate: ["#addEmployee input[name='Startdate']", "get", "Contracted Date", "Start Date"] },
                    Startdate: { required: true, checkDate: true },
                    MarriedStatus: { required: true },
                    TitleId: { required: true },
                    DepartmentId: { required: true },
                    EmpStatusId: { required: true },
                    IssueDate: { required: true, checkDate: true, checkAgeIssue: ["#addEmployee input[name='DOB']"] },
                    DOB: { required: true, checkDate: true, checkBirthDate: true },
                    Startdate: { required: true, checkDate: true, compareDate: ["#addEmployee input[name='DOB']", "get", "Start Date", "Date Of Birth"], checkAge: ["#addEmployee input[name='DOB']"] },
                    PersonalEmail: { email: true },
                    OfficeEmail: { email: true },
                    Nationality: { required: true },
                    TaxIssueDate: { checkDate: true },
                    IDIssueLocation: { required: true },
                    JR: { required: true,remote: {
                        url: "/Employee/CheckJRExits",
                        type: "post",
                        data: {
                            jr: function () {
                                return $("#JR").val();
                            },
                            empID: function () {
                                return '';
                            }
                        }
                    }
                    }
                },
                submitHandler: function (form) {
                    if ($('#OfficeEmail').val() != "") {
                        jQuery.ajax({
                            url: "/Employee/CheckEmailExits",
                            type: "POST",
                            datatype: "json",
                            data: ({
                                'email': $('#OfficeEmail').val()
                            }),
                            success: function (mess) {
                                if (mess.MsgType == 1) {
                                    CRM.message(mess.MsgText, 'block', 'msgError');
                                    return false;
                                }
                                else {
                                    formSubmit(form);
                                }
                            }
                        })
                    }
                    else {
                        formSubmit(form);
                    }
                }
            });
        });        
    </script>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ModuleName" runat="server">
    <%=HiringCenterPageInfo.ComName%>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="FunctionTitle" runat="server">
    <%
        if (ViewData.Model == null)
            Response.Redirect("/Interview");
        Candidate canObj = (CRM.Models.Candidate)ViewData.Model; %>
        <%=  CommonFunc.GetCurrentMenu(Request.RawUrl) + Html.ActionLink(canObj.FirstName + " " + canObj.MiddleName + " " + canObj.LastName, "../Interview/Detail/" + canObj.ID)
                                  + " » " + HiringCenterPageInfo.FuncTransferToEmployee%>
   <%-- <%= HiringCenterPageInfo.MenuName + CommonPageInfo.AppDetailSepChar %>
    <a href='/Interview/'>
        <%=HiringCenterPageInfo.ModInterview%></a> »
    <% if (canObj != null) Response.Write(Html.ActionLink(canObj.FirstName + " " + canObj.MiddleName + " " + canObj.LastName, "../Interview/Detail/" + canObj.ID)); %>
    »
    <%=HiringCenterPageInfo.FuncTransferToEmployee%>--%>
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="LoginRoles" runat="server">
</asp:Content>
