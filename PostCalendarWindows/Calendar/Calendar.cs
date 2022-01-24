﻿using System;
using System.Collections.Generic;
using System.Data;
//这个命名空间都是和进程操作有关
using System.Diagnostics;
//这个COM组件可以调用excel所有的功能
using Excel = Microsoft.Office.Interop.Excel;

namespace PostCalendarWindows.Calendar
{
    public class Calendar
    {
        public List<Curriculum> currs = new List<Curriculum>();


        public Calendar()
        {

        }

        static DataTable readExecl(string path)
        {
            DataTable dt = new DataTable();
            //不得不说，大微软家的东西可以相互调用就是方便
            Excel.Application app = new Excel.Application();
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
                        string colName = app.ActiveCell.Text.ToString();
                        if (dt.Columns.Contains(colName))
                        {
                            dt.Columns.Add(colName + j);
                        }
                        else
                        {
                            dt.Columns.Add(colName);
                        }
                    }
                    dr[j - 1] = app.ActiveCell.Text.ToString();
                }
                dt.Rows.Add(dr);
            }

            app.Quit();

            //貌似除了直接杀掉，没有其他的办法可以干掉这个打开的excel
            //这真是愚蠢
            //难道微软连这个简单的事情都没办法做好吗？
            Process[] pros = Process.GetProcessesByName("excel");
            foreach(Process proc in pros)
            {
                proc.Kill();
            }

            //将表格开头的两行干掉
            //还有最后一行备注也是
            dt.Rows.RemoveAt(0);
            dt.Rows.RemoveAt(0);
            dt.Rows.RemoveAt(dt.Rows.Count - 1);
            return dt;
        }

        //解析读取到的excel数据的方法
        void Analyse_excel_data(DataTable dt)
        {
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
                if(col.Ordinal > 6)
                {
                    dayOfWeek = 0;
                }
                else
                {
                    dayOfWeek = col.Ordinal + 1;
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
                            if(name == last_curriculum_name)
                            {
                                continue;
                            }
                            else
                            {
                                last_curriculum_name = name;
                                //分别匹配两种情况
                                if(result.Length == 6)
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
                                if(match_result1.Count == 1)
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
                                foreach(System.Text.RegularExpressions.Match m in match_result2)
                                {
                                    class_array.Add(int.Parse(m.Value));
                                }
                                start_time = class_start_time[class_array[0] - 1];
                                end_time = class_start_time[class_array[class_array.Count - 1] - 1].AddMinutes(45);
                                Curriculum c = new Curriculum(name, teacher, place, start_time, end_time, start_week, end_week, dayOfWeek);
                                currs.Add(c);
                            }
                        }
                    }
                }
            }
        }
    }

    //存储课程信息的类
    public class Curriculum
    {
        public struct LastLength
        {
            public TimeOnly start_time;
            public TimeOnly end_time;
        }

        public string name, teacher, place;
        public int start_week, end_week, dayOfWeek;
        public LastLength last_time;

        public Curriculum(string event_name, string _teacher, string _place, TimeOnly start, TimeOnly end, int _start_week, int _end_week, int _dayOfWeek)
        {
            name = event_name;
            teacher = _teacher;
            place = _place;
            last_time.start_time = start;
            last_time.end_time = end;
            start_week = _start_week;
            end_week = _end_week;
            dayOfWeek = _dayOfWeek;
        }
    }


    //转换日历中事件宽度的转换器
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

    //转换滚动条高度的转换器
    public class CalendarHeightConverter : System.Windows.Data.IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo cultureInfo)
        {
            double _value = (double)value;
            if(_value > 60)
            {
                return _value - 60;
            }
            else
            {
                return _value;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo cultureInfo)
        {
            return value;
        }
    }
}
