using System;
using System.Collections.Generic;
using System.Data;
//这个COM组件可以调用excel所有的功能
using Excel = Microsoft.Office.Interop.Excel;
using PostCalendarWindows.DataModel;

namespace PostCalendarWindows.Calendar
{
    public class CalendarManager
    { 
        public List<ShowItem> ShowItems = new List<ShowItem>();
        public DateOnly WeekFirstDay;

        private List<CalendarEvent> events = new List<CalendarEvent>();
        private readonly CalendarItemContext _context = new CalendarItemContext();

        public CalendarManager()
        {
            //获得本周的第一天
            GetWeekFirstDay();
            events.AddRange(_context.LoadDataFromDb(WeekFirstDay));
            Refresh();
        }

        /// <summary>
        /// 将excel表格中的数据添加进入数据库中
        /// </summary>
        /// <param name="path">excel文件的位置</param>
        public void AddCurriculumFromExcel(string path)
        {
            List<CalendarEvent> curriculum_events = new List<CalendarEvent>();

            DataTable dt = ReadExcel(path);
            List<Curriculum> currs = AnalyseExcelData(dt);
            var semester_first_day = new DateOnly(2022, 2, 27);//这里有大问题
            
            foreach(Curriculum cur in currs)
            {
                for(int week = cur.start_week - 1; week < cur.end_week; week++)
                {
                    var e = new CalendarEvent();
                    e.Name = cur.Name;
                    e.Place = cur.place;
                    e.Details = cur.teacher;
                    e.BeginTime = cur.last_time.start_time;
                    e.EndTime = cur.last_time.end_time;
                    e.Date = semester_first_day.AddDays(cur.dayOfWeek + 7 * week);
                    curriculum_events.Add(e);
                }
            }

            foreach(CalendarEvent e in curriculum_events)
            {
                _context.CreateCalendarItem(e);
            }
            _context.SaveChanges();
            
            Refresh();
        }

        /// <summary>
        /// 重新加载需要显示的数据
        /// </summary>
        public void Refresh()
        {
            events.Clear();
            events.AddRange(_context.LoadDataFromDb(WeekFirstDay));
            ShowItems.Clear();
            foreach (CalendarEvent e in events)
            {
                ShowItems.Add(new ShowItem(e.Id, e.Name, e.Place, (int)e.DayOfWeek, e.BeginTime, e.EndTime));
            }
        }

        /// <summary>
        /// 销毁对象
        /// </summary>
        public void Dispose()
        {
            _context.Dispose();
        }

