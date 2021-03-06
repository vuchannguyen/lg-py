﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CRM.Library.Common;
using System.Data.Common;
using System.Web.UI.WebControls;
using System.Data.Linq.SqlClient;
using System.Linq.Dynamic;
using CRM.Models.Entities;

namespace CRM.Models
{
    public class ServiceRequestDao : BaseDao
    {
        #region Public methods

        /// <summary>
        /// Get list service request
        /// </summary>
        /// <param name="title">title or description</param>
        /// <param name="subCateId">sub category id</param>
        /// <param name="cateId">category id</param>
        /// <param name="statusId">status id</param>
        /// <param name="assignID">assign id</param>
        /// <param name="requestor">requestor(name)</param>
        /// <returns>List<sp_SR_GetServiceRequestResult> </returns>
        public List<sp_SR_GetServiceRequestResult> GetList(string title, int? subCateId, int? cateId, int? statusId, string assignName, string requestor,
            string loginRole, string loginName)
        {
            
                return dbContext.sp_SR_GetServiceRequest(title, subCateId, cateId, 
                    statusId, assignName, requestor, loginRole, loginName,
                    Constants.SR_STATUS_TO_BE_APPROVED, Constants.SR_STATUS_APPROVED, 
                    Constants.SR_STATUS_REJECTED, Constants.SEPARATE_CC_LIST.ToString(), 
                    Constants.SEPARATE_INVOLVE_CHAR.ToString(), 
                    Constants.PORTAL_ROLE_MANAGER, 
                    Constants.PORTAL_ROLE_EMPLOYEE).OrderByDescending(p => p.ID).ToList();
        }
        /// <summary>
        /// GetListForGenearalSurveyReport
        /// </summary>
        /// <param name="assignName"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public List<GeneralSurveyReportEntity> GetListForGenearalSurveyReport(string assignName, DateTime? startDate, DateTime? endDate)
        {
            CheckSurveyDate(ref startDate, ref endDate, ref assignName);
            List<GeneralSurveyReportEntity> list = (from sr in dbContext.SR_ServiceRequests
                                                    join e in dbContext.SR_Evaluations on sr.ID equals e.SRId
                                                    where sr.StatusID == Constants.SR_STATUS_CLOSED
                                                    && (((sr.AssignUser.Equals(assignName) && !sr.AssignUser.Equals(string.Empty)) || assignName.Equals(string.Empty))
                                                    && (sr.CloseDate >= startDate && sr.CloseDate <= endDate && !string.IsNullOrEmpty(startDate.ToString()) && !string.IsNullOrEmpty(endDate.ToString()))

                                                        )

                                                    group sr by sr.AssignUser
                                                        into grp
                                                        select new GeneralSurveyReportEntity
                                                        {
                                                            administrator = grp.Key,
                                                            averageMarks = GetAverageSurveyMarks(grp.Key, startDate, endDate).ToString(),
                                                            numSR = GetClosedServiceRequests(grp.Key, startDate, endDate).ToString(),
                                                            percentOfResponse = GetPercentageOfSurvey(grp.Key, startDate, endDate).ToString()
                                                        }).ToList();
            
            return list ;
        }

        public int GetClosedServiceRequests(string userName, DateTime? startDate, DateTime? endDate)
        {
            var list = (from sr in dbContext.SR_ServiceRequests
                        where sr.StatusID == Constants.SR_STATUS_CLOSED
                                                    && (((sr.AssignUser.Equals(userName) && !sr.AssignUser.Equals(string.Empty)))
                                                    && (sr.CloseDate >= startDate && sr.CloseDate <= endDate && !string.IsNullOrEmpty(startDate.ToString()) && !string.IsNullOrEmpty(endDate.ToString()))
                                                        )
                        select sr).ToList();
            return list.Count();
        }
        public float GetAverageSurveyMarks(string userName, DateTime? startDate, DateTime? endDate)
        {
            var result = (from sr in dbContext.SR_ServiceRequests
                          join e in dbContext.SR_Evaluations on sr.ID equals e.SRId
                          where sr.StatusID == Constants.SR_STATUS_CLOSED
                                                    && (((sr.AssignUser.Equals(userName) && !sr.AssignUser.Equals(string.Empty)) || userName.Equals(string.Empty))
                                                    && (sr.CloseDate >= startDate && sr.CloseDate <= endDate && !string.IsNullOrEmpty(startDate.ToString()) && !string.IsNullOrEmpty(endDate.ToString()))
                                                        )    
                          select new { avg = e.MarkLevel}).ToList();
            int total = 0;
            foreach (var m in result)
                total += m.avg;
            return (float)total / result.Count();
        }

        public float GetPercentageOfSurvey(string userName, DateTime? startDate, DateTime? endDate)
        {
            var responsedRequestes = (from sr in dbContext.SR_ServiceRequests
                          join e in dbContext.SR_Evaluations on sr.ID equals e.SRId
                          where sr.AssignUser == userName && sr.StatusID == Constants.SR_STATUS_CLOSED &&
                          sr.CloseDate >= startDate && sr.CloseDate <= endDate
                          select new { avg = e.MarkLevel }).ToList();
            int requests = GetClosedServiceRequests(userName, startDate, endDate);
            if (responsedRequestes.Count() > 0)
                return (float)(responsedRequestes.Count() * 100)/requests;
            return 0;
        }

