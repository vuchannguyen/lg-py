<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<%
    List<EForm_Detail> eFormDetailList = ViewData["detail_list"] == null ? null : (List<EForm_Detail>)ViewData["detail_list"];
    
 %>


<%=Html.Hidden("Hidden1", ViewData["eform"]==null? "" : ViewData["eform"].ToString())%>    
    <table id="tblComment" width="100%" border="0" cellpadding="0" cellspacing="0" class="grid" >
           <tr>
                <th align=left  class="gray" colspan=2>
                       Reason of Promotion & New Responsibilities:
                </th>                
            </tr>
           <tr>                
                <td  colspan=2>
                    <%=CommonFunc.GetTextBoxValue("TextBox1", eFormDetailList)%>                        
                </td>
           </tr>                      
           <tr>  
                <td style="width:45%">
                    Acknowledge by: &nbsp;                    
                    <%=CommonFunc.GetTextBoxValue("TextBox2", eFormDetailList)%>    
                </td>
                <td style="width:60%">
                    Date: &nbsp; 
                    
                    <%=CommonFunc.GetTextBoxValue("TextBox3", eFormDetailList)%>
                    
                </td>
           </tr>              
    </table>
    <br />
    