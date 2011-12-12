using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CRM.Library.Common;

namespace CRM.Models
{
    public class PurchaseRequestApprovalDao : BaseDao
    {        
        

        /// <summary>
        /// Delete purchase request approval by purchase id
        /// </summary>
        /// <param name="id">purchase request id</param>
        public void DeleteByPurchaseId(int id)
        {
            List<PurchaseRequestApproval> list = dbContext.PurchaseRequestApprovals.Where(c => c.RequestID == id).ToList();
            dbContext.PurchaseRequestApprovals.DeleteAllOnSubmit(list);
            dbContext.SubmitChanges();
        }

        /// <summary>
        /// Insert purchase request approval 
        /// </summary>
        /// <param name="objUI">PurchaseRequestApproval</param>
        /// <returns>Message</returns>
        public Message Insert(PurchaseRequestApproval objUI)
        {
            Message msg = null;
            try
            {
                if (objUI != null)
                {

                    dbContext.PurchaseRequestApprovals.InsertOnSubmit(objUI);
                    dbContext.SubmitChanges();                                        
                    //new PurchaseRequestLogDao().WriteLogForPurchaseRequest(null, objUI, ELogAction.Insert);
                }
            }
            catch (Exception)
            {
                // Show system error
                msg = new Message(MessageConstants.E0007, MessageType.Error);
            }
            return msg;
        }
    }
}