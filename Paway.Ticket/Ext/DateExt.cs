using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Paway.Ticket
{
    public class DateExt
    {
        public static string[] EnglishSimpleWeeks = { "Mon", "Tue", "Wed", "Thu", "Fri", "Sat", "Sun" };

        public static string[] EnglishSimpleMonths = { "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec" };

        public static string[] ChineseSimpleWeeks = { "周一", "周二", "周三", "周四", "周五", "周门", "周日" };

        public static string GetWeekSimpleEnglishStr(int week)
        {
            switch (week)
            {
                case 0:
                    return EnglishSimpleWeeks[6];
                case 1:
                    return EnglishSimpleWeeks[0];
                case 2:
                    return EnglishSimpleWeeks[1];
                case 3:
                    return EnglishSimpleWeeks[2];
                case 4:
                    return EnglishSimpleWeeks[3];
                case 5:
                    return EnglishSimpleWeeks[4];
                case 6:
                    return EnglishSimpleWeeks[5];
                case 7:
                    return EnglishSimpleWeeks[6];
            }
            return EnglishSimpleWeeks[0];
        }

        public static string GetMonthSimpleEnglishStr(int month)
        {
            switch (month)
            {
                case 1:
                    return EnglishSimpleMonths[0];
                case 2:
                    return EnglishSimpleMonths[1];
                case 3:
                    return EnglishSimpleMonths[2];
                case 4:
                    return EnglishSimpleMonths[3];
                case 5:
                    return EnglishSimpleMonths[4];
                case 6:
                    return EnglishSimpleMonths[5];
                case 7:
                    return EnglishSimpleMonths[6];
                case 8:
                    return EnglishSimpleMonths[7];
                case 9:
                    return EnglishSimpleMonths[8];
                case 10:
                    return EnglishSimpleMonths[9];
                case 11:
                    return EnglishSimpleMonths[10];
                case 12:
                    return EnglishSimpleMonths[11];
            }
            return EnglishSimpleMonths[0];
        }

        public static string GetWeekSimpleChineseStr(int week)
        {
            switch (week)
            {
                case 0:
                    return ChineseSimpleWeeks[6];
                case 1:
                    return ChineseSimpleWeeks[1];
                case 2:
                    return ChineseSimpleWeeks[2];
                case 3:
                    return ChineseSimpleWeeks[3];
                case 4:
                    return ChineseSimpleWeeks[4];
                case 5:
                    return ChineseSimpleWeeks[5];
                case 6:
                    return ChineseSimpleWeeks[6];
            }
            return ChineseSimpleWeeks[0];
        }
        public static string GetWeekSimpleChineseStr(DayOfWeek week)
        {
            return GetWeekSimpleChineseStr((int)week);
        }
    }
}
