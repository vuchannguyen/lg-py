<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <table width="450px" border="0" cellpadding="0" cellspacing="0">
        <tr>
            <td class="chlft">
                <ul>
                    <li><a href="/TrainingCenterAdmin/index" class="ems"></a></li>
                    <li><a href="/TrainingCenterAdmin/class" class="jobrequest"></a></li>
                    <li></li>
                    <br />
                    
                    <li><a href="/TrainingCenterAdmin/profcourse" class="prm"></a></li>
                    <li><a id="linkMoving" href="/TrainingCenterAdmin/engcourse" class="moving"></a></li>                  
                    <li></li>
                    <br />
                    <li><a id="linkPayroll" href="/TrainingMaterial/MaterialList/<%= Constants.TRAINING_MATERIAL_PROF_COURSE %>" class="payroll"></a></li>
                    <li><a id="linkAsset" href="/TrainingMaterial/MaterialList/<%= Constants.TRAINING_MATERIAL_ENG %>" class="asset"></a></li>
                    <li><a id="linkSkillset" href="/TrainingMaterial/MaterialList/<%=Constants.TRAINING_MATERIAL_CATEGORY %>" class="skillset"></a></li>                    
                </ul>
            </td>
        </tr>
    </table>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="PageTitle" runat="server">
<%= TrainingMaterialPageInfo.ComName + CommonPageInfo.AppSepChar + TrainingMaterialPageInfo.FuncDashBoard + 
        CommonPageInfo.AppSepChar + CommonPageInfo.AppName%>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="ModuleName" runat="server">
<%= TrainingMaterialPageInfo.ComName%>
</asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="FunctionTitle" runat="server">
<%=CommonFunc.GetCurrentMenu(Request.RawUrl).Trim().TrimEnd('»')%>
</asp:Content>

<asp:Content ID="Content6" ContentPlaceHolderID="LoginRoles" runat="server">
</asp:Content>
