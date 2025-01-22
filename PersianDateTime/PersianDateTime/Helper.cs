using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersianDateTime
{
    internal class Helper
    {
        public DateTime ParsePersianDate(string input, PersianCalendar persianCalendar)
        {
            var parts = input.Split('/');
            int year = int.Parse(parts[0]);
            int month = int.Parse(parts[1]);
            int day = int.Parse(parts[2]);
            return persianCalendar.ToDateTime(year, month, day, 0, 0, 0, 0);
        }

        public DateTime AddMonth(DateTime date, PersianCalendar persianCalendar, int num)
        {
            int year = persianCalendar.GetYear(date);
            int month = persianCalendar.GetMonth(date);
            int day = persianCalendar.GetDayOfMonth(date);

            // کاهش یا افزایش ماه
            month += num;
            if (month < 1)
            {
                year -= 1;
                month = 12;
            }

            // تنظیم روز در صورتی که ماه جدید تعداد روزهای کمتری داشته باشد
            int daysInNewMonth = persianCalendar.GetDaysInMonth(year, month);
            if (day > daysInNewMonth)
                day = daysInNewMonth;

            return persianCalendar.ToDateTime(year, month, day, 0, 0, 0, 0);
        }
        public string ConvertToPersianDate(DateTime date, PersianCalendar persianCalendar)
        {
            
            int year = persianCalendar.GetYear(date);
            int month = persianCalendar.GetMonth(date);
            int day = persianCalendar.GetDayOfMonth(date);
            return $"{year}/{month:D2}/{day:D2}";
        }
        public DateTime SubtractOneMonth(DateTime date, PersianCalendar persianCalendar)
        {
            int year = persianCalendar.GetYear(date);
            int month = persianCalendar.GetMonth(date);
            int day = persianCalendar.GetDayOfMonth(date);

            // کاهش یک ماه
            month -= 1;
            if (month < 1)
            {
                year -= 1;
                month = 12;
            }

            // تنظیم روز در صورتی که ماه جدید تعداد روزهای کمتری داشته باشد
            int daysInNewMonth = persianCalendar.GetDaysInMonth(year, month);
            if (day > daysInNewMonth)
                day = daysInNewMonth;

            return persianCalendar.ToDateTime(year, month, day, 0, 0, 0, 0);
        }

        public string GetFirstDayOfMonth(DateTime date, PersianCalendar persianCalendar)
        {
            int year = persianCalendar.GetYear(date);
            int month = persianCalendar.GetMonth(date);
            return $"{year}/{month:D2}/01";
        }

        public string GetLastDayOfMonth(DateTime date, PersianCalendar persianCalendar)
        {
            int year = persianCalendar.GetYear(date);
            int month = persianCalendar.GetMonth(date);
            int daysInMonth = persianCalendar.GetDaysInMonth(year, month);
            return $"{year}/{month:D2}/{daysInMonth:D2}";
        }

        public string ConvertToGregorianDate(string input, PersianCalendar persianCalendar)
        {
            var parts = input.Split('/');
            int year = int.Parse(parts[0]);
            int month = int.Parse(parts[1]);
            int day = int.Parse(parts[2]);
            return persianCalendar.ToDateTime(year, month, day, 0, 0, 0, 0).ToString("yyyy/MM/dd");
        }
       
        public string GetDetailedPersianDate(string input, PersianCalendar persianCalendar)
        {
            var parts = input.Split('/');
            int year = int.Parse(parts[0]);
            int month = int.Parse(parts[1]);
            int day = int.Parse(parts[2]);

            string[] persianDaysOfWeek = { "شنبه", "یک‌شنبه", "دوشنبه", "سه‌شنبه", "چهارشنبه", "پنج‌شنبه", "جمعه" };
            string[] persianMonths = {
            "فروردین", "اردیبهشت", "خرداد", "تیر", "مرداد", "شهریور",
            "مهر", "آبان", "آذر", "دی", "بهمن", "اسفند"
            };

            DateTime date = persianCalendar.ToDateTime(year, month, day, 0, 0, 0, 0);
            string dayOfWeek = persianDaysOfWeek[(int)date.DayOfWeek];
            string monthName = persianMonths[month - 1];

            return $"{dayOfWeek}، {day} {monthName} {year}";
        }

        public string AddDays(string input, PersianCalendar persianCalendar, int days)
        {
            var parts = input.Split('/');
            int year = int.Parse(parts[0]);
            int month = int.Parse(parts[1]);
            int day = int.Parse(parts[2]);

            // ایجاد تاریخ اولیه
            DateTime date = persianCalendar.ToDateTime(year, month, day, 0, 0, 0, 0);

            // اضافه کردن روز
            date = date.AddDays(days);

            // تبدیل دوباره به تاریخ شمسی
            int newYear = persianCalendar.GetYear(date);
            int newMonth = persianCalendar.GetMonth(date);
            int newDay = persianCalendar.GetDayOfMonth(date);

            return $"{newYear}/{newMonth:D2}/{newDay:D2}";
        }

        public string AddYears(string input, PersianCalendar persianCalendar, int years)
        {
            var parts = input.Split('/');
            int year = int.Parse(parts[0]);
            int month = int.Parse(parts[1]);
            int day = int.Parse(parts[2]);

            // ایجاد تاریخ اولیه
            DateTime date = persianCalendar.ToDateTime(year, month, day, 0, 0, 0, 0);

            // اضافه کردن سال 
            date = date.AddYears(years);


            // تبدیل دوباره به تاریخ شمسی
            int newYear = persianCalendar.GetYear(date);
            int newMonth = persianCalendar.GetMonth(date);
            int newDay = persianCalendar.GetDayOfMonth(date);

            return $"{newYear}/{newMonth:D2}/{newDay:D2}";
        }

        public Tuple<int ,int ,int> GetPersianDateDifference(string firstDate, string secondDate, PersianCalendar persianCalendar)
        {
            var dateFunc = new Helper();
            DateTime firstGregorianDate = Convert.ToDateTime(dateFunc.ConvertToGregorianDate(firstDate, persianCalendar));
            DateTime secondGregorianDate = Convert.ToDateTime(ConvertToGregorianDate(secondDate, persianCalendar));

            TimeSpan difference = secondGregorianDate - firstGregorianDate;
            int days = Math.Abs(difference.Days);
            int years = days / 365;
            int remainingDays = days % 365;

            return  Tuple.Create(days,years, remainingDays);
        }
    }
}
