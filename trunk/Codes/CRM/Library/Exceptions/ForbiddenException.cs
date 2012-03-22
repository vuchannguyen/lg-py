using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

using CRM.Library.Common;

namespace CRM.Library.Exceptions
{
    public class ForbiddenException : CrmException
    {
        #region  CONSTRUCTORS >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>> (4)

        public ForbiddenException(String errorCode, String errorMsg, Exception ex)
            : base(errorCode, errorMsg, ex)
        {
            this._errorCode = errorCode;
            this._errorMsg = errorMsg;
        }

        public ForbiddenException(String errorCode, String errorMsg)
            : base(errorCode, errorMsg)
        {
            this._errorCode = errorCode;
            this._errorMsg = errorMsg;
        }

        public ForbiddenException(String errorMsg)
            :base(MessageConstants.E0002, errorMsg)
        {            
            this._errorMsg = errorMsg;
        }

        public ForbiddenException()
        {
            Message msg = new Message(MessageConstants.E0002, MessageType.Info);
            this._errorCode = MessageConstants.E0002;
            this._errorMsg = msg.ToString();
        }

        #endregion
    }
}
