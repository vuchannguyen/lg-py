<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<dynamic>" %>
<script type="text/javascript">
    $(document).ready(function () {
        $("#JobTitleForm").validate({
            debug: false,
            errorElement: "span",
            errorPlacement: function (error, element) {
                error.tooltip({
                    bodyHandler: function () {
                        return error.html();
                    }
                });
                error.insertAfter(element);
            },
            rules: {
                DisplayName: {
                    required: true                    
                },
                JobTitleId: {
                    required: true                    
                },
                JobLevel: {
                    required: true            
                }
            }
        });
    });

    function onSubmit() {
        if ($("#JobTitleForm").valid()) {
            $("#btnSubmit").attr("disabled", "disabled");
            $("#JobTitleForm").submit();
        }
    }

</script>
<table width="100%" border="0" cellpadding="0" cellspacing="0" class="edit">
    <% 
        JobTitleLevel jobTitle = (JobTitleLevel)ViewData.Model;        
    %>
    <tr>
        <td class="label required">
            Display Name<span>*</span>
        </td>
        <td class="input">
            <% if (ViewData.Model == null)
               {
                   Response.Write(Html.TextBox("DisplayName", "", new { @maxlength = "100", @style = "width:200px" }));
               }
               else
               {
                   Response.Write(Html.TextBox("DisplayName", jobTitle.DisplayName, new { @maxlength = "100", @style = "width:200px" }));
                   Response.Write(Html.Hidden("ID", jobTitle.ID));
                   Response.Write(Html.Hidden("UpdateDate", jobTitle.UpdateDate.ToString()));
               }
            %>
        </td>
    </tr>
    <tr>
        <td class="label required">
            Job Title<span>*</span>
        </td>
        <td class="input">
           <%  Response.Write(Html.DropDownList(CommonDataKey.JTL_JOBTITLE, null, Constants.FIRST_ITEM_DEPARTMENT, new { @style = "width:200px" }));%>
        </td>
    </tr>
    <tr>
        <td class="label required">
            Level <span>*</span>
        </td>
        <td class="input">
           <%  Response.Write(Html.DropDownList(CommonDataKey.JTL_JOBTITLE_LEVEL, null, Constants.FIRST_LEVEL, new { @style = "width:200px" }));%>
        </td>
    </tr>
    <tr>
        <td class="label">
            Active
        </td>
        <td class="input">
            <% if (ViewData.Model == null)
               {
                   Response.Write(Html.CheckBox("IsActive"));
               }
               else
               {
                   Response.Write(Html.CheckBox("IsActive", jobTitle.IsActive));
               }
            %>
        </td>
    </tr>
    <tr>
        <td colspan="2" align="center">
            <input id="btnSubmit" type="button" class="save" value="" onclick="onSubmit()" alt="Update" />
            <input type="button" class="cancel" value="" alt="" onclick="CRM.closePopup()" />
        </td>
    </tr>
</table>

