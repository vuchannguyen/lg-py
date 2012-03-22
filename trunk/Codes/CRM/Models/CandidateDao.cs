using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CRM.Library.Common;
using System.Data.Common;
using System.Web.UI.WebControls;
using System.Linq.Dynamic;
using System.Text.RegularExpressions;
using System.Data.Linq.SqlClient;

namespace CRM.Models
{
    public class CandidateDao : BaseDao
    {
        #region New paging GetCandidateList
        public IQueryable<Candidate> GetQueryListCandidate(string candidateName, int? source, int? position, int? status, string fromDate, string toDate, int? university, int? officeId)
        {
            var sql = from can in dbContext.Candidates
                      select can;

            if (!string.IsNullOrEmpty(candidateName))
            {
                candidateName = CommonFunc.GetFilterText(candidateName);
                sql = sql.Where(p => (p.MiddleName != null ? SqlMethods.Like(p.FirstName + " " + p.MiddleName + " " + p.LastName, candidateName) : SqlMethods.Like(p.FirstName + " " + p.LastName, candidateName))
                                        || SqlMethods.Like(p.Email, candidateName));
            }

            if (ConvertUtil.ConvertToInt(source) != 0)
            {
                sql = sql.Where(p => p.SourceId == ConvertUtil.ConvertToInt(source));
            }

            if (ConvertUtil.ConvertToInt(status) != 0)
            {
                sql = sql.Where(p => p.Status == ConvertUtil.ConvertToInt(status));
            }

            if (ConvertUtil.ConvertToInt(position) != 0)
            {
                sql = sql.Where(p => p.TitleId == ConvertUtil.ConvertToInt(position));
            }

            if (!string.IsNullOrEmpty(fromDate))
            {
                sql = sql.Where(p => p.SearchDate >= ConvertUtil.ConvertToDatetime(fromDate));
            }

            if (!string.IsNullOrEmpty(toDate))
            {
                sql = sql.Where(p => p.SearchDate <= ConvertUtil.ConvertToDatetime(toDate));
            }

            if (ConvertUtil.ConvertToInt(university) != 0)
            {
                sql = sql.Where(p => p.UniversityId == ConvertUtil.ConvertToInt(university));
            }

            if (ConvertUtil.ConvertToInt(officeId) != 0)
            {
                sql = sql.Where(p => p.OfficeID == ConvertUtil.ConvertToInt(officeId));
            }

            sql = sql.Where(p => p.DeleteFlag == false);

            return sql;
        }

        public int GetCountListCandidateLinq(string candidateName, int? source, int? position, int? status, string fromDate, string toDate, int? university, int? officeId)
        {
            return GetQueryListCandidate(candidateName, source, position, status, fromDate, toDate, university, officeId).Count();
        }

        public List<Candidate> GetListCandidateLinq(string candidateName, int? source, int? position, int? status, string fromDate, string toDate, int? university, int? officeId, string sortColumn, string sortOrder, int skip, int take)
        {
            string sortSQL = string.Empty;

            switch (sortColumn)
            {
                case "CandidateName":
                    sortSQL += "FirstName " + sortOrder + "," + "MiddleName " + sortOrder + "," + "LastName " + sortOrder;
                    break;

                case "DOB":
                    sortSQL += "DOB " + sortOrder;
                    break;

                case "Gender":
                    sortSQL += "Gender " + sortOrder;
                    break;

                case "SearchDate":
                    sortSQL += "SearchDate " + sortOrder;
                    break;

                case "Source":
                    sortSQL += "CandidateSource.Name " + sortOrder;
                    break;

                case "Title":
                    sortSQL += "JobTitleLevel.DisplayName " + sortOrder;
                    break;

                case "Status":
                    sortSQL += "Status " + sortOrder;
                    break;

                case "CellPhone":
                    sortSQL += "CellPhone " + sortOrder;
                    break;

                case "University":
                    sortSQL += "University.Name " + sortOrder;
                    break;
            }

            var sql = GetQueryListCandidate(candidateName, source, position, status, fromDate, toDate, university, officeId).OrderBy(sortSQL);

            if (skip == 0 && take == 0)
            {
                return sql.ToList();
            }

            return sql.Skip(skip).Take(take).ToList();
        }

        # endregion



        #region "Get"
        public List<ListItem> GetListSource()
        {
            List<ListItem> listItem = new List<ListItem>();
            List<CandidateSource> list = GetListSourceItem();
            foreach (CandidateSource item in list)
            {
                listItem.Add(new ListItem(item.Name, item.SourceId.ToString()));
            }
            return listItem;
        }

