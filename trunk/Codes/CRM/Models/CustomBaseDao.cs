using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Common;
using System.Data.Linq;
using System.Linq;
using System.Linq.Dynamic;
using System.Web;
using CRM.Library.Common;

namespace CRM.Models
{
    public class CustomBaseDao<T> : IDisposable where T : class, new()
    {
        public static readonly string CONNECTION_STRING = ConfigurationManager.AppSettings["CRMConnectionString"];
        // CRM Data Context
        protected static CRMDB dbContext = new CRMDB(CONNECTION_STRING);
        private static Table<T> currentTable;

        /// <summary>
        /// CustomBaseDao constructor
        /// </summary>
        protected CustomBaseDao()
        {
            currentTable = (Table<T>)dbContext.GetTable(typeof(T));
        }

        #region IDisposable Members

        public void Dispose()
        {
            if (dbContext != null)
                dbContext.Dispose();
        }

        #endregion

        /// <summary>
        /// use for insertion items to databasse
        /// </summary>
        /// <param name="domainObject"></param>
        /// <returns>Message</returns>
        public Message Insert(T domainObject)
        {
            try
            {
                currentTable.InsertOnSubmit(domainObject);
                dbContext.SubmitChanges();
                return new Message(MessageConstants.I0001, MessageType.Info, "Your Information", "added");
            }
            catch (Exception)
            {
                return new Message(MessageConstants.E0007, MessageType.Error);
            }
        }

        /// <summary>
        /// use for insertion list to databasse
        /// </summary>
        /// <param name="domainObject"></param>
        /// <returns>Message</returns>
        public Message InsertList(List<T> domainObject)
        {
            try
            {
                currentTable.InsertAllOnSubmit(domainObject);
                dbContext.SubmitChanges();
                return new Message(MessageConstants.I0001, MessageType.Info, "Your Information", "added");
            }
            catch (Exception)
            {
                return new Message(MessageConstants.E0007, MessageType.Error);
            }
        }

        /// <summary>
        /// use for delete item on database
        /// </summary>
        /// <param name="domainObject"></param>
        /// <returns>Message</returns>
        public Message Delete(T domainObject)
        {
            DbTransaction transaction = null;
            try
            {
                dbContext.Connection.Open();
                transaction = dbContext.Connection.BeginTransaction();
                dbContext.Transaction = transaction;
                currentTable.DeleteOnSubmit(domainObject);
                dbContext.SubmitChanges();
                transaction.Commit();
                return new Message(MessageConstants.I0011, MessageType.Info, "These record(s) had been deleted.");
            }
            catch (Exception)
            {
                return new Message(MessageConstants.I0007, MessageType.Error, "Cannot delete this record");
            }
        }

        /// <summary>
        /// use for delete list item on database
        /// </summary>
        /// <param name="domainObject"></param>
        /// <returns>Message</returns>
        public Message DeleteList(List<T> domainObject)
        {
            DbTransaction transaction = null;
            try
            {
                dbContext.Connection.Open();
                transaction = dbContext.Connection.BeginTransaction();
                dbContext.Transaction = transaction;
                currentTable.DeleteAllOnSubmit(domainObject);
                dbContext.SubmitChanges();
                transaction.Commit();
                return new Message(MessageConstants.I0011, MessageType.Info, "These record(s) had been deleted.");
            }
            catch (Exception)
            {
                return new Message(MessageConstants.I0007, MessageType.Error, "Cannot delete this record");
            }
        }

        /// <summary>
        /// use for update item on database
        /// </summary>
        /// <param name="domainObject"></param>
        /// <returns>Message</returns>
        public Message Update(T domainObject)
        {
            try
            {
                dbContext.SubmitChanges();
                return new Message(MessageConstants.I0011, MessageType.Info, "This record have been updated");
            }
            catch (Exception)
            {
                return new Message(MessageConstants.I0007, MessageType.Error, "Cannot update this record");
            }
        }

        /// <summary>
        /// use for get all item from database
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        public List<T> GetList(Func<T, bool> condition)
        {
            return currentTable.Where(condition).ToList();
        }

        /// <summary>
        /// use for get an item from database
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        public T GetItem(Func<T, bool> condition)
        {
            return currentTable.Where(condition).FirstOrDefault();
        }

        /// <summary>
        /// use for Sort item from a list
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="listObject"></param>
        /// <param name="selector"></param>
        /// <param name="orderType"></param>
        /// <returns></returns>
        public List<T> SortItem<TKey>(List<T> listObject, Func<T, TKey> selector, string orderType)
        {
            if (orderType.Equals("desc"))
            {
                return listObject.OrderByDescending(selector).ToList<T>();
            }
            else
            {
                return listObject.OrderBy(selector).ToList<T>();
            }
        }
    }
}