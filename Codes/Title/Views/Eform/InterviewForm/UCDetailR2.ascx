<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<% 
    List<EForm_Detail> eFormDetailList = ViewData["detail_list"] == null ? null : (List<EForm_Detail>)ViewData["detail_list"];%>
<%=Html.Hidden("Hidden1",ViewData["eform"]==null?"":ViewData["eform"].ToString())%>
<script type="text/javascript">
    var gAutoPrint = true; // Flag for whether or not to automatically call the print function

    function printSpecial() {
        if (document.getElementById != null) {
            var html = '<HTML style="overflow-y:auto">\n<HEAD>\n';

            if (document.getElementsByTagName != null) {
                var headTags = document.getElementsByTagName("head");
                if (headTags.length > 0)
                    html += headTags[0].innerHTML;
            }
            html += '\n</HE' + 'AD>\n<BODY>\n';

            jhtml = $("#printReady");
            if (jhtml.length != 0) {
                spanCheck = $(jhtml).find('span.check');
                $(spanCheck).each(function () {
                    if ($(this).text() == '') {
                        $(this).parent().append("<img src='/Content/Images/ExtraIcons/tick.png' alt='' />");
                        $(this).remove();
                    }
                    else {
                        $(this).parent().append("<img src='/Content/Images/ExtraIcons/tick.png' alt='' />");
                        $(this).parent().append("<b>" + $(this).text() + "</b>");
                        $(this).remove();
                    }
                });
                html += $(jhtml).html();
            }
            else {
                alert("Could not find the printReady section in the HTML");
                return;
            }

            html += '\n</BO' + 'DY>\n</HT' + 'ML>';
            var printWin = window.open("", "printSpecial", "width=800, height=600, directories=no, location=no, menubar=no,resizable=no,scrollbars=1,status=no,toolbar=no");
            printWin.document.open();
            printWin.document.write(html);
            printWin.document.close();
            if (gAutoPrint)
                printWin.print(); printWin.close();
        }
        else {
            alert("Sorry, the print ready feature is only available in modern browsers.");
        }
    }
