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

        //public const string DEFAULT_STATUS_SP_ALL = "Tất cả";
        public const string DEFAULT_STATUS_SP_NOT_ZERO = "Còn";
        public const string DEFAULT_STATUS_SP_ZERO = "Hết";

        public const int DEFAULT_STATUS_USED_DATE_BEFORE = 0;
        public const int DEFAULT_STATUS_USED_DATE_NEAR = 1;
        public const int DEFAULT_STATUS_USED_DATE_END = 2;

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

        public static int IsExpired(DateTime createDate, int warningDays, int value, string typeOfValue)
        {
            DateTime usedDay = CalculateExpiredDate(createDate, value, typeOfValue);

            if (DateTime.Now.AddDays(warningDays) < usedDay)
            {
                return DEFAULT_STATUS_USED_DATE_BEFORE;
            }

            if (DateTime.Now.AddDays(warningDays) >= usedDay &&
                DateTime.Now.AddDays(-1) <= usedDay)
            {
                return DEFAULT_STATUS_USED_DATE_NEAR;
            }

            if (DateTime.Now.AddDays(-1) > usedDay)
            {
                return DEFAULT_STATUS_USED_DATE_END;
            }

            return 0;
        }

        public static DateTime CalculateExpiredDate(DateTime createDate, int value, string typeOfValue)
        {
            DateTime usedDay = createDate;

            switch (typeOfValue)
            {
                case DEFAULT_TYPE_DAY:
                    usedDay = createDate.AddDays(value);
                    break;

                case DEFAULT_TYPE_MONTH:
                    usedDay = createDate.AddMonths(value);
                    break;

                case DEFAULT_TYPE_YEAR:
                    usedDay = createDate.AddYears(value);
                    break;

                default:
                    usedDay = createDate.AddDays(value);
                    break;
            }

            return usedDay;
        }
    }
}
