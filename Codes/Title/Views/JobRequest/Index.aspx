<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="PageTitle" runat="server">
    <%= JobRequestPageInfo.ModJobRequest + CommonPageInfo.AppSepChar + CommonPageInfo.AppName%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="HeadContent" runat="server">
    <script src="/Scripts/Grid/js/grid.grouping.js" type="text/javascript"></script>
    <link rel="stylesheet" type="text/css" href="/Content/Css/tooltip.css" />
    <link rel="stylesheet" type="text/css" href="/Content/Css/tooltip.css" />
    <script src="/Scripts/Tooltip.js" type="text/javascript"></script>
    <script src="/Scripts/Grid/js/grid.subgrid.js" type="text/javascript"></script>
    <script type="text/javascript">
        jQuery(document).ready(function () {                                    
            $("#txtKeyword").autocomplete('/Library/GenericHandle/AutoCompleteHandler.ashx/?Page=JRIndex&Role=' + $("#Role :selected").val());
            jQuery.validator.addMethod("checkCC", function (value, element) {
                var isvalid = true;
                var word = value.split(';');
                var keyName = "";
                for (var i in word) {
                    if (i < word.length - 1) {
                        if (keyName.indexOf(word[i]) < 0) {
                            keyName += word[i] + ',';
                        }
                        else {
                            jQuery.validator.messages.checkCC = '<%=string.Format(Resources.Message.E0020,  "' + word[i] + '" , "CC List.")%>'; ;
                            isvalid = false;
                            break;
                        }
                    }
                }
                return isvalid;
            }, jQuery.validator.messages.checkCC);
            //Dropdownlist 
            $("#Role").change(function () {
                window.location = "/JobRequest/ChangeRole/?RoleId=" + $('#Role').val();
            });
            CRM.onEnterKeyword();
            <% if( Constants.HR_ID == (int)ViewData[Constants.JOB_REQUEST_ROLE]){ %>
                jQuery("#list").jqGrid({
                    url: '/JobRequest/GetListJQGrid/?text=' + encodeURIComponent($('#txtKeyword').val())
                         + '&department=' + $('#Department').val() + '&subdepartment=' + $('#SubDepartment').val() + '&positionId=' + $('#positionId').val()
                         + '&requestorId=' + $('#requestorId').val() + '&statusId=' + $('#statusId').val() + '&request=' + $('#RequestType').val(),
                    datatype: 'json',
                    mtype: 'GET',
                    colNames: ["ID#", "ID", 'Request Date', 'Position' ,'Quantity' , 'Sub Department', 'Expected Start Date', 'Resolution', 'Status',"Request", 'Requestor', 'Forwarded to', 'Action'],
                    colModel: [
                        { name: 'RealID', index: 'RealID', align: "center", width: 10, hidden: true },
                        { name: 'ID', index: 'ID', align: "center", width: 40, sortable: true },
                        { name: 'RequestDate', index: 'RequestDate', align: "center", width: 60, sortable: true },
                        { name: 'JobTitleName', index: 'JobTitleName', align: "left", width: 80, sortable: true },
                        { name: 'Quantity', index: 'Quantity', align: "center", width: 40, sortable: true },
                        { name: 'DepartmentName', index: 'DepartmentName', align: "left", width: 60, sortable: true },
                        { name: 'ExpectedStartDate', index: 'ExpectedStartDate', align: "center", width: 80, sortable: true },
                        { name: 'ResolutionName', index: 'ResolutionName', align: "center", width: 90, sortable: true },
                        { name: 'StatusName', index: 'StatusName', align: "center", width: 40, sortable: true },
                        { name: 'Request', index: 'Request', align: "center", width: 45, sortable: true },
                        { name: 'RequestorName', index: 'RequestorName', align: "center", width: 60, sortable: true },
                        { name: 'AssignName', index: 'AssignName', align: "center", width: 100, sortable: true },
                        { name: 'Action', index: 'Action', editable: false, width: 30, align: 'center', sortable: false}],
                    pager: '#pager',
                    sortname: '<%= (string)ViewData[Constants.JOB_REQUEST_COLUMN]%>',
                    sortorder: '<%= (string)ViewData[Constants.JOB_REQUEST_ORDER]%>',
                    rowNum: '<%= (string)ViewData[Constants.JOB_REQUEST_ROW_COUNT]%>',
                    page: '<%= (string)ViewData[Constants.JOB_REQUEST_PAGE_INDEX]%>',
                    rowList: [3,20, 30, 50, 100, 200],
                    multiselect: false,
                    viewrecords: true,
                    width: 1200, height: "auto",
                    imgpath: '/scripts/grid/themes/basic/images',
                    loadui: 'block',
                    subGrid : true,
                    subGridUrl: '/JobRequest/GetSubList/', 
                    subGridModel: [
                        {
                            name : ['#','JR','Candidate','Emp ID','Job Title','Actual Start Date', 'Status', 'Probation Salary','Contracted Salary ','Action'],                             
                            width : [55,88,185,87,198,115,63,117,117,25],
                            align : ['center','center','left','center','left', 'center', 'center','center', 'center','center']
                        }
                    ]

                });                                               
            <% } else { %>

            jQuery("#list").jqGrid({
                url: '/JobRequest/GetListJQGrid/?text=' + encodeURIComponent($('#txtKeyword').val())
                     + '&department=' + $('#Department').val() + '&subdepartment=' + $('#SubDepartment').val() + '&positionId=' + $('#positionId').val()
                     + '&requestorId=' + $('#requestorId').val() + '&statusId=' + $('#statusId').val() + '&request=' + $('#RequestType').val(),
                datatype: 'json',
                mtype: 'GET',
                colNames: ["ID", "Req#", 'Request Date', 'Position' ,'Quantity' , 'Sub Department', 'Expected Start Date', 'Resolution', 'Status',"Request", 'Requestor', 'Forwarded to', 'Action'],
                colModel: [
                    { name: 'RealID', index: 'RealID', align: "center", width: 10, hidden: true },
                    { name: 'ID', index: 'ID', align: "center", width: 40, sortable: true },
                    { name: 'RequestDate', index: 'RequestDate', align: "center", width: 60, sortable: true },
                    { name: 'JobTitleName', index: 'JobTitleName', align: "left", width: 80, sortable: true },
                    { name: 'Quantity', index: 'Quantity', align: "center", width: 40, sortable: true },
                    { name: 'DepartmentName', index: 'DepartmentName', align: "left", width: 60, sortable: true },
                    { name: 'ExpectedStartDate', index: 'ExpectedStartDate', align: "center", width: 80, sortable: true },
                    { name: 'ResolutionName', index: 'ResolutionName', align: "left", width: 90, sortable: true },
                    { name: 'StatusName', index: 'StatusName', align: "center", width: 40, sortable: true },
                    { name: 'Request', index: 'Request', align: "center", width: 40, sortable: true },
                    { name: 'RequestorName', index: 'RequestorName', align: "center", width: 60, sortable: true },
                    { name: 'AssignName', index: 'AssignName', align: "center", width: 100, sortable: true },
                    { name: 'Action', index: 'Action', editable: false, width: 30, align: 'center', sortable: false}],
                pager: '#pager',
                sortname: '<%= (string)ViewData[Constants.JOB_REQUEST_COLUMN]%>',
                sortorder: '<%= (string)ViewData[Constants.JOB_REQUEST_ORDER]%>',
                rowNum: '<%= (string)ViewData[Constants.JOB_REQUEST_ROW_COUNT]%>',
                page: '<%= (string)ViewData[Constants.JOB_REQUEST_PAGE_INDEX]%>',
                rowList: [20, 30, 50, 100, 200],
                multiselect: true,
                viewrecords: true,
                width: 1200, height: "auto",
                imgpath: '/scripts/grid/themes/basic/images',
                loadui: 'block',
                loadComplete: function(){
                    ShowTooltip($("a[class=showTooltip]"), $("#shareit-box"), "/JobRequest/DetailTooltip");
                }
            });

            <% } %>
            $("#btnDelete").click(function () {
                CRM.deleteList('#list', 'RealID', '/JobRequest/DeleteList');
            });
            $("#btnRefresh").click(function () {
                window.location = "/JobRequest/Refresh";
            });
            $('#btnAddNew').click(function () {
                CRM.popup('/JobRequest/Create', 'Add New', 650);
            });

            $("#btnExport").click(function () {
                var numberRow = $("#list").getGridParam("records");
                if (numberRow <= 0) {
                    CRM.msgBox("Have no data for Export !", "300");
                }
                else {
                    var name = encodeURIComponent($('#txtKeyword').val());
                    if (name == '<%= Constants.JOB_REQUEST  %>') {
                        name = "";
                    }
                    window.location = "/JobRequest/ExportToExcel/?text=" + name
                    + '&department=' + $('#Department').val() + '&subdepartment=' + $('#SubDepartment').val() + '&positionId=' + $('#positionId').val()
                    + '&requestorId=' + $('#requestorId').val() + '&statusId=' + $('#statusId').val() + '&request=' + $('#RequestType').val()
                }

            });

            $("#Department").change(function () {
                $("#SubDepartment").html("");
                $("#positionId").html("");
                $("#positionId").append($("<option value=''><%= Constants.FIRST_ITEM_JOBTITLE %></option>"));
                var department = $("#Department").val();
                $("#SubDepartment").append($("<option value=''><%= Constants.FIRST_ITEM_SUB_DEPARTMENT%></option>"));
                $.getJSON('/Library/GenericHandle/DropdownListHandler.ashx?ID=' + department + '&Page=Department', function (item) {
                    $.each(item, function () {
                        if (this['ID'] != undefined) {
                            $("#positionId").append($("<option value='" + this['ID'] + "'>" + this['Name'] + "</option>"));
                        }
                    });
                });
                $.getJSON('/Library/GenericHandle/DropdownListHandler.ashx?ID=' + department + '&Page=SubDepartment', function (item) {
                    $.each(item, function () {
                        if (this['ID'] != undefined) {
                            $("#SubDepartment").append($("<option value='" + this['ID'] + "'>" + this['Name'] + "</option>"));
                        }
                    });
                });
            });
            
            //Filter
            $("#btnFilter").click(function () {
                var name = encodeURIComponent($('#txtKeyword').val());
                if (name == '<%= Constants.JOB_REQUEST  %>') {
                    name = "";
                }
                $('#list').setGridParam({ url: '/JobRequest/GetListJQGrid/?text=' + name
                     + '&department=' + $('#Department').val() + '&subdepartment=' + $('#SubDepartment').val() + '&positionId=' + $('#positionId').val()
                     + '&requestorId=' + $('#requestorId').val() + '&statusId=' + $('#statusId').val() + '&request=' + $('#RequestType').val()
                }).trigger('reloadGrid');
            });

            //Dropdownlist 
            $("#department").change(function () {
                $("#positionId").html("");
                var departmentId = $("#department").val();
                $("#positionId").append($("<option value=''>-Select Position-</option>"));
                $.getJSON('/Library/GenericHandle/DropdownListHandler.ashx?ID=' + departmentId + '&Page=Department', function (item) {
                    $.each(item, function () {
                        if (this['ID'] != undefined) {
                            $("#positionId").append($("<option value='" + this['ID'] + "'>" + this['Name'] + "</option>"));
                        }
                    });
                });
            });
            
            
            /*if($("#Role").val() == '<%=Constants.REQUESTOR_ID %>')     
            {
                $("#btnAddNew").css("display","");
            }
            else
            {
                 $("#btnAddNew").css("display","none");
            }*/            
        });
    </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ModuleName" runat="server">
    <%= JobRequestPageInfo.ModJobRequest %>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="FunctionTitle" runat="server">
    <%= CommonFunc.GetCurrentMenu(Request.RawUrl,true)%>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="LoginRoles" runat="server">
    <% if (ViewData["Role"] != null)
        {%>
    <span class="bold">Login As: </span>
    <%=Html.DropDownList("Role", null, new { @style = "width:180px" })%>
    <%} %>
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="MainContent" runat="server">
    <%= TempData["Message"]%>
    <div id="cactionbutton">
        <table style="width: 100%">
            <tr>                
                <td>
                    <button type="button" id="btnRefresh" title="Refresh" class="button refresh">Refresh</button>
                    <% if (Constants.HR_ID != (int)ViewData[Constants.JOB_REQUEST_ROLE])
                       { %>
                    <button type="button" id="btnExport" title="Export" class="button export">Export</button>
                    
                    <% } %>
                    <%                       
                        //Only Requestor Role has permission to add new and delete                          
                        if (ViewData.Model == Constants.REQUESTOR_ID)
                        {%>
                    <button type="button"  id="btnDelete" title="Delete" class="button delete">Delete</button>
                    <%} %>
                    <% 
                        if(ViewData.Model != null && ConvertUtil.ConvertToInt(ViewData.Model) == Constants.REQUESTOR_ID)                        
                        { %>

                        <button type="button"  id="btnAddNew" title="Add New" class="button addnew">Add New</button>
                   <%
                        } %>
                </td>
            </tr>
        </table>
    </div>
    <input type="text" class="hidden" /> 
    <div id="cfilter">
        <table>
            <tr>
                <td>
                    <input type="text"  id="txtKeyword" maxlength="100" style="width: 170px" value="<%= (string)ViewData[Constants.JOB_REQUEST_KEYWORD]  %>"
                        onfocus="ShowOnFocus(this,'<%= Constants.JOB_REQUEST  %>')" 
                        onblur="ShowOnBlur(this,'<%= Constants.JOB_REQUEST  %>')" autocomplete="off" />
                    <%=Html.DropDownList("Department", ViewData[Constants.JOB_REQUEST_DEPARTMENT] as SelectList, Constants.FIRST_ITEM_DEPARTMENT)%>
                    <%=Html.DropDownList("SubDepartment", ViewData[Constants.JOB_REQUEST_SUB_DEPARTMENT] as SelectList, Constants.FIRST_ITEM_SUB_DEPARTMENT, new { @style = "width:200px" })%>
                    <%=Html.DropDownList("positionId", ViewData[Constants.JOB_REQUEST_POSITION_ID] as SelectList, Constants.FIRST_ITEM_JOBTITLE, new { @style = "width:180px" })%>
                    <%=Html.DropDownList("requestorId", ViewData[Constants.JOB_REQUEST_REQUESTOR_ID] as SelectList, Constants.JOB_REQUEST_REQUESTOR_FIRST_ITEM)%>
                    <%=Html.DropDownList("statusId", ViewData[Constants.JOB_REQUEST_STATUS_ID] as SelectList, Constants.JOB_REQUEST_STATUS_FIRST_ITEM)%>
                    <%=Html.DropDownList("RequestType", ViewData[Constants.JOB_REQUEST_REQUEST_TYPE] as SelectList, Constants.JOB_REQUEST_REQUEST_FIRST_ITEM)%>
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
</asp:Content>
