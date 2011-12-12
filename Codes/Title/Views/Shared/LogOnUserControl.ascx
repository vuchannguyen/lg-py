<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<%
    if (Request.IsAuthenticated) {
%>        
        <%--<%: Html.ActionLink("Log Off", "DoLogOff", "Authenticate")%>--%>
        <a class="logout" href="/Authenticate/DoLogOff">Log Off</a>[ <b><%: Page.User.Identity.Name %></b> ]
<%
    }
    else {
%> 
        [ <%: Html.ActionLink("Log On", "LogOn", "Account") %> ]
<%
    }
%>
