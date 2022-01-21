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

        public int week_first_day { get; private set; }

        private int _year;
        private int _month;
        private int _day;

        public ColumnTimeData(int year, int month, int day, DayOfWeek week)
        {
            _year = year;
            _month = month;
            //这里为了符合C#在设计时把week中的星期日设置为0的设计，我把日历中每周的第一天也设置为星期日
            //这样子这里处理的时候比较方便
            _day = day - (int)week;
            Is_day_empty();
            Year = _year;
            Mon = $"{_month}-{_day}";
            _day++;
            Is_month_day_full();
            Tues = $"{_month}-{_day}";
            _day++;
            Is_month_day_full();
            Wed = $"{_month}-{_day}";
            _day++;
            Is_month_day_full();
            Thu = $"{_month}-{_day}";
            _day++;
            Is_month_day_full();
            Fri = $"{_month}-{_day}";
            _day++;
            Is_month_day_full();
            Sat = $"{_month}-{_day}";
            _day++;
            Is_month_day_full();
            Sun = $"{_month}-{_day}";
        }

        void Is_month_day_full()
        {
            int[] months = { 31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 };
            //这里记得处理闰年的情况
            if((_year % 4 ==0 && _year % 100 != 0) || _year % 400 == 0)
            {
                months[1] = 29;
            }
            if(_day > months[_month - 1])
            {
                //如果年底了
                if(_month == 12)
                {
                    _year++;
                    _month = 1;
                    _day = 1;
                }
                else
                {
                    _month++;
                    _day = 1;
                }
            }
        }

        void Is_day_empty()
        {
            int[] months = { 31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 };
            if ((_year % 4 == 0 && _year % 100 != 0) || _year % 400 == 0)
            {
                months[1] = 29;
            }
            if(_day <= 0)
            {
                if(_month == 1)
                {
                    _year--;
                    _month = 12;
                    _day = months[11] + _day;
                }
                else
                {
                    _month--;
                    _day = months[_month - 1] + _day;
                }
            }
        }
        
    }
}
