using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CRM.Library.Common;

namespace CRM.Library.Exceptions
{
    public class ForbiddenExceptionOnCurrentPage : CrmException
    {
        #region  CONSTRUCTORS >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>> (4)

        public ForbiddenExceptionOnCurrentPage(String errorCode, String errorMsg, Exception ex)
            : base(errorCode, errorMsg, ex)
        {
            this._errorCode = errorCode;
            this._errorMsg = errorMsg;
        }

        public ForbiddenExceptionOnCurrentPage(String errorCode, String errorMsg)
            : base(errorCode, errorMsg)
        {
            this._errorCode = errorCode;
            this._errorMsg = errorMsg;
        }

        public ForbiddenExceptionOnCurrentPage(String errorMsg)
            :base(MessageConstants.E0002, errorMsg)
        {            
            this._errorMsg = errorMsg;
        }

        public ForbiddenExceptionOnCurrentPage()
        {
            Message msg = new Message(MessageConstants.E0002, MessageType.Info);
            this._errorCode = MessageConstants.E0002;
            this._errorMsg = msg.ToString();
        }

        #endregion
    }
}