using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostCalendarWindows.DDL
{
    public enum DDLType
    {
        All = 0,
        Study,
        Personal,
        Other,
        NotDDL,
    }

    public enum ActivityType
    {
        All = 0,
        Thought,
        PE,
        Volunteer,
        Lecture,
        Competition,
        Recoginition,
        Other,
        NotActivity,
    }

#pragma warning disable CS8618
    public class DeadlineEvent
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
            activityType = (ActivityType)deadline.Type;
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

    }

    public class DeadlineSpanEvent
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

    }
#pragma warning restore CS8618
}
