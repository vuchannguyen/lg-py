<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<%@ Register Src="PerformanceReviewForm/UCPRAdmin.ascx" TagName="UCPRRnD1" TagPrefix="uc1" %>     
    
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <%using (Html.BeginForm("PerformanceReviewAdmin", "Eform", FormMethod.Post, new { @id = "editForm", @class = "form" })) %>
        <% { %>
    <div class="form" style="height:900px;overflow-x:scroll;">
    <uc1:UCPRRnD1 ID="UCPRRnD11" runat="server" />
    </div>
    <table style="width: 1024px">
        <tr>
            <td>
                <div class="form" style="text-align: center">
                    <input type="submit" title="Save" class="save" value="" />
                    <% string javascript = "javascript:CRM.back()";                       
                    %>
                    <input type="button" title="Cancel" onclick="<%=javascript %>" id="btnCancel" class="cancel"
                        value="" />
                </div>
            </td>
        </tr>
    </table>
    <% } %>
</asp:Content>    
<asp:Content ID="Content2" ContentPlaceHolderID="PageTitle" runat="server">
    <%=HiringCenterPageInfo.ModInterview + CommonPageInfo.AppSepChar + HiringCenterPageInfo.ComName + CommonPageInfo.AppSepChar + CommonPageInfo.AppName%>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ModuleName" runat="server">
    <%=HiringCenterPageInfo.ComName%>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="FunctionTitle" runat="server">
    
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="LoginRoles" runat="server">
</asp:Content>