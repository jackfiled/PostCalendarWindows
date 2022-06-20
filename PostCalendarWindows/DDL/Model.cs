using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostCalendarWindows.DDL
{
    public enum ColumnType
    {
        DDL = 0,
        DDLSpan,
    }

    public enum DDLType
    {
        /// <summary>
        /// 所有的类型
        /// </summary>
        All = 0,
        /// <summary>
        /// 学习
        /// </summary>
        Study,
        /// <summary>
        /// 个人
        /// </summary>
        Personal,
        /// <summary>
        /// 其他
        /// </summary>
        Other,
        NotDDL,
    }

    public enum ActivityType
    {
        /// <summary>
        /// 所有的类型
        /// </summary>
        All = 0,
        /// <summary>
        /// 思政
        /// </summary>
        Thought,
        /// <summary>
        /// 文体
        /// </summary>
        PE,
        /// <summary>
        /// 志愿
        /// </summary>
        Volunteer,
        /// <summary>
        /// 讲座
        /// </summary>
        Lecture,
        /// <summary>
        /// 竞赛
        /// </summary>
        Competition,
        /// <summary>
        /// 评优
        /// </summary>
        Recoginition,
        /// <summary>
        /// 其他
        /// </summary>
        Other,
        /// <summary>
        /// 内部变量
        /// </summary>
        NotActivity,
    }

#pragma warning disable CS8618
    public class DeadlineEvent : IComparable<DeadlineEvent>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Details { get; set; }
        public DateTime EndDateTime { get; set; }
        public DDLType ddlType { get; set; }
        public ActivityType activityType { get; set; }

        /// <summary>
        /// 空构造函数
        /// </summary>
        public DeadlineEvent() 
        {
            
        }

        /// <summary>
        /// 程序内部初始化ddl时间点对象
        /// </summary>
        /// <param name="name">事件的名称</param>
        /// <param name="details">事件的详情</param>
        /// <param name="endDateTime">事件的结束时间</param>
        /// <param name="ddlType">事件的ddl分类</param>
        /// <param name="activityType">事件的活动分类</param>
        public DeadlineEvent(string name, string details, DateTime endDateTime, DDLType ddlType, ActivityType activityType)
        {
            Name = name;
            Details = details;
            EndDateTime = endDateTime;
            this.ddlType = ddlType;
            this.activityType = activityType;
        }

        /// <summary>
        /// 从数据库内部存储对象初始化ddl时间点对象
        /// </summary>
        /// <param name="item">数据库中存储ddl时间点的对象</param>
        public DeadlineEvent(DataModel.DeadlineItem item)
        {
            Id = item.Id;
            Name = item.Name;
            Details = item.Detail;
            EndDateTime = DateTime.Parse(item.EndDateTimeString);
            ddlType = (DDLType)item.Type;
            activityType = (ActivityType)item.ActivityType;
        }

        public int CompareTo(DeadlineEvent? e)
        {
            if(e == null)
            {
                return 1;
            }
            else
            {
                return EndDateTime.CompareTo(e.EndDateTime);
            }
        }
    }

    public class DeadlineSpanEvent : IComparable<DeadlineSpanEvent>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Details { get; set; }
        public DateTime StartDateTime { get; set; }
        public DateTime EndDateTime { get; set; }
        public ActivityType activityType { get; set; }

        /// <summary>
        /// 空构造函数
        /// </summary>
        public DeadlineSpanEvent()
        {

        }

        /// <summary>
        /// 程序内部初始化ddl时间段对象
        /// </summary>
        /// <param name="name">事件的名称</param>
        /// <param name="details">事件的详情</param>
        /// <param name="startDateTime">事件的开始时间</param>
        /// <param name="endDateTime">事件的结束时间</param>
        /// <param name="activityType">事件的活动类型</param>
        public DeadlineSpanEvent(string name, string details, DateTime startDateTime, DateTime endDateTime, ActivityType activityType)
        {
            Name = name;
            Details = details;
            StartDateTime = startDateTime;
            EndDateTime = endDateTime;
            this.activityType = activityType;
        }
        
        public DeadlineSpanEvent(DataModel.DeadlineSpanItem item)
        {
            Id = item.Id;
            Name = item.Name;
            Details = item.Detail;
            StartDateTime = DateTime.Parse(item.StartDateTimeString);
            EndDateTime = DateTime.Parse(item.EndDateTimeString);
            activityType = (ActivityType)item.ActivityType;
        }

        public int CompareTo(DeadlineSpanEvent? e)
        {
            if (e == null)
            {
                return 1;
            }
            else
            {
                return EndDateTime.CompareTo(e.EndDateTime);
            }
        }
    }
#pragma warning restore CS8618
}
