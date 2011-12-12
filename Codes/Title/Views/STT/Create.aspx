<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<%@ Register Src="UCInfo.ascx" TagName="UCInfo" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <%= ViewData["Message"]%>
    <br />
    <%using (Html.BeginForm("Create", "STT", FormMethod.Post, new { id = "addSTT", @class = "form" }))
      {%>
    <uc1:UCInfo ID="UCInfo1" runat="server" />
    <% } %>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PageTitle" runat="server">
<%= STTPageInfo.MenuName + CommonPageInfo.AppSepChar + STTPageInfo.Create + CommonPageInfo.AppSepChar + EmsPageInfo.ComName + CommonPageInfo.AppSepChar + CommonPageInfo.AppName%>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
    <script src="/Scripts/jquery.maskedinput-1.2.2.js" type="text/javascript"></script>
    <script type="text/javascript">
        jQuery(function ($) {
            $("#ID").mask("aaa999-999");
        });
        var i = 0;
        $(document).ready(function () {

            $("#DepartmentName").change(function () {
                $("#DepartmentId").html("");
                var department = $("#DepartmentName").val();
                $("#DepartmentId").append($("<option value=''><%= Constants.FIRST_ITEM_SUB_DEPARTMENT %></option>"));
                if (department != 0) {
                    $.getJSON('/Library/GenericHandle/DropdownListHandler.ashx?ID=' + department + '&Page=SubDepartment', function (item) {
                        $.each(item, function () {
                            if (this['ID'] != undefined) {
                                $("#DepartmentId").append($("<option value='" + this['ID'] + "'>" + this['Name'] + "</option>"));
                            }
                        });
                    });
                }
            });



            $("#addSTT").validate({
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
                    ID: { required: true, remote: {
                        url: "/STT/CheckIDExits",
                        type: "post",
                        data: {
                            id: function () {
                                return $("#ID").val();
                            }
                        }
                    }
                    },
                    LastName: { required: true },
                    FirstName: { required: true },
                    VnFirstName: { required: true },
                    VnLastName: { required: true },
                    Startdate: { required: true, checkDate: true, compareExcDate: ["#addSTT input[name='DOB']", "get", "Start Date", "Date Of Birth"], checkAge: ["#addSTT input[name='DOB']"] },
                    TitleId: { required: true },
                    DepartmentId: { required: true },
                    IDNumber: { min: 1, number: true },
                    ExpectedEndDate: { checkDate: true, compareExcDate: ["#addSTT input[name='Startdate']", "get", "Contracted Date", "Start Date"] },
                    IssueDate: { checkDate: true },
                    DOB: { checkDate: true, checkBirthDate: true },
                    PersonalEmail: { email: true },
                    OfficeEmail: { email: true },
                    TaxIssueDate: { checkDate: true },
                    STTStatusId: { required: true },
                    JR: {
                    number: true,
                    remote: {
                        url: '/Employee/CheckJRExits',
                        data: {
                            jr: function () {
                                return $('#JR').val();
                            },
                            empID: function () {
                                return '';
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
                                    'email': $('#OfficeEmail').val()
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
    <%= EmsPageInfo.ComName %>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="FunctionTitle" runat="server">
<%= CommonFunc.GetCurrentMenu(Request.RawUrl) +
        STTPageInfo.Create  %>
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="LoginRoles" runat="server">
</asp:Content>
