using System;
using System.Globalization;

namespace CRM.Library.Common
{
    public enum Quarter
    {
        First = 1,
        Second = 2,
        Third = 3,
        Fourth = 4
    }

    public enum Month
    {
        January = 1,
        February = 2,
        March = 3,
        April = 4,
        May = 5,
        June = 6,
        July = 7,
        August = 8,
        September = 9,
        October = 10,
        November = 11,
        December = 12
    }

    public enum Weekday : int
    {
        Monday = 1,
        Tuesday = 2,
        Wednesday = 3,
        Thursday = 4,
        Friday = 5,
        Saturday = 6,
        Sunday = 7
    }
    public class DateTimeUtil
    {

        #region Quarter

        public static DateTime GetStartOfQuarter(int Year, Quarter Qtr)
        {
            if (Qtr == Quarter.First)	// 1st Quarter = January 1 to March 31
                return new DateTime(Year, 1, 1, 0, 0, 0, 0);
            else if (Qtr == Quarter.Second) // 2nd Quarter = April 1 to June 30
                return new DateTime(Year, 4, 1, 0, 0, 0, 0);
            else if (Qtr == Quarter.Third) // 3rd Quarter = July 1 to September 30
                return new DateTime(Year, 7, 1, 0, 0, 0, 0);
            else // 4th Quarter = October 1 to December 31
                return new DateTime(Year, 10, 1, 0, 0, 0, 0);
        }

        public static DateTime GetEndOfQuarter(int Year, Quarter Qtr)
        {
            if (Qtr == Quarter.First)	// 1st Quarter = January 1 to March 31
                return new DateTime(Year, 3, DateTime.DaysInMonth(Year, 3), 23, 59, 59, 999);
            else if (Qtr == Quarter.Second) // 2nd Quarter = April 1 to June 30
                return new DateTime(Year, 6, DateTime.DaysInMonth(Year, 6), 23, 59, 59, 999);
            else if (Qtr == Quarter.Third) // 3rd Quarter = July 1 to September 30
                return new DateTime(Year, 9, DateTime.DaysInMonth(Year, 9), 23, 59, 59, 999);
            else // 4th Quarter = October 1 to December 31
                return new DateTime(Year, 12, DateTime.DaysInMonth(Year, 12), 23, 59, 59, 999);
        }

        public static Quarter GetQuarter(Month month)
        {
            if (month <= Month.March)	// 1st Quarter = January 1 to March 31
                return Quarter.First;
            else if ((month >= Month.April) && (month <= Month.June)) // 2nd Quarter = April 1 to June 30
                return Quarter.Second;
            else if ((month >= Month.July) && (month <= Month.September)) // 3rd Quarter = July 1 to September 30
                return Quarter.Third;
            else // 4th Quarter = October 1 to December 31
                return Quarter.Fourth;
        }

        public static DateTime GetEndOfLastQuarter()
        {
            if (DateTime.Now.Month <= (int)Month.March) //go to last quarter of previous year
                return GetEndOfQuarter(DateTime.Now.Year - 1, GetQuarter(Month.December));
            else //return last quarter of current year
                return GetEndOfQuarter(DateTime.Now.Year, GetQuarter((Month)DateTime.Now.Month));
        }

        public static DateTime GetStartOfLastQuarter()
        {
            if (DateTime.Now.Month <= 3) //go to last quarter of previous year
                return GetStartOfQuarter(DateTime.Now.Year - 1, GetQuarter(Month.December));
            else //return last quarter of current year
                return GetStartOfQuarter(DateTime.Now.Year, GetQuarter((Month)DateTime.Now.Month));
        }

        public static DateTime GetStartOfCurrentQuarter()
        {
            return GetStartOfQuarter(DateTime.Now.Year, GetQuarter((Month)DateTime.Now.Month));
        }

        public static DateTime GetEndOfCurrentQuarter()
        {
            return GetEndOfQuarter(DateTime.Now.Year, GetQuarter((Month)DateTime.Now.Month));
        }
        #endregion

