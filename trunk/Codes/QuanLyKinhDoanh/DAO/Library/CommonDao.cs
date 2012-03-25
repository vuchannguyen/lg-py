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
