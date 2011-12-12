<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <%= TempData["Message"]%>
    <div id="cactionbutton">
        <button id="btnRefresh" type="button" title="Refresh" class="button refresh">Refresh</button> 
    <button id="btnClearLog" type="button" title="Clear Log" class="button delete">
            Clear Logs</button>
    </div>
    <div id="cfilter">
        <table border="0" cellpadding="0" cellspacing="0">
            <tr>
                <td>
                    <%=Html.DropDownList("UserAdmin", null, Constants.SELECT_USER_ADMIN, new { @style = "width:150px" })%>
                </td>
                <td>
                        Login From
                    </td>
                    <td>
                        <%=Html.TextBox("FromDate", (string)ViewData[Constants.HOME_STATISTIC_FROM_DATE], new { @style = "width:75px" })%>
                    </td>
                    <td>
                        To
                    </td>
                    <td>
                        <%=Html.TextBox("ToDate", (string)ViewData[Constants.HOME_STATISTIC_TO_DATE], new { @style = "width:75px" })%>
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
        <div id="pager" class="scroll" style="text-align: center;"></div>
    </div>   
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="PageTitle" runat="server">
    <%= SystemLogInfo.MenuName + CommonPageInfo.AppSepChar+ SystemLogInfo.ComAdminAccess + CommonPageInfo.AppSepChar + CommonPageInfo.AppName%>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
     <script type="text/javascript">
         $(document).ready(function () {
             $("#FromDate").datepicker();
             $("#ToDate").datepicker();
             jQuery("#list").jqGrid({
                 url: '/Home/GetListJQGrid?userAdmin=' + $('#UserAdmin').val()
                     + '&dateFrom=' + $('#FromDate').val() + '&dateTo=' + $('#ToDate').val(),
                 datatype: 'json',
                 mtype: 'POST',
                 colNames: ['User Admin', 'IP Address', 'Login Time', 'Logout Time'],
                 colModel: [
                  { name: 'UserAdmin', index: 'UserAdmin', align: "left", sortable: true, width: 50, title: false },
                  { name: 'UserIp', index: 'UserIp', align: "center", sortable: true, width: 50, title: false },
                  { name: 'DatetimeAccess', index: 'DatetimeAccess', align: "center", sortable: true, width: 50, title: false },
                  { name: 'DatetimeOut', index: 'DatetimeOut', align: "center", sortable: true, width: 50, title: false }
                  ],
                 rowList: [20, 30, 50, 100, 200],
                 width: 1024,
                 height: "100%",
                 pager: '#pager',
                 sortname: '<%= (string)ViewData[Constants.HOME_STATISTIC_COLUMN]%>',
                 sortorder: '<%= (string)ViewData[Constants.HOME_STATISTIC_ORDER]%>',
                 rowNum: '<%= (string)ViewData[Constants.HOME_STATISTIC_ROW_COUNT]%>',
                 page: '<%= (string)ViewData[Constants.HOME_STATISTIC_PAGE_INDEX]%>',
                 viewrecords: true,
                 multiselect: false,
                 loadui: 'block',
                 loadComplete: function () {
                 }
             });

             $("#btnRefresh").click(function () {
                 window.location = "/Home/Refresh";
             });

             $('#btnClearLog').click(function () {
                 window.location.href = 'javascript:CRM.popup("../UserLog/ClearLog?type=AdminAccess","Clear Logs", 400);';
             });

             $("#btnFilter").click(function () {

                 var isValid = true;
                 if ($('#FromDate').val() != '') {
                     if (!isDate($('#FromDate').val())) {
                         alert('<%= String.Format(Resources.Message.E0030,"Login Time From") %>');
                         isValid = false;
                     }
                 }

                 if ($('#ToDate').val() != '') {
                     if (!isDate($('#ToDate').val())) {
                         alert('<%= String.Format(Resources.Message.E0030,"Login Time To") %>');
                         isValid = false;
                     }
                 }

                 if (isValid) {
                     $('#list').setGridParam({ url: '/Home/GetListJQGrid?userAdmin=' + $('#UserAdmin').val()
                     + '&dateFrom=' + $('#FromDate').val() + '&dateTo=' + $('#ToDate').val()
                     }).trigger('reloadGrid');
                 }
             });
         });
    </script>
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="ModuleName" runat="server">
    <%= SystemLogInfo.ComAdminAccess %>
</asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="FunctionTitle" runat="server">

    <%= CommonFunc.GetCurrentMenu(Request.RawUrl).TrimEnd().TrimEnd('»')%>
</asp:Content>

<asp:Content ID="Content6" ContentPlaceHolderID="LoginRoles" runat="server">
</asp:Content>
