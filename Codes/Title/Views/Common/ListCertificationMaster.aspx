<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>
 <div class="clist">
    <div id="cfilter">
        <table border="0" cellpadding="0" cellspacing="0"> 
            <tr>
                <td>                    
                    <input type="text" maxlength="100" style="width: 190px" value="<%= Constants.TRAINING_CERTIFICATION_MASTER_SEARCH_NAME %>"
                        id="txtKeyword1" onfocus="ShowOnFocus(this,'<%= Constants.TRAINING_CERTIFICATION_MASTER_SEARCH_NAME %>')"
                        onblur="ShowOnBlur(this,'<%= Constants.TRAINING_CERTIFICATION_MASTER_SEARCH_NAME  %>')" autocomplete="off" />
                </td>
                <td>
                    <button type="button" id="btnFilter1" title="Filter" class="button filter">
                        Filter</button>
                </td>
            </tr>
        </table>
    </div>
        <table id="list1" class="scroll">
        </table>
        <div id="pager" class="scroll" style="text-align: center;">
        </div>
 </div>
    <div id="shareit-box">
        <img src='../../Content/Images/loading3.gif' alt='' />
    </div>
  <%--  <style type="text/css">
        .ac_results
        {
            width: 270px !important;
        }
    </style>--%>
    <script type="text/javascript">
        function ChooseCertificationID(id, name) {
            CRM.pInPopupClose();
            $("#CertificationId").val(name);
        }
        $(document).ready(function () {
            $("#txtKeyword1").keypress(function (event) {
                if (event.keyCode == 13) {
                    $("#btnFilter1").click();
                }
            });
            jQuery("#list1").jqGrid({
                url: '/Common/GetListCertificationGrid?certificationName=' + encodeURIComponent($('#txtKeyword1').val()),
                datatype: 'json',
                colNames: ['HiddenId', 'ID', 'Name', 'Description'],
                colModel: [
                  { name: 'HiddenId', index: 'HiddenId', align: "center", width: 50, hidden: true },
                  { name: 'ID', index: 'ID', align: "left", width: 30, title: false },
                  { name: 'Name', index: 'Name', align: "left", width: 130, title: false },
                  { name: 'Description', index: 'Description', align: "left", width: 215, title: false}],
                                        
                rowList: [20, 30, 50, 100, 200],
                width: 530,
                height: "100%",
                pager: '#pager',
                sortname: 'ID',
                sortorder: "desc",
                viewrecords: true,
                loadui: 'block'

            });
            $("#btnFilter1").click(function () {
                var name = $('#txtKeyword1').val();
                $('#list1').setGridParam({ url: '/Common/GetListCertificationGrid?certificationName=' + encodeURIComponent($('#txtKeyword1').val())
                }).trigger('reloadGrid');
            });
        });
    </script>


