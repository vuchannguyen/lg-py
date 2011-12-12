<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <style type="text/css">
        #shareit-box {
            width:800px;
        }
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
        
        table.dateoff_detail th.date
        {
            width:100px;
        }
        table.dateoff_detail th.number
        {
            width:15px;
        }
        table.dateoff_detail th.hours
        {
            width:55px  ;
        }
        table.dateoff_detail th.paid
        {
            width:55px;
        }
        table.dateoff_detail th.type
        {
            width:150px;
            
        }
        table.dateoff_detail th.reason
        {
            width:120px;
            
        }
    </style>
</head>
<body>
    <div>
    <%
        var dateList = ViewData.Model as List<PTO_Detail>;
    %>
    
    <table class="dateoff_detail" cellpadding="0" cellspacing="0" border="1">
        <tr>
            <th class="number">#</th>
            <th class="date">Date</th>
            <th class="date">Submitting Date</th>
            <th class="hours">Hour(s)</th>
            <th class="paid">Is Paid</th>
            <th class="type">Type</th>
            <th class="reason">Reason</th>
            <th class="remark">Remark</th>
        </tr>
        <%
            string rowTemplate = "<tr><td style='text-align:center'>{0}</td>" +
                        "<td style='text-align:left; '>{1}</td>" +
                        "<td style='text-align:center; '>{2}</td>" +
                        "<td style='text-align:center;'>{3}</td>" +
                        "<td style='text-align:center'>{4}</td>" + 
                        "<td>{5}</td><td>{6}</td><td>{7}</td></tr>";
            int count = 0;
            foreach (var item in dateList)
            {
                count++;
                if (item.PTO.PTO_Type.IsHourType)
                {
                    Response.Write(string.Format(rowTemplate,count,
                        item.DateOff.Value.ToString(Constants.DATETIME_FORMAT_VIEW),
                        item.PTO.CreateDate.ToString(Constants.DATETIME_FORMAT_VIEW),
                        item.TimeOff,
                        item.IsCompanyPay ? "Yes" : "No",
                        item.PTO.PTO_Type.Name, item.PTO.Reason, item.PTO.HRComment));
                }
                else
                {
                    DateTime dFrom = (DateTime)ViewData["dFrom"];
                    DateTime dTo = (DateTime)ViewData["dTo"];
                    DateTime toDate = item.DateOffTo.Value <= dTo ? item.DateOffTo.Value : dTo;
                    DateTime fromDate = item.DateOffFrom >= dFrom ? item.DateOffFrom.Value : dFrom;
                    Response.Write(string.Format(rowTemplate, count,
                        fromDate.ToString(Constants.DATETIME_FORMAT_VIEW) +
                        "<br/><span style='height:10px !important' class='arrow_to_right'></span>" +
                        toDate.ToString(Constants.DATETIME_FORMAT_VIEW),
                        item.PTO.CreateDate.ToString(Constants.DATETIME_FORMAT_VIEW),
                        CommonFunc.GetWorkingHours(fromDate, toDate),
                        item.IsCompanyPay ? "Yes" : "No",
                        item.PTO.PTO_Type.Name, item.PTO.Reason, item.PTO.HRComment));
                }
            }
        %>
    </table>
    </div>
</body>
</html>
