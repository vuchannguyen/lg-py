using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CRM.Library.Common;
using System.Data.Linq;
using System.Reflection;
using System.Data.Common;

namespace CRM.Models
{
    public class STTDao : BaseDao 
    {
     
        public STTDao()
        {
        }

        public STT GetByJR(string jrID)
        {
            return dbContext.STTs.Where(q => q.JR == jrID).FirstOrDefault();
        }

        public Message InsertMulti(List<STT> list,string resultDate,string userName)
        {
            Message msg = null;
            DbTransaction trans = null;
            try
            {
                dbContext.Connection.Open();
                trans = dbContext.Connection.BeginTransaction();
                dbContext.Transaction = trans;
                if (list.Count > 0)
                {
                    dbContext.STTs.InsertAllOnSubmit(list);
                    dbContext.SubmitChanges();
                    msg = new Message(MessageConstants.I0001, MessageType.Info, list.Count.ToString() + " STT(s)", "insert");
                    trans.Commit();
                    List<STT_RefResult> listRef = new List<STT_RefResult>();
                     string[] arrayDate = resultDate.TrimEnd(',').Split(',');
                     int index = 0;
                     foreach (STT item in list)
                    {
                        if (item.ResultId.HasValue)
                        {
                            STT_RefResult objUI = new STT_RefResult();
                            objUI.SttID = item.ID;
                            objUI.ResultId = item.ResultId.Value;
                            objUI.EndDate = DateTime.Parse(arrayDate[index]);
                            objUI.CreatedBy = userName;
                            objUI.CreatedDate = DateTime.Now;
                            objUI.UpdatedBy = userName;
                            objUI.UpdatedDate = DateTime.Now;
                            objUI.DeleteFlag = false;
                            listRef.Add(objUI);
                            index++;
                        }
                    }
                     new STTRefResultDao().InsertMulti(listRef);
                }
                else
                {
                    if (trans != null) trans.Rollback();
                    msg = new Message(MessageConstants.E0007, MessageType.Error);
                }
            }
            catch
            {
                if (trans != null) trans.Rollback();
                // Show system error
                msg = new Message(MessageConstants.E0007, MessageType.Error);
            }
            return msg;
        }

        #region Public methods

        /// <summary>
        /// Get list of STT
        /// </summary>
        /// <returns></returns>
        public List<sp_GetSTTResult> GetList(string name, int? status, int? result, DateTime? fromStartDate, DateTime? toStartDate, DateTime? fromEndDate, DateTime? toEndDate, string cls)
        {
            return dbContext.sp_GetSTT(name, status, result, fromStartDate, toStartDate, fromEndDate, toEndDate, cls, null).ToList<sp_GetSTTResult>();
        }

        public List<sp_GetSTTResult> GetList(string name, int? status, int? result, DateTime? fromStartDate, DateTime? toStartDate, DateTime? fromEndDate, DateTime? toEndDate, string cls, string locationCode)
        {
            return dbContext.sp_GetSTT(name, status, result, fromStartDate, toStartDate, fromEndDate, toEndDate, cls, locationCode).ToList<sp_GetSTTResult>();
        }

        /// <summary>
        /// Get list of STT for Export
        /// </summary>
        /// <returns></returns>
        public List<sp_GetSTTForExportResult> GetListForExport(string name, int? status, int? result, DateTime? fromStartDate, DateTime? toStartDate, DateTime? fromEndDate, DateTime? toEndDate, string cls, string locationCode)
        {
            return dbContext.sp_GetSTTForExport(name, status, result, fromStartDate, toStartDate, fromEndDate, toEndDate, cls, locationCode).ToList<sp_GetSTTForExportResult>();
        }

        /// <summary>
        /// Get Name By AutoComplete
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public List<sp_GetSTTResult> GetListByName(string name)
        {
            return this.GetList(name, 0, 0, null, null, null, null, string.Empty).ToList<sp_GetSTTResult>();
        }

