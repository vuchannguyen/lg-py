<%@ Page Title="" Language="C#" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<% if (ViewData.Model != null)  
   {
       Candidate canObj = (CRM.Models.Candidate)ViewData.Model;
%>
<html>
<body>
<table style="margin:5px;">
    <tr>
        <td>
             <% 
                 if (!string.IsNullOrEmpty(canObj.Photograph))
                 {
                     Response.Write("<img align='left' class='image_align' src='" + Constants.PHOTO_PATH_ROOT_CANDIDATE + canObj.Photograph + "' />");
                 }
                 else
                 {
                     Response.Write("<img src='/Content/Images/Common/nopic.gif' height='120px' width='120px' />");
                 }
        %>
        </td>
        <td valign="top" style="padding-left:10px;">
            <table width="100%" cellpadding="1px" cellspacing="1px" >
                <tr>
                    <td style="width:80px"><span style="font-weight: bold">FulName: </span></td>
                    <td><%  Response.Write(canObj.FirstName + " " + canObj.MiddleName + " " + canObj.LastName);%></td>
                </tr>
                
                <tr>
                    <td><span style="font-weight: bold">VnName: </span></td>
                    <td><%  Response.Write(canObj.VnFirstName + " " + canObj.VnMiddleName + " " + canObj.VnLastName);%></td>
                </tr>
                 
                <tr>
                    <td><span style="font-weight: bold">DOB: </span></td>
                    <td><%  Response.Write(canObj.DOB.ToString(Constants.DATETIME_FORMAT_VIEW));%></td>
                </tr>
                 
                <tr>
                    <td><span style="font-weight: bold">Telephone: </span></td>
                    <td><%  Response.Write(canObj.CellPhone);%> </td>
                </tr>
                <tr>
                    <td><span style="font-weight: bold">Email: </span></td>
                    <td><%  Response.Write(canObj.Email);%></td>
                </tr>
                <tr>
                    <td><span style="font-weight: bold">Address: </span></td>
                    <td> <%  Response.Write(canObj.Address);%></td>
                </tr>
                <tr>
                    <td><span style="font-weight: bold">Note: </span></td>
                    <td><%  Response.Write(canObj.Note);%></td>
                </tr>
            </table>
                      
        </td>
    </tr>
</table>
</body>
</html>
<%            
   }
%>
