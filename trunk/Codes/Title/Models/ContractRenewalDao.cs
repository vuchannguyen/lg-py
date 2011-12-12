using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CRM.Library.Common;
using System.Data.Common;
using System.Text.RegularExpressions;
namespace CRM.Models
{
    public class ContractRenewalDao : BaseDao
    {

        public List<Contract> GetList()
        {
            return dbContext.Contracts.Where(p => p.DeleteFlag == false).OrderByDescending(p => p.StartDate).ToList<Contract>();
        }

        public bool CheckOverlapDate(int id, string empID, string startDate, string endDate)
        {
            bool hasOverlapDate = false;
            if(dbContext.Func_CountOverlapDate(id, empID, startDate, endDate).Value == 1)
            {
               hasOverlapDate = true; 
            }
            return hasOverlapDate;
        }

        public List<Contract> GetList(string empId)
        {
            return dbContext.Contracts.Where(p => p.EmployeeId == empId && p.DeleteFlag == false).OrderByDescending(p => p.StartDate).ToList<Contract>();
        }

        public Contract GetById(int id)
        {
            return dbContext.Contracts.Where(p => p.ContractId == id && p.DeleteFlag == false).FirstOrDefault<Contract>();
        }

        public Contract GetEndDateEmptyById(string empId)
        {
            return dbContext.Contracts.Where(p => (p.EmployeeId == empId) && (p.EndDate == null) && (p.DeleteFlag == false)).FirstOrDefault<Contract>();
        }

        public Contract GetLastByEmpId(string empid)
        {
            return dbContext.Contracts.Where(c => c.EmployeeId.Equals(empid) && c.DeleteFlag == false).OrderByDescending(c => c.EndDate).FirstOrDefault<Contract>();
        }

        public Message UpdateNotification(int contractId)
        {
            Message msg = null;
            try
            {
                Contract objDb = GetById(contractId);
                if (objDb != null)
                {
                    objDb.NotificationClosed = true;
                    dbContext.SubmitChanges();
                    msg = new Message(MessageConstants.I0001, MessageType.Info, "Notification", "updated");
                }
            }
            catch
            {
                msg = new Message(MessageConstants.E0007, MessageType.Error);
            }
            return msg;
        }

        public Message UpdateNotificationCurrent(int contractId,DateTime endDate)
        {
            Message msg = null;
            try
            {
                Contract objDb = GetById(contractId);
                if (objDb != null)
                {
                    objDb.NotificationClosed = true;
                    objDb.EndDate = endDate;
                    dbContext.SubmitChanges();
                    msg = new Message(MessageConstants.I0001, MessageType.Info, "Notification", "updated");
                }
            }
            catch
            {
                msg = new Message(MessageConstants.E0007, MessageType.Error);
            }
            return msg;
        }


        public List<ContractType> GetListContractType()
        {
            return dbContext.ContractTypes.ToList<ContractType>();
        }

        public ContractType GetContractTypeById(int id)
        {
            return dbContext.ContractTypes.Where(p => p.ContractTypeId == id).FirstOrDefault<ContractType>();
        }

        public Message Insert(Contract objUI)
        {
            Message msg = null;
            try
            {
                if (objUI != null)
                {
                    // Set more info
                    objUI.NotificationClosed = false;
                    objUI.DeleteFlag = false;
                    objUI.CreatedDate = DateTime.Now;
                    objUI.UpdatedDate = DateTime.Now;
                    dbContext.Contracts.InsertOnSubmit(objUI);
                    dbContext.SubmitChanges();
                    new ContractlLogDao().WriteLogForContract(null, objUI, ELogAction.Insert);
                    // Show success message
                    msg = new Message(MessageConstants.I0001, MessageType.Info, "Contract on " + objUI.StartDate.ToString(Constants.DATETIME_FORMAT_VIEW), "added");
                }
            }
            catch 
            {
                // Show system error
                msg = new Message(MessageConstants.E0007, MessageType.Error);
            }
            return msg;
        }

