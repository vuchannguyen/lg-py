using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CRM.Library.Common;
using System.Data.Common;
using System.Text.RegularExpressions;
using System.Linq.Dynamic;
using System.Data.Linq.SqlClient;

namespace CRM.Models
{
    public class NonEmployeeCerfiticationDao: BaseDao
    {
        public List<NonEmployee> GetNonEmployeeList(string text)
        {
            if (text == Constants.TRAINING_NONEMPLOYEE_CERTIFICATION_SEARCH_NAME)
            {
                text = null;
            }
            return dbContext.NonEmployees.Where(p=>p.DeleteFlag==false).ToList();
        }

        public List<NonEmployee> Sort(List<NonEmployee> list, string sortColumn, string sortOrder)
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
                         delegate(NonEmployee m1, NonEmployee m2)
                         { return m1.ID.CompareTo(m2.ID) * order; });
                    break;
                case "DisplayName":
                    list.Sort(
                         delegate(NonEmployee m1, NonEmployee m2)
                         {
                             return CommonFunc.GetNonemployeeFullName(m1, Constants.FullNameFormat.FirstMiddleLast).
                             CompareTo(CommonFunc.GetNonemployeeFullName(m2, Constants.FullNameFormat.FirstMiddleLast)) 
                             * order;
                         });
                    break;
                case "PhoneNumber":
                    list.Sort(
                         delegate(NonEmployee m1, NonEmployee m2)
                         { return m1.PhoneNumber.CompareTo(m2.PhoneNumber) * order; });
                    break;
                case "Email":
                    list.Sort(
                         delegate(NonEmployee m1, NonEmployee m2)
                         { return m1.Email.CompareTo(m2.Email) * order; });
                    break;
                case "Partnership":
                    list.Sort(
                         delegate(NonEmployee m1, NonEmployee m2)
                         { return m1.Partnership.CompareTo(m2.Partnership) * order; });
                    break;
                case "Year":
                    list.Sort(
                         delegate(NonEmployee m1, NonEmployee m2)
                         { return (m1.Year.HasValue ? m1.Year.Value : 0).CompareTo(m2.DOB.HasValue ? m2.Year.Value : 0) * order; });
                    break;
                case "Class":
                    list.Sort(
                         delegate(NonEmployee m1, NonEmployee m2)
                         { return m1.Class.CompareTo(m2.Class) * order; });
                    break;
                case "CertID":
                    list.Sort(
                         delegate(NonEmployee m1, NonEmployee m2)
                         { return (m1.CertID.HasValue? m1.CertID.Value: 0).CompareTo(m2.CertID.HasValue ? m2.CertID.Value : 0) * order; });
                    break;
            }

            return list;
        }

        public NonEmployee GetByID(string id)
        {
            return dbContext.NonEmployees.Where(p=>p.ID.Equals(id)&&p.DeleteFlag==false).FirstOrDefault<NonEmployee>();
        }

        public Message Insert(NonEmployee objUI)
        {
            Message msg = null;
            try
            {
                    objUI.CreateDate = DateTime.Now;
                    objUI.UpdateDate = DateTime.Now;
                    objUI.DeleteFlag = false;
                    objUI.CreatedBy = objUI.UpdatedBy = HttpContext.Current.User.Identity.Name;
                    dbContext.NonEmployees.InsertOnSubmit(objUI);
                    dbContext.SubmitChanges();
                    msg = new Message(MessageConstants.I0001, MessageType.Info, "NonEmployee Certification '"
                        + objUI.FirstName + " "
                        + objUI.MiddleName == null ? string.Empty : objUI.MiddleName + " "
                        + objUI.LastName + "'", "added");
            }
            catch (Exception ex)
            {
                msg = new Message(MessageConstants.E0007, MessageType.Error);
                throw ex;
            }
            return msg;

        }
        public Message Update(NonEmployee objUI)
        {
            Message msg = null;
           
            try
            {
                NonEmployee objDB = GetByID(objUI.ID.ToString());
                if (objDB != null)
                {
                    objDB.ID = objUI.ID;
                    objDB.FirstName = objUI.FirstName;
                    objDB.MiddleName = objUI.MiddleName;
                    objDB.LastName = objUI.LastName;
                    objDB.CertID = objUI.CertID;
                    objDB.Class = objUI.Class;
                    objDB.CreatedBy = objUI.CreatedBy;
                    objDB.CreateDate = DateTime.Now;
                    objDB.DeleteFlag = objUI.DeleteFlag;
                    objDB.DOB = objUI.DOB;
                    objDB.Email = objUI.Email;
                    objDB.Notes = objUI.Notes;
                    objDB.Partnership = objUI.Partnership;
                    objDB.PhoneNumber = objUI.PhoneNumber;
                    objDB.UpdatedBy = HttpContext.Current.User.Identity.Name;
                    objDB.UpdateDate = DateTime.Now;
                    objDB.Year = objUI.Year;
                    objDB.CreatedBy = objUI.CreatedBy;
                    dbContext.SubmitChanges();  
                    msg = new Message(MessageConstants.I0001, MessageType.Info, "NonEmployee '"
                        + objDB.FirstName + " "
                        + objDB.MiddleName == null ? string.Empty : objDB.MiddleName + " "
                        + objDB.LastName + "'", "updated");
                }
            }
            catch (Exception ex)
            {                             // Show system error
                msg = new Message(MessageConstants.E0007, MessageType.Error);
            }
            return msg;
        }
      
        private void Delete(NonEmployee objUI)
        {
            if (objUI != null)
            {
                NonEmployee objDb = GetByID(objUI.ID.ToString());
                if (objDb != null)
                {               
                    objDb.DeleteFlag = true;
                    objDb.UpdateDate = DateTime.Now;
                    objDb.UpdatedBy = objUI.UpdatedBy;
                    dbContext.SubmitChanges();

                }
            }
        }
        public Message DeleteList(string[] nonenID)
        {
            DbTransaction transaction = null;
            try
            {
                if (dbContext.Connection.State == System.Data.ConnectionState.Closed)
                {
                    dbContext.Connection.Open();
                }
                transaction = dbContext.Connection.BeginTransaction();
                dbContext.Transaction = transaction;
                int total = nonenID.Length;
                foreach (var id in nonenID)
                {
                    var customer = dbContext.NonEmployees.FirstOrDefault(p => p.ID == ConvertUtil.ConvertToInt(id));
                    customer.UpdateDate = DateTime.Now;
                    customer.UpdatedBy = HttpContext.Current.User.Identity.Name;
                    customer.DeleteFlag = true;
                    dbContext.SubmitChanges();
                }
                transaction.Commit();
                return new Message(MessageConstants.I0011, MessageType.Info, nonenID.Length +
                    (nonenID.Length > 1 ? " NonEmployee have" : " NonEmployee has") + " been deleted");

            }
            catch (Exception)
            {
                transaction.Rollback();
                return new Message(MessageConstants.E0001, MessageType.Error);
            }
        }
        private bool IsValidUpdateDate(NonEmployee objUI, NonEmployee objDb, out Message msg)
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
                            msg = new Message(MessageConstants.E0025, MessageType.Error, "NonEmployee ID " + objDb.ID);
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
        public IQueryable<NonEmployee> GetListQuery(string text)
        {
            var sql = from nonemple in dbContext.NonEmployees
                      where nonemple.DeleteFlag == false
                      select nonemple;
            if (text != string.Empty)
            {
                sql = sql.Where(p => (p.MiddleName != null ? (p.FirstName + " " + p.MiddleName + " " + p.LastName) : (p.FirstName + " " + p.LastName)).Contains(text)
                    ||p.Class.ToString().Contains(text)||p.PhoneNumber.Contains(text)||p.Year.ToString().Contains(text));
            }
            return sql;
        }
        public List<NonEmployee> GetList(string sortColumn, string sortOrder, int skip, int take, string text)
        {
            var sql = GetListQuery(text);
            switch (sortColumn)
            {

                case "DisplayName":
                    sql = sql.OrderBy("FirstName " + sortOrder + "," + "MiddleName " + sortOrder + "," + "LastName " + sortOrder);
                    break;
                case "PhoneNumber":
                    sql = sql.OrderBy("PhoneNumber " + sortOrder);
                    break;
                case "Email":
                    sql = sql.OrderBy("Email " + sortOrder);
                    break;
                case "DOB":
                    sql = sql.OrderBy("DOB " + sortOrder);
                    break;
                case "Partnership":
                    sql = sql.OrderBy("Partnership " + sortOrder);
                    break;
                case "Year":
                    sql = sql.OrderBy("Year " + sortOrder);
                    break;
                case "Class":
                    sql = sql.OrderBy("Class " + sortOrder);
                    break;
                case "CertID":
                    sql = sql.OrderBy("CertID " + sortOrder);
                    break;
                case "Notes":
                    sql = sql.OrderBy("Notes " + sortOrder);
                    break;
                default:
                    sql = sql.OrderBy(sortColumn + " " + sortOrder);
                    break;
            }
            if (skip == 0 && take == 0)
                return sql.ToList();
            else
                return sql.Skip(skip).Take(take).ToList();
    

        }
    }
}