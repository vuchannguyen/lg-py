using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CRM.Models
{
    public class SRSurveyDao:BaseDao
    {
        public SR_Survey GetSurvey()
        {
            return dbContext.SR_Surveys.FirstOrDefault();
        }
    }
}