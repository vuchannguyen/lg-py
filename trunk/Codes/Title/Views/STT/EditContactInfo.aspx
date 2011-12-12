<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<script type="text/javascript">
    $(document).ready(function () {
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
                PersonalEmail: {
                    email: true
                },
                OfficeEmail: {
                    email: true
                }
            },
            submitHandler: function (form) {
                if ($("#OfficeEmail").val() != "") {
                    jQuery.ajax({
                        url: "/Employee/CheckEmailExits",
                        type: "POST",
                        datatype: "json",
                        data: ({
                            'email': $('#OfficeEmail').val(),
                            'id': $('#ID').val()
                        }),
                        success: function (mess) {
                            if (mess.MsgType == 1) {
                                CRM.summary(mess.MsgText, 'block', 'msgError');
                                return false;
                            }
                            else {
                                form.submit();
                            }
                        }
                    })
                }
                else {
                    form.submit(); ;
                }
            }
        });
    });
</script>
<%using (Html.BeginForm("EditContactInfo", "STT", FormMethod.Post, new { @id = "editForm", @class = "form" }))
  { %>
<% STT emp = (STT)ViewData.Model;%>
<table cellspacing="0" cellpadding="0" border="0" width="100%">
    <tbody>
        <tr>
            <td valign="top" class="ccbox">
                <%=Html.Hidden("ID", emp.ID)%>
                <%=Html.Hidden("UpdateDate", emp.UpdateDate.ToString())%>
                <table cellspacing="0" cellpadding="0" border="0" width="100%" class="edit">
                    <tbody>
                        <tr>
                            <td width="110px" class="label">
                                Home Phone
                            </td>
                            <td class="input">
                                <%=Html.TextBox("HomePhone", emp.HomePhone, new { @Style = "width: 130px", @maxlength = "20" })%>
                            </td>
                        </tr>
                        <tr>
                            <td width="110px" class="label">
                                Cell Phone
                            </td>
                            <td class="input">
                                <%=Html.TextBox("CellPhone", emp.CellPhone, new { @Style = "width: 130px", @maxlength = "20" })%>
                            </td>
                        </tr>
                        <tr>
                            <td width="110px" class="label">
                                Ext Number
                            </td>
                            <td class="input">
                                <%=Html.TextBox("ExtensionNumber", emp.ExtensionNumber, new { @Style = "width: 130px", @maxlength = "10" })%>
                            </td>
                        </tr>
                        <tr>
                            <td width="110px" class="label">
                                SkypeID
                            </td>
                            <td class="input">
                                <%=Html.TextBox("SkypeId", emp.SkypeId, new { @Style = "width: 130px", @maxlength = "50" })%>
                            </td>
                        </tr>
                        <tr>
                            <td width="110px" class="label">
                                YahooID
                            </td>
                            <td class="input">
                                <%=Html.TextBox("YahooId", emp.YahooId, new { @Style = "width: 130px", @maxlength = "50" })%>
                            </td>
                        </tr>
                        <tr>
                            <td width="110px" class="label">
                                Personal Email
                            </td>
                            <td class="input">
                                <%=Html.TextBox("PersonalEmail", emp.PersonalEmail, new { @Style = "width: 130px", @maxlength = "50" })%>
                            </td>
                        </tr>
                        <tr>
                            <td width="110px" class="label required">
                                Office Email <span>*</span>
                            </td>
                            <td class="input">
                                <%=Html.TextBox("OfficeEmail", emp.OfficeEmail, new { @Style = "width: 130px", @maxlength = "50" })%>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" class="label" style="text-align:left">
                                <b>Emergency Contact</b>
                            </td>
                        </tr>
                        <tr>
                            <td width="110px" class="label">
                                Contact Name
                            </td>
                            <td class="input">
                                <%=Html.TextBox("EmergencyContactName", emp.EmergencyContactName, new { @Style = "width: 130px", @maxlength = "50" })%>
                            </td>
                        </tr>
                        <tr>
                            <td width="110px" class="label">
                                Phone
                            </td>
                            <td class="input">
                                <%=Html.TextBox("EmergencyContactPhone", emp.EmergencyContactPhone, new { @Style = "width: 130px", @maxlength = "20" })%>
                            </td>
                        </tr>
                        <tr>
                            <td width="110px" class="label">
                                Relationship
                            </td>
                            <td class="input">
                                <%=Html.TextBox("EmergencyContactRelationship", emp.EmergencyContactRelationship, new { @Style = "width: 130px", @maxlength = "50" })%>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" align="center" valign="middle" class="cbutton">
                                <input type="submit" class="save" value="" alt="" />
                                <input type="button" class="cancel" value="" alt="" onclick="CRM.closePopup()" />
                            </td>
                        </tr>
                    </tbody>
                </table>
            </td>
        </tr>
    </tbody>
</table>
<% } %>