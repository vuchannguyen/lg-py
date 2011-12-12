<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
   <%= TempData["Message"]%>
    <div id="cactionbutton">
        <table style="width: 100%">
            <tr>
                <td>
                 <button type="reset" id="btnRefresh" title="Refresh" class="button refresh">
                        Refresh</button> 

                    <button type="button" id="btnExport" title="Export" class="button export">
                        Export</button>
                                                                             
                    <button type="button" id="btnAddNew" title="Submit PTO" class="button addnew">
                        Submit PTO</button>                   
                </td>
            </tr>
        </table>
    </div>
    <div id="cfilter">
        <table>
            <tr>
                <td>                    
                    <input type="text" maxlength="50" style="width: 150px" 
                        value="<%= (string)ViewData[Constants.PTO_ADMIN_TEXT] %>"
                        id="txtKeyword" onfocus="ShowOnFocus(this,'<%= Constants.PTO_ADMIN_EMPLOYEE_NAME%>')"
                        onblur="ShowOnBlur(this,'<%= Constants.PTO_ADMIN_EMPLOYEE_NAME  %>')" autocomplete="off"  />
                </td>
                <td>
                    <%=Html.DropDownList("Status", ViewData[Constants.PTO_ADMIN_STATUS] as SelectList, 
                        Constants.PTO_FIRST_STATUS)%>
                </td>
                <td>
                <%=Html.DropDownList(CommonDataKey.PTO_ADMIN_TYPE_PARENT_ID, null,
                                            Constants.PTO_PARENT_FIRST_TYPE, new { @style="width:180px"})%>
                    <%=Html.DropDownList(CommonDataKey.PTO_ADMIN_TYPE_ID, null,
                                                Constants.PTO_FIRST_TYPE, new { @style = "width:150px" })%>
                </td>                
                <td>                    
                    <button type="button" id="btnFilter" title="Filter" class="button filter"> Filter</button>
                </td>                
                <td align="right" style="width:150px">                
                    <input id="txtFilterDate" type="text" readonly="readonly" 
                        value='<%= (string)ViewData[Constants.PTO_ADMIN_MONTH] %>' style="width:65px; text-align:center;" />
                </td>
               <td align="right" style="width:170px;">
                    <button type="button" id="btnPreMonth" title="Go to previous month" style="margin-left:5px" class="button">&lt;</button>
                    <button type="button" id="btnCurMonth" title="Go to current month" style="margin-left:5px" class="button">Current</button>
                    <button type="button" id="btnNextMonth" title="Go to next month" style="margin-left:5px" class="button">&gt;</button>
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

<asp:Content ID="Content2" ContentPlaceHolderID="PageTitle" runat="server">
<%= PTOPageInfo.MenuName + CommonPageInfo.AppSepChar + PTOPageInfo.Admin + CommonPageInfo.AppSepChar + PTOPageInfo.ComName + CommonPageInfo.AppSepChar + CommonPageInfo.AppName%>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
<style type="text/css">
    .tooltip th 
    {
        background-image: url("/Content/Images/Common/ghead.gif") !important;
        color:White
    }
     .ac_results
    {
        width:250px !important;
    }
    .ui-jqgrid tr.jqgrow td {
        white-space: normal !important;
    }
