using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;



namespace CRM.Models
{
    public class AssetStatusDao : BaseDao
    {
        public List<AssetStatus> GetAssetStatusList()
        {
            return dbContext.AssetStatus.ToList();
        }
        
    }
}