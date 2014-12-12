#region Imports

using System;
using System.Collections;
using System.Text;
using System.Text.RegularExpressions;
using System.Data;

#endregion

namespace Controller.Common
{
    public class ConvertUtil
    {
        /// <summary>
        /// InputText
        /// </summary>
        /// <param name="inputString"></param>
        /// <param name="maxLength"></param>
        /// <returns></returns>
        public static string InputText(string inputString, int maxLength)
        {
            StringBuilder retVal = new StringBuilder();

            // check incoming parameters for null or blank string
            if ((inputString != null) && (inputString != String.Empty))
            {
                inputString = inputString.Trim();

                //chop the string incase the client-side max length
                //fields are bypassed to prevent buffer over-runs
                if (inputString.Length > maxLength)
                    inputString = inputString.Substring(0, maxLength);

                //convert some harmful symbols incase the regular
                //expression validators are changed
                for (int i = 0; i < inputString.Length; i++)
                {
                    switch (inputString[i])
                    {
                        case '"':
                            retVal.Append("&quot;");
                            break;
                        case '<':
                            retVal.Append("<");
                            break;
                        case '>':
                            retVal.Append("&gt;");
                            break;
                        default:
                            retVal.Append(inputString[i]);
                            break;
                    }
                }

                // Replace single quotes with white space
                retVal.Replace("'", " ");
            }

            return retVal.ToString();
        }

        /// <summary>
        /// CleanText
        /// </summary>
        /// <param name="inputString"></param>
        /// <param name="maxLength"></param>
        /// <returns></returns>
        public static string CleanText(string inputString, int maxLength)
        {
            StringBuilder retVal = new StringBuilder();

            if ((inputString != null) && (inputString != String.Empty))
            {
                inputString = inputString.Trim();

                if (inputString.Length > maxLength)
                {
                    inputString = inputString.Substring(0, maxLength);
                }

                for (int i = 0; i < inputString.Length; i++)
                {
                    switch (inputString[i])
                    {
                        case '"':
                            retVal.Append("&quot;");
                            break;
                        case '&':
                            retVal.Append("&amp;");
                            break;
                        case '<':
                            retVal.Append("&lt;");
                            break;
                        case '>':
                            retVal.Append("&gt;");
                            break;
                        default:
                            retVal.Append(inputString[i]);
                            break;
                    }
                }
                retVal.Replace("'", " ");
            }
            return retVal.ToString();
        }

        /// <summary>
        /// CleanText
        /// </summary>
        /// <param name="inputString"></param>
        /// <returns></returns>
        public static string CleanText(string inputString)
        {
            StringBuilder retVal = new StringBuilder();

            if ((inputString != null) && (inputString != String.Empty))
            {
                inputString = inputString.Trim();

                for (int i = 0; i < inputString.Length; i++)
                {
                    switch (inputString[i])
                    {
                        case '"':
                            retVal.Append("&quot;");
                            break;
                        case '&':
                            retVal.Append("&amp;");
                            break;
                        case '<':
                            retVal.Append("&lt;");
                            break;
                        case '>':
                            retVal.Append("&gt;");
                            break;
                        default:
                            retVal.Append(inputString[i]);
                            break;
                    }
                }
                retVal.Replace("'", " ");
            }

            return retVal.ToString();
        }

        /// <summary>
        /// CleanTextHTML
        /// </summary>
        /// <param name="inputString"></param>
        /// <returns></returns>
        public static string CleanTextHTML(string inputString)
        {
            StringBuilder retVal = new StringBuilder();

            if ((inputString != null) && (inputString != String.Empty))
            {
                inputString = inputString.Trim();

                for (int i = 0; i < inputString.Length; i++)
                {
                    switch (inputString[i])
                    {
                        case '"':
                            retVal.Append("&quot;");
                            break;
                        case '&':
                            retVal.Append("&amp;");
                            break;
                        case '<':
                            retVal.Append("&lt;");
                            break;
                        case '>':
                            retVal.Append("&gt;");
                            break;
                        default:
                            retVal.Append(inputString[i]);
                            break;
                    }
                }
                retVal.Replace("'", "&#39;");
                retVal.Replace("\r\n", "<br />");
            }

            return retVal.ToString();
        }

