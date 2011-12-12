<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="PageTitle" runat="server">
<%= EmsPageInfo.MenuName + CommonPageInfo.AppSepChar + STTPageInfo.List + CommonPageInfo.AppSepChar + EmsPageInfo.ComName + CommonPageInfo.AppSepChar + CommonPageInfo.AppName %>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ModuleName" runat="server">
   <%= EmsPageInfo.ComName %>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="FunctionTitle" runat="server">
    <%= CommonFunc.GetCurrentMenu(Request.RawUrl,true)%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <%= TempData["Message"]%>
    <div id="cactionbutton">
        <button  id="btnRefresh" type="button" title="Refresh" class="button refresh">Refresh</button> 
        <button  id="btnExport" type="button" title="Export" class="button export">Export</button> 
        <button  id="btnDelete" type="button" title="Delete" class="button delete">Delete</button> 
        <button  id="btnAddNew" type="button" title="Add New" class="button addnew">Add New</button>
    </div>
    <div id="cfilter">
        <table border="0" cellpadding="0" cellspacing="0">
            <tr>
                <td>
                    <input type="text" maxlength="100" style="width: 150px" value="<%= (string)ViewData[Constants.STT_LIST_NAME]%>"
                        id="txtKeyword" onfocus="ShowOnFocus(this,'<%= Constants.FULLNAME_OR_USERID  %>');"
                        onblur="ShowOnBlur(this,'<%= Constants.FULLNAME_OR_USERID  %>');" autocomplete="off" />
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
                    <%=Html.DropDownList("Cls", ViewData[Constants.STT_LIST_CLASS] as SelectList, Constants.FIRST_ITEM_CLASS)%>
                </td>
                <td>
                    <%=Html.DropDownList("StatusId", ViewData[Constants.STT_LIST_STATUS] as SelectList, Constants.FIRST_ITEM_STATUS)%>
                </td>
                <td>
                    <%=Html.DropDownList("ResultId", ViewData[Constants.STT_LIST_RESULT] as SelectList, Constants.FIRST_ITEM_RESULT, new { @style = "width:180px" })%>
                </td>
                <td>
                    <button type="button" class="icon plus" id="expand" title="Advanced Search"></button>
                </td>
                <td>
                    <button  id="btnFilter" type="button" title="Filter" class="button filter">Filter</button>
                </td>
            </tr>
        </table>
        <table id="expandFilter" style="display: none">
            <tr height="35">
                <td>
                    Start Date :
                </td>
                <td>
                    From
                </td>
                <td>
                    <% Response.Write(Html.TextBox("FromStartdate", (string)ViewData[Constants.STT_LIST_STARTDATE_FROM], new { @style = "width:80px" }));%>
                </td>
                <td>
                    To
                </td>
                <td>
                    <% Response.Write(Html.TextBox("ToStartdate", (string)ViewData[Constants.STT_LIST_STARTDATE_TO], new { @style = "width:80px" })); %>
                </td>
                <td>
                    Expected End Date :
                </td>
                <td>
                    From
                </td>
                <td>
                    <% Response.Write(Html.TextBox("FromEnddate", (string)ViewData[Constants.STT_LIST_FROMDATE_FROM], new { @style = "width:80px" }));%>
                </td>
                <td>
                    To
                </td>
                <td>
                    <%  Response.Write(Html.TextBox("ToEnddate", (string)ViewData[Constants.STT_LIST_FROMDATE_TO], new { @style = "width:80px" })); %>
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
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
<style type="text/css">
 
    .ac_results
    {
        width:250px !important;
    }
