<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="PageTitle" runat="server">
   <%=HiringCenterPageInfo.MenuName + CommonPageInfo.AppSepChar + HiringCenterPageInfo.ModInterview + CommonPageInfo.AppSepChar + CommonPageInfo.AppName%> 
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ModuleName" runat="server">
    <%= HiringCenterPageInfo.ComName%>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="FunctionTitle" runat="server">
    <%=CommonFunc.GetCurrentMenu(Request.RawUrl,true)%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <%= TempData["Message"]%>
   <%-- <%using (Html.BeginForm("Index", "Interview", FormMethod.Post, new { @id = "searchForm"}))
      { %>--%>
    <div id="cactionbutton">      
         <button type="button" id="btnRefresh" title="Refresh" class="button refresh">Refresh</button>           
        <button type="button" id="btnExport" title="Export" class="button export" onclick="return btnExport_onclick()">Export</button>        
    </div>    
    <div id="cfilter">        
            <table border="0" cellpadding="0" cellspacing="0">
                <tr>
                    <td>
                        <input type="text" maxlength="100" style="width: 150px" value="<%= ViewData[Constants.INTERVIEW_LIST_NAME] == null ?Constants.CANDIDATE_NAME: ViewData[Constants.INTERVIEW_LIST_NAME]  %>"
                            id="txtKeyword" onfocus="ShowOnFocus(this,'<%= Constants.CANDIDATE_NAME  %>')"
                            onblur="ShowOnBlur(this,'<%= Constants.CANDIDATE_NAME  %>')" autocomplete="off" />
                    </td>
                    <td>
                        <%=Html.DropDownList("InterviewStatus", ViewData[Constants.INTERVIEW_LIST_STATUS] as SelectList, Constants.FIRST_ITEM_STATUS, new { @style = "width:150px" })%>
                    </td>
                    <td>
                        <%=Html.DropDownList("InterviewResult", ViewData[Constants.INTERVIEW_LIST_RESULT] as SelectList, Constants.FIRST_ITEM_RESULT, new { @style = "width:150px" })%>
                    </td>
                    <td>
                        <%=Html.DropDownList("InterviewedBy", ViewData[Constants.INTERVIEW_LIST_INTERVIEWER] as SelectList, Constants.T_INTERVIEWED_BY, new { @style = "width:150px" })%>
                    </td>
                    <td>
                        Date from
                    </td>
                    <td>
                        <%=Html.TextBox("FromDate", (string)ViewData[Constants.INTERVIEW_LIST_DATE_FROM], new { @style = "width:70px" })%>
                    </td>
                    <td>
                        To
                    </td>
                    <td>
                        <%=Html.TextBox("ToDate", (string)ViewData[Constants.INTERVIEW_LIST_DATE_TO], new { @style = "width:70px" })%>
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
    <%--<% } %>--%>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
<style type="text/css">
 
    .ac_results
    {
        width:270px !important;
    }
