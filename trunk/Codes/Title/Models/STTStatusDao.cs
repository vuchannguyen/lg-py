using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CRM.Library.Common;

namespace CRM.Models
{
    public class STTStatusDao : BaseDao
    {
        public List<STT_Status> GetList()
        {
            return dbContext.STT_Status.ToList<STT_Status>();
        }

        public List<STT_Status> GetListForAddNew()
        {
            return dbContext.STT_Status.Where(p => p.ID.Equals(Constants.STT_STATUS_OJT) || p.ID.Equals(Constants.STT_STATUS_IN_CLASS) || p.ID.Equals(Constants.STT_STATUS_ON_LEAVE)).ToList<STT_Status>();
        }

        public STT_Status GetById(int id)
        {
            return dbContext.STT_Status.Where(p => p.ID == id).FirstOrDefault<STT_Status>();
        }

        public STT_Status GetByName(string name)
        {
            return dbContext.STT_Status.Where(p => p.Name == name).FirstOrDefault<STT_Status>();
        }
    }
}