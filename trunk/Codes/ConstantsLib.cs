using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Fusion.Libraries
{
    public class ConstantsLib
    {
        public const string Website_URL = "http://knydev.fusionproductions.com:81/kny.web/";

        public enum JsMouseEvent
        {
            onMouseOver,
            onMouseUp,
            onMouseDown,
            onMouseEnter,
            onBlur
        }
        public enum JsDropDownListEvent
        {
            onchange
        }

        //IE : Internet Explorer ; FF : FireFox
        public const string Browser_Default = "FF";
        public const string Format_Date_Unique = "MMddyyhhmmss";
        //public const string ConnectionString_Default = "Data Source=;Initial Catalog=;User Id=;Password=;";
        //public const string Username_Login_Value = "";
        //public const string Password_Login_Value = "";

        //step 1
        public const string FirstName = "Tuc";
        public const string LastName = "Nguyen";
        public const string Email = "tuc.nguyen@logigear.com";
        public const string Password = "test";
        public const string QuestionAnswer = "Viet Nam";

        //step 3
        public const string StreetAddress = "Phan Xich Long";
        public const string City = "HCM";
        public const string ZipCode = "10001";


        public const string PEOPLE_SEARCH_OPTION_ID = "";
        public const string PEOPLE_SEARCH_TEXTBOX_ID = "";
        public const string PEOPLE_SEARCH_TEXTBOX_VALUE = "Peter";
        public const string PEOPLE_SEARCH_RADIO_ID = "";
        public const string PEOPLE_SEARCH_OPTION_XPATH = "";
        public const string PEOPLE_SEARCH__BUTTON_ID = "";

        public const string EVENT_SEARCH_OPTION_ID = "";
        public const string EVENT_SEARCH_TEXTBOX_ID = "";
        public const string EVENT_SEARCH_TEXTBOX_VALUE = "";
        public const string EVENT_SEARCH_RADIO_ID = "";
        public const string EVENT_SEARCH_OPTION_XPATH = "";
        public const string EVENT_SEARCH_BUTTON_ID = "";

        public const string FILTER_SEARCH_OPTION_ID = "";
        public const string FILTER_SEARCH_TEXTBOX_ID = "";
        public const string FILTER_SEARCH_TEXTBOX_VALUE = "New York";
        public const string FILTER_SEARCH_RADIO_ID = "";
        public const string FILTER_SEARCH_OPTION_XPATH = "";
        public const string SEARCH_OPTION_ID = "";

        public const int TimeOut = 240;

        public enum LanguageType:int
        { 
            English=1,
            Brazilian=2,
            French=3,
            Italian=4,
            Japanese=5,
            Chinese_Simplified=6,
            Chinese_Traditional=7,
            Korean=8,
            Turkish=9
        }
        
    }
}
