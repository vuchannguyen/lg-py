<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<div style="margin-top:50px">
<%
    Message msg = new Message (MessageConstants.E0002, MessageType.Error);
    {
%>
        <img src="/Content/Images/ExtraIcons/error-icon-1928115.jpg" align="left" />
        <br/><br/>
        <h3 style="font-weight:bold; color:Red"><%:msg.MsgText%></h3>
<%
    }
%>
</div>