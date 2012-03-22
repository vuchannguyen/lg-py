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
    /// Exam Question Section DAO
    /// </summary>
    public class ExamQuestionSectionDAO: BaseDao
    {
        /// <summary>
        /// Get List
        /// </summary>
        /// <returns></returns>
        public List<LOT_ExamQuestion_Section> GetList()
        {
            return dbContext.LOT_ExamQuestion_Sections.ToList<LOT_ExamQuestion_Section>();
        }


        /// <summary>
        /// GetByID
        /// </summary>
        /// <returns></returns>
        public LOT_ExamQuestion_Section GetByID(int id)
        {
            return dbContext.LOT_ExamQuestion_Sections.Where(c => c.ID == id).FirstOrDefault<LOT_ExamQuestion_Section>();
        }


        /// <summary>
        /// GetByExQuestionIDAndSectionID
        /// </summary>
        /// <returns></returns>
        public LOT_ExamQuestion_Section GetByExQuestionIDAndSectionID(int examQuestionID, int sectionID)
        {
            return dbContext.LOT_ExamQuestion_Sections.Where(c => c.ExamQuestionID == examQuestionID && c.SectionID == sectionID).FirstOrDefault<LOT_ExamQuestion_Section>();
        }

        /// <summary>
        /// GetListByExamQuestionID
        /// </summary>
        /// <returns></returns>
        public List<LOT_ExamQuestion_Section> GetListByExamQuestionID(int examQuestionID)
        {
            return dbContext.LOT_ExamQuestion_Sections.Where(c => c.ExamQuestionID == examQuestionID).ToList<LOT_ExamQuestion_Section>();
        }

        /// <summary>
        /// Get LOT_ExamQuestion_SectionListID
        /// </summary>
        /// <param name="candidateExamId"></param>
        /// <returns></returns>
        public List<LOT_ExamQuestion_Section> GetLOT_ExamQuestion_SectionListID(int candidateExamId)
        {                       
            return (from CA in dbContext.LOT_CandidateAnswers 
                     join EQS in dbContext.LOT_ExamQuestion_Sections 
                     on CA.ExamQuestionSectionID equals EQS.ID
                    where CA.CandidateExamID == candidateExamId && EQS.SectionID != Constants.LOT_WRITING_SKILL_ID && EQS.SectionID != Constants.LOT_PROGRAMMING_SKILL_ID
                     select EQS).Distinct().ToList<LOT_ExamQuestion_Section>();
        }


        /// <summary>
        /// Insert to db
        /// </summary>
        /// <param name="objUI"></param>
        /// <returns></returns>
        public Message Insert(LOT_ExamQuestion_Section objUI)
        {
            Message msg = null;
            try
            {
                if (objUI != null)
                {
                    // Set more info                    

                    dbContext.LOT_ExamQuestion_Sections.InsertOnSubmit(objUI);
                    dbContext.SubmitChanges();

                    // Show success message
                    msg = new Message(MessageConstants.I0001, MessageType.Info, "Exam Question-Section ", "added");

                }
            }
            catch (Exception)
            {
                // Show system error
                msg = new Message(MessageConstants.E0007, MessageType.Error);
            }
            return msg;
        }


        /// <summary>
        /// Update to db
        /// </summary>
        /// <param name="objUI"></param>
        /// <returns></returns>
        public Message Update(LOT_ExamQuestion_Section objUI)
        {
            Message msg = null;
            try
            {
                if (objUI != null)
                {                                      
                    LOT_ExamQuestion_Section objDB = GetByExQuestionIDAndSectionID(objUI.ExamQuestionID, objUI.SectionID);
                    if (objDB != null)
                    {
                        objDB.MaxMark = objUI.MaxMark;
                        objDB.NumberOfQuestions = objUI.NumberOfQuestions;
                        objDB.IsRandom = objUI.IsRandom;                        
                        dbContext.SubmitChanges();
                    }
                    else
                    {
                        msg = new Message(MessageConstants.E0005, MessageType.Error, "Section name '" + objUI.LOT_Section.SectionName + "'", "database");
                    }
                    // Show success message
                    msg = new Message(MessageConstants.I0001, MessageType.Info, "Exam Question-Section ", "updated");

                }
            }
            catch (Exception)
            {
                // Show system error
                msg = new Message(MessageConstants.E0007, MessageType.Error);
            }
            return msg;
        }

        /// <summary>
        /// Delete by exam question id
        /// </summary>
        /// <param name="objUI"></param>
        public Message DeleteByExamQuestionID(int examQuestionID)
        {
            Message msg = null;
            try
            {
                // Get current group in dbContext
                List<LOT_ExamQuestion_Section> list =  GetListByExamQuestionID(examQuestionID);
                dbContext.LOT_ExamQuestion_Sections.DeleteAllOnSubmit(list);
                dbContext.SubmitChanges();
                
                msg = new Message(MessageConstants.I0001, MessageType.Info, "Exam Question-Section ", "deleted");
            }             
            catch (Exception)
            {
                // Show system error
                msg = new Message(MessageConstants.E0036, MessageType.Error);
            }
            return msg;
        }


        /// <summary>
        /// Delete by object
        /// </summary>
        /// <param name="objUI"></param>
        public Message Delete(LOT_ExamQuestion_Section objGUI)
        {
            Message msg = null;
            try
            {
                // Get current group in dbContext
                LOT_ExamQuestion_Section item = GetByExQuestionIDAndSectionID(objGUI.ExamQuestionID, objGUI.SectionID);
                dbContext.LOT_ExamQuestion_Sections.DeleteOnSubmit(item);
                dbContext.SubmitChanges();

                msg = new Message(MessageConstants.I0001, MessageType.Info, "Exam Question-Section ", "deleted");
            }
            catch (Exception)
            {
                // Show system error
                msg = new Message(MessageConstants.E0036, MessageType.Error);
            }
            return msg;
        }
    }
}