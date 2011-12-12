<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <%= TempData["Message"]%>
    <div id="cactionbutton">
        <button id="btnRefresh" type="button" title="Refresh" class="button refresh">Refresh</button>
        <button id="btnDelete" type="button" title="Delete" class="button delete">
            Delete</button>
        <button type="button" id="addnew" title="Add New" class="button addnew">
            Add New</button>
    </div>
    <div id="cfilter">
        <table>
            <tr>
                <td>
                    <input type="text" id="txtKeyword" maxlength="100" style="width: 150px"
                        value="<%= (string)ViewData[Constants.JOB_TITLE_NAME] %>" onfocus="ShowOnFocus(this,'<%= Constants.JOB_TITLE  %>')"
                        onblur="ShowOnBlur(this,'<%= Constants.JOB_TITLE  %>')" autocomplete="off" />
                </td>
                <td>
                    <%=Html.DropDownList(CommonDataKey.JTL_DEPARTMENT, ViewData[Constants.JOB_TITLE_DEPARTMENT] as SelectList, Constants.FIRST_ITEM_DEPARTMENT, new { @style = "width:180px" })%>
                </td>                
                <td>
                    <button type="button" id="btnFilter" title="Filter" class="button filter">
                        Filter</button>
                </td>
            </tr>
        </table>
    </div>
    <div class="clist">
        <table id="list" class="scroll" style="width: 1024px">
        </table>
        <div id="pager" class="scroll" style="text-align: center;">
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PageTitle" runat="server">
    <%=JobTitlePageInfo.MenuName + CommonPageInfo.AppSepChar + JobTitlePageInfo.JobTitle + CommonPageInfo.AppSepChar  + CommonPageInfo.AppName%>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
    <script type="text/javascript">
        jQuery(document).ready(function () {
            CRM.onEnterKeyword();            
            jQuery("#list").jqGrid({
                url: '/JobTitle/GetListJQGrid/?text=' + encodeURIComponent($("#txtKeyword").val()) + '&departmentId=' + $("#DepartmentId").val(),
                datatype: 'json',
                mtype: 'GET',
                colNames: ['ID', 'Title Name', 'Department', 'Description', 'Is Manager', 'Action'],
                colModel: [
                  { name: 'ID', index: 'ID', align: "center", width: 50, hidden: true },
                  { name: 'TitleName', index: 'TitleName', align: "left", width: 150, sortable: true },
                  { name: 'Department', index: 'Department', align: "left", width: 150, sortable: true },
                  { name: 'Description', index: 'Description', align: "left", width: 200, sortable: true },
                  { name: 'IsManager', index: 'IsManager', align: "center", width: 70, sortable: true },                  
                  { name: 'Action', index: 'Action', editable: false, width: 50, align: 'center', sortable: false}],
                pager: jQuery('#pager'),
                rowList: [20, 30, 50, 100, 200],                
                multiselect: { required: false, width: 15 },
                sortname: '<%= (string)ViewData[Constants.JOB_TITLE_COLUMN]%>',
                sortorder: '<%= (string)ViewData[Constants.JOB_TITLE_ORDER]%>',
                rowNum: '<%= (string)ViewData[Constants.JOB_TITLE_ROW_COUNT]%>',
                page: '<%= (string)ViewData[Constants.JOB_TITLE_PAGE_INDEX]%>',
                viewrecords: true,
                width: 1024, height: "auto",
                imgpath: '/scripts/grid/themes/basic/images',
                loadui: 'block'
            });
            //set width for checkbox column
            jQuery("#jqgh_cb").parent().css("width", "15px");
            jQuery("tr#_empty > td:first").css("width", "15px");

            $('#addnew').click(function () {
                CRM.popup('/JobTitle/Create', 'Add New', 400);
            });

            //if mask is clicked            
            $("#btnDelete").click(function () {
                CRM.deleteList('#list', 'ID', '/JobTitle/DeleteList');
            });

            $("#btnRefresh").click(function () {
                window.location = "/JobTitle/Refresh";
            });

            $("#btnFilter").click(function () {
                var url_send = '/JobTitle/GetListJQGrid/?text=' + encodeURIComponent($("#txtKeyword").val()) + '&departmentId=' + $("#DepartmentId").val();
                $('#list').setGridParam({ page: 1, url: url_send });
                $("#list").trigger('reloadGrid');
            });
        });        
    </script>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ModuleName" runat="server">
    <%= JobTitlePageInfo.ComName%>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="FunctionTitle" runat="server">
    <%= CommonFunc.GetCurrentMenu(Request.RawUrl,true)%>
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="LoginRoles" runat="server">
</asp:Content>