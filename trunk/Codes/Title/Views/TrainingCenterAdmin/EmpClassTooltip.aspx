<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>EmpClassTooltip</title>
    <style type="text/css">
        table.dateoff_detail
        {
            
            border:1px solid black !important;
            border-collapse:collapse;
            width:100%;
        }
        table.dateoff_detail td, table.dateoff_detail th
        {
            vertical-align:middle;
            border: 1px solid #CCCCCC;
            padding: 4px 5px;
        }
        table.dateoff_detail th
        {
            background-image: url("/Content/Images/Common/ghead.gif") !important;
            color: White;
        }
    </style>
</head>
<body>
    <div>
        <table class="dateoff_detail" cellpadding="0" cellspacing="0" border="1">
        <tr>
            <th class="number">Class ID</th>
            <th class="date">Instructors</th>
            <th class="date">Start Date</th>
            <th class="date">Time</th>
            <th class="hours">Score</th>
        </tr>
        <%
            var theClass = ViewData.Model as Training_Class;
            string rowTemplate = "<tr><td style='text-align:center'>{0}</td>" +
                        "<td style='text-align:left; '>{1}</td>" +
                        "<td style='text-align:center;'>{2}</td>" +
                        "<td style='text-align:left'>{3}</td>" +
                        "<td style='text-align:center;'>{4}</td>";
            Response.Write(string.Format(rowTemplate, theClass.ClassId, theClass.Instructors, 
                theClass.StartDate.ToString(Constants.DATETIME_FORMAT_VIEW), theClass.ClassTime, ViewData["result"]));
        %>
    </table>  
    </div>
</body>
</html>
