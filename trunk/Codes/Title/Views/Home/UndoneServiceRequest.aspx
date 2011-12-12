<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<%
    var list = ViewData.Model as List<SR_ServiceRequest>;
    string result = string.Empty;
    if (list!= null && list.Count > 0)
    {
        //result += "<button id='btnExport' type='button' title='Export' class='button export' style='margin-bottom:7px;margin-right:20px;'>Export</button>";

        if (list.Count > 10)
        {
            result += "<div id='div_notification' style='overflow-x: scroll;overflow-y: auto; padding-top: 0px; height:280px; width:650px'>";
        }
        result += "<table class='grid'>";
        result += "<tr>";
        result += "<th>ID</th>";
        result += "<th style='width:160px'>Title</th>";
        result += "<th>Status</th>";
        result += "<th>Assigned To</th>";
        result += "<th style='width:140px'>Due Date</th>";
        result += "</tr>";
        foreach (var item in list)
        {
            result += "<tr>";
            result += "<td align='center'><a  href='/ServiceRequestAdmin/Detail/" + item.ID + "' id='" + 
                item.ID + "' class='srTooltip' >" + Constants.SR_SERVICE_REQUEST_PREFIX + item.ID + "</a></td>";
            item.Title = CommonFunc.SubStringRoundWordNotMultiline(item.Title, 20);
            result += "<td align='justify'>" + HttpUtility.HtmlEncode(item.Title) + "</td>";
            result += "<td align='center'>" + item.SR_Status.Name + "</td>";
            result += "<td align='center'>" + item.AssignUser + "</td>";
            result += "<td align='center'>" + item.DueDate.Value.ToString("dd-MMM-yyyy hh:mm tt") + "</td>";
            result += "</tr>";
        }
        result += "</table>";
        if (list.Count > 10)
        {
            result += "</div>";
        }
    }
    Response.Write(result);    
 %>