        public Message InsertMulti(List<Candidate> list)
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
                    dbContext.Candidates.InsertAllOnSubmit(list);
                    dbContext.SubmitChanges();
                    msg = new Message(MessageConstants.I0001, MessageType.Info, list.Count.ToString() + " Candidate(s)", "insert");
                    trans.Commit();
                    
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

        public List<CandidateSource> GetListSourceItem()
        {
            return dbContext.CandidateSources.ToList<CandidateSource>();
        }

        public CandidateSource GetSourceByName(string name)
        {
            return dbContext.CandidateSources.Where(p => p.Name == name).FirstOrDefault<CandidateSource>();
        }

        public CandidateSource GetSourceById(int id)
        {
            return dbContext.CandidateSources.Where(c => c.SourceId == id).FirstOrDefault<CandidateSource>();
        }

        public List<sp_GetCandidateResult> GetList(string candidate_name, int source, int position,int status, string fromdate, string todate,int university)
        {
            return dbContext.sp_GetCandidate(candidate_name, source, position, status, fromdate, todate,university, 0).ToList<sp_GetCandidateResult>();
        }
        public List<sp_GetCandidateResult> GetList(string candidate_name, int source, int position, int status, string fromdate, string todate, int university, int officeId)
        {
            return dbContext.sp_GetCandidate(candidate_name, source, position, status, fromdate, todate, university, officeId).ToList<sp_GetCandidateResult>();
        }
        
        public Candidate GetById(string id)
        {
            return dbContext.Candidates.Where(c => c.ID.ToString().Equals(id)).SingleOrDefault<Candidate>();
        }

        public Candidate GetByEmail(string email)
        {
            List<Candidate> cans = dbContext.Candidates.Where(c => c.Email.Equals(email)).ToList<Candidate>();
            if (cans.Count > 0)
            {
                return cans[0];
            }
            else
            {
                return null;
            }            
        }
        public List<Candidate> GetByNameAndDOB(string vnName, string engName, string DOB)
        {
            vnName = vnName.Replace(" ", "");
            engName = engName.Replace(" ", "");
            List<Candidate> cans = dbContext.Candidates.Where(c => 
                                (
                                    (c.FirstName + c.MiddleName + c.LastName).ToLower().Replace(" ", "") == engName.ToLower() 
                                    && (c.VnFirstName + c.VnMiddleName + c.VnLastName.ToLower().Replace(" ","")==vnName.ToLower())
                                    && (c.DOB==DateTime.Parse(DOB))
                                    && (c.DeleteFlag==false)
                                )
                                
                                ).ToList<Candidate>();
            
            return cans;
        }
        public Candidate GetByName(string fullName)
        {
            fullName = fullName.Replace(" ", "");
            List<Candidate> cans = dbContext.Candidates.Where(c => ((c.FirstName + c.MiddleName +c.LastName).ToLower().Replace(" ","") == fullName.ToLower())).ToList<Candidate>();
            if (cans.Count > 0)
            {
                return cans[0];
            }
            return null;
        }

        public List<sp_GetCandidateResult> Sort(List<sp_GetCandidateResult> list, string sortColumn, string sortOrder)
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

                case "CandidateName":
                    list.Sort(
                         delegate(sp_GetCandidateResult m1, sp_GetCandidateResult m2)
                         { return m1.DisplayName.CompareTo(m2.DisplayName) * order; });
                    break;
                case "DOB":
                    if (order == -1)
                    {
                        list = list.OrderByDescending(p => p.DOB).ToList<sp_GetCandidateResult>();
                    }
                    else
                    {
                        list = list.OrderBy(p => p.DOB).ToList<sp_GetCandidateResult>();
                    }
                    break;
                case "Gender":
                    list.Sort(
                         delegate(sp_GetCandidateResult m1, sp_GetCandidateResult m2)
                         {
                             return m1.Gender.CompareTo(m2.Gender) * order;
                         });
                    break;
                case "SearchDate":
                    list.Sort(
                         delegate(sp_GetCandidateResult m1, sp_GetCandidateResult m2)
                         { return m1.SearchDate.CompareTo(m2.SearchDate) * order; });
                    break;
                case "Source":
                    list.Sort(
                         delegate(sp_GetCandidateResult m1, sp_GetCandidateResult m2)
                         { return m1.SourceName.CompareTo(m2.SourceName) * order; });
                    break;
                case "Title":
                    list.Sort(
                         delegate(sp_GetCandidateResult m1, sp_GetCandidateResult m2)
                         { return m1.Title.CompareTo(m2.Title) * order; });
                    break;
                case "Status":
                    list.Sort(
                         delegate(sp_GetCandidateResult m1, sp_GetCandidateResult m2)
                         { return m1.Status.CompareTo(m2.Status) * order; });
                    break;
                case "CellPhone":
                    list.Sort(
                         delegate(sp_GetCandidateResult m1, sp_GetCandidateResult m2)
                         { return m1.CellPhone.CompareTo(m2.CellPhone) * order; });
                    break;
                case "University":
                    list.Sort(
                         delegate(sp_GetCandidateResult m1, sp_GetCandidateResult m2)
                         { return (!string.IsNullOrEmpty(m1.University) ? m1.University : string.Empty).CompareTo((!string.IsNullOrEmpty(m2.University) ? m2.University : string.Empty)) * order; });
                    break;
            }

