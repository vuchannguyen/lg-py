using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Web;
using CRM.Library.Common;
using CRM.Models.Entities;

namespace CRM.Models
{
    public class UserConfigDao : BaseDao
    {
        public UserConfig GetByID(int userAdminID)
        {
            return dbContext.UserConfigs.Where(q => q.UserAdminID.Equals(userAdminID)).FirstOrDefault();
        }

        public List<UserConfigEntity> GetUserConfig (int userAdminID)
        {
            var query = from userConfigModule in dbContext.UserConfigModules
                        join userConfig in dbContext.UserConfigs on userConfigModule.Id equals userConfig.ModuleId
                            into ps
                        from userConfig in ps.Where(userConfig=> userConfig.UserAdminID==userAdminID).DefaultIfEmpty()
                        select new UserConfigEntity
                                   {
                                       AutoReplyMessage = userConfig.AutoReplyMessage,
                                       Description = userConfigModule.Description,
                                       ModuleId = userConfig.ModuleId,
                                       Name = userConfigModule.Name,
                                       UserAdminId = userConfig.UserAdminID,
                                       Id = userConfigModule.Id
                                   };
            return query.ToList();
        }

        public List<UserConfig> GetListUserConfig(int userAdminId)
        {
            return dbContext.UserConfigs.Where(q => q.UserAdminID.Equals(userAdminId)).ToList();
        }

        public Message Create(UserConfig objUI)
        {
            Message msg = null;
            try
            {
                    dbContext.UserConfigs.InsertOnSubmit(objUI);
                    dbContext.SubmitChanges();
                    // Show success message
                    msg = new Message(MessageConstants.I0001, MessageType.Info, objUI.UserAdmin.UserName, "added");
              

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
                        objDb.ModuleId = objUI.ModuleId;
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

        public Message  Delete(int userAdminID)
        {
            DbTransaction transaction = null;
            Message msg = null;
            try
            {
                dbContext.Connection.Open();
                transaction = dbContext.Connection.BeginTransaction();
                dbContext.Transaction = transaction;
                List<UserConfig> userConfigs = GetListUserConfig(userAdminID);
                if (userConfigs != null)
                {
                    dbContext.UserConfigs.DeleteAllOnSubmit(userConfigs);
                    dbContext.SubmitChanges();
                }
                
                transaction.Commit();
                msg = new Message(MessageConstants.I0001, MessageType.Info, "", "updated");
            }
            catch (Exception exception)
            {
                if (transaction != null) transaction.Rollback();
                msg = new Message(MessageConstants.E0007, MessageType.Error);
            }
            return msg;
        }

        /// <summary>
        /// get employee prefix like tri.mao from office email and then get userAdminID from this prefix
        /// </summary>
        /// <param name="userAdmin"></param>
        /// <returns></returns>
        public UserConfig GetByUsername(string userId)
        {
            var employeeName = (from employee in dbContext.Employees where employee.ID.Contains(userId.TrimStart()) select employee).FirstOrDefault();


            string userAdmins = employeeName.OfficeEmail.Split('@')[0];

            //UserConfig userConfig = dbContext.UserConfigs.Where(p => p.UserAdminID == userAdmins && p.ModuleId == 2).FirstOrDefault();
            
            return null;
        }
    }
}