using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CRM.Library.Common;
using System.Data.Common;
using System.Text.RegularExpressions;

namespace CRM.Models
{
    public class EformDao : BaseDao
    {

        /// <summary>
        /// Get max index of eform
        /// </summary>
        /// <param name="masterID">ID of master Eform</param>
        /// <param name="personID">ID of Personal</param>
        /// <param name="personType">kind of personal</param>
        /// <returns></returns>
        private int GetMaxIndex(string masterID, string personID, int personType)
        {
            int result = 0;

            result = dbContext.sp_GetMaxIndexEForm(masterID, personID, personType);

            return result;
        }

        public EFormMaster GetEFormMasterByID(string id)
        {
            return dbContext.EFormMasters.Where(c => c.Code == id).FirstOrDefault();
        }

        /// <summary>
        /// Get index  from EForm
        /// </summary>
        /// <param name="masterID">ID of master</param>
        /// <param name="personID">ID of personnal</param>
        /// <param name="personType">Kind of personnal</param>
        /// <param name="round">round</param>
        /// <returns>int</returns>
        public int GetIndexEform(string masterID, string personID, int personType, int round)
        {
            return dbContext.sp_GetIndexEForm(masterID, personID, personType, round);
        }

        /// <summary>
        /// Delete e form
        /// </summary>
        /// <param name="masterID">string</param>
        /// <param name="personID">string</param>
        /// <param name="personType">int</param>
        /// <param name="round">int</param>
        /// <returns>Message</returns>
        public Message DeleteEform(string masterID, string personID, int personType, int round,string userUpdate)
        {
            Message msg = null;
            EForm eForm =  dbContext.EForms.Where(e => e.MasterID == masterID && e.PersonID == personID && e.FormIndex == round) .FirstOrDefault();

            try
            {
                if (eForm != null)
                {
                    dbContext.EForms.DeleteOnSubmit(eForm);
                    dbContext.SubmitChanges();
                    msg = new Message(MessageConstants.I0001, MessageType.Info, "The record(s) ", "deleted");
                }
            }
            catch (Exception)
            {
                dbContext.SubmitChanges();
                msg = new Message(MessageConstants.E0007, MessageType.Error);
            }

            return msg;
        }

        /// <summary>
        /// Insert EForm
        /// </summary>
        /// <param name="obj">EForm</param>
        /// <returns>Message</returns>
        public Message InsertEForm(EForm obj)
        {
            
            Message msg = null;
            obj.CreateDate = DateTime.Now;
            // Get max index then add 1;
            //obj.FormIndex = GetMaxIndex(obj.MasterID, obj.PersonID, obj.PersonType) + 1;

            try
            {
                if (obj != null)
                {
                    int result = dbContext.sp_InsertEForm(obj.MasterID, obj.PersonID, obj.PersonType, obj.FormIndex, obj.CreatedBy);
                   
                    dbContext.SubmitChanges();
                   // new EFormLogDao().WriteLogForEForm(null, obj, ELogAction.Insert);
                    msg = new Message(MessageConstants.I0001, MessageType.Info, "New record(s) ", "added");
                }              

            }
            catch (Exception)
            {
                dbContext.SubmitChanges();
                // Show system error
                msg = new Message(MessageConstants.E0007, MessageType.Error);
            }
            
            return msg;
        }
        public Message Inser(List<EForm_Detail> objList, PerformanceReview pr, PRComment comment)
        {
            Message msg = null;
            DbTransaction trans = null;
            try
            {
                dbContext.Connection.Open();
                trans = dbContext.Connection.BeginTransaction();
                dbContext.Transaction = trans;

                new PerformanceReviewDao().UpdateResolution(pr, comment);
                msg = Inser(objList);
                trans.Commit();
            }
            catch (Exception)
            {
                // Show system error
                msg = new Message(MessageConstants.E0007, MessageType.Error);
                trans.Rollback();
            }
            return msg;

        }
        public Message Inser(List<EForm_Detail> objList, PerformanceReview pr)
        {
            Message msg = null;
            DbTransaction trans = null;
            try
            {
                dbContext.Connection.Open();
                trans = dbContext.Connection.BeginTransaction();
                dbContext.Transaction = trans;
                
                new PerformanceReviewDao().UpdateHolder(pr);
                msg = Inser(objList);
                if (msg.MsgType != MessageType.Error)
                {
                    msg = new Message(MessageConstants.I0001, MessageType.Info, "Performance Review \"" + pr.ID + "\"", "added");
                    trans.Commit();
                }
            }
            catch (Exception)
            {
                // Show system error
                msg = new Message(MessageConstants.E0007, MessageType.Error);
                trans.Rollback();
            }
            return msg;

        }

