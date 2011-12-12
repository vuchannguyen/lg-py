<%@ Page Title="" Language="C#" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<script type="text/javascript">
    $(function () {
        CRM.summary('', 'none', '');
        $("#ajaxUploadForm").ajaxForm({
            iframe: true,
            dataType: "json",
            success: function (result) {
                if (result.msg.MsgType == 1) {
                    CRM.summary(result.msg.MsgText, 'block', 'msgError');
                }
                else {
                    CRM.message('Upload CV successfully.', 'block', 'msgSuccess');
                    CRM.setUploadCV(result.msg.Holders);
                    CRM.closePopup();
                }
            }
        });
    });
</script>
<form id="ajaxUploadForm" action="<%= Url.Action("UploadFile", "Common")%>" method="post"
enctype="multipart/form-data">
<table width="100%" border="0" cellpadding="0" cellspacing="0" class="form">
    <tr>
        <td class="required" style="width: 320px">
            Select Document to Upload <span>*</span>
        </td>
        <td style="width: 150px">
            <input type="file" name="file" runat="server" />
        </td>
    </tr>
    <tr>
        <td colspan="2" align="center">
            <input type="submit" class="save" value="" alt="" />
            <input type="button" class="cancel" value="" alt="" onclick="CRM.closePopup()" />
        </td>
    </tr>
</table>
<%--<%=Html.Hidden("empId", ViewData["EmpId"] as string)%>
<%=Html.Hidden("updateDate", ViewData["UpdateDate"] as string)%>--%>
<%=Html.Hidden("path", Request.QueryString["saveTo"] )%>
<%=Html.Hidden("recordId", Request.QueryString["recordID"])%>
<%=Html.Hidden("controller", Request.QueryString["controller"])%>
<%=Html.Hidden("value", Request.QueryString["value"])%>
</form>
