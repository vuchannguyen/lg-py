using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CRM.Models.Entities
{
    public class NonEmpCertificationEntity
    {
        private int? id;

        public int? Id
        {
            get { return id; }
            set { id = value; }
        }
        private int? nonEmployeeID;

        public int? NonEmployeeID
        {
            get { return nonEmployeeID; }
            set { nonEmployeeID = value; }
        }
        private int? typeID;

        public int? TypeID
        {
            get { return typeID; }
            set { typeID = value; }
        }
        private double score;

        public double Score
        {
            get { return score; }
            set { score = value; }
        }
        private DateTime? expireDate;

        public DateTime? ExpireDate
        {
            get { return expireDate; }
            set { expireDate = value; }
        }
        private string notes;

        public string Notes
        {
            get { return notes; }
            set { notes = value; }
        }
        private string certificationName;

        public string CertificationName
        {
            get { return certificationName; }
            set { certificationName = value; }
        }
        private string createdBy;

        public string CreatedBy
        {
            get { return createdBy; }
            set { createdBy = value; }
        }
        private string updatedBy;

        public string UpdatedBy
        {
            get { return updatedBy; }
            set { updatedBy = value; }
        }
        private DateTime updateDate;

        public DateTime UpdateDate
        {
            get { return updateDate; }
            set { updateDate = value; }
        }
        private DateTime createDate;

        public DateTime CreateDate
        {
            get { return createDate; }
            set { createDate = value; }
        }
        private bool deleteFlag;

        public bool DeleteFlag
        {
            get { return deleteFlag; }
            set { deleteFlag = value; }
        }

    }
}