            return list;
        }
        #endregion
        #region "Insert-delete-update"
        public void UpdateStatus(CandidateStatus status,int candidateID)
        {
            Candidate objDb = GetById(candidateID.ToString());
            if (objDb != null)
            {
                objDb.Status = (int)status;
                //new CandidateLogDao().WriteLogForUpdateStatus(objDb, ELogAction.Update);
                dbContext.SubmitChanges();
            }
        }

        public Candidate GetByJRID(int id)
        {
            return dbContext.Candidates.Where(q => q.JRId == id && (q.SaveHistory.HasValue && q.SaveHistory == true)).FirstOrDefault();
        }

        public Message Inser(Candidate objUI)
        {
            Message msg = null;
            try
            {
                if (objUI != null)
                {
                    objUI.DeleteFlag = false;
                    objUI.SaveHistory = false;
                    dbContext.Candidates.InsertOnSubmit(objUI);
                    dbContext.SubmitChanges();
                    //Write Log
                    new CandidateLogDao().WriteLogForCandidate(null, objUI, ELogAction.Insert);
                    msg = new Message(MessageConstants.I0001, MessageType.Info, "Candidate '" + objUI.FirstName + " " + objUI.MiddleName + " " + objUI.LastName + "'", "added");
                }
            }
            catch (Exception)
            {
                // Show system error
                msg = new Message(MessageConstants.E0007, MessageType.Error);
            }
            return msg;

        }
        public Message Update(Candidate objUI)
        {
            Message msg = null;
            try
            {
                if (objUI != null)
                {
                    // Get current group in dbContext
                    Candidate objDb = GetById(objUI.ID.ToString());

                    if (objDb != null)
                    {
                        new CandidateLogDao().WriteLogForCandidate(null, objUI, ELogAction.Update);
                        objDb.Address = objUI.Address;
                        objDb.CellPhone = objUI.CellPhone;
                        objDb.CVFile = objUI.CVFile;
                        objDb.DOB = objUI.DOB;
                        objDb.Email = objUI.Email;
                        objDb.FirstName = objUI.FirstName;
                        objDb.LastName = objUI.LastName;
                        objDb.MiddleName = objUI.MiddleName;
                        objDb.Note = objUI.Note;
                        objDb.Photograph = objUI.Photograph;
                        objDb.Gender = objUI.Gender;
                        objDb.SearchDate = objUI.SearchDate;
                        objDb.SourceId = objUI.SourceId;
                        objDb.TitleId = objUI.TitleId;
                        objDb.Status = objUI.Status;
                        objDb.VnFirstName = objUI.VnFirstName;
                        objDb.VnLastName = objUI.VnLastName;
                        objDb.VnMiddleName = objUI.VnMiddleName;
                        objDb.UpdateDate = DateTime.Now;
                        objDb.UpdatedBy = objUI.UpdatedBy;
                        objDb.UniversityId = objUI.UniversityId;
                        objDb.OfficeID = objUI.OfficeID;
                        dbContext.SubmitChanges();
                        // Show success message
                        msg = new Message(MessageConstants.I0001, MessageType.Info, "Candidate '" + objUI.FirstName + " " + objUI.MiddleName + " " + objUI.LastName + "'", "updated");
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
        public Message DeleteList(string ids, string stUpdatedBy)
        {
            Message msg = null;
            DbTransaction trans = null;
            bool canDelete = true;
            try
            {
                dbContext.Connection.Open();
                trans = dbContext.Connection.BeginTransaction();
                dbContext.Transaction = trans;
                
                if (!string.IsNullOrEmpty(ids))
                {
                    ids = ids.TrimEnd(',');
                    string[] idArr = ids.Split(',');
                    int totalId = idArr.Count();
                    foreach (string id in idArr)
                    {
                      
                        Candidate candidateObj = GetById(id);
                        if (candidateObj != null)
                        {
                            candidateObj.UpdatedBy = stUpdatedBy;
                            candidateObj.DeleteFlag = true;
                            candidateObj.UpdateDate = DateTime.Now;
                            candidateObj.UpdatedBy = candidateObj.UpdatedBy;
                            new CandidateLogDao().WriteLogForCandidate(null, candidateObj, ELogAction.Delete);
                            dbContext.SubmitChanges();
                        }
                        else
                        {
                            totalId--;
                        }
                    }
                    if (canDelete)
                    {
                        // Show succes message
                        msg = new Message(MessageConstants.I0001, MessageType.Info, totalId.ToString()+ " candidate(s)", "deleted");
                        trans.Commit();
                    }
                    else
                    {
                        trans.Rollback();
                    }
                }
            }
            catch (Exception)
            {
                if (trans != null) trans.Rollback();
                // Show system error
                msg = new Message(MessageConstants.E0007, MessageType.Error);
            }
            return msg;
        }
       
        #endregion
        #region "General"
        public Message UpdateImage(Candidate objUI)
        {
            Message msg = null;
            try
            {
                if (objUI != null)
                {
                    Candidate objDb = GetById(objUI.ID.ToString());

                    if (objDb != null)
                    {
                        new CandidateLogDao().WriteLogForUpdateImage(objUI, ELogAction.Update);
                        objDb.Photograph = objUI.Photograph;
                        objDb.UpdateDate = DateTime.Now;
                        objDb.UpdatedBy = objUI.UpdatedBy;
                        dbContext.SubmitChanges();

                        msg = new Message(MessageConstants.I0001, MessageType.Info, "Candidate photo", "uploaded");

                    }
                }
            }
            catch
            {
                msg = new Message(MessageConstants.E0007, MessageType.Error);
            }
            return msg;
        }
        public Message UpdateFile(Candidate objUI)
        {
            Message msg = null;
            try
            {
                if (objUI != null)
                {
                    Candidate objDb = GetById(objUI.ID.ToString());

                    if (objDb != null)
                    {
                        new CandidateLogDao().WriteLogForUpdateCV(objUI, ELogAction.Update);
                        objDb.CVFile = objUI.CVFile;
                        objDb.UpdateDate = DateTime.Now;
                        objDb.UpdatedBy = objUI.UpdatedBy;
                        dbContext.SubmitChanges();

                        msg = new Message(MessageConstants.I0001, MessageType.Info, "Candidate CV File", "uploaded");

                    }
                }
            }
            catch
            {
                msg = new Message(MessageConstants.E0007, MessageType.Error);
            }
            return msg;
        }
        /////////////////////// vkson added:06/10/2010
        /// <summary>
        /// UpdateJR
        /// </summary>
        /// <param name="objUI"></param>
        /// <returns></returns>
        public Message UpdateJR(Candidate objUI)
        {
            Message msg = null;

            try
            {

                if (objUI != null)
                {
                    // Get current group in dbContext
                    Candidate objDb = GetById(objUI.ID.ToString());

                    if (objDb != null)
                    {
                        // Check valid update date
                        if (IsValidUpdateDate(objUI, objDb, out msg))
                        {
                            // Update info by objUI   
                            new CandidateLogDao().WriteLogForUpdateJR(objUI, ELogAction.Update);
                            objDb.JRId = objUI.JRId;
                            objDb.UpdateDate = DateTime.Now;
                            objDb.UpdatedBy = objUI.UpdatedBy;

                            // Submit changes to dbContext
                            dbContext.SubmitChanges();

                            // Show success message
                            msg = new Message(MessageConstants.I0001, MessageType.Info, "Candidate '" + objDb.FirstName + " " + objDb.MiddleName + " " + objDb.LastName + "'", "updated");
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
        private bool IsValidUpdateDate(Candidate objUI, Candidate objDb, out Message msg)
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
                            msg = new Message(MessageConstants.E0025, MessageType.Error, "Candidate ID " + objDb.ID);
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

        /// <summary>
        /// Check candidate id was employee 
        /// </summary>
        /// <param name="id">candidate id</param>
        /// <returns>bool</returns>
        public bool IsEmployee(string id)
        {
            Employee emp = dbContext.Employees.Where(p => p.CandidateId.Equals(id)).FirstOrDefault();
            if (emp != null)
                return true;
            else
                return false;
        }

        ///////////////////////end vkson
        #endregion

        public List<University> GetUniversityList(string text)
        {
            return dbContext.Universities.Where(p => p.Name.ToLower().Contains(text.ToLower())).ToList();
        }

        public University GetUniversityByID(int id)
        {
            return dbContext.Universities.Where(p => p.ID == id).FirstOrDefault();
        }

        /// <summary>
        /// Get all University
        /// </summary>
        /// <returns></returns>
        public List<University> GetListUniversity()
        {
            return dbContext.Universities.ToList<University>();
        }
    }

}