        public Message Update(Contract objUI)
        {
            Message msg = null;
            try
            {
                if (objUI != null)
                {
                    // Get current group in dbContext
                    Contract objDb = GetById(objUI.ContractId);

                    if (objDb != null)
                    {
                        if (IsContractDuplicated(objUI))
                        {
                            // Show error message
                            msg = new Message(MessageConstants.E0004, MessageType.Error, "Contract on  " + objUI.StartDate.ToString(Constants.DATETIME_FORMAT_VIEW));
                        }
                        else
                        {
                            new ContractlLogDao().WriteLogForContract(objDb,objUI, ELogAction.Update);
                            // Update info by objUI
                            objDb.StartDate = objUI.StartDate;
                            objDb.EndDate = objUI.EndDate;
                            objDb.ContractFile = objUI.ContractFile;
                            objDb.ContractType = objUI.ContractType;
                            objDb.ContractNumber = objUI.ContractNumber;
                            objDb.UpdatedDate = DateTime.Now;
                            objDb.UpdatedBy = objUI.UpdatedBy;
                            objDb.Comment = objUI.Comment;
                            // Submit changes to dbContext
                            dbContext.SubmitChanges();

                            // Show success message
                            msg = new Message(MessageConstants.I0001, MessageType.Info, "Contract on " + objUI.StartDate.ToString(Constants.DATETIME_FORMAT_VIEW), "updated");
                        }
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

        public Message UpdateCV(Contract objUI)
        {
            Message msg = null;
            try
            {
                if (objUI != null)
                {
                    // Get current group in dbContext
                    Contract objDb = GetById(objUI.ContractId);

                    if (objDb != null)
                    {
                        new ContractlLogDao().WriteLogForRemoveContractCV(objUI,ELogAction.Delete);
                        // Update info by objUI                       
                        objDb.ContractFile = objUI.ContractFile;
                        objDb.UpdatedDate = DateTime.Now;
                        objDb.UpdatedBy = objUI.UpdatedBy;

                        // Submit changes to dbContext
                        dbContext.SubmitChanges();

                        // Show success message
                        msg = new Message(MessageConstants.I0001, MessageType.Info, "Contract", "uploaded");
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

        public void Delete(Contract objUI)
        {
            if (objUI != null)
            {
                // Get current group in dbContext
                Contract objDb = GetById(objUI.ContractId);

                if (objDb != null)
                {
                    // Set delete info

                    objDb.DeleteFlag = true;
                    objDb.UpdatedDate = DateTime.Now;
                    objDb.UpdatedBy = objUI.UpdatedBy;

                    // Submit changes to dbContext
                    dbContext.SubmitChanges();
                    new ContractlLogDao().WriteLogForContract(objDb, objUI, ELogAction.Delete);
                }
            }
        }

        private bool IsContractDuplicated(Contract objUI)
        {
            bool isDuplicated = false;
            List<Contract> objList = new List<Contract>();
            objList = dbContext.Contracts.Where(p => (
                     (p.ContractId != objUI.ContractId) && (p.ContractType == objUI.ContractType) && (p.EmployeeId == objUI.EmployeeId) && (p.DeleteFlag == false))).ToList<Contract>();
            if (objList.Count > 0)
            {
                isDuplicated = true;
            }
            return isDuplicated;
        }

        public Contract GetByEmpIdAndType(string empId, int contractType)
        {
            return dbContext.Contracts.Where(p => p.EmployeeId.Equals(empId) && p.ContractType == contractType).
                FirstOrDefault();
        }

        public DateTime? GetDateValue(string input)
        {
            string regexPattern = @"\d+"; 
            if (string.IsNullOrEmpty(input))
                return null;
            int iYear = int.Parse(Regex.Matches(input, regexPattern)[2].Value);
            int iMonth = int.Parse(Regex.Matches(input, regexPattern)[1].Value);
            int iDay = int.Parse(Regex.Matches(input, regexPattern)[0].Value);
            return new DateTime(iYear, iMonth, iDay);
        }

        public Message ImportContract(string empId, string number, string startDate, string endDate, int type)
        {
            Message msg = null;
            var principal = HttpContext.Current.User as AuthenticationProjectPrincipal;
            Contract ctr = GetByEmpIdAndType(empId, type);
            if (ctr != null)
            {
                ctr.ContractNumber = number;
                ctr.StartDate = GetDateValue(startDate).Value;
                ctr.EndDate = GetDateValue(endDate);
                ctr.UpdatedDate = DateTime.Now;
                ctr.UpdatedBy = principal.UserData.UserName;
                msg = Update(ctr);
            }
            else
            {
                ctr = new Contract();
                ctr.ContractNumber = number;
                ctr.EmployeeId = empId;
                ctr.ContractType = type;
                ctr.StartDate = GetDateValue(startDate).Value;
                ctr.EndDate = GetDateValue(endDate);
                ctr.UpdatedDate = ctr.CreatedDate = DateTime.Now;
                ctr.UpdatedBy = ctr.CreatedBy = principal.UserData.UserName;
                msg = Insert(ctr);
            }
            return msg;
        }

        public Message ImportContract(Employee emp, string _1st_Number, string _1st_From, string _1st_To, 
            string _2nd_Number, string _2nd_From, string _2nd_To, string _Permanent_Number, string _Permanent_From)
        {
            Message msg = null;
            if(!string.IsNullOrEmpty(_1st_Number))
            {
                msg = ImportContract(emp.ID, _1st_Number, _1st_From, _1st_To, Constants.FIRST_YEAR_CONTRACT);
            }
            if (msg.MsgType!= MessageType.Error && !string.IsNullOrEmpty(_2nd_Number))
            {
                msg = ImportContract(emp.ID, _2nd_Number, _2nd_From, _2nd_To, Constants.SECOND_YEAR_CONTRACT);
            }
            if (msg.MsgType != MessageType.Error && !string.IsNullOrEmpty(_Permanent_Number))
            {
                msg = ImportContract(emp.ID, _Permanent_Number, _Permanent_From, null, Constants.PERMANENT_CONTRACT);
            }
            return msg;  
        }
    }
}