        /// <summary>
        /// Sort Emplooyee
        /// </summary>
        /// <param name="list"></param>
        /// <param name="sortColumn"></param>
        /// <param name="sortOrder"></param>
        /// <returns></returns>
        public List<sp_GetSTTResult> Sort(List<sp_GetSTTResult> list, string sortColumn, string sortOrder)
        {
            int order;

            if (sortOrder == "desc")
            {
                order = -1;
            }
            else
            {
                order = 1;
            }
            switch (sortColumn)
            {
                case "ID":
                    list.Sort(
                         delegate(sp_GetSTTResult m1, sp_GetSTTResult m2)
                         { return m1.ID.CompareTo(m2.ID) * order; });
                    break;
                case "DisplayName":
                    list.Sort(
                         delegate(sp_GetSTTResult m1, sp_GetSTTResult m2)
                         { return m1.DisplayName.CompareTo(m2.DisplayName) * order; });
                    break;
                case "Status":
                    list.Sort(
                         delegate(sp_GetSTTResult m1, sp_GetSTTResult m2)
                         { return m1.Status.CompareTo(m2.Status) * order; });
                    break;
                case "Result":
                    list.Sort(
                         delegate(sp_GetSTTResult m1, sp_GetSTTResult m2)
                         { return m1.Result.CompareTo(m2.Result) * order; });
                    break;
                case "StartDate":
                    list.Sort(
                         delegate(sp_GetSTTResult m1, sp_GetSTTResult m2)
                         { return m1.StartDate.CompareTo(m2.StartDate) * order; });
                    break;
                case "ExpectedEndDate":
                    list.Sort(
                         delegate(sp_GetSTTResult m1, sp_GetSTTResult m2)
                         { return (m1.ExpectedEndDate.HasValue ? m1.ExpectedEndDate.Value.ToString(Constants.DATETIME_FORMAT_VIEW) : "").CompareTo((m2.ExpectedEndDate.HasValue ? m2.ExpectedEndDate.Value.ToString(Constants.DATETIME_FORMAT_VIEW) : "")) * order; });
                    break;
                case "Remark":
                    list.Sort(
                         delegate(sp_GetSTTResult m1, sp_GetSTTResult m2)
                         { return m1.Remarks.CompareTo(m2.Remarks) * order; });
                    break;
            }

            return list;
        }

        public List<sp_GetSTTForExportResult> SortForExport(List<sp_GetSTTForExportResult> list, string sortColumn, string sortOrder)
        {
            int order;

            if (sortOrder == "desc")
            {
                order = -1;
            }
            else
            {
                order = 1;
            }
            switch (sortColumn)
            {
                case "ID":
                    list.Sort(
                         delegate(sp_GetSTTForExportResult m1, sp_GetSTTForExportResult m2)
                         { return m1.ID.CompareTo(m2.ID) * order; });
                    break;
                case "DisplayName":
                    list.Sort(
                         delegate(sp_GetSTTForExportResult m1, sp_GetSTTForExportResult m2)
                         { return m1.DisplayName.CompareTo(m2.DisplayName) * order; });
                    break;
                case "Status":
                    list.Sort(
                         delegate(sp_GetSTTForExportResult m1, sp_GetSTTForExportResult m2)
                         { return m1.Status.CompareTo(m2.Status) * order; });
                    break;
                case "Result":
                    list.Sort(
                         delegate(sp_GetSTTForExportResult m1, sp_GetSTTForExportResult m2)
                         { return m1.Result.CompareTo(m2.Result) * order; });
                    break;
                case "StartDate":
                    list.Sort(
                         delegate(sp_GetSTTForExportResult m1, sp_GetSTTForExportResult m2)
                         { return m1.StartDate.CompareTo(m2.StartDate) * order; });
                    break;
                case "ExpectedEndDate":
                    list.Sort(
                         delegate(sp_GetSTTForExportResult m1, sp_GetSTTForExportResult m2)
                         { return (m1.ExpectedEndDate.HasValue ? m1.ExpectedEndDate.Value.ToString(Constants.DATETIME_FORMAT_VIEW) : "").CompareTo((m2.ExpectedEndDate.HasValue ? m2.ExpectedEndDate.Value.ToString(Constants.DATETIME_FORMAT_VIEW) : "")) * order; });
                    break;
                case "Remark":
                    list.Sort(
                         delegate(sp_GetSTTForExportResult m1, sp_GetSTTForExportResult m2)
                         { return m1.Remarks.CompareTo(m2.Remarks) * order; });
                    break;
            }

            return list;
        }

        /// <summary>
        /// get STT by ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public STT GetById(string id)
        {
            return dbContext.STTs.Where(c => c.ID.Equals(id)).FirstOrDefault<STT>();
        }

        public STT GetSTTHasSeatcodeByID(string id)
        {
            return dbContext.STTs.Where(c => c.ID.Equals(id) && c.LocationCode != null).FirstOrDefault<STT>();            
        }

