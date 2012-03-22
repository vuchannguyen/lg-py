using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;

namespace CRM.Library.Common
{
    
        public enum CandidateStatus
        {
            Available = 1,
            Unavailable = 2,
            Interviewing = 3,
            Passed = 4,
            Failed = 5,
            Waiting = 6

        };
        public enum DomainUserProperty { 
          
            LoginName=0,
            AccountName=1,
            DisplayName=2,
            Title=3,
            Department=4,
            Company=5,
            SeatCode=6,
            CreatedDate=7,
            ChangedDate=8,
            OutlookEmail=9

        };

        public enum LocationType
        {
            SeatCode = 1,
            Floor = 2,
            Office = 3,
            Branch = 4
        };

        public enum SortOrder
        {
            desc,
            asc,
        };

        public enum LogType
        { 
            Pto,
            ServiceRequest,
            Other
        };
}