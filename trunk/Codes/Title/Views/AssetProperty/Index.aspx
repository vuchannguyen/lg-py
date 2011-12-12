<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="PageTitle" runat="server">
    <%=AssetPropertyPageInfo.AssetProperty + CommonPageInfo.AppSepChar + AssetPropertyPageInfo.ComName + CommonPageInfo.AppSepChar + CommonPageInfo.AppName%>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ModuleName" runat="server">
    <%=AssetPropertyPageInfo.ComName%>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="FunctionTitle" runat="server">
    <%=AssetPropertyPageInfo.AssetProperty%>
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
                    <input type="text" maxlength="100" style="width: 150px" value="<%= (string)ViewData[Constants.ASSET_PROPERTY_SEARCH_TEXT] %>"
                        id="txtSearchText" onfocus="ShowOnFocus(this,'<%= Constants.ASSET_PROPERTY_DEFAULT_SEARCH_TEXT  %>')"
                        onblur="ShowOnBlur(this,'<%= Constants.ASSET_PROPERTY_DEFAULT_SEARCH_TEXT  %>')" />
                </td>                
                <td>
                <%
                    Response.Write(Html.DropDownList("CategoryId", ViewData[Constants.ASSET_PROPERTY_CATEGORY_LIST] as SelectList, Constants.SR_FIRST_CATEGORY, new { @style = "width:136px" }));
                %>
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
                url: '/AssetProperty/GetListJQGrid?searchText=' + encodeURI($('#txtSearchText').val()) + '&categoryid=' + $('#CategoryId').val(),
                datatype: 'json',
                mtype: 'GET',
                colNames: ['ID', 'Property Name', 'Category', 'Display Order', 'Created By', 'Updated By', 'Action'],
                colModel: [
                  { name: 'ID', index: 'ID', align: "center", width: 50, hidden: true },
                  { name: 'PropertyName', index: 'PropertyName', align: "left", width: 200, sortable: true },
                  { name: 'CategoryName', index: 'CategoryName', align: "left", width: 200, sortable: true },
                  { name: 'DisplayOrder', index: 'DisplayOrder', align: "center", width: 50, sortable: true },
                  { name: 'CreatedBy', index: 'CreatedBy', align: "center", width: 50, sortable: true },
                  { name: 'UpdatedBy', index: 'UpdatedBy', align: "center", width: 50, sortable: true },
                  { name: 'Action', index: 'Action', editable: false, width: 50, align: 'center', sortable: false}],
                pager: jQuery('#pager'),
                rowList: [20, 200, 200, 100, 200],
                rowNum: 20,
                sortname: 'PropertyName',
                sortorder: "asc",
                multiselect: { required: false, width: 15 },
                viewrecords: true,
                width: 1024, height: "auto",
                imgpath: '/scripts/grid/themes/basic/images',
                loadui: 'block'
            });


            $("#btnRefesh").click(function () {
                window.location = "/AssetProperty/Refresh";
            });

            $("#btnAddNew").click(function () {
                CRM.popup('/AssetProperty/Create', 'Add New', 600);
            });

            $("#btnDelete").click(function () {
                //alert("ID");
                CRM.deletes('#list', 'ID', '/AssetProperty/DeleteList');
            });

            $("#btnFilter").click(function () {
                var name = $('#txtSearchText').val();
                $('#list').setGridParam({ url: '/AssetProperty/GetListJQGrid?searchText=' + encodeURI(name) + '&categoryId=' + $('#CategoryId').val() }).trigger('reloadGrid');
            });

            $("#btnExport").click(function () {
                CRM.loading();
                var numberRow = $("#list").getGridParam("records");
                if (numberRow <= 0) {
                    CRM.msgBox('<%= Resources.Message.E0027 %>', "300");
                }
                else {
                    var name = $('#txtSearchText').val();
                    window.location = '/AssetProperty/Export/?searchtext=' + encodeURI(name) + '&categoryId=' + $('#CategoryId').val();
                }
                CRM.completed();
            });
        });
    </script>
    <link rel="stylesheet" type="text/css" href="/Content/Css/tooltip.css" />
    <script src="/Scripts/Tooltip.js" type="text/javascript"></script>
</asp:Content>