        /// <summary>
        /// get STT by ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public STT_RefResult GetRefResultById(string id)
        {
            return dbContext.STT_RefResults.Where(c => c.SttID.Equals(id)).FirstOrDefault<STT_RefResult>();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="objUI"></param>
        /// <returns></returns>
        public Message Insert(STT objUI)
        {
            Message msg = null;
            try
            {
                if (objUI != null)
                {
                    objUI.DeleteFlag = false;
                    dbContext.STTs.InsertOnSubmit(objUI);
                    dbContext.SubmitChanges();
                    //Write Log
                    new STTLogDao().WriteLogForSTT(null, objUI, ELogAction.Insert);
                    // Show success message
                    msg = new Message(MessageConstants.I0001, MessageType.Info, objUI.ID + " " + objUI.FirstName + " " + objUI.MiddleName + " " + objUI.LastName + "'", "added");
                }
            }
            catch
            {
                msg = new Message(MessageConstants.E0007, MessageType.Error);
            }
            return msg;

        }

        /// <summary>
        /// GetListClass
        /// </summary>
        /// <returns></returns>
        public List<sp_GetSTTClassResult> GetListClass()
        {
            return dbContext.sp_GetSTTClass().ToList <sp_GetSTTClassResult>();
        }

        public Message Update(STT objUI)
        {
            Message msg = null;
            try
            {
                if (objUI != null)
                {
                    STT objDb = GetById(objUI.ID);
                    if (objDb != null)
                    {
                        // Check valid update date
                        if (IsValidUpdateDate(objUI, objDb, out msg))
                        {
                            new STTLogDao().WriteLogForSTT(null, objUI, ELogAction.Update);
                            objDb.FirstName = objUI.FirstName;
                            objDb.MiddleName = objUI.MiddleName;
                            objDb.LastName = objUI.LastName;
                            objDb.Major = objUI.Major;
                            objDb.DOB = objUI.DOB;
                            objDb.ManagerId = objUI.ManagerId;
                            objDb.Project = objUI.Project;
                            objDb.POB = objUI.POB;
                            objDb.PlaceOfOrigin = objUI.PlaceOfOrigin;
                            objDb.Nationality = objUI.Nationality;
                            objDb.IDNumber = objUI.IDNumber;
                            objDb.IssueDate = objUI.IssueDate;
                            objDb.Gender = objUI.Gender;
                            objDb.Religion = objUI.Religion;
                            objDb.MarriedStatus = objUI.MarriedStatus;
                            objDb.STTStatusId = objUI.STTStatusId;
                            objDb.JR = objUI.JR;
                            objDb.JRApproval = objUI.JRApproval;
                            objDb.StartDate = objUI.StartDate;
                            objDb.ExpectedEndDate = objUI.ExpectedEndDate;                             
                            objDb.DepartmentId = objUI.DepartmentId;                             
                            objDb.LaborUnion = objUI.LaborUnion;
                            objDb.LaborUnionDate = objUI.LaborUnionDate;
                            objDb.HomePhone = objUI.HomePhone;
                            objDb.CellPhone = objUI.CellPhone;
                            objDb.Floor = objUI.Floor;
                            objDb.ExtensionNumber = objUI.ExtensionNumber;
                            objDb.SeatCode = objUI.SeatCode;
                            objDb.SkypeId = objUI.SkypeId;
                            objDb.YahooId = objUI.YahooId;
                            objDb.PersonalEmail = objUI.PersonalEmail;
                            objDb.OfficeEmail = objUI.OfficeEmail;
                            objDb.EmergencyContactName = objUI.EmergencyContactName;
                            objDb.EmergencyContactPhone = objUI.EmergencyContactPhone;
                            objDb.EmergencyContactRelationship = objUI.EmergencyContactRelationship;
                            objDb.PermanentAddress = objUI.PermanentAddress;
                            objDb.PermanentArea = objUI.PermanentArea;
                            objDb.PermanentDistrict = objUI.PermanentDistrict;
                            objDb.PermanentCityProvince = objUI.PermanentCityProvince;
                            objDb.PermanentCountry = objUI.PermanentCountry;
                            objDb.TempAddress = objUI.TempAddress;
                            objDb.TempArea = objUI.TempArea;
                            objDb.TempDistrict = objUI.TempDistrict;
                            objDb.TempCityProvince = objUI.TempCityProvince;
                            objDb.TempCountry = objUI.TempCountry;
                            objDb.BankName = objUI.BankName;
                            objDb.BankAccount = objUI.BankAccount;
                            objDb.Remarks = objUI.Remarks;
                            objDb.UpdateDate = DateTime.Now;
                            objDb.UpdatedBy = objUI.UpdatedBy;
                            objDb.VnFirstName = objUI.VnFirstName;
                            objDb.VnMiddleName = objUI.VnMiddleName;
                            objDb.VnLastName = objUI.VnLastName;
                            objDb.VnPOB = objUI.VnPOB;
                            objDb.VnPlaceOfOrigin = objUI.VnPlaceOfOrigin;
                            objDb.Degree = objUI.Degree;
                            objDb.OtherDegree = objUI.OtherDegree;
                            objDb.Race = objUI.Race;
                            objDb.IDIssueLocation = objUI.IDIssueLocation;
                            objDb.VnIDIssueLocation = objUI.VnIDIssueLocation;
                            objDb.VnPermanentAddress = objUI.VnPermanentAddress;
                            objDb.VnPermanentArea = objUI.VnPermanentArea;
                            objDb.VnPermanentDistrict = objUI.VnPermanentDistrict;
                            objDb.VnPermanentCityProvince = objUI.VnPermanentCityProvince;
                            objDb.VnPermanentCountry = objUI.VnPermanentCountry;
                            objDb.VnTempAddress = objUI.VnTempAddress;
                            objDb.VnTempArea = objUI.VnTempArea;
                            objDb.VnTempDistrict = objUI.VnTempDistrict;
                            objDb.VnTempCityProvince = objUI.VnTempCityProvince;
                            objDb.VnTempCountry = objUI.VnTempCountry;
                            objDb.LocationCode = objUI.LocationCode;
                            // Submit changes to dbContext
                            dbContext.SubmitChanges();
                            // Show success message
                            msg = new Message(MessageConstants.I0001, MessageType.Info, objDb.ID + " " + objDb.FirstName + " " + objDb.MiddleName + " " + objDb.LastName + "", "updated");

                        }
                    }
                }
            }
            catch
            {   
                msg = new Message(MessageConstants.E0007, MessageType.Error);
            }
            return msg;
        }
        
        public Message UpdateCV(STT objUI)
        {
            Message msg = null;
            try
            {
                if (objUI != null)
                {
                    // Get current group in dbContext
                    STT objDb = GetById(objUI.ID);
                    if (objDb != null)
                    {
                        // Check valid update date
                        if (IsValidUpdateDate(objUI, objDb, out msg))
                        {
                            new STTLogDao().WriteLogForUpdateCV(objUI, ELogAction.Update);
                            // Update info by objUI                       
                            objDb.CVFile = objUI.CVFile;
                            //objDb.UpdateDate = DateTime.Now;
                            //objDb.UpdatedBy = objUI.UpdatedBy;
                            // Submit changes to dbContext
                            dbContext.SubmitChanges();

                            // Show success message
                            msg = new Message(MessageConstants.I0001, MessageType.Info, "STT CV", "uploaded");
                        }
                    }
                }
            }
            catch
            {
                msg = new Message(MessageConstants.E0007, MessageType.Error);
            }
            return msg;
        }

        public Message UpdateImage(STT objUI)
        {
            Message msg = null;
            try
            {
                if (objUI != null)
                {
                    // Get current group in dbContext
                    STT objDb = GetById(objUI.ID);

                    if (objDb != null)
                    {
                        // Check valid update date
                        if (IsValidUpdateDate(objUI, objDb, out msg))
                        {
                            new STTLogDao().WriteLogForUpdateImage(objUI, ELogAction.Update);
                            // Update info by objUI                       
                            objDb.Photograph = objUI.Photograph;
                            //objDb.UpdateDate = DateTime.Now;
                            //objDb.UpdatedBy = objUI.UpdatedBy;

                            // Submit changes to dbContext
                            dbContext.SubmitChanges();

                            // Show success message
                            msg = new Message(MessageConstants.I0001, MessageType.Info, "STT photo", "uploaded");
                        }
                    }
                }
            }
            catch
            {
                msg = new Message(MessageConstants.E0007, MessageType.Error);
            }
            return msg;
        }
        public Message DeleteList(string ids, string userName)
        {
            Message msg = null;
            DbTransaction trans = null;
            try
            {
                dbContext.Connection.Open();
                trans = dbContext.Connection.BeginTransaction();
                dbContext.Transaction = trans;

                if (!string.IsNullOrEmpty(ids))
                {
                    ids = ids.TrimEnd(',');
                    string[] idArr = ids.Split(',');
                    int total = idArr.Count();
                    foreach (string id in idArr)
                    {
                        string empID = id;
                        STT emp = GetById(empID);
                        if (emp != null)
                        {
                            emp.UpdatedBy = userName;
                            Delete(emp);
                        }
                        else
                        {
                            total--;
                        }
                    }

                    // Show succes message
                    msg = new Message(MessageConstants.I0001, MessageType.Info, total.ToString() + " STT(s)", "deleted");
                    trans.Commit();
                }
            }
            catch
            {
                if (trans != null) trans.Rollback();
                // Show system error
                msg = new Message(MessageConstants.E0007, MessageType.Error);
            }
            return msg;
        }

        private void Delete(STT objUI)
        {
            if (objUI != null)
            {
                // Get current group in dbContext
                STT objDb = GetById(objUI.ID);
                if (objDb != null)
                {
                    // Set delete info                    
                    objDb.DeleteFlag = true;
                    objDb.UpdateDate = DateTime.Now;
                    objDb.UpdatedBy = objUI.UpdatedBy;
                    // Submit changes to dbContext

                    new STTLogDao().WriteLogForSTT(null, objUI, ELogAction.Delete);
                    dbContext.SubmitChanges();     
                }
            }
        }

        public Message UpdateResult(string sttID,int resultID)
        {
            Message msg = null;
            try
            {
                STT objDb = GetById(sttID);
                if (objDb != null)
                {
                        objDb.ResultId = resultID;
                        if (resultID == Constants.STT_RESULT_FAIL)
                        {
                            objDb.STTStatusId = Constants.STT_STATUS_REJECTED;
                        }
                        else
                        {
                            objDb.STTStatusId = Constants.STT_STATUS_NEED_TO_PROMOTED;
                        }
                        dbContext.SubmitChanges();
                        msg = new Message(MessageConstants.I0001, MessageType.Info, objDb.ID +" "+ objDb.FirstName + " " + objDb.MiddleName + " " + objDb.LastName, "updated");

                }
            }
            catch
            {
                msg = new Message(MessageConstants.E0007, MessageType.Error);
            }
            return msg;
        }

        private void UpdateForPromoted(STT objUI)
        {
            if (objUI != null)
            {
                STT objDb = GetById(objUI.ID);
                if (objDb != null)
                {
                    new STTLogDao().WriteLogForUpdatePromoted(objUI, ELogAction.Update);
                    objDb.STTStatusId =Constants.STT_STATUS_PROMOTED;
                    dbContext.SubmitChanges();
                }
            }
        }

        public Message UpdatePromote(string sttID,string newID, Employee obj)
        {
            Message msg = null;
            try
            {
            #region Set Value for Employee
            STT objSTT = new STTDao().GetById(sttID);
            
            if (objSTT != null)
            {
                UpdateForPromoted(objSTT);
                //Insert Employee
                obj.OldEmployeeId = sttID;
                obj.ID = newID;
                obj.FirstName = objSTT.FirstName;
                obj.MiddleName = objSTT.MiddleName;
                obj.LastName = objSTT.LastName;
                obj.DOB = objSTT.DOB;
                obj.POB = objSTT.POB;
                obj.PlaceOfOrigin = objSTT.PlaceOfOrigin;
                obj.Nationality = objSTT.Nationality;
                obj.IDNumber = objSTT.IDNumber;
                obj.IssueDate = objSTT.IssueDate;
                obj.Gender = objSTT.Gender;
                obj.Religion = objSTT.Religion;
                obj.MarriedStatus = objSTT.MarriedStatus;
                obj.JR = objSTT.JR;
                obj.JRApproval = objSTT.JRApproval;
                obj.StartDate = objSTT.StartDate;
                obj.DepartmentId = objSTT.DepartmentId;
                obj.LaborUnion = objSTT.LaborUnion;
                obj.LaborUnionDate = objSTT.LaborUnionDate;
                obj.HomePhone = objSTT.HomePhone;
                obj.CellPhone = objSTT.CellPhone;
                obj.Floor = objSTT.Floor;
                obj.ExtensionNumber = objSTT.ExtensionNumber;
                obj.SeatCode = objSTT.SeatCode;
                obj.SkypeId = objSTT.SkypeId;
                obj.YahooId = objSTT.YahooId;
                obj.PersonalEmail = objSTT.PersonalEmail;
                obj.OfficeEmail = objSTT.OfficeEmail;
                obj.EmergencyContactName = objSTT.EmergencyContactName;
                obj.EmergencyContactPhone = objSTT.EmergencyContactPhone;
                obj.EmergencyContactRelationship = objSTT.EmergencyContactRelationship;
                obj.PermanentAddress = objSTT.PermanentAddress;
                obj.PermanentArea = objSTT.PermanentArea;
                obj.PermanentDistrict = objSTT.PermanentDistrict;
                obj.PermanentCityProvince = objSTT.PermanentCityProvince;
                obj.PermanentCountry = objSTT.PermanentCountry;
                obj.TempAddress = objSTT.TempAddress;
                obj.TempArea = objSTT.TempArea;
                obj.TempDistrict = objSTT.TempDistrict;
                obj.TempCityProvince = objSTT.TempCityProvince;
                obj.TempCountry = objSTT.TempCountry;
                obj.BankName = objSTT.BankName;
                obj.BankAccount = objSTT.BankAccount;
                obj.Remarks = objSTT.Remarks;
                obj.UpdateDate = DateTime.Now;
                obj.CreateDate = DateTime.Now;
                obj.VnFirstName = objSTT.VnFirstName;
                obj.VnMiddleName = objSTT.VnMiddleName;
                obj.VnLastName = objSTT.VnLastName;
                obj.VnPOB = objSTT.VnPOB;
                obj.VnPlaceOfOrigin = objSTT.VnPlaceOfOrigin;
                obj.Degree = objSTT.Degree;
                obj.OtherDegree = objSTT.OtherDegree;
                obj.Race = objSTT.Race;
                obj.IDIssueLocation = objSTT.IDIssueLocation;
                obj.VnIDIssueLocation = objSTT.VnIDIssueLocation;
                obj.VnPermanentAddress = objSTT.VnPermanentAddress;
                obj.VnPermanentArea = objSTT.VnPermanentArea;
                obj.VnPermanentDistrict = objSTT.VnPermanentDistrict;
                obj.VnPermanentCityProvince = objSTT.VnPermanentCityProvince;
                obj.VnPermanentCountry = objSTT.VnPermanentCountry;
                obj.VnTempAddress = objSTT.VnTempAddress;
                obj.VnTempArea = objSTT.VnTempArea;
                obj.VnTempDistrict = objSTT.VnTempDistrict;
                obj.VnTempCityProvince = objSTT.VnTempCityProvince;
                obj.VnTempCountry = objSTT.VnTempCountry;
                msg = new EmployeeDao().Insert(obj);
                if (msg.MsgType == MessageType.Info)
                {
                    msg = new EmployeeDao().InsertTracking(obj);
                }
            }
            #endregion
            msg = new Message(MessageConstants.I0001, MessageType.Info, objSTT.ID + " " + objSTT.FirstName + " " + objSTT.MiddleName + " " + objSTT.LastName, "updated");
            }
            catch
            {
                msg = new Message(MessageConstants.E0007, MessageType.Error);
            }
            return msg;

        }

        #endregion

        #region Detail

        public Message UpdatePersonalInfo(STT objUI)
        {
            Message msg = null;
            try
            {
                if (objUI != null)
                {
                    // Get current group in dbContext
                    STT objDb = GetById(objUI.ID);

                    if (objDb != null)
                    {
                        // Check valid update date
                        if (IsValidUpdateDate(objUI, objDb, out msg))
                        {
                            new STTLogDao().WriteLogForUpdatePersonal(objUI, ELogAction.Update);
                            // Update info by objUI      
                            objDb.Major = objUI.Major;
                            objDb.FirstName = objUI.FirstName;
                            objDb.MiddleName = objUI.MiddleName;
                            objDb.LastName = objUI.LastName;
                            objDb.DOB = objUI.DOB;
                            objDb.POB = objUI.POB;
                            objDb.PlaceOfOrigin = objUI.PlaceOfOrigin;
                            objDb.Nationality = objUI.Nationality;
                            objDb.IDNumber = objUI.IDNumber;
                            objDb.IssueDate = objUI.IssueDate;
                            objDb.Gender = objUI.Gender;
                            objDb.Religion = objUI.Religion;
                            objDb.MarriedStatus = objUI.MarriedStatus;
                            objDb.STTStatusId = objUI.STTStatusId;
                            objDb.UpdateDate = DateTime.Now;
                            objDb.UpdatedBy = objUI.UpdatedBy;
                            objDb.VnFirstName = objUI.VnFirstName;
                            objDb.Race = objUI.Race;
                            objDb.VnMiddleName = objUI.VnMiddleName;
                            objDb.VnPlaceOfOrigin = objUI.VnPlaceOfOrigin;
                            objDb.VnLastName = objUI.VnLastName;
                            objDb.VnPOB = objUI.VnPOB;
                            objDb.Degree = objUI.Degree;
                            objDb.OtherDegree = objUI.OtherDegree;
                            objDb.IDIssueLocation = objUI.IDIssueLocation;
                            objDb.VnIDIssueLocation = objUI.VnIDIssueLocation;
                            // Submit changes to dbContext
                            dbContext.SubmitChanges();

                            // Show success message
                            msg = new Message(MessageConstants.I0001, MessageType.Info, objDb.ID + " " + objDb.FirstName + " " + objDb.MiddleName + " " + objDb.LastName + "'", "updated");
                        }
                    }
                }
            }
            catch
            {
                msg = new Message(MessageConstants.E0007, MessageType.Error);
            }


            return msg;
        }

        public Message UpdateCompanyInfo(STT objUI)
        {
            Message msg = null;
            try
            {
                if (objUI != null)
                {
                    // Get current group in dbContext
                    STT objDb = GetById(objUI.ID);

                    if (objDb != null)
                    {
                        // Check valid update date
                        if (IsValidUpdateDate(objUI, objDb, out msg))
                        {           
                            new STTLogDao().WriteLogForUpdateCompany(objUI, ELogAction.Update);
                           
                            // Update info by objUI                       
                            objDb.JR = objUI.JR;
                            objDb.JRApproval = objUI.JRApproval;
                            objDb.StartDate = objUI.StartDate;
                            objDb.ExpectedEndDate = objUI.ExpectedEndDate;
                            objDb.LaborUnion = objUI.LaborUnion;
                            objDb.LaborUnionDate = objUI.LaborUnionDate;
                            objDb.UpdateDate = DateTime.Now;
                            objDb.UpdatedBy = objUI.UpdatedBy;
                            objDb.DepartmentId = objUI.DepartmentId;
                            objDb.ManagerId = objUI.ManagerId;
                            objDb.LocationCode = objUI.LocationCode;
                            // Submit changes to dbContext
                            dbContext.SubmitChanges();

                            // Show success message
                            msg = new Message(MessageConstants.I0001, MessageType.Info, objDb.ID + " "  +objDb.FirstName + " " + objDb.MiddleName + " " + objDb.LastName + "'", "updated");
                        }
                    }
                }
            }
            catch
            {
                msg = new Message(MessageConstants.E0007, MessageType.Error);
            }

            return msg;
        }

        public Message UpdateContactInfo(STT objUI)
        {
            Message msg = null;

            try
            {
                if (objUI != null)
                {
                    // Get current group in dbContext
                    STT objDb = GetById(objUI.ID);

                    if (objDb != null)
                    {
                        // Check valid update date
                        if (IsValidUpdateDate(objUI, objDb, out msg))
                        {
                            new STTLogDao().WriteLogForUpdateContact(objUI, ELogAction.Update);
                            // Update info by objUI                       
                            objDb.HomePhone = objUI.HomePhone;
                            objDb.CellPhone = objUI.CellPhone;
                            objDb.Floor = objUI.Floor;
                            objDb.ExtensionNumber = objUI.ExtensionNumber;
                            objDb.SeatCode = objUI.SeatCode;
                            objDb.SkypeId = objUI.SkypeId;
                            objDb.YahooId = objUI.YahooId;
                            objDb.PersonalEmail = objUI.PersonalEmail;
                            objDb.OfficeEmail = objUI.OfficeEmail;
                            objDb.EmergencyContactName = objUI.EmergencyContactName;
                            objDb.EmergencyContactPhone = objUI.EmergencyContactPhone;
                            objDb.EmergencyContactRelationship = objUI.EmergencyContactRelationship;
                            objDb.UpdateDate = DateTime.Now;
                            objDb.UpdatedBy = objUI.UpdatedBy;

                            // Submit changes to dbContext
                            dbContext.SubmitChanges();

                            // Show success message
                            msg = new Message(MessageConstants.I0001, MessageType.Info, objDb.ID + " "  +objDb.FirstName + " " + objDb.MiddleName + " " + objDb.LastName + "'", "updated");
                        }
                    }
                }
            }
            catch
            {
                msg = new Message(MessageConstants.E0007, MessageType.Error);
            }


            return msg;
        }

        public Message UpdateBankAccountInfo(STT objUI)
        {
            Message msg = null;

            try
            {
                if (objUI != null)
                {
                    // Get current group in dbContext
                    STT objDb = GetById(objUI.ID);

                    if (objDb != null)
                    {
                        // Check valid update date
                        if (IsValidUpdateDate(objUI, objDb, out msg))
                        {
                            new STTLogDao().WriteLogForUpdateBanks(objUI, ELogAction.Update);
                            // Update info by objUI                       
                            objDb.BankName = objUI.BankName;
                            objDb.BankAccount = objUI.BankAccount;
                            objDb.UpdateDate = DateTime.Now;
                            objDb.UpdatedBy = objUI.UpdatedBy;

                            // Submit changes to dbContext
                            dbContext.SubmitChanges();

                            // Show success message
                            msg = new Message(MessageConstants.I0001, MessageType.Info, objDb.ID + " "+ objDb.FirstName + " " + objDb.MiddleName + " " + objDb.LastName + "'", "updated");
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

        public Message UpdateRemark(STT objUI)
        {
            Message msg = null;

            try
            {

                if (objUI != null)
                {
                    // Get current group in dbContext
                    STT objDb = GetById(objUI.ID);

                    if (objDb != null)
                    {
                        // Check valid update date
                        if (IsValidUpdateDate(objUI, objDb, out msg))
                        {
                            new STTLogDao().WriteLogForUpdateRemarks(objUI, ELogAction.Update);
                            // Update info by objUI                       
                            objDb.Remarks = objUI.Remarks;
                            objDb.UpdateDate = DateTime.Now;
                            objDb.UpdatedBy = objUI.UpdatedBy;

                            // Submit changes to dbContext
                            dbContext.SubmitChanges();

                            // Show success message
                            msg = new Message(MessageConstants.I0001, MessageType.Info, objDb.ID + " " + objDb.FirstName + " " + objDb.MiddleName + " " + objDb.LastName + "'", "updated");
                        }
                    }
                }
            }
            catch
            {
                msg = new Message(MessageConstants.E0007, MessageType.Error);
            }
            return msg;
        }

        public Message UpdateAddressInfo(STT objUI)
        {
            Message msg = null;

            try
            {
                if (objUI != null)
                {
                    // Get current group in dbContext
                    STT objDb = GetById(objUI.ID);

                    if (objDb != null)
                    {
                        // Check valid update date
                        if (IsValidUpdateDate(objUI, objDb, out msg))
                        {
                            new STTLogDao().WriteLogForUpdateAddress(objUI, ELogAction.Update);
                            // Update info by objUI                       
                            objDb.PermanentAddress = objUI.PermanentAddress;
                            objDb.PermanentArea = objUI.PermanentArea;
                            objDb.PermanentDistrict = objUI.PermanentDistrict;
                            objDb.PermanentCityProvince = objUI.PermanentCityProvince;
                            objDb.PermanentCountry = objUI.PermanentCountry;
                            objDb.TempAddress = objUI.TempAddress;
                            objDb.TempArea = objUI.TempArea;
                            objDb.TempDistrict = objUI.TempDistrict;
                            objDb.TempCityProvince = objUI.TempCityProvince;
                            objDb.TempCountry = objUI.TempCountry;
                            objDb.VnPermanentAddress = objUI.VnPermanentAddress;
                            objDb.VnPermanentArea = objUI.VnPermanentArea;
                            objDb.VnPermanentDistrict = objUI.VnPermanentDistrict;
                            objDb.VnPermanentCityProvince = objUI.VnPermanentCityProvince;
                            objDb.VnPermanentCountry = objUI.VnPermanentCountry;
                            objDb.VnTempAddress = objUI.VnTempAddress;
                            objDb.VnTempArea = objUI.VnTempArea;
                            objDb.VnTempDistrict = objUI.VnTempDistrict;
                            objDb.VnTempCityProvince = objUI.VnTempCityProvince;
                            objDb.VnTempCountry = objUI.VnTempCountry;
                            objDb.UpdateDate = DateTime.Now;
                            objDb.UpdatedBy = objUI.UpdatedBy;

                            // Submit changes to dbContext
                            dbContext.SubmitChanges();

                            // Show success message
                            msg = new Message(MessageConstants.I0001, MessageType.Info, objDb.ID + " " + objDb.FirstName + " " + objDb.MiddleName + " " + objDb.LastName + "'", "updated");
                        }
                    }
                }
            }
            catch
            {
                msg = new Message(MessageConstants.E0007, MessageType.Error);
            }
            return msg;
        }

        /// <summary>
        /// Check update date whether is valid
        /// </summary>
        /// <param name="objUI"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        private bool IsValidUpdateDate(STT objUI, STT objDb, out Message msg)
        {
            bool isValid = false;
            msg = null;

            try
            {
                if ((objUI != null) && (objUI.UpdateDate != null))
                {
                    if (objDb != null)
                    {
                        if (objDb.UpdateDate.ToString().Equals(objUI.UpdateDate.ToString()))
                        {
                            isValid = true;
                        }
                        else
                        {
                            msg = new Message(MessageConstants.E0025, MessageType.Error, "STT " + objDb.ID);
                        }
                    }
                }
            }
            catch
            {
                throw;
            }

            return isValid;
        }

        public bool IsValidUpdateDateForResult(DateTime currentDate, STT objDb, out Message msg)
        {
            bool isValid = false;
            msg = null;

            try
            {
                    if (objDb != null)
                    {
                        if (objDb.UpdateDate.ToString().Equals(currentDate.ToString()))
                        {
                            isValid = true;
                        }
                        else
                        {
                            msg = new Message(MessageConstants.E0025, MessageType.Error, "STT " + objDb.ID);
                        }
                    }
            }
            catch
            {
                throw;
            }

            return isValid;
        }

        #endregion


        public Message UpdatePosition(STT objUI, bool onlyLocation = false)
        {
            Message msg = null;
            try
            {
                STT stt = GetById(objUI.ID);
                if (stt != null)
                {
                    new STTLogDao().WriteLogForUpdatePosition(objUI, ELogAction.Update);
                    if (!onlyLocation)
                    {
                        stt.ManagerId = objUI.ManagerId;
                        stt.Project = objUI.Project;
                    }
                    stt.Floor = objUI.Floor;
                    stt.SeatCode = objUI.SeatCode;
                    stt.LocationCode = objUI.LocationCode;
                    dbContext.SubmitChanges();
                    msg = new Message(MessageConstants.I0001, MessageType.Info, "STT " + stt.ID, "updated");
                }
            }
            catch
            {
                msg = new Message(MessageConstants.E0033, MessageType.Error);
            }

            return msg;
        }
    }
}