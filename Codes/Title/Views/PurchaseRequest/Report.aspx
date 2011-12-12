<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <%= TempData["Message"]%>
    <div id="cactionbutton">
        <table style="width: 100%">
            <tr>
                <td>                                
                    
                </td>
                <td>                    
                    <button type="button" id="btnRefresh" title="Refresh" class="button refresh">
                        Refresh</button>
                    <button type="button" id="btnExport" title="Export" class="button export">
                        Export</button>
                   <button type="button" id="btnToogle" title="Cost Statistic" class="button statistic">
                        Show Chart</button>
                </td>
            </tr>
            <tr>
                <td>
                
                </td>
            </tr>
        </table>
    </div>
    <table id="ghStatistic" style="width: 1200px; display:none">
        <tr>
            <td class="chrgh" style="width: 100%;">
                <div id="accordion" >
                    <div class="panel">
                        <div class="pntitle">
                            <span class="pndown"></span><span class="pntext" >PR COST STATISTIC </span>
                        </div>
                        <table width="100%" cellpadding="0" cellspacing="0" border="0">
                            <tr>
                                <td style="border-right:1px solid #bbbbbb">
                                     <div id="chart-vnd-container" style="width:600px; height:200px"></div>
                                </td>
                                
                                <td >
                                    <div id="chart-usd-container" style="width:600px; height:200px" ></div>
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
            </td>
        </tr>
    </table>
    <input type="text" class="hidden" />
    <div id="cfilter">
        <% Response.Write(Html.Hidden("IsViewAll", ViewData[CommonDataKey.PR_IS_VIEW_ALL])); %>
        <% Response.Write(Html.Hidden("LoginId", ViewData[CommonDataKey.PR_USER_LOGIN_ID])); %>
        <% Response.Write(Html.Hidden("LoginName", ViewData[CommonDataKey.PR_USER_LOGIN_NAME])); %>
        <table width="1200px">
            <tr>
                <td colspan="7" style="height:10px;"></td>
                <td rowspan="4">
                    <fieldset style="width:340px; height:40px; margin:0;font-weight:bold; color:Black; background:none !important; float:right">
                        <legend align="center" id="divDate" style="font-weight:bold; color:Black;padding: 0 !important; padding-bottom: 6px !important">
                        </legend>
                        <div id="divAll" style="text-align:center"></div>
                    </fieldset>
                </td>
            </tr>
            <tr>
                <td style="width:180px">
                    <input type="text" id="txtKeyword" maxlength="100" style="width: 170px" value="<%= (string)ViewData[Constants.PURCHASE_REQUEST_KEYWORD]  %>"
                        onfocus="ShowOnFocus(this,'<%= Constants.PURCHASE_REQUEST  %>')" onblur="ShowOnBlur(this,'<%= Constants.PURCHASE_REQUEST  %>')" />
                </td>
                <td style="width:220px">
                    <%=Html.DropDownList("Department", ViewData[Constants.PURCHASE_REQUEST_DEPARTMENT] as SelectList, Constants.FIRST_ITEM_DEPARTMENT, new { @style = "width:220px" })%>
                </td>
                <td style="width:170px">
                    <%=Html.DropDownList("SubDepartment", ViewData[Constants.PURCHASE_REQUEST_SUB_DEPARTMENT] as SelectList, Constants.FIRST_ITEM_SUB_DEPARTMENT, new { @style = "width:170px" })%>
                </td>
                <td style="width:170px">
                    <%=Html.DropDownList("requestorId", ViewData[Constants.PURCHASE_REQUEST_REQUESTOR_ID] as SelectList, Constants.PURCHASE_REQUEST_REQUESTOR_FIRST_ITEM, new { @style = "width:170px" })%>
                </td>
                <td rowspan="3" style="padding:0;">
                    
                </td>
            </tr>
            <tr style="height: 7px">
                <td>
                </td>
                <td>
                </td>
                <td>
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td>
                    <%=Html.DropDownList("assignId", ViewData[Constants.PURCHASE_REQUEST_ASSIGN_ID] as SelectList, Constants.PURCHASE_REQUEST_ASSIGN_FIRST_ITEM, new { @style = "width:175px" })%>
                </td>
                <td>
                    <%=Html.DropDownList("resolutionId", ViewData[Constants.PURCHASE_REQUEST_RESOLUTION_ID] as SelectList, Constants.PURCHASE_REQUEST_STATUS_FIRST_ITEM, new { @style = "width:220px" })%>
                </td>
                <td>
                    <%=Html.DatePicker("txtFromDate", (string)ViewData[Constants.PURCHASE_REQUEST_REPORT_FROM_DATE], new { @style = "width:140px", @onfocus = "ShowOnFocus(this,'" + Constants.PURCHASE_REQUEST_SUBMIT_FROM_DATE_KEY + "')", @onblur = "ShowOnBlur(this,'" + Constants.PURCHASE_REQUEST_SUBMIT_FROM_DATE_KEY + "')" })%>
                </td>
                <td>
                    <%=Html.DatePicker("txtToDate", (string)ViewData[Constants.PURCHASE_REQUEST_REPORT_TO_DATE], new { @style = "width:140px", @onfocus = "ShowOnFocus(this,'" + Constants.PURCHASE_REQUEST_SUBMIT_TO_DATE_KEY + "')", @onblur = "ShowOnBlur(this,'" + Constants.PURCHASE_REQUEST_SUBMIT_TO_DATE_KEY + "')" })%>
                </td>
                <td style="width:70px">
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
    <div style="width:1200px;">
        <fieldset style="width:340px; height:100%; margin:0;font-weight:bold; color:Black; background:none !important; float:right">
            <legend align="center" id="divDate_bottom" style="font-weight:bold; color:Black;padding: 0 10 !important; padding-bottom: 6px !important">
            </legend>
            <div id="divAll_bottom"  style="text-align:center"></div>
        </fieldset>
    </div>
    <div id="shareit-box">
        <img src='../../Content/Images/loading3.gif' alt='' />
    </div>
    <br />
    <table width="100%" cellpadding="0" cellspacing="0">
        <tr>
            <td style="padding-right: 50px" align="right">
                <div id="divOnPage"></div>                            
            </td>
        </tr>
    </table>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PageTitle" runat="server">
    <%= PurchaseRequestPageInfo.ComNameReport + CommonPageInfo.AppSepChar + CommonPageInfo.AppName%>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
    <link rel="stylesheet" type="text/css" href="/Content/Css/tooltip.css" />
    <script src="/Scripts/Tooltip.js" type="text/javascript"></script>
    <script type="text/javascript" src="/Scripts/hightchart/highcharts.js"></script>
    <style type="text/css">
        .urgent_row
        {
            background: none;
            background-color: #ffbbbb;
        }
        
        a.justification
        {
            color: Black;
            text-decoration: none;
            cursor: default;
        }
        #shareit-box
        {
            background: none repeat scroll 0 0 #FFFFDD;
            border-width: 1px;
            padding: 2px;
            max-width: 300px;
        }
        .ui-state-highlight a, .ui-widget-content .ui-state-highlight a
        {
            color: Black;
        }
    </style>
    
    <script type="text/javascript">
    function getTotal() {
        jQuery.ajax({
            url: '/PurchaseRequest/SetSumTotalOnPage',
            type: "GET",
            datatype: "json",
            success: function (result) {
                var Resultarray = result.split('-');
                var total='<strong style="color:Blue">Total on this page : </strong> <strong style="color:Red" >'+Resultarray[0]+'</strong> + <strong style="color:Red" >'+Resultarray[1]+'</strong>'
                $("#divOnPage").html(total);
               
            }
        });

    }
    function showChartByType(moneyOfMonth,listOfMonth,type)
    {
        
        var srData = new Array();
        var cateData = new Array();
        var sr = moneyOfMonth;
        var cate = listOfMonth;
        var srArr = sr.split(',');
        var cateArr = cate.split(',');
       
        $.each(srArr, function (no, val) {
            srData.push(parseFloat(val));
        });
        $.each(cateArr, function (no, val) {
            cateData.push(val);
        });
        var chart = new Highcharts.Chart({
            chart: {
                renderTo: 'chart-'+type+'-container',
                defaultSeriesType: 'line',
            },
            title: {
                text: type.toUpperCase()
            },
            xAxis: {
                categories: cateData
            },
            yAxis: {
                title: {
                    text: 'Total'
                }
            },
            series: [
                {
                    name: 'Cost',
                    data: srData
                }
            ]
        });
    }
    function showChart() {
        jQuery.ajax({
            url: '/PurchaseRequest/SetChartValue',
            type: "GET",
            datatype: "json",
            success: function (result) {
                var Resultarray = result.split('-');
                vndOfMonth=Resultarray[0];
                usdOfMonth=Resultarray[1];
                listOfMonth=Resultarray[2];
                showChartByType(Resultarray[0],Resultarray[2],'vnd');
                showChartByType(Resultarray[1],Resultarray[2],'usd');
            }
        });
       
        
    }
    function getTotalAll() {
        jQuery.ajax({
            async: false,
            url: '/PurchaseRequest/SetSumTotalAll',
            type: "GET",
            datatype: "json",
            success: function (result) {
                var Resultarray = result.split('-');
                var fromDate = $('#txtFromDate').val();
                var todate= $('#txtToDate').val();
                var date ='Cost statistics';
                if(isDate(fromDate) && isDate(todate)) {
                    date= 'Cost statistics from <strong style="color:Blue">' + fromDate + '</strong> to <strong style="color:Blue">'+ todate + '</strong>';
                } else if (isDate(fromDate)) {
                    date = 'Cost statistics from <strong style="color:Blue">' + fromDate + '</strong>';
                } else if (isDate(todate))  {
                    date = 'Cost statistics to <strong style="color:Blue">' + todate + '</strong>';
                } 
                var total='Total : <strong style="color:Red" >'+Resultarray[0]+'</strong>' + ' VND + ' +
                            '<strong style="color:Red" >' +Resultarray[1]+'</strong> USD';
                $("#divDate").html('');
                if( date != ''){
                    $("#divDate").html(date);
                    $("#divDate_bottom").html(date);
                }
                $("#divAll").html(total);
                $("#divAll_bottom").html(total);
            }
        });
        
    }
    function showhidePanel(obj) {
            $accordion = obj.next();
            // Kiểm tra nếu đang ẩn thì sẽ hiện và ẩn các phần tử khác
            // Nếu đang hiện thì click vào h3 sẽ ẩn
            if ($accordion.is(':hidden') === true) {
                $("#accordion .pncontent").slideUp();
                $("#accordion .pnup").attr('class', 'pndown');
                $accordion.slideDown();
                obj.children().first().attr('class', 'pnup');
            } else {
                $accordion.slideUp();
                obj.children().first().attr('class', 'pndown');
            }
        }
    box_color = "Black";
    box_sticky_color = "Black";
    tooltipoffsets = [0, 30];
    jQuery(document).ready(function () {
        $(function () {
                // Ẩn tất cả .accordion trừ accordion đầu tiên
                $("#accordion").hide();
                $("#accordion .panel .pntitle").children().first().attr('class', 'pnup');
                // Áp dụng sự kiện click vào thẻ h3
                $("#accordion .pntitle").click(function () {
                    showhidePanel($(this));
                });
                
                $("#btnToogle").click(function() {
                                     
                    if ($("#ghStatistic").is(':hidden') == true) {
                        $(this).text('Hide Chart');
                    } else {
                        $(this).text('Show Chart');
                    }
                    $("#ghStatistic").slideToggle();     
                    
                    if ($("#accordion").is(':hidden') == true) {
                        $(this).text('Hide Chart');
                    } else {
                        $(this).text('Show Chart');
                    }
                    $("#accordion").slideToggle();                    
                });
            });
        jQuery.validator.addMethod("checkCC", function (value, element) {
            var isvalid = true;
            var word = value.split(';');
            var keyName = "";
            for (var i in word) {
                if (i < word.length - 1) {
                    if (keyName.indexOf(word[i]) < 0) {
                        keyName += word[i] + ',';
                    }
                    else {
                        jQuery.validator.messages.checkCC = '<%=string.Format(Resources.Message.E0020,  "' + word[i] + '" , "CC List.")%>'; ;
                        isvalid = false;
                        break;
                    }
                }
            }
            return isvalid;
        }, jQuery.validator.messages.checkCC);


        CRM.onEnterKeyword();
        jQuery("#list").jqGrid({
            url: '/PurchaseRequest/GetListReportJQGrid/?purchaseId=' + encodeURIComponent($('#txtKeyword').val())
                     + '&department=' + $('#Department').val() + '&subdepartment=' + $('#SubDepartment').val()
                     + '&requestorId=' + $('#requestorId').val() + '&assignId=' + $('#assignId').val() + '&resolutionId=' + $('#resolutionId').val()
                     + '&fromdate=' + $('#txtFromDate').val() + '&todate=' + $('#txtToDate').val(),
            datatype: 'json',
            mtype: 'GET',
            colNames: ["ID", "Priority", "Req#", 'Request Date', 'Expected Date', 'Requestor', 'Department', 'Status', 'Forwarded To', 'Approval', 'Justification', 'Cost'],
            colModel: [
                    { name: 'RealID', index: 'RealID', align: "left", width: 10, hidden: true },
                    { name: 'Priority', index: 'Priority', align: "left", hidden: true },
                    { name: 'ID', index: 'ID', align: "left", width: 54, sortable: true },
                    { name: 'RequestDate', index: 'RequestDate', align: "left", width: 90, sortable: true },
                    { name: 'ExpectedDate', index: 'ExpectedDate', align: "left", width: 90, sortable: true },
                    { name: 'RequestorName', index: 'RequestorName', align: "left", width: 110, sortable: true },
                    { name: 'DepartmentName', index: 'DepartmentName', align: "left", width: 90, sortable: true },
                    { name: 'ResolutionName', index: 'ResolutionName', align: "left", width: 110, sortable: true },
                    { name: 'AssignName', index: 'AssignName', align: "left", width: 170, sortable: true },
                    { name: 'PurchaseApproval', index: 'PurchaseApproval', align: "left", width: 70, sortable: true },
                     { name: 'Justification', index: 'Justification', align: "left", width: 220, sortable: true },
                    { name: 'Total', index: 'Total', editable: false, width: 150, align: 'left', sortable: false}],
            pager: '#pager',
            sortname: '<%= (string)ViewData[Constants.PURCHASE_REQUEST_COLUMN]%>',
            sortorder: '<%= (string)ViewData[Constants.PURCHASE_REQUEST_ORDER]%>',
            rowNum: '<%= (string)ViewData[Constants.PURCHASE_REQUEST_ROW_COUNT]%>',
            page: '<%= (string)ViewData[Constants.PURCHASE_REQUEST_PAGE_INDEX]%>',
            rowList: [20, 30, 50, 100, 200],
            multiselect: true,
            width: 1200, height: "auto",
            imgpath: '/scripts/grid/themes/basic/images',
            loadui: 'block',
            loadComplete: function () {
                CRM.highlightRow(this, 'Priority', '<%=Constants.URGENT_VALUE%>', '#ffcccc');
                ShowTooltip($("a.justification"), $("#shareit-box"), "/PurchaseRequest/JustificationTooltip");
                //getTotal();
                getTotalAll();
                showChart();
            }
        });

        $("#btnRefresh").click(function () {
            window.location = "/PurchaseRequest/RefreshReport";
        });

        $("#btnExport").click(function () {
            var numberRow = $("#list").getGridParam("records");
            if (numberRow <= 0) {
                CRM.msgBox("Have no data for Export !", "300");
            }
            else {
                var name = $('#txtKeyword').val();
                if (name == '<%= Constants.PURCHASE_REQUEST  %>') {
                    name = "";
                }
                window.location = '/PurchaseRequest/ExportToExcel/?purchaseId=' + encodeURIComponent($('#txtKeyword').val())
                     + '&department=' + $('#Department').val() + '&subdepartment=' + $('#SubDepartment').val()
                     + '&requestorId=' + $('#requestorId').val() + '&assignId=' + $('#assignId').val() + '&statusId=' + $('#resolutionId').val()
                     + '&fromdate=' + $('#txtFromDate').val() + '&todate=' + $('#txtToDate').val() +'&page=Report'
            }

        });

        $("#Department").change(function () {
            $("#SubDepartment").html("");
            var department = $("#Department").val();
            $("#SubDepartment").append($("<option value=''><%= Constants.FIRST_ITEM_SUB_DEPARTMENT%></option>"));
            $.getJSON('/Library/GenericHandle/DropdownListHandler.ashx?ID=' + department + '&Page=SubDepartment', function (item) {
                $.each(item, function () {
                    if (this['ID'] != undefined) {
                        $("#SubDepartment").append($("<option value='" + this['ID'] + "'>" + this['Name'] + "</option>"));
                    }
                });
            });
        });

        //Filter
        $("#btnFilter").click(function () {

            var name = encodeURIComponent($('#txtKeyword').val());
            if (name == '<%= Constants.PURCHASE_REQUEST  %>') {
                name = "";
            };
            $('#list').setGridParam({ url: '/PurchaseRequest/GetListReportJQGrid/?purchaseId=' + name
                     + '&department=' + $('#Department').val() + '&subdepartment=' + $('#SubDepartment').val()
                     + '&requestorId=' + $('#requestorId').val() + '&assignId=' + $('#assignId').val() + '&resolutionId='
                     + $('#resolutionId').val() + '&fromdate=' + $('#txtFromDate').val() + '&todate=' + $('#txtToDate').val()
            }).trigger('reloadGrid');
            
            //getTotalAll();
        });

    });
    </script>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ModuleName" runat="server">
    <%= PurchaseRequestPageInfo.ComNameReport%>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="FunctionTitle" runat="server">
    <%= CommonFunc.GetCurrentMenu(Request.RawUrl)%>Report
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="LoginRoles" runat="server">
</asp:Content>
