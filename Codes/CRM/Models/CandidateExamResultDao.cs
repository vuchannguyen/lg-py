using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CRM.Library.Common;
using CRM.Library.Utils;
using System.Data.Common;
using System.Text.RegularExpressions;
using System.Linq.Dynamic;

namespace CRM.Models
{
    public class CandidateExamResultDao : BaseDao
    {
        #region Get Data

        public List<LOT_Candidate_Exam_Result> GetListByCandidateExamID(long candidateExamId)
        {
            return dbContext.LOT_Candidate_Exam_Results.Where(p => p.CandidateExamId == candidateExamId).OrderByDescending(p => p.LOT_Section.SectionTypeId).ToList<LOT_Candidate_Exam_Result>();
        }

        public LOT_Candidate_Exam_Result GetResultByCandidateExamIDAndSectionID(long candidateExamId, int sectionId)
        {
            return dbContext.LOT_Candidate_Exam_Results.Where(p => p.CandidateExamId == candidateExamId && p.SectionId == sectionId).OrderByDescending(p => p.LOT_Section.SectionTypeId).FirstOrDefault<LOT_Candidate_Exam_Result>();
        }

        public List<LOT_Candidate_Exam_Result> GetResultByCandidateExamIDAndSectionTypeID(long candidateExamId, int sectionTypeId)
        {
            return dbContext.LOT_Candidate_Exam_Results.Where(p => p.CandidateExamId == candidateExamId && p.LOT_Section.SectionTypeId == sectionTypeId).OrderBy(p => p.LOT_Section.SectionName).ToList<LOT_Candidate_Exam_Result>();
        }

        public LOT_Candidate_Exam_Result GetVerbalResultByCandidateExamID(long candidateExamId)
        {
            return dbContext.LOT_Candidate_Exam_Results.Where(p => p.CandidateExamId == candidateExamId && 
                                                                   p.LOT_Section.SectionTypeId == Constants.LOT_VERBAL_SECTION_TYPE_ID &&
                                                                   p.SectionId != Constants.LOT_VERBAL_SKILL_ID).OrderByDescending(p => p.LOT_Section.SectionTypeId).FirstOrDefault<LOT_Candidate_Exam_Result>();
        }

        #endregion

        public Message UpdateMarkByPerson(long assId, bool isActive)
        {
            try
            {
                AssetMaster obj = dbContext.AssetMasters.Where(p => p.ID == assId).FirstOrDefault();
                if (obj == null)
                    return new Message(MessageConstants.E0005, MessageType.Error,
                        "Selected Asset", "system");
                obj.IsActive = isActive;
                dbContext.SubmitChanges();
                return new Message(MessageConstants.I0001, MessageType.Info, "Asset " + obj.ID +
                    " in group \"" + obj.ID + "\"", "set " + (isActive ? "active" : "inactive"));
            }
            catch
            {
                return new Message(MessageConstants.E0007, MessageType.Error);
            }
        }

        private bool IsDublicateCandidateMark(LOT_Candidate_Exam_Result objUI)
        {
            bool isDublicateName = true;
            LOT_Candidate_Exam_Result dublicateName = dbContext.LOT_Candidate_Exam_Results.Where(a => a.CandidateExamId.Equals(objUI.CandidateExamId)
                                                                                                   && a.SectionId.Equals(objUI.SectionId)
                                                                                                ).FirstOrDefault<LOT_Candidate_Exam_Result>();
            if (dublicateName != null && dublicateName.CandidateExamId == objUI.CandidateExamId && dublicateName.SectionId == objUI.SectionId)
            {
                isDublicateName = true;
            }
            else
            {
                isDublicateName = false;
            }
            return isDublicateName;
        }

        private bool IsDBChanged(LOT_Candidate_Exam_Result objUI, LOT_Candidate_Exam_Result objDb)
        {
            bool isChannged = true;
            if (objDb.UpdateDate.ToString() == objUI.UpdateDate.ToString())
            {
                isChannged = false;
            }
            return isChannged;
        }

        public Message Insert(LOT_Candidate_Exam_Result objUI)
        {
            Message msg = null;
            try
            {
                if (!IsDublicateCandidateMark(objUI))
                {
                    objUI.CreateDate = System.DateTime.Now;
                    objUI.UpdateDate = System.DateTime.Now;
                    dbContext.LOT_Candidate_Exam_Results.InsertOnSubmit(objUI);
                    dbContext.SubmitChanges();

                    msg = new Message(MessageConstants.I0001, MessageType.Info, new CandidateExamDao().GetCandidateName(objUI.LOT_Candidate_Exam) + "'s " + objUI.LOT_Section.SectionName + " mark in " + objUI.LOT_Candidate_Exam.LOT_Exam.Title + " exam", "added");
                }
                else
                {
                    msg = new Message(MessageConstants.E0020, MessageType.Error, new CandidateExamDao().GetCandidateName(objUI.LOT_Candidate_Exam) + "'s " + objUI.LOT_Section.SectionName + " mark", objUI.LOT_Candidate_Exam.LOT_Exam.Title + " exam");
                }
            }
            catch (Exception)
            {
                msg = new Message(MessageConstants.E0007, MessageType.Error);
            }
            return msg;
        }

        public Message Update(LOT_Candidate_Exam_Result objUI)
        {
            Message msg = null;
            try
            {
                LOT_Candidate_Exam_Result objDb = null;
                if (objUI.SectionId == Constants.LOT_SECTION_VERBAL_TOEIC_ID || objUI.SectionId == Constants.LOT_SECTION_VERBAL_LEVEL_ID)
                {
                    objDb = this.GetVerbalResultByCandidateExamID(objUI.CandidateExamId);
                }
                else
                {
                    objDb = this.GetResultByCandidateExamIDAndSectionID(objUI.CandidateExamId, objUI.SectionId);
                }

                if (objDb != null)
                {
                    Update(objUI, objDb, ref msg);
                }
                else
                {
                    msg = new Message(MessageConstants.E0005, MessageType.Error, new CandidateExamDao().GetCandidateName(objUI.LOT_Candidate_Exam) + "'s " + objUI.LOT_Section.SectionName + " mark", objUI.LOT_Candidate_Exam.LOT_Exam.Title + " exam");
                }
            }
            catch (Exception)
            {
                msg = new Message(MessageConstants.E0007, MessageType.Error);
            }

            return msg;
        }

        private void Update(LOT_Candidate_Exam_Result objUI, LOT_Candidate_Exam_Result objDb, ref Message msg)
        {
            if (!IsDBChanged(objUI, objDb))
            {
                objDb.Mark = objUI.Mark;
                objDb.SectionId = objUI.SectionId;
                objDb.MarkBy = objUI.MarkBy;
                objDb.Comment = objUI.Comment;
                objDb.UpdateDate = DateTime.Now;
                objDb.UpdatedBy = objUI.UpdatedBy;

                dbContext.SubmitChanges();
                msg = new Message(MessageConstants.I0001, MessageType.Info, new CandidateExamDao().GetCandidateName(objUI.LOT_Candidate_Exam) + "'s " + objUI.LOT_Section.SectionName + " mark in " + objUI.LOT_Candidate_Exam.LOT_Exam.Title + " exam", "updated");
            }
            else
            {
                msg = new Message(MessageConstants.E0025, MessageType.Info, new CandidateExamDao().GetCandidateName(objUI.LOT_Candidate_Exam) + "'s " + objUI.LOT_Section.SectionName + " mark in " + objUI.LOT_Candidate_Exam.LOT_Exam.Title + " exam");
            }
        }

    }
}