<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<%@ Register Src="UCInfo.ascx" TagName="UCInfo" TagPrefix="uc1" %>
<form id="settingForm" class="form" action="<%= Url.Action("Create", "ServiceRequestSetting")%>"
method="post" enctype="multipart/form-data">
<uc1:UCInfo ID="UCInfo" runat="server" />
</form>
