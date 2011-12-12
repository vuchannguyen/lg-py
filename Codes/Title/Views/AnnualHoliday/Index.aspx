<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <%= TempData[CommonDataKey.TEMP_SYSTEM_MESSAGE]%>
    <div id="cactionbutton">
        <button id="btnRefresh" type="button" title="Refresh" class="button refresh">Refresh</button>
        <button id="btnExport" type="button" title="Export" class="button export">Export to Excel</button>
        <button type="button" id="btnDelete" title="Delete" class="button delete">Delete</button>
        <button type="button" id="btnAddNew" title="Add New" class="button addnew">Add New</button>
    </div>
    <div id="cfilter">
        <table border="0" cellpadding="0" cellspacing="0">
            <tr>
                <td>
                    <input type="text" class="keyword" value="<%= (string)ViewData[Constants.ANNUAL_HOLIDAY_TEXT] %>"
                        id="txtKeyword"  onfocus="ShowOnFocus(this,'<%=Constants.ANNUAL_HOLIDAY_NAME  %>')" 
                        onblur="ShowOnBlur(this,'<%=Constants.ANNUAL_HOLIDAY_NAME  %>')" autocomplete="off"  />
                    <input type = "hidden" id="hidFilteredName" value=""/>
                </td>
                <td>
                    <%= Html.DropDownList("Year", ViewData[Constants.ANNUAL_HOLIDAY_YEAR] as SelectList, Constants.FIRST_ITEM_YEAR)%>
                    <input type = "hidden" id="hidFilteredYear" value=""/>
                </td>
                <td>
                    <button type="button" id="btnFilter" title="Filter" class="button filter">Filter</button>
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
    <div id="divEdit" style="display: none; cursor: default;">
    </div>
</asp:Content>


<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
      .ui-jqgrid tr.jqgrow td {white-space: normal;}
    </style>
    <script src="/Scripts/jquery.validate.js" type="text/javascript"></script>    
    <script type="text/javascript">

        jQuery(document).ready(function () {            
            CRM.onEnterKeyword();
            jQuery("#list").jqGrid({
                url: '/AnnualHoliday/GetListJQGrid/?name=' + encodeURIComponent($('#txtKeyword').val()) + '&year=' + $('#Year').val(),
                datatype: 'json',
                scroll: 0,
                mtype: 'GET',
                colNames: ['ID', 'Name', 'Day', 'Date', 'Description', 'Action'],
                colModel: [
                  { name: 'ID', index: 'ID', align: "center", width: 0, hidden: true },
                  { name: 'HolidayName', index: 'HolidayName', align: "justify", width: 300, sortable: true },
                  { name: 'HolidayDay', index: 'HolidayDay', align: "center", width: 150, sortable: true },
                  { name: 'HolidayDate', index: 'HolidayDate', align: "center", width: 150, sortable: true },
                  { name: 'Description', index: 'Description', editable: false, width: 300, sortable: true },
                  { name: 'Action', index: 'Action', editable: false, width: 100, align: 'center', sortable: false}],
                pager: jQuery('#pager'),
                rowList: [20, 30, 50, 100, 200],
                sortname: '<%= (string)ViewData[Constants.ANNUAL_HOLIDAY_COLUMN]%>',
                sortorder: '<%= (string)ViewData[Constants.ANNUAL_HOLIDAY_ORDER]%>',
                rowNum: '<%= (string)ViewData[Constants.ANNUAL_HOLIDAY_ROW_COUNT]%>',
                page: '<%= (string)ViewData[Constants.ANNUAL_HOLIDAY_PAGE_INDEX]%>',
                multiselect: { required: false, width: 24 },
                viewrecords: true,
                width: 1024, height: "auto",
                imgpath: '/scripts/grid/themes/basic/images',
                loadui: 'block'
            });
            $('#btnAddNew').click(function () {
                CRM.popup("/AnnualHoliday/Create", "Add New Holiday", 500);
            });
            $("#btnDelete").click(function () {
                CRM.deleteList('#list', 'ID', '/AnnualHoliday/DeleteList');
            });
            $("#btnRefresh").click(function () {
                window.location = "/AnnualHoliday/Refresh";
            });
            $("#btnExport").click(function () {
                if ($("#list tr").length < 2) {
                    CRM.msgBox(CRM.format(E0027), 300);
                }
                else {
                    var name = $("#hidFilteredName").val();
                    var year = $("#hidFilteredYear").val();
                    var sortName = $('#list').jqGrid('getGridParam', 'sortname');
                    var sortOrder = $('#list').jqGrid('getGridParam', 'sortorder');
                    var targetUrl = '/AnnualHoliday/Export?name=' + name
                    + '&year=' + year + "&sortname=" + sortName + "&sortorder=" + sortOrder;
                    window.location = targetUrl;
                }
            });
            $("#btnFilter").click(function () {
                var name = encodeURIComponent($('#txtKeyword').val());
                if (name == encodeURIComponent('<%= Constants.ANNUAL_HOLIDAY_NAME  %>')) {
                    name = "";
                }
                var year = $('#Year').val();
                var targetUrl = '/AnnualHoliday/GetListJQGrid?name=' + name
                    + '&year=' + year;
                $('#list').setGridParam({ page: 1, url: targetUrl }).trigger('reloadGrid');
                $("#hidFilteredName").val(name);
                $("#hidFilteredYear").val(year);
            });
        });        
    </script>
</asp:Content>

<asp:Content ID="Content7" ContentPlaceHolderID="PageTitle" runat="server">
    <%= PTOPageInfo.MenuName + CommonPageInfo.AppSepChar + PTOPageInfo.AnnualHoliday + CommonPageInfo.AppSepChar+ PTOPageInfo.ComName + CommonPageInfo.AppSepChar + CommonPageInfo.AppName%>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ModuleName" runat="server">
    <%= PTOPageInfo.ComName%>
</asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="FunctionTitle" runat="server">
    <%--<%=PTOPageInfo.MenuName + CommonPageInfo.AppDetailSepChar + PTOPageInfo.AnnualHoliday%>--%>
    <%= CommonFunc.GetCurrentMenu(Request.RawUrl,true)%>
</asp:Content>

<asp:Content ID="Content6" ContentPlaceHolderID="LoginRoles" runat="server">
</asp:Content>
