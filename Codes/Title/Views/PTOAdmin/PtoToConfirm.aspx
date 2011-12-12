<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <%= TempData["Message"]%>
    <div id="cactionbutton">
        <table style="width: 100%">
            <tr>
                <td>
                    <button type="reset" id="btnRefresh" title="Refresh" class="button refresh">
                        Refresh</button> 

                    <button type="button" id="btnSendEmail" title="Export" class="button export">
                        Send Reminder to Confirm PTO Data</button>
                </td>
            </tr>
        </table>
    </div>
    <div id="cfilter">
        <table>
            <tr>
                <td>                    
                    <input type="text" maxlength="50" style="width: 150px" 
                        value="<%= (string)ViewData[Constants.PTO_CONFIRM_TEXT] %>"
                        id="txtKeyword" onfocus="ShowOnFocus(this,'<%= Constants.PTO_ADMIN_EMPLOYEE_NAME%>')"
                        onblur="ShowOnBlur(this,'<%= Constants.PTO_ADMIN_EMPLOYEE_NAME  %>')" />
                </td>
                <td>Manager</td>
                <td>
                    <%=Html.DropDownList(CommonDataKey.PTO_MANAGER_LIST, ViewData[Constants.PTO_CONFIRM_MANAGER] as SelectList, Constants.FIRST_MANAGER,
                        new { @style = "width:200px"})%>
                </td>
                <td>PTO Type</td>
                <td>
                <%=Html.DropDownList(CommonDataKey.PTO_TYPE_PARENT_ID, null, Constants.PTO_PARENT_FIRST_TYPE,
                        new { @style = "width:200px" })%>
                    <%=Html.DropDownList(CommonDataKey.PTO_TYPE_LIST, null, Constants.PTO_FIRST_TYPE,
                        new { @style = "width:200px" })%>
                </td>                
                <td>                    
                    <button type="button" id="btnFilter" title="Filter" class="button filter"> Filter</button>
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
    <div id="shareit-box">    
        <img src='../../Content/Images/loading3.gif' alt='' />    
    </div>
</asp:Content>



<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
 <style type="text/css">
 .ac_results
    {
        width:250px !important;
    }
    </style>
