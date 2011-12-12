<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <%= TempData["Message"]%>
    <div id="cactionbutton">   
        <button id="btnRefresh" type="button" title="Refresh" class="button refresh">Refresh</button> 
        <button id="btnDelete" type="button" title="Delete" class="button delete">Delete</button>      
        <button type="button" id="btnAddNew" title="Add New" class="button addnew">
            Submit New</button>        
    </div>
    <div id="cfilter">
        <table>
            <tr>
                <td>
                    <input type="text" maxlength="50" style="width: 150px" value="<%= (string)ViewData[Constants.SR_LIST_ADMIN_TITLE] %>"
                        id="txtKeyword" onfocus="ShowOnFocus(this,'<%= Constants.SR_FIRST_KEY_WORD%>')"
                        onblur="ShowOnBlur(this,'<%= Constants.SR_FIRST_KEY_WORD  %>')" autocomplete="off" />
                </td>
                <td>
                    <%=Html.DropDownList(CommonDataKey.SR_ADMIN_CATEGORY, ViewData[Constants.SR_ADMIN_CATEGORY_LIST] as SelectList,
                                                                    Constants.SR_FIRST_CATEGORY, new { @style = "width:150px" })%>
                    <%=Html.DropDownList(CommonDataKey.SR_ADMIN_SUB_CATEGORY, ViewData[Constants.SR_ADMIN_SUBCATEGORY_LIST] as SelectList,
                                                                        Constants.SR_FIRST_SUBCATEGORY, new { @style = "width:150px" })%>
                    <%=Html.DropDownList(CommonDataKey.SR_ADMIN_STATUS, ViewData[Constants.SR_ADMIN_STATUS_LIST] as SelectList,
                                                                        Constants.SR_FIRST_STATUS, new { @style = "width:150px" })%>
                    <%=Html.DropDownList(CommonDataKey.SR_ADMIN_REQUESTOR, ViewData[Constants.SR_ADMIN_REQUESTOR_LIST] as SelectList,
                                                                        Constants.SR_FIRST_REQUESTOR, new { @style = "width:150px" })%>
                    <%=Html.DropDownList(CommonDataKey.SR_ADMIN_ASSIGN_TO, ViewData[Constants.SR_ADMIN_ASSIGNTO_LIST] as SelectList,
                                                                        Constants.SR_FIRST_ASSIGNTO, new { @style = "width:150px" })%>
                    
                </td>
                <td>
                    <button type="button" class="icon plus" id="expand" title="Search more..."></button>
                </td>
                <td>
                    <button type="button" id="btnFilter" title="Filter" style="float: left" class="button filter">
                        Filter</button>
                </td>
            </tr>
        </table>
        <table id="expandFilter" style="display: none">
            <tr height="35">
                <td>
                    Request Date :
                </td>
                <td>
                    From
                </td>
                <td>
                    <% Response.Write(Html.TextBox("Fromdate", (string)ViewData[Constants.STT_LIST_STARTDATE_FROM], new { @style = "width:80px" }));%>
                </td>
                <td>
                    To
                </td>
                <td>
                    <% Response.Write(Html.TextBox("Todate", (string)ViewData[Constants.STT_LIST_STARTDATE_TO], new { @style = "width:80px" })); %>
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
 </asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PageTitle" runat="server">
    <%= ServiceRequestPageInfo.ComName + CommonPageInfo.AppSepChar + ServiceRequestPageInfo.FuncNameSRList + 
        CommonPageInfo.AppSepChar + CommonPageInfo.AppName%>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
