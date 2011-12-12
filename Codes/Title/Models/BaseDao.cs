namespace CRM.Models
{
    /// <summary>
    /// Base Dao
    /// All of DAO need inherit this class
    /// </summary>
    public class BaseDao
    {
        public static readonly string CONNECTION_STRING = System.Configuration.ConfigurationManager.AppSettings["CRMConnectionString"];
        // CRM Data Context
        protected CRMDB dbContext = new CRMDB(CONNECTION_STRING);
    }
}