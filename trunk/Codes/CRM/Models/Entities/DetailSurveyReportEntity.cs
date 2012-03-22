using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CRM.Models.Entities
{
    public class DetailSurveyReportEntity
    {
        public string administrator { get; set; }
        public string srId { get; set; }
        public string answer { get; set; }
        public string comment { get; set; }
    }
}