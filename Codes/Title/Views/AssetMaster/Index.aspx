<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="PageTitle" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ModuleName" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="FunctionTitle" runat="server">
</asp:Content>

<asp:Content ID="Content6" ContentPlaceHolderID="MainContent" runat="server">
 <%= TempData["Message"]%>
    <div id="cactionbutton">
        <button id="btnRefesh" type="button" title="Refresh" class="button refresh">Refresh</button> 
        <button id="btnExport" type="button" title="Export" class="button export">Export</button> 
        <button id="btnDelete" type="button" title="Delete" class="button delete">Delete</button> 
        <button id="btnAddNew" type="button" title="Add New" class="button addnew">Add New</button>
    </div>
    <div id="cfilter">
        <table border="0" cellpadding="0" cellspacing="0">
            <tr>
                <td>
                    <input type="text" maxlength="100" style="width: 185px" value="<%= Constants.ASSET_MASTER_ASSETID_USERNAME_USERID %>"
                        id="txtSearchText" onfocus="ShowOnFocus(this,'<%= Constants.ASSET_MASTER_ASSETID_USERNAME_USERID  %>')"
                        onblur="ShowOnBlur(this,'<%= Constants.ASSET_MASTER_ASSETID_USERNAME_USERID  %>')" autocomplete="off" />
                </td> 
                 <td>
                    <%=Html.DropDownList("Category", ViewData[Constants.ASSET_LIST_CATEGORY] as SelectList, Constants.ASSET_MASTER_FIRST_ITEM_CATEGORY, new { @style = "width:160px" })%>
                </td>
                <td> 
                    <%=Html.DropDownList("Status", ViewData[Constants.ASSET_LIST_STATUS] as SelectList, Constants.ASSET_MASTER_FIRST_ITEM_STATUS, new { @style = "width:165px" })%>
                </td>
                <td>
                    <%=Html.DropDownList("Project", ViewData[Constants.ASSET_LIST_EMPLOYEE_PROJECT] as SelectList, Constants.ASSET_MASTER_FIRST_ITEM_PROJECT, new { @style = "width:170px" })%>
                </td>            
                <td>
                    <button type="button"  id="btnFilter" title="Filter" class="button filter">Filter</button>
                </td>
            </tr>
        </table>
    </div>
    <div class="clist">
        <table id="list" class="scroll">
        </table>
        <div id="pager" class="scroll" style="text-align: center;"></div>
    </div>   
    <div id="shareit-box">    
        <img src='../../Content/Images/loading3.gif' alt='' />    
    </div>
</asp:Content>
<asp:Content ID="Content7" ContentPlaceHolderID="HeadContent" runat="server">
    <script type="text/javascript">
        $(document).ready(function () {
            CRM.onEnterKeyword();

            jQuery("#list").jqGrid({
                url: '/AssetMaster/GetListJQGrid?searchText=' + encodeURIComponent($('#txtSearchText').val()) + '&category=' + $('#Category').val() + '&status=' + $('#Status').val() + '&project=' + $('#Project').val(),
                datatype: 'json',
                mtype: 'GET',
                colNames: ['ID','ID', 'Category', 'UserName', 'Status', 'Remark', 'IsActive', 'Action'],
                colModel: [
                  { name: 'ID', index: 'ID', align: "center", width: 50, hidden: true },
                  { name: 'AssetId', index: 'AssetId', align: "left", width: 50, sortable: true },
                  { name: 'AssetCategoryName', index: 'AssetCategoryName', align: "left", width: 120, sortable: true },
                  { name: 'UserName', index: 'UserName', align: "left", width: 130, sortable: true },
                  { name: 'StatusName', index: 'StatusName', align: "left", width: 50, sortable: true },
                  { name: 'Remark', index: 'Remark', align: "left", width: 200, sortable: true },
                  { name: 'IsActive', index: 'IsActive', align: "center", width: 50, sortable: true },
                  { name: 'Action', index: 'Action', editable: false, width: 50, align: 'center', sortable: false}],
                rowList: [20, 30, 50, 100, 200],
                width: 1200,
                height: "100%",
                pager: '#pager',
                sortname: '<%= (string)ViewData[Constants.ASSET_MASTER_LIST_COLUMN]%>',
                sortorder: '<%= (string)ViewData[Constants.ASSET_MASTER_LIST_ORDER]%>',
                rowNum: '<%= (string)ViewData[Constants.ASSET_MASTER_LIST_ROW_COUNT]%>',
                page: '<%= (string)ViewData[Constants.ASSET_MASTER_LIST_PAGE_INDEX]%>',
                viewrecords: true,
                sortorder: "asc",
                multiselect: true,
                imgpath: '/scripts/grid/themes/basic/images',
                loadui: 'block'
            });


            $("#btnRefesh").click(function () {
                window.location = "/AssetMaster/Refresh";
            });

            $("#btnAddNew").click(function () {
                window.location = "/AssetMaster/Create";
            });

            $("#btnExport").click(function () {
                CRM.loading();
                var numberRow = $("#list").getGridParam("records");
                if (numberRow <= 0) {
                    CRM.msgBox('<%= Resources.Message.E0027 %>', "300");
                }
                else {
                    window.location = "/AssetMaster/ExportToExcel/?Active=" + '<%=Constants.ASSET_MASTER_ACTIVE %>';
                }
                CRM.completed();
            });

            $("#btnDelete").click(function () {
                CRM.deletes('#list', 'ID', '/AssetMaster/DeleteList');
            });

            $("#btnFilter").click(function () {
                var name = $('#txtSearchText').val();
                $('#list').setGridParam({ url: '/AssetMaster/GetListJQGrid?searchText=' + encodeURIComponent($('#txtSearchText').val()) 
                + '&category=' + $('#Category').val() + '&status=' + $('#Status').val() + '&project=' + $('#Project').val()}).trigger('reloadGrid');
            });
        });
    </script>
    <link rel="stylesheet" type="text/css" href="/Content/Css/tooltip.css" />
    <script src="/Scripts/Tooltip.js" type="text/javascript"></script>
</asp:Content>