        public void CheckSurveyDate(ref DateTime? startDate, ref DateTime? endDate, ref string assignName)
        {
            if (string.IsNullOrEmpty(startDate.ToString()))
                startDate = ConvertUtil.ConvertToDatetime("1/1/1753 12:00:00 AM");
            if (string.IsNullOrEmpty(endDate.ToString()))
                endDate = DateTime.MaxValue;
            if (assignName.Trim().Equals(Constants.USER_ADMIN_DEFAULT_LABEL))
            {
                assignName = string.Empty;
            }
        }
        /// <summary>
        /// GetListForDetailSurveyReport
        /// </summary>
        /// <param name="assignName"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public List<DetailSurveyReportEntity> GetListForDetailSurveyReport(string assignName, DateTime? startDate, DateTime? endDate, int surveyId)
        {
            string[] aMarks = GetSurveyMarks(surveyId);
            CheckSurveyDate(ref startDate, ref endDate, ref assignName);
            var list = (from sr in dbContext.SR_ServiceRequests.ToList()
                        join e in dbContext.SR_Evaluations.ToList() on sr.ID equals e.SRId
                        where sr.StatusID== Constants.SR_STATUS_CLOSED
                                                     && (((sr.AssignUser.Equals(assignName) && !sr.AssignUser.Equals(string.Empty)) || assignName.Equals(string.Empty))
                                                     && (sr.CloseDate >= startDate && sr.CloseDate <= endDate && !string.IsNullOrEmpty(startDate.ToString()) && !string.IsNullOrEmpty(endDate.ToString()))
                                                         )
                        select new DetailSurveyReportEntity
                        {
                            administrator = sr.AssignUser,
                            srId = sr.ID.ToString(),
                            answer = aMarks[e.MarkLevel - 1],//(e.MarkLevel - 1).ToString(),
                            comment = e.Comment
                        }).ToList();                                   
            return list;
        }
        /// <summary>
        /// GetSurveyMarks
        /// </summary>
        /// <param name="surveyId"></param>
        /// <returns></returns>
        public string[] GetSurveyMarks(int surveyId)
        {
            string marks = (from s in dbContext.SR_Surveys
                            where s.ID == surveyId
                            select s.Marks).FirstOrDefault().ToString();
            string[] aMarks = marks.Trim().Split(Constants.SEPARATE_MARKS_CHAR);
            return aMarks;
        }
        /// <summary>
        /// SortDetailSurveyReport
        /// </summary>
        /// <param name="list"></param>
        /// <param name="sortColumn"></param>
        /// <param name="sortOrder"></param>
        /// <returns></returns>
        public List<DetailSurveyReportEntity> SortDetailSurveyReport(List<DetailSurveyReportEntity> list, string sortColumn, string sortOrder)
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
                case "administrator":
                    list.Sort(
                         delegate(DetailSurveyReportEntity m1, DetailSurveyReportEntity m2)
                         { return m1.administrator.CompareTo(m2.administrator) * order; });
                    break;
                case "srId":
                    list.Sort(
                         delegate(DetailSurveyReportEntity m1, DetailSurveyReportEntity m2)
                         //{ return m1.administrator.CompareTo(m2.administrator) * order; });
                         { return m1.srId.CompareTo(m2.srId) * order; });
                    break;
                case "answer":
                    list.Sort(
                         delegate(DetailSurveyReportEntity m1, DetailSurveyReportEntity m2)
                         { return m1.answer.CompareTo(m2.answer) * order; });
                    break;
                case "comment":
                    list.Sort(
                         delegate(DetailSurveyReportEntity m1, DetailSurveyReportEntity m2)
                         { return m1.comment.CompareTo(m2.comment) * order; });
                    break;

            }

