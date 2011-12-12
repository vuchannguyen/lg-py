<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="PageTitle" runat="server">
<%=HiringCenterPageInfo.MenuName + CommonPageInfo.AppSepChar + HiringCenterPageInfo.ModCandidate +
               CommonPageInfo.AppSepChar + CommonPageInfo.AppName%>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ModuleName" runat="server">
    <% Response.Write(HiringCenterPageInfo.ComName ); %>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="FunctionTitle" runat="server">
    <%--<% Response.Write(HiringCenterPageInfo.MenuName + CommonPageInfo.AppDetailSepChar + HiringCenterPageInfo.ModCandidate); %>--%>
    <%= CommonFunc.GetCurrentMenu(Request.RawUrl,true)%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <%= TempData["Message"]%>
    <div id="cactionbutton">
        <button type="button" id="btnRefresh" title="Refresh" class="button refresh">Refresh</button> 
        <button type="button" id="btnExport" title="Export" class="button export">Export</button>
        <button type="button" id="btnDelete" title="Delete" class="button delete">Delete</button> 
        <button type="button" id="btnAddNew" title="Add New" class="button addnew">Add New</button>
    </div>
    
    <div id="cfilter">
            <table border="0" cellpadding="0" cellspacing="0">
                <tr>
                    <td>
                        <input type="text" maxlength="100" style="width: 100px" value="<%= (string)ViewData[Constants.CANDIDATE_LIST_NAME] %>"
                        id="txtKeyword" onfocus="ShowOnFocus(this,'<%= Constants.CANDIDATE_NAME  %>')"
                        onblur="ShowOnBlur(this,'<%= Constants.CANDIDATE_NAME  %>')" autocomplete="off"  />
                    </td>                    
                    <td>
                        <%=Html.DropDownList("LocationOffice", ViewData[Constants.LOCATION_LIST_OFFICE] as SelectList, Constants.FIRST_ITEM_OFFICE, new { @style = "width:130px" })%>
                    </td>
                    <td>
                        <%=Html.DropDownList("Source", ViewData[Constants.CANDIDATE_LIST_SOURCE] as SelectList, Constants.CANDIDATE_SOURCE, new { @style = "width:100px" })%>
                    </td>
                    <td>
                        <%=Html.DropDownList("TitleId", ViewData[Constants.CANDIDATE_LIST_JOB_TITLE] as SelectList, Constants.FIRST_ITEM_JOBTITLE, new { @style = "width:150px" })%>
                    </td>
                    <td>
                        <%=Html.DropDownList("Status", ViewData[Constants.CANDIDATE_LIST_STATUS] as SelectList, Constants.FIRST_ITEM_STATUS, new { @style = "width:110px" })%>
                    </td>
                    <td>
                        Searched from
                    </td>
                    <td>
                        <%=Html.TextBox("FromDate", (string)ViewData[Constants.CANDIDATE_LIST_FROM_DATE], new { @style = "width:70px" })%>
                    </td>
                    <td>
                        Searched to
                    </td>
                    <td>
                        <%=Html.TextBox("ToDate", (string)ViewData[Constants.CANDIDATE_LIST_TO_DATE], new { @style = "width:70px" })%>
                    </td>
                    <td>
                        <%=Html.DropDownList("University", ViewData[Constants.CANDIDATE_LIST_UNIVERSITY] as SelectList, Constants.FIRST_ITEM_UNIVERSITY, new { @style = "width:140px" })%>
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
        width:270px !important;
    }
