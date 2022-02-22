using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PostCalendarWindows.DDL;

#pragma warning disable CS8618

namespace PostCalendarWindows.ViewModel
{
    /// <summary>
    /// 添加时间点的vm
    /// </summary>
    public class DDLDetailItem
    {
        public string TopColumn { get; private set; }
        public string NameColumn { get; private set; }
        public string DetailColumn { get; private set; }
        public string EndTimeColumn { get; private set; }
        public string DDLTypeColumn { get; private set; }
        public string ActivityTypeColumn { get; private set; }

        public string Name { get; set; }
        public string Detail { get; set; }
        public string EndDateStr { get; set; }
        public string EndTimeStr { get; set; }
        public DDLType ddlType { get; set; }
        public ActivityType activityType { get; set; }

        public bool isDDL;

        public DDLDetailItem(bool _isDDL)
        {
            isDDL = _isDDL;
            if (isDDL)
            {
                TopColumn = "添加DDL";
                NameColumn = "DDL名称";
                DetailColumn = "DDL详情";
                EndTimeColumn = "DDL时间";
                DDLTypeColumn = "DDL分类";
                ActivityTypeColumn = "非活动";
            }
            else
            {
                TopColumn = "添加活动";
                NameColumn = "活动名称";
                DetailColumn = "活动详情";
                EndTimeColumn = "活动时间";
                DDLTypeColumn = "非DDL";
                ActivityTypeColumn = "活动分类";
            }
        }

        public DDLDetailItem(bool _isDDL, DeadlineEvent _event)
        {
            if (isDDL)
            {
                NameColumn = "DDL名称";
                DetailColumn = "DDL详情";
                EndTimeColumn = "DDL时间";
                DDLTypeColumn = "DDL分类";
                ActivityTypeColumn = "非活动";

                Name = _event.Name;
                Detail = _event.Details;
                EndDateStr = new DateOnly(_event.EndDateTime.Year, _event.EndDateTime.Month, _event.EndDateTime.Day).ToString();
                EndTimeStr = new TimeOnly(_event.EndDateTime.Hour, _event.EndDateTime.Minute, _event.EndDateTime.Second).ToString();
                ddlType = _event.ddlType;
                activityType = ActivityType.NotActivity;
            }
            else
            {
                NameColumn = "活动名称";
                DetailColumn = "活动详情";
                EndTimeColumn = "活动时间";
                DDLTypeColumn = "非DDL";
                ActivityTypeColumn = "活动分类";

                Name = _event.Name;
                Detail = _event.Details;
                EndDateStr = new DateOnly(_event.EndDateTime.Year, _event.EndDateTime.Month, _event.EndDateTime.Day).ToString();
                EndTimeStr = new TimeOnly(_event.EndDateTime.Hour, _event.EndDateTime.Minute, _event.EndDateTime.Second).ToString();
                ddlType = DDLType.NotDDL;
                activityType = _event.activityType;
            }
        }
    }
}
