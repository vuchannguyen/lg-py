using System;
using System.Collections.Generic;
using System.Text;

namespace DTO
{
    public class DanhSachGame_DTO
    {
        #region Attributes
        private String _ma;
        private String _ten;
        private int _nguoiChoi;
        private String _theLoai;
        private String _maMay;
        private String _ghiChu;
        #endregion

        #region Properties
        public String Ma
        {
          get { return _ma; }
          set { _ma = value; }
        }

        public String Ten
        {
          get { return _ten; }
          set { _ten = value; }
        } 

        public int NguoiChoi
        {
          get { return _nguoiChoi; }
          set { _nguoiChoi = value; }
        }

        public String TheLoai
        {
          get { return _theLoai; }
          set { _theLoai = value; }
        }

        public String MaMay
        {
            get { return _maMay; }
            set { _maMay = value; }
        }

        public String GhiChu
        {
          get { return _ghiChu; }
          set { _ghiChu = value; }
        } 
        #endregion

        #region Default Contructor
        public DanhSachGame_DTO()
        {
            _ma = String.Empty;
            _ten = String.Empty;
            _nguoiChoi = 0;
            _theLoai = String.Empty;
            _maMay = String.Empty;
            _ghiChu = String.Empty;
        }
        #endregion
    }
}