</style>
    <script src="/Scripts/Grid/js/grid.grouping.js" type="text/javascript"></script>
    <script type="text/javascript">
        function ToHistory(id) {
            window.location='/Interview/TransferToHistory/'+id;
        }   
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
                    ToDate: { checkDate: true}
                }
            });

            jQuery("#list").jqGrid({
                url: '/Interview/GetListJQGrid/?can_name=' + encodeURIComponent($('#txtKeyword').val()) + '&status=' + $('#InterviewStatus').val()
                     + '&result=' + $('#InterviewResult').val() + '&interviewedBy=' + $('#InterviewedBy').val()
                     + '&from_date=' + $('#FromDate').val() + '&to_date=' + $('#ToDate').val(),                
                datatype: 'json',
                mtype: 'GET',
                colNames: ['Dept', 'JR', 'Candidate Name', 'Sub Dept', 'Position', 'Status', 'Result', 'Interviewed by', 'Time', 'Venue', 'Email to Candidate', 'Send Meeting Request', 'Action'],
                colModel: [
                  { name: 'Dept', index: 'Dept', align: "center", width: 20, hidden: true, sortable: false},
                  { name: 'JRId', index: 'JRId', align: "center", width: 50, sortable: false },
                  { name: 'DisplayName', index: 'DisplayName', align: "left", width:130, sortable: true },
                  { name: 'SubDept', index: 'SubDept', align: "center", width: 60, sortable: true },
                  { name: 'Position', index: 'Position', align: "center", width: 50, sortable: true },
                  { name: 'Status', index: 'Status', align: "center", width: 50, sortable: true },
                  { name: 'ResultName', index: 'ResultName', align: "center", width: 50, sortable: true },
                  { name: 'Pic', index: 'Pic', align: "center", width: 100, sortable: true },
                  { name: 'InterviewDate', index: 'InterviewDate', align: "center", width: 85, sortable: true },
                  { name: 'Venue', index: 'Venue', align: "center", width: 85, sortable: true },
                  { name: 'IsSentMailCandidate', index: 'IsSentMailCandidate', align: "center", width: 85, sortable: true },                      
                  { name: 'IsSendMailInterviewer', index: 'IsSendMailInterviewer', align: "center", width: 85, sortable: true },                  
                  { name: 'Action', index: 'Action', editable: false, width: 60, align: 'center', sortable: false}],                
                pager: jQuery('#pager'),
                rowList: [20, 30, 50, 100, 200],                
                viewrecords: true,
                width: 1200, height: "auto",
                grouping: true,
                rownumbers: true,
                sortname: '<%= (string)ViewData[Constants.INTERVIEW_LIST_COLUMN]%>',
                sortorder: '<%= (string)ViewData[Constants.INTERVIEW_LIST_ORDER]%>',
                rowNum: '<%= (string)ViewData[Constants.INTERVIEW_LIST_ROW_COUNT]%>',
                page: '<%= (string)ViewData[Constants.INTERVIEW_LIST_PAGE_INDEX]%>',
                onSelectRow: function (rowId) {
                    $("#list").expandSubGridRow(rowId);
                },
                groupingView: { groupField: ['Dept'],
                    groupColumnShow: [false],
                    groupText: ['<b>{0} </b> - {1} Item(s)'],
                   groupCollapse: false
                },
                gridComplete: function () {
                    var rowIds = $("#list").getDataIDs();
                    $.each(rowIds, function (index, rowId) {
                        $("#list").expandSubGridRow(rowId);
                    });
                },
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

                    var url_send = '/Interview/ExportToExcel?can_name=' + encodeURIComponent(name) + '&status=' + $('#InterviewStatus').val() + '&result=' + $('#InterviewResult').val();
                    url_send = url_send + '&interviewedBy=' + $('#InterviewedBy').val() + '&from_date=' + $('#FromDate').val() + '&to_date=' + $('#ToDate').val();
                    window.location = url_send;
                }

            });

            $('#btnRefresh').click(function () {
                window.location = "/Interview/Refresh";
            });

            $("#btnFilter").click(function () {
                var name = $('#txtKeyword').val();

                if (name == '<%= Constants.CANDIDATE_NAME  %>') {
                    name = "";
                }
                var url_send = '/Interview/GetListJQGrid?can_name=' + encodeURIComponent(name) + '&status=' + $('#InterviewStatus').val() + '&result=' + $('#InterviewResult').val();
                url_send = url_send + '&interviewedBy=' + $('#InterviewedBy').val() + '&from_date=' + $('#FromDate').val() + '&to_date=' + $('#ToDate').val();

                $('#list').setGridParam({ url: url_send });
                $("#list").trigger('reloadGrid');
            });
        });

    </script>
    <link rel="stylesheet" type="text/css" href="/Content/Css/tooltip.css" />
    <script src="/Scripts/Tooltip.js" type="text/javascript"></script>
</asp:Content>
