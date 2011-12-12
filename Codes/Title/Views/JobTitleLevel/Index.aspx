<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <%= TempData["Message"]%>
    <div id="cactionbutton">
        <button id="btnRefresh" type="button" title="Refresh" class="button refresh">
            Refresh</button>
        <button id="btnExport" type="button" title="Export" class="button export">
            Export</button>        
        <button id="btnDelete" type="button" title="Delete" class="button delete">
            Delete</button>
        <button type="button" id="addnew" title="Add New" class="button addnew">
            Add New</button>
        
    </div>
    <div id="cfilter">
        <table>
            <tr>
                <td>
                    <input type="text" id="txtKeyword" maxlength="100" style="width: 150px" value="<%= (string)ViewData[Constants.JOB_TITLE_LEVEL_NAME] %>"
                        onfocus="ShowOnFocus(this,'<%= Constants.JOB_TITLE_lEVEL  %>')" 
                        onblur="ShowOnBlur(this,'<%= Constants.JOB_TITLE_lEVEL  %>')" autocomplete="off" />
                </td>
                <td>
                    <%=Html.DropDownList("JobTitleId", ViewData[Constants.JOB_TITLE_LEVEL_SELECTION] as SelectList, Constants.FIRST_ITEM_JOBTITLE, new { @style = "width:180px" })%>
                </td>
                <td>
                    <button type="button" id="btnFilter" title="Filter" class="button filter">
                        Filter</button>
                </td>
            </tr>
        </table>
    </div>
    <div class="clist">
        <table id="list" class="scroll" style="width: 1024px">
        </table>
        <div id="pager" class="scroll" style="text-align: center;">
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PageTitle" runat="server">
<%= JobTitlePageInfo.MenuName + CommonPageInfo.AppSepChar + JobTitlePageInfo.List + CommonPageInfo.AppSepChar + CommonPageInfo.AppName%>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
    <script type="text/javascript">
        jQuery(document).ready(function () {
            $("#txtKeyword").autocomplete('/Library/GenericHandle/AutoCompleteHandler.ashx/?Page=JobTitleLevel');
            CRM.onEnterKeyword();
            jQuery("#list").jqGrid({
                url: '/JobTitleLevel/GetListJQGrid/?text=' + encodeURIComponent($("#txtKeyword").val()) + '&jobTitleId=' + $("#JobTitleId").val(),
                datatype: 'json',
                mtype: 'GET',
                colNames: ['ID', 'Title Name', 'Job Title', 'Department', 'Manager', 'Active', 'Action'],
                colModel: [
                  { name: 'ID', index: 'ID', align: "center", width: 50, hidden: true },
                  { name: 'TitleName', index: 'TitleName', align: "left", width: 140, sortable: true },
                  { name: 'JobTitle', index: 'Department', align: "left", width: 140, sortable: true },
                  { name: 'Department', index: 'Department', align: "left", width: 80, sortable: true },
                  { name: 'IsManager', index: 'IsManager', align: "center", width: 50, sortable: true },
                  { name: 'Active', index: 'Active', align: "center", width: 50, sortable: false },
                  { name: 'Action', index: 'Action', editable: false, width: 50, align: 'center', sortable: false}],
                pager: jQuery('#pager'),
                rowList: [20, 30, 50, 100, 200],
                sortname: '<%= (string)ViewData[Constants.JOB_TITLE_LEVEL_COLUMN]%>',
                sortorder: '<%= (string)ViewData[Constants.JOB_TITLE_LEVEL_ORDER]%>',
                rowNum: '<%= (string)ViewData[Constants.JOB_TITLE_LEVEL_ROW_COUNT]%>',
                page: '<%= (string)ViewData[Constants.JOB_TITLE_LEVEL_PAGE_INDEX]%>',
                multiselect: { required: false, width: 15 },
                viewrecords: true,
                width: 1024, height: "auto",
                imgpath: '/scripts/grid/themes/basic/images',
                loadui: 'block'
            });
            $('#addnew').click(function () {
                CRM.popup('/JobTitleLevel/Create', 'Add New', 400);
            });

            //if mask is clicked            
            $("#btnDelete").click(function () {
                CRM.deleteList('#list', 'ID', '/JobTitleLevel/DeleteList');
            });

            $("#btnRefresh").click(function () {
                window.location = "/JobTitleLevel/Refresh";
            });

            $("#btnFilter").click(function () {
                var url_send = '/JobTitleLevel/GetListJQGrid/?text=' + encodeURIComponent($("#txtKeyword").val()) + '&jobTitleID=' + $("#JobTitleId").val();
                $('#list').setGridParam({ page: 1, url: url_send });
                $("#list").trigger('reloadGrid');
            });

            $("#btnExport").click(function () {
                CRM.loading();
                var numberRow = $("#list").getGridParam("records");
                if (numberRow <= 0) {
                    CRM.msgBox('<%= Resources.Message.E0027 %>', "300");
                }
                else {
                    window.location = "/JobTitleLevel/ExportToExcel";
                }
                CRM.completed();
            });
        });        
    </script>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ModuleName" runat="server">
<%=JobTitlePageInfo.ComName%>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="FunctionTitle" runat="server">
<%=CommonFunc.GetCurrentMenu(Request.RawUrl,true)%>
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="LoginRoles" runat="server">
</asp:Content>
