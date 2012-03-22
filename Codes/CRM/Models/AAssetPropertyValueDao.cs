using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CRM.Library.Common;
using System.Data.Common;
using CRM.Models.Entities;
using System.Web.Mvc;

namespace CRM.Models
{
    public class AAssetPropertyValueDao : CustomBaseDao<A_AssetPropertyValue>
    {
        public A_AssetPropertyValue GetById(long assetPropertyValueId)
        {
            return dbContext.A_AssetPropertyValues.Where(a => a.ID == assetPropertyValueId).FirstOrDefault();
        }
        
        public A_AssetPropertyValue GetByAssetIdAndPropertyId(long assetId,long propertyId)
        {
            return dbContext.A_AssetPropertyValues.Where(a => a.PropertyId == propertyId && a.AssetId == assetId).FirstOrDefault();
        }

        public List<A_AssetPropertyValue> GetListByAssetId(long assetId)
        {
            return dbContext.A_AssetPropertyValues.Where(a => a.AssetId == assetId).ToList<A_AssetPropertyValue>();
        }
    }
}