<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>
<%@ Register src="UCInfo.ascx" tagname="UCInfo" tagprefix="uc1" %>
<%
    using (Html.BeginForm("Edit", "Menu", FormMethod.Post, new { @id="frmMenuForm" }))
    {
%>
    <uc1:UCInfo ID="UCInfo1" runat="server" />
<%  
    } 
%>