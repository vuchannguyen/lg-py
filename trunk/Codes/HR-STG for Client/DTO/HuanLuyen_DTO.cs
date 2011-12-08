using System;
using System.Collections.Generic;
using System.Text;

namespace DTO
{
    public class HuanLuyen_DTO
    {
        #region Attributes & Properties
        private int ma;

        public int Ma
        {
            get { return ma; }
            set { ma = value; }
        }

        private string nganh;

        public string Nganh
        {
            get { return nganh; }
            set { nganh = value; }
        }

        private string khoa;

        public string Khoa
        {
            get { return khoa; }
            set { khoa = value; }
        }

        private string tenKhoa;

        public string TenKhoa
        {
            get { return tenKhoa; }
            set { tenKhoa = value; }
        }

        private string khoaTruong;

        public string KhoaTruong
        {
            get { return khoaTruong; }
            set { khoaTruong = value; }
        }

        private DateTime nam;

        public DateTime Nam
        {
            get { return nam; }
            set { nam = value; }
        }

        private string mHL;

        public string MHL
        {
            get { return mHL; }
            set { mHL = value; }
        }

        private string tinhTrang;

        public string TinhTrang
        {
            get { return tinhTrang; }
            set { tinhTrang = value; }
        }
        #endregion



        #region Default Contructor
        public HuanLuyen_DTO()
        {
            //ma = 0;
            nganh = string.Empty;
            khoa = string.Empty;
            tenKhoa = string.Empty;
            Khoa = string.Empty;
            nam = new DateTime(2000, 1, 1);
            mHL = string.Empty;
            tinhTrang = string.Empty;
        }
        #endregion
    }
}
