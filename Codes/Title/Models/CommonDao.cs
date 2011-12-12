using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Web;
using System.Data.Linq;
using System.Data.Common;
using CRM.Library.Common;

namespace CRM.Models
{
    public class CommonDao : BaseDao
    {
        public IList<JobTitleLevel> GetJobTitleList()
        {
            return dbContext.JobTitleLevels.Where(p => p.DeleteFlag == false).OrderBy(p=>p.DisplayName).ToList<JobTitleLevel>();
        }

        public IList<JobTitleLevel> GetJobTitleList(int departId)
        {
            return dbContext.JobTitleLevels.Where(p => p.JobTitle != null && p.JobTitle.DepartmentId == departId
                && p.DeleteFlag == false).OrderBy(p => p.DisplayName).ToList<JobTitleLevel>();
        }

        public List<WFRole> GetRoleListForApproval(int WF)
        {
            List<WFRole> list = new List<WFRole>();
            var entities = from f in dbContext.WFRoles 
                           where f.WFID == WF && f.ID != Constants.PR_PURCHASING_ID && f.ID != Constants.PR_REQUESTOR_ID && f.ID != Constants.PR_SR_MANAGER_ID
                           orderby f.ID ascending

                           select new
                           {
                               ID = f.ID,
                               Name = f.Name
                           };

            foreach (var e in entities)
            {
                WFRole role = new WFRole();
                role.ID = e.ID;
                role.Name = e.Name;
                list.Add(role);
            }

            return list;
        }

        public List<WFRole> GetRoleListForApprovalUS(int WF)
        {
            List<WFRole> list = new List<WFRole>();
            var entities = from f in dbContext.WFRoles
                           where f.WFID == WF && f.ID != Constants.PR_PURCHASING_ID_US && f.ID != Constants.PR_REQUESTOR_ID_US && f.ID != Constants.PR_DEPARTMENT_HEAD
                           orderby f.ID ascending

                           select new
                           {
                               ID = f.ID,
                               Name = f.Name
                           };

            foreach (var e in entities)
            {
                WFRole role = new WFRole();
                role.ID = e.ID;
                role.Name = e.Name;
                list.Add(role);
            }

            return list;
        }

        public List<WFRole> GetRoleList(int userAdminId, int WF)
        {
            List<WFRole> list = new List<WFRole>();
            var entities = from e in dbContext.UserAdmin_WFRoles
                           join f in dbContext.WFRoles on e.WFRoleID equals f.ID
                           where e.UserAdminId == userAdminId && e.IsActive == true && f.WFID == WF
                           orderby f.ID ascending

                           select new
                           {
                               ID = e.WFRoleID,
                               Name = f.Name
                           };

            foreach (var e in entities)
            {
                WFRole role = new WFRole();
                role.ID = e.ID;
                role.Name = e.Name;
                list.Add(role);
            }

            return list;
        }

        public List<UserAdmin> GetUserAdminByRole(int WF)
        {
            List<UserAdmin> list = new List<UserAdmin>();
            var entities = from e in dbContext.UserAdmin_WFRoles
                           join f in dbContext.UserAdmins on e.UserAdminId equals f.UserAdminId
                           where e.IsActive == true && e.WFRoleID == WF && f.DeleteFlag == false
                           orderby f.UserName ascending

                           select new
                           {
                               ID = f.UserAdminId,
                               Name = f.UserName
                           };

            foreach (var e in entities)
            {
                UserAdmin admin = new UserAdmin();
                admin.UserAdminId = e.ID;
                admin.UserName = e.Name;
                list.Add(admin);
            }

            return list;
        }

        public List<int> GetRoleListInt(int userAdminId, int WF)
        {            
            List<WFRole> list = GetRoleList(userAdminId, WF);
            return list.Select(c => c.ID).ToList();
        }
    }
}