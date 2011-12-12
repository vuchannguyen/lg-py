<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>

<% List<EForm_Detail> eFormDetailList = ViewData["detail_list"] == null ? null : (List<EForm_Detail>)ViewData["detail_list"];%>
<script type="text/javascript">
   
    function RemoveChecked(radioname) {
        $("input[name='" + radioname + "']").attr('checked', false);
    }

    $(document).ready(function () {
        $("#TextBox14").datepicker();
        $("#TextBox15").datepicker();
        $("#TextBox18").datepicker();
        $("a.linkRemove").click(function () {
            id = $(this).attr("id").substring(9);
            RemoveChecked('RadioButton' + id);
        });
        jQuery.validator.addMethod(
                    "validateChecked", function (value, element, parameter) {
                        var result = false;
                        $("input[name='" + parameter[0] + "']").each(function () {
                            if ($(this).is(":checked") == true || $(this).is(":checked") == 'true') {
                                result = true;
                            }
                        });
                        if ((result == true && $.trim(value) != "") || (result == false && $.trim(value) == "")) {
                            return true;
                        }
                        else {
                            return false;
                        }
                    }, 'Please enter the value and select an option.');
        $("#editForm").validate({
            debug: false,
            errorElement: "span",
            errorPlacement: function (error, element) {
                error.tooltip({
                    bodyHandler: function () {
                        return error.html();
                    }
                });
                error.insertAfter(element);
            },
            rules: {

                TextBox1: { validateChecked: ["RadioButton1"] },
                TextBox2: { validateChecked: ["RadioButton2"] },
                TextBox3: { validateChecked: ["RadioButton3"] },
                TextBox4: { validateChecked: ["RadioButton4"] },
                TextBox5: { validateChecked: ["RadioButton5"] },
                TextBox6: { validateChecked: ["RadioButton6"] },
                TextBox7: { validateChecked: ["RadioButton7"] },
                TextBox8: { validateChecked: ["RadioButton8"] },
                TextBox12: { required: true },
                TextBox13: { required: true },
                TextBox14: { required: true, checkDate: true },
                TextBox15: { required: true, checkDate: true },
                TextBox16: { required: true },
                TextBox18: { required: true, checkDate: true },
                TextBox19: { required: true },
                TextBox20: { required: true },
                TextBox21: { required: true }
            }
        });
    });
