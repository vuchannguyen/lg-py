<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>
<script type="text/javascript">
    function showInputMark(){
        var markType = $("#<%=CommonDataKey.LOT_VERBAL_MARK_TYPE%>").val();
        var markTextbox = $("#<%=CommonDataKey.LOT_VERBAL_MARK%>");
        var markList = $("#<%=CommonDataKey.LOT_VERBAL_LEVEL_LIST%>");
        if (markType != "") {
            if (markType == 0) {
                $(markTextbox).attr("disabled", "disabled");
                $(markTextbox).hide();
                $(markList).removeAttr("disabled");
                $(markList).show();
            }
            else{
                $(markTextbox).removeAttr("disabled");
                $(markTextbox).show();
                $(markList).attr("disabled", "disabled");
                $(markList).hide();
            }
        }
        else{
            $(markTextbox).attr("disabled", "disabled");
            $(markTextbox).hide();
            $(markList).attr("disabled", "disabled");
        }
    }
    $(document).ready(function () {
        $("#<%=CommonDataKey.LOT_VERBAL_TESTED_BY%>").autocomplete('/Library/GenericHandle/AutoCompleteHandler.ashx/?Page=UserAdmin', {employee:true});
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
                '<%=CommonDataKey.LOT_VERBAL_MARK%>': {
                    required: true,
                    number: true,
                    min: 0,
                    //max: '<%=Constants.LOT_VERBAL_MAX_MARK%>'
                    maxlength: 3
                },
                '<%=CommonDataKey.LOT_VERBAL_COMMENT%>': {
                    maxlength: 255
                },
                '<%=CommonDataKey.LOT_VERBAL_MARK_TYPE%>': {
                    required: true
                },
                '<%=CommonDataKey.LOT_VERBAL_LEVEL_LIST%>': {
                    required: true
                },
                '<%=CommonDataKey.LOT_VERBAL_TESTED_BY%>': {
                    required: true,
                    maxlength: 100,
                    remote: {
                        url: "/Exam/CheckNameExists",
                        type: "post",
                        data: {
                            name: function () {
                                return $("#<%=CommonDataKey.LOT_VERBAL_TESTED_BY%>").val();
                            }
                        }
                    }
                }
            }
        });
        showInputMark();
        $("#<%=CommonDataKey.LOT_VERBAL_MARK_TYPE%>").change(function () {
            showInputMark();
        });
    });
</script>
<%using (Html.BeginForm("InputVerbalMark", "Exam", FormMethod.Post, new { id = "ExamForm", @class = "form" }))
  { %>  
<div class="edit">
<%=Html.Hidden("CandidateExamId", RouteData.Values["id"]) %>
<%=Html.Hidden(CommonDataKey.LOT_VERBAL_UPDATE_DATE)%>

<table width="100%" border="0" cellpadding="0" cellspacing="0">
    <tr>
        <td class="label required">
            Type<span>*</span>
        </td>
        <td class="input" style="width:130px">
            <%=Html.DropDownList(CommonDataKey.LOT_VERBAL_MARK_TYPE, null, Constants.LOT_VERBAL_MARK_TYPE_LABEL, new { width="100px"})%>
        </td>
        <td class="label required" style="width:70px">
            Mark<span>*</span>
        </td>
        <td class="input">
            <%=Html.DropDownList(CommonDataKey.LOT_VERBAL_LEVEL_LIST, null, Constants.LOT_VERBAL_LEVEL_LABEL, new { @style = "width:85px" })%>
            <%=Html.TextBox(CommonDataKey.LOT_VERBAL_MARK, null, new { @style="width:78px"})%>
        </td>
    </tr>
    <tr>
        <td class="label required">
            Tested by<span>*</span>
        </td>
        <td class="input" colspan="3">
            <%=Html.TextBox(CommonDataKey.LOT_VERBAL_TESTED_BY, null, new { @style="width:200px"})%>
        </td>
    </tr>
    <tr>
        <td class="label">
            Comments
        </td>
        <td class="input" colspan="3">
            <%=Html.TextArea(CommonDataKey.LOT_VERBAL_COMMENT, null, new { @style = "width:300px;height:50px" })%>
        </td>
    </tr>
    <tr>        
        <td colspan="4" align="center">
            <input type="submit" class="save" value="" />
            <input type="button" class="cancel" value="" onclick="CRM.closePopup()" />
        </td>
    </tr>
</table>
</div>
<%} %>