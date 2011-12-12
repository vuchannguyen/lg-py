<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="help-page">
    <table border="0" cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td class="col-lft">
                <div id="treecontrol">
		            <a title="Collapse the entire tree below" href="#">Collapse All</a> | 
		            <a title="Expand the entire tree below" href="#">Expand All</a> | 
		            <a title="Toggle the tree below, opening closed branches, closing open branches" href="#">Toggle All</a>
	            </div>
                Root
                <ul id="browser" class="filetree">
                    <%--<li><span class="folder">Management</span>
                        <ul>
                            <li><span class="folder">Employee</span>
                                <ul>
                                    <li><span class="file"><a href="javascript:;" onclick="getData('emp_active_list.htm')">Active List</a></span></li>
                                    <li><span class="file"><a href="javascript:;" onclick="getData('emp_resigned_list.htm')">Resigned List</a></span></li>
                                    <li><span class="file"><a href="javascript:;" onclick="getData('emp_stt_list.htm')">STT List</a></span></li>
                                </ul>
                            </li>
                            <li><span class="folder">Hiring Center</span>
                                <ul>
                                    <li><span class="file"><a href="javascript:;" onclick="getData('')">Candidate Profiles</a></span></li>
                                    <li><span class="file"><a href="javascript:;" onclick="getData('')">Interview List</a></span></li>
                                    <li><span class="file"><a href="javascript:;" onclick="getData('')">Interview History</a></span></li>
                                </ul>
                            </li>
                            <li><span class="folder">PTO</span>
                                <ul>
                                    <li><span class="file"><a href="javascript:;" onclick="getData('')">Anual Holiday</a></span></li>
                                    <li><span class="file"><a href="javascript:;" onclick="getData('')">PTO Admin</a></span></li>
                                    <li><span class="file"><a href="javascript:;" onclick="getData('')">PTO Report</a></span></li>
                                    <li><span class="file"><a href="javascript:;" onclick="getData('')">PTO Reminder</a></span></li>
                                </ul>
                            </li>
                            <li><span class="folder">Online Test</span>
                                <ul>
                                    <li><span class="file"><a href="javascript:;" onclick="getData('')">Exam List</a></span></li>
                                    <li><span class="file"><a href="javascript:;" onclick="getData('')">Exam Question</a></span></li>
                                    <li><span class="file"><a href="javascript:;" onclick="getData('')">Question List</a></span></li>                                    
                                </ul>
                            </li>
                            <li><span class="folder">Performance Review</span>
                                <ul>
                                    <li><span class="file"><a href="javascript:;" onclick="getData('')">PR List</a></span></li>                                    
                                </ul>
                            </li>
                        </ul>
                    </li>--%>
                    <li><span class="folder">Requests</span>
                        <ul>                            
                            <%--<li><span class="file"><a href="javascript:;" onclick="getData('')">Job Request</a></span></li>--%>
                            <li><span class="file"><a href="javascript:;" onclick="getData('req_workflow.htm')">PR Process Flow</a></span></li>
                            <li><span class="file"><a href="javascript:;" onclick="getData('req_instruction.htm')">Instruction for Requestor</a></span></li>
                        </ul>
                    </li>
                    <%--<li><span class="folder">System</span>
                        <ul>
                            <li><span class="folder">Master Data</span>
                                <ul>
                                    <li><span class="file"><a href="javascript:;" onclick="getData('emp_active_list.htm')">Job Title</a></span></li>
                                    <li><span class="file"><a href="javascript:;" onclick="getData('emp_resigned_list.htm')">Job Title Level</a></span></li>                                    
                                </ul>
                            </li>
                            <li><span class="folder">Admin Account</span>
                                <ul>
                                    <li><span class="file"><a href="javascript:;" onclick="getData('')">Group</a></span></li>
                                    <li><span class="file"><a href="javascript:;" onclick="getData('')">Group Permission</a></span></li>
                                    <li><span class="file"><a href="javascript:;" onclick="getData('')">Account</a></span></li>
                                </ul>
                            </li>
                            <li><span class="folder">Workflow</span>
                                <ul>
                                    <li><span class="file"><a href="javascript:;" onclick="getData('')">Workflow Accounts</a></span></li>
                                    <li><span class="file"><a href="javascript:;" onclick="getData('')">WF Role Resolution</a></span></li>                                    
                                </ul>
                            </li>
                            <li><span class="folder">System Logs</span>
                                <ul>
                                    <li><span class="file"><a href="javascript:;" onclick="getData('')">Data Logs</a></span></li>                                    
                                </ul>
                            </li>                            
                        </ul>
                    </li>--%>                                        
                </ul>                
            </td>
            <td class="col-rgh">
                <div id="help-content"><h1 class="heading">CRM Help Center</h1>Choose an item from the tree</div>
            </td>
        </tr>
    </table>
    </div>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PageTitle" runat="server">
    <%= HelpPageInfo.ComName %>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
    <link rel="stylesheet" href="/Scripts/Treeview/jquery.treeview.css" />	
	<script src="/Scripts/Treeview/lib/jquery.cookie.js" type="text/javascript"></script>
	<script src="/Scripts/Treeview/jquery.treeview.min.js" type="text/javascript"></script>
    <script type="text/javascript">
    $(document).ready(function(){			
	    $("#browser").treeview({
		    animated:"fast",
		    persist: "cookie",		    
		    control: "#treecontrol"
	    });
    });
    function getData(id) {        
        var url = "/Help/GetData/";
        var result = 'System Error';
        CRM.loading();
        $.ajax({
            async: false,
            cache: false,
            type: "POST",
            dataType: "html",
            timeout: 1000,
            url: url,
            data: ({
                'id': id
            }),
            success: function (msg) {
                $("#help-content").html(msg);
            }
        });
        CRM.completed();      
    };
</script>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ModuleName" runat="server">
    <%= HelpPageInfo.ComName %>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="FunctionTitle" runat="server">
    <%= HelpPageInfo.ComName%>
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="LoginRoles" runat="server">
</asp:Content>
