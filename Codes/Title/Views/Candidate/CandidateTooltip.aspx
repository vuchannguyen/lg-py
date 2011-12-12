<%@ Page Title="" Language="C#" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<% if (ViewData.Model != null)  
   {
       Candidate canObj = (CRM.Models.Candidate)ViewData.Model;
%>
<html>
<body>
<table style="margin:5px; padding-left:10px;" class="view">
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
                    <td class="label">Full Name </td>
                    <td class="input" ><% Response.Write(canObj.FirstName + " " + canObj.MiddleName + " " + canObj.LastName);%></td>
                </tr>
                
                <tr>
                    <td class="label">VN Name</td>
                    <td class="input"><%  Response.Write(canObj.VnFirstName + " " + canObj.VnMiddleName + " " + canObj.VnLastName);%></td>
                </tr>
                 
                <tr>
                    <td class="label">DOB</td>
                    <td class="input"><%  Response.Write(canObj.DOB.HasValue?canObj.DOB.Value.ToString(Constants.DATETIME_FORMAT_VIEW):"");%></td>
                </tr>
                 
                <tr>
                    <td class="label">Telephone</td>
                    <td class="input"><%  Response.Write(canObj.CellPhone);%> </td>
                </tr>
                <tr>
                    <td class="label">Email</td>
                    <td class="input"><%  Response.Write(canObj.Email);%></td>
                </tr>
                <tr>
                    <td class="label">Address</td>
                    <td class="input"> <%  Response.Write(canObj.Address);%></td>
                </tr>
                <tr class="last">
                    <td class="label">Note</td>
                    <td class="input"><%  Response.Write(canObj.Note);%></td>
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
