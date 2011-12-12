<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>
<%@ Register src="UCInfoClass.ascx" tagname="UCInfoClass" tagprefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>EditClass</title>
</head>
<body>
    <div>
    <%using (Html.BeginForm("EditClass", "TrainingCenterAdmin", FormMethod.Post, new { @id = "frmCourse", @class = "form" }))
      { %>
        <uc1:UCInfoClass ID="UCInfo1" runat="server" />
    <%} %>
    </div>
</body>
</html>
