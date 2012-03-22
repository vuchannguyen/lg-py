using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CRM.Library.Common;

namespace CRM.Models
{
    public class ContractlLogDao : BaseDao
    {
        //
        // GET: /ContractlLogDao/
        private CommonLogDao commonDao = new CommonLogDao();
        /// <summary>
        /// Write Log For Employee
        /// </summary>
        /// <param name="oldInfo"></param>
        /// <param name="newInfo"></param>
        /// <param name="action"></param>
        public void WriteLogForContract(Contract oldInfo, Contract newInfo, ELogAction action)
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
                    case ELogAction.Insert:
                        // Insert to Master Log
                        commonDao.InsertMasterLog(logId, newInfo.CreatedBy, ELogTable.Contract.ToString(), action.ToString());
                        // Write Insert Log
                        WriteInsertLogForContract(newInfo, logId);
                        break;
                    case ELogAction.Update:
                        // Insert to Master Log
                        commonDao.InsertMasterLog(logId, newInfo.UpdatedBy, ELogTable.Contract.ToString(), action.ToString());
                        // Write Update Log
                        bool isUpdated = WriteUpdateLogForContract(newInfo, logId);
                        if (!isUpdated)
                        {
                            commonDao.DeleteMasterLog(logId);
                        }
                        break;
                    case ELogAction.Delete:
                        // Insert to Master Log
                        commonDao.InsertMasterLog(logId, newInfo.UpdatedBy, ELogTable.Contract.ToString(), action.ToString());
                        // Write Delete Log
                        string key = "Contract on " + newInfo.StartDate.ToString(Constants.DATETIME_FORMAT_VIEW) + " of " + newInfo.EmployeeId + " [" + newInfo.Employee.FirstName + " " + newInfo.Employee.LastName + "]";
                        commonDao.InsertLogDetail(logId, "ContractId", "Key for Delete", key, null);
                        break;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Write Insert Log For Contract
        /// </summary>
        /// <param name="objInfo"></param>
        /// <param name="logId"></param>
        private void WriteInsertLogForContract(Contract objInfo, string logId)
        {
            try
            {
                if ((objInfo != null) && (logId != null))
                {
                    // Insert to Log Detail                       
                    commonDao.InsertLogDetail(logId, "EmployeeId", "Full Name", null, objInfo.EmployeeId + " [" + objInfo.Employee.FirstName + " " + objInfo.Employee.LastName + "]");
                    commonDao.InsertLogDetail(logId, "StartDate", "Start Date", null, objInfo.StartDate.ToString(Constants.DATETIME_FORMAT_VIEW));
                    if (objInfo.EndDate.HasValue)
                    {
                        commonDao.InsertLogDetail(logId, "EndDate", "End Date", null, objInfo.EndDate.Value.ToString(Constants.DATETIME_FORMAT_VIEW));
                    }
                    commonDao.InsertLogDetail(logId, "ContractType", "Contract Type", null, objInfo.ContractType1.ContractTypeName);
                    if (!string.IsNullOrEmpty(objInfo.ContractFile))
                    {
                        string[] array = objInfo.ContractFile.TrimEnd(Constants.FILE_CHAR_PREFIX).Split(Constants.FILE_CHAR_PREFIX);
                        if (array.Count() > 0)
                        {
                            foreach (string item in array)
                            {
                                commonDao.InsertLogDetail(logId, "AttachFile", "Attach File", null, item);
                            }
                        }
                    }
                    if (!string.IsNullOrEmpty(objInfo.Comment))
                    {
                        commonDao.InsertLogDetail(logId, "Comment", "Comment", null, objInfo.Comment);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Write Update Log For Employee
        /// </summary>
        /// <param name="newInfo"></param>
        /// <param name="logId"></param>
        /// <returns></returns>
        private bool WriteUpdateLogForContract(Contract newInfo, string logId)
        {
            bool isUpdated = false;
            try
            {
                // Get old info
                Contract oldInfo = new ContractRenewalDao().GetById(newInfo.ContractId);

                if ((oldInfo != null) && (newInfo != null) && (logId != null))
                {
                    if (newInfo.StartDate != oldInfo.StartDate)
                    {
                        commonDao.InsertLogDetail(logId, "StartDate", "Start Date", oldInfo.StartDate.ToString(Constants.DATETIME_FORMAT_VIEW), newInfo.StartDate.ToString(Constants.DATETIME_FORMAT_VIEW));
                        isUpdated = true;
                    }
                    if (newInfo.EndDate != oldInfo.EndDate)
                    {
                        commonDao.InsertLogDetail(logId, "EndDate", "End Date",oldInfo.EndDate.HasValue?oldInfo.EndDate.Value.ToString(Constants.DATETIME_FORMAT_VIEW):"",newInfo.EndDate.HasValue?newInfo.EndDate.Value.ToString(Constants.DATETIME_FORMAT_VIEW):"");
                        isUpdated = true;
                    }
                    if (newInfo.ContractType != oldInfo.ContractType)
                    {
                        ContractType obj = new ContractRenewalDao().GetContractTypeById(newInfo.ContractType);
                        if (obj != null)
                        {
                            commonDao.InsertLogDetail(logId, "ContractType", "Contract Type", oldInfo.ContractType1.ContractTypeName, obj.ContractTypeName);
                            isUpdated = true;
                        }
                    }
                    if (newInfo.ContractFile != oldInfo.ContractFile)
                    {
                        if (!string.IsNullOrEmpty(newInfo.ContractFile))
                        {
                            string[] arrayNew = newInfo.ContractFile.TrimEnd(Constants.FILE_CHAR_PREFIX).Split(Constants.FILE_CHAR_PREFIX);
                            if (!string.IsNullOrEmpty(oldInfo.ContractFile))
                            {
                                foreach (string item in oldInfo.ContractFile.TrimEnd(Constants.FILE_CHAR_PREFIX).Split(Constants.FILE_CHAR_PREFIX))//case delete file
                                {
                                    if (!newInfo.ContractFile.TrimEnd(Constants.FILE_CHAR_PREFIX).Split(Constants.FILE_CHAR_PREFIX).Contains(item))
                                    {
                                        commonDao.InsertLogDetail(logId, "AttachFile", "Attach File", item, "");
                                        isUpdated = true;
                                    }
                                }
                            }
                            foreach (string fileNew in arrayNew) //case add new file
                            {
                                if (!oldInfo.ContractFile.Contains(fileNew))
                                {
                                    commonDao.InsertLogDetail(logId, "AttachFile", "Attach File", "", fileNew);
                                    isUpdated = true;
                                }
                            }
                        }
                    }
                    if (newInfo.Comment != oldInfo.Comment)
                    {
                        commonDao.InsertLogDetail(logId, "Comment", "Comment", oldInfo.Comment, newInfo.Comment);
                        isUpdated = true;
                    }
                    if (isUpdated)
                    {
                        // Insert Key Name
                        string key = "Contract on " +oldInfo.StartDate.ToString(Constants.DATETIME_FORMAT_VIEW)+ " of " + oldInfo.EmployeeId + " [" + oldInfo.Employee.FirstName + " " + oldInfo.Employee.LastName + "]";
                        commonDao.InsertLogDetail(logId, "EmployeeId", "Key for Update", key, null);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return isUpdated;
        }

        public void WriteLogForRemoveContractCV(Contract newInfo, ELogAction action)
        {

            try
            {
                bool isUpdated = false;
                if (newInfo == null)
                {
                    return;
                }
                MasterLog objMasterLog = new MasterLog();
                string logId = commonDao.UniqueId;
                commonDao.InsertMasterLog(logId, newInfo.UpdatedBy, ELogTable.Contract.ToString(), action.ToString());
                Contract oldInfo = new ContractRenewalDao().GetById(newInfo.ContractId);

                if ((oldInfo != null) && (newInfo != null) && (logId != null))
                {
                    foreach (string item in oldInfo.ContractFile.TrimEnd(Constants.FILE_CHAR_PREFIX).Split(Constants.FILE_CHAR_PREFIX))
                    {
                        if (!newInfo.ContractFile.TrimEnd(Constants.FILE_CHAR_PREFIX).Split(Constants.FILE_CHAR_PREFIX).Contains(item))
                        {
                            commonDao.InsertLogDetail(logId, "AttachFile", "Attach File", item, "");
                            isUpdated = true;
                        }
                    }
                    
                    if (isUpdated)
                    {
                        // Insert Key Name
                        string key ="Contract on Start Date " + oldInfo.StartDate.ToString(Constants.DATETIME_FORMAT_VIEW) + (oldInfo.EndDate.HasValue?" to " +oldInfo.EndDate.Value.ToString(Constants.DATETIME_FORMAT_VIEW):"") + " of " +  oldInfo.EmployeeId + " [" + oldInfo.Employee.FirstName + " " + oldInfo.Employee.LastName + "]";
                        commonDao.InsertLogDetail(logId, "EmployeeId", "Key for Delete", key, null);
                    }
                    else
                    {
                        commonDao.DeleteMasterLog(logId);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
