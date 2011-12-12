<%@ Page Title="" Language="C#" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<%using (Html.BeginForm("EditBankAccountInfo", "Employee", FormMethod.Post, new { @id = "editForm", @class = "form" }))
  { %>
<% Employee emp = (Employee)ViewData.Model;%>
<table cellspacing="0" cellpadding="0" border="0" width="100%">
    <tbody>
        <tr>
            <td valign="top" class="ccbox">
                <table cellspacing="0" cellpadding="0" border="0" width="100%" class="edit">
                    <tbody>
                        <tr>
                            <td width="110px" class="label">
                                Bank Name
                            </td>
                            <td class="input">
                                <%=Html.Hidden("UpdateDate", emp.UpdateDate.ToString())%>
                                <%=Html.TextBox("BankName", emp.BankName, new { @Style = "width: 130px", @maxlength = "100" })%>
                            </td>
                        </tr>
                        <tr>
                            <td width="110px" class="label">
                                Bank Account
                            </td>
                            <td class="input">
                                <%=Html.TextBox("BankAccount", emp.BankAccount, new { @Style = "width: 130px", @maxlength = "20" })%>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" class="cbutton">
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