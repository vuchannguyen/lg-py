using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CRM.Models.Entities
{
    public class DueDateEntity
    {
        public SR_Category Category { get; set; }
        public SR_Urgency Urgent { get; set; }
        public double hours { get; set; }
        public bool IsActive { get; set; }
        public int ID { get; set; }
        public string Remarks { get; set; }
        public string PCategoryName { get; set; }
    }
}