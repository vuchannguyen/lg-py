using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CRM.Library.Common;

namespace CRM.Models
{
    public class InterviewResultDao : BaseDao
    {        
        public List<InterviewResult> GetList()
        {
            List<InterviewResult> list = new List<InterviewResult>();
            list = dbContext.InterviewResults.ToList<InterviewResult>();
            return list;
            
        }
        
        public InterviewResult GetById(int id)
        {
            return dbContext.InterviewResults.Where(p => p.Id == id).FirstOrDefault<InterviewResult>();
        }
    }
}