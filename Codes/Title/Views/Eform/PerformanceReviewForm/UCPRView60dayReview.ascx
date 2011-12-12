<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<%
    List<EForm_Detail> eFormDetailList = ViewData["detail_list"] == null ? null : (List<EForm_Detail>)ViewData["detail_list"];
    
 %>
<script type="text/javascript">    
    
    $(document).ready(function () {
        
        
    });
    
</script>

<%=Html.Hidden("Hidden1", ViewData["eform"]==null? "" : ViewData["eform"].ToString())%>
    <table width="100%" border="0" cellpadding="0" cellspacing="0" class="grid" >
        <tr>
            <th align="center" style="width:20%;" class="gray">
               Criteria
            </th>
            <th align="center" style="width:10%;" class="gray">
               Rating
            </th>
            <th align="center" style="width:70%;"class="gray">
               Comments
            </th>
        </tr>            
        <tr>
            <td style="width:20%;" class="title">
               Attendance 
            </td>
            <td align="center" style="width:10%;" >
               <%=Html.Encode(CommonFunc.GetTextBoxValue("TextBox1", eFormDetailList))%>    
            </td>
            <td align="center" style="width:70%;">
               <%=Html.Encode(CommonFunc.GetTextBoxValue("TextBox2", eFormDetailList))%>    
            </td>
        </tr>            
        <tr>
            <td style="width:20%;"class="title">
               Teamwork
            </td>
            <td align="center" style="width:10%;">
               <%=Html.Encode(CommonFunc.GetTextBoxValue("TextBox3", eFormDetailList))%>    
            </td>
            <td align="center" style="width:70%;">
               <%=Html.Encode(CommonFunc.GetTextBoxValue("TextBox4", eFormDetailList))%>    
            </td>
        </tr>     
        <tr>
            <td style="width:20%;" class="title">
               Leadership
            </td>
            <td align="center" style="width:10%;">
               <%=Html.Encode(CommonFunc.GetTextBoxValue("TextBox7", eFormDetailList))%>    
            </td>
            <td align="center" style="width:70%;">
               <%=Html.Encode(CommonFunc.GetTextBoxValue("TextBox8", eFormDetailList))%>    
            </td>
        </tr>     
        <tr>
            <td style="width:20%;" class="title">
               Quality of work <br />(tasks varies from different departments)
            </td>
            <td align="center" style="width:10%;">
               
            </td>
            <td align="center" style="width:70%;">
               <table width="100%" border="0" cellpadding="0" cellspacing="0" class="grid" >
                    <tr>
                        <th align="center" style="width:20%;" class="gray">
                            Tasks
                        </th>
                        <th align="center" style="width:10%;" class="gray">
                            Rating of evaluation
                        </th>            
                    </tr>    
                    <tr>
                        <td align="left" style="width:20%;">
                            1.<%= Html.Encode(CommonFunc.GetTextBoxValue("TextBox21", eFormDetailList))%>    
                        </td>
                        <td align="left" style="width:10%;">
                            <%= Html.Encode(CommonFunc.GetTextBoxValue("TextBox22", eFormDetailList))%>    
                        </td>            
                    </tr>    
                    <tr>
                        <td align="left" style="width:20%;">
                            2.<%=Html.Encode(CommonFunc.GetTextBoxValue("TextBox23", eFormDetailList))%>    
                        </td>
                        <td align="left" style="width:10%;">
                            <%=Html.Encode(CommonFunc.GetTextBoxValue("TextBox24", eFormDetailList))%>    
                        </td>            
                    </tr>    
                    <tr>
                        <td align="left" style="width:20%;">
                            3.<%=Html.Encode(CommonFunc.GetTextBoxValue("TextBox25", eFormDetailList))%>    
                        </td>
                        <td align="left" style="width:10%;">
                            <%=Html.Encode(CommonFunc.GetTextBoxValue("TextBox26", eFormDetailList))%>    
                        </td>            
                    </tr>    
                    <tr>
                        <td align="left" style="width:20%;">
                            4.<%=Html.Encode(CommonFunc.GetTextBoxValue("TextBox27", eFormDetailList))%>    
                        </td>
                        <td align="left" style="width:10%;">
                            <%=Html.Encode(CommonFunc.GetTextBoxValue("TextBox28", eFormDetailList))%>    
                        </td>            
                    </tr>    
                    <tr>
                        <td align="left" style="width:20%;">
                            5.<%=Html.Encode(CommonFunc.GetTextBoxValue("TextBox29", eFormDetailList))%>    
                        </td>
                        <td align="left" style="width:10%;">
                            <%=Html.Encode(CommonFunc.GetTextBoxValue("TextBox210", eFormDetailList))%>    
                        </td>            
                    </tr>    
                    <tr>
                        <td align="left" style="width:20%;">
                            6.<%=Html.Encode(CommonFunc.GetTextBoxValue("TextBox211", eFormDetailList))%>    
                        </td>
                        <td align="left" style="width:10%;">
                            <%=Html.Encode(CommonFunc.GetTextBoxValue("TextBox212", eFormDetailList))%>    
                        </td>            
                    </tr>    
                    <tr>
                        <td align="left" style="width:20%;">
                            7.<%=Html.Encode(CommonFunc.GetTextBoxValue("TextBox213", eFormDetailList))%>    
                        </td>
                        <td align="left" style="width:10%;">
                            <%=Html.Encode(CommonFunc.GetTextBoxValue("TextBox214", eFormDetailList))%>    
                        </td>            
                    </tr>    
                    <tr>
                        <td align="left" style="width:20%;">
                            8.<%=Html.Encode(CommonFunc.GetTextBoxValue("TextBox215", eFormDetailList))%>    
                        </td>
                        <td align="left" style="width:10%;">
                            <%=Html.Encode(CommonFunc.GetTextBoxValue("TextBox216", eFormDetailList))%>    
                        </td>            
                    </tr>    
                    <tr>
                        <td align="left" style="width:20%;">
                            9.<%=Html.Encode(CommonFunc.GetTextBoxValue("TextBox217", eFormDetailList))%>    
                        </td>
                        <td align="left" style="width:10%;">
                            <%=Html.Encode(CommonFunc.GetTextBoxValue("TextBox218", eFormDetailList))%>    
                        </td>            
                    </tr>    
                    <tr>
                        <td align="left" style="width:20%;">
                            10.<%=Html.Encode(CommonFunc.GetTextBoxValue("TextBox219", eFormDetailList))%>    
                        </td>
                        <td align="left" style="width:10%;">
                            <%=Html.Encode(CommonFunc.GetTextBoxValue("TextBox220", eFormDetailList))%>    
                        </td>            
                    </tr>    
                    <tr>
                        <td align="left" colspan=2>
                            <strong> Conclusion: &nbsp;</strong>
                            <%=Html.Encode(CommonFunc.GetTextBoxValue("TextBox221", eFormDetailList))%>    
                        </td>                        
                    </tr>    
                </table>
            </td>
        </tr>    
        <tr>
            <td style="width:20%;" class="title">
               Quantity of work
            </td>
            <td align="center" style="width:10%;">
               <%=Html.Encode(CommonFunc.GetTextBoxValue("TextBox9", eFormDetailList))%>    
            </td>
            <td align="center" style="width:70%;">
               <%=Html.Encode(CommonFunc.GetTextBoxValue("TextBox10", eFormDetailList))%>    
            </td>
        </tr>      
        <tr>
            <td class="title">
               Ability to learn job
            </td>
            <td align="center" style="width:10%;">
               <%=Html.Encode(CommonFunc.GetTextBoxValue("TextBox110", eFormDetailList))%>    
            </td>
            <td align="center" style="width:70%;">
               <%=Html.Encode(CommonFunc.GetTextBoxValue("TextBox111", eFormDetailList))%>    
            </td>
        </tr>      
        <tr>
            <td class="title">
               Ability to improve job
            </td>
            <td align="center" style="width:10%;">
               <%=Html.Encode(CommonFunc.GetTextBoxValue("TextBox112", eFormDetailList))%>    
            </td>
            <td align="center" style="width:70%;">
               <%=Html.Encode(CommonFunc.GetTextBoxValue("TextBox113", eFormDetailList))%>    
            </td>
        </tr>      
        <tr>
            <td class="title">
               Work ethic
            </td>
            <td align="center" style="width:10%;">
               <%=Html.Encode(CommonFunc.GetTextBoxValue("TextBox114", eFormDetailList))%>    
            </td>
            <td align="center" style="width:70%;">
               <%=Html.Encode(CommonFunc.GetTextBoxValue("TextBox115", eFormDetailList))%>    
            </td>
        </tr>      
        <tr>
            <td class="title">
               Initiative
            </td>
            <td align="center" style="width:10%;">
               <%=Html.Encode(CommonFunc.GetTextBoxValue("TextBox116", eFormDetailList))%>    
            </td>
            <td align="center" style="width:70%;">
               <%=Html.Encode(CommonFunc.GetTextBoxValue("TextBox117", eFormDetailList))%>    
            </td>
        </tr>      
        <tr>
            <td class="title">
               Dependability
            </td>
            <td align="center" style="width:10%;">
               <%=Html.Encode(CommonFunc.GetTextBoxValue("TextBox118", eFormDetailList))%>    
            </td>
            <td align="center" style="width:70%;">
               <%=Html.Encode(CommonFunc.GetTextBoxValue("TextBox119", eFormDetailList))%>    
            </td>
        </tr>      
        <tr>
            <td class="title">
               Communication skill
            </td>
            <td align="center" style="width:10%;">
               <%=Html.Encode(CommonFunc.GetTextBoxValue("TextBox120", eFormDetailList))%>    
            </td>
            <td align="center" style="width:70%;">
               <%=Html.Encode(CommonFunc.GetTextBoxValue("TextBox121", eFormDetailList))%>    
            </td>
        </tr>   
        <tr>
            <td class="title">
               English skill
            </td>
            <td align="center" style="width:10%;">
               <%=Html.Encode(CommonFunc.GetTextBoxValue("TextBox122", eFormDetailList))%>    
            </td>
            <td align="center" style="width:70%;">
               <%=Html.Encode(CommonFunc.GetTextBoxValue("TextBox123", eFormDetailList))%>    
            </td>
        </tr>       
        <tr>
            <td class="title">
               Work knowledge
            </td>
            <td align="center" style="width:10%;">
               <%=Html.Encode(CommonFunc.GetTextBoxValue("TextBox124", eFormDetailList))%>    
            </td>
            <td align="center" style="width:70%;">
               <%=Html.Encode(CommonFunc.GetTextBoxValue("TextBox125", eFormDetailList))%>    
            </td>
        </tr>        
        <tr>
            <td class="title">
               Areas of strength
            </td>
            <td align="center" style="width:10%;">
               <%=Html.Encode(CommonFunc.GetTextBoxValue("TextBox126", eFormDetailList))%>    
            </td>
            <td align="center" style="width:70%;">
               <%=Html.Encode(CommonFunc.GetTextBoxValue("TextBox127", eFormDetailList))%>    
            </td>
        </tr>    
        <tr>
            <td class="title">
               Areas for development
            </td>
            <td align="center" style="width:10%;">
               <%=Html.Encode(CommonFunc.GetTextBoxValue("TextBox128", eFormDetailList))%>    
            </td>
            <td align="center" style="width:70%;">
               <%=Html.Encode(CommonFunc.GetTextBoxValue("TextBox129", eFormDetailList))%>    
            </td>
        </tr>    
        <tr>
            <td class="title">
               Other comments
            </td>
            <td align="center" style="width:10%;">
               <%=Html.Encode(CommonFunc.GetTextBoxValue("TextBox130", eFormDetailList))%>    
            </td>
            <td align="center" style="width:70%;">
               <%=Html.Encode(CommonFunc.GetTextBoxValue("TextBox131", eFormDetailList))%>    
            </td>
        </tr>  
    </table>    

    <br />
    <table width="100%" border="0" cellpadding="0" cellspacing="0" class="grid" >
        <tr>
            <th align=left class="gray">
            1.	Result of the probationary period: (Noted by Direct Manager)
            </th>
        </tr>
        <tr>
            <td>
                <strong>• Rating</strong> <br />
                <table width="100%" border="0" cellpadding="0" cellspacing="0">                
                <tr>            
                    <td style="border:none">
                        &nbsp;&nbsp;
                        <span class='<%= CommonFunc.GetRadioStatus("RadioButton11", eFormDetailList, "1")?"check":"none"%>'>
                                    Poor = 1-2</span>
                    </td>
                    <td style="border:none">
                        <span class='<%= CommonFunc.GetRadioStatus("RadioButton11", eFormDetailList, "2")?"check":"none"%>'>
                                    Fair = 3-4</span>
                        
                    </td>
                    <td style="border:none">
                        <span class='<%= CommonFunc.GetRadioStatus("RadioButton11", eFormDetailList, "3")?"check":"none"%>'>
                                    Satisfactory = 5-6</span>
                        
                    </td>            
                    <td style="border:none">
                        <span class='<%= CommonFunc.GetRadioStatus("RadioButton11", eFormDetailList, "4")?"check":"none"%>'>
                                    Good = 7-8</span>
                        
                    </td>            
                    <td style="border:none">
                        <span class='<%= CommonFunc.GetRadioStatus("RadioButton11", eFormDetailList, "5")?"check":"none"%>'>
                                    Excellent = 9-10</span>
                        
                    </td>            
                </tr>
            </table>
                
            </td>
        </tr>
        <tr>
            <td>
                <strong>• Development Plans:</strong> <br />
                <%=Html.Encode(CommonFunc.GetTextBoxValue("TextBox132", eFormDetailList))%>    
            </td>
        </tr>
    </table>
    <br />
    <table width="100%" border="0" cellpadding="0" cellspacing="0" class="grid" >
        <tr>
            <th align=left class="gray">
            2.	Employee comments:
            </th>
        </tr>
        <tr>
            <td >
                <%=Html.Encode(CommonFunc.GetTextBoxValue("TextBox133", eFormDetailList))%>    
                <br />
                Date: &nbsp;
                <%
                        string date = DateTime.Now.Date.ToShortDateString();
                        string insertDate = CommonFunc.GetTextBoxValue("TextBox134", eFormDetailList);
                    %>
                    <%=insertDate%>                
            </td>
        </tr>
    </table>
    <br />
    <table width="100%" border="0" cellpadding="0" cellspacing="0" class="grid" >
        <tr>
            <th align=left class="gray">
            3.	Direct Manager comments:
            </th>
        </tr>
        <tr>
            <td>
                <%=Html.Encode(CommonFunc.GetTextBoxValue("TextBox135", eFormDetailList))%>    
                <br />
                Date: &nbsp;
                <%=CommonFunc.GetTextBoxValue("TextBox136", eFormDetailList)%>    
            </td>
        </tr>
    </table>
    <br />
    <table width="100%" border="0" cellpadding="0" cellspacing="0" class="grid" >
        <tr>
            <th align=left class="gray">
            4.	Department Head comments:
            </th>
        </tr>
        <tr>
            <td>
                <%= Html.Encode(CommonFunc.GetTextBoxValue("TextBox137", eFormDetailList))%>    
                <br />
                Date: &nbsp;
                <%= CommonFunc.GetTextBoxValue("TextBox138", eFormDetailList)%>    
                <br />
                <table width="100%" border="0" cellpadding="0" cellspacing="0">
                    <tr>
                        <td style="border:none;width:20%";">
                            &nbsp;&nbsp;
                            <span class='<%=CommonFunc.GetRadioStatus("RadioButton12", eFormDetailList, "1")?"check":"none"%>'>
                                    Fail</span>                            
                        </td>
                        <td style="border:none;width:20%";"">
                            <span class='<%=CommonFunc.GetRadioStatus("RadioButton12", eFormDetailList, "2")?"check":"none"%>'>
                                    signed labor contract</span>
                            
                        </td>
                        <td style="border:none;width:20%";"">
                            <span class='<%=CommonFunc.GetRadioStatus("RadioButton12", eFormDetailList, "3")?"check":"none"%>'>
                                    extend</span>                            
                            <%=CommonFunc.GetTextBoxValue("TextBox139", eFormDetailList)%>    
                            month(s).
                        </td>
                        
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <br />
    <table width="100%" border="0" cellpadding="0" cellspacing="0" class="grid" >
        <tr>
            <th align=left class="gray">
            5.	HR comments:
            </th>
        </tr>
        <tr>            
            <td>
                <%=Html.Encode(CommonFunc.GetTextBoxValue("TextBox140", eFormDetailList))%>    
            </td>            
        </tr>
        <tr>
            <td>
                <table width="500px" border="0" cellpadding="0" cellspacing="0" class="grid">
                    <tr>    
                        <td style="width:150px;" class="title">
                            Contract signed date:
                        </td>
                        <td>
                            <%=Html.Encode(CommonFunc.GetTextBoxValue("TextBox141", eFormDetailList))%>    
                        </td>
                    </tr>
                    <tr>    
                        <td style="width:150px;" class="title">
                            Official salary date:
                        </td>
                        <td>
                            <%=Html.Encode(CommonFunc.GetTextBoxValue("TextBox142", eFormDetailList))%>    
                        </td>
                    </tr>
                    <tr>    
                        <td style="width:150px;" class="title">
                            Next review date:
                        </td>
                        <td>
                            <%=CommonFunc.GetTextBoxValue("TextBox143", eFormDetailList)%>    
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>            
            <td> Date: &nbsp;
                <%=CommonFunc.GetTextBoxValue("TextBox144", eFormDetailList)%>    
            </td>            
        </tr>
    </table>
