<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>
<%
    if (ViewData.Model != null)
    {
        PurchaseRequest pr = (PurchaseRequest)ViewData.Model;
        Response.Write(HttpUtility.HtmlEncode(pr.Justification).Replace("\r\n", "<br/>"));
    }
%>