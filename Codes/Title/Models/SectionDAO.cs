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
        /// <summary>
        /// Get list of all section
        /// </summary>
        /// <returns>List<LOT_Section></returns>
        public List<LOT_Section> GetListAll()
        {
            return dbContext.LOT_Sections.ToList<LOT_Section>();
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