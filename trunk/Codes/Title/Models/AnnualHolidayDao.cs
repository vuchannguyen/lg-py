using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CRM.Library.Common;
using System.Data.Common;
using System.Web.UI.WebControls;

namespace CRM.Models
{
    /// <summary>
    /// Annual holiday DAO
    /// </summary>
    public class AnnualHolidayDao:BaseDao
    {
        /// <summary>
        /// Get a holiday by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public AnnualHoliday GetByID(int id)
        {
            return dbContext.AnnualHolidays.FirstOrDefault(p => p.ID == id);
        }
        /// <summary>
        /// Get list by ids
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public List<AnnualHoliday> GetListByIDs(string ids)
        {
            List<AnnualHoliday> result = new List<AnnualHoliday>();
            string[] arrID = ids.Split(',');
            foreach(string id in arrID)
            {
                result.Add(GetByID(int.Parse(id)));
            }
            return result;
        }

        /// <summary>
        /// Get list of holiday filtered by name/ year
        /// </summary>
        /// <param name="name"></param>
        /// <param name="year"></param>
        /// <returns></returns>
        public List<AnnualHoliday> GetFilterList(string name, string year)
        {
            var holidayList = GetAll();
            if (!string.IsNullOrEmpty(name))
            {
                holidayList = holidayList.Where(holiday => holiday.HolidayName.Trim().ToLower().
                    Contains(name.Trim().ToLower())).ToList();
            }
            if (!string.IsNullOrEmpty(year))
            {
                holidayList = holidayList.Where(holiday => holiday.HolidayDate.Year == int.Parse(year)).ToList();
            }
            return holidayList;
        }
        /// <summary>
        /// Get all holiday
        /// </summary>
        /// <returns></returns>
        public List<AnnualHoliday> GetAll()
        {
            return dbContext.AnnualHolidays.ToList();
        }
        /// <summary>
        /// Get list of existing year(s)
        /// </summary>
        /// <returns></returns>
        public List<ListItem> GetListOfExistingYear()
        {
            List<int> list =  dbContext.AnnualHolidays.Select(p=>p.HolidayDate.Year).Distinct().ToList();
            List<ListItem> listItem = new List<ListItem>();
            foreach (int item in list)
            {
                listItem.Add(new ListItem("Year", item.ToString()));
            }
            return listItem;
        }
        /// <summary>
        /// Sort the list of holiday
        /// </summary>
        /// <param name="holidayList"></param>
        /// <param name="sortColumn"></param>
        /// <param name="sortOrder"></param>
        /// <returns></returns>
        public List<AnnualHoliday> Sort(List<AnnualHoliday> holidayList, string sortColumn, string sortOrder)
        {
            int order = 1;
            if (sortOrder == "desc")
            {
                order = -1;
            }
            switch (sortColumn)
            {
                case "HolidayName":
                    holidayList.Sort(
                         delegate(AnnualHoliday holiday1, AnnualHoliday holiday2)
                         { return holiday1.HolidayName.CompareTo(holiday2.HolidayName) * order; });
                    break;
                case "HolidayDate":
                    holidayList.Sort(
                         delegate(AnnualHoliday holiday1, AnnualHoliday holiday2)
                         { return holiday1.HolidayDate.CompareTo(holiday2.HolidayDate) * order; });
                    break;
                case "HolidayDay":
                    holidayList.Sort(
                         delegate(AnnualHoliday holiday1, AnnualHoliday holiday2)
                         { return holiday1.HolidayDate.DayOfWeek.CompareTo(holiday2.HolidayDate.DayOfWeek) * order; });
                    break;
                case "Description":
                    holidayList.Sort(
                         delegate(AnnualHoliday holiday1, AnnualHoliday holiday2)
                         {
                             holiday1.Description = string.IsNullOrEmpty(holiday1.Description) ? "" : holiday1.Description;
                             holiday2.Description = string.IsNullOrEmpty(holiday2.Description) ? "" : holiday2.Description;
                             return holiday1.Description.CompareTo(holiday2.Description) * order;
                         });
                    break;
                case "CreateDate":
                    holidayList.Sort(
                         delegate(AnnualHoliday holiday1, AnnualHoliday holiday2)
                         { return holiday1.CreateDate.CompareTo(holiday2.CreateDate) * order; });
                    break;
                case "CreatedBy":
                    holidayList.Sort(
                         delegate(AnnualHoliday holiday1, AnnualHoliday holiday2)
                         { return holiday1.CreatedBy.CompareTo(holiday2.CreatedBy) * order; });
                    break;
                case "UpdateDate":
                    holidayList.Sort(
                         delegate(AnnualHoliday holiday1, AnnualHoliday holiday2)
                         { return holiday1.UpdateDate.CompareTo(holiday2.UpdateDate) * order; });
                    break;
                case "UpdatedBy":
                    holidayList.Sort(
                         delegate(AnnualHoliday holiday1, AnnualHoliday holiday2)
                         { return holiday1.UpdatedBy.CompareTo(holiday2.UpdatedBy) * order; });
                    break;
            }
            return holidayList;
        }
        /// <summary>
        /// Check if the holiday exists
        /// </summary>
        /// <param name="holiday"></param>
        /// <returns></returns>
        public bool IsExist(AnnualHoliday holiday)
        {
            return dbContext.AnnualHolidays.Where(p => p.HolidayDate.Date.
                Equals(holiday.HolidayDate.Date)).ToList().Count != 0 ? true: false;
        }
        /// <summary>
        /// Insert new holiday
        /// </summary>
        /// <param name="holiday"></param>
        /// <returns></returns>
        public Message Insert(AnnualHoliday holiday)
        {
            Message msg = null;
            try
            {
                if (!IsExist(holiday))
                {
                    holiday.CreateDate = DateTime.Now;
                    holiday.UpdateDate = DateTime.Now;
                    dbContext.AnnualHolidays.InsertOnSubmit(holiday);
                    dbContext.SubmitChanges();
                    msg = new Message(MessageConstants.I0001, MessageType.Info, 
                        "Holiday '" + holiday.HolidayName + "'", "added");
                }
                else
                {
                    msg = new Message(MessageConstants.E0020, MessageType.Error, 
                        "The day '" + holiday.HolidayDate.ToString(Constants.DATETIME_FORMAT_VIEW) + "' has already", 
                        "Annual Holiday list" );
                }
            }
            catch
            {
                msg = new Message(MessageConstants.E0007, MessageType.Error);   
            }
            return msg;
        }
        /// <summary>
        /// Update a holiday
        /// </summary>
        /// <param name="holiday"></param>
        /// <returns></returns>
        public Message Update(AnnualHoliday holiday)
        {
            Message msg = null;
            try
            {
                AnnualHoliday holidayDb = GetByID(holiday.ID);
                //Another user has updated this holiday
                if (!holidayDb.UpdateDate.ToString().Trim().Equals(holiday.UpdateDate.ToString().Trim()))
                {
                    msg = new Message(MessageConstants.E0025, MessageType.Error, "The holiday");   
                }
                //The holiday has already existed in DB
                else if (!holiday.HolidayDate.Date.Equals(holidayDb.HolidayDate.Date) && IsExist(holiday))
                {
                    msg = new Message(MessageConstants.E0020, MessageType.Error,
                        "The day '" + holiday.HolidayDate.ToString(Constants.DATETIME_FORMAT_VIEW) + "' has already",
                        "Annual Holiday list");
                }
                //Successful case
                else
                {
                    holidayDb.HolidayName = holiday.HolidayName;
                    holidayDb.HolidayDate = holiday.HolidayDate;
                    holidayDb.Description = holiday.Description;
                    holidayDb.UpdatedBy = holiday.UpdatedBy;
                    holidayDb.UpdateDate = DateTime.Now;
                    dbContext.SubmitChanges();
                    msg = new Message(MessageConstants.I0001, MessageType.Info,
                        "Holiday '" + holidayDb.HolidayName + "'", "edited");
                }
            }
            catch
            {
                msg = new Message(MessageConstants.E0007, MessageType.Error);
            }
            return msg;
        }
        /// <summary>
        /// Delete list
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public Message DeleteList(string ids)
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
                    string[] idArr = ids.TrimEnd(',').Split(',');
                    int total = 0;
                    foreach (string id in idArr)
                    {
                        int holidayID = int.Parse(id);
                        AnnualHoliday holiday = GetByID(holidayID);
                        if (holiday != null)
                        {
                            dbContext.AnnualHolidays.DeleteOnSubmit(holiday);
                            total++;
                        }
                    }
                    dbContext.SubmitChanges();
                    trans.Commit();
                    msg = new Message(MessageConstants.I0001, MessageType.Info,
                            total.ToString() + " Holiday(s)", "deleted");
                }
                else
                {
                    trans.Rollback();
                    msg = new Message(MessageConstants.E0041, MessageType.Error);
                }
            }
            catch
            {
                if (trans != null)
                {
                    trans.Rollback();
                }
                // Show system error
                msg = new Message(MessageConstants.E0007, MessageType.Error);
            }
            return msg;
        }
    }
}