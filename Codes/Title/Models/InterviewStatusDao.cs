using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CRM.Library.Common;

namespace CRM.Models
{
    public class InterviewStatusDao : BaseDao
    {
        public List<InterviewStatus> GetList()
        {
            return dbContext.InterviewStatus.ToList<InterviewStatus>();
        }

        public List<InterviewStatus> GetListForAddNew()
        {
            return dbContext.InterviewStatus.Where(p => p.Id.Equals(Constants.STT_STATUS_OJT)).ToList<InterviewStatus>();
        }

        public InterviewStatus GetById(int id)
        {
            return dbContext.InterviewStatus.Where(p => p.Id == id).FirstOrDefault<InterviewStatus>();
        }
    }
}