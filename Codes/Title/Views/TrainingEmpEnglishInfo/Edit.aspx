﻿<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>
<%@ Register src="UCInfo.ascx" tagname="UCInfo" tagprefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Edit</title>
</head>
<body>
    <div>
    <%using (Html.BeginForm("Edit", "TrainingEmpEnglishInfo", FormMethod.Post, new { @id="frmEnglishInfo", @class="form"}))
      { %>
        <uc1:UCInfo ID="UCInfo1" runat="server" />
    <%} %>
    </div>
</body>
</html>