<script type="text/javascript">

    function getListTargetUrl() {
        var url = '/ServiceRequestAdmin/GetListJQGrid/?' +
                'name=' + encodeURIComponent($("#txtKeyword").val()) +
                '&status=' + $('#<%=CommonDataKey.SR_ADMIN_STATUS%>').val() +
                '&category=' + $('#<%=CommonDataKey.SR_ADMIN_CATEGORY%>').val() +
                '&subcate=' + $('#<%=CommonDataKey.SR_ADMIN_SUB_CATEGORY%>').val() +
                '&assignto=' + $("#<%=CommonDataKey.SR_ADMIN_ASSIGN_TO%>").val() +
                '&startDate=' + $('#Fromdate').val() +
                '&endDate=' + $('#Todate').val() +
                '&requestor=' + $('#<%=CommonDataKey.SR_ADMIN_REQUESTOR%>').val();
        return url;
    }

    $(document).ready(function () {

        CRM.onEnterKeyword();

        if ($("#Fromdate").val() != "" || $("#Todate").val() != "") {
            $('#expandFilter').css("display", "");
            $("#expand").attr("class", "icon minus");
        }

        $("#btnAddNew").click(function () {
            window.location = "/ServiceRequestAdmin/Create";
        });

        $("#btnRefresh").click(function () {
            window.location = "/ServiceRequestAdmin/Refresh";
        });

        $("#Fromdate").datepicker();
        $("#Todate").datepicker();

        $("#expand").click(function () {
            var styl = $("#expand").attr("class");
            if (styl == "icon minus") {
                $('#expandFilter').css("display", "none");
                $("#expand").attr("class", "icon plus");
                $("#Fromdate").attr("value", "");
                $("#Todate").attr("value", "");
            }
            else {
                $('#expandFilter').css("display", "");
                $("#expand").attr("class", "icon minus");
            }
        });

        jQuery("#list").jqGrid({
            url: getListTargetUrl(),
            datatype: 'json',
            mtype: 'GET',
            colNames: ['', 'Alert', 'ID', 'Title', 'Category', 'Sub Cat', 'Status', 'Requestor', 'Assign to', 'Request Date', 'Action'],
            colModel: [
                  { name: 'ID', index: 'ID', align: "center", width: 70, hidden: true },
                  { name: 'Icon', index: 'Urgency', align: "center", width: 40, sortable: false },
                  { name: 'Code', index: 'Code', align: "center", width: 70, sortable: true },
                  { name: 'Title', index: 'Title', align: "left", width: 160, sortable: true },
                  { name: 'Category', index: 'Category', align: "left", width: 140, sortable: true },
                  { name: 'SubCategory', index: 'SubCategory', align: "left", width: 140, sortable: true },
                  { name: 'Status', index: 'Status', align: "center", width: 100, sortable: true },
                  { name: 'Requestor', index: 'Requestor', align: "center", width: 110, sortable: true },
                  { name: 'AssignName', index: 'AssignName', align: "center", width: 110, sortable: true },
                  { name: 'RequestDate', index: 'RequestDate', align: "center", width: 100, sortable: true },
                  { name: 'Action', index: 'Action', editable: false, width: 80, align: 'center', sortable: false}],
            pager: jQuery('#pager'),
            rowList: [20, 30, 50, 100, 200],
            viewrecords: true,
            width: 1024, height: "auto",
            multiselect: true,
            grouping: false,
            sortname: '<%= (string)ViewData[Constants.SR_LIST_ADMIN_COLUMN]%>',
            sortorder: '<%= (string)ViewData[Constants.SR_LIST_ADMIN_ORDER]%>',
            rowNum: '<%= (string)ViewData[Constants.SR_LIST_ADMIN_ROW_COUNT]%>',
            page: '<%= (string)ViewData[Constants.SR_LIST_ADMIN_PAGE_INDEX]%>',
            imgpath: '/scripts/grid/themes/basic/images',
            loadui: 'block',
            loadComplete: function () {
                ShowTooltip($("a[class=showTooltip]"), $("#shareit-box"), "/ServiceRequestAdmin/ShowTitleTooltip");
            }
        });

        $("#btnFilter").click(function () {

            var isValid = true;
            if ($('#Fromdate').val() != '') {
                if (!isDate($('#Fromdate').val())) {
                    alert("Request Date From is invalid.");
                    $('#Fromdate').focus();
                    isValid = false;
                }
            }
            if ($('#Todate').val() != '') {
                if (!isDate($('#Todate').val())) {
                    alert("Request Date To is invalid.");
                    $('#Todate').focus();
                    isValid = false;
                }
            }
            if (isValid) {
                var url_send = getListTargetUrl();

                $('#list').setGridParam({ url: url_send });
                $("#list").trigger('reloadGrid');
            }
        });

        $("#<%=CommonDataKey.SR_LIST_LOGIN_ROLE %>").change(function () {
            var value = $("#<%=CommonDataKey.SR_LIST_LOGIN_ROLE%>").val();

            $("#btnFilter").click();

        });

        $("#btnDelete").click(function () {
            CRM.deleteList('#list', 'ID', '/ServiceRequestAdmin/DeleteList');
        });

        $('#<%=CommonDataKey.SR_ADMIN_CATEGORY%>').change(function () {
            $("#<%=CommonDataKey.SR_ADMIN_SUB_CATEGORY%>").html("");
            var id = $("#<%=CommonDataKey.SR_ADMIN_CATEGORY%>").val();
            $("#<%=CommonDataKey.SR_ADMIN_SUB_CATEGORY%>").append($("<option value=''><%= Constants.SR_FIRST_SUBCATEGORY%></option>"));
            if (id != "") {
                $.getJSON('/Library/GenericHandle/DropdownListHandler.ashx?ID=' + id + '&Page=Category', function (item) {
                    if (item != null) {
                        $.each(item, function () {
                            $("#<%=CommonDataKey.SR_ADMIN_SUB_CATEGORY%>").append($("<option value='" + this['ID'] + "'>" + this['Name'] + "</option>"));
                        });
                    }
                });
            }
            else {
                $.getJSON('/Library/GenericHandle/DropdownListHandler.ashx?ID=-1' + '&Page=Category', function (item) {
                    if (item != null) {
                        $.each(item, function () {
                            $("#<%=CommonDataKey.SR_ADMIN_SUB_CATEGORY%>").append($("<option value='" + this['ID'] + "'>" + this['Name'] + "</option>"));
                        });
                    }
                });
            }

        });

    });

    </script>
    <link rel="stylesheet" type="text/css" href="/Content/Css/tooltip.css" />
    <script src="/Scripts/Tooltip.js" type="text/javascript"></script>
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="ModuleName" runat="server">
<%= ServiceRequestPageInfo.ComName%>
</asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="FunctionTitle" runat="server">
<%=CommonFunc.GetCurrentMenu(Request.RawUrl).Trim().TrimEnd('»')%>
</asp:Content>

<asp:Content ID="Content6" ContentPlaceHolderID="LoginRoles" runat="server">
</asp:Content>
