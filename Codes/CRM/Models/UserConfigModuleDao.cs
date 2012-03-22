using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CRM.Models
{
    public class UserConfigModuleDao : BaseDao
    {
        public List<UserConfigModule> GetAllUserConfigModule(int userAdminId)
        {
            return (from userConfigModule in dbContext.UserConfigModules select userConfigModule).ToList();
        }
    }
}