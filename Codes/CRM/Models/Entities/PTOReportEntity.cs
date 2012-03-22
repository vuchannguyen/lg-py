namespace CRM.Models.Entities
{
    public class PTOReportEntity
    {
        private int _ID;

        private string _EmployeeID;

        private int _CarriedForward;

        private int _MonthlyVacation;

        private string _DisplayName;

        private System.DateTime _StartDate;

        private System.Nullable<System.DateTime> _ContractedDate;

        private System.Nullable<int> _SubtractedBalance;

        private System.Nullable<double> _Used;

        private System.Nullable<int> _EOMBalance;

        private System.Nullable<int> _UnpaidLeave;

        private string _Comment;

        private System.Nullable<int> _BorrowedHours;

        private string _ManagerID;

        private string _ManagerName;

        private string _OfficeEmail;

        private string _DepartmentName;

        private int _DepartmentID;

        private string _Project;

        public int ID
        {
            get { return _ID; }
            set { _ID = value; }
        }

        public string DepartmentName
        {
            get { return _DepartmentName; }
            set { _DepartmentName = value; }
        }

        public int DepartmentID
        {
            get { return _DepartmentID; }
            set { _DepartmentID = value; }
        }

        public string Project
        {
            get { return _Project; }
            set { _Project = value; }
        }

        public string EmployeeID
        {
            get { return _EmployeeID; }
            set { _EmployeeID = value; }
        }

        public string ManagerName
        {
            get { return _ManagerName; }
            set { _ManagerName = value; }
        }
        
        public int CarriedForward
        {
            get { return _CarriedForward; }
            set { _CarriedForward = value; }
        }

        public int MonthlyVacation
        {
            get { return _MonthlyVacation; }
            set { _MonthlyVacation = value; }
        }

        public string DisplayName
        {
            get { return _DisplayName; }
            set { _DisplayName = value; }
        }

        public System.DateTime StartDate
        {
            get { return _StartDate; }
            set { _StartDate = value; }
        }

        public System.Nullable<System.DateTime> ContractedDate
        {
            get { return _ContractedDate; }
            set { _ContractedDate = value; }
        }
       
        public System.Nullable<int> SubtractedBalance
        {
            get { return _SubtractedBalance; }
            set { _SubtractedBalance = value; }
        }

        public string ManagerID
        {
            get { return _ManagerID; }
            set { _ManagerID = value; }
        }
       
        public System.Nullable<double> Used
        {
            get { return _Used; }
            set { _Used = value; }
        }

        public System.Nullable<int> EOMBalance
        {
            get { return _EOMBalance; }
            set { _EOMBalance = value; }
        }
        

        public System.Nullable<int> UnpaidLeave
        {
            get { return _UnpaidLeave; }
            set { _UnpaidLeave = value; }
        }
        
        public string Comment
        {
            get { return _Comment; }
            set { _Comment = value; }
        }

        public System.Nullable<int> BorrowedHours
        {
            get { return _BorrowedHours; }
            set { _BorrowedHours = value; }
        }

        public string OfficeEmail
        {
            get { return _OfficeEmail; }
            set { _OfficeEmail = value; }
        }


    }

    public class ManagerByHierarchy
    {
        private string _ID;
        private string _FullName;
        public string ID
        {
            get { return _ID; }
            set { _ID = value; }
        }
        public string FullName
        {
            get { return _FullName; }
            set { _FullName = value; }
        }
    }
}

