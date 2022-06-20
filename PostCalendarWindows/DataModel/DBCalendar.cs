using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

using PostCalendarWindows.Calendar;

namespace PostCalendarWindows.DataModel
{
#pragma warning disable CS8618
    public class CalendarItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Detail { get; set; }
        public string Place { get; set; }
        public string BeginDataString { get; set; }
        public string EndDataString { get; set; }

        /// <summary>
        /// 作为POCO类，必须有一个不接受任何参数的构造函数
        /// </summary>
        public CalendarItem()
        {

        }

        public CalendarItem(CalendarEvent _event)
        {
            Name = _event.Name;
            Place = _event.Place;
            Detail = _event.Details;

            // 处理时间的转换
            DateTime date = new DateTime(
                _event.Date.Year,
                _event.Date.Month,
                _event.Date.Day
                );

            DateTime beginDateTime = date.Add(_event.BeginTime - TimeOnly.MinValue);
            DateTime endDateTime = date.Add(_event.EndTime - TimeOnly.MinValue);

            BeginDataString = beginDateTime.ToString();
            EndDataString = endDateTime.ToString();
        }
    }

    public class CalendarItemContext : DbContext
    {
        public CalendarItemContext()
        {
            Database.EnsureCreated();
        }

        public DbSet<CalendarItem> CalendarItems { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(
                "Data Source=calendarItems.db");
        }

        /// <summary>
        /// 在数据库中创建一个日历对象
        /// </summary>
        /// <param name="_event">日历事件，该事件不需要有id属性</param>
        public void CreateCalendarItem(CalendarEvent _event)
        {
            CalendarItem item = new CalendarItem(_event);
            this.Add<CalendarItem>(item);
            this.SaveChanges();
        }

        /// <summary>
        /// 更新数据库中的一个日历事件
        /// </summary>
        /// <param name="_event">需要更新的日历事件</param>
        public void UpdateCalendarItem(CalendarEvent _event)
        {
            CalendarItem item = new CalendarItem(_event);
            item.Id = _event.Id;
            this.Update(item);
            this.SaveChanges();
        }

        /// <summary>
        /// 从数据库中读取一个日历事件，如果不存在该id返回null
        /// </summary>
        /// <param name="id">读取的日历事件的id</param>
        /// <returns>该日历事件或者null</returns>
        public CalendarEvent? ReadCalendarItem(int id)
        {
            CalendarEvent _event = new CalendarEvent();

            CalendarItem? item = this.CalendarItems.SingleOrDefault(e => e.Id == id);
            if(item == default)
            {
                return null;
            }
            else
            {
                // TODO
                return _event;
            }
        }

        /// <summary>
        /// 从数据库中删除一个日历对象
        /// </summary>
        /// <param name="id">需要删除的日历对象的id</param>
        /// <returns>若真则已删除，反之未删除</returns>
        public bool DeleteCalendarItem(int id)
        {
            CalendarItem? item = this.CalendarItems.SingleOrDefault(e => e.Id == id);
            if(item == default)
            {
                return false;
            }
            else
            {
                this.CalendarItems.Remove(item);
                this.SaveChanges();
                return true;
            }
        }

        /// <summary>
        /// 在数据库中查询一周的日程
        /// </summary>
        /// <param name="sunday">一周第一天的日期，也就是周日</param>
        /// <returns>这一周事件的列表</returns>
        public List<CalendarEvent> LoadDataFromDb(DateOnly sunday)
        {
            List<CalendarEvent> event_list = new List<CalendarEvent>();
            DateOnly monday = sunday.AddDays(6);

            foreach(var item in this.CalendarItems)
            {
                CalendarEvent e = new CalendarEvent();
                e.SetFromDatabase(item);
                event_list.Add(e);
            }

            var query = from item in event_list
                        where item.Date >= sunday && item.Date <= monday
                        select item;

            return query.ToList();
        }
    }
}
