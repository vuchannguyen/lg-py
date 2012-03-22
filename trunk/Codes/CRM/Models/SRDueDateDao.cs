
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Web;
using CRM.Library.Common;
using CRM.Models.Entities;


namespace CRM.Models
{
    public class SRDueDateDao : BaseDao
    {
        public List<sp_GetSRDueDateResult> GetListDueDate(int? categoryId, int? subCategoryId, int? urgentId, int? statusId)
        {
           
           return dbContext.sp_GetSRDueDate(categoryId, subCategoryId, urgentId, statusId).ToList();
        }

        public List<sp_GetSRDueDateResult> Sort(List<sp_GetSRDueDateResult> list, string sortColumn, string sortOrder)
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
                case "CategoryName":
                    list.Sort(
                         delegate(sp_GetSRDueDateResult m1, sp_GetSRDueDateResult m2)
                         { return System.String.CompareOrdinal(m1.CategoryName, m2.CategoryName) * order; });
                    break;
                case "UrgentName":
                    list.Sort(
                         delegate(sp_GetSRDueDateResult m1, sp_GetSRDueDateResult m2)
                         { return System.String.CompareOrdinal(m1.UrgentName, m2.UrgentName) * order; });
                    break;
                case "Hours":
                    list.Sort(
                         delegate(sp_GetSRDueDateResult m1, sp_GetSRDueDateResult m2)
                         { return m1.Hours.CompareTo(m2.Hours) * order; });
                    break;
                case "IsActive":
                    list.Sort(
                         delegate(sp_GetSRDueDateResult m1, sp_GetSRDueDateResult m2)
                         { return m1.IsActive.CompareTo(m2.IsActive) * order; });
                    break;
            }
            return list;
        }

        public Message InserttOrUpdate(int? Id, SR_DueDate dueDate)
        {
            if(Id == null || Id == 0)
            {
                dueDate.CreateDate = dueDate.UpdateDate = DateTime.Now;
                dueDate.CreatedBy = dueDate.UpdatedBy = HttpContext.Current.User.Identity.Name;
                dbContext.SR_DueDates.InsertOnSubmit(dueDate);
                dbContext.SubmitChanges();
                return new Message(MessageConstants.I0001, MessageType.Info, "DueDate had been", "added");
            }
            else
            {
                SR_DueDate dueDates = dbContext.SR_DueDates.Single(duedate => duedate.ID == Id);
                dueDates.UrgentID = dueDate.UrgentID;
                dueDates.CreateDate = DateTime.Now;
                dueDates.CreatedBy = HttpContext.Current.User.Identity.Name;
                dueDates.SubCategoryID = dueDate.SubCategoryID;
                dueDates.Hours = dueDate.Hours;
                dueDates.IsActive = dueDate.IsActive;
                dueDates.Remark = dueDate.Remark;
                dueDates.UpdateDate = DateTime.Now;
                dueDates.UpdatedBy = HttpContext.Current.User.Identity.Name;
                dbContext.SubmitChanges();
                return new Message(MessageConstants.I0001, MessageType.Info, "DueDate had been updated", "updated");
            }
        }

        public Message UpdateStatus(int id)
        {
            Message msg = null;
            var dueDate = dbContext.SR_DueDates.Single(p => p.ID == id);
            try
            {
                if (dueDate.IsActive)
                    dueDate.IsActive = false;
                else
                    dueDate.IsActive = true;
                dbContext.SubmitChanges();
                msg = new Message(MessageConstants.I0011, MessageType.Info, "DueDate status had been updated.");
            }
            catch (Exception exception)
            {
                msg = new Message(MessageConstants.E0007, MessageType.Error);
            }
            return msg;
        }

        public SR_DueDate GetDueDateById(string Id)
        {
            var dueDate = dbContext.SR_DueDates.Single(p => p.ID == Convert.ToInt32(Id));
            return dueDate;
        }

        public SR_DueDate GetDueDateBy(string subId, string urgentId)
        {
            var dueDate = dbContext.SR_DueDates.Where(p => p.SubCategoryID == ConvertUtil.ConvertToInt(subId) && p.UrgentID == ConvertUtil.ConvertToInt(urgentId)).FirstOrDefault();
            
            return dueDate;
        }

        public Message DeleteDueDate(string id)
        {
            Message msg = null;
            DbTransaction transaction = null;
            try
            {
                dbContext.Connection.Open();
                transaction = dbContext.Connection.BeginTransaction();
                dbContext.Transaction = transaction;

                if (!string.IsNullOrEmpty(id))
                {
                    id = id.TrimEnd(',');
                    string[] ids = id.Split(',').Distinct().ToArray();

                    foreach (var s in ids)
                    {
                        var dueDate = GetDueDateById(s);
                        if (dueDate != null)
                        {
                            dbContext.SR_DueDates.DeleteOnSubmit(dueDate);
                            dbContext.SubmitChanges();
                        }
                    }
                    msg = new Message(MessageConstants.I0011, MessageType.Info, "DueDate had been deleted successful"); 
                    transaction.Commit();
                }
            }
            catch (Exception exception)
            {
                if (transaction != null) transaction.Rollback();
                msg = new Message(MessageConstants.E0007, MessageType.Error);
            }
            return msg;
        }
    }
}