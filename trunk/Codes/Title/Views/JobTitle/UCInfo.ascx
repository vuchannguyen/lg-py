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
                JobTitleName: {
                    required: true                    
                },
                DepartmentId: {
                    required: true                    
                },
                Description: {                   
                    maxlength: 500
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
        JobTitle jobTitle = (JobTitle)ViewData.Model;        
    %>
    <tr>
        <td class="label required">
            Job Title Name<span>*</span>
        </td>
        <td class="input">
            <% if (ViewData.Model == null)
               {
                   Response.Write(Html.TextBox("JobTitleName", "", new { @maxlength = "50", @style = "width:200px" }));
               }
               else
               {
                   Response.Write(Html.TextBox("JobTitleName", jobTitle.JobTitleName, new { @maxlength = "50", @style = "width:200px" }));
                   Response.Write(Html.Hidden("JobTitleId", jobTitle.JobTitleId));
                   Response.Write(Html.Hidden("UpdateDate", jobTitle.UpdateDate.ToString()));
               }
            %>
        </td>
    </tr>
    <tr>
        <td class="label required">
            Department <span>*</span>
        </td>
        <td class="input">
            <% if (ViewData.Model == null)
               {
                   Response.Write(Html.DropDownList(CommonDataKey.JTL_DEPARTMENT, ViewData[CommonDataKey.JTL_DEPARTMENT] as SelectList, Constants.FIRST_ITEM_DEPARTMENT, new { @style = "width:200px" }));                   
               }
               else
               {
                   Response.Write(Html.DropDownList(CommonDataKey.JTL_DEPARTMENT, null, Constants.FIRST_ITEM_DEPARTMENT, new { @style = "width:200px" }));
               }
            %>
        </td>
    </tr>
    <tr>
        <td class="label">
            Is Manager
        </td>
        <td class="input">
            <% if (ViewData.Model == null)
               {
                   Response.Write(Html.CheckBox("IsManager"));
               }
               else
               {
                   Response.Write(Html.CheckBox("IsManager",jobTitle.IsManager));
               }
            %>
        </td>
    </tr>
    <tr>
        <td class="label" style="vertical-align:top">
            Description
        </td>
        <td class="input">
            <% if (ViewData.Model == null)
               {
                   Response.Write(Html.TextArea("Description", "", new { @style = "width:200px" }));
               }
               else
               {
                   Response.Write(Html.TextArea("Description", jobTitle.Description, new { @style = "width:200px" }));
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

