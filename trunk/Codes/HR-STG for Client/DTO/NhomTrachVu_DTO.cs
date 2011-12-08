using System;
using System.Collections.Generic;
using System.Text;

namespace DTO
{
    public class NhomTrachVu_DTO
    {
        #region Attributes & Properties
        private string ma;

        public string Ma
        {
            get { return ma; }
            set { ma = value; }
        }

        private string ten;

        public string Ten
        {
            get { return ten; }
            set { ten = value; }
        }

        private string moTa;

        public string MoTa
        {
            get { return moTa; }
            set { moTa = value; }
        }
        #endregion



        #region Default Contructor
        public NhomTrachVu_DTO()
        {
            ma = string.Empty;
            ten = string.Empty;
            moTa = string.Empty;
        }
        #endregion
    }
}
