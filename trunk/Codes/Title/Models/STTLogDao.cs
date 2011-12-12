using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CRM.Library.Common;
using CRM.Models;

namespace CRM.Models
{
    public class STTLogDao : BaseDao
    {
        private CommonLogDao commonDao = new CommonLogDao();
        #region Methods

        /// <summary>
        /// Write Log For STT
        /// </summary>
        /// <param name="oldInfo"></param>
        /// <param name="newInfo"></param>
        /// <param name="action"></param>
        public void WriteLogForSTT(STT oldInfo, STT newInfo, ELogAction action)
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
                        commonDao.InsertMasterLog(logId, newInfo.CreatedBy, ELogTable.STT.ToString(), action.ToString());
                        // Write Insert Log
                        WriteInsertLogForSTT(newInfo, logId);
                        break;
                    case ELogAction.Update:
                        // Insert to Master Log
                        commonDao.InsertMasterLog(logId, newInfo.UpdatedBy, ELogTable.STT.ToString(), action.ToString());
                        // Write Update Log
                        bool isUpdated = WriteUpdateLogForSTT(newInfo, logId);
                        if (!isUpdated)
                        {
                            commonDao.DeleteMasterLog(logId);
                        }
                        break;
                    case ELogAction.Delete:
                        // Insert to Master Log
                        commonDao.InsertMasterLog(logId, newInfo.UpdatedBy, ELogTable.STT.ToString(), action.ToString());
                        // Write Delete Log
                        string key = newInfo.ID + " [" + newInfo.FirstName + " " + newInfo.LastName + "]";                                               
                        commonDao.InsertLogDetail(logId, "STT_ID", "Key for Delete", key, null);
                        break;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void WriteLogForUpdatePersonal(STT newInfo, ELogAction action)
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
                commonDao.InsertMasterLog(logId, newInfo.UpdatedBy, ELogTable.STT.ToString(), action.ToString());
                STT oldInfo = new STTDao().GetById(newInfo.ID);

