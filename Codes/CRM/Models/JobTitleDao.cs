using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using System.Data.Common;
using CRM.Library.Common;
using CRM.Models;


namespace CRM.Models
{
    public class JobTitleDao : BaseDao
    {
        #region public method

        /// <summary>
        /// Get List
        /// </summary>
        /// <returns></returns>
        public List<JobTitle> GetList()
        {
            return dbContext.JobTitles.Where(p => p.DeleteFlag == false).ToList<JobTitle>();
        }

        /// <summary>
        /// Get By ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public JobTitle GetByID(int id)
        {
            return dbContext.JobTitles.Where(p => p.JobTitleId == id).FirstOrDefault<JobTitle>();
        }

        /// <summary>
        /// Get Manager Title
        /// </summary>
        /// <returns></returns>
        public List<JobTitle> GetManagerTitle()
        {
            return dbContext.JobTitles.Where(p => p.DeleteFlag == false && p.IsManager == true).ToList<JobTitle>();
        }

        /// <summary>
        /// Get List By Department Id
        /// </summary>
        /// <param name="departmentId"></param>
        /// <returns></returns>
        public List<JobTitle> GetListByDepartment(int departmentId)
        {
            return dbContext.JobTitles.Where(p => p.DepartmentId == departmentId).ToList<JobTitle>();
        }

        //Tuc 13-12-11
        public List<JobTitle> GetJobTitleListByDepartmentId(int DepartmentId)
        {
            return dbContext.JobTitles.Where(p => p.Department != null && p.DepartmentId == DepartmentId
                && p.DeleteFlag == false).ToList<JobTitle>();
        }

        /// <summary>
        /// Insert to database
        /// </summary>
        /// <param name="objUI"></param>
        /// <returns></returns>
        public Message Insert(JobTitle objUI)
        {
            Message msg = null;
            try
            {
                if (objUI != null)
                {
                    // Set more info                    
                    dbContext.JobTitles.InsertOnSubmit(objUI);
                    dbContext.SubmitChanges();
                    msg = new Message(MessageConstants.I0001, MessageType.Info, "Job Title '" + objUI.JobTitleName + "'", "added");
                }
            }
            catch (Exception)
            {
                // Show system error
                msg = new Message(MessageConstants.E0007, MessageType.Error);
            }
            return msg;
        }


