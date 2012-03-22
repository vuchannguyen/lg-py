using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CRM.Library.Common;

namespace CRM.Models.Entities
{
    public class PTOEntity
    {
        private string _ID;

        private string _Submitter;

        private string _FirstName;

        private string _MiddleName;

        private string _LastName;

        private string _OfficeEmail;

        private System.Nullable<System.Double> _hour;

        private string _StatusName;

        private string _TypeName;

        private int _StatusID;

        private string _Reason;

        private System.DateTime _CreateDate;

        private int _Key_ID;

        private int _PTOType_ID;

        private System.Nullable<int> _ParentTypeId;

        private string _SubmitTo;

        private System.Nullable<int> _Balance;

        public  string ID
        {
            get { return _ID; }
            set { _ID = value; }
        }

        public string Submitter
        {
            get { return _Submitter; }
            set { _Submitter = value; }
        }

        public string FirstName
        {
            get { return _FirstName; }
            set { _FirstName = value; }
        }

        public string MiddleName
        {
            get { return _MiddleName; }
            set { _MiddleName = value; }
        }
        
        public string LastName
        {
            get { return _LastName; }
            set { _LastName = value; }
        }

        public string OfficeEmail
        {
            get { return _OfficeEmail; }
            set { _OfficeEmail = value; }
        }

        public System.Nullable<System.Double> Hours
        {
            get { return _hour; }
            set { _hour = value; }
        }

        public string StatusName
        {
            get { return _StatusName; }
            set { _StatusName = value; }
        }

        public string TypeName
        {
            get { return _TypeName; }
            set { _TypeName = value; }
        }

        public int StatusID
        {
            get { return _StatusID; }
            set { _StatusID = value; }
        }

        public string Reason
        {
            get { return _Reason; }
            set { _Reason = value; }
        }

        public System.DateTime CreateDate
        {
            get { return _CreateDate; }
            set { _CreateDate = value; }
        }

        public int Key_ID
        {
            get { return _Key_ID; }
            set { _Key_ID = value; }
        }

        public int PTOType_ID
        {
            get { return _PTOType_ID; }
            set { _PTOType_ID = value; }
        }

        public System.Nullable<int> ParentTypeId
        {
            get { return _ParentTypeId; }
            set { _ParentTypeId = value; }
        }

        public string SubmitTo
        {
            get { return _SubmitTo; }
            set { _SubmitTo = value; }
        }

        public System.Nullable<int> Balance
        {
            get { return _Balance; }
            set { _Balance = value; }
        }
    }
}