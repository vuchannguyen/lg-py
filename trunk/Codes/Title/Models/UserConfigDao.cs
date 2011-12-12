using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CRM.Library.Common;

namespace CRM.Models
{
    public class UserConfigDao : BaseDao
    {
        public UserConfig GetByID(int userAdminID)
        {
            return dbContext.UserConfigs.Where(q => q.UserAdminID.Equals(userAdminID)).FirstOrDefault();
        }

        public Message Create(UserConfig objUI)
        {
            Message msg = null;
            try
            {
                if (objUI != null)
                {
                    dbContext.UserConfigs.InsertOnSubmit(objUI);
                    dbContext.SubmitChanges();
                    // Show success message
                    msg = new Message(MessageConstants.I0001, MessageType.Info, objUI.UserAdmin.UserName, "added");
                }
            }
            catch (Exception ex)
            {
                msg = new Message(MessageConstants.E0007, MessageType.Error);
                throw ex;
            }
            return msg;
        }

        public Message Update(UserConfig objUI)
        {
            Message msg = null;

            try
            {
                if (objUI != null)
                {
                    // Get current group in dbContext
                    UserConfig objDb = GetByID(objUI.UserAdminID);

                    if (objDb != null)
                    {
                        // Update info by objUI                       
                        objDb.IsOff = objUI.IsOff;
                        objDb.AutoReplyMessage = objUI.AutoReplyMessage;
                        // Submit changes to dbContext
                        dbContext.SubmitChanges();

                        // Show success message
                        msg = new Message(MessageConstants.I0001, MessageType.Info, objDb.UserAdmin.UserName, "updated");
                    }
                }
            }
            catch
            {
                // Show system error
                msg = new Message(MessageConstants.E0007, MessageType.Error);
            }

            return msg;
        }
    }
}