</style>
    <script type="text/javascript">
        $(document).ready(function () {
            if ($("#FromStartdate").val() != "" || $("#ToStartdate").val() != "" || $("#FromEnddate").val() != "" || $("#ToEnddate").val() != "") {
                $('#expandFilter').css("display", "");
                $("#expand").attr("class", "icon minus");
            }
            CRM.onEnterKeyword();
            
            if ($("#LocationOffice").val() == '') {
                $("#LocationFloor").attr("disabled", "true");
            }

            $("#FromStartdate").datepicker();
            $("#ToStartdate").datepicker();
            $("#FromEnddate").datepicker();
            $("#ToEnddate").datepicker();
            $("#expand").click(function () {
                var styl = $("#expand").attr("class");
                if (styl == "icon minus") {
                    $('#expandFilter').css("display", "none");
                    $("#expand").attr("class", "icon plus");
                    $("#FromStartdate").attr("value", "");
                    $("#ToStartdate").attr("value", "");
                    $("#FromEnddate").attr("value", "");
                    $("#ToEnddate").attr("value", "");
                }
                else {
                    $('#expandFilter').css("display", "");
                    $("#expand").attr("class", "icon minus");
                }
            });
            //$("#txtKeyword").autocomplete('Library/GenericHandle/AutoCompleteHandler.ashx/?Page=STT', {employee:true});
            jQuery("#list").jqGrid({
                url: '/STT/GetListJQGrid?name=' + encodeURIComponent($('#txtKeyword').val())
                            + '&branch=' + $('#LocationBranch').val() + '&office=' + $('#LocationOffice').val() + '&floor=' + $('#LocationFloor').val()
                            + '&cls=' + $('#Cls').val() + '&statusId=' + $('#StatusId').val() + '&resultId=' + $('#ResultId').val()
                            + '&startDateBegin=' + $('#FromStartdate').val() + '&startDateEnd=' + $('#ToStartdate').val()
                            + '&endDateBegin=' + $('#FromEnddate').val() + '&endDateEnd=' + $('#ToEnddate').val(),
                datatype: 'json',
                contentType: "application/json; charset=utf-8",
                colNames: ['STT Id', 'Full Name', 'Status', 'Start Date', 'Expected End Date', 'Result', 'Remark', 'Action'],
                colModel: [
                  { name: 'Id', index: 'ID', align: "center", width: 50, title: false },
                  { name: 'DisplayName', index: 'DisplayName', align: "left", sortable: true, title: false },
                  { name: 'Status', index: 'Status', align: "center", width: 70, title: false },
                  { name: 'StartDate', index: 'StartDate', align: "center", width: 80, title: false },
                  { name: 'ExpectedEndDate', index: 'ExpectedEndDate', align: "center", width: 80, title: false },
                  { name: 'Result', align: "center", width: 60, title: false, sortable: false },
                  { name: 'Remark', align: "left", width: 150, title: false, sortable: false },
                  { name: 'Action', align: "center", index: 'Action', width: 50, sortable: false }
                  ],
                rowList: [20, 30, 50, 100, 200],
                width: 1200,
                height: "100%",
                pager: 'pager',
                sortname: '<%= (string)ViewData[Constants.STT_LIST_COLUMN]%>',
                sortorder: '<%= (string)ViewData[Constants.STT_LIST_ORDER]%>',
                rowNum: '<%= (string)ViewData[Constants.STT_LIST_ROW_COUNT]%>',
                page: '<%= (string)ViewData[Constants.STT_LIST_PAGE_INDEX]%>',
                viewrecords: true,
                multiselect: true,
                
                loadui: 'block',
                loadComplete: function () {
                    ShowTooltip($("a[class=showTooltip]"), $("#shareit-box"), "/STT/STTToolTip");

                }
            });
            $("#btnDelete").click(function () {
                CRM.deleteList('#list', 'Id', '/STT/DeleteList');
            });
            $("#btnRefresh").click(function () {
                window.location = "/STT/Refresh";
            });
            $("#btnAddNew").click(function () {
                window.location = "/STT/Create";
            });
            $("#btnFilter").click(function () {
                var isValid = true;
                if ($('#FromStartdate').val() != '') {
                    if (!isDate($('#FromStartdate').val())) {
                        alert("Start Date From is invalid.");
                        isValid = false;
                    }
                }
                if ($('#ToStartdate').val() != '') {
                    if (!isDate($('#ToStartdate').val())) {
                        alert("Start Date To is invalid.");
                        isValid = false;
                    }
                }
                if ($('#FromEnddate').val() != '') {
                    if (!isDate($('#FromEnddate').val())) {
                        alert("End Date From is invalid.");
                        isValid = false;
                    }
                }
                if ($('#ToEnddate').val() != '') {
                    if (!isDate($('#ToEnddate').val())) {
                        alert("End Date To is invalid.");
                        isValid = false;
                    }
                }
                if (isValid) {
                    var name = $('#txtKeyword').val();
                    $('#list').setGridParam({ url: '/STT/GetListJQGrid?name=' + encodeURIComponent(name)
                                            + '&branch=' + $('#LocationBranch').val() + '&office=' + $('#LocationOffice').val() + '&floor=' + $('#LocationFloor').val()
                                            + '&cls=' + $('#Cls').val() + '&statusId=' + $('#StatusId').val() + '&resultId=' + $('#ResultId').val()
                                            + '&startDateBegin=' + $('#FromStartdate').val() + '&startDateEnd=' + $('#ToStartdate').val()
                                            + '&endDateBegin=' + $('#FromEnddate').val() + '&endDateEnd=' + $('#ToEnddate').val()
                    }).trigger('reloadGrid');
                }
            });

            $("#btnExport").click(function () {
                var numberRow = $("#list").getGridParam("records");
                if (numberRow <= 0) {
                    CRM.msgBox('<%= Resources.Message.E0027 %>', "300");
                }
                else {
                    var name = $('#txtKeyword').val();
                    window.location = "/STT/ExportToExcel";
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
        });
    </script>
    <link rel="stylesheet" type="text/css" href="/Content/Css/tooltip.css" />
    <script src="/Scripts/Tooltip.js" type="text/javascript"></script>
</asp:Content>
