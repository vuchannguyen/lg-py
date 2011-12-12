<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="PageTitle" runat="server">
<%= AdminAccountPageInfo.MenuName + CommonPageInfo.AppSepChar + AdminAccountPageInfo.ComAccount  + CommonPageInfo.AppSepChar + CommonPageInfo.AppName%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <%= TempData["Message"]%>
    <style type="text/css">
 
    .ac_results
    {
        width:270px !important;
    }
</style>
    <script src="/Scripts/jquery.validate.js" type="text/javascript"></script>
    <script src="/Scripts/Grid/js/grid.grouping.js" type="text/javascript"></script>
    
    <script type="text/javascript">
        jQuery(document).ready(function () {
            CRM.onEnterKeyword();
            jQuery("#list").jqGrid({
                url: '/UserAdmin/GetListJQGrid/?name=' + encodeURIComponent($('#txtKeyword').val()) + '&GroupName=' + $('#GroupName').val(),
                datatype: 'json',
                mtype: 'GET',
                colNames: ['ID', 'User Name', 'Group Name', 'Active', 'Created By', 'Updated By', 'Action'],
                colModel: [
                  { name: 'ID', index: 'ID', align: "center", width: 50 },
                  { name: 'UserName', index: 'UserName', align: "left", width: 120, sortable: true },
                  { name: 'GroupName', index: 'GroupName', align: "center", width: 120, sortable: true },
                  { name: 'Active', index: 'Active', align: "center", width: 60, sortable: true },
                  { name: 'CreatedBy', index: 'CreatedBy', align: "left", width: 60, sortable: true },
                  { name: 'UpdatedBy', index: 'UpdatedBy', align: "left", width: 60, sortable: true },
                  { name: 'Action', index: 'Action', editable: false, width: 50, align: 'center', sortable: false}],
                pager: jQuery('#pager'),
                rowList: [20, 30, 50, 100, 200],
                viewrecords: true,
                sortname: '<%= (string)ViewData[Constants.ACCOUNT_COLUMN]%>',
                sortorder: '<%= (string)ViewData[Constants.ACCOUNT_ORDER]%>',
                rowNum: '<%= (string)ViewData[Constants.ACCOUNT_ROW_COUNT]%>',
                page: '<%= (string)ViewData[Constants.ACCOUNT_PAGE_INDEX]%>',
                multiselect: { required: false, width: 15 },
                grouping: true,
                width: 1200,
                height: "100%",
                imgpath: '/scripts/grid/themes/basic/images',
                loadui: 'block',
                onSelectRow: function (rowId) {
                    $("#list").expandSubGridRow(rowId);
                },
                groupingView: { groupField: ['GroupName'],
                    groupColumnShow: [false],
                    groupText: ['<b>{0} </b> - {1} Item(s)'],
                    groupCollapse: false
                },
                gridComplete: function () {
                    var rowIds = $("#list").getDataIDs();
                    $.each(rowIds, function (index, rowId) {
                        $("#list").expandSubGridRow(rowId);
                    });
                }
            });
            //set width for checkbox column
            jQuery("#jqgh_cb").parent().css("width", "15px");
            jQuery("tr#_empty > td:first").css("width", "15px");

            $('#addnew').click(function () {
                CRM.popup('/UserAdmin/Create', 'Add New', 500);
            });

            //if mask is clicked            
            $("#btnDelete").click(function () {
                CRM.deleteList('#list', 'ID', '/UserAdmin/DeleteList');
            });

            $("#btnRefresh").click(function () {
                window.location = "/UserAdmin/Refresh";
            });

            $("#btnFilter").click(function () {
                var name = encodeURIComponent($('#txtKeyword').val());
                if (name == '<%= Constants.USERNAME  %>') {
                    name = "";
                }
                $('#list').setGridParam({ url: '/UserAdmin/GetListJQGrid?name=' + name + '&GroupName=' + $('#GroupName').val()
                }).trigger('reloadGrid');
            });
            $("#txtKeyword").autocomplete('Library/GenericHandle/AutoCompleteHandler.ashx/?Page=JRAdmin', { employee: true });
        });        
    </script>
    <div id="cactionbutton">
        <button type="button" id="btnRefresh" title="Refresh" class="button refresh">Refresh</button> <button type="button" id="btnDelete" title="Delete" class="button delete">Delete</button>
         <button type="button" id="addnew" title="Add New" class="button addnew">Add New</button>
    </div>
    <div id="cfilter">
        <table border="0" cellpadding="0" cellspacing="0">
            <tr>
                <td>
                    <input type="text" maxlength="100" style="width: 150px" value="<%= (string)ViewData[Constants.ACCOUNT_NAME] %>"
                        id="txtKeyword" onfocus="ShowOnFocus(this,'<%= Constants.USERNAME  %>')" onblur="ShowOnBlur(this,'<%= Constants.USERNAME  %>')" autocomplete="off"/>
                </td>
                <td>
                    <%=Html.DropDownList("GroupName", ViewData[Constants.ACCOUNT_GROUP_ID] as SelectList, Constants.FIRST_ITEM_GROUP_NAME)%>
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
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ModuleName" runat="server">
<%=AdminAccountPageInfo.ComAccount%>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="FunctionTitle" runat="server">
 <%= CommonFunc.GetCurrentMenu(Request.RawUrl,true)%>
</asp:Content>
