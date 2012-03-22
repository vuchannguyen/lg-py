using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CRM.Library.Common;

namespace CRM.Models
{
    public class InterviewResultTemplateDao : BaseDao
    {
        //public List<ResultTemplate> GetList()
        //{
        //    return dbContext.ResultTemplates.ToList<ResultTemplate>();
        //}
        public List<EFormMaster> GetList()
        {
            List<EFormMaster> list = new List<EFormMaster>();
            list = dbContext.EFormMasters.Where(e => e.Code.StartsWith(Constants.INTERVIEW_FORM_CODE)).ToList<EFormMaster>();
            return list;
            
        }
        //public ResultTemplate GetById(int id)
        //{
        //    return dbContext.ResultTemplates.Where(p => p.Id == id).FirstOrDefault<ResultTemplate>();
        //}
        public EFormMaster GetById(string code)
        {
            return dbContext.EFormMasters.Where(p => p.Code == code).FirstOrDefault<EFormMaster>();
        }
    }
}