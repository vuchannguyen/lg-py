<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<dynamic>" %>
<script type="text/javascript">

    $(document).ready(function () {
        $.validator.addMethod('integer', function (value, element, param) {
            if ($.trim(value) == '') {
                return true;
            }
            else {
                return (value == parseInt(value, 10));
            }
        }, E0037);
        $("#ExamQuestionForm").validate({
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
                Title: {
                    required: true,
                    maxlength: 100
                },
                Time: {
                    required: true,
                    integer: true,
                    min: 1
                }
            },

            submitHandler: function (form) {
                CRM.summary('', 'none', '');
                $("#btnSubmit").attr("disabled", "disabled");
                $(form).ajaxSubmit({
                    success: function (result) {
                        if (result.msg.MsgType == 1) {
                            $("#btnSubmit").attr("disabled", "");
                            CRM.summary(result.msg.MsgText, 'block', 'msgError');
                        }
                        else {
                            CRM.message(result.msg.MsgText, 'block', 'msgSuccess');
                            $("#btnFilter").click();
                            CRM.closePopup();
                        }
                    },
                    url: form.action,
                    dataType: 'json'
                });
            }
        });
    });

    function tickOnSection(id) {
        if ($("#" + id).attr("checked") == true) {
            $("#MaxMark_" + id).attr("disabled", "");
            $("#IsRandom_" + id).attr("disabled", "");
            if ($("#IsRandom_" + id).attr("checked") == true) {
                $("#NumberOfQuestions_IsRandom_" + id).attr("disabled", "");
                $("#NumberOfQuestions_IsRandom_" + id).rules("add", { required: true, integer: true, min: 1 });
            }
            else {
                $("#NumberOfQuestions_IsRandom_" + id).attr("disabled", "disabled");
            }
            $("#MaxMark_" + id).rules("add", { required: true, integer: true, min: 1 });


        }
        else {
            $("#MaxMark_" + id).rules("remove");
            $("#MaxMark_" + id).attr("disabled", "disabled");
            $("#IsRandom_" + id).attr("disabled", "disabled");
            $("#NumberOfQuestions_IsRandom_" + id).attr("disabled", "disabled");
        }
    }


    function tickOnIsRandom(object) {
        var id = object.id;
        if ($("#" + id).attr("checked") == true) {
            $("#NumberOfQuestions_" + id).attr("disabled", "");
            $("#NumberOfQuestions_" + id).rules("add", { required: true, integer: true, min: 1 });            
        }
        else {
            $("#NumberOfQuestions_" + id).rules("remove");
            $("#NumberOfQuestions_" + id).attr("disabled", "disabled");
        }
    }  

