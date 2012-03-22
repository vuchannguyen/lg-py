using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CRM.Models.Entities
{
    public class AssetPropertyEntitiy
    {
        private long _Id;
        private string _Name;
        private string _MasterData;
        private int _CategoryId;
        private string _CategoryName;
        private int? _ParentCategoryId;
        private string _ParentCategoryName;
        private int _DisplayOrder;
        private bool _DeleteFlag;
        private string _CreatedBy;
        private DateTime _CreateDate;
        private string _UpdatedBy;
        private DateTime _UpdateDate;

        public long Id
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

        public string MasterData
        {
            get
            {
                return this._MasterData;
            }
            set
            {
                if ((this._MasterData != value))
                {
                    this._MasterData = value;
                }
            }
        }

        public int CategoryId
        {
            get
            {
                return this._CategoryId;
            }
            set
            {
                if ((this._CategoryId != value))
                {
                    this._CategoryId = value;
                }
            }
        }

        public string CategoryName
        {
            get
            {
                return this._CategoryName;
            }
            set
            {
                if ((this._CategoryName != value))
                {
                    this._CategoryName = value;
                }
            }
        }

        public int? ParentCategoryId
        {
            get
            {
                return this._ParentCategoryId;
            }
            set
            {
                if ((this._ParentCategoryId != value))
                {
                    this._ParentCategoryId = value;
                }
            }
        }

        public string ParentCategoryName
        {
            get
            {
                return this._ParentCategoryName;
            }
            set
            {
                if ((this._ParentCategoryName != value))
                {
                    this._ParentCategoryName = value;
                }
            }
        }

        public int DisplayOrder
        {
            get
            {
                return this._DisplayOrder;
            }
            set
            {
                if ((this._DisplayOrder != value))
                {
                    this._DisplayOrder = value;
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