using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CRM.Models.Entities
{
    public class GeneralSurveyReportEntity 
    {
        public string administrator { get; set; }
        public string numSR { get; set; }
        public string averageMarks { get; set;}
        public string percentOfResponse { get; set; }
    }
}
