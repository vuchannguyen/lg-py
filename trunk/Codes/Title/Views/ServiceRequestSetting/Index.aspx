<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <%= TempData["Message"]%>
    <div id="cactionbutton">
        <button type="button" id="btnRefresh" title="Refresh" class="button refresh">
            Refresh</button>
        <button type="button" id="btnDelete" title="Delete" class="button delete">
            Delete</button>
        <button type="button" id="btnAddNew" title="Add New" class="button addnew">
            Add New</button>
    </div>
    <input type="text" class="hidden" />
    <div id="cfilter">
        <table>
            <tr>
                <td>
                    <input type="text" id="txtKeyword" maxlength="100" style="width: 150px" value="<%= (string)ViewData[Constants.SR_LIST_SETTING_TEXT]  %>"
                        onfocus="ShowOnFocus(this,'<%= Constants.USERNAME  %>')" onblur="ShowOnBlur(this,'<%= Constants.USERNAME  %>')" autocomplete="off"/>
                </td>
                <td>
                    <%=Html.DropDownList("Project", ViewData[Constants.SR_LIST_SETTING_PROJECT] as SelectList, Constants.FIRST_ITEM_PROJECT,new {@style="width:150px"})%>
                </td>
                <td>
                    <%=Html.DropDownList("Branch", ViewData[Constants.SR_LIST_SETTING_BRANCH] as SelectList, Constants.FIRST_ITEM_BRANCH, new { @style = "width:150px" })%>
                </td>
                <td>
                    <%=Html.DropDownList("Office", ViewData[Constants.SR_LIST_SETTING_OFFICE] as SelectList, Constants.FIRST_ITEM_OFFICE, new { @style = "width:150px" })%>
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
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PageTitle" runat="server">
    <%= ServiceRequestInfo.MenuName + CommonPageInfo.AppSepChar + CommonPageInfo.AppName%>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
    <script src="/Scripts/Grid/js/grid.grouping.js" type="text/javascript"></script>
    <script type="text/javascript">
        jQuery(document).ready(function () {
            $("#txtKeyword").autocomplete('/Library/GenericHandle/AutoCompleteHandler.ashx/?Page=ServiceRequestSetting');

            CRM.onEnterKeyword();
            jQuery("#list").jqGrid({
                url: '/ServiceRequestSetting/GetListJQGrid/?filterText=' + encodeURIComponent($("#txtKeyword").val())
                     + '&project=' + $('#Project').val() + '&branch=' + $('#Branch').val()
                     + '&office=' + $('#Office').val(),
                datatype: 'json',
                mtype: 'GET',
                colNames: ["ID", "Office", "Project", 'User Admin', 'Active', 'Action'],
                colModel: [
                    { name: 'ID', index: 'ID', align: "center", width: 10, hidden: true },
                    { name: 'Office', index: 'Office', align: "center" },
                    { name: 'Project', index: 'Project', align: "left", width: 150, sortable: true },
                    { name: 'UserName', index: 'UserName', align: "left", width: 150, sortable: true },
                    { name: 'Active', index: 'Active', align: "center", width: 50, sortable: true },
                    { name: 'Action', index: 'Action', editable: false, width: 50, align: 'center', sortable: false}],
                pager: '#pager',
                sortname: '<%= (string)ViewData[Constants.SR_LIST_SETTING_COLUMN]%>',
                sortorder: '<%= (string)ViewData[Constants.SR_LIST_SETTING_ORDER]%>',
                rowNum: '<%= (string)ViewData[Constants.SR_LIST_SETTING_ROW]%>',
                page: '<%= (string)ViewData[Constants.SR_LIST_SETTING_PAGE_INDEX]%>',
                rowList: [20, 30, 50, 100, 200],
                multiselect: true,
                grouping: true,
                width: 1024, height: "100%",
                imgpath: '/scripts/grid/themes/basic/images',
                loadui: 'block',
                onSelectRow: function (rowId) {
                    $("#list").expandSubGridRow(rowId);
                },
                groupingView: { groupField: ['Office'],
                    groupColumnShow: [false],
                    groupText: ['<b>{0} </b> - {1} Item(s)'],
                    groupCollapse: false
                }
            });
            $("#btnDelete").click(function () {
                CRM.deleteList('#list', 'ID', '/ServiceRequestSetting/DeleteList');
            });
            $("#btnRefresh").click(function () {
                window.location = "/ServiceRequestSetting/Refresh";
            });
            $('#btnAddNew').click(function () {
                CRM.popup('/ServiceRequestSetting/Create', "Routing", '550');
            });

            $("#btnFilter").click(function () {
                $('#list').setGridParam({ url: '/ServiceRequestSetting/GetListJQGrid/?filterText=' + encodeURIComponent($("#txtKeyword").val())
                     + '&project=' + $('#Project').val() + '&branch=' + $('#Branch').val()
                     + '&office=' + $('#Office').val()
                }).trigger('reloadGrid');
            });

        });
    </script>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ModuleName" runat="server">
    <%= ServiceRequestInfo.FuncSettting%>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="FunctionTitle" runat="server">
    <%=CommonFunc.GetCurrentMenu(Request.RawUrl,true)%>
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="LoginRoles" runat="server">
</asp:Content>