</style>
    <script type="text/javascript">
        $(document).ready(function () {
            CRM.onEnterKeyword();
            $("#FromDate").datepicker();
            $("#ToDate").datepicker();
            
            jQuery("#list").jqGrid({
                url: '/Candidate/GetListJQGrid?can_name=' + encodeURIComponent($('#txtKeyword').val()) + '&office=' + $('#LocationOffice').val() + '&source=' + $('#Source').val()
                    + '&titleId=' + $('#TitleId').val() + '&status=' + $('#Status').val() + '&from_date=' + $('#FromDate').val() + '&to_date=' + $('#ToDate').val()
                    + '&university=' + $('#University').val(),
                datatype: 'json',
                mtype: 'POST',
                colNames: ['No', 'ID', 'Candidate Name', 'DOB', 'Telephone', 'Gender', 'Searched date', 'Source', 'Position','University','Status',  'Action'],
                colModel: [
                  { name: 'No', index: 'No', align: "center", width: 20, hidden: false, sortable: false },
                  { name: 'ID', index: 'ID', align: "center", width: 50, hidden: true },
                  { name: 'CandidateName', index: 'CandidateName', align: "left", sortable: true },
                  { name: 'DOB', index: 'DOB', align: "center", width: 60, sortable: true },
                  { name: 'CellPhone', index: 'CellPhone', align: "center", width: 60, sortable: true },
                  { name: 'Gender', index: 'Gender', align: "center", width: 45, sortable: true },
                  { name: 'SearchDate', index: 'SearchDate', align: "center", width: 80, sortable: true },
                  { name: 'Source', index: 'Source', align: "center", width: 90, sortable: true },
                  { name: 'Title', index: 'Title', align: "center", width: 100, sortable: true },
                  { name: 'University', index: 'University', align: "left", width: 100, sortable: true },
                  { name: 'Status', index: 'Status', editable: false, width: 50, align: 'center', sortable: true },
                  { name: 'Action', index: 'Action', editable: false, width: 40, align: 'center', sortable: false}],
                pager: jQuery('#pager'),
                rowList: [20, 30, 50, 100, 200],
                sortname: '<%= (string)ViewData[Constants.CANDIDATE_LIST_COLUMN]%>',
                sortorder: '<%= (string)ViewData[Constants.CANDIDATE_LIST_ORDER]%>',
                rowNum: '<%= (string)ViewData[Constants.CANDIDATE_LIST_ROW_COUNT]%>',
                page: '<%= (string)ViewData[Constants.CANDIDATE_LIST_PAGE_INDEX]%>',
                multiselect: true,
                viewrecords: true,
                width: 1200, height: "auto",
                imgpath: '/scripts/grid/themes/basic/images',
                loadui: 'block',
                loadComplete: function () {
                    ShowTooltip($("a[class=showTooltip]"), $("#shareit-box"), "/Candidate/CandidateToolTip");
                }
            });

            $("#btnDelete").click(function () {
                CRM.deleteList('#list', 'ID', '/Candidate/DeleteList');
            });

            $('#btnRefresh').click(function () {
                window.location.href = "/Candidate/Refresh";
            });

            $('#btnAddNew').click(function () {
                window.location.href = '/Candidate/Create';
            });
            $("#btnExport").click(function () {
                var numberRow = $("#list").getGridParam("records");
                if (numberRow <= 0) {
                    CRM.msgBox('<%= Resources.Message.E0027 %>', "300");

                } else {
                    var url_send = '/Candidate/ExportToExcel?can_name=' + encodeURIComponent($('#txtKeyword').val()) + '&source=' + $('#Source').val();
                    url_send = url_send + '&titleId=' + $('#TitleId').val() + '&status=' + $('#Status').val() + '&from_date=' + $('#FromDate').val() + '&to_date=' + $('#ToDate').val()
                    + '&university=' + $('#University').val() + "&officeId=" + $("#LocationOffice").val();
                    window.location = url_send;
                }

            });

            $("#btnFilter").click(function () {
                var url_send = '/Candidate/GetListJQGrid?can_name=' + encodeURIComponent($('#txtKeyword').val()) + '&office=' + $('#LocationOffice').val() + '&source=' + $('#Source').val();
                url_send = url_send + '&titleId=' + $('#TitleId').val() + '&status=' + $('#Status').val() + '&from_date=' + $('#FromDate').val() + '&to_date=' + $('#ToDate').val()
                +'&university=' + $('#University').val();
                $('#list').setGridParam({ url: url_send });
                $("#list").trigger('reloadGrid');
            });
        });
    </script>
    <link rel="stylesheet" type="text/css" href="/Content/Css/tooltip.css" />
    <script src="/Scripts/Tooltip.js" type="text/javascript"></script>
</asp:Content>
