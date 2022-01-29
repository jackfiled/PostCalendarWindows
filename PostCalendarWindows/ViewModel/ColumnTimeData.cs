using System;

namespace PostCalendarWindows.ViewModel
{
    //表示日历日期显示部分的类
    public class ColumnTimeData
    {
        public int Year { get; private set; }
        public string Mon { get; private set; }
        public string Tues { get; private set; }
        public string Wed { get; private set; }
        public string Thu { get; private set; }
        public string Fri { get; private set; }
        public string Sat { get; private set; }
        public string Sun { get; private set; }

        private DateOnly date;
        private DateOnly week_frist_date;
        
        public ColumnTimeData(DateOnly _date)
        {
            date = _date;
            week_frist_date = _date;

            Year = date.Year;
            Sun = $"{date.Month}-{date.Day}";
            date.AddDays(1);
            Mon = $"{date.Month}-{date.Day}";
            date = date.AddDays(1);
            Tues = $"{date.Month}-{date.Day}";
            date = date.AddDays(1);
            Wed = $"{date.Month}-{date.Day}";
            date = date.AddDays(1);
            Thu = $"{date.Month}-{date.Day}";
            date = date.AddDays(1);
            Fri = $"{date.Month}-{date.Day}";
            date = date.AddDays(1);
            Sat = $"{date.Month}-{date.Day}";
        }

        public void AddDays(int value)
        {
            date = week_frist_date.AddDays(value);

            Year = date.Year;
            Sun = $"{date.Month}-{date.Day}";
            date.AddDays(1);
            Mon = $"{date.Month}-{date.Day}";
            date = date.AddDays(1);
            Tues = $"{date.Month}-{date.Day}";
            date = date.AddDays(1);
            Wed = $"{date.Month}-{date.Day}";
            date = date.AddDays(1);
            Thu = $"{date.Month}-{date.Day}";
            date = date.AddDays(1);
            Fri = $"{date.Month}-{date.Day}";
            date = date.AddDays(1);
            Sat = $"{date.Month}-{date.Day}";
        }
    }
}
