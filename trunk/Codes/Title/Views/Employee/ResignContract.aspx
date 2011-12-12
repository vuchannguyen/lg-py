<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <%= Html.Hidden("EmployeeId",TempData["EmployeeId"] as string)%>
    <% Employee emp = (Employee)ViewData["Employee"];%>
    <div id="cactionbutton">
    </div>
    <div class="clist">
        <table id="list" class="scroll">
        </table>
        <div id="pager" class="scroll" style="text-align: center;">
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PageTitle" runat="server">
    Resigned Contract Renewal - CRM
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
    <script type="text/javascript">
        $(document).ready(function () {
            jQuery("#list").jqGrid({
                url: '/Employee/GetResignContractJQGrid/?id=' + $("#EmployeeId").val(),
                datatype: 'json',
                mtype: 'GET',
                colNames: ['ContractId', 'Start Date', 'Start Date', 'End Date', 'End Date', 'Contract Type', 'Contract File'],
                colModel: [
                  { name: 'ContractId', index: 'ContractId', align: "center", width: 50, hidden: true, sortable: false },
                  { name: 'StartDateView', index: 'StartDateView', align: "center", width: 60, sortable: false },
                  { name: 'StartDate', index: 'StartDate', align: "center", width: 60, hidden: true },
                  { name: 'EndDate', index: 'EndDate', align: "center", width: 60, hidden: true },
                  { name: 'EndDateView', index: 'EndDateView', align: "center", width: 60, sortable: false },
                  { name: 'ContractType', index: 'ContractType', align: "center", width: 120, sortable: false },
                  { name: 'ContactFile', align: "left", width: 120, sortable: false}],
                pager: jQuery('#pager'),
                rowList: [20, 30, 50, 100, 200],
                sortname: 'StartDate',
                sortorder: "desc",
                viewrecords: true,
                width: 1024,
                height: "auto",
                imgpath: '/scripts/grid/themes/basic/images',
                loadui: 'block'
            });
        });
    </script>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ModuleName" runat="server">
    Employee Management System
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="FunctionTitle" runat="server">

 <% Employee emp = (Employee)ViewData["Employee"];%>
  <%= "Management » Employee » " +  "<a href='/Employee/EmployeeResignList/'>" + EmsPageInfo.ModResignedEmployees + "</a> » "
         + "<a href='/Employee/Detail/" + emp.ID+"'>"+emp.ID + " - " + emp.FirstName + " " + emp.MiddleName + " " + emp.LastName+"</a>"%>
    » <%=EmsPageInfo.ModContractRenewal%>
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="LoginRoles" runat="server">
</asp:Content>
