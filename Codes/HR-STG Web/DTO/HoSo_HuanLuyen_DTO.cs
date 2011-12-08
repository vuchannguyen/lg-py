using System;
using System.Collections.Generic;
using System.Text;

namespace DTO
{
    public class HoSo_HuanLuyen_DTO
    {
        #region Attributes & Properties
        private int maHoSo;

        public int MaHoSo
        {
            get { return maHoSo; }
            set { maHoSo = value; }
        }

        private int maHuanLuyen;

        public int MaHuanLuyen
        {
            get { return maHuanLuyen; }
            set { maHuanLuyen = value; }
        }
        #endregion



        #region Default Contructor
        public HoSo_HuanLuyen_DTO()
        {
            maHoSo = 0;
            maHuanLuyen = 0;
        }
        #endregion
    }
}