        #region Weeks
        public static DateTime GetStartOfLastWeek()
        {
            int DaysToSubtract = (int)DateTime.Now.DayOfWeek + 7;
            DateTime dt = DateTime.Now.Subtract(System.TimeSpan.FromDays(DaysToSubtract));
            return new DateTime(dt.Year, dt.Month, dt.Day, 0, 0, 0, 0);
        }

        public static DateTime GetEndOfLastWeek()
        {
            DateTime dt = GetStartOfLastWeek().AddDays(6);
            return new DateTime(dt.Year, dt.Month, dt.Day, 23, 59, 59, 999);
        }

        public static DateTime GetStartOfCurrentWeek()
        {
            int DaysToSubtract = (int)DateTime.Now.DayOfWeek;
            DateTime dt = DateTime.Now.Subtract(System.TimeSpan.FromDays(DaysToSubtract));
            return new DateTime(dt.Year, dt.Month, dt.Day, 0, 0, 0, 0);
        }

        public static DateTime GetEndOfCurrentWeek()
        {
            DateTime dt = GetStartOfCurrentWeek().AddDays(6);
            return new DateTime(dt.Year, dt.Month, dt.Day, 23, 59, 59, 999);
        }
        #endregion

        #region Months

        public static DateTime GetStartOfMonth(int Month, int Year)
        {
            return new DateTime(Year, Month, 1, 0, 0, 0, 0);
        }

        public static DateTime GetEndOfMonth(int Month, int Year)
        {
            return new DateTime(Year, Month, DateTime.DaysInMonth(Year, Month), 23, 59, 59, 999);
        }

        public static DateTime GetStartOfLastMonth()
        {
            if (DateTime.Now.Month == 1)
                return GetStartOfMonth(12, DateTime.Now.Year - 1);
            else
                return GetStartOfMonth(DateTime.Now.Month - 1, DateTime.Now.Year);
        }

        public static DateTime GetEndOfLastMonth()
        {
            if (DateTime.Now.Month == 1)
                return GetEndOfMonth(12, DateTime.Now.Year - 1);
            else
                return GetEndOfMonth(DateTime.Now.Month - 1, DateTime.Now.Year);
        }

        public static DateTime GetStartOfCurrentMonth()
        {
            return GetStartOfMonth(DateTime.Now.Month, DateTime.Now.Year);
        }

        public static DateTime GetEndOfCurrentMonth()
        {
            return GetEndOfMonth(DateTime.Now.Month, DateTime.Now.Year);
        }
        #endregion

        #region Years
        public static DateTime GetStartOfYear(int Year)
        {
            return new DateTime(Year, 1, 1, 0, 0, 0, 0);
        }

        public static DateTime GetEndOfYear(int Year)
        {
            return new DateTime(Year, 12, DateTime.DaysInMonth(Year, 12), 23, 59, 59, 999);
        }

        public static DateTime GetStartOfLastYear()
        {
            return GetStartOfYear(DateTime.Now.Year - 1);
        }

        public static DateTime GetEndOfLastYear()
        {
            return GetEndOfYear(DateTime.Now.Year - 1);
        }

        public static DateTime GetStartOfCurrentYear()
        {
            return GetStartOfYear(DateTime.Now.Year);
        }

        public static DateTime GetEndOfCurrentYear()
        {
            return GetEndOfYear(DateTime.Now.Year);
        }
        #endregion

        #region Days

        public static DateTime GetStartOfDay(DateTime date)
        {
            return new DateTime(date.Year, date.Month, date.Day, 0, 0, 0, 0);
        }

        public static DateTime GetEndOfDay(DateTime date)
        {
            return new DateTime(date.Year, date.Month, date.Day, 23, 59, 59, 999);
        }

        #endregion

        // 2010.12.01 tan.tran
        public static int CompareNullDateTo(DateTime? obj1, DateTime? obj2)
        {
            if (obj1 == null || obj2 == null)
            {
                return (obj1 == null) ? -1 : 1;
            }

            if (obj1 == null && obj2 == null)
            {
                return 0;
            }
            return obj1.Value.CompareTo(obj2.Value);
        }

