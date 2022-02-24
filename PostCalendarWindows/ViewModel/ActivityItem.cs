using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PostCalendarWindows.DDL;

namespace PostCalendarWindows.ViewModel
{
    public class ActivityItem : IComparable<ActivityItem>
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public string StartTime { get; private set; }
        public DateTime time { get; private set; }
        public string EndTime
        {
            get
            {
                return time.ToString();
            }
        }
        public string Details { get ; private set; }
        public string AddButtonText { get; private set; }

        /// <summary>
        /// 表示是否为ddl时间点对象
        /// </summary>
        public bool IsDDL;

        /// <summary>
        /// 活动界面的vm类，从ddl时间点对象初始化，StartTime属性设置为空
        /// </summary>
        /// <param name="_event">ddl时间点对象</param>
        public ActivityItem(DeadlineEvent _event)
        {
            IsDDL = true;
            Id = _event.Id;
            Name = _event.Name;
            StartTime = "";
            time = _event.EndDateTime;
            Details = _event.Details;
            AddButtonText = "添加到个人ddl";
        }

        /// <summary>
        /// 活动界面的vm类，从ddl时间段对象初始化
        /// </summary>
        /// <param name="_event">ddl时间段对象</param>
        public ActivityItem(DeadlineSpanEvent _event)
        {
            IsDDL=false;
            Id= _event.Id;
            Name= _event.Name;
            Details = _event.Details;
            StartTime = _event.StartDateTime.ToString();
            time = _event.EndDateTime;
            AddButtonText = "添加到个人日历";
        }

        public int CompareTo(ActivityItem? item)
        {
            if (item == null)
            {
                return 1;
            }
            else
            {
                return this.time.CompareTo(item.time);
            }
        }
    }

}