                if ((oldInfo != null) && (newInfo != null) && (logId != null))
                {                        
                    if (newInfo.VnIDIssueLocation != oldInfo.VnIDIssueLocation)
                    {
                        commonDao.InsertLogDetail(logId, "VnIDIssueLocation", "Vn Issue Location", oldInfo.VnIDIssueLocation, newInfo.VnIDIssueLocation);
                        isUpdated = true;
                    }
                    if (newInfo.Major != oldInfo.Major)
                    {
                        commonDao.InsertLogDetail(logId, "Major", "Major", oldInfo.Major, newInfo.Major);
                        isUpdated = true;
                    }   
                    if (newInfo.Race != oldInfo.Race)
                    {
                        commonDao.InsertLogDetail(logId, "Race", "Race", oldInfo.Race, newInfo.Race);
                        isUpdated = true;
                    }
                    if (newInfo.IDIssueLocation != oldInfo.IDIssueLocation)
                    {
                        commonDao.InsertLogDetail(logId, "IDIssueLocation", "Issue Location", oldInfo.IDIssueLocation, newInfo.IDIssueLocation);
                        isUpdated = true;
                    }
                    if (newInfo.Degree != oldInfo.Degree)
                    {
                        commonDao.InsertLogDetail(logId, "Degree", "Degree", oldInfo.Degree, newInfo.Degree);
                        isUpdated = true;
                    }
                    if (newInfo.OtherDegree != oldInfo.OtherDegree)
                    {
                        commonDao.InsertLogDetail(logId, "OtherDegree", "OtherDegree", oldInfo.OtherDegree, newInfo.OtherDegree);
                        isUpdated = true;
                    }
                    if (newInfo.VnPlaceOfOrigin != oldInfo.VnPlaceOfOrigin)
                    {
                        commonDao.InsertLogDetail(logId, "VnPlaceOfOrigin", "Vn Place Of Origin", oldInfo.VnPlaceOfOrigin, newInfo.VnPlaceOfOrigin);
                        isUpdated = true;
                    }
                    if (newInfo.VnPOB != oldInfo.VnPOB)
                    {
                        commonDao.InsertLogDetail(logId, "VnPOB", "Vn Place Of Birth", oldInfo.VnPOB, newInfo.VnPOB);
                        isUpdated = true;
                    }
                    if (newInfo.VnLastName != oldInfo.VnLastName)
                    {
                        commonDao.InsertLogDetail(logId, "VnLastName", "Vn Last Name", oldInfo.VnLastName, newInfo.VnLastName);
                        isUpdated = true;
                    }
                    if (newInfo.VnMiddleName != oldInfo.VnMiddleName)
                    {
                        commonDao.InsertLogDetail(logId, "VnMiddleName", "Vn Middle Name", oldInfo.VnMiddleName, newInfo.VnMiddleName);
                        isUpdated = true;
                    }
                    if (newInfo.VnFirstName != oldInfo.VnFirstName)
                    {
                        commonDao.InsertLogDetail(logId, "VnFirstName", "Vn First Name", oldInfo.VnFirstName, newInfo.VnFirstName);
                        isUpdated = true;
                    }
                    if (newInfo.LastName != oldInfo.LastName)
                    {
                        commonDao.InsertLogDetail(logId, "LastName", "Last Name", oldInfo.LastName, newInfo.LastName);
                        isUpdated = true;
                    }
                    if (newInfo.MiddleName != oldInfo.MiddleName)
                    {
                        commonDao.InsertLogDetail(logId, "MiddleName", "Middle Name", oldInfo.MiddleName, newInfo.MiddleName);
                        isUpdated = true;
                    }
                    if (newInfo.FirstName != oldInfo.FirstName)
                    {
                        commonDao.InsertLogDetail(logId, "FirstName", "First Name", oldInfo.FirstName, newInfo.FirstName);
                        isUpdated = true;
                    }
                    if (newInfo.Gender != oldInfo.Gender)
                    {
                        commonDao.InsertLogDetail(logId, "Gender", "Gender",oldInfo.Gender.HasValue?oldInfo.Gender == Constants.MALE ? "Male" : "Famale":"",newInfo.Gender.HasValue? newInfo.Gender == Constants.MALE ? "Male" : "Famale":"");
                        isUpdated = true;
                    }
                    if (newInfo.DOB != oldInfo.DOB)
                    {
                        commonDao.InsertLogDetail(logId, "DOB", "Date Of Birth",oldInfo.DOB.HasValue?oldInfo.DOB.Value.ToString(Constants.DATETIME_FORMAT_VIEW):"",newInfo.DOB.HasValue?newInfo.DOB.Value.ToString(Constants.DATETIME_FORMAT_VIEW):"");
                        isUpdated = true;
                    }
                    if (newInfo.POB != oldInfo.POB)
                    {
                        commonDao.InsertLogDetail(logId, "POB", "Place Of Birth", oldInfo.POB, newInfo.POB);
                        isUpdated = true;
                    }
                    if (newInfo.Nationality != oldInfo.Nationality)
                    {
                        commonDao.InsertLogDetail(logId, "Nationality", "Nationality", oldInfo.Nationality, newInfo.Nationality);
                        isUpdated = true;
                    }
                    if (newInfo.PlaceOfOrigin != oldInfo.PlaceOfOrigin)
                    {
                        commonDao.InsertLogDetail(logId, "PlaceOfOrigin", "Place Of Origin", oldInfo.PlaceOfOrigin, newInfo.PlaceOfOrigin);
                        isUpdated = true;
                    }
                    if (newInfo.IDNumber != oldInfo.IDNumber)
                    {
                        commonDao.InsertLogDetail(logId, "IDNumber", "ID Number", oldInfo.IDNumber, newInfo.IDNumber);
                        isUpdated = true;
                    }
                    if (newInfo.IssueDate != oldInfo.IssueDate)
                    {
                        commonDao.InsertLogDetail(logId, "IssueDate", "Issue Date",oldInfo.IssueDate.HasValue? oldInfo.IssueDate.Value.ToString(Constants.DATETIME_FORMAT_VIEW):"",newInfo.IssueDate.HasValue? newInfo.IssueDate.Value.ToString(Constants.DATETIME_FORMAT_VIEW):"");
                        isUpdated = true;
                    }
                    if (newInfo.MarriedStatus != oldInfo.MarriedStatus)
                    {
                        commonDao.InsertLogDetail(logId, "Married Status", "Married Status",oldInfo.MarriedStatus.HasValue? oldInfo.MarriedStatus == Constants.SINGLE ? "Single" : "Married":"",oldInfo.MarriedStatus.HasValue? newInfo.MarriedStatus == Constants.SINGLE ? "Single" : "Married":"");
                        isUpdated = true;
                    }
                    if (newInfo.Religion != oldInfo.Religion)
                    {
                        commonDao.InsertLogDetail(logId, "Religion", "Religion", oldInfo.Religion, newInfo.Religion);
                        isUpdated = true;
                    }
                    if (newInfo.STTStatusId != oldInfo.STTStatusId)
                    {
                        commonDao.InsertLogDetail(logId, "STTStatusId", "STT Status", oldInfo.STTStatusId.ToString(), new STTStatusDao().GetById(newInfo.STTStatusId).Name);
                        isUpdated = true;
                    }
                    if (isUpdated)
                    {
                        // Insert Key Name
                        string key = oldInfo.ID + " [" + oldInfo.FirstName + " " + oldInfo.MiddleName + " " + oldInfo.LastName + "]";
                        commonDao.InsertLogDetail(logId, "ID", "Key for Update", key, null);
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

        public void WriteLogForUpdateCompany(STT newInfo, ELogAction action)
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
                commonDao.InsertMasterLog(logId, newInfo.UpdatedBy, ELogTable.STT.ToString(), action.ToString());
                STT oldInfo = new STTDao().GetById(newInfo.ID);

                if ((oldInfo != null) && (newInfo != null) && (logId != null))
                {
                    if (newInfo.JR != oldInfo.JR)
                    {
                        commonDao.InsertLogDetail(logId, "JR", "Job Request", oldInfo.JR, newInfo.JR);
                        isUpdated = true;
                    }
                    if (newInfo.Project != oldInfo.Project)
                    {
                        commonDao.InsertLogDetail(logId, "Project", "Project", oldInfo.Project, newInfo.Project);
                        isUpdated = true;
                    }
                    if (newInfo.ManagerId != oldInfo.ManagerId)
                    {
                        commonDao.InsertLogDetail(logId, "Manager", "Manager", oldInfo.ManagerId, newInfo.ManagerId);
                        isUpdated = true;
                    }
                    if (newInfo.JRApproval != oldInfo.JRApproval)
                    {
                        commonDao.InsertLogDetail(logId, "JRApproval", "Job Request Approval", oldInfo.JRApproval, newInfo.JRApproval);
                        isUpdated = true;
                    }
                    if (newInfo.StartDate != oldInfo.StartDate)
                    {
                        commonDao.InsertLogDetail(logId, "StartDate", "Start Date", oldInfo.StartDate.ToString(Constants.DATETIME_FORMAT_VIEW), newInfo.StartDate.ToString(Constants.DATETIME_FORMAT_VIEW));
                        isUpdated = true;
                    }
                    if (newInfo.ExpectedEndDate != oldInfo.ExpectedEndDate)
                    {
                        commonDao.InsertLogDetail(logId, "ContractedDate", "Contracted Date",oldInfo.ExpectedEndDate.HasValue? oldInfo.ExpectedEndDate.Value.ToString(Constants.DATETIME_FORMAT_VIEW):"",newInfo.ExpectedEndDate.HasValue? newInfo.ExpectedEndDate.Value.ToString(Constants.DATETIME_FORMAT_VIEW):"");
                        isUpdated = true;
                    }
                    if (newInfo.DepartmentId != oldInfo.DepartmentId)
                    {
                        Department sub = new DepartmentDao().GetById(newInfo.DepartmentId);
                        commonDao.InsertLogDetail(logId, "DepartmentId", "Sub Department", oldInfo.Department.DepartmentName, sub.DepartmentName);
                        isUpdated = true;
                    }                    
                    if (newInfo.LaborUnion != oldInfo.LaborUnion)
                    {
                        commonDao.InsertLogDetail(logId, "LaborUnion", "Labor Union",oldInfo.LaborUnion.HasValue? oldInfo.LaborUnion == true ? "Yes" : "No":"",oldInfo.LaborUnion.HasValue? newInfo.LaborUnion == true ? "Yes" : "No":"");
                        isUpdated = true;
                    }
                    if (newInfo.LaborUnionDate != oldInfo.LaborUnionDate)
                    {
                        commonDao.InsertLogDetail(logId, "LaborUnionDate", "Labor Union date", oldInfo.LaborUnionDate.HasValue ? oldInfo.LaborUnionDate.Value.ToString(Constants.DATETIME_FORMAT_VIEW) : "", newInfo.LaborUnionDate.HasValue ? newInfo.LaborUnionDate.Value.ToString(Constants.DATETIME_FORMAT_VIEW) : "");
                        isUpdated = true;
                    }                         
                    if (isUpdated)
                    {
                        // Insert Key Name
                        string key = oldInfo.ID + " [" + oldInfo.FirstName + " " + oldInfo.MiddleName + " " + oldInfo.LastName + "]";
                        commonDao.InsertLogDetail(logId, "ID", "Key for Update", key, null);
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

        public void WriteLogForUpdateContact(STT newInfo, ELogAction action)
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
                commonDao.InsertMasterLog(logId, newInfo.UpdatedBy, ELogTable.STT.ToString(), action.ToString());
                STT oldInfo = new STTDao().GetById(newInfo.ID);

                if ((oldInfo != null) && (newInfo != null) && (logId != null))
                {
                    if (newInfo.HomePhone != oldInfo.HomePhone)
                    {
                        commonDao.InsertLogDetail(logId, "HomePhone", "Home Phone", oldInfo.HomePhone, newInfo.HomePhone);
                        isUpdated = true;
                    }
                    if (newInfo.CellPhone != oldInfo.CellPhone)
                    {
                        commonDao.InsertLogDetail(logId, "CellPhone", "Cell Phone", oldInfo.CellPhone, newInfo.CellPhone);
                        isUpdated = true;
                    }
                    if (newInfo.ExtensionNumber != oldInfo.ExtensionNumber)
                    {
                        commonDao.InsertLogDetail(logId, "ExtensionNumber", "Extension Number", oldInfo.ExtensionNumber, newInfo.ExtensionNumber);
                        isUpdated = true;
                    }
                    if (newInfo.LocationCode != oldInfo.LocationCode)
                    {
                        commonDao.InsertLogDetail(logId, "LocationCode", "LocationCode", CommonFunc.GenerateStringOfLocation(oldInfo.LocationCode), CommonFunc.GenerateStringOfLocation(newInfo.LocationCode));
                        isUpdated = true;
                    }
                    if (newInfo.SkypeId != oldInfo.SkypeId)
                    {
                        commonDao.InsertLogDetail(logId, "SkypeId", "SkypeId", oldInfo.SkypeId, newInfo.SkypeId);
                        isUpdated = true;
                    }
                    if (newInfo.YahooId != oldInfo.YahooId)
                    {
                        commonDao.InsertLogDetail(logId, "YahooId", "YahooId", oldInfo.YahooId, newInfo.YahooId);
                        isUpdated = true;
                    }
                    if (newInfo.PersonalEmail != oldInfo.PersonalEmail)
                    {
                        commonDao.InsertLogDetail(logId, "PersonalEmail", "Personal Email", oldInfo.PersonalEmail, newInfo.PersonalEmail);
                        isUpdated = true;
                    }
                    if (newInfo.OfficeEmail != oldInfo.OfficeEmail)
                    {
                        commonDao.InsertLogDetail(logId, "OfficeEmail", "Office Email", oldInfo.OfficeEmail, newInfo.OfficeEmail);
                        isUpdated = true;
                    }
                    if (newInfo.EmergencyContactName != oldInfo.EmergencyContactName)
                    {
                        commonDao.InsertLogDetail(logId, "EmergencyContactName", "Emergency Contact Name", oldInfo.EmergencyContactName, newInfo.EmergencyContactName);
                        isUpdated = true;
                    }
                    if (newInfo.EmergencyContactPhone != oldInfo.EmergencyContactPhone)
                    {
                        commonDao.InsertLogDetail(logId, "EmergencyContactPhone", "Emergency Contact Phone", oldInfo.EmergencyContactPhone, newInfo.EmergencyContactPhone);
                        isUpdated = true;
                    }
                    if (newInfo.EmergencyContactRelationship != oldInfo.EmergencyContactRelationship)
                    {
                        commonDao.InsertLogDetail(logId, "EmergencyContactRelationship", "Emergency Contact Relationship", oldInfo.EmergencyContactRelationship, newInfo.EmergencyContactRelationship);
                        isUpdated = true;
                    }
                    if (isUpdated)
                    {
                        // Insert Key Name
                        string key = oldInfo.ID + " [" + oldInfo.FirstName + " " + oldInfo.MiddleName + " " + oldInfo.LastName + "]";
                        commonDao.InsertLogDetail(logId, "ID", "Key for Update", key, null);
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

        public void WriteLogForUpdateAddress(STT newInfo, ELogAction action)
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
                commonDao.InsertMasterLog(logId, newInfo.UpdatedBy, ELogTable.STT.ToString(), action.ToString());
                STT oldInfo = new STTDao().GetById(newInfo.ID);

                if ((oldInfo != null) && (newInfo != null) && (logId != null))
                {
                    if (newInfo.PermanentAddress != oldInfo.PermanentAddress)
                    {
                        commonDao.InsertLogDetail(logId, "PermanentAddress", "Permanent Address", oldInfo.PermanentAddress, newInfo.PermanentAddress);
                        isUpdated = true;
                    }
                    if (newInfo.PermanentArea != oldInfo.PermanentArea)
                    {
                        commonDao.InsertLogDetail(logId, "PermanentArea", "Permanent Ward", oldInfo.PermanentArea, newInfo.PermanentArea);
                        isUpdated = true;
                    }
                    if (newInfo.PermanentDistrict != oldInfo.PermanentDistrict)
                    {
                        commonDao.InsertLogDetail(logId, "PermanentDistrict", "Permanent District", oldInfo.PermanentDistrict, newInfo.PermanentDistrict);
                        isUpdated = true;
                    }
                    if (newInfo.PermanentCityProvince != oldInfo.PermanentCityProvince)
                    {
                        commonDao.InsertLogDetail(logId, "PermanentCityProvince", "Permanent City Province", oldInfo.PermanentCityProvince, newInfo.PermanentCityProvince);
                        isUpdated = true;
                    }
                    if (newInfo.PermanentCountry != oldInfo.PermanentCountry)
                    {
                        commonDao.InsertLogDetail(logId, "PermanentCountry", "Permanent Country", oldInfo.PermanentCountry, newInfo.PermanentCountry);
                        isUpdated = true;
                    }
                    if (newInfo.TempAddress != oldInfo.TempAddress)
                    {
                        commonDao.InsertLogDetail(logId, "TempAddress", "Temp Address", oldInfo.TempAddress, newInfo.TempAddress);
                        isUpdated = true;
                    }
                    if (newInfo.TempArea != oldInfo.TempArea)
                    {
                        commonDao.InsertLogDetail(logId, "TempArea", "Temp Area", oldInfo.TempArea, newInfo.TempArea);
                        isUpdated = true;
                    }
                    if (newInfo.TempDistrict != oldInfo.TempDistrict)
                    {
                        commonDao.InsertLogDetail(logId, "TempDistrict", "Temp District", oldInfo.TempDistrict, newInfo.TempDistrict);
                        isUpdated = true;
                    }
                    if (newInfo.TempCityProvince != oldInfo.TempCityProvince)
                    {
                        commonDao.InsertLogDetail(logId, "TempCityProvince", "Temp City Province", oldInfo.TempCityProvince, newInfo.TempCityProvince);
                        isUpdated = true;
                    }
                    if (newInfo.TempCountry != oldInfo.TempCountry)
                    {
                        commonDao.InsertLogDetail(logId, "TempCountry", "Temp Country", oldInfo.TempCountry, newInfo.TempCountry);
                        isUpdated = true;
                    }
                    if (newInfo.VnPermanentAddress != oldInfo.VnPermanentAddress)
                    {
                        commonDao.InsertLogDetail(logId, "VnPermanentAddress", "Vn Permanent Address", oldInfo.VnPermanentAddress, newInfo.VnPermanentAddress);
                        isUpdated = true;
                    }
                    if (newInfo.VnPermanentArea != oldInfo.VnPermanentArea)
                    {
                        commonDao.InsertLogDetail(logId, "VnPermanentArea", "Vn Permanent Area", oldInfo.VnPermanentArea, newInfo.VnPermanentArea);
                        isUpdated = true;
                    }
                    if (newInfo.VnPermanentDistrict != oldInfo.VnPermanentDistrict)
                    {
                        commonDao.InsertLogDetail(logId, "VnPermanentDistrict", "Vn Permanent District", oldInfo.VnPermanentDistrict, newInfo.VnPermanentDistrict);
                        isUpdated = true;
                    }
                    if (newInfo.VnPermanentCityProvince != oldInfo.VnPermanentCityProvince)
                    {
                        commonDao.InsertLogDetail(logId, "VnPermanentCityProvince", "Vn Permanent City Province", oldInfo.VnPermanentCityProvince, newInfo.VnPermanentCityProvince);
                        isUpdated = true;
                    }
                    if (newInfo.VnPermanentCountry != oldInfo.VnPermanentCountry)
                    {
                        commonDao.InsertLogDetail(logId, "VnPermanentCountry", "Vn Permanent Country", oldInfo.VnPermanentCountry, newInfo.VnPermanentCountry);
                        isUpdated = true;
                    }
                    if (newInfo.VnTempAddress != oldInfo.VnTempAddress)
                    {
                        commonDao.InsertLogDetail(logId, "VnTempAddress", "Vn Temp Address", oldInfo.VnTempAddress, newInfo.VnTempAddress);
                        isUpdated = true;
                    }
                    if (newInfo.VnTempArea != oldInfo.VnTempArea)
                    {
                        commonDao.InsertLogDetail(logId, "VnTempArea", "Vn Temp Area", oldInfo.VnTempArea, newInfo.VnTempArea);
                        isUpdated = true;
                    }
                    if (newInfo.VnTempCityProvince != oldInfo.VnTempCityProvince)
                    {
                        commonDao.InsertLogDetail(logId, "VnTempCityProvince", "Vn Temp City Province", oldInfo.VnTempCityProvince, newInfo.VnTempCityProvince);
                        isUpdated = true;
                    }
                    if (newInfo.VnTempCountry != oldInfo.VnTempCountry)
                    {
                        commonDao.InsertLogDetail(logId, "VnTempCountry", "Vn Temp Country", oldInfo.VnTempCountry, newInfo.VnTempCountry);
                        isUpdated = true;
                    }
                    if (isUpdated)
                    {
                        // Insert Key Name
                        string key = oldInfo.ID + " [" + oldInfo.FirstName + " " + oldInfo.MiddleName + " " + oldInfo.LastName + "]";
                        commonDao.InsertLogDetail(logId, "ID", "Key for Update", key, null);
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

        public void WriteLogForUpdateBanks(STT newInfo, ELogAction action)
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
                commonDao.InsertMasterLog(logId, newInfo.UpdatedBy, ELogTable.STT.ToString(), action.ToString());
                STT oldInfo = new STTDao().GetById(newInfo.ID);

                if ((oldInfo != null) && (newInfo != null) && (logId != null))
                {
                    if (newInfo.BankName != oldInfo.BankName)
                    {
                        commonDao.InsertLogDetail(logId, "BankName", "Bank Name", oldInfo.BankName, newInfo.BankName);
                        isUpdated = true;
                    }
                    if (newInfo.BankAccount != oldInfo.BankAccount)
                    {
                        commonDao.InsertLogDetail(logId, "BankAccount", "Bank Account", oldInfo.BankAccount, newInfo.BankAccount);
                        isUpdated = true;
                    }
                    if (isUpdated)
                    {
                        // Insert Key Name
                        string key = oldInfo.ID + " [" + oldInfo.FirstName + " " + oldInfo.MiddleName + " " + oldInfo.LastName + "]";
                        commonDao.InsertLogDetail(logId, "ID", "Key for Update", key, null);
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

        public void WriteLogForUpdateRemarks(STT newInfo, ELogAction action)
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
                commonDao.InsertMasterLog(logId, newInfo.UpdatedBy, ELogTable.STT.ToString(), action.ToString());
                STT oldInfo = new STTDao().GetById(newInfo.ID);

                if ((oldInfo != null) && (newInfo != null) && (logId != null))
                {
                    if (newInfo.Remarks != oldInfo.Remarks)
                    {
                        commonDao.InsertLogDetail(logId, "Remarks", "Remarks", oldInfo.Remarks, newInfo.Remarks);
                        isUpdated = true;
                    }
                    if (isUpdated)
                    {
                        // Insert Key Name
                        string key = oldInfo.ID + " [" + oldInfo.FirstName + " " + oldInfo.MiddleName + " " + oldInfo.LastName + "]";
                        commonDao.InsertLogDetail(logId, "ID", "Key for Update", key, null);
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

        public void WriteLogForUpdateImage(STT newInfo, ELogAction action)
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
                commonDao.InsertMasterLog(logId, newInfo.UpdatedBy, ELogTable.STT.ToString(), action.ToString());
                STT oldInfo = new STTDao().GetById(newInfo.ID);

                if ((oldInfo != null) && (newInfo != null) && (logId != null))
                {
                    if (newInfo.Photograph != oldInfo.Photograph)
                    {
                        commonDao.InsertLogDetail(logId, "Photograph", "Photograph", oldInfo.Photograph, newInfo.Photograph);
                        isUpdated = true;
                    }
                    if (isUpdated)
                    {
                        // Insert Key Name
                        string key = oldInfo.ID + " [" + oldInfo.FirstName + " " + oldInfo.MiddleName + " " + oldInfo.LastName + "]";
                        commonDao.InsertLogDetail(logId, "ID", "Key for Update", key, null);
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

        public void WriteLogForUpdateCV(STT newInfo, ELogAction action)
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
                commonDao.InsertMasterLog(logId, newInfo.UpdatedBy, ELogTable.STT.ToString(), action.ToString());
                STT oldInfo = new STTDao().GetById(newInfo.ID);

                if ((oldInfo != null) && (newInfo != null) && (logId != null))
                {
                    if (newInfo.CVFile != oldInfo.CVFile)
                    {
                        commonDao.InsertLogDetail(logId, "CVFile", "CVFile", oldInfo.CVFile, newInfo.CVFile);
                        isUpdated = true;
                    }
                    if (isUpdated)
                    {
                        // Insert Key Name
                        string key = oldInfo.ID + " [" + oldInfo.FirstName + " " + oldInfo.MiddleName + " " + oldInfo.LastName + "]";
                        commonDao.InsertLogDetail(logId, "ID", "Key for Update", key, null);
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

        /// <summary>
        /// Write Insert Log For STT
        /// </summary>
        /// <param name="objInfo"></param>
        /// <param name="logId"></param>
        private void WriteInsertLogForSTT(STT objInfo, string logId)
        {
            try
            {
                if ((objInfo != null) && (logId != null))
                {
                    // Insert to Log Detail                       
                    commonDao.InsertLogDetail(logId, "ID", "ID", null, objInfo.ID.ToString());
                    
                    commonDao.InsertLogDetail(logId, "FirstName", "First Name", null, objInfo.FirstName);
                    commonDao.InsertLogDetail(logId, "LastName", "Last Name", null, objInfo.LastName);
                    if (!string.IsNullOrEmpty(objInfo.MiddleName))
                    {
                        commonDao.InsertLogDetail(logId, "MiddleName", "Middle Name", null, objInfo.MiddleName);
                    }
                    if (!string.IsNullOrEmpty(objInfo.Project))
                    {
                        commonDao.InsertLogDetail(logId, "Project", "Project", null, objInfo.Project);
                    }
                    if (!string.IsNullOrEmpty(objInfo.ManagerId))
                    {
                        commonDao.InsertLogDetail(logId, "Manager", "Manager", null, objInfo.ManagerId);
                    }
                    if (!string.IsNullOrEmpty(objInfo.Major))
                    {
                        commonDao.InsertLogDetail(logId, "Major", "Major", null, objInfo.Major);
                    }
                    commonDao.InsertLogDetail(logId, "VnFirstName", "Vn First Name", null, objInfo.VnFirstName);
                    if (!string.IsNullOrEmpty(objInfo.VnMiddleName))
                    {
                        commonDao.InsertLogDetail(logId, "VnMiddleName", "Vn Middle Name", null, objInfo.VnMiddleName);
                    }
                    commonDao.InsertLogDetail(logId, "VnLastName", "Vn Last Name", null, objInfo.VnLastName);
                    if (!string.IsNullOrEmpty(objInfo.VnPlaceOfOrigin))
                    {
                        commonDao.InsertLogDetail(logId, "VnPlaceOfOrigin", "Vn Place Of Origin", null, objInfo.VnPlaceOfOrigin);
                    }
                    if (!string.IsNullOrEmpty(objInfo.VnPOB))
                    {
                        commonDao.InsertLogDetail(logId, "VnPOB", "Vn Place Of Birth", null, objInfo.VnPOB);
                    }
                    if (!string.IsNullOrEmpty(objInfo.Degree))
                    {
                        commonDao.InsertLogDetail(logId, "Degree", "Degree", null, objInfo.Degree);
                    }
                    if (!string.IsNullOrEmpty(objInfo.OtherDegree))
                    {
                        commonDao.InsertLogDetail(logId, "OtherDegree", "OtherDegree", null, objInfo.OtherDegree);
                    }
                    if (!string.IsNullOrEmpty(objInfo.Race))
                    {
                        commonDao.InsertLogDetail(logId, "Race", "Race", null, objInfo.Race);
                    }                   
                    if (!string.IsNullOrEmpty(objInfo.IDIssueLocation))
                    {
                        commonDao.InsertLogDetail(logId, "IDIssueLocation", "Issue Location", null, objInfo.IDIssueLocation);
                    }
                    if (!string.IsNullOrEmpty(objInfo.VnIDIssueLocation))
                    {
                        commonDao.InsertLogDetail(logId, "VnIDIssueLocation", "Vn Issue Location", null, objInfo.VnIDIssueLocation);
                    }
                    if (objInfo.Gender.HasValue)
                    {
                        commonDao.InsertLogDetail(logId, "Gender", "Gender", null, objInfo.Gender == Constants.MALE ? "Male" : "Female");
                    }
                    if (!string.IsNullOrEmpty(objInfo.IDNumber))
                    {
                        commonDao.InsertLogDetail(logId, "IDNumber", "ID Number", null, objInfo.IDNumber);
                    }
                    commonDao.InsertLogDetail(logId, "StartDate", "Start Date", null, objInfo.StartDate.ToString(Constants.DATETIME_FORMAT_VIEW));

                    if (objInfo.ExpectedEndDate.HasValue)
                    {
                        commonDao.InsertLogDetail(logId, "GenExpectedEndDateder", "Expected End Date", null, objInfo.ExpectedEndDate.Value.ToString(Constants.DATETIME_FORMAT_VIEW));
                    }
                    if (!string.IsNullOrEmpty(objInfo.Photograph))
                    {
                        commonDao.InsertLogDetail(logId, "Photograph", "Photograph", null, objInfo.Photograph);
                    }
                    if (!string.IsNullOrEmpty(objInfo.JR))
                    {
                        commonDao.InsertLogDetail(logId, "JobRequest", "Job Request", null, objInfo.JR);
                    }
                    if (!string.IsNullOrEmpty(objInfo.JRApproval))
                    {
                        commonDao.InsertLogDetail(logId, "Job Request Approval", "Job Request Approval", null, objInfo.JRApproval);
                    }
                    if (!string.IsNullOrEmpty(objInfo.TempAddress))
                    {
                        commonDao.InsertLogDetail(logId, "TempAddress", "Temp Address", null, objInfo.TempAddress);
                    }
                    if (!string.IsNullOrEmpty(objInfo.TempArea))
                    {
                        commonDao.InsertLogDetail(logId, "TempArea", "Temp Area", null, objInfo.TempArea);
                    }
                    if (!string.IsNullOrEmpty(objInfo.TempDistrict))
                    {
                        commonDao.InsertLogDetail(logId, "TempDistrict", "Temp District", null, objInfo.TempDistrict);
                    }
                    if (!string.IsNullOrEmpty(objInfo.TempCityProvince))
                    {
                        commonDao.InsertLogDetail(logId, "TempCityProvince", "Temp City Province", null, objInfo.TempCityProvince);
                    }
                    if (!string.IsNullOrEmpty(objInfo.TempCountry))
                    {
                        commonDao.InsertLogDetail(logId, "TempCountry", "Temp Country", null, objInfo.TempCountry);
                    }
                    if (!string.IsNullOrEmpty(objInfo.PermanentAddress))
                    {
                        commonDao.InsertLogDetail(logId, "PermanentAddress", "Permanent Address", null, objInfo.PermanentAddress);
                    }
                    if (!string.IsNullOrEmpty(objInfo.PermanentArea))
                    {
                        commonDao.InsertLogDetail(logId, "PermanentArea", "Permanent Area", null, objInfo.PermanentArea);
                    }
                    if (!string.IsNullOrEmpty(objInfo.PermanentDistrict))
                    {
                        commonDao.InsertLogDetail(logId, "PermanentDistrict", "Permanent District", null, objInfo.PermanentDistrict);
                    }
                    if (!string.IsNullOrEmpty(objInfo.PermanentCityProvince))
                    {
                        commonDao.InsertLogDetail(logId, "PermanentCityProvince", "Permanent City Province", null, objInfo.PermanentCityProvince);
                    }
                    if (!string.IsNullOrEmpty(objInfo.PermanentCountry))
                    {
                        commonDao.InsertLogDetail(logId, "PermanentCountry", "Permanent Country", null, objInfo.PermanentCountry);
                    }
                    if (!string.IsNullOrEmpty(objInfo.HomePhone))
                    {
                        commonDao.InsertLogDetail(logId, "HomePhone", "Home Phone", null, objInfo.HomePhone);
                    }
                    if (!string.IsNullOrEmpty(objInfo.CellPhone))
                    {
                        commonDao.InsertLogDetail(logId, "CellPhone", "Cell Phone", null, objInfo.CellPhone);
                    }
                    if (!string.IsNullOrEmpty(objInfo.PersonalEmail))
                    {
                        commonDao.InsertLogDetail(logId, "PersonalEmail", "Personal Email", null, objInfo.PersonalEmail);
                    }
                    if (!string.IsNullOrEmpty(objInfo.OfficeEmail))
                    {
                        commonDao.InsertLogDetail(logId, "OfficeEmail", "Office Email", null, objInfo.OfficeEmail);
                    }
                    if (!string.IsNullOrEmpty(objInfo.ExtensionNumber))
                    {
                        commonDao.InsertLogDetail(logId, "ExtensionNumber", "Extension Number", null, objInfo.ExtensionNumber);
                    }
                    if (objInfo.DOB.HasValue)
                    {
                        commonDao.InsertLogDetail(logId, "DOB", "Date Of Birth", null, objInfo.DOB.Value.ToString(Constants.DATETIME_FORMAT_VIEW));
                    }
                    if (!string.IsNullOrEmpty(objInfo.POB))
                    {
                        commonDao.InsertLogDetail(logId, "POB", "Place Of Birth", null, objInfo.POB);
                    }
                    if (!string.IsNullOrEmpty(objInfo.Nationality))
                    {
                        commonDao.InsertLogDetail(logId, "Nationality", "Nationality", null, objInfo.Nationality);
                    }
                    if (!string.IsNullOrEmpty(objInfo.PlaceOfOrigin))
                    {
                        commonDao.InsertLogDetail(logId, "PlaceOfOrigin", "Place Of Origin", null, objInfo.PlaceOfOrigin);
                    }
                    if (objInfo.IssueDate.HasValue)
                    {
                        commonDao.InsertLogDetail(logId, "IssueDate", "Issue Date", null, objInfo.IssueDate.Value.ToString(Constants.DATETIME_FORMAT_VIEW));
                    }
                    if (!string.IsNullOrEmpty(objInfo.BankAccount))
                    {
                        commonDao.InsertLogDetail(logId, "BankAccount", "Bank Account", null, objInfo.BankAccount);
                    }
                    if (!string.IsNullOrEmpty(objInfo.BankName))
                    {
                        commonDao.InsertLogDetail(logId, "BankName", "Bank Name", null, objInfo.BankName);
                    }
                    if (objInfo.LaborUnion.HasValue)
                    {
                        commonDao.InsertLogDetail(logId, "LaborUnion", "Labor Union", null, objInfo.LaborUnion == Constants.LABOR_UNION_FALSE ? "No" : "Yes");
                    }
                    if (objInfo.LaborUnionDate.HasValue)
                    {
                        commonDao.InsertLogDetail(logId, "LaborUnionDate", "Labor Union Date", null, objInfo.LaborUnionDate.Value.ToString(Constants.DATETIME_FORMAT_VIEW));
                    }
                    if (!string.IsNullOrEmpty(objInfo.Remarks))
                    {
                        commonDao.InsertLogDetail(logId, "Remarks", "Remarks", null, objInfo.Remarks);
                    }
                    if (objInfo.MarriedStatus.HasValue)
                    {
                        commonDao.InsertLogDetail(logId, "MarriedStatus", "Married Status", null, objInfo.MarriedStatus == Constants.SINGLE ? "Single" : "Married");
                    }
                    if (!string.IsNullOrEmpty(objInfo.Religion))
                    {
                        commonDao.InsertLogDetail(logId, "Religion", "Religion", null, objInfo.Religion);
                    }
                    if (!string.IsNullOrEmpty(objInfo.EmergencyContactName))
                    {
                        commonDao.InsertLogDetail(logId, "EmergencyContactName", "Emergency Contact Name", null, objInfo.EmergencyContactName);
                    }
                    if (!string.IsNullOrEmpty(objInfo.EmergencyContactPhone))
                    {
                        commonDao.InsertLogDetail(logId, "EmergencyContactPhone", "Emergency Contact Phone", null, objInfo.EmergencyContactPhone);
                    }
                    if (!string.IsNullOrEmpty(objInfo.EmergencyContactRelationship))
                    {
                        commonDao.InsertLogDetail(logId, "EmergencyContactRelationship", "Emergency Contact Relationship", null, objInfo.EmergencyContactRelationship);
                    }

                    commonDao.InsertLogDetail(logId, "Title", "Job Title", null, Constants.STT_DEFAULT_VALUE);
                    commonDao.InsertLogDetail(logId, "DepartmentId", "Sub Department", null, objInfo.Department.DepartmentName);
                    commonDao.InsertLogDetail(logId, "Department", "Department", null, new DepartmentDao().GetDepartmentNameBySub(objInfo.DepartmentId));
                    if (!string.IsNullOrEmpty(objInfo.SkypeId))
                    {
                        commonDao.InsertLogDetail(logId, "SkypeId", "SkypeId", null, objInfo.SkypeId);
                    }
                    if (!string.IsNullOrEmpty(objInfo.YahooId))
                    {
                        commonDao.InsertLogDetail(logId, "YahooId", "YahooId", null, objInfo.YahooId);
                    }
                    if (!string.IsNullOrEmpty(objInfo.LocationCode))
                    {
                        commonDao.InsertLogDetail(logId, "LocationCode", "Location Code", null, CommonFunc.GenerateStringOfLocation(objInfo.LocationCode));
                    }
                    if (!string.IsNullOrEmpty(objInfo.CVFile))
                    {
                        commonDao.InsertLogDetail(logId, "CVFile", "CVFile", null, objInfo.CVFile);
                    }
                    commonDao.InsertLogDetail(logId, "STTStatusId", "STT Status", null, objInfo.STT_Status.Name);                      
                    if (!string.IsNullOrEmpty(objInfo.VnPermanentAddress))
                    {
                        commonDao.InsertLogDetail(logId, "VnPermanentAddress", "Vn Permanent Address", null, objInfo.VnPermanentAddress);
                    }
                    if (!string.IsNullOrEmpty(objInfo.VnPermanentArea))
                    {
                        commonDao.InsertLogDetail(logId, "VnPermanentArea", "Vn Permanent Area", null, objInfo.VnPermanentArea);
                    }
                    if (!string.IsNullOrEmpty(objInfo.VnPermanentDistrict))
                    {
                        commonDao.InsertLogDetail(logId, "VnPermanentDistrict", "Vn Permanent District", null, objInfo.VnPermanentDistrict);
                    }
                    if (!string.IsNullOrEmpty(objInfo.VnPermanentCityProvince))
                    {
                        commonDao.InsertLogDetail(logId, "VnPermanentCityProvince", "Vn Permanent City Province", null, objInfo.VnPermanentCityProvince);
                    }
                    if (!string.IsNullOrEmpty(objInfo.VnPermanentCountry))
                    {
                        commonDao.InsertLogDetail(logId, "VnPermanentCountry", "Vn Permanent Country", null, objInfo.VnPermanentCountry);
                    }
                    if (!string.IsNullOrEmpty(objInfo.VnTempAddress))
                    {
                        commonDao.InsertLogDetail(logId, "VnTempAddress", "Vn Temp Address", null, objInfo.VnTempAddress);
                    }
                    if (!string.IsNullOrEmpty(objInfo.VnTempArea))
                    {
                        commonDao.InsertLogDetail(logId, "VnTempArea", "Vn Temp Area", null, objInfo.VnTempArea);
                    }
                    if (!string.IsNullOrEmpty(objInfo.VnTempDistrict))
                    {
                        commonDao.InsertLogDetail(logId, "VnTempDistrict", "Vn Temp District", null, objInfo.VnTempDistrict);
                    }
                    if (!string.IsNullOrEmpty(objInfo.VnTempCityProvince))
                    {
                        commonDao.InsertLogDetail(logId, "VnTempCityProvince", "Vn Temp City Province", null, objInfo.VnTempCityProvince);
                    }
                    if (!string.IsNullOrEmpty(objInfo.VnTempCountry))
                    {
                        commonDao.InsertLogDetail(logId, "VnTempCountry", "Vn Temp Country", null, objInfo.VnTempCountry);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Write Update Log For STT
        /// </summary>
        /// <param name="newInfo"></param>
        /// <param name="logId"></param>
        /// <returns></returns>
        private bool WriteUpdateLogForSTT(STT newInfo, string logId)
        {
            bool isUpdated = false;
            try
            {
                // Get old info
                STT oldInfo = new STTDao().GetById(newInfo.ID);

                if ((oldInfo != null) && (newInfo != null) && (logId != null))
                {
                    if (newInfo.IDNumber != oldInfo.IDNumber)
                    {
                        commonDao.InsertLogDetail(logId, "IDNumber", "ID Number", oldInfo.IDNumber, newInfo.IDNumber);
                        isUpdated = true;
                    }
                    if (newInfo.LastName != oldInfo.LastName)
                    {
                        commonDao.InsertLogDetail(logId, "LastName", "Last Name", oldInfo.LastName, newInfo.LastName);
                        isUpdated = true;
                    }
                    if (newInfo.Project != oldInfo.Project)
                    {
                        commonDao.InsertLogDetail(logId, "Project", "Project", oldInfo.Project, newInfo.Project);
                        isUpdated = true;
                    }
                    if (newInfo.ManagerId != oldInfo.ManagerId)
                    {
                        commonDao.InsertLogDetail(logId, "Manager", "Manager", oldInfo.ManagerId, newInfo.ManagerId);
                        isUpdated = true;
                    }
                    if (newInfo.Major != oldInfo.Major)
                    {
                        commonDao.InsertLogDetail(logId, "Major", "Major", oldInfo.Major, newInfo.Major);
                        isUpdated = true;
                    }
                    if (newInfo.MiddleName != oldInfo.MiddleName)
                    {
                        commonDao.InsertLogDetail(logId, "MiddleName", "Middle Name", oldInfo.MiddleName, newInfo.MiddleName);
                        isUpdated = true;
                    }
                    if (newInfo.FirstName != oldInfo.FirstName)
                    {
                        commonDao.InsertLogDetail(logId, "FirstName", "First Name", oldInfo.FirstName, newInfo.FirstName);
                        isUpdated = true;
                    }
                    if (newInfo.VnFirstName != oldInfo.VnFirstName)
                    {
                        commonDao.InsertLogDetail(logId, "VnFirstName", "Vn First Name", oldInfo.VnFirstName, newInfo.VnFirstName);
                        isUpdated = true;
                    }
                    if (newInfo.VnMiddleName != oldInfo.VnMiddleName)
                    {
                        commonDao.InsertLogDetail(logId, "VnMiddleName", "Vn Middle Name", oldInfo.VnMiddleName, newInfo.VnMiddleName);
                        isUpdated = true;
                    }
                    if (newInfo.VnLastName != oldInfo.VnLastName)
                    {
                        commonDao.InsertLogDetail(logId, "VnLastName", "Vn Last Name", oldInfo.VnLastName, newInfo.VnLastName);
                        isUpdated = true;
                    }
                    if (newInfo.VnPOB != oldInfo.VnPOB)
                    {
                        commonDao.InsertLogDetail(logId, "VnPOB", "Vn Place Of Birth", oldInfo.VnPOB, newInfo.VnPOB);
                        isUpdated = true;
                    }
                    if (newInfo.VnPlaceOfOrigin != oldInfo.VnPlaceOfOrigin)
                    {
                        commonDao.InsertLogDetail(logId, "VnPlaceOfOrigin", "Vn Place Of Origin", oldInfo.VnPlaceOfOrigin, newInfo.VnPlaceOfOrigin);
                        isUpdated = true;
                    }
                    if (newInfo.Degree != oldInfo.Degree)
                    {
                        commonDao.InsertLogDetail(logId, "Degree", "Degree", oldInfo.Degree, newInfo.Degree);
                        isUpdated = true;
                    }
                    if (newInfo.OtherDegree != oldInfo.OtherDegree)
                    {
                        commonDao.InsertLogDetail(logId, "OtherDegree", "OtherDegree", oldInfo.OtherDegree, newInfo.OtherDegree);
                        isUpdated = true;
                    }
                    if (newInfo.Race != oldInfo.Race)
                    {
                        commonDao.InsertLogDetail(logId, "Race", "Race", oldInfo.Race, newInfo.Race);
                        isUpdated = true;
                    }
                     if (newInfo.IDIssueLocation != oldInfo.IDIssueLocation)
                    {
                        commonDao.InsertLogDetail(logId, "IDIssueLocation", "Issue Location", oldInfo.IDIssueLocation, newInfo.IDIssueLocation);
                        isUpdated = true;
                    }
                    if (newInfo.VnIDIssueLocation != oldInfo.VnIDIssueLocation)
                    {
                        commonDao.InsertLogDetail(logId, "VnIDIssueLocation", "Vn Issue Location", oldInfo.VnIDIssueLocation, newInfo.VnIDIssueLocation);
                        isUpdated = true;
                    }                           
                    if (newInfo.VnPermanentAddress != oldInfo.VnPermanentAddress)
                    {
                        commonDao.InsertLogDetail(logId, "VnPermanentAddress", "Vn Permanent Address", oldInfo.VnPermanentAddress, newInfo.VnPermanentAddress);
                        isUpdated = true;
                    }
                    if (newInfo.VnPermanentArea != oldInfo.VnPermanentArea)
                    {
                        commonDao.InsertLogDetail(logId, "VnPermanentArea", "Vn Permanent Area", oldInfo.VnPermanentArea, newInfo.VnPermanentArea);
                        isUpdated = true;
                    }
                    if (newInfo.VnPermanentDistrict != oldInfo.VnPermanentDistrict)
                    {
                        commonDao.InsertLogDetail(logId, "VnPermanentDistrict", "Vn Permanent District", oldInfo.VnPermanentDistrict, newInfo.VnPermanentDistrict);
                        isUpdated = true;
                    }
                    if (newInfo.VnPermanentCityProvince != oldInfo.VnPermanentCityProvince)
                    {
                        commonDao.InsertLogDetail(logId, "VnPermanentCityProvince", "Vn Permanent City Province", oldInfo.VnPermanentCityProvince, newInfo.VnPermanentCityProvince);
                        isUpdated = true;
                    }
                    if (newInfo.VnPermanentCountry != oldInfo.VnPermanentCountry)
                    {
                        commonDao.InsertLogDetail(logId, "VnPermanentCountry", "Vn Permanent Country", oldInfo.VnPermanentCountry, newInfo.VnPermanentCountry);
                        isUpdated = true;
                    }
                    if (newInfo.VnTempAddress != oldInfo.VnTempAddress)
                    {
                        commonDao.InsertLogDetail(logId, "VnTempAddress", "Vn Temp Address", oldInfo.VnTempAddress, newInfo.VnTempAddress);
                        isUpdated = true;
                    }
                    if (newInfo.VnTempArea != oldInfo.VnTempArea)
                    {
                        commonDao.InsertLogDetail(logId, "VnTempArea", "Vn Temp Area", oldInfo.VnTempArea, newInfo.VnTempArea);
                        isUpdated = true;
                    }
                    if (newInfo.VnTempCityProvince != oldInfo.VnTempCityProvince)
                    {
                        commonDao.InsertLogDetail(logId, "VnTempCityProvince", "Vn Temp City Province", oldInfo.VnTempCityProvince, newInfo.VnTempCityProvince);
                        isUpdated = true;
                    }
                    if (newInfo.VnTempCountry != oldInfo.VnTempCountry)
                    {
                        commonDao.InsertLogDetail(logId, "VnTempCountry", "Vn Temp Country", oldInfo.VnTempCountry, newInfo.VnTempCountry);
                        isUpdated = true;
                    }
                    if (newInfo.Gender != oldInfo.Gender)
                    {
                        commonDao.InsertLogDetail(logId, "Gender", "Gender",oldInfo.Gender.HasValue?oldInfo.Gender.Value == Constants.MALE ? "Male" : "Female":"",newInfo.Gender.HasValue?newInfo.Gender.Value == Constants.MALE ? "Male" : "Female":"");
                        isUpdated = true;
                    }
                    if (newInfo.StartDate != oldInfo.StartDate)
                    {
                        commonDao.InsertLogDetail(logId, "StartDate", "Start Date", oldInfo.StartDate.ToString(Constants.DATETIME_FORMAT_VIEW), newInfo.StartDate.ToString(Constants.DATETIME_FORMAT_VIEW));
                        isUpdated = true;
                    }
                    if (newInfo.ExpectedEndDate != oldInfo.ExpectedEndDate)
                    {
                        commonDao.InsertLogDetail(logId, "ExpectedEndDate", "Expected End Date",oldInfo.ExpectedEndDate.HasValue?oldInfo.ExpectedEndDate.Value.ToString(Constants.DATETIME_FORMAT_VIEW):"",newInfo.ExpectedEndDate.HasValue? newInfo.ExpectedEndDate.Value.ToString(Constants.DATETIME_FORMAT_VIEW):"");
                        isUpdated = true;
                    }
                    if (newInfo.Photograph != oldInfo.Photograph)
                    {
                        commonDao.InsertLogDetail(logId, "Photograph", "Photograph", oldInfo.Photograph, newInfo.Photograph);
                        isUpdated = true;
                    }
                    if (newInfo.JR != oldInfo.JR)
                    {
                        commonDao.InsertLogDetail(logId, "JobRequest", "Job Request", oldInfo.JR, newInfo.JR);
                        isUpdated = true;
                    }
                    if (newInfo.JRApproval != oldInfo.JRApproval)
                    {
                        commonDao.InsertLogDetail(logId, "JobRequestApproval", "Job Request Approval", oldInfo.JRApproval, newInfo.JRApproval);
                        isUpdated = true;
                    }
                    if (newInfo.TempAddress != oldInfo.TempAddress)
                    {
                        commonDao.InsertLogDetail(logId, "TempAddress", "Temp Address", oldInfo.TempAddress, newInfo.TempAddress);
                        isUpdated = true;
                    }
                    if (newInfo.TempArea != oldInfo.TempArea)
                    {
                        commonDao.InsertLogDetail(logId, "TempArea", "Temp Area", oldInfo.TempArea, newInfo.TempArea);
                        isUpdated = true;
                    }
                    if (newInfo.TempDistrict != oldInfo.TempDistrict)
                    {
                        commonDao.InsertLogDetail(logId, "TempDistrict", "Temp District", oldInfo.TempDistrict, newInfo.TempDistrict);
                        isUpdated = true;
                    }
                    if (newInfo.TempCityProvince != oldInfo.TempCityProvince)
                    {
                        commonDao.InsertLogDetail(logId, "TempCityProvince", "Temp City Province", oldInfo.TempCityProvince, newInfo.TempCityProvince);
                        isUpdated = true;
                    }
                    if (newInfo.TempCountry != oldInfo.TempCountry)
                    {
                        commonDao.InsertLogDetail(logId, "TempCountry", "Temp Country", oldInfo.TempCountry, newInfo.TempCountry);
                        isUpdated = true;
                    }
                    if (newInfo.PermanentAddress != oldInfo.PermanentAddress)
                    {
                        commonDao.InsertLogDetail(logId, "PermanentAddress", "Permanent Address", oldInfo.PermanentAddress, newInfo.PermanentAddress);
                        isUpdated = true;
                    }
                    if (newInfo.PermanentArea != oldInfo.PermanentArea)
                    {
                        commonDao.InsertLogDetail(logId, "PermanentArea", "Permanent Area", oldInfo.PermanentArea, newInfo.PermanentArea);
                        isUpdated = true;
                    }
                    if (newInfo.PermanentDistrict != oldInfo.PermanentDistrict)
                    {
                        commonDao.InsertLogDetail(logId, "PermanentDistrict", "Permanent District", oldInfo.PermanentDistrict, newInfo.PermanentDistrict);
                        isUpdated = true;
                    }
                    if (newInfo.PermanentCityProvince != oldInfo.PermanentCityProvince)
                    {
                        commonDao.InsertLogDetail(logId, "PermanentCityProvince", "Permanent City Province", oldInfo.PermanentCityProvince, newInfo.PermanentCityProvince);
                        isUpdated = true;
                    }
                    if (newInfo.PermanentCountry != oldInfo.PermanentCountry)
                    {
                        commonDao.InsertLogDetail(logId, "PermanentCountry", "Permanent Country ", oldInfo.PermanentCountry, newInfo.PermanentCountry);
                        isUpdated = true;
                    }
                    if (newInfo.HomePhone != oldInfo.HomePhone)
                    {
                        commonDao.InsertLogDetail(logId, "HomePhone", "Home Phone", oldInfo.HomePhone, newInfo.HomePhone);
                        isUpdated = true;
                    }
                    if (newInfo.CellPhone != oldInfo.CellPhone)
                    {
                        commonDao.InsertLogDetail(logId, "CellPhone", "Cell Phone", oldInfo.CellPhone, newInfo.CellPhone);
                        isUpdated = true;
                    }
                    if (newInfo.PersonalEmail != oldInfo.PersonalEmail)
                    {
                        commonDao.InsertLogDetail(logId, "PersonalEmail", "Personal Email", oldInfo.PersonalEmail, newInfo.PersonalEmail);
                        isUpdated = true;
                    }
                    if (newInfo.OfficeEmail != oldInfo.OfficeEmail)
                    {
                        commonDao.InsertLogDetail(logId, "OfficeEmail", "Office Email", oldInfo.OfficeEmail, newInfo.OfficeEmail);
                        isUpdated = true;
                    }
                    if (newInfo.ExtensionNumber != oldInfo.ExtensionNumber)
                    {
                        commonDao.InsertLogDetail(logId, "ExtensionNumber", "Extension Number", oldInfo.ExtensionNumber, newInfo.ExtensionNumber);
                        isUpdated = true;
                    }
                    if (newInfo.DOB != oldInfo.DOB)
                    {
                        commonDao.InsertLogDetail(logId, "DOB", "Date Of Birth",oldInfo.DOB.HasValue?oldInfo.DOB.Value.ToString(Constants.DATETIME_FORMAT_VIEW):"",newInfo.DOB.HasValue?newInfo.DOB.Value.ToString(Constants.DATETIME_FORMAT_VIEW):"");
                        isUpdated = true;
                    }
                    if (newInfo.POB != oldInfo.POB)
                    {
                        commonDao.InsertLogDetail(logId, "POB", "Place Of Birth", oldInfo.POB, newInfo.POB);
                        isUpdated = true;
                    }
                    if (newInfo.Nationality != oldInfo.Nationality)
                    {
                        commonDao.InsertLogDetail(logId, "Nationality", "Nationality", oldInfo.Nationality, newInfo.Nationality);
                        isUpdated = true;
                    }
                    if (newInfo.PlaceOfOrigin != oldInfo.PlaceOfOrigin)
                    {
                        commonDao.InsertLogDetail(logId, "PlaceOfOrigin", "Place Of Origin", oldInfo.PlaceOfOrigin, newInfo.PlaceOfOrigin);
                        isUpdated = true;
                    }
                    if (newInfo.IssueDate != oldInfo.IssueDate)
                    {
                        commonDao.InsertLogDetail(logId, "IssueDate", "Issue Date",oldInfo.IssueDate.HasValue? oldInfo.IssueDate.Value.ToString(Constants.DATETIME_FORMAT_VIEW):"",newInfo.IssueDate.HasValue?newInfo.IssueDate.Value.ToString(Constants.DATETIME_FORMAT_VIEW):"");
                        isUpdated = true;
                    }
                    if (newInfo.BankAccount != oldInfo.BankAccount)
                    {
                        commonDao.InsertLogDetail(logId, "BankAccount", "Bank Account", oldInfo.BankAccount, newInfo.BankAccount);
                        isUpdated = true;
                    }
                    if (newInfo.BankName != oldInfo.BankName)
                    {
                        commonDao.InsertLogDetail(logId, "BankName", "Bank Name", oldInfo.BankName, newInfo.BankName);
                        isUpdated = true;
                    }
                    if (newInfo.ResignedDate != oldInfo.ResignedDate)
                    {
                        commonDao.InsertLogDetail(logId, "ResignedDate", "Resigned Date", oldInfo.ResignedDate.HasValue ? oldInfo.ResignedDate.Value.ToString(Constants.DATETIME_FORMAT_VIEW) : "", newInfo.ResignedDate.HasValue?newInfo.ResignedDate.Value.ToString(Constants.DATETIME_FORMAT_VIEW):"");
                        isUpdated = true;
                    }
                    if (newInfo.ResignedReason != oldInfo.ResignedReason)
                    {
                        commonDao.InsertLogDetail(logId, "ResignedReason", "Resigned Reason", oldInfo.ResignedReason, newInfo.ResignedReason);
                        isUpdated = true;
                    }
                    if (newInfo.ResignedAllowance != oldInfo.ResignedAllowance)
                    {
                        commonDao.InsertLogDetail(logId, "ResignedAllowance", "Resigned Allowance",oldInfo.ResignedAllowance.HasValue?oldInfo.ResignedAllowance.ToString():"", newInfo.ResignedAllowance.HasValue?newInfo.ResignedAllowance.ToString():"");
                        isUpdated = true;
                    }
                    if (newInfo.LaborUnion != oldInfo.LaborUnion)
                    {
                        commonDao.InsertLogDetail(logId, "LaborUnion", "Labor Union",oldInfo.LaborUnion.HasValue? oldInfo.LaborUnion.Value == Constants.LABOR_UNION_FALSE ? "No" : "Yes":"",newInfo.LaborUnion.HasValue?newInfo.LaborUnion.Value == Constants.LABOR_UNION_FALSE ? "No" : "Yes":"");
                        isUpdated = true;
                    }
                    if (newInfo.LaborUnionDate != oldInfo.LaborUnionDate)
                    {
                        commonDao.InsertLogDetail(logId, "LaborUnionDate", "Labor Union Date", oldInfo.LaborUnionDate.HasValue ? oldInfo.LaborUnionDate.Value.ToString(Constants.DATETIME_FORMAT_VIEW) : "", newInfo.LaborUnionDate.HasValue ? newInfo.LaborUnionDate.Value.ToString(Constants.DATETIME_FORMAT_VIEW) : "");
                        isUpdated = true;
                    }
                    if (newInfo.Remarks != oldInfo.Remarks)
                    {
                        commonDao.InsertLogDetail(logId, "Remarks", "Remarks", oldInfo.Remarks, newInfo.Remarks);
                        isUpdated = true;
                    }
                    if (newInfo.MarriedStatus != oldInfo.MarriedStatus)
                    {
                        commonDao.InsertLogDetail(logId, "Married Status", "Married Status",oldInfo.MarriedStatus.HasValue?oldInfo.MarriedStatus.Value == Constants.SINGLE ? "Single" : "Married":"",newInfo.MarriedStatus.HasValue?newInfo.MarriedStatus.Value == Constants.SINGLE ? "Single" : "Married":"");
                        isUpdated = true;
                    }
                    if (newInfo.Religion != oldInfo.Religion)
                    {
                        commonDao.InsertLogDetail(logId, "Religion", "Religion", oldInfo.Religion, newInfo.Religion);
                        isUpdated = true;
                    }
                    if (newInfo.EmergencyContactName != oldInfo.EmergencyContactName)
                    {
                        commonDao.InsertLogDetail(logId, "EmergencyContactName", "Emergency Contact Name", oldInfo.EmergencyContactName, newInfo.EmergencyContactName);
                        isUpdated = true;
                    }
                    if (newInfo.EmergencyContactPhone != oldInfo.EmergencyContactPhone)
                    {
                        commonDao.InsertLogDetail(logId, "EmergencyContactPhone", "Emergency Contact Phone", oldInfo.EmergencyContactPhone, newInfo.EmergencyContactPhone);
                        isUpdated = true;
                    }
                    if (newInfo.EmergencyContactRelationship != oldInfo.EmergencyContactRelationship)
                    {
                        commonDao.InsertLogDetail(logId, "EmergencyContactRelationship", "Emergency Contact Relationship", oldInfo.EmergencyContactRelationship, newInfo.EmergencyContactRelationship);
                        isUpdated = true;
                    }                      
                    if (newInfo.DepartmentId != oldInfo.DepartmentId)
                    {
                        DepartmentDao departDao = new DepartmentDao();
                        Department objDepart = departDao.GetById(newInfo.DepartmentId);
                        if (objDepart != null)
                        {
                            commonDao.InsertLogDetail(logId, "DepartmentId", "Department", oldInfo.Department.DepartmentName, objDepart.DepartmentName);
                            commonDao.InsertLogDetail(logId, "Department", "Department", new DepartmentDao().GetDepartmentNameBySub(oldInfo.DepartmentId), new DepartmentDao().GetDepartmentNameBySub(newInfo.DepartmentId));
                            isUpdated = true;
                        }
                    }
                    if (newInfo.SkypeId != oldInfo.SkypeId)
                    {
                        commonDao.InsertLogDetail(logId, "SkypeId", "SkypeId", oldInfo.SkypeId, newInfo.SkypeId);
                        isUpdated = true;
                    }
                    if (newInfo.YahooId != oldInfo.YahooId)
                    {
                        commonDao.InsertLogDetail(logId, "YahooId", "YahooId", oldInfo.YahooId, newInfo.YahooId);
                        isUpdated = true;
                    }
                    if (newInfo.LocationCode != oldInfo.LocationCode)
                    {
                        commonDao.InsertLogDetail(logId, "LocationCode", "LocationCode", CommonFunc.GenerateStringOfLocation(oldInfo.LocationCode), CommonFunc.GenerateStringOfLocation(newInfo.LocationCode));
                        isUpdated = true;
                    }
                    if (newInfo.CVFile != oldInfo.CVFile)
                    {
                        commonDao.InsertLogDetail(logId, "CVFile", "CVFile", oldInfo.CVFile, newInfo.CVFile);
                        isUpdated = true;
                    }
                    if (newInfo.STTStatusId != oldInfo.STTStatusId)
                    {
                        commonDao.InsertLogDetail(logId, "STTStatusId", "STT Status", oldInfo.STT_Status.Name, new STTStatusDao().GetById(newInfo.STTStatusId).Name);
                        isUpdated = true;
                    }
                    if (isUpdated)
                    {
                        // Insert Key Name
                        string key = oldInfo.ID + " [" + oldInfo.FirstName + " " + oldInfo.MiddleName + " " + oldInfo.LastName + "]";
                        commonDao.InsertLogDetail(logId, "ID", "Key for Update", key, null);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return isUpdated;
        }


        public void WriteLogForUpdateResult(STT_RefResult newInfo, ELogAction action)
        {

            try
            {
                if (newInfo == null)
                {
                    return;
                }
                MasterLog objMasterLog = new MasterLog();
                string logId = commonDao.UniqueId;
                commonDao.InsertMasterLog(logId, newInfo.UpdatedBy, ELogTable.STT.ToString(), action.ToString());
                STT oldSTTInfo = new STTDao().GetById(newInfo.SttID);

                if ((oldSTTInfo != null) && (newInfo != null) && (logId != null))
                {
                    if (newInfo.ResultId != oldSTTInfo.ResultId)
                    {
                        commonDao.InsertLogDetail(logId, "ResultId", "Result", oldSTTInfo.ResultId.HasValue?oldSTTInfo.STT_Result.Name:"", new STTResultDao().GetById(newInfo.ResultId).Name);
                        if (newInfo.ResultId == Constants.STT_RESULT_FAIL)
                        {
                            commonDao.InsertLogDetail(logId, "STTStatusId", "Status", oldSTTInfo.STT_Status.Name, new STTStatusDao().GetById(Constants.STT_STATUS_REJECTED).Name);
                        }
                        else
                        {
                            commonDao.InsertLogDetail(logId, "STTStatusId", "Status", oldSTTInfo.STT_Status.Name, new STTStatusDao().GetById(Constants.STT_STATUS_NEED_TO_PROMOTED).Name);
                        }
                    }
                        commonDao.InsertLogDetail(logId, "EndDate", "End Date", null, newInfo.EndDate.ToString(Constants.DATETIME_FORMAT_VIEW));
                        if (!string.IsNullOrEmpty(newInfo.Remarks))
                        {
                            commonDao.InsertLogDetail(logId, "Remarks", "Remarks", null, newInfo.Remarks);
                        }
                        string[] array = newInfo.Attachfile.TrimEnd(Constants.FILE_CHAR_PREFIX).Split(Constants.FILE_CHAR_PREFIX);
                        if (array.Count() > 0)
                        {
                            foreach (string item in array)
                            {
                                commonDao.InsertLogDetail(logId, "AttachFile", "Attach File", null, item);
                            }
                        }
                        // Insert Key Name
                        string key = oldSTTInfo.ID + " [" + oldSTTInfo.FirstName + " " + oldSTTInfo.MiddleName + " " + oldSTTInfo.LastName + "]";
                        commonDao.InsertLogDetail(logId, "ID", "Key for Update", key, null);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void WriteLogForEditResult(STT_RefResult newInfo, ELogAction action)
        {

            try
            {
                if (newInfo == null)
                {
                    return;
                }
                bool isUpdated = false;
                MasterLog objMasterLog = new MasterLog();
                string logId = commonDao.UniqueId;
                commonDao.InsertMasterLog(logId, newInfo.UpdatedBy, ELogTable.STT.ToString(), action.ToString());
                STT oldSTTInfo = new STTDao().GetById(newInfo.SttID);
                STT_RefResult oldInfo = new STTRefResultDao().GetById(newInfo.SttID);
                if ((oldSTTInfo != null) && (newInfo != null) && (logId != null))
                {
                    if (oldInfo.Remarks != newInfo.Remarks)                     
                    {
                        commonDao.InsertLogDetail(logId, "Remarks", "Remarks", oldInfo.Remarks, newInfo.Remarks);
                        isUpdated = true;
                    }
                    if (oldInfo.Attachfile != newInfo.Attachfile)
                    {
                        //string[] arrayNew = newInfo.Attachfile.TrimEnd(Constants.FILE_CHAR_PREFIX).Split(Constants.FILE_CHAR_PREFIX);

                        //foreach (string fileNew in arrayNew)
                        //{
                        //    if (!oldInfo.Attachfile.Contains(fileNew))
                        //    {
                        //        commonDao.InsertLogDetail(logId, "AttachFile", "Attach File", "", fileNew);
                        //        isUpdated = true;
                        //    }
                        //}

                        string[] Attachfile = newInfo.Attachfile.TrimEnd(Constants.FILE_CHAR_PREFIX).Split(Constants.FILE_CHAR_PREFIX);
                        foreach (string item in oldInfo.Attachfile.TrimEnd(Constants.FILE_CHAR_PREFIX).Split(Constants.FILE_CHAR_PREFIX))//case delete file
                        {
                            if (!newInfo.Attachfile.TrimEnd(Constants.FILE_CHAR_PREFIX).Split(Constants.FILE_CHAR_PREFIX).Contains(item))
                            {
                                commonDao.InsertLogDetail(logId, "AttachFile", "Attach File", item, "");
                                isUpdated = true;
                            }
                        }
                        foreach (string fileNew in Attachfile) //case add new file
                        {
                            if (!oldInfo.Attachfile.Contains(fileNew))
                            {
                                commonDao.InsertLogDetail(logId, "AttachFile", "Attach File", "", fileNew);
                                isUpdated = true;
                            }
                        }
                    }
                    if (oldInfo.ResultId != newInfo.ResultId)
                    {
                        if (newInfo.ResultId == Constants.STT_RESULT_FAIL)
                        {
                            isUpdated = true;
                            commonDao.InsertLogDetail(logId, "ResultId", "Result", oldInfo.STT_Result.Name,new STTResultDao().GetById(newInfo.ResultId).Name);
                            commonDao.InsertLogDetail(logId, "STTStatusId", "Status", oldSTTInfo.STT_Status.Name, new STTStatusDao().GetById(Constants.STT_STATUS_REJECTED).Name);
                        }
                        if (newInfo.ResultId == Constants.STT_RESULT_PASS)
                        {
                            isUpdated = true;
                            commonDao.InsertLogDetail(logId, "ResultId", "Result", oldInfo.STT_Result.Name, new STTResultDao().GetById(newInfo.ResultId).Name);
                            commonDao.InsertLogDetail(logId, "STTStatusId", "Status", oldSTTInfo.STT_Status.Name, new STTStatusDao().GetById(Constants.STT_STATUS_NEED_TO_PROMOTED).Name);
                        }
                    }
                    if (oldInfo.EndDate != newInfo.EndDate)
                    {
                        isUpdated = true;
                        commonDao.InsertLogDetail(logId, "EndDate", "End Date", oldInfo.EndDate.ToString(Constants.DATETIME_FORMAT_VIEW), newInfo.EndDate.ToString(Constants.DATETIME_FORMAT_VIEW));
                    }
                    // Insert Key Name
                    if (isUpdated)
                    {
                        string key = oldSTTInfo.ID + " [" + oldSTTInfo.FirstName + " " + oldSTTInfo.MiddleName + " " + oldSTTInfo.LastName + "]";
                        commonDao.InsertLogDetail(logId, "STT_ID", "Key for Update", key, null);
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

        public void WriteLogForRemoveAttachFile(STT_RefResult newInfo, ELogAction action)
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
                commonDao.InsertMasterLog(logId, newInfo.UpdatedBy, ELogTable.STT.ToString(), action.ToString());
                STT_RefResult oldInfo = new STTRefResultDao().GetById(newInfo.SttID);

                if ((oldInfo != null) && (newInfo != null) && (logId != null))
                {
                    foreach (string item in oldInfo.Attachfile.TrimEnd(Constants.FILE_CHAR_PREFIX).Split(Constants.FILE_CHAR_PREFIX))
                    {
                        if (!newInfo.Attachfile.TrimEnd(Constants.FILE_CHAR_PREFIX).Split(Constants.FILE_CHAR_PREFIX).Contains(item))
                        {
                            commonDao.InsertLogDetail(logId, "AttachFile", "Attach File", item, "");
                            isUpdated = true;
                        }
                    }

                    if (isUpdated)
                    {
                        // Insert Key Name
                        STT obj = new STTDao().GetById(oldInfo.SttID);
                        string key = oldInfo.SttID + " [" + obj.FirstName + " " + obj.MiddleName + " " + obj.LastName + "]";
                        commonDao.InsertLogDetail(logId, "STT_ID", "Key for Delete", key, null);
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

        public void WriteLogForUpdatePromoted(STT newInfo, ELogAction action)
        {

            try
            {
                if (newInfo == null)
                {
                    return;
                }
                MasterLog objMasterLog = new MasterLog();
                string logId = commonDao.UniqueId;
                commonDao.InsertMasterLog(logId, newInfo.UpdatedBy, ELogTable.STT.ToString(), action.ToString());
                STT oldSTTInfo = new STTDao().GetById(newInfo.ID);

                if ((oldSTTInfo != null) && (newInfo != null) && (logId != null))
                {
                   
                    commonDao.InsertLogDetail(logId, "STTStatusId", "Status", oldSTTInfo.STT_Status.Name, new STTStatusDao().GetById(Constants.STT_STATUS_PROMOTED).Name);
                     
                    // Insert Key Name
                    string key = oldSTTInfo.ID + " [" + oldSTTInfo.FirstName + " " + oldSTTInfo.MiddleName + " " + oldSTTInfo.LastName + "]";
                    commonDao.InsertLogDetail(logId, "STT_ID", "Key for Update", key, null);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="newInfo"></param>
        /// <param name="action"></param>
        public void WriteLogForUpdatePosition(STT newInfo, ELogAction action)
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
                commonDao.InsertMasterLog(logId, newInfo.UpdatedBy, ELogTable.STT.ToString(), action.ToString());
                STT oldInfo = new STTDao().GetById(newInfo.ID);

                if ((oldInfo != null) && (newInfo != null) && (logId != null))
                {
                    if (newInfo.ManagerId != oldInfo.ManagerId)
                    {
                        commonDao.InsertLogDetail(logId, "Manager", "Manager", oldInfo.ManagerId, newInfo.ManagerId);
                        isUpdated = true;
                    }
                    if (newInfo.Project != oldInfo.Project)
                    {
                        commonDao.InsertLogDetail(logId, "Project", "Project", oldInfo.Project, newInfo.Project);
                        isUpdated = true;
                    }
                    if (newInfo.Floor != oldInfo.Floor)
                    {
                        commonDao.InsertLogDetail(logId, "Floor", "Floor", oldInfo.Floor, newInfo.Floor);
                        isUpdated = true;
                    }
                    if (newInfo.SeatCode != oldInfo.SeatCode)
                    {
                        commonDao.InsertLogDetail(logId, "SeatCode", "SeatCode", oldInfo.SeatCode, newInfo.SeatCode);
                        isUpdated = true;
                    }
                    if (newInfo.LocationCode != oldInfo.LocationCode)
                    {
                        commonDao.InsertLogDetail(logId, "LocationCode", "LocationCode", CommonFunc.GenerateStringOfLocation(oldInfo.LocationCode), CommonFunc.GenerateStringOfLocation(newInfo.LocationCode));
                        isUpdated = true;
                    }
                    if (isUpdated)
                    {
                        // Insert Key Name
                        string key = oldInfo.ID + " [" + oldInfo.FirstName + " " + oldInfo.MiddleName + " " + oldInfo.LastName + "]";
                        commonDao.InsertLogDetail(logId, "ID", "Key for Update", key, null);
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
        #endregion
    }
}