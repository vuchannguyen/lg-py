using System;

namespace CRM.Library.Exceptions
{
    public class NotAuthenticate : Exception
    {
        #region  FIELDS >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>> (2)

        protected string _errorCode;
        protected string _errorMsg;

        #endregion

        #region  CONSTRUCTORS >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>> (3)

        public NotAuthenticate(string strErrorCode, string strErrorMsg, Exception ex)
            : base(strErrorCode + ":" + strErrorMsg, ex)
        {
            _errorCode = strErrorCode;
            _errorMsg = strErrorMsg;
        }

        public NotAuthenticate(string strErrorCode, string strErrorMsg)
            : base(strErrorCode + ":" + strErrorMsg)
        {
            _errorCode = strErrorCode;
            _errorMsg = strErrorMsg;
        }

        public NotAuthenticate() : base() { }

        #endregion

        #region  PROPERTIES >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>> (2)

        public string ErrorCode
        {
            get { return _errorCode; }
            set { this._errorCode = value; }
        }

        public string ErrorMessage
        {
            get { return _errorMsg; }
            set { this._errorMsg = value; }
        }

        #endregion
    }
}
