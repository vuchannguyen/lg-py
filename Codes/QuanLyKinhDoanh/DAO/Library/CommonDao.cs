using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Library
{
    public class CommonDao
    {
        public static string SORT_ASCENDING = "asc";
        public static string SORT_DESCENDING = "desc";
        public static string SEPERATE_STRING = ", ";

        public const int ID_TYPE_MUA = 1;
        public const int ID_TYPE_BAN = 2;
        public const int ID_TYPE_THU = 3;
        public const int ID_TYPE_CHI = 4;
        public const int ID_TYPE_MUA_CHI = 5;
        public const int ID_TYPE_BAN_THU = 6;

        public const int ID_STATUS_DONE = 1;
        public const int ID_STATUS_DEBT = 2;

        public const string DEFAULT_TYPE_DAY = "Ngày";
        public const string DEFAULT_TYPE_MONTH = "Tháng";
        public const string DEFAULT_TYPE_YEAR = "Năm";

        public const string DEFAULT_STATUS_SP_ALL = "Tất cả";
        public const string DEFAULT_STATUS_SP_NOT_ZERO = "Còn";
        public const string DEFAULT_STATUS_SP_ZERO = "Hết";

        public static string GetFilterText(string text)
        {
            string filter = string.Empty;

            if (!string.IsNullOrEmpty(text))
            {
                filter = text.Replace("%", "[%]");
                filter = filter.Replace("[", "[[]");
                filter = filter.Replace("_", "[_]");
                filter = "%" + Regex.Replace(filter.Trim(), @"\s+", "%") + "%";
            }

            return filter;
        }

        public static int ConvertStringToInt(string content)
        {
            int result = 0;
            int.TryParse(content, out result);

            return result;
        }
    }
}
