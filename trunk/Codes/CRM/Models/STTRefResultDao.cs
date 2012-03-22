using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CRM.Library.Common;

namespace CRM.Models
{
    public class STTRefResultDao:BaseDao
    {
        public List<STT_RefResult> GetList()
        {
            return dbContext.STT_RefResults.Where(p => p.DeleteFlag == false).OrderByDescending(p => p.UpdatedDate).ToList<STT_RefResult>();
        }

        public List<STT_RefResult> GetList(string sttId)
        {
            return dbContext.STT_RefResults.Where(p => p.SttID == sttId && p.DeleteFlag == false).OrderByDescending(p => p.UpdatedDate).ToList<STT_RefResult>();
        }

        public STT_RefResult GetById(string id)
        {
            return dbContext.STT_RefResults.Where(p => p.SttID == id && p.DeleteFlag == false).FirstOrDefault<STT_RefResult>();
        }

        public void InsertMulti(List<STT_RefResult> list)
        {
            dbContext.STT_RefResults.InsertAllOnSubmit(list);
                dbContext.SubmitChanges();
        } 
      
        public Message Insert(STT_RefResult objUI)
        {
            Message msg = null;
            try
            {
                if (objUI != null)
                {
                    STT objSTTDb = new STTDao().GetById(objUI.SttID);
                    if (objSTTDb != null)
                    {
                            objUI.DeleteFlag = false;
                            objUI.CreatedDate = DateTime.Now;
                            objUI.UpdatedDate = DateTime.Now;
                            dbContext.STT_RefResults.InsertOnSubmit(objUI);  
                            dbContext.SubmitChanges();          
                            new STTDao().UpdateResult(objUI.SttID, objUI.ResultId);
                            new STTLogDao().WriteLogForUpdateResult(objUI, ELogAction.Update);
                            // Show success message
                            msg = new Message(MessageConstants.I0001, MessageType.Info, "Result of STT " + objUI.SttID, "added");
                    }
                }
            }
            catch
            {
                // Show system error
                msg = new Message(MessageConstants.E0007, MessageType.Error);
            }
            return msg;
        }

        public Message UpdateResult(STT_RefResult objUI)
        {
            Message msg = null;
            try
            {
                if (objUI != null)
                {
                    STT_RefResult objDb = GetById(objUI.SttID);
                    if (objDb != null)
                    {
                        new STTLogDao().WriteLogForEditResult(objUI, ELogAction.Update);
                        new STTDao().UpdateResult(objUI.SttID,objUI.ResultId);                       
                        objDb.ResultId = objUI.ResultId;
                        objDb.EndDate = objUI.EndDate;
                        objDb.UpdatedDate = objUI.UpdatedDate;
                        objDb.UpdatedBy = objUI.UpdatedBy;
                        objDb.Remarks = objUI.Remarks;
                        objDb.Attachfile = objUI.Attachfile;
                        dbContext.SubmitChanges();
                        // Show success message
                        msg = new Message(MessageConstants.I0001, MessageType.Info, "STT " + objUI.SttID, "update");
                    }
                }
            }
            catch
            {
                // Show system error
                msg = new Message(MessageConstants.E0007, MessageType.Error);
            }
            return msg;
        }

        public Message UpdateAttachFile(STT_RefResult objUI)
        {
            Message msg = null;
            try
            {
                if (objUI != null)
                {
                    // Get current group in dbContext
                    STT_RefResult objDb = GetById(objUI.SttID);

                    if (objDb != null)
                    {
                        new STTLogDao().WriteLogForRemoveAttachFile(objUI, ELogAction.Delete);
                        // Update info by objUI                       
                        objDb.Attachfile = objUI.Attachfile;
                        objDb.UpdatedDate = DateTime.Now;
                        objDb.UpdatedBy = objUI.UpdatedBy;
                        // Submit changes to dbContext
                        dbContext.SubmitChanges();
                        // Show success message
                        STT obj = new STTDao().GetById(objUI.SttID);
                        msg = new Message(MessageConstants.I0001, MessageType.Info, obj.FirstName + " " + obj.MiddleName + " " + obj.LastName, "update");
                    }
                }
            }
            catch (Exception)
            {
                // Show system error
                msg = new Message(MessageConstants.E0007, MessageType.Error);
            }

            return msg;
        }

        private bool IsValidUpdateDate(DateTime date, STT_RefResult objDb, out Message msg)
        {
            bool isValid = false;
            msg = null;

            try
            {
                    if (objDb != null)
                    {
                        if (objDb.UpdatedDate.ToString().Equals(date.ToString()))
                        {
                            isValid = true;
                        }
                        else
                        {
                            msg = new Message(MessageConstants.E0025, MessageType.Error, "STT " + objDb.STT);
                        }
                    }
            }
            catch
            {
                throw;
            }

            return isValid;
        }
    }
}