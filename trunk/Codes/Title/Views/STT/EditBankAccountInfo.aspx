<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<%using (Html.BeginForm("EditBankAccountInfo", "STT", FormMethod.Post, new { @id = "editForm", @class = "form" }))
  { %>
<% STT emp = (STT)ViewData.Model;%>
<table cellspacing="0" cellpadding="0" border="0" width="100%" class="edit">
    <tbody>
        <tr>
            <td class="label" style="text-align:left" class="label">
                Bank Name
            </td>
            <td class="input">
                <%=Html.Hidden("UpdateDate", emp.UpdateDate.ToString())%>
                <%=Html.TextBox("BankName", emp.BankName, new { @Style = "width: 130px", @maxlength = "100" })%>
            </td>
        </tr>
        <tr class="last">
            <td class="label" style="text-align:left" class="label">
                Bank Account
            </td>
            <td class="input">
                <%=Html.TextBox("BankAccount", emp.BankAccount, new { @Style = "width: 130px", @maxlength = "20" })%>
            </td>
        </tr>
        <tr>
            <td></td>
            <td >
                <input type="submit" class="save" value="" alt="" />
                <input type="button" class="cancel" value="" alt="" onclick="CRM.closePopup()" />
            </td>
        </tr>
    </tbody>
</table>
<% } %>
