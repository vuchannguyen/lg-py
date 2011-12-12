<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<%
    List<EForm_Detail> eFormDetailList = ViewData["detail_list"] == null ? null : (List<EForm_Detail>)ViewData["detail_list"];
    string interviewID = (string)ViewData["InterviewID"];
    Interview interview = new InterviewDao().GetById(interviewID);
 %>
<script type="text/javascript">
   function RemoveChecked(radioname) {
       $("input[name='" + radioname + "']").attr('checked', false);
   }
   $(document).ready(function () {
       $("#TextBox14").datepicker();

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
               TextBox13: { required: false },
               TextBox14: { required: true, checkDate: true },
               TextBox15: { required: true },
               TextBox1: { validateChecked: ["RadioButton12"] },
               TextBox2: { validateChecked: ["RadioButton14"] },
               TextBox3: { validateChecked: ["RadioButton16"] },
               TextBox4: { validateChecked: ["RadioButton18"] },
               TextBox5: { validateChecked: ["RadioButton20"] }
           }
       });
   });
   
</script>

<%=Html.Hidden("Hidden1",ViewData["eform"]==null?"":ViewData["eform"].ToString())%>

    <h2 class="heading">Interview Evaluation:</h2>
    <table cellspacing="0" cellpadding="0" border="0" class="grid">
        <tr>
            <th align="center">
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
                <strong style="font-size: 10pt">1<sup>st</sup> Round-General interview: </strong>(For
                HR Dept)
            </td>
        </tr>
        <tr>
            <td class="title">
            1. Appearance
                
            </td>
            <td align="center">
                <%=Html.RadioButton("RadioButton1", "radio2", CommonFunc.GetRadioStatus("RadioButton1", eFormDetailList, "radio2"))%>
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
                <a href="javascript:void(0);" id="lnkRemove1" title="Remove Checked" class="linkRemove">
                        <img alt="Remove Checked" src="/Content/Images/ExtraIcons/broom__minus.png" />
                    </a>
            </td>
            <td class="title">
                11. Personnel mangement skill
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
                    <a href="javascript:void(0);" id="lnkRemove2" title="Remove Checked" class="linkRemove">
                        <img alt="Remove Checked" src="/Content/Images/ExtraIcons/broom__minus.png" />
                    </a>
            </td>
        </tr>
        <tr>
            <td class="title">
                2. Character
                               
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
                <a  class="linkRemove" href="javascript:void(0);" id="lnkRemove3" title="Remove Checked">
                        <img alt="Remove Checked" src="/Content/Images/ExtraIcons/broom__minus.png" />
                    </a>
            </td>
            <td class="title">
                12. Project management skill
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
                <a  class="linkRemove" href="javascript:void(0);" id="lnkRemove4" title="Remove Checked">
                        <img alt="Remove Checked" src="/Content/Images/ExtraIcons/broom__minus.png" />
                </a>
            </td>
        </tr>
        <tr>
            <td class="title">
                3. Communication
                    
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
            <a  class="linkRemove" href="javascript:void(0);" id="lnkRemove5" title="Remove Checked">
                        <img alt="Remove Checked" src="/Content/Images/ExtraIcons/broom__minus.png" />
                    </a></td>
            <td class="title">
                13. Time management
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
                <a  class="linkRemove" href="javascript:void(0);" id="lnkRemove6" title="Remove Checked">
                        <img alt="Remove Checked" src="/Content/Images/ExtraIcons/broom__minus.png" />
                </a>
            </td>
        </tr>
        <tr>
            <td class="title">
                4. Dynamic
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
                <a  class="linkRemove" href="javascript:void(0);" id="lnkRemove7" title="Remove Checked">
                        <img alt="Remove Checked" src="/Content/Images/ExtraIcons/broom__minus.png" />
                    </a>
            </td>
            <td class="title">
                14. Vision
    
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
                <a  class="linkRemove" href="javascript:void(0);" id="lnkRemove8" title="Remove Checked">
                        <img alt="Remove Checked" src="/Content/Images/ExtraIcons/broom__minus.png" />
                    </a>
            </td>
        </tr>
        <tr>
            <td class="title">
                5. Creativeness/initiative
            </td>
            <td align="center">
                <%=Html.RadioButton("RadioButton9", "radio1", CommonFunc.GetRadioStatus("RadioButton9", eFormDetailList, "radio1"))%>
            </td>
            <td align="center">
                <%=Html.RadioButton("RadioButton9", "radio2", CommonFunc.GetRadioStatus("RadioButton9", eFormDetailList, "radio2"))%>
            </td>
            <td align="center">
                <%=Html.RadioButton("RadioButton9", "radio3", CommonFunc.GetRadioStatus("RadioButton9", eFormDetailList, "radio3"))%>
            </td>
            <td align="center">
                <%=Html.RadioButton("RadioButton9", "radio4", CommonFunc.GetRadioStatus("RadioButton9", eFormDetailList, "radio4"))%>
            </td>
            <td align="center">
                <%=Html.RadioButton("RadioButton9", "radio5", CommonFunc.GetRadioStatus("RadioButton9", eFormDetailList, "radio5"))%>
            </td>
            <td align="center">
                <a  class="linkRemove" href="javascript:void(0);" id="lnkRemove9" title="Remove Checked">
                        <img alt="Remove Checked" src="/Content/Images/ExtraIcons/broom__minus.png" />
                    </a>
            </td>
            <td class="title">
                15. English skill
     
            </td>
            <td align="center">
                <%=Html.RadioButton("RadioButton10", "radio1", CommonFunc.GetRadioStatus("RadioButton10", eFormDetailList, "radio1"))%>
            </td>
            <td align="center">
                <%=Html.RadioButton("RadioButton10", "radio2", CommonFunc.GetRadioStatus("RadioButton10", eFormDetailList, "radio2"))%>
            </td>
            <td align="center">
                <%=Html.RadioButton("RadioButton10", "radio3", CommonFunc.GetRadioStatus("RadioButton10", eFormDetailList, "radio3"))%>
            </td>
            <td align="center">
                <%=Html.RadioButton("RadioButton10", "radio4", CommonFunc.GetRadioStatus("RadioButton10", eFormDetailList, "radio4"))%>
            </td>
            <td align="center">
                <%=Html.RadioButton("RadioButton10", "radio5", CommonFunc.GetRadioStatus("RadioButton10", eFormDetailList, "radio5"))%>
            </td>
            <td align="center">
                <a  class="linkRemove" href="javascript:void(0);" id="lnkRemove10" title="Remove Checked">
                        <img alt="Remove Checked" src="/Content/Images/ExtraIcons/broom__minus.png" />
                    </a>
            </td>
        </tr>
        <tr >
            <td class="title">
                6. Teamwork spirit
            </td>
            <td align="center">
                <%=Html.RadioButton("RadioButton11", "radio1", CommonFunc.GetRadioStatus("RadioButton11", eFormDetailList, "radio1"))%>
            </td>
            <td align="center">
                <%=Html.RadioButton("RadioButton11", "radio2", CommonFunc.GetRadioStatus("RadioButton11", eFormDetailList, "radio2"))%>
            </td>
            <td align="center">
                <%=Html.RadioButton("RadioButton11", "radio3", CommonFunc.GetRadioStatus("RadioButton11", eFormDetailList, "radio3"))%>
            </td>
            <td align="center">
                <%=Html.RadioButton("RadioButton11", "radio4", CommonFunc.GetRadioStatus("RadioButton11", eFormDetailList, "radio4"))%>
            </td>
            <td align="center">
                <%=Html.RadioButton("RadioButton11", "radio5", CommonFunc.GetRadioStatus("RadioButton11", eFormDetailList, "radio5"))%>
            </td>
            <td align="center">
                    <a  class="linkRemove" href="javascript:void(0);" id="lnkRemove11" title="Remove Checked">
                        <img alt="Remove Checked" src="/Content/Images/ExtraIcons/broom__minus.png" />
                    </a>
            </td>
            <td class="title">
                16. <%=Html.TextBox("TextBox1", CommonFunc.GetTextBoxValue("TextBox1", eFormDetailList), new { @style = "width:150px", maxlength = "50" })%>
                    
            </td>
            <td align="center">
                <%=Html.RadioButton("RadioButton12", "radio1", CommonFunc.GetRadioStatus("RadioButton12", eFormDetailList, "radio1"))%>
            </td>
            <td align="center">
                <%=Html.RadioButton("RadioButton12", "radio2", CommonFunc.GetRadioStatus("RadioButton12", eFormDetailList, "radio2"))%>
            </td>
            <td align="center">
                <%=Html.RadioButton("RadioButton12", "radio3", CommonFunc.GetRadioStatus("RadioButton12", eFormDetailList, "radio3"))%>
            </td>
            <td align="center">
                <%=Html.RadioButton("RadioButton12", "radio4", CommonFunc.GetRadioStatus("RadioButton12", eFormDetailList, "radio4"))%>
            </td>
            <td align="center">
                <%=Html.RadioButton("RadioButton12", "radio5", CommonFunc.GetRadioStatus("RadioButton12", eFormDetailList, "radio5"))%>
            </td>
            <td align="center">
                <a  class="linkRemove" href="javascript:void(0);" id="lnkRemove12" title="Remove Checked">
                        <img alt="Remove Checked" src="/Content/Images/ExtraIcons/broom__minus.png" />
                    </a>
            </td>
        </tr>
        <tr>
            <td class="title">
                7. Interest in company
            </td>
            <td align="center">
                <%=Html.RadioButton("RadioButton13", "radio1", CommonFunc.GetRadioStatus("RadioButton13", eFormDetailList, "radio1"))%>
            </td>
            <td align="center">
                <%=Html.RadioButton("RadioButton13", "radio2", CommonFunc.GetRadioStatus("RadioButton13", eFormDetailList, "radio2"))%>
            </td>
            <td align="center">
                <%=Html.RadioButton("RadioButton13", "radio3", CommonFunc.GetRadioStatus("RadioButton13", eFormDetailList, "radio3"))%>
            </td>
            <td align="center">
                <%=Html.RadioButton("RadioButton13", "radio4", CommonFunc.GetRadioStatus("RadioButton13", eFormDetailList, "radio4"))%>
            </td>
            <td align="center">
                <%=Html.RadioButton("RadioButton13", "radio5", CommonFunc.GetRadioStatus("RadioButton13", eFormDetailList, "radio5"))%>
            </td>
            <td align="center">
                 <a  class="linkRemove" href="javascript:void(0);" id="lnkRemove13" title="Remove Checked">
                        <img alt="Remove Checked" src="/Content/Images/ExtraIcons/broom__minus.png" />
                    </a>
            </td>
            <td class="title">
                17. <%=Html.TextBox("TextBox2", CommonFunc.GetTextBoxValue("TextBox2", eFormDetailList), new { @style = "width:150px", maxlength = "50" })%>
            </td>
            <td align="center">
                <%=Html.RadioButton("RadioButton14", "radio1", CommonFunc.GetRadioStatus("RadioButton14", eFormDetailList, "radio1"))%>
            </td>
            <td align="center">
                <%=Html.RadioButton("RadioButton14", "radio2", CommonFunc.GetRadioStatus("RadioButton14", eFormDetailList, "radio2"))%>
            </td>
            <td align="center">
                <%=Html.RadioButton("RadioButton14", "radio3", CommonFunc.GetRadioStatus("RadioButton14", eFormDetailList, "radio3"))%>
            </td>
            <td align="center">
                <%=Html.RadioButton("RadioButton14", "radio4", CommonFunc.GetRadioStatus("RadioButton14", eFormDetailList, "radio4"))%>
            </td>
            <td align="center">
                <%=Html.RadioButton("RadioButton14", "radio5", CommonFunc.GetRadioStatus("RadioButton14", eFormDetailList, "radio5"))%>
            </td>
            <td align="center">
                <a  class="linkRemove" href="javascript:void(0);" id="lnkRemove14" title="Remove Checked">
                        <img alt="Remove Checked" src="/Content/Images/ExtraIcons/broom__minus.png" />
                    </a>
            </td>
        </tr>
        <tr>
            <td class="title">
                 8. Learning skill
            </td>
            <td align="center">
                <%=Html.RadioButton("RadioButton15", "radio1", CommonFunc.GetRadioStatus("RadioButton15", eFormDetailList, "radio1"))%>
            </td>
            <td align="center">
                <%=Html.RadioButton("RadioButton15", "radio2", CommonFunc.GetRadioStatus("RadioButton15", eFormDetailList, "radio2"))%>
            </td>
            <td align="center">
                <%=Html.RadioButton("RadioButton15", "radio3", CommonFunc.GetRadioStatus("RadioButton15", eFormDetailList, "radio3"))%>
            </td>
            <td align="center">
                <%=Html.RadioButton("RadioButton15", "radio4", CommonFunc.GetRadioStatus("RadioButton15", eFormDetailList, "radio4"))%>
            </td>
            <td align="center">
                <%=Html.RadioButton("RadioButton15", "radio5", CommonFunc.GetRadioStatus("RadioButton15", eFormDetailList, "radio5"))%>
            </td>
            <td align="center">
                <a  class="linkRemove" href="javascript:void(0);" id="lnkRemove15" title="Remove Checked">
                        <img alt="Remove Checked" src="/Content/Images/ExtraIcons/broom__minus.png" />
                    </a>
            </td>
            <td class="title">
                18. <%=Html.TextBox("TextBox3", CommonFunc.GetTextBoxValue("TextBox3", eFormDetailList), new { @Style = "width:150px", maxlength = "50" })%>
            </td>
            <td align="center">
                <%=Html.RadioButton("RadioButton16", "radio1", CommonFunc.GetRadioStatus("RadioButton16", eFormDetailList, "radio1"))%>
            </td>
            <td align="center">
                <%=Html.RadioButton("RadioButton16", "radio2", CommonFunc.GetRadioStatus("RadioButton16", eFormDetailList, "radio2"))%>
            </td>
            <td align="center">
                <%=Html.RadioButton("RadioButton16", "radio3", CommonFunc.GetRadioStatus("RadioButton16", eFormDetailList, "radio3"))%>
            </td>
            <td align="center">
                <%=Html.RadioButton("RadioButton16", "radio4", CommonFunc.GetRadioStatus("RadioButton16", eFormDetailList, "radio4"))%>
            </td>
            <td align="center">
                <%=Html.RadioButton("RadioButton16", "radio5", CommonFunc.GetRadioStatus("RadioButton16", eFormDetailList, "radio5"))%>
            </td>
            <td align="center">
                <a  class="linkRemove" href="javascript:void(0);" id="lnkRemove16" title="Remove Checked">
                        <img alt="Remove Checked" src="/Content/Images/ExtraIcons/broom__minus.png" />
                    </a>
            </td>
        </tr>
        <tr>
            <td class="title">
                9. Problem skill
            </td>
            <td align="center">
                <%=Html.RadioButton("RadioButton17", "radio1", CommonFunc.GetRadioStatus("RadioButton17", eFormDetailList, "radio1"))%>
            </td>
            <td align="center">
                <%=Html.RadioButton("RadioButton17", "radio2", CommonFunc.GetRadioStatus("RadioButton17", eFormDetailList, "radio2"))%>
            </td>
            <td align="center">
                <%=Html.RadioButton("RadioButton17", "radio3", CommonFunc.GetRadioStatus("RadioButton17", eFormDetailList, "radio3"))%>
            </td>
            <td align="center">
                <%=Html.RadioButton("RadioButton17", "radio4", CommonFunc.GetRadioStatus("RadioButton17", eFormDetailList, "radio4"))%>
            </td>
            <td align="center">
                <%=Html.RadioButton("RadioButton17", "radio5", CommonFunc.GetRadioStatus("RadioButton17", eFormDetailList, "radio5"))%>
            </td>
            <td align="center">
                <a  class="linkRemove" href="javascript:void(0);" id="lnkRemove17" title="Remove Checked">
                        <img alt="Remove Checked" src="/Content/Images/ExtraIcons/broom__minus.png" />
                    </a>
            </td>
            <td class="title">
                19. <%=Html.TextBox("TextBox4", CommonFunc.GetTextBoxValue("TextBox4", eFormDetailList), new { @Style = "width:150px", maxlength = "50" })%>
                
            </td>
            <td align="center">
                <%=Html.RadioButton("RadioButton18", "radio1", CommonFunc.GetRadioStatus("RadioButton18", eFormDetailList, "radio1"))%>
            </td>
            <td align="center">
                <%=Html.RadioButton("RadioButton18", "radio2", CommonFunc.GetRadioStatus("RadioButton18", eFormDetailList, "radio2"))%>
            </td>
            <td align="center">
                <%=Html.RadioButton("RadioButton18", "radio3", CommonFunc.GetRadioStatus("RadioButton18", eFormDetailList, "radio3"))%>
            </td>
            <td align="center">
                <%=Html.RadioButton("RadioButton18", "radio4", CommonFunc.GetRadioStatus("RadioButton18", eFormDetailList, "radio4"))%>
            </td>
            <td align="center">
                <%=Html.RadioButton("RadioButton18", "radio5", CommonFunc.GetRadioStatus("RadioButton18", eFormDetailList, "radio5"))%>
            </td>
            <td align="center">
                <a  class="linkRemove" href="javascript:void(0);" id="lnkRemove18" title="Remove Checked">
                        <img alt="Remove Checked" src="/Content/Images/ExtraIcons/broom__minus.png" />
                    </a>
            </td>
        </tr>
        <tr>
            <td class="title">
                10. Negotiation skill
            </td>
            <td align="center">
                <%=Html.RadioButton("RadioButton19", "radio1", CommonFunc.GetRadioStatus("RadioButton19", eFormDetailList, "radio1"))%>
            </td>
            <td align="center">
                <%=Html.RadioButton("RadioButton19", "radio2", CommonFunc.GetRadioStatus("RadioButton19", eFormDetailList, "radio2"))%>
            </td>
            <td align="center">
                <%=Html.RadioButton("RadioButton19", "radio3", CommonFunc.GetRadioStatus("RadioButton19", eFormDetailList, "radio3"))%>
            </td>
            <td align="center">
                <%=Html.RadioButton("RadioButton19", "radio4", CommonFunc.GetRadioStatus("RadioButton19", eFormDetailList, "radio4"))%>
            </td>
            <td align="center">
                <%=Html.RadioButton("RadioButton19", "radio5", CommonFunc.GetRadioStatus("RadioButton19", eFormDetailList, "radio5"))%>
            </td>
            <td align="center">
                <a  class="linkRemove" href="javascript:void(0);" id="lnkRemove19" title="Remove  Checked"">
                        <img alt="Remove Checked" src="/Content/Images/ExtraIcons/broom__minus.png" />
                    </a>
            </td>
            <td class="title">
                20. <%=Html.TextBox("TextBox5", CommonFunc.GetTextBoxValue("TextBox5", eFormDetailList), new { @Style = "width:150px", maxlength = "500" })%>
            </td>
            <td align="center">
                <%=Html.RadioButton("RadioButton20", "radio1", CommonFunc.GetRadioStatus("RadioButton20", eFormDetailList, "radio1"))%>
            </td>
            <td align="center">
                <%=Html.RadioButton("RadioButton20", "radio2", CommonFunc.GetRadioStatus("RadioButton20", eFormDetailList, "radio2"))%>
            </td>
            <td align="center">
                <%=Html.RadioButton("RadioButton20", "radio3", CommonFunc.GetRadioStatus("RadioButton20", eFormDetailList, "radio3"))%>
            </td>
            <td align="center">
                <%=Html.RadioButton("RadioButton20", "radio4", CommonFunc.GetRadioStatus("RadioButton20", eFormDetailList, "radio4"))%>
            </td>
            <td align="center">
                <%=Html.RadioButton("RadioButton20", "radio5", CommonFunc.GetRadioStatus("RadioButton20", eFormDetailList, "radio5"))%>
            </td>
            <td align="center">
                <a  class="linkRemove" href="javascript:void(0);" id="lnkRemove20" title="Remove Checked">
                        <img alt="Remove Checked" src="/Content/Images/ExtraIcons/broom__minus.png" />
                    </a>
            </td>
        </tr>
    </table>
    <h2 class="heading">Comments from Interviewer(s):</h2>
    <table width="100%" border="0" cellpadding="0" cellspacing="0" class="edit" >
            <tr>
                <td class="label">
                       Experiences
                </td>
                <td class="input">
                    <%=Html.TextArea("TextBox6", CommonFunc.GetTextBoxValue("TextBox6", eFormDetailList), new { @style = "width:99%", @maxlength = "500" })%>
                </td>
            </tr>
            <tr>
                <td class="label">
                     Education/training
                 </td>
                 <td class="input">
                    <%=Html.TextArea("TextBox7", CommonFunc.GetTextBoxValue("TextBox7", eFormDetailList), new { @style = "width:99%", @maxlength = "500" })%>
                </td>
            </tr>
            <tr>
                <td class="label">
                     Strengths
                 </td>
                <td class="input">
                    <%=Html.TextArea("TextBox8", CommonFunc.GetTextBoxValue("TextBox8", eFormDetailList), new { @style = "width:99%", maxlength = "500" })%>
                </td>
            </tr>
            <tr>
                <td class="label">
                     Weaknesses
                 </td>
                <td class="input">
                    <%=Html.TextArea("TextBox9", CommonFunc.GetTextBoxValue("TextBox9", eFormDetailList), new { @style = "width:99%", maxlength = "500" })%>
                </td>
            </tr>
            <tr>
                <td class="label">
                     Leave reasons
                 </td>
                <td class="input">
                    <%=Html.TextArea("TextBox10", CommonFunc.GetTextBoxValue("TextBox10", eFormDetailList), new { @style = "width:99%", maxlength = "500" })%>
                </td>
            </tr>
            <tr>
                <td class="label">
                     Expected salary
                    
                </td>
                <td class="input">
                    <%=Html.TextArea("TextBox11", CommonFunc.GetTextBoxValue("TextBox11", eFormDetailList), new { @style = "width:99%", maxlength = "500" })%>
                </td>
            </tr>
            <tr>
                <td class="label">
                     Additional comments
                    
                </td>
               <td class="input">
                    <%=Html.TextArea("TextBox12", CommonFunc.GetTextBoxValue("TextBox12", eFormDetailList), new { @style = "width:99%", maxlength = "500" })%>
                </td>
            </tr>
            <tr>
                <td class="label">
                     Start Date
                    
                </td>
                <td class="input">
                    <%=Html.TextBox("TextBox13", CommonFunc.GetTextBoxValue("TextBox13", eFormDetailList), new { @width = "100px", maxlength = "500" })%>
                 </td>
            </tr>
            <tr>
                <td class="label">
                    Conclusion
                </td>
                <td class="input" >
                    <table id="tblComment" width="100%">
                        <tr >
                            <td>
                                <%=Html.RadioButton("RadioButton21", "radio1", true)%>
                                &nbsp;Poor = 1-2
                            </td>
                            <td>
                                <%=Html.RadioButton("RadioButton21", "radio2", CommonFunc.GetRadioStatus("RadioButton21", eFormDetailList, "radio2"))%>
                                &nbsp;Fair = 3-4
                            </td>
                            <td>
                                <%=Html.RadioButton("RadioButton21", "radio3", CommonFunc.GetRadioStatus("RadioButton21", eFormDetailList, "radio3"))%>
                                &nbsp;Satisfactory = 5-6
                            </td>
                            <td>
                                <%=Html.RadioButton("RadioButton21", "radio4", CommonFunc.GetRadioStatus("RadioButton21", eFormDetailList, "radio4"))%>
                                &nbsp;Good = 7-8
                            </td>
                            <td>
                                <%=Html.RadioButton("RadioButton21", "radio5", CommonFunc.GetRadioStatus("RadioButton21", eFormDetailList, "radio5"))%>
                                &nbsp;Excellent = 9-10
                            </td>
                        </tr>
                        
                    </table>
                </td>
            </tr>
            <tr >
                <td class="label required">
                    Date<span>*</span>
                </td>
                <td class="input">
                    <%
                        string insertDate=CommonFunc.GetTextBoxValue("TextBox14", eFormDetailList);
                    %>
                    <%=Html.TextBox("TextBox14", insertDate == "" ? (interview != null ? interview.InterviewDate.Value.ToString(Constants.DATETIME_FORMAT) : DateTime.Now.ToShortDateString()) : insertDate, new { @style = "width:150px", maxlength = "500" })%>
                </td>
            </tr>
            <tr class="last">
                <td class="label required">
                    Interviewed by<span>*</span>
                </td>
                <td class="input">
                    <%
                        string user = CommonFunc.GetTextBoxValue("TextBox15", eFormDetailList);
                    %>
                    <%=Html.TextBox("TextBox15", user == "" ? (interview!=null?interview.Pic:(string)ViewData["user"]) : user, new { @style = "width:150px", maxlength = "500" })%>
                </td>
                        
                
            </tr>
    </table>
