<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="PageTitle" runat="server">
    <%= EmsPageInfo.MenuName + CommonPageInfo.AppSepChar + EmsPageInfo.ModActiveEmployees + CommonPageInfo.AppSepChar + EmsPageInfo.ComName + CommonPageInfo.AppSepChar + CommonPageInfo.AppName%>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ModuleName" runat="server">
    <%= EmsPageInfo.ComName%>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="FunctionTitle" runat="server">
    <%-- <%=EmsPageInfo.MenuName + CommonPageInfo.AppDetailSepChar + EmsPageInfo.ModActiveEmployees%>--%>
    <%=CommonFunc.GetCurrentMenu(Request.RawUrl,true)%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <%= TempData["Message"]%>
    <div id="cactionbutton">
        <button id="btnRefresh" type="button" title="Refresh" class="button refresh">
            Refresh</button>
        <button id="btnExport" type="button" title="Export" class="button export">
            Export</button>
        <button id="btnDelete" type="button" title="Delete" class="button delete">
            Delete</button>
        <button id="btnAddNew" type="button" title="Add New" class="button addnew">
            Add New</button>
    </div>
    <div id="cfilter">
        <table border="0" cellpadding="0" cellspacing="0">
            <tr>
                <td>                    
                    <!-- tan.tran: add attribute 'autocomplete="off"' to avoid bug when click on back button -->
                    <input type="text" maxlength="100" style="width: 150px" value="<%= (string)ViewData[Constants.EMPLOYEE_LIST_NAME] %>"
                        id="txtKeyword" onfocus="ShowOnFocus(this,'<%= Constants.FULLNAME_OR_USERID  %>')"
                        onblur="ShowOnBlur(this,'<%= Constants.FULLNAME_OR_USERID  %>')" autocomplete="off" />
                </td>
                <td>
                    <%=Html.DropDownList("LocationBranch", ViewData[Constants.LOCATION_LIST_BRANCH] as SelectList, Constants.FIRST_ITEM_BRANCH, new { @style = "width:160px" })%>
                </td>
                <td>
                    <%=Html.DropDownList("LocationOffice", ViewData[Constants.LOCATION_LIST_OFFICE] as SelectList, Constants.FIRST_ITEM_OFFICE, new { @style = "width:160px" })%>
                </td>
                <td>
                    <%=Html.DropDownList("LocationFloor", ViewData[Constants.LOCATION_LIST_FLOOR] as SelectList, Constants.FIRST_ITEM_FLOOR, new { @style = "width:115px" })%>
                </td>
                <td>
                    <%=Html.DropDownList("DepartmentName", ViewData[Constants.EMPLOYEE_LIST_DEPARTMENT] as SelectList, Constants.FIRST_ITEM_DEPARTMENT)%>
                </td>
                <td>
                    <%=Html.DropDownList("DepartmentId", ViewData[Constants.EMPLOYEE_LIST_SUB_DEPARTMENT] as SelectList, Constants.FIRST_ITEM_SUB_DEPARTMENT, new { @style = "width:180px" })%>
                </td>
                <td>
                    <%=Html.DropDownList("TitleId", ViewData[Constants.EMPLOYEE_LIST_JOB_TITLE] as SelectList, Constants.FIRST_ITEM_JOBTITLE, new { @style = "width:180px" })%>
                </td>
                <td>
                    <button type="button" id="btnFilter" title="Filter" class="button filter">
                        Filter</button>
                </td>
            </tr>
        </table>
    </div>
    <div class="clist">
        <table id="list" class="scroll">
        </table>
        <div id="pager" class="scroll" style="text-align: center;">
        </div>
    </div>
    <div id="shareit-box">
        <img src='../../Content/Images/loading3.gif' alt='' />
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
        .ac_results
        {
            width: 270px !important;
        }
    </style>
    <script type="text/javascript">
        $(document).ready(function () {
            //$("#txtKeyword").autocomplete('/Library/GenericHandle/AutoCompleteHandler.ashx/?Page=Employee' + '&IsActive=1', {employee:true});
            CRM.onEnterKeyword();
            if ($("#LocationOffice").val() == '') {
                $("#LocationFloor").attr("disabled", "true");
            }
            jQuery("#list").jqGrid({
                url: '/Employee/GetListJQGrid?name=' + encodeURIComponent($('#txtKeyword').val())
                     + '&department=' + $('#DepartmentName').val() + '&subDepartment=' + $('#DepartmentId').val() + '&titleId=' + $('#TitleId').val()
                     + '&branch=' + $('#LocationBranch').val() + '&office=' + $('#LocationOffice').val() + '&floor=' + $('#LocationFloor').val(),
                datatype: 'json',
                colNames: ['Id', 'Full Name', 'Job Title', 'Department', 'Sub Department', 'Start Date', 'Status', 'Action'],
                colModel: [
                  { name: 'Id', index: 'ID', align: "center", width: 30, title: false },
                  { name: 'DisplayName', index: 'DisplayName', align: "left", sortable: true, title: false },
                  { name: 'JobTitle', index: 'JobTitle', align: "left", width: 140, title: false },
                  { name: 'Department', index: 'Department', align: "left", width: 100, title: false },
                  { name: 'SubDepartment', index: 'SubDepartment', align: "left", width: 100, title: false },
                  { name: 'StartDate', index: 'StartDate', align: "center", width: 75, title: false },
                  { name: 'Status', index: 'Status', align: "center", width: 65, title: false },
                  { name: 'Action', index: 'Action', align: "center", width: 70, sortable: false }
                  ],
                rowList: [20, 30, 50, 100, 200],
                width: 1200,
                height: "100%",
                pager: '#pager',
                sortname: '<%= (string)ViewData[Constants.EMPLOYEE_LIST_COLUMN]%>',
                sortorder: '<%= (string)ViewData[Constants.EMPLOYEE_LIST_ORDER]%>',
                rowNum: '<%= (string)ViewData[Constants.EMPLOYEE_LIST_ROW_COUNT]%>',
                page: '<%= (string)ViewData[Constants.EMPLOYEE_LIST_PAGE_INDEX]%>',
                viewrecords: true,
                sortorder: "asc",
                multiselect: true,
                loadui: 'block',
                loadComplete: function () {
                    ShowTooltip($("a[class=showTooltip]"), $("#shareit-box"), "/Employee/EmployeeToolTip");
                }
            });

            $("#LocationBranch").change(function () {
                var selectedBranch = $(this).val();
                $("#LocationFloor").attr("disabled", "true");
                var url = "/Common/BranchListOnChange?branchID=" + selectedBranch;
                $.ajax({
                    async: false,
                    cache: false,
                    type: "GET",
                    dataType: "json",
                    timeout: 1000,
                    url: url,
                    error: function () {
                        CRM.message("error", "block", "msgError");
                    },
                    success: function (data) {
                        if (Boolean(data.success)) {
                            var newOptionTemplate = "<option value='{0}'>{1}</option>";
                            var newOption = "";
                            $("#LocationOffice").html(
                                $.format(newOptionTemplate, "", "<%=Constants.FIRST_ITEM_OFFICE%>"));
                            $("#LocationFloor").html(
                                $.format(newOptionTemplate, "", "<%=Constants.FIRST_ITEM_FLOOR%>"));
                            $.each(data.offices, function () {
                                newOption = $.format(newOptionTemplate, this["ID"], this["Name"]);
                                $("#LocationOffice").append(newOption);
                            });
                            $.each(data.floors, function () {
                                newOption = $.format(newOptionTemplate, this["ID"], this["Name"]);
                                $("#LocationFloor").append(newOption);
                            });
                        }
                        else {
                            CRM.message("error", "block", "msgError");
                        }
                    }
                });
            });
            $("#LocationOffice").change(function () {
                var selectedBranch = $("#LocationBranch").val();
                var selectedOffice = $(this).val();
                if (selectedOffice == '') {
                    $("#LocationFloor").attr("disabled", "true");
                }
                else {
                    $("#LocationFloor").removeAttr("disabled");
                }
                var url = "/Common/OfficeListOnChange?branchID=" + selectedBranch + "&officeID=" + selectedOffice;
                $.ajax({
                    async: false,
                    cache: false,
                    type: "GET",
                    dataType: "json",
                    timeout: 1000,
                    url: url,
                    error: function () {
                        CRM.message("error", "block", "msgError");
                    },
                    success: function (data) {
                        if (Boolean(data.success)) {
                            var newOptionTemplate = "<option value='{0}'>{1}</option>";
                            var newOption = "";
                            $("#LocationFloor").html(
                                $.format(newOptionTemplate, "", "<%=Constants.FIRST_ITEM_FLOOR%>"));
                            $.each(data.floors, function () {
                                newOption = $.format(newOptionTemplate, this["ID"], this["Name"]);
                                $("#LocationFloor").append(newOption);
                            });
                        }
                        else {
                            CRM.message("error", "block", "msgError");
                        }
                    }
                });
            });

            $("#DepartmentName").change(function () {
                $("#TitleId").html("");
                $("#DepartmentId").html("");
                var department = $("#DepartmentName").val();
                $("#TitleId").append($("<option value=''>" + '<%= Constants.FIRST_ITEM_JOBTITLE %>' + "</option>"));
                $("#DepartmentId").append($("<option value=''>" + '<%= Constants.FIRST_ITEM_SUB_DEPARTMENT %>' + "</option>"));
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
            });

            $("#btnRefresh").click(function () {
                window.location = "/Employee/Refresh";
            });

            $("#btnAddNew").click(function () {
                window.location = "/Employee/Create";
            });

            $("#btnDelete").click(function () {
                CRM.deleteList('#list', 'Id', '/Employee/DeleteList');
            });

            $("#btnFilter").click(function () {
                var name = $('#txtKeyword').val();
                $('#list').setGridParam({ url: '/Employee/GetListJQGrid?name=' + encodeURIComponent(name)
                     + '&department=' + $('#DepartmentName').val() + '&subDepartment=' + $('#DepartmentId').val() + '&titleId=' + $('#TitleId').val()
                     + '&branch=' + $('#LocationBranch').val() + '&office=' + $('#LocationOffice').val() + '&floor=' + $('#LocationFloor').val()
                }).trigger('reloadGrid');
            });

            $("#btnExport").click(function () {
                CRM.loading();
                var numberRow = $("#list").getGridParam("records");
                if (numberRow <= 0) {
                    CRM.msgBox('<%= Resources.Message.E0027 %>', "300");
                }
                else {
                    window.location = "/Employee/ExportToExcel/?Active=" + '<%=Constants.EMPLOYEE_ACTIVE %>';
                }
                CRM.completed();
            });
        });
    </script>
    <link rel="stylesheet" type="text/css" href="/Content/Css/tooltip.css" />
    <script src="/Scripts/Tooltip.js" type="text/javascript"></script>
</asp:Content>
