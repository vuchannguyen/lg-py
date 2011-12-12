<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<dynamic>" %>
<script type="text/javascript">
    $(document).ready(function () {
        $("#WorkFlow").change(function () {
            $("#WFRoleID").html("");
            $("#WFResolutionID").html("");
            var item = $("#WorkFlow").val();
            $("#WFRoleID").append($("<option value=''>" + '<%= Constants.FIRST_ITEM_ROLE %>' + "</option>"));
            $("#WFResolutionID").append($("<option value=''>" + '<%= Constants.FIRST_ITEM_RESOLUTION %>' + "</option>"));
            $.getJSON('/Library/GenericHandle/DropdownListHandler.ashx?ID=' + item + '&Page=WFRole', function (item) {
                $.each(item, function () {
                    $("#WFRoleID").append($("<option value='" + this['ID'] + "'>" + this['Name'] + "</option>"));
                });
            });
            $.getJSON('/Library/GenericHandle/DropdownListHandler.ashx?ID=' + item + '&Page=WFResolution', function (item) {
                $.each(item, function () {
                    $("#WFResolutionID").append($("<option value='" + this['ID'] + "'>" + this['Name'] + "</option>"));
                });
            });
        });
        $("#manageWorkFlowForm").validate({
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
                wfID: {
                    required: true                    
                },
                WFRoleID: {
                    required: true                    
                },
                WFResolutionID: {
                    required: true            
                }
            }
        });
    });

    function onSubmit() {
        if ($("#manageWorkFlowForm").valid()) {
            $("#btnSubmit").attr("disabled", "disabled");
            $("#manageWorkFlowForm").submit();
        }
    }

</script>
<table width="100%" border="0" cellpadding="0" cellspacing="0" class="edit">
    <% 
        WFRole_WFResolution obj = (WFRole_WFResolution)ViewData.Model;        
    %>
    <tr>
        <td class="label required">
            WorkFlow <span>*</span>
        </td>
        <td class="input">
            <%=Html.DropDownList("WorkFlow", null, Constants.FIRST_ITEM_REQUEST, new { @style = "width:180px" })%>
        </td>
    </tr>
    <tr>
        <td class="label required">
            Role <span>*</span>
        </td>
        <td class="input">
            <%=Html.DropDownList("WFRoleID", null, Constants.FIRST_ITEM_ROLE, new { @style = "width:180px" })%>
        </td>
    </tr>
    <tr>
        <td class="label required">
            Resolution <span>*</span>
        </td>
        <td class="input">
            <%=Html.DropDownList("WFResolutionID", null, Constants.FIRST_ITEM_RESOLUTION, new { @style = "width:180px" })%>
        </td>
    </tr>
    <tr>
        <td class="label">
            Is Hold
        </td>
        <td class="input">
            <% if (ViewData.Model == null)
               {
                   Response.Write(Html.CheckBox("IsHold"));
               }
               else
               {
                   Response.Write(Html.CheckBox("IsHold", obj.IsHold));
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

