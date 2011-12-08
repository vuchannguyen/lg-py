using System;
using System.Collections.Generic;
using System.Text;

namespace DTO
{
    public class SuKien_DTO
    {
        #region Attributes & Properties
        private string ma;

        public string Ma
        {
            get { return ma; }
            set { ma = value; }
        }

        private string avatar;

        public string Avatar
        {
            get { return avatar; }
            set { avatar = value; }
        }

        private string iDS;

        public string IDS
        {
            get { return iDS; }
            set { iDS = value; }
        }

        private string ten;

        public string Ten
        {
            get { return ten; }
            set { ten = value; }
        }

        private string diaDiem;

        public string DiaDiem
        {
            get { return diaDiem; }
            set { diaDiem = value; }
        }

        private string donViToChuc;

        public string DonViToChuc
        {
            get { return donViToChuc; }
            set { donViToChuc = value; }
        }

        private string nhomLoaiHinh;

        public string NhomLoaiHinh
        {
            get { return nhomLoaiHinh; }
            set { nhomLoaiHinh = value; }
        }

        private string loaiHinh;

        public string LoaiHinh
        {
            get { return loaiHinh; }
            set { loaiHinh = value; }
        }

        private string nganh;

        public string Nganh
        {
            get { return nganh; }
            set { nganh = value; }
        }

        private DateTime khaiMac;

        public DateTime KhaiMac
        {
            get { return khaiMac; }
            set { khaiMac = value; }
        }

        private DateTime beMac;

        public DateTime BeMac
        {
            get { return beMac; }
            set { beMac = value; }
        }

        private string moTa;

        public string MoTa
        {
            get { return moTa; }
            set { moTa = value; }
        }
        #endregion



        #region Default Contructor
        /// <summary>
        /// Default Contructor
        /// </summary>
        public SuKien_DTO()
        {
            ma = string.Empty;
            avatar = string.Empty;
            iDS = string.Empty;
            ten = string.Empty;
            diaDiem = string.Empty;
            donViToChuc = string.Empty;
            nhomLoaiHinh = string.Empty;
            loaiHinh = string.Empty;
            khaiMac = new DateTime(2000, 1, 1);
            beMac = new DateTime(2000, 1, 1);
            moTa = string.Empty;
        }
        #endregion
    }
}
