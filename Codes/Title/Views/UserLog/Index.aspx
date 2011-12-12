<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
<%= TempData["Message"]%>
    <div id="cactionbutton">
        <button id="btnRefresh" type="button" title="Refresh" class="button refresh">
            Refresh</button>
        <button id="btnClearLog" type="button" title="Clear Log" class="button delete">
            Clear Logs</button>
    </div>
    <div id="cfilter">
        <table border="0" cellpadding="0" cellspacing="0">
            <tr>
                <td>
                    <input type="text" maxlength="100" style="width: 150px" value="<%= (string)ViewData[Constants.USER_LOG_NAME] %>"
                        id="txtKeyword" onfocus="ShowOnFocus(this,'<%= Constants.USERNAME  %>')" onblur="ShowOnBlur(this,'<%= Constants.USERNAME  %>')" autocomplete="off" />
                </td>
                <td>
                    <input id="txtDate" type="text" value="<%= (string)ViewData[Constants.USER_LOG_DATE] %>" />
                </td>
                <td>
                    <button id="btnFilter" title="Filter" class="button filter">
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
    <%= SystemLogInfo.MenuName + CommonPageInfo.AppSepChar + SystemLogInfo.ComDataLog + CommonPageInfo.AppSepChar + CommonPageInfo.AppName%>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
        .ui-state-hover, .ui-widget-content .ui-state-hover
        {
            background: #e1e1e1 !important;
        }
    </style>
    <script type="text/javascript">
        $(document).ready(function () {
            $("#txtKeyword").autocomplete('Library/GenericHandle/AutoCompleteHandler.ashx/?Page=UserLogs');
            CRM.onEnterKeyword();
            $("#txtDate").datepicker();
            jQuery("#list").jqGrid({
                url: '/UserLog/GetListJQGrid?name=' + encodeURIComponent($('#txtKeyword').val()) + '&date=' + $('#txtDate').val(),
                datatype: 'json',
                mtype: 'GET',
                colNames: ['User Name', 'Date', 'Action'],
                colModel: [
                  { name: 'UserName', index: 'UserName', width: 80, align: "left", width: 0, title: false, sortable: true },
                  { name: 'Date', index: 'Date', align: "center", width: 80, sortable: true, title: false },
                  { name: 'Action', width: 400, index: 'Action', align: "left", title: false}],
                rowList: [20, 30, 40],
                width: 1200,
                height: "100%",
                pager: '#pager',
                sortname: '<%= (string)ViewData[Constants.USER_LOG_COLUMN]%>',
                sortorder: '<%= (string)ViewData[Constants.USER_LOG_ORDER]%>',
                rowNum: '<%= (string)ViewData[Constants.USER_LOG_ROW_COUNT]%>',
                page: '<%= (string)ViewData[Constants.USER_LOG_PAGE_INDEX]%>',
                viewrecords: true,
                loadui: 'block'
            });

            $("#btnRefresh").click(function () {
                window.location = "/UserLog/Refresh";
            });

            $('#btnClearLog').click(function () {
                window.location.href = 'javascript:CRM.popup("UserLog/ClearLog?type=DataLog","Clear Logs", 400);';
            });

            $("#btnFilter").click(function () {
                var name = encodeURIComponent($('#txtKeyword').val());
                if (name == '<%= Constants.USERNAME  %>') {
                    name = "";
                }
                var isValid = true;
                var date = "";
                if ($('#txtDate').val() != "") {
                    isValid = isDate($('#txtDate').val());
                    date = $('#txtDate').val();
                }
                if (isValid) {
                    $('#list').setGridParam({ url: '/UserLog/GetListJQGrid?name=' + name
                     + '&date=' + date
                    }).trigger('reloadGrid');
                }
                else {

                    alert("Date is invalid.");
                }

            });
        });
    </script>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ModuleName" runat="server">
    <%=  SystemLogInfo.ComDataLog%>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="FunctionTitle" runat="server">
    <%= CommonFunc.GetCurrentMenu(Request.RawUrl,true)%>
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="LoginRoles" runat="server">
</asp:Content>
