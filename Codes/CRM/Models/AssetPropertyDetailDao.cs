using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CRM.Library.Common;
using System.Data.Common;
using System.Text.RegularExpressions;

namespace CRM.Models
{
    public class AssetPropertyDetailDao : BaseDao
    {
        public AssetPropertyDetail GetById(string id)
        {
            return dbContext.AssetPropertyDetails.Where(c => c.ID.Equals(id)).FirstOrDefault<AssetPropertyDetail>();
        }

        public AssetPropertyDetail GetById(string assetId, string propertyId)
        {
            return dbContext.AssetPropertyDetails.Where(c => c.AssetId.Equals(assetId) && c.PropertyId.Equals(propertyId)).FirstOrDefault<AssetPropertyDetail>();
        }

        public List<AssetPropertyDetail> GetByPropertyId(string propertyId)
        {
            return dbContext.AssetPropertyDetails.Where(c => c.PropertyId.Equals(propertyId)).ToList<AssetPropertyDetail>();
        }

        public List<AssetPropertyDetail> GetByAssetId(string AssetId)
        {
            return dbContext.AssetPropertyDetails.Where(c => c.AssetId.Equals(AssetId)).ToList<AssetPropertyDetail>();
        }

        public bool Insert(AssetPropertyDetail objUI)
        {
            bool isSuccess=true;
            try
            {
                dbContext.AssetPropertyDetails.InsertOnSubmit(objUI);
                dbContext.SubmitChanges();
            }
            catch (Exception)
            {
                isSuccess = false;
            }
            return isSuccess;
        }

        public bool Update(AssetPropertyDetail objUI)
        {
            bool isSuccess = true;
            try
            {
                AssetPropertyDetail objDb = GetById(objUI.ID.ToString());

                if (objDb != null)
                {
                    Update(objUI, objDb);
                }
                else
                {
                    isSuccess = false;
                }
            }
            catch (Exception)
            {
                isSuccess = false;
            }

            return isSuccess;
        }

        private void Update(AssetPropertyDetail objUI, AssetPropertyDetail objDb)
        {
            objDb.AssetId = objUI.AssetId;
            objDb.PropertyId = objUI.PropertyId;
            objDb.Value = objUI.Value;
            dbContext.SubmitChanges();
        }

        private void Delete(AssetPropertyDetail objUI)
        {
            if (objUI != null)
            {
                AssetPropertyDetail objDb = GetById(objUI.ID.ToString());
                if (objDb != null)
                {
                    dbContext.AssetPropertyDetails.DeleteOnSubmit(objDb);
                }
            }
        }

        public bool DeleteList(List<AssetPropertyDetail> assPropDetailList)
        {
            bool isSuccess = true;
            DbTransaction trans = null;
            try
            {
                dbContext.Connection.Open();
                trans = dbContext.Connection.BeginTransaction();
                dbContext.Transaction = trans;

                if (assPropDetailList.Count != 0)
                {
                    /*ids = ids.TrimEnd(',');
                    string[] idArr = ids.Split(',');
                    int total = idArr.Count();
                    foreach (string id in idArr)
                    {
                        string empID = id;
                        AssetPropertyDetail assMas = GetById(empID);
                        if (assMas != null)
                        {
                            Delete(assMas);
                        }
                        else
                        {
                            total--;
                        }
                    }*/
                    foreach (AssetPropertyDetail a in assPropDetailList)
                    {
                        Delete(a);
                    }
                    trans.Commit();
                }
            }
            catch
            {
                if (trans != null) trans.Rollback();
                    isSuccess = false;
            }
            return isSuccess;
        }
    }
}
