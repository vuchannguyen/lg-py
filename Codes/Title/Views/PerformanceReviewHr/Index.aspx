<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <%= TempData[CommonDataKey.TEMP_SYSTEM_MESSAGE]%>
    <div id="cactionbutton">
        <button type="button" id="btnRefresh" title="Refresh" class="button refresh">Refresh</button>
    </div>
    <div id="cfilter">
        <table>
            <tr>
                <td>
                    <input type="text" maxlength="50" style="width: 150px" value="<%= ViewData[Constants.PRW_HR_LIST_NAME] == null ?Constants.PER_REVIEW_FIRST_KEY_WORD: ViewData[Constants.PRW_HR_LIST_NAME] %>"
                        id="txtKeyword" onfocus="ShowOnFocus(this,'<%= Constants.PER_REVIEW_FIRST_KEY_WORD%>')"
                        onblur="ShowOnBlur(this,'<%= Constants.PER_REVIEW_FIRST_KEY_WORD  %>')" />
                </td>
                <td>
                    <%=Html.DropDownList(Constants.PRW_HR_LIST_STATUS, null, Constants.FIRST_ITEM_STATUS, new { @style = "width:150px;" })%>
                    
                </td>
                <td>
                    <%=Html.DropDownList(Constants.PRW_HR_LIST_NEED, null, new { @style ="width:150px;"})%>
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
    <div id="shareit-box">    
        <img src='../../Content/Images/loading3.gif' alt='' />    
    </div>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PageTitle" runat="server">
<%=PerformanceReviewPageInfo.ModName + CommonPageInfo.AppSepChar + CommonPageInfo.AppName%>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
    <script src="/Scripts/Grid/js/grid.grouping.js" type="text/javascript"></script>
    <script src="/Scripts/Grid/js/grid.subgrid.js" type="text/javascript"></script>
    <script src="/Scripts/Tooltip.js" type="text/javascript"></script>
    <link href="/Content/Css/tooltip.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        function getListTargetUrl() {
            var url = '/PerformanceReviewHr/GetListHrJQGrid/?' +
                'filterText=' + encodeURIComponent($("#txtKeyword").val()) +
                '&status=' + $('#<%=Constants.PRW_HR_LIST_STATUS%>').val() +
                '&need=' + $('#<%=Constants.PRW_HR_LIST_NEED%>').val();
            return url;
        }
        function GetAutoCompleteUrl() {
            var url = '/Library/GenericHandle/AutoCompleteHandler.ashx/?Page=HRPR';
            var param = getListTargetUrl();
            param = param.substring(param.indexOf("&"), param.length);
            return url + param;
        }
        $(document).ready(function () {
            //$("#txtKeyword").autocomplete(GetAutoCompleteUrl());
            CRM.onEnterKeyword();
            jQuery("#list").jqGrid({
                url: getListTargetUrl(),
                datatype: 'json',
                mtype: 'GET',
                colNames: ['ID', 'Employee', 'Department', 'Manager', 'Next PR Date'],
                colModel: [
                  { name: 'ID', index: 'ID', align: "center", width: 30, sortable: true },
                  { name: 'LoginName', index: 'LoginName', align: "Left", width: 100, sortable: true },
                  { name: 'Department', index: 'Department', align: "center", width: 70, sortable: true },
                  { name: 'ManagerName', index: 'ManagerName', align: "left", width: 100, sortable: true },
                  { name: 'NextReviewDate', index: 'NextReviewDate', align: "center", width: 40, sortable: false }],
                  
                pager: jQuery('#pager'),
                rowList: [20, 30, 50, 80, 100],
                viewrecords: true,
                multiselect: false,
                width: 1024, height: "auto",
                sortname: 'UpdateDate',
                sortorder: 'asc',

                rowNum: 20,
                page: 1,
                imgpath: '/scripts/grid/themes/basic/images',
                loadui: 'block',
                subGrid: true,
                subGridUrl: '/PerformanceReviewHR/GetListHrJQSubGrid/',
                subGridModel:
                [
                    {
                        name: ['ID', 'Suppervisor', 'PR Date', 'Status', 'Resolution', 'Forward To', 'Action'],
                        width: [100, 200, 100, 91, 140, 212, 75],
                        align: ['center', 'left', 'center', 'left', 'left', 'left', 'center']
                    }
                ],
                loadComplete: function () {
                    ShowTooltip($("a[class=showTooltip]"), $("#shareit-box"), "/Common/PTODetailTooltip");
                }
            });

            $("#btnFilter").click(function () {
                var url_send = getListTargetUrl();
                $('#list').setGridParam({ url: url_send, page: 1 });
                $("#list").trigger('reloadGrid');
            });

            $("#btnRefresh").click(function () {
                window.location = "/PerformanceReviewHR/Refresh";
            });

        });
    </script>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="ModuleName" runat="server">
    <%= PerformanceReviewPageInfo.ModName%>
</asp:Content>

<asp:Content ID="Content6" ContentPlaceHolderID="FunctionTitle" runat="server">
    <%= CommonFunc.GetCurrentMenu(Request.RawUrl,true)%>
</asp:Content>

<asp:Content ID="Content7" ContentPlaceHolderID="LoginRoles" runat="server">
</asp:Content>