<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>
<%@ Register src="UCInfo.ascx" tagname="UCInfo" tagprefix="uc1" %>
<%using (Html.BeginForm("Create", "SRCategory", FormMethod.Post, new { @id = "frmCategory", @class="form" }))
    { %>    
    <uc1:UCInfo ID="UCInfo1" runat="server" />
<%} %>