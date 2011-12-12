<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<table class="form" width="100%">
    <tr height="20px">
        <td align="left" style="width: 120px">
            Time range to clear:
        </td>
        <td align="left">
            <%
                Response.Write(Html.DropDownList("TimeToClearLog", ViewData["TimeToClearLog"] as SelectList, Constants.FIRST_ITEM, new { @style = "width:160px" }));
            %>
        </td>
    </tr>
    <tr height="40px" align="center" valign="middle">
        <td colspan="2">
            <%
                Response.Write(Html.Hidden("hiddenType", ViewData["LogType"]));
            %>
            <input id="btnClearLog" type="submit" class="btnClear" value="" alt="" />
            <input type="button" class="cancel" value="" alt="" onclick="CRM.closePopup()" />
        </td>
    </tr>
</table>
