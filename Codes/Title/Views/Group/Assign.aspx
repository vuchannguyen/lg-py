<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="PageTitle" runat="server">
<%= AdminAccountPageInfo.MenuName + CommonPageInfo.AppSepChar + AdminAccountPageInfo.SetPermissionForGroup + CommonPageInfo.AppSepChar + AdminAccountPageInfo.ComGroup  + CommonPageInfo.AppSepChar + CommonPageInfo.AppName%>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
    <script type="text/javascript">
        function ToggleAll(checked) {
            var items = document.getElementsByTagName("input");
            for (i = 0; i < items.length; i++) {
                if (items[i].type == "checkbox") {
                    items[i].checked = checked;
                }
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ModuleName" runat="server">
    <%=AdminAccountPageInfo.ComGroup  %>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="FunctionTitle" runat="server"> 
<%=CommonFunc.GetCurrentMenu(Request.RawUrl) +
    ViewData["Title"] + CommonPageInfo.AppDetailSepChar + AdminAccountPageInfo.SetPermissionForGroup
    %>   
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <%using (Html.BeginForm("Assign", "Group", FormMethod.Post, new { id = "assignForm",@class="form" }))
      {%>
    <div>
        <div id="cactionbutton">
            <button type="button" id="btnSave" title="Save" class="button save" onclick="$('#assignForm').submit()">
                Save</button>
        </div>
        <table id="list" class="grid" style="width: 75%">
            <tr>
                <th>
                    Module Name
                </th>
                <th>
                    Permissions
                </th>
            </tr>
            <% foreach (ModuleModel module in (IEnumerable)ViewData.Model)
               {%>
            <tr>
                <td class="label">
                    <%= module.ModuleName%>
                </td>
                <td>
                    <%{
                          if (!string.IsNullOrEmpty(module.PermissionNames))
                          {
                              string[] perNameArr = module.PermissionNames.Split(',');
                              string[] perIdArr = module.PermissionIds.Split(',');
                              for (int i = 0; i < perNameArr.Length; i++)
                              {
                                  if (perIdArr[i].Contains('_'))
                                  {
                                      string permissionId = perIdArr[i].Split('_')[0];

                                      Response.Write(Html.CheckBox(module.ModuleId + "_" + permissionId, true, new { @id = module.ModuleId + "_" + permissionId }) + "&nbsp;");
                                      Response.Write("<label for=\"" + module.ModuleId + "_" + permissionId + "\">" + perNameArr[i] + "</label>&nbsp;&nbsp;&nbsp;&nbsp;");
                                  }
                                  else
                                  {
                                      Response.Write(Html.CheckBox(module.ModuleId + "_" + perIdArr[i], new { @id = module.ModuleId + "_" + perIdArr[i] }) + "&nbsp;");
                                      Response.Write("<label for=\"" + module.ModuleId + "_" + perIdArr[i] + "\">" + perNameArr[i] + "</label>&nbsp;&nbsp;&nbsp;&nbsp;");
                                  }
                              }
                          }
                      }%>
                </td>
            </tr>
            <% } %>
            <tr>
                <td>
                </td>
                <td>
                    <a href="#" onclick="ToggleAll(true)"><strong>Check All</strong></a> / <a href="#"
                        onclick="ToggleAll(false)"><strong>UnCheck All</strong></a>
                </td>
            </tr>
        </table>
    </div>
    <% } %>
</asp:Content>