        public Message Inser(EForm_Detail ObjUI)
        {
            Message msg = null;
            try
            {
                if (ObjUI != null)
                {
                  
                    dbContext.EForm_Details.InsertOnSubmit(ObjUI);
                    dbContext.SubmitChanges();

                    msg = new Message(MessageConstants.I0001, MessageType.Info, "New record(s) ", "added");
                }
            }
            catch (Exception)
            {
                // Show system error
                msg = new Message(MessageConstants.E0007, MessageType.Error);
            }
            return msg;

        }
        public Message Inser(List<EForm_Detail> objList)
        {
            Message msg = null;
            try
            {
                if (objList != null)
                {

                    dbContext.EForm_Details.InsertAllOnSubmit(objList);
                    dbContext.SubmitChanges();

                    msg = new Message(MessageConstants.I0001, MessageType.Info,"Interview form", "added");
                }
            }
            catch (Exception)
            {
                // Show system error
                msg = new Message(MessageConstants.E0007, MessageType.Error);
            }
            return msg;

        }
        public Message Update(List<EForm_Detail> newList, PerformanceReview pr, PRComment comment)
        {
            Message msg = null;
            DbTransaction trans = null;
            bool canSuccess = true;
            try
            {
                dbContext.Connection.Open();
                trans = dbContext.Connection.BeginTransaction();
                dbContext.Transaction = trans;
                List<EForm_Detail> oldList = dbContext.EForm_Details.Where(e => e.EFormID == newList[0].EFormID).ToList();
                dbContext.EForm_Details.DeleteAllOnSubmit(oldList);
                dbContext.EForm_Details.InsertAllOnSubmit(newList);
                dbContext.SubmitChanges();
                new PerformanceReviewDao().UpdateResolution(pr, comment);
                if (canSuccess)
                {
                    // Show succes message
                    msg = new Message(MessageConstants.I0001, MessageType.Info, "Interview", "updated");
                    trans.Commit();
                }
                else
                {
                    trans.Rollback();
                }

            }
            catch (Exception)
            {
                canSuccess = false;
                if (trans != null) trans.Rollback();
                // Show system error
                msg = new Message(MessageConstants.E0007, MessageType.Error);
            }
            return msg;
        }
        public Message Update(List<EForm_Detail> newList, PerformanceReview pr)
        {
            Message msg = null;
            DbTransaction trans = null;
            bool canSuccess = true;
            try
            {
                dbContext.Connection.Open();
                trans = dbContext.Connection.BeginTransaction();
                dbContext.Transaction = trans;
                List<EForm_Detail> oldList = dbContext.EForm_Details.Where(e => e.EFormID == newList[0].EFormID).ToList();
                dbContext.EForm_Details.DeleteAllOnSubmit(oldList);
                dbContext.EForm_Details.InsertAllOnSubmit(newList);
                dbContext.SubmitChanges();
                new PerformanceReviewDao().UpdateHolder(pr);
                if (canSuccess)
                {
                    // Show succes message
                    msg = new Message(MessageConstants.I0001, MessageType.Info, "Performance Review \"" + pr.ID + "\"", "updated");
                    trans.Commit();
                }
                else
                {
                    trans.Rollback();
                }

            }
            catch (Exception)
            {
                canSuccess = false;
                if (trans != null) trans.Rollback();
                // Show system error
                msg = new Message(MessageConstants.E0007, MessageType.Error);
            }
            return msg;
        }
        public Message Update(List<EForm_Detail> newList)
        {
            Message msg = null;
            DbTransaction trans = null;
            bool canSuccess = true;
            try
            {
                dbContext.Connection.Open();
                trans = dbContext.Connection.BeginTransaction();
                dbContext.Transaction = trans;
                List<EForm_Detail> oldList = dbContext.EForm_Details.Where(e => e.EFormID == newList[0].EFormID).ToList();                
                dbContext.EForm_Details.DeleteAllOnSubmit(oldList);
                dbContext.EForm_Details.InsertAllOnSubmit(newList);
                dbContext.SubmitChanges();

                if (canSuccess)
                {
                    // Show succes message
                    msg = new Message(MessageConstants.I0001, MessageType.Info, "Interview", "updated");
                    trans.Commit();
                }
                else
                {
                    trans.Rollback();
                }
               
            }
            catch (Exception)
            {
                canSuccess = false;
                if (trans != null) trans.Rollback();
                // Show system error
                msg = new Message(MessageConstants.E0007, MessageType.Error);
            }
            return msg;
        }
        public EForm GetEformByPersonID_MasterID(string personID, string masterID)
        {
            return dbContext.EForms.Where(e=>e.MasterID==masterID && e.PersonID==personID).SingleOrDefault();
        }

        public EForm GetLastEformByPersonID_MasterID(string personID, string masterID,int personType,int formIndex)
        {
            return dbContext.EForms.Where(e => e.MasterID == masterID && e.PersonID == personID
                && e.PersonType == personType && e.FormIndex == formIndex).SingleOrDefault();
        }
        public List<EForm_Detail> GetEformDetailByEFormID(int id)
        {
            return dbContext.EForm_Details.Where(e => e.EFormID == id).ToList();
        }
        public List<EFormMaster> GetAllEFormMaster()
        {
            return dbContext.EFormMasters.ToList();
        }
        public List<EFormMaster> GetListByPrefix(string prefix)
        {
            return GetAllEFormMaster().Where(p => p.Code.Split('-')[0].Equals(prefix)).ToList();
        }
    }
}