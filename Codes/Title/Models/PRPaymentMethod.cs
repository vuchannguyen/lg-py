using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CRM.Library.Common;

namespace CRM.Models
{
    public class PRPaymentMethodDao : BaseDao
    {
        #region Public methods

        /// <summary>
        /// Get By Id
        /// </summary>
        /// <param name="groupId"></param>
        /// <returns></returns>
        public List<PR_PaymentMethod> GetList()
        {
            return dbContext.PR_PaymentMethods.ToList<PR_PaymentMethod>();
        }
        public PR_PaymentMethod GetById(int id)
        {
            return dbContext.PR_PaymentMethods.FirstOrDefault(p=>p.ID==id);
        }
        #endregion
    }    
}