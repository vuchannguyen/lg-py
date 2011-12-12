<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<%
    List<EForm_Detail> eFormDetailList = ViewData["detail_list"] == null ? null : (List<EForm_Detail>)ViewData["detail_list"];
    
 %>
<script type="text/javascript">        
    
</script>

<%=Html.Hidden("Hidden1", ViewData["eform"]==null? "" : ViewData["eform"].ToString())%>
    <table width="100%" border="0" cellpadding="0" cellspacing="0" class="grid" >
        <tr>
            <th align="center" colspan=6 class="gray">
                Xếp hạng
            </th>
        </tr>    
        <tr class="last">
            <th align="center" style="width:20%;"  class="gray">&nbsp;
            </th>
            <th align="center" style="width:16%;"  class="gray">
                (5) = Kém
            </th>
            <th align="center" style="width:16%;"  class="gray">
                (4) = Yếu
            </th>
            <th align="center" style="width:16%;" class="gray">
                (3) = Trung bình
            </th>
            <th align="center" style="width:16%;" class="gray">
                (2) = Tốt
            </th>
            <th align="center" style="width:16%;" class="gray">
                (1) = Xuất xắc
            </th>
        </tr>    
         <tr>
            <td  class="title" >
                Kiến thức nghề nghiệp
            </td>
            <td align="center" >            
                <span class='<%=CommonFunc.GetRadioStatus("RadioButton1", eFormDetailList, "radiobutton1")?"check":"none"%>'>
                </span>
            </td>
            <td align="center" >
                <span class='<%=CommonFunc.GetRadioStatus("RadioButton1", eFormDetailList, "radiobutton2")?"check":"none"%>'>
                </span>                
            </td>
            <td align="center" >
                <span class='<%=CommonFunc.GetRadioStatus("RadioButton1", eFormDetailList, "radiobutton3")?"check":"none"%>'>
                </span>                                
            </td>
            <td align="center" >
                <span class='<%=CommonFunc.GetRadioStatus("RadioButton1", eFormDetailList, "radiobutton4")?"check":"none"%>'>
                </span>                
                
            </td>
            <td align="center" >
                <span class='<%=CommonFunc.GetRadioStatus("RadioButton1", eFormDetailList, "radiobutton5")?"check":"none"%>'>
                </span>                
                
            </td>
        </tr>    
        <tr>
            <td class="title">
                Hoàn thành công việc
            </td>
            <td align="center" >
                <span class='<%=CommonFunc.GetRadioStatus("RadioButton2", eFormDetailList, "radiobutton21")?"check":"none"%>'>
                </span>                                
            </td>
            <td align="center" >
                <span class='<%=CommonFunc.GetRadioStatus("RadioButton2", eFormDetailList, "radiobutton22")?"check":"none"%>'>
                </span>                                                
            </td>
            <td align="center" >
                <span class='<%=CommonFunc.GetRadioStatus("RadioButton2", eFormDetailList, "radiobutton23")?"check":"none"%>'>
                </span>                                                
            </td>
            <td align="center" >
                <span class='<%=CommonFunc.GetRadioStatus("RadioButton2", eFormDetailList, "radiobutton24")?"check":"none"%>'>
                </span>                                                                
            </td>
            <td align="center" >
                <span class='<%=CommonFunc.GetRadioStatus("RadioButton2", eFormDetailList, "radiobutton25")?"check":"none"%>'>
                </span>                                                                
            </td>
        </tr>    
        <tr>
            <td class="title">
                Chất lượng công việc
            </td>
            <td align="center" >
                <span class='<%=CommonFunc.GetRadioStatus("RadioButton3", eFormDetailList, "radiobutton31")?"check":"none"%>'>
                </span>                   
            </td>
            <td align="center" >
                <span class='<%=CommonFunc.GetRadioStatus("RadioButton3", eFormDetailList, "radiobutton32")?"check":"none"%>'>
                </span>                                   
            </td>
            <td align="center" >
                <span class='<%=CommonFunc.GetRadioStatus("RadioButton3", eFormDetailList, "radiobutton33")?"check":"none"%>'>
                </span>                                   
            </td>
            <td align="center" >
                <span class='<%=CommonFunc.GetRadioStatus("RadioButton3", eFormDetailList, "radiobutton34")?"check":"none"%>'>
                </span>                                                   
            </td>
            <td align="center" >
                <span class='<%=CommonFunc.GetRadioStatus("RadioButton3", eFormDetailList, "radiobutton35")?"check":"none"%>'>
                </span>                                                   
            </td>
        </tr>    
        <tr>
            <td class="title">
                Sáng kiến
            </td>
            <td align="center" >
                <span class='<%=CommonFunc.GetRadioStatus("RadioButton4", eFormDetailList, "radiobutton41")?"check":"none"%>'>
                </span>                                                   
            </td>
            <td align="center" >
                <span class='<%=CommonFunc.GetRadioStatus("RadioButton4", eFormDetailList, "radiobutton42")?"check":"none"%>'>
                </span>                                   
                
            </td>
            <td align="center" >
                <span class='<%=CommonFunc.GetRadioStatus("RadioButton4", eFormDetailList, "radiobutton43")?"check":"none"%>'>
                </span>                                                   
            </td>
            <td align="center" >
                <span class='<%=CommonFunc.GetRadioStatus("RadioButton4", eFormDetailList, "radiobutton44")?"check":"none"%>'>
                </span>                                                   
                
            </td>
            <td align="center" >
                <span class='<%=CommonFunc.GetRadioStatus("RadioButton4", eFormDetailList, "radiobutton45")?"check":"none"%>'>
                </span>                                                                   
            </td>
        </tr>   
        <tr>
            <td class="title">
                Đánh giá, ra quyết định
            </td>
            <td align="center" >
                <span class='<%=CommonFunc.GetRadioStatus("RadioButton5", eFormDetailList, "radiobutton51")?"check":"none"%>'>
                </span>                                                   
                
            </td>
            <td align="center" >
                <span class='<%=CommonFunc.GetRadioStatus("RadioButton5", eFormDetailList, "radiobutton52")?"check":"none"%>'>
                </span>                  
            </td>
            <td align="center" >
                <span class='<%=CommonFunc.GetRadioStatus("RadioButton5", eFormDetailList, "radiobutton53")?"check":"none"%>'>
                </span>                  
            </td>
            <td align="center" >
                <span class='<%=CommonFunc.GetRadioStatus("RadioButton5", eFormDetailList, "radiobutton54")?"check":"none"%>'>
                </span>  
                
            </td>
            <td align="center" >
                <span class='<%=CommonFunc.GetRadioStatus("RadioButton5", eFormDetailList, "radiobutton55")?"check":"none"%>'>
                </span>                  
            </td>
        </tr>    
        <tr>
            <td class="title">
                Giải quyết vấn đề
            </td>
            <td align="center" >
                <span class='<%=CommonFunc.GetRadioStatus("RadioButton6", eFormDetailList, "radiobutton61")?"check":"none"%>'>
                </span>  
                
            </td>
            <td align="center" >
                <span class='<%=CommonFunc.GetRadioStatus("RadioButton6", eFormDetailList, "radiobutton62")?"check":"none"%>'>
                </span>  
                
            </td>
            <td align="center" >
                <span class='<%=CommonFunc.GetRadioStatus("RadioButton6", eFormDetailList, "radiobutton63")?"check":"none"%>'>
                </span>  
                
            </td>
            <td align="center" >
                <span class='<%=CommonFunc.GetRadioStatus("RadioButton6", eFormDetailList, "radiobutton64")?"check":"none"%>'>
                </span>  
                
            </td>
            <td align="center" >
                <span class='<%=CommonFunc.GetRadioStatus("RadioButton6", eFormDetailList, "radiobutton65")?"check":"none"%>'>
                </span>  
                
            </td>
        </tr>  
        <tr>
            <td class="title">
                Lập kế hoạch
            </td>
            <td align="center" >
                <span class='<%=CommonFunc.GetRadioStatus("RadioButton7", eFormDetailList, "radiobutton71")?"check":"none"%>'>
                </span>  
                
            </td>
            <td align="center" >
                <span class='<%=CommonFunc.GetRadioStatus("RadioButton7", eFormDetailList, "radiobutton72")?"check":"none"%>'>
                </span>  
                
            </td>
            <td align="center" >
                <span class='<%=CommonFunc.GetRadioStatus("RadioButton7", eFormDetailList, "radiobutton73")?"check":"none"%>'>
                </span>                  
            </td>
            <td align="center" >
                <span class='<%=CommonFunc.GetRadioStatus("RadioButton7", eFormDetailList, "radiobutton74")?"check":"none"%>'>
                </span>  
            
            </td>
            <td align="center" >
                <span class='<%=CommonFunc.GetRadioStatus("RadioButton7", eFormDetailList, "radiobutton75")?"check":"none"%>'>
                </span>  
                
            </td>
        </tr>  
        <tr>
            <td class="title">
                Kỹ năng giao tiếp
            </td>
            <td align="center" >
                <span class='<%=CommonFunc.GetRadioStatus("RadioButton8", eFormDetailList, "radiobutton81")?"check":"none"%>'>
                </span>  
                
            </td>
            <td align="center" >
                <span class='<%=CommonFunc.GetRadioStatus("RadioButton8", eFormDetailList, "radiobutton82")?"check":"none"%>'>
                </span>  
                
            </td>
            <td align="center" >
                <span class='<%=CommonFunc.GetRadioStatus("RadioButton8", eFormDetailList, "radiobutton83")?"check":"none"%>'>
                </span>  
                
            </td>
            <td align="center" >
                <span class='<%=CommonFunc.GetRadioStatus("RadioButton8", eFormDetailList, "radiobutton84")?"check":"none"%>'>
                </span>  
            
            </td>
            <td align="center" >
                <span class='<%=CommonFunc.GetRadioStatus("RadioButton8", eFormDetailList, "radiobutton85")?"check":"none"%>'>
                </span>  
            
            </td>
        </tr>  
        <tr>
            <td class="title">
                Làm việc nhóm
            </td>
            <td align="center" >
                <span class='<%=CommonFunc.GetRadioStatus("RadioButton9", eFormDetailList, "radiobutton91")?"check":"none"%>'>
                </span>  
            
            </td>
            <td align="center" >
                <span class='<%=CommonFunc.GetRadioStatus("RadioButton9", eFormDetailList, "radiobutton92")?"check":"none"%>'>
                </span>  
                
            </td>
            <td align="center" >
                <span class='<%=CommonFunc.GetRadioStatus("RadioButton9", eFormDetailList, "radiobutton93")?"check":"none"%>'>
                </span>  
                
            </td>
            <td align="center" >
                <span class='<%=CommonFunc.GetRadioStatus("RadioButton9", eFormDetailList, "radiobutton94")?"check":"none"%>'>
                </span>  
                
            </td>
            <td align="center" >
                <span class='<%=CommonFunc.GetRadioStatus("RadioButton9", eFormDetailList, "radiobutton95")?"check":"none"%>'>
                </span>  
                
            </td>
        </tr>  
        <tr>
            <td class="title">
                Thời gian làm việc
            </td>
            <td align="center" >
                <span class='<%=CommonFunc.GetRadioStatus("RadioButton10", eFormDetailList, "radiobutton101")?"check":"none"%>'>
                </span>  
                
            </td>
            <td align="center" >
            <span class='<%=CommonFunc.GetRadioStatus("RadioButton10", eFormDetailList, "radiobutton102")?"check":"none"%>'>
                </span>  
            </td>
            <td align="center" >
            <span class='<%=CommonFunc.GetRadioStatus("RadioButton10", eFormDetailList, "radiobutton103")?"check":"none"%>'>
                </span>  
            </td>
            <td align="center" >
            <span class='<%=CommonFunc.GetRadioStatus("RadioButton10", eFormDetailList, "radiobutton104")?"check":"none"%>'>
                </span>  
            </td>
            <td align="center" >
            <span class='<%=CommonFunc.GetRadioStatus("RadioButton10", eFormDetailList, "radiobutton105")?"check":"none"%>'>
                </span>  
            </td>
        </tr>  
    </table>    
    <br />
    <table width="100%" border="0" cellpadding="0" cellspacing="0" class="grid" >
        <tr>
            <th align=center class="gray">
            Ưu điểm
            </th>
        </tr>
        <tr>
            <td>
                <%=Html.Encode(CommonFunc.GetTextBoxValue("TextBox1", eFormDetailList))%>    
            </td>
        </tr>
    </table>
    <br />
    <table width="100%" border="0" cellpadding="0" cellspacing="0" class="grid" >
        <tr>
            <th align=center class="gray">
            Những công việc cần trau dồi/cải tiến:
            </th>
        </tr>
        <tr>
            <td>
                <%=Html.Encode(CommonFunc.GetTextBoxValue("TextBox2", eFormDetailList))%>    
            </td>
        </tr>
    </table>
    <br />
    <table width="100%" border="0" cellpadding="0" cellspacing="0" class="grid" >
        <tr>
            <th align=center class="gray">
            Nhận xét thêm:
            </th>
        </tr>
        <tr>
            <td>
                <%=Html.Encode(CommonFunc.GetTextBoxValue("TextBox3", eFormDetailList))%>    
            </td>
        </tr>
    </table>
    <br />
    <table width="100%" border="0" cellpadding="0" cellspacing="0" class="grid" >
        <tr>
            <th align=center class="gray">
            Mục tiêu nghề nghiệp (như đã được thoả thuận giữa nhân viên và quản lý):
            </th>
        </tr>
        <tr>
            <td>
                <%=Html.Encode(CommonFunc.GetTextBoxValue("TextBox4", eFormDetailList))%>    
            </td>
        </tr>
    </table>
    <br />
    <table width="100%" border="0" cellpadding="0" cellspacing="0" class="grid" >
        <tr>
            <th align=center class="gray" colspan=4>
            Đánh giá chung
            </th>
        </tr>
        <tr>
            
            <td style="width:25%">
                <span class='<%=CommonFunc.GetRadioStatus("RadioButton11", eFormDetailList, "radiobutton111")?"check":"none"%>'>
                Yếu</span>
                
            </td>
            <td style="width:25%">
             <span class='<%=CommonFunc.GetRadioStatus("RadioButton11", eFormDetailList, "radiobutton112")?"check":"none"%>'>
                Trung bình</span>
                
            </td>
            <td style="width:25%">
             <span class='<%=CommonFunc.GetRadioStatus("RadioButton11", eFormDetailList, "radiobutton113")?"check":"none"%>'>
                Tốt</span>
                
            </td>            
            <td style="width:25%">
             <span class='<%=CommonFunc.GetRadioStatus("RadioButton11", eFormDetailList, "radiobutton114")?"check":"none"%>'>
                Xuất xắc</span>                
            </td>            
        </tr>
    </table>