<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="PageTitle" runat="server">
    <%=LOTPageInfo.MenuName + CommonPageInfo.AppSepChar+ LOTPageInfo.AssignCandidate + CommonPageInfo.AppSepChar + LOTPageInfo.ComName + CommonPageInfo.AppSepChar + CommonPageInfo.AppName%>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ModuleName" runat="server">
    <%= LOTPageInfo.ComName%>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="FunctionTitle" runat="server">
    <%--<%= LOTPageInfo.MenuName + CommonPageInfo.AppDetailSepChar+
      "<a href='/Exam/Index'>" + LOTPageInfo.Exam + "</a>"+ CommonPageInfo.AppDetailSepChar+
       ViewData[CommonDataKey.EXAM_TITLE].ToString() + CommonPageInfo.AppDetailSepChar+LOTPageInfo.AssignCandidate %>--%>
     <%= CommonFunc.GetCurrentMenu(Request.RawUrl)+
       ViewData[CommonDataKey.EXAM_TITLE].ToString() + CommonPageInfo.AppDetailSepChar+LOTPageInfo.AssignCandidate %>
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <%= TempData["Message"]%>
    <div>
        <%=Html.Hidden(CommonDataKey.EXAM_ID, ViewData[CommonDataKey.EXAM_ID] as SelectList)%>
    </div>
    <div id="cactionbutton">
        <button id="btnExportList" type="button" title="Export Candidate List" class="button export">
            Export Candidate List</button>
        <button id="btnExportPin" type="button" title="Export Candidate PIN" class="button export">
            Export Candidate PIN</button>
        <button id="btnDelete" type="button" title="Delete" class="button delete">
            Delete</button>
        <button id="btnAddNew" type="button" title="Assgin" class="button addnew">
            Assign</button>
        <button type="button" id="btnBack" title="Back" class="button back">
            Back</button>
    </div>
    <div class="clist">
        <table id="list" class="scroll">
        </table>
        <div id="pager" class="scroll" style="text-align: center;">
        </div>
    </div>
    <div id="shareit-box">
        <img src="../../../../Content/Images/loading3.gif" alt='' />
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
    <script type="text/javascript">
        $(document).ready(function () {
            jQuery("#list").jqGrid({
                url: '/Exam/GetAssignEmployeeListJQGrid?examID=' + $('#ExamID').val(),
                datatype: 'json',
                colNames: ['ID', 'ID', 'Full Name', 'Job Title', 'Department', 'Sub Department', 'Start Date', 'Status', 'Pin'],
                colModel: [
                  { name: 'ID', index: 'ID', align: "center", width: 30, title: false, hidden: true },
                  { name: 'EmployeeID', index: 'EmployeeID', align: "center", width: 30, title: false },
                  { name: 'DisplayName', index: 'DisplayName', align: "left", sortable: true, title: false },
                  { name: 'JobTitle', index: 'JobTitle', align: "center", width: 120, title: false },
                  { name: 'Department', index: 'Department', align: "center", width: 100, title: false },
                  { name: 'SubDepartment', index: 'SubDepartment', align: "center", width: 120, title: false },
                  { name: 'StartDate', index: 'StartDate', align: "center", width: 75, title: false },
                  { name: 'Status', index: 'Status', align: "center", width: 65, title: false },
                  { name: 'CandidatePin', index: 'CandidatePin', align: "center", width: 70, sortable: true }
                  ],
                pager: jQuery('#pager'),
                rowList: [20, 30, 50, 100, 200],
                sortname: 'ID',
                sortorder: "asc",
                multiselect: true,
                viewrecords: true,
                width: 1200, height: 'auto',
                imgpath: '/scripts/grid/themes/basic/images',
                loadui: 'block',
                loadComplete: function () {
                    ShowTooltip($("a[class=showTooltip]"), $("#shareit-box"), "/Employee/EmployeeToolTip");
                }
            });

            //set width for checkbox column
            jQuery("#jqgh_cb").parent().css("width", "15px");
            jQuery("tr#_empty > td:first").css("width", "15px");

            $('#btnAddNew').click(function () {
                CRM.popup('/Exam/AssignEmployee/' + $('#ExamID').val(), 'Assign', 1050);
            });

            //if mask is clicked            
            $("#btnDelete").click(function () {
                var arrID = getJqgridSelectedIDs('#list', 'ID');                
                if (arrID == '') {
                    CRM.msgBox("Please select row(s) to delete!", 350);
                }
                else {
                    CRM.msgConfirmBox('Are you sure you want to delete?', 350, 'removeAssignedQuestion(\'' + arrID + '\')');
                }
            });

            $("#btnBack").click(function () {
                window.location = "/Exam/Index";
            });

            $("#btnExportList").click(function () {
                var numberRow = $("#list").getGridParam("records");
                if (numberRow <= 0) {
                    CRM.msgBox('<%= Resources.Message.E0027 %>', "300");

                } else {
                    var url_send = "/Exam/ExportCandidateListToExcel/" + $('#ExamID').val();
                    window.location = url_send;
                }
            });
            $("#btnExportPin").click(function () {
                var numberRow = $("#list").getGridParam("records");
                if (numberRow <= 0) {
                    CRM.msgBox('<%= Resources.Message.E0027 %>', "300");

                } else {
                    var url_send = "/Exam/ExportCandidatePinToExcel/" + $('#ExamID').val();
                    window.location = url_send;
                }
            });

        });

        function removeAssignedQuestion(arrID) {
            window.location = "/Exam/RemoveSelectedEmployee/?ids=" + arrID + "&examID=" + $('#ExamID').val();
        }
    </script>
    <link rel="stylesheet" type="text/css" href="/Content/Css/tooltip.css" />
    <script src="/Scripts/Tooltip.js" type="text/javascript"></script>
</asp:Content>
