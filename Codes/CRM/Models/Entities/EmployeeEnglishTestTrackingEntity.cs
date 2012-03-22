using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CRM.Models.Entities
{
    public class EmployeeEnglishTestTrackingEntity
    {
        private string _Title;
        private DateTime _ExamDate;
        private double? _Score;
        private double? _Level;
        private string _Remark;

        public string Title
		{
			get
			{
				return this._Title;
			}
			set
			{
				if ((this._Title != value))
				{
					this._Title = value;
				}
			}
		}

        public DateTime ExamDate
		{
			get
			{
                return this._ExamDate;
			}
			set
			{
                if ((this._ExamDate != value))
				{
                    this._ExamDate = value;
				}
			}
		}

        public double? Score
        {
            get
            {
                return this._Score;
            }
            set
            {
                if ((this._Score != value))
                {
                    this._Score = value;
                }
            }
        }

        public double? Level
        {
            get
            {
                return this._Level;
            }
            set
            {
                if ((this._Level != value))
                {
                    this._Level = value;
                }
            }
        }

        public string Remark
        {
            get
            {
                return this._Remark;
            }
            set
            {
                if ((this._Remark != value))
                {
                    this._Remark = value;
                }
            }
        }
    }
}