using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CRM.Models.Entities
{
    public class AssetCategoryEntitiy
    {
        private int _Id;
        private string _Name;
        private string _Description;
        private int? _ParentId;
        private string _ParentName;
        private bool _DeleteFlag;
        private string _CreatedBy;
        private DateTime _CreateDate;
        private string _UpdatedBy;
        private DateTime _UpdateDate;

        public int Id
		{
			get
			{
                return this._Id;
			}
			set
			{
                if ((this._Id != value))
				{
                    this._Id = value;
				}
			}
		}

        public string Name
		{
			get
			{
                return this._Name;
			}
			set
			{
                if ((this._Name != value))
				{
                    this._Name = value;
				}
			}
		}

        public string Description
        {
            get
            {
                return this._Description;
            }
            set
            {
                if ((this._Description != value))
                {
                    this._Description = value;
                }
            }
        }

        public int? ParentId
        {
            get
            {
                return this._ParentId;
            }
            set
            {
                if ((this._ParentId != value))
                {
                    this._ParentId = value;
                }
            }
        }

        public string ParentName
        {
            get
            {
                return this._ParentName;
            }
            set
            {
                if ((this._ParentName != value))
                {
                    this._ParentName = value;
                }
            }
        }

        public bool DeleteFlag
        {
            get
            {
                return this._DeleteFlag;
            }
            set
            {
                if ((this._DeleteFlag != value))
                {
                    this._DeleteFlag = value;
                }
            }
        }

        public string CreatedBy
        {
            get
            {
                return this._CreatedBy;
            }
            set
            {
                if ((this._CreatedBy != value))
                {
                    this._CreatedBy = value;
                }
            }
        }

        public DateTime CreateDate
        {
            get
            {
                return this._CreateDate;
            }
            set
            {
                if ((this._CreateDate != value))
                {
                    this._CreateDate = value;
                }
            }
        }

        public string UpdatedBy
        {
            get
            {
                return this._UpdatedBy;
            }
            set
            {
                if ((this._UpdatedBy != value))
                {
                    this._UpdatedBy = value;
                }
            }
        }

        public DateTime UpdateDate
        {
            get
            {
                return this._UpdateDate;
            }
            set
            {
                if ((this._UpdateDate != value))
                {
                    this._UpdateDate = value;
                }
            }
        }
    }
}