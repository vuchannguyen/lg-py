<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="PageTitle" runat="server">
    <%=AssetCategoryPageInfo.AssetCategory + CommonPageInfo.AppSepChar + AssetCategoryPageInfo.ComName + CommonPageInfo.AppSepChar + CommonPageInfo.AppName%>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ModuleName" runat="server">
    <%=AssetCategoryPageInfo.ComName%>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="FunctionTitle" runat="server">
    <%=AssetCategoryPageInfo.AssetCategory%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
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
                    <input type="text" maxlength="100" style="width: 150px" value="<%= (string)ViewData[Constants.ASSET_CATEGORY_SEARCH_TEXT] %>"
                        id="txtSearchText" onfocus="ShowOnFocus(this,'<%= Constants.ASSET_CATEGORY_NAME_OR_DESCRIPTION  %>')"
                        onblur="ShowOnBlur(this,'<%= Constants.ASSET_CATEGORY_NAME_OR_DESCRIPTION  %>')" />
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
<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
    <script type="text/javascript">
        $(document).ready(function () {
            CRM.onEnterKeyword();
            
            jQuery("#list").jqGrid({
                url: '/AssetCategory/GetListJQGrid?searchText=' + encodeURI($('#txtSearchText').val()),
                datatype: 'json',
                mtype: 'GET',
                colNames: ['ID', 'Category Name', 'Description', 'Active', 'Created By', 'Updated By', 'Action'],
                colModel: [
                  { name: 'ID', index: 'ID', align: "center", width: 50, hidden: true },
                  { name: 'CategoryName', index: 'CategoryName', align: "left", width: 200, sortable: true },
                  { name: 'Description', index: 'Description', align: "left", width: 200, sortable: true },
                  { name: 'Active', index: 'Active', align: "center", width: 50, sortable: true },
                  { name: 'CreatedBy', index: 'CreatedBy', align: "center", width: 50, sortable: true },
                  { name: 'UpdatedBy', index: 'UpdatedBy', align: "center", width: 50, sortable: true },
                  { name: 'Action', index: 'Action', editable: false, width: 50, align: 'center', sortable: false}],
                pager: jQuery('#pager'),
                rowList: [20, 200, 200, 100, 200],                
                rowNum: 20,
                sortname: 'CategoryName',
                sortorder: "asc",
                multiselect: { required: false, width: 15 },
                viewrecords: true,
                width: 1024, height: "auto",
                imgpath: '/scripts/grid/themes/basic/images',
                loadui: 'block'
            });   


            $("#btnRefesh").click(function () {
                window.location = "/AssetCategory/Refresh";
            });

            $("#btnAddNew").click(function () {
                CRM.popup('/AssetCategory/Create', 'Add New', 500);
            });

            $("#btnExport").click(function () {
                window.location = '/AssetCategory/Export/?text=' + encodeURIComponent($("#txtSearchText").val());
            });

            $("#btnDelete").click(function () {
                CRM.deletes('#list', 'ID', '/AssetCategory/DeleteList');
            });

            $("#btnFilter").click(function () {
                var name = $('#txtSearchText').val();
                $('#list').setGridParam({ url: '/AssetCategory/GetListJQGrid?searchText=' + encodeURI(name)}).trigger('reloadGrid');                
            });
        });
    </script>
    <link rel="stylesheet" type="text/css" href="/Content/Css/tooltip.css" />
    <script src="/Scripts/Tooltip.js" type="text/javascript"></script>
</asp:Content>
