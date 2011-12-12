<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <%=Html.TextBox("txtFixedField", null, new { @style="display:none"})%>
    <%= TempData[CommonDataKey.TEMP_SYSTEM_MESSAGE]%>
    <div id="cactionbutton">
        <button id="btnRefresh" type="button" title="Refresh" class="button refresh">Refresh</button>
        <button type="button" id="btnDelete" title="Delete" class="button delete">Delete</button>
        <button type="button" id="btnAddNew" title="Add New" class="button addnew">Add New</button>
    </div>
    <div id="cfilter">
        <table border="0" cellpadding="0" cellspacing="0">
            <tr>
                <td>
                    <input type="text" class="keyword" value="<%= (string)ViewData[CommonDataKey.TRAINING_EEI_SEARCH_TEXT] %>"
                        id="txtKeyword"  onfocus="ShowOnFocus(this,'<%=Constants.TRAINING_EEI_TXT_KEYWORD_LABEL%>')" 
                        onblur="ShowOnBlur(this,'<%=Constants.TRAINING_EEI_TXT_KEYWORD_LABEL  %>')" style="width:200px" autocomplete="off" />
                </td>
                <td>
                    <%= Html.DropDownList(CommonDataKey.TRAINING_EEI_SKILL_TYPE_LIST, null, Constants.TRAINING_EEI_LIST_LABEL,
                        new { @style="width:150px" })%>
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
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="PageTitle" runat="server">
<%=TrainingCenterPageInfo.FuncNameEEI + CommonPageInfo.AppSepChar + TrainingCenterPageInfo.ComName +  
    CommonPageInfo.AppSepChar + CommonPageInfo.AppName%>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
<script type="text/javascript">
    function getFilterParams() {
        var url = '<%=Url.Action("GetListJQGrid")%>' +
            '?name=' + encodeURIComponent($("#txtKeyword").val()) +
            '&type=' + $('#<%=CommonDataKey.TRAINING_EEI_SKILL_TYPE_LIST%>').val();
        return url;
    }
    jQuery(document).ready(function () {
        CRM.onEnterKeyword();
        jQuery("#list").jqGrid({
            url: getFilterParams(),
            datatype: 'json',
            scroll: 0,
            mtype: 'GET',
            colNames: ['ID', 'ID', 'Name', 'Type', 'Score', 'Expire Date', 'Notes','Action'],
            colModel: [
                  { name: 'ID', index: 'ID', hidden: true },
                  { name: 'EmpID', index: 'EmpID', align: "left", width: 30, sortable: true },
                  { name: 'Name', index: 'Name', align: "left", width: 100, sortable: true },
                  { name: 'Type', index: 'Type', align: "center", width: 50, sortable: true },
                  { name: 'Score', index: 'Score', align: "center", width: 20, sortable: true },
                  { name: 'ExpireDate', index: 'ExpireDate', align: "center", width: 50, sortable: true },
                  { name: 'Notes', index: 'Notes', align: "justify", width: 100, sortable: false },
                  { name: 'Action', index: 'Action', editable: false, width: 20, align: 'center', sortable: false}],
            pager: jQuery('#pager'),
            rowList: [20, 30, 50, 100, 200],
            sortname: '<%= (string)ViewData[CommonDataKey.TRAINING_EEI_SEARCH_SORT_COLUMN]%>',
            sortorder: '<%= (string)ViewData[CommonDataKey.TRAINING_EEI_SEARCH_SORT_ORDER]%>',
            rowNum: '<%= (string)ViewData[CommonDataKey.TRAINING_EEI_SEARCH_ROW_COUNT]%>',
            page: '<%= (string)ViewData[CommonDataKey.TRAINING_EEI_SEARCH_PAGE_INDEX]%>',
            multiselect: { required: false, width: 24 },
            viewrecords: true,
            width: 1024, height: "auto",
            imgpath: '/scripts/grid/themes/basic/images',
            loadui: 'block'
        });
        $('#btnAddNew').click(function () {
            CRM.popup('<%=Url.Action("Create")%>', "Add New Employee English Information", 550);

        });
        $("#btnDelete").click(function () {
            CRM.deletes('#list', 'ID', '<%=Url.Action("Delete") %>');
        });
        $("#btnRefresh").click(function () {
            window.location = '<%=Url.Action("Refresh")%>';
        });
        $("#btnFilter").click(function () {
            var targetUrl = getFilterParams();
            $('#list').setGridParam({ page: 1, url: targetUrl }).trigger('reloadGrid');
        });
    });        
</script>
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="ModuleName" runat="server">
<%=TrainingCenterPageInfo.ComName%>
</asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="FunctionTitle" runat="server">
<%=CommonFunc.GetCurrentMenu(Url.Action("Index", "TrainingCenterAdmin"), false) + TrainingCenterPageInfo.FuncEmpEnglishInfo%>
</asp:Content>

<asp:Content ID="Content6" ContentPlaceHolderID="LoginRoles" runat="server">
</asp:Content>
