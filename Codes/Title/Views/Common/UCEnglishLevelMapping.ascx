<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<style type="text/css">
    .subheading
    {
        font-weight: bolder;
    }
    div.skill
    {
        display: inline-block;    
        width:300px;
        margin:10px;
    }
    div.verbal
    {
        display: inline-block;    
        width:200px;
        margin:10px;
    }
    div.esmapping
    {
        display: inline-block;
        width:500px;
        margin:10px;
    }
    div.rndmapping
    {
        display: inline-block;
        width:500px;
        margin:10px;
    }
    .grid td.jobtitle
    {
        font-weight:bolder;
    }
    
    .grid th.number
    {
        width:20px;    
    }
    .grid th.title
    {
        width:280px;    
        text-align:left;
    }
    .grid th.skill
    {
        width:100px;    
    }
    .grid th.verbal
    {
        width:100px;    
    }
</style>
<%
   
        TrainingLevelMappingDao mappingDao = new TrainingLevelMappingDao ();
        var listSkillMapping = mappingDao.GetListSkillMapping();
        var listVerbalMapping = mappingDao.GetListVerbalMapping();
        var listTitleMappingES = mappingDao.GetListTitleMapping(Constants.DEPARTMENT_ES_ID).OrderBy(p=>p.TitleID).ToList();
        var listTitleMappingRnD = mappingDao.GetListTitleMapping(Constants.DEPARTMENT_RnD_ID);
%>
    <h2 class="heading" style="margin-bottom:10px">English Level Mapping Table</h2>
    <div class="skill">
        <h3 class="subheading">Skill</h3>
        <table class="grid">
            <tr>
                <th>Skill Level</th>
                <th>Skill Test</th>
                <th>TOEIC Test</th>
            </tr>
            <%
        foreach (var item in listSkillMapping)
        {
            string sSkillTest = item.SkillTestTo.HasValue ?
                item.SkillTestFrom + " - " + item.SkillTestTo : ">" + (item.SkillTestFrom - 1);
            string sToeicTest = item.ToeicTestTo.HasValue ?
                item.ToeicTestFrom + " - " + item.ToeicTestTo : ">" + (item.ToeicTestFrom - 1);
            %>
            <tr>
                <td align="center"><%:item.SkillLevel%></td>
                <td align="center"><%:sSkillTest%></td>
                <td align="center"><%:sToeicTest%></td>
            </tr>
            <%
        }
            
            %>
        </table>
    </div>

    <div class="verbal">
        <h3 class="subheading">Verbal</h3>
        <table class="grid">
            <tr>
                <th>Verbal Level</th>
                <th>TOEIC Test</th>
            </tr>
            <%
        foreach (var item in listVerbalMapping)
        {
            string sToeicTest = item.ToeicTestTo.HasValue ?
                item.ToeicTestFrom + " - " + item.ToeicTestTo : ">" + (item.ToeicTestFrom - 1);
            %>
            <tr>
                <td align="center"><%:item.VerbalLevel%></td>
                <td align="center"><%:sToeicTest%></td>
            </tr>
            <%
        }
            
            %>
        </table>
    </div>
    <br />
    <div class="esmapping">
        <h3 class="subheading">Matching English Scale of ES</h3>
        <table class="grid">
            <tr>
                <th class="number">No.</th>
                <th class="title">Title</th>
                <th class="skill">Skill Level</th>
                <th class="verbal">Verbal Level</th>
            </tr>
            <%
        
        foreach (var item in listTitleMappingES)
        {
            %>
            <tr>
                <td align="center"><%:listTitleMappingES.IndexOf(item) + 1%></td>
                <td class="jobtitle" align="left"><%:item.TitleName%></td>
                <td align="center"><%:item.SkillLevel%></td>
                <td align="center"><%:item.VerbalLevel%></td>
            </tr>
            <%
        }
            
            %>
        </table>
    </div>

    <div class="rndmapping">
        <h3 class="subheading">Matching English Scale of RnD</h3>
        <table class="grid">
            <tr>
                <th class="number">No.</th>
                <th class="title">Title</th>
                <th class="skill">Skill Level</th>
                <th class="verbal">Verbal Level</th>
            </tr>
            <%
        string breakLine = "<br/>";
        string sTitle = "";
        int count = 0;
        for (int i = 0; i < listTitleMappingRnD.Count; i++ )
        {
            var item = listTitleMappingRnD[i];
            var nextItem = i != listTitleMappingRnD.Count - 1 ?  listTitleMappingRnD[i+1] : null;
            if (nextItem != null && nextItem.VerbalLevel == item.VerbalLevel)
            {
                sTitle += item.TitleName + breakLine;
                continue;
            }
            else
            {
                count++;
                sTitle += item.TitleName;
            %>
            <tr>
                <td align="center"><%:count%></td>
                <td class="jobtitle" align="left"><%=sTitle%></td>
                <td align="center"><%:item.SkillLevel%></td>
                <td align="center"><%:item.VerbalLevel%></td>
            </tr>
            <%
                sTitle = "";
            }
        }
            
            %>
        </table>
    </div>