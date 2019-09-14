using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;

namespace Utils
{
    public class MyDateTimeUtil
    {

        public static DateTime firstDate = new DateTime(1900, 1, 1, 0, 0, 0);

        /// <summary>
        /// 使DateTime在C#里的默认值与SQL Server里的默认值匹配
        /// 即将小于1900年1月1日0:00:00的时间转化为1900年1月1日0:00:00
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>

        public static DateTime? LimitDateTime(DateTime? dt)
        {
            if (dt <= firstDate)
                return firstDate;
            else
                return dt;
        }

        public static DateTime GetMinDate()
        {
            return firstDate;
        }

        public static DateTime GetMaxDate()
        {
            return DateTime.Now.AddYears(100);
        }


        public static String GetNowTimeWithAMPM()
        {
            String result = "";
            if (DateTime.Now.Hour <= 12&& DateTime.Now.Hour >= 1)
            {
                result += "am ";
            }
            else
            {
                result += "pm ";
            }
            result= result + DateTime.Now.ToString("hh:mm");
            return result;
        }

        public static String GetNowTime24()
        {
            return DateTime.Now.ToString("HH:mm");
        }

        public static String GetNowDateTime()
        {
            return DateTime.Now.ToString("yyyyMMddHHmmss");
        }

        public static String Format(DateTime dt, String format)
        {
            //return String.Format(format, dt);
            return dt.ToString(format, DateTimeFormatInfo.InvariantInfo);
        }

        

        public static List<int> YearMonthToYearSeason(int year,int mnth)
        {
            List<int> ym = new List<int>();
            if (mnth >= 3 && mnth <= 5)
            {
                ym.Add(year);
                ym.Add(1);
            }
            else if (mnth >= 6 && mnth <= 8)
            {
                ym.Add(year);
                ym.Add(2);
            }
            else if (mnth >= 9 && mnth <= 11)
            {
                ym.Add(year);
                ym.Add(3);
            }
            else if (mnth == 12)
            {
                ym.Add(year);
                ym.Add(4);
            }
            else
            {
                ym.Add(year-1);
                ym.Add(4);
            }
            return ym;
        }
    }
}