        void GetWeekFirstDay()
        {
            DateOnly now = new DateOnly(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
            DayOfWeek day = now.DayOfWeek;
            WeekFirstDay = now.AddDays(-(int)day);
        }

        static DataTable ReadExcel(string path)
        {
            var dt = new DataTable();
            //不得不说，大微软家的东西可以相互调用就是方便
            var app = new Excel.Application();
            Excel.Workbooks wbs = app.Workbooks;
            wbs.Open(path);
            Excel.Worksheet ws = app.Worksheets[1];
            
            int rows = ws.UsedRange.Rows.Count;
            int columns = ws.UsedRange.Columns.Count;

            for(int i = 1; i <= rows; i++)
            {
                DataRow dr = dt.NewRow();
                for(int j = 1; j <= columns; j++)
                {
                    //这里按照我的理解，就是选定那一个单元格
                    //谨记调用Office的API是图形化的API
                    Excel.Range range = ws.Range[app.Cells[i, j], app.Cells[i, j]];
                    range.Select();
                    //这里处理表头，并且考虑到了存在重复列名的问题
                    //不过，在我这个程序中不会用到列名
                    if (i == 1)
                    {
                        string col_name = app.ActiveCell.Text.ToString();
                        if (dt.Columns.Contains(col_name))
                        {
                            dt.Columns.Add(col_name + j);
                        }
                        else
                        {
                            dt.Columns.Add(col_name);
                        }
                    }
                    dr[j - 1] = app.ActiveCell.Text.ToString();
                }
                dt.Rows.Add(dr);
            }

            wbs.Close();
            app.Quit();

            //将表格开头的两行干掉
            //还有最后一行备注也是
            dt.Rows.RemoveAt(0);
            dt.Rows.RemoveAt(0);
            dt.Rows.RemoveAt(dt.Rows.Count - 1);
            return dt;
        }

        //解析读取到的excel数据的方法
        List<Curriculum> AnalyseExcelData(DataTable dt)
        {
            List<Curriculum> currs = new List<Curriculum>();
            //北邮作息时间表
            TimeOnly[] class_start_time = {new TimeOnly(8,0), new TimeOnly(8, 50), new TimeOnly(9, 50), new TimeOnly(10, 40), 
            new TimeOnly(11, 30), new TimeOnly(13, 0), new TimeOnly(13, 50), new TimeOnly(14,45), new TimeOnly(15,40), new TimeOnly(16,35),
            new TimeOnly(17, 25), new TimeOnly(18, 30), new TimeOnly(19, 20), new TimeOnly(20,10)};

            string? last_curriculum_name = null;
            string name, teacher, place;
            int start_week, end_week, dayOfWeek;
            TimeOnly start_time, end_time;


            System.Text.RegularExpressions.Regex pattern = new System.Text.RegularExpressions.Regex(@"\d+");
            System.Text.RegularExpressions.MatchCollection match_result1;
            System.Text.RegularExpressions.MatchCollection match_result2;
            List<int> class_array = new List<int>();

            foreach(DataColumn col in dt.Columns)
            {
                //这里已经把星期处理成星期日为0
                if(col.Ordinal >= 6)
                {
                    dayOfWeek = 0;
                }
                else
                {
                    dayOfWeek = col.Ordinal;
                }
                foreach(DataRow dr in dt.Rows)
                {
                    string? cell = dr[col].ToString();
                    //首先判断单元格里有信息
                    if(cell!=null && cell.Length != 1)
                    {
                        string[] result = cell.Split('\n');
                        //这里只处理这两种情况，避免一些不必要的麻烦
                        //没有被处理的单元格引发错误吧
                        if(result.Length == 6 || result.Length == 7)
                        {
                            name = result[1];
                            if (name != last_curriculum_name)
                            {
                                last_curriculum_name = name;
                                //分别匹配两种情况
                                if (result.Length == 6)
                                {
                                    match_result1 = pattern.Matches(result[3]);
                                    match_result2 = pattern.Matches(result[5]);
                                    teacher = result[2];
                                    place = result[4];
                                }
                                else
                                {
                                    match_result1 = pattern.Matches(result[4]);
                                    match_result2 = pattern.Matches(result[6]);
                                    teacher = result[3];
                                    place = result[5];
                                }
                                //处理有的课只上一周的问题
                                if (match_result1.Count == 1)
                                {
                                    start_week = int.Parse(match_result1[0].Value);
                                    end_week = start_week;
                                }
                                else
                                {
                                    start_week = int.Parse(match_result1[0].Value);
                                    end_week = int.Parse(match_result1[1].Value);
                                }
                                //处理上课的节数
                                foreach (System.Text.RegularExpressions.Match m in match_result2)
                                {
                                    class_array.Add(int.Parse(m.Value));
                                }
                                start_time = class_start_time[class_array[0] - 1];
                                end_time = class_start_time[class_array[class_array.Count - 1] - 1].AddMinutes(45);
                                Curriculum c = new Curriculum(name, teacher, place, start_time, end_time, start_week, end_week, dayOfWeek);
                                currs.Add(c);
                                class_array.Clear();
                            }
                        }
                    }
                }
            }
            return currs;
        }
    }

#pragma warning disable CS8618
    /// <summary>
    ///储存日历中的一次事件对象
    /// </summary>
    public class CalendarEvent
    {
        /// <summary>
        /// 在应用中新建事件时，没有id属性
        /// 只有事件被写入数据库中之后，才会有id属性
        /// </summary>
        public int Id { get; set; }
        public string Name { get; set; }
        public string Place { get; set; }
        public string Details { get; set; }
        public DateOnly Date { get; set; }
        public TimeOnly BeginTime { get; set; }
        public TimeOnly EndTime { get; set; }
        public DayOfWeek DayOfWeek
        {
            get { return Date.DayOfWeek; }
        }
        public TimeSpan Length
        {
            get { return EndTime - BeginTime; }
        }

