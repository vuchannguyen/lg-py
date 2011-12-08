using System;
using System.Collections.Generic;
using System.Text;

namespace DTO
{
    public class IDV_DTO
    {
        #region Attributes & Properties
        private string ma;

        public string Ma
        {
            get { return ma; }
            set { ma = value; }
        }

        private string iDV;

        public string IDV
        {
            get { return iDV; }
            set { iDV = value; }
        }

        private string dienGiai;

        public string DienGiai
        {
            get { return dienGiai; }
            set { dienGiai = value; }
        }

        private string matKhau;

        public string MatKhau
        {
            get { return matKhau; }
            set { matKhau = value; }
        }
        #endregion



        #region Default Contructor
        public IDV_DTO()
        {
            ma = string.Empty;
            iDV = string.Empty;
            dienGiai = string.Empty;
            matKhau = string.Empty;
        }
        #endregion
    }
}
