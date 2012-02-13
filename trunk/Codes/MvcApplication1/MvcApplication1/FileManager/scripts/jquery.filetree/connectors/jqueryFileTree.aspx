<%@ Page Language="C#" AutoEventWireup="true" %>
<%  
      HttpCookie cookie = Request.RequestContext.HttpContext.Request.Cookies[Constants.COOKIE_CRM];

    if (cookie != null)
    {
        FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(cookie.Value);
        FormsIdentity identity = new FormsIdentity(ticket);
        UserData udata = UserData.CreateUserData(ticket.UserData);
        AuthenticationProjectPrincipal principal = new AuthenticationProjectPrincipal(identity, udata);

        if (CommonFunc.CheckAuthorized(principal.UserData.UserID, (int)Modules.Question, (int)Permissions.Read))
        {
            string dir;
            if (Request.Form["dir"] == null || Request.Form["dir"].Length <= 0)
                dir = "/";
            else
                dir = Request.Form["dir"];

            string newUrl = dir;

            while ((newUrl = Uri.UnescapeDataString(dir)) != dir)
            { 
                dir = newUrl;
            }
            bool test = Page.IsPostBack;
            if (dir != "Empty")
            {
                string fullPhysicalPath = Server.MapPath(dir);

                System.IO.DirectoryInfo di = new System.IO.DirectoryInfo(fullPhysicalPath);
                
                Response.Write("<ul class=\"jqueryFileTree\" id=\"Parent\" style=\"display: inline;\">\n");
                if (di.FullName.EndsWith("LOT\\") && Session["header"] == null)
                {
                    Response.Write("\t<li class=\"directory collapsed\"><a href=\"#\" rel=\"" + dir + "\">" + di.Name + "</a></li>\n");
                     Session["header"] = true;
                }
                else
                {
                 
                    foreach (System.IO.DirectoryInfo di_child in di.GetDirectories())
                    {
                        Response.Write("\t<li class=\"directory collapsed\"><a href=\"#\" rel=\"" + dir + di_child.Name + "/\">" + di_child.Name + "</a></li>\n");
                    }
                }
                
                Response.Write("</ul>");
            }
            else
            {
                Response.Write("No Directory");
            }
        }
        else
        {
            Response.Write("No Permission");
        }
    }
    else
    {
        Response.Write("Please login and try again");
    }
       %>
       <script type="text/javascript">
           $("#Test").css("display", "none");
           $(document).ready(function () {
               $(document).keypress(function (e) {
                   if (e.keyCode == 116 || e.keyCode == '116') {
                       return false;
                   }

               });

           });
 </script>