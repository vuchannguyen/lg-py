<%@ Page Title="" Language="C#" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<% if (ViewData.Model != null)  
   {
       STT stt = (CRM.Models.STT)ViewData.Model;
%>
<html>
<body>
<table style="margin:5px;">
    <tr>
        <td>
             <% 
                 if (!string.IsNullOrEmpty(stt.Photograph))
                 {
                     Response.Write("<img align='left' class='image_align' src='" + Constants.IMAGE_PATH + stt.Photograph + "' />");
                 }
                 else
                 {
                     Response.Write("<img src='/Content/Images/Common/nopic.gif' height='120px' width='120px' />");
                 }
        %>
        </td>
        <td valign="top" style="padding-left:10px;">
            <span style="font-weight: bold">Name: </span><%  Response.Write(stt.FirstName + " " + stt.MiddleName + " " + stt.LastName);%><br />
            <span style="font-weight: bold">Start Date: </span><%  Response.Write(stt.StartDate.ToString(Constants.DATETIME_FORMAT_VIEW));%><br />                        
            <span style="font-weight: bold">Result: </span><%  Response.Write( stt.ResultId.HasValue?stt.STT_Result.Name:"");%><br />
            <span style="font-weight: bold">Status: </span><%  Response.Write(stt.STT_Status.Name);%><br />
            <span style="font-weight: bold">Yahoo Id: </span><%  Response.Write(stt.YahooId);%><br />
             <span style="font-weight: bold">Skype Id: </span><%  Response.Write(stt.SkypeId);%><br />
            <span style="font-weight: bold">Cell Phone: </span><%  Response.Write(stt.CellPhone);%>           
        </td>
    </tr>
</table>
</body>
</html>
<%            
   }
%>
