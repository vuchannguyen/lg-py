<%@ Page Title="" Language="C#" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
	<title>CRM - Login</title>
    <meta name="author" content="Administrator" />
    <meta name="description" content="CRM - Corporate Resource Management" />
    <meta name="keywords" content="CRM" />
    <meta name="Generator" content="" />
    <meta name="robots" content="index, follow" />
    <link rel="shortcut icon" href="/Images/favicon.ico" />
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link type="text/css" href="/Content/Css/Core.css" rel="stylesheet" />
	<link type="text/css" href="/Content/Css/Login.css" rel="stylesheet" />
    <!--[if IE]>
		<link type="text/css" href="/Content/Css/Fixie.css" rel="stylesheet" />
	<![endif]-->
	<!--[if IE 6]>
		<link type="text/css" href="/Content/Css/FixieIE6.css" rel="stylesheet" />
	<![endif]-->        
</head>
<body>
    <% using (Html.BeginForm())
       { %>
    <div id="wrapper">
    	<div id="container">
        	<div id="login">				
				<div id="lform">
					<div><label for="username">Username</label></div>
					<div><input type="text" name="username" /></div>
					<div class="mr10"><label for="password">Password</label></div>
					<div><input type="password" name="password" /></div>
					<div class="mr10">
						<input type="submit" class="btn_lg" value="" />
						<input type="reset"  class="btn_cc" value="" />
					</div>
					<div class="mr10 err"><%= Html.Encode(ViewData["ErrorDetails"]) %></div>
				</div>
			</div>
        </div>
    </div>
    <% } %>
</body>
</html>
