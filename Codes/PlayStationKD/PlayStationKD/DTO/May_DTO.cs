using System;
using System.Collections.Generic;
using System.Text;

namespace DTO
{
    public class May_DTO
    {
        #region Attributes
        private String _ma;
        private String _ghiChu;
        #endregion

        #region Properties
        public String Ma
        {
            get { return _ma; }
            set { _ma = value; }
        }

        public String GhiChu
        {
            get { return _ghiChu; }
            set { _ghiChu = value; }
        }
        #endregion

        #region Default Contructor
        public May_DTO()
        {
            _ma = String.Empty;
            _ghiChu = String.Empty;
        }
        #endregion
    }
}
