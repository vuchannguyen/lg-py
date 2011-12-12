<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<%@ Register Src="UCContractInfo.ascx" TagName="UCContractInfo" TagPrefix="uc1" %>
<form id="contractForm" action="<%= Url.Action("EditContract", "Employee")%>" method="post"
enctype="multipart/form-data">
<%= Html.Hidden("ContractId", ((Contract)ViewData.Model).ContractId)%>
<uc1:UCContractInfo ID="UCContractInfo" runat="server" />
</form>
<script type="text/javascript">
    var i = 0;
    $(document).ready(function () {
        $("#contractForm").validate({
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
                ContractNumber: { maxlength: 30 },
                StartDate: { required: true, checkDate: true, compareDate: ["#contractForm input[name='ContractedDate']", "get", "Start Date", "Contracted Date"] },
                ContractType: { required: true,
                    remote: {
                        url: "/Employee/CheckContractedType",
                        type: "post",
                        data: {
                            empId: function () {
                                return $("#EmployeeId").val();
                            },
                            contractType: function () {
                                return $("#ContractType").val();
                            },
                            contractName: function () {
                                return $("#ContractType option:selected").text();
                            },
                            contractId: function () {
                                return $("#ContractId").val();
                            }
                        }
                    }
                }
            },
            submitHandler: function (form) {
                if (i == 0) {
                    $(form).ajaxSubmit({
                        url: form.action,
                        dataType: 'json',
                        iframe: true,
                        success: function (result) {
                            if (result.msg.MsgType == 1) {
                                CRM.summary(result.msg.MsgText, 'block', 'msgError');
                                i--;
                            }
                            else {
                                if ($("#content_Duration").val() == 0) {
                                    $("#btnAddNew").hide();
                                }
                                CRM.message(result.msg.MsgText, 'block', 'msgSuccess');
                                $('#list').setGridParam({ url: '/Employee/GetContractRenewalJQGrid/?id=' + $("#EmployeeId").val() + '&Status=' + $("#EmpStatus").val() }).trigger('reloadGrid');
                                CRM.closePopup();
                            }
                        }
                    });
                    i++;
                }
            }
        });
        if ('<%=ViewData["LastEndDate"] %>' == '<%=Constants.DATE_MIN %>') {
            $("#EndDate").rules("remove");
        }
        else {
            $("#EndDate").rules("add", {  checkDate: true, compareDate: ["#contractForm input[name='StartDate']", "get", "End Date", "Start Date"]});
        }

    });
</script>
