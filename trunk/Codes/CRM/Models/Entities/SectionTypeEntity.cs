using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CRM.Models.Entities
{
    public class SectionTypeEntity
    {
        private int _ID;
        private string _SectionTypeName;
        private string _Description;
        public int ID
		{
			get
			{
				return this._ID;
			}
			set
			{
				if ((this._ID != value))
				{
					this._ID = value;
				}
			}
		}

        public string SectionTypeName
		{
			get
			{
                return this._SectionTypeName;
			}
			set
			{
                if ((this._SectionTypeName != value))
				{
                    this._SectionTypeName = value;
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
    }
}