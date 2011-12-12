using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.DirectoryServices;

namespace CRM.Models
{
    public class AuthenticateDao : BaseDao
    {
        public UserAdmin GetUserById(int userId)
        {
            return dbContext.UserAdmins.FirstOrDefault(usr => usr.UserAdminId == userId && usr.DeleteFlag == false);
        }

        public bool IsUserInGroup(string userName)
        {
            bool isUserInGroup = false;
            List<User_Group> list = dbContext.User_Groups.Where(q => q.UserAdmin.UserName.Equals(userName) && q.UserAdmin.DeleteFlag == false && q.IsActive == true).ToList<User_Group>();
            if (list.Count > 0)
            {
                isUserInGroup = true;
            }
            return isUserInGroup;
        }

        public UserAdmin GetUserByName(string username)
        {
            return dbContext.UserAdmins.FirstOrDefault(user => user.UserName.Equals(username) && user.DeleteFlag == false);
        }

        /// <summary>
        /// Check in Active Directory
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public bool CheckExistInAD(string username, string password)
        {
            bool isOK = false;
            if ((!string.IsNullOrEmpty(username)) && (!string.IsNullOrEmpty(password)))
            {
                DirectoryEntry dentry = new DirectoryEntry("LDAP://logigear.com", username, password);
                DirectorySearcher dsearch = new DirectorySearcher(dentry);
                dsearch.Filter = "(SAMAccountName=" + username + ")";
                dsearch.PropertiesToLoad.AddRange(new string[] { "name", "pwdLastSet", "SAMAccountName" });
                try
                {
                    SearchResult result = dsearch.FindOne();
                    if (result != null)
                    {
                        isOK = true;
                    }
                }
                catch
                { }

                //#region New Source Code
                //DirectorySearcher ds = new DirectorySearcher(dentry);
                //ds.Filter = ("(&(objectclass=user)(objectcategory=person)(SAMAccountName=" + username + "))");

                //ds.SearchScope = SearchScope.Subtree;

                //SearchResult results = ds.FindOne();

                //if (results != null)
                //{
                //    isOK = true;
                //}
                //#endregion
            }
            return isOK;
        }
        /// <summary>
        /// Check in Active Directory
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public bool CheckExistInAD(string username)
        {
            if (!string.IsNullOrEmpty(username))
            {
                DirectoryEntry dentry = new DirectoryEntry("LDAP://logigear.com");
                DirectorySearcher deSearch = new DirectorySearcher(dentry);

                deSearch.Filter = "(SAMAccountName=" + username + ")";

                SearchResult result = deSearch.FindOne();

                if (result == null)
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// Check Permission
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="moduleId"></param>
        /// <param name="permissionIds"></param>
        /// <returns></returns>
        public bool CheckPermission(int userId, byte moduleId, int[] permissionIds)
        { 
            var user = GetUserById(userId);

            if (user == null)
            {
                return false;
            }

            List<int> groupIds = user.User_Groups.Where(q => q.IsActive).Select(c => c.GroupId).ToList<int>();            

            IEnumerable<GroupPermission> userModFuncs = dbContext.GroupPermissions.Where(c => groupIds.Contains(c.GroupId) && c.ModuleId == moduleId && permissionIds.Contains(c.PermissionId) && c.Group.IsActive == true );

            if (userModFuncs.Count() == 0)
            {
                return false;
            }

            return true;             
        }

        public bool CheckUserHasPermission(string userName, int permission)
        {
            return dbContext.func_CheckUserHasPermissionModule(userName, permission).Value;
        }
    }
}