using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CRM.Library.Common;
using System.Data.Common;
using System.Text.RegularExpressions;

namespace CRM.Models
{
    public class JobTitleLevelDao : BaseDao
    {
        //
        // GET: /JobTitleLevelDao/

        public List<JobTitleLevel> GetList()
        {
            return dbContext.JobTitleLevels.Where(p => p.DeleteFlag == false).ToList<JobTitleLevel>();
        }

        public List<sp_GetJobTitleLevelResult> GetListFilter(string text, int jobTitleID)
        {
            return dbContext.sp_GetJobTitleLevel(text, jobTitleID).ToList();
        }

        public JobTitleLevel GetByName(string name)
        {
            return dbContext.JobTitleLevels.Where(c => c.DisplayName == name && c.DeleteFlag == false).FirstOrDefault<JobTitleLevel>();
        }

        public List<JobTitleLevel> GetJobTitleListByDepId(int depId)
        {
            return dbContext.JobTitleLevels.Where(p => p.JobTitle != null && p.JobTitle.DepartmentId == depId
                && p.DeleteFlag == false).ToList<JobTitleLevel>();
        }

        public JobTitleLevel GetById(int id)
        {
            return dbContext.JobTitleLevels.Where(p => p.DeleteFlag == false && p.ID ==id).FirstOrDefault<JobTitleLevel>();
        }

        /// <summary>
        /// Sort Job Title Level
        /// </summary>
        /// <param name="list"></param>
        /// <param name="sortColumn"></param>
        /// <param name="sortOrder"></param>
        /// <returns></returns>
        public List<sp_GetJobTitleLevelResult> Sort(List<sp_GetJobTitleLevelResult> list, string sortColumn, string sortOrder)
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
                         delegate(sp_GetJobTitleLevelResult m1, sp_GetJobTitleLevelResult m2)
                         { return m1.DisplayName.CompareTo(m2.DisplayName) * order; });
                    break;
                case "Department":
                    list.Sort(
                         delegate(sp_GetJobTitleLevelResult m1, sp_GetJobTitleLevelResult m2)
                         { return m1.DisplayName.CompareTo(m2.DisplayName) * order; });
                    break;
                case "IsManager":
                    list.Sort(
                         delegate(sp_GetJobTitleLevelResult m1, sp_GetJobTitleLevelResult m2)
                         { return m1.IsManager.CompareTo(m2.IsManager) * order; });
                    break;
                case "JobTitle":
                    list.Sort(
                         delegate(sp_GetJobTitleLevelResult m1, sp_GetJobTitleLevelResult m2)
                         { return m1.JobTitleName.CompareTo(m2.JobTitleName) * order; });
                    break;
            }

            return list;
        }

        /// <summary>
        /// Insert to database
        /// </summary>
        /// <param name="objUI"></param>
        /// <returns></returns>
        public Message Insert(JobTitleLevel objUI)
        {
            Message msg = null;
            try
            {
                if (objUI != null)
                {
                    // Set more info                    
                    objUI.DeleteFlag = false;
                    dbContext.JobTitleLevels.InsertOnSubmit(objUI);
                    dbContext.SubmitChanges();
                    msg = new Message(MessageConstants.I0001, MessageType.Info, "Job Title Level '" + objUI.DisplayName + "'", "added");
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
        public Message Update(JobTitleLevel objUI)
        {
            Message msg = null;
            try
            {
                if (objUI != null)
                {
                    JobTitleLevel objDB = GetById(objUI.ID);
                    if (IsValidExamUpdateDate(objUI, objDB, out msg))
                    {
                        objDB.JobTitleId = objUI.JobTitleId;
                        objDB.JobLevel = objUI.JobLevel;
                        objDB.DisplayName = objUI.DisplayName;
                        objDB.IsActive = objUI.IsActive;
                        // Set more info         
                        objDB.UpdateDate = DateTime.Now;
                        objDB.UpdatedBy = objUI.UpdatedBy;

                        dbContext.SubmitChanges();
                        msg = new Message(MessageConstants.I0001, MessageType.Info, "Job Title Level '" + objUI.DisplayName + "'", "updated");
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
        /// Check update date whether is valid
        /// </summary>
        /// <param name="objUI"></param>
        /// <param name="objDb"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        private bool IsValidExamUpdateDate(JobTitleLevel objUI, JobTitleLevel objDb, out Message msg)
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
                            msg = new Message(MessageConstants.E0025, MessageType.Error, "Job Title Level '" + objDb.DisplayName + "'");
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
                    int id = 0;
                    int totalID = idArr.Count();

                    //loop each id to delete
                    foreach (string sID in idArr)
                    {
                        //is check all records to delete 
                        bool isInterger = Int32.TryParse(sID, out id);
                        if (isInterger)
                        {
                            JobTitleLevel jobTitle = GetById(id);
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
                            else
                            {
                                totalID--;
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
                        msg = new Message(MessageConstants.I0001, MessageType.Info, totalID.ToString() + " job title level(s)", "deleted");
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
        private Message Delete(JobTitleLevel objUI)
        {
            Message msg = null;
            try
            {
                if (objUI != null)
                {
                    JobTitleLevel objDb = GetById(objUI.ID);
                    if (objDb != null)
                    {
                        if (IsValidDeleteJobTitleLevel(objDb.ID))
                        {
                            // Set delete info
                            objDb.DeleteFlag = true;
                            objDb.UpdateDate = DateTime.Now;
                            objDb.UpdatedBy = objUI.UpdatedBy;
                            dbContext.SubmitChanges();
                            msg = new Message(MessageConstants.I0001, MessageType.Info, "Job Title Level", "deleted");
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

        private bool IsValidDeleteJobTitleLevel(int id)
        {
            bool isValid = true;
            Employee objEmployee = dbContext.Employees.Where(q => q.TitleId == id && q.DeleteFlag == false).FirstOrDefault();
            JobRequest objJR = dbContext.JobRequests.Where(q => (q.PositionFrom == id || q.PositionTo == id) && q.DeleteFlag == false).FirstOrDefault();
            Candidate objCandidate = dbContext.Candidates.Where(q => q.TitleId == id && q.DeleteFlag == false).FirstOrDefault();
            JobRequestItem objJRItem = dbContext.JobRequestItems.Where(q => q.FinalTitleId == id && q.DeleteFlag == false).FirstOrDefault();
            if (objEmployee != null || objJR != null || objCandidate != null || objJRItem != null)
            {
                isValid = false;
            }
            return isValid;
        }


        public List<JobTitleLevel> GetLikeName(string titleName, bool getOne)
        {
            titleName = Regex.Replace(titleName.Trim().ToLower(), @"\b\s+\b", " ");
            string[] arrTitleName = titleName.Split(' ', '.');
            List<JobTitleLevel> titleList = dbContext.JobTitleLevels.ToList();
            foreach (string part in arrTitleName)
            {
                titleList = titleList.Where(p => p.DisplayName.ToLower().Trim().Contains(part)).ToList();
            }
            if (getOne && titleList.Count > 1)
                titleList = titleList.Where(p => p.DisplayName.Replace(". ", " ").Split(' ', '.').Length == arrTitleName.Length).ToList();
            return titleList;
        }
    }
}