        /// <summary>
        /// SqlLiteral
        /// </summary>
        /// <param name="inputStr"></param>
        /// <returns></returns>
        public static string SqlLiteral(string inputStr)
        {
            StringBuilder result = new StringBuilder();
            char[] arrChar;
            Boolean mark = false;
            long length = 0;

            if ((inputStr != null) && (!string.Empty.Equals(inputStr)))
            {
                inputStr = inputStr.Trim();
                arrChar = inputStr.ToCharArray();
                length = arrChar.LongLength - 1;
                for (long i = 0; i <= length; i++)
                {
                    switch (arrChar[i])
                    {
                        case '\'':
                            if (result.Length > 0)
                            {
                                if (!mark)
                                {
                                    result.Append("'");
                                }
                                result.Append(" + char(39)");
                            }
                            else
                            {
                                result.Append("'");
                                result.Append(" + char(39)");
                            }

                            if (i == length)
                            {
                                result.Append("+");
                                result.Append(" ");
                                result.Append("N'");
                            }
                            mark = true;
                            break;

                        default:
                            if (mark)
                            {
                                result.Append("+");
                                result.Append(" ");
                                result.Append("N'");
                                mark = false;
                            }
                            result.Append(arrChar[i]);
                            break;
                    }
                }
            }

            return result.ToString();
        }

        /// <summary>
        /// SqlLiteral
        /// </summary>
        /// <param name="inputStr"></param>
        /// <param name="maxLength"></param>
        /// <returns></returns>
        public static string SqlLiteral(string inputStr, int maxLength)
        {
            StringBuilder result = new StringBuilder();
            char[] arrChar;
            Boolean mark = false;
            long length = 0;

            if ((inputStr != null) && (!string.Empty.Equals(inputStr)))
            {
                if (inputStr.Length > maxLength)
                {
                    inputStr = inputStr.Substring(0, maxLength);
                }
                arrChar = inputStr.ToCharArray();
                length = arrChar.LongLength - 1;

                for (long i = 0; i <= length; i++)
                {
                    switch (arrChar[i])
                    {
                        case '\'':
                            if (result.Length > 0)
                            {
                                if (!mark)
                                {
                                    result.Append("'");
                                }
                                result.Append(" + char(39)");
                            }
                            else
                            {
                                result.Append("'");
                                result.Append(" + char(39)");
                            }

                            if (i == length)
                            {
                                result.Append("+");
                                result.Append("N'");
                            }
                            mark = true;
                            break;

                        default:
                            if (mark)
                            {
                                result.Append("+");
                                result.Append(" ");
                                result.Append("N'");
                                mark = false;
                            }
                            result.Append(arrChar[i]);
                            break;
                    }
                }
            }

            return result.ToString();
        }

        /// <summary>
        /// TagsReplaceStr
        /// </summary>
        /// <param name="inputStr"></param>
        /// <returns></returns>
        public static string TagsReplaceStr(string inputStr)
        {
            if ((inputStr != null) && (!string.Empty.Equals(inputStr)))
            {
                inputStr = Regex.Replace(inputStr, "{", "&#123;");
                inputStr = Regex.Replace(inputStr, "}", "&#125;");
                inputStr = Regex.Replace(inputStr, "&quot;", "\"");
                inputStr = Regex.Replace(inputStr, "&lt;", "<");
                inputStr = Regex.Replace(inputStr, "&lt;", "<");
                inputStr = Regex.Replace(inputStr, "&gt;", ">");
                inputStr = Regex.Replace(inputStr, "\r\n", "\n");
                inputStr = Regex.Replace(inputStr, "\r", "\n");
                inputStr = Regex.Replace(inputStr, "\n", "<br/>");
                inputStr = Regex.Replace(inputStr, "\n", "");
            }

            return inputStr;
        }

