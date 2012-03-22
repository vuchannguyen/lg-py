
namespace CRM.Models.Entities
{
    public class MasterLogEntity
    {
        private string _UserName;

        public string UserName
        {
            get { return _UserName; }
            set { _UserName = value; }
        }

        private System.DateTime _LogDate;

        public System.DateTime LogDate
        {
            get { return _LogDate; }
            set { _LogDate = value; }
        }

        private System.Nullable<int> _LogType;

        public System.Nullable<int> LogType
        {
            get { return _LogType; }
            set { _LogType = value; }
        }

    }
}