        #region Other from PRM 
        /// <summary>
        /// GetDateFromWeekOfYear
        /// </summary>
        /// <param name="Year"></param>
        /// <param name="week"></param>
        /// <returns></returns>
        public static DateTime GetDateFromWeekOfYear(int Year, int week)
        {
            DateTime date = new DateTime(Year, 1, 1);
            date = date.AddDays(week * 7 - 1);
            return date;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static int GetCurrentWeekOfYear()
        {
            GregorianCalendar calendar = new GregorianCalendar();
            return calendar.GetWeekOfYear(DateTime.Today, CalendarWeekRule.FirstFullWeek, DayOfWeek.Monday);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static int GetWeekOfYear(DateTime date)
        {
            GregorianCalendar calendar = new GregorianCalendar();
            return calendar.GetWeekOfYear(date, CalendarWeekRule.FirstFullWeek, DayOfWeek.Monday);
        }

        /// <summary>
        /// Get First Date Of Month
        /// </summary>
        /// <param name="weekday"></param>
        /// <param name="month"></param>
        /// <param name="year"></param>
        /// <returns></returns>
        public static int GetFirstDateOfMonth(Weekday weekday, int month, int year)
        {
            int date = -1;
            string dayCmp = weekday.ToString().Substring(0, 3);

            for (int i = 1; i <= 7; i++)
            {
                if (dayCmp.Equals(new DateTime(year, month, i).ToString("ddd")))
                {
                    date = i;
                    break;
                }
            }

            return date;
        }

        /// <summary>
        /// Get First Date Of Month
        /// </summary>
        /// <param name="weekday"></param>
        /// <param name="month"></param>
        /// <param name="year"></param>
        /// <returns></returns>
        public static int GetFirstDateOfMonth(int day, int month, int year)
        {
            int date = -1;
            int week = 1;

            if (day > 7 && day <= 14)
                week = 2;
            else if (day > 14 && day <= 21)
                week = 3;
            else if (day > 21 && day <= 28)
                week = 4;
            else if (day > 28 && day <= 31)
                week = 5;

            date = GetFirstDateOfMonth(Weekday.Monday, month, year);

            if ((week > 1) && (week <= 5))
            {
                date += ((week - 1) * 7);
            }

            return date;
        }
        /// <summary>
        /// True is ok
        /// StartDate + year >= EndDate (true)
        /// </summary>
        /// <param name="startdate"></param>
        /// <param name="enddate"></param>
        /// <returns></returns>
        public static bool CompareYear(DateTime startdate, DateTime enddate, int year)
        {
            if (startdate.AddYears(year).CompareTo(enddate) < 0)
            {
                return true;
            }
            return false;
        }


        /// <summary>
        /// GetLastOccurenceOfDay
        /// </summary>
        /// <param name="value"></param>
        /// <param name="dayOfWeek"></param>
        /// <returns></returns>
        public static DateTime GetLastOccurenceOfDay(DateTime value, DayOfWeek dayOfWeek)
        {
            int daysToAdd = dayOfWeek - value.DayOfWeek;
            if (daysToAdd < 1)
            {
                daysToAdd -= 7;
            }
            return value.AddDays(daysToAdd);
        }

        public static string GetDateInWeek(int index)
        {
            string result = string.Empty;
            switch (index)
            {
                case 0:
                    result = "Mon";
                    break;
                case 1:
                    result = "Tue";
                    break;
                case 2:
                    result = "Wed";
                    break;
                case 3:
                    result = "Thu";
                    break;
                case 4:
                    result = "Fri";
                    break;
                case 5:
                    result = "Sat";
                    break;
                case 6:
                    result = "Sun";
                    break;
            }
            return result;
        }

        public static string GetDateInWeek(DateTime date)
        {
            string result = string.Empty;
            switch ((int)date.DayOfWeek)
            {
                case 0:
                    result = "Sun" ;
                    break;
                case 1:
                    result = "Mon";
                    break;
                case 2:
                    result = "Tue";
                    break;
                case 3:
                    result = "Wed";
                    break;
                case 4:
                    result = "Thu";
                    break;
                case 5:
                    result = "Fri";
                    break;
                case 6:
                    result = "Sat";
                    break;               
            }
            return result +" (" + date.Day + "/" +date.Month + ")";
        }
    }
    #endregion
}
