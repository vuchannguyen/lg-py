<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>
<%@ Register Src="UCInfo.ascx" TagName="UCInfo" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Create</title>
</head>
<body>
    <div>
    <%  using (Html.BeginForm("Create", "AnnualHoliday", FormMethod.Post, 
            new { id = "holidayForm", @class = "form" }))
        {%>
        <uc1:UCInfo ID="UCInfo1" runat="server" />
    <%  } %>
    </div>
</body>
</html>
