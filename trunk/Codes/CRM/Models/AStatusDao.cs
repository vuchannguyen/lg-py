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
    public class AStatusDao : CustomBaseDao<A_Status>
    {
        /// <summary>
        /// (29/2/2012) Linh.Le: Get Asset Status List.
        /// </summary>
        /// <param name="exStatusIds">exclude list of status in Status SelectList</param>
        /// <returns></returns>
        public List<A_Status> GetAssetStatusList(List<int> exStatusIds = null)
        {
            if (exStatusIds!=null)
                return dbContext.A_Status.Where(s=>!exStatusIds.Contains(s.ID)).ToList();
            else
                return dbContext.A_Status.ToList();
        }

        /// <summary>
        /// (29/2/2012) Linh.Le: Get Non-Parent Asset Status SelectList.
        /// </summary>
        /// <param name="selectedValue">select value in Status SelectList</param>
        /// <param name="exStatusIds">exclude list of status in Status SelectList</param>
        /// <returns></returns>
        public List<SelectListItem> GetAssetStatusSelectList(object selectedValue = null, List<int> exStatusIds = null)
        {
            List<A_Status> list = GetAssetStatusList(exStatusIds);

            List<SelectListItem> assetStatusList = new List<SelectListItem>();
            foreach (A_Status obj in list)
            {
                SelectListItem item = new SelectListItem();
                item.Value = obj.ID.ToString();
                item.Text = HttpUtility.HtmlDecode(obj.Name);
                if (item.Value == ConvertUtil.ConvertToString(selectedValue))
                    item.Selected = true;
                assetStatusList.Add(item);
            }
            return assetStatusList.ToList();
        }
    }
}