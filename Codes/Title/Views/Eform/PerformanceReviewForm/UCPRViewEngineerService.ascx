<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<%
    List<EForm_Detail> eFormDetailList = ViewData["detail_list"] == null ? null : (List<EForm_Detail>)ViewData["detail_list"];
    
 %>
<script type="text/javascript"> 
</script>

<%=Html.Hidden("Hidden1", ViewData["eform"]==null? "" : ViewData["eform"].ToString())%>
    <table width="100%" border="0" cellpadding="0" cellspacing="0" class="grid" >
        <tr>
            <th rowspan =2 style="width:20%;" class="gray">
            Qualification Group
            </th>
            <th align="center" colspan=8 style="width:56%;" class="gray">
                Criteria
            </th>
            <th align="center" rowspan=2 style="width:24%;" class="gray">
                Remark
            </th>
        </tr>        
        <tr class="last">            
            <th align="center" style="width:7%;" class="gray">
                C1
            </th>
            <th align="center" style="width:7%;" class="gray">
                C2
            </th>
            <th align="center" style="width:7%;" class="gray">
                C3
            </th>
            <th align="center" style="width:7%;" class="gray">
                C4
            </th>
            <th align="center" style="width:7%;" class="gray">
                C5
            </th>
            <th align="center" style="width:7%;" class="gray">
                C6
            </th>
            <th align="center" style="width:7%;" class="gray">
                C7
            </th>
            <th align="center" style="width:7%;" class="gray">
                C8
            </th>
            
        </tr>    
        <tr>
            <td align="left">
                Year of Services
            </td>
            <td align="center" >            
                <span class='<%= CommonFunc.GetRadioStatus("RadioButton1", eFormDetailList, "1")?"check":"none"%>'></span>               
                 
            </td>
            <td align="center" >
                <span class='<%= CommonFunc.GetRadioStatus("RadioButton1", eFormDetailList, "2")?"check":"none"%>'></span>               
                
            </td>
            <td align="center" >
                <span class='<%= CommonFunc.GetRadioStatus("RadioButton1", eFormDetailList, "3")?"check":"none"%>'></span>               
            </td>
            <td align="center" >
                <span class='<%= CommonFunc.GetRadioStatus("RadioButton1", eFormDetailList, "4")?"check":"none"%>'></span>               
            </td>
            <td align="center" >
                <span class='<%= CommonFunc.GetRadioStatus("RadioButton1", eFormDetailList, "5")?"check":"none"%>'></span>               
            </td>
            <td align="center" >
                <span class='<%= CommonFunc.GetRadioStatus("RadioButton1", eFormDetailList, "6")?"check":"none"%>'></span>               
            </td>
            <td align="center" >
                <span class='<%= CommonFunc.GetRadioStatus("RadioButton1", eFormDetailList, "7")?"check":"none"%>'></span>               
            </td>
            <td align="center" >
                <span class='<%= CommonFunc.GetRadioStatus("RadioButton1", eFormDetailList, "8")?"check":"none"%>'></span>                               
            </td>
            <td align="left" >
                
                <%=Html.Encode(CommonFunc.GetTextBoxValue("TextArea1", eFormDetailList))%>    
            </td>
        </tr>            
        <tr>
            <td align="left" >
                English
            </td>
            <td align="center" >            
                 <span class='<%= CommonFunc.GetRadioStatus("RadioButton2", eFormDetailList, "1")?"check":"none"%>'></span>    
            </td>
            <td align="center" >
                <span class='<%= CommonFunc.GetRadioStatus("RadioButton2", eFormDetailList, "2")?"check":"none"%>'></span>    
            </td>
            <td align="center" >
                <span class='<%= CommonFunc.GetRadioStatus("RadioButton2", eFormDetailList, "3")?"check":"none"%>'></span>    
            </td>
            <td align="center" >
                <span class='<%= CommonFunc.GetRadioStatus("RadioButton2", eFormDetailList, "4")?"check":"none"%>'></span>    
            </td>
            <td align="center" >
                <span class='<%= CommonFunc.GetRadioStatus("RadioButton2", eFormDetailList, "5")?"check":"none"%>'></span>    
            </td>
            <td align="center" >
                <span class='<%= CommonFunc.GetRadioStatus("RadioButton2", eFormDetailList, "6")?"check":"none"%>'></span>    
            </td>
            <td align="center" >
                <span class='<%= CommonFunc.GetRadioStatus("RadioButton2", eFormDetailList, "7")?"check":"none"%>'></span>    
            </td>
            <td align="center" >
                <span class='<%= CommonFunc.GetRadioStatus("RadioButton2", eFormDetailList, "8")?"check":"none"%>'></span>    
            </td>
            <td align="left" >
                <%=Html.Encode(CommonFunc.GetTextBoxValue("TextArea2", eFormDetailList))%>                 
                
            </td>
        </tr>     
        <tr>
            <td align="left" >
                Communication
            </td>
            <td align="center" > 
                 <span class='<%= CommonFunc.GetRadioStatus("RadioButton3", eFormDetailList, "1")?"check":"none"%>'></span>    
            </td>
            <td align="center" >
                <span class='<%= CommonFunc.GetRadioStatus("RadioButton3", eFormDetailList, "2")?"check":"none"%>'></span>    
            </td>
            <td align="center" >
                <span class='<%= CommonFunc.GetRadioStatus("RadioButton3", eFormDetailList, "3")?"check":"none"%>'></span>    
            </td>
            <td align="center" >
                <span class='<%= CommonFunc.GetRadioStatus("RadioButton3", eFormDetailList, "4")?"check":"none"%>'></span>    
            </td>
            <td align="center" >
                <span class='<%= CommonFunc.GetRadioStatus("RadioButton3", eFormDetailList, "5")?"check":"none"%>'></span>    
            </td>
            <td align="center" >
                <span class='<%= CommonFunc.GetRadioStatus("RadioButton3", eFormDetailList, "6")?"check":"none"%>'></span>    
            </td>
            <td align="center" >
                <span class='<%= CommonFunc.GetRadioStatus("RadioButton3", eFormDetailList, "7")?"check":"none"%>'></span>    
            </td>
            <td align="center" >
                <span class='<%= CommonFunc.GetRadioStatus("RadioButton3", eFormDetailList, "8")?"check":"none"%>'></span>    
            </td>
            <td align="left" >
                <%=Html.Encode(CommonFunc.GetTextBoxValue("TextArea3", eFormDetailList))%>                
            </td>
        </tr>     
        <tr>
            <td align="left" >
                Mentoring
            </td>
            <td align="center" >            
                 <span class='<%= CommonFunc.GetRadioStatus("RadioButton4", eFormDetailList, "1")?"check":"none"%>'></span>    
            </td>
            <td align="center" >
                <span class='<%= CommonFunc.GetRadioStatus("RadioButton4", eFormDetailList, "2")?"check":"none"%>'></span>    
            </td>
            <td align="center" >
                <span class='<%= CommonFunc.GetRadioStatus("RadioButton4", eFormDetailList, "3")?"check":"none"%>'></span>    
            </td>
            <td align="center" >
                <span class='<%= CommonFunc.GetRadioStatus("RadioButton4", eFormDetailList, "4")?"check":"none"%>'></span>    
            </td>
            <td align="center" >
                <span class='<%= CommonFunc.GetRadioStatus("RadioButton4", eFormDetailList, "5")?"check":"none"%>'></span>    
            </td>
            <td align="center" >
                <span class='<%= CommonFunc.GetRadioStatus("RadioButton4", eFormDetailList, "6")?"check":"none"%>'></span>    
            </td>
            <td align="center" >
                <span class='<%= CommonFunc.GetRadioStatus("RadioButton4", eFormDetailList, "7")?"check":"none"%>'></span>    
            </td>
            <td align="center" >
                <span class='<%= CommonFunc.GetRadioStatus("RadioButton4", eFormDetailList, "8")?"check":"none"%>'></span>    
            </td>
            <td align="left" >
                <%=Html.Encode(CommonFunc.GetTextBoxValue("TextArea4", eFormDetailList))%>
            </td>
        </tr>     
        <tr>
            <td align="left" >
                Technical Training
            </td>
            <td align="center" >
                 <span class='<%= CommonFunc.GetRadioStatus("RadioButton5", eFormDetailList, "1")?"check":"none"%>'></span>    
            </td>
            <td align="center" >
                <span class='<%= CommonFunc.GetRadioStatus("RadioButton5", eFormDetailList, "2")?"check":"none"%>'></span>    
            </td>
            <td align="center" >
                <span class='<%= CommonFunc.GetRadioStatus("RadioButton5", eFormDetailList, "3")?"check":"none"%>'></span>    
            </td>
            <td align="center" >
                <span class='<%= CommonFunc.GetRadioStatus("RadioButton5", eFormDetailList, "4")?"check":"none"%>'></span>    
            </td>
            <td align="center" >
                <span class='<%= CommonFunc.GetRadioStatus("RadioButton5", eFormDetailList, "5")?"check":"none"%>'></span>    
            </td>
            <td align="center" >
                <span class='<%= CommonFunc.GetRadioStatus("RadioButton5", eFormDetailList, "6")?"check":"none"%>'></span>    
            </td>
            <td align="center" >
                <span class='<%= CommonFunc.GetRadioStatus("RadioButton5", eFormDetailList, "7")?"check":"none"%>'></span>    
            </td>
            <td align="center" >
                <span class='<%= CommonFunc.GetRadioStatus("RadioButton5", eFormDetailList, "8")?"check":"none"%>'></span>    
            </td>
            <td align="left" >
                <%=Html.Encode(CommonFunc.GetTextBoxValue("TextArea5", eFormDetailList))%>
            </td>
        </tr>     
        <tr>
            <td align="left" >
                Interview
            </td>
            <td align="center" >            
                 <span class='<%= CommonFunc.GetRadioStatus("RadioButton6", eFormDetailList, "1")?"check":"none"%>'></span>    
            </td>
            <td align="center" >
                <span class='<%= CommonFunc.GetRadioStatus("RadioButton6", eFormDetailList, "2")?"check":"none"%>'></span>    
            </td>
            <td align="center" >
                <span class='<%= CommonFunc.GetRadioStatus("RadioButton6", eFormDetailList, "3")?"check":"none"%>'></span>    
            </td>
            <td align="center" >
                <span class='<%= CommonFunc.GetRadioStatus("RadioButton6", eFormDetailList, "4")?"check":"none"%>'></span>    
            </td>
            <td align="center" >
                <span class='<%= CommonFunc.GetRadioStatus("RadioButton6", eFormDetailList, "5")?"check":"none"%>'></span>    
            </td>
            <td align="center" >
                <span class='<%= CommonFunc.GetRadioStatus("RadioButton6", eFormDetailList, "6")?"check":"none"%>'></span>    
            </td>
            <td align="center" >
                <span class='<%= CommonFunc.GetRadioStatus("RadioButton6", eFormDetailList, "7")?"check":"none"%>'></span>    
            </td>
            <td align="center" >
                <span class='<%= CommonFunc.GetRadioStatus("RadioButton6", eFormDetailList, "8")?"check":"none"%>'></span>    
            </td>
            <td align="left" >
                <%=Html.Encode(CommonFunc.GetTextBoxValue("TextArea6", eFormDetailList))%>
            </td>
        </tr>     
        <tr>
            <td align="left" >
                Management Training
            </td>
            <td align="center" >            
                 <span class='<%= CommonFunc.GetRadioStatus("RadioButton7", eFormDetailList, "1")?"check":"none"%>'></span>    
            </td>
            <td align="center" >
                <span class='<%= CommonFunc.GetRadioStatus("RadioButton7", eFormDetailList, "2")?"check":"none"%>'></span>    
            </td>
            <td align="center" >
                <span class='<%= CommonFunc.GetRadioStatus("RadioButton7", eFormDetailList, "3")?"check":"none"%>'></span>    
            </td>
            <td align="center" >
                <span class='<%= CommonFunc.GetRadioStatus("RadioButton7", eFormDetailList, "4")?"check":"none"%>'></span>    
            </td>
            <td align="center" >
                <span class='<%= CommonFunc.GetRadioStatus("RadioButton7", eFormDetailList, "5")?"check":"none"%>'></span>    
            </td>
            <td align="center" >
                <span class='<%= CommonFunc.GetRadioStatus("RadioButton7", eFormDetailList, "6")?"check":"none"%>'></span>    
            </td>
            <td align="center" >
                <span class='<%= CommonFunc.GetRadioStatus("RadioButton7", eFormDetailList, "7")?"check":"none"%>'></span>    
            </td>
            <td align="center" >
                <span class='<%= CommonFunc.GetRadioStatus("RadioButton7", eFormDetailList, "8")?"check":"none"%>'></span>    
            </td>
            <td align="left" >
                <%=Html.Encode(CommonFunc.GetTextBoxValue("TextArea7", eFormDetailList))%>
            </td>
        </tr>     
        <tr>
            <td align="left" >
                Leadership
            </td>
            <td align="center" >            
                 <span class='<%= CommonFunc.GetRadioStatus("RadioButton8", eFormDetailList, "1")?"check":"none"%>'></span>    
            </td>
            <td align="center" >
                <span class='<%= CommonFunc.GetRadioStatus("RadioButton8", eFormDetailList, "2")?"check":"none"%>'></span>    
            </td>
            <td align="center" >
                <span class='<%= CommonFunc.GetRadioStatus("RadioButton8", eFormDetailList, "3")?"check":"none"%>'></span>    
            </td>
            <td align="center" >
                <span class='<%= CommonFunc.GetRadioStatus("RadioButton8", eFormDetailList, "4")?"check":"none"%>'></span>    
            </td>
            <td align="center" >
                <span class='<%= CommonFunc.GetRadioStatus("RadioButton8", eFormDetailList, "5")?"check":"none"%>'></span>    
            </td>
            <td align="center" >
                <span class='<%= CommonFunc.GetRadioStatus("RadioButton8", eFormDetailList, "6")?"check":"none"%>'></span>    
            </td>
            <td align="center" >
                <span class='<%= CommonFunc.GetRadioStatus("RadioButton8", eFormDetailList, "7")?"check":"none"%>'></span>    
            </td>
            <td align="center" >
                <span class='<%= CommonFunc.GetRadioStatus("RadioButton8", eFormDetailList, "8")?"check":"none"%>'></span>    
            </td>
            <td align="left" >
                <%= Html.Encode(CommonFunc.GetTextBoxValue("TextArea8", eFormDetailList)) %>
            </td>
        </tr>     
        <tr>
            <td align="left" >
                Project Management
            </td>
            <td align="center" >            
                 <span class='<%= CommonFunc.GetRadioStatus("RadioButton9", eFormDetailList, "1")?"check":"none"%>'></span>    
            </td>
            <td align="center" >
                <span class='<%= CommonFunc.GetRadioStatus("RadioButton9", eFormDetailList, "2")?"check":"none"%>'></span>    
            </td>
            <td align="center" >
                <span class='<%= CommonFunc.GetRadioStatus("RadioButton9", eFormDetailList, "3")?"check":"none"%>'></span>    
            </td>
            <td align="center" >
                <span class='<%= CommonFunc.GetRadioStatus("RadioButton9", eFormDetailList, "4")?"check":"none"%>'></span>    
            </td>
            <td align="center" >
                <span class='<%= CommonFunc.GetRadioStatus("RadioButton9", eFormDetailList, "5")?"check":"none"%>'></span>    
            </td>
            <td align="center" >
                <span class='<%= CommonFunc.GetRadioStatus("RadioButton9", eFormDetailList, "6")?"check":"none"%>'></span>    
            </td>
            <td align="center" >
                <span class='<%= CommonFunc.GetRadioStatus("RadioButton9", eFormDetailList, "7")?"check":"none"%>'></span>    
            </td>
            <td align="center" >
                <span class='<%= CommonFunc.GetRadioStatus("RadioButton9", eFormDetailList, "8")?"check":"none"%>'></span>    
            </td>
            <td align="left" >
                <%= Html.Encode(CommonFunc.GetTextBoxValue("TextArea9", eFormDetailList))%>
            </td>
        </tr>     
    </table>    
    <br />
    <table width="100%" border="0" cellpadding="0" cellspacing="0" class="grid" >
        <tr>
            <th align=left class="gray">
            Comments from reviewer(s)
            </th>
        </tr>
        <tr>
            <td>
            <strong>Achievements: </strong>
            <br />
                <%=CommonFunc.GetTextBoxValue("TextBox10", eFormDetailList)%>                    
            </td>
        </tr>
        <tr>
            <td>
            <strong>Need Improvements:</strong>
            <br />
                <%=CommonFunc.GetTextBoxValue("TextBox11", eFormDetailList)%>                     
            </td>
        </tr>
        <tr>
            <td>
            <strong>Conclusion:</strong>
            <br />
                <%=Html.Encode(CommonFunc.GetTextBoxValue("TextArea12", eFormDetailList))%>
            </td>
        </tr>
    </table>
    <br />
    <table width="100%" border="0" cellpadding="0" cellspacing="0" class="grid" >
        <tr>
            <th align=left class="gray">
            Comments from HR
            </th>
        </tr>
        <tr>
            <td>
                <%=Html.Encode(CommonFunc.GetTextBoxValue("TextArea13", eFormDetailList))%>                
            </td>
        </tr>
    </table>
    <br />
    <table width="100%" border="0" cellpadding="0" cellspacing="0" class="grid" >
        <tr>
            <th align=left class="gray">
            Objectives for next period
            </th>
        </tr>
        <tr>
            <td>
            <strong>Next title: </strong>
            <br />
                <%=Html.Encode(CommonFunc.GetTextBoxValue("TextArea14", eFormDetailList))%>                
                
            </td>
        </tr>
        <tr>
            <td>
            <strong>term objectives</strong>  (12 months or 6 month for first review): 
            <br />                
                 <%=Html.Encode(CommonFunc.GetTextBoxValue("TextArea15", eFormDetailList))%>  
            </td>
        </tr>
        <tr>
            <td>
            <strong>Short term objectives</strong> (for regular Performance Review and broken down from the long term objectives):            
            <br />
                <%=Html.Encode(CommonFunc.GetTextBoxValue("TextArea16", eFormDetailList))%>  
                
            </td>
        </tr>
         <tr>
            <td>
            <strong>Next review date for short term objectives: </strong>
            <br />
                <%=Html.Encode(CommonFunc.GetTextBoxValue("TextArea17", eFormDetailList))%>                  
            </td>
        </tr>
    </table>    
    <br />
    <table width="100%" border="0" cellpadding="0" cellspacing="0" class="grid" >
        <tr>
            <th align=left class="gray" colspan=5>
            Performance Rating:
            </th>
        </tr>
        <tr>            
            <td style="width:20%">
                <span class='<%= CommonFunc.GetRadioStatus("RadioButton11", eFormDetailList, "1")?"check":"none"%>'>     
                Poor
                </span>          
                
            </td>
            <td  style="width:20%">
                <span class='<%= CommonFunc.GetRadioStatus("RadioButton11", eFormDetailList, "2")?"check":"none"%>'>     
                Fair
                </span>                          
            </td>            
            <td style="width:20%">
                <span class='<%= CommonFunc.GetRadioStatus("RadioButton11", eFormDetailList, "3")?"check":"none"%>'>     
                Good
                </span>          
                
            </td>            
            <td style="width:20%">
                <span class='<%= CommonFunc.GetRadioStatus("RadioButton11", eFormDetailList, "4")?"check":"none"%>'>     
                Excellent
                </span>          
                
            </td>            
            <td style="width:20%">
                <span class='<%= CommonFunc.GetRadioStatus("RadioButton11", eFormDetailList, "5")?"check":"none"%>'>     
                Exceptional
                </span>          
                
            </td>            
        </tr>
    </table>
    <br />
    <table width="100%" border="0" cellpadding="0" cellspacing="0" class="grid" >
        <tr>
            <th align=left class="gray">
            Manager comment and proposal	
            </th>
        </tr>
        <tr>
            <td>
                
                <%=Html.Encode(CommonFunc.GetTextBoxValue("TextBox31", eFormDetailList))%>
            </td>
        </tr>
    </table>