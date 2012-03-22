using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CRM.Models
{
    public class TrainingLevelMappingDao: BaseDao
    {
        public List<Training_SkillMapping> GetListSkillMapping()
        {
            return dbContext.Training_SkillMappings.ToList();
        }
        public List<Training_VerbalMapping> GetListVerbalMapping()
        {
            return dbContext.Training_VerbalMappings.ToList();
        }
        public List<sp_GetListEngLishTitleMappingResult> GetListTitleMapping(int departmentId)
        {
            return dbContext.sp_GetListEngLishTitleMapping(departmentId).ToList();
        }

        public Training_EnglishTitleMapping GetEnglishMappingByTitleId(int titleID)
        {
            return dbContext.Training_EnglishTitleMappings.Where(q => q.TitleId == titleID).FirstOrDefault();
        }
    }
}
