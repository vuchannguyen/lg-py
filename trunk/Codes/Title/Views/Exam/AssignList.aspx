<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
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
        <button type="button" id="addnew" title="Add New" class="button addnew">
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
    <div id="divEdit" style="display: none; cursor: default;">
    </div>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PageTitle" runat="server">
    <%=LOTPageInfo.MenuName + CommonPageInfo.AppSepChar+ LOTPageInfo.AssignCandidate + CommonPageInfo.AppSepChar + LOTPageInfo.ComName + CommonPageInfo.AppSepChar + CommonPageInfo.AppName%>    
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
    <script src="/Scripts/jquery.validate.js" type="text/javascript"></script>
    <script type="text/javascript" src="/Scripts/jquery.jplayer.min.js"></script>
    <script type="text/javascript" src="/Scripts/jquery.sound.js"></script>
    <script type="text/javascript">
        jQuery(document).ready(function () {
            CRM.onEnterKeyword();
            jQuery("#list").jqGrid({
                url: '/Exam/GetAssignListJQGrid?examID=' + $('#ExamID').val(),
                datatype: 'json',
                mtype: 'POST',
                colNames: ['No', 'ID', 'Candidate Name', 'DOB', 'Telephone', 'Gender', 'Searched date', 'Source', 'Position', 'Status', 'Remarks', 'Pin'],
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
                  { name: 'Status', index: 'Status', editable: false, width: 50, align: 'center', sortable: true },
                  { name: 'Note', index: 'Note', align: "left", width: 100, sortable: true },
                  { name: 'CandidatePin', index: 'CandidatePin', align: "center", width: 100, sortable: true}],
                pager: jQuery('#pager'),
                rowList: [20, 30, 50, 100, 200],
                sortname: 'ID',
                sortorder: "asc",
                multiselect: true,
                viewrecords: true,
                width: 1200, height: 'auto',
                imgpath: '/scripts/grid/themes/basic/images',
                loadui: 'block'
            });
            //set width for checkbox column
            jQuery("#jqgh_cb").parent().css("width", "15px");
            jQuery("tr#_empty > td:first").css("width", "15px");

            $('#addnew').click(function () {
                CRM.popup('/Exam/Assign/' + $('#ExamID').val(), 'Assign', 1050);
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
            window.location = "/Exam/RemoveAssignList/?ids=" + arrID + "&examID=" + $('#ExamID').val();
        } 

    </script>
    <style>
        .ui-jqgrid tr.jqgrow td
        {
            white-space: normal;
        }
    </style>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ModuleName" runat="server">
    <%= LOTPageInfo.ComName%>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="FunctionTitle" runat="server">
<%=CommonFunc.GetCurrentMenu(Request.RawUrl) + CommonPageInfo.AppDetailSepChar + ViewData[CommonDataKey.EXAM_TITLE].ToString() 
    + CommonPageInfo.AppDetailSepChar+LOTPageInfo.AssignCandidate%>
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="LoginRoles" runat="server">
</asp:Content>

