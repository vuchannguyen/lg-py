<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<script type="text/javascript">
    jQuery(document).ready(function () {
        $("#WFResolutionID").change(function () {
            var firstItem = '<%= ViewData["FirstChoiceStatus"]%>';
            $("#WFStatus").html("");
            var resolutionId = $("#WFResolutionID").val();
            if (firstItem != '' && resolutionId == 0) {
                $("#WFStatus").append($("<option value=''>" + firstItem + "</option>"));
            }
            if (resolutionId != 0) {

                $.getJSON('/Library/GenericHandle/DropdownListHandler.ashx?ID=' + resolutionId + '&Page=Status', function (item) {
                    $.each(item, function () {
                        if (this['ID'] != undefined) {
                            $("#WFStatus").append($("<option value='" + this['ID'] + "'>" + this['Name'] + "</option>"));
                        }

                    });
                });
            }

            //$("#assignTr").attr("style", "display:none"); //set default for assign list just dislay when have data
            firstItem = '<%= ViewData["FirstChoiceAssign"]%>';            
            if (firstItem != '' && resolutionId == 0) {                
                $("#assignTr").attr("style", "display:");
            }
            if (resolutionId == '<%=Constants.PRW_RESOLUTION_CANCEL %>' || resolutionId == '<%=Constants.PRW_RESOLUTION_COMPLETE_ID %>') {
                $("#assignTr").css("display", "none");
            }
            else {
                if (resolutionId != 0) {                    
                        $("#assignTr").css("display", "");                    
                }
            }
        });
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
                Contents : {required: true, maxlength:500},
                Assign: { required: true }
            }
        });

    });
</script>
<%using (Html.BeginForm("ClosePR", "PerformanceReviewHR", FormMethod.Post, new { id = "editForm" }))
  {%>
  <%
      PerformanceReview jr = (PerformanceReview)ViewData.Model;
      
      Response.Write(Html.Hidden("ID", jr.ID));
      Response.Write(Html.Hidden("UpdateDate", jr.UpdateDate.ToString()));
  %>
<table width="100%" border="0" cellpadding="0" cellspacing="0" class="form">
    
    <tr>
        <td class="label required" style="width: 130px">
            Comment <span>*</span>
        </td>
        <td colspan=3>
            <% Response.Write(Html.TextArea("Contents", "", 2, 266, new { @style = "width:436px;maxlength:500" })); %>
        </td>                
    </tr>
    <tr>
        <td class="label required" style="width: 130px">
            Resolution <span>*</span>
        </td>
        <td>
            <% Response.Write(Html.DropDownList(CommonDataKey.PER_REVIEW_RESOLUTION, null, new { @style = "width:136px" })); %>
        </td>        
        <td class="label required" style="width: 130px">
            Status <span>*</span>
        </td>
        <td>
            <% Response.Write(Html.DropDownList(CommonDataKey.PER_REVIEW_STATUS, null, new { @style = "width:136px" })); %>
        </td>        
    </tr>
    <tr id="assignTr">
        <td class="label required">
            Forward To <span>*</span>
        </td>
        <td colspan=3>
            <% Response.Write(Html.DropDownList(CommonDataKey.PER_REVIEW_ASSIGN, null, new { @style = "width:136px" })); %>
        </td>        
        
    </tr>
    <tr>
        <td colspan="4" align="center">
            <input type="submit" class="save" value="" alt="Save" />
            <input type="button" class="cancel" value="" alt="Cancel" onclick="CRM.closePopup()" />
        </td>
    </tr>
</table>
<% } %>
