<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <%= TempData["Message"]%>
    <% Employee emp = (Employee)ViewData["Employee"];%>
    <%= Html.Hidden("EmployeeId",TempData["EmployeeId"])%>
    <div class="clist">
        <table id="list" class="scroll">
        </table>
        <div id="pager" class="scroll" style="text-align: center;">
        </div>
    </div>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="PageTitle" runat="server">
<%=EmsPageInfo.ModPerformanceReview + CommonPageInfo.AppSepChar + EmsPageInfo.ComName + CommonPageInfo.AppSepChar + CommonPageInfo.AppName%>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
<script type="text/javascript">
    function getListTargetUrl() {
        var url = "/Employee/GetListPerformanceReview";
        return url;
    }
    $(document).ready(function () {
        CRM.onEnterKeyword();
        jQuery("#list").jqGrid({
            url: getListTargetUrl(),
            datatype: 'json',
            mtype: 'GET',
            colNames: ['ID', 'Manager', 'PR Date', 'Next PR Date', 'Status', 'Resolution'],
            colModel: [
                  { name: 'ID', index: 'ID', align: "center", width: 20, sortable: true },
                  { name: 'Manager', index: 'Manager', align: "left", width: 40, sortable: true },
                  { name: 'PRDate', index: 'PRDate', align: "center", width: 40, sortable: true },
                  { name: 'NextPRDate', index: 'NextPRDate', align: "center", width: 40, sortable: true },
                  { name: 'Status', index: 'Status', align: "center", width: 40, sortable: true },
                  { name: 'Resolution', index: 'Resolution', align: "center", width: 30, sortable: true }],
            pager: jQuery('#pager'),
            rowList: [20, 30, 50, 80, 100],
            viewrecords: true,
            multiselect: false,
            width: 1024, height: "auto",
            sortname: 'ID',
            sortorder: 'asc',
            rowNum: 20,
            page: 1,
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
    });
    
    </script>
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="ModuleName" runat="server">
<%=  EmsPageInfo.ComName%>
</asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="FunctionTitle" runat="server">
<% Employee emp = (Employee)ViewData.Model;%>
 <%= CommonFunc.GetCurrentMenu(Request.RawUrl)+ "<a href='/Employee/Detail/" + emp.ID +"'>"
        + (emp.ID + " - " + emp.FirstName + " " + emp.MiddleName + " " + emp.LastName)+"</a>"+
    " » " + EmsPageInfo.ModPerformanceReview%>
</asp:Content>

<asp:Content ID="Content6" ContentPlaceHolderID="LoginRoles" runat="server">
</asp:Content>