        /// <summary>
        /// Update
        /// </summary>
        /// <param name="objUI"></param>
        /// <returns></returns>
        public Message Update(JobTitle objUI)
        {
            Message msg = null;
            try
            {
                if (objUI != null)
                {
                    JobTitle objDB = GetByID(objUI.JobTitleId);
                    if (IsValidExamUpdateDate(objUI, objDB, out msg))
                    {
                        objDB.JobTitleName = objUI.JobTitleName;
                        objDB.DepartmentId = objUI.DepartmentId;
                        objDB.Description = objUI.Description;
                        objDB.IsManager = objUI.IsManager;

                        // Set more info         
                        objDB.UpdateDate = DateTime.Now;
                        objDB.UpdatedBy = objUI.UpdatedBy;

                        dbContext.SubmitChanges();
                        msg = new Message(MessageConstants.I0001, MessageType.Info, "Job Title '" + objUI.JobTitleName + "'", "updated");
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

        /// <summary>
        /// Sort JobTitle
        /// </summary>
        /// <param name="list"></param>
        /// <param name="sortColumn"></param>
        /// <param name="sortOrder"></param>
        /// <returns></returns>
        public List<JobTitle> Sort(List<JobTitle> list, string sortColumn, string sortOrder)
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
                case "TitleName":
                    list.Sort(
                         delegate(JobTitle m1, JobTitle m2)
                         { return m1.JobTitleName.CompareTo(m2.JobTitleName) * order; });
                    break;
                case "Department":
                    list.Sort(
                         delegate(JobTitle m1, JobTitle m2)
                         { return m1.DepartmentId.CompareTo(m2.DepartmentId) * order; });
                    break;
                case "Description":
                    list.Sort(
                         delegate(JobTitle m1, JobTitle m2)
                         { return m1.Description.CompareTo(m2.Description) * order; });
                    break;
                case "IsManager":
                    list.Sort(
                         delegate(JobTitle m1, JobTitle m2)
                         { return m1.IsManager.CompareTo(m2.IsManager) * order; });
                    break;
            }

            return list;
        }
        /// <summary>
        /// Check update date whether is valid
        /// </summary>
        /// <param name="objUI"></param>
        /// <param name="objDb"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        private bool IsValidExamUpdateDate(JobTitle objUI, JobTitle objDb, out Message msg)
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
                            msg = new Message(MessageConstants.E0025, MessageType.Error, "Job Title '" + objDb.JobTitleName + "'");
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
        /// Delete a list of job title
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public Message DeleteList(string ids, string userName)
        {
            Message msg = null;
            DbTransaction trans = null;
            bool isOK = true;
            try
            {
                //begin transaction
                dbContext.Connection.Open();
                trans = dbContext.Connection.BeginTransaction();
                dbContext.Transaction = trans;

                if (!string.IsNullOrEmpty(ids))
                {
                    //split ids by char ','
                    ids = ids.TrimEnd(Constants.SEPARATE_IDS_CHAR);
                    string[] idArr = ids.Split(Constants.SEPARATE_IDS_CHAR);
                    int jobTitleId = 0;
                    int totalID = idArr.Count();

                    //loop each id to delete
                    foreach (string id in idArr)
                    {
                        //is check all records to delete 
                        bool isInterger = Int32.TryParse(id, out jobTitleId);
                        if (isInterger)
                        {
                            JobTitle jobTitle = GetByID(jobTitleId);
                            if (jobTitle != null)
                            {
                                jobTitle.UpdatedBy = userName;
                                //delete from db
                                msg = Delete(jobTitle);
                                if (msg.MsgType == MessageType.Error)
                                {
                                    isOK = false;
                                    break;
                                }

                            }
                        }
                        else
                        {
                            //minus the first item which is 'false' value
                            totalID--;
                        }
                    }

                    if (isOK)
                    {
                        // Show succes message
                        msg = new Message(MessageConstants.I0001, MessageType.Info, totalID.ToString() + " job title(s)", "deleted");
                        trans.Commit();
                    }
                    else
                    {
                        trans.Rollback();
                    }
                }
            }
            catch
            {
                if (trans != null) { trans.Rollback(); }
                // Show system error
                msg = new Message(MessageConstants.E0006, MessageType.Error, "delete", "it");
            }
            return msg;
        }

        /// <summary>
        /// Delete by set DeleteFlag = true
        /// </summary>
        /// <param name="objUI"></param>
        private Message Delete(JobTitle objUI)
        {
            Message msg = null;
            try
            {
                if (objUI != null)
                {
                    JobTitle objDb = GetByID(objUI.JobTitleId);
                    if (objDb != null)
                    {
                        bool isValid = true;
                        List<JobTitleLevel> listJobTitle = dbContext.JobTitleLevels.Where(c => c.JobTitleId == objUI.JobTitleId).ToList<JobTitleLevel>();
                        foreach (JobTitleLevel item in listJobTitle)
                        {
                            if (!IsValidDeleteJobTitle(item.ID))
                            {
                                isValid = false;
                                break;
                            }
                        }
                        if (isValid == true)
                        {
                            // Set delete info
                            objDb.DeleteFlag = true;
                            objDb.UpdateDate = DateTime.Now;
                            objDb.UpdatedBy = objUI.UpdatedBy;
                            msg = new Message(MessageConstants.I0001, MessageType.Info, "Job Title", "deleted");                            
                        }
                        else
                        {
                            msg = new Message(MessageConstants.E0006, MessageType.Error, "delete", "it");
                        }
                    }
                    else
                    {
                        msg = new Message(MessageConstants.E0006, MessageType.Error, "delete", "it");
                    }
                    // Submit changes to dbContext
                  
                    dbContext.SubmitChanges();
                }
            }
            catch
            {
                msg = new Message(MessageConstants.E0006, MessageType.Error, "delete", "it");
            }
            return msg;
        }

        private bool IsValidDeleteJobTitle(int id)
        {
            bool isValid = true;
            Employee objEmployee = dbContext.Employees.Where(q => q.TitleId == id && q.DeleteFlag == false).FirstOrDefault();
            JobRequest objJR = dbContext.JobRequests.Where(q => (q.PositionFrom == id || q.PositionTo == id) && q.DeleteFlag == false).FirstOrDefault();
            Candidate objCandidate = dbContext.Candidates.Where(q => q.TitleId == id && q.DeleteFlag == false).FirstOrDefault();
            JobRequestItem objJRItem = dbContext.JobRequestItems.Where(q => q.FinalTitleId == id && q.DeleteFlag == false).FirstOrDefault();
            JobTitleLevel objJobTitleLevel = dbContext.JobTitleLevels.Where(q => q.JobTitleId == id && q.DeleteFlag == false).FirstOrDefault();
            if (objEmployee != null || objJR != null || objCandidate != null || objJRItem != null)
            {
                isValid = false;
            }
            return isValid;
        }
        #endregion
    }
}