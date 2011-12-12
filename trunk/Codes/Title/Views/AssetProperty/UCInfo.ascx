<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<dynamic>" %>
<script type="text/javascript" src="../../Scripts/highslide/highslide.js"></script>
<link rel="stylesheet" type="text/css" href="../../Scripts/highslide/highslide.css" />
<link href="/Scripts/AutoComplete/autoSuggest.css" rel="stylesheet" type="text/css" />
<script type="text/javascript">
    hs.graphicsDir = '../../Scripts/highslide/graphics/';
    hs.outlineType = 'rounded-white';
</script>
<div class="form" style="width: 520x">
<table width="100%" border="0" cellpadding="0" cellspacing="0" class="edit">
    <tr>
        <td class="label required" style="width:100px">
            Name <span>*</span>
        </td>
        <td class="input">
            <% 
                AssetProperty assCat = null;
                if (ViewData.Model != null)
                {
                    assCat = (AssetProperty)ViewData.Model;
                }
                
                if (assCat == null)
                {
                    Response.Write(Html.TextBox("Name", "", new { @maxlength = "255", @style = "width:300px" }));
                }
                else
                {
                    Response.Write(Html.TextBox("Name", assCat.Name, new { @maxlength = "255", @style = "width:300px" }));
                    Response.Write(Html.Hidden("ID", assCat.ID));
                    Response.Write(Html.Hidden("UpdateDate", assCat.UpdateDate.ToString()));
                }
            %>
        </td>
    </tr>    
    <tr>
        <td class="label required">
            Category <span>*</span>
        </td>
        <td class="input">
           <% 
               Response.Write(Html.DropDownList("AssetCategoryId", ViewData[Constants.ASSET_PROPERTY_CATEGORY_LIST] as SelectList, Constants.SR_FIRST_CATEGORY, new { @style = "width:150px" }));
            %>
        </td>
    </tr>
    <tr>
        <td class="label required">
            Display Order <span>*</span>
        </td>
        <td class="input">
            <%                                
                
                if (assCat == null)
                {
                    Response.Write(Html.TextBox("DisplayOrder", "", new { @maxlength = "5", @style = "width:100px" }));
                }
                else
                {
                    Response.Write(Html.TextBox("DisplayOrder", assCat.DisplayOrder, new { @maxlength = "5", @style = "width:100px" }));                    
                }
            %>
        </td>
    </tr>
    <tr>
        <td class="label">
            Master Data
        </td>
        <td class="input">
            <%                 
                Response.Write(Html.TextBox("AutoMasterData", (assCat != null ? assCat.MasterData : ""), new { @style = "width:300px" }));
                Response.Write(Html.Hidden("MasterData", ""));
            %>
        </td>
    </tr>
    <tr>        
        <td></td>
        <td colspan>
            <input type="submit" class="save" value="" alt="" />
            <input type="button" class="cancel" value="" alt="" onclick="CRM.closePopup()" />
        </td>
    </tr>
</table>
</div>
<link href="/Scripts/AutoComplete/autoSuggest.css" rel="stylesheet" type="text/css" />
<%--<script src="/Scripts/AutoComplete/jquery.autoSuggest.js" type="text/javascript"></script>--%>
<script type="text/javascript">
    $(document).ready(function () {
//        $("#AutoMasterData").autoSuggest('/Library/GenericHandle/AutoCompleteHandler.ashx/?Page=AssetMasterData',
//            { max: 50, highlightItem: true, multiple: true, multipleSeparator: ";", faceBook: true, hidField: "#MasterData", multiObject: true });
        $("#AutoMasterData").autocomplete('/Library/GenericHandle/AutoCompleteHandler.ashx/?Page=AssetMasterData'
                , { max: 50, highlightItem: true, multiple: true, multipleSeparator: ";", faceBook: true, hidField: "#MasterData", employee: true });
    });
</script>
