<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <%= TempData["Message"]%>
    <div id="cactionbutton">
        <button id="btnExport" type="button" title="Export" class="button export">
            Export</button>
    </div>
    <div id="cfilter">
        <table border="0" cellpadding="0" cellspacing="0">
            <tr>
                <td>
                    <b>Exam Name:</b>
                    <%= ViewData[Constants.EXAM_NAME] %>
                    <input id="hdExamId" type="hidden" value='<%= ViewData[CommonDataKey.EXAM_ID] %>' />
                </td>
            </tr>
            <tr>
                <td>
                    <b>Exam Date:</b>
                    <%= ViewData[Constants.EXAM_DATE] %>
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
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PageTitle" runat="server">
    <%=LOTPageInfo.MenuName + CommonPageInfo.AppSepChar + LOTPageInfo.CandidateTestList + CommonPageInfo.AppSepChar + LOTPageInfo.ComName + CommonPageInfo.AppSepChar + CommonPageInfo.AppName%>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
    <%
        LOT_Exam obj = (LOT_Exam)ViewData.Model;
        CRM.Library.Controls.GridViewControls grid = new CRM.Library.Controls.GridViewControls();
        string urlLink = "/Exam/GetCandidateTestListJQGrid/?examid=" + (string)ViewData[CommonDataKey.EXAM_ID];
        List<LOT_ExamQuestion_Section> list = (List<LOT_ExamQuestion_Section>)ViewData[Constants.EXAM_LIST_QUESTION];
        int beginIndex = 5;
        int countColumn = 5;
        if (obj.ExamType == Constants.LOT_CANDIDATE_EXAM_ID)
        {
            beginIndex = 4;
            countColumn = 4;
        }
        if (list != null)
        {
            countColumn += list.Count + 3;
        }
        string[] colNames = new string[countColumn];
        string[] colModels = new string[countColumn];
        if (obj.ExamType == Constants.LOT_CANDIDATE_EXAM_ID)
        {

            colNames[0] = "No";
            colNames[1] = "Candidate Name";
            colNames[2] = "Email";
            colNames[3] = "Writing Mark";
            colModels[0] = "{name: 'No', index: 'No', align: 'center', width: 20}";
            colModels[1] = "{name: 'CandidateName', index: 'CandidateName', align: 'left', width: 140}";
            colModels[2] = "{name: 'Email', index: 'Email', align: 'left', width: 150}";
            colModels[3] = "{name: 'Writing', index: 'Writing', left: 'center', width: 60, sortable: false}";
            grid.LoadComplete = "CRM.setupFreezeColumn(['No','CandidateName','Email'])";
        }
        else
        {
            colNames[0] = "No";
            colNames[1] = "ID";
            colNames[2] = "Candidate Name";
            colNames[3] = "Email";
            colNames[4] = "Writing Mark";
            colModels[0] = "{name: 'No', index: 'No', align: 'center', width: 20}";
            colModels[1] = "{name: 'ID', index: 'ID', align: 'center', width: 50}";
            colModels[2] = "{name: 'CandidateName', index: 'CandidateName', align: 'left', width: 140}";
            colModels[3] = "{name: 'Email', index: 'Email', align: 'left', width: 150}";
            colModels[4] = "{name: 'Writing', index: 'Writing', align: 'center', width: 90, sortable: false}";
            grid.LoadComplete = "CRM.setupFreezeColumn(['No','ID','CandidateName','Email']);CRM.setGridWith(1200);";
        }
        grid.OnSelectRow = "CRM.removeSelectRow(id);";
        grid.Width = 1200;
        grid.Height = "auto";
        grid.RowNum = 20;
        grid.RowList = "[20,50,100,200,300]";

        for (int i = 0; i < list.Count; i++)
        {
            colNames[i + beginIndex] = HttpUtility.HtmlEncode(list[i].LOT_Section.SectionName);
        }
        for (int i = 0; i < list.Count; i++)
        {
            colModels[i + beginIndex] = "{name: '" + list[i].LOT_Section.SectionName + "', index: '" + list[i].LOT_Section.SectionName + "', align: 'center', width: 100,sortable:false}";
        }
        colNames[countColumn - 3] = "Send Mail";
        colNames[countColumn - 2] = "Total Mark";
        colNames[countColumn - 1] = "Action";
        colModels[countColumn - 3] = "{name: 'SendMail', index: 'SendMail', align: 'center', width: 60}";
        colModels[countColumn - 2] = "{name: 'Mark', index: 'Mark', align: 'center', width: 60}";
        colModels[countColumn - 1] = "{ name: 'Action', index: 'Action', editable: false, width: 100, align: 'center', sortable: false}";

        grid.SortOrder = CommonFunc.SetSortOrder(SortDirection.Ascending);
        grid.SortName = "Candidate";
        grid.Page = 1;
        grid.RowNum = 20;
        grid.MultiSelect = false;
        grid.HoverRows = false;
        Response.Write(grid.ShowGrid(urlLink, colNames, colModels));
                
    %>
    <script type="text/javascript">
        jQuery(document).ready(function () {
            $("#btnExport").click(function () {
                CRM.loading();
                var numberRow = $("#list").getGridParam("records");
                if (numberRow <= 0) {
                    CRM.msgBox('<%= Resources.Message.E0027 %>', "300");
                }
                else {
                    window.location = "/Exam/ExportCandidateToExcel/" + $("#hdExamId").val();
                }
                CRM.completed();
            });
        });
    </script>
    <%//LOT_Exam obj = (LOT_Exam)ViewData.Model;
        //string isCandidate = "true";
        //if (obj.ExamType == Constants.LOT_CANDIDATE_EXAM_ID)
        //{
        //    isCandidate = "false";
        //}
    %>
    <%--   <script type="text/javascript">
        jQuery(document).ready(function () {
            CRM.onEnterKeyword();            
            jQuery("#list").jqGrid({
                url: '/Exam/GetCandidateTestListJQGrid/?examid=' + $("#hdExamId").val(),
                datatype: 'json',
                mtype: 'GET',
                colNames: ['No','ID','Candidate Name', 'Email', 'Mark', 'Writing Mark','Technical Skill','Mail','Action'],
                colModel: [
                  { name: 'No', index: 'No', align: "center", width: 20, hidden: false, sortable: false },
                  { name: 'ID', index: 'ID', align: "center", width: 20, hidden: <%=isCandidate %>, sortable: false },
                  { name: 'CandidateName', index: 'CandidateName', align: "left", width: 150, sortable: true },
                  { name: 'Email', index: 'Email', align: "left", width: 150, sortable: true },
                  { name: 'Mark', index: 'Mark', align: "center", width: 50, sortable: true },
                  { name: 'WritingMark', index: 'WritingMark', align: "center", width: 50, sortable: true },
                  { name: 'ProgrammingMark', index: 'ProgrammingMark', align: "center", width: 68, sortable: true },
                  { name: 'SendMail', index: 'SendMail', align: "center", width: 50, sortable: true },
                  { name: 'Action', index: 'Action', editable: false, width: 50, align: 'center', sortable: false}],
                pager: jQuery('#pager'),
                rowList: [20, 30, 50, 100, 200],                
                rowNum: 20,                
                sortname: 'CandiateName',
                sortorder: "desc",                
                viewrecords: true,
                width: 1200, height: "auto",
                imgpath: '/scripts/grid/themes/basic/images',
                loadui: 'block'
            });            
        });        
    </script>--%>
    <style>
        .ui-jqgrid tr.jqgrow td
        {
            white-space: normal;
        }
    </style>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ModuleName" runat="server">
    <% Response.Write(LOTPageInfo.ComName);%>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="FunctionTitle" runat="server">
    <%--<%= LOTPageInfo.MenuName + CommonPageInfo.AppDetailSepChar +
            "<a href='/Exam'>" + LOTPageInfo.Exam + "</a>" + CommonPageInfo.AppDetailSepChar +
            ViewData[Constants.EXAM_NAME] + CommonPageInfo.AppDetailSepChar + LOTPageInfo.CandidateTestList
    %>--%>
    <%= CommonFunc.GetCurrentMenu(Request.RawUrl) +
            ViewData[Constants.EXAM_NAME] + CommonPageInfo.AppDetailSepChar + LOTPageInfo.CandidateTestList
    %>
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="LoginRoles" runat="server">
</asp:Content>
