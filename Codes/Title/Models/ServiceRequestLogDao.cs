using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CRM.Library.Common;
using CRM.Models;

namespace CRM.Models
{
    public class ServiceRequestLogDao : BaseDao
    {
        private CommonLogDao commonDao = new CommonLogDao();
        #region Methods

        /// <summary>
        /// Write Log For Employee
        /// </summary>
        /// <param name="oldInfo"></param>
        /// <param name="newInfo"></param>
        /// <param name="action"></param>
        public void WriteLogForSR(SR_ServiceRequest oldInfo, SR_ServiceRequest newInfo, ELogAction action)
        {

            try
            {
                if (newInfo == null)
                {
                    return;
                }              
                
                MasterLog objMasterLog = new MasterLog();

                string logId = commonDao.UniqueId;

                switch (action)
                {
                    
                    case ELogAction.Delete:
                        // Insert to Master Log
                        commonDao.InsertMasterLog(logId, newInfo.UpdatedBy, ELogTable.ServiceRequest.ToString(), action.ToString());
                        // Write Delete Log
                        string key = newInfo.ID + " [" + newInfo.Title + "]";
                        commonDao.InsertLogDetail(logId, "ID", "Key for Delete", key, null);
                        break;
                    default:
                        break;
                        
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        #endregion
    }
}