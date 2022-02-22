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

        public void SetFromDatabase(DataModel.DeadLine deadline)
        {
            Id= deadline.Id;
            Name= deadline.Name;
            Details= deadline.Details;
            EndDateTime = DateTime.Parse(deadline.EndDateTimeStr);
            ddlType = (DDLType)deadline.Type;
            activityType = (ActivityType)deadline.ActivityType;
        }

        public void SetInner(int id, string name, string details, DateTime endDateTime, DDLType ddlType, ActivityType activityType)
        {
            Id = id;
            Name = name;
            Details = details;
            EndDateTime = endDateTime;
            this.ddlType = ddlType;
            this.activityType = activityType;
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

        public void SetFromDatabase(DataModel.DeadLineSpan deadline)
        {
            Id = deadline.Id;
            Name = deadline.Name;
            Details = deadline.Details;
            StartDateTime = DateTime.Parse(deadline.StartDateTimeStr);
            EndDateTime = DateTime.Parse(deadline.EndDateTimeStr);
            activityType = (ActivityType)deadline.ActivityType;
        }

        public void SetInner(int id, string name, string details, DateTime startDateTime, DateTime endDateTime, ActivityType activityType)
        {
            Id = id;
            Name = name;
            Details = details;
            StartDateTime = startDateTime;
            EndDateTime = endDateTime;
            this.activityType = activityType;
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
