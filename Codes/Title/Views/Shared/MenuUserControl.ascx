<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<ul id="mmenu" style="margin-left: 10px;">
<%    
    CRM.Models.MenuDao menuDao = new MenuDao();
    string result = string.Empty;
    List<CRM.Models.Menu> menuList = menuDao.GetList();
    List<CRM.Models.Menu> menuNotChild = menuDao.GetNotChildList(menuList);
    foreach (CRM.Models.Menu menu in menuNotChild)
    {
        result += ExtensionMethods.RecursionMenu(menuList, menu);
    }
    Response.Write(result);    
%>
</ul>