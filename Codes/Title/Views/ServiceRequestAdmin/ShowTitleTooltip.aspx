<%@ Page Title="" Language="C#" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<% if (ViewData.Model != null)  
   {
       SR_ServiceRequest sr = (CRM.Models.SR_ServiceRequest)ViewData.Model;
%>
<html>
<body>
<table style="margin:5px;width:400px;">
    <tr>        
        <td valign="top" style="padding-left:10px;width:385px;">
            <span style="font-weight: bold;word-wrap:break-word">Title: </span><%  Response.Write(Server.HtmlEncode(sr.Title).Replace("\r\n", "<br/>"));%><br />
            <span style="font-weight: bold;word-wrap:break-word">Description: </span><%  Response.Write(Server.HtmlEncode(sr.Description).Replace("\r\n", "<br/>"));%><br />            
        </td>
    </tr>
</table>
</body>
</html>
<%            
   }
%>