</script>
<div style="text-align:right; font-weight:bold; padding-right:18px"><a href="javascript:;" onclick="printSpecial();"><img src="/Content/Images/ExtraIcons/printer.png"> Print</a></div>
<div style="height: 500px; overflow-x: auto" id="printReady">
    <h2 class="heading">
        Interview Evaluation:</h2>
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
           
        </tr>
        <tr>
            <td align="center" colspan="12">
                <strong style="font-size: 10pt">2<sup>nd</sup> Round-Technicall Professional interview:
                </strong>(For HR Dept)<br />
                (Please make a list of technical skill for evaluation)
            </td>
        </tr>
        <tr>
            <td class="title">
                1. Test plan
            </td>
            <td align="center">
                <span class='<%=CommonFunc.GetRadioStatus("RadioButton1", eFormDetailList, "radio1")?"check":"none"%>'>
                </span>
            </td>
            <td align="center">
                <span class='<%=CommonFunc.GetRadioStatus("RadioButton1", eFormDetailList, "radio2")?"check":"none"%>'>
                </span>
            </td>
            <td align="center">
                <span class='<%=CommonFunc.GetRadioStatus("RadioButton1", eFormDetailList, "radio3")?"check":"none"%>'>
                </span>
            </td>
            <td align="center">
                <span class='<%=CommonFunc.GetRadioStatus("RadioButton1", eFormDetailList, "radio4")?"check":"none"%>'>
                </span>
            </td>
            <td align="center">
                <span class='<%=CommonFunc.GetRadioStatus("RadioButton1", eFormDetailList, "radio5")?"check":"none"%>'>
                </span>
            </td>
            <td class="title">
                11. Communication
            </td>
            <td align="center">
                <span class='<%=CommonFunc.GetRadioStatus("RadioButton2", eFormDetailList, "radio1")?"check":"none"%>'>
                </span>
            </td>
            <td align="center">
                <span class='<%=CommonFunc.GetRadioStatus("RadioButton2", eFormDetailList, "radio2")?"check":"none"%>'>
                </span>
            </td>
            <td align="center">
                <span class='<%=CommonFunc.GetRadioStatus("RadioButton2", eFormDetailList, "radio3")?"check":"none"%>'>
                </span>
            </td>
            <td align="center">
                <span class='<%=CommonFunc.GetRadioStatus("RadioButton2", eFormDetailList, "radio4")?"check":"none"%>'>
                </span>
            </td>
            <td align="center">
                <span class='<%=CommonFunc.GetRadioStatus("RadioButton2", eFormDetailList, "radio5")?"check":"none"%>'>
                </span>
            </td>
        </tr>
        <tr>
            <td class="title">
                2. Test case
            </td>
            <td align="center">
                <span class='<%=CommonFunc.GetRadioStatus("RadioButton3", eFormDetailList, "radio1")?"check":"none"%>'>
                </span>
            </td>
            <td align="center">
                <span class='<%=CommonFunc.GetRadioStatus("RadioButton3", eFormDetailList, "radio2")?"check":"none"%>'>
                </span>
            </td>
            <td align="center">
                <span class='<%=CommonFunc.GetRadioStatus("RadioButton3", eFormDetailList, "radio3")?"check":"none"%>'>
                </span>
            </td>
            <td align="center">
                <span class='<%=CommonFunc.GetRadioStatus("RadioButton3", eFormDetailList, "radio4")?"check":"none"%>'>
                </span>
            </td>
            <td align="center">
                <span class='<%=CommonFunc.GetRadioStatus("RadioButton3", eFormDetailList, "radio5")?"check":"none"%>'>
                </span>
            </td>
            <td class="title">
                12. Carefulness and Meticulousness
            </td>
            <td align="center">
                <span class='<%=CommonFunc.GetRadioStatus("RadioButton4", eFormDetailList, "radio1")?"check":"none"%>'>
                </span>
            </td>
            <td align="center">
                <span class='<%=CommonFunc.GetRadioStatus("RadioButton4", eFormDetailList, "radio2")?"check":"none"%>'>
                </span>
            </td>
            <td align="center">
                <span class='<%=CommonFunc.GetRadioStatus("RadioButton4", eFormDetailList, "radio3")?"check":"none"%>'>
                </span>
            </td>
            <td align="center">
                <span class='<%=CommonFunc.GetRadioStatus("RadioButton4", eFormDetailList, "radio4")?"check":"none"%>'>
                </span>
            </td>
            <td align="center">
                <span class='<%=CommonFunc.GetRadioStatus("RadioButton4", eFormDetailList, "radio5")?"check":"none"%>'>
                </span>
            </td>
        </tr>
        <tr>
            <td class="title">
                3. Test strategy
            </td>
            <td align="center">
                <span class='<%=CommonFunc.GetRadioStatus("RadioButton5", eFormDetailList, "radio1")?"check":"none"%>'>
                </span>
            </td>
            <td align="center">
                <span class='<%=CommonFunc.GetRadioStatus("RadioButton5", eFormDetailList, "radio2")?"check":"none"%>'>
                </span>
            </td>
            <td align="center">
                <span class='<%=CommonFunc.GetRadioStatus("RadioButton5", eFormDetailList, "radio3")?"check":"none"%>'>
                </span>
            </td>
            <td align="center">
                <span class='<%=CommonFunc.GetRadioStatus("RadioButton5", eFormDetailList, "radio4")?"check":"none"%>'>
                </span>
            </td>
            <td align="center">
                <span class='<%=CommonFunc.GetRadioStatus("RadioButton5", eFormDetailList, "radio5")?"check":"none"%>'>
                </span>
            </td>
            <td class="title">
                <div style="float: left">
                    13.
                    <%=CommonFunc.GetTextBoxValue("TextBox1", eFormDetailList)%>
                </div>
            </td>
            <td align="center">
                <span class='<%=CommonFunc.GetRadioStatus("RadioButton6", eFormDetailList, "radio1")?"check":"none"%>'>
                </span>
            </td>
            <td align="center">
                <span class='<%=CommonFunc.GetRadioStatus("RadioButton6", eFormDetailList, "radio2")?"check":"none"%>'>
                </span>
            </td>
            <td align="center">
                <span class='<%=CommonFunc.GetRadioStatus("RadioButton6", eFormDetailList, "radio3")?"check":"none"%>'>
                </span>
            </td>
            <td align="center">
                <span class='<%=CommonFunc.GetRadioStatus("RadioButton6", eFormDetailList, "radio4")?"check":"none"%>'>
                </span>
            </td>
            <td align="center">
                <span class='<%=CommonFunc.GetRadioStatus("RadioButton6", eFormDetailList, "radio5")?"check":"none"%>'>
                </span>
            </td>
        </tr>
        <tr>
            <td class="title">
                4. Quality Assurance - QA
            </td>
            <td align="center">
                <span class='<%=CommonFunc.GetRadioStatus("RadioButton7", eFormDetailList, "radio1")?"check":"none"%>'>
                </span>
            </td>
            <td align="center">
                <span class='<%=CommonFunc.GetRadioStatus("RadioButton7", eFormDetailList, "radio2")?"check":"none"%>'>
                </span>
            </td>
            <td align="center">
                <span class='<%=CommonFunc.GetRadioStatus("RadioButton7", eFormDetailList, "radio3")?"check":"none"%>'>
                </span>
            </td>
            <td align="center">
                <span class='<%=CommonFunc.GetRadioStatus("RadioButton7", eFormDetailList, "radio4")?"check":"none"%>'>
                </span>
            </td>
            <td align="center">
                <span class='<%=CommonFunc.GetRadioStatus("RadioButton7", eFormDetailList, "radio5")?"check":"none"%>'>
                </span>
            </td>
            <td class="title">
                <div style="float: left">
                    14.
                    <%=CommonFunc.GetTextBoxValue("TextBox2", eFormDetailList)%>
                </div>
            </td>
            <td align="center">
                <span class='<%=CommonFunc.GetRadioStatus("RadioButton8", eFormDetailList, "radio1")?"check":"none"%>'>
                </span>
            </td>
            <td align="center">
                <span class='<%=CommonFunc.GetRadioStatus("RadioButton8", eFormDetailList, "radio2")?"check":"none"%>'>
                </span>
            </td>
            <td align="center">
                <span class='<%=CommonFunc.GetRadioStatus("RadioButton8", eFormDetailList, "radio3")?"check":"none"%>'>
                </span>
            </td>
            <td align="center">
                <span class='<%=CommonFunc.GetRadioStatus("RadioButton8", eFormDetailList, "radio4")?"check":"none"%>'>
                </span>
            </td>
            <td align="center">
                <span class='<%=CommonFunc.GetRadioStatus("RadioButton8", eFormDetailList, "radio5")?"check":"none"%>'>
                </span>
            </td>
        </tr>
        <tr>
            <td class="title">
                5. Test Architect - TA
            </td>
            <td align="center">
                <span class='<%=CommonFunc.GetRadioStatus("RadioButton9", eFormDetailList, "radio1")?"check":"none"%>'>
                </span>
            </td>
            <td align="center">
                <span class='<%=CommonFunc.GetRadioStatus("RadioButton9", eFormDetailList, "radio2")?"check":"none"%>'>
                </span>
            </td>
            <td align="center">
                <span class='<%=CommonFunc.GetRadioStatus("RadioButton9", eFormDetailList, "radio3")?"check":"none"%>'>
                </span>
            </td>
            <td align="center">
                <span class='<%=CommonFunc.GetRadioStatus("RadioButton9", eFormDetailList, "radio4")?"check":"none"%>'>
                </span>
            </td>
            <td align="center">
                <span class='<%=CommonFunc.GetRadioStatus("RadioButton9", eFormDetailList, "radio5")?"check":"none"%>'>
                </span>
            </td>
            <td class="title">
                15.
                <%=CommonFunc.GetTextBoxValue("TextBox3", eFormDetailList)%>
            </td>
            <td align="center">
                <span class='<%=CommonFunc.GetRadioStatus("RadioButton10", eFormDetailList, "radio1")?"check":"none"%>'>
                </span>
            </td>
            <td align="center">
                <span class='<%=CommonFunc.GetRadioStatus("RadioButton10", eFormDetailList, "radio2")?"check":"none"%>'>
                </span>
            </td>
            <td align="center">
                <span class='<%=CommonFunc.GetRadioStatus("RadioButton10", eFormDetailList, "radio3")?"check":"none"%>'>
                </span>
            </td>
            <td align="center">
                <span class='<%=CommonFunc.GetRadioStatus("RadioButton10", eFormDetailList, "radio4")?"check":"none"%>'>
                </span>
            </td>
            <td align="center">
                <span class='<%=CommonFunc.GetRadioStatus("RadioButton10", eFormDetailList, "radio5")?"check":"none"%>'>
                </span>
            </td>
        </tr>
        <tr>
            <td class="title">
                6. Automation
            </td>
            <td align="center">
                <span class='<%=CommonFunc.GetRadioStatus("RadioButton11", eFormDetailList, "radio1")?"check":"none"%>'>
                </span>
            </td>
            <td align="center">
                <span class='<%=CommonFunc.GetRadioStatus("RadioButton11", eFormDetailList, "radio2")?"check":"none"%>'>
                </span>
            </td>
            <td align="center">
                <span class='<%=CommonFunc.GetRadioStatus("RadioButton11", eFormDetailList, "radio3")?"check":"none"%>'>
                </span>
            </td>
            <td align="center">
                <span class='<%=CommonFunc.GetRadioStatus("RadioButton11", eFormDetailList, "radio4")?"check":"none"%>'>
                </span>
            </td>
            <td align="center">
                <span class='<%=CommonFunc.GetRadioStatus("RadioButton11", eFormDetailList, "radio5")?"check":"none"%>'>
                </span>
            </td>
            <td class="title">
                16.
                <%=CommonFunc.GetTextBoxValue("TextBox4", eFormDetailList)%>
            </td>
            <td align="center">
                <span class='<%=CommonFunc.GetRadioStatus("RadioButton12", eFormDetailList, "radio1")?"check":"none"%>'>
                </span>
            </td>
            <td align="center">
                <span class='<%=CommonFunc.GetRadioStatus("RadioButton12", eFormDetailList, "radio2")?"check":"none"%>'>
                </span>
            </td>
            <td align="center">
                <span class='<%=CommonFunc.GetRadioStatus("RadioButton12", eFormDetailList, "radio3")?"check":"none"%>'>
                </span>
            </td>
            <td align="center">
                <span class='<%=CommonFunc.GetRadioStatus("RadioButton12", eFormDetailList, "radio4")?"check":"none"%>'>
                </span>
            </td>
            <td align="center">
                <span class='<%=CommonFunc.GetRadioStatus("RadioButton12", eFormDetailList, "radio5")?"check":"none"%>'>
                </span>
            </td>
        </tr>
        <tr>
            <td class="title">
                7. Bug
            </td>
            <td align="center">
                <span class='<%=CommonFunc.GetRadioStatus("RadioButton13", eFormDetailList, "radio1")?"check":"none"%>'>
                </span>
            </td>
            <td align="center">
                <span class='<%=CommonFunc.GetRadioStatus("RadioButton13", eFormDetailList, "radio2")?"check":"none"%>'>
                </span>
            </td>
            <td align="center">
                <span class='<%=CommonFunc.GetRadioStatus("RadioButton13", eFormDetailList, "radio3")?"check":"none"%>'>
                </span>
            </td>
            <td align="center">
                <span class='<%=CommonFunc.GetRadioStatus("RadioButton13", eFormDetailList, "radio4")?"check":"none"%>'>
                </span>
            </td>
            <td align="center">
                <span class='<%=CommonFunc.GetRadioStatus("RadioButton13", eFormDetailList, "radio5")?"check":"none"%>'>
                </span>
            </td>
            <td class="title">
                17.
                <%=CommonFunc.GetTextBoxValue("TextBox5", eFormDetailList)%>
            </td>
            <td align="center">
                <span class='<%=CommonFunc.GetRadioStatus("RadioButton14", eFormDetailList, "radio1")?"check":"none"%>'>
                </span>
            </td>
            <td align="center">
                <span class='<%=CommonFunc.GetRadioStatus("RadioButton14", eFormDetailList, "radio2")?"check":"none"%>'>
                </span>
            </td>
            <td align="center">
                <span class='<%=CommonFunc.GetRadioStatus("RadioButton14", eFormDetailList, "radio3")?"check":"none"%>'>
                </span>
            </td>
            <td align="center">
                <span class='<%=CommonFunc.GetRadioStatus("RadioButton14", eFormDetailList, "radio4")?"check":"none"%>'>
                </span>
            </td>
            <td align="center">
                <span class='<%=CommonFunc.GetRadioStatus("RadioButton14", eFormDetailList, "radio5")?"check":"none"%>'>
                </span>
            </td>
        </tr>
        <tr>
            <td class="title">
                8. Analyses
            </td>
            <td align="center">
                <span class='<%=CommonFunc.GetRadioStatus("RadioButton15", eFormDetailList, "radio1")?"check":"none"%>'>
                </span>
            </td>
            <td align="center">
                <span class='<%=CommonFunc.GetRadioStatus("RadioButton15", eFormDetailList, "radio2")?"check":"none"%>'>
                </span>
            </td>
            <td align="center">
                <span class='<%=CommonFunc.GetRadioStatus("RadioButton15", eFormDetailList, "radio3")?"check":"none"%>'>
                </span>
            </td>
            <td align="center">
                <span class='<%=CommonFunc.GetRadioStatus("RadioButton15", eFormDetailList, "radio4")?"check":"none"%>'>
                </span>
            </td>
            <td align="center">
                <span class='<%=CommonFunc.GetRadioStatus("RadioButton15", eFormDetailList, "radio5")?"check":"none"%>'>
                </span>
            </td>
            <td class="title">
                18.
                <%=CommonFunc.GetTextBoxValue("TextBox6", eFormDetailList)%>
            </td>
            <td align="center">
                <span class='<%=CommonFunc.GetRadioStatus("RadioButton16", eFormDetailList, "radio1")?"check":"none"%>'>
                </span>
            </td>
            <td align="center">
                <span class='<%=CommonFunc.GetRadioStatus("RadioButton16", eFormDetailList, "radio2")?"check":"none"%>'>
                </span>
            </td>
            <td align="center">
                <span class='<%=CommonFunc.GetRadioStatus("RadioButton16", eFormDetailList, "radio3")?"check":"none"%>'>
                </span>
            </td>
            <td align="center">
                <span class='<%=CommonFunc.GetRadioStatus("RadioButton16", eFormDetailList, "radio4")?"check":"none"%>'>
                </span>
            </td>
            <td align="center">
                <span class='<%=CommonFunc.GetRadioStatus("RadioButton16", eFormDetailList, "radio5")?"check":"none"%>'>
                </span>
            </td>
        </tr>
        <tr>
            <td class="title">
                9. Organization
            </td>
            <td align="center">
                <span class='<%=CommonFunc.GetRadioStatus("RadioButton17", eFormDetailList, "radio1")?"check":"none"%>'>
                </span>
            </td>
            <td align="center">
                <span class='<%=CommonFunc.GetRadioStatus("RadioButton17", eFormDetailList, "radio2")?"check":"none"%>'>
                </span>
            </td>
            <td align="center">
                <span class='<%=CommonFunc.GetRadioStatus("RadioButton17", eFormDetailList, "radio3")?"check":"none"%>'>
                </span>
            </td>
            <td align="center">
                <span class='<%=CommonFunc.GetRadioStatus("RadioButton17", eFormDetailList, "radio4")?"check":"none"%>'>
                </span>
            </td>
            <td align="center">
                <span class='<%=CommonFunc.GetRadioStatus("RadioButton17", eFormDetailList, "radio5")?"check":"none"%>'>
                </span>
            </td>
            <td class="title">
                19.
                <%=CommonFunc.GetTextBoxValue("TextBox7", eFormDetailList)%>
            </td>
            <td align="center">
                <span class='<%=CommonFunc.GetRadioStatus("RadioButton18", eFormDetailList, "radio1")?"check":"none"%>'>
                </span>
            </td>
            <td align="center">
                <span class='<%=CommonFunc.GetRadioStatus("RadioButton18", eFormDetailList, "radio2")?"check":"none"%>'>
                </span>
            </td>
            <td align="center">
                <span class='<%=CommonFunc.GetRadioStatus("RadioButton18", eFormDetailList, "radio3")?"check":"none"%>'>
                </span>
            </td>
            <td align="center">
                <span class='<%=CommonFunc.GetRadioStatus("RadioButton18", eFormDetailList, "radio4")?"check":"none"%>'>
                </span>
            </td>
            <td align="center">
                <span class='<%=CommonFunc.GetRadioStatus("RadioButton18", eFormDetailList, "radio5")?"check":"none"%>'>
                </span>
            </td>
        </tr>
        <tr>
            <td class="title">
                10. Evaluated Bug
            </td>
            <td align="center">
                <span class='<%=CommonFunc.GetRadioStatus("RadioButton19", eFormDetailList, "radio1")?"check":"none"%>'>
                </span>
            </td>
            <td align="center">
                <span class='<%=CommonFunc.GetRadioStatus("RadioButton19", eFormDetailList, "radio2")?"check":"none"%>'>
                </span>
            </td>
            <td align="center">
                <span class='<%=CommonFunc.GetRadioStatus("RadioButton19", eFormDetailList, "radio3")?"check":"none"%>'>
                </span>
            </td>
            <td align="center">
                <span class='<%=CommonFunc.GetRadioStatus("RadioButton19", eFormDetailList, "radio4")?"check":"none"%>'>
                </span>
            </td>
            <td align="center">
                <span class='<%=CommonFunc.GetRadioStatus("RadioButton19", eFormDetailList, "radio5")?"check":"none"%>'>
                </span>
            </td>
            <td class="title">
                20.
                <%=CommonFunc.GetTextBoxValue("TextBox8", eFormDetailList)%>
            </td>
            <td align="center">
                <span class='<%=CommonFunc.GetRadioStatus("RadioButton20", eFormDetailList, "radio1")?"check":"none"%>'>
                </span>
            </td>
            <td align="center">
                <span class='<%=CommonFunc.GetRadioStatus("RadioButton20", eFormDetailList, "radio2")?"check":"none"%>'>
                </span>
            </td>
            <td align="center">
                <span class='<%=CommonFunc.GetRadioStatus("RadioButton20", eFormDetailList, "radio3")?"check":"none"%>'>
                </span>
            </td>
            <td align="center">
                <span class='<%=CommonFunc.GetRadioStatus("RadioButton20", eFormDetailList, "radio4")?"check":"none"%>'>
                </span>
            </td>
            <td align="center">
                <span class='<%=CommonFunc.GetRadioStatus("RadioButton20", eFormDetailList, "radio5")?"check":"none"%>'>
                </span>
            </td>
        </tr>
    </table>
    <h2 class="heading">
        Comments from Interviewer(s):</h2>
    <table width="100%" border="0" cellpadding="0" cellspacing="0" class="view">
        <tr>
            <td style="padding-left: 10px">
                <fieldset style="width: 97%" class="white">
                    <legend>Experiences</legend>
                    <%=CommonFunc.GetTextBoxValue("TextBox9", eFormDetailList)%>
                </fieldset>
            </td>
        </tr>
        <tr>
            <td style="padding-left: 10px">
                <fieldset style="width: 97%" class="white">
                    <legend>Strengths</legend>
                    <%=CommonFunc.GetTextBoxValue("TextBox10", eFormDetailList)%>
                </fieldset>
            </td>
        </tr>
        <tr>
            <td style="padding-left: 10px">
                <fieldset style="width: 97%" class="white">
                    <legend>Weaknesses</legend>
                    <%=CommonFunc.GetTextBoxValue("TextBox11", eFormDetailList)%>
                </fieldset>
            </td>
        </tr>
        <tr>
            <td style="padding-left: 10px">
                <fieldset style="width: 97%" class="white">
                    <legend>Additional comments</legend>
                    <%=CommonFunc.GetTextBoxValue("TextBox12", eFormDetailList)%>
                </fieldset>
            </td>
        </tr>
        <tr>
            <td style="padding-left: 10px">
                <fieldset style="width: 97%" class="white">
                    <legend>Conclusion</legend>
                    <table width="100%">
                        <tr>
                            <td>
                                <span class='<%=CommonFunc.GetRadioStatus("RadioButton21", eFormDetailList, "radio1")?"rcheck":"none"%>'>
                                    Poor=1-2</span>
                            </td>
                            <td>
                                <span class='<%=CommonFunc.GetRadioStatus("RadioButton21", eFormDetailList, "radio2")?"rcheck":"none"%>'>
                                    Fair=3-4</span>
                            </td>
                            <td>
                                <span class='<%=CommonFunc.GetRadioStatus("RadioButton21", eFormDetailList, "radio3")?"rcheck":"none"%>'>
                                    Satisfactory=5-6</span>
                            </td>
                            <td>
                                <span class='<%=CommonFunc.GetRadioStatus("RadioButton21", eFormDetailList, "radio4")?"rcheck":"none"%>'>
                                    Good=7-8</span>
                            </td>
                            <td>
                                <span class='<%=CommonFunc.GetRadioStatus("RadioButton21", eFormDetailList, "radio5")?"rcheck":"none"%>'>
                                    Excellent=9-10</span>
                            </td>
                        </tr>
                    </table>
                    <table width="70%">
                    </table>
                </fieldset>
            </td>
        </tr>
        <tr>
            <td>
                <table width="100%">
                    <tr>
                        <td style="padding-left: 10px">
                            <strong>Date:&nbsp;&nbsp;<%=CommonFunc.GetTextBoxValue("TextBox13", eFormDetailList)%></strong>
                        </td>
                        <td style="padding-right: 10px;" align="right">
                            <strong>Interviewed by:&nbsp;&nbsp;<%=CommonFunc.GetTextBoxValue("TextBox14", eFormDetailList)%></strong>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td align="center">
                <div class="form">
                    <script type="text/javascript">
                        $(document).ready(function () {
                            $("#btnClose").click(function () {
                                CRM.closePopup();
                            });
                        });
                    </script>
                    <input type="button" id="btnClose" class="close" />
                </div>
            </td>
        </tr>
    </table>
</div>
