<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <%= TempData["Message"]%>
    <div id="cactionbutton" style="width:1024px;">   
        <button id="btnRefresh" type="button" title="Refresh" class="button refresh">Refresh</button> 
        <button id="btnExport" type="button" title="Export" class="button export">Export</button>              
    </div>
    <div id="cfilter">        
        <table >
            <tr>
                <td>
                    Request Date :
                </td>
                <td>
                    From
                </td>
                <td>
                    <% Response.Write(Html.TextBox("Fromdate", (string)ViewData[Constants.SR_REPORT_WEEKLY_ADMIN_START_DATE], new { @style = "width:80px" }));%>
                </td>
                <td>
                    To
                </td>
                <td>
                    <% Response.Write(Html.TextBox("Todate", (string)ViewData[Constants.SR_REPORT_WEEKLY_ADMIN_END_DATE], new { @style = "width:80px" })); %>
                </td>           
                <td>
                    <button type="button" id="btnFilter" title="Filter" style="float: left" class="button filter">
                        Filter</button>
                </td>     
            </tr>
        </table>
    </div>

    <div id="tabs">
    <ul>
	    <li><a id="t-1" href="#tabs-1" onclick="getContentTab(1);">Request Active</a></li>
        <li><a id="t-2" href="#tabs-2" onclick="getContentTab(2);">Request Closed</a></li>
        <li><a id="t-3" href="#tabs-3" onclick="getContentTab(3);">Detail IT Team</a></li>
    </ul>
    <div id="list">
    <div id="tabs-1"></div>
    <div id="tabs-2"> </div>
    <div id="tabs-3"> </div>
    </div>
    </div>
    <div id="shareit-box">
        <img src='../../Content/Images/loading3.gif' alt='' />
    </div>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="PageTitle" runat="server">
<%= ServiceRequestPageInfo.ComName + CommonPageInfo.AppSepChar + ServiceRequestPageInfo.FuncNameReportWeekly + 
        CommonPageInfo.AppSepChar + CommonPageInfo.AppName%>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
<script type="text/javascript">
        var cur_index = 1;
        function getContentTab(index) {            
            cur_index = index;
            var url='/ServiceRequestAdmin/getAjaxTab?id=' + index + '&startDate=' + $("#Fromdate").val() + '&endDate=' + $("#Todate").val() ;
            var targetDiv = "#tabs-" + index;
            var ajaxLoading = "<img id='ajax-loader' src='<%= Url.Content("~/Content/Images") %>/ajax-loader.gif' align='left' height='28' width='28'>";

            $(targetDiv).html("<p>" + ajaxLoading + " Loading...</p>"); 

            $.get(url,null, function(result) {
                $(targetDiv).html(result);
                ShowTooltip($("a[class=showTooltip]"), $("#shareit-box"), "/ServiceRequestAdmin/ShowTitleTooltip");
            });
        }
        
        $(document).ready(function() {

            CRM.onEnterKeyword();
            $("#Fromdate").datepicker();
            $("#Todate").datepicker();
            $("#tabs").tabs();
            $("#t-1").click();
            $("#btnExport").click(function () {
                CRM.loading();                
                window.location = '/ServiceRequestAdmin/ExportWeeklyReport' + '?startDate=' + $("#Fromdate").val() + '&endDate=' + $("#Todate").val() ;                
                CRM.completed();
            });
                      
           $("#btnRefresh").click(function () {
                window.location = "/ServiceRequestAdmin/RefreshWeeklyReport";
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
                    $("#t-" + cur_index).click();                
                }
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
