<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<%using (Html.BeginForm("EditAddressInfo", "STT", FormMethod.Post, new { @id = "editForm", @class = "form" }))
  { %>
<% STT emp = (STT)ViewData.Model;%>
<table cellspacing="0" cellpadding="0" border="0" width="100%" class="edit">
    <tbody>
        <tr>
            <td class="label" style="text-align:left" style="text-align:left">
                <b>Permanent Address</b>
            </td>
        </tr>
        <tr>
            <td class="input">
                <%=Html.Hidden("UpdateDate", emp.UpdateDate.ToString())%>
                <%  if (!string.IsNullOrEmpty(emp.PermanentAddress))
                    {
                        Response.Write(Html.TextBox("PermanentAddress", emp.PermanentAddress, new { @style = "width:200px", @maxlength = "255" }));
                    }
                    else
                    {
                        Response.Write(Html.TextBox("PermanentAddress", Constants.ADDRESS, new { @style = "width:200px", @maxlength = "255", @onfocus = "ShowOnFocus(this,'" + Constants.ADDRESS + "')", @onblur = "ShowOnBlur(this,'" + Constants.ADDRESS + "')" }));
                    }%>
                <span class="fsep"></span>
                <% if (!string.IsNullOrEmpty(emp.PermanentArea))
                   {
                       Response.Write(Html.TextBox("PermanentArea", emp.PermanentArea, new { @style = "width:100px", @maxlength = "30" }));
                   }
                   else
                   {
                       Response.Write(Html.TextBox("PermanentArea", Constants.AREA, new { @style = "width:100px", @maxlength = "30", @onfocus = "ShowOnFocus(this,'" + Constants.AREA + "')", @onblur = "ShowOnBlur(this,'" + Constants.AREA + "')" }));
                   }%>
            </td>
        </tr>
        <tr>
            <td class="input">
                <%  if (!string.IsNullOrEmpty(emp.PermanentDistrict))
                    {
                        Response.Write(Html.TextBox("PermanentDistrict", emp.PermanentDistrict, new { @style = "width:90px", @maxlength = "30" }));
                    }
                    else
                    {
                        Response.Write(Html.TextBox("PermanentDistrict", Constants.DISTRICT, new { @style = "width:90px", @maxlength = "30", @onfocus = "ShowOnFocus(this,'" + Constants.DISTRICT + "')", @onblur = "ShowOnBlur(this,'" + Constants.DISTRICT + "')" }));
                    }%>
                <span class="fsep"></span>
                <%if (!string.IsNullOrEmpty(emp.PermanentCityProvince))
                  {
                      Response.Write(Html.TextBox("PermanentCityProvince", emp.PermanentCityProvince, new { @style = "width:90px", @maxlength = "30" }));
                  }
                  else
                  {
                      Response.Write(Html.TextBox("PermanentCityProvince", Constants.CITYPROVINCE, new { @style = "width:90px", @maxlength = "30", @onfocus = "ShowOnFocus(this,'" + Constants.CITYPROVINCE + "')", @onblur = "ShowOnBlur(this,'" + Constants.CITYPROVINCE + "')" }));
                  }%>
                <span class="fsep"></span>
                <%=Html.DropDownList("PermanentCountry", null, Constants.FIRST_ITEM, new { @Style = "width:106px" })%>
            </td>
        </tr>
        <tr>
            <td class="label" style="text-align:left">
                <b>VN Permanet Address</b>
            </td>
        </tr>
        <tr>
            <td class="input">
                <% if (ViewData.Model == null)
                   {
                       Response.Write(Html.TextBox("VnPermanentAddress", Constants.VN_ADDRESS, new { @style = "width:200px", @maxlength = "255", @onfocus = "ShowOnFocus(this,'" + Constants.VN_ADDRESS + "')", @onblur = "ShowOnBlur(this,'" + Constants.VN_ADDRESS + "')" }));
                   }
                   else
                   {
                       if (!string.IsNullOrEmpty(emp.VnPermanentAddress))
                       {
                           Response.Write(Html.TextBox("VnPermanentAddress", emp.VnPermanentAddress, new { @style = "width:200px", @maxlength = "255" }));
                       }
                       else
                       {
                           Response.Write(Html.TextBox("VnPermanentAddress", Constants.VN_ADDRESS, new { @style = "width:200px", @maxlength = "255", @onfocus = "ShowOnFocus(this,'" + Constants.VN_ADDRESS + "')", @onblur = "ShowOnBlur(this,'" + Constants.VN_ADDRESS + "')" }));
                       }
                   }
                %>
                <span class="fsep"></span>
                <% if (ViewData.Model == null)
                   {
                       Response.Write(Html.TextBox("VnPermanentArea", Constants.VN_AREA, new { @style = "width:100px", @maxlength = "30", @onfocus = "ShowOnFocus(this,'" + Constants.VN_AREA + "')", @onblur = "ShowOnBlur(this,'" + Constants.VN_AREA + "')" }));
                   }
                   else
                   {
                       if (!string.IsNullOrEmpty(emp.VnPermanentArea))
                       {
                           Response.Write(Html.TextBox("VnPermanentArea", emp.VnPermanentArea, new { @style = "width:100px", @maxlength = "30" }));
                       }
                       else
                       {
                           Response.Write(Html.TextBox("VnPermanentArea", Constants.VN_AREA, new { @style = "width:100px", @maxlength = "30", @onfocus = "ShowOnFocus(this,'" + Constants.VN_AREA + "')", @onblur = "ShowOnBlur(this,'" + Constants.VN_AREA + "')" }));
                       }
                   }
                %>
            </td>
        </tr>
        <tr>
            <td class="input">
                <% if (ViewData.Model == null)
                   {
                       Response.Write(Html.TextBox("VnPermanentDistrict", Constants.VN_DISTRICT, new { @style = "width:90px", @maxlength = "30", @onfocus = "ShowOnFocus(this,'" + Constants.VN_DISTRICT + "')", @onblur = "ShowOnBlur(this,'" + Constants.VN_DISTRICT + "')" }));
                   }
                   else
                   {
                       if (!string.IsNullOrEmpty(emp.VnPermanentDistrict))
                       {
                           Response.Write(Html.TextBox("VnPermanentDistrict", emp.VnPermanentDistrict, new { @style = "width:90px", @maxlength = "30" }));
                       }
                       else
                       {
                           Response.Write(Html.TextBox("VnPermanentDistrict", Constants.VN_DISTRICT, new { @style = "width:90px", @maxlength = "30", @onfocus = "ShowOnFocus(this,'" + Constants.VN_DISTRICT + "')", @onblur = "ShowOnBlur(this,'" + Constants.VN_DISTRICT + "')" }));
                       }
                   }
                %>
                <span class="fsep"></span>
                <% if (ViewData.Model == null)
                   {
                       Response.Write(Html.TextBox("PermanentCityProvince", Constants.VN_CITYPROVINCE, new { @style = "width:90px", @maxlength = "30", @onfocus = "ShowOnFocus(this,'" + Constants.VN_CITYPROVINCE + "')", @onblur = "ShowOnBlur(this,'" + Constants.VN_CITYPROVINCE + "')" }));
                   }
                   else
                   {
                       if (!string.IsNullOrEmpty(emp.VnPermanentCityProvince))
                       {
                           Response.Write(Html.TextBox("VnPermanentCityProvince", emp.VnPermanentCityProvince, new { @style = "width:90px", @maxlength = "30" }));
                       }
                       else
                       {
                           Response.Write(Html.TextBox("VnPermanentCityProvince", Constants.VN_CITYPROVINCE, new { @style = "width:90px", @maxlength = "30", @onfocus = "ShowOnFocus(this,'" + Constants.VN_CITYPROVINCE + "')", @onblur = "ShowOnBlur(this,'" + Constants.VN_CITYPROVINCE + "')" }));
                       }
                   }
                %>
                <span class="fsep"></span>
                <% if (ViewData.Model == null)
                   {
                       Response.Write(Html.DropDownList("VnPermanentCountry", null, "-Quốc gia-", new { @style = "width:106px" }));
                   }
                   else
                   {
                       if (!string.IsNullOrEmpty(emp.PermanentCountry))
                       {
                           Response.Write(Html.DropDownList("VnPermanentCountry", null, new { @style = "width:106px" }));
                       }
                       else
                       {
                           Response.Write(Html.DropDownList("VnPermanentCountry", null, "-Quốc gia-", new { @style = "width:106px" }));
                       }
                   }
                %>
            </td>
        </tr>
        <tr>
            <td class="label" style="text-align:left">
                <b>Temp Address</b>
            </td>
        </tr>
        <tr>
            <td class="input">
                    <%  if (!string.IsNullOrEmpty(emp.TempAddress))
                        {
                            Response.Write(Html.TextBox("TempAddress", emp.TempAddress, new { @style = "width:200px", @maxlength = "255" }));
                        }
                        else
                        {
                            Response.Write(Html.TextBox("TempAddress", Constants.ADDRESS, new { @style = "width:200px", @maxlength = "255", @onfocus = "ShowOnFocus(this,'" + Constants.ADDRESS + "')", @onblur = "ShowOnBlur(this,'" + Constants.ADDRESS + "')" }));
                        }%>
                    <span class="fsep"></span>
                    <% if (!string.IsNullOrEmpty(emp.TempArea))
                       {
                           Response.Write(Html.TextBox("TempArea", emp.TempArea, new { @style = "width:100px", @maxlength = "30" }));
                       }
                       else
                       {
                           Response.Write(Html.TextBox("TempArea", Constants.AREA, new { @style = "width:100px", @maxlength = "30", @onfocus = "ShowOnFocus(this,'" + Constants.AREA + "')", @onblur = "ShowOnBlur(this,'" + Constants.AREA + "')" }));
                       }%>
                </td>
        </tr>
        <tr>
            <td class="input">
                <% if (!string.IsNullOrEmpty(emp.TempDistrict))
                    {
                        Response.Write(Html.TextBox("TempDistrict", emp.TempDistrict, new { @style = "width:90px", @maxlength = "30" }));
                    }
                    else
                    {
                        Response.Write(Html.TextBox("TempDistrict", Constants.DISTRICT, new { @style = "width:90px", @maxlength = "30", @onfocus = "ShowOnFocus(this,'" + Constants.DISTRICT + "')", @onblur = "ShowOnBlur(this,'" + Constants.DISTRICT + "')" }));
                    } %>
                <span class="fsep"></span>
                <%if (!string.IsNullOrEmpty(emp.TempCityProvince))
                    {
                        Response.Write(Html.TextBox("TempCityProvince", emp.TempCityProvince, new { @style = "width:90px", @maxlength = "30" }));
                    }
                    else
                    {
                        Response.Write(Html.TextBox("TempCityProvince", Constants.CITYPROVINCE, new { @style = "width:90px", @maxlength = "30", @onfocus = "ShowOnFocus(this,'" + Constants.CITYPROVINCE + "')", @onblur = "ShowOnBlur(this,'" + Constants.CITYPROVINCE + "')" }));
                    } %>
                <span class="fsep"></span>
                <%=Html.DropDownList("TempCountry", null, Constants.FIRST_ITEM, new { @Style = "width:106px" })%>
            </td>
        </tr>
        <tr>
            <td class="label" style="text-align:left">
                <b>VN Temp Address</b>
            </td>
        </tr>
        <tr>
            <td  class="input">
                    <% if (ViewData.Model == null)
                       {
                           Response.Write(Html.TextBox("VnTempAddress", Constants.VN_ADDRESS, new { @style = "width:200px", @maxlength = "255", @onfocus = "ShowOnFocus(this,'" + Constants.VN_ADDRESS + "')", @onblur = "ShowOnBlur(this,'" + Constants.VN_ADDRESS + "')" }));
                       }
                       else
                       {
                           if (!string.IsNullOrEmpty(emp.VnTempAddress))
                           {
                               Response.Write(Html.TextBox("VnTempAddress", emp.VnTempAddress, new { @style = "width:200px", @maxlength = "255" }));
                           }
                           else
                           {
                               Response.Write(Html.TextBox("VnTempAddress", Constants.VN_ADDRESS, new { @style = "width:200px", @maxlength = "255", @onfocus = "ShowOnFocus(this,'" + Constants.VN_ADDRESS + "')", @onblur = "ShowOnBlur(this,'" + Constants.VN_ADDRESS + "')" }));
                           }
                       }
                    %>
                    <span class="fsep"></span>
                    <% if (ViewData.Model == null)
                       {
                           Response.Write(Html.TextBox("VnTempArea", Constants.VN_AREA, new { @style = "width:100px", @maxlength = "30", @onfocus = "ShowOnFocus(this,'" + Constants.VN_AREA + "')", @onblur = "ShowOnBlur(this,'" + Constants.VN_AREA + "')" }));
                       }
                       else
                       {
                           if (!string.IsNullOrEmpty(emp.VnTempArea))
                           {
                               Response.Write(Html.TextBox("VnTempArea", emp.VnTempArea, new { @style = "width:100px", @maxlength = "30" }));
                           }
                           else
                           {
                               Response.Write(Html.TextBox("VnTempArea", Constants.VN_AREA, new { @style = "width:100px", @maxlength = "30", @onfocus = "ShowOnFocus(this,'" + Constants.VN_AREA + "')", @onblur = "ShowOnBlur(this,'" + Constants.VN_AREA + "')" }));
                           }
                       }
                    %>
                </td>
        </tr>
        <tr>
            <td  class="input">
                <% if (ViewData.Model == null)
                    {
                        Response.Write(Html.TextBox("VnTempDistrict", Constants.VN_DISTRICT, new { @style = "width:90px", @maxlength = "30", @onfocus = "ShowOnFocus(this,'" + Constants.VN_DISTRICT + "')", @onblur = "ShowOnBlur(this,'" + Constants.VN_DISTRICT + "')" }));
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(emp.VnTempDistrict))
                        {
                            Response.Write(Html.TextBox("VnTempDistrict", emp.VnTempDistrict, new { @style = "width:90px", @maxlength = "30" }));
                        }
                        else
                        {
                            Response.Write(Html.TextBox("VnTempDistrict", Constants.VN_DISTRICT, new { @style = "width:90px", @maxlength = "30", @onfocus = "ShowOnFocus(this,'" + Constants.VN_DISTRICT + "')", @onblur = "ShowOnBlur(this,'" + Constants.VN_DISTRICT + "')" }));
                        }
                    }
                %>
                <span class="fsep"></span>
                <% if (ViewData.Model == null)
                    {
                        Response.Write(Html.TextBox("VnTempCityProvince", Constants.VN_CITYPROVINCE, new { @style = "width:90px", @maxlength = "30", @onfocus = "ShowOnFocus(this,'" + Constants.VN_CITYPROVINCE + "')", @onblur = "ShowOnBlur(this,'" + Constants.VN_CITYPROVINCE + "')" }));
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(emp.VnTempCityProvince))
                        {
                            Response.Write(Html.TextBox("VnTempCityProvince", emp.VnTempCityProvince, new { @style = "width:90px", @maxlength = "30" }));
                        }
                        else
                        {
                            Response.Write(Html.TextBox("VnTempCityProvince", Constants.VN_CITYPROVINCE, new { @style = "width:90px", @maxlength = "30", @onfocus = "ShowOnFocus(this,'" + Constants.VN_CITYPROVINCE + "')", @onblur = "ShowOnBlur(this,'" + Constants.VN_CITYPROVINCE + "')" }));
                        }
                    }
                %>
                <span class="fsep"></span>
                <% if (ViewData.Model == null)
                    {
                        Response.Write(Html.DropDownList("VnTempCountry", null, "-Quốc gia-", new { @style = "width:106px" }));
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(emp.PermanentCountry))
                        {
                            Response.Write(Html.DropDownList("VnTempCountry", null, new { @style = "width:106px" }));
                        }
                        else
                        {
                            Response.Write(Html.DropDownList("VnTempCountry", null, "-Quốc gia-", new { @style = "width:106px" }));
                        }
                    }
                %>
            </td>
        </tr>
        <tr>
            <td align="center" valign="middle">
                <input type="submit" class="save" value="" alt="" />
                <input type="button" class="cancel" value="" alt="" onclick="CRM.closePopup()" />
            </td>
        </tr>
    </tbody>
</table>
<% } %>