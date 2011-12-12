<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>

<%
    List<EForm_Detail> eFormDetailList = ViewData["detail_list"] == null ? null : (List<EForm_Detail>)ViewData["detail_list"];
    
 %>
<%=Html.Hidden("Hidden1", ViewData["eform"]==null? "" : ViewData["eform"].ToString())%>
    <table width="100%" border="0" cellpadding="0" cellspacing="0" class="grid" >
        <tr>
            <th align="center" class="gray" style="width:50%" >
                Pros
            </th>
            <th align="center" class="gray" style="width:50%">
                Cons
            </th>
        </tr>    
        <tr>
            <td align="left">
            <%=CommonFunc.GetTextBoxValue("TextBox1", eFormDetailList)%>   
            </td>            
            <td align="left">
            <%=CommonFunc.GetTextBoxValue("TextBox2", eFormDetailList)%>   
            </td>            
        </tr>    
        <tr>
            <th align="left"  colspan=2 class="gray">
            Objectives
            </th>            
        </tr>    
        <tr>
            <td align="left"  colspan=2 >
            <%=Html.Encode(CommonFunc.GetTextBoxValue("TextBox3", eFormDetailList))%>    
            </td>            
        </tr>
        <tr>
            <th align="left"  colspan=2 class="gray">
            Comment from reviewer(s)
            </th>            
        </tr>    
        <tr>
            <td align="left"  colspan=2>
            <%=Html.Encode(CommonFunc.GetTextBoxValue("TextBox4", eFormDetailList))%>    
            </td>            
        </tr>

    </table>
    <br />
    <table width="100%" border="0" cellpadding="0" cellspacing="0" class="grid" >
        <tr>
            <th align="left" class="gray">
                Comments from HR (if any)
            </th>
        </tr>
        <tr>
            <td>
                <%=Html.Encode(CommonFunc.GetTextBoxValue("TextBox10", eFormDetailList))%>    
            </td>
        </tr>
    </table>
    <h2 class="heading">Performance Rating:</h2>
    <table width="100%" border="0" cellpadding="0" cellspacing="0" class="edit" >
        <tr>
            <td class="input"  style="width:20%">
                <span class='<%= CommonFunc.GetRadioStatus("RadioButton1", eFormDetailList, "radiobutton1")?"check":"none"%>'>
                 Poor</span>
            </td>
            <td class="input" style="width:20%">
                <span class='<%= CommonFunc.GetRadioStatus("RadioButton1", eFormDetailList, "radiobutton2")?"check":"none"%>'>
                 Fair</span>
            </td>
            <td class="input" style="width:20%">
                <span class='<%= CommonFunc.GetRadioStatus("RadioButton1", eFormDetailList, "radiobutton3")?"check":"none"%>'>
                 Good</span>
            </td>
            <td class="input" style="width:20%">
                <span class='<%= CommonFunc.GetRadioStatus("RadioButton1", eFormDetailList, "radiobutton4")?"check":"none"%>'>
                 Excellent</span>
            </td>
            <td class="input" style="width:20%">
                <span class='<%= CommonFunc.GetRadioStatus("RadioButton1", eFormDetailList, "radiobutton5")?"check":"none"%>'>
                Exceptional</span>
            </td>
        </tr>
    </table>
    <br />
    <table id="tblComment" width="100%" border="0" cellpadding="0" cellspacing="0" class="grid" >
           <tr>
                <th align=left  colspan=2 class="gray">
                       Comments from employee
                </th>                
            </tr>
           <tr class="last">
                <td colspan=2>
                    <label>I have read all the comments above and:</label>
                </td>
           </tr>           
           <tr id="parent" class="last">
            <td style="width:300px">
                <span class='<%= CommonFunc.GetRadioStatus("RadioButton2", eFormDetailList, "radiobutton21")?"check":"none"%>'>               
                 I agree with them and wish to add the following points</span>
            </td>
            <td id="sub"  style="width:600px">
                <%= Html.Encode(CommonFunc.GetTextBoxValue("TextBox21", eFormDetailList))%>    
            </td>
           </tr>
           <tr >
            <td>
            <span class='<%= CommonFunc.GetRadioStatus("RadioButton2", eFormDetailList, "radiobutton22")?"check":"none"%>'>               
                I do not agree with them for the following reasons</span>                
            </td>
            <td style="width:100px">
                <%= Html.Encode(CommonFunc.GetTextBoxValue("TextBox22", eFormDetailList))%>    
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
                <%= Html.Encode(CommonFunc.GetTextBoxValue("TextBox31", eFormDetailList))%>    
            </td>
        </tr>
    </table>