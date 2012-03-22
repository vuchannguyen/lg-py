using System.Collections.Generic;
using System.Linq;
using CRM.Models;

namespace CRM.Models
{
    /// <summary>
    /// Data Acess Object of answer
    /// </summary>
    public class AnswerDao : BaseDao
    {
        /// <summary>
        /// Get list of answer by question ID
        /// </summary>
        /// <param name="questionID"></param>
        /// <returns>List<LOT_Answer></returns>
        public List<LOT_Answer> GetListByQuestionID(int questionID)
        {
            return dbContext.LOT_Answers.Where
                (p => ((p.QuestionID == questionID) && !p.DeleteFlag))
                .OrderBy(p => (p.AnswerOrder)).ToList<LOT_Answer>();
        }

        /// <summary>
        /// Insert new answer
        /// </summary>
        /// <param name="answer"></param>
        public void Insert(LOT_Answer answer)
        {            
            dbContext.LOT_Answers.InsertOnSubmit(answer);
            dbContext.SubmitChanges();
        }

        /// <summary>
        /// Get an answer by ID
        /// </summary>
        /// <param name="answerID"></param>
        /// <returns>LOT_Answer</returns>
        public LOT_Answer GetByID(long answerID)
        {
            return dbContext.LOT_Answers.Where(p => (p.ID == answerID)).SingleOrDefault<LOT_Answer>();
        }

        /// <summary>
        /// Delete an answer
        /// </summary>
        /// <param name="answerUI"></param>
        public void Delete(LOT_Answer answerUI)
        {
            LOT_Answer answerDb = GetByID((int)answerUI.ID);
            if (answerDb != null)
            {
                answerDb.DeleteFlag = true;
                dbContext.SubmitChanges();
            }
        }
    }
}