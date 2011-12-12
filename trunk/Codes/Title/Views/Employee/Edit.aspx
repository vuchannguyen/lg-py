<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<%@ Register Src="UCInfo.ascx" TagName="UCInfo" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <%= TempData["Message"]%>
    <br />
    <%using (Html.BeginForm("Edit", "Employee", FormMethod.Post, new { id = "addEmployee" }))
      {%>
    <uc1:UCInfo ID="UCInfo1" runat="server" />
    <% } %>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PageTitle" runat="server">
    <%=EmsPageInfo.MenuName + CommonPageInfo.AppSepChar + EmsPageInfo.FuncEditEmployee + CommonPageInfo.AppSepChar + EmsPageInfo.ComName + CommonPageInfo.AppSepChar + CommonPageInfo.AppName%>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
    <script type="text/javascript">
        var i = 0;
        function ChooseHospital(id) {
            CRM.closePopup();
            $("#InsuranceHospitalID").attr('value', ($("#" + id).attr('id')));
            $("#InsuranceName").attr('value', ($("#" + id).text()));
            $("#InsuranceName").attr('title', ($("#" + id).text()));
            SetRowHospital();
        }
        function SetRowHospital() {
            if ($("#InsuranceHospitalID").val() != '<%=((CRM.Models.Employee)ViewData.Model).InsuranceHospitalID %>') {
                $("#trHospitalEffectDate").css("display", "");
                $("#hospitalEffectDate").rules("add", "required");
            } else {
                $("#trHospitalEffectDate").css("display", "none");
                $("#hospitalEffectDate").val("");
                $("#hospitalEffectDate").rules("remove");
            }
        }
        function RemoveHospital() {
            $("#InsuranceName").attr('value', '');
            $("#InsuranceHospitalID").attr('value', '');
            $("#trHospitalEffectDate").css("display", "none");
            $("#hospitalEffectDate").val("");
            $("#hospitalEffectDate").rules("remove");
        }
        $(document).ready(function () {
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

            $("#DepartmentId").change(function () {
                // Display textbox DepartEffectDate when change value of Department                                
                if ($("#DepartmentId").val() != '<%=((CRM.Models.Employee)ViewData.Model).DepartmentId %>') {
                    $("#trdepartEffectDate").css("display", "");
                    $("#departEffectDate").rules("add", "required");
                } else {
                    $("#trdepartEffectDate").css("display", "none");
                    $("#departEffectDate").val("");
                    $("#departEffectDate").rules("remove");
                }
            });

            $("#TitleId").change(function () {
                // Display textbox TitleEffectDate when change value of Title
                if ($("#TitleId").val() != '<%=((CRM.Models.Employee)ViewData.Model).TitleId %>') {
                    $("#trTitleEffectDate").css("display", "");
                    $("#titleEffectDate").rules("add", "required");
                } else {
                    $("#trTitleEffectDate").css("display", "none");
                    $("#titleEffectDate").val("");
                    $("#titleEffectDate").rules("remove");
                }
            });
            $.validator.addMethod('idNumber', function (value, element) {
                if (value.length == 0)
                    return true;
                return /^[a-z0-9]+$/i.test(value);
            }, "Space and special characters are not allowed!");
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
                    Startdate: { required: true, checkDate: true, compareExcDate: ["#addEmployee input[name='DOB']", "get", "Start Date", "Date Of Birth"], checkAge: ["#addEmployee input[name='DOB']"] },
                    TitleId: { required: true },
                    DepartmentId: { required: true },
                    IDNumber: { idNumber: true, maxlength: '<%=CommonFunc.GetLengthLimit(new Employee(), "IDNumber")%>' },
                    ContractedDate: { checkDate: true, compareExcDate: ["#addEmployee input[name='Startdate']", "get", "Contracted Date", "Start Date"] },
                    IssueDate: { checkDate: true },
                    DOB: { checkDate: true, checkBirthDate: true },
                    PersonalEmail: { email: true },
                    OfficeEmail: { email: true },
                    TaxIssueDate: { checkDate: true },
                    departEffectDate: {
                        checkDate: true,
                        remote: {
                            url: "/Employee/CheckDepartEffectiveDate",
                            type: "post",
                            data: {
                                empId: function () {
                                    return $("#ID").val();
                                },
                                effectDate: function () {
                                    return $("#departEffectDate").val();
                                },
                                actionName: 'Department'
                            }
                        }

                    },
                    titleEffectDate: {
                        checkDate: true,
                        remote: {
                            url: "/Employee/CheckDepartEffectiveDate",
                            type: "post",
                            data: {
                                empId: function () {
                                    return $("#ID").val();
                                },
                                effectDate: function () {
                                    return $("#titleEffectDate").val();
                                },
                                actionName: 'JobTitle'
                            }
                        }

                    },
                    hospitalEffectDate: {
                        checkDate: true,
                        remote: {
                            url: "/Employee/CheckInsuranceHospitalEffectiveDate",
                            type: "post",
                            data: {
                                empId: function () {
                                    return $("#ID").val();
                                },
                                effectDate: function () {
                                    return $("#hospitalEffectDate").val();
                                }
                            }
                        }

                    },
                    Resigned_Date: {
                        required: true,
                        checkDate: true,
                        compareDate: ["#Startdate", "get", "Resigned Date", "Start Date"]
                    },
                    ResignedAllowance: {
                        number: true,
                        min: 10
                    },
                    ResignedReason: {
                        maxlength: 500
                    },
                    JR: {
                        remote: {
                            url: "/Employee/CheckJRExits",
                            type: "post",
                            data: {
                                jr: function () {
                                    return $("#JR").val();
                                },
                                empID: function () {
                                    return $("#ID").val();
                                }
                            }
                        }
                    }
                },
                submitHandler: function (form) {
                    if (i == 0) {
                        if ($("#OfficeEmail").val() != "") {
                            jQuery.ajax({
                                url: "/Employee/CheckEmailExits",
                                type: "POST",
                                datatype: "json",
                                data: ({
                                    'email': $('#OfficeEmail').val(),
                                    'id': $('#ID').val()
                                }),
                                success: function (mess) {
                                    if (mess.MsgType == 1) {
                                        CRM.message(mess.MsgText, 'block', 'msgError');
                                        i--;
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
                        i++;
                    }
                }
            });
        });        
    </script>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ModuleName" runat="server">
    <%=EmsPageInfo.ComName%>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="FunctionTitle" runat="server">
    <% Employee emp = (Employee)ViewData.Model;
       string name = emp.ID + " - " + emp.FirstName + " " + emp.MiddleName + " " + emp.LastName;
      // string modName = EmsPageInfo.ModResignedEmployees;
       //string urlList = "/Employee/EmployeeResignList/";
       if (emp.EmpStatusId != Constants.RESIGNED)
       {
           Response.Write(CommonFunc.GetCurrentMenu(Request.RawUrl) + "<a href='/Employee/Detail/" + emp.ID + "'>" + name + "</a> " + CommonPageInfo.AppDetailSepChar + " " + EmsPageInfo.FuncEditEmployee);
       }
       else
       {
           Response.Write("Management » Employee » <a href='/Employee/EmployeeResignList/'>Resigned List</a>" +
               " » <a href='/Employee/Detail/" + emp.ID + "'>" + name + "</a> " + CommonPageInfo.AppDetailSepChar + " " + EmsPageInfo.FuncEditEmployee);
       }
    %>
    <%-- <%=EmsPageInfo.MenuName + CommonPageInfo.AppDetailSepChar%>--%>
   <%-- <%= CommonFunc.GetCurrentMenu(Request.RawUrl)%><a href='/Employee/Detail/<%=emp.ID %>'>
        <%= name%>
    </a>»
    <%=EmsPageInfo.FuncEditEmployee%>--%>
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="LoginRoles" runat="server">
</asp:Content>