<script src="/Scripts/Grid/js/grid.grouping.js" type="text/javascript"></script>
<script type="text/javascript">
    function daysInMonth(month, year) {
        return new Date(year, month, 0).getDate();
    }
    function SendRemindEmail(managerIds) {
        CRM.showProgressInMessageBox(CONST_POPUP_CONTENT, ProgressType.Replace, CRM.format(I0008, "Sending Email(s)") + "<br/>",
            "width:100%; text-align:center; font-weight:bold", "");
        var targetUrl = "/PTOAdmin/SendRemindEmail/?managerIds=" + managerIds;
        jQuery.ajax({
            url: targetUrl,
            type: "GET",
            datatype: "json",
            error: function () {
                CRM.message(CRM.format(E0007), 'block', 'msgError');
                CRM.closePopup();
            },
            success: function (mess) {
                var msgClass = "msgSuccess";
                if (mess.MsgType == 1) {
                    msgClass = "msgError";
                }
                CRM.message(mess.MsgText, 'block', msgClass);
                CRM.closePopup();
            }
        })
    }
    $(document).ready(function () {
        $("#PTOType_Parent_ID").change(function () {
            $("#PTOType_ID").html("");
            var id = $("#PTOType_Parent_ID").val();
            $("#PTOType_ID").append($("<option value=''><%= Constants.PTO_FIRST_TYPE%></option>"));
            if (id != "") {
                $.getJSON('/Library/GenericHandle/DropdownListHandler.ashx?ID=' + id + '&Page=PTOType', function (item) {
                    $.each(item, function () {
                        $("#PTOType_ID").append($("<option value='" + this['ID'] + "'>" + this['Name'] + "</option>"));
                    });
                });
            }
        });
        CRM.onEnterKeyword();
        $("#btnAddNew").click(function () {
            CRM.popup("/PTOAdmin/Create", "Add New PTO ", 550);
        });
        jQuery("#list").jqGrid({
            url: '/PTOAdmin/GetListPtoToConfirmJQGrid/?filterText=' + encodeURIComponent($("#txtKeyword").val()) +
                '&managerId=' + $('#Manager').val() + '&type=' +
                $('#PTOType_ID').val(),
            datatype: 'json',
            mtype: 'GET',
            colNames: ['ID', 'Employee', "Manager", 'Hour(s)', 'PTO Type', 'Balance', 'Reason', 'ManagerId'],
            colModel: [
                  { name: 'ID', index: 'ID', align: "left", width: 100, sortable: true },
                  { name: 'Employee', index: 'Employee', align: "Left", width: 200, sortable: true },
                  { name: 'Manager', index: 'Manager', align: "Left", width: 200, sortable: true },
                  { name: 'Hours', index: 'Hours', align: "center", width: 100, sortable: true },
                  { name: 'TypeName', index: 'TypeName', align: "center", width: 150, sortable: true },
                  { name: 'Balance', index: 'Balance', editable: false, width: 50, align: 'center', sortable: true },
                  { name: 'Reason', index: 'Reason', align: "left", width: 300, sortable: true },
                  { name: 'ManagerId', index: 'ManagerId', align: "left", hidden: true}],
            pager: jQuery('#pager'),
            rowList: [20, 30, 50, 80, 100],
            page: 1,
            viewrecords: true,
            width: 1024, height: "auto",
            rownumbers: false,
            sortname: '<%= (string)ViewData[Constants.PTO_CONFIRM_COLUMN]%>',
            sortorder: '<%= (string)ViewData[Constants.PTO_CONFIRM_ORDER]%>',
            rowNum: '<%= (string)ViewData[Constants.PTO_CONFIRM_ROW_COUNT]%>',
            page: '<%= (string)ViewData[Constants.PTO_CONFIRM_PAGE_INDEX]%>',
            imgpath: '/scripts/grid/themes/basic/images',
            loadui: 'block',
            grouping: true,
            onSelectRow: function (rowId) {
                $("#list").expandSubGridRow(rowId);
            },
            groupingView: { groupField: ['Manager'],
                groupColumnShow: [false],
                groupText: ['<b>{0} </b> - ({1})'],
                groupCollapse: false
            },
            gridComplete: function () {
                var rowIds = $("#list").getDataIDs();
                $.each(rowIds, function (index, rowId) {
                    $("#list").expandSubGridRow(rowId);
                });
            },
            loadComplete: function () {
                ShowTooltip($("a[class=empTooltip]"), $("#shareit-box"), "/Employee/EmployeeToolTip");
            }
        });

        $("#btnFilter").click(function () {
            var url_send = '/PTOAdmin/GetListPtoToConfirmJQGrid/?filterText=' + encodeURIComponent($("#txtKeyword").val()) +
                '&managerId=' + $('#Manager').val() + '&type=' +
                $('#PTOType_ID').val();
            $('#list').setGridParam({ url: url_send, page: 1 });
            $("#list").trigger('reloadGrid');
        });

        $("#txtKeyword").autocomplete('/Library/GenericHandle/AutoCompleteHandler.ashx/?Page=Employee' + '&IsActive=1', { employee: true });

        $("#btnSendEmail").click(function () {

            var numberRow = $("#list").getGridParam("records");
            if (numberRow <= 0) {
                CRM.msgBox(CRM.format(E0027), 300);
            }
            else {
                var sManagerIds = $("#list").getCell(1, "ManagerId");
                CRM.msgConfirmBox(CRM.format(I0004, "send reminder email(s) to manager(s)"),
                    500, "SendRemindEmail('" + sManagerIds + "')");
            }
        });

        $("#btnRefresh").click(function () {
            window.location = "/PTOAdmin/Refresh/2";
        });
    });
    
</script>
    <link rel="stylesheet" type="text/css" href="/Content/Css/tooltip.css" />
    <script src="/Scripts/Tooltip.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PageTitle" runat="server">
    <%=PTOPageInfo.MenuName + CommonPageInfo.AppSepChar + PTOPageInfo.Need_To_Confirm + CommonPageInfo.AppSepChar + 
        PTOPageInfo.ComName + CommonPageInfo.AppSepChar + CommonPageInfo.AppName %>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ModuleName" runat="server">
    <%=PTOPageInfo.ComName%>
</asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="FunctionTitle" runat="server">
  <%=CommonFunc.GetCurrentMenu(Request.RawUrl,true)%>
</asp:Content>

<asp:Content ID="Content6" ContentPlaceHolderID="LoginRoles" runat="server">
</asp:Content>
