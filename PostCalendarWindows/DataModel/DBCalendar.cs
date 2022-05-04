using System.Linq;
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

        }
    }

    public class CalendarItemContext : DbContext
    {
        public DbSet<CalendarItem> CalendarItems { get; set; }

        protected override void OnConfiguring(
            DbContextOptionsBuilder optionsBuilder)
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
    }
}
