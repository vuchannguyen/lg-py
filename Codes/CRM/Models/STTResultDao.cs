using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CRM.Library.Common;

namespace CRM.Models
{
    public class STTResultDao : BaseDao
    {
        public List<STT_Result> GetList()
        {
            return dbContext.STT_Results.ToList<STT_Result>();
        }

        public STT_Result GetById(int id)
        {
            return dbContext.STT_Results.Where(p => p.ID == id).FirstOrDefault<STT_Result>();
        }

        public STT_Result GetByName(string name)
        {
            return dbContext.STT_Results.Where(p => p.Name == name).FirstOrDefault<STT_Result>();
        }
    }
}