            return list;
        }
        /// <summary>
        ///  SortGeneralSurveyReport
        /// </summary>
        /// <param name="list"></param>
        /// <param name="sortColumn"></param>
        /// <param name="sortOrder"></param>
        /// <returns></returns>
        public List<GeneralSurveyReportEntity> SortGeneralSurveyReport(List<GeneralSurveyReportEntity> list, string sortColumn, string sortOrder)
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
                case "administrator":
                    list.Sort(
                         delegate(GeneralSurveyReportEntity m1, GeneralSurveyReportEntity m2)
                         { return m1.administrator.CompareTo(m2.administrator) * order; });
                    break;
                case "numSR":
                    list.Sort(
                         delegate(GeneralSurveyReportEntity m1, GeneralSurveyReportEntity m2)
                         { return float.Parse(m1.numSR).CompareTo(float.Parse(m2.numSR)) * order; });
                    break;
                case "averageMarks":
                    list.Sort(
                         delegate(GeneralSurveyReportEntity m1, GeneralSurveyReportEntity m2)
                         { return float.Parse(m1.averageMarks).CompareTo(float.Parse(m2.averageMarks)) * order; });
                    break;
                case "percentOfResponse":
                    list.Sort(
                         delegate(GeneralSurveyReportEntity m1, GeneralSurveyReportEntity m2)
                         { return float.Parse(m1.percentOfResponse).CompareTo(float.Parse(m2.percentOfResponse)) * order; });
                    break;
             
            }

            return list;
        }
        /// <summary>
        /// Get list assign to
        /// </summary>
        /// <returns>List<ListItem></returns>
        public List<ListItem> GetListAssign()
        {
            var list = dbContext.SR_ServiceRequests.Where(p => p.DeleteFlag == false).OrderBy(p => p.AssignUser).Select(p => p.AssignUser).Distinct().ToList();
            List<ListItem> result = new List<ListItem>();
            foreach (var item in list)
            {
                result.Add(new ListItem(
                    item,
                    item
                    ));
            }
            return result;
        }

        /// <summary>
        /// Get list assign to
        /// </summary>
        /// <returns>List<ListItem></returns>
        public List<ListItem> GetListRequestor()
        {
            var list = dbContext.SR_ServiceRequests.Where(p => p.DeleteFlag == false).OrderBy(p => p.RequestUser).Select(p => p.RequestUser).Distinct().ToList();
            List<ListItem> result = new List<ListItem>();
            foreach (var item in list)
            {
                result.Add(new ListItem(
                    item,
                    item
                    ));
            }
            return result;
        }



        #region "Get List service request for admin - LINQ"
        public IQueryable<SR_ServiceRequest> GetQueryList4Admin(string title, int? subCateId, int? cateId, int? statusId, string assignName,
                                                                DateTime? startdate, DateTime? enddate, string requestor)
        {
            var sql = from service in dbContext.SR_ServiceRequests
                      where service.DeleteFlag == false
                      select service;

            if (!string.IsNullOrEmpty(title))
            {
                title = CommonFunc.GetFilterText(title);
                sql = sql.Where(p => SqlMethods.Like(p.Title, title)
                                  || SqlMethods.Like(p.ID.ToString(), title));
            }

            if (subCateId != null && subCateId != 0)
                sql = sql.Where(p => p.CategoryID == subCateId);

            if (cateId != null && cateId != 0)
                sql = sql.Where(p => dbContext.GetCategoryIdBySubId(p.CategoryID) == cateId);

            if (statusId != null && statusId != 0)
                sql = sql.Where(p => p.StatusID == statusId);

            if (!string.IsNullOrEmpty(assignName))
                sql = sql.Where(p => p.AssignUser == assignName);

            if (!string.IsNullOrEmpty(requestor))
                sql = sql.Where(p => p.RequestUser == requestor);

            if (!(startdate == null && enddate == null))
            {
                if (enddate != null)
                    sql = sql.Where(p => p.CreateDate.Date <= enddate);
                if (startdate != null)
                    sql = sql.Where(p => p.CreateDate.Date >= startdate);
            }

            return sql;
        }

        public int GetCountList4Admin(string title, int? subCateId, int? cateId, int? statusId, string assignName,
                                                                DateTime? startdate, DateTime? enddate, string requestor)
        {
            return GetQueryList4Admin( title, subCateId, cateId, statusId, assignName, startdate, enddate, requestor).Count();
        }

        /// <summary>
        /// Get list service request for admin
        /// </summary>
        /// <param name="sortColumn">sortColumn</param>
        /// <param name="sortOrder">sortOrder</param>
        /// <param name="skip">skip</param>
        /// <param name="take">take</param>
        /// <param name="title">title or description</param>
        /// <param name="subCateId">sub category id</param>
        /// <param name="cateId">category id</param>
        /// <param name="statusId">status id</param>
        /// <param name="assignName">assign id</param>
        /// <param name="startdate">startdate</param>
        /// <param name="enddate">enddate</param>
        /// <param name="requestor">requestor(name)</param>
        /// <returns></returns>
        public List<SR_ServiceRequest> GetList4Admin(string sortColumn, string sortOrder, int skip, int take,
                                                    string title, int? subCateId, int? cateId, int? statusId, string assignName,
                                                    DateTime? startdate, DateTime? enddate, string requestor)
        {
            var sql = GetQueryList4Admin(title, subCateId, cateId, statusId, assignName, startdate, enddate, requestor);

            switch (sortColumn)
            {
                case "Code":
                    sql = sql.OrderBy("ID" + " " + sortOrder);
                    break;
                case "Title":
                    sql = sql.OrderBy("Title" + " " + sortOrder);
                    break;
                case "Description":
                    sql = sql.OrderBy("Description" + " " + sortOrder);
                    break;
                case "SubCategory":
                    sql = sql.OrderBy("SR_Category.Name" + " " + sortOrder);
                    break;
                case "Category" :
                    sql = sql.OrderBy("SR_Category.SR_Category1.Name" + " " + sortOrder);
                    break;
                case "Requestor":
                    sql = sql.OrderBy("RequestUser" + " " + sortOrder);
                    break;
                case "RequestDate":
                    sql = sql.OrderBy("CreateDate" + " " + sortOrder);
                    break;
                case "AssignName":
                    sql = sql.OrderBy("AssignUser" + " " + sortOrder);
                    break;
                case "Status":
                    sql = sql.OrderBy("SR_Status.Name" + " " + sortOrder);
                    break;
                default:
                    sql = sql.OrderBy("ID desc");
                    break;
            }

            if (skip == 0 && take == 0)
                return sql.ToList();
            else
                return sql.Skip(skip).Take(take).ToList();

        }

        #endregion

        /// <summary>
        /// Sort Emplooyee
        /// </summary>
        /// <param name="list">sp_SR_GetServiceRequestResult list </param>
        /// <param name="sortColumn">column sort</param>
        /// <param name="sortOrder">order sort</param>
        /// <returns>List<sp_SR_GetServiceRequestResult></returns>
        public List<sp_SR_GetServiceRequestResult> Sort(List<sp_SR_GetServiceRequestResult> list, string sortColumn, string sortOrder)
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
                         delegate(sp_SR_GetServiceRequestResult m1, sp_SR_GetServiceRequestResult m2)
                         { return m1.ID.CompareTo(m2.ID) * order; });
                    break;
                case "Title":
                    list.Sort(
                         delegate(sp_SR_GetServiceRequestResult m1, sp_SR_GetServiceRequestResult m2)
                         { return m1.Title.CompareTo(m2.Title) * order; });
                    break;
                case "Description":
                    list.Sort(
                         delegate(sp_SR_GetServiceRequestResult m1, sp_SR_GetServiceRequestResult m2)
                         { return m1.Description.CompareTo(m2.Description) * order; });
                    break;
                case "Category":
                    list.Sort(
                         delegate(sp_SR_GetServiceRequestResult m1, sp_SR_GetServiceRequestResult m2)
                         { return m1.Category.CompareTo(m2.Category) * order; });
                    break;
                case "SubCategory":
                    list.Sort(
                         delegate(sp_SR_GetServiceRequestResult m1, sp_SR_GetServiceRequestResult m2)
                         { return m1.SubCategory.CompareTo(m2.SubCategory) * order; });
                    break;
                case "RequestDate":
                    list.Sort(
                         delegate(sp_SR_GetServiceRequestResult m1, sp_SR_GetServiceRequestResult m2)
                         { return m1.RequestDate.CompareTo(m2.RequestDate) * order; });
                    break;
                case "AssignName":
                    list.Sort(
                         delegate(sp_SR_GetServiceRequestResult m1, sp_SR_GetServiceRequestResult m2)
                         { return m1.AssginName.CompareTo(m2.AssginName) * order; });
                    break;
                case "Status":
                    list.Sort(
                         delegate(sp_SR_GetServiceRequestResult m1, sp_SR_GetServiceRequestResult m2)
                         { return m1.Status.CompareTo(m2.Status) * order; });
                    break;
            }

            return list;
        }


        /// <summary>
        /// Get urgenct list
        /// </summary>
        /// <returns>List<SR_Urgency></returns>
        public List<SR_Urgency> GetUrgencyList()
        {
            return dbContext.SR_Urgencies.ToList();
        }


        /// <summary>
        /// Delete List
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
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
                        SR_ServiceRequest emp = GetById(int.Parse(empID));
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
                    msg = new Message(MessageConstants.I0001, MessageType.Info, total.ToString() + " Service request(s)", "deleted");
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

        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="objUI"></param>
        private void Delete(SR_ServiceRequest objUI)
        {
            if (objUI != null)
            {
                // Get current group in dbContext
                SR_ServiceRequest objDb = GetById(objUI.ID);
                if (objDb != null)
                {
                    // Set delete info                    
                    objDb.DeleteFlag = true;
                    objDb.UpdateDate = DateTime.Now;
                    objDb.UpdatedBy = objUI.UpdatedBy;
                    // Submit changes to dbContext

                    new ServiceRequestLogDao().WriteLogForSR(null, objUI, ELogAction.Delete);
                    dbContext.SubmitChanges();

                }
            }
        }

        /// <summary>
        /// Get report status list
        /// </summary>
        /// <param name="startDate">request date from</param>
        /// <param name="endDate">request date to</param>
        /// <returns>List<sp_SR_GetOpenCloseRequestResult></returns>
        public List<sp_SR_GetOpenCloseRequestResult> GetListReportStatus(DateTime? startDate, DateTime? endDate)
        {
            return dbContext.sp_SR_GetOpenCloseRequest(startDate, endDate).ToList();
        }

        /// <summary>
        /// Get list open close report 
        /// </summary>
        /// <param name="startDate">from date</param>
        /// <param name="endDate">to date</param>
        /// <param name="categoryId">category id</param>
        /// <returns>List<sp_SR_GetListOpenCloseRequestResult></returns>
        public List<sp_SR_GetListOpenCloseRequestResult> GetListReportOpenClose(DateTime? startDate, DateTime? endDate, int categoryId, int type)
        {
            return dbContext.sp_SR_GetListOpenCloseRequest(startDate, endDate, categoryId, type).ToList();
        }

        /// <summary>
        /// Sort report all status
        /// </summary>
        /// <param name="list">List<sp_SR_GetOpenCloseRequestResult></param>
        /// <param name="sortColumn">column sort</param>
        /// <param name="sortOrder">order</param>
        /// <returns>List<sp_SR_GetOpenCloseRequestResult></returns>
        public List<sp_SR_GetOpenCloseRequestResult> SortReportStatus(List<sp_SR_GetOpenCloseRequestResult> list, string sortColumn, string sortOrder)
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
                case "Category":
                    list.Sort(
                         delegate(sp_SR_GetOpenCloseRequestResult m1, sp_SR_GetOpenCloseRequestResult m2)
                         { return m1.Name.CompareTo(m2.Name) * order; });
                    break;
                case "Open":
                    list.Sort(
                         delegate(sp_SR_GetOpenCloseRequestResult m1, sp_SR_GetOpenCloseRequestResult m2)
                         { return m1.TotalOpened.Value.CompareTo(m2.TotalOpened.Value) * order; });
                    break;
                case "Close":
                    list.Sort(
                         delegate(sp_SR_GetOpenCloseRequestResult m1, sp_SR_GetOpenCloseRequestResult m2)
                         { return m1.TotalClosed.Value.CompareTo(m2.TotalClosed.Value) * order; });
                    break;
                case "Total":
                    list.Sort(
                         delegate(sp_SR_GetOpenCloseRequestResult m1, sp_SR_GetOpenCloseRequestResult m2)
                         { return m1.Total.Value.CompareTo(m2.Total.Value) * order; });
                    break;
                
            }

            return list;
        }

        /// <summary>
        /// Get list activity report
        /// </summary>
        /// <param name="startDate">request date from </param>
        /// <param name="endDate">request date to</param>
        /// <returns>List<sp_SR_GetSRActivityResult></returns>
        public List<sp_SR_GetSRActivityResult> GetListReportActivity(DateTime? startDate, DateTime? endDate)
        {
            return dbContext.sp_SR_GetSRActivity(startDate, endDate).ToList();
        }

        /// <summary>
        /// Sort activity report
        /// </summary>
        /// <param name="list">List<sp_SR_GetSRActivityResult></param>
        /// <param name="sortColumn">column sort</param>
        /// <param name="sortOrder">order</param>
        /// <returns>List<sp_SR_GetSRActivityResult></returns>
        public List<sp_SR_GetSRActivityResult> SortReportActivity(List<sp_SR_GetSRActivityResult> list, string sortColumn, string sortOrder)
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
                case "Department":
                    list.Sort(
                         delegate(sp_SR_GetSRActivityResult m1, sp_SR_GetSRActivityResult m2)
                         { return m1.SubDepartment.CompareTo(m2.SubDepartment) * order; });
                    break;
                case "SR_Count":
                    list.Sort(
                         delegate(sp_SR_GetSRActivityResult m1, sp_SR_GetSRActivityResult m2)
                         { return m1.SR_Count.Value.CompareTo(m2.SR_Count.Value) * order; });
                    break;
                case "TotalTime":
                    list.Sort(
                         delegate(sp_SR_GetSRActivityResult m1, sp_SR_GetSRActivityResult m2)
                         { return m1.TotalTime.Value.CompareTo(m2.TotalTime.Value) * order; });
                    break;

            }

            return list;
        }

        /// <summary>
        /// Get list ITeam report
        /// </summary>
        /// <param name="startDate">request date from</param>
        /// <param name="endDate">request date to</param>
        /// <returns>List<sp_SR_GetReportITTeamResult></returns>
        public List<sp_SR_GetReportITTeamResult> GetListReportITTeam(DateTime? startDate, DateTime? endDate)
        {
            return dbContext.sp_SR_GetReportITTeam(startDate, endDate).ToList();
        }

        /// <summary>
        /// Sort ITeam report
        /// </summary>
        /// <param name="list">List<sp_SR_GetReportITTeamResult></param>
        /// <param name="sortColumn">column sort</param>
        /// <param name="sortOrder">order</param>
        /// <returns>List<sp_SR_GetReportITTeamResult></returns>
        public List<sp_SR_GetReportITTeamResult> SortReportITTeam(List<sp_SR_GetReportITTeamResult> list, string sortColumn, string sortOrder)
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
                case "DisplayName":
                    list.Sort(
                         delegate(sp_SR_GetReportITTeamResult m1, sp_SR_GetReportITTeamResult m2)
                         { return m1.DisplayName.CompareTo(m2.DisplayName) * order; });
                    break;
                case "AmountTime":
                    list.Sort(
                         delegate(sp_SR_GetReportITTeamResult m1, sp_SR_GetReportITTeamResult m2)
                         { return m1.TotalTime.Value.CompareTo(m2.TotalTime.Value) * order; });
                    break;
                case "TotalOpen":
                    list.Sort(
                         delegate(sp_SR_GetReportITTeamResult m1, sp_SR_GetReportITTeamResult m2)
                         { return m1.TotalOpened.Value.CompareTo(m2.TotalOpened.Value) * order; });
                    break;
                case "TotalClose":
                    list.Sort(
                         delegate(sp_SR_GetReportITTeamResult m1, sp_SR_GetReportITTeamResult m2)
                         { return m1.TotalClosed.Value.CompareTo(m2.TotalClosed.Value) * order; });
                    break;
                case "TotalDoing":
                        list.Sort(
                         delegate(sp_SR_GetReportITTeamResult m1, sp_SR_GetReportITTeamResult m2)
                         { return m1.TotalDoing.Value.CompareTo(m2.TotalDoing) * order; });
                    break;
            }

            return list;
        }

        /// <summary>
        /// Get list active report
        /// </summary>
        /// <param name="startDate">request date from </param>
        /// <param name="endDate">request date to</param>
        /// <returns>List<sp_SR_GetReportAllStatusResult></returns>
        public List<sp_SR_GetReportAllStatusResult> GetListReportActive(DateTime? startDate, DateTime? endDate)
        {
            return dbContext.sp_SR_GetReportAllStatus(startDate, endDate).ToList();
        }

        /// <summary>
        /// Sort active report 
        /// </summary>
        /// <param name="list">List<sp_SR_GetReportAllStatusResult></param>
        /// <param name="sortColumn">column sort</param>
        /// <param name="sortOrder">order</param>
        /// <returns>List<sp_SR_GetReportAllStatusResult></returns>
        public List<sp_SR_GetReportAllStatusResult> SortReportActive(List<sp_SR_GetReportAllStatusResult> list, string sortColumn, string sortOrder)
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
                case "DisplayName":
                    list.Sort(
                         delegate(sp_SR_GetReportAllStatusResult m1, sp_SR_GetReportAllStatusResult m2)
                         { return m1.DisplayName.CompareTo(m2.DisplayName) * order; });
                    break;
                case "Active":
                    list.Sort(
                         delegate(sp_SR_GetReportAllStatusResult m1, sp_SR_GetReportAllStatusResult m2)
                         { return m1.TotalActive.Value.CompareTo(m2.TotalActive.Value) * order; });
                    break;
                case "New":
                    list.Sort(
                         delegate(sp_SR_GetReportAllStatusResult m1, sp_SR_GetReportAllStatusResult m2)
                         { return m1.TotalNew.Value.CompareTo(m2.TotalNew.Value) * order; });
                    break;
                case "Open":
                    list.Sort(
                         delegate(sp_SR_GetReportAllStatusResult m1, sp_SR_GetReportAllStatusResult m2)
                         { return m1.TotalOpen.Value.CompareTo(m2.TotalOpen.Value) * order; });
                    break;
                case "ToBeApproved":
                    list.Sort(
                     delegate(sp_SR_GetReportAllStatusResult m1, sp_SR_GetReportAllStatusResult m2)
                     { return m1.TotalToBeApprove.Value.CompareTo(m2.TotalToBeApprove.Value) * order; });
                    break;
                case "Closed":
                    list.Sort(
                     delegate(sp_SR_GetReportAllStatusResult m1, sp_SR_GetReportAllStatusResult m2)
                     { return m1.TotalClosed.Value.CompareTo(m2.TotalClosed.Value) * order; });
                    break;
                case "VerifiedClose":
                    list.Sort(
                     delegate(sp_SR_GetReportAllStatusResult m1, sp_SR_GetReportAllStatusResult m2)
                     { return m1.TotalVerifiedClosed.Value.CompareTo(m2.TotalVerifiedClosed.Value) * order; });
                    break;
                case "Pending":
                    list.Sort(
                     delegate(sp_SR_GetReportAllStatusResult m1, sp_SR_GetReportAllStatusResult m2)
                     { return m1.TotalPending.Value.CompareTo(m2.TotalPending.Value) * order; });
                    break;
                case "Postponed":
                    list.Sort(
                     delegate(sp_SR_GetReportAllStatusResult m1, sp_SR_GetReportAllStatusResult m2)
                     { return m1.TotalPostponed.Value.CompareTo(m2.TotalPostponed.Value) * order; });
                    break;
                case "Approved":
                    list.Sort(
                     delegate(sp_SR_GetReportAllStatusResult m1, sp_SR_GetReportAllStatusResult m2)
                     { return m1.TotalApproved.Value.CompareTo(m2.TotalApproved.Value) * order; });
                    break;
                case "Rejected":
                    list.Sort(
                     delegate(sp_SR_GetReportAllStatusResult m1, sp_SR_GetReportAllStatusResult m2)
                     { return m1.TotalRejected.Value.CompareTo(m2.TotalRejected.Value) * order; });
                    break;
            }

            return list;
        }

        /// <summary>
        /// Get list request closed
        /// </summary>
        /// <param name="startDate">request date from</param>
        /// <param name="endDate">request date to</param>
        /// <returns>List<sp_SR_GetRequestClosedResult></returns>
        public List<sp_SR_GetRequestClosedResult> GetListRequestClosed(DateTime? startDate, DateTime? endDate)
        {
            return dbContext.sp_SR_GetRequestClosed(startDate, endDate,Constants.SR_STATUS_CLOSED).ToList();
        }

        /// <summary>
        /// Get list seperate IT team
        /// </summary>
        /// <param name="list">List<sp_SR_GetRequestClosedResult></param>
        /// <returns>string[]</returns>
        private string[] GetListEmpIT(List<sp_SR_GetRequestClosedResult> list)
        {
            return list.Select(p => p.AssignUser).Distinct().ToArray<string>();
        }

        /// <summary>
        /// Get service requests of IT staff
        /// </summary>
        /// <param name="list">List<sp_SR_GetRequestClosedResult></param>
        /// <param name="user">string</param>
        /// <returns>string[]</returns>
        private string[] GetServiceRequestIDOfEmp(List<sp_SR_GetRequestClosedResult> list, string user)
        {
            return list.Where(p => p.AssignUser == user).OrderBy(p=> p.ID).Select(p => p.ID.ToString()).ToArray<string>();
        }

        /// <summary>
        /// Get list status closed of it staff
        /// </summary>
        /// <param name="startDate">request date from</param>
        /// <param name="endDate">request date to</param>
        /// <param name="max">max amount service request id</param>
        /// <returns>List<RequestClosed></returns>
        public List<RequestClosed> GetListEmpClosed(DateTime? startDate, DateTime? endDate, ref int max)
        {
            List<sp_SR_GetRequestClosedResult> list = GetListRequestClosed(startDate, endDate);
            string[] listEmp = GetListEmpIT(list);
            List<RequestClosed> result = new List<RequestClosed>();
            if (list != null && list.Count > 0)
            {
                foreach (string emp in listEmp)
                {                    
                    RequestClosed sr = new RequestClosed();
                    sr.emp_name = emp;
                    sr.arrID = GetServiceRequestIDOfEmp(list, emp);
                    result.Add(sr);
                }

                max = result.Max(p => p.arrID.Count());
            }

            return result;
        }

        #endregion

        public bool HasViewPermision(int srID, string loginName, string loginRole)
        {
            try
            {
                SR_ServiceRequest sr = GetById(srID);
                if (!string.IsNullOrEmpty(loginName) && !string.IsNullOrEmpty(loginRole) && sr != null)
                {
                    Employee employee = new EmployeeDao().GetByOfficeEmailInActiveList(loginName + Constants.PREFIX_EMAIL_LOGIGEAR);
                    if (employee == null)
                        return false;
                    return IsRequestor(sr, loginName, loginRole) || IsAssigned(sr, loginName, loginRole) ||
                        IsInCCList(sr, loginName) || IsInInvolveList(sr, loginName, loginRole);
                }
                return false;
            }
            catch
            {
                return false;
            }
        }

        private bool IsRequestor(SR_ServiceRequest pr, string loginName, string loginRole)
        {
            if (loginRole == Constants.PORTAL_ROLE_EMPLOYEE && (pr.RequestUser == loginName || pr.SubmitUser == loginName))
                return true;
            return false;
        }

        public bool IsAssigned(SR_ServiceRequest sr, string loginName, string loginRole)
        {
            string assignRole = string.Empty;
            if (sr.StatusID == Constants.SR_STATUS_TO_BE_APPROVED)
            {
                assignRole = Constants.PORTAL_ROLE_MANAGER;
            }
            else
            {
                assignRole = Constants.PORTAL_ROLE_EMPLOYEE;
            }
            if (sr.AssignUser == loginName && assignRole == loginRole)
                return true;
            return false;
        }

        private bool IsInInvolveList(SR_ServiceRequest sr, string loginName, string loginRole)
        {
            int? result = dbContext.check_in_list_invoice_SR(sr.InvolveUser, loginName, sr.InvolveStatus, Constants.SR_STATUS_APPROVED.ToString() ,Constants.SR_STATUS_REJECTED.ToString(), loginRole,
                 Constants.PORTAL_ROLE_MANAGER, Constants.PORTAL_ROLE_EMPLOYEE, Constants.SR_FILE_SEPARATE_SIGN);
            return result.HasValue ? result.Value == 1 ? true : false : false;
        }

        private bool IsInCCList(SR_ServiceRequest sr, string loginName)
        {
            int? result = dbContext.check_in_list_string(sr.CCList, loginName, Constants.SEPARATE_USER_ADMIN_USERNAME);
            return result.HasValue ? result.Value == 1 ? true : false : false;
        }

        public Message Insert(SR_ServiceRequest sr)
        {
            try
            {
                sr.CreateDate = sr.UpdateDate = DateTime.Now;
                sr.CreatedBy = sr.UpdatedBy = HttpContext.Current.User.Identity.Name;
                if (sr.StatusID == 0)
                    sr.StatusID = Constants.SR_STATUS_NEW;
                else if (sr.StatusID == Constants.SR_STATUS_CLOSED)
                    sr.CloseDate = DateTime.Now;
                sr.InvolveDate = DateTime.Now + Constants.SEPARATE_INVOLVE_SIGN;
                sr.InvolveUser = sr.SubmitUser + Constants.SEPARATE_INVOLVE_SIGN;
                sr.InvolveStatus = sr.StatusID + Constants.SEPARATE_INVOLVE_SIGN;
                dbContext.SR_ServiceRequests.InsertOnSubmit(sr);
                dbContext.SubmitChanges();
                new ServiceRequestLogDao().WriteLogForSR(null, sr, ELogAction.Insert);
                return new Message(MessageConstants.I0001, MessageType.Info, "Service request " + 
                    Constants.SR_SERVICE_REQUEST_PREFIX + sr.ID, "submitted");
            }
            catch
            {
                return new Message(MessageConstants.E0007, MessageType.Error);   
            }
        }

        public Message InsertFromPortal(SR_ServiceRequest sr)
        {
            try
            {
                sr.CreateDate = sr.UpdateDate = DateTime.Now;
                sr.CreatedBy = sr.UpdatedBy = HttpContext.Current.User.Identity.Name;
                if (sr.StatusID == 0)
                    sr.StatusID = Constants.SR_STATUS_NEW;
                else if (sr.StatusID == Constants.SR_STATUS_CLOSED)
                    sr.CloseDate = DateTime.Now;
                sr.InvolveDate = DateTime.Now + Constants.SEPARATE_INVOLVE_SIGN;
                //sr.InvolveUser = sr.SubmitUser + Constants.SEPARATE_INVOLVE_SIGN;
                sr.InvolveStatus = sr.StatusID + Constants.SEPARATE_INVOLVE_SIGN;
                dbContext.SR_ServiceRequests.InsertOnSubmit(sr);
                dbContext.SubmitChanges();
                new ServiceRequestLogDao().WriteLogForSR(null, sr, ELogAction.Insert);
                return new Message(MessageConstants.I0001, MessageType.Info, "Service request " +
                    Constants.SR_SERVICE_REQUEST_PREFIX + sr.ID, "submitted");
            }
            catch
            {
                return new Message(MessageConstants.E0007, MessageType.Error);
            }
        }

        public List<SR_Activity> GetListActivityBySrID(int srID)
        {
            return dbContext.SR_Activities.Where(q => q.ServiceRequestID == srID && !q.DeleteFlag).ToList();
        }

        public SR_ServiceRequest GetById(int id)
        {
            return dbContext.SR_ServiceRequests.Where(p => p.ID == id && !p.DeleteFlag).FirstOrDefault();
        }

        public Message Update(SR_ServiceRequest sr)
        {
            try
            {
                sr.UpdateDate = DateTime.Now;
                sr.UpdatedBy = HttpContext.Current.User.Identity.Name;
                SR_ServiceRequest objDb = GetById(sr.ID);
                new ServiceRequestLogDao().WriteLogForSR(objDb, sr, ELogAction.Update);
                objDb.UpdateDate = sr.UpdateDate;
                objDb.UpdatedBy = sr.UpdatedBy;
                objDb.CategoryID = sr.CategoryID;
                objDb.CCList = sr.CCList;
                objDb.Description = sr.Description;
                objDb.Files = sr.Files;
                objDb.ParentID = sr.ParentID;
                objDb.RequestUser = sr.RequestUser;
                objDb.Title = sr.Title;
                objDb.UrgencyID = sr.UrgencyID;
                objDb.DueDate = sr.DueDate;
                objDb.SR_Comments.AddRange(sr.SR_Comments);
                bool writeHistory = false;//write history if AssignUser or Status has been changed.
                if (!string.IsNullOrEmpty(sr.AssignUser))
                {
                    objDb.AssignUser = sr.AssignUser;
                    writeHistory = true;
                }
                if (sr.StatusID != 0)
                {
                    objDb.StatusID = sr.StatusID;
                    if (sr.StatusID == Constants.SR_STATUS_CLOSED)
                        objDb.CloseDate = DateTime.Now;
                    writeHistory = true;
                }
                if (writeHistory)
                {
                    objDb.InvolveDate += DateTime.Now + Constants.SEPARATE_INVOLVE_SIGN;
                    objDb.InvolveUser += HttpContext.Current.User.Identity.Name + Constants.SEPARATE_INVOLVE_SIGN;
                    objDb.InvolveStatus += objDb.StatusID + Constants.SEPARATE_INVOLVE_SIGN;
                }
                
                dbContext.SubmitChanges();
                return new Message(MessageConstants.I0001, MessageType.Info, "Service request " + 
                    Constants.SR_SERVICE_REQUEST_PREFIX + sr.ID, "updated");
            }
            catch
            {
                return new Message(MessageConstants.E0007, MessageType.Error);
            }
        }
        public string GetLastInvolveUser(SR_ServiceRequest sr)
        {
            if (string.IsNullOrEmpty(sr.InvolveUser))
                return "";
            return sr.InvolveUser.Split(new char[]{Constants.SEPARATE_INVOLVE_CHAR}, 
                StringSplitOptions.RemoveEmptyEntries).Last();
        }
        public Message ChangeStatus(int id, SR_Comment comment, int statusId)
        {
            Message msg = null;
            try
            {
                SR_ServiceRequest sr = GetById(id);
                if (statusId == Constants.SR_STATUS_CLOSED)
                    sr.CloseDate = DateTime.Now;
                else
                    sr.AssignUser = GetLastInvolveUser(sr);
                sr.StatusID = statusId;
                sr.InvolveDate += DateTime.Now + Constants.SEPARATE_INVOLVE_SIGN;
                sr.InvolveUser += HttpContext.Current.User.Identity.Name + Constants.SEPARATE_INVOLVE_SIGN;
                sr.InvolveStatus += statusId + Constants.SEPARATE_INVOLVE_SIGN;
                sr.UpdateDate = DateTime.Now;
                sr.UpdatedBy = HttpContext.Current.User.Identity.Name;
                dbContext.SR_Comments.InsertOnSubmit(comment);
                dbContext.SubmitChanges();
                string action = "";
                if (statusId == Constants.SR_STATUS_APPROVED)
                    action = "approved";
                else if (statusId == Constants.SR_STATUS_REJECTED)
                    action = "rejected";
                else if (statusId == Constants.SR_STATUS_CLOSED)
                    action = "closed";
                return new Message(MessageConstants.I0001, MessageType.Info, "Service request " +
                    Constants.SR_SERVICE_REQUEST_PREFIX + sr.ID, action);
            }
            catch
            {
                msg = new Message(MessageConstants.E0007, MessageType.Error);
            }
            return msg;
        }

        public Message GetApproval(int srId, string managerName, SR_Comment comment)
        {
            Message msg = null;
            DbTransaction trans = null;
            try
            {
                dbContext.Connection.Open();
                trans = dbContext.Connection.BeginTransaction();
                dbContext.Transaction = trans;
                SR_ServiceRequest sr = GetById(srId);
                sr.AssignUser = managerName;
                sr.StatusID = Constants.SR_STATUS_TO_BE_APPROVED;
                sr.InvolveDate += DateTime.Now + Constants.SEPARATE_INVOLVE_SIGN;
                sr.InvolveUser += HttpContext.Current.User.Identity.Name + Constants.SEPARATE_INVOLVE_SIGN;
                sr.InvolveStatus += sr.StatusID + Constants.SEPARATE_INVOLVE_SIGN;
                sr.UpdateDate = DateTime.Now;
                sr.UpdatedBy = HttpContext.Current.User.Identity.Name;
                dbContext.SR_Comments.InsertOnSubmit(comment);
                msg = new Message(MessageConstants.I0001, MessageType.Info, "Service request " +
                    Constants.SR_SERVICE_REQUEST_PREFIX + sr.ID, "updated");
                dbContext.SubmitChanges();
                trans.Commit();
            }
            catch
            {
                trans.Rollback();
                msg = new Message(MessageConstants.E0007, MessageType.Error);
            }
            return msg;
        }

        public Message UpdateSolution(int srId, string solution)
        {
            Message msg = null;
            try
            {
                SR_ServiceRequest sr = GetById(srId);
                sr.Solution = solution;
                sr.SolutionLastModified = string.Format(Constants.SR_SOLUTION_STRING_FORMAT, 
                    HttpContext.Current.User.Identity.Name, DateTime.Now.ToString("dd-MMM-yyyy hh:mm tt"));
                msg = new Message(MessageConstants.I0001, MessageType.Info, "Solution", "updated");
                dbContext.SubmitChanges();
            }
            catch
            {
                msg = new Message(MessageConstants.E0007, MessageType.Error);
            }
            return msg;
        }

        public List<SR_ServiceRequest> GetUndoneListByDate(DateTime dateTime)
        {
            return dbContext.SR_ServiceRequests.Where(p=> !p.DeleteFlag && p.StatusID != Constants.SR_STATUS_CLOSED
                && p.DueDate.HasValue && p.DueDate.Value < dateTime).OrderBy(p=>p.DueDate.Value).ToList();
        }

        public List<sp_SR_GetListActivityResult> GetListActivities(DateTime? startDate, DateTime? endDate, int departmentId)
        {
            return dbContext.sp_SR_GetListActivity(startDate, endDate, departmentId).ToList();
        }

        public List<sp_SR_GetListReportITTeamResult> GetListITeam(DateTime? startDate, DateTime? endDate, string assignUser, int type)
        {
            return dbContext.sp_SR_GetListReportITTeam(startDate, endDate, assignUser, type).ToList();
        }

        public List<sp_SR_GetListReportStatusResult> GetListAllStatus(DateTime? startDate, DateTime? endDate, string assignUser, int statusId)
        {
            return dbContext.sp_SR_GetListReportStatus(startDate, endDate, statusId, assignUser).ToList();
        }

        public List<sp_SR_GetNotesOfService_reqResult> GetListServiceReq()
        {
            return dbContext.sp_SR_GetNotesOfService_req().ToList();
            
        }

        public int GetIdFromSysAid(int id)
        {
            return dbContext.func_GetIdServiceRequestFormSysAid(id).Value;
        }

        public List<sp_SR_GetFilesOfService_reqResult> GetListFilesSR()
        {
            return dbContext.sp_SR_GetFilesOfService_req().ToList();
        }
    }
}