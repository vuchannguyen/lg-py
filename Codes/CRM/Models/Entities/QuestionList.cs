using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CRM.Models.Entities
{
    public class QuestionList
    {
        private int _ID;

        private string _QuestionContent;

        private int _SectionId;

        private string _SectionName;

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

        public string QuestionContent
        {
            get
            {
                return this._QuestionContent;
            }
            set
            {
                if ((this._QuestionContent != value))
                {
                    this._QuestionContent = value;
                }
            }
        }

        public int SectionId
        {
            get
            {
                return this._SectionId;
            }
            set
            {
                if ((this._SectionId != value))
                {
                    this._SectionId = value;
                }
            }
        }

        public string SectionName
        {
            get
            {
                return this._SectionName;
            }
            set
            {
                if ((this._SectionName != value))
                {
                    this._SectionName = value;
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