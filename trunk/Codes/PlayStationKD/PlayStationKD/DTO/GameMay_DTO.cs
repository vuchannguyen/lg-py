using System;
using System.Collections.Generic;
using System.Text;

namespace DTO
{
    public class GameMay_DTO
    {
        #region Attributes
        private String _maGame;
        private String _maMay;
        #endregion

        #region Properties
        public String MaGame
        {
            get { return _maGame; }
            set { _maGame = value; }
        }

        public String MaMay
        {
            get { return _maMay; }
            set { _maMay = value; }
        }
        #endregion

        #region Default Contructor
        public GameMay_DTO()
        {
            _maGame = String.Empty;
            _maMay = String.Empty;
        }
        #endregion
    }
}
