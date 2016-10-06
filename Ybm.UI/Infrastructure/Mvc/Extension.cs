using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Web;
using Ybm.Framework;

namespace Ybm.UI
{
    public static class Extension
    {
        public static Kendo.Mvc.UI.DataSourceResult ToDataSourceResult(this System.Collections.IEnumerable enumerable, Kendo.Mvc.UI.DataSourceRequest request, int total)
        {
            return new Kendo.Mvc.UI.DataSourceResult()
            {
                Data = enumerable.AsQueryable(),
                Total = total
            };
        }
        public static string ToTimeSince(this DateTime date)
        {
            const int SECOND = 1;
            const int MINUTE = 60 * SECOND;
            const int HOUR = 60 * MINUTE;
            const int DAY = 24 * HOUR;
            const int MOUNTH = 30 * DAY;
            var ts = DateTime.Now.Ticks < date.Ticks ? new TimeSpan(date.Ticks - DateTime.Now.Ticks) : new TimeSpan(DateTime.Now.Ticks - date.Ticks);
            double delta = Math.Abs(ts.TotalSeconds);
            if (delta < 1 * MINUTE)
                return ts.Seconds == 1 ? "یک ثانیه پیش" : ts.Seconds + "ثانیه پیش";
            //return ts.Seconds == 1 ? "one second ago" : ts.Seconds + "second ago";

            if (delta < 2 * MINUTE)
                return "یک دقیقه پیش";
            //return "a minute ago";
            if (delta < 45 * MINUTE)
                return ts.Minutes + "دقیقه پیش";
            //return ts.Minutes + "minutes ago";
            if (delta < 90 * MINUTE)
                return "یک ساعت پیش";
            //return "an hour ago";
            if (delta < 24 * HOUR)
                return ts.Hours + " ساعت پیش";
            //return ts.Hours + " hours ago";
            if (delta < 48 * HOUR)
                return "دیروز";
            //return "yesterday";
            if (delta < 30 * DAY)
                return ts.Days + " روز پیش";
            //return ts.Days + " days ago";
            if (delta < 12 * MOUNTH)
            {
                int month = Convert.ToInt32(Math.Floor((double)ts.Days / 30));
                return month <= 1 ? "یک ماه پیش" : month + "ماه پیش ";
                //return month <= 1 ? "one month ago" : month + "month ago";
            }

            else
            {
                int years = Convert.ToInt32(Math.Floor((double)ts.Days / 365));
                return years <= 1 ? "یک سال پیش" : years + " سال پیش ";
                //return years <= 1 ? "one year ago" : years + "years ago";

            }
        }
        public static string ToPersianMonthDate(this DateTime date)
        {
            var persianDate = Convert.ToDateTime(date.GetPersianDateTime("yyyy/mm/dd"));
            var year = persianDate.Year;
            int month = persianDate.Month;
            string monthText = "";
            var day = persianDate.Day;
            switch (month)
            {
                case 1:
                    monthText = "فروردین";
                    break;
                case 2:
                    monthText = "اردیبهشت";
                    break;
                case 3:
                    monthText = "خرداد";
                    break;
                case 4:
                    monthText = "تیر";
                    break;
                case 5:
                    monthText = "مرداد";
                    break;
                case 6:
                    monthText = "شهریور";
                    break;
                case 7:
                    monthText = "مهر";
                    break;
                case 8:
                    monthText = "آبان";
                    break;
                case 9:
                    monthText = "آذر";
                    break;
                case 10:
                    monthText = "دی";
                    break;
                case 11:
                    monthText = "بهمن";
                    break;
                case 12:
                    monthText = "اسفند";
                    break;


            }
            return (day + "  " + monthText + "\r" + +year);
        }
    }
}