</style>
<script type="text/javascript">
    function daysInMonth(month, year) {
        return new Date(year, month, 0).getDate();
    }
    function DeletePTO(id) {
        window.location = '/PTOAdmin/DeletePTO/' + id;
    }  
    $(document).ready(function () {
        //$("#txtKeyword").autocomplete('/Library/GenericHandle/AutoCompleteHandler.ashx/?Page=Employee' + '&IsActive=1', { employee: true });
        CRM.onEnterKeyword();
        $("#TypeParent").change(function () {
            $("#Type").html("");
            var id = $("#TypeParent").val();
            $("#Type").append($("<option value=''><%= Constants.PTO_FIRST_TYPE%></option>"));
            if (id != "") {
                $.getJSON('/Library/GenericHandle/DropdownListHandler.ashx?ID=' + id + '&Page=PTOType', function (item) {
                    $.each(item, function () {
                        $("#Type").append($("<option value='" + this['ID'] + "'>" + this['Name'] + "</option>"));
                    });
                });
            }
        });
        $("#btnAddNew").click(function () {
            CRM.popup("/PTOAdmin/Create", "Submit PTO ", 600);
        });
        jQuery("#list").jqGrid({
            url: '/PTOAdmin/GetListJQGrid/?filterText=' + encodeURIComponent($("#txtKeyword").val()) +
                '&status=' + $('#Status').val() + '&type=' +
                $('#Type').val() + '&month=' + $('#txtFilterDate').val(),
            datatype: 'json',
            mtype: 'GET',
            colNames: ['', 'ID', 'Employee', 'Hour(s)', 'Submitting Date', 'Status', 'PTO Type', 'Reason', 'Action'],
            colModel: [
                  { name: 'Icon', index: 'StatusName', align: "center", width: 30, sortable: false },
                  { name: 'ID', index: 'ID', align: "left", width: 80, sortable: true },
                  { name: 'Employee', index: 'Employee', align: "Left", width: 200, sortable: true },
                  { name: 'Hours', index: 'Hours', align: "center", width: 60, sortable: true },
                  { name: 'SubmitDate', index: 'SubmitDate', align: "center", width: 100, sortable: false },
                  { name: 'StatusName', index: 'StatusName', align: "center", width: 100, sortable: true },
                  { name: 'TypeName', index: 'TypeName', align: "center", width: 150, sortable: true },
                  { name: 'Reason', index: 'Reason', align: "left", width: 260, sortable: true },
                  { name: 'Action', index: 'Action', editable: false, width: 50, align: 'center', sortable: false}],
            pager: jQuery('#pager'),
            rowList: [20, 30, 50, 80, 100],
            viewrecords: true,
            width: 1050, height: "auto",
            rownumbers: false,
            sortname: '<%= (string)ViewData[Constants.PTO_ADMIN_COLUMN]%>',
            sortorder: '<%= (string)ViewData[Constants.PTO_ADMIN_ORDER]%>',
            rowNum: '<%= (string)ViewData[Constants.PTO_ADMIN_ROW_COUNT]%>',
            page: '<%= (string)ViewData[Constants.PTO_ADMIN_PAGE_INDEX]%>',
            imgpath: '/scripts/grid/themes/basic/images',
            loadui: 'block',
            loadComplete: function () {
                ShowTooltip($("a[class=ptoTooltip]"), $("#shareit-box"), "/Common/PTODetailTooltip");
                ShowTooltip($("a[class=empTooltip]"), $("#shareit-box"), "/Employee/EmployeeToolTip");
            }
        });

        $("#btnFilter").click(function () {
            var url_send = '/PTOAdmin/GetListJQGrid/?filterText=' + encodeURIComponent($("#txtKeyword").val()) +
                '&status=' + $('#Status').val() +
                '&type=' + $('#Type').val() +
                '&month=' + $('#txtFilterDate').val();
            $('#list').setGridParam({ url: url_send });
            $("#list").trigger('reloadGrid');
        });

        $("#btnPreMonth").click(function () {
            var date = date = $("#txtFilterDate").val();
            AddMonth("txtFilterDate", date, -1);
            $("#btnFilter").click();
        });

        $("#btnNextMonth").click(function () {
            var date = date = $("#txtFilterDate").val();
            AddMonth("txtFilterDate", date, 1);
            $("#btnFilter").click();
        });

        $("#btnCurMonth").click(function () {
            CurMonth("txtFilterDate");
            $("#btnFilter").click();
        });


        $("#btnExport").click(function () {
            var numberRow = $("#list").getGridParam("records");
            if (numberRow <= 0) {
                CRM.msgBox(CRM.format(E0027, "300"));
            }
            else {
                var targetUrl = "/PTOAdmin/ExportToExcel/?name=" + encodeURIComponent($("#txtKeyword").val()) +
                '&status=' + $('#Status').val() + '&type=' + $('#Type').val() +
                '&month=' + $('#txtFilterDate').val() +
                '&sortColumn=' + $('#list').getGridParam("sortname") +
                '&sortOrder=' + $('#list').getGridParam("sortorder");
                window.location = targetUrl;
            }
        });

        $("#btnRefresh").click(function () {
            window.location = "/PTOAdmin/Refresh";
        });
    });
    
</script>
    <link rel="stylesheet" type="text/css" href="/Content/Css/tooltip.css" />
    <script src="/Scripts/Tooltip.js" type="text/javascript"></script>
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="ModuleName" runat="server">
    <%= PTOPageInfo.ComName %>
</asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="FunctionTitle" runat="server">
    <%=CommonFunc.GetCurrentMenu(Request.RawUrl,true)%>
</asp:Content>

<asp:Content ID="Content6" ContentPlaceHolderID="LoginRoles" runat="server">
</asp:Content>
