<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<div style="background-color:#EEEEEE">
<%using (Html.BeginForm("Close", "ServiceRequestAdmin", FormMethod.Post,
      new { @id = "frmComment", @class = "form" }))
  { %>
    <%=Html.Hidden("srId", Page.RouteData.Values["id"]) %>
    <%=Html.Hidden("hidCallerPage", Request.UrlReferrer) %>
    <table class="edit" width="100%">
        <tr>
            <td class="label required" style="vertical-align:text-top">
                Comment<span>*</span>
            </td>
            <td class="input">
                <%=Html.TextArea("Comment", new { @style="width:85%; height: 100px"})%>
            </td>
        </tr>
    </table>
    <div style="text-align:center">
        <input class="close" type="submit" alt="" value="" />
        <input class="cancel" type="button" onclick="CRM.closePopup();" alt="" value="" />
    </div>
<%} %>
</div>
<script type="text/javascript">
    $(document).ready(function () {
        $("#frmComment").validate({
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
                Comment: { required: true }
            }
        });
        $("#frmComment").submit(function () {
            if ($(this).valid())
                $("input[type='submit']").attr("disabled", "disabled");
        });
    });
</script>