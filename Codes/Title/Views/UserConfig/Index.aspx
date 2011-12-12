<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <%= TempData["Message"] %>
    <%using (Html.BeginForm("Index", "UserConfig", FormMethod.Post, new { id = "purchaseForm", @class = "form", enctype = "multipart/form-data" }))
      { %>
    <% 
        UserConfig obj = (UserConfig)ViewData.Model;
        Response.Write(Html.Hidden("UserAdminID", (string)ViewData["UserID"]));
        Response.Write(Html.Hidden("IsCreate", (bool)ViewData["IsCreate"]));
    %>
    <script type="text/javascript">
        $(document).ready(function () {
            $("#btnCancel").click(function () {
                window.location = "/Home";
            })
            $("#purchaseForm").validate({
                debug: false,
                errorElement: "span",
                errorPlacement: function (error, element) {
                    error.tooltip({
                        bodyHandler: function () {
                            return error.html();
                        }
                    });
                    error.insertAfter(element);
                }
            });
            $("input[name=IsOff]").change(function () {
                if ($(this).val() == "False") {
                    $("#AutoReplyMessage").rules("add", "required");
                }
                else {
                    $("#AutoReplyMessage").rules("remove");
                }
            });
        });
        function onSubmit() {
            if ($("#purchaseForm").valid()) {
                $("#btnSubmit").attr("disabled", "disabled");
                $("#purchaseForm").submit();
            }
        }
    </script>
    <fieldset class="form" style="width: 580px;">
        <legend>User Config </legend>
        <table class="view" cellspacing="0" cellpadding="0" style="width: 580px; border: 0px;">
            <tr>
                <td>
                    <% 
          if (obj != null)
                       {
                           Response.Write(Html.RadioButton("IsOff", true, obj.IsOff == true ? true : false, new { id = "inOffice" }));
                       }
                       else
                       {
                           Response.Write(Html.RadioButton("IsOff", true, true, new { id = "inOffice" }));
                       }
                   
                    %>
                    <label for=inOffice>I'm in Office</label>
                </td>
            </tr>
            <tr>
                <td>
                    <% 
          if (obj != null)
                       {
                           Response.Write(Html.RadioButton("IsOff", false, obj.IsOff == false ? true : false, new { id = "outOffice"} ));
                       }
                       else
                       {
                           Response.Write(Html.RadioButton("IsOff", false, new { id = "outOffice" }));
                       }
                   
                    %>
                    <label for=outOffice>I'm out Office</label>
                </td>
            </tr>
            <tr>
                <td>
                    Auto Reply Message
                </td>
            </tr>
            <tr>
                <td>
                    <% Response.Write(Html.TextArea("AutoReplyMessage", obj != null ? obj.AutoReplyMessage : string.Empty, new { @style = "width:500px;height:50px" }));%>
                </td>
            </tr>
            <tr>
                <td>
                    <input type="submit" class="save" value=""  />
                    <input type="button" class="cancel" id="btnCancel" value="" />
                </td>
            </tr>
        </table>
    </fieldset>
    <%} %>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PageTitle" runat="server">
    <%= UserConfigPageInfo.MenuName  + CommonPageInfo.AppSepChar + CommonPageInfo.AppName%>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ModuleName" runat="server">
    <%= UserConfigPageInfo.MenuName%>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="FunctionTitle" runat="server">
    <%=CommonFunc.GetCurrentMenu(Request.RawUrl,true)%>
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="LoginRoles" runat="server">
</asp:Content>
