<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
   
</head>
<body>
<%  
    string currentSkillScore = ViewData[CommonDataKey.TRAINING_CENTER_SCORE_SKILL].ToString();
    string currentVerbalScore = ViewData[CommonDataKey.TRAINING_CENTER_SCORE_VERBAL].ToString();
    var currentVerbalLevel = ViewData[CommonDataKey.TRAINING_CENTER_LEVEL_VERBAL];

%> 
    <div>
        <table cellspacing="0" cellpadding="0" border="0" width="100%" class="view">
        <tr>
                <td colspan="3" class="input" style="color: #0066CC; font-weight:bold; padding: 6px;text-align:center;">
                    Current English Score
                </td>
            </tr>
        <tr >
            <td class="label" style="border-right: 1px solid #CCCCCC;width:80px"></td>
            <td class="label" style="text-align:center;width:100px;border-right: 1px solid #CCCCCC">Score</td>
            <td class="label" style="text-align:center;width:100px;border-right: 1px solid #CCCCCC">Level</td>
        </tr>
        <tr >
                <td class="label" style="border-right: 1px solid #CCCCCC">
                    Skill
                </td>
                <td class="input"  style="text-align:center;border-right: 1px solid #CCCCCC">
                    <%= currentSkillScore%>
                </td>
                <td class="input"  style="text-align:center;border-right: 1px solid #CCCCCC">
                    <%=CommonFunc.GetEngLishSkillLevel(ConvertUtil.ConvertToInt(currentSkillScore))%>
                </td>
            </tr>
            <tr>
                <td class="label" style="border-right: 1px solid #CCCCCC">
                    Verbal
                </td>
                <td class="input" style="text-align:center;border-right: 1px solid #CCCCCC">
                    <%= currentVerbalScore%>
                </td>
                <td class="input" style="text-align:center;border-right: 1px solid #CCCCCC">
                    <%= currentVerbalLevel != null ? currentVerbalLevel : ""%>
                </td>
            </tr>
        </table>  
    </div>
</body>
</html>
