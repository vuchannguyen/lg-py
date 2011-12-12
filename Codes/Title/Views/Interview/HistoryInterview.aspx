<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="PageTitle" runat="server">
    <%=HiringCenterPageInfo.MenuName + CommonPageInfo.AppSepChar + HiringCenterPageInfo.ModInterviewHistory + CommonPageInfo.AppSepChar + CommonPageInfo.AppName%>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ModuleName" runat="server">
    <%= HiringCenterPageInfo.ComName%>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="FunctionTitle" runat="server">
    <%=CommonFunc.GetCurrentMenu(Request.RawUrl,true)%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <%= TempData["Message"]%>    
    <%using (Html.BeginForm("HistoryInterview", "Interview", FormMethod.Post, new { @id = "searchForm" }))
      { %>
    <div id="cactionbutton">                
         <button type="button" id="btnRefresh" title="Refresh" class="button refresh">Refresh</button>    
        <button type="button" id="btnExport" title="Export" class="button export">Export</button>        
    </div>  
    <div id="cfilter">        
            <table border="0" cellpadding="0" cellspacing="0">
                <tr>
                    <td>
                        <input type="text" maxlength="100" style="width: 150px" value="<%= (string)ViewData[Constants.INTERVIEW_LIST_HISTORY_NAME]  %>"
                            id="txtKeyword" onfocus="ShowOnFocus(this,'<%= Constants.CANDIDATE_NAME  %>')"
                            onblur="ShowOnBlur(this,'<%= Constants.CANDIDATE_NAME  %>')" autocomplete="off" />
                    </td>
                    <td>
                        <%=Html.DropDownList("Source", ViewData[Constants.INTERVIEW_LIST_HISTORY_SOURCE] as SelectList, Constants.CANDIDATE_SOURCE, new { @style = "width:100px" })%>
                    </td>
                    <td>
                        <%=Html.DropDownList("TitleId", ViewData[Constants.INTERVIEW_LIST_HISTORY_POSITION] as SelectList, Constants.FIRST_ITEM_JOBTITLE, new { @style = "width:150px" })%>
                    </td>
                    <td>
                        <%=Html.DropDownList("InterviewResult", ViewData[Constants.INTERVIEW_LIST_HISTORY_RESULT] as SelectList, Constants.FIRST_ITEM_RESULT, new { @style = "width:150px" })%>
                    </td>                    
                    <td>
                        Date from
                    </td>
                    <td>
                        <%=Html.TextBox("FromDate", (string)ViewData[Constants.INTERVIEW_LIST_HISTORY_DATE_FROM], new { @style = "width:70px" })%>
                    </td>
                    <td>
                        To
                    </td>
                    <td>
                        <%=Html.TextBox("ToDate", (string)ViewData[Constants.INTERVIEW_LIST_HISTORY_DATE_TO], new { @style = "width:70px" })%>
                    </td>
                    <td>                        
                        <button type="button"  id="btnFilter" title="Filter" class="button filter">Filter</button>
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
    <% } %>
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

            $("#FromDate").datepicker();
            $("#ToDate").datepicker();
            //$("#txtKeyword").autocomplete('/Library/GenericHandle/AutoCompleteHandler.ashx/?Page=Candidate');
            CRM.onEnterKeyword();

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

            jQuery("#list").jqGrid({
                url: '/Interview/GetListJQGridHistory/?can_name=' + encodeURIComponent($('#txtKeyword').val()) + '&source=' + $('#Source').val() +
                    '&result=' + $('#InterviewResult').val() + '&position=' + $('#TitleId').val() + '&from_date=' + $('#FromDate').val() + '&to_date=' + $('#ToDate').val(),
                datatype: 'json',
                mtype: 'GET',
                colNames: ['No', 'ID', 'Candidate Name', 'Email', 'Phone', 'Gender', 'Searched date', 'Source', 'Position', 'Result', 'Note', 'Action'],
                colModel: [
                  { name: 'No', index: 'No', align: "center", width: 20, hidden: false, sortable: false },
                  { name: 'ID', index: 'ID', align: "center", width: 20, hidden: true, sortable: false },
                  { name: 'DisplayName', index: 'DisplayName', align: "left", width: 120, sortable: true },
                  { name: 'Email', index: 'Email', align: "left", width: 80, sortable: true },
                  { name: 'CellPhone', index: 'CellPhone', align: "center", width: 60, sortable: true },
                  { name: 'SearchBy', index: 'SearchBy', align: "center", width: 80, sortable: true },
                  { name: 'SearchDate', index: 'SearchDate', align: "center", width: 80, sortable: true },
                  { name: 'Source', index: 'Source', align: "center", width: 100, sortable: true },
                  { name: 'Title', index: 'Title', align: "center", width: 100, sortable: true },
                  { name: 'ResultName', index: 'ResultName', align: "center", width: 80, sortable: true },
                  { name: 'Note', index: 'Note', align: "center", width: 60, sortable: true },
                  { name: 'Action', index: 'Action', editable: false, width: 35, align: 'center', sortable: false}],
                pager: jQuery('#pager'),
                rowNum: 20,
                rowList: [20, 30, 50, 100, 200],
                sortname: 'DisplayName',
                sortorder: "asc",
                viewrecords: true,
                width: 1200, height: "auto",
                imgpath: '/scripts/grid/themes/basic/images',
                loadui: 'block',
                loadComplete: function () {
                    ShowTooltip($("a[class=showTooltip]"), $("#shareit-box"), "/Candidate/CandidateToolTip");
                }
            });

            $("#btnExport").click(function () {
                var numberRow = $("#list").getGridParam("records");
                if (numberRow <= 0) {
                    CRM.msgBox('<%= Resources.Message.E0027 %>', "300");
                } else {
                    var name = $('#txtKeyword').val();
                    if (name == '<%= Constants.CANDIDATE_NAME  %>') {
                        name = "";
                    }
                    var url_send = '/Interview/ExportHistoryToExcel?can_name=' + encodeURIComponent(name) + '&source=' + $('#Source').val();
                    url_send = url_send + '&result=' + $('#InterviewResult').val() + '&position=' + $('#TitleId').val() + '&from_date=' + $('#FromDate').val() + '&to_date=' + $('#ToDate').val();
                    window.location = url_send;
                }

            });

            $('#btnRefresh').click(function () {
                window.location = "/Interview/Refresh/2";
            });

            $("#btnFilter").click(function () {
                var name = $('#txtKeyword').val();
                var url_send = '/Interview/GetListJQGridHistory?can_name=' + encodeURIComponent(name) + '&source=' + $('#Source').val();
                url_send = url_send + '&result=' + $('#InterviewResult').val() + '&position=' + $('#TitleId').val() + '&from_date=' + $('#FromDate').val() + '&to_date=' + $('#ToDate').val();
                $('#list').setGridParam({ url: url_send });
                $("#list").trigger('reloadGrid');
            });
        });

    </script>
    <link rel="stylesheet" type="text/css" href="/Content/Css/tooltip.css" />
    <script src="/Scripts/Tooltip.js" type="text/javascript"></script>
</asp:Content>
