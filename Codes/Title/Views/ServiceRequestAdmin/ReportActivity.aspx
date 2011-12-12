<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <%= TempData["Message"]%>
    <div id="cactionbutton">   
        <button id="btnRefresh" type="button" title="Refresh" class="button refresh">Refresh</button> 
        <button id="btnExport" type="button" title="Export" class="button export">Export</button>              
    </div>
    <div id="cfilter">        
        <table >
            <tr height="35">
                <td>
                    Request Date :
                </td>
                <td>
                    From
                </td>
                <td>
                    <% Response.Write(Html.TextBox("Fromdate", (string)ViewData[Constants.SR_REPORT_ACTIVITY_ADMIN_START_DATE], new { @style = "width:80px" }));%>
                </td>
                <td>
                    To
                </td>
                <td>
                    <% Response.Write(Html.TextBox("Todate", (string)ViewData[Constants.SR_REPORT_ACTIVITY_ADMIN_END_DATE], new { @style = "width:80px" })); %>
                </td>           
                <td>
                    <button type="button" id="btnFilter" title="Filter" style="float: left" class="button filter">
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
    <div id="shareit-box">
        <img src='../../Content/Images/loading3.gif' alt='' />
    </div></asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="PageTitle" runat="server">
    <%= ServiceRequestPageInfo.ComName + CommonPageInfo.AppSepChar + ServiceRequestPageInfo.FuncNameReportActivity + 
        CommonPageInfo.AppSepChar + CommonPageInfo.AppName%>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
<script type="text/javascript">

    function getListTargetUrl() {
        var url = '/ServiceRequestAdmin/GetListJQGridReportActivity/?' +
                'startDate=' + $('#Fromdate').val() +
                '&endDate=' + $('#Todate').val();
        return url;
    }

    $(document).ready(function () {
        CRM.onEnterKeyword();
        $("#Fromdate").datepicker();
        $("#Todate").datepicker();

        $("#searchForm").validate({
            debug: false,
            errorElement: "span",
            errorPlacement: function (error, element) {
                error.tooltip({
                    bodyHandler: function () {
                        return error.html();
                    }
                });
                error.insertAfter(element);
            },
            rules: {
                FromDate: { checkDate: true },
                ToDate: { checkDate: true }
            }
        });

        $("#btnRefresh").click(function () {
            window.location = "/ServiceRequestAdmin/RefreshActivityReport";
        });

        jQuery("#list").jqGrid({
            url: getListTargetUrl(),
            datatype: 'json',
            mtype: 'GET',
            colNames: ['Department', 'SR Count', 'Total Time'],
            colModel: [
                  { name: 'Department', index: 'Department', align: "center", width: 160, sortable: true },
                  { name: 'SR_Count', index: 'SR_Count', align: "center", width: 80, sortable: true },
                  { name: 'TotalTime', index: 'TotalTime', align: "center", width: 80, sortable: true}],
            viewrecords: true,
            width: 1024, height: "auto",
            rownumbers: true,
            grouping: false,
            sortname: '<%= (string)ViewData[Constants.SR_REPORT_ACTIVITY_LIST_ADMIN_COLUMN]%>',
            sortorder: '<%= (string)ViewData[Constants.SR_REPORT_ACTIVITY_LIST_ADMIN_ORDER]%>',
            imgpath: '/scripts/grid/themes/basic/images',
            loadui: 'block',
            footerrow: true,
            userDataOnFooter: true,
            altRows: true
        });

        $("#btnFilter").click(function () {

            var isValid = true;
            if ($('#Fromdate').val() != '') {
                if (!isDate($('#Fromdate').val())) {
                    alert("Request Date From is invalid.");
                    $('#Fromdate').focus();
                    isValid = false;
                }
            }
            if ($('#Todate').val() != '') {
                if (!isDate($('#Todate').val())) {
                    alert("Request Date To is invalid.");
                    $('#Todate').focus();
                    isValid = false;
                }
            }
            if (isValid) {
                var url_send = getListTargetUrl();

                $('#list').setGridParam({ url: url_send });
                $("#list").trigger('reloadGrid');
            }
        });

        $("#btnExport").click(function () {
            CRM.loading();
            var numberRow = $("#list").getGridParam("records");
            if (numberRow <= 0) {
                CRM.msgBox('<%= Resources.Message.E0027 %>', "300");
            }
            else {
                window.location = "/ServiceRequestAdmin/ExportActivityExcel";
            }
            CRM.completed();
        });
    });
    
</script>
    <link rel="stylesheet" type="text/css" href="/Content/Css/tooltip.css" />
    <script src="/Scripts/Tooltip.js" type="text/javascript"></script>
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="ModuleName" runat="server">
<%= ServiceRequestPageInfo.ComName%>
</asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="FunctionTitle" runat="server">
<%=CommonFunc.GetCurrentMenu(Request.RawUrl).Trim().TrimEnd('»')%>
</asp:Content>

<asp:Content ID="Content6" ContentPlaceHolderID="LoginRoles" runat="server">
</asp:Content>
