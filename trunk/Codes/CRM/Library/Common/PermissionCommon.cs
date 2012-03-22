using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using CRM.Models;

namespace CRM.Library.Common
{
    public class PermissionCommon
    {
        public static  bool IsUserAuthorized(int userId, byte moduleId, int[] permissionIds)
        {
            AuthenticateDao auDao = new AuthenticateDao();            
            return auDao.CheckPermission(userId, moduleId, Array.ConvertAll(permissionIds.ToArray(), arr => (int)arr));

        }

        /// <summary>
        /// Getting List GroupPermission by userId
        /// @author : tai.pham - 08 Mar
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public static List<GroupPermission> GetListPermissionOfUser(int userId)
        {
            AuthenticateDao auDao = new AuthenticateDao();
            return auDao.GetListPermissionOfUser(userId);
        } 
    }
}