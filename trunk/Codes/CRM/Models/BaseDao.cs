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

        public BaseDao()
        {
            // set command time out
            string stTimeout = System.Configuration.ConfigurationManager.AppSettings["COMMAND_TIME_OUT"];
            if (!string.IsNullOrEmpty(stTimeout) && CRM.Library.Common.CheckUtil.IsInteger(stTimeout))
            {
                dbContext.CommandTimeout = int.Parse(stTimeout);
            } 
        }
    }
}