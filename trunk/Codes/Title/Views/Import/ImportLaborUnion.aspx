<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
 <%=TempData[CommonDataKey.TEMP_SYSTEM_MESSAGE] %>
<fieldset>
        <legend>Import Labor Union/Contract</legend>
        <form id="employeeForm" class="form" action="<%= Url.Action("ImportLaborUnion", "Import")%>" method="post"
        enctype="multipart/form-data">
        <div>
            File: <input type="file" id="file" name="file" />
        </div>
        <br />
        <div>
            <table class="edit">
                <tr>
                    <th colspan="6" align="left">Options: (value in parentheses is default if field is empty)</th>
                </tr>
                <tr>
                    <td class="label">Start Row</td>
                    <td class="input">
                        <input type="text" name="startRow" style="width:50px"/>(4)
                    </td>
                    <td class="label">End Row</td>
                    <td class="input">
                        <input type="text" name="endRow" style="width:50px"/>(500)
                    </td>
                    <td class="label">Sheet Index</td>
                    <td class="input">
                        <input type="text" name="sheetIndex" style="width:50px"/>(1)
                    </td>
                </tr>
                <tr>
                    <td class="label">Items to import</td>
                </tr>
                <tr>
                    <td align="right">
                        <input type="checkbox" checked="checked" name="ckbLaborUnion" />
                    </td>
                    <td align="left">
                        Labor union
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        <input type="checkbox" checked="checked" name="ckbContract" />
                    </td>
                    <td align="left">
                        Contract
                    </td>
                </tr>
            </table>
        </div>
        <div class="cbutton" style="text-align: left">
            <input type="submit" title="Submit" class="submit" value="" />
        </div>
        
        </form>
    </fieldset>
<%
    if (ViewData["SUCCESS"] != null && ViewData["NOTSUCCESS"] != null)
    {
        List<string> success = ViewData["SUCCESS"] as List<string>;
        List<string> notSuccess = ViewData["NOTSUCCESS"] as List<string>;
%>
    <table border="1" width="600px">
        <tr>
            <td>SUCCESS: <%= success.Count%></td>
            <td>NOT SUCCESS: <%= notSuccess.Count%></td>
        </tr>
        <tr>
            <td valign="top">
                <%
                foreach (string id in success)
                {
                    Response.Write(id + "<br/>");
                }
                            %>
                        </td>
                        <td valign="top">
                            <%
                foreach (string id in notSuccess)
                {
                    Response.Write(id + "<br/>");
                }
                %>
            </td>
        </tr>
    </table>
<%
    }
 %>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="PageTitle" runat="server">
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="ModuleName" runat="server">
</asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="FunctionTitle" runat="server">
</asp:Content>

<asp:Content ID="Content6" ContentPlaceHolderID="LoginRoles" runat="server">
</asp:Content>