</script>
<table width="100%" border="0" cellpadding="0" cellspacing="0" class="edit">
    <% LOT_ExamQuestion examQuestion = (LOT_ExamQuestion)ViewData.Model; %>
    <tr>
        <td class="label required">
            Title <span>*</span>
        </td>
        <td class="input">
            <% if (ViewData.Model == null)
               {
                   Response.Write(Html.TextBox("Title", "", new { @maxlength = "100", @style = "width:200px" }));
               }
               else
               {
                   Response.Write(Html.TextBox("Title", examQuestion.Title, new { @maxlength = "100", @style = "width:200px" }));
                   Response.Write(Html.Hidden("ID", examQuestion.ID));
               }
            %>
        </td>
    </tr>
    <tr>
        <td class="label required" style="vertical-align:top">
            Section <span>*</span>
        </td>    
        <td class="input">
            <table class="grid" style="width: 80%">
                <tr>
                    <th>
                    </th>
                    <th>
                        Name
                    </th>
                    <th>
                        Max Mark
                    </th>
                    <th>
                        Is Random
                    </th>
                    <th>
                        Number of Question
                    </th>
                </tr>
                <%
                    List<LOT_Section> sectionList = (List<LOT_Section>)ViewData[CommonDataKey.SECTION];                    
                    foreach (LOT_Section item in sectionList)
                    {
                        if (ViewData.Model == null)
                        {                         
                %>
                <tr>
                    <td>
                        <%= Html.CheckBox(Constants.LOT_SECTIONID_STARTWITH + item.ID.ToString(), true, new { @id = Constants.LOT_SECTIONID_STARTWITH + item.ID.ToString(), @onclick = "tickOnSection('" + Constants.LOT_SECTIONID_STARTWITH + item.ID.ToString() + "');" })%>
                    </td>
                    <td>
                        <%= item.SectionName%>
                    </td>
                    <td>
                        <%if (item.ID != Constants.LOT_VERBAL_SKILL_ID) %>
                        <%= Html.TextBox("MaxMark_" + Constants.LOT_SECTIONID_STARTWITH + item.ID.ToString(), "", new { @id = "MaxMark_" + Constants.LOT_SECTIONID_STARTWITH + item.ID.ToString(), @maxlength = "3", @style = "width:50px" })%>
                    </td>
                    <% if (Constants.LOT_RandomList.Contains(item.ID))
                       {
                    %>
                    <td align="center">
                        <%if (item.ID != Constants.LOT_VERBAL_SKILL_ID) %>
                        <%= Html.CheckBox("IsRandom_" + Constants.LOT_SECTIONID_STARTWITH + item.ID.ToString(), true, new { @id = "IsRandom_" + Constants.LOT_SECTIONID_STARTWITH + item.ID.ToString(), @onclick = "tickOnIsRandom(this)" })%>
                    </td>
                    <td>
                        <%if (item.ID != Constants.LOT_VERBAL_SKILL_ID) %>
                        <%= Html.TextBox("NumberOfQuestions_IsRandom_" + Constants.LOT_SECTIONID_STARTWITH + item.ID.ToString(), "", new { @id = "NumberOfQuestions_IsRandom_" + Constants.LOT_SECTIONID_STARTWITH + item.ID.ToString(), @maxlength = "3", @style = "width:50px" })%>
                    </td>
                    <%
                        }
                       else
                       {
                    %>
                    <td align="center">
                        <%if (item.ID != Constants.LOT_VERBAL_SKILL_ID) %>
                        <%= Html.CheckBox("IsRandom_" + Constants.LOT_SECTIONID_STARTWITH + item.ID.ToString(), false, new { @id = "IsRandom_" + Constants.LOT_SECTIONID_STARTWITH + item.ID.ToString(), @onclick = "tickOnIsRandom(this)", @disabled = "disabled" })%>
                    </td>
                    <td>
                        <%if (item.ID != Constants.LOT_VERBAL_SKILL_ID) %>
                        <%= Html.TextBox("NumberOfQuestions_IsRandom_" + Constants.LOT_SECTIONID_STARTWITH + item.ID.ToString(), "", new { @id = "NumberOfQuestions_IsRandom_" + Constants.LOT_SECTIONID_STARTWITH + item.ID.ToString(), @maxlength = "3", @style = "width:50px", @disabled = "disabled" })%>
                    </td>
                    <%
                        } 
                    %>
                </tr>
                <% 
                        }
                        else //Edit case
                        {
                            List<LOT_ExamQuestion_Section> examQuestionList = (List<LOT_ExamQuestion_Section>)ViewData[CommonDataKey.EXAM_QUESTION_SECTION];
                            LOT_ExamQuestion_Section examQuestionSection = examQuestionList.Where(c => c.SectionID == item.ID).FirstOrDefault<LOT_ExamQuestion_Section>();                            
                            
                            %>
                <tr>
                    <td>
                        <%= Html.CheckBox(Constants.LOT_SECTIONID_STARTWITH + item.ID.ToString(), examQuestionSection == null ? false : true, new { @id = Constants.LOT_SECTIONID_STARTWITH + item.ID.ToString(), @onclick = "tickOnSection('" + Constants.LOT_SECTIONID_STARTWITH + item.ID.ToString() + "');" })%>
                    </td>
                    <td>
                        <%= item.SectionName%>
                    </td>
                    <td>
                        <%if (item.ID != Constants.LOT_VERBAL_SKILL_ID) %>
                        <%= Html.TextBox("MaxMark_" + Constants.LOT_SECTIONID_STARTWITH + item.ID.ToString(), examQuestionSection == null ? string.Empty : examQuestionSection.MaxMark.ToString(), new { @id = "MaxMark_" + Constants.LOT_SECTIONID_STARTWITH + item.ID.ToString(), @maxlength = "3", @style = "width:50px" })%>
                    </td>
                   
                    <td align="center">
                        <%if (item.ID != Constants.LOT_VERBAL_SKILL_ID) %>
                        <%= Html.CheckBox("IsRandom_" + Constants.LOT_SECTIONID_STARTWITH + item.ID.ToString(), examQuestionSection != null && examQuestionSection.IsRandom ? true : false, new { @id = "IsRandom_" + Constants.LOT_SECTIONID_STARTWITH + item.ID.ToString(), @onclick = "tickOnIsRandom(this)", @disabled = "disabled" })%>
                    </td>
                    <td>
                        <%if (item.ID != Constants.LOT_VERBAL_SKILL_ID) %>
                        <%= Html.TextBox("NumberOfQuestions_IsRandom_" + Constants.LOT_SECTIONID_STARTWITH + item.ID.ToString(), examQuestionSection != null && examQuestionSection.IsRandom ? examQuestionSection.NumberOfQuestions.Value.ToString() : string.Empty, new { @id = "NumberOfQuestions_IsRandom_" + Constants.LOT_SECTIONID_STARTWITH + item.ID.ToString(), @maxlength = "3", @style = "width:50px", @disabled = "disabled" })%>
                    </td>                   
                </tr>
                <% 
                        }
                    Response.Write("<script> $(document).ready(function () { tickOnSection('" + Constants.LOT_SECTIONID_STARTWITH + item.ID.ToString() + "'); }); </script>");
                    }
                %>
            </table>
        </td>
    </tr>
    <tr>
        <td class="label required">
            Time <span>*</span>
        </td>
        <td class="input">
            <% if (ViewData.Model == null)
               {
                   Response.Write(Html.TextBox("Time", "", new { @maxlength = "3", @style = "width:50px" }) + " min(s)");
               }
               else
               {
                   Response.Write(Html.TextBox("Time", examQuestion.ExamQuestionTime, new { @maxlength = "3", @style = "width:50px" }) + " min(s)");
               }
            %>
        </td>
    </tr>
    <tr>
        <td colspan="2" align="center">
            <input id="btnSubmit" type="submit" class="save" value="" alt="Update" />
            <input type="button" class="cancel" value="" alt="" onclick="CRM.closePopup()" />
        </td>
    </tr>
</table>