</script>
<%=Html.Hidden("Hidden1",ViewData["eform"]==null?"":ViewData["eform"].ToString())%>
    <h2 class="heading">Interview Evaluation:</h2>
    <table cellspacing="0" cellpadding="0" border="0" class="grid">
        <tr>
            <th align="center" style="width:300px">
                Category
            </th>
            <th align="center">
                1
            </th>
            <th align="center">
                2
            </th>
            <th align="center">
                3
            </th>
            <th align="center">
                4
            </th>
            <th align="center">
                5
            </th>
            <th>&nbsp;</th>
            <th align="center" style="width:300px">
                Category
            </th>
            <th align="center">
                1
            </th>
            <th align="center">
                2
            </th>
            <th align="center">
                3
            </th>
            <th align="center">
                4
            </th>
            <th align="center">
                5
            </th>
            <th>&nbsp;</th>
        </tr>
        <tr>
            <td align="center" colspan="14">
                <strong style="font-size: 10pt">3<sup>rd</sup> Round-Final interview: </strong>
                (Please make a list of skill for the evaluation)-(For MOM/BOD)
            </td>
        </tr>
        <tr>
            <td class="title">
                1. <%=Html.TextBox("TextBox1", CommonFunc.GetTextBoxValue("TextBox1", eFormDetailList), new { @style = "width:180px", maxlength = "50" })%> 
             
            </td>
            <td align="center">
                <%=Html.RadioButton("RadioButton1", "radio1",CommonFunc.GetRadioStatus("RadioButton1",eFormDetailList,"radio1"))%>
            </td>
            <td align="center">
                <%=Html.RadioButton("RadioButton1", "radio2", CommonFunc.GetRadioStatus("RadioButton1", eFormDetailList, "radio2"))%>
            </td>
            <td align="center">
                <%=Html.RadioButton("RadioButton1", "radio3", CommonFunc.GetRadioStatus("RadioButton1", eFormDetailList, "radio3"))%>
            </td>
            <td align="center">
                <%=Html.RadioButton("RadioButton1", "radio4", CommonFunc.GetRadioStatus("RadioButton1", eFormDetailList, "radio4"))%>
            </td>
            <td align="center">
                <%=Html.RadioButton("RadioButton1", "radio5", CommonFunc.GetRadioStatus("RadioButton1", eFormDetailList, "radio5"))%>
            </td>
            <td align="center">
                <a  class="linkRemove" href="javascript:void(0);" id="lnkRemove1" title="Remove Checked"">
                        <img alt="Remove Checked" src="/Content/Images/ExtraIcons/broom__minus.png" />
                    </a>
            </td>
            <td class="title">
                5. <%=Html.TextBox("TextBox2", CommonFunc.GetTextBoxValue("TextBox2", eFormDetailList), new { @style = "width:180px", maxlength = "50" })%> 
                              
            </td>
            <td align="center">
                <%=Html.RadioButton("RadioButton2", "radio1", CommonFunc.GetRadioStatus("RadioButton2", eFormDetailList, "radio1"))%>
            </td>
            <td align="center">
                <%=Html.RadioButton("RadioButton2", "radio2", CommonFunc.GetRadioStatus("RadioButton2", eFormDetailList, "radio2"))%>
            </td>
            <td align="center">
                <%=Html.RadioButton("RadioButton2", "radio3", CommonFunc.GetRadioStatus("RadioButton2", eFormDetailList, "radio3"))%>
            </td>
            <td align="center">
                <%=Html.RadioButton("RadioButton2", "radio4", CommonFunc.GetRadioStatus("RadioButton2", eFormDetailList, "radio4"))%>
            </td>
            <td align="center">
                <%=Html.RadioButton("RadioButton2", "radio5", CommonFunc.GetRadioStatus("RadioButton2", eFormDetailList, "radio5"))%>
            </td>
            <td align="center">
                <a  class="linkRemove" href="javascript:void(0);" id="lnkRemove2" title="Remove Checked"">
                        <img alt="Remove Checked" src="/Content/Images/ExtraIcons/broom__minus.png" />
                    </a>
            </td>
        </tr>
        <tr>
            <td class="title">
                2. <%=Html.TextBox("TextBox3", CommonFunc.GetTextBoxValue("TextBox3", eFormDetailList), new { @style = "width:180px", maxlength = "50" })%>
                                
            </td>
            <td align="center">
                <%=Html.RadioButton("RadioButton3", "radio1", CommonFunc.GetRadioStatus("RadioButton3", eFormDetailList, "radio1"))%>
            </td>
            <td align="center">
                <%=Html.RadioButton("RadioButton3", "radio2", CommonFunc.GetRadioStatus("RadioButton3", eFormDetailList, "radio2"))%>
            </td>
            <td align="center">
                <%=Html.RadioButton("RadioButton3", "radio3", CommonFunc.GetRadioStatus("RadioButton3", eFormDetailList, "radio3"))%>
            </td>
            <td align="center">
                <%=Html.RadioButton("RadioButton3", "radio4", CommonFunc.GetRadioStatus("RadioButton3", eFormDetailList, "radio4"))%>
            </td>
            <td align="center">
                <%=Html.RadioButton("RadioButton3", "radio5", CommonFunc.GetRadioStatus("RadioButton3", eFormDetailList, "radio5"))%>
            </td>
            <td align="center">
              <a  class="linkRemove" href="javascript:void(0);" id="lnkRemove3" title="Remove Checked"">
                        <img alt="Remove Checked" src="/Content/Images/ExtraIcons/broom__minus.png" />
              </a>
            </td>
            <td class="title">
                6. <%=Html.TextBox("TextBox4", CommonFunc.GetTextBoxValue("TextBox4", eFormDetailList), new { @style = "width:180px", maxlength = "50" })%>
         
            </td>
            <td align="center">
                <%=Html.RadioButton("RadioButton4", "radio1", CommonFunc.GetRadioStatus("RadioButton4", eFormDetailList, "radio1"))%>
            </td>
            <td align="center">
                <%=Html.RadioButton("RadioButton4", "radio2", CommonFunc.GetRadioStatus("RadioButton4", eFormDetailList, "radio2"))%>
            </td>
            <td align="center">
                <%=Html.RadioButton("RadioButton4", "radio3", CommonFunc.GetRadioStatus("RadioButton4", eFormDetailList, "radio3"))%>
            </td>
            <td align="center">
                <%=Html.RadioButton("RadioButton4", "radio4", CommonFunc.GetRadioStatus("RadioButton4", eFormDetailList, "radio4"))%>
            </td>
            <td align="center">
                <%=Html.RadioButton("RadioButton4", "radio5", CommonFunc.GetRadioStatus("RadioButton4", eFormDetailList, "radio5"))%>
            </td>
            <td align="center">
              <a  class="linkRemove" href="javascript:void(0);" id="lnkRemove4" title="Remove Checked"">
                        <img alt="Remove Checked" src="/Content/Images/ExtraIcons/broom__minus.png" />
              </a>
            </td>
        </tr>
        <tr>
            <td class="title">
                3. <%=Html.TextBox("TextBox5", CommonFunc.GetTextBoxValue("TextBox5", eFormDetailList), new { @style = "width:180px", maxlength = "50" })%>
                
            </td>
            <td align="center">
                <%=Html.RadioButton("RadioButton5", "radio1", CommonFunc.GetRadioStatus("RadioButton5", eFormDetailList, "radio1"))%>
            </td>
            <td align="center">
                <%=Html.RadioButton("RadioButton5", "radio2", CommonFunc.GetRadioStatus("RadioButton5", eFormDetailList, "radio2"))%>
            </td>
            <td align="center">
                <%=Html.RadioButton("RadioButton5", "radio3", CommonFunc.GetRadioStatus("RadioButton5", eFormDetailList, "radio3"))%>
            </td>
            <td align="center">
                <%=Html.RadioButton("RadioButton5", "radio4", CommonFunc.GetRadioStatus("RadioButton5", eFormDetailList, "radio4"))%>
            </td>
            <td align="center">
                <%=Html.RadioButton("RadioButton5","radio5",  CommonFunc.GetRadioStatus("RadioButton5", eFormDetailList, "radio5"))%>
            </td>
             <td align="center">
              <a  class="linkRemove" href="javascript:void(0);" id="lnkRemove5" title="Remove Checked"">
                        <img alt="Remove Checked" src="/Content/Images/ExtraIcons/broom__minus.png" />
              </a>
            </td>
            <td class="title">
                7. <%=Html.TextBox("TextBox6", CommonFunc.GetTextBoxValue("TextBox6", eFormDetailList), new { @style = "width:180px", maxlength = "50" })%> 
  
            </td>
            <td align="center">
                <%=Html.RadioButton("RadioButton6", "radio1", CommonFunc.GetRadioStatus("RadioButton6", eFormDetailList, "radio1"))%>
            </td>
            <td align="center">
                <%=Html.RadioButton("RadioButton6", "radio2", CommonFunc.GetRadioStatus("RadioButton6", eFormDetailList, "radio2"))%>
            </td>
            <td align="center">
                <%=Html.RadioButton("RadioButton6", "radio3", CommonFunc.GetRadioStatus("RadioButton6", eFormDetailList, "radio3"))%>
            </td>
            <td align="center">
                <%=Html.RadioButton("RadioButton6", "radio4", CommonFunc.GetRadioStatus("RadioButton6", eFormDetailList, "radio4"))%>
            </td>
            <td align="center">
                <%=Html.RadioButton("RadioButton6", "radio5", CommonFunc.GetRadioStatus("RadioButton6", eFormDetailList, "radio5"))%>
            </td>
             <td align="center">
              <a  class="linkRemove" href="javascript:void(0);" id="lnkRemove6" title="Remove Checked"">
                        <img alt="Remove Checked" src="/Content/Images/ExtraIcons/broom__minus.png" />
              </a>
            </td>
        </tr>
        <tr>
            <td class="title">
                4. <%=Html.TextBox("TextBox7", CommonFunc.GetTextBoxValue("TextBox7", eFormDetailList), new { @style = "width:180px", maxlength = "50" })%>
   
            </td>
            <td align="center">
                <%=Html.RadioButton("RadioButton7", "radio1", CommonFunc.GetRadioStatus("RadioButton7", eFormDetailList, "radio1"))%>
            </td>
            <td align="center">
                <%=Html.RadioButton("RadioButton7", "radio2", CommonFunc.GetRadioStatus("RadioButton7", eFormDetailList, "radio2"))%>
            </td>
            <td align="center">
                <%=Html.RadioButton("RadioButton7", "radio3", CommonFunc.GetRadioStatus("RadioButton7", eFormDetailList, "radio3"))%>
            </td>
            <td align="center">
                <%=Html.RadioButton("RadioButton7", "radio4", CommonFunc.GetRadioStatus("RadioButton7", eFormDetailList, "radio4"))%>
            </td>
            <td align="center">
                <%=Html.RadioButton("RadioButton7","radio5",CommonFunc.GetRadioStatus("RadioButton7", eFormDetailList, "radio5"))%>
            </td>
             <td align="center">
              <a  class="linkRemove" href="javascript:void(0);" id="lnkRemove7" title="Remove Checked"">
                        <img alt="Remove Checked" src="/Content/Images/ExtraIcons/broom__minus.png" />
              </a>
            </td>
            <td class="title">
                8. <%=Html.TextBox("TextBox8", CommonFunc.GetTextBoxValue("TextBox8", eFormDetailList), new { @style = "width:180px", maxlength = "50" })%> 
            
            </td>
            <td align="center">
                <%=Html.RadioButton("RadioButton8", "radio1", CommonFunc.GetRadioStatus("RadioButton8", eFormDetailList, "radio1"))%>
            </td>
            <td align="center">
                <%=Html.RadioButton("RadioButton8", "radio2", CommonFunc.GetRadioStatus("RadioButton8", eFormDetailList, "radio2"))%>
            </td>
            <td align="center">
                <%=Html.RadioButton("RadioButton8", "radio3", CommonFunc.GetRadioStatus("RadioButton8", eFormDetailList, "radio3"))%>
            </td>
            <td align="center">
                <%=Html.RadioButton("RadioButton8", "radio4", CommonFunc.GetRadioStatus("RadioButton8", eFormDetailList, "radio4"))%>
            </td>
            <td align="center">
                <%=Html.RadioButton("RadioButton8", "radio5", CommonFunc.GetRadioStatus("RadioButton8", eFormDetailList, "radio5"))%>
            </td>
                <td align="center">
              <a  class="linkRemove" href="javascript:void(0);" id="lnkRemove8" title="Remove Checked"">
                        <img alt="Remove Checked" src="/Content/Images/ExtraIcons/broom__minus.png" />
              </a>
            </td>
        </tr>
        
       
    </table>
    <h2 class="heading">Comments from Interviewer(s):</h2>
    <table width="100%" border="0" cellpadding="0" cellspacing="0" class="edit">
           <tr>
                <td class="label">
                    Strengths
                    
                </td>
                <td class="input">
                    &nbsp;<%=Html.TextArea("TextBox9", CommonFunc.GetTextBoxValue("TextBox9", eFormDetailList), new { @style = "width:99%", maxlength = "500" })%>
                </td>
            </tr>
           
            <tr>
                <td class="label">
                    
                        Weaknesses
                    
                </td>
                <td class="input">
                    <%=Html.TextArea("TextBox10", CommonFunc.GetTextBoxValue("TextBox10", eFormDetailList), new { @style = "width:99%",maxlength = "500" })%>
                </td>
            </tr>
         
            <tr>
                <td class="label">
                   Additional comments
                </td>
                <td class="input">
                    <%=Html.TextArea("TextBox11", CommonFunc.GetTextBoxValue("TextBox11", eFormDetailList), new { @style = "width:99%", maxlength = "500" })%>
                </td>
            </tr>
           
            <tr>
                <td class="label">
                      Conclusion
                </td>
                <td class="input">
                    <table id="tblComment" width="100%">
                        <tr>
                            <td>
                                <%=Html.RadioButton("RadioButton9", "radio1", true)%>
                                &nbsp;Poor = 1-2
                            </td>
                            <td>
                                <%=Html.RadioButton("RadioButton9", "radio2", CommonFunc.GetRadioStatus("RadioButton9", eFormDetailList, "radio2"))%>
                                &nbsp;Fair = 3-4
                            </td>
                            <td>
                                <%=Html.RadioButton("RadioButton9", "radio3", CommonFunc.GetRadioStatus("RadioButton9", eFormDetailList, "radio3"))%>
                                &nbsp;Satisfactory = 5-6
                            </td>
                            <td>
                                <%=Html.RadioButton("RadioButton9", "radio4", CommonFunc.GetRadioStatus("RadioButton9", eFormDetailList, "radio4"))%>
                                &nbsp;Good = 7-8
                            </td>
                            <td>
                                <%=Html.RadioButton("RadioButton9", "radio5", CommonFunc.GetRadioStatus("RadioButton9", eFormDetailList, "radio5"))%>
                                &nbsp;Excellent = 9-10
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td class="label required">
                    Proposal Offer<span>*</span>
                </td>
                <td class="input">
                  <table width="100%" border="1">
                        <tr>
                            <td style="width:50px">Salary: </td>
                            <td><%=Html.TextBox("TextBox12", CommonFunc.GetTextBoxValue("TextBox12", eFormDetailList), new { @style = "width:100px",maxlength = "20" })%> vnd</td>
                            <td style="width:50px">Position:</td>
                            <td><%=Html.TextBox("TextBox13", CommonFunc.GetTextBoxValue("TextBox13", eFormDetailList), new { @style = "width:200px",maxlength = "50" })%></td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td class="label required"> 
                      Starting Date<span>*</span>
                </td>
                <td class="input">
                    <%=Html.TextBox("TextBox14", CommonFunc.GetTextBoxValue("TextBox14", eFormDetailList), new { @style = "width:100px", maxlength = "10" })%>
                </td>
            </tr>
           
            <tr>
                <td class="label required">Date<span>*</span></td>
                <td class="input">
                    <%
                        string date = DateTime.Now.Date.ToShortDateString();
                        string insertDate=CommonFunc.GetTextBoxValue("TextBox15", eFormDetailList);
                    %>
                    <%=Html.TextBox("TextBox15", insertDate==""?date:insertDate, new { @style = "width:100px",maxlength = "10"  })%>
                </td>
            </tr>
            <tr class="last">
                <td class="label required">
                    Fullname<span>*</span>
                </td>
                <td class="input">
                    <%
                        string user = CommonFunc.GetTextBoxValue("TextBox16", eFormDetailList);
                    %>
                    <%=Html.TextBox("TextBox16", user == "" ? ViewData["user"] : user, new { @style = "width:150px",maxlength = "20"})%>
                </td>
            </tr>          
          </table>
    
    <h2 class="heading">Recruiting Decision: (Noted by HR dept)</h2>
     <table width="100%" cellpadding="0" cellspacing="0" class="edit">
            <tr>
                <td class="label">1. Title/Position </td>
                <td class="input"><%=Html.TextBox("TextBox17", CommonFunc.GetTextBoxValue("TextBox17", eFormDetailList), new { @style = "width:150px",maxlength = "20" })%></td>
            </tr>
           
            <tr>
                <td class="label required">2. Start date<span>*</span></td>
                <td class="input"><%=Html.TextBox("TextBox18", CommonFunc.GetTextBoxValue("TextBox18", eFormDetailList), new { @style = "width:150px" ,maxlength = "10" })%></td>
            </tr>
            
            <tr class="last">
                <td class="label">3. Salary</td>
                <td class="input">
                    <table width="100%">
                        <tr>
                            <td class="label required">Offered Salary<span>*</span></td>
                            <td><%=Html.TextBox("TextBox19", CommonFunc.GetTextBoxValue("TextBox19", eFormDetailList), new { @style = "width:100px", maxlength = "20" })%></td>
                            <td class="label required">Probation Salary<span>*</span></td>
                            <td><%=Html.TextBox("TextBox20", CommonFunc.GetTextBoxValue("TextBox20", eFormDetailList), new { @style = "width:100px", maxlength = "20" })%></td>
                        </tr>
                        
                        <tr>
                            <td class="label required">Allowance<span>*</span></td>
                            <td><%=Html.TextBox("TextBox21", CommonFunc.GetTextBoxValue("TextBox21", eFormDetailList), new { @style = "width:100px",maxlength = "20" })%></td>
                            <td class="label required">Other benifits<span>*</span></td>
                            <td><%=Html.TextBox("TextBox22", CommonFunc.GetTextBoxValue("TextBox22", eFormDetailList), new { @style = "width:100px", maxlength = "20" })%></td>
                        </tr>
                    </table>
                   
                </td>
                
            </tr>
           
            
        </table>
        
    
    


