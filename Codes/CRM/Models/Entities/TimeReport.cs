using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CRM.Models.Entities
{
    public class TimeReport
    {
        private string _empId;

        private DateTime? _transDate;

        private string _empName;

        private string _jobTitle;

        private int? _deptId;

        private string _deptName;

        private string _locationCode;

        private string _managerName;

        private int? _timeIn;

        private int? _timeOut;

        public string EmpId
        {
            get { return _empId; }
            set { _empId = value; }
        }

        public DateTime? TransDate
        {
            get { return _transDate; }
            set { _transDate = value; }
        }

        public string EmpName
        {
            get { return _empName; }
            set { _empName = value; }
        }

        public string JobTitle
        {
            get { return _jobTitle; }
            set { _jobTitle = value; }
        }

        public int? DeptId
        {
            get { return _deptId; }
            set { _deptId = value; }
        }

        public string DeptName
        {
            get { return _deptName; }
            set { _deptName = value; }
        }

        public string LocationCode
        {
            get { return _locationCode; }
            set { _locationCode = value; }
        }

        public string ManagerName
        {
            get { return _managerName; }
            set { _managerName = value; }
        }

        public int? TimeIn
        {
            get { return _timeIn; }
            set { _timeIn = value; }
        }

        public int? TimeOut
        {
            get { return _timeOut; }
            set { _timeOut = value; }
        }
    }
}