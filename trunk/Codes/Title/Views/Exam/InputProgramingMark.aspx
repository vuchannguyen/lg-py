<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>
<script type="text/javascript">
    $(document).ready(function () {
        $.validator.addMethod('integer', function (value, element, param) {
            if ($.trim(value) == '') {
                return true;
            }
            else {
                return value.match('^(0|[1-9][0-9]*)$'); 
            }
        }, E0037);            
        $("#ExamForm").validate({
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
                ProgramingMark: {                    
                    integer: true,
                    min:0,
                    max:<%= ViewData[CommonDataKey.CTL_MAX_PROGRAMING_MARK]%>
                },             
                ProgramingComment: {
                    maxlength: 255
                }
            }
        });

        $("#scrollContainer").scrollTop(0);
    });
</script>
<div id="" class="details_view form">
<%using (Html.BeginForm("InputProgramingMark", "Exam", FormMethod.Post, new { id = "ExamForm", @class = "form" }))
  { %>  
  <div class="edit">
<table width="100%" border="0" cellpadding="0" cellspacing="0">
    <tr>
        <td class="label">
        Candidate
        </td>
        <td class="input">
            <%= ViewData[CommonDataKey.CTL_DISPLAY_NAME]%>
            <input id="UpdateDate" name="UpdateDate" type="hidden" value='<%= ViewData[CommonDataKey.CTL_UPDATE_DATE] %>' />
        </td>        
    </tr>
    <%
        List<sp_GetEssayInfoByCandidateExamIDResult> writingInfos = (List<sp_GetEssayInfoByCandidateExamIDResult>)ViewData[CommonDataKey.CTL_WRITING_INFOS];
        for (int i = 0; i < writingInfos.Count; i++)
        {
    %>
    <tr>
        <td width="200" class="label">Question <%= (i+1) %></td>
        <td class="input">
            <textarea cols="20" style="height:190px;width: 460px;" readonly="readonly" rows="12"><%= HttpUtility.HtmlEncode(writingInfos[i].QuestionContent) %></textarea></td>
    </tr>
    <tr>
        <td class="label">Answer</td>
        <td class="input">
            <textarea cols="20" style="height:190px;width: 460px;" readonly="readonly" rows="12"><%if(writingInfos[i].Essay!=null)
            {
                Response.Write(HttpUtility.HtmlEncode(writingInfos[i].Essay));
            }
        %></textarea>
        </td>
    </tr>
    <% } %>   
    <tr>
        <td class="label">
            Mark</td>
        <td valign="bottom" class="input">
            <input name="ProgramingMark" type="text" style="width: 50px; text-align: right;" maxlength="3" value='<%= ViewData[CommonDataKey.CTL_PROGRAMING_MARK] %>'/>
            /<%= ViewData[CommonDataKey.CTL_MAX_PROGRAMING_MARK]%></td>
    </tr>
    <tr>
    <td class="label">Comment    
    </td>
    <td class="input">
        <textarea name="ProgramingComment" cols="20" rows="4" style="height:75px;width: 460px;"><%= ViewData[CommonDataKey.CTL_PROGRAMING_COMMENT]%></textarea></td>
    </tr>
    <tr>        
        <td colspan="2" align="center">
            <input type="submit" class="save" value="" />
            <input type="button" class="cancel" value="" onclick="CRM.closePopup()" />
        </td>
    </tr>
</table>
</div>
<%} %>
</div>