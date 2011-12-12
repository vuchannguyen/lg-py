<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<script type="text/javascript">
    isSubmited = false;
    $(document).ready(function () {
        $.validator.addMethod('integer', function (value, element, param) {
            if ($.trim(value) == '') {
                return true;
            }
            else {
                return (value == parseInt(value, 10));
            }
        }, E0037);
        $("#frmCategory").validate({
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
                Name: { required: true },
                DisplayOrder: { integer: true, maxlength: 3 }
            },
            submitHandler: function (form) {
                formSubmitHandler(form);
            }
        });
        $("#DisplayOrder").keydown(function (e) {
            var key = e.charCode || e.keyCode || 0;
            // allow backspace, tab, delete, arrows, numbers and keypad numbers ONLY
            return (
                key == 8 ||
                key == 9 ||
                key == 46 ||
                (key >= 37 && key <= 40) ||
                (key >= 48 && key <= 57) ||
                (key >= 96 && key <= 105));
        })
    });
</script>
<%=Html.Hidden("UpdateDate")%>
<table style="width: 100%;" class="edit">
    <tr>
        <td class="label required">
            Name<span>*</span>
        </td>
        <td class="input" colspan="3">
            <%=Html.TextBox("Name", null, new { @style="width:230px"})%>
        </td>
    </tr>
   
    <tr>
        <td class="label">
            Category
        </td>
        <td class="input" colspan="3">
            <%=Html.DropDownList(CommonDataKey.SR_CATEGORY_LIST, null, Constants.SR_LIST_CATEGORY_LABEL,new { @style = "width:235px" })%>
        </td>
    </tr>
     <tr>
        <td class="label">
            Display Order
        </td>
        <td class="input" style="width:70px">
            <%=Html.TextBox("DisplayOrder", null, new { @style = "width:40px" })%>
        </td>
        <td class="label">
            Active
        </td>
        <td class="input">
            <%=Html.CheckBox("IsActive", ViewData.Model == null ? true : (ViewData.Model as SR_Category).IsActive)%>
        </td>
    </tr>
    <tr>
        <td class="label" style="vertical-align:top">
            Description
        </td>
        <td class="input" colspan="3">
            <%=Html.TextArea("Description", null, new { @style = "width:230px; height:50px" })%>
        </td>
    </tr>
    <tr>
        <td colspan="4" style="text-align:center; vertical-align:bottom; padding-top:20px">
            <input class="save" type="submit" value="" alt="" />
            <input class="cancel" type="button" value="" alt="" onclick="CRM.closePopup();" />
        </td>
    </tr>
</table>
