<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <h2 class="heading"><%= ViewData["title"]%></h2>
    <br />
    <ul class="materiallist">
    <% List<sp_TC_GetMaterialListResult> list = (List<sp_TC_GetMaterialListResult>)ViewData.Model;
       int type = ConvertUtil.ConvertToInt(RouteData.Values["id"]);
    %>
    <% if (list != null && list.Count > 0)
       {
           
           foreach (sp_TC_GetMaterialListResult item in list)
           {
               string key = type != Constants.TRAINING_CENTER_MATERIAL_SEARCH_TYPE_CATEGORY ? 
                   item.ID.ToString() : HttpUtility.HtmlEncode(item.Name);
            %>
            <li>
            <a href="/TrainingMaterial/SubList?type=<%= ViewData["type"] %>&key=<%=key%>"><%= Server.HtmlEncode(item.Name).Replace("\r\n", "<br>") %>
            <span class="num">[ <%= item.amount %>]</span></a>           
            </li>
        <% } %>
       <% } %>
       </ul>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="PageTitle" runat="server">
<%= TrainingMaterialPageInfo.ComName + CommonPageInfo.AppSepChar + TrainingMaterialPageInfo.FuncMaterialList + 
        CommonPageInfo.AppSepChar + CommonPageInfo.AppName%>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="ModuleName" runat="server">
<%= TrainingMaterialPageInfo.ComName%>
</asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="FunctionTitle" runat="server">
<%int nType = ConvertUtil.ConvertToInt(RouteData.Values["id"]); %>
<%=CommonFunc.GetCurrentMenu(Url.Action("Index", "TrainingCenterAdmin"), false) + TrainingCenterPageInfo.FuncMaterial + CommonPageInfo.AppDetailSepChar +
        (nType == Constants.TRAINING_CENTER_MATERIAL_SEARCH_TYPE_PRO_COURSE ?
            TrainingCenterPageInfo.FuncChildPro : nType == Constants.TRAINING_CENTER_MATERIAL_SEARCH_TYPE_ENGLISH_COURSE ?
        TrainingCenterPageInfo.FuncChildEng : TrainingCenterPageInfo.FuncChildCat)%>
<%--<%=CommonFunc.GetCurrentMenu(Request.RawUrl).Trim().TrimEnd('»')%>--%>
</asp:Content>

<asp:Content ID="Content6" ContentPlaceHolderID="LoginRoles" runat="server">
</asp:Content>
