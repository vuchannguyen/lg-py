<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<script type="text/javascript">
    $(document).ready(function () {
        isSubmited = false;
        $("#HolidayDate").datepicker();
        $("#holidayForm").validate({
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
                HolidayName: {
                    required: true,
                    maxlength: '<%= Constants.ANNUAL_HOLIDAY_NAME_MAX_LENGTH %>'
                },
                HolidayDate: {
                    required: true,
                    checkDate: true
                }
            },
            submitHandler: function (form) {
                formSubmitHandler(form);
            }
        });
    });
</script>
<%: Html.ValidationSummary(true) %>
<div id="summary" style="display: none" class=""></div>
<div>
<table class="edit" style = "width:100%">
    <tr>
        <td class="label required" style="width:100px">
            Name<span>*</span>
        </td>
        <td class = "input">
            <%= Html.TextBox("HolidayName", null, new { @style = "width:90%"})%>
            <%= Html.Hidden("ID") %>
        </td>
    </tr>
    <tr>
        <td class="label required">
            Date<span>*</span>
        </td>
        <td class = "input">
            <%if(ViewData.Model != null)
              {
                  Response.Write(Html.TextBox("HolidayDate", ((AnnualHoliday)ViewData.Model).
                      HolidayDate.ToString(Constants.DATETIME_FORMAT), 
                      new { @maxlength = "10", @style = "width:80px" }));
                  Response.Write(Html.Hidden("UpdateDate"));
              }
              else
              {
                  Response.Write(Html.TextBox("HolidayDate", null, 
                      new { @maxlength = "10", @style = "width:80px" }));
              }%>
        </td>
    </tr>
        <tr>
        <td class="label required">
            Description
        </td>
        <td class = "input">
            <%= Html.TextArea("Description", new { @style = "width: 304px;height:50px;", @maxlength=500 })%>
        </td>
    </tr>
    <tr>
        <td align="center" colspan="2">
            <input type="submit" class="save" id="btnCreate" value="" title="Save" />
            <input type="button" class="cancel" value="" id="btnCancel" onclick="CRM.closePopup()" title="Cancel" />
        </td>
    </tr>
</table>
</div>
   


