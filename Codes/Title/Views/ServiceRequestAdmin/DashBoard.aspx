<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <table width="100%" border="0" cellpadding="0" cellspacing="0">
        <tr>
            <td class="chrgh" style="padding-right: 30px;">
                <div id="accordionleft">
                    <div class="panel">
                        <div class="pntitle">
                            <span class="pndown"></span><span class="pntext">Service Request by Status</span>
                        </div>
                        <div class="pncontent" style="height: 400px">
                            <div class="assigntoyou">
                                <% if (ViewData["SRStatistic_Count"] != null)
                                   {
                                       Response.Write("There are " + Html.ActionLink((string)ViewData["SRStatistic_Count"], "/Index") + " Service Requests have been assigned to you.");
                                   }%>
                            </div>
                            <div>
                               <%= Html.ActionLink("View all service requests","/Index") %>
                            </div>
                            <div class="pncontent">
                                <div id="divStatus" style="width: 500px; height: 350px; margin: 0; z-index: 0;">
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </td>
            <td class="chrgh" style="width: 650px;">
                <div id="accordion">
                    <div class="panel">
                        <div class="pntitle">
                            <span class="pndown"></span><span class="pntext">New and Close by Date</span>
                        </div>
                        <div class="pncontent" style="height: 400px">
                            <div id="chart-container" style="width: 600px; height: 350px; margin: 0; z-index: 0;">
                            </div>
                        </div>
                    </div>
                </div>
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PageTitle" runat="server">
    <%= ServiceRequestPageInfo.ComName + CommonPageInfo.AppSepChar + CommonPageInfo.AppName%>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
    <!-- Script HightChart -->
    <script type="text/javascript" src="/Scripts/hightchart/highcharts.js"></script>
    <!-- Script HightChart -->
    <script type="text/javascript">
        $(document).ready(function ($) {
            var newSRData = new Array();
            var category = new Array();
            var closeSRData = new Array();
            var statusData = new Array();
            var cats = '<%= ViewData["LoginStatistic_Title"]  %>';
            var newSR = '<%= ViewData["LoginStatistic_Value"]  %>';
            var closeSR = '<%= ViewData["LoginStatistic_CloseSR"]  %>';

            var newSRs = newSR.split(',');
            $.each(newSRs, function (no, val) {
                newSRData.push(parseFloat(val));
            });

            var closeSRs = closeSR.split(',');
            $.each(closeSRs, function (no, val) {
                closeSRData.push(parseFloat(val));
            });

            var catss = cats.split(',');
            $.each(catss, function (no, cat) {
                category.push(cat);
            });

            var options = {
                chart: {
                    renderTo: 'chart-container',
                    defaultSeriesType: 'line'
                },
                title:false,
                xAxis: {
                    categories: category
                },
                yAxis: {
                    title: {
                        text: 'Number of Requests'
                    }
                },
                series: [
                    {
                        name: 'New SR',
                        data: newSRData
                    },
                    {
                        name: 'Closed SR',
                        data: closeSRData
                    }
                ]
            };

            // Create the chart
            var chart = new Highcharts.Chart(options);
            var statusChart;
            //Chart Status
            var options1 = {
                chart: {
                    renderTo: 'divStatus'
                },
                title: false,
                tooltip: {
                    formatter: function () {
                        return '' + this.point.name + ': ' + this.y + ' %';
                    }
                },
                plotOptions: {
                    pie: {
                        allowPointSelect: true,
                        cursor: 'pointer',
                        dataLabels: {
                            enabled: false
                        },
                        showInLegend: true
                    }
                },
                series: []
            }

            Highcharts.theme = {
                colors: ['#058DC7', '#50B432', '#ED561B', '#DDDF00', '#24CBE5', '#64E572', '#FF9655', '#FFF263', '#6AF9C4'],
                xAxis: {
                    gridLineWidth: 1,
                    lineColor: '#000',
                    tickColor: '#000',
                    labels: {
                        style: {
                            color: '#000',
                            font: '11px Trebuchet MS, Verdana, sans-serif'
                        }
                    },
                    title: {
                        style: {
                            color: '#333',
                            fontWeight: 'bold',
                            fontSize: '12px',
                            fontFamily: 'Trebuchet MS, Verdana, sans-serif'

                        }
                    }
                },
                yAxis: {
                    minorTickInterval: 'auto',
                    lineColor: '#000',
                    lineWidth: 1,
                    tickWidth: 1,
                    tickColor: '#000',
                    labels: {
                        style: {
                            color: '#000',
                            font: '11px Trebuchet MS, Verdana, sans-serif'
                        }
                    },
                    title: {
                        style: {
                            color: '#333',
                            fontWeight: 'bold',
                            fontSize: '12px',
                            fontFamily: 'Trebuchet MS, Verdana, sans-serif'
                        }
                    }
                },
                legend: {
                    itemStyle: {
                        font: '9pt Trebuchet MS, Verdana, sans-serif',
                        color: 'black'

                    },
                    itemHoverStyle: {
                        color: '#039'
                    },
                    itemHiddenStyle: {
                        color: 'gray'
                    }
                },
                labels: {
                    style: {
                        color: '#99b'
                    }
                }
            };

            // Apply the theme
            var highchartsOptions = Highcharts.setOptions(Highcharts.theme);

            //Calls the JSON
            jQuery.getJSON("GetStatusStatic",
                        null, function (items) {
                            //Creates the new series as stated in the documentation
                            //http://www.highcharts.com/ref/#series
                            var series = {
                                type: 'pie',
                                name: 'Status',
                                data: []
                            };
                            jQuery.each(items, function (itemNo, item) {
                                //Get the items from the JSON and add then
                                //to the data array of the series
                                series.data.push({
                                    name: item.Key,
                                    y: parseFloat(item.Value)
                                })
                            });
                            options1.series.push(series);
                            //Create the chart
                            statusChart = new Highcharts.Chart(options1);
                        });

            $(function () {
                // Ẩn tất cả .accordion trừ accordion đầu tiên
                $("#accordion .pncontent:not(:first)").hide();
                $("#accordion .panel .pntitle").children().first().attr('class', 'pnup');
                // Áp dụng sự kiện click vào thẻ h3
                $("#accordion .pntitle").click(function () {
                    showhidePanel($(this));
                });

            });

            $(function () {
                // Ẩn tất cả .accordion trừ accordion đầu tiên
                //$("#accordionleft .pncontent:not(:first)").hide();
                $("#accordionleft .panel .pntitle").children().first().attr('class', 'pnup');
                // Áp dụng sự kiện click vào thẻ h3
                $("#accordionleft .pntitle").click(function () {
                    showhidePanelLeft($(this));
                });
            });
        });
        function showhidePanel(obj) {
            $accordion = obj.next();
            // Kiểm tra nếu đang ẩn thì sẽ hiện và ẩn các phần tử khác
            // Nếu đang hiện thì click vào h3 sẽ ẩn
            if ($accordion.is(':hidden') === true) {
                $("#accordion .pncontent").slideUp();
                $("#accordion .pnup").attr('class', 'pndown');
                $accordion.slideDown();
                obj.children().first().attr('class', 'pnup');
            } else {
                $accordion.slideUp();
                obj.children().first().attr('class', 'pndown');
            }
        }

        function showhidePanelLeft(obj) {
            $accordion = obj.next();
            // Kiểm tra nếu đang ẩn thì sẽ hiện và ẩn các phần tử khác
            // Nếu đang hiện thì click vào h3 sẽ ẩn
            if ($accordion.is(':hidden') === true) {
                $("#accordionleft .pncontent").slideUp();
                $("#accordionleft .pnup").attr('class', 'pndown');
                $accordion.slideDown();
                obj.children().first().attr('class', 'pnup');
            } else {
                $accordion.slideUp();
                obj.children().first().attr('class', 'pndown');
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ModuleName" runat="server">
    <%= ServiceRequestPageInfo.ComName%>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="FunctionTitle" runat="server">
    <%=CommonFunc.GetCurrentMenu(Request.RawUrl,true)%>
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="LoginRoles" runat="server">
</asp:Content>
