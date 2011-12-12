<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<dynamic>" %>
<script type="text/javascript">
    $(document).ready(function () {
        $("#DOB").datepicker({
            yearRange: "-100:100",
            onClose: function () { $(this).valid(); }
        });
        $("#CustomerForm").validate({
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
                CustomerName: {
                    required: true,
                    maxlength: 100
                },
                Address: {
                    required: true,
                    maxlength: 200
                },
                TaxCode: {
                    maxlength: 12
                },
                PhoneNumber: {
                    required: true,
                    maxlength: 20,
                    standardPhone: true
                },
                DOB: {
                    checkDate: true,
                    checkBirthDate: true 
                },
                Email: {
                    required: true,
                    maxlength: 100,
                    email: true
                },
                Remark: {
                    maxlength: 500
                }
            }
        });
    });

    function onSubmit() {
        if ($("#CustomerForm").valid()) {
            $("#btnSubmit").attr("disabled", "disabled");
            $("#CustomerForm").submit();
        }
    }

</script>
<table width="100%" border="0" cellpadding="0" cellspacing="0" class="edit">
    <% 
        sp_GetAssetMasterResult assetMaster = (sp_GetAssetMasterResult)ViewData.Model;        
    %>
    <tr>
        <td class="label required">
            ID<span>*</span>
        </td>
        <td class="input">
            <% if (ViewData.Model == null)
               {
                   Response.Write(Html.TextBox("AssetID", "", new { @maxlength = "50", @style = "width:100px" }));
               }
               else
               {
                   Response.Write(Html.TextBox("AssetID", assetMaster.AssetId, new { @maxlength = "50", @style = "width:100px" }));
                   Response.Write(Html.Hidden("ID", assetMaster.ID));
                   Response.Write(Html.Hidden("UpdateDate", assetMaster.UpdateDate.ToString()));
               }
            %>
        </td>
        <td class="label required">
            Category<span>*</span>
        </td>
        <td class="input">
            <% 
               Response.Write(Html.DropDownList("AssetCategoryId", ViewData[Constants.ASSET_MASTER_CATEGORY_LIST] as SelectList, Constants.ASSET_MASTER_DEFAULT_CATEGORY_LIST, new { @style = "width:100px" }));
            %>
        </td>
    </tr>

    <tr>
        <td class="label required">
            Status<span>*</span>
        </td>
        <td class="input">
            <% 
               Response.Write(Html.DropDownList("AssetStatusId", ViewData[Constants.ASSET_MASTER_STATUS_LIST] as SelectList, Constants.ASSET_MASTER_DEFAULT_STATUS_LIST, new { @style = "width:100px" }));
            %>
        </td>
        <td class="label">
            User Name
        </td>
        <td class="input">
            <% if (ViewData.Model == null)
               {
                   Response.Write(Html.TextBox("Username", "", new { @maxlength = "100", @style = "width:100px" }));
               }
               else
               {
                   Response.Write(Html.TextBox("Username", assetMaster.DisplayName, new { @maxlength = "100", @style = "width:100px" }));
               }
            %>
        </td>
    </tr>
    <tr>
        <td class="label required">
            Remark
        </td>
        <td class="input" colspan="3">
            <% if (ViewData.Model == null)
               {
                   Response.Write(Html.TextArea("Remark", "", new { @maxlength = "500", @style = "width:200px", @rows = "5" }));
               }
               else
               {
                   Response.Write(Html.TextArea("Remark", customer.Remark, new { @maxlength = "500", @style = "width:200px" }));
               }
            %>
        </td>
    </tr>
    <tr>
        <td colspan="4">
            <fieldset>
                <legend>Property</legend>
            </fieldset>
        </td>
    </tr> 
    <tr>
        <td class="label required">
            Active
        </td>
        <td class="input" colspan="3">
            <% if (ViewData.Model == null)
               {
                   Response.Write(Html.CheckBox("IsActive"));
               }
               else
               {
                   Response.Write(Html.CheckBox("IsActive",customer.IsActive));
               }
            %>
        </td>
    </tr>
    <tr>
        <td align="center" colspan="4">
            <input id="btnSubmit" type="button" class="save" value="" onclick="onSubmit()" alt="Update" />
            <input type="button" class="cancel" value="" alt="" onclick="CRM.closePopup()" />
        </td>
    </tr>
</table>

