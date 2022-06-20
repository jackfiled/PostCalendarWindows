using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

using PostCalendarWindows.DDL;

#pragma warning disable CS8618

namespace PostCalendarWindows.DataModel
{
    public class DeadlineSpanItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Detail { get; set; }
        public string StartDateTimeString { get; set; }
        public string EndDateTimeString { get; set; }
        public int ActivityType { get; set; }

        public DeadlineSpanItem()
        {

        }

        public DeadlineSpanItem(DeadlineSpanEvent _event)
        {
            Name = _event.Name;
            Detail = _event.Details;
            StartDateTimeString = _event.StartDateTime.ToString();
            EndDateTimeString = _event.EndDateTime.ToString();
            ActivityType = (int)_event.activityType;
        }
    }

    public class DeadlineSpanItemContext : DbContext
    {
        public DeadlineSpanItemContext()
        {
            Database.EnsureCreated();
        }

        public DbSet<DeadlineSpanItem> DeadlineSpanItems { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(
                "Data Source=deadlineSpanItems.db");
        }

        /// <summary>
        /// 在数据库中创建一个ddl时间段对象
        /// </summary>
        /// <param name="_event">需要创建的ddl时间段对象，这里没有id属性</param>
        public void CreateDeadlineSpanItem(DeadlineSpanEvent _event)
        {
            DeadlineSpanItem item = new DeadlineSpanItem(_event);
            this.Add<DeadlineSpanItem>(item);
            this.SaveChanges();
        }

        /// <summary>
        /// 在数据库中更新一个ddl时间段对象
        /// </summary>
        /// <param name="_event">需要更新的ddl时间段对象</param>
        public void UpdateDeadlineSpanItem(DeadlineSpanEvent _event)
        {
            DeadlineSpanItem item = new DeadlineSpanItem(_event);
            item.Id = _event.Id;
            this.Update(item);
            this.SaveChanges();
        }

        /// <summary>
        /// 在数据库中读取一个ddl时间段对象
        /// </summary>
        /// <param name="id">需要读取的时间段对象的id</param>
        /// <returns>该ddl时间段对象</returns>
        public DeadlineSpanEvent? ReadDeadlineSpanItem(int id)
        {
            DeadlineSpanEvent _event = new DeadlineSpanEvent();

            DeadlineSpanItem? item = this.DeadlineSpanItems.SingleOrDefault(x => x.Id == id);
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
        /// 删除一个数据库中的ddl时间段对象
        /// </summary>
        /// <param name="id">需要删除的ddl时间段对象的id</param>
        /// <returns>若真则已删除，反之未删除</returns>
        public bool DeleteDeadlineSpanItem(int id)
        {
            DeadlineSpanItem? item = this.DeadlineSpanItems.SingleOrDefault(x => x.Id == id);
            if(item == default)
            {
                return false;
            }
            else
            {
                this.DeadlineSpanItems.Remove(item);
                this.SaveChanges();
                return true;
            }
        }

        public List<DeadlineSpanEvent> LoadDeadlineSpan(ActivityType type, DateTime time)
        {
            List<DeadlineSpanEvent> deadline_span_events = new();

            foreach(var item in this.DeadlineSpanItems)
            {
                deadline_span_events.Add(new DeadlineSpanEvent(item));
            }

            if(type == ActivityType.All)
            {
                var query = from deadline_span_event in deadline_span_events
                            where deadline_span_event.activityType != ActivityType.NotActivity
                                && deadline_span_event.EndDateTime < time
                            select deadline_span_event;

                return query.ToList();
            }
            else
            {
                var query = from deadline_span_event in deadline_span_events
                            where deadline_span_event.activityType == type
                                && deadline_span_event.EndDateTime < time
                            select deadline_span_event;

                return query.ToList();
            }
        }
    }
}
