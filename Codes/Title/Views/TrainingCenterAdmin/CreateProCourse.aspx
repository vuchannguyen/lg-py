<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<%@ Register src="UCInfo.ascx" tagname="UCInfo" tagprefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>CreateProCourse</title>
</head>
<body>
    <%using (Html.BeginForm("CreateCourse", "TrainingCenterAdmin", FormMethod.Post, new { @id = "frmCourse", @class = "form" }))
      { %>
        <uc1:UCInfo ID="UCInfo1" runat="server" />
    <%} %>
</body>
</html>
