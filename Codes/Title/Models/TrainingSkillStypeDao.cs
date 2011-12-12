using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CRM.Library.Common;

namespace CRM.Models
{
    public class TrainingSkillTypeDao : BaseDao
    {
        public List<Training_SkillType> GetList()
        {
            return dbContext.Training_SkillTypes.ToList<Training_SkillType>();
        }

        public Training_SkillType GetById(int id)
        {
            return dbContext.Training_SkillTypes.Where(p => p.ID == id).FirstOrDefault<Training_SkillType>();
        }

        public Training_SkillType GetByName(string name)
        {
            return dbContext.Training_SkillTypes.Where(p => p.Name == name).FirstOrDefault<Training_SkillType>();
        }
    }
}