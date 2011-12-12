<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="PageTitle" runat="server">
<%= AdminAccountPageInfo.MenuName + CommonPageInfo.AppSepChar + AdminAccountPageInfo.ComGroup  + CommonPageInfo.AppSepChar + CommonPageInfo.AppName%>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ModuleName" runat="server">
 <%=AdminAccountPageInfo.ComGroup%>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="FunctionTitle" runat="server">
<%= CommonFunc.GetCurrentMenu(Request.RawUrl,true)%>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">    
    <%= TempData["Message"]%>            
    <div id="cactionbutton">                
        <button type="button" id="btnRefresh" title="Refresh"  class="button refresh">Refresh</button>
        <button type="button" id="btnDelete" title="Delete" class="button delete">Delete</button>
        <button type="button" id="addnew" title="Add New" class="button addnew">Add New</button>        
    </div>
    <div id="cfilter">
        <table border="0" cellpadding="0" cellspacing="0">
            <tr>
                <td><input type="text" id="txtKeyword" value="<%= (string)ViewData[Constants.GROUP_NAME] %>" 
                onfocus="ShowOnFocus(this,'<%= Constants.GROUPNAME  %>')"
                 onblur="ShowOnBlur(this,'<%= Constants.GROUPNAME  %>')" autocomplete="off" /></td>                
                <td><button type="button" id="btnFilter" title="Filter" class="button filter">Filter</button></td>
            </tr>
        </table>
        <%--<button type="button" id="btnTest" title="ChangeStatus" class="button text" >ChangeStatus</button>--%>
    </div>
    <div class="clist">
        <table id="list" class="scroll"></table>
        <div id="pager" class="scroll" style="text-align: center;"></div>
    </div>    
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
    <script src="/Scripts/jquery.validate.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            CRM.onEnterKeyword();
            $("#txtKeyword").autocomplete('/Library/GenericHandle/AutoCompleteHandler.ashx/?Page=Group');
            jQuery("#list").jqGrid({
                url: '/Group/GetListJQGrid/?optionSearch=' + encodeURIComponent($('#txtKeyword').val()),
                datatype: 'json',
                mtype: 'GET',
                colNames: ['GroupId', 'Group Name', 'Active', 'Order', 'Created By', 'Updated By', 'Action'],
                colModel: [
                  { name: 'GroupId', index: 'GroupId', align: "center", width: 50, hidden: true },
                  { name: 'GroupName', index: 'GroupName', align: "center", sortable: true },
                  { name: 'Active', index: 'Active', align: "center", width: 30, sortable: true },
                  { name: 'DisplayOrder', index: 'DisplayOrder', align: "center", width: 60, sortable: true },
                  { name: 'CreatedBy', index: 'CreatedBy', align: "center", width: 60, sortable: true },
                  { name: 'UpdatedBy', index: 'UpdatedBy', align: "center", width: 60, sortable: true },
                  { name: 'Action', index: 'Action', editable: false, width: 25, align: 'center', sortable: false}],
                pager: jQuery('#pager'),
                rowList: [20, 30, 50, 100, 200],
                sortname: '<%= (string)ViewData[Constants.GROUP_COLUMN]%>',
                sortorder: '<%= (string)ViewData[Constants.GROUP_ORDER]%>',
                rowNum: '<%= (string)ViewData[Constants.GROUP_ROW_COUNT]%>',
                page: '<%= (string)ViewData[Constants.GROUP_PAGE_INDEX]%>',
                multiselect: true,
                viewrecords: true,
                width: 1200, height: "100%",
                imgpath: '/scripts/grid/themes/basic/images',
                loadui: 'block'
            });
            CRM.onEnterKeyword();
            $("#btnRefresh").click(function () {
                window.location = "/Group/Refresh";
            });
            $("#btnDelete").click(function () {
                CRM.deleteList('#list', 'GroupId', '/Group/DeleteList');
            });
            $('#addnew').click(function () {
                CRM.popup('/Group/Create', 'Add New', 400);
            });
            $("#btnFilter").click(function () {
                $('#list').setGridParam({ url: '/Group/GetListJQGrid?optionSearch=' + encodeURIComponent($('#txtKeyword').val()) });
                $("#list").trigger('reloadGrid');
            });
//            $("#btnTest").click(function () {
//                CRM.changeActiveStatus('/Group/ChangeActiveSatus/163?isActive=false'); 
//                $('#btnFilter').click();
//            });
        });
    </script>
</asp:Content>