        /// <summary>
        /// TagsReplaceStr
        /// </summary>
        /// <param name="inputStr"></param>
        /// <returns></returns>
        public static string FormatFileName(string inputStr)
        {
            if ((inputStr != null) && (!string.Empty.Equals(inputStr)))
            {
                inputStr = inputStr.Replace("'", "");
                inputStr = inputStr.Replace("\"", "");
                inputStr = inputStr.Replace("<", "");
                inputStr = inputStr.Replace(">", "");
                inputStr = inputStr.Replace("{", "");
                inputStr = inputStr.Replace("}", "");
                inputStr = inputStr.Replace("&", "");
                inputStr = inputStr.Replace("(", "");
                inputStr = inputStr.Replace(")", "");
                inputStr = inputStr.Replace(":", "");
                inputStr = inputStr.Replace(",", "");
                inputStr = inputStr.Replace(" ", "_");
            }

            return inputStr;
        }

        /// <summary>
        /// RemoveTagHTML
        /// </summary>
        /// <param name="inputStr"></param>
        /// <returns></returns>
        public static string RemoveTagHTML(string inputStr)
        {
            if ((inputStr != null) && (!string.Empty.Equals(inputStr)))
            {
                inputStr = Regex.Replace(inputStr, "<[^>]*>", string.Empty);
            }

            return inputStr;
        }

        /// <summary>
        /// GetYear
        /// </summary>
        /// <param name="dateStr"></param>
        /// <returns></returns>
        public static string GetYear(string dateStr)
        {
            string yearStr = string.Empty;

            try
            {
                yearStr = (DateTime.Parse(dateStr).Year).ToString();
            }
            catch
            {
            }

            return yearStr;
        }

        /// <summary>
        /// GetMonth
        /// </summary>
        /// <param name="dateStr"></param>
        /// <returns></returns>
        public static string GetMonth(string dateStr)
        {
            string monthStr = string.Empty;

            try
            {
                monthStr = (DateTime.Parse(dateStr).Month).ToString();
            }
            catch
            {
            }

            return monthStr;
        }

        /// <summary>
        /// GetDay
        /// </summary>
        /// <param name="dateStr"></param>
        /// <returns></returns>
        public static string GetDay(string dateStr)
        {
            string dayStr = string.Empty;

            try
            {
                dayStr = (DateTime.Parse(dateStr).Day).ToString();
            }
            catch
            {
            }

            return dayStr;
        }

        /// <summary>
        /// GetHour
        /// </summary>
        /// <param name="dateStr"></param>
        /// <returns></returns>
        public static string GetHour(string dateStr)
        {
            string hourStr = string.Empty;

            try
            {
                hourStr = (DateTime.Parse(dateStr).Hour).ToString();
            }
            catch
            {
            }

            return hourStr;
        }

        /// <summary>
        /// GetMinute
        /// </summary>
        /// <param name="dateStr"></param>
        /// <returns></returns>
        public static string GetMinute(string dateStr)
        {
            string minuteStr = string.Empty;

            try
            {
                minuteStr = (DateTime.Parse(dateStr).Minute).ToString();
            }
            catch
            {
            }

            return minuteStr;
        }

        /// <summary>
        /// GetSecond
        /// </summary>
        /// <param name="dateStr"></param>
        /// <returns></returns>
        public static string GetSecond(string dateStr)
        {
            string secondStr = string.Empty;

            try
            {
                secondStr = (DateTime.Parse(dateStr).Second).ToString();
            }
            catch
            {
            }

            return secondStr;
        }

