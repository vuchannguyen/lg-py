using System;
using System.Collections.Generic;
using System.Web;
using System.IO;
using System.Xml.Serialization;
using System.Security.Principal;

namespace CRM.Models
{
    public class UserData
    {
        public int UserID;
        public string UserName;
        public int Role;

        public UserData(){}

        public UserData(int userID, string userName, int role)
        {
            this.UserID = userID;
            this.UserName = userName;
            this.Role = role;
        }


        public string ToXml()
        {
            StringWriter sw = new StringWriter();
            XmlSerializer serializer = new XmlSerializer(typeof(UserData));
            serializer.Serialize(sw, this);
            sw.Close();

            return sw.ToString();
        }

        public static UserData CreateUserData(string userDataXml)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(UserData));
            UserData ud = (UserData)serializer.Deserialize(new StringReader(userDataXml));
            return ud;
        }
    }

    public class AuthenticationProjectPrincipal : IPrincipal
    {
        private IIdentity identity;
        private UserData userData;

        public AuthenticationProjectPrincipal(IIdentity identity, UserData udata)
        {
            this.identity = identity;
            this.userData = udata;
        }


        public UserData UserData
        {
            get
            {
                return userData;
            }
        }


        #region IPrincipal Members

        public IIdentity Identity
        {
            get { return identity; }
        }

        public bool IsInRole(string role)
        {
            if (userData.Role == int.Parse(role))
                return true;
            else
                return false;
        }

        #endregion
    }
}