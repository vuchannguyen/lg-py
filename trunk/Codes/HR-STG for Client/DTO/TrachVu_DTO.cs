using System;
using System.Collections.Generic;
using System.Text;

namespace DTO
{
    public class TrachVu_DTO
    {
        #region Attributes & Properties
        private string ma;

        public string Ma
        {
            get { return ma; }
            set { ma = value; }
        }

        private string maDoiTuong;

        public string MaDoiTuong
        {
            get { return maDoiTuong; }
            set { maDoiTuong = value; }
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
        public TrachVu_DTO()
        {
            ma = string.Empty;
            maDoiTuong = string.Empty;
            ten = string.Empty;
            moTa = string.Empty;
        }
        #endregion
    }
}
