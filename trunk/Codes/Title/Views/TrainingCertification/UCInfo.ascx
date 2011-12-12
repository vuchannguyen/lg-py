<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<dynamic>" %>
<link href="/Scripts/AutoComplete/autoSuggest.css" rel="stylesheet" type="text/css" />
<% Training_CertificationMaster trainingCerMas = (Training_CertificationMaster)ViewData.Model;
   if (ViewData.Model != null)
   {
       Response.Write(Html.Hidden("UpdateDate", trainingCerMas.UpdateDate.ToString()));
   }
%>
<script type="text/javascript">
    $(document).ready(function () {
        $("#TrainingCertificationForm").validate({
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
                Name: {
                    required: true,
                    maxlength: 200
                }
            }
        });
    });

    function onSubmit() {
        if ($("#TrainingCertificationForm").valid()) {
            $("#btnSubmit").attr("disabled", "disabled");
            $("#TrainingCertificationForm").submit();
        }
    }

</script>
<%--<%using (Html.BeginForm("Create", "Customer1", FormMethod.Post, new { id = "addForm",@class="form" }))
  {%>
<uc1:UCInfo ID="UCInfo1" runat="server" />
<% } %>--%>
<table width="100%" border="0" cellpadding="0" cellspacing="0" class="edit">
    <tr>
        <td class="label required" style = "vertical-align:top; width:120px">
            Certification Name <span>*</span>
        </td>
        <td class="input">
             <% if (ViewData.Model == null)
               {
                   Response.Write(Html.TextBox("Name", "", new { @maxlength = "200", @style = "width:150px" }));
               }
               else
               {
                   Response.Write(Html.TextBox("Name", trainingCerMas.Name, new { @maxlength = "200", @style = "width:150px" }));
                  
               }
            %>
        </td>
    </tr>
    <tr>
        <td class="label" style = "vertical-align:top">
            Description
        </td>
         <td class="input">
             <% if (ViewData.Model == null)
               {
                   Response.Write(Html.TextArea("Description", "", new { @maxlength = "500", @style = "width:285px; height:100px" }));
               }
               else
               {
                   Response.Write(Html.TextArea("Description", trainingCerMas.Description, new { @maxlength = "500", @style = "width:285px; height:100px" }));
                  
               }
            %>
        </td>
    </tr>
    <tr>
        <td class="label">
            Active
        </td>
        <td valign="bottom" class="input">
            <% 
                string check = "checked=\"checked\"";
               if (ViewData.Model != null)               
               {
                   check = ((Training_CertificationMaster)ViewData.Model).IsActive ? "checked=\"checked\"" : "";
               }
               Response.Write("<input type=\"checkbox\" value=\"true\" name=\"IsActive\" id=\"IsActive\" " + check + " />");
            %>
        </td>
    </tr>
     <tr>
        <td>
        </td>
        <td>
            <input type="submit" class="save" value="" alt="Update" />
            <input type="button" class="cancel" value="" alt="" onclick="CRM.closePopup()" />
        </td>
    </tr>              
</table>