        public string DateString
        {
            get { return Date.ToString(); }
        }

        public string BeginTimeString
        {
            get { return BeginTime.ToString();}
        }

        public string EndTimeString
        {
            get { return EndTime.ToString();}
        }

        /// <summary>
        /// 在程序内部初始化一次事件
        /// </summary>
        /// <param name="_name">事件的名称</param>
        /// <param name="_place">事件的地点</param>
        /// <param name="_details">事件的详细信息</param>
        /// <param name="_date">事件发生的日期</param>
        /// <param name="_begin_time">事件开始时间</param>
        /// <param name="_end_time">事件结束时间</param>
        public void SetInner(string _name, string _place, string _details, DateOnly _date, TimeOnly _begin_time, TimeOnly _end_time)
        {
            Name = _name;
            Place = _place;
            Details = _details;
            Date = _date;
            BeginTime = _begin_time;
            EndTime = _end_time;
        }

        /// <summary>
        /// 从数据库初始化日历事件对象
        /// </summary>
        /// <param name="calendar">数据库中的日历mapping对象</param>
        public void SetFromDatabase(DataModel.CalendarItem calendar)
        {
            Id = calendar.Id;
            Name = calendar.Name;
            Place = calendar.Place;
            Details = calendar.Detail;

            // 处理时间的转换
            DateTime begin_date_time = DateTime.Parse(calendar.BeginDataString);
            DateTime end_date_time = DateTime.Parse(calendar.EndDataString);

            Date = new DateOnly(begin_date_time.Year,
                begin_date_time.Month,
                begin_date_time.Day
                );

            TimeOnly time = TimeOnly.MinValue; //一天时间的零点
            BeginTime = time.Add(begin_date_time.TimeOfDay);
            EndTime = time.Add(end_date_time.TimeOfDay);
        }
    }

    /// <summary>
    /// 内部用于储存从excel表格中读取到的课程数据类
    /// </summary>
    class Curriculum
    {
        public struct LastLength
        {
            public TimeOnly start_time;
            public TimeOnly end_time;
        }

        public string Name, teacher, place;
        public int start_week, end_week, dayOfWeek;
        public LastLength last_time;

        public Curriculum(string event_name, string _teacher, string _place, TimeOnly start, TimeOnly end, int _start_week, int _end_week, int _dayOfWeek)
        {
            Name = event_name;
            teacher = _teacher;
            place = _place;
            last_time.start_time = start;
            last_time.end_time = end;
            start_week = _start_week;
            end_week = _end_week;
            dayOfWeek = _dayOfWeek;
        }
    }

    /// <summary>
    /// 用于显示的对象类
    /// </summary>
    public class ShowItem
    {
        public int id;
        public string name;
        public string place;
        public int dayOfWeek;
        public double begin_length, length;

        public ShowItem(int _id, string _name, string _place, int _dayOfWeek, TimeOnly begin_time, TimeOnly end_time)
        {
            id = _id;
            name = _name;
            place = _place;
            dayOfWeek = _dayOfWeek;
            TimeSpan length_span = end_time - begin_time;
            length = length_span.TotalHours * 50;
            TimeSpan begin_span = begin_time - new TimeOnly(0, 0);
            begin_length = begin_span.TotalHours * 50;
        }
    }
#pragma warning restore CS8618

    /// <summary>
    ///转换日历中事件宽度的转换器
    /// </summary>
    public class CalendarItemWidthConverter : System.Windows.Data.IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo cultureInfo)
        {
            double _value = (double)value;
            if(_value > 80)
            {
                return (_value - 80) / 7;
            }
            else
            {
                return 0;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo cultureInfo)
        {
            return value;
        }
    }

    /// <summary>
    /// 转换滚动条高度的转换器
    /// </summary>
    public class CalendarHeightConverter : System.Windows.Data.IValueConverter
    {
        public object Convert(object value, Type target_type, object parameter, System.Globalization.CultureInfo culture_info)
        {
            double _value = (double)value;
            if(_value > 90)
            {
                return _value - 90;
            }
            else
            {
                return _value;
            }
        }

        public object ConvertBack(object value, Type target_type, object parameter, System.Globalization.CultureInfo culture_info)
        {
            return value;
        }
    }
}
