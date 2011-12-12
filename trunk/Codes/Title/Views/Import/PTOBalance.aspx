<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
<%=TempData[CommonDataKey.TEMP_SYSTEM_MESSAGE] %>
<fieldset>
        <legend>Import Project, Manager, Seat code, Floor</legend>
        <form id="employeeForm" class="form" action="<%= Url.Action("PTOBalance", "Import")%>" method="post"
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
                    <td class="label">Report Month</td>
                    <td class="input">
                        <input type="text" name="reportMonth" style="width:50px" value="9"/>
                    </td>
                    <td class="label">Report Year</td>
                    <td class="input">
                        <input type="text" name="reportYear" style="width:50px" value="2011"/>
                    </td>
                </tr>
                <tr>
                    <td>Probation Ids</td>
                    <td colspan="5">
                        <input type="text" name="probationId" style="width:100%" 
                        value="1884,1885,1887,1888,1889,1890,1891,1892,1893,1895,5044,5052,1884,1885,5052,1876,1877,1895,1894,5056,5058,5057,5059,5048,5046,5061,5060,5047,1896,1897,1898,1899,1900,1901,1902,1903,1904,1905,1906"/>
                    </td>
                </tr>
                <tr>
                    <td class="label">Start Row</td>
                    <td class="input">
                        <input type="text" name="startRow" style="width:50px" value="3"/>
                    </td>
                    <td class="label">End Row</td>
                    <td class="input">
                        <input type="text" name="endRow" style="width:50px" value="500"/>
                    </td>
                    <td class="label">Sheet Index</td>
                    <td class="input">
                        <input type="text" name="sheetIndex" style="width:50px" value="1"/>
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
<script type="text/javascript">
    $(document).ready(function () {
        $("form").submit(function () {
            CRM.loading();
        });
    });
</script>
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="ModuleName" runat="server">
</asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="FunctionTitle" runat="server">
</asp:Content>

<asp:Content ID="Content6" ContentPlaceHolderID="LoginRoles" runat="server">
</asp:Content>
