<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="PageTitle" runat="server">
  <%= WorkFlowPageInfo.MenuName + CommonPageInfo.AppSepChar + CommonPageInfo.AppName%>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="FunctionTitle" runat="server">
<%= CommonFunc.GetCurrentMenu(Request.RawUrl,true)%>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="ModuleName" runat="server">
      <%= WorkFlowPageInfo.MenuName%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <%= TempData["Message"]%>
    <div id="cactionbutton" style="width: 930px;">
        <button type="button" id="btnRefresh" title="Refresh" class="button refresh">Refresh</button> <button type="button" id="btnDelete" title="Delete" class="button delete">Delete</button>
         <button type="button" id="btnAddNew" title="Add New" class="button addnew">Add New</button>
    </div>
    <div id="cfilter">
        <table border="0" cellpadding="0" cellspacing="0">
            <tr>
                <td>
                    <input type="text" maxlength="100" style="width: 150px" value="<%= (string)ViewData[Constants.JR_ADMIN_NAME]  %>"
                        id="txtKeyword" onfocus="ShowOnFocus(this,'<%= Constants.USERNAME  %>')" 
                        onblur="ShowOnBlur(this,'<%= Constants.USERNAME  %>')" autocomplete="off"  />
                </td>
                <td>
                    <% Response.Write(Html.DropDownList("Worlflow", ViewData[Constants.JR_ADMIN_WORKFLOW] as SelectList, Constants.FIRST_ITEM_WORKFLOW, new { @style = "width:160px" })); %>
                </td>
                <td>
                    <% Response.Write(Html.DropDownList("Role", ViewData[Constants.JR_ADMIN_GROUP_NAME] as SelectList, Constants.FIRST_ITEM_GROUP_NAME, new { @style = "width:160px" })); %>
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
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
    <script src="/Scripts/jquery.validate.js" type="text/javascript"></script>
    <script type="text/javascript">
        jQuery(document).ready(function () {            
            //bind gridview
            jQuery("#list").jqGrid({
                url: '/JRAdmin/GetListJQGrid/?name=' + encodeURIComponent($('#txtKeyword').val())
                     + '&workflow=' + $('#Worlflow').val() + '&role=' + $('#Role').val(),
                datatype: 'json',
                mtype: 'GET',
                colNames: ["ID", 'User Name', 'Workflow', 'Group Name', 'Group ID', 'Active', 'Created By', 'Updated By', 'Action'],
                colModel: [
                  { name: 'ID', index: 'ID', align: "center", width: 50, hidden: true },
                  { name: 'UserName', index: 'UserName', align: "center", width: 120, sortable: true },
                  { name: 'Workflow', index: 'Workflow', align: "center", width: 120, sortable: true },
                  { name: 'GroupName', index: 'GroupName', align: "center", width: 120, sortable: true },
                  { name: 'GroupID', index: 'GroupID', align: "center", width: 120, sortable: true, hidden: true },
                  { name: 'Active', index: 'Active', align: "center", width: 50, sortable: true },
                  { name: 'CreatedBy', index: 'CreatedBy', align: "center", width: 90, sortable: true },
                  { name: 'UpdatedBy', index: 'UpdatedBy', align: "center", width: 90, sortable: true },
                  { name: 'Action', index: 'Action', editable: false, width: 50, align: 'center', sortable: false}],
                pager: jQuery('#pager'),
                rowList: [20, 30, 50, 100, 200],
                sortname: '<%= (string)ViewData[Constants.JR_ADMIN_COLUMN]%>',
                sortorder: '<%= (string)ViewData[Constants.JR_ADMIN_ORDER]%>',
                rowNum: '<%= (string)ViewData[Constants.JR_ADMIN_ROW_COUNT]%>',
                page: '<%= (string)ViewData[Constants.JR_ADMIN_PAGE_INDEX]%>',
                multiselect: { required: false, width: 15 },
                viewrecords: true,
                width: 1200, height: "auto",
                imgpath: '/scripts/grid/themes/basic/images',
                loadui: 'block'
            });
            //set width for checkbox column
            jQuery("#jqgh_cb").parent().css("width", "20px");
            jQuery("tr#_empty > td:first").css("width", "20px");

            $("#btnDelete").click(function () {
                CRM.deletesMultiOptions('#list', ['ID', 'GroupID'], '/JRAdmin/DeleteList');
            });
            $('#btnAddNew').click(function () {
                CRM.popup('/JRAdmin/Create', 'Add New', 500);
            });
            $('#btnRefresh').click(function () {
                window.location = "/JRAdmin/Refresh";
            });
            CRM.onEnterKeyword();
            //Filter
            $("#btnFilter").click(function () {
                $('#list').setGridParam({ url: '/JRAdmin/GetListJQGrid/?name=' + encodeURIComponent($('#txtKeyword').val())
                     + '&workflow=' + $('#Worlflow').val() + '&role=' + $('#Role').val()
                }).trigger('reloadGrid');
            });
            //Search Autocomplete
            $("#txtKeyword").autocomplete('/Library/GenericHandle/AutoCompleteHandler.ashx/?Page=JRAdmin', { employee: true });

            //Dropdownlist 
            $("#Worlflow").change(function () {
                getListRoleByWorkflow();
            });
            //getListRoleByWorkflow();
            function getListRoleByWorkflow() {
                $("#Role").html("");
                var wfId = $("#Worlflow").val();
                $("#Role").append($("<option value=''>-Select Group Name-</option>"));
                $.getJSON('/Library/GenericHandle/DropdownListHandler.ashx?ID=' + wfId + '&Page=JRAdmin', function (item) {
                    $.each(item, function () {
                        if (this['ID'] != undefined) {
                            $("#Role").append($("<option value='" + this['ID'] + "'>" + this['Name'] + "</option>"));
                        }
                    });
                });
            }

        });
    </script>
</asp:Content>
