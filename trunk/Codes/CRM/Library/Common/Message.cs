using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CRM.Library.Common
{
    #region Message entity

    [Serializable]
    public class Message
    {
        #region Variable(s)

        private string _msgCode;
        private object[] _holders;
        private MessageType _msgType;
        private string _msgText;

        #endregion

        #region Constructor(s)

        public Message(string msgCode, MessageType msgType)
        {
            _msgCode = msgCode;
            _msgType = msgType;
            _msgText = HttpUtility.HtmlEncode(String.Format(Resources.Message.ResourceManager.GetString(msgCode)));            
        }

        public Message(string msgCode, MessageType msgType, params object[] holders)
        {
            _msgCode = msgCode;
            _msgType = msgType;
            _holders = holders;

            _msgText = HttpUtility.HtmlEncode(String.Format(Resources.Message.ResourceManager.GetString(msgCode), holders));
        }

        #endregion

        #region Properties

        public string MsgText
        {
            get { return _msgText; }
            set { _msgText = value; }
        }

        public string MsgCode
        {
            get { return _msgCode; }
            set { _msgCode = value; }
        }

        public object[] Holders
        {
            get { return _holders; }
            set { _holders = value; }
        }

        public MessageType MsgType
        {
            get { return _msgType; }
            set { _msgType = value; }
        }

        #endregion

        #region Method

        public override string ToString()
        {
            return _msgText;
        }

        #endregion
    }

    #region Message Collection

    public class MessageCollection
    {
        private List<Message> _msgCollection = new List<Message>();

        public void Add(Message message)
        {
            _msgCollection.Add(message);
        }

        public Message this[int i]
        {
            get { return _msgCollection[i]; }
            set { _msgCollection[i] = value; }
        }

        public List<Message> MsgCollection
        {
            get { return _msgCollection; }
            set { _msgCollection = value; }
        }
    }

    #endregion

    #endregion

    #region Message type

    public enum MessageType
    {
        Default = 0,
        Error = 1,
        Info = 2,
        Warning = 3,
        Inquiry = 4
    }

    #endregion

    #region Message constant(s)

    public class MessageConstants
    {
        // Error message
        public const string E0001 = "E0001";
        public const string E0002 = "E0002";
        public const string E0003 = "E0003";
        public const string E0004 = "E0004";
        public const string E0005 = "E0005";
        public const string E0006 = "E0006";
        public const string E0007 = "E0007";
        public const string E0008 = "E0008";
        public const string E0009 = "E0009";
        public const string E0010 = "E0010";
        public const string E0011 = "E0011";
        public const string E0012 = "E0012";
        public const string E0013 = "E0013";
        public const string E0014 = "E0014";
        public const string E0015 = "E0015";
        public const string E0016 = "E0016";
        public const string E0017 = "E0017";
        public const string E0018 = "E0018";
        public const string E0019 = "E0019";
        public const string E0020 = "E0020";
        public const string E0021 = "E0021";
        public const string E0022 = "E0022";
        public const string E0023 = "E0023";
        public const string E0024 = "E0024";
        public const string E0025 = "E0025";
        public const string E0026 = "E0026";
        public const string E0027 = "E0027";
        public const string E0028 = "E0028";
        public const string E0029 = "E0029";
        public const string E0030 = "E0030";
        public const string E0031 = "E0031";
        public const string E0032 = "E0032";
        public const string E0033 = "E0033";
        public const string E0034 = "E0034";
        public const string E0035 = "E0035";
        public const string E0036 = "E0036";
        public const string E0038 = "E0038";
        public const string E0039 = "E0039";
        public const string E0040 = "E0040";
        public const string E0041 = "E0041";
        public const string E0042 = "E0042";
        public const string E0043 = "E0043";
        public const string E0044 = "E0044";
        public const string E0045 = "E0045";
        public const string E0046 = "E0046";
        public const string E0047 = "E0047";
        public const string E0048 = "E0048";
        public const string E0049 = "E0049";
        public const string E0050 = "E0050";
        public const string E0051 = "E0051";
        public const string E0052 = "E0052";
        public const string E0053 = "E0053";
        public const string E0054 = "E0054";
        public const string E0055 = "E0055";
       
        // Info message
        public const string I0001 = "I0001";
        public const string I0002 = "I0002";
        public const string I0003 = "I0003";
        public const string I0004 = "I0004";
        public const string I0005 = "I0005";
        public const string I0006 = "I0006";
        public const string I0007 = "I0007";
        public const string I0008 = "I0008";
        public const string I0009 = "I0009";
        public const string I0010 = "I0010";
        public const string I0011 = "I0011";
        public const string I0012 = "I0012";
        public const string I0013 = "I0013";
        public const string I0014 = "I0014";
        public const string I0015 = "I0015";
        public const string I0016 = "I0016";
    }

    #endregion
}