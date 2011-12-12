<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
<%= TempData["Message"]%>
<br />
   <div class="clist">
        <table id="list" class="scroll">
        </table>
        <div id="pager" class="scroll" style="text-align: center;"></div>
    </div>   

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="PageTitle" runat="server">
<%=  ModulePermissonPageInfo.ComName + CommonPageInfo.AppSepChar + EmsPageInfo.ComName + CommonPageInfo.AppSepChar + CommonPageInfo.AppName%>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
<script type="text/javascript">
    $(document).ready(function () {
        jQuery("#list").jqGrid({
            url: '/ModulePermisson/GetListJQGrid',
            datatype: 'json',
            mtype: 'GET',
            colNames: ["ID",'Module',"Permisson", 'Action'],
            colModel: [
                { name: 'ID', index: 'ID', align: "center", width: 20, title: false, sortable: false },
                  { name: 'Module', index: 'Module', align: "left", width: 30, title: false, sortable: false },
                  { name: 'Permisson', index: 'Permisson', align: "left", width: 150, title: false, sortable: false },
                  { name: 'Action', index: 'Action', align: "center", width: 30, sortable: false }],
            rowList: [20, 30, 50, 100, 200],
            width: 1200,
            height: "100%",
            pager: '#pager',
            viewrecords: true,
            sortorder: "asc",
            multiselect: true,
            loadui: 'block'
        });
    });
    </script>
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="ModuleName" runat="server">
<%=  ModulePermissonPageInfo.ComName%>
</asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="FunctionTitle" runat="server">
<%=  CommonFunc.GetCurrentMenu(Request.RawUrl,true)%>
</asp:Content>

<asp:Content ID="Content6" ContentPlaceHolderID="LoginRoles" runat="server">
</asp:Content>
