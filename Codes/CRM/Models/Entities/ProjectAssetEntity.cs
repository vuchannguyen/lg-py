using CRM.Models;
namespace CRM.Models.Entities
{
    public class ProjectAssetEntity : BaseDao
    {
        private long id;

        public long Id
        {
            get { return id; }
            set { id = value; }
        }

        private string _assetID;

        public string AssetID
        {
            get { return _assetID; }
            set { _assetID = value; }
        }

        private int categoryId;

        public int CategoryId
        {
            get { return categoryId; }
            set { categoryId = value; }
        }

        private string categoryName;

        public string CategoryName
        {
            get { return categoryName; }
            set { categoryName = value; }
        }

        private System.Nullable<int> statusId;

        public System.Nullable<int> StatusId
        {
            get { return statusId; }
            set { statusId = value; }
        }

        private string statusName;

        public string StatusName
        {
            get { return statusName; }
            set { statusName = value; }
        }

        private string employeeId;

        public string EmployeeId
        {
            get { return employeeId; }
            set { employeeId = value; }
        }

        private string employeeName;

        public string EmployeeName
        {
            get { return employeeName; }
            set { employeeName = value; }
        }

        private string employeeEmail;

        public string EmployeeOfficeEmail
        {
            get { return employeeEmail; }
            set { employeeEmail = value; }
        }

        private string managerId;

        public string ManagerId
        {
            get { return managerId; }
            set { managerId = value; }
        }

        private string managerName;

        public string ManagerName
        {
            get { return managerName; }
            set { managerName = value; }
        }

        private string managerOfficeEmail;

        public string ManagerOfficeEmail
        {
            get { return managerOfficeEmail; }
            set { managerOfficeEmail = value; }
        }

        private System.Nullable<int> departmentId;

        public System.Nullable<int> DepartmentId
        {
            get { return departmentId; }
            set { departmentId = value; }
        }

        private string departmentName;

        public string DepartmentName
        {
            get { return departmentName; }
            set { departmentName = value; }
        }

        private string seatCode;

        public string SeatCode
        {
            get { return seatCode; }
            //set { seatCode = string.IsNullOrEmpty(value) ? string.Empty : dbContext.GetEmployeeSeatCode(value.Split('S')[1]); }
            set { seatCode = value; }
        }

        private string project;

        public string Project
        {
            get { return project; }
            set { project = value; }
        }

        private string remark;

        public string Remark
        {
            get { return remark; }
            set { remark = value; }
        }
    }
}