<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content2" ContentPlaceHolderID="PageTitle" runat="server">
    <%= LOTPageInfo.MenuName + CommonPageInfo.AppSepChar + LOTPageInfo.Question + CommonPageInfo.AppSepChar
                           + LOTPageInfo.ComName + CommonPageInfo.AppSepChar + CommonPageInfo.AppName
    %>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ModuleName" runat="server">
    <% Response.Write(LOTPageInfo.ComName); %>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="FunctionTitle" runat="server">
    <%=CommonFunc.GetCurrentMenu(Request.RawUrl,true)%>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <%= TempData[CommonDataKey.TEMP_SYSTEM_MESSAGE]%>
    <div id="cactionbutton">
        <button id="btnRefresh" type="button" title="Refresh" class="button refresh">
            Refresh</button>
        <button type="button" id="btnDelete" title="Delete" class="button delete">
            Delete</button>
        <button type="button" id="addnew" title="Add New" class="button addnew">
            Add New</button>
    </div>
    <div id="cfilter">
        <table border="0" cellpadding="0" cellspacing="0">
            <tr>
                <td>
                    <input type="text" maxlength="<%=Constants.TEXTBOX_KEYWORD_MAX_LENGTH %>" class="keyword"
                        value="<%= (string)ViewData[Constants.QUESTION_TEXT]%>" id="txtKeyword" onfocus="ShowOnFocus(this,'<%=Constants.QUESTIONNAME  %>')"
                        onblur="ShowOnBlur(this,'<%=Constants.QUESTIONNAME  %>')" autocomplete="off" />
                </td>
                <td>
                    <%= Html.DropDownList("SectionName", ViewData[Constants.QUESTION_SECTION] as SelectList, Constants.FIRST_ITEM_SECTION)%>
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
    <div id="divEdit" style="display: none; cursor: default;">
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
        .ui-jqgrid tr.jqgrow td
        {
            white-space: normal;
        }
    </style>
    <script src="/Scripts/jquery.validate.js" type="text/javascript"></script>
    <script type="text/javascript" src="/Scripts/jquery.jplayer.min.js"></script>
    <script type="text/javascript" src="/Scripts/jquery.sound.js"></script>
    <script type="text/javascript">

        jQuery(document).ready(function () {
            CRM.onEnterKeyword();
            jQuery("#list").jqGrid({
                url: '/Question/GetListJQGrid/?name=' + encodeURIComponent($('#txtKeyword').val()) + '&sectionID=' + $('#SectionName').val(),
                datatype: 'json',
                scroll: 0,
                mtype: 'GET',
                colNames: ['Question ID', 'Question', 'Section Name', 'Action'],
                colModel: [
                  { name: 'QuestionID', index: 'ID', align: "center", width: 50, hidden: true },
                  { name: 'Question', index: 'QuestionContent', align: "justify", width: 550, sortable: true },
                  { name: 'SectionName', index: 'SectionName', align: "center", width: 120, sortable: true },
                  { name: 'Action', index: 'Action', editable: false, width: 100, align: 'center', sortable: false}],
                pager: jQuery('#pager'),
                rowList: [20, 30, 50, 100, 200],
                sortname: '<%= (string)ViewData[Constants.QUESTION_COLUMN]%>',
                sortorder: '<%= (string)ViewData[Constants.QUESTION_ORDER]%>',
                rowNum: '<%= (string)ViewData[Constants.QUESTION_ROW_COUNT]%>',
                page: '<%= (string)ViewData[Constants.QUESTION_PAGE_INDEX]%>',                
                multiselect: { required: false, width: 24 },
                viewrecords: true,
                width: 1024, height: "auto",
                imgpath: '/scripts/grid/themes/basic/images',
                loadui: 'block'
            });
            $('#addnew').click(function () {
                window.location = "/Question/Create";
            });
            $("#btnDelete").click(function () {
                CRM.deleteList('#list', 'QuestionID', '/Question/DeleteList');
            });
            $("#btnRefresh").click(function () {
                window.location = "/Question/Refresh";
            });
            $("#btnFilter").click(function () {
                var name = encodeURIComponent($('#txtKeyword').val());
                if (name == encodeURIComponent('<%= Constants.QUESTIONNAME  %>')) {
                    name = "";
                }
                var targetUrl = '/Question/GetListJQGrid?name=' + name
                    + '&sectionID=' + $('#SectionName').val();
                $('#list').setGridParam({ page: 1, url: targetUrl }).trigger('reloadGrid');
            });
        });        
    </script>
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="LoginRoles" runat="server">
</asp:Content>
