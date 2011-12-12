using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Web;
using System.Data.Linq;
using System.Data.Common;
using System.Web.UI.WebControls;

using CRM.Library.Common;
using CRM.Models.Entities;

namespace CRM.Models
{
    public class GroupPermissionDao : BaseDao
    {      
        #region Public methods

        public GroupPermission GetByGroupIdAndModuleId(int groupId, int moduleId)
        {
            return dbContext.GroupPermissions.Where(c => c.GroupId == groupId && c.ModuleId == moduleId)
                .FirstOrDefault<GroupPermission>();
        }
        public IList<ModulePermission> GetPermissionByModuleId(int moduleId)
        {
            return dbContext.ModulePermissions.Where(c => c.ModuleId == moduleId).ToList<ModulePermission>();
        }      

        public List<ModuleModel> GetModuleList(int groupId)
        {
            IList<GroupPermission> perlist = dbContext.GroupPermissions.Where(c => c.GroupId == groupId)
                .ToList<GroupPermission>(); 

            List<ModuleModel> list = new List<ModuleModel>();            

            List<ListItem> moduleList = PermissionConstants.ListModule;
            List<ListItem> permissionList = PermissionConstants.ListPermission;

            foreach (ListItem mod in moduleList)
            {
                ModuleModel module = new ModuleModel();
                module.ModuleId = int.Parse(mod.Value);
                module.ModuleName = mod.Text;
                IList<ModulePermission> perList = GetPermissionByModuleId(module.ModuleId);
                foreach (ModulePermission per in perList)
                {
                    GroupPermission groupPer = perlist.Where(c => c.ModuleId == module.ModuleId 
                        && c.PermissionId.ToString() == per.PermissionId.ToString()).FirstOrDefault<GroupPermission>();
                    
                    ListItem item = permissionList.Where(c=> c.Value.Equals(per.PermissionId.ToString()))
                        .FirstOrDefault<ListItem>();
                    if (groupPer == null)
                    {
                        module.PermissionIds += item.Value + ",";
                    }
                    else
                    {
                        module.PermissionIds += item.Value + "_true,";
                    }

                    module.PermissionNames += item.Text + ",";
                }

                if (!string.IsNullOrEmpty(module.PermissionIds) && module.PermissionIds.Length > 1)
                {
                    module.PermissionIds = module.PermissionIds.TrimEnd(',');
                }

                if (!string.IsNullOrEmpty(module.PermissionNames) && module.PermissionNames.Length > 1)
                {
                    module.PermissionNames = module.PermissionNames.TrimEnd(',');
                }

                list.Add(module);
            }

            return list;
        }

        public Message AssignPermission(int groupId, List<GroupPermission> groupList)
        {
            Message msg = null;
            DbTransaction trans = null;
            try
            {
                dbContext.Connection.Open();
                trans = dbContext.Connection.BeginTransaction();
                dbContext.Transaction = trans;


                List<GroupPermission> deleteGroup = dbContext.GroupPermissions.Where(c => c.GroupId == groupId)
                    .ToList<GroupPermission>();
                dbContext.GroupPermissions.DeleteAllOnSubmit(deleteGroup);
                dbContext.SubmitChanges();

                foreach (GroupPermission item in groupList)
                {
                    dbContext.GroupPermissions.InsertOnSubmit(item);
                    // Submit changes to dbContext
                    dbContext.SubmitChanges();
                }

                // Show succes message
                msg = new Message(MessageConstants.I0001, MessageType.Info, groupList.Count.ToString() + " permision(s)", "assigned");
                trans.Commit();
            }
            catch (Exception )
            {
                if (trans != null) trans.Rollback();                
                // Show system error
                msg = new Message(MessageConstants.E0007, MessageType.Error);
            }

            return msg;
        }

        #endregion
    }
}