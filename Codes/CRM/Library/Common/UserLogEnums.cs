using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CRM.Library.Common
{
    public enum ELogAction : int
    {
        Insert = 1,
        Update = 2,
        Delete = 3
    }

    public enum ELogTable : int
    {
        Employee = 1,
        Contract = 2,
        Group = 3,
        Account = 4,
        WorkFlowAdmin = 5,
        JobRequest = 6,
        UserAdmin = 7,
        STT = 8,
        Candidate = 9,
        Interview = 10,
        EForm = 11,
        PurchaseRequest = 12,
        ServiceRequest = 13,
        PTO = 14
    }
}