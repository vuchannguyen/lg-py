<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <%= TempData["Message"]%>
    <div id="cactionbutton">
        <button id="btnRefresh" type="button" title="Refresh" class="button refresh">Refresh</button>
        <button id="btnExport" type="button" title="Export" class="button export">Export to Excel</button>
    </div>

    <div id="cfilter">
        <table width="1200">
            <tr>
                <td>
                    <input type="text" id="txtKeyword" maxlength="100" style="width: 150px" maxlength="200"
                        value="<%= (string)ViewData[Constants.PTO_REPORT_TEXT] %>" onfocus="ShowOnFocus(this,'<%= Constants.EMPLOYEE  %>')"
                        onblur="ShowOnBlur(this,'<%= Constants.EMPLOYEE  %>')" autocomplete="off" />
                </td>
                <td>
                    <button type="button" id="btnFilter" title="Filter" class="button filter">Filter</button>
                </td>                               
                <td style="width:780px; text-align:right">                
                    <input id="txtFilterDate" type="text" readonly="readonly" value='<%= (string)ViewData[Constants.PTO_REPORT_MONTH] %>' style="width:65px; text-align:center;" />
                </td>
                <td align="right" style="width:170px; padding-right:0px;">
                    <button type="button" id="btnPreMonth" title="Go to previous month" style="margin-left:5px" class="button">&lt;</button>
                    <button type="button" id="btnCurMonth" title="Go to current month" style="margin-left:5px" class="button">Current</button>
                    <button type="button" id="btnNextMonth" title="Go to next month" style="margin-left:5px" class="button">&gt;</button>
                </td>     
            </tr>
        </table>
    </div>
    <div class="clist">        
        <table id="list" class="scroll" style="width: 1024px">
        </table>
        <div id="pager" class="scroll" style="text-align: center;">
        </div>
    </div>
    <div id="shareit-box">
        <img src='/Content/Images/loading3.gif' alt='' />
    </div>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PageTitle" runat="server">
    <%= PTOPageInfo.MenuName + CommonPageInfo.AppSepChar+ PTOPageInfo.Report + CommonPageInfo.AppSepChar + PTOPageInfo.ComName + CommonPageInfo.AppSepChar + CommonPageInfo.AppName%>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
<link rel="stylesheet" type="text/css" href="/Content/Css/tooltip.css" />
<script src="/Scripts/Tooltip.js" type="text/javascript"></script>
<style type="text/css">
    .ui-jqgrid .ui-jqgrid-htable th div {
        height:30px;
    }
    
    .ui-state-highlight a, .ui-widget-content .ui-state-highlight a {
        color: Black;
    }
    a.showTooltip
    {
        text-decoration:none;
        color:Black;    
    }
