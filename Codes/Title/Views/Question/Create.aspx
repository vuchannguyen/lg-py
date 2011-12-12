﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" 
    Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<%@ Register Src="UCInfo.ascx" TagName="UCInfo" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <%using (Html.BeginForm("Create", "Question", FormMethod.Post, 
          new { id = "questionForm", @class = "form" }))
      {%>
    <uc1:UCInfo ID="UCInfo1" runat="server" />
    <% } %>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PageTitle" runat="server">
    <%= LOTPageInfo.MenuName + CommonPageInfo.AppSepChar+ LOTPageInfo.AddNewQuestion +CommonPageInfo.AppSepChar
                           + LOTPageInfo.ComName + CommonPageInfo.AppSepChar + CommonPageInfo.AppName
        %>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ModuleName" runat="server">
    <% Response.Write(LOTPageInfo.ComName); %>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="FunctionTitle" runat="server">
<%--<%=LOTPageInfo.MenuName + CommonPageInfo.AppDetailSepChar +
    "<a href='/Question'>" + LOTPageInfo.Question + "</a>" + CommonPageInfo.AppDetailSepChar +LOTPageInfo.AddNewQuestion
     %>--%>
     <%=CommonFunc.GetCurrentMenu(Request.RawUrl) + LOTPageInfo.AddNewQuestion
     %>
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="LoginRoles" runat="server">
</asp:Content>
