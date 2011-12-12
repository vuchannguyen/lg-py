<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<%@ Register Src="UCResult.ascx" TagName="UCResult" TagPrefix="uc1" %>
<script type="text/javascript">   
    $(document).ready(function () {
        $("#EditResultInfo").validate({
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
                ResultId: { required: true },
                Enddate: { required: true, checkDate: true }
            }
        });
    });
</script>
<%using (Html.BeginForm("EditResultInfo", "STT", FormMethod.Post, new { @id = "EditResultInfo", @class = "form", @enctype = "multipart/form-data" }))
  { %>
<% STT_RefResult stt = (STT_RefResult)ViewData.Model;%>
<%= Html.Hidden("ID", stt.Id)%>
<%= Html.Hidden("SttID", stt.SttID)%>
<uc1:UCResult ID="UCResult" runat="server" />
<% } %>
