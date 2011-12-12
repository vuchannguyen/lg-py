using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CRM.Library.Common;

namespace CRM.Models
{
    public class ResultTemplateDao : BaseDao
    {
        public List<ResultTemplate> GetList()
        {
            return dbContext.ResultTemplates.ToList<ResultTemplate>();
        }

        public ResultTemplate GetById(string id)
        {
            return dbContext.ResultTemplates.Where(p => p.Id == id).FirstOrDefault<ResultTemplate>();
        }
    }
}