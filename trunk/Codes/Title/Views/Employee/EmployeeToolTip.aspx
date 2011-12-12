<%@ Page Title="" Language="C#" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<% if (ViewData.Model != null)  
   {
       Employee emp = (CRM.Models.Employee)ViewData.Model;
%>
<html>
<body>
<table style="margin:5px;">
    <tr>
        <td>
             <% 
                 if (!string.IsNullOrEmpty(emp.Photograph))
                 {
                     Response.Write("<img align='left' class='image_align' src='" + Constants.IMAGE_PATH + emp.Photograph + "' />");
                 }
                 else
                 {
                     Response.Write("<img src='/Content/Images/Common/nopic.gif' height='120px' width='120px' />");
                 }
        %>
        </td>
        <td valign="top" style="padding-left:10px;">
            <span style="font-weight: bold">Name: </span><%  Response.Write(emp.FirstName + " " + emp.MiddleName + " " + emp.LastName);%><br />
            <span style="font-weight: bold">Start Date: </span><%  Response.Write(emp.StartDate.ToString(Constants.DATETIME_FORMAT_VIEW));%><br />
            <span style="font-weight: bold">Sub Department: </span><%  Response.Write(emp.Department.DepartmentName);%><br />
            <span style="font-weight: bold">Job Title: </span><%  Response.Write(emp.JobTitleLevel.DisplayName);%><br />
            <span style="font-weight: bold">Status: </span><%  Response.Write(emp.EmpStatusId.HasValue? emp.EmployeeStatus.StatusName:"");%><br />
            <span style="font-weight: bold">Yahoo Id: </span><%  Response.Write(emp.YahooId);%><br />
             <span style="font-weight: bold">Skype Id: </span><%  Response.Write(emp.SkypeId);%><br />
            <span style="font-weight: bold">Cell Phone: </span><%  Response.Write(emp.CellPhone);%>           
        </td>
    </tr>
</table>
</body>
</html>
<%            
   }
%>
