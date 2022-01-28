using System;
using System.Collections.Generic;
using System.Linq;
using LinqToDB;
using LinqToDB.Mapping;
using PostCalendarWindows.Calendar;

namespace PostCalendarWindows.DataModel
{
    [Table(Name = "Calendar" )]
    public class Calendar
    {
        [Column, PrimaryKey, Identity]
        public int Id { get; set; } 
        [Column, NotNull]
        public string? Name { get; set; }
        [Column, NotNull]
        public string? Place { get; set; }
        [Column]
        public string? details { get; set; }
        [Column, NotNull]
        public string Date { get; set; }
        [Column, NotNull]
        public string Begin_time { get; set; }
        [Column, NotNull]
        public string End_time { get; set; }
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
        public string? type { get; set; }
        [Column]
        public string? name { set; get; }
        [Column]
        public string? tbl_name { get; set; }
        [Column]
        public int rootpage { get; set; }
        [Column]
        public string? sql { get; set; }
    }

    [Table(Name = "Const")]
    public class Const
    {
        [Column]
        public string? semester { get; set; }
        [Column]
        public string? start_data { get; set; }
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
            if (!tabel_list.Contains("Const"))
            {
                this.CreateTable<Const>();
            }
        }

        //数据库中相关表格的定义
        public ITable<SqliteMaster> SqliteMaster => GetTable<SqliteMaster>();
        public ITable<Calendar> Calendar => GetTable<Calendar>();
        public ITable<Const> Const => GetTable<Const>();

        public void LoadDataIntoDb(List<Event> events)
        {
            foreach(var e in events)
            {
                Calendar calendar = new Calendar();
                //复制事件的属性
                calendar.Name = e.Name;
                calendar.Date = e.Date_string;
                calendar.Place = e.Place;
                calendar.Begin_time = e.Begin_time_string;
                calendar.End_time = e.End_time_string;
                calendar.details = e.Details;
                this.Insert<Calendar>(calendar);
            }
        }

        //从数据库查询这一周的日程
        public List<Event> LoadDataFromDb(DateOnly monday)
        {
            List<Event> events = new List<Event>();
            DateOnly sunday = monday.AddDays(6);

            foreach(var item in this.Calendar)
            {
                Event e = new Event();
                e.Name = item.Name;
                e.Place = item.Place;
                e.Details = item.details;
                e.Date = DateOnly.Parse(item.Date);
                e.Begin_time = TimeOnly.Parse(item.Begin_time);
                e.End_time = TimeOnly.Parse(item.End_time);
                events.Add(e);
            }
            var query = from item in events
                        where item.Date >= monday && item.Date <= sunday
                        select item;

            return query.ToList();
        }

        public DateOnly? getSemesterFirstDay(string semester)
        {
            var query = from data in this.Const
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
