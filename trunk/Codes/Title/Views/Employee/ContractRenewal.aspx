<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content4" ContentPlaceHolderID="ModuleName" runat="server">
    <%=  EmsPageInfo.ComName%>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="FunctionTitle" runat="server">
    <% Employee emp = (Employee)ViewData["Employee"];%>
    <%--<%=EmsPageInfo.MenuName + CommonPageInfo.AppDetailSepChar %>--%>
    <%--<a href="/Employee/"><%=EmsPageInfo.ModActiveEmployees%></a> » <a href='<%= "/Employee/Detail/" + emp.ID %>'>--%>
        <%= CommonFunc.GetCurrentMenu(Request.RawUrl) + "<a href='/Employee/Detail/" + emp.ID + "'>" + (emp.ID + " - " + emp.FirstName + " " + emp.MiddleName + " " + emp.LastName) + "</a>"%>
    » <%=EmsPageInfo.ModContractRenewal%>
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="LoginRoles" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PageTitle" runat="server">
    <%=EmsPageInfo.ModContractRenewal + CommonPageInfo.AppSepChar + EmsPageInfo.ComName + CommonPageInfo.AppSepChar + CommonPageInfo.AppName%>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <%= TempData["Message"]%>
    <% Employee emp = (Employee)ViewData["Employee"];%>
    <%= Html.Hidden("EmployeeId",TempData["EmployeeId"])%>
    <div id="cactionbutton">
        <button type="button" id="btnRefesh" title="Refesh" class="button refresh">
            Refesh</button>
        <button  type="button" id="btnDelete" title="Delete" class="button delete">
            Delete</button>
        <% if (TempData["AddNew"] == null)
           {%>
        <button type="button" id="btnAddNew" title="Add New Contract" class="button addnew">
            Add New</button>
        <% } %>
    </div>
    <div class="clist">
        <table id="list" class="scroll">
        </table>
        <div id="pager" class="scroll" style="text-align: center;">
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
    <script type="text/javascript">
        $(document).ready(function () {
            jQuery("#list").jqGrid({
                url: '/Employee/GetContractRenewalJQGrid/?id=' + $("#EmployeeId").val(),
                datatype: 'json',
                mtype: 'GET',
                colNames: ['ContractId', 'Contract Number','Start Date', 'Start Date', 'End Date', 'End Date', 'Contract Type', 'Contract File', 'Action'],
                colModel: [
                  { name: 'ContractId', index: 'ContractId', align: "center", width: 50, hidden: true, sortable: false },
                  { name: 'ContractNumber', index: 'ContractNumber', align: "left", width: 80, hidden: false, sortable: false },
                  { name: 'StartDateView', index: 'StartDateView', align: "center", width: 60, sortable: false },
                  { name: 'StartDate', index: 'StartDate', align: "center", width: 60, hidden: true },
                  { name: 'EndDate', index: 'EndDate', align: "center", width: 60, hidden: true },
                  { name: 'EndDateView', index: 'EndDateView', align: "center", width: 60, sortable: false },
                  { name: 'ContractType', index: 'ContractType', align: "center", width: 80, sortable: false },
                  { name: 'ContactFile', align: "left", width: 120, sortable: false },
                  { name: 'Action', align: "center", width: 60, sortable: false}],
                pager: jQuery('#pager'),
                rowList: [20, 30, 50, 100, 200],
                sortname: 'StartDate',
                sortorder: "desc",
                multiselect: true,
                viewrecords: true,
                width: 1024,
                height: "auto",
                imgpath: '/scripts/grid/themes/basic/images',
                loadui: 'block',
                loadComplete: function () {
                    var numberRow = $("#list").getGridParam("records");
                    for (var i = 1; i <= numberRow; i++) {
                        var status = $("#" + i).find("td").find("div").attr("id");
                        if (status != undefined) {
                            $("#" + i).find("td").addClass("row_active");
                            break;
                        }
                    }
                }
            });

            $("#btnDelete").click(function () {
                CRM.deletes('#list', 'ContractId', '/Employee/DeleteContractList');
            });

            $("#btnRefesh").click(function () {
                window.location = "/Employee/ContractRenewal/" + $("#EmployeeId").val();
            });

            $("#btnAddNew").click(function () {
                CRM.popup("/Employee/CreateContract/" + $("#EmployeeId").val(), "Add New Contract ", 550);
            });
        });
    </script>
</asp:Content>
