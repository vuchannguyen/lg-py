using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CRM.Models
{
    public class PositionDao : BaseDao
    {
        #region Public methods

        public List<JobTitleLevel> GetList()
        {
            return dbContext.JobTitleLevels.Where(c => c.DeleteFlag == false).ToList<JobTitleLevel>();
        }


        public JobTitle GetByJobTitleByLevelID(int positionID)
        {

            JobTitleLevel jobTitleLevel = dbContext.JobTitleLevels.Where(c => c.ID == positionID && c.DeleteFlag == false).FirstOrDefault<JobTitleLevel>();
            if(jobTitleLevel != null)
            {
                return dbContext.JobTitles.Where(c => c.JobTitleId == jobTitleLevel.JobTitleId).FirstOrDefault<JobTitle>();
            }
            return null;            
        }

        public List<JobTitleLevel> GetListByJobTitleId(int jobTitleId)
        {
            return dbContext.JobTitleLevels.Where(c => c.JobTitleId == jobTitleId && c.DeleteFlag == false).ToList<JobTitleLevel>();
        }


        public List<JobTitleLevel> GetListByLevelId(int positionID)
        {
            JobTitle jobTitle = GetByJobTitleByLevelID(positionID);
            if (jobTitle != null)
            {
                return GetListByJobTitleId(jobTitle.JobTitleId);
            }
            return null;
        }

        //public IList<JobTitleLevel> GetJobTitleListByDepId(int depId)
        //{
        //    return dbContext.JobTitleLevels.Where(p => p.JobTitle != null && p.JobTitle.DepartmentId == depId
        //        && p.DeleteFlag == false).ToList<JobTitleLevel>();
        //}

        public IList<JobTitle> GetJobTitleListByDepId(int depId)
        {
            return dbContext.JobTitles.Where(p => p.Department != null && p.DepartmentId == depId
                && p.DeleteFlag == false).ToList<JobTitle>();
        }

        public IList<JobTitleLevel> GetJobTitleLevelListByJobTitleId(int jobTitleId)
        {
            return dbContext.JobTitleLevels.Where(p => p.JobTitle != null && p.JobTitleId == jobTitleId
                && p.DeleteFlag == false).ToList<JobTitleLevel>();
        }

        #endregion
    }
}