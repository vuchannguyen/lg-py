<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>
<%
    if (ViewData.Model != null)
    {
        Training_Material pr = (Training_Material)ViewData.Model;
        
        
        %>
<html>
<body>
    <table style="margin:5px;width:500px">
        <tr>
            <td style="width:490px">
            <%Response.Write(HttpUtility.HtmlEncode(pr.Description).Replace("\r\n", "<br/>")); %>        
            </td>
        </tr>
    </table>
</body>
</html>
<%
    }
%>