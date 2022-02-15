﻿using System;
using System.Collections.Generic;
using System.Linq;
using LinqToDB;
using LinqToDB.Mapping;
using PostCalendarWindows.Calendar;

#pragma warning disable CS8618

namespace PostCalendarWindows.DataModel
{
    [Table(Name = "Calendar" )]
    public class Calendar
    {
        [Column, PrimaryKey, Identity]
        public int Id { get; set; } 
        [Column, NotNull]
        public string Name { get; set; }
        [Column, NotNull]
        public string Place { get; set; }
        [Column]
        public string details { get; set; }
        [Column, NotNull]
        public string Date { get; set; }
        [Column, NotNull]
        public string Begin_time { get; set; }
        [Column, NotNull]
        public string End_time { get; set; }

        /// <summary>
        /// 作为POCO类，必须有一个接受任何参数的构造函数
        /// </summary>
        public Calendar()
        {

        }

        /// <summary>
        /// 从CalendarEvent创建数据库日历对象的构造函数，注意id属性不会被赋值
        /// </summary>
        /// <param name="event">用于创建数据库日历对象的日历对象</param>
        public Calendar(CalendarEvent @event)
        {
            Name = @event.Name;
            Place = @event.Place;
            details = @event.Details;
            Date = @event.Date_string;
            Begin_time = @event.Begin_time_string;
            End_time = @event.End_time_string;
        }
    }

    [Table(Name = "DDL")]
    public class DDL
    {

    }

    //sqlite的系统表，存储了当前已经建立的表的信息
    [Table(Name ="sqlite_master")]
    public class SqliteMaster
    {
        [Column]
        public string type { get; set; }
        [Column]
        public string name { set; get; }
        [Column]
        public string tbl_name { get; set; }
        [Column]
        public int rootpage { get; set; }
        [Column]
        public string sql { get; set; }
    }

    [Table(Name = "CalendarConst")]
    public class CalendarConst
    {
        [Column]
        public string semester { get; set; }
        [Column]
        public string start_data { get; set; }
        [Column]
        public int teaching_week_number { get; set; }
    }


    public class Database : LinqToDB.Data.DataConnection
    {
        //注意，这里的ConnectionPath是在开头带上Data source的字符串
        public Database(string ConnectionPath) : base(ProviderName.SQLite, ConnectionPath)
        {
            //在构造函数中处理如果没有创建表的问题
            var tabelQuery = from item in this.SqliteMaster
                             select item.name;
            var tabel_list = tabelQuery.ToList();
            if (!tabel_list.Contains("Calendar"))
            {
                this.CreateTable<Calendar>();
            }
            if (!tabel_list.Contains("DDL"))
            {
                //this.CreateTable<DDL>();
            }
            if (!tabel_list.Contains("CalendarConst"))
            {
                this.CreateTable<CalendarConst>();
            }
        }

        //数据库中相关表格的定义
        public ITable<SqliteMaster> SqliteMaster => GetTable<SqliteMaster>();
        public ITable<Calendar> Calendar => GetTable<Calendar>();
        public ITable<CalendarConst> CalendarConst => GetTable<CalendarConst>();

        /// <summary>
        /// 在数据库中创建一个日历对象
        /// </summary>
        /// <param name="_event">日历事件，该事件不需要有id属性</param>
        /// <returns>该日历事件在数据库中的编号</returns>
        public int CreateCalendarItem(CalendarEvent _event)
        {
            Calendar clendar = new Calendar(_event);
            return this.InsertWithInt32Identity<Calendar>(clendar);
        }

        /// <summary>
        /// 更新数据库中的一个日历事件
        /// </summary>
        /// <param name="_event">需要更新的日历事件</param>
        public void UpdateCalendarItem(CalendarEvent _event)
        {
            Calendar calendar = new Calendar(_event);
            calendar.Id = _event.Id;
            this.Update<Calendar>(calendar);
        }

        /// <summary>
        /// 从数据库中读取一个日历事件，如果不存在该id返回null
        /// </summary>
        /// <param name="id">读取的日历事件的id</param>
        /// <returns>该日历事件或者null</returns>
        public CalendarEvent? ReadCalendarItem(int id)
        {
            CalendarEvent _event = new CalendarEvent();

            var query = from item in this.Calendar
                        where item.Id == id
                        select item;
            if(query.Count() > 0)
            {
                _event.SetFromDatabase(query.ToList<Calendar>()[0]);
                return _event;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 从数据库中删除一个日历对象
        /// </summary>
        /// <param name="id">需要删除的日历对象的id</param>
        /// <returns>若真则已删除，反之未删除</returns>
        public bool DeleteCalendarItem(int id)
        {
            CalendarEvent? _event = this.ReadCalendarItem(id);

            if(_event != null)
            {
                this.Calendar
                    .Where(c => c.Id == id)
                    .Delete();
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 在数据库中查询一周的日程
        /// </summary>
        /// <param name="sunday">一周第一天的日期</param>
        /// <returns>包含这一周事件的列表</returns>
        public List<CalendarEvent> LoadDataFromDb(DateOnly sunday)
        {
            List<CalendarEvent> events = new List<CalendarEvent>();
            DateOnly monday = sunday.AddDays(6);

            foreach(var item in this.Calendar)
            {
                CalendarEvent e = new CalendarEvent();
                e.SetFromDatabase(item);
                events.Add(e);
            }
            var query = from item in events
                        where item.Date >= sunday && item.Date <= monday
                        select item;

            return query.ToList();
        }

        /// <summary>
        /// 获得这个学期教学周的第一天
        /// </summary>
        /// <param name="semester">学期的名称</param>
        /// <returns>教学周的第一天，如果为null则数据库中不存在这个学期</returns>
        public DateOnly? getSemesterFirstDay(string semester)
        {
            var query = from data in this.CalendarConst
                        where semester == data.semester
                        select data.start_data;
            if(query.Count() == 0)
            {
                return null;
            }
            DateOnly result = DateOnly.Parse(query.ToList()[0]);
            return result;
        }
    }
}
