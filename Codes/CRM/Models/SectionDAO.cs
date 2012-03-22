using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Common;
using CRM.Library.Common;
using CRM.Models;

namespace CRM.Models
{
    /// <summary>
    /// The Data Access Object of section
    /// </summary>
    public class SectionDAO : BaseDao
    {
        /// <summary>
        /// Get list exclude the question type "Listening Topic Question"
        /// </summary>
        /// <returns></returns>
        public List<LOT_Section> GetList()
        {
            return dbContext.LOT_Sections.Where(c => c.ID != Constants.LOT_LISTENING_QUESTION && c.ID != Constants.LOT_COMPREHENSION_QUESTION_ID).ToList<LOT_Section>();
        }

        public List<Entities.SectionTypeEntity> GetSectionTypeByCandidateExamID(long candidateExamId)
        {
            var sqlQuery = from sectionType in dbContext.LOT_Candidate_Exam_Results
                           where sectionType.CandidateExamId == candidateExamId
                           select new Entities.SectionTypeEntity()
                           {
                               ID = sectionType.LOT_Section.LOT_Section_Type.Id,
                               SectionTypeName = sectionType.LOT_Section.LOT_Section_Type.Name,
                               Description = sectionType.LOT_Section.LOT_Section_Type.Description
                           };
            return sqlQuery.Distinct().ToList();
        }

        /// <summary>
        /// Get none verbal list in section
        /// </summary>
        /// <param name="examQuestionId"></param>
        /// <returns></returns>
        public List<sp_GetSectionNoneVerbalListByExamQuestionIDResult> GetSectionNoneVerbalListByExamQuestionId(int examQuestionId)
        {
            return dbContext.sp_GetSectionNoneVerbalListByExamQuestionID(examQuestionId).ToList<sp_GetSectionNoneVerbalListByExamQuestionIDResult>();
        }

        public List<LOT_Section> GetListLevelAndToeicSection()
        {
            return dbContext.LOT_Sections.Where(c => c.ID == Constants.LOT_SECTION_VERBAL_LEVEL_ID || c.ID == Constants.LOT_SECTION_VERBAL_TOEIC_ID).ToList<LOT_Section>();
        }

        public List<LOT_Section> GetListExcludeSubVerbal()
        {
            return dbContext.LOT_Sections.Where(c => c.ID != Constants.LOT_LISTENING_QUESTION && c.ID != Constants.LOT_COMPREHENSION_QUESTION_ID
                && (c.SectionTypeId != Constants.LOT_SECTION_VERBAL_TYPE || c.ID == Constants.LOT_VERBAL_SKILL_ID)).OrderBy(p => p.SectionName).ToList<LOT_Section>();
        } 
        /// <summary>
        /// Get list of all section
        /// </summary>
        /// <returns>List<LOT_Section></returns>
        public List<LOT_Section> GetListAll()
        {
            return dbContext.LOT_Sections.OrderBy(p => p.SectionName).ToList<LOT_Section>();
        }
        /// <summary>
        /// Get the section by its id
        /// </summary>
        /// <param name="sectionID"></param>
        /// <returns>LOT_Section</returns>
        public LOT_Section GetByID(int sectionID)
        {
            return (LOT_Section)dbContext.LOT_Sections.Where(p => (p.ID == sectionID))
                .FirstOrDefault<LOT_Section>();
        }

    }
}