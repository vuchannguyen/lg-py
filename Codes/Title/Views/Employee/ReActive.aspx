<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<%@ Register Src="UCInfo.ascx" TagName="UCInfo" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <%= TempData["Message"]%>
    <br />
    <%using (Html.BeginForm("ReActive", "Employee", FormMethod.Post, new { id = "addEmployee" }))
      {%>
    <uc1:UCInfo ID="UCInfo1" runat="server" />
    <% } %>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PageTitle" runat="server">
    <%=EmsPageInfo.MenuName + CommonPageInfo.AppSepChar+ EmsPageInfo.ModReActivateEmployee + CommonPageInfo.AppSepChar + EmsPageInfo.ComName + CommonPageInfo.AppSepChar + CommonPageInfo.AppName%>
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
        $(document).ready(function () {
            jQuery.validator.addMethod(
            "checkResigned", function (value, element, parameters) {
                var firstDate = Date.parse(value);
                var secondDate = Date.parse($(parameters[0]).val());
                var isValid = false;
                isValid = firstDate > secondDate;
                return isValid;
            }, '<%=string.Format(Resources.Message.E0024, "Start Date", "greater than Resigned Date.")%>');

            $("#DepartmentName").change(function () {
                $("#TitleId").html("");
                $("#DepartmentId").html("");
                var department = $("#DepartmentName").val();
                $("#TitleId").append($("<option value=''><%=Constants.FIRST_ITEM_JOBTITLE%></option>"));
                $("#DepartmentId").append($("<option value=''><%=Constants.FIRST_ITEM_SUB_DEPARTMENT%></option>"));
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
                    LastName: { required: true },
                    FirstName: { required: true },
                    VnFirstName: { required: true },
                    VnLastName: { required: true },
                    NewID: { required: true, number: true, min: 1, remote: {
                        url: "/Employee/CheckIDExits",
                        type: "post",
                        data: {
                            id: function () {
                                return $("#NewID").val();
                            }
                        }
                    }
                    },
                    Startdate: { required: true, checkDate: true, compareExcDate: ["#addEmployee input[name='DOB']", "get", "Start Date", "Date Of Birth"], checkAge: ["#addEmployee input[name='DOB']"] },
                    TitleId: { required: true },
                    DepartmentId: { required: true },
                    IDNumber: { min: 1, number: true },
                    ContractedDate: { checkDate: true, compareExcDate: ["#addEmployee input[name='Startdate']", "get", "Contracted Date", "Start Date"] },
                    IssueDate: { checkDate: true },
                    DOB: { checkDate: true, checkBirthDate: true },
                    PersonalEmail: { email: true },
                    OfficeEmail: { email: true },
                    TaxIssueDate: { checkDate: true }
                },
                submitHandler: function (form) {
                    $("input[type=Submit]").attr('disabled', 'disabled');
                    if ($("#OfficeEmail").val() != "") {
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
                                    $("input[type=Submit]").attr('disabled', '');
                                    return false;
                                }
                                else {
                                    form.submit();
                                }
                            }
                        })
                    }
                    else {
                        form.submit();
                    }
                }
            });
        });        
    </script>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ModuleName" runat="server">
    <%= EmsPageInfo.ComName %>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="FunctionTitle" runat="server">

 <% Employee emp = (Employee)ViewData.Model;
       string funcTitle = string.Empty;
       funcTitle =  "Management » Employee » " +  "<a href='/Employee/EmployeeResignList/'>" + EmsPageInfo.ModResignedEmployees + "</a> » <a href='/Employee/Detail/" + emp.ID + "'>" +
              emp.ID + " - " + emp.FirstName + " " + emp.MiddleName + " " + emp.LastName + "</a> » " + EmsPageInfo.ModReActivateEmployee;         
     %>
    <%= funcTitle%>
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="LoginRoles" runat="server">
</asp:Content>
