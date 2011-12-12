<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<dynamic>" %> 

    <div>
         <% 
             if (ViewData["PTO"] != null)
             {
                 PTO pto = (PTO)ViewData["PTO"];
                 List<PTO_Detail> details = (List<PTO_Detail>)ViewData["PTO_Details"];
                 if (pto.PTO_Type.IsHourType)
                 {
         %>
        
        <table class="tooltip" style="width:300px;">
            <tr>
                <th colspan="3"><%=pto.ID%></th>
            </tr>
            <tr>
                <td style="font-weight:bold; text-align:center">Date</td>
                <td style="font-weight:bold; text-align:center">Hour(s)</td>
                <td style="font-weight:bold; text-align:center">Time Off</td>
            </tr>
            <%                
                foreach (PTO_Detail detail in details)
                { 
            %>                               
                    <tr>
                        <td style="text-align:center;"><%=!detail.DateOff.HasValue ? "" : detail.DateOff.Value.ToString(Constants.DATETIME_FORMAT_VIEW)%></td>
                        <td style="text-align:center;"><%= !detail.DateOff.HasValue ? "" : detail.TimeOff.Value.ToString()%></td>
                        <td style="text-align:center;"><%= detail.HourFrom.ToString() + ":00 - " + detail.HourTo.ToString() + ":00" %></td>
                    </tr>
            <%
                }  
            %>            
        </table>
        <%
                 }
                 else
                 { %>
        
        <table class="tooltip" style="width:200px;">
            <tr>
                <th colspan="2"><%=pto.ID%></th>                
            </tr>
            <tr>
                <td style="font-weight:bold; text-align:center">From Date</td>
                <td style="text-align:center;"><%= details[0].DateOffFrom.Value.ToString(Constants.DATETIME_FORMAT_VIEW)%></td>
            </tr>
            <tr>
                <td style="font-weight:bold; text-align:center">To Date</td>
                <td style="text-align:center;"><%= details[0].DateOffTo.Value.ToString(Constants.DATETIME_FORMAT_VIEW)%></td>
            </tr>                    
        </table>
        <% 
                 }
             } 
        %>
    </div>