using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CRM.Library.Common;

namespace CRM.Models
{
    public class ModulePermissonDao : BaseDao
    {
        /// <summary>
        /// Get List Module by Enum Module
        /// </summary>
        /// <returns></returns>
        public List<ModuleData> GetList()
        {
            List<ModuleData> listEnum = new List<ModuleData>();
            foreach (int id in Enum.GetValues(typeof(Modules)))
            {
                //listEnum.Add(name);
                ModuleData item = new ModuleData();
                item.ID = id;
                item.Name = ((Modules)id).ToString();
                listEnum.Add(item);
            }
            return listEnum;
        }
        public List<sp_GetModulePermissionResult> GetList(int userAdminID)
        {
            return dbContext.sp_GetModulePermission(userAdminID).ToList();
        }
        public List<int> GetAccessibleModules(int userAdminId)
        {
            return dbContext.sp_GetAccessibleModules(userAdminId, (int)Permissions.Read).Select(p=>p.ModuleId).ToList();
        }
        /// <summary>
        /// Get List Action By Module ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<ModulePermission> GetActionByModuleID(int id)
        {
            return dbContext.ModulePermissions.Where(q => q.ModuleId == id).ToList();
        }

        public Message Assign(List<ModulePermission> list, int moduleID)
        {
            Message msg = null;
            try
            {
                dbContext.sp_DeleteModulePermisson(moduleID);
                dbContext.ModulePermissions.InsertAllOnSubmit(list);
                dbContext.SubmitChanges();
                msg = new Message(MessageConstants.I0001, MessageType.Info, "Module " + ((Modules)moduleID).ToString() + "", "update");
            }
            catch (Exception)
            {
                // Show system error
                msg = new Message(MessageConstants.E0007, MessageType.Error);
            }

            return msg;
        }
    }
}