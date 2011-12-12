<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<div style="background-color:#EEEEEE">
<%using (Html.BeginForm("GetApproval", "ServiceRequestAdmin", FormMethod.Post,
      new { @id = "frmGetApproval", @class = "form" }))
  {
      SR_ServiceRequest sr = ViewData.Model as SR_ServiceRequest;
      string manager = "";
      try
      {
          manager = CommonFunc.GetUserNameLoginByEmpID(CommonFunc.GetEmployeeByUserName(sr.RequestUser).ManagerId);
      }
      catch
      {
          manager = ""; 
      }
    %>
    <%=Html.Hidden("srId", Page.RouteData.Values["id"]) %>
    <%=Html.Hidden("hidCallerPage", Request.UrlReferrer) %>
    <table class="edit" width="100%">
        <tr>
            <td class="label required" style="vertical-align:text-top">
                Manager<span>*</span>
            </td>
            <td class="input">
                <%=Html.TextBox("Manager", manager, new { @style="width:150px"} )%>
            </td>
        </tr>
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
        <input class="save" type="submit" alt="" value="" />
        <input class="cancel" type="button" onclick="CRM.closePopup();" alt="" value="" />
    </div>
<%} %>
</div>
<script type="text/javascript">
    $(document).ready(function () {
        $("#Manager").autocomplete('/Library/GenericHandle/AutoCompleteHandler.ashx/?Page=ManagerWithID', 
            { employee: true });
        $("#frmGetApproval").validate({
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
                Manager: {  required: true,
                            remote: {
                                url: '<%=Url.Action("CheckMangerExisted") %>',
                                type: "post",
                                data: {
                                    name: function(){ return $("#Manager").val(); }
                                }
                            }
                },
                Comment: { required: true, maxlength: parseInt('<%=CommonFunc.GetLengthLimit(new SR_Comment(), "Contents")%>') }
            }
        });
        $("#frmGetApproval").submit(function () {
            if ($(this).valid())
                $("input[type='submit']").attr("disabled", "disabled");
        });
    });
</script>
