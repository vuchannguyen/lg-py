<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
<%
    List<string> success = ViewData["SUCCESS"] as List<string>;
    List<string> notSuccess = ViewData["NOTSUCCESS"] as List<string>;
%>
    <table border="1" width="600px">
        <tr>
            <td>SUCCESS: <%= success.Count %></td>
            <td>NOT SUCCESS: <%= notSuccess.Count %></td>
        </tr>
        <tr>
            <td valign="top">
                <%
                    foreach (string id in success)
                    {
                        Response.Write(id + "<br/>");
                    }
                %>
            </td>
            <td valign="top">
                <%
                    foreach (string id in notSuccess)
                    {
                        Response.Write(id + "<br/>");
                    }
                %>
            </td>
        </tr>
    </table>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="PageTitle" runat="server">
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="ModuleName" runat="server">
    
</asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="FunctionTitle" runat="server">
</asp:Content>

<asp:Content ID="Content6" ContentPlaceHolderID="LoginRoles" runat="server">
</asp:Content>
