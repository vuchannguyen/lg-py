<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<div style="background-color:#EEEEEE">
<%using (Html.BeginForm("AddSolution", "ServiceRequestAdmin", FormMethod.Post,
      new { @id = "frmSolution", @class = "form" }))
  { %>
    <%=Html.Hidden("srId", Page.RouteData.Values["id"]) %>
    <table class="edit" width="100%">
        <tr>
            <td class="label required" style="vertical-align:text-top">
                Solution<span>*</span>
            </td>
            <td class="input">
                <%=Html.TextArea("Solution", new { @style="width:85%; height: 100px"})%>
            </td>
        </tr>
    </table>
    <div style="text-align:center">
        <input class="save" type="submit" alt="" value="" />
        <input class="cancel" type="button" onclick="CRM.closePopup();" alt="" value="" />
    </div>
<%} %>
</div>
<script type="text/javascript">
    $(document).ready(function () {
        $("#frmSolution").validate({
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
                Solution: { required: true, maxlength: parseInt('<%=CommonFunc.GetLengthLimit(new SR_ServiceRequest(), "Solution")%>') }
            }
        });
        $("#frmSolution").submit(function () {
            if ($(this).valid())
                $("input[type='submit']").attr("disabled", "disabled");
        });
    });
</script>
