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
    public class DeadlineItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Detail { get; set; }
        public string EndDateTimeString { get; set; }
        public int Type { get; set; }
        public int ActivityType { get; set; }

        public DeadlineItem()
        {

        }

        public DeadlineItem(DeadlineEvent _event)
        {
            Name = _event.Name;
            Detail = _event.Details;
            EndDateTimeString = _event.EndDateTime.ToString();
            Type = (int)_event.ddlType;
            ActivityType = (int)_event.activityType;
        }
    }

    public class DeadlineItemContext : DbContext
    {
        public DbSet<DeadlineItem> DeadlineItems { get; set; }


        protected override void OnConfiguring(
            DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(
                "Data Source=deadlineItems.db");
        }

        /// <summary>
        /// 储存一个ddl事件
        /// </summary>
        /// <param name="_event">需要储存的ddl事件对象，不需要id</param>
        public void CreateDeadlineEvent(DeadlineEvent _event)
        {
            DeadlineItem item = new DeadlineItem(_event);
            this.Add<DeadlineItem>(item);
            this.SaveChanges();
        }

        /// <summary>
        /// 更新数据库中的ddl对象
        /// </summary>
        /// <param name="_event">需要更新的对象，需要id</param>
        public void UpdateDeadlineEvent(DeadlineEvent _event)
        {
            DeadlineItem item = new DeadlineItem(_event);
            item.Id = _event.Id;
            this.Update<DeadlineItem>(item);
            this.SaveChanges();
        }

        /// <summary>
        /// 从数据库中读取一个DDL事件
        /// </summary>
        /// <param name="id">需要读取事件的id</param>
        /// <returns>返回该DDL事件</returns>
        public DeadlineEvent? ReadDeadlineEvent(int id)
        {
            DeadlineEvent _event = new DeadlineEvent();

            DeadlineItem? item = this.DeadlineItems.SingleOrDefault(x => x.Id == id);
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
        /// 删除一个数据库中的ddl对象
        /// </summary>
        /// <param name="id">需要删除的ddl对象id</param>
        /// <returns>若为真测删除成功，反之则查无此id</returns>
        public bool DeleteDeadlineItem(int id)
        {
            DeadlineItem? item = this.DeadlineItems.SingleOrDefault(x => x.Id == id);
            if(item == default)
            {
                return false;
            }
            else
            {
                this.DeadlineItems.Remove(item);
                this.SaveChanges();
                return true;
            }
        }
    }

}