</style>
    
    <script src="/Scripts/jquery.validate.js" type="text/javascript"></script>
    <script type="text/javascript">
        //box_color = "Black";
        //box_sticky_color = "Black";
        tooltipoffsets = [-50, 30];
        function DisplayTooltip(object, tooltip, urlTooltip, month) {
            var $targets = $(object);
            var $tooltip = tooltip.appendTo(document.body);
            if ($targets.length == 0)
                return;
            if (!rightclickstick)
                stickynotice1[1] = '';
            stickynotice1 = "";
            hidebox($, $tooltip);
            $targets.bind('mouseenter', function (e) {
                $tooltip.show();
                jQuery.ajax({
                    url: urlTooltip,
                    type: "POST",
                    datatype: "json",
                    data: ({
                        'id': $(this).attr('id'),
                        'month': month
                    }),
                    success: function (mess) {
                        if (mess != "")
                            $tooltip.html(mess);
                        else
                            $tooltip.hide();
                    }
                });
                showbox($, $tooltip, e);
            });
            $targets.bind('mouseleave', function (e) {
                hidebox($, $tooltip);
            });
            $targets.bind('mousemove', function (e) {
                if (!isdocked) {
                    positiontooltip($, $tooltip, e);
                }
            });
            $tooltip.bind("mouseenter", function () {
                hidebox($, $tooltip)
            });
            $tooltip.bind("click", function (e) {
                e.stopPropagation()
            });
            $("body").bind("click", function (e) {
                if (e.button == 0) {
                    isdocked = false;
                    hidebox($, $tooltip);
                }
            });
            object.bind("contextmenu", function (e) {
                if (rightclickstick && $(e.target).parents().andSelf().filter(object).length == 1) { //if oncontextmenu over a target element
                    docktooltip($, $tooltip, e);
                    return false;
                }
            });
            object.bind('keypress', function (e) {
                var keyunicode = e.charCode || e.keyCode
                if (keyunicode == 115) { //if "s" key was pressed
                    docktooltip($, $tooltip, e);
                }
            });
        };
        
        jQuery(document).ready(function () {            
            CRM.onEnterKeyword();
            jQuery("#list").jqGrid({
                url: '/PTOReport/GetListJQGrid/?empName=' + encodeURIComponent($("#txtKeyword").val()) + '&date=' + $("#txtFilterDate").val(),
                datatype: 'json',
                mtype: 'GET',
                colNames: ['ID', 'ID', 'Name', 'Start Date', 'Contracted Date', 'Carried Forward <br/>(Hours)', 'Monthly Vacation <br/>(Hours)', 'Used <br/>(Hours)', 'Date Off <br/>(DD/MM/YY)', 'EOM Balance <br/>(Hours)', 'Paid leave <br/>(Hours)', 'Unpaid leave <br/>(Hours)', 'Comment', 'Action'],
                colModel: [
                  { name: 'ID', index: 'ID', align: "center", width: 50, hidden: true },
                  { name: 'EmployeeID', index: 'EmployeeID', align: "center", width: 25, sortable: true },
                  { name: 'Name', index: 'Name', align: "left", width: 100, sortable: true },
                  { name: 'StartDate', index: 'StartDate', align: "center", width: 50, sortable: true },
                  { name: 'ContractedDate', index: 'ContractedDate', align: "center", width: 65, sortable: true },
                  { name: 'CarriedForward', index: 'CarriedForward', align: "center", width: 55, sortable: true },
                  { name: 'MonthLyVacation', index: 'MonthLyVacation', align: "center", width: 55, sortable: false },
                  { name: 'Used', index: 'Used', align: "center", width: 35, sortable: true },
                  { name: 'DateOff', index: 'DateOff', align: "center", width: 90, sortable: false },
                  { name: 'EOMBalance', index: 'EOMBalance', align: "center", width: 50, sortable: true },
                  { name: 'UnpaidHour', index: 'UnpaidHour', align: "center", width: 40, sortable: true },
                  { name: 'UnpaidLeave', index: 'UnpaidLeave', align: "center", width: 40, sortable: true },
                  { name: 'Comment', index: 'Comment', align: "left", width: 50, sortable: true },
                  { name: 'Action', index: 'Action', editable: false, width: 25, align: 'center', sortable: false}],
                pager: jQuery('#pager'),
                rowList: [20, 30, 50, 100, 200,250, 300],
                sortname: '<%= (string)ViewData[Constants.PTO_REPORT_COLUMN]%>',
                sortorder: '<%= (string)ViewData[Constants.PTO_REPORT_ORDER]%>',
                rowNum: '<%= (string)ViewData[Constants.PTO_REPORT_ROW_COUNT]%>',
                page: '<%= (string)ViewData[Constants.PTO_REPORT_PAGE_INDEX]%>',
                multiselect: false,
                viewrecords: true,
                width: 1200, height: "auto",
                imgpath: '/scripts/grid/themes/basic/images',
                loadui: 'block',
                loadComplete: function () {
                    DisplayTooltip($("a[class=showTooltip]"), $("#shareit-box"), 
                        '<%=Url.Action("DateOffTooltip") %>', $("#txtFilterDate").val());
                }
            });
            //set width for checkbox column
            jQuery("#jqgh_cb").parent().css("width", "15px");
            jQuery("tr#_empty > td:first").css("width", "15px");           
           
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

            $("#btnFilter").click(function (){                 
                filter();
            });
            $("#btnRefresh").click(function () {
                window.location = "/PTOReport/Refresh";
            });
            $("#btnExport").click(function () {
                if ($("#list tr").length < 2) {
                    CRM.msgBox(CRM.format(E0027), 300);
                }
                else {
                    var name = $("#hidFilteredName").val();
                    var year = $("#hidFilteredYear").val();
                    var sortName = $('#list').jqGrid('getGridParam', 'sortname');
                    var sortOrder = $('#list').jqGrid('getGridParam', 'sortorder');
                    var targetUrl = '/PTOReport/ExportToExcel/?empName=' + encodeURIComponent($("#txtKeyword").val()) + '&date=' + $("#txtFilterDate").val() + "&sortColumn=" + sortName + "&sortOrder=" + sortOrder;
                    window.location = targetUrl;
                }
            });          
        });


        function filter() {
            var url_send = '/PTOReport/GetListJQGrid/?empName=' + encodeURIComponent($("#txtKeyword").val()) + '&date=' + $("#txtFilterDate").val();
            $('#list').setGridParam({ page: 1, url: url_send });
            $("#list").trigger('reloadGrid');
        }    
              
    </script>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ModuleName" runat="server">
    <%= PTOPageInfo.ComName%>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="FunctionTitle" runat="server">
    <%= CommonFunc.GetCurrentMenu(Request.RawUrl,true)%>
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="LoginRoles" runat="server">
</asp:Content>