        /// <summary>
        /// GetTime
        /// </summary>
        /// <param name="hour"></param>
        /// <param name="minute"></param>
        /// <param name="second"></param>
        /// <returns></returns>
        public static string GetTime(string hour, string minute, string second)
        {
            string timeStr = string.Empty;

            try
            {
                if ("0".Equals(hour) || string.Empty.Equals(hour))
                {
                    timeStr = string.Empty;
                }
                else
                {
                    if ("0".Equals(minute) || string.Empty.Equals(minute))
                    {
                        timeStr = string.Empty;
                    }
                    else
                    {
                        if ("0".Equals(second) || string.Empty.Equals(second))
                        {
                            timeStr = string.Empty;
                        }
                        else
                        {
                            timeStr = hour + ":" + minute + ":" + second;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return timeStr;
        }

        /// <summary>
        /// get date by format YYYY-MM-DD
        /// </summary>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <param name="day"></param>
        /// <returns></returns>
        public static string GetDate(string year, string month, string day)
        {
            string datesStr = string.Empty;

            try
            {
                if ("0".Equals(year) || string.Empty.Equals(year))
                {
                    datesStr = string.Empty;
                }
                else
                {
                    if ("0".Equals(month) || string.Empty.Equals(month))
                    {
                        datesStr = string.Empty;
                    }
                    else
                    {
                        if ("0".Equals(day) || string.Empty.Equals(day))
                        {
                            datesStr = string.Empty;
                        }
                        else
                        {
                            datesStr = year + "-" + month + "-" + day;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return datesStr;
        }

        /// <summary>
        /// GetDate
        /// </summary>
        /// <param name="dateStr"></param>
        /// <returns></returns>
        public static string GetDate(string dateStr)
        {
            string dateStrReturn = string.Empty;
            string yearStr = string.Empty;
            string monthStr = string.Empty;
            string dayStr = string.Empty;
            string hour = string.Empty;
            string minute = string.Empty;
            string second = string.Empty;

            try
            {
                yearStr = GetYear(dateStr);
                monthStr = GetMonth(dateStr);
                dayStr = GetDay(dateStr);
                hour = GetHour(dateStr);
                minute = GetMinute(dateStr);
                second = GetSecond(dateStr);
                dateStrReturn = GetDate(yearStr, monthStr, dayStr);
                if (!string.Empty.Equals(dateStrReturn))
                {
                    dateStrReturn = dateStrReturn + " " + GetTime(hour, minute, second);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dateStrReturn;
        }

        /// <summary>
        /// GetDate
        /// </summary>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <param name="day"></param>
        /// <param name="hour"></param>
        /// <param name="minute"></param>
        /// <returns></returns>
        public static string GetDate(string year, string month, string day, string hour, string minute)
        {
            string strReturn = string.Empty;
            string time = string.Empty;

            if ("0".Equals(year) || string.Empty.Equals(year))
            {
                strReturn = string.Empty;
            }
            else
            {
                if ("0".Equals(month) || string.Empty.Equals(month))
                {
                    strReturn = string.Empty;
                }
                else
                {
                    if ("0".Equals(day) || string.Empty.Equals(day))
                    {
                        strReturn = string.Empty;
                    }
                    else
                    {
                        strReturn = month + "/" + day + "/" + year;
                    }
                }
            }

            if (!string.Empty.Equals(hour) || !string.Empty.Equals(minute))
            {
                if (string.Empty.Equals(hour.Trim()))
                {
                    hour = "0";
                }

                if (string.Empty.Equals(minute.Trim()))
                {
                    minute = "0";
                }

                strReturn = strReturn + " " + hour + ":" + minute;
            }
            return strReturn;
        }

        /// <summary>
        /// GetDateRss
        /// </summary>
        /// <param name="datestr"></param>
        /// <param name="spec"></param>
        /// <returns></returns>
        public static string GetDateRss(string datestr, string spec)
        {
            string strReturn = string.Empty;
            DateTime createDate = DateTime.Parse(datestr);

            if (spec == "Rss1.0")
            {
                strReturn = createDate.ToString("s");//2005-07-21T11:42:44
            }
            else if (spec == "Rss2.0")
            {
                strReturn = createDate.ToString("r");//Thur, Aug 17, 2000 16:32:32
                strReturn = strReturn.Replace(" GMT", string.Empty);
            }

            return strReturn;
        }

        /// <summary>
        /// Format zip-code string
        /// </summary>
        /// <param name="zipCode"></param>
        /// <param name="format"></param>
        /// <returns></returns>
        public static string FormatNumberString(string str, string format)
        {
            string result = string.Empty;

            try
            {
                result = String.Format("{0:" + format + "}", Convert.ToInt64(str));
            }
            catch
            {
                result = string.Empty;
            }

            return result;
        }

        /// <summary>
        /// CompareTag
        /// </summary>
        /// <param name="inputString"></param>
        /// <param name="arrNotRemoveTag"></param>
        /// <param name="lengthTag"></param>
        /// <returns></returns>
        private static bool CompareTag(string inputString, ArrayList arrNotRemoveTag, out int lengthTag)
        {
            string tag = string.Empty;

            inputString = inputString.Trim();
            lengthTag = 0;

            for (int i = 0; i < arrNotRemoveTag.Count; i++)
            {
                tag = arrNotRemoveTag[i].ToString().Trim().ToLower();
                if (inputString.Length >= tag.Length)
                {
                    if (inputString.Substring(0, tag.Length).ToLower().Equals(tag))
                    {
                        lengthTag = tag.Length;
                        return true;
                    }
                }
            }

            return false;
        }

        /// <summary>
        /// CreateTable
        /// </summary>
        /// <param name="obDataView"></param>
        /// <returns></returns>
        public static DataTable CreateTable(DataView obDataView)
        {
            DataTable obNewDt = obDataView.Table.Clone();
            int idx = 0;
            string[] strColNames = new string[obNewDt.Columns.Count];
            foreach (DataColumn col in obNewDt.Columns)
            {
                strColNames[idx++] = col.ColumnName;
            }

            IEnumerator viewEnumerator = obDataView.GetEnumerator();
            while (viewEnumerator.MoveNext())
            {
                DataRowView drv = (DataRowView)viewEnumerator.Current;
                DataRow dr = obNewDt.NewRow();
                try
                {
                    foreach (string strName in strColNames)
                    {
                        dr[strName] = drv[strName];
                    }
                }
                catch
                {

                }
                obNewDt.Rows.Add(dr);
            }
            return obNewDt;
        }

        /// <summary>
        /// Format string to search from db
        /// </summary>
        /// <param name="st"></param>
        /// <returns></returns>
        public static string FormatSearchText(string st)
        {
            try
            {
                st = st.Replace("<", "");
                st = st.Replace("/", "");
                st = st.Replace(">", "");
                st = st.Replace("script", "");
                st = st.Replace("%", "");
                return st;
            }
            catch
            {
                return "";
            }
        }

        public static int ConvertToInt(object value)
        {
            int result = 0;

            if (value != null)
            {
                int.TryParse(value.ToString(), out result);
            }

            return result;
        }

        public static long ConvertToLong(object value)
        {
            long result = 0;

            if (value != null)
            {
                long.TryParse(value.ToString(), out result);
            }

            return result;
        }

        public static byte ConvertToByte(object value)
        {
            byte result = 0;

            if (value != null)
            {
                byte.TryParse(value.ToString(), out result);
            }

            return result;
        }

        public static double ConvertToDouble(object value)
        {
            double result = 0;

            if (value != null)
            {
                double.TryParse(value.ToString(), out result);
            }

            return result;
        }

        public static DateTime ConvertToDatetime(object value)
        {
            DateTime result = new DateTime();
            try
            {
                result = DateTime.Parse(value.ToString());
                return result;
            }
            catch
            {
                return result;
            }
        }

        public static string ConvertToString(object value)
        {
            return (value != null && !string.IsNullOrEmpty(value.ToString())) ? value.ToString() : "";
        }

        public static long ConvertDateTimeToTicks(DateTime dtInput)
        {
            long ticks = 0;
            ticks = dtInput.Ticks;
            return ticks;
        }

        public static DateTime ConvertTicksToDateTime(long lticks)
        {
            DateTime dtresult = new DateTime(lticks);
            return dtresult;
        }
    }
}