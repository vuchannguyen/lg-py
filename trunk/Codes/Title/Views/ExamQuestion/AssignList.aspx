<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <%= TempData["Message"]%>
    <div>        
        Section: <%=Html.DropDownList(CommonDataKey.SECTION_LIST, null, new { @style = "width:180px" })%>
        <%=Html.Hidden(CommonDataKey.EXAM_QUESTION_ID, ViewData[CommonDataKey.EXAM_QUESTION_ID] as SelectList)%>
    </div>
    <div id="cactionbutton">
        <button id="btnDelete" type="button" title="Delete" class="button delete">
            Delete</button>
        <button type="button" id="addnew" title="Add New" class="button addnew">
            Add New</button>
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
    <%=LOTPageInfo.MenuName + CommonPageInfo.AppSepChar + LOTPageInfo.AssignQuestion + CommonPageInfo.AppSepChar + LOTPageInfo.ComName + CommonPageInfo.AppSepChar + CommonPageInfo.AppName%>    
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
    <script src="/Scripts/jquery.validate.js" type="text/javascript"></script>
    <script type="text/javascript">
        jQuery(document).ready(function () {
            //Dropdownlist 
            $("#SectionList").change(function () {
                var urls = '/ExamQuestion/GetAssignListJQGrid/?examQuestionSectionId=' + $('#SectionList').val();
                $('#list').setGridParam({ url: urls
                }).trigger('reloadGrid');
            });

            CRM.onEnterKeyword();
            jQuery("#list").jqGrid({
                url: '/ExamQuestion/GetAssignListJQGrid/?examQuestionSectionId=' + $('#SectionList').val(),
                datatype: 'json',
                mtype: 'GET',
                colNames: ['ID', 'Content', 'Action'],
                colModel: [
                  { name: 'ID', index: 'ID', align: "center", width: 50, hidden: true },
                  { name: 'Content', index: 'Title', align: "left", width: 300, sortable: false },
                  { name: 'Action', index: 'Action', editable: false, width: 50, align: 'center', sortable: false}],
                pager: jQuery('#pager'),
                rowList: [20, 30, 50, 100, 200],
                sortname: 'ID',
                sortorder: "asc",
                multiselect: { required: false, width: 15 },
                viewrecords: true,
                width: 1024, height: "auto",
                imgpath: '/scripts/grid/themes/basic/images',
                loadui: 'block'
            });
            //set width for checkbox column
            jQuery("#jqgh_cb").parent().css("width", "15px");
            jQuery("tr#_empty > td:first").css("width", "15px");

            $('#addnew').click(function () {                
                CRM.popup('/ExamQuestion/Assign/' + $('#SectionList').val(), 'Add New', 840);
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
                window.location = "/ExamQuestion/Index";
            });
        });

        function removeAssignedQuestion(arrID) {
            window.location = "/ExamQuestion/RemoveAssignList/?ids=" + arrID + "&examQuestionID=" + $('#ExamQuestionID').val() + "&examQuestionSectionId=" + $('#SectionList').val();                                   
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
<%--<%= LOTPageInfo.MenuName + CommonPageInfo.AppDetailSepChar + 
    "<a href='/ExamQuestion/Index'>"+ LOTPageInfo.ExamQuestion+"</a>"+CommonPageInfo.AppDetailSepChar+
        "<a href='/ExamQuestion/Details/" + ViewData[CommonDataKey.EXAM_QUESTION_ID] + "'>" + ViewData[CommonDataKey.EXAM_QUESTION_TITLE] + "</a>" + CommonPageInfo.AppDetailSepChar + LOTPageInfo.AssignQuestion
     %>--%>
     <%= CommonFunc.GetCurrentMenu(Request.RawUrl) +
        "<a href='/ExamQuestion/Details/" + ViewData[CommonDataKey.EXAM_QUESTION_ID] + "'>" + ViewData[CommonDataKey.EXAM_QUESTION_TITLE] + "</a>" + CommonPageInfo.AppDetailSepChar + LOTPageInfo.AssignQuestion
     %>
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="LoginRoles" runat="server">
</asp:Content>
