using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CRM.Models.Entities
{
    public class EmployeeEntity
    {
        private string _Id;
        private string _FullName;
        private int _DepartmentId;
        private string _DepartmentName;

        public string Id
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

        public string FullName
		{
			get
			{
                return this._FullName;
			}
			set
			{
                if ((this._FullName != value))
				{
                    this._FullName = value;
				}
			}
		}

        public int DepartmentId
        {
            get
            {
                return this._DepartmentId;
            }
            set
            {
                if ((this._DepartmentId != value))
                {
                    this._DepartmentId = value;
                }
            }
        }

        public string DepartmentName
        {
            get
            {
                return this._DepartmentName;
            }
            set
            {
                if ((this._DepartmentName != value))
                {
                    this._DepartmentName = value;
                }
            }
        }
    }
}