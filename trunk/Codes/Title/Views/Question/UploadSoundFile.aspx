<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>UploadSoundFile</title>
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
                        CRM.message(result.msg.MsgText
                            .substring('<%=Constants.UNIQUEID_STRING_FORMAT%>'.length + 1), 'block', 'msgSuccess');
                        $("#filename").html(result.msg.MsgText.split(' ')[0]
                            .substring('<%=Constants.UNIQUEID_STRING_FORMAT%>'.length + 1));
                        $("#fullFileName").val('<%=Constants.UPLOAD_TEMP_PATH%>' 
                            + result.msg.MsgText.split(' ')[0]);
                        $("#btnUpload").css("display", "none");
                        $("#btnPlay").css("display", "inline-block");
                        $("#btnRemoveFile").css("display", "inline-block");
                        CRM.closePopup();
                    }
                }
            });
        });
    </script>
</head>
<body>
    <div>
        <div id="summary" style="display: none" class="">
        </div>
        <form id="ajaxUploadForm" action="<%= Url.Action("UploadSoundFile","Question" )%>"
        method="post" enctype="multipart/form-data">
        <table width="100%" border="0" cellpadding="0" cellspacing="0" class="form">
            <tr>
                <td class="required" style="width: 320px">
                    Select a MP3 file to Upload <span>(*)</span>
                </td>
                <td style="width: 150px">
                    <input type="file" name="file" />
                </td>
            </tr>
            <tr>
                <td colspan="2" align="center">
                    <input type="submit" class="save" value="" alt="" />
                    <input type="button" class="cancel" value="" alt="" onclick="CRM.closePopup()" />
                </td>
            </tr>
        </table>
        </form>
    </div>
</body>
</html>
