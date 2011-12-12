<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<dynamic>" %>
<% STT_RefResult stt = (STT_RefResult)ViewData.Model;%>
<script type="text/javascript">
    $("#Enddate").datepicker();
    if ($("#Enddate").attr("disabled") != '') {
        $("#Enddate").datepicker("disable");
    }    
</script>
<table id="tbl" cellspacing="0" cellpadding="0" border="0" width="100%" class="edit">
    <tbody>
        <tr>
            <td class="required label">
                Result <span>*</span>
            </td>
            <td class="input" style="width:430px" >
                <%
                    if (ViewData.Model != null)
                    {
                        if (stt.ResultId == null)
                        {
                            Response.Write(Html.DropDownList("ResultId", ViewData["Result"] as SelectList, Constants.FIRST_ITEM_RESULT, new { @style = "width:126px" }));
                        }
                        else if (stt.ResultId != null && ViewData["Status"] == null)
                        {
                            Response.Write(Html.DropDownList("ResultId", ViewData["Result"] as SelectList, Constants.FIRST_ITEM_RESULT, new { @style = "width:126px", @disabled = "disabled" }));
                        }
                        else
                        {
                            Response.Write(Html.DropDownList("ResultId", ViewData["Result"] as SelectList, Constants.FIRST_ITEM_RESULT, new { @style = "width:126px" }));
                        }
                    }
                    else
                    {
                        Response.Write(Html.DropDownList("ResultId", ViewData["Result"] as SelectList, Constants.FIRST_ITEM_RESULT, new { @style = "width:126px" }));
                    }
                %>
            </td>
            <td ></td>
        </tr>
        <tr>
            <td class="required label">
                End date <span>*</span>
            </td>
            <td class="input" >
                <% if (ViewData.Model == null)
                   {
                       Response.Write(Html.TextBox("Enddate", "", new { @style = "width:120px" }));
                   }
                   else
                   {
                       if (stt.ResultId == null)
                       {
                           Response.Write(Html.TextBox("Enddate", stt.EndDate.ToString(Constants.DATETIME_FORMAT), new { @style = "width:120px" }));
                       }
                       else if (stt.ResultId != null && ViewData["Status"] == null)
                       {
                           Response.Write(Html.TextBox("Enddate", stt.EndDate.ToString(Constants.DATETIME_FORMAT), new { @style = "width:120px", @disabled = "disabled" }));
                       }
                       else
                       {
                           Response.Write(Html.TextBox("Enddate", stt.EndDate.ToString(Constants.DATETIME_FORMAT), new { @style = "width:120px" }));
                       }
                   }
                %>
            </td>
            <td></td>
        </tr>
        <tr>
            <td class="label">
                Remarks
            </td>
            <td class="input" >
                <%=Html.Hidden("UpdateDate", stt != null ? stt.UpdatedDate.ToString():"" )%>
                <%=Html.TextArea("Remarks", stt != null ? stt.Remarks:"", new { @style = "width: 270px; height: 80px;",@maxlength="254" })%>
            </td>
            <% if (ViewData.Model != null)
                   { %>
                <td  valign="bottom">
                      <% int y = 0;
                       if (!string.IsNullOrEmpty(stt.Attachfile))
                       {
                           string[] array = stt.Attachfile.TrimEnd(Constants.FILE_CHAR_PREFIX).Split(Constants.FILE_CHAR_PREFIX);
                           y = array.Count();
                       }
                       else
                       {
                           y = 1;
                       }%><button type="button"  onclick='return AddRowUpload(<%=y %>);' title='Add New Upload' class='icon plus'> </button> </td>
            <% } %>
        </tr>
        <% if (ViewData.Model == null)
           {%>
        <tr id="row_upload">
            <td class="label">
                Attachment
            </td>
            <td id="contentFile" class="input">
                <input type="file" name="file" />
            </td>
            <td valign="top">
                <button class="icon plus" type="button" title="Add New Upload" onclick="return AddRowUpload();">
                </button>
            </td>
        </tr>
        <%}
           else
           {
               Response.Write(Html.Hidden("hidDeleteFile"));
               Response.Write(CommonFunc.SplitFileNameForView(stt.Attachfile, Constants.STT_RESULT_PATH, Constants.STT_DEFAULT_VALUE, stt.SttID, "hidDeleteFile"));%>
        <%  } %>
    </tbody>
</table>
<table width="100%" border="0" cellpadding="0" cellspacing="0" class="form">
    <tr>
        <td colspan="2" align="center" class="cbutton">
            <input type="submit" class="save" value="" alt="" />
            <input type="button" class="cancel" value="" alt="" onclick="CRM.closePopup();" />
        </td>
    </tr>
</table>
