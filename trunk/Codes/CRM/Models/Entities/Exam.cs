using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CRM.Models.Entities
{
    public class Exam
    {
        private int _ID;

        private LOT_ExamQuestion _ExamQuestion;
		
		private string _Title;
		
		private byte _ExamType;
		
		private System.DateTime _ExamDate;
		
		private string _MarkStatus;
		
		private string _ProgramingMarkStatus;

        private bool _DeleteFlag;
		


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

        public LOT_ExamQuestion ExamQuestion
        {
            get
            {
                return this._ExamQuestion;
            }
            set
            {
                if ((this._ExamQuestion != value))
                {
                    this._ExamQuestion = value;
                }
            }
        }
		
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
		
		public byte ExamType
		{
			get
			{
				return this._ExamType;
			}
			set
			{
				if ((this._ExamType != value))
				{
					this._ExamType = value;
				}
			}
		}

		public System.DateTime ExamDate
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
		
		public string MarkStatus
		{
			get
			{
				return this._MarkStatus;
			}
			set
			{
				if ((this._MarkStatus != value))
				{
					this._MarkStatus = value;
				}
			}
		}
		
		public string ProgramingMarkStatus
		{
			get
			{
				return this._ProgramingMarkStatus;
			}
			set
			{
				if ((this._ProgramingMarkStatus != value))
				{
					this._ProgramingMarkStatus